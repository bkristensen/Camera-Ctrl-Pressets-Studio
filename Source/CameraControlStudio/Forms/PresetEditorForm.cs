using System;
using System.Drawing;
using System.Windows.Forms;
namespace CameraControlStudio {
 public partial class PresetEditorForm:AppForm {
  Func<string,bool> exists;
  public string PresetName {get{return input.Text.Trim();}}
  public string IconId {get{return choices.SelectedItem as string??"camera";}}
  public PresetEditorForm(){InitializeComponent();if(InDesigner)return;Prepare(this);FormText.Translate(this);Text=UiText.T("Edit preset","Rediger position");prompt.Text=UiText.T("Preset symbol","Symbol for position");choices.DrawMode=DrawMode.OwnerDrawFixed;choices.ItemHeight=42;choices.Items.AddRange(PresetIcons.Ids);
   choices.DrawItem+=(o,e)=>{if(e.Index<0)return;e.DrawBackground();PresetIcons.Draw(e.Graphics,(string)choices.Items[e.Index],new Rectangle(e.Bounds.X+6,e.Bounds.Y+5,60,30),e.ForeColor);TextRenderer.DrawText(e.Graphics,UiText.T("Icon: "+choices.Items[e.Index],"Icon: "+choices.Items[e.Index]),Font,new Rectangle(e.Bounds.X+78,e.Bounds.Y,e.Bounds.Width-80,e.Bounds.Height),e.ForeColor,TextFormatFlags.VerticalCenter);e.DrawFocusRectangle();};
   choices.SelectedIndexChanged+=(o,e)=>{var suggestion=UiText.T("Icon: "+IconId,"Icon: "+IconId);if(string.IsNullOrWhiteSpace(input.Text)&&!(exists?.Invoke(suggestion)??false))input.Text=suggestion;};
   ok.Click+=(o,e)=>{if(PresetName.Length==0){input.Focus();return;}if(exists?.Invoke(PresetName)??false){MessageBox.Show(this,UiText.T("Name already exists.","Navnet findes allerede."));return;}DialogResult=DialogResult.OK;};
  }
  public PresetEditorForm(Preset preset,Func<string,bool> nameExists):this(){exists=nameExists;input.Text=preset?.Name??"";if(preset!=null)choices.SelectedItem=preset.IconId??"camera";}
 }
}
