using System;
using System.Linq;
using System.Windows.Forms;
namespace BrioPresets {
 public sealed class ProfileControls:TableLayoutPanel {
  readonly ProfileStore store=new ProfileStore();
  readonly ComboBox list=new ComboBox {Dock=DockStyle.Fill,DropDownStyle=ComboBoxStyle.DropDownList};
  readonly Timer poll=new Timer {Interval=2000};
  readonly Form owner;readonly Action<ProfileEntry> apply;readonly Func<bool> dirty,busy;readonly Action<string> status;
  bool changing,initialized;string selected=ProfileStore.Standard;ProfileEntry current;
  public event Action ProfileSelected;
  public string SelectedName {get{return selected;}}
  public string FilePath {get{return store.FilePath;}}
  public ProfileControls(Form form,Action<ProfileEntry> onApply,Func<bool> hasUnsaved,Func<bool> isBusy,Action<string> report,Action openSettings) {
   owner=form;apply=onApply;dirty=hasUnsaved;busy=isBusy;status=report;
   Dock=DockStyle.Fill;ColumnCount=4;RowCount=1;Margin=Padding.Empty;
   ColumnStyles.Add(new ColumnStyle(SizeType.Percent,100));ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,48));ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100));ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,110));
   var create=new Button {Text=UiText.T("New", "Ny"),Dock=DockStyle.Fill};
   var refresh=new Button {Text=UiText.T("Refresh", "Opdater"),Dock=DockStyle.Fill};
   var settings=new Button {Text=UiText.T("Settings", "Indstillinger"),Dock=DockStyle.Fill};
   var tips=new ToolTip();tips.SetToolTip(list,UiText.T("Shared profile", "Fælles profil"));tips.SetToolTip(refresh,UiText.T("Refresh profiles from the other app", "Opdater profiler fra det andet program"));
   Controls.Add(list,0,0);Controls.Add(create,1,0);Controls.Add(refresh,2,0);Controls.Add(settings,3,0);
   settings.Click+=(s,e)=>Guard(openSettings);
   list.SelectedIndexChanged+=(s,e)=>{if(!changing&&initialized)Guard(Switch);};
   create.Click+=(s,e)=>Guard(Create);refresh.Click+=(s,e)=>Guard(()=>{if(Discard())RefreshData(true);});
   poll.Tick+=(s,e)=>{if(!initialized||dirty()||busy()||list.DroppedDown)return;try{RefreshData(false);}catch(Exception ex){poll.Stop();status(ex.Message);}};
   form.FormClosed+=(s,e)=>{poll.Stop();poll.Dispose();tips.Dispose();};
  }
  void Guard(Action action){try{action();}catch(Exception ex){MessageBox.Show(owner,ex.Message,UiText.T("Profiles", "Profiler"),MessageBoxButtons.OK,MessageBoxIcon.Warning);}}
  bool Discard(){return !dirty()||MessageBox.Show(owner,UiText.T("Discard unsaved settings and load this profile from disk?", "Kassér ugemte indstillinger og indlæs profilen fra filen?"),UiText.T("Profiles", "Profiler"),MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes;}
  public void Initialize(){RefreshData(true);initialized=true;poll.Start();}
  void SetList(ProfileCatalog data) {
   changing=true;try{list.Items.Clear();list.Items.AddRange(data.Profiles.Keys.OrderBy(n=>n==ProfileStore.Standard?0:1).ThenBy(n=>n).Cast<object>().ToArray());list.SelectedItem=selected;}finally{changing=false;}
  }
  void RefreshData(bool force) {
   var data=store.Read();if(!data.Profiles.ContainsKey(selected))selected=ProfileStore.Standard;
   bool changed=force||current==null||current.Revision!=data.Profiles[selected].Revision;
   SetList(data);if(changed){current=data.Profiles[selected];apply(current);}
   if(force){initialized=true;poll.Start();}
  }
  void Switch() {
   string target=list.SelectedItem as string;if(target==null||target==selected)return;
   if(!Discard()){changing=true;list.SelectedItem=selected;changing=false;return;}
   string old=selected;selected=target;
   try{RefreshData(true);ProfileSelected?.Invoke();}catch{selected=old;changing=true;list.SelectedItem=old;changing=false;throw;}
  }
  void Create() {
   if(dirty())throw new InvalidOperationException(UiText.T("Save your current settings before creating a profile.", "Gem de aktuelle indstillinger før du opretter en profil."));
   using(var dialog=new Form {Text=UiText.T("New profile", "Ny profil"),ClientSize=new System.Drawing.Size(330,155),StartPosition=FormStartPosition.CenterParent,FormBorderStyle=FormBorderStyle.FixedDialog,MaximizeBox=false,MinimizeBox=false}) {
    var name=new TextBox {Left=12,Top=30,Width=306,MaxLength=60};
    var copy=new CheckBox {Text=UiText.T("Copy the current saved configuration", "Kopiér den aktuelle gemte konfiguration"),Left=12,Top=66,Width=306,Checked=true};
    var ok=new Button {Text=UiText.T("Create", "Opret"),Left=160,Top=108,Width=75,DialogResult=DialogResult.OK};
    var cancel=new Button {Text=UiText.T("Cancel", "Annuller"),Left=243,Top=108,Width=75,DialogResult=DialogResult.Cancel};
    dialog.Controls.AddRange(new Control[]{new Label {Text=UiText.T("Profile name", "Profilnavn"),Left=12,Top=10,AutoSize=true},name,copy,ok,cancel});dialog.AcceptButton=ok;dialog.CancelButton=cancel;
    if(dialog.ShowDialog(owner)!=DialogResult.OK)return;
    var data=store.Create(name.Text,copy.Checked?selected:null);selected=name.Text.Trim();current=data.Profiles[selected];SetList(data);apply(current);ProfileSelected?.Invoke();
   }
  }
  public ProfileEntry Save(SavedData data) {
   if(!initialized||current==null)throw new InvalidOperationException(UiText.T("Profiles could not be loaded. Use Refresh before saving.", "Profilerne kunne ikke indlæses. Brug Opdater før du gemmer."));
   current=store.Save(selected,data,current.Revision);return current;
  }
 }
}
