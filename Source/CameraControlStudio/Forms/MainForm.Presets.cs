using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class MainForm
    {
        sealed class RowItem
        {
            public Preset Preset; public bool Temporary; public string DeviceId;
        }
        RowItem SelectedPresetRow => presetGrid.SelectedRows.Count == 0
            ? null : presetGrid.SelectedRows[0].Tag as RowItem;
        List<Preset> Permanent
        {
            get
            {
                if (!config.Cameras.TryGetValue(CameraId, out var list))
                    config.Cameras[CameraId] = list = new List<Preset>();
                return list;
            }
        }
        List<Preset> Temporary
        {
            get
            {
                return temporary.For(profile, CameraId);
            }
        }
        void FormatPresetCell(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !(presetGrid.Rows[e.RowIndex].Tag is RowItem row))
                return;

            var background = BlueTheme.Panel;
            if (row.Preset.Name == activity.Name)
            {
                if (activity.Phase == PresetPhase.Moving)
                    background = Color.Gold;
                else if (activity.Phase == PresetPhase.Reached)
                    background = Color.MediumSeaGreen;
            }
            e.CellStyle.BackColor = background;
            e.CellStyle.SelectionBackColor = background == BlueTheme.Panel ? Color.FromArgb(15, 90, 160) : background;
            e.CellStyle.ForeColor = e.CellStyle.SelectionForeColor = background == BlueTheme.Panel ? Color.White : Color.Black;
        }

        void ConfigureGrid()
        {
            permanentFont = new Font(presetGrid.Font, FontStyle.Bold);
            iconColumnWidth = iconColumn.Width;
            recallColumnWidth = recallColumn.Width;
            presetGrid.CellPainting += PaintRecallButton;
            presetGrid.SizeChanged += (sender, e) => SizePresetColumns();
            SizePresetColumns();
        }
        void SizePresetColumns()
        {
            bool compact = presetGrid.ClientSize.Width < 190;
            iconColumn.Width = compact ? Math.Min(34, iconColumnWidth) : iconColumnWidth;
            recallColumn.Width = compact ? Math.Min(30, recallColumnWidth) : recallColumnWidth;
        }
        void PaintRecallButton(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != recallColumn.Index)
                return;

            e.Paint(e.ClipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.SelectionBackground);
            bool selected = (e.State & DataGridViewElementStates.Selected) != 0;
            var color = selected ? e.CellStyle.SelectionForeColor : e.CellStyle.ForeColor;
            var button = Rectangle.Inflate(e.CellBounds, -5, -6);
            button.Width--;
            button.Height--;
            using (var border = new Pen(Color.FromArgb(170, color)))
                e.Graphics.DrawRectangle(border, button);
            TextRenderer.DrawText(e.Graphics, recallColumn.Text, presetGrid.Font, button, color,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Focus);
            e.Handled = true;
        }
        Bitmap IconImage(string id)
        {
            id = PresetIcons.ResolveId(id);
            if (!iconImages.TryGetValue(id, out var image))
            {
                image = new Bitmap(64, 48);
                using (var g = Graphics.FromImage(image))
                    PresetIcons.Draw(g, id, new Rectangle(2, 2, 60, 44));
                iconImages[id] = image;
            }
            return image;
        }
        void RefreshPresets()
        {
            if (config == null)
                return;
            var selected = SelectedPresetRow;
            string edited = (permanentList.SelectedItem as Preset)?.Name;
            refreshingPresets = true;
            try
            {
                presetGrid.Rows.Clear();
                permanentList.Items.Clear();
                DataGridViewRow selectedRow = null;
                string recall = UiText.T("Recall preset", "Kør til position");
                foreach (var pair in Permanent.Select(x => new RowItem { Preset = x, Temporary = false, DeviceId = CameraId }).Concat(Temporary.Select(x => new RowItem { Preset = x, Temporary = true, DeviceId = CameraId })))
                {
                    int n = presetGrid.Rows.Add();
                    var row = presetGrid.Rows[n];
                    row.Cells[iconColumn.Index].Value = pair.Temporary ? null : IconImage(pair.Preset.IconId);
                    row.Cells[positionColumn.Index].Value = pair.Preset.Name;
                    row.Tag = pair;
                    row.Cells[recallColumn.Index].ToolTipText = recall;
                    row.DefaultCellStyle.Font = pair.Temporary ? presetGrid.Font : permanentFont;
                    if (selected != null && pair.DeviceId == selected.DeviceId
                        && pair.Temporary == selected.Temporary && pair.Preset.Name == selected.Preset.Name)
                        selectedRow = row;
                }
                permanentList.Items.AddRange(Permanent.ToArray());
                SelectPermanentPreset(edited);
                presetGrid.CurrentCell = selectedRow?.Cells[positionColumn.Index];
                presetGrid.ClearSelection();
                if (selectedRow != null)
                    selectedRow.Selected = true;
            }
            finally
            {
                refreshingPresets = false;
            }
            RefreshActionButtons();
        }
        void ClearPresetSelection()
        {
            presetGrid.CurrentCell = null;
            presetGrid.ClearSelection();
            permanentList.ClearSelected();
        }
        void SelectPresetInSettings(RowItem row)
        {
            if (row != null)
                SelectPermanentPreset(row.Temporary ? null : row.Preset.Name);
        }
        void SelectPermanentPreset(string name)
        {
            permanentList.SelectedItem = name == null ? null
                : permanentList.Items.Cast<Preset>().FirstOrDefault(p => p.Name == name);
        }
        void RefreshActionButtons()
        {
            bool presetSelected = permanentList.SelectedItem is Preset;
            // A disconnected camera can still connect on demand when a capture action is used.
            bool canCapture = loaded && (camera == null || axes.Any(axis => HasManualAxis(axis, out _)));
            addPermanent.Enabled = temporaryButton.Enabled = canCapture;
            editPermanent.Enabled = deletePermanent.Enabled = presetSelected;
            updatePermanent.Enabled = presetSelected && canCapture;
            movePermanentUp.Enabled = loaded && permanentList.SelectedIndex > 0;
            movePermanentDown.Enabled = loaded && presetSelected && permanentList.SelectedIndex < permanentList.Items.Count - 1;
            bool temporarySelected = SelectedPresetRow?.Temporary == true;
            deleteTemporaryButton.Enabled = deleteTemporaryButton.Visible = temporarySelected;
            presetHeader.ColumnStyles[1].Width = temporarySelected ? temporaryDeleteWidth : 0;
            profileButton.Enabled = newProfile.Enabled = copyProfile.Enabled = loaded;
            renameProfile.Enabled = loaded && profilesList.SelectedItem is string selectedProfile
                && selectedProfile != ProfileStore.Standard;
        }
        bool NameExists(string name, Preset except = null)
        {
            return Permanent.Concat(Temporary).Any(p => p != except && string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        Dictionary<string, int> ReadPose()
        {
            NeedCamera();
            var pose = new Dictionary<string, int>();
            foreach (var key in axes)
                if (camera.Settings.TryGetValue(key, out var setting) && (setting.Caps & 2) != 0)
                    pose[key] = camera.Get(key);
            if (pose.Count == 0)
                throw new InvalidOperationException(UiText.T("The driver does not provide position or zoom controls.", "Driveren stiller ikke position eller zoom til rådighed."));
            return pose;
        }
        void AddTemporary()
        {
            NeedCamera();
            using (var dialog = new TextEntryForm(UiText.T("Temporary preset name", "Navn på midlertidig position"), ""))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                if (NameExists(dialog.Value))
                    throw new InvalidOperationException(UiText.T("Name already exists.", "Navnet findes allerede."));
                Temporary.Add(new Preset { Name = dialog.Value, Position = ReadPose(), FieldOfView = ReadFieldOfView() });
                RefreshPresets();
            }
        }
        void DeleteTemporary()
        {
            var row = SelectedPresetRow;
            if (row == null || !row.Temporary)
                return;
            Temporary.Remove(row.Preset);
            if (activity.Name == row.Preset.Name)
                StopMotion();
            RefreshPresets();
        }
        void AddPermanent()
        {
            NeedCamera();
            using (var dialog = new PresetEditorForm(null, x => NameExists(x)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                var p = new Preset { Name = dialog.PresetName, IconId = dialog.IconId, Position = ReadPose(), FieldOfView = ReadFieldOfView() };
                Permanent.Add(p);
                try
                {
                    SaveCurrent();
                }
                catch { Permanent.Remove(p); throw; }
                SelectPermanentPreset(p.Name);
            }
        }
        void EditPermanent()
        {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            using (var dialog = new PresetEditorForm(p, x => NameExists(x, p)))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                    return;
                string old = p.Name, icon = p.IconId;
                p.Name = dialog.PresetName;
                p.IconId = dialog.IconId;
                try
                {
                    SaveCurrent();
                }
                catch { p.Name = old; p.IconId = icon; throw; }
                if (activity.Name == old)
                    StopMotion();
            }
        }
        void UpdatePermanent()
        {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            NeedCamera();
            if (ShowMessage(UiText.T("Replace with current position?", "Erstat med aktuel position?"), Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            var old = p.Position;
            var oldFov = p.FieldOfView;
            var nextPosition = ReadPose();
            var nextFov = ReadFieldOfView();
            p.Position = nextPosition;
            p.FieldOfView = nextFov;
            try
            {
                SaveCurrent();
            }
            catch { p.Position = old; p.FieldOfView = oldFov; throw; }
            StopMotion();
        }
        void DeletePermanent()
        {
            var p = permanentList.SelectedItem as Preset;
            if (p == null)
                return;
            string confirmation = string.Format(UiText.T("Delete permanent preset: {0}?", "Slet den permanente position »{0}«?"), p.Name);
            if (ShowMessage(confirmation, Text, MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            int i = Permanent.IndexOf(p);
            Permanent.RemoveAt(i);
            try
            {
                SaveCurrent();
            }
            catch { Permanent.Insert(i, p); throw; }
            StopMotion();
        }
        void MovePermanent(int direction)
        {
            if (!loaded || !(permanentList.SelectedItem is Preset selected))
                return;
            var presets = Permanent;
            int from = presets.IndexOf(selected);
            int to = from + direction;
            if (from < 0 || to < 0 || to >= presets.Count)
                return;

            var neighbor = presets[to];
            presets[from] = neighbor;
            presets[to] = selected;
            try
            {
                SaveCurrent();
            }
            catch
            {
                presets[from] = selected;
                presets[to] = neighbor;
                throw;
            }
        }
    }
}
