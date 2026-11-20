using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CameraControlStudio
{
    public partial class ProfilePickerForm : AppForm
    {
        public string Selected
        {
            get
            {
                return choices.SelectedItem as string;
            }
        }

        public bool KeepLayout
        {
            get
            {
                return keepLayout.Checked;
            }
        }

        public ProfilePickerForm()
        {
            InitializeComponent();
            if (InDesigner)
                return;
            FormText.Translate(this);
            ok.Enabled = Selected != null;
            choices.SelectedIndexChanged += (o, e) => ok.Enabled = Selected != null;
            ok.Click += (o, e) =>
            {
                if (Selected != null)
                    DialogResult = DialogResult.OK;
            };
        }

        public ProfilePickerForm(IEnumerable<string> names, string selected, bool keep) : this()
        {
            foreach (var n in names)
                choices.Items.Add(n);
            choices.SelectedItem = selected;
            keepLayout.Checked = keep;
        }
    }
}
