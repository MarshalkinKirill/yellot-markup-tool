using MarkingUpDrawingTool.View.ViewInteraface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View
{
    public partial class SizeNoteForm : Form
    {
        ISizeView sizeView;
        public SizeNoteForm(ISizeView _sizeView)
        {
            InitializeComponent();
            sizeView = _sizeView;

            this.KeyDown += SizeNoteForm_KeyDown;
            this.SizeTextBox.KeyDown += SizeNoteForm_KeyDown;
        }
        public SizeNoteForm(ISizeView _sizeView, string note)
        {
            InitializeComponent();
            sizeView = _sizeView;
            this.SizeTextBox.Text = note;

            this.KeyDown += SizeNoteForm_KeyDown;
            this.SizeTextBox.KeyDown += SizeNoteForm_KeyDown;
        }

        private void SizeNoteForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveSizeButton_Click(sender, e);
            }
        }

        private void SaveSizeButton_Click(object sender, EventArgs e)
        {
            if (this.SizeTextBox.Text != String.Empty)
            {
                sizeView.SetSizeNote(SizeTextBox.Text);
                sizeView.SaveSizeNoteForm(sender, e);
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonDegree_Click(object sender, EventArgs e)
        {
            this.SizeTextBox.Text += " \u00B0";
        }

        private void buttonDiameter_Click(object sender, EventArgs e)
        {
            this.SizeTextBox.Text += " \u2300" ;
        }
    }
}
