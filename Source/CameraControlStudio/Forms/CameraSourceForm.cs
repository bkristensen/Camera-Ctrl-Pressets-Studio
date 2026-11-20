using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace CameraControlStudio {
 public partial class CameraSourceForm:AppForm {
  public Device Selected {get{return choices.SelectedItem as Device;}}
  public CameraSourceForm(){
   InitializeComponent();if(InDesigner)return;FormText.Translate(this);
   ok.Enabled=Selected!=null;
   choices.SelectedIndexChanged+=(o,e)=>ok.Enabled=Selected!=null;
   ok.Click+=(o,e)=>{if(Selected!=null)DialogResult=DialogResult.OK;};
  }
  public CameraSourceForm(IEnumerable<Device> devices,string id):this(){foreach(var d in devices)choices.Items.Add(d);choices.SelectedItem=choices.Items.Cast<Device>().FirstOrDefault(d=>d.Id==id);}
 }
}
