using MarkingUpDrawingTool.Model;
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
    public partial class TableNoteForm : Form
    {
        ITableView mainForm;
        public TableNoteForm(ITableView mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.KeyDown += TableNoteForm_KeyDown;
            this.nameTextBox.KeyDown += TableNoteForm_KeyDown;
            this.massTextBox.KeyDown += TableNoteForm_KeyDown;
            this.matirialTextBox.KeyDown += TableNoteForm_KeyDown;
        }

        public TableNoteForm(ITableView mainForm, string name, string mass, string matirial)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.KeyDown += TableNoteForm_KeyDown;
            this.nameTextBox.KeyDown += TableNoteForm_KeyDown;
            this.massTextBox.KeyDown += TableNoteForm_KeyDown;
            this.matirialTextBox.KeyDown += TableNoteForm_KeyDown;
            this.nameTextBox.Text = name;
            this.massTextBox.Text = mass;
            this.matirialTextBox.Text = matirial;
            //this.ShowDialog();
        }

        private void ScaleLabel_Click(object sender, EventArgs e)
        {

        }

        private void TableNoteForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveTableNoteButton_Click(sender, e);
            }
        }

        private void SaveTableNoteButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text != String.Empty && massTextBox.Text != String.Empty && matirialTextBox.Text != String.Empty)
            {
                mainForm.SetTableNote(new TableNote(nameTextBox.Text, massTextBox.Text, matirialTextBox.Text));
                mainForm.SaveTableNoteForm(sender, e);
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
