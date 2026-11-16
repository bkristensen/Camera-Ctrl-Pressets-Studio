using System;
using System.IO;
using System.Runtime.Serialization.Json;
namespace BrioPresets {
 public static class SelfTest {
  static void Assert(bool ok,string message){if(!ok)throw new Exception(message);}
  public static void Run(){
   UiText.Initialize(new[]{"--lang=en"});Assert(UiText.T("Save", "Gem")=="Save","English override");
   UiText.Initialize(new[]{"--lang=da"});Assert(UiText.T("Save", "Gem")=="Gem","Danish override");
   var oldCulture=System.Globalization.CultureInfo.CurrentUICulture;
   System.Globalization.CultureInfo.CurrentUICulture=new System.Globalization.CultureInfo("da-DK");
   UiText.Initialize(new string[0]);Assert(UiText.IsDanish,"Danish Windows language");
   System.Globalization.CultureInfo.CurrentUICulture=new System.Globalization.CultureInfo("de-DE");
   UiText.Initialize(new string[0]);Assert(!UiText.IsDanish,"English fallback for other display languages");
   System.Globalization.CultureInfo.CurrentUICulture=oldCulture;UiText.Initialize(new string[0]);
   Assert(Motion.Ease(0)==0&&Motion.Ease(1)==1,"endpoints");double previous=0;
   for(int i=0;i<=10000;i++){double x=Motion.Ease(i/10000.0);Assert(x>=previous-1e-12&&x>=0&&x<=1,"monotonic easing");previous=x;}
   var s=new Setting { Min=-180,Max=180,Step=3 };
   Assert(ManualMotion.Target(s,0,1,1,false)==3,"fine step");
   Assert(ManualMotion.Target(s,0,1,5,false)==15,"normal steps");
   Assert(ManualMotion.Target(s,0,1,5,true)==-15,"reverse positive direction");
   Assert(ManualMotion.Target(s,0,-1,1,true)==3,"reverse negative direction");
   Assert(ManualMotion.Target(s,177,1,5,false)==180,"upper limit");
   Assert(ManualMotion.Target(s,-177,-1,5,false)==-180,"lower limit");
   for(int i=-500;i<=500;i++){int v=s.Snap(i);Assert(v>=-180&&v<=180&&(v+180)%3==0,"driver range and step");}
   s.Min=10;s.Max=19;s.Step=4;Assert(s.Snap(19)==18,"unaligned upper limit");
   var data=new SavedData {PanSteps=7,FinePanSteps=2,ZoomSteps=9,FineZoomSteps=1,ReverseHorizontal=true,ReverseVertical=true};data.Cameras["kamera1"]=new System.Collections.Generic.List<Preset>{new Preset{Name="Café højre",Position=new System.Collections.Generic.Dictionary<string,int>{{"Pan",12},{"Zoom",200}}}};
   using(var stream=new MemoryStream()){var serializer=new DataContractJsonSerializer(typeof(SavedData));serializer.WriteObject(stream,data);stream.Position=0;var copy=(SavedData)serializer.ReadObject(stream);Assert(copy.PanSteps==7&&copy.FinePanSteps==2&&copy.ZoomSteps==9&&copy.FineZoomSteps==1&&copy.ReverseHorizontal&&copy.ReverseVertical,"manual settings roundtrip");Assert(copy.Cameras["kamera1"][0].Name=="Café højre"&&copy.Cameras["kamera1"][0].Position["Zoom"]==200,"preset roundtrip");}
   File.WriteAllText("self-test-result.txt","PASS: fine/normal steps, direction reversal, settings persistence, easing, step alignment, bounds, Unicode preset roundtrip.");
  }
 }
}
