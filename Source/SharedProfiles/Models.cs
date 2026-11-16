using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace BrioPresets {
 [DataContract] public class Preset {
  [DataMember] public string Name;
  [DataMember] public Dictionary<string,int> Position=new Dictionary<string,int>();
  public override string ToString(){return Name;}
 }
 [DataContract] public class SavedData {
  [DataMember] public Dictionary<string,List<Preset>> Cameras=new Dictionary<string,List<Preset>>();
  [DataMember] public decimal Seconds=2;
  [DataMember] public int PanSteps=5, ZoomSteps=5, FinePanSteps=1, FineZoomSteps=1;
  [DataMember] public bool ReverseHorizontal, ReverseVertical;
  [DataMember] public int FastPanSteps=32;
  [DataMember] public bool AlwaysOnTop;
  [DataMember] public string CameraId;
  [DataMember] public bool WaitForStartup=true;
  [DataMember] public bool AutoStartPrograms;
  [DataMember] public List<StartupProgram> StartupPrograms=new List<StartupProgram>();
  [DataMember] public string SettingsCodeSalt,SettingsCodeHash;
  [OnDeserializing] void InitializeDefaults(StreamingContext context) {
   Cameras=new Dictionary<string,List<Preset>>();Seconds=2;
   PanSteps=5;ZoomSteps=5;FinePanSteps=1;FineZoomSteps=1;FastPanSteps=32;WaitForStartup=true;
   StartupPrograms=new List<StartupProgram>();
  }
 }
 [DataContract] public sealed class StartupProgram {
  [DataMember] public string Name,FileName,Arguments,ProcessName;
 }
 public static class Motion {
  public static double Ease(double t){t=Math.Max(0,Math.Min(1,t));return t*t*t*(t*(t*6-15)+10);}
 }
}
