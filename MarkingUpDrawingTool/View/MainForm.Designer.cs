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
            this.ToolStripMenuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripButtonBorder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProjection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProjectionSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuSaveProjection = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDeleteProjection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxProjection = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripButtonHole = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuHoleSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuHoleDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuHoleSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxHole = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.выделитьГлавнуюТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выделитьПобочнуюТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.сохранитьТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьТаблицуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxTable = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuSizeAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuSaveSize = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDeleteSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonArrow = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuSaveArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDeleteArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxArrow = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripButtonGap = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuSaveGap = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDeleteGap = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripComboBoxGap = new System.Windows.Forms.ToolStripComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1443, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.ToolStripMenuSaveAs});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // ToolStripMenuSaveAs
            // 
            this.ToolStripMenuSaveAs.Name = "ToolStripMenuSaveAs";
            this.ToolStripMenuSaveAs.Size = new System.Drawing.Size(157, 22);
            this.ToolStripMenuSaveAs.Text = "Сохранить как ";
            this.ToolStripMenuSaveAs.Click += new System.EventHandler(this.ToolStripMenuSaveAs_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButtonBorder,
            this.toolStripButtonProjection,
            this.toolStripButtonProjectionSettings,
            this.toolStripComboBoxProjection,
            this.ToolStripButtonHole,
            this.toolStripButton2,
            this.toolStripComboBoxHole,
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripComboBoxTable,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripComboBoxSize,
            this.toolStripButtonArrow,
            this.toolStripButton7,
            this.toolStripComboBoxArrow,
            this.ToolStripButtonGap,
            this.toolStripButton8,
            this.ToolStripComboBoxGap});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1443, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ToolStripButtonBorder
            // 
            this.ToolStripButtonBorder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonBorder.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonBorder.Image")));
            this.ToolStripButtonBorder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonBorder.Name = "ToolStripButtonBorder";
            this.ToolStripButtonBorder.Size = new System.Drawing.Size(24, 24);
            this.ToolStripButtonBorder.Text = "Border";
            // 
            // toolStripButtonProjection
            // 
            this.toolStripButtonProjection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProjection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProjection.Image")));
            this.toolStripButtonProjection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProjection.Name = "toolStripButtonProjection";
            this.toolStripButtonProjection.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonProjection.Text = "Projection";
            this.toolStripButtonProjection.ToolTipText = "toolStripButtonProjection";
            // 
            // toolStripButtonProjectionSettings
            // 
            this.toolStripButtonProjectionSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProjectionSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuSaveProjection,
            this.ToolStripMenuDeleteProjection});
            this.toolStripButtonProjectionSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProjectionSettings.Image")));
            this.toolStripButtonProjectionSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProjectionSettings.Name = "toolStripButtonProjectionSettings";
            this.toolStripButtonProjectionSettings.Size = new System.Drawing.Size(33, 24);
            this.toolStripButtonProjectionSettings.Text = "toolStripButtonProjectionSettings";
            this.toolStripButtonProjectionSettings.ToolTipText = "toolStripButtonProjectionSettings";
            // 
            // ToolStripMenuSaveProjection
            // 
            this.ToolStripMenuSaveProjection.Name = "ToolStripMenuSaveProjection";
            this.ToolStripMenuSaveProjection.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuSaveProjection.Text = "Сохранить проекцию";
            // 
            // ToolStripMenuDeleteProjection
            // 
            this.ToolStripMenuDeleteProjection.Name = "ToolStripMenuDeleteProjection";
            this.ToolStripMenuDeleteProjection.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuDeleteProjection.Text = "Удалить проекцию";
            // 
            // toolStripComboBoxProjection
            // 
            this.toolStripComboBoxProjection.Name = "toolStripComboBoxProjection";
            this.toolStripComboBoxProjection.Size = new System.Drawing.Size(121, 27);
            // 
            // ToolStripButtonHole
            // 
            this.ToolStripButtonHole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonHole.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonHole.Image")));
            this.ToolStripButtonHole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonHole.Name = "ToolStripButtonHole";
            this.ToolStripButtonHole.Size = new System.Drawing.Size(24, 24);
            this.ToolStripButtonHole.Text = "Hole";
            this.ToolStripButtonHole.ToolTipText = "ToolStripButtonHole";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuHoleSave,
            this.ToolStripMenuHoleDelete,
            this.ToolStripMenuHoleSearch});
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // ToolStripMenuHoleSave
            // 
            this.ToolStripMenuHoleSave.Name = "ToolStripMenuHoleSave";
            this.ToolStripMenuHoleSave.Size = new System.Drawing.Size(191, 22);
            this.ToolStripMenuHoleSave.Text = "Сохранить отверстие";
            // 
            // ToolStripMenuHoleDelete
            // 
            this.ToolStripMenuHoleDelete.Name = "ToolStripMenuHoleDelete";
            this.ToolStripMenuHoleDelete.Size = new System.Drawing.Size(191, 22);
            this.ToolStripMenuHoleDelete.Text = "Удалить отверстие";
            // 
            // ToolStripMenuHoleSearch
            // 
            this.ToolStripMenuHoleSearch.Name = "ToolStripMenuHoleSearch";
            this.ToolStripMenuHoleSearch.Size = new System.Drawing.Size(191, 22);
            this.ToolStripMenuHoleSearch.Text = "Поиск отверстий";
            // 
            // toolStripComboBoxHole
            // 
            this.toolStripComboBoxHole.Name = "toolStripComboBoxHole";
            this.toolStripComboBoxHole.Size = new System.Drawing.Size(75, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выделитьГлавнуюТаблицуToolStripMenuItem,
            this.выделитьПобочнуюТаблицуToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // выделитьГлавнуюТаблицуToolStripMenuItem
            // 
            this.выделитьГлавнуюТаблицуToolStripMenuItem.Name = "выделитьГлавнуюТаблицуToolStripMenuItem";
            this.выделитьГлавнуюТаблицуToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.выделитьГлавнуюТаблицуToolStripMenuItem.Text = "Выделить главную таблицу";
            this.выделитьГлавнуюТаблицуToolStripMenuItem.Click += new System.EventHandler(this.выделитьГлавнуюТаблицуToolStripMenuItem_Click);
            // 
            // выделитьПобочнуюТаблицуToolStripMenuItem
            // 
            this.выделитьПобочнуюТаблицуToolStripMenuItem.Name = "выделитьПобочнуюТаблицуToolStripMenuItem";
            this.выделитьПобочнуюТаблицуToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.выделитьПобочнуюТаблицуToolStripMenuItem.Text = "Выделить побочную таблицу";
            this.выделитьПобочнуюТаблицуToolStripMenuItem.Click += new System.EventHandler(this.выделитьПобочнуюТаблицуToolStripMenuItem_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьТаблицуToolStripMenuItem,
            this.удалитьТаблицуToolStripMenuItem});
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // сохранитьТаблицуToolStripMenuItem
            // 
            this.сохранитьТаблицуToolStripMenuItem.Name = "сохранитьТаблицуToolStripMenuItem";
            this.сохранитьТаблицуToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.сохранитьТаблицуToolStripMenuItem.Text = "Сохранить таблицу ";
            this.сохранитьТаблицуToolStripMenuItem.Click += new System.EventHandler(this.сохранитьТаблицуToolStripMenuItem_Click);
            // 
            // удалитьТаблицуToolStripMenuItem
            // 
            this.удалитьТаблицуToolStripMenuItem.Name = "удалитьТаблицуToolStripMenuItem";
            this.удалитьТаблицуToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.удалитьТаблицуToolStripMenuItem.Text = "Удалить таблицу ";
            this.удалитьТаблицуToolStripMenuItem.Click += new System.EventHandler(this.удалитьТаблицуToolStripMenuItem_Click);
            // 
            // toolStripComboBoxTable
            // 
            this.toolStripComboBoxTable.Name = "toolStripComboBoxTable";
            this.toolStripComboBoxTable.Size = new System.Drawing.Size(75, 27);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuSizeAuto,
            this.ToolStripMenuSize});
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton4.Text = "toolStripButton4";
            // 
            // ToolStripMenuSizeAuto
            // 
            this.ToolStripMenuSizeAuto.Name = "ToolStripMenuSizeAuto";
            this.ToolStripMenuSizeAuto.Size = new System.Drawing.Size(200, 22);
            this.ToolStripMenuSizeAuto.Text = "Выделить размер авто.";
            // 
            // ToolStripMenuSize
            // 
            this.ToolStripMenuSize.Name = "ToolStripMenuSize";
            this.ToolStripMenuSize.Size = new System.Drawing.Size(200, 22);
            this.ToolStripMenuSize.Text = "Выделить размер ";
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuSaveSize,
            this.ToolStripMenuDeleteSize});
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton5.Text = "toolStripButton5";
            // 
            // ToolStripMenuSaveSize
            // 
            this.ToolStripMenuSaveSize.Name = "ToolStripMenuSaveSize";
            this.ToolStripMenuSaveSize.Size = new System.Drawing.Size(176, 22);
            this.ToolStripMenuSaveSize.Text = "Сохранить размер";
            // 
            // ToolStripMenuDeleteSize
            // 
            this.ToolStripMenuDeleteSize.Name = "ToolStripMenuDeleteSize";
            this.ToolStripMenuDeleteSize.Size = new System.Drawing.Size(176, 22);
            this.ToolStripMenuDeleteSize.Text = "Удалить размер";
            // 
            // toolStripComboBoxSize
            // 
            this.toolStripComboBoxSize.Name = "toolStripComboBoxSize";
            this.toolStripComboBoxSize.Size = new System.Drawing.Size(75, 27);
            // 
            // toolStripButtonArrow
            // 
            this.toolStripButtonArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonArrow.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonArrow.Image")));
            this.toolStripButtonArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonArrow.Name = "toolStripButtonArrow";
            this.toolStripButtonArrow.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonArrow.Text = "Arrow";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuSaveArrow,
            this.ToolStripMenuDeleteArrow});
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton7.Text = "toolStripButton7";
            // 
            // ToolStripMenuSaveArrow
            // 
            this.ToolStripMenuSaveArrow.Name = "ToolStripMenuSaveArrow";
            this.ToolStripMenuSaveArrow.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuSaveArrow.Text = "Сохранить срелку";
            // 
            // ToolStripMenuDeleteArrow
            // 
            this.ToolStripMenuDeleteArrow.Name = "ToolStripMenuDeleteArrow";
            this.ToolStripMenuDeleteArrow.Size = new System.Drawing.Size(174, 22);
            this.ToolStripMenuDeleteArrow.Text = "Удалить стрелку";
            // 
            // toolStripComboBoxArrow
            // 
            this.toolStripComboBoxArrow.Name = "toolStripComboBoxArrow";
            this.toolStripComboBoxArrow.Size = new System.Drawing.Size(75, 27);
            // 
            // ToolStripButtonGap
            // 
            this.ToolStripButtonGap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonGap.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonGap.Image")));
            this.ToolStripButtonGap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonGap.Name = "ToolStripButtonGap";
            this.ToolStripButtonGap.Size = new System.Drawing.Size(24, 24);
            this.ToolStripButtonGap.Text = "Gap";
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton8.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuSaveGap,
            this.ToolStripMenuDeleteGap});
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton8.Text = "toolStripButton8";
            // 
            // ToolStripMenuSaveGap
            // 
            this.ToolStripMenuSaveGap.Name = "ToolStripMenuSaveGap";
            this.ToolStripMenuSaveGap.Size = new System.Drawing.Size(176, 22);
            this.ToolStripMenuSaveGap.Text = "Сохранить разрыв";
            // 
            // ToolStripMenuDeleteGap
            // 
            this.ToolStripMenuDeleteGap.Name = "ToolStripMenuDeleteGap";
            this.ToolStripMenuDeleteGap.Size = new System.Drawing.Size(176, 22);
            this.ToolStripMenuDeleteGap.Text = "Удалить разрыв";
            // 
            // ToolStripComboBoxGap
            // 
            this.ToolStripComboBoxGap.Name = "ToolStripComboBoxGap";
            this.ToolStripComboBoxGap.Size = new System.Drawing.Size(75, 27);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1443, 806);
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
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1443, 806);
            this.panel1.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1443, 857);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSaveProjection;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDeleteProjection;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProjection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton ToolStripButtonHole;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxHole;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuHoleSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuHoleDelete;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuHoleSearch;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem выделитьГлавнуюТаблицуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выделитьПобочнуюТаблицуToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem сохранитьТаблицуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьТаблицуToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTable;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSizeAuto;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSize;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton5;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSaveSize;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDeleteSize;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSize;
        private System.Windows.Forms.ToolStripButton toolStripButtonArrow;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton7;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSaveArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDeleteArrow;
        private System.Windows.Forms.ToolStripButton ToolStripButtonGap;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton8;
        private System.Windows.Forms.ToolStripComboBox ToolStripComboBoxGap;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSaveGap;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDeleteGap;
        private System.Windows.Forms.ToolStripButton ToolStripButtonBorder;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSaveAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}