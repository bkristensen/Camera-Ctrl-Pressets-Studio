using System;
using System.Text.RegularExpressions;
using System.Threading;
namespace BrioPresets {
 public sealed class ShareState {
  public bool Screen2Only,ContentOnly,Sound,VideoOptimized,ZoomWindowsHidden;
  public bool Ready { get {return Screen2Only&&ContentOnly&&Sound&&VideoOptimized&&ZoomWindowsHidden;} }
 }
 public interface IShareSurface {
  void Configure(CancellationToken token);
  ShareState ReadState(CancellationToken token);
  void Submit(CancellationToken token);
 }
 public static class ShareWorkflow {
  public static void Run(IShareSurface surface,CancellationToken token) {
   token.ThrowIfCancellationRequested();surface.Configure(token);
   token.ThrowIfCancellationRequested();var state=surface.ReadState(token);
   if(state==null||!state.Ready)throw new InvalidOperationException(UiText.T("The selected screen or sharing options could not be verified. Sharing was not started.", "Zooms skærmvalg eller delingsindstillinger kunne ikke bekræftes. Deling er ikke startet."));
   token.ThrowIfCancellationRequested();surface.Submit(token);
  }
  public static string Normalize(string s){
   string value=Regex.Replace((s??"").Replace("&","").Trim(),@"\s+"," ").ToLowerInvariant();
   return Regex.Replace(value,@"\s*\((?:alt|ctrl|control|shift)(?:\s*\+\s*[a-z0-9]+)+\)$","").Trim();
  }
  public static bool Screen2(string s){return Regex.IsMatch(Normalize(s),@"^(screen|skærm) 2($|\s*[,(:])");}
  public static bool Named(string value,params string[] names){string v=Normalize(value);foreach(string n in names)if(v==Normalize(n))return true;return false;}
 }
}
