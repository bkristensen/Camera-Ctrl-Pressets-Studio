using System;
using System.Globalization;
namespace BrioPresets {
 /// <summary>English first, Danish on a Danish Windows display language.</summary>
 public static class UiText {
  static bool danish=CultureInfo.CurrentUICulture.TwoLetterISOLanguageName=="da";
  public static bool IsDanish {get{return danish;}}
  public static void Initialize(string[] args) {
   danish=CultureInfo.CurrentUICulture.TwoLetterISOLanguageName=="da";
   foreach(string arg in args) {
    if(string.Equals(arg,"--lang=da",StringComparison.OrdinalIgnoreCase))danish=true;
    if(string.Equals(arg,"--lang=en",StringComparison.OrdinalIgnoreCase))danish=false;
   }
  }
  public static string T(string english,string dansk){return danish?dansk:english;}
 }
}
