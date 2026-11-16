using System;
using System.Collections.Generic;
using System.Linq;
namespace BrioPresets {
 // A running Zoom process never proves meeting readiness. The UI must also ask
 // for explicit confirmation before granting a session-only camera connection.
 public static class CameraStartupGate {
  public static List<string> Missing(IEnumerable<StartupProgram> programs,IProgramRuntime runtime) {
   var missing=new List<string>();
   foreach(var p in programs)if(!runtime.IsRunning(p))missing.Add(p.Name);
   if(!runtime.IsRunning(new StartupProgram {Name="Zoom",ProcessName="Zoom"})&&!missing.Any(n=>string.Equals(n,"Zoom",StringComparison.OrdinalIgnoreCase)))missing.Add("Zoom");
   return missing;
  }
 }
}
