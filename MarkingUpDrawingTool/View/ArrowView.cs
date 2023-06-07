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
    public class ArrowView : IArrowView
    {
        private IView mainForm;
        private ArrowPresenter arrowPresenter;
        private LayerService layerService;
        private Arrow currentArrow { get; set; }
        public Arrow CurrentArrow { get { return currentArrow; } set { currentArrow = value; } }

        private ToolStripButton arrowTool;
        private ToolStripMenuItem arrowSaveTool;
        private ToolStripMenuItem arrowDeleteTool;
        private ToolStripComboBox arrowComboBox;

        public event EventHandler<Arrow> AddArrow;
        public event EventHandler SaveArrow;
        public event EventHandler<Arrow> DeleteArrow;

        public ArrowView(IView _mainForm)
        {
            mainForm = _mainForm;
            arrowPresenter = new ArrowPresenter(this);
            layerService = mainForm.LayerService;

            arrowTool = mainForm.GetArrowTool();
            arrowSaveTool = mainForm.GetArrowSaveTool();
            arrowDeleteTool = mainForm.GetArrowDeleteTool();
            arrowComboBox = mainForm.GetArrowComboBox();

            arrowTool.Click += ArrowTool_Click;
            arrowSaveTool.Click += ArrowSaveTool_Click;
            arrowDeleteTool.Click += ArrowDeleteTool_Click;
            arrowComboBox.SelectedIndexChanged += ArrowComboBox_SelectedIndexChanged;
            
            arrowComboBox.KeyDown += Arrow_KeyDown;
        }

        public void Arrow_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawArrowMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    ArrowSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    ArrowDeleteTool_Click(this, e);
                }
            }
        }
        private void ArrowTool_Click(object sender, EventArgs e)
        {
            layerService.DrawArrowMod = !layerService.DrawArrowMod;
            SaveDrawArrowMod();
        }

        private void SaveDrawArrowMod()
        {
            if (layerService.DrawArrowMod)
            {
                arrowTool.Checked = true;

                layerService.MouseDown += layerServiceArrow_MouseDown;
                layerService.MouseUp += layerServiceArrow_MouseUp;
                layerService.MouseMove += layerServiceArrow_MouseMove;
            }
            else
            {
                arrowTool.Checked = false;

                layerService.MouseDown -= layerServiceArrow_MouseDown;
                layerService.MouseUp -= layerServiceArrow_MouseUp;
                layerService.MouseMove -= layerServiceArrow_MouseMove;
            }
        }

        private void layerServiceArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawArrowMod && e.Button == MouseButtons.Left)
            {
                arrowPresenter.CleanMarkedArrow();
                if (layerService.StartPoint == Point.Empty)
                {
                    layerService.StartPoint = e.Location;
                }
                else
                {
                    layerService.StartNote = e.Location;
                }

            }
            if (layerService.DrawArrowMod && e.Button == MouseButtons.Right)
            {
                if(layerService.EndPoint != Point.Empty)
                {
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartPoint, layerService.EndPoint));
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                    layerService.StartNote = Point.Empty;
                    layerService.EndNote = Point.Empty;
                    currentArrow = arrowPresenter.GetMarkedArrow();
                    layerService.Invalidate();
                }
            }
        }

        private void layerServiceArrow_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawArrowMod && e.Button == MouseButtons.Left)
            {
                if (layerService.StartPoint != Point.Empty && layerService.StartNote == Point.Empty)
                {
                    layerService.EndPoint = e.Location;
                }
                else
                {
                    layerService.EndNote = e.Location;
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote));
                }
                currentArrow = arrowPresenter.GetMarkedArrow();
                if (layerService.EndNote != Point.Empty)
                {
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                    layerService.StartNote = Point.Empty;
                    layerService.EndNote = Point.Empty;
                }
                layerService.Invalidate();
            }
        }

        private void layerServiceArrow_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawArrowMod && e.Button == MouseButtons.Left)
            {
                if (layerService.StartPoint != Point.Empty && layerService.StartNote == Point.Empty)
                {
                    layerService.EndPoint = e.Location;
                }
                else
                {
                    layerService.EndNote = e.Location;
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote));
                    currentArrow = arrowPresenter.GetMarkedArrow();
                }
                layerService.Invalidate();
            }
        }

        private void ArrowSaveTool_Click(object sender, EventArgs e)
        {
            if (layerService.DrawArrowMod)
            {
                SaveArrow?.Invoke(sender, e);
                arrowComboBox.Items.Clear();
                List<Arrow> arrows = arrowPresenter.GetArrows();
                foreach (Arrow arrow in arrows)
                {
                    arrowComboBox.Items.Add(arrow);
                }

                arrowComboBox.ComboBox.DisplayMember = "name";
                arrowPresenter.CleanMarkedArrow();
            }
        }

        private void ArrowDeleteTool_Click(object sender, EventArgs e)
        {
            Console.WriteLine(currentArrow.Name.ToString() + " " + currentArrow.Start + currentArrow.End + currentArrow.NoteStart + currentArrow.NoteEnd);

            DeleteArrow?.Invoke(sender, currentArrow);
            currentArrow = new Arrow();
            arrowComboBox.Items.Clear();
            List<Arrow> arrows = arrowPresenter.GetArrows();
            foreach (var arrow in arrows)
            {
                Console.WriteLine(arrow.Name);
                arrowComboBox.Items.Add(arrow);
            }

            arrowComboBox.ComboBox.DisplayMember = "name";
            arrowPresenter.CleanMarkedArrow();
            layerService.Invalidate();
        }

        private void ArrowComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Arrow)comboBox.SelectedItem;
            currentArrow = selectedObject;
            Console.WriteLine("Выбрана " + selectedObject.Name);
            Console.WriteLine(currentArrow.Name.ToString() + " " + currentArrow.Start + currentArrow.End + currentArrow.NoteStart + currentArrow.NoteEnd);
            AddArrow?.Invoke(sender, currentArrow);
            layerService.Invalidate();
        }

        public void DrawArrow(Graphics g)
        {
            List<Arrow> arrows = arrowPresenter.GetArrows();

            Pen pen = new Pen(Color.Red, 3);

            foreach (Arrow arrow in arrows)
            {
                Point start = arrow.Start;
                Point end = arrow.End;
                Point noteStart = arrow.NoteStart;
                Point noteEnd = arrow.NoteEnd;
                GraphicsPath path1 = new GraphicsPath();
                if (start ==  noteStart && end == noteEnd)
                {
                    path1 = GetAnglePath(end, start);
                }
                GraphicsPath path2 = GetAnglePath(start, end);

                g.DrawPath(pen, path1);
                g.DrawPath(pen, path2);
                g.DrawLine(pen, noteStart, noteEnd);
            }
            if (layerService.DrawArrowMod)
            {
                pen.Color = Color.Purple;
                Arrow _currentArrow = arrowPresenter.GetMarkedArrow();
                Point start, end, noteStart, noteEnd;
                if (_currentArrow.NoteEnd == Point.Empty)
                {
                    start = layerService.StartPoint;
                    end = layerService.EndPoint;
                    noteStart = layerService.StartNote;
                    noteEnd = layerService.EndNote;
                }
                else
                {
                    start = _currentArrow.Start;
                    end = _currentArrow.End;
                    noteStart = _currentArrow.NoteStart;
                    noteEnd = _currentArrow.NoteEnd;
                }

                GraphicsPath path1 = new GraphicsPath();
                if (start == noteStart && end == noteEnd)
                {
                    path1 = GetAnglePath(end, start);
                }
                GraphicsPath path2 = GetAnglePath(start, end);

                g.DrawPath(pen, path1);
                g.DrawPath(pen, path2);
                g.DrawLine(pen, noteStart, noteEnd);
            }
        }
        private GraphicsPath GetAnglePath(Point start, Point end)
        {
            // Вычисляем угол между начальной и конечной точкой
            float angle = (float)(Math.Atan2(end.Y - start.Y, end.X - start.X) * 180 / Math.PI);

            GraphicsPath path = new GraphicsPath();
            // Добавляем линию от начальной до конечной точки
            path.AddLine(start, end);

            // Создаем конечные точки стрелки
            float arrowWidth = 5; // Ширина стрелки
            float arrowHeight = 17; // Высота стрелки

            // Вычисляем координаты конечных точек стрелки
            float arrowX = end.X - arrowHeight * (float)Math.Cos(angle * Math.PI / 180);
            float arrowY = end.Y - arrowHeight * (float)Math.Sin(angle * Math.PI / 180);
            PointF arrowPoint1 = new PointF(arrowX + arrowWidth * (float)Math.Sin(angle * Math.PI / 180),
                                            arrowY - arrowWidth * (float)Math.Cos(angle * Math.PI / 180));
            PointF arrowPoint2 = new PointF(arrowX - arrowWidth * (float)Math.Sin(angle * Math.PI / 180),
                                            arrowY + arrowWidth * (float)Math.Cos(angle * Math.PI / 180));

            // Добавляем стрелку в путь
            path.AddPolygon(new PointF[] { end, arrowPoint1, arrowPoint2 });
            return path;
        }
    }
}
