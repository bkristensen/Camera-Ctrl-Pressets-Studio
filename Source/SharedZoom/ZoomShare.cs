using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace BrioPresets {
 public static class ZoomShareButton {
  public static Button Create(Form owner,Action<string> status,Action stopMotion) {
   var button=new Button { Text=UiText.T("Share screen 2 in Zoom", "Del skærm 2 i Zoom"),Dock=DockStyle.Fill,Height=30 };
   var lifetime=new CancellationTokenSource();owner.FormClosed+=(s,e)=>lifetime.Cancel();
   button.Click+=async (s,e)=>{
    if(Screen.AllScreens.Length<2){MessageBox.Show(owner,UiText.T("Windows needs at least two active displays in extended mode.", "Windows skal have mindst to aktive skærme i udvidet tilstand."),UiText.T("Zoom sharing", "Zoom-deling"));return;}
    stopMotion();button.Enabled=false;status(UiText.T("Preparing screen 2 in Zoom …", "Indstiller skærm 2 i Zoom …"));
    using(var timeout=CancellationTokenSource.CreateLinkedTokenSource(lifetime.Token)) {
     timeout.CancelAfter(TimeSpan.FromSeconds(20));
     try {
      string result=await Task.Run(()=>ZoomShare.Run(timeout.Token));
      if(!owner.IsDisposed)status(result);
     }catch(Exception ex){if(!owner.IsDisposed){status(UiText.T("Check Zoom sharing.", "Zoom-deling kræver kontrol."));MessageBox.Show(owner,
      (ex is OperationCanceledException?UiText.T("The attempt was cancelled or timed out.", "Forsøget blev afbrudt eller tog for lang tid."):ex.Message)+
      UiText.T("\n\nTry opening the Zoom sharing dialog manually, then click the button again. Do not interact with Zoom during the attempt. Check the sharing status in Zoom.", "\n\nÅbn eventuelt Zooms delingsvindue manuelt, og prøv knappen igen. Undlad at betjene Zoom under forsøget. Kontrollér delingsstatus i Zoom."),UiText.T("Zoom sharing", "Zoom-deling"),MessageBoxButtons.OK,MessageBoxIcon.Information);}}
     finally{if(!owner.IsDisposed)button.Enabled=true;}
    }
   };
   return button;
  }
 }
 public sealed class ZoomShare:IShareSurface {
  static readonly string[] Sound={"Share sound","Share computer sound","Del lyd","Del computerlyd"};
  static readonly string[] Optimize={"Optimize for video sharing","Optimize Share for Full-screen Video Clip","Optimer til videodeling","Optimer til deling af video"};
  static readonly string[] Content={"Content only","Kun indhold"};
  static readonly string[] ZoomWindows={"Share Zoom windows","Del Zoom-vinduer","Del Zoom vinduer"};
  AutomationElement dialog;bool submitted;
  [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hwnd);
  [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hwnd,int command);
  [DllImport("user32.dll")] static extern bool IsIconic(IntPtr hwnd);

  public static string Run(CancellationToken token) {
   using(var mutex=new Mutex(false,"Local\\BrioZoomShareOperation")) {
    bool taken=false;
    try {
     try{taken=mutex.WaitOne(0);}catch(AbandonedMutexException){taken=true;}
     if(!taken)throw new InvalidOperationException(UiText.T("The other Cam Pressets app is already preparing Zoom sharing.", "Det andet Cam Pressets-program er allerede i gang med Zoom-deling."));
     var session=new ZoomShare();session.OpenDialog(token);
     try{ShareWorkflow.Run(session,token);return session.VerifyStarted(token);}
     catch(Exception ex){if(session.submitted)throw new InvalidOperationException(UiText.T("The share command was sent, but the result could not be confirmed. Check Zoom before trying again.\n", "Delingskommandoen er sendt, men resultatet kunne ikke bekræftes. Kontrollér Zoom før du prøver igen.\n")+ex.Message);throw;}
    }finally{if(taken)mutex.ReleaseMutex();}
   }
  }
  static List<AutomationElement> Windows(CancellationToken token) {
   token.ThrowIfCancellationRequested();var ids=new HashSet<int>();
   foreach(var p in Process.GetProcessesByName("Zoom")){using(p)ids.Add(p.Id);}
   if(ids.Count==0)throw new InvalidOperationException(UiText.T("Zoom is not running. Open Zoom and join the meeting first.", "Zoom er ikke startet. Åbn Zoom, og deltag i mødet først."));
   var result=new List<AutomationElement>();
   foreach(AutomationElement w in AutomationElement.RootElement.FindAll(TreeScope.Children,Condition.TrueCondition)) {
    token.ThrowIfCancellationRequested();try{if(ids.Contains(w.Current.ProcessId))result.Add(w);}catch(ElementNotAvailableException){}
   }
   return result;
  }
  static List<AutomationElement> Items(AutomationElement root,CancellationToken token) {
   token.ThrowIfCancellationRequested();var items=root.FindAll(TreeScope.Descendants,Condition.TrueCondition);var result=new List<AutomationElement>();
   foreach(AutomationElement item in items){token.ThrowIfCancellationRequested();try{if(!item.Current.IsOffscreen)result.Add(item);}catch(ElementNotAvailableException){}}
   return result;
  }
  static bool Name(AutomationElement e,string[] names){return ShareWorkflow.Named(e.Current.Name,names);}
  static AutomationElement Unique(IEnumerable<AutomationElement> items,string description) {
   var list=items.ToList();if(list.Count!=1)throw new InvalidOperationException(UiText.T("Could not identify one unique Zoom control: ", "Kunne ikke identificere én entydig Zoom-kontrol: ")+description+UiText.T(" (found: ", " (fundet: ")+list.Count+").");return list[0];
  }
  static bool HasPattern(AutomationElement e,AutomationPattern pattern){object p;return e.TryGetCurrentPattern(pattern,out p);}
  static bool IsStop(AutomationElement e){return ShareWorkflow.Named(e.Current.Name,"Stop Share","Stop sharing","Stop deling","Stop skærmdeling");}
  void OpenDialog(CancellationToken token) {
   var windows=Windows(token);
   foreach(var w in windows)if(Items(w,token).Any(IsStop))throw new InvalidOperationException(UiText.T("Zoom is already sharing. Stop the existing share in Zoom first; this button will not change it.", "Zoom deler allerede. Stop den eksisterende deling i Zoom først; knappen ændrer den ikke."));
   dialog=FindDialog(windows,token);if(dialog!=null)return;
   var meetingWindows=new List<AutomationElement>();
   foreach(var w in windows) {
    var list=Items(w,token);
    bool participants=list.Any(e=>ShareWorkflow.Named(e.Current.Name,"Participants","Deltagere")||ShareWorkflow.Normalize(e.Current.Name).StartsWith("participants ("));
    bool audio=list.Any(e=>ShareWorkflow.Named(e.Current.Name,"Mute","Unmute","Slå lyd fra","Slå lyd til","Mute My Audio","Unmute My Audio"));
    if(participants&&audio)meetingWindows.Add(w);
   }
   var meeting=Unique(meetingWindows,UiText.T("meeting window with participants and microphone controls", "mødevindue med deltagere og mikrofonknap"));
   IntPtr handle=new IntPtr(meeting.Current.NativeWindowHandle);
   if(handle!=IntPtr.Zero){if(IsIconic(handle))ShowWindow(handle,9);SetForegroundWindow(handle);}
   var share=Unique(Items(meeting,token).Where(e=>ShareWorkflow.Named(e.Current.Name,"Share Screen","Share screen","Share","Del skærm","Del")&&HasPattern(e,InvokePattern.Pattern)),UiText.T("open screen sharing", "åbn skærmdeling"));
   Invoke(share,token);
   for(int i=0;i<24;i++){token.ThrowIfCancellationRequested();dialog=FindDialog(Windows(token),token);if(dialog!=null)return;Pause(token);}
   throw new InvalidOperationException(UiText.T("The Zoom sharing dialog was not found. Open it manually and try again.", "Zooms delingsvindue blev ikke fundet. Åbn det manuelt og prøv igen."));
  }
  static AutomationElement FindDialog(List<AutomationElement> windows,CancellationToken token) {
   var matches=new List<AutomationElement>();
   foreach(var w in windows){var items=Items(w,token);if(items.Any(e=>Name(e,Sound))&&items.Any(e=>Name(e,Optimize)))matches.Add(w);}
   if(matches.Count>1)throw new InvalidOperationException(UiText.T("Multiple Zoom sharing dialogs are open. Keep only the sharing dialog for the intended meeting open.", "Der er flere Zoom-delingsvinduer. Behold kun det ønskede mødes delingsvindue åbent."));
   return matches.FirstOrDefault();
  }
  static void Pause(CancellationToken token){if(token.WaitHandle.WaitOne(200))token.ThrowIfCancellationRequested();}
  static void Invoke(AutomationElement element,CancellationToken token) {
   token.ThrowIfCancellationRequested();if(!element.Current.IsEnabled)throw new InvalidOperationException(UiText.T("The Zoom button is disabled. Check your permission to share in the meeting.", "Zoom-knappen er deaktiveret. Kontrollér din ret til at dele i mødet."));
   object pattern;if(!element.TryGetCurrentPattern(InvokePattern.Pattern,out pattern))throw new InvalidOperationException(UiText.T("The Zoom button cannot be invoked through Windows.", "Zoom-knappen kan ikke aktiveres via Windows."));
   token.ThrowIfCancellationRequested();((InvokePattern)pattern).Invoke();
  }
  AutomationElement Selector(Func<string,bool> match,string description,CancellationToken token) {
   return Unique(Items(dialog,token).Where(e=>match(e.Current.Name)&&(HasPattern(e,SelectionItemPattern.Pattern)||HasPattern(e,TogglePattern.Pattern))),description);
  }
  static bool Selected(AutomationElement e) {
   object p;if(e.TryGetCurrentPattern(SelectionItemPattern.Pattern,out p))return ((SelectionItemPattern)p).Current.IsSelected;
   if(e.TryGetCurrentPattern(TogglePattern.Pattern,out p))return ((TogglePattern)p).Current.ToggleState==ToggleState.On;
   return false;
  }
  static void Select(AutomationElement e,CancellationToken token) {
   token.ThrowIfCancellationRequested();if(!e.Current.IsEnabled)throw new InvalidOperationException(UiText.T("This Zoom option is disabled.", "Valget i Zoom er deaktiveret."));
   object p;if(e.TryGetCurrentPattern(SelectionItemPattern.Pattern,out p))((SelectionItemPattern)p).Select();
   else if(e.TryGetCurrentPattern(TogglePattern.Pattern,out p)){if(((TogglePattern)p).Current.ToggleState!=ToggleState.On)((TogglePattern)p).Toggle();}
   else throw new InvalidOperationException(UiText.T("This Zoom option cannot be verified.", "Zoom-valget kan ikke kontrolleres."));
   Pause(token);if(!Selected(e))throw new InvalidOperationException(UiText.T("Zoom did not confirm the requested selection.", "Zoom bekræftede ikke det ønskede valg."));
  }
  AutomationElement CheckBox(string[] names,CancellationToken token,bool optional=false) {
   var named=Items(dialog,token).Where(e=>Name(e,names)).ToList();
   if(optional&&named.Count==0)return null;
   return Unique(named.Where(e=>HasPattern(e,TogglePattern.Pattern)),names[0]);
  }
  static bool Checked(AutomationElement e){object p;return e!=null&&e.TryGetCurrentPattern(TogglePattern.Pattern,out p)&&((TogglePattern)p).Current.ToggleState==ToggleState.On;}
  void SetCheck(string[] names,bool value,CancellationToken token,bool optional=false) {
   var e=CheckBox(names,token,optional);if(e==null)return;
   token.ThrowIfCancellationRequested();if(!e.Current.IsEnabled)throw new InvalidOperationException(names[0]+UiText.T(" is disabled in Zoom.", " er deaktiveret i Zoom."));
   var pattern=(TogglePattern)e.GetCurrentPattern(TogglePattern.Pattern);
   if(pattern.Current.ToggleState!=(value?ToggleState.On:ToggleState.Off))pattern.Toggle();
   Pause(token);if(pattern.Current.ToggleState!=(value?ToggleState.On:ToggleState.Off))throw new InvalidOperationException(names[0]+UiText.T(" could not be set.", " kunne ikke indstilles."));
  }
  public void Configure(CancellationToken token) {
   Select(Selector(ShareWorkflow.Screen2,"Screen 2",token),token);
   Select(Selector(n=>ShareWorkflow.Named(n,Content),"Content only",token),token);
   SetCheck(Sound,true,token);SetCheck(Optimize,true,token);SetCheck(ZoomWindows,false,token,true);
  }
  public ShareState ReadState(CancellationToken token) {
   var screen=Selector(ShareWorkflow.Screen2,"Screen 2",token);bool only=false;object p;
   // Reject multiple selected screens or application windows, even if Screen 2 is selected.
   if(screen.TryGetCurrentPattern(SelectionItemPattern.Pattern,out p)) {
    var item=(SelectionItemPattern)p;var container=item.Current.SelectionContainer;object selection;
    if(container!=null&&container.TryGetCurrentPattern(SelectionPattern.Pattern,out selection)){
     var selected=((SelectionPattern)selection).Current.GetSelection();only=selected.Length==1&&Automation.Compare(selected[0],screen)&&item.Current.IsSelected;
    }
   }
   var zw=CheckBox(ZoomWindows,token,true);
   return new ShareState {Screen2Only=only,ContentOnly=Selected(Selector(n=>ShareWorkflow.Named(n,Content),"Content only",token)),
    Sound=Checked(CheckBox(Sound,token)),VideoOptimized=Checked(CheckBox(Optimize,token)),ZoomWindowsHidden=zw==null||((TogglePattern)zw.GetCurrentPattern(TogglePattern.Pattern)).Current.ToggleState==ToggleState.Off};
  }
  public void Submit(CancellationToken token) {
   var share=Unique(Items(dialog,token).Where(e=>ShareWorkflow.Named(e.Current.Name,"Share","Del")&&HasPattern(e,InvokePattern.Pattern)),"Share");
   // Re-read immediately before the external action; no keyboard toggles or screen coordinates.
   if(!ReadState(token).Ready)throw new InvalidOperationException(UiText.T("The sharing options changed. Try again.", "Delingsvalgene ændrede sig. Prøv igen."));
   token.ThrowIfCancellationRequested();submitted=true;Invoke(share,token);
  }
  string VerifyStarted(CancellationToken token) {
   for(int i=0;i<20;i++){foreach(var w in Windows(token))if(Items(w,token).Any(IsStop))return UiText.T("Zoom is now sharing. Check screen 2 in Zoom.", "Zoom deler nu. Kontrollér skærm 2 i Zoom.");Pause(token);}
   return UiText.T("Share command sent. Sharing could not be confirmed; check Zoom.", "Delingskommando sendt. Starten kunne ikke bekræftes; kontrollér Zoom.");
  }
 }
}
