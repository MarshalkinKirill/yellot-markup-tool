using MarkingUpDrawingTool.Model;
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
using System.Web.UI.WebControls;
using Image = System.Drawing.Image;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;
using Size = MarkingUpDrawingTool.Model.Size;
using Table = MarkingUpDrawingTool.Model.Table;
using System.Linq;

namespace MarkingUpDrawingTool.View
{
    public partial class MainForm : Form, IView
    {
        private string fileName = string.Empty;

        
        private Layer imageLayer;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        VScrollBar vScrollBar;
        HScrollBar hScrollBar;
        private Timer scrollTimer;
        private const int DebounceDelay = 100;

        //Объявление событий 
        private ISizeView sizeView;
        private IArrowView arrowView;
        private IGapView gapView;
        private IBorderView borderView;
        private IProjectionView projectionView;
        private IProjectionRoiView projectionRoiView;
        private IHoleView holeView;
        private ITableView tableView;
        private ISymbolView symbolView;

        public MainForm()
        {
            InitializeComponent();
            layerService = new LayerService();
            scrollTimer = new Timer();
            scrollTimer.Interval = DebounceDelay;
            scrollTimer.Tick += ScrollTimer_Tick;

            //Подписка на глобальные события формы
            this.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxHole.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxProjection.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxTable.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxArrow.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxSize.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxGap.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxProjectionRoi.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxSymbol.KeyDown += mainForm_KeyDown;
            this.Resize += mainForm_Resize;

            //Views
            sizeView = new SizeView(this);
            arrowView = new ArrowView(this);
            gapView = new GapView(this);
            borderView = new BorderView(this);
            projectionView = new ProjectionView(this);
            holeView = new HoleView(this);
            tableView = new TableView(this);
            projectionRoiView = new ProjectionRoiView(this);
            symbolView = new SymbolView(this);

        }

        private void mainForm_Resize(object sender, EventArgs e)
        {
            if (imageLayer != null)
            {
                panel1.Controls.Remove(vScrollBar);
                vScrollBar = new VScrollBar();
                vScrollBar.Dock = DockStyle.Right;
                vScrollBar.Maximum = imageLayer.Image.Height + 40 - panel1.Height;
                if (vScrollBar.Maximum > 0)
                {
                    vScrollBar.Scroll += VScrollBar_Scroll;
                    panel1.Controls.Add(vScrollBar);
                }
                else
                {
                    vScrollBar.Maximum = 0;
                    vScrollBar.Minimum = 0;
                }
                // Создание горизонтального скролла
                panel1.Controls.Remove(hScrollBar);
                hScrollBar = new HScrollBar();
                hScrollBar.Dock = DockStyle.Bottom;
                hScrollBar.Maximum = imageLayer.Image.Width + 40 - panel1.Width;
                if (hScrollBar.Maximum > 0)
                {
                    hScrollBar.Scroll += HScrollBar_Scroll;
                    panel1.Controls.Add(hScrollBar);
                }
                else
                {
                    hScrollBar.Maximum = 0;
                    hScrollBar.Minimum = 0;
                }
            }
        }


        //Метод инициализации слоев для отображения графики
        private void InitDrawLayers()
        {
            Layer projectionLayer = new Layer(new Point(0,0));
            projectionLayer.DrawActions = DrawProjection;
            layerService.AddLayer(projectionLayer);

            Layer projectionRoi = new Layer();
            projectionRoi.DrawActions = DrawProjectionRoi;
            layerService.AddLayer(projectionRoi);
            
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

            Layer symbolLayer = new Layer();
            symbolLayer.DrawActions = DrawSymbol;
            layerService.AddLayer(symbolLayer);

            // Создание вертикального скролла
            vScrollBar = new VScrollBar();
            vScrollBar.Dock = DockStyle.Right;
            vScrollBar.Maximum = imageLayer.Image.Height + 40 - panel1.Height;
            if (vScrollBar.Maximum > 0)
            {
                vScrollBar.Scroll += VScrollBar_Scroll;
                panel1.Controls.Add(vScrollBar);
            } else
            {
                vScrollBar.Maximum = 0;
                vScrollBar.Minimum = 0;
            }
            // Создание горизонтального скролла
            hScrollBar = new HScrollBar();
            hScrollBar.Dock = DockStyle.Bottom;
            hScrollBar.Maximum = imageLayer.Image.Width + 40 - panel1.Width;
            if (hScrollBar.Maximum > 0)
            {
                hScrollBar.Scroll += HScrollBar_Scroll;
                panel1.Controls.Add(hScrollBar);
            } else
            {
                hScrollBar.Maximum = 0;
                hScrollBar.Minimum = 0;
            }
            
            
            panel1.AutoScroll = true;
            panel1.Controls.Add(layerService);
        }

        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // Получение направления прокрутки
            ScrollOrientation orientation = e.ScrollOrientation;

