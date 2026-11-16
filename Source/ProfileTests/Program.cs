using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using BrioPresets;
class FakeRuntime:IProgramRuntime {
 public HashSet<string> Running=new HashSet<string>();public List<string> Started=new List<string>();public string Fail;
 public bool IsRunning(StartupProgram p){return Running.Contains(p.ProcessName);}
 public void Start(StartupProgram p){Started.Add(p.Name);}
 public bool WaitForProcess(StartupProgram p,CancellationToken t){return p.Name!=Fail;}
}
class Program {
 static void Check(bool ok,string message){if(!ok)throw new Exception(message);}
 static void Write(string path,SavedData data){using(var f=File.Create(path))new DataContractJsonSerializer(typeof(SavedData)).WriteObject(f,data);}
 static void Main() {
  string dir=Path.Combine(Path.GetTempPath(),"CamPressetsTests-"+Guid.NewGuid());Directory.CreateDirectory(dir);
  try {
   string full=Path.Combine(dir,"full.json"),mini=Path.Combine(dir,"mini.json"),path=Path.Combine(dir,"profiles.json");
   var a=new SavedData {PanSteps=9,ReverseHorizontal=true};a.Cameras["camera"]=new List<Preset>{new Preset {Name="Speaker",Position=new Dictionary<string,int>{{"Zoom",100}}}};
   var b=new SavedData();b.Cameras["camera"]=new List<Preset>{new Preset {Name="Speaker",Position=new Dictionary<string,int>{{"Zoom",250}}}};Write(full,a);Write(mini,b);
   var legacy=System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(mini)).AsObject();
   foreach(string field in new[]{"PanSteps","ZoomSteps","FinePanSteps","FineZoomSteps","ReverseHorizontal","ReverseVertical","AutoStartPrograms","StartupPrograms","SettingsCodeSalt","SettingsCodeHash","FastPanSteps","AlwaysOnTop","CameraId","WaitForStartup"})legacy.Remove(field);
   File.WriteAllText(mini,legacy.ToJsonString());
   var store=new ProfileStore(path,full,mini);var peer=new ProfileStore(path);var initial=store.Read().Profiles[ProfileStore.Standard];
   Check(initial.Config.Cameras["camera"].Count==2,"migration keeps conflicting presets");Check(initial.Config.ReverseHorizontal&&initial.Config.PanSteps==9,"migration keeps Studio settings");
   Check(File.Exists(full)&&File.Exists(mini),"migration leaves originals");
   var copy=store.Create("Hall B",ProfileStore.Standard);Check(copy.Profiles["Hall B"].Config.Cameras["camera"].Count==2,"profile copy");
   var blank=store.Create("Blank");Check(blank.Profiles["Blank"].Config.Cameras.Count==0&&!blank.Profiles["Blank"].Config.AutoStartPrograms,"new profile safe defaults");
   bool rejected=false;try{store.Create("standard");}catch(InvalidOperationException){rejected=true;}Check(rejected,"case-insensitive uniqueness");
   var snapshot=peer.Read().Profiles[ProfileStore.Standard];initial.Config.Seconds=4;store.Save(ProfileStore.Standard,initial.Config,initial.Revision);
   rejected=false;try{peer.Save(ProfileStore.Standard,snapshot.Config,snapshot.Revision);}catch(IOException){rejected=true;}Check(rejected,"stale save refused");Check(peer.Read().Profiles[ProfileStore.Standard].Config.Seconds==4,"peer sees shared config");Check(peer.Read().Profiles["Hall B"].Config.Seconds==2,"profiles stay isolated");
   var current=store.Read().Profiles[ProfileStore.Standard];int successes=0;var gate=new ManualResetEventSlim(false);
   var tasks=Enumerable.Range(0,2).Select(i=>Task.Run(()=>{gate.Wait();try{new ProfileStore(path).Save(ProfileStore.Standard,ProfileStore.Copy(current.Config),current.Revision);Interlocked.Increment(ref successes);}catch(IOException){}})).ToArray();gate.Set();Task.WaitAll(tasks);Check(successes==1,"concurrent save has one winner");
   var code=new SavedData();SettingsCode.Set(code,"123456");Check(SettingsCode.Verify(code,"123456")&&!SettingsCode.Verify(code,"654321"),"settings code verification");Check(code.SettingsCodeHash!="123456","code not plaintext");
   var cfg=store.Read().Profiles["Hall B"];cfg.Config.SettingsCodeHash=code.SettingsCodeHash;cfg.Config.SettingsCodeSalt=code.SettingsCodeSalt;cfg.Config.AutoStartPrograms=true;
   var programs=new[]{new StartupProgram {Name="Existing",FileName=Path.Combine(dir,"existing.exe"),ProcessName="existing"},new StartupProgram {Name="First",FileName=Path.Combine(dir,"first.exe"),ProcessName="first"},new StartupProgram {Name="Second",FileName=Path.Combine(dir,"second.exe"),ProcessName="second"}};
   cfg.Config.FastPanSteps=37;cfg.Config.AlwaysOnTop=true;cfg.Config.CameraId="usb-test";
   cfg.Config.StartupPrograms=programs.ToList();store.Save("Hall B",cfg.Config,cfg.Revision);var read=peer.Read().Profiles["Hall B"].Config;
   Check(read.FastPanSteps==37&&read.AlwaysOnTop&&read.CameraId=="usb-test","Studio preferences roundtrip");
   Check(read.AutoStartPrograms&&read.StartupPrograms[1].Name=="First"&&SettingsCode.Verify(read,"123456"),"startup order and code roundtrip");
   var runtime=new FakeRuntime();runtime.Running.Add("existing");StartupLogic.Run(programs,runtime,CancellationToken.None,_=>{});Check(string.Join(",",runtime.Started)=="First,Second","existing skipped and order preserved");
   runtime=new FakeRuntime {Fail="First"};runtime.Running.Add("existing");rejected=false;try{StartupLogic.Run(programs,runtime,CancellationToken.None,_=>{});}catch(InvalidOperationException){rejected=true;}Check(rejected&&runtime.Started.Count==1,"failure stops sequence");
   using(var cancel=new CancellationTokenSource()){cancel.Cancel();runtime=new FakeRuntime();try{StartupLogic.Run(programs,runtime,cancel.Token,_=>{});}catch(OperationCanceledException){}Check(runtime.Started.Count==0,"cancel prevents launches");}
   Check(new SavedData().WaitForStartup,"camera gate enabled by default");
   using(var m=new MemoryStream(System.Text.Encoding.UTF8.GetBytes("{}"))){var old=(SavedData)new DataContractJsonSerializer(typeof(SavedData)).ReadObject(m);Check(old.WaitForStartup,"old profiles get safe camera gate default");}
   var optOut=new SavedData {WaitForStartup=false};Check(!ProfileStore.Copy(optOut).WaitForStartup,"explicit opt out persists");
   runtime=new FakeRuntime();Check(CameraStartupGate.Missing(new StartupProgram[0],runtime).Contains("Zoom"),"empty list does not bypass Zoom");
   runtime.Running.Add("Zoom");Check(CameraStartupGate.Missing(programs,runtime).Count==3,"Zoom alone does not bypass required programs");
   foreach(var item in programs)runtime.Running.Add(item.ProcessName);
   Check(CameraStartupGate.Missing(programs,runtime).Count==0,"all required processes detected");
   runtime.Running.Remove("Zoom");Check(CameraStartupGate.Missing(programs,runtime).Contains("Zoom"),"Zoom must still be running");
   string broken="{broken-json";File.WriteAllText(path,broken);rejected=false;try{store.Read();}catch(Exception){rejected=true;}Check(rejected&&File.ReadAllText(path)==broken,"invalid file not overwritten");
   Console.WriteLine("PASS: shared profiles, legacy migration, profile isolation, concurrent conflict protection, startup sequence, cancellation and code protection.");
  }finally{Directory.Delete(dir,true);}
 }
}
