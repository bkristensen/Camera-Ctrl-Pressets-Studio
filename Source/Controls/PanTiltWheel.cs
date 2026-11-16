using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CamPressetsControlDemo.Controls;

public enum MoveDirection { Up, Right, Down, Left }
public enum MoveSpeed { Normal, Fast }

public sealed class MovementEventArgs(MoveDirection direction, MoveSpeed speed) : EventArgs
{
    public MoveDirection Direction { get; } = direction;
    public MoveSpeed Speed { get; } = speed;
}

/// <summary>A resolution-independent, two-speed PTZ mouse wheel.</summary>
public sealed class PanTiltWheel : FrameworkElement
{
    private const double Center = 200;
    private (MoveDirection Direction, MoveSpeed Speed)? _active;
    private static readonly Brush Outer = MakeBrush("#6699DD", "#355E9C");
    private static readonly Brush Inner = MakeBrush("#718FBF", "#405F95");
    private static readonly Brush Middle = MakeBrush("#6087BD", "#2C4F82");
    private static readonly Pen OuterPen = MakePen("#98DDFF", 2.5);
    private static readonly Pen InnerPen = MakePen("#C2DFFF", 2);
    private static readonly Pen DividerPen = MakePen("#3B6199", 1.5);
    private static readonly Pen ArrowPen = MakePen("#F2F7FF", 5);

    public event EventHandler<MovementEventArgs>? MovementStarted;
    public event EventHandler? MovementStopped;

    public PanTiltWheel()
    {
        Width = 400;
        Height = 400;
        Cursor = Cursors.Hand;
        Focusable = true;
    }

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);
        Point c = new(Center, Center);
        dc.DrawEllipse(Outer, OuterPen, c, 185, 185);
        dc.DrawEllipse(Inner, InnerPen, c, 118, 118);
        for (int angle = 45; angle < 360; angle += 90)
        {
            double rad = angle * Math.PI / 180;
            Point p1 = new(Center + Math.Sin(rad) * 46, Center - Math.Cos(rad) * 46);
            Point p2 = new(Center + Math.Sin(rad) * 185, Center - Math.Cos(rad) * 185);
            dc.DrawLine(DividerPen, p1, p2);
        }
        dc.DrawEllipse(Middle, InnerPen, c, 44, 44);
        DrawArrows(dc, 150, true);
        DrawArrows(dc, 83, false);
        // Camera icon in central, non-interactive area.
        dc.DrawRoundedRectangle(Brushes.White, null, new Rect(184, 190, 25, 21), 4, 4);
        var cameraLens = StreamGeometry.Parse("M 213,196 L 225,189 225,212 213,205 Z");
        dc.DrawGeometry(Brushes.White, null, cameraLens);
    }

    private static void DrawArrows(DrawingContext dc, double radius, bool fast)
    {
        for (int a = 0; a < 360; a += 90)
        {
            dc.PushTransform(new RotateTransform(a, Center, Center));
            double y = Center - radius;
            DrawChevron(dc, y + (fast ? -6 : 0));
            if (fast) DrawChevron(dc, y + 9);
            dc.Pop();
        }
    }

    private static void DrawChevron(DrawingContext dc, double y)
    {
        dc.DrawLine(ArrowPen, new Point(187, y + 8), new Point(200, y - 5));
        dc.DrawLine(ArrowPen, new Point(200, y - 5), new Point(213, y + 8));
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        Focus();
        if (TryGetMovement(e.GetPosition(this), out var movement))
        {
            CaptureMouse();
            ChangeMovement(movement);
        }
        e.Handled = true;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (!IsMouseCaptured || e.LeftButton != MouseButtonState.Pressed) return;
        if (TryGetMovement(e.GetPosition(this), out var movement))
            ChangeMovement(movement);
        else
            StopMovement();
    }

    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonUp(e);
        StopMovement();
        if (IsMouseCaptured) ReleaseMouseCapture();
        e.Handled = true;
    }

    protected override void OnLostMouseCapture(MouseEventArgs e)
    {
        base.OnLostMouseCapture(e);
        StopMovement();
    }

    private static bool TryGetMovement(Point p, out (MoveDirection Direction, MoveSpeed Speed) movement)
    {
        double dx = p.X - Center, dy = p.Y - Center;
        double distance = Math.Sqrt(dx * dx + dy * dy);
        movement = default;
        if (distance < 47 || distance > 185) return false;
        MoveDirection direction = Math.Abs(dx) > Math.Abs(dy)
            ? (dx > 0 ? MoveDirection.Right : MoveDirection.Left)
            : (dy > 0 ? MoveDirection.Down : MoveDirection.Up);
        movement = (direction, distance <= 118 ? MoveSpeed.Normal : MoveSpeed.Fast);
        return true;
    }

    private void ChangeMovement((MoveDirection Direction, MoveSpeed Speed) next)
    {
        if (_active == next) return;
        StopMovement();
        _active = next;
        InvalidateVisual();
        MovementStarted?.Invoke(this, new MovementEventArgs(next.Direction, next.Speed));
    }

    private void StopMovement()
    {
        if (_active is null) return;
        _active = null;
        InvalidateVisual();
        MovementStopped?.Invoke(this, EventArgs.Empty);
    }

    private static Pen MakePen(string color, double thickness)
    {
        var pen = new Pen((Brush)new BrushConverter().ConvertFromString(color)!, thickness);
        pen.StartLineCap = PenLineCap.Round;
        pen.EndLineCap = PenLineCap.Round;
        pen.LineJoin = PenLineJoin.Round;
        pen.Freeze();
        return pen;
    }
    private static Brush MakeBrush(string top, string bottom)
    {
        var a = (Color)ColorConverter.ConvertFromString(top);
        var b = (Color)ColorConverter.ConvertFromString(bottom);
        var brush = new LinearGradientBrush(a, b, 90);
        brush.Freeze();
        return brush;
    }
}
