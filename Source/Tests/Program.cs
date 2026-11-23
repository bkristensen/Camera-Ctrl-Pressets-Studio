using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using CameraControlStudio;
class Program {

    static void TestReleaseNotice()
    {
        const string tagUrl = "https://github.com/bkristensen/Camera-Ctrl-Pressets-Studio/releases/tag/";
        const string releases = """
            [
              {"tag_name":"v0.7.0-beta.10","html_url":"URLv0.7.0-beta.10","prerelease":true},
              {"tag_name":"Beta","name":"Camera Control Studio 0.7.0-beta.2","html_url":"URLBeta"},
              {"tag_name":"v0.7.0-beta.3","html_url":"URLv0.7.0-beta.3"},
              {"tag_name":"v8.0.0","html_url":"URLv8.0.0","draft":true},
              {"tag_name":"v9.0.0","html_url":"https://example.com/releases/tag/v9.0.0"}
            ]
            """;
        Uri CheckReleases(string installedVersion, string json)
        {
            using (var payload = new MemoryStream(Encoding.UTF8.GetBytes(json.Replace("URL", tagUrl))))
                return GitHubReleases.FindNewer(installedVersion, payload);
        }

        Check(CheckReleases("0.7.0-beta.3+91abc", releases)?.AbsoluteUri == tagUrl + "v0.7.0-beta.10",
            "release check orders numeric prereleases and skips drafts and foreign URLs");
        Check(CheckReleases("0.7.0-beta.10+91abc", releases) == null, "build metadata does not change release precedence");
        Check(CheckReleases("0.7.0", releases) == null, "a prerelease of the installed stable version is older");

        const string legacy = """
            [{"tag_name":"Beta","name":"Camera Control Studio 0.7.0-beta.2","html_url":"URLBeta"}]
            """;
        Check(CheckReleases("0.7.0-beta.1", legacy)?.AbsoluteUri == tagUrl + "Beta", "legacy Beta tag reads its release title");
        Check(CheckReleases("0.7.0-beta.3", legacy) == null, "legacy Beta cannot downgrade the installed app");

        const string stable = """
            [{"tag_name":"v0.7.0","html_url":"URLv0.7.0"}]
            """;
        Check(CheckReleases("0.7.0-rc.1", stable)?.AbsoluteUri == tagUrl + "v0.7.0",
            "stable release wins over its release candidate");
    }

