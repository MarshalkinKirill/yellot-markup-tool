using Emgu.CV;
using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.Presenter;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInteraface;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View
{
    public class TableView : ITableView
    {
        private IView mainForm;
        private TablePresenter tablePresenter;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        private Table currentTable { get; set; }
        public Table CurrentTable { get => currentTable; set => currentTable = value; }

        private ToolStripMenuItem tableTool;
        private ToolStripMenuItem tableMainTool;
        private ToolStripMenuItem tableDeleteTool;
        private ToolStripMenuItem tableSaveTool;
        private ToolStripComboBox tableComboBox;

        public event EventHandler<Table> AddTable;
        public event EventHandler<TableNote> AddTableNote;
        public event EventHandler DetectTableNote;
        public event EventHandler SaveTable;
        public event EventHandler<Table> DeleteTable;
        public TableView(IView _mainForm)
        {
            mainForm = _mainForm;
            tablePresenter = new TablePresenter(this);
            layerService = mainForm.LayerService;

            tableTool = mainForm.GetTableTool();
            tableMainTool = mainForm.GetMainTableTool();
            tableSaveTool = mainForm.GetTableSaveTool();
            tableDeleteTool = mainForm.GetTableDeleteTool();
            tableComboBox = mainForm.GetTableComboBox();

            tableTool.Click += TableTool_Click;
            tableMainTool.Click += TableMainTool_Click;
            tableSaveTool.Click += TableSaveTool_Click;
            tableDeleteTool.Click += TableDeleteTool_Click;
            tableComboBox.SelectedIndexChanged += TableComboBox_SelectedIndexChanged;
        }

        public void Table_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawTableMod || layerService.DrawMainTableMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    TableSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    TableDeleteTool_Click(this, e);
                }
            }
        }

        private void TableTool_Click(object sender, EventArgs e)
        {
            layerService.DrawTableMod = !layerService.DrawTableMod;
            layerService.DrawMainTableMod = false;
            SaveDrawTableMod();
        }

        private void TableMainTool_Click(object sender, EventArgs e)
        {
            layerService.DrawMainTableMod = !layerService.DrawMainTableMod;
            layerService.DrawTableMod = false;
            SaveDrawTableMod();
        }

        private void SaveDrawTableMod()
        {
            if (layerService.DrawTableMod || layerService.DrawMainTableMod)
            {
                if (layerService.DrawMainTableMod)
                {
                    tableMainTool.Checked = true;
                    tableTool.Checked = false;
                }
                else
                {
                    tableTool.Checked = true;
                    tableMainTool.Checked = false;
                }
                layerService.MouseMove += layerServiceTable_MouseMove;
                layerService.MouseDown += layerServiceTable_MouseDown;
                layerService.MouseUp += layerServiceTable_MouseUp;
            }
            else
            {
                tableMainTool.Checked = false;
                tableTool.Checked = false;

                layerService.MouseMove -= layerServiceTable_MouseMove;
                layerService.MouseDown -= layerServiceTable_MouseDown;
                layerService.MouseUp -= layerServiceTable_MouseUp;
            }
        }

        private void layerServiceTable_MouseDown(object sender, MouseEventArgs e)
        {
            if ((layerService.DrawTableMod || layerService.DrawMainTableMod) && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
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
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                AddTable?.Invoke(this, new Table(layerService.StartPoint, layerService.EndPoint, layerService.Origin));

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
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                AddTable?.Invoke(this, new Table(layerService.StartPoint, layerService.EndPoint, layerService.Origin));
                currentTable = tablePresenter.GetMarkedTable();
                layerService.Invalidate();
            }
        }

        private void TableSaveTool_Click(object sender, EventArgs e)
        {
            if ((layerService.DrawTableMod || layerService.DrawMainTableMod))
            {
                SaveTable?.Invoke(this, e);
                tableComboBox.Items.Clear();
                List<Table> tables = tablePresenter.GetTables();
                foreach (Table table in tables)
                {
                    tableComboBox.Items.Add(table);
                }

                tableComboBox.ComboBox.DisplayMember = "name";
                tablePresenter.CleanMarkedTable();
            }
        }

        private void TableDeleteTool_Click(object sender, EventArgs e)
        {
            DeleteTable?.Invoke(this, currentTable);
            currentTable = new Table();
            tableComboBox.Items.Clear();
            List<Table> tables = tablePresenter.GetTables();
            foreach (var table in tables)
            {
                Console.WriteLine(table.Name);
                tableComboBox.Items.Add(table);
            }

            tableComboBox.ComboBox.DisplayMember = "name";
            tablePresenter.CleanMarkedTable();
            layerService.Invalidate();
        }

        private void TableComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        public void DrawTable(Graphics g)
        {
            List<Table> tables = tablePresenter.GetTables();

            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = DashStyle.Dot;

            foreach (Table table in tables)
            {
                g.TranslateTransform(table.Origin.X, table.Origin.Y);
                Point start = new Point(table.Start.X - (table.Origin.X), table.Start.Y - (table.Origin.Y));
                Point end = new Point(table.End.X - (table.Origin.X), table.End.Y - (table.Origin.Y));

                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);

                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);

                g.TranslateTransform(-table.Origin.X, -table.Origin.Y);
            }
            if (layerService.DrawTableMod || LayerService.DrawMainTableMod)
            {
                pen.Color = Color.Purple;

                Table table = currentTable;
                g.TranslateTransform(table.Origin.X, table.Origin.Y);
                Point start = new Point(table.Start.X - (table.Origin.X), table.Start.Y - (table.Origin.Y));
                Point end = new Point(table.End.X - (table.Origin.X), table.End.Y - (table.Origin.Y));

                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);

                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);

                g.TranslateTransform(-table.Origin.X, -table.Origin.Y);
            }
        }

        public void SetTableNote(TableNote _tableNote)
        {
            AddTableNote?.Invoke(this, _tableNote);
        }

        public List<Table> GetTables()
        {
            return tablePresenter.GetTables();
        }
    }
}
