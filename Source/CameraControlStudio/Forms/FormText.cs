using System.Windows.Forms;
namespace CameraControlStudio {
 internal static class FormText {
  public static void Translate(Control parent){foreach(Control c in parent.Controls){if(!string.IsNullOrEmpty(c.Text))c.Text=UiText.T(c.Text,c.Text);Translate(c);}}
 }
}