 static void Check(bool value,string message){if(!value)throw new Exception(message);}
 static void CheckMovementSpeeds() {
  Check(PanMotion.Direction(PanMotion.Sector(-1, -1)) == (-1, -1) &&
   PanMotion.Direction(PanMotion.Sector(1, -1)) == (1, -1) &&
   PanMotion.Direction(PanMotion.Sector(1, 1)) == (1, 1) &&
   PanMotion.Direction(PanMotion.Sector(-1, 1)) == (-1, 1), "diagonal sectors move both axes");
  Check(PanMotion.DragVelocity(30, 0, 60) == (.5, 0), "drag speed scales with distance");
  var clamped = PanMotion.DragVelocity(300, 400, 60);
  Check(clamped == (.6, .8), "drag caps the vector without changing its direction");
  double pending = 0;
  Check(PanMotion.TakeSteps(ref pending, PanMotion.DiagonalShare) == 0 &&
   PanMotion.TakeSteps(ref pending, PanMotion.DiagonalShare) == 0 &&
   PanMotion.TakeSteps(ref pending, PanMotion.DiagonalShare) == 1, "diagonal hold carries fractional driver steps");

  var setting = new Setting { Min = -5, Max = 8, Step = 3 };
  Check(PresetSpeedCurve.AxisDistance(setting, 7, 1) == .5, "preset distance uses the reachable range");
  var shortMove = new PresetSpeedCurve(.04, .5);
  var longMove = new PresetSpeedCurve(.8, .5);
  Check(shortMove.Duration < longMove.Duration, "short preset moves finish sooner");
  Check(new PresetSpeedCurve(.8, .25).Duration > longMove.Duration, "slower speed lengthens a move");
  Check(new PresetSpeedCurve(0, .5).Duration == 0, "a preset at the current position completes immediately");
  Check(longMove.Progress(0) == 0 && longMove.Progress(longMove.Duration) == 1, "preset curve reaches both endpoints");
  double CruiseRate(PresetSpeedCurve curve) => (curve.Progress(1) - curve.Progress(.5)) * .8 / .5;
  Check(Math.Abs(CruiseRate(longMove) - .5) < 1e-9, "long preset move cruises at the selected rate");
  Check(longMove.Progress(.1) < longMove.Progress(.5) * .2, "preset movement accelerates");
  Check(Math.Abs(shortMove.Progress(shortMove.Duration / 2) - .5) < 1e-9 &&
   Math.Abs(longMove.Progress(longMove.Duration - .1) + longMove.Progress(.1) - 1) < 1e-9, "short and long moves brake symmetrically");

  var serializer = new DataContractJsonSerializer(typeof(SavedData));
  using (var oldProfile = new MemoryStream(Encoding.UTF8.GetBytes("{\"Seconds\":2.5}"))) {
   var old = (SavedData)serializer.ReadObject(oldProfile);
   Check(!old.UseTransitionSpeed && old.Seconds == 2.5m && old.TransitionSpeedPercent == 30 && old.FinePanPercent == 50,
    "old profiles keep fixed duration and receive speed defaults");
  }
  var copy = ProfileStore.Copy(new SavedData { UseTransitionSpeed = true, TransitionSpeedPercent = 45, FinePanPercent = 25 });
  Check(copy.UseTransitionSpeed && copy.TransitionSpeedPercent == 45 && copy.FinePanPercent == 25,
   "movement speed settings survive profile copy");
 }
 static void CheckFieldOfViewPersistence() {
  var serializer = new DataContractJsonSerializer(typeof(SavedData));
  const string legacy = "{\"Cameras\":[{\"Key\":\"camera\",\"Value\":[{\"Name\":\"Wide\",\"Position\":[{\"Key\":\"Zoom\",\"Value\":100}]}]}]}";
  using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(legacy))) {
   var profile = (SavedData)serializer.ReadObject(stream);
   var preset = profile.Cameras["camera"][0];
   Check(profile.StartupFieldOfView == 0 && !profile.AllowFieldOfViewChange && profile.AdjustFieldOfViewAtLimits,
    "legacy profiles use the widest startup FOV and enable automatic changes");
   Check(!preset.FieldOfView.HasValue && preset.Position["Zoom"] == 100, "legacy presets keep their zoom without inventing an FOV");
   profile.StartupFieldOfView = -1;
   profile.AllowFieldOfViewChange = true;
   profile.AdjustFieldOfViewAtLimits = false;
   preset.FieldOfView = 78;
   var copy = ProfileStore.Copy(profile);
   Check(copy.StartupFieldOfView == -1 && copy.AllowFieldOfViewChange && !copy.AdjustFieldOfViewAtLimits,
    "explicit FOV preferences survive profile copy");
   Check(copy.Cameras["camera"][0].FieldOfView == 78, "preset FOV survives profile copy");
  }
 }
 static void CheckDriverFieldOfViewOptions() {
  var support = new byte[64];
  void Set(int offset, int value) => BitConverter.GetBytes(value).CopyTo(support, offset);
  Set(0, 3); // GET and SET
  Set(4, 58);
  Set(32, 1);
  Set(40, 1); // range
  Set(44, 2);
  Set(48, 1);
  support[56] = 1;
  support[57] = 2;
  var angles = BrioFieldOfView.ReadSupportedAngles(support, 58);
  Check(angles.SequenceEqual(new[] { 78, 65 }), "only angles in the driver range are offered");
  Check(ManualMotion.NextFieldOfView(angles, 78, -1) == null, "automatic zoom stops at the driver's widest angle");
  Check(BrioFieldOfView.ReadSupportedAngles(support, 4).Length == 0 &&
    BrioFieldOfView.ReadSupportedAngles(support, 57).Length == 0, "missing or truncated ranges provide no options");
  Set(0, 1);
  Check(BrioFieldOfView.ReadSupportedAngles(support, 58).Length == 0, "read-only field of view provides no options");
  Set(0, 3);
  Set(4, 64);
  Set(44, 8);
  Set(56, 0);
  Set(60, 8);
  Check(BrioFieldOfView.ReadSupportedAngles(support, 64).SequenceEqual(new[] { 90, 78, 65 }),
    "32-bit bounds are supported and unknown device codes are skipped");
 }
 static void CheckFieldOfViewSteps() {
  var fov = new[] { 90, 78, 65 };
  Check(ManualMotion.NextFieldOfView(fov, 65, -1) == 78 && ManualMotion.NextFieldOfView(fov, 78, -1) == 90 &&
   ManualMotion.NextFieldOfView(fov, 90, -1) == null, "zoom out uses each wider FOV and stops at the widest");
  Check(ManualMotion.NextFieldOfView(fov, 90, 1) == 78 && ManualMotion.NextFieldOfView(fov, 78, 1) == 65 &&
   ManualMotion.NextFieldOfView(fov, 65, 1) == null, "zoom in uses each narrower FOV and stops at the narrowest");
  Check(ManualMotion.NextFieldOfView(fov, 85, 1) == null, "unrecognized FOV does not trigger a device write");

  var zoom = new Setting { Min = 100, Max = 500, Step = 5 };
  double? wider = ManualMotion.EquivalentZoom(zoom, 100, 65, zoom, 78);
  double? narrower = ManualMotion.EquivalentZoom(zoom, 500, 90, zoom, 78);
  int zoomOut = ManualMotion.ZoomAfterFieldOfViewStep(zoom, 100, wider, -1);
  int zoomIn = ManualMotion.ZoomAfterFieldOfViewStep(zoom, 500, narrower, 1);
  Check(zoomOut == 125 && ManualMotion.CanMove(zoom, zoomOut, -1, false), "wider FOV leaves room to zoom out again");
  Check(zoomIn == 405 && ManualMotion.CanMove(zoom, zoomIn, 1, false), "narrower FOV leaves room to zoom in again");
  Check(ManualMotion.PanFieldOfViewOptions(fov, 78, zoom, 200).SequenceEqual(new[] { 90, 65 }),
   "panning tries a wider FOV first and considers a narrower FOV if it can keep the crop");
  Check(!ManualMotion.PanFieldOfViewOptions(fov, 90, zoom, 100).Any(),
   "panning skips a narrower FOV that needs zoom below the device minimum");
  var zeroBased = new Setting { Min = 0, Max = 10, Step = 2 };
  Check(!ManualMotion.EquivalentZoom(zeroBased, 0, 65, zeroBased, 78).HasValue &&
   ManualMotion.ZoomAfterFieldOfViewStep(zeroBased, 0, null, -1) == 2,
   "drivers without a usable zoom ratio still leave a normal step after switching");
 }
 static void Main(){
  TestReleaseNotice();
  CheckMovementSpeeds();
  CheckFieldOfViewPersistence();
  CheckDriverFieldOfViewOptions();
  CheckFieldOfViewSteps();
  var setting=new Setting{Min=0,Max=200,Step=2};Check(ManualMotion.Target(setting,50,1,5,false)==60,"zoom uses driver units");Check(ManualMotion.Target(setting,50,-1,32,false)==0,"movement clamps to range");Check(ManualMotion.Target(setting,50,1,5,true)==40,"reverse direction");
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
  using(var oldLayout=new MemoryStream(Encoding.UTF8.GetBytes("{\"ControllerRatio\":0.36,\"SettingsTab\":3}"))) {
   var migrated=(ProfileLayout)layoutSerializer.ReadObject(oldLayout);
   Check(migrated.ControllerRatio==LayoutPolicy.DefaultControllerRatio,"old default gets the compact layout split");
   using (var saved = new MemoryStream()) {
    layoutSerializer.WriteObject(saved, migrated);
    Check(!Encoding.UTF8.GetString(saved.ToArray()).Contains("SettingsTab"), "legacy tab selection is no longer saved");
   }
  }
  Check(ProfileStore.Copy(new SavedData{Layout=new ProfileLayout{ControllerRatio=.36}}).Layout.ControllerRatio==.36,"newly chosen split is preserved");
  string dir=Path.Combine(Path.GetTempPath(),"camera-tests-"+Guid.NewGuid());Directory.CreateDirectory(dir);
  try {
   foreach (int code in new[] { unchecked((int)0x80070015), unchecked((int)0x8007048F), unchecked((int)0x80070006) }) {
    try { Camera.CheckControlResult(code,"Get","Zoom"); throw new Exception("unavailable camera failure was ignored"); }
    catch (CameraUnavailableException ex) { Check(ex.HResult==code&&ex.Message.Contains("Get Zoom"),"driver HRESULT and operation preserved"); }
   }
   Camera.CheckControlResult(0,"Get","Zoom");
   var programDirectory=Path.Combine(dir,"program");
   var userLogs=Path.Combine(dir,"logs");
   var diagnostics=new DiagnosticLog(programDirectory,userLogs){CameraName="Test camera"};
   string logPath;
   try { Camera.CheckControlResult(unchecked((int)0x80070015),"Get","Zoom"); throw new Exception("expected camera failure"); }
   catch (CameraUnavailableException ex) { logPath=diagnostics.Write("WARNING","Test zoom read",ex); }
   Check(Path.GetDirectoryName(logPath)==programDirectory&&diagnostics.FindLatest()==logPath,"log goes in the program folder");
   string log=File.ReadAllText(logPath);
   Check(log.Contains("Version:")&&log.Contains("Test camera")&&log.Contains("0x80070015")&&log.Contains("Camera.CheckControlResult")&&log.Contains("Program.cs:"),"camera log includes diagnostic context");
   var blockedPath=Path.Combine(dir,"not-a-directory");File.WriteAllText(blockedPath,"");
   var fallback=new DiagnosticLog(blockedPath,userLogs);
   Check(Path.GetDirectoryName(fallback.Write("WARNING","Fallback test"))==userLogs,"unwritable program log uses the per-user log folder");
   Check(new DiagnosticLog(blockedPath,blockedPath).Write("WARNING","No storage")==null,"logging failure does not interrupt the app");
   File.WriteAllText(logPath,new string('x',1024*1024));
   Check(diagnostics.Write("ERROR","After rotation")==logPath&&File.Exists(Path.Combine(programDirectory,"CameraControlStudio.1.log"))&&File.ReadAllText(logPath).Contains("After rotation"),"log rotates at the size limit");
   var loggingSettings=new DiagnosticLogSettings(Path.Combine(dir,"selected"),3,1);var loggingFile=Path.Combine(dir,"logging.xml");
   loggingSettings.Save(loggingFile);loggingSettings=DiagnosticLogSettings.Load(loggingFile);diagnostics.Configure(loggingSettings);
   Check(loggingSettings.SizeMiB==3&&loggingSettings.FileCount==1&&Path.GetDirectoryName(diagnostics.Write("WARNING","Custom folder"))==loggingSettings.DirectoryPath,"custom log options persist and direct new entries");
   var singleLog=diagnostics.FindLatest();File.WriteAllText(singleLog,new string('x',3*1024*1024));
   diagnostics.Write("WARNING","Replace only file");
   Check(Directory.GetFiles(loggingSettings.DirectoryPath,"*.log").Length==1&&File.ReadAllText(singleLog).Contains("Replace only file"),"retention counts the active log");
   var store=new ProfileStore(Path.Combine(dir,"profiles.json"));var entry=store.Read().Profiles[ProfileStore.Standard];Check(!entry.Config.RequireStreamingConfirmation&&!entry.Config.KeepLayoutOnSwitch,"safe defaults");
   Check(entry.Config.PanSteps==1&&entry.Config.FastPanSteps==4&&entry.Config.ZoomSteps==1&&entry.Config.Seconds==1.0m,"new movement defaults");
   entry.Config.Layout=from;entry.Config.ZoomSteps=19;entry.Config.Seconds=2.5m;entry.Config.KeepLayoutOnSwitch=true;entry.Config.Cameras["cam"]=new List<Preset>{new Preset{Name="Scene",IconId="combo:lectern+cafe-table",Position=new Dictionary<string,int>{{"Zoom",50}}}};
   entry.Config.ImageValues["cam"]=new Dictionary<string,ImageValue>{{"Brightness",new ImageValue{Value=45,Automatic=false}}};
   store.Save(ProfileStore.Standard,entry.Config,entry.Revision);
   var serialized=File.ReadAllText(store.FilePath).Replace("\"ZoomSteps\":19","\"FastZoomSteps\":999,\"ZoomSteps\":19");
   File.WriteAllText(store.FilePath,serialized);
   var saved=store.Read().Profiles[ProfileStore.Standard];Check(saved.Config.ZoomSteps==19&&saved.Config.Seconds==2.5m&&saved.Config.Layout.Width==1000&&saved.Config.KeepLayoutOnSwitch,"layout, zoom and transition survive legacy fast zoom setting");Check(saved.Config.Cameras["cam"][0].IconId=="combo:lectern+cafe-table","combined icon persisted");Check(saved.Config.ImageValues["cam"]["Brightness"].Value==45,"image values persisted");
   bool conflict=false;try{store.Save(ProfileStore.Standard,entry.Config,entry.Revision);}catch(IOException){conflict=true;}Check(conflict,"stale saves refused");
   saved.Config.Language="de";
   store.Save(ProfileStore.Standard,saved.Config,saved.Revision);
   Check(!File.ReadAllText(store.FilePath).Contains("FastZoomSteps"),"retired zoom setting is discarded on save");
   var copied=store.Create("Person A",ProfileStore.Standard).Profiles["Person A"];store.Rename("Person A","Person B",copied.Revision);Check(store.Read().Profiles["Person B"].Config.Layout.Width==1000&&copied.Config.Language=="de","profile copy and rename retain language");
   UiText.Initialize(Array.Empty<string>(),"da");Check(UiText.Language=="da"&&UiText.Preference=="da","selected profile language used before constructing forms");
   UiText.Initialize(new[]{"--lang=fr"},"da");Check(UiText.Language=="fr"&&UiText.Preference=="da","command line override preserves the profile preference");
   var sessions=new SessionPresets();sessions.For("Person B","cam").Add(new Preset{Name="Temporary"});Check(sessions.For("Standard","cam").Count==0,"temporary profile isolation");sessions.RenameProfile("Person B","Person C");Check(sessions.For("Person C","cam").Count==1,"session presets survive rename");Check(!File.ReadAllText(store.FilePath).Contains("Temporary"),"temporary not serialized");
   using(var stream=new MemoryStream(Encoding.UTF8.GetBytes("{\"WaitForStartup\":true,\"AutoStartPrograms\":true,\"StartupPrograms\":[{\"Name\":\"Legacy\",\"FileName\":\"unused.exe\"}]}"))){var migrated=(SavedData)new DataContractJsonSerializer(typeof(SavedData)).ReadObject(stream);Check(migrated.RequireStreamingConfirmation&&migrated.ZoomSteps==1&&migrated.Seconds==1.0m&&migrated.Layout!=null,"old profile loads and new defaults apply");Check(!typeof(SavedData).GetFields().Any(f=>f.Name=="StartupPrograms"),"program launch data removed");}
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
  Console.WriteLine("PASS: motion, activity colours, camera consent, profile/layout migration, image persistence, movement defaults, session presets, FOV settings, release notices and concurrency.");
 }
}
