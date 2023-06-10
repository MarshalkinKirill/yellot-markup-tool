using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.Presenter;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using CvPoint = OpenCvSharp.Point;
using Point = System.Drawing.Point;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Drawing.Drawing2D;
using MarkingUpDrawingTool.View.ViewInteraface;
using System.IO;
using DrSize = System.Drawing.Size;
using Size = MarkingUpDrawingTool.Model.Size;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarkingUpDrawingTool.View
{
    public partial class MainForm : Form, IView
    {
        private string fileName = string.Empty;
        private Projection currentProjection;
        private Hole currentHole;
        private Table currentTable;

        
        private Layer imageLayer;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }

        private ProjectionPresenter projectionPresenter;
        private HolePresenter holePresenter;
        private TablePresenter tablePresenter;

        //Объявление событий 
        private ISizeView sizeView;
        private IArrowView arrowView;
        private IGapView gapView;
        private IBorderView borderView;
        private IProjectionView projectionView;

        public event EventHandler<Point> PointMarked;
        public event EventHandler SaveProjection;
        public event EventHandler<Projection> DeleteProjection;

        public event EventHandler<Hole> AddHole;
        public event EventHandler SaveHole;
        public event EventHandler<Hole> DeleteHole;

        public event EventHandler<Table> AddTable;
        public event EventHandler<TableNote> AddTableNote;
        public event EventHandler SaveTable;
        public event EventHandler<Table> DeleteTable;

        public MainForm()
        {
            InitializeComponent();
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            //Вспомогательные инструменты TODO: Вынести в отдельный класс
            layerService = new LayerService();
            
            //Подписка на глобальные события формы
            toolStripComboBoxHole.SelectedIndexChanged += comboBoxHole_SelectedIndexChange;
            toolStripComboBoxTable.SelectedIndexChanged += comboBoxTable_SelectIndexChange;
            this.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxHole.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxProjection.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxTable.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxArrow.KeyDown += mainForm_KeyDown;
            this.toolStripComboBoxSize.KeyDown += mainForm_KeyDown;
            this.ToolStripComboBoxGap.KeyDown += mainForm_KeyDown;
            //Презенторы
            sizeView = new SizeView(this);
            arrowView = new ArrowView(this);
            gapView = new GapView(this);
            borderView = new BorderView(this);
            projectionView = new ProjectionView(this);

            //projectionPresenter = new ProjectionPresenter(this);
            holePresenter = new HolePresenter(this);
            tablePresenter = new TablePresenter(this);
            
        }


        //Метод инициализации слоев для отображения графики
        private void InitDrawLayers()
        {
            Layer projectionLayer = new Layer();
            projectionLayer.DrawActions = DrawProjection;
            layerService.AddLayer(projectionLayer);
            
            Layer holeLayer = new Layer();
            holeLayer.DrawActions = DrawCyrcle;
            layerService.AddLayer(holeLayer);

            Layer tableLayer = new Layer();
            tableLayer.DrawActions = DrawDotRectangle;
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
            if (layerService.DrawMainTableMod || layerService.DrawTableMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    сохранитьТаблицуToolStripMenuItem_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    удалитьТаблицуToolStripMenuItem_Click(this, e);
                }
            }
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
                //string Json = JsonConvert.SerializeObject(sizeView.GetSizes(), Formatting.Indented);
                //string sizesJson = JsonConvert.SerializeObject(sizeView.GetSizes(), Formatting.Indented);
                //string sizesJson = JsonConvert.SerializeObject(sizeView.GetSizes(), Formatting.Indented);

                var combinedJson = new JObject
                {
                    { "Sizes", JArray.Parse(sizesJson) },
                    { "Arrows", JArray.Parse(arrowsJson) },
                    { "Border", JArray.Parse(borderJson) },
                    { "Gaps", JArray.Parse(gapsJson) },
                    //{ "Object2", JObject.Parse(arrowsJson) },
                    //{ "Object2", JObject.Parse(arrowsJson) },
                    //{ "Object2", JObject.Parse(arrowsJson) }
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

        /*//Перечень методов для работы интрумента "Projection"
        //Подписка других методов на событие используемого инструмента 
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            layerService.DrawProjectionMod = !layerService.DrawProjectionMod; // Инвертируем режим рисования

            if (layerService.DrawProjectionMod)
            {
                toolStripButtonProjection.Checked = true;
                layerService.MouseMove += layerServiceProjection_MouseMove;
                layerService.MouseDown += layerServiceProjection_MouseDown;
                layerService.MouseUp += layerServiceProjection_MouseUp;
            }
            else
            {
                toolStripButtonProjection.Checked = false;
                layerService.MouseMove -= layerServiceProjection_MouseMove;
                layerService.MouseDown -= layerServiceProjection_MouseDown;
                layerService.MouseUp -= layerServiceProjection_MouseUp;
            }
        }
        //
        private void layerServiceProjection_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawProjectionMod && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = e.Location;
            }
            if (layerService.DrawProjectionMod && e.Button == MouseButtons.Right)
            {
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceProjection_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawProjectionMod && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left)
            {
                layerService.EndPoint = e.Location;

                //PointMarked?.Invoke(this, layerService.PointToImageCoordinates( e.Location, imageLayer));
                PointMarked?.Invoke(this, e.Location);
                layerService.Invalidate();
            }
        }

        private void layerServiceProjection_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawProjectionMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = e.Location;
                //PointMarked?.Invoke(this, layerService.PointToImageCoordinates(e.Location, imageLayer));
                layerService.Invalidate();
            }
        }

        public void DrawLine(Graphics g)
        {
            if (projectionPresenter.GetPoints().Count > 1)
            {

                //Console.WriteLine(layerService.StartPoint + " " + layerService.EndPoint);
                var points = projectionPresenter.GetPoints();

                Pen pen = new Pen(Color.Red, 3);
                if (points.Count > 1)
                {
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        g.DrawLine(pen, points[i], points[i + 1]);
                    }
                }
                if (layerService.DrawProjectionMod)
                {
                    g.DrawLine(pen, layerService.StartPoint, layerService.EndPoint);
                }
            }
        }

        //Метод для сохраннения отрисованной проекции
        private void сохранитьПроекциюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            SaveProjection?.Invoke(this, e);
            toolStripComboBoxProjection.Items.Clear();
            var objects = projectionPresenter.GetProjections();
            foreach (var obj in objects)
            {
                toolStripComboBoxProjection.Items.Add(obj);
                Console.WriteLine(obj.ToString());
            }

            toolStripComboBoxProjection.ComboBox.DisplayMember = "name";
            projectionPresenter.GetPoints().Clear();
        }

        //Метод для удаления выбранной проекции из ComboBox'а
        private void удалитьПроекциюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteProjection?.Invoke(this, currentProjection);

            toolStripComboBoxProjection.Items.Clear();
            var objects = projectionPresenter.GetProjections();
            foreach (var obj in objects)
            {
                toolStripComboBoxProjection.Items.Add(obj);
                Console.WriteLine(obj.ToString());
            }

            toolStripComboBoxProjection.ComboBox.DisplayMember = "name";
            projectionPresenter.GetPoints().Clear();
        }

        //Метод для выбора проекции из ComboBox'а
        private void comboBoxProjection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получение выбранного элемента
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Projection)comboBox.SelectedItem;
            
            currentProjection = selectedObject;
            // Обработка выбранного объекта
            Console.WriteLine("Выбрана " + selectedObject.ToString());
            Console.WriteLine("Кол-во точек " + selectedObject.Points.Count);
            projectionPresenter.SetPoints(selectedObject.Points);
            layerService.Invalidate();
        }*/


        //Перечень методов для обнаружения и работы с отверстиями
        //Используется библиотека OpenCvSharp
        //Инструмент для обнаружения отверстий с помощью машинного зрения
        private void toolStripButtonHole_Click(object sender, EventArgs e)
        {
            layerService.DrawHoleMod = !layerService.DrawHoleMod; // Инвертируем режим рисования

            if (layerService.DrawHoleMod)
            {
                toolStripButtonHole.Checked = true;
                layerService.MouseMove += layerServiceHole_MouseMove;
                layerService.MouseDown += layerServiceHole_MouseDown;
                layerService.MouseUp += layerServiceHole_MouseUp;
            }
            else
            {
                toolStripButtonHole.Checked = false;
                layerService.MouseMove -= layerServiceHole_MouseMove;
                layerService.MouseDown -= layerServiceHole_MouseDown;
                layerService.MouseUp -= layerServiceHole_MouseUp;
            }
        }

        private void layerServiceHole_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawHoleMod && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = e.Location;
            }
            if (layerService.DrawHoleMod && e.Button == MouseButtons.Right)
            {
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceHole_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawHoleMod && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left)
            {
                layerService.EndPoint = e.Location;
                float dist = (float)Math.Sqrt(Math.Pow(layerService.EndPoint.X - layerService.StartPoint.X, 2) + Math.Pow(layerService.EndPoint.Y - layerService.StartPoint.Y, 2));
                Console.WriteLine(dist.ToString());
                AddHole?.Invoke(this, new Hole(layerService.StartPoint,dist));
                layerService.Invalidate();
                currentHole = holePresenter.GetMarkedHole();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceHole_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawHoleMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = e.Location;
                float dist = (float)Math.Sqrt(Math.Pow(layerService.EndPoint.X - layerService.StartPoint.X, 2) + Math.Pow(layerService.EndPoint.Y - layerService.StartPoint.Y, 2));
                AddHole?.Invoke(this, new Hole(layerService.StartPoint, dist));
                currentHole = holePresenter.GetMarkedHole();
                layerService.Invalidate();
            }
        }


        public void DrawCyrcle(Graphics g)
        {
            List<Hole> holes = holePresenter.GetHoles();
            Pen pen = new Pen(Color.Red, 3);
            foreach (Hole hole in holes)
            {
                Point center = hole.Center;
                float radius = hole.Radius;
                int x = (int)(center.X - radius);
                int y = (int)(center.Y - radius);

                // Вычисляем ширину и высоту прямоугольника
                int diameter = (int)(radius * 2);

                // Рисуем окружность с использованием метода DrawEllipse объекта Graphics
                g.DrawEllipse(pen, x, y, diameter, diameter);
            }
            if (layerService.DrawHoleMod)
            {
                Hole _markedHole = currentHole;
                //Console.WriteLine(_markedHole.Center.ToString());
                //Console.WriteLine(_markedHole.Radius.ToString());
                Point center = _markedHole.Center;
                float radius = _markedHole.Radius;
                int x = (int)(center.X - radius);
                int y = (int)(center.Y - radius);

                // Вычисляем ширину и высоту прямоугольника
                int diameter = (int)(radius * 2);

                pen.Color = Color.Purple;
                // Рисуем окружность с использованием метода DrawEllipse объекта Graphics
                g.DrawEllipse(pen, x, y, diameter, diameter);
            }
        }

        private void поискОтверстийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<CircleSegment> _circles = holePresenter.DetectHoles(imageLayer);
            foreach (var _circle in _circles)
            {
                Point _center = new Point((int)_circle.Center.X,(int)_circle.Center.Y);

                AddHole?.Invoke(this, new Hole(_center, _circle.Radius));
                SaveHole?.Invoke(this, e);
                layerService.Invalidate();

                toolStripComboBoxHole.Items.Clear();
                List<Hole> _holes = holePresenter.GetHoles();
                foreach (var _hole in _holes)
                {
                    toolStripComboBoxHole.Items.Add(_hole);
                    Console.WriteLine(_hole.ToString());
                }

                toolStripComboBoxHole.ComboBox.DisplayMember = "name";
                holePresenter.CleanMarkedHole();
            }
        }

        private void comboBoxHole_SelectedIndexChange(object sender, EventArgs e)
        {
            // Получение выбранного элемента
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Hole)comboBox.SelectedItem;
            currentHole = selectedObject;
            // Обработка выбранного объекта
            Console.WriteLine("Выбрана " + selectedObject.ToString());
            Console.WriteLine("Center " + selectedObject.Center.ToString());
            Console.WriteLine("Radius " + selectedObject.Radius.ToString());
            //holePresenter
            layerService.Invalidate();
        }

        private void сохранитьОтверстиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (layerService.DrawHoleMod)
            {
                SaveHole?.Invoke(this, e);
                toolStripComboBoxHole.Items.Clear();
                List<Hole> _holes = holePresenter.GetHoles();
                foreach (var _hole in _holes)
                {
                    toolStripComboBoxHole.Items.Add(_hole);
                    Console.WriteLine(_hole.ToString());
                }

                toolStripComboBoxHole.ComboBox.DisplayMember = "name";
                holePresenter.CleanMarkedHole();
            }
        }

        private void удалитьОтверстиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteHole?.Invoke(this, currentHole);
            currentHole = new Hole();
            toolStripComboBoxHole.Items.Clear();
            List<Hole> _holes = holePresenter.GetHoles();
            foreach (var _hole in _holes)
            {
                toolStripComboBoxHole.Items.Add(_hole);
                Console.WriteLine(_hole.ToString());
            }

            toolStripComboBoxHole.ComboBox.DisplayMember = "name";
            holePresenter.CleanMarkedHole();
            layerService.Invalidate()
