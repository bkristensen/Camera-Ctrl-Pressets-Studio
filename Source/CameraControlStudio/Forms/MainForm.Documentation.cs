using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
namespace CameraControlStudio {
 public partial class MainForm {
  // Documentation mode never sets loaded, opens a camera, or reads/writes profiles.
  bool documentationMode;string documentationDirectory;Timer documentationTimer;int documentationStage;
  internal MainForm(string outputDirectory):this(){documentationMode=true;documentationDirectory=Path.GetFullPath(outputDirectory);}
  void InitializeDocumentation(){
   config=new SavedData {CameraId="documentation-only"};
   var examples = new[] { "stage", "lectern", "combo:lectern+cafe-table", "combo:table+lectern+cafe-table" };
   config.Cameras[config.CameraId]=new List<Preset>();
   foreach (var example in examples)
    config.Cameras[config.CameraId].Add(new Preset { Name = PresetIcons.GetTitle(example), IconId = example });
   Temporary.Add(new Preset{Name=UiText.T("Temporary preset name","Midlertidig position")});
   panSteps.Value=5;fastPanSteps.Value=32;zoomSteps.Value=5;fastZoomSteps.Value=32;seconds.Value=2;
   profilesList.Items.Add("Standard");profileButton.Text="Standard ▾";
   cameraStatus.Text="Documentation · Demo camera";
   status.Text=UiText.T("Position reached.","Positionen er nået.");
   RefreshPresets();activity.Begin(Permanent[1].Name);activity.Complete(true);
   StartPosition=FormStartPosition.Manual;Location=Screen.PrimaryScreen.WorkingArea.Location+new Size(12,12);Size=new Size(900,500);TopMost=true;
   Directory.CreateDirectory(documentationDirectory);
   documentationTimer=new Timer{Interval=900};documentationTimer.Tick+=(o,e)=>{
    try {
     if(documentationStage==0){CaptureDocumentation("main",new Control[]{profileButton,settingsButton,wheel,zoomPanel,mainSplit,presetGrid,temporaryButton,footer},new[]{"A","B","C","D","E","F","G","H"});SetSettings(true,true);}
     else if(documentationStage==1){CaptureDocumentation("settings",new Control[]{settingsTabs,generalFields},new[]{"I","J"});settingsTabs.SelectedTab=presetsTab;}
     else {CaptureDocumentation("presets",new Control[]{permanentList,addPermanent,editPermanent,updatePermanent,deletePermanent},new[]{"K","L","M","N","O"});documentationTimer.Stop();documentationTimer.Dispose();Close();}
     documentationStage++;
    }catch(Exception ex){documentationTimer.Stop();MessageBox.Show(ex.Message);Close();}
   };documentationTimer.Start();
  }
  void CaptureDocumentation(string name,Control[] controls,string[] labels){
   using(var bitmap=new Bitmap(ClientSize.Width,ClientSize.Height))using(var g=Graphics.FromImage(bitmap))using(var font=new Font("Segoe UI",10,FontStyle.Bold)){
    g.CopyFromScreen(PointToScreen(Point.Empty),Point.Empty,ClientSize);
    for(int i=0;i<controls.Length;i++){
     var c=controls[i];var p=PointToClient(c.PointToScreen(new Point(Math.Max(0,c.Width-25),2)));
     if(labels[i]=="E")p=PointToClient(mainSplit.PointToScreen(new Point(mainSplit.SplitterDistance-10,mainSplit.Height/2)));
     g.FillEllipse(Brushes.Gold,p.X,p.Y,23,23);TextRenderer.DrawText(g,labels[i],font,new Rectangle(p.X,p.Y,23,23),Color.Black,TextFormatFlags.HorizontalCenter|TextFormatFlags.VerticalCenter|TextFormatFlags.NoPadding);
    }
    bitmap.Save(Path.Combine(documentationDirectory,UiText.Language+"-"+name+".png"),ImageFormat.Png);
   }
  }
 }
}
