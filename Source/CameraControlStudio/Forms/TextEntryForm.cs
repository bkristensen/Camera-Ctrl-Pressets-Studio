using System;
using System.Windows.Forms;
namespace CameraControlStudio {
 public partial class TextEntryForm:AppForm {
  public string Value {get{return input.Text.Trim();}}
  public TextEntryForm(){
   InitializeComponent();if(InDesigner)return;FormText.Translate(this);
   ok.Enabled=Value.Length>0;
   input.TextChanged+=(o,e)=>ok.Enabled=Value.Length>0;
   ok.Click+=(o,e)=>{if(Value.Length>0)DialogResult=DialogResult.OK;else input.Focus();};
  }
  public TextEntryForm(string title,string value):this(){Text=title;prompt.Text=title;input.Text=value;Shown+=(o,e)=>{input.Focus();input.SelectAll();};}
 }
}
