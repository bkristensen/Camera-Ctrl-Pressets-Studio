using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
namespace BrioPresets {
 public interface IProgramRuntime {
  bool IsRunning(StartupProgram program);
  void Start(StartupProgram program);
  bool WaitForProcess(StartupProgram program,CancellationToken token);
 }
 public static class StartupLogic {
  public static void Run(IEnumerable<StartupProgram> programs,IProgramRuntime runtime,CancellationToken token,Action<string> progress) {
   foreach(var p in programs) {
    token.ThrowIfCancellationRequested();Validate(p);
    if(runtime.IsRunning(p)){progress(p.Name+UiText.T(": already running",": kører allerede"));continue;}
    progress(UiText.T("Starting: ","Starter: ")+p.Name);token.ThrowIfCancellationRequested();runtime.Start(p);
    if(!runtime.WaitForProcess(p,token))throw new InvalidOperationException(p.Name+UiText.T(" did not start within 20 seconds. The remaining programs were not started."," startede ikke inden 20 sekunder. De resterende programmer blev ikke startet."));
   }
  }
  public static void Validate(StartupProgram p) {
   if(p==null||string.IsNullOrWhiteSpace(p.Name)||string.IsNullOrWhiteSpace(p.FileName)||string.IsNullOrWhiteSpace(p.ProcessName))throw new ArgumentException(UiText.T("Every program needs a name, executable/shortcut and process name.","Hvert program skal have et navn, en programfil/genvej og et procesnavn."));
   string ext=Path.GetExtension(p.FileName).ToLowerInvariant();
   if(!Path.IsPathRooted(p.FileName)||(ext!=".exe"&&ext!=".lnk"&&ext!=".appref-ms"))throw new ArgumentException(UiText.T("Select a full path to an .exe, .lnk or .appref-ms file.","Vælg en fuld sti til en .exe-, .lnk- eller .appref-ms-fil."));
   if(p.ProcessName.IndexOfAny(new[]{'\\','/',':','\r','\n'})>=0||p.ProcessName.EndsWith(".exe",StringComparison.OrdinalIgnoreCase))throw new ArgumentException(UiText.T("Enter the process name without a path or .exe extension.","Indtast procesnavnet uden sti eller .exe-endelse."));
  }
 }
 public sealed class WindowsProgramRuntime:IProgramRuntime {
  public bool IsRunning(StartupProgram program) {
   int session;using(var self=Process.GetCurrentProcess())session=self.SessionId;
   bool running=false;
   foreach(var process in Process.GetProcessesByName(program.ProcessName))using(process){try{if(process.SessionId==session)running=true;}catch(InvalidOperationException){}catch(System.ComponentModel.Win32Exception){}}
   return running;
  }
  public void Start(StartupProgram program) {
   if(!File.Exists(program.FileName))throw new FileNotFoundException(UiText.T("Program or shortcut not found: ","Program eller genvej blev ikke fundet: ")+program.FileName);
   using(var p=Process.Start(new ProcessStartInfo {FileName=program.FileName,Arguments=program.Arguments??"",WorkingDirectory=Path.GetDirectoryName(program.FileName),UseShellExecute=true})){}
  }
  public bool WaitForProcess(StartupProgram program,CancellationToken token) {
   var watch=Stopwatch.StartNew();while(watch.Elapsed.TotalSeconds<20){token.ThrowIfCancellationRequested();if(IsRunning(program))return true;token.WaitHandle.WaitOne(200);}return false;
  }
 }
}
