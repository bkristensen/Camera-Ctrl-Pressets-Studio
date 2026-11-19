using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace CameraControlStudio {
 public class AppForm:Form {
  public AppForm(){Font=new Font("Segoe UI",9);AutoScaleMode=AutoScaleMode.Dpi;BackColor=BlueTheme.Background;ForeColor=Color.White;}
  protected bool InDesigner {get{return LicenseManager.UsageMode==LicenseUsageMode.Designtime||DesignMode;}}
  protected void Prepare(Control root){
   foreach(Control c in root.Controls){
    c.ForeColor=Color.White;c.BackColor=BlueTheme.Panel;
    if(c is TextBox t){t.AutoSize=false;t.Height=28;t.Font=new Font("Segoe UI",10);t.BackColor=BlueTheme.Background;}
    if(c is ComboBox combo){combo.DrawMode=DrawMode.OwnerDrawFixed;combo.ItemHeight=22;combo.DrawItem+=(o,e)=>{if(e.Index<0)return;e.DrawBackground();TextRenderer.DrawText(e.Graphics,combo.GetItemText(combo.Items[e.Index]),combo.Font,e.Bounds,e.ForeColor,TextFormatFlags.VerticalCenter);};combo.BackColor=BlueTheme.Background;}
    if(c is ListBox list){list.ItemHeight=28;list.BackColor=BlueTheme.Background;}
    if(c is LinkLabel link){link.LinkColor=Color.LightSkyBlue;link.VisitedLinkColor=Color.LightSkyBlue;}
    if(c is NumericUpDown number){number.AutoSize=false;number.MinimumSize=new Size(65,28);number.BackColor=BlueTheme.Background;}
    Prepare(c);
   }
  }
 }
}
