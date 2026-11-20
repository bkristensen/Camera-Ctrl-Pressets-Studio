using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CameraControlStudio {
    public partial class PresetEditorForm : AppForm {
        readonly Func<string, bool> exists;
        readonly string originalIconId;

        public string PresetName => input.Text.Trim();
        public string IconId {
            get {
                var selected = choices.SelectedItem as string ?? PresetIcons.DefaultId;
                return originalIconId != null && selected == PresetIcons.ResolveId(originalIconId)
                    ? originalIconId : selected;
            }
        }

        public PresetEditorForm() : this(null, null) { }

        public PresetEditorForm(Preset preset, Func<string, bool> nameExists) {
            InitializeComponent();
            if (InDesigner)
                return;

            exists = nameExists;
            originalIconId = preset?.IconId;
            FormText.Translate(this);
            choices.Items.AddRange(PresetIcons.Ids.ToArray());
            input.Text = preset?.Name ?? "";
            ok.Enabled = PresetName.Length != 0;
            input.TextChanged += (sender, e) => ok.Enabled = PresetName.Length != 0;
            if (preset != null)
                choices.SelectedItem = PresetIcons.ResolveId(originalIconId);

            choices.DrawItem += DrawIconChoice;
            choices.SelectedIndexChanged += (sender, e) => {
                var suggestion = PresetIcons.GetTitle(IconId);
                if (string.IsNullOrWhiteSpace(input.Text) && !(exists?.Invoke(suggestion) ?? false))
                    input.Text = suggestion;
            };
            ok.Click += (sender, e) => {
                if (PresetName.Length == 0) {
                    input.Focus();
                    return;
                }
                if (exists?.Invoke(PresetName) ?? false) {
                    ShowMessage(UiText.T("Name already exists.", "Navnet findes allerede."));
                    return;
                }
                DialogResult = DialogResult.OK;
            };
        }

        void DrawIconChoice(object sender, DrawItemEventArgs e) {
            if (e.Index < 0)
                return;

            var id = (string)choices.Items[e.Index];
            bool rightToLeft = choices.RightToLeft == RightToLeft.Yes;
            var flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            if (rightToLeft)
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            var iconBounds = new Rectangle(rightToLeft ? e.Bounds.Right - 66 : e.Bounds.X + 6, e.Bounds.Y + 5, 60, 30);
            var textBounds = new Rectangle(e.Bounds.X + (rightToLeft ? 2 : 78), e.Bounds.Y, e.Bounds.Width - 80, e.Bounds.Height);
            e.DrawBackground();
            PresetIcons.Draw(e.Graphics, id, iconBounds);
            TextRenderer.DrawText(e.Graphics, PresetIcons.GetTitle(id), e.Font, textBounds, e.ForeColor, flags);
            e.DrawFocusRectangle();
        }
    }
}
