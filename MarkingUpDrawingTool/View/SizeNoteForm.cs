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
            Load += Form_Load;
            sizeView = _sizeView;
            KeyDown += SizeNoteForm_KeyDown;
            SizeTextBox.KeyDown += SizeNoteForm_KeyDown;
            
        }

        private void Form_Load(object sender, EventArgs e)
        {
            SizeTextBox.Text = "";
            SizeTextBox.Select(0, 0);
        }

        public SizeNoteForm(ISizeView _sizeView, string note)
        {
            InitializeComponent();
            sizeView = _sizeView;
            SizeTextBox.Text = note;

            KeyDown += SizeNoteForm_KeyDown;
            SizeTextBox.KeyDown += SizeNoteForm_KeyDown;
            SizeTextBox.Focus();
        }

        private void SizeNoteForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveSizeButton_Click(sender, e);
            } else
            {
                SizeTextBox.Focus();
                SizeTextBox.Select(SizeTextBox.Text.Length, 0);
            }
        }

        private void SaveSizeButton_Click(object sender, EventArgs e)
        {
            if (SizeTextBox.Text != String.Empty)
            {
                sizeView.SetSizeNote(SizeTextBox.Text);
                sizeView.SaveSizeNoteForm(sender, e);
                Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void buttonDegree_Click(object sender, EventArgs e)
        {
            SizeTextBox.Text += " \u00B0";
            SizeTextBox.Focus();
            SizeTextBox.Select(SizeTextBox.Text.Length, 0);
        }

        private void buttonDiameter_Click(object sender, EventArgs e)
        {
            SizeTextBox.Text += " \u2300" ;
            SizeTextBox.Focus();
            SizeTextBox.Select(SizeTextBox.Text.Length, 0);
        }
    }
}
