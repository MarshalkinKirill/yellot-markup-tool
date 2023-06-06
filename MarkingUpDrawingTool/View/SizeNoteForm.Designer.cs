using System;

namespace MarkingUpDrawingTool.View
{
    partial class SizeNoteForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SizeTextBox = new System.Windows.Forms.TextBox();
            this.Sizelabel = new System.Windows.Forms.Label();
            this.SaveSizeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SizeTextBox
            // 
            this.SizeTextBox.Location = new System.Drawing.Point(77, 31);
            this.SizeTextBox.Name = "SizeTextBox";
            this.SizeTextBox.Size = new System.Drawing.Size(100, 20);
            this.SizeTextBox.TabIndex = 0;
            // 
            // Sizelabel
            // 
            this.Sizelabel.AutoSize = true;
            this.Sizelabel.Location = new System.Drawing.Point(25, 34);
            this.Sizelabel.Name = "Sizelabel";
            this.Sizelabel.Size = new System.Drawing.Size(37, 13);
            this.Sizelabel.TabIndex = 1;
            this.Sizelabel.Text = "Текст";
            // 
            // SaveSizeButton
            // 
            this.SaveSizeButton.Location = new System.Drawing.Point(60, 65);
            this.SaveSizeButton.Name = "SaveSizeButton";
            this.SaveSizeButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSizeButton.TabIndex = 2;
            this.SaveSizeButton.Text = "Применить";
            this.SaveSizeButton.UseVisualStyleBackColor = true;
            this.SaveSizeButton.Click += new System.EventHandler(this.SaveSizeButton_Click);
            // 
            // SizeNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 100);
            this.Controls.Add(this.SaveSizeButton);
            this.Controls.Add(this.Sizelabel);
            this.Controls.Add(this.SizeTextBox);
            this.Name = "SizeNoteForm";
            this.Text = "SizeNoteForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.TextBox SizeTextBox;
        private System.Windows.Forms.Label Sizelabel;
        private System.Windows.Forms.Button SaveSizeButton;
    }
}