using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace BrioPresets {
 public static class BlueTheme {
  public static readonly Color Background=Color.FromArgb(5,40,79),Panel=Color.FromArgb(9,52,94),Line=Color.FromArgb(73,144,214);
  public static GraphicsPath Round(RectangleF r,float radius) {var p=new GraphicsPath();float d=radius*2;p.AddArc(r.X,r.Y,d,d,180,90);p.AddArc(r.Right-d,r.Y,d,d,270,90);p.AddArc(r.Right-d,r.Bottom-d,d,d,0,90);p.AddArc(r.X,r.Bottom-d,d,d,90,90);p.CloseFigure();return p;}
  public static void Apply(Control c) {
   c.ForeColor=Color.White;c.BackColor=Panel;
   if(c is Button b){b.FlatStyle=FlatStyle.Flat;b.FlatAppearance.BorderColor=Line;b.BackColor=Color.FromArgb(20,79,136);}
   if(c is TextBoxBase||c is ListBox||c is ComboBox||c is NumericUpDown)c.BackColor=Background;
   foreach(Control child in c.Controls)Apply(child);
  }
 }
 public class BluePanel:Panel {
  public BluePanel(){DoubleBuffered=true;BackColor=BlueTheme.Panel;Padding=new Padding(14);}
  protected override void OnPaint(PaintEventArgs e){base.OnPaint(e);e.Graphics.SmoothingMode=SmoothingMode.AntiAlias;using(var path=BlueTheme.Round(new RectangleF(1,1,Width-3,Height-3),14))using(var pen=new Pen(BlueTheme.Line,1.5f))e.Graphics.DrawPath(pen,path);}
 }
 public class BlueButton:Button {
  bool over,down;public bool Primary {get;set;}
  public BlueButton(){SetStyle(ControlStyles.UserPaint|ControlStyles.OptimizedDoubleBuffer|ControlStyles.AllPaintingInWmPaint,true);FlatStyle=FlatStyle.Flat;ForeColor=Color.White;BackColor=BlueTheme.Panel;Cursor=Cursors.Hand;}
  protected override void OnMouseEnter(EventArgs e){over=true;Invalidate();base.OnMouseEnter(e);}protected override void OnMouseLeave(EventArgs e){over=false;down=false;Invalidate();base.OnMouseLeave(e);}
  protected override void OnMouseDown(MouseEventArgs e){down=true;Invalidate();base.OnMouseDown(e);}protected override void OnMouseUp(MouseEventArgs e){down=false;Invalidate();base.OnMouseUp(e);}
  protected override void OnPaint(PaintEventArgs e){var g=e.Graphics;g.SmoothingMode=SmoothingMode.AntiAlias;var r=new RectangleF(2,2,Width-5,Height-5);if(r.Width<=0||r.Height<=0)return;using(var p=BlueTheme.Round(r,10))using(var brush=new LinearGradientBrush(r,down?Color.FromArgb(0,65,125):Primary||over?Color.FromArgb(10,155,255):Color.FromArgb(39,106,175),Color.FromArgb(6,46,88),90))using(var pen=new Pen(Primary||over?Color.FromArgb(80,219,255):BlueTheme.Line,1.5f)){g.FillPath(brush,p);g.DrawPath(pen,p);}TextRenderer.DrawText(g,Text,Font,Rectangle.Round(r),Enabled?ForeColor:Color.Gray,TextFormatFlags.HorizontalCenter|TextFormatFlags.VerticalCenter|TextFormatFlags.EndEllipsis);if(Focused)ControlPaint.DrawFocusRectangle(g,new Rectangle(7,7,Width-15,Height-15));}
 }
 public sealed class PanWheel:Control {
  public event Action<string,int,bool> MovementRequested;public event Action Released;
  int active=-1;public PanWheel(){DoubleBuffered=true;BackColor=BlueTheme.Background;AccessibleName=UiText.T("Camera direction wheel. Inner ring normal, outer ring fast.","Kamerahjul. Inderste ring normal, yderste ring hurtig.");TabStop=true;}
  int Hit(Point p){float size=Math.Min(Width,Height)-24,cx=Width/2f,cy=Height/2f,dx=p.X-cx,dy=p.Y-cy,r=(float)Math.Sqrt(dx*dx+dy*dy);if(r<size*.18||r>size*.5)return -1;int direction=((int)Math.Floor((Math.Atan2(dy,dx)*180/Math.PI+405)/90))%4;return direction+(r>size*.315?4:0);}
  void Trigger(int n){if(n<0)return;MovementRequested?.Invoke(n%4==0||n%4==2?"Pan":"Tilt",n%4==0||n%4==3?1:-1,n>=4);}
  protected override void OnMouseDown(MouseEventArgs e){base.OnMouseDown(e);if(e.Button!=MouseButtons.Left||!Enabled)return;Focus();active=Hit(e.Location);if(active>=0){Capture=true;Trigger(active);}Invalidate();}
  protected override void OnMouseUp(MouseEventArgs e){base.OnMouseUp(e);Release();}
  protected override void OnMouseCaptureChanged(EventArgs e){base.OnMouseCaptureChanged(e);if(!Capture)Release();}
  void Release(){active=-1;Capture=false;Released?.Invoke();Invalidate();}
  protected override bool IsInputKey(Keys key){return key==Keys.Up||key==Keys.Down||key==Keys.Left||key==Keys.Right||base.IsInputKey(key);}
  protected override void OnKeyDown(KeyEventArgs e){base.OnKeyDown(e);int n=e.KeyCode==Keys.Right?0:e.KeyCode==Keys.Down?1:e.KeyCode==Keys.Left?2:e.KeyCode==Keys.Up?3:-1;if(n>=0){Trigger(n+(e.Shift?4:0));e.Handled=true;}}
  protected override void OnKeyUp(KeyEventArgs e){base.OnKeyUp(e);Released?.Invoke();}
  protected override void OnPaint(PaintEventArgs e){base.OnPaint(e);var g=e.Graphics;g.SmoothingMode=SmoothingMode.AntiAlias;float size=Math.Min(Width,Height)-24,cx=Width/2f,cy=Height/2f;if(size<60)return;
   for(int ring=0;ring<2;ring++)for(int dir=0;dir<4;dir++) {float outer=size*(ring==0?.305f:.5f),inner=size*(ring==0?.18f:.325f);float angle=dir*90-43;
    using(var p=new GraphicsPath()){p.AddArc(cx-outer,cy-outer,outer*2,outer*2,angle,86);p.AddArc(cx-inner,cy-inner,inner*2,inner*2,angle+86,-86);p.CloseFigure();var r=p.GetBounds();bool selected=active==dir+ring*4;
     using(var b=new LinearGradientBrush(r,selected?Color.FromArgb(65,210,255):ring==1?Color.FromArgb(0,142,238):Color.FromArgb(38,100,160),Color.FromArgb(3,44,85),90))g.FillPath(b,p);
     if(ring==1)using(var glow=new Pen(Color.FromArgb(40,0,200,255),8))g.DrawPath(glow,p);
     using(var pen=new Pen(ring==1?Color.FromArgb(92,222,255):BlueTheme.Line,2))g.DrawPath(pen,p);
    }
    float radius=(outer+inner)/2;var state=g.Save();g.TranslateTransform(cx+(float)Math.Cos(dir*Math.PI/2)*radius,cy+(float)Math.Sin(dir*Math.PI/2)*radius);g.RotateTransform(dir*90);
    using(var pen=new Pen(Enabled?Color.White:Color.SlateGray,Math.Max(3,size/75))){pen.StartCap=pen.EndCap=LineCap.Round;float a=size*.025f;for(int n=0;n<(ring==1?2:1);n++){float x=(n-(ring==1?.5f:0))*a*1.5f;g.DrawLines(pen,new[]{new PointF(x-a,-a*1.5f),new PointF(x+a,0),new PointF(x-a,a*1.5f)});}}g.Restore(state);
   }
   float center=size*.16f;using(var b=new LinearGradientBrush(new RectangleF(cx-center,cy-center,center*2,center*2),Color.FromArgb(23,101,173),Color.FromArgb(1,28,59),90))g.FillEllipse(b,cx-center,cy-center,center*2,center*2);using(var p=new Pen(BlueTheme.Line,2))g.DrawEllipse(p,cx-center,cy-center,center*2,center*2);
   using(var p=BlueTheme.Round(new RectangleF(cx-size*.07f,cy-size*.04f,size*.105f,size*.08f),4))g.FillPath(Brushes.White,p);g.FillPolygon(Brushes.White,new[]{new PointF(cx+size*.045f,cy-size*.025f),new PointF(cx+size*.08f,cy-size*.04f),new PointF(cx+size*.08f,cy+size*.04f),new PointF(cx+size*.045f,cy+size*.025f)});
  }
 }
}
