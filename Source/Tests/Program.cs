using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using CameraControlStudio;
class Program {
 static void Check(bool value,string message){if(!value)throw new Exception(message);}
 static void Main(){
  var setting=new Setting{Min=0,Max=200,Step=2};Check(ManualMotion.Target(setting,50,1,5,false)==60,"normal zoom uses driver units");Check(ManualMotion.Target(setting,50,1,32,false)==114,"fast zoom independent steps");Check(ManualMotion.Target(setting,50,-1,32,false)==0,"movement clamps to range");Check(ManualMotion.Target(setting,50,1,5,true)==40,"reverse direction");
  Check(!ManualMotion.CanMove(setting,0,-1,false)&&ManualMotion.CanMove(setting,0,1,false),"minimum blocks outward movement only");
  Check(!ManualMotion.CanMove(setting,200,1,false)&&ManualMotion.CanMove(setting,200,-1,false),"maximum blocks outward movement only");
  Check(!ManualMotion.CanMove(setting,200,-1,true)&&ManualMotion.CanMove(setting,200,1,true),"reversed direction swaps blocked side");
  var unevenRange=new Setting{Min=-5,Max=8,Step=3};
  Check(!ManualMotion.CanMove(unevenRange,7,1,false)&&ManualMotion.CanMove(unevenRange,7,-1,false),"reachable maximum honors driver step");
  Check(ManualMotion.ZoomAtFraction(unevenRange,0)==-5&&ManualMotion.ZoomAtFraction(unevenRange,1)==7,"slider spans reachable zoom range");
  Check(ManualMotion.ZoomAtFraction(unevenRange,.5)==1&&ManualMotion.ZoomFraction(unevenRange,1)==.5,"slider maps zoom in both directions");
  Check(!ManualMotion.CanMove(new Setting{Min=0,Max=1,Step=2},0,1,false),"an axis without reachable steps cannot move");
  var activity=new PresetActivity();activity.Begin("A");Check(activity.Phase==PresetPhase.Moving,"yellow while moving");activity.Complete(true);Check(activity.Phase==PresetPhase.Reached,"green only when reached");activity.Clear();Check(activity.Phase==PresetPhase.None&&activity.Name==null,"manual override clears state");activity.Begin("B");activity.Complete(false);Check(activity.Phase==PresetPhase.None,"failed target never green");activity.Begin("A");activity.Begin("B");Check(activity.Name=="B","new preset replaces old state");
  var gate=new CameraAccessGate();int asks=0;Check(gate.Allow(false,()=>{asks++;return false;})&&asks==0,"default no confirmation");Check(!gate.Allow(true,()=>false),"No blocks camera");Check(gate.Allow(true,()=>true),"Yes allows camera");Check(gate.Allow(true,()=>throw new Exception("repeat prompt")),"confirmation remembered");gate.Reset();Check(!gate.Allow(true,()=>false),"profile reset blocks camera again");
  var from=new ProfileLayout{X=20,Width=1000,ControllerRatio=.4,SettingsOpen=true};var to=new ProfileLayout{X=800,Width=900,ControllerRatio=.6};var carried=LayoutPolicy.OnSwitch(from,to,true);Check(carried.X==20&&carried.ControllerRatio==.4&&carried.SettingsOpen,"keep layout applies current geometry");Check(!ReferenceEquals(carried,from),"layout copy isolated");Check(LayoutPolicy.OnSwitch(from,to,false).X==800,"default switch restores target geometry");Check(LayoutPolicy.Ratio(double.NaN)==LayoutPolicy.DefaultControllerRatio,"bad ratio corrected");
  var layoutSerializer=new DataContractJsonSerializer(typeof(ProfileLayout));
  using(var oldLayout=new MemoryStream(Encoding.UTF8.GetBytes("{\"ControllerRatio\":0.36}"))) {
   var migrated=(ProfileLayout)layoutSerializer.ReadObject(oldLayout);
   Check(migrated.ControllerRatio==LayoutPolicy.DefaultControllerRatio,"old default gets the compact layout split");
  }
  Check(ProfileStore.Copy(new SavedData{Layout=new ProfileLayout{ControllerRatio=.36}}).Layout.ControllerRatio==.36,"newly chosen split is preserved");
  string dir=Path.Combine(Path.GetTempPath(),"camera-tests-"+Guid.NewGuid());Directory.CreateDirectory(dir);
  try {
   var store=new ProfileStore(Path.Combine(dir,"profiles.json"));var entry=store.Read().Profiles[ProfileStore.Standard];Check(!entry.Config.RequireStreamingConfirmation&&!entry.Config.KeepLayoutOnSwitch&&entry.Config.FastZoomSteps==32,"safe defaults");
   entry.Config.Layout=from;entry.Config.FastZoomSteps=19;entry.Config.KeepLayoutOnSwitch=true;entry.Config.Cameras["cam"]=new List<Preset>{new Preset{Name="Scene",IconId="combo:lectern+cafe-table",Position=new Dictionary<string,int>{{"Zoom",50}}}};
   entry.Config.ImageValues["cam"]=new Dictionary<string,ImageValue>{{"Brightness",new ImageValue{Value=45,Automatic=false}}};
   store.Save(ProfileStore.Standard,entry.Config,entry.Revision);var saved=store.Read().Profiles[ProfileStore.Standard];Check(saved.Config.FastZoomSteps==19&&saved.Config.Layout.Width==1000&&saved.Config.KeepLayoutOnSwitch,"layout and zoom persisted");Check(saved.Config.Cameras["cam"][0].IconId=="combo:lectern+cafe-table","combined icon persisted");Check(saved.Config.ImageValues["cam"]["Brightness"].Value==45,"image values persisted");
   bool conflict=false;try{store.Save(ProfileStore.Standard,entry.Config,entry.Revision);}catch(IOException){conflict=true;}Check(conflict,"stale saves refused");
   var copied=store.Create("Person A",ProfileStore.Standard).Profiles["Person A"];store.Rename("Person A","Person B",copied.Revision);Check(store.Read().Profiles["Person B"].Config.Layout.Width==1000,"profile renamed with layout");
   var sessions=new SessionPresets();sessions.For("Person B","cam").Add(new Preset{Name="Temporary"});Check(sessions.For("Standard","cam").Count==0,"temporary profile isolation");sessions.RenameProfile("Person B","Person C");Check(sessions.For("Person C","cam").Count==1,"session presets survive rename");Check(!File.ReadAllText(store.FilePath).Contains("Temporary"),"temporary not serialized");
   using(var stream=new MemoryStream(Encoding.UTF8.GetBytes("{\"WaitForStartup\":true,\"AutoStartPrograms\":true,\"StartupPrograms\":[{\"Name\":\"Legacy\",\"FileName\":\"unused.exe\"}]}"))){var migrated=(SavedData)new DataContractJsonSerializer(typeof(SavedData)).ReadObject(stream);Check(migrated.RequireStreamingConfirmation&&migrated.FastZoomSteps==32&&migrated.Layout!=null,"old profile loads and new defaults apply");Check(!typeof(SavedData).GetFields().Any(f=>f.Name=="StartupPrograms"),"program launch data removed");}
   Check(UiText.LanguageCodes.Length==UiText.LanguageNames.Length,"one menu name per language");
   Check(UiText.ResolveLanguage("auto","ja-JP")=="en","unsupported Windows language falls back to English");
   var languageFile=Path.Combine(dir,"language.txt");
   using(var english=typeof(UiText).Assembly.GetManifestResourceStream("CamPressets.Locales.en.xml")) {
    var keys=XDocument.Load(english).Root.Elements("text").Select(node=>(string)node.Attribute("key")).ToArray();
    foreach(var lang in UiText.LanguageCodes.Skip(1)) {
     Check(UiText.ResolveLanguage("auto",lang+"-XX")==lang,"Windows language resolved: "+lang);
     UiText.SavePreferenceAt(lang,languageFile);
     Check(File.ReadAllText(languageFile)==lang,"locale preference saved: "+lang);
     UiText.Initialize(new[]{"--lang="+lang});
     Check(UiText.Language==lang&&UiText.CommandLineOverride,"locale selected: "+lang);
     Check(UiText.IsRightToLeft==(lang=="ar"||lang=="ur"),"text direction: "+lang);
     foreach(var key in keys)
      Check(!string.IsNullOrWhiteSpace(UiText.Translate(key,null)),"translation available: "+lang+" / "+key);
     Check(string.Format(UiText.T("Delete permanent preset: {0}?",""),"FX").Contains("FX"),"localized confirmation: "+lang);
    }
   }
  }finally{Directory.Delete(dir,true);}
  Console.WriteLine("PASS: motion, activity colours, camera consent, profile/layout migration, image persistence, independent zoom steps, session presets and concurrency.");
 }
}