;        }




        //Перечень методов для выделения таблиц
        private void выделитьГлавнуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layerService.DrawMainTableMod = !layerService.DrawMainTableMod;
            layerService.DrawTableMod = false;
            SaveDrawTableMod();
        }

        private void выделитьПобочнуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layerService.DrawTableMod = !layerService.DrawTableMod;
            layerService.DrawMainTableMod = false;
            SaveDrawTableMod();
        }

        private void SaveDrawTableMod()
        {
            if (layerService.DrawTableMod || layerService.DrawMainTableMod)
            {
                if (layerService.DrawMainTableMod)
                {
                    выделитьГлавнуюТаблицуToolStripMenuItem.Checked = true;
                    выделитьПобочнуюТаблицуToolStripMenuItem.Checked = false;
                }
                else
                {
                    выделитьПобочнуюТаблицуToolStripMenuItem.Checked = true;
                    выделитьГлавнуюТаблицуToolStripMenuItem.Checked = false;
                }
                layerService.MouseMove += layerServiceTable_MouseMove;
                layerService.MouseDown += layerServiceTable_MouseDown;
                layerService.MouseUp += layerServiceTable_MouseUp;
            }
            else
            {
                выделитьГлавнуюТаблицуToolStripMenuItem.Checked = false;
                выделитьПобочнуюТаблицуToolStripMenuItem.Checked = false;

                layerService.MouseMove -= layerServiceTable_MouseMove;
                layerService.MouseDown -= layerServiceTable_MouseDown;
                layerService.MouseUp -= layerServiceTable_MouseUp;
            }
        }

        private void layerServiceTable_MouseDown(object sender, MouseEventArgs e)
        {
            if ((layerService.DrawTableMod || layerService.DrawMainTableMod) && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = e.Location;
            }
            if ((layerService.DrawTableMod || layerService.DrawMainTableMod) && e.Button == MouseButtons.Right)
            {
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceTable_MouseUp(object sender, MouseEventArgs e)
        {
            if ((layerService.DrawTableMod || layerService.DrawMainTableMod) && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left)
            {
                layerService.EndPoint = e.Location;
                AddTable?.Invoke(this, new Table(layerService.StartPoint, layerService.EndPoint));

                if (layerService.DrawMainTableMod)
                {
                    TableNoteForm noteForm = new TableNoteForm(this);
                    noteForm.ShowDialog();
                }
                currentTable = tablePresenter.GetMarkedTable();

                layerService.Invalidate();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceTable_MouseMove(object sender, MouseEventArgs e)
        {
            if ((layerService.DrawTableMod || layerService.DrawMainTableMod) && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = e.Location;
                AddTable?.Invoke(this, new Table(layerService.StartPoint, layerService.EndPoint));
                currentTable = tablePresenter.GetMarkedTable();
                layerService.Invalidate();
            }
        }

        private void DrawDotRectangle(Graphics g)
        {
            layerService.DrawDotRectangle(g, tablePresenter, currentTable);
        }

        public void SetTableNote(TableNote _tableNote)
        {
            AddTableNote?.Invoke(this, _tableNote);
        }

        private void сохранитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((layerService.DrawTableMod || layerService.DrawMainTableMod))
            {
                SaveTable?.Invoke(this, e);
                toolStripComboBoxTable.Items.Clear();
                List<Table> tables = tablePresenter.GetTables();
                foreach (Table table in tables)
                {
                    toolStripComboBoxTable.Items.Add(table);
                }

                toolStripComboBoxTable.ComboBox.DisplayMember = "name";
                tablePresenter.CleanMarkedTable();
            }
        }

        private void удалитьТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteTable?.Invoke(this, currentTable);
            currentTable = new Table();
            toolStripComboBoxTable.Items.Clear();
            List<Table> tables = tablePresenter.GetTables();
            foreach (var table in tables)
            {
                Console.WriteLine(table.Name);
                toolStripComboBoxTable.Items.Add(table);
            }

            toolStripComboBoxTable.ComboBox.DisplayMember = "name";
            tablePresenter.CleanMarkedTable();
            layerService.Invalidate();
        }

        private void comboBoxTable_SelectIndexChange(object sender, EventArgs e)
        {
            // Получение выбранного элемента
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Table)comboBox.SelectedItem;
            currentTable = selectedObject;
            // Обработка выбранного объекта
            Console.WriteLine("Выбрана " + selectedObject.TableNote.Name);
            Console.WriteLine(selectedObject.Start.ToString() + " - " + selectedObject.End.ToString());
            //TablePresenter
            layerService.Invalidate();
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
