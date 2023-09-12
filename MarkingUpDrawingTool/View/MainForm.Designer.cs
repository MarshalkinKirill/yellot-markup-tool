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
            this.jsonLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuSaveObject = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDeleteObject = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolStripButtonBorder = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProjection = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxProjection = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonProjectionRoi = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxProjectionRoi = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripButtonHole = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuHoleSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxHole = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuMainTable = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxTable = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuSizeAuto = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuSize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonArrow = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuLinearArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuAngularArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuRadialArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDiametralArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuReferenceArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuConeArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuChamferArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxArrow = new System.Windows.Forms.ToolStripComboBox();
            this.ToolStripButtonGap = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxGap = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButtonSymbol = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBoxSymbol = new System.Windows.Forms.ToolStripComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openJsonFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1438, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.ToolStripMenuSaveAs,
            this.jsonLoadToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // ToolStripMenuSaveAs
            // 
            this.ToolStripMenuSaveAs.Name = "ToolStripMenuSaveAs";
            this.ToolStripMenuSaveAs.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuSaveAs.Text = "Сохранить как ";
            this.ToolStripMenuSaveAs.Click += new System.EventHandler(this.ToolStripMenuSaveAs_Click);
            // 
            // jsonLoadToolStripMenuItem
            // 
            this.jsonLoadToolStripMenuItem.Name = "jsonLoadToolStripMenuItem";
            this.jsonLoadToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.jsonLoadToolStripMenuItem.Text = "Загрузить json-файл";
            this.jsonLoadToolStripMenuItem.Click += new System.EventHandler(this.jsonLoadToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuSaveObject,
            this.ToolStripMenuDeleteObject});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // ToolStripMenuSaveObject
            // 
            this.ToolStripMenuSaveObject.Name = "ToolStripMenuSaveObject";
            this.ToolStripMenuSaveObject.Size = new System.Drawing.Size(182, 22);
            this.ToolStripMenuSaveObject.Text = "Сохранить элемент";
            // 
            // ToolStripMenuDeleteObject
            // 
            this.ToolStripMenuDeleteObject.Name = "ToolStripMenuDeleteObject";
            this.ToolStripMenuDeleteObject.Size = new System.Drawing.Size(182, 22);
            this.ToolStripMenuDeleteObject.Text = "Удалить элемент";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButtonBorder,
            this.toolStripButtonProjection,
            this.toolStripComboBoxProjection,
            this.toolStripButtonProjectionRoi,
            this.toolStripComboBoxProjectionRoi,
            this.ToolStripButtonHole,
            this.toolStripButton2,
            this.toolStripComboBoxHole,
            this.toolStripButton1,
            this.toolStripComboBoxTable,
            this.toolStripButton4,
            this.toolStripComboBoxSize,
            this.toolStripButtonArrow,
            this.toolStripComboBoxArrow,
            this.ToolStripButtonGap,
            this.toolStripComboBoxGap,
            this.toolStripButtonSymbol,
            this.toolStripComboBoxSymbol});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1438, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ToolStripButtonBorder
            // 
            this.ToolStripButtonBorder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonBorder.Enabled = false;
            this.ToolStripButtonBorder.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonBorder.Image")));
            this.ToolStripButtonBorder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonBorder.Name = "ToolStripButtonBorder";
            this.ToolStripButtonBorder.Size = new System.Drawing.Size(24, 24);
            this.ToolStripButtonBorder.Text = "Рамка";
            // 
            // toolStripButtonProjection
            // 
            this.toolStripButtonProjection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProjection.Enabled = false;
            this.toolStripButtonProjection.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProjection.Image")));
            this.toolStripButtonProjection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProjection.Name = "toolStripButtonProjection";
            this.toolStripButtonProjection.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonProjection.Text = "Контур проекции ";
            this.toolStripButtonProjection.ToolTipText = "Контур проекции ";
            // 
            // toolStripComboBoxProjection
            // 
            this.toolStripComboBoxProjection.Enabled = false;
            this.toolStripComboBoxProjection.Name = "toolStripComboBoxProjection";
            this.toolStripComboBoxProjection.Size = new System.Drawing.Size(121, 27);
            // 
            // toolStripButtonProjectionRoi
            // 
            this.toolStripButtonProjectionRoi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonProjectionRoi.Enabled = false;
            this.toolStripButtonProjectionRoi.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProjectionRoi.Image")));
            this.toolStripButtonProjectionRoi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProjectionRoi.Name = "toolStripButtonProjectionRoi";
            this.toolStripButtonProjectionRoi.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonProjectionRoi.Text = "Область проекции";
            // 
            // toolStripComboBoxProjectionRoi
            // 
            this.toolStripComboBoxProjectionRoi.Enabled = false;
            this.toolStripComboBoxProjectionRoi.Name = "toolStripComboBoxProjectionRoi";
            this.toolStripComboBoxProjectionRoi.Size = new System.Drawing.Size(120, 27);
            // 
            // ToolStripButtonHole
            // 
            this.ToolStripButtonHole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonHole.Enabled = false;
            this.ToolStripButtonHole.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonHole.Image")));
            this.ToolStripButtonHole.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonHole.Name = "ToolStripButtonHole";
            this.ToolStripButtonHole.Size = new System.Drawing.Size(24, 24);
            this.ToolStripButtonHole.Text = "Отверсие";
            this.ToolStripButtonHole.ToolTipText = "Отверсие";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuHoleSearch});
            this.toolStripButton2.Enabled = false;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton2.Text = "HoleSettings";
            // 
            // ToolStripMenuHoleSearch
            // 
            this.ToolStripMenuHoleSearch.Enabled = false;
            this.ToolStripMenuHoleSearch.Name = "ToolStripMenuHoleSearch";
            this.ToolStripMenuHoleSearch.Size = new System.Drawing.Size(168, 22);
            this.ToolStripMenuHoleSearch.Text = "Поиск отверстий";
            // 
            // toolStripComboBoxHole
            // 
            this.toolStripComboBoxHole.Enabled = false;
            this.toolStripComboBoxHole.Name = "toolStripComboBoxHole";
            this.toolStripComboBoxHole.Size = new System.Drawing.Size(75, 27);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuMainTable,
            this.ToolStripMenuTable});
            this.toolStripButton1.Enabled = false;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton1.Text = "Таблица";
            this.toolStripButton1.ToolTipText = "Таблица";
            // 
            // ToolStripMenuMainTable
            // 
            this.ToolStripMenuMainTable.Name = "ToolStripMenuMainTable";
            this.ToolStripMenuMainTable.Size = new System.Drawing.Size(236, 22);
            this.ToolStripMenuMainTable.Text = "Выделить главную таблицу";
            // 
            // ToolStripMenuTable
            // 
            this.ToolStripMenuTable.Name = "ToolStripMenuTable";
            this.ToolStripMenuTable.Size = new System.Drawing.Size(236, 22);
            this.ToolStripMenuTable.Text = "Выделить побочную таблицу";
            // 
            // toolStripComboBoxTable
            // 
            this.toolStripComboBoxTable.Enabled = false;
            this.toolStripComboBoxTable.Name = "toolStripComboBoxTable";
            this.toolStripComboBoxTable.Size = new System.Drawing.Size(75, 27);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuSizeAuto,
            this.ToolStripMenuSize});
            this.toolStripButton4.Enabled = false;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(33, 24);
            this.toolStripButton4.Text = "Размер";
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
            // toolStripComboBoxSize
            // 
            this.toolStripComboBoxSize.Enabled = false;
            this.toolStripComboBoxSize.Name = "toolStripComboBoxSize";
            this.toolStripComboBoxSize.Size = new System.Drawing.Size(75, 27);
            // 
            // toolStripButtonArrow
            // 
            this.toolStripButtonArrow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonArrow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuLinearArrow,
            this.ToolStripMenuAngularArrow,
            this.ToolStripMenuRadialArrow,
            this.ToolStripMenuDiametralArrow,
            this.ToolStripMenuReferenceArrow,
            this.ToolStripMenuConeArrow,
            this.ToolStripMenuChamferArrow});
            this.toolStripButtonArrow.Enabled = false;
            this.toolStripButtonArrow.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonArrow.Image")));
            this.toolStripButtonArrow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonArrow.Name = "toolStripButtonArrow";
            this.toolStripButtonArrow.Size = new System.Drawing.Size(33, 24);
            this.toolStripButtonArrow.Text = "Размерные стрелки";
            // 
            // ToolStripMenuLinearArrow
            // 
            this.ToolStripMenuLinearArrow.Name = "ToolStripMenuLinearArrow";
            this.ToolStripMenuLinearArrow.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuLinearArrow.Text = "Линейная стрелка";
            // 
            // ToolStripMenuAngularArrow
            // 
            this.ToolStripMenuAngularArrow.Name = "ToolStripMenuAngularArrow";
            this.ToolStripMenuAngularArrow.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuAngularArrow.Text = "Угловая стрелка";
            // 
            // ToolStripMenuRadialArrow
            // 
            this.ToolStripMenuRadialArrow.Name = "ToolStripMenuRadialArrow";
            this.ToolStripMenuRadialArrow.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuRadialArrow.Text = "Стрелка радиуса";
            // 
            // ToolStripMenuDiametralArrow
            // 
            this.ToolStripMenuDiametralArrow.Name = "ToolStripMenuDiametralArrow";
            this.ToolStripMenuDiametralArrow.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuDiametralArrow.Text = "Стрелка диаметра";
            // 
            // ToolStripMenuReferenceArrow
            // 
            this.ToolStripMenuReferenceArrow.Name = "ToolStripMenuReferenceArrow";
            this.ToolStripMenuReferenceArrow.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuReferenceArrow.Text = "Справочная стрелка";
            // 
            // ToolStripMenuConeArrow
            // 
            this.ToolStripMenuConeArrow.Name = "ToolStripMenuConeArrow";
            this.ToolStripMenuConeArrow.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuConeArrow.Text = "Конусная стрелка";
            // 
            // ToolStripMenuChamferArrow
            // 
            this.ToolStripMenuChamferArrow.Name = "ToolStripMenuChamferArrow";
            this.ToolStripMenuChamferArrow.Size = new System.Drawing.Size(187, 22);
            this.ToolStripMenuChamferArrow.Text = "Фасочнпя стрелка";
            // 
            // toolStripComboBoxArrow
            // 
            this.toolStripComboBoxArrow.Enabled = false;
            this.toolStripComboBoxArrow.Name = "toolStripComboBoxArrow";
            this.toolStripComboBoxArrow.Size = new System.Drawing.Size(100, 27);
            // 
            // ToolStripButtonGap
            // 
            this.ToolStripButtonGap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButtonGap.Enabled = false;
            this.ToolStripButtonGap.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonGap.Image")));
            this.ToolStripButtonGap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButtonGap.Name = "ToolStripButtonGap";
            this.ToolStripButtonGap.Size = new System.Drawing.Size(24, 24);
            this.ToolStripButtonGap.Text = "Разрыв";
            // 
            // toolStripComboBoxGap
            // 
            this.toolStripComboBoxGap.Enabled = false;
            this.toolStripComboBoxGap.Name = "toolStripComboBoxGap";
            this.toolStripComboBoxGap.Size = new System.Drawing.Size(75, 27);
            // 
            // toolStripButtonSymbol
            // 
            this.toolStripButtonSymbol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSymbol.Enabled = false;
            this.toolStripButtonSymbol.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSymbol.Image")));
            this.toolStripButtonSymbol.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSymbol.Name = "toolStripButtonSymbol";
            this.toolStripButtonSymbol.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSymbol.Text = "Символ";
            // 
            // toolStripComboBoxSymbol
            // 
            this.toolStripComboBoxSymbol.Enabled = false;
            this.toolStripComboBoxSymbol.Name = "toolStripComboBoxSymbol";
            this.toolStripComboBoxSymbol.Size = new System.Drawing.Size(75, 27);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 51);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1438, 806);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Image Files (*.img;*.jpg;*.jpeg;*.png;*.tiff)|*.img;*.jpg;*.jpeg;*.png;*.tiff|All" +
    " Files (*.*)|*.*";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSize = true;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 51);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1438, 806);
            this.panel1.TabIndex = 3;
            // 
            // openJsonFileDialog
            // 
            this.openJsonFileDialog.FileName = "openJsonFileDialog";
            this.openJsonFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1438, 857);
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
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProjection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton ToolStripButtonHole;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxHole;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuHoleSearch;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuMainTable;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuTable;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxTable;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSizeAuto;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSize;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSize;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxArrow;
        private System.Windows.Forms.ToolStripButton ToolStripButtonGap;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxGap;
        private System.Windows.Forms.ToolStripButton ToolStripButtonBorder;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSaveAs;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripButton toolStripButtonProjectionRoi;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxProjectionRoi;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuSaveObject;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDeleteObject;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButtonArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuLinearArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuAngularArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuRadialArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDiametralArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuReferenceArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuConeArrow;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuChamferArrow;
        private System.Windows.Forms.ToolStripButton toolStripButtonSymbol;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSymbol;
        private System.Windows.Forms.ToolStripMenuItem jsonLoadToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openJsonFileDialog;
    }
}