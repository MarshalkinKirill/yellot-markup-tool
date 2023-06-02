namespace MarkingUpDrawingTool.View
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonProjection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProjectionSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.сохранитьПроекциюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПроекциюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxProjection = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonHole = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.сохранитьОтверстиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьОтверстиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискОтверстийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxHole = new System.Windows.Forms.ToolStripComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1924, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(150, 26);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonProjection,
            this.toolStripButtonProjectionSettings,
            this.toolStripComboBoxProjection,
            this.toolStripButtonHole,
            this.toolStripButton2,
            this.toolStripComboBoxHole});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1924, 28);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonProjection
            // 
            this.toolStripButtonProjection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProjection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProjection.Image")));
            this.toolStripButtonProjection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProjection.Name = "toolStripButtonProjection";
            this.toolStripButtonProjection.Size = new System.Drawing.Size(29, 25);
            this.toolStripButtonProjection.Text = "toolStripButtonProjection";
            this.toolStripButtonProjection.ToolTipText = "toolStripButtonProjection";
            this.toolStripButtonProjection.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButtonProjectionSettings
            // 
            this.toolStripButtonProjectionSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProjectionSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьПроекциюToolStripMenuItem,
            this.удалитьПроекциюToolStripMenuItem});
            this.toolStripButtonProjectionSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProjectionSettings.Image")));
            this.toolStripButtonProjectionSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProjectionSettings.Name = "toolStripButtonProjectionSettings";
            this.toolStripButtonProjectionSettings.Size = new System.Drawing.Size(34, 25);
            this.toolStripButtonProjectionSettings.Text = "toolStripButtonProjectionSettings";
            this.toolStripButtonProjectionSettings.ToolTipText = "toolStripButtonProjectionSettings";
            // 
            // сохранитьПроекциюToolStripMenuItem
            // 
            this.сохранитьПроекциюToolStripMenuItem.Name = "сохранитьПроекциюToolStripMenuItem";
            this.сохранитьПроекциюToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.сохранитьПроекциюToolStripMenuItem.Text = "Сохранить проекцию";
            this.сохранитьПроекциюToolStripMenuItem.Click += new System.EventHandler(this.сохранитьПроекциюToolStripMenuItem_Click);
            // 
            // удалитьПроекциюToolStripMenuItem
            // 
            this.удалитьПроекциюToolStripMenuItem.Name = "удалитьПроекциюToolStripMenuItem";
            this.удалитьПроекциюToolStripMenuItem.Size = new System.Drawing.Size(242, 26);
            this.удалитьПроекциюToolStripMenuItem.Text = "Удалить проекцию";
            this.удалитьПроекциюToolStripMenuItem.Click += new System.EventHandler(this.удалитьПроекциюToolStripMenuItem_Click);
            // 
            // toolStripComboBoxProjection
            // 
            this.toolStripComboBoxProjection.Name = "toolStripComboBoxProjection";
            this.toolStripComboBoxProjection.Size = new System.Drawing.Size(160, 28);
            // 
            // toolStripButtonHole
            // 
            this.toolStripButtonHole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonHole.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHole.Image")));
            this.toolStripButtonHole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHole.Name = "toolStripButtonHole";
            this.toolStripButtonHole.Size = new System.Drawing.Size(29, 25);
            this.toolStripButtonHole.Text = "toolStripButtonHole";
            this.toolStripButtonHole.ToolTipText = "toolStripButtonHole";
            this.toolStripButtonHole.Click += new System.EventHandler(this.toolStripButtonHole_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьОтверстиеToolStripMenuItem,
            this.удалитьОтверстиеToolStripMenuItem,
            this.поискОтверстийToolStripMenuItem});
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(39, 25);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // сохранитьОтверстиеToolStripMenuItem
            // 
            this.сохранитьОтверстиеToolStripMenuItem.Name = "сохранитьОтверстиеToolStripMenuItem";
            this.сохранитьОтверстиеToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.сохранитьОтверстиеToolStripMenuItem.Text = "Сохранить отверстие";
            this.сохранитьОтверстиеToolStripMenuItem.Click += new System.EventHandler(this.сохранитьОтверстиеToolStripMenuItem_Click);
            // 
            // удалитьОтверстиеToolStripMenuItem
            // 
            this.удалитьОтверстиеToolStripMenuItem.Name = "удалитьОтверстиеToolStripMenuItem";
            this.удалитьОтверстиеToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.удалитьОтверстиеToolStripMenuItem.Text = "Удалить отверстие";
            this.удалитьОтверстиеToolStripMenuItem.Click += new System.EventHandler(this.удалитьОтверстиеToolStripMenuItem_Click);
            // 
            // поискОтверстийToolStripMenuItem
            // 
            this.поискОтверстийToolStripMenuItem.Name = "поискОтверстийToolStripMenuItem";
            this.поискОтверстийToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.поискОтверстийToolStripMenuItem.Text = "Поиск отверстий";
            this.поискОтверстийToolStripMenuItem.Click += new System.EventHandler(this.поискОтверстийToolStripMenuItem_Click);
            // 
            // toolStripComboBoxHole
            // 
            this.toolStripComboBoxHole.Name = "toolStripComboBoxHole";
            this.toolStripComboBoxHole.Size = new System.Drawing.Size(99, 28);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 56);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1924, 999);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 56);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1924, 999);
            this.panel1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripButton toolStripButtonProjection;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButtonProjectionSettings;
        private System.Windows.Forms.ToolStripMenuItem сохранитьПроекциюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьПроекциюToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProjection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton toolStripButtonHole;
        private System.Windows.Forms.ToolStripSplitButton toolStripButton2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxHole;
        private System.Windows.Forms.ToolStripMenuItem сохранитьОтверстиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьОтверстиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискОтверстийToolStripMenuItem;
    }
}