            // Получение значения прокрутки с инверсией
            int scrollValue = -e.NewValue + e.OldValue;

            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                // Проверка на превышение максимального значения
                if (layerService.Top + scrollValue >= vScrollBar.Maximum)
                {
                    // Установка максимального значения, если превышение
                    layerService.Top = vScrollBar.Maximum;
                }
                else
                {
                    layerService.Top += scrollValue;
                }
            }
            layerService.vPanel_Scroll(this, e);

            // Обновление отображения
            layerService.Invalidate();
        }
        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            // Получение направления прокрутки
            ScrollOrientation orientation = e.ScrollOrientation;

            // Получение значения прокрутки с инверсией
            int scrollValue = -e.NewValue + e.OldValue;

            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                // Проверка на превышение максимального значения
                if (layerService.Left + scrollValue >= hScrollBar.Maximum)
                {
                    // Установка максимального значения, если превышение
                    layerService.Left = hScrollBar.Maximum;
                }
                else
                {
                    layerService.Left += scrollValue;
                }
            }
            layerService.hPanel_Scroll(this, e);

            // Обновление отображения
            layerService.Invalidate();
        }
        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            projectionView.Projection_KeyDown(sender, e);
            projectionRoiView.ProjectionRoi_KeyDown(sender, e);
            holeView.Hole_KeyDown(sender, e);
            tableView.Table_KeyDown(sender, e);
            arrowView.Arrow_KeyDown(sender, e);
            sizeView.Size_KeyDown(sender, e);
            gapView.Gap_KeyDown(sender, e);
            symbolView.Symbol_KeyDown(sender, e);
            borderView.Border_KeyDown(sender, e);

            if (e.KeyCode == Keys.ShiftKey)
            {
                ScrollMouseEvent();
            }
        }

        private void ScrollMouseEvent()
        {
            layerService.ScrollMod = !layerService.ScrollMod;
            if (layerService.ScrollMod)
            {
                layerService.MouseDown += mainForm_MouseDown;
                layerService.MouseMove += mainForm_MouseMove;
            }
            else
            {
                layerService.MouseDown -= mainForm_MouseDown;
                layerService.MouseMove -= mainForm_MouseMove;
            }
        }

        private void mainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                layerService.MainFormStartPoint = e.Location;
            }
        }


        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            scrollTimer.Stop();
            VScrollBar_Scroll(sender, layerService.LastVScrollEventArgs);
            HScrollBar_Scroll(sender, layerService.LastHScrollEventArgs);
        }

        private void mainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                int newValue;
                /*Console.WriteLine(imageLayer.Image.Height.ToString() + " " +  panel1.Height.ToString());
                Console.WriteLine(imageLayer.Image.Width.ToString() + " " + panel1.Width.ToString());*/
                if (true/*imageLayer.Image.Height > panel1.Height*/)
                {
                    // Вертикальный Scroll
                    int deltaY = e.Y - layerService.MainFormStartPoint.Y;
                    newValue = vScrollBar.Value - deltaY;

                    // Ограничиваем новое значение скролла в пределах минимального и максимального
                    newValue = Math.Max(vScrollBar.Minimum, Math.Min(vScrollBar.Maximum, newValue));

                    vScrollBar.Value = newValue;
                    layerService.LastVScrollEventArgs = new ScrollEventArgs(ScrollEventType.ThumbPosition, newValue);
                }
                if (true/*imageLayer.Image.Width > panel1.Width*/)
                {
                    // Горизонтальный Scroll
                    int deltaX = e.X - layerService.MainFormStartPoint.X;
                    newValue = hScrollBar.Value - deltaX;

                    // Ограничиваем новое значение скролла в пределах минимального и максимального
                    newValue = Math.Max(hScrollBar.Minimum, Math.Min(hScrollBar.Maximum, newValue));

                    hScrollBar.Value = newValue;
                    layerService.LastHScrollEventArgs = new ScrollEventArgs(ScrollEventType.ThumbPosition, newValue);
                }

                scrollTimer.Stop();
                scrollTimer.Start();

                layerService.MainFormStartPoint = e.Location;
            }
        }

        //Метод для подключения изображения на форму 
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                Refresh();
                fileName = openFileDialog1.FileName;

                imageLayer = new Layer(Image.FromFile(fileName), new Point(0,0), Path.GetFileNameWithoutExtension(fileName));
                
                layerService.AddLayer(imageLayer);
                layerService.Dock = DockStyle.Fill;
                layerService.Size = new System.Drawing.Size(imageLayer.Image.Width + 40, imageLayer.Image.Height + 40);

                //layerService.Show();
                /*layerServiceControl.AddLayer(imageLayer);
                layerServiceControl.Dock = DockStyle.Fill;
                layerServiceControl.Show();*/
                InitDrawLayers();

                
                //panel1.Size = imageLayer.Image.Size;
                //panel1.Controls.Add(layerServiceControl);
                panel1.Controls.Add(layerService);

            } else
            {
                MessageBox.Show("Выберите чертеж.","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Refresh()
        {
            ToolStripButtonBorder.Enabled = true;

            toolStripButtonProjection.Enabled = true;
            toolStripComboBoxProjection.Enabled = true;
            toolStripComboBoxProjection.Items.Clear();

            toolStripButtonProjectionRoi.Enabled = true;
            toolStripComboBoxProjectionRoi.Enabled = true;
            toolStripComboBoxProjectionRoi.Items.Clear();

            toolStripButtonSymbol.Enabled = true;
            toolStripComboBoxSymbol.Enabled = true;
            toolStripComboBoxSymbol.Items.Clear();

            ToolStripButtonHole.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripComboBoxHole.Enabled = true;
            toolStripComboBoxHole.Items.Clear();

            toolStripButton1.Enabled = true;
            toolStripComboBoxTable.Enabled = true;
            toolStripComboBoxTable.Items.Clear();

            toolStripButton4.Enabled = true;
            toolStripComboBoxSize.Enabled = true;
            toolStripComboBoxSize.Items.Clear();

            toolStripButtonArrow.Enabled = true;
            toolStripComboBoxArrow.Enabled = true;
            toolStripComboBoxArrow.Items.Clear();

            ToolStripButtonGap.Enabled = true;
            toolStripComboBoxGap.Enabled = true;
            toolStripComboBoxGap.Items.Clear();

            layerService = new LayerService();
            sizeView = new SizeView(this);
            arrowView = new ArrowView(this);
            gapView = new GapView(this);
            borderView = new BorderView(this);
            projectionView = new ProjectionView(this);
            projectionRoiView = new ProjectionRoiView(this);
            holeView = new HoleView(this);
            tableView = new TableView(this);
            symbolView = new SymbolView(this);
            panel1.Controls.Clear();
        }
        private void ToolStripMenuSaveAs_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "JSON файлы (*.json)|*.json|Все файлы (*.*)|*.*";
            this.saveFileDialog.Title = "Сохранить JSON файл";
            this.saveFileDialog.FileName = Path.GetFileNameWithoutExtension(fileName) + ".json";

            DialogResult saveResult = saveFileDialog.ShowDialog();
            if (saveResult == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                // Создание отдельной директории
                string directoryPath = Path.GetDirectoryName(filePath);
                /*if (imageLayer != null && imageLayer.Name != string.Empty)
                {
                    directoryPath += "\\" + imageLayer.Name;
                    Directory.CreateDirectory(directoryPath);
                }
                else
                {
                    Directory.CreateDirectory(directoryPath);
                }*/

                string sizesJson = JsonConvert.SerializeObject(sizeView.GetSizes(), Formatting.Indented);
                string arrowsJson = JsonConvert.SerializeObject(arrowView.GetArrows(), Formatting.Indented);
                string borderJson = JsonConvert.SerializeObject(borderView.GetBorder(), Formatting.Indented);
                string gapsJson = JsonConvert.SerializeObject(gapView.GetGaps(), Formatting.Indented);
                string projectionsJson = JsonConvert.SerializeObject(projectionView.GetProjections(), Formatting.Indented);
                string projectionsRoiJson = JsonConvert.SerializeObject(projectionRoiView.GetProjectionRois(), Formatting.Indented);
                string holesJson = JsonConvert.SerializeObject(holeView.GetHoles());
                string tablesJson = JsonConvert.SerializeObject(tableView.GetTables(), Formatting.Indented);
                string symbolsJson = JsonConvert.SerializeObject(symbolView.GetSymbols(), Formatting.Indented);

                JObject combinedJson = new JObject { };

                /*var combinedJson = new JObject
                {
                    //{ "Sizes", JArray.Parse(sizesJson) },
                    { "Arrows", JArray.Parse(arrowsJson) },
                    { "Border", JArray.Parse(borderJson) },
                    { "Gaps", JArray.Parse(gapsJson) },
                    { "Projections", JArray.Parse(projectionsJson) },
                    { "ProjectionsRoi", JArray.Parse(projectionsRoiJson) },
                    { "Holes", JArray.Parse(holesJson) },
                    { "Tables", JArray.Parse(tablesJson) },
                    { "Symbols", JArray.Parse(symbolsJson) }
                };*/

                if (sizesJson != "[]") combinedJson.Add("Sizes", JArray.Parse(sizesJson)); 
                if (arrowsJson != "[]") combinedJson.Add("Arrows", JArray.Parse(arrowsJson)); 
                if (borderJson != "[]") combinedJson.Add("Border", JArray.Parse(borderJson)); 
                if (gapsJson != "[]") combinedJson.Add("Gaps", JArray.Parse(gapsJson)); 
                if (projectionsJson != "[]") combinedJson.Add("Projections", JArray.Parse(projectionsJson)); 
                if (projectionsRoiJson != "[]") combinedJson.Add("ProjectionsRoi", JArray.Parse(projectionsRoiJson)); 
                if (holesJson != "[]") combinedJson.Add("Holes", JArray.Parse(holesJson)); 
                if (tablesJson != "[]") combinedJson.Add("Tables", JArray.Parse(tablesJson)); 
                if (symbolsJson != "[]") combinedJson.Add("Symbols", JArray.Parse(symbolsJson));
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

        private void jsonLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openJsonFileDialog.Title = "Загрузить JSON файл";

            DialogResult loadJsonResult = openJsonFileDialog.ShowDialog();
            if (loadJsonResult == DialogResult.OK)
            {
                string jsonFilePath = openJsonFileDialog.FileName;

                try
                {
                    if (fileName != null)
                    {
                        string jsonContent = File.ReadAllText(jsonFilePath);

                        DeserializeObjects deserializeObjects = JsonConvert.DeserializeObject<DeserializeObjects>(jsonContent);

                        imageLayer = new Layer(Image.FromFile(fileName), new Point(0, 0), Path.GetFileNameWithoutExtension(fileName));

                        layerService.AddLayer(imageLayer);
                        layerService.Dock = DockStyle.Fill;
                        layerService.Size = new System.Drawing.Size(imageLayer.Image.Width, imageLayer.Image.Height);
                        InitDrawLayers();
                        panel1.Controls.Add(layerService);

                        if (deserializeObjects.Sizes != null) sizeView.SetSizes(deserializeObjects.Sizes.ToList());
                        if (deserializeObjects.Arrows != null) arrowView.SetArrows(deserializeObjects.Arrows.ToList());
                        if (deserializeObjects.Border != null) borderView.SetBorders(deserializeObjects.Border.ToList().First());
                        if (deserializeObjects.Gaps != null) gapView.SetGaps(deserializeObjects.Gaps.ToList());
                        if (deserializeObjects.Projections != null) projectionView.SetProjections(deserializeObjects.Projections.ToList());
                        if (deserializeObjects.ProjectionRois != null) projectionRoiView.SetProjectionRois(deserializeObjects.ProjectionRois.ToList());
                        if (deserializeObjects.Holes != null) holeView.SetHoles(deserializeObjects.Holes.ToList());
                        if (deserializeObjects.Tables != null) tableView.SetTables(deserializeObjects.Tables.ToList());
                        if (deserializeObjects.Symbols != null) symbolView.SetSymbols(deserializeObjects.Symbols.ToList());
                    }
                    else
                    {
                        MessageBox.Show("Откройте изображение!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Откройте изображение!");
                }
            }
            else
            {
                MessageBox.Show("Выберите json-файл.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
                
            }

        public void MainForm_CheckedChanged()
        {
            toolStripButtonProjectionRoi.Checked = false;
            toolStripButtonSymbol.Checked = false;
            ToolStripButtonHole.Checked = false;
            toolStripButtonProjection.Checked = false;
            ToolStripButtonGap.Checked = false;
            ToolStripMenuSize.Checked = false;
            ToolStripMenuTable.Checked = false;
            ToolStripMenuMainTable.Checked = false;
            ToolStripMenuLinearArrow.Checked = false;
            ToolStripMenuAngularArrow.Checked = false;
            ToolStripMenuRadialArrow.Checked = false;
            ToolStripMenuDiametralArrow.Checked = false;
            ToolStripMenuReferenceArrow.Checked = false;
            ToolStripMenuConeArrow.Checked = false;
            ToolStripMenuChamferArrow.Checked = false;
            ToolStripButtonBorder.Checked = false;
            layerService.RefreshDrawMods();
        }

        //Save Delete Get obj
        public ToolStripMenuItem GetSaveTool()
        {
            return ToolStripMenuSaveObject;
        }

        public ToolStripMenuItem GetDeleteTool()
        {
            return ToolStripMenuDeleteObject;
        }

        //Методы для разметки Projection
        public ToolStripButton GetProjectionTool()
        {
            return toolStripButtonProjection;
        }

        public ToolStripComboBox GetProjectionComboBox()
        {
            return toolStripComboBoxProjection;
        }

        public void DrawProjection(Graphics g)
        {
            projectionView.DrawProjection(g);
        }

        //Методы для разметки ProjectionRoi
        public ToolStripButton GetProjectionRoiTool()
        {
            return toolStripButtonProjectionRoi;
        }

        public ToolStripMenuItem GetProjectionRoiSaveTool()
        {
            return ToolStripMenuSaveObject;
        }

        public ToolStripMenuItem GetProjectionRoiDeleteTool()
        {
            return ToolStripMenuDeleteObject;
        }

        public ToolStripComboBox GetProjectionRoiComboBox()
        {
            return toolStripComboBoxProjectionRoi;
        }

        public void DrawProjectionRoi(Graphics g)
        {
            projectionRoiView.DrawProjectionRoi(g);
        }

        //Symbol
        public ToolStripButton GetSymbolTool()
        {
            return toolStripButtonSymbol;
        }

        public ToolStripComboBox GetSymbolComboBox()
        {
            return toolStripComboBoxSymbol;
        }

        public void DrawSymbol(Graphics g)
        {
            symbolView.DrawSymbol(g);
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
        public ToolStripMenuItem GetLinearArrowTool() 
        {
            return this.ToolStripMenuLinearArrow;
        }

        public ToolStripMenuItem GetAngularArrowTool()
        {
            return this.ToolStripMenuAngularArrow;
        }

        public ToolStripMenuItem GetRadialArrowTool()
        {
            return this.ToolStripMenuRadialArrow;
        }

        public ToolStripMenuItem GetDiametralArrowTool()
        {
            return this.ToolStripMenuDiametralArrow;
        }

        public ToolStripMenuItem GetReferenceArrowTool()
        {
            return this.ToolStripMenuReferenceArrow;
        }

        public ToolStripMenuItem GetConeArrowTool()
        {
            return this.ToolStripMenuConeArrow;
        }

        public ToolStripMenuItem GetChamferArrowTool()
        {
            return this.ToolStripMenuChamferArrow;
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

        public ToolStripComboBox GetGapComboBox()
        {
            return this.toolStripComboBoxGap;
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
