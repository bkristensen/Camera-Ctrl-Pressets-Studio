using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BrioPresets {
 public sealed class StartupCoordinator {
  readonly Form owner;readonly Action<string> report;CancellationTokenSource running;
  public StartupCoordinator(Form form,Action<string> status){owner=form;report=status;form.FormClosed+=(s,e)=>running?.Cancel();}
  public async void Start(SavedData data,bool force=false) {
   running?.Cancel();if(!force&&!data.AutoStartPrograms)return;
   var cts=new CancellationTokenSource();running=cts;
   var programs=data.StartupPrograms.Select(p=>new StartupProgram {Name=p.Name,FileName=p.FileName,Arguments=p.Arguments,ProcessName=p.ProcessName}).ToArray();
   var progress=new Progress<string>(s=>{if(!owner.IsDisposed)report(s);});
   try {
    await Task.Run(()=>{
     using(var mutex=new Mutex(false,"Local\\CamPressetsProgramStartup")) {
      bool held=false;try{var watch=System.Diagnostics.Stopwatch.StartNew();while(!held){cts.Token.ThrowIfCancellationRequested();try{held=mutex.WaitOne(200);}catch(AbandonedMutexException){held=true;}if(watch.Elapsed.TotalSeconds>60)throw new TimeoutException(UiText.T("The other app is still starting programs. Try again later.","Det andet program starter stadig programmer. Prøv igen senere."));}
       StartupLogic.Run(programs,new WindowsProgramRuntime(),cts.Token,s=>((IProgress<string>)progress).Report(s));
      }finally{if(held)mutex.ReleaseMutex();}
     }
    });
    if(!owner.IsDisposed&&!cts.IsCancellationRequested)report(UiText.T("Startup program check completed.","Kontrollen af opstartsprogrammer er færdig."));
   }catch(OperationCanceledException){}catch(Exception ex){if(!owner.IsDisposed)MessageBox.Show(owner,ex.Message,UiText.T("Startup programs", "Opstartsprogrammer"),MessageBoxButtons.OK,MessageBoxIcon.Warning);}
   finally{if(running==cts)running=null;cts.Dispose();}
  }
 }
}
