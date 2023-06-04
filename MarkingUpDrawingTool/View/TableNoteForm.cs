﻿using MarkingUpDrawingTool.Model;
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
        MainForm mainForm;
        public TableNoteForm(MainForm _mainForm)
        {
            InitializeComponent();
            mainForm = _mainForm;
        }

        private void ScaleLabel_Click(object sender, EventArgs e)
        {

        }

        private void SaveTableNoteButton_Click(object sender, EventArgs e)
        {
            if (nameTextBox.Text != String.Empty && massTextBox.Text != String.Empty && scaleTextBox.Text != String.Empty)
            {
                mainForm.SetTableNote(new TableNote(nameTextBox.Text, massTextBox.Text, scaleTextBox.Text));
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
