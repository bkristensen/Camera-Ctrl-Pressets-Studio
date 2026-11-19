using System.Windows.Forms;
namespace CameraControlStudio {
 internal static class FormText {
  public static void Translate(Control parent){
   foreach(Control c in parent.Controls){
    if(!string.IsNullOrEmpty(c.Text))
     c.Text=UiText.Translate(c.Text,c.Text);
    if(!string.IsNullOrEmpty(c.AccessibleName))
     c.AccessibleName=UiText.Translate(c.AccessibleName,c.AccessibleName);
    Translate(c);
   }
  }
 }
}
