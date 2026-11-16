using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Windows.Forms;

namespace BrioPresets {
 static class Program {
  [STAThread] static void Main(string[] args) {
   UiText.Initialize(args);
   if(args.Contains("--self-test")){ SelfTest.Run(); return; }
   bool created;
   using(var mutex=new Mutex(true,"Local\\BrioPresetsControl",out created)) {
    if(!created){MessageBox.Show(UiText.T("Cam Pressets Studio is already running.", "Cam Pressets Studio er allerede åbent."));return;}
    Application.EnableVisualStyles();Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new MainForm());
   }
  }
 }
 public class MainForm:Form {
  readonly ComboBox devices=new ComboBox { DropDownStyle=ComboBoxStyle.DropDownList,Width=330 };
  readonly ListBox presets=new ListBox { Dock=DockStyle.Fill,Font=new Font("Segoe UI",13),IntegralHeight=false };
  readonly TextBox name=new TextBox { Width=240,Text=UiText.T("Center speaker", "Taler midt") };
  readonly NumericUpDown seconds=new NumericUpDown { Minimum=0,Maximum=15,DecimalPlaces=1,Increment=.5m,Value=2,Width=70 };
  readonly Label status=new Label { Dock=DockStyle.Fill,Padding=new Padding(3),AutoEllipsis=true,Text=UiText.T("Select a camera.", "Vælg kamera.") };
  readonly FlowLayoutPanel position=Stack(), advanced=Stack(), special=Stack();
  readonly NumericUpDown panSteps=StepInput(5), zoomSteps=StepInput(5), finePanSteps=StepInput(1), fineZoomSteps=StepInput(1);
  readonly CheckBox reverseHorizontal=new CheckBox { Text=UiText.T("Reverse left / right", "Omvend venstre / højre"),AutoSize=true };
  readonly CheckBox reverseVertical=new CheckBox { Text=UiText.T("Reverse up / down", "Omvend op / ned"),AutoSize=true };
  readonly Label driverSteps=new Label { Text=UiText.T("Select a camera to see its minimum steps.", "Vælg kamera for at se dets mindste trin."),AutoSize=true };
  static NumericUpDown StepInput(int value){return new NumericUpDown {Minimum=1,Maximum=100,Value=value,Width=65};}
  readonly System.Windows.Forms.Timer timer=new System.Windows.Forms.Timer { Interval=33 };
  readonly Stopwatch clock=new Stopwatch();
  readonly Dictionary<string,TrackBar> sliders=new Dictionary<string,TrackBar>();
  readonly Dictionary<string,CheckBox> autos=new Dictionary<string,CheckBox>();
  readonly Dictionary<string,Label> values=new Dictionary<string,Label>();
  ProfileControls profiles;
  StartupCoordinator startup;
  bool settingsDirty,applyingProfile;
  string path {get{return profiles.FilePath;}}

  SavedData saved=new SavedData(); List<Device> available=new List<Device>(); Camera camera;
  Dictionary<string,int> start,target,last; double duration; bool updating,storageReady=true;
  static readonly string[] axes={"Zoom","Pan","Tilt"};
  static FlowLayoutPanel Stack(){return new FlowLayoutPanel { Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,WrapContents=false,AutoScroll=true,Padding=new Padding(10) };}
  static FlowLayoutPanel Row(){return new FlowLayoutPanel { AutoSize=true,WrapContents=true,Margin=new Padding(0,3,0,3) };}
  Button Button(string text,Action action,int width=135) {
   var b=new BlueButton { Text=text,Width=width,Height=42,FlatStyle=FlatStyle.Flat,Margin=new Padding(3) };
   b.Click+=(s,e)=>Safe(action); return b;
  }
  readonly PanWheel wheel=new PanWheel {Dock=DockStyle.Fill};
  readonly NumericUpDown fastPanSteps=StepInput(32);
  readonly CheckBox alwaysTop=new CheckBox {Text=UiText.T("Always on top","Altid øverst"),AutoSize=true};
  bool cameraApproved;
  readonly CheckBox waitForStartup=new CheckBox {Text=UiText.T("Enable camera only after startup programs and Zoom meeting","Aktivér først kamera efter programmer og Zoom-møde"),Checked=true,AutoSize=true,MaximumSize=new Size(285,0)};
  readonly CheckBox launchPrograms=new CheckBox {Text=UiText.T("Start programs at startup","Start programmer ved opstart"),AutoSize=true};
  readonly Label programSummary=new Label {AutoSize=true,MaximumSize=new Size(300,0)};
  readonly System.Windows.Forms.Timer manualTimer=new System.Windows.Forms.Timer {Interval=150};
  string heldAxis;int heldDirection;bool heldFast;
  TableLayoutPanel body;BluePanel settingsPanel;Label cameraStatus;
  bool panelOpen;
  public MainForm() {
   Text="Cam Pressets Studio";ClientSize=new Size(1030,720);MinimumSize=new Size(720,540);
   Font=new Font("Segoe UI",11);BackColor=BlueTheme.Background;ForeColor=Color.White;AutoScaleMode=AutoScaleMode.Dpi;
   Icon=Icon.ExtractAssociatedIcon(Application.ExecutablePath);
   var root=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=3,Padding=new Padding(12),BackColor=BlueTheme.Background};
   root.RowStyles.Add(new RowStyle(SizeType.Absolute,65));root.RowStyles.Add(new RowStyle(SizeType.Percent,100));root.RowStyles.Add(new RowStyle(SizeType.Absolute,62));
   var header=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=3};header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,55));header.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,55));
   header.Controls.Add(new Label {Text="Cam Pressets Studio",Font=new Font("Segoe UI",25,FontStyle.Bold),Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleLeft},0,0);
   header.Controls.Add(Button("⚙",ToggleSettings,50),1,0);header.Controls.Add(Button("?",()=>MessageBox.Show(this,UiText.T("Inner ring: normal steps. Outer ring: fast steps. Hold a button to repeat. Arrow keys move the focused wheel; Shift uses fast steps. Escape stops movement.\n\nSave current position reads the camera. Select a row, then ▶ to recall or ✎ to rename. Presets store camera position only, not screen sharing.\n\nSettings code protects startup-program editing only.","Inderste ring: normale trin. Yderste ring: hurtige trin. Hold knappen nede for at gentage. Piletaster styrer hjulet, når det har fokus; Shift giver hurtige trin. Escape stopper.\n\nGem aktuel position aflæser kameraet. Vælg en række, og brug ▶ for at hente eller ✎ for at omdøbe. Presets gemmer kun kameraposition, ikke skærmdeling.\n\nIndstillingskoden beskytter kun redigering af opstartsprogrammer."),Text),50),2,0);root.Controls.Add(header,0,0);
   body=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=3,RowCount=1,BackColor=BlueTheme.Background};
   body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,43));body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,57));body.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,0));
   var motion=new TableLayoutPanel {Dock=DockStyle.Fill,RowCount=3,ColumnCount=1};motion.RowStyles.Add(new RowStyle(SizeType.Percent,100));motion.RowStyles.Add(new RowStyle(SizeType.Absolute,66));motion.RowStyles.Add(new RowStyle(SizeType.Absolute,30));motion.Controls.Add(wheel,0,0);
   var zoomRow=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=3,Padding=new Padding(30,0,30,0)};foreach(var n in new[]{32,36,32})zoomRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,n));
   var minus=Button("−",()=>{},70);var plus=Button("+",()=>{},70);minus.Font=plus.Font=new Font("Segoe UI",25,FontStyle.Bold);minus.Dock=plus.Dock=DockStyle.Fill;
   zoomRow.Controls.Add(minus,0,0);zoomRow.Controls.Add(new Label {Text="Zoom",Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleCenter,Font=new Font("Segoe UI",16,FontStyle.Bold)},1,0);zoomRow.Controls.Add(plus,2,0);motion.Controls.Add(zoomRow,0,1);
   motion.Controls.Add(new Label {Text=UiText.T("Inner: normal · Outer: fast","Indre: normal · Ydre: hurtig"),Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleCenter},0,2);body.Controls.Add(motion,0,0);
   wheel.MovementRequested+=(axis,direction,fast)=>Safe(()=>BeginHeld(axis,direction,fast));wheel.Released+=EndHeld;
   foreach(var pair in new[]{Tuple.Create(minus,-1),Tuple.Create(plus,1)}) {var button=pair.Item1;int direction=pair.Item2;button.MouseDown+=(o,e)=>{if(e.Button==MouseButtons.Left)Safe(()=>BeginHeld("Zoom",direction,false));};button.MouseUp+=(o,e)=>EndHeld();button.MouseCaptureChanged+=(o,e)=>{if(!button.Capture)EndHeld();};button.KeyDown+=(o,e)=>{if(e.KeyCode==Keys.Space)Safe(()=>Nudge("Zoom",direction));};}
   var center=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=2,Padding=new Padding(8)};center.RowStyles.Add(new RowStyle(SizeType.Absolute,50));center.RowStyles.Add(new RowStyle(SizeType.Percent,100));
   profiles=new ProfileControls(this,ApplyProfile,()=>settingsDirty,()=>timer.Enabled||manualTimer.Enabled||devices.DroppedDown||panelOpen,text=>status.Text=text,ToggleSettings);
   startup=new StartupCoordinator(this,text=>status.Text=text);profiles.ProfileSelected+=()=>startup.Start(saved);center.Controls.Add(profiles,0,0);
   var card=new BluePanel {Dock=DockStyle.Fill};var presetLayout=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=1,RowCount=3};presetLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,44));presetLayout.RowStyles.Add(new RowStyle(SizeType.Percent,100));presetLayout.RowStyles.Add(new RowStyle(SizeType.Absolute,60));
   presetLayout.Controls.Add(new Label {Text="☷  Presets",Font=new Font("Segoe UI",19,FontStyle.Bold),Dock=DockStyle.Fill},0,0);
   presets.DrawMode=DrawMode.OwnerDrawFixed;presets.ItemHeight=62;presets.BorderStyle=BorderStyle.None;presets.BackColor=BlueTheme.Panel;presets.ForeColor=Color.White;presets.DrawItem+=DrawPreset;presets.MouseClick+=(o,e)=>{int index=presets.IndexFromPoint(e.Location);if(index<0)return;presets.SelectedIndex=index;if(e.X>presets.ClientSize.Width-45)Safe(RenamePreset);else if(e.X>presets.ClientSize.Width-90)Safe(Recall);};presetLayout.Controls.Add(presets,0,1);
   var saveRow=new TableLayoutPanel {Dock=DockStyle.Fill,ColumnCount=2};saveRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));saveRow.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,56));
   var save=Button(UiText.T("Save current position","Gem aktuel position"),()=>{NeedCamera();string title=Prompt(UiText.T("Preset name","Navn på position"),"");if(title!=null){name.Text=title;SavePreset();}},230);((BlueButton)save).Primary=true;save.Dock=DockStyle.Fill;saveRow.Controls.Add(save,0,0);var del=Button("⌫",Delete,48);del.Dock=DockStyle.Fill;saveRow.Controls.Add(del,1,0);presetLayout.Controls.Add(saveRow,0,2);card.Controls.Add(presetLayout);center.Controls.Add(card,0,1);body.Controls.Add(center,1,0);
   settingsPanel=new BluePanel {Dock=DockStyle.Fill,Visible=false};var tabs=new TabControl {Dock=DockStyle.Fill};
   var generalPage=new TabPage(UiText.T("Settings","Indstillinger"));var imagePage=new TabPage(UiText.T("Image","Billede"));generalPage.Controls.Add(special);imagePage.Controls.Add(advanced);tabs.TabPages.Add(generalPage);tabs.TabPages.Add(imagePage);settingsPanel.Controls.Add(tabs);body.Controls.Add(settingsPanel,2,0);BuildSpecial();root.Controls.Add(body,0,1);
   var footer=new TableLayoutPanel {Dock=DockStyle.Fill,RowCount=2};footer.RowStyles.Add(new RowStyle(SizeType.Absolute,28));footer.RowStyles.Add(new RowStyle(SizeType.Percent,100));cameraStatus=new Label {Dock=DockStyle.Fill,Text=UiText.T("Camera: not connected","Kamera: ikke tilsluttet")};footer.Controls.Add(cameraStatus,0,0);footer.Controls.Add(status,0,1);root.Controls.Add(footer,0,2);Controls.Add(root);
   BlueTheme.Apply(root);presets.BackColor=BlueTheme.Panel;root.BackColor=body.BackColor=motion.BackColor=wheel.BackColor=BlueTheme.Background;
   advanced.SizeChanged+=(o,e)=>FitSettings(advanced);special.SizeChanged+=(o,e)=>FitSettings(special);
   KeyPreview=true;KeyDown+=(o,e)=>{if(e.KeyCode==Keys.Escape){Stop();e.Handled=true;}};Deactivate+=(o,e)=>Stop();
   devices.SelectedIndexChanged+=(o,e)=>Safe(()=>{if(!applyingProfile)settingsDirty=true;Connect();});
   presets.SelectedIndexChanged+=(o,e)=>{if(presets.SelectedItem is Preset)name.Text=((Preset)presets.SelectedItem).Name;};
   presets.KeyDown+=(o,e)=>{if(e.KeyCode==Keys.Enter){Safe(Recall);e.Handled=true;}if(e.KeyCode==Keys.F2)Safe(RenamePreset);};
   timer.Tick+=(o,e)=>Safe(Tick);manualTimer.Tick+=(o,e)=>Safe(()=>Nudge(heldAxis,heldDirection,heldFast));
   seconds.ValueChanged+=(o,e)=>{if(!applyingProfile)settingsDirty=true;};
   foreach(var input in new[]{panSteps,zoomSteps,finePanSteps,fineZoomSteps,fastPanSteps})input.ValueChanged+=(o,e)=>{if(!applyingProfile)settingsDirty=true;};
   reverseHorizontal.CheckedChanged+=(o,e)=>{if(!applyingProfile)settingsDirty=true;};reverseVertical.CheckedChanged+=(o,e)=>{if(!applyingProfile)settingsDirty=true;};alwaysTop.CheckedChanged+=(o,e)=>{TopMost=alwaysTop.Checked;if(!applyingProfile)settingsDirty=true;};
   Shown+=(o,e)=>{LoadData();if(storageReady){Safe(Reload);startup.Start(saved);}};
   FormClosing+=(o,e)=>{Stop();if(settingsDirty){var answer=MessageBox.Show(this,UiText.T("Save changed settings before closing?","Gem ændrede indstillinger før programmet lukkes?"),Text,MessageBoxButtons.YesNoCancel);if(answer==DialogResult.Cancel){e.Cancel=true;return;}if(answer==DialogResult.Yes)try{WriteData();}catch(Exception ex){MessageBox.Show(this,ex.Message);e.Cancel=true;}}};
   FormClosed+=(o,e)=>{Stop();timer.Dispose();manualTimer.Dispose();if(camera!=null)camera.Dispose();foreach(var d in available)d.Dispose();};
  }
  void ToggleSettings(){EndHeld();panelOpen=!panelOpen;settingsPanel.Visible=panelOpen;body.ColumnStyles[2].Width=panelOpen?350:0;MinimumSize=new Size(panelOpen?1040:720,540);if(panelOpen&&Width<1180)Width=1180;FitSettings(special);}
  void BeginHeld(string axis,int direction,bool fast){EndHeld();heldAxis=axis;heldDirection=direction;heldFast=fast;Nudge(axis,direction,fast);manualTimer.Start();}
  void EndHeld(){manualTimer.Stop();}
  void DrawPreset(object sender,DrawItemEventArgs e){if(e.Index<0)return;var g=e.Graphics;bool selected=(e.State&DrawItemState.Selected)!=0;var r=new Rectangle(e.Bounds.X+2,e.Bounds.Y+3,e.Bounds.Width-5,e.Bounds.Height-6);g.SmoothingMode=System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
   using(var path=BlueTheme.Round(r,8))using(var brush=new System.Drawing.Drawing2D.LinearGradientBrush(r,selected?Color.FromArgb(0,143,250):BlueTheme.Panel,selected?Color.FromArgb(0,79,161):BlueTheme.Background,90))using(var pen=new Pen(BlueTheme.Line)){g.FillPath(brush,path);g.DrawPath(pen,path);}
   TextRenderer.DrawText(g,(e.Index+1).ToString(),presets.Font,new Rectangle(r.X+4,r.Y,40,r.Height),Color.White,TextFormatFlags.VerticalCenter|TextFormatFlags.HorizontalCenter);
   TextRenderer.DrawText(g,((Preset)presets.Items[e.Index]).Name,presets.Font,new Rectangle(r.X+48,r.Y,Math.Max(1,r.Width-140),r.Height),Color.White,TextFormatFlags.VerticalCenter|TextFormatFlags.EndEllipsis);
   TextRenderer.DrawText(g,"▶",presets.Font,new Rectangle(r.Right-88,r.Y,42,r.Height),Color.White,TextFormatFlags.VerticalCenter|TextFormatFlags.HorizontalCenter);TextRenderer.DrawText(g,"✎",presets.Font,new Rectangle(r.Right-44,r.Y,42,r.Height),Color.White,TextFormatFlags.VerticalCenter|TextFormatFlags.HorizontalCenter);e.DrawFocusRectangle();}
  string Prompt(string title,string value){using(var f=new Form {Text=title,ClientSize=new Size(370,130),StartPosition=FormStartPosition.CenterParent,FormBorderStyle=FormBorderStyle.FixedDialog,MaximizeBox=false,MinimizeBox=false}){var input=new TextBox {Left=15,Top=20,Width=340,Text=value,MaxLength=60};var ok=new Button {Text=UiText.T("Save","Gem"),Left=170,Top=75,DialogResult=DialogResult.OK};var cancel=new Button {Text=UiText.T("Cancel","Annuller"),Left=260,Top=75,DialogResult=DialogResult.Cancel};f.Controls.AddRange(new Control[]{input,ok,cancel});f.AcceptButton=ok;f.CancelButton=cancel;BlueTheme.Apply(f);return f.ShowDialog(this)==DialogResult.OK&&!string.IsNullOrWhiteSpace(input.Text)?input.Text.Trim():null;}}
  void RenamePreset(){var p=presets.SelectedItem as Preset;if(p==null)return;string title=Prompt(UiText.T("Rename preset","Omdøb position"),p.Name);if(title==null||title==p.Name)return;if(Current.Any(x=>x!=p&&string.Equals(x.Name,title,StringComparison.OrdinalIgnoreCase)))throw new InvalidOperationException(UiText.T("Name already exists.","Navnet findes allerede."));string old=p.Name;p.Name=title;try{WriteData();}catch{p.Name=old;throw;}ListPresets();presets.SelectedItem=p;}
  void FitSettings(FlowLayoutPanel panel) {
   int width=Math.Max(180,panel.ClientSize.Width-panel.Padding.Horizontal-SystemInformation.VerticalScrollBarWidth-4);
   foreach(Control child in panel.Controls) {
    if(child is TableLayoutPanel || child is FlowLayoutPanel)child.Width=width;
    if(child is FlowLayoutPanel)child.MaximumSize=new Size(width,0);
    if(child is Label)child.MaximumSize=new Size(width,0);
   }
  }
  void Safe(Action action) {try{action();}catch(Exception ex){Stop();status.Text=UiText.T("Error: ", "Fejl: ")+ex.Message;MessageBox.Show(this,ex.Message+UiText.T("\n\nIf the camera is busy, close Logitech Control and try again.", "\n\nHvis kameraet er optaget, luk Logitech Control og prøv igen."),"Cam Pressets Studio",MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
  void Stop(){EndHeld();timer.Stop();clock.Stop();status.Text=UiText.T("Movement stopped.", "Bevægelse stoppet.");}
  void DisconnectCamera() {
   Stop();if(camera!=null){camera.Dispose();camera=null;}wheel.Enabled=false;
   advanced.Controls.Clear();sliders.Clear();autos.Clear();values.Clear();
   cameraStatus.Text=UiText.T("Camera disconnected — waiting for Zoom meeting","Kamera frakoblet — afventer Zoom-møde");
   status.Text=UiText.T("Start the listed programs and join the Zoom meeting. Then enable the camera in Settings.","Start programmerne i listen, og gå ind i Zoom-mødet. Aktivér derefter kameraet under Indstillinger.");
  }
  bool CameraAllowed() {
   if(!storageReady)return false;
   if(!waitForStartup.Checked)return true;
   if(!cameraApproved)return false;
   if(CameraStartupGate.Missing(saved.StartupPrograms,new WindowsProgramRuntime()).Count==0)return true;
   cameraApproved=false;return false;
  }
  void ConfirmCameraReady() {
   if(!storageReady)throw new IOException(UiText.T("Load a profile first.","Indlæs først en profil."));
   var missing=CameraStartupGate.Missing(saved.StartupPrograms,new WindowsProgramRuntime());
   if(missing.Count>0)throw new InvalidOperationException(UiText.T("These programs must be running first: ","Disse programmer skal køre først: ")+string.Join(", ",missing));
   if(MessageBox.Show(this,UiText.T("Is the Zoom meeting already started or joined, and is the camera image correct in Zoom?\n\nOnly choose Yes after checking. A running Zoom process is not enough.","Er Zoom-mødet startet eller tilsluttet, og vises kamerabilledet korrekt i Zoom?\n\nVælg kun Ja efter at have kontrolleret det. Det er ikke nok, at Zoom-programmet er åbent."),UiText.T("Confirm Zoom meeting","Bekræft Zoom-møde"),MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)!=DialogResult.Yes)return;
   cameraApproved=true;Reload();
  }
  void Reload() {
   if(!CameraAllowed()){DisconnectCamera();return;}
   Stop(); bool priorApplying=applyingProfile;applyingProfile=true;try {if(camera!=null)camera.Dispose();camera=null;
   devices.Items.Clear();foreach(var d in available)d.Dispose();available=Camera.Enumerate();
   devices.Items.AddRange(available.ToArray());
   if(available.Count>0){int n=available.FindIndex(d=>d.Id==saved.CameraId);if(n<0)n=available.FindIndex(d=>d.Name.IndexOf("BRIO",StringComparison.OrdinalIgnoreCase)>=0);devices.SelectedIndex=Math.Max(0,n);}
   else {position.Controls.Clear();advanced.Controls.Clear();presets.Items.Clear();status.Text=UiText.T("No camera found. Connect BRIO and click Find cameras.", "Intet kamera fundet. Tilslut BRIO, og tryk Find kameraer.");cameraStatus.Text=UiText.T("Camera: not connected","Kamera: ikke tilsluttet");}

   }finally{applyingProfile=priorApplying;}
  }
  string DeviceId { get {var d=devices.SelectedItem as Device;if(d==null)throw new InvalidOperationException(UiText.T("Select a camera first.", "Vælg et kamera først."));return d.Id;} }
  List<Preset> Current {get {string id=DeviceId;if(!saved.Cameras.ContainsKey(id))saved.Cameras[id]=new List<Preset>();return saved.Cameras[id];}}
  void NeedCamera(){if(camera==null)throw new InvalidOperationException(UiText.T("Select and connect the camera first.", "Vælg og tilslut kameraet først."));}
  void Connect() {
   if(!CameraAllowed()){DisconnectCamera();return;}
   Stop();if(camera!=null)camera.Dispose();camera=null;position.Controls.Clear();advanced.Controls.Clear();sliders.Clear();autos.Clear();values.Clear();presets.Items.Clear();
   var d=devices.SelectedItem as Device;if(d==null)return;
   camera=new Camera(d);wheel.Enabled=true;
   driverSteps.Text=UiText.T("Minimum camera steps: ", "Mindste kameratrin: ")+string.Join(" · ",axes.Select(k=>k+": "+(camera.Settings.ContainsKey(k)?camera.Settings[k].Step.ToString():UiText.T("not supported", "ikke understøttet"))))+UiText.T(". Values use the driver units.", ". Tallene er driverens enheder.");
   foreach(var key in new[]{UiText.T("Brightness", "Lysstyrke"),UiText.T("Contrast", "Kontrast"),UiText.T("Saturation", "Farveintensitet"),UiText.T("Focus", "Fokus"),UiText.T("White balance", "Hvidbalance"),UiText.T("Exposure", "Eksponering")})AddSlider(advanced,key);
   advanced.Controls.Add(new Label { AutoSize=true,MaximumSize=new Size(440,0),Text=UiText.T("HDR, field of view (65/78/90 degrees), 50/60 Hz and aspect ratio are not controlled by this version. Set them in Logitech Control first, then close it. Presets store position and zoom only.", "HDR, synsfelt (65°/78°/90°), 50/60 Hz og billedformat styres ikke af denne version. Vælg dem i Logitech Control først, og luk derefter programmet. Presets gemmer kun position og zoom.") });
   BlueTheme.Apply(advanced);RefreshValues();ListPresets();FitSettings(advanced);cameraStatus.Text="●  "+d.Name;status.Text=UiText.T("Connected: ", "Tilsluttet: ")+d.Name;
  }
  void AddSlider(FlowLayoutPanel panel,string key) {
   Setting setting;
   if(!camera.Settings.TryGetValue(key,out setting) || (setting.Caps&2)==0) {panel.Controls.Add(new Label { Text=key+UiText.T(": not available through the driver", ": ikke tilgængelig via driveren"),AutoSize=true });return;}
   var row=new TableLayoutPanel { Height=39,Width=290,ColumnCount=3,RowCount=1,Margin=new Padding(0,3,0,3) };
   row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100));row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,52));
   var label=new Label { Dock=DockStyle.Fill,Text=key,TextAlign=ContentAlignment.MiddleLeft };
   var slider=new TrackBar { Minimum=0,Maximum=1000,TickStyle=TickStyle.None,Dock=DockStyle.Fill,SmallChange=1,LargeChange=25,Margin=Padding.Empty };
   var val=new Label { Dock=DockStyle.Fill,TextAlign=ContentAlignment.MiddleRight };row.Controls.Add(label,0,0);row.Controls.Add(slider,1,0);row.Controls.Add(val,2,0);panel.Controls.Add(row);
   sliders[key]=slider;values[key]=val;
   slider.Scroll+=(s,e)=>Safe(()=>{if(updating)return;Stop();camera.Set(key,setting.Min+slider.Value/1000.0*((double)setting.Max-setting.Min));RefreshValues();});
   if((setting.Caps&1)!=0) {
    var auto=new CheckBox { Text=UiText.T("Automatic ", "Automatisk ")+key.ToLowerInvariant(),AutoSize=true };autos[key]=auto;panel.Controls.Add(auto);
    auto.CheckedChanged+=(s,e)=>Safe(()=>{if(updating)return;Stop();camera.Set(key,camera.Get(key),auto.Checked);RefreshValues();});
   }
  }
  void RefreshValues() {
   NeedCamera();updating=true;
   try {foreach(var pair in sliders) {var s=camera.Settings[pair.Key];int flags;int v=camera.Get(pair.Key,out flags);
    pair.Value.Value=Math.Max(0,Math.Min(1000,(int)Math.Round((v-(double)s.Min)/(s.Max-(double)s.Min)*1000)));
    values[pair.Key].Text=v.ToString();pair.Value.Enabled=(flags&1)==0;
    if(autos.ContainsKey(pair.Key))autos[pair.Key].Checked=(flags&1)!=0;
   }}finally{updating=false;}
  }
  Dictionary<string,int> ReadPose() {
   NeedCamera();var pose=new Dictionary<string,int>();foreach(string k in axes)if(camera.Settings.ContainsKey(k)&&(camera.Settings[k].Caps&2)!=0)pose[k]=camera.Get(k);
   if(pose.Count==0)throw new InvalidOperationException(UiText.T("The driver does not provide position or zoom controls.", "Driveren stiller ikke position eller zoom til rådighed."));return pose;
  }
  void Nudge(string key,int direction,bool fine=false) {
   timer.Stop();clock.Stop();NeedCamera();if(!camera.Settings.ContainsKey(key))throw new InvalidOperationException(key+UiText.T(" is not supported.", " understøttes ikke."));
   int count=(int)(key=="Zoom"?(fine?fineZoomSteps.Value:zoomSteps.Value):(fine?fastPanSteps.Value:panSteps.Value));
   bool reverse=key=="Pan"?reverseHorizontal.Checked:key=="Tilt"&&reverseVertical.Checked;
   int current=camera.Get(key),target=ManualMotion.Target(camera.Settings[key],current,direction,count,reverse);
   camera.Set(key,target);RefreshValues();status.Text=target==current?UiText.T("Camera limit reached.", "Kameraets grænse er nået."):key+": "+camera.Get(key)+" · "+count+UiText.T(" camera steps", " kameratrin");
  }
  void AddPanRow(string text,bool fine) {
   var row=Row();row.Controls.Add(new Label {Text=text,Width=62,Margin=new Padding(0,8,0,0)});
   row.Controls.Add(Button("◀",()=>Nudge("Pan",-1,fine),44));row.Controls.Add(Button("▶",()=>Nudge("Pan",1,fine),44));
   row.Controls.Add(Button("▲",()=>Nudge("Tilt",1,fine),44));row.Controls.Add(Button("▼",()=>Nudge("Tilt",-1,fine),44));position.Controls.Add(row);
  }
  void BuildSpecial() {
   special.Padding=new Padding(8);
   special.Controls.Add(new Label {Text=UiText.T("Camera movement (steps)","Kamerabevægelse (steps)"),AutoSize=true,Font=new Font(Font,FontStyle.Bold)});
   foreach(var pair in new[]{Tuple.Create(UiText.T("Normal steps","Normale trin"),panSteps),Tuple.Create(UiText.T("Fast steps","Hurtige trin"),fastPanSteps),Tuple.Create(UiText.T("Zoom steps","Zoomtrin"),zoomSteps)}){
    var row=new TableLayoutPanel {Height=66,Width=300,ColumnCount=2,RowCount=2};row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));row.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,64));
    row.Controls.Add(new Label {Text=pair.Item1,AutoSize=true},0,0);row.SetColumnSpan(row.Controls[0],2);
    var slider=new TrackBar {Minimum=1,Maximum=100,Value=(int)pair.Item2.Value,TickStyle=TickStyle.None,Dock=DockStyle.Fill};var number=pair.Item2;number.Dock=DockStyle.Fill;row.Controls.Add(slider,0,1);row.Controls.Add(number,1,1);slider.ValueChanged+=(o,e)=>number.Value=slider.Value;number.ValueChanged+=(o,e)=>slider.Value=(int)number.Value;special.Controls.Add(row);
   }
   var timing=Row();timing.Controls.Add(new Label {Text=UiText.T("Transition (sec.)","Skiftetid (sek.)"),AutoSize=true});timing.Controls.Add(seconds);special.Controls.Add(timing);
   special.Controls.Add(reverseHorizontal);special.Controls.Add(reverseVertical);
   special.Controls.Add(new Label {Text=UiText.T("Startup programs","Start programmer"),Font=new Font(Font,FontStyle.Bold),AutoSize=true});special.Controls.Add(programSummary);
   special.Controls.Add(Button(UiText.T("Edit programs / settings code…","Redigér programmer / kode…"),OpenStartupSettings,290));
   special.Controls.Add(launchPrograms);launchPrograms.Click+=(o,e)=>Safe(()=>{bool wanted=launchPrograms.Checked;launchPrograms.Checked=saved.AutoStartPrograms;if(!StartupSettingsDialog.Unlock(this,saved))return;bool old=saved.AutoStartPrograms;saved.AutoStartPrograms=wanted;try{WriteData();}catch{saved.AutoStartPrograms=old;throw;}});
   special.Controls.Add(waitForStartup);
   waitForStartup.Click+=(o,e)=>Safe(()=>{cameraApproved=false;DisconnectCamera();settingsDirty=true;if(!waitForStartup.Checked)Reload();});
   special.Controls.Add(Button(UiText.T("Zoom meeting is ready — enable camera","Zoom-mødet er klar — aktivér kamera"),ConfirmCameraReady,290));
   special.Controls.Add(new Label {Text=UiText.T("Camera","Kamera"),Font=new Font(Font,FontStyle.Bold),AutoSize=true});devices.Width=280;special.Controls.Add(devices);special.Controls.Add(Button(UiText.T("Find cameras","Find kameraer"),Reload,200));special.Controls.Add(alwaysTop);
   special.Controls.Add(Button(UiText.T("Save settings","Gem indstillinger"),()=>{WriteData();status.Text=UiText.T("Settings saved.","Indstillinger gemt.");},230));
   special.Controls.Add(driverSteps);
  }
  void UpdateProgramSummary(){launchPrograms.Checked=saved.AutoStartPrograms;programSummary.Text=(saved.StartupPrograms.Count==0?UiText.T("No programs selected. Add JW Library, Zoom or other programs.","Ingen programmer valgt. Tilføj JW Library, Zoom eller andre programmer."):string.Join("\n",saved.StartupPrograms.Select((p,i)=>(i+1)+". "+p.Name)))+"\n"+UiText.T("Start automatically: ","Start automatisk: ")+(saved.AutoStartPrograms?UiText.T("On","Til"):UiText.T("Off","Fra"));}
  void Center(){var pose=ReadPose();foreach(string k in new[]{"Pan","Tilt"})if(pose.ContainsKey(k))pose[k]=camera.Settings[k].Snap(0);BeginMove(pose);}
  void Recall(){var p=presets.SelectedItem as Preset;if(p==null)throw new InvalidOperationException(UiText.T("Select a preset.", "Vælg en gemt position."));BeginMove(p.Position);}
  void BeginMove(Dictionary<string,int> pose) {
   Stop();start=ReadPose();target=new Dictionary<string,int>();last=new Dictionary<string,int>(start);
   foreach(var pair in pose) {
    if(!start.ContainsKey(pair.Key))throw new InvalidOperationException(UiText.T("The preset needs a control not available through this driver: ", "Preset kræver en kontrol, som driveren ikke tilbyder: ")+pair.Key);
    int v=camera.Settings[pair.Key].Snap(pair.Value);
    if(v!=pair.Value)throw new InvalidOperationException(UiText.T("The camera range has changed. Save this preset again.", "Kameraets område er ændret. Gem positionen igen."));
    target[pair.Key]=v;
   }
   if(target.Count==0)throw new InvalidOperationException(UiText.T("The preset is empty.", "Positionen er tom."));
   duration=(double)seconds.Value;clock.Restart();timer.Start();Tick();
  }
  void Tick() {
   double t=duration<=0?1:Math.Min(1,clock.Elapsed.TotalSeconds/duration);double f=Motion.Ease(t);
   foreach(string key in axes)if(target.ContainsKey(key)) {
    int value=camera.Settings[key].Snap(start[key]+(target[key]-(double)start[key])*f);
    if(value!=last[key] || t>=1){camera.Set(key,value);last[key]=value;}
   }
   status.Text=UiText.T("Moving to preset … ", "Skifter position … ")+(int)(t*100)+" %";
   if(t>=1){timer.Stop();clock.Stop();RefreshValues();
    bool reached=target.All(pair=>camera.Get(pair.Key)==pair.Value);
    status.Text=reached?UiText.T("Preset recalled. Camera step size determines how smooth the movement looks.", "Position valgt. Kameraets trinstørrelse bestemmer, hvor glidende skiftet ses."):UiText.T("The camera did not reach every target value. Check the image and camera zoom limits.", "Kameraet nåede ikke alle målværdier. Kontrollér billedet og kameraets zoombegrænsninger.");
   }
  }
  void ListPresets(){presets.Items.Clear();presets.Items.AddRange(Current.ToArray());cameraStatus.Text="●  "+devices.SelectedItem+"   |   "+profiles.SelectedName+"   |   Presets: "+presets.Items.Count;}
  void SavePreset() {
   if(!storageReady)throw new InvalidOperationException(UiText.T("The preset file could not be read. Move or repair it first: ", "Presetfilen kunne ikke læses. Flyt eller reparer den først: ")+path);
   Stop();string text=name.Text.Trim();if(text.Length==0 || text.Length>60)throw new InvalidOperationException(UiText.T("Enter a name of 1–60 characters.", "Skriv et navn på 1–60 tegn."));
   var pose=ReadPose();var old=Current.FirstOrDefault(p=>string.Equals(p.Name,text,StringComparison.OrdinalIgnoreCase));
   if(old!=null && MessageBox.Show(this,UiText.T("Replace preset '", "Erstat positionen '")+old.Name+"'?",UiText.T("Save preset", "Gem position"),MessageBoxButtons.YesNo)!=DialogResult.Yes)return;
   var replacement=new Preset { Name=text,Position=pose };var list=Current;int index=old==null?list.Count:list.IndexOf(old);
   if(old==null)list.Add(replacement);else list[index]=replacement;
   try {WriteData();}catch {if(old==null)list.Remove(replacement);else list[index]=old;throw;}
   ListPresets();presets.SelectedItem=replacement;status.Text=UiText.T("Saved: ", "Gemt: ")+text;
  }
  void Delete() {
   var p=presets.SelectedItem as Preset;if(p==null)return;
   if(MessageBox.Show(this,UiText.T("Delete '", "Slet '")+p.Name+"'?",UiText.T("Delete preset", "Slet position"),MessageBoxButtons.YesNo)!=DialogResult.Yes)return;
   var list=Current;int index=list.IndexOf(p);list.RemoveAt(index);
   try{WriteData();}catch{list.Insert(index,p);throw;}ListPresets();
  }
  void OpenStartupSettings() {
   if(!storageReady)throw new IOException(UiText.T("Refresh profiles before editing settings.","Opdater profiler før du ændrer indstillinger."));
   SavedData edited;bool runNow;
   if(!StartupSettingsDialog.Edit(this,saved,out edited,out runNow))return;
   cameraApproved=false;if(waitForStartup.Checked)DisconnectCamera();var old=saved;saved=edited;
   try{WriteData();}catch{saved=old;throw;}
   status.Text=UiText.T("Profile settings saved.","Profilindstillinger gemt.");if(runNow)startup.Start(saved,true);
  }
  void WriteData() {
   if(!storageReady)throw new IOException(UiText.T("Profiles are not available. Refresh profiles first.", "Profilerne er ikke tilgængelige. Opdater profiler først."));
   saved.Seconds=seconds.Value;saved.PanSteps=(int)panSteps.Value;saved.ZoomSteps=(int)zoomSteps.Value;
   saved.FinePanSteps=(int)finePanSteps.Value;saved.FineZoomSteps=(int)fineZoomSteps.Value;
   saved.WaitForStartup=waitForStartup.Checked;saved.FastPanSteps=(int)fastPanSteps.Value;saved.AlwaysOnTop=alwaysTop.Checked;saved.CameraId=(devices.SelectedItem as Device)?.Id??saved.CameraId;
   saved.ReverseHorizontal=reverseHorizontal.Checked;saved.ReverseVertical=reverseVertical.Checked;
   saved=profiles.Save(saved).Config;settingsDirty=false;UpdateProgramSummary();
  }
  void ApplyProfile(ProfileEntry entry) {
   Stop();applyingProfile=true;
   try {cameraApproved=false;DisconnectCamera();saved=entry.Config;waitForStartup.Checked=saved.WaitForStartup;fastPanSteps.Value=saved.FastPanSteps;alwaysTop.Checked=saved.AlwaysOnTop;UpdateProgramSummary();
    var preferred=available.FindIndex(d=>d.Id==saved.CameraId);if(preferred>=0&&devices.SelectedIndex!=preferred)devices.SelectedIndex=preferred;seconds.Value=Math.Max(0,Math.Min(15,saved.Seconds));
    panSteps.Value=saved.PanSteps;zoomSteps.Value=saved.ZoomSteps;finePanSteps.Value=saved.FinePanSteps;fineZoomSteps.Value=saved.FineZoomSteps;
    reverseHorizontal.Checked=saved.ReverseHorizontal;reverseVertical.Checked=saved.ReverseVertical;
    string typed=name.Text;string chosen=(presets.SelectedItem as Preset)?.Name;
    if(devices.SelectedItem!=null){ListPresets();presets.SelectedItem=Current.FirstOrDefault(p=>p.Name==chosen);}
    name.Text=typed;storageReady=true;settingsDirty=false;
   }finally{applyingProfile=false;}
  }
  void LoadData() {
   try{profiles.Initialize();storageReady=true;}
   catch(Exception ex){storageReady=false;MessageBox.Show(this,ex.Message+"\n"+path,UiText.T("Profiles", "Profiler"));}
  }
 }
}
