using System;
using System.Threading;
using BrioPresets;
class Fake:IShareSurface {
 public ShareState State=new ShareState {Screen2Only=true,ContentOnly=true,Sound=true,VideoOptimized=true,ZoomWindowsHidden=true};
 public int Configures,Submits,Reads;public CancellationTokenSource Cancel;
 public void Configure(CancellationToken t){Configures++;}
 public ShareState ReadState(CancellationToken t){Reads++;if(Cancel!=null)Cancel.Cancel();return State;}
 public void Submit(CancellationToken t){Submits++;}
}
class Program {
 static void Assert(bool ok,string message){if(!ok)throw new Exception(message);}
 static void Main(){
  foreach(string s in new[]{"Screen 2","Skærm 2","Screen 2 (1920x1080)","Screen 2, selected"})Assert(ShareWorkflow.Screen2(s),"accept screen2 "+s);
  foreach(string s in new[]{"Screen 20","Screen 1","App Screen 2","Screen 2 app","Skærm 21"})Assert(!ShareWorkflow.Screen2(s),"reject wrong screen "+s);
  Assert(ShareWorkflow.Named("Share Screen (Alt+S)","Share screen"),"shortcut suffix");
  Assert(!ShareWorkflow.Named("Stop Share","Share"),"never stop existing share");
  var ok=new Fake();ShareWorkflow.Run(ok,CancellationToken.None);Assert(ok.Configures==1&&ok.Reads==1&&ok.Submits==1,"valid share exactly once");
  for(int i=0;i<5;i++){
   var f=new Fake();if(i==0)f.State.Screen2Only=false;if(i==1)f.State.ContentOnly=false;if(i==2)f.State.Sound=false;if(i==3)f.State.VideoOptimized=false;if(i==4)f.State.ZoomWindowsHidden=false;
   try{ShareWorkflow.Run(f,CancellationToken.None);throw new Exception("expected refusal");}catch(InvalidOperationException){}
   Assert(f.Submits==0,"unverified settings must never submit");
  }
  using(var cancel=new CancellationTokenSource()){
   var f=new Fake {Cancel=cancel};try{ShareWorkflow.Run(f,cancel.Token);throw new Exception("expected cancellation");}catch(OperationCanceledException){}
   Assert(f.Submits==0,"cancel after read prevents external action");
  }
  Console.WriteLine("PASS: screen matching, shortcut labels, verified submit, five refusal paths, cancellation before submit.");
 }
}
