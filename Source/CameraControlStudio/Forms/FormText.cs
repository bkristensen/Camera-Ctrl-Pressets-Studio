using System.Windows.Forms;
namespace CameraControlStudio {
 internal static class FormText {
  public static void Translate(Control control){
   if(!string.IsNullOrEmpty(control.Text))
    control.Text=UiText.Translate(control.Text,control.Text);
   if(!string.IsNullOrEmpty(control.AccessibleName))
    control.AccessibleName=UiText.Translate(control.AccessibleName,control.AccessibleName);
   foreach(Control child in control.Controls)
    Translate(child);
  }
 }
}
