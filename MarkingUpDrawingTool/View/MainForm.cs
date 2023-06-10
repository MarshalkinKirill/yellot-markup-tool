﻿using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Drawing;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using MarkingUpDrawingTool.View.ViewInteraface;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarkingUpDrawingTool.View
{
    public partial class MainForm : Form, IView
    {
        private string fileName = string.Empty;

        
        private Layer imageLayer;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }


        //Объявление событий 
        private ISizeView sizeView;
        private IArrowView arrowView;
        private IGapView gapView;
        private IBorderView borderView;
        private IProjectionView projectionView;
        private IHoleView holeView;
        private ITableView tableView;

        public MainForm()
        {
            InitializeComponent();
            layerService = new LayerService();
            
            //Подписка на глобальные события формы
            this.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxHole.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxProjection.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxTable.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxArrow.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxSize.KeyDown += mainForm_KeyDown;
            this.ToolStripComboBoxGap.KeyDown += mainForm_KeyDown;

            //Views
            sizeView = new SizeView(this);
            arrowView = new ArrowView(this);
            gapView = new GapView(this);
            borderView = new BorderView(this);
            projectionView = new ProjectionView(this);
            holeView = new HoleView(this);
            tableView = new TableView(this);

        }


        //Метод инициализации слоев для отображения графики
        private void InitDrawLayers()
        {
            Layer projectionLayer = new Layer();
            projectionLayer.DrawActions = DrawProjection;
            layerService.AddLayer(projectionLayer);
            
            Layer holeLayer = new Layer();
            holeLayer.DrawActions = DrawHole;
            layerService.AddLayer(holeLayer);

            Layer tableLayer = new Layer();
            tableLayer.DrawActions = DrawTable;
            layerService.AddLayer(tableLayer);

            Layer sizeLayer = new Layer();
            sizeLayer.DrawActions = DrawRectangle;
            layerService.AddLayer(sizeLayer);

            Layer arrowLayer = new Layer();
            arrowLayer.DrawActions = DrawArrow;
            layerService.AddLayer(arrowLayer);

            Layer gapLayer = new Layer();
            gapLayer.DrawActions = DrawGap;
            layerService.AddLayer(gapLayer);

            Layer borderLayer = new Layer();
            borderLayer.DrawActions = DrawBorder;
            layerService.AddLayer(borderLayer);

            panel1.Controls.Add(layerService);
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            projectionView.Projection_KeyDown(sender, e);
            holeView.Hole_KeyDown(sender, e);
            tableView.Table_KeyDown(sender, e);
            arrowView.Arrow_KeyDown(sender, e);
            sizeView.Size_KeyDown(sender, e);
            gapView.Gap_KeyDown(sender, e);
        }

        //Метод для подключения изображения на форму 
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;

                imageLayer = new Layer(Image.FromFile(fileName), new Point(0,0), Path.GetFileNameWithoutExtension(fileName));

                layerService.AddLayer(imageLayer);
                layerService.Dock = DockStyle.Fill;
                layerService.Show();
                InitDrawLayers();

                panel1.Size = imageLayer.Image.Size;
                panel1.Controls.Add(layerService);

                Console.WriteLine(imageLayer.Image.Width.ToString(), imageLayer.Image.Height.ToString());
                Layer drawLayer = new Layer();
            } else
            {
                MessageBox.Show("Выберите чертеж.","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ToolStripMenuSaveAs_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*";
            this.saveFileDialog.Title = "Сохранить JSON файл";
            this.saveFileDialog.FileName = "Новый файл.json";

            DialogResult saveResult = saveFileDialog.ShowDialog();
            if (saveResult == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                // Создание отдельной директории
                string directoryPath = Path.GetDirectoryName(filePath);
                if (imageLayer != null && imageLayer.Name != string.Empty)
                {
                    directoryPath += "\\" + imageLayer.Name;
                    Console.WriteLine(directoryPath);
                    Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string sizesJson = JsonConvert.SerializeObject(sizeView.GetSizes(), Formatting.Indented);
                string arrowsJson = JsonConvert.SerializeObject(arrowView.GetArrows(), Formatting.Indented);
                string borderJson = JsonConvert.SerializeObject(borderView.GetBorder(), Formatting.Indented);
                string gapsJson = JsonConvert.SerializeObject(gapView.GetGaps(), Formatting.Indented);
                string projectionsJson = JsonConvert.SerializeObject(projectionView.GetProjections(), Formatting.Indented);
                string holesJson = JsonConvert.SerializeObject(holeView.GetHoles(), Formatting.Indented);
                string tablesJson = JsonConvert.SerializeObject(tableView.GetTables(), Formatting.Indented);

                var combinedJson = new JObject
                {
                    { "Sizes", JArray.Parse(sizesJson) },
                    { "Arrows", JArray.Parse(arrowsJson) },
                    { "Border", JArray.Parse(borderJson) },
                    { "Gaps", JArray.Parse(gapsJson) },
                    { "Projections", JArray.Parse(projectionsJson) },
                    { "Holes", JArray.Parse(holesJson) },
                    { "Tables", JArray.Parse(tablesJson) }
                };
                // Сохранение JSON-строки в файл
                string fileName = Path.GetFileName(filePath);
                string fullPath = Path.Combine(directoryPath, fileName);
                File.WriteAllText(fullPath, combinedJson.ToString());

                // Оповещение пользователя об успешном сохранении файла
                MessageBox.Show("JSON файл успешно сохранен.", "Сохранение файла", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("выберите путь сохранения", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //Методы для разметки Projection
        public ToolStripButton GetProjectionTool()
        {
            return toolStripButtonProjection;
        }

        public ToolStripMenuItem GetProjectionSaveTool()
        {
            return ToolStripMenuSaveProjection;
        }

        public ToolStripMenuItem GetProjectionDeleteTool()
        {
            return ToolStripMenuDeleteProjection;
        }

        public ToolStripComboBox GetProjectionComboBox()
        {
            return toolStripComboBoxProjection;
        }

        public void DrawProjection(Graphics g)
        {
            projectionView.DrawProjection(g);
        }

        //Методы для разметки Hole
        public ToolStripButton GetHoleTool()
        {
            return ToolStripButtonHole;
        }

        public ToolStripMenuItem GetHoleSearchTool()
        {
            return ToolStripMenuHoleSearch;
        }

        public ToolStripMenuItem GetHoleSaveTool()
        {
            return ToolStripMenuHoleSave;
        }

        public ToolStripMenuItem GetHoleDeleteTool()
        {
            return ToolStripMenuHoleDelete;
        }

        public ToolStripComboBox GetHoleComboBox()
        {
            return toolStripComboBoxHole;
        }

        public void DrawHole(Graphics g)
        {
            holeView.DrawHole(g);
        }

        //Методы для разметки Table
        public ToolStripMenuItem GetTableTool()
        {
            return ToolStripMenuTable;
        }

        public ToolStripMenuItem GetMainTableTool()
        {
            return ToolStripMenuMainTable;
        }

        public ToolStripMenuItem GetTableSaveTool()
        {
            return ToolStripMenuTableSave;
        }

        public ToolStripMenuItem GetTableDeleteTool()
        {
            return ToolStripMenuTableDelete;
        }

        public ToolStripComboBox GetTableComboBox()
        {
            return toolStripComboBoxTable;
        }

        public void DrawTable(Graphics g)
        {
            tableView.DrawTable(g);
        }

        //Перечень методов для разметки Size
        public ToolStripMenuItem GetSizeTool()
        {
            return this.ToolStripMenuSize;
        }

        public ToolStripMenuItem GetSizeAutoTool()
        {
            return this.ToolStripMenuSizeAuto;
        }

        public ToolStripMenuItem GetSizeSaveTool()
        {
            return this.ToolStripMenuSaveSize;
        }

        public ToolStripMenuItem GetSizeDeleteTool()
        {
            return this.ToolStripMenuDeleteSize;
        }

        public ToolStripComboBox GetSizeComboBox()
        {
            return this.toolStripComboBoxSize;
        }

        public Layer GetImageLayer()
        {
            return imageLayer;
        }

        public void DrawRectangle(Graphics g)
        {
            sizeView.DrawRectangle(g);
        }

        //Перечень методов для раметки Arrow
        public ToolStripButton GetArrowTool() 
        {
            return this.toolStripButtonArrow;
        }

        public ToolStripMenuItem GetArrowSaveTool()
        {
            return this.ToolStripMenuSaveArrow;
        }

        public ToolStripMenuItem GetArrowDeleteTool()
        {
            return this.ToolStripMenuDeleteArrow;
        }

        public ToolStripComboBox GetArrowComboBox()
        {
            return this.toolStripComboBoxArrow;
        }

        public void DrawArrow(Graphics g)
        {
            arrowView.DrawArrow(g);
        }

        //Перечень методов для разметки Gap
        public ToolStripButton GetGapTool()
        {
            return this.ToolStripButtonGap;
        }

        public ToolStripMenuItem GetGapSaveTool()
        {
            return this.ToolStripMenuSaveGap;
        }

        public ToolStripMenuItem GetGapDeleteTool()
        {
            return this.ToolStripMenuDeleteGap;
        }

        public ToolStripComboBox GetGapComboBox()
        {
            return this.ToolStripComboBoxGap;
        }

        public void DrawGap(Graphics g)
        {
            gapView.DrawGap(g);
        }

        //перечень методов для разметки Border
        public ToolStripButton GetBorderTool()
        {
            return this.ToolStripButtonBorder;
        }

        public void DrawBorder(Graphics g)
        {
            borderView.DrawBorder(g);
        }

    }
}
