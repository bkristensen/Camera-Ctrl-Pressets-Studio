using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CamPressetsControlDemo.Controls;

namespace CamPressetsControlDemo;

public sealed class CameraPreset
{
    public string Name { get; set; } = "";
    // Demonstration coordinates only. Replace with actual camera values in production.
    public double Pan { get; set; }
    public double Tilt { get; set; }
    public double Zoom { get; set; }
}

public sealed class CameraProfile
{
    public string Name { get; set; } = "Standard";
    public List<CameraPreset> Presets { get; set; } = new();
}

public partial class MainWindow : Window
{
    private readonly ObservableCollection<CameraProfile> _profiles = new();
    private readonly DispatcherTimer _movementTimer = new() { Interval = TimeSpan.FromMilliseconds(50) };
    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "CamPressetsControlDemo", "profiles.json");

    private CameraProfile? CurrentProfile => ProfilePicker.SelectedItem as CameraProfile;
    private MoveDirection? _direction;
    private MoveSpeed _speed;
    private int _zoomDirection;
    private double _pan, _tilt, _zoom;

    public MainWindow()
    {
        InitializeComponent();
        _movementTimer.Tick += MovementTimer_Tick;
        LoadProfiles();
        ProfilePicker.ItemsSource = _profiles;
        ProfilePicker.SelectedIndex = 0;
        Closed += (_, _) => { _movementTimer.Stop(); SaveProfiles(); };
    }

    private void LoadProfiles()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                var loaded = JsonSerializer.Deserialize<List<CameraProfile>>(File.ReadAllText(SettingsPath));
                if (loaded is not null)
                    foreach (var profile in loaded.Where(p => !string.IsNullOrWhiteSpace(p.Name)))
                        _profiles.Add(profile);
            }
        }
        catch (Exception ex)
        {
            Status.Text = $"Kunne ikke indlæse profiler: {ex.Message}";
        }
        if (_profiles.Count == 0)
            _profiles.Add(new CameraProfile { Name = "Standard" });
    }

    private void SaveProfiles()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SettingsPath)!);
            File.WriteAllText(SettingsPath, JsonSerializer.Serialize(_profiles.ToList(), new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Kunne ikke gemme profiler: {ex.Message}", "Gemning mislykkedes", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void ProfilePicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        StopAllMovement();
        PresetList.ItemsSource = CurrentProfile?.Presets;
        if (CurrentProfile is not null)
            Status.Text = $"Profil: {CurrentProfile.Name} · {CurrentProfile.Presets.Count} presets · Simuleret kamera";
    }

    private void AddProfile_Click(object sender, RoutedEventArgs e)
    {
        var name = PromptForName("Ny profil", "Profilnavn:", $"Profil {_profiles.Count + 1}");
        if (name is null) return;
        if (_profiles.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "Der findes allerede en profil med dette navn.", "Profil findes", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        var profile = new CameraProfile { Name = name };
        _profiles.Add(profile);
        ProfilePicker.SelectedItem = profile;
        SaveProfiles();
    }

    private void SavePreset_Click(object sender, RoutedEventArgs e)
    {
        if (CurrentProfile is null) return;
        var name = PromptForName("Gem aktuel position", "Navn på preset:", $"Position {CurrentProfile.Presets.Count + 1}");
        if (name is null) return;
        if (CurrentProfile.Presets.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show(this, "Der findes allerede et preset med dette navn.", "Preset findes", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        StopAllMovement();
        CurrentProfile.Presets.Add(new CameraPreset { Name = name, Pan = _pan, Tilt = _tilt, Zoom = _zoom });
        PresetList.Items.Refresh();
        SaveProfiles();
        Status.Text = $"Preset gemt: {name} (simuleret position)";
        // Production: read the actual device's absolute PTZ position before saving.
    }

    private void Preset_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as Button)?.Tag is not CameraPreset preset) return;
        StopAllMovement();
        _pan = preset.Pan;
        _tilt = preset.Tilt;
        _zoom = preset.Zoom;
        Status.Text = $"Preset valgt: {preset.Name} (simuleret position)";
        // TODO: await camera.MoveToAsync(preset.Pan, preset.Tilt, preset.Zoom);
    }

    private static string? PromptForName(string title, string label, string defaultName)
    {
        var dialog = new Window
        {
            Title = title, Width = 360, Height = 175, MinWidth = 320, MaxHeight = 175,
            WindowStartupLocation = WindowStartupLocation.CenterOwner,
            ResizeMode = ResizeMode.NoResize, Background = System.Windows.Media.Brushes.White,
            Foreground = System.Windows.Media.Brushes.Black
        };
        var grid = new Grid { Margin = new Thickness(18) };
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        var text = new TextBlock { Text = label, Margin = new Thickness(0, 0, 0, 7) };
        var input = new TextBox { Text = defaultName, Padding = new Thickness(6), FontSize = 14 };
        var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 12, 0, 0) };
        var cancel = new Button { Content = "Annuller", MinWidth = 85, IsCancel = true };
        var ok = new Button { Content = "Gem", MinWidth = 85, IsDefault = true };
        cancel.Click += (_, _) => dialog.DialogResult = false;
        ok.Click += (_, _) => { if (!string.IsNullOrWhiteSpace(input.Text)) dialog.DialogResult = true; };
        buttons.Children.Add(cancel);
        buttons.Children.Add(ok);
        Grid.SetRow(input, 1);
        Grid.SetRow(buttons, 2);
        grid.Children.Add(text);
        grid.Children.Add(input);
        grid.Children.Add(buttons);
        dialog.Content = grid;
        dialog.Loaded += (_, _) => { input.Focus(); input.SelectAll(); };
        // The caller sets the owner indirectly via the currently active window.
        dialog.Owner = Application.Current.MainWindow;
        return dialog.ShowDialog() == true ? input.Text.Trim() : null;
    }

    private void Wheel_MovementStarted(object sender, MovementEventArgs e)
    {
        _direction = e.Direction;
        _speed = e.Speed;
        _movementTimer.Start();
        Status.Text = $"Pan/Tilt: {e.Direction} · {e.Speed} (demo)";
        // TODO: camera.StartPanTilt(e.Direction, e.Speed);
    }

    private void Wheel_MovementStopped(object sender, EventArgs e)
    {
        _direction = null;
        if (_zoomDirection == 0) _movementTimer.Stop();
        Status.Text = "Pan/Tilt stoppet (demo)";
        // TODO: camera.StopPanTilt();
    }

    private void ZoomIn_Down(object sender, MouseButtonEventArgs e) => BeginZoom((Button)sender, 1, e);
    private void ZoomOut_Down(object sender, MouseButtonEventArgs e) => BeginZoom((Button)sender, -1, e);
    private void BeginZoom(Button button, int direction, MouseButtonEventArgs e)
    {
        button.CaptureMouse();
        _zoomDirection = direction;
        _movementTimer.Start();
        Status.Text = direction > 0 ? "Zoom ind (demo)" : "Zoom ud (demo)";
        // TODO: camera.StartZoom(direction > 0);
        e.Handled = true;
    }
    private void Zoom_Up(object sender, MouseButtonEventArgs e)
    {
        EndZoom((Button)sender);
        e.Handled = true;
    }
    private void Zoom_LostCapture(object sender, MouseEventArgs e) => EndZoom((Button)sender);
    private void EndZoom(Button button)
    {
        if (button.IsMouseCaptured) button.ReleaseMouseCapture();
        if (_zoomDirection == 0) return;
        _zoomDirection = 0;
        if (_direction is null) _movementTimer.Stop();
        Status.Text = "Zoom stoppet (demo)";
        // TODO: camera.StopZoom();
    }

    private void MovementTimer_Tick(object? sender, EventArgs e)
    {
        double step = _speed == MoveSpeed.Fast ? 1.2 : 0.4;
        switch (_direction)
        {
            case MoveDirection.Left: _pan = Math.Clamp(_pan - step, -100, 100); break;
            case MoveDirection.Right: _pan = Math.Clamp(_pan + step, -100, 100); break;
            case MoveDirection.Up: _tilt = Math.Clamp(_tilt + step, -100, 100); break;
            case MoveDirection.Down: _tilt = Math.Clamp(_tilt - step, -100, 100); break;
        }
        _zoom = Math.Clamp(_zoom + _zoomDirection * 0.5, 0, 100);
    }
    private void StopAllMovement()
    {
        _movementTimer.Stop();
        _direction = null;
        _zoomDirection = 0;
        if (ZoomIn.IsMouseCaptured) ZoomIn.ReleaseMouseCapture();
        if (ZoomOut.IsMouseCaptured) ZoomOut.ReleaseMouseCapture();
        if (Wheel.IsMouseCaptured) Wheel.ReleaseMouseCapture();
    }
    private void Home_Click(object sender, RoutedEventArgs e)
    {
        StopAllMovement();
        _pan = _tilt = _zoom = 0;
        Status.Text = "Startposition (demo)";
        // TODO: camera.MoveHome();
    }
    private void Center_Click(object sender, RoutedEventArgs e)
    {
        StopAllMovement();
        _pan = _tilt = 0;
        Status.Text = "Pan/Tilt centreret (demo)";
        // TODO: camera.Center();
    }
}
