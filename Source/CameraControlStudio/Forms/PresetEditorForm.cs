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
            Prepare(this);
            FormText.Translate(this);
            Text = UiText.T("Edit preset", "Rediger position");
            prompt.Text = UiText.T("Preset symbol", "Symbol for position");
            choices.DrawMode = DrawMode.OwnerDrawFixed;
            choices.ItemHeight = 42;
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
                    MessageBox.Show(this, UiText.T("Name already exists.", "Navnet findes allerede."));
                    return;
                }
                DialogResult = DialogResult.OK;
            };
        }

        void DrawIconChoice(object sender, DrawItemEventArgs e) {
            if (e.Index < 0)
                return;

            var id = (string)choices.Items[e.Index];
            e.DrawBackground();
            PresetIcons.Draw(e.Graphics, id, new Rectangle(e.Bounds.X + 6, e.Bounds.Y + 5, 60, 30));
            TextRenderer.DrawText(e.Graphics, PresetIcons.GetTitle(id), e.Font,
                new Rectangle(e.Bounds.X + 78, e.Bounds.Y, e.Bounds.Width - 80, e.Bounds.Height),
                e.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            e.DrawFocusRectangle();
        }
    }
}
