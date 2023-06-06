namespace MarkingUpDrawingTool.View
{
    partial class TableNoteForm
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
            this.NameLabel = new System.Windows.Forms.Label();
            this.MassLabel = new System.Windows.Forms.Label();
            this.ScaleLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.massTextBox = new System.Windows.Forms.TextBox();
            this.scaleTextBox = new System.Windows.Forms.TextBox();
            this.SaveTableNoteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(27, 27);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(95, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Название детали";
            // 
            // MassLabel
            // 
            this.MassLabel.AutoSize = true;
            this.MassLabel.Location = new System.Drawing.Point(27, 60);
            this.MassLabel.Name = "MassLabel";
            this.MassLabel.Size = new System.Drawing.Size(78, 13);
            this.MassLabel.TabIndex = 1;
            this.MassLabel.Text = "Масса детали";
            // 
            // ScaleLabel
            // 
            this.ScaleLabel.AutoSize = true;
            this.ScaleLabel.Location = new System.Drawing.Point(27, 93);
            this.ScaleLabel.Name = "ScaleLabel";
            this.ScaleLabel.Size = new System.Drawing.Size(91, 13);
            this.ScaleLabel.TabIndex = 2;
            this.ScaleLabel.Text = "Масштаб детали";
            this.ScaleLabel.Click += new System.EventHandler(this.ScaleLabel_Click);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(150, 24);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 3;
            // 
            // massTextBox
            // 
            this.massTextBox.Location = new System.Drawing.Point(150, 57);
            this.massTextBox.Name = "massTextBox";
            this.massTextBox.Size = new System.Drawing.Size(100, 20);
            this.massTextBox.TabIndex = 4;
            // 
            // scaleTextBox
            // 
            this.scaleTextBox.Location = new System.Drawing.Point(150, 90);
            this.scaleTextBox.Name = "scaleTextBox";
            this.scaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.scaleTextBox.TabIndex = 5;
            // 
            // SaveTableNoteButton
            // 
            this.SaveTableNoteButton.Location = new System.Drawing.Point(96, 133);
            this.SaveTableNoteButton.Name = "SaveTableNoteButton";
            this.SaveTableNoteButton.Size = new System.Drawing.Size(75, 23);
            this.SaveTableNoteButton.TabIndex = 6;
            this.SaveTableNoteButton.Text = "Применить";
            this.SaveTableNoteButton.UseVisualStyleBackColor = true;
            this.SaveTableNoteButton.Click += new System.EventHandler(this.SaveTableNoteButton_Click);
            // 
            // TableNoteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 178);
            this.Controls.Add(this.SaveTableNoteButton);
            this.Controls.Add(this.scaleTextBox);
            this.Controls.Add(this.massTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.ScaleLabel);
            this.Controls.Add(this.MassLabel);
            this.Controls.Add(this.NameLabel);
            this.Name = "TableNoteForm";
            this.Text = "Основная таблица";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label MassLabel;
        private System.Windows.Forms.Label ScaleLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox massTextBox;
        private System.Windows.Forms.TextBox scaleTextBox;
        private System.Windows.Forms.Button SaveTableNoteButton;
    }
}