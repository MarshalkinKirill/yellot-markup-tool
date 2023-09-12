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
using System.Web.Caching;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View
{
    public class BorderView : IBorderView
    {
        private IView mainForm;
        private BorderPresenter borderPresenter;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        private Border currentBorder { get; set; } 
        public Border CurrentBorder { get => currentBorder; set => currentBorder = value; }

        private ToolStripButton borderTool;
        private ToolStripMenuItem borderSaveTool;
        private ToolStripMenuItem borderDeleteTool;

        public event EventHandler<Border> SaveBorder;
        public event EventHandler<Border> AddBorder;

        public BorderView(IView mainForm)
        {
            this.mainForm = mainForm;
            borderPresenter = new BorderPresenter(this);
            layerService = mainForm.LayerService;
            currentBorder = new Border(new Point(0, 0), new Point(0, 0), new Point(0, 0));

            borderTool = mainForm.GetBorderTool();
            borderDeleteTool = mainForm.GetDeleteTool();
            borderSaveTool = mainForm.GetSaveTool();

            borderTool.Click += BorderTool_Click;
            borderDeleteTool.Click += BorderDeleteTool_Click;
            borderSaveTool.Click += BorderSaveTool_Click;
        }

        public void Border_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawBorderMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    BorderSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    BorderDeleteTool_Click(this, e);
                }
            }
        }

        private void BorderTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawBorderMod)
            {
                mainForm.MainForm_CheckedChanged();
            }
            layerService.DrawBorderMod = !layerService.DrawBorderMod;
            SaveDrawBorderMod();
        }

        private void SaveDrawBorderMod()
        {
            if (layerService.DrawBorderMod)
            {
                borderTool.Checked = true;

                layerService.MouseDown += layerServiceBorder_MouseDown;
                layerService.MouseUp += layerServiceBorder_MouseUp;
                layerService.MouseMove += layerServiceBorder_MouseMove;
            }
            else
            {
                borderTool.Checked = false;

                layerService.MouseDown -= layerServiceBorder_MouseDown;
                layerService.MouseUp -= layerServiceBorder_MouseUp;
                layerService.MouseMove -= layerServiceBorder_MouseMove;
            }
        }

        private void layerServiceBorder_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawBorderMod && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                borderPresenter.CleanMarkedBorder();
                layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
            }
            
        }

        private void layerServiceBorder_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawBorderMod && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                AddBorder?.Invoke(this, new Border(layerService.StartPoint, layerService.EndPoint, layerService.Origin));
                currentBorder = borderPresenter.GetMarkedBorder();

                layerService.Invalidate();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawBorderMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                AddBorder?.Invoke(this, new Border(layerService.StartPoint, layerService.EndPoint, layerService.Origin));
                currentBorder = borderPresenter.GetMarkedBorder();
                Console.WriteLine(currentBorder.Start.ToString() + currentBorder.End.ToString() + currentBorder.Origin.ToString());
                layerService.Invalidate();
            }
        }

        private void BorderSaveTool_Click(object sender, EventArgs e)
        {
            if (layerService.DrawBorderMod)
            {
                SaveBorder?.Invoke(this, borderPresenter.GetMarkedBorder());
                currentBorder = borderPresenter.GetCurrentBorder();
            }
        }

        private void BorderDeleteTool_Click(object sender, EventArgs e)
        {
            currentBorder = null;

            layerService.Invalidate();
        }

        public void DrawBorder(Graphics g)
        {
            Pen pen;
            if (currentBorder != null)
            {
                pen = new Pen(Color.Purple, 3);
                pen.DashStyle = DashStyle.Dot;
                //currentBorder = borderPresenter.GetMarkedBorder();
                if (!layerService.DrawBorderMod)
                {
                    g.TranslateTransform(currentBorder.Origin.X, currentBorder.Origin.Y);
                    Point start = new Point(currentBorder.Start.X - (currentBorder.Origin.X), currentBorder.Start.Y - (currentBorder.Origin.Y));
                    Point end = new Point(currentBorder.End.X - (currentBorder.Origin.X), currentBorder.End.Y - (currentBorder.Origin.Y));
                    int width = Math.Abs(start.X - end.X);
                    int height = Math.Abs(start.Y - end.Y);
                    int x = Math.Min(start.X, end.X);
                    int y = Math.Min(start.Y, end.Y);
                    pen.DashStyle = DashStyle.Dot;
                    // Рисование прямоугольника с пунктирными границами
                    g.DrawRectangle(pen, x, y, width, height);
                    g.TranslateTransform(-currentBorder.Origin.X, -currentBorder.Origin.Y);
                }
                else
                {
                    g.TranslateTransform(currentBorder.Origin.X, currentBorder.Origin.Y);
                    pen.Color = Color.Purple;
                    Point start = new Point(currentBorder.Start.X - (currentBorder.Origin.X), currentBorder.Start.Y - (currentBorder.Origin.Y));
                    Point end = new Point(currentBorder.End.X - (currentBorder.Origin.X), currentBorder.End.Y - (currentBorder.Origin.Y));
                    int width = Math.Abs(start.X - end.X);
                    int height = Math.Abs(start.Y - end.Y);
                    int x = Math.Min(start.X, end.X);
                    int y = Math.Min(start.Y, end.Y);
                    pen.DashStyle = DashStyle.Dot;
                    // Рисование прямоугольника с пунктирными границами
                    g.DrawRectangle(pen, x, y, width, height);
                    g.TranslateTransform(-currentBorder.Origin.X, -currentBorder.Origin.Y);
                }
            }
            pen = new Pen(Color.Red, 3);
            pen.DashStyle = DashStyle.Dot;
            currentBorder = borderPresenter.GetCurrentBorder();
            if (!layerService.DrawBorderMod)
            {
                g.TranslateTransform(currentBorder.Origin.X, currentBorder.Origin.Y);
                Point start = new Point(currentBorder.Start.X - (currentBorder.Origin.X), currentBorder.Start.Y - (currentBorder.Origin.Y));
                Point end = new Point(currentBorder.End.X - (currentBorder.Origin.X), currentBorder.End.Y - (currentBorder.Origin.Y));
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                g.TranslateTransform(-currentBorder.Origin.X, -currentBorder.Origin.Y);
            }
            else
            {
                g.TranslateTransform(currentBorder.Origin.X, currentBorder.Origin.Y);
                pen.Color = Color.Purple;
                Point start = new Point(currentBorder.Start.X - (currentBorder.Origin.X), currentBorder.Start.Y - (currentBorder.Origin.Y));
                Point end = new Point(currentBorder.End.X - (currentBorder.Origin.X), currentBorder.End.Y - (currentBorder.Origin.Y));
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                g.TranslateTransform(-currentBorder.Origin.X, -currentBorder.Origin.Y);
            }
        }

        public List<Border> GetBorder()
        {
            List<Border> borderList = new List<Border>();
            borderList.Add(borderPresenter.GetMarkedBorder());
            return borderList ;
        }

        public void SetBorders(Border border)
        {
            borderPresenter.SetBorders(border);

            layerService.Invalidate();
        }
    }
}
