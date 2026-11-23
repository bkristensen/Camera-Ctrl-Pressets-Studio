using System;
using System.Drawing;
using System.Windows.Forms;
namespace CameraControlStudio {
    partial class CameraSourceForm {
        private System.ComponentModel.IContainer components;
        protected override void Dispose(bool disposing) {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraSourceForm));
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();
            this.layout = new TableLayoutPanel();
            this.layout.Name = "layout";
            this.layout.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.layout.Dock = DockStyle.Fill;
            this.layout.ColumnCount = 1;
            this.layout.RowCount = 4;
            this.layout.Margin = new Padding(0);
            this.layout.Padding = new Padding(16);
            this.layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            this.layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            this.layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            this.Controls.Add(this.layout);
            this.prompt = new Label();
            this.prompt.Name = "prompt";
            this.prompt.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.prompt.Text = "Choose camera source";
            this.prompt.Dock = DockStyle.Fill;
            this.layout.Controls.Add(this.prompt, 0, 0);
            this.choices = new ListBox();
            this.choices.Name = "choices";
            this.choices.BackColor = System.Drawing.Color.FromArgb(5, 40, 79);
            this.choices.ForeColor = System.Drawing.Color.White;
            this.choices.ItemHeight = 28;
            this.choices.Dock = DockStyle.Fill;
            this.choices.IntegralHeight = false;
            this.layout.Controls.Add(this.choices, 0, 1);
            this.hint = new Label();
            this.hint.Name = "hint";
            this.hint.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.hint.Text = "";
            this.hint.Dock = DockStyle.Fill;
            this.hint.AutoEllipsis = true;
            this.layout.Controls.Add(this.hint, 0, 2);
            this.buttons = new TableLayoutPanel();
            this.buttons.Name = "buttons";
            this.buttons.BackColor = System.Drawing.Color.FromArgb(9, 52, 94);
            this.buttons.Dock = DockStyle.Fill;
            this.buttons.ColumnCount = 3;
            this.buttons.RowCount = 1;
            this.buttons.Margin = new Padding(0);
            this.layout.Controls.Add(this.buttons, 0, 3);
            this.buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            this.buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            this.buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.ok = new BlueButton();
            this.ok.Name = "ok";
            this.ok.Text = "Save";
            this.ok.Dock = DockStyle.Fill;
            this.buttons.Controls.Add(this.ok, 1, 0);
            this.cancel = new BlueButton();
            this.cancel.Name = "cancel";
            this.cancel.Text = "Cancel";
            this.cancel.DialogResult = DialogResult.Cancel;
            this.cancel.Dock = DockStyle.Fill;
            this.buttons.Controls.Add(this.cancel, 2, 0);
            this.AcceptButton = this.ok;
            this.CancelButton = this.cancel;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ClientSize = new Size(440, 300);
            this.MinimumSize = new Size(360, 260);
            this.AutoScaleDimensions = new SizeF(96F, 96F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.Text = "Choose camera source";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ShowInTaskbar = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ResumeLayout(false);
        }
        private TableLayoutPanel layout;
        private Label prompt;
        private ListBox choices;
        private Label hint;
        private TableLayoutPanel buttons;
        private BlueButton ok;
        private BlueButton cancel;
    }
}
