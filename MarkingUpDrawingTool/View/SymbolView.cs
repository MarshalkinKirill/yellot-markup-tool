using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.Presenter;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInteraface;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View
{
    public class SymbolView : ISymbolView
    {
        private IView MainForm;
        private SymbolPresenter SymbolPresenter;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        private Symbol currentSymbol { get; set; }
        public Symbol CurrentSymbol { get => currentSymbol; set => currentSymbol = value; }

        private ToolStripButton SymbolTool;
        private ToolStripMenuItem SymbolDeleteTool;
        private ToolStripMenuItem SymbolSaveTool;
        private ToolStripComboBox SymbolComboBox;

        public event EventHandler<Symbol> AddSymbol;
        public event EventHandler SaveSymbol;
        public event EventHandler<Symbol> DeleteSymbol;

        public SymbolView(IView mainForm)
        {
            this.MainForm = mainForm;
            this.SymbolPresenter = new SymbolPresenter(this);
            this.layerService = MainForm.LayerService;

            SymbolTool = MainForm.GetSymbolTool();
            SymbolSaveTool = MainForm.GetSaveTool();
            SymbolDeleteTool = MainForm.GetDeleteTool();
            SymbolComboBox = MainForm.GetSymbolComboBox();

            SymbolTool.Click += SymbolTool_Click;
            SymbolSaveTool.Click += SymbolSaveTool_Click;
            SymbolDeleteTool.Click += SymbolDeleteTool_Click;
            SymbolComboBox.SelectedIndexChanged += SymbolComboBox_SelectedIndexChanged;
        }

        public void Symbol_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawSymbolMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    SymbolSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    SymbolDeleteTool_Click(this, e);
                }
            }
        }

        private void SymbolTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawSymbolMod)
            {
                MainForm.MainForm_CheckedChanged();
            }
            layerService.DrawSymbolMod = !layerService.DrawSymbolMod;
            SaveDrawSymbolMod();
        }

        private void SaveDrawSymbolMod()
        {
            if (layerService.DrawSymbolMod)
            {
                SymbolTool.Checked = true;

                layerService.MouseMove += LayerServiceSymbol_MouseMove;
                layerService.MouseDown += LayerServiceSymbol_MouseDown;
                layerService.MouseUp += LayerServiceSymbol_MouseUp;
            }
            else
            {
                SymbolTool.Checked = false;

                layerService.MouseMove -= LayerServiceSymbol_MouseMove;
                layerService.MouseDown -= LayerServiceSymbol_MouseDown;
                layerService.MouseUp -= LayerServiceSymbol_MouseUp;
            }
        }

        private void LayerServiceSymbol_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("1");
            if (layerService.DrawSymbolMod && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
            }
            if (layerService.DrawSymbolMod && e.Button == MouseButtons.Right)
            {
                if (layerService.EndPoint != Point.Empty)
                {
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                }
            }
        }

        private void LayerServiceSymbol_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawSymbolMod && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left)
            {
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                AddSymbol?.Invoke(this, new Symbol(layerService.StartPoint, layerService.EndPoint, layerService.Origin));

                currentSymbol = SymbolPresenter.GetMarkedSymbol();

                layerService.Invalidate();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void LayerServiceSymbol_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawSymbolMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y); ;
                AddSymbol?.Invoke(this, new Symbol(layerService.StartPoint, layerService.EndPoint, layerService.Origin));
                currentSymbol = SymbolPresenter.GetMarkedSymbol();
                layerService.Invalidate();
            }
        }

        private void SymbolSaveTool_Click(object sender, EventArgs e)
        {
            if (layerService.DrawSymbolMod)
            {
                SaveSymbol?.Invoke(sender, e);
                SymbolComboBox.Items.Clear();
                List<Symbol> Symbols = SymbolPresenter.GetSymbols();
                foreach (Symbol Symbol in Symbols)
                {
                    SymbolComboBox.Items.Add(Symbol);
                }

                SymbolComboBox.ComboBox.DisplayMember = "name";
                SymbolPresenter.CleanMarkedSymbol();
            }
        }

        private void SymbolDeleteTool_Click(object sender, EventArgs e)
        {
            DeleteSymbol?.Invoke(sender, currentSymbol);
            currentSymbol = new Symbol();
            SymbolComboBox.Items.Clear();
            List<Symbol> Symbols = SymbolPresenter.GetSymbols();
            foreach (var Symbol in Symbols)
            {
                Console.WriteLine(Symbol.Name);
                SymbolComboBox.Items.Add(Symbol);
            }

            SymbolComboBox.ComboBox.DisplayMember = "name";
            SymbolPresenter.CleanMarkedSymbol();
            layerService.Invalidate();
        }

        private void SymbolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Symbol)comboBox.SelectedItem;
            currentSymbol = selectedObject;
            Console.WriteLine("Выбрана " + selectedObject.Name);
            Console.WriteLine(currentSymbol.Name.ToString() + " " + currentSymbol.Start + currentSymbol.End);
            AddSymbol?.Invoke(sender, currentSymbol);
            layerService.Invalidate();
        }

        public void DrawSymbol(Graphics g)
        {
            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = DashStyle.Dot;

            List<Symbol> symbols = SymbolPresenter.GetSymbols();

            foreach (var symbol in symbols)
            {
                g.TranslateTransform(symbol.Origin.X, symbol.Origin.Y);
                Point start = new Point(symbol.Start.X - (symbol.Origin.X), symbol.Start.Y - (symbol.Origin.Y));
                Point end = new Point(symbol.End.X - (symbol.Origin.X), symbol.End.Y - (symbol.Origin.Y));
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);

                Font font = new Font("Arial", 12);
                SolidBrush brush = new SolidBrush(pen.Color);
                g.DrawString("Symbol", font, brush, x, y);

                g.TranslateTransform(-symbol.Origin.X, -symbol.Origin.Y);
            }
            if (layerService.DrawSymbolMod && currentSymbol != null)
            {
                pen.Color = Color.Purple;
                currentSymbol = SymbolPresenter.GetMarkedSymbol();
                g.TranslateTransform(currentSymbol.Origin.X, currentSymbol.Origin.Y);
                Point start = new Point(currentSymbol.Start.X - (currentSymbol.Origin.X), currentSymbol.Start.Y - (currentSymbol.Origin.Y));
                Point end = new Point(currentSymbol.End.X - (currentSymbol.Origin.X), currentSymbol.End.Y - (currentSymbol.Origin.Y));
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);

                Font font = new Font("Arial", 12);
                SolidBrush brush = new SolidBrush(pen.Color);
                g.DrawString("Symbol", font, brush, x, y);

                g.TranslateTransform(-currentSymbol.Origin.X, -currentSymbol.Origin.Y);
            }
        }

        public List<Symbol> GetSymbols()
        {
            return SymbolPresenter.GetSymbols();
        }

        public LayerService LayerService1
        {
            get => default;
            set
            {
            }
        }
    }
}
