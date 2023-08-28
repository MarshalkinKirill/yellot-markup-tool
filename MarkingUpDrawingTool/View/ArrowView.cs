using Emgu.CV;
using Emgu.CV.CvEnum;
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
using System.Security.AccessControl;
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

        private ToolStripMenuItem linearArrowTool;
        private ToolStripMenuItem angularArrowTool;
        private ToolStripMenuItem radialArrowTool;
        private ToolStripMenuItem diametralArrowTool;
        private ToolStripMenuItem referenceArrowTool;
        private ToolStripMenuItem coneArrowTool;
        private ToolStripMenuItem chamferArrowTool;
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

            linearArrowTool = mainForm.GetLinearArrowTool();
            angularArrowTool = mainForm.GetAngularArrowTool();
            radialArrowTool = mainForm.GetRadialArrowTool();
            diametralArrowTool = mainForm.GetDiametralArrowTool();
            referenceArrowTool = mainForm.GetReferenceArrowTool();
            coneArrowTool = mainForm.GetConeArrowTool();
            chamferArrowTool = mainForm.GetChamferArrowTool();
            arrowSaveTool = mainForm.GetSaveTool();
            arrowDeleteTool = mainForm.GetDeleteTool();
            arrowComboBox = mainForm.GetArrowComboBox();

            linearArrowTool.Click += LinearArrowTool_Click;
            angularArrowTool.Click += AngularArrowTool_Click;
            radialArrowTool.Click += RadialArrowTool_Click;
            /*diametralArrowTool.Click += DiametralArrowTool_Click;
            referenceArrowTool.Click += ReferenceArrowTool_Click;
            coneArrowTool.Click += ConeArrowTool_Click;
            chamferArrowTool.Click += ChamferArrowTool_Click;*/
            arrowSaveTool.Click += ArrowSaveTool_Click;
            arrowDeleteTool.Click += ArrowDeleteTool_Click;
            arrowComboBox.SelectedIndexChanged += ArrowComboBox_SelectedIndexChanged;
            
        }

        public void Arrow_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawLinearArrowMod || layerService.DrawAngularArrowMod || layerService.DrawRadialArrowMod || layerService.DrawDiametralArrowMod || layerService.DrawReferenceArrowMod || layerService.DrawConeArrowMod || layerService.DrawChamferArrowMod)
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
        
        //Linear Arrow functionaliry
        private void LinearArrowTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawLinearArrowMod)
            {
                mainForm.MainForm_CheckedChanged();
                UnSubAll();
            }
            layerService.DrawLinearArrowMod = !layerService.DrawLinearArrowMod;
            if (layerService.DrawLinearArrowMod)
            {
                linearArrowTool.Checked = true;

                layerService.MouseDown += layerServiceLinearArrow_MouseDown;
                layerService.MouseUp += layerServiceLinearArrow_MouseUp;
                layerService.MouseMove += layerServiceLinearArrow_MouseMove;
            }
            else
            {
                linearArrowTool.Checked = false;

                layerService.MouseDown -= layerServiceLinearArrow_MouseDown;
                layerService.MouseUp -= layerServiceLinearArrow_MouseUp;
                layerService.MouseMove -= layerServiceLinearArrow_MouseMove;
            }
        }

        private void layerServiceLinearArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawLinearArrowMod && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                arrowPresenter.CleanMarkedArrow();
                if (layerService.StartPoint == Point.Empty)
                {
                    layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }
                else
                {
                    layerService.StartNote = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }

            }
            if (layerService.DrawLinearArrowMod && e.Button == MouseButtons.Middle)
            {
                if (layerService.EndPoint != Point.Empty)
                {
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartPoint, layerService.EndPoint, layerService.Origin, ArrowType.Linear));
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                    layerService.StartNote = Point.Empty;
                    layerService.EndNote = Point.Empty;
                    currentArrow = arrowPresenter.GetMarkedArrow();
                    layerService.Invalidate();
                }
            }
        }

        private void layerServiceLinearArrow_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawLinearArrowMod && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                if (layerService.StartPoint != Point.Empty && layerService.StartNote == Point.Empty)
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }
                else
                {
                    layerService.EndNote = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote, layerService.Origin, ArrowType.Linear));
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

        private void layerServiceLinearArrow_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawLinearArrowMod && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                if (layerService.StartPoint != Point.Empty && layerService.StartNote == Point.Empty)
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y); ;
                }
                else
                {
                    layerService.EndNote = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y); ;
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote, layerService.Origin, ArrowType.Linear));
                    currentArrow = arrowPresenter.GetMarkedArrow();
                }
                layerService.Invalidate();
            }
        }

        //Angular Arrow functionality
        private void AngularArrowTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawAngularArrowMod)
            {
                mainForm.MainForm_CheckedChanged();
                UnSubAll();
            }
            layerService.DrawAngularArrowMod = !layerService.DrawAngularArrowMod;
            if (layerService.DrawAngularArrowMod)
            {
                angularArrowTool.Checked = true;

                layerService.MouseDown += layerServiceAngularArrow_MouseDown;
                layerService.MouseUp += layerServiceAngularArrow_MouseUp;
                layerService.MouseMove += layerServiceAngularArrow_MouseMove;
            }
            else
            {
                angularArrowTool.Checked = false;

                layerService.MouseDown -= layerServiceAngularArrow_MouseDown;
                layerService.MouseUp -= layerServiceAngularArrow_MouseUp;
                layerService.MouseMove -= layerServiceAngularArrow_MouseMove;
            }
        }

        private void layerServiceAngularArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawAngularArrowMod && e.Button == MouseButtons.Left)
            {
                arrowPresenter.CleanMarkedArrow();
                if (layerService.CenterPoint == Point.Empty)
                {
                    layerService.CenterPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }
                else
                {
                    layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }

            }
            if (layerService.DrawAngularArrowMod && e.Button == MouseButtons.Middle)
            {
                if (layerService.EndPoint != Point.Empty)
                {
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                    layerService.CenterPoint = Point.Empty;
                    currentArrow = arrowPresenter.GetMarkedArrow();
                    layerService.Invalidate();
                }
            }
        }

        private void layerServiceAngularArrow_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawAngularArrowMod && e.Button == MouseButtons.Left)
            {
                if (layerService.CenterPoint != Point.Empty && layerService.StartPoint == Point.Empty)
                {
                    layerService.CenterPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }
                else
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartPoint, layerService.EndPoint, layerService.Origin, ArrowType.Angular, layerService.CenterPoint));
                }
                currentArrow = arrowPresenter.GetMarkedArrow();
                if (layerService.EndPoint != Point.Empty)
                {
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                    layerService.CenterPoint = Point.Empty;
                }
                layerService.Invalidate();
            }
        }

        private void layerServiceAngularArrow_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawAngularArrowMod && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                if (layerService.CenterPoint != Point.Empty && layerService.StartPoint == Point.Empty)
                {
                    layerService.CenterPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }
                else
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartPoint, layerService.EndPoint, layerService.Origin, ArrowType.Angular, layerService.CenterPoint));
                }
                currentArrow = arrowPresenter.GetMarkedArrow();
                layerService.Invalidate();
            }
        }

        //Radial Arrow functionality
        private void RadialArrowTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawRadialArrowMod)
            {
                mainForm.MainForm_CheckedChanged();
                UnSubAll();
            }
            layerService.DrawRadialArrowMod = !layerService.DrawRadialArrowMod;
            if (layerService.DrawRadialArrowMod)
            {
                radialArrowTool.Checked = true;

                layerService.MouseDown += layerServiceRadialArrow_MouseDown;
                layerService.MouseUp += layerServiceRadialArrow_MouseUp;
                layerService.MouseMove += layerServiceRadialArrow_MouseMove;
            }
            else
            {
                radialArrowTool.Checked = false;

                layerService.MouseDown -= layerServiceRadialArrow_MouseDown;
                layerService.MouseUp -= layerServiceRadialArrow_MouseUp;
                layerService.MouseMove -= layerServiceRadialArrow_MouseMove;
            }
        }

        private void layerServiceRadialArrow_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawRadialArrowMod && e.Button == MouseButtons.Left)
            {
                arrowPresenter.CleanMarkedArrow();
                if (layerService.StartPoint == Point.Empty)
                {
                    layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }
                else
                {
                    layerService.StartNote = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                }

            }
            if (layerService.DrawRadialArrowMod && e.Button == MouseButtons.Middle)
            {
                if (layerService.EndPoint != Point.Empty)
                {
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartPoint, layerService.EndPoint, layerService.Origin, ArrowType.Radial));
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                    layerService.StartNote = Point.Empty;
                    layerService.EndNote = Point.Empty;
                    currentArrow = arrowPresenter.GetMarkedArrow();
                    layerService.Invalidate();
                }
            }
        }

        private void layerServiceRadialArrow_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawRadialArrowMod && e.Button == MouseButtons.Left)
            {
                if (layerService.StartPoint != Point.Empty && layerService.StartNote == Point.Empty)
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote, layerService.Origin, ArrowType.Radial));
                }
                else
                {
                    layerService.EndNote = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote, layerService.Origin, ArrowType.Radial));
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

        private void layerServiceRadialArrow_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawRadialArrowMod && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                if (layerService.StartPoint != Point.Empty && layerService.StartNote == Point.Empty)
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote, layerService.Origin, ArrowType.Radial));
                }
                else
                {
                    layerService.EndNote = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y); 
                    AddArrow?.Invoke(this, new Arrow(layerService.StartPoint, layerService.EndPoint, layerService.StartNote, layerService.EndNote, layerService.Origin, ArrowType.Radial));
                    currentArrow = arrowPresenter.GetMarkedArrow();
                }
                layerService.Invalidate();
            }
        }

        private void ArrowSaveTool_Click(object sender, EventArgs e)
        {
            if (layerService.DrawLinearArrowMod || layerService.DrawAngularArrowMod || layerService.DrawRadialArrowMod || layerService.DrawDiametralArrowMod || layerService.DrawReferenceArrowMod || layerService.DrawConeArrowMod || layerService.DrawChamferArrowMod)
            {
                currentArrow = arrowPresenter.GetMarkedArrow();
                SaveArrow?.Invoke(sender,e);
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
            Console.WriteLine(currentArrow.Name.ToString() + " " + currentArrow.Start + currentArrow.End + currentArrow.Center);
            AddArrow?.Invoke(sender, currentArrow);
            layerService.Invalidate();
        }

        public void DrawArrow(Graphics g)
        {
            List<Arrow> arrows = arrowPresenter.GetArrows();

            Pen pen = new Pen(Color.Red, 3);

            foreach (Arrow _arrow in arrows)
            {
                if (_arrow.Type == ArrowType.Linear)
                {
                    DrawLinearArrow(g, _arrow, pen);
                }
                if (_arrow.Type == ArrowType.Angular)
                {
                    DrawAngularArrow(g, _arrow, pen);
                }
                if (_arrow.Type == ArrowType.Radial)
                {
                    DrawRadialArrow(g, _arrow, pen);
                }
            }
            pen.Color = Color.Purple;
            currentArrow = arrowPresenter.GetMarkedArrow();
            Console.WriteLine(currentArrow.Type);
            if (layerService.DrawRadialArrowMod && currentArrow != null && currentArrow.Type == ArrowType.Radial)
            {
                DrawRadialArrow(g, currentArrow, pen);
            }
            if (layerService.DrawLinearArrowMod && currentArrow != null && currentArrow.Type == ArrowType.Linear)
            {
                if (currentArrow.Type == ArrowType.Linear)
                {
                    DrawLinearArrow(g, currentArrow, pen);
                } 
            }
            if (layerService.DrawAngularArrowMod && currentArrow != null && currentArrow.Type == ArrowType.Angular)
            {
                DrawAngularArrow(g, currentArrow, pen);
            }
        }
        
        private void DrawLinearArrow(Graphics g, Arrow arrow, Pen pen)
        {
            g.TranslateTransform(arrow.Origin.X, arrow.Origin.Y);
            Point start = new Point(), end = new Point(), noteStart = new Point(), noteEnd = new Point();

            if (arrow.NoteEnd == Point.Empty)
            {
                if (layerService.DrawLinearArrowMod)
                {
                    start = new Point(layerService.StartPoint.X - (arrow.Origin.X), layerService.StartPoint.Y - (arrow.Origin.Y));
                    end = new Point(layerService.EndPoint.X - (arrow.Origin.X), layerService.EndPoint.Y - (arrow.Origin.Y));
                    noteStart = new Point(layerService.StartNote.X - (arrow.Origin.X), layerService.StartNote.Y - (arrow.Origin.Y));
                    noteEnd = new Point(layerService.EndNote.X - (arrow.Origin.X), layerService.EndNote.Y - (arrow.Origin.Y));
                }
            }
            else
            {
                start = new Point(arrow.Start.X - (arrow.Origin.X), arrow.Start.Y - (arrow.Origin.Y));
                end = new Point(arrow.End.X - (arrow.Origin.X), arrow.End.Y - (arrow.Origin.Y));
                noteStart = new Point(arrow.NoteStart.X - (arrow.Origin.X), arrow.NoteStart.Y - (arrow.Origin.Y));
                noteEnd = new Point(arrow.NoteEnd.X - (arrow.Origin.X), arrow.NoteEnd.Y - (arrow.Origin.Y));
            }

            GraphicsPath path1 = new GraphicsPath();
            //if (start == noteStart && end == noteEnd)
            //{
            path1 = GetAnglePath(end, start);
            //}
            GraphicsPath path2 = GetAnglePath(start, end);

            g.DrawPath(pen, path1);
            g.DrawPath(pen, path2);
            g.DrawLine(pen, noteStart, noteEnd);

            g.TranslateTransform(-arrow.Origin.X, -arrow.Origin.Y);
        }

        private void DrawAngularArrow(Graphics g, Arrow arrow, Pen pen)
        {
            Point start = new Point(arrow.Start.X - (arrow.Origin.X), arrow.Start.Y - (arrow.Origin.Y));
            Point end = new Point(arrow.End.X - (arrow.Origin.X), arrow.End.Y - (arrow.Origin.Y));
            Point center = new Point(arrow.Center.X - (arrow.Origin.X), arrow.Center.Y - (arrow.Origin.Y));
            Console.WriteLine(start.ToString());
            Console.WriteLine(end.ToString());
            Console.WriteLine(center.ToString());
            if (arrow.Start != Point.Empty && arrow.End != Point.Empty)
            {
                g.TranslateTransform(arrow.Origin.X, arrow.Origin.Y);
                // Вычисление радиуса окружности
                float radius = (float)Math.Sqrt(Math.Pow(start.X - center.X, 2) + Math.Pow(start.Y - center.Y, 2));

                // Вычисление углов начала и конца дуги
                float startAngle = (float)(Math.Atan2(start.Y - center.Y, start.X - center.X) * 180 / Math.PI);
                float endAngle = (float)(Math.Atan2(end.Y - center.Y, end.X - center.X) * 180 / Math.PI);

                // Отрисовка дуги окружности
                //g.DrawArc(pen, center.X - radius, center.Y - radius, radius * 2, radius * 2, Math.Abs(startAngle), Math.Abs(endAngle - startAngle));

                Point[] points = GetPointsOnArc(center, radius, startAngle, endAngle, 1000);
                g.DrawLines(pen, points);
                GraphicsPath path1 = GetAnglePath(points[70],points[0]);
                
                GraphicsPath path2 = GetAnglePath(points[points.Count() - 70], points.Last());

                g.DrawPath(pen, path1);
                g.DrawPath(pen, path2);

                g.TranslateTransform(-arrow.Origin.X, -arrow.Origin.Y);
            }
        }

        private void DrawRadialArrow(Graphics g, Arrow arrow, Pen pen)
        {
            g.TranslateTransform(arrow.Origin.X, arrow.Origin.Y);
            Point start = new Point(), end = new Point(), noteStart = new Point(), noteEnd = new Point();

            if (arrow.NoteEnd == Point.Empty)
            {
                if (layerService.DrawRadialArrowMod)
                {
                    start = new Point(layerService.StartPoint.X - (arrow.Origin.X), layerService.StartPoint.Y - (arrow.Origin.Y));
                    end = new Point(layerService.EndPoint.X - (arrow.Origin.X), layerService.EndPoint.Y - (arrow.Origin.Y));
                    noteStart = new Point(layerService.StartNote.X - (arrow.Origin.X), layerService.StartNote.Y - (arrow.Origin.Y));
                    noteEnd = new Point(layerService.EndNote.X - (arrow.Origin.X), layerService.EndNote.Y - (arrow.Origin.Y));
                    Console.WriteLine(start.ToString() + end.ToString());
                }
            }
            else
            {
                start = new Point(arrow.Start.X - (arrow.Origin.X), arrow.Start.Y - (arrow.Origin.Y));
                end = new Point(arrow.End.X - (arrow.Origin.X), arrow.End.Y - (arrow.Origin.Y));
                noteStart = new Point(arrow.NoteStart.X - (arrow.Origin.X), arrow.NoteStart.Y - (arrow.Origin.Y));
                noteEnd = new Point(arrow.NoteEnd.X - (arrow.Origin.X), arrow.NoteEnd.Y - (arrow.Origin.Y));
            }

            GraphicsPath path1 = new GraphicsPath();
            
            //path1 = GetAnglePath(end, start);
            
            GraphicsPath path2 = GetAnglePath(start, end);

            g.DrawPath(pen, path1);
            g.DrawPath(pen, path2);
            g.DrawLine(pen, noteStart, noteEnd);

            g.TranslateTransform(-arrow.Origin.X, -arrow.Origin.Y);
        }

        public Point[] GetPointsOnArc(Point center, float radius, float startAngle, float endAngle, int numPoints)
        {
            Point[] points = new Point[numPoints];

            float angleIncrement = (endAngle - startAngle) / (numPoints - 1);

            for (int i = 0; i < numPoints; i++)
            {
                float currentAngle = startAngle + i * angleIncrement;
                int x = center.X + (int)(radius * Math.Cos(currentAngle * Math.PI / 180));
                int y = center.Y + (int)(radius * Math.Sin(currentAngle * Math.PI / 180));
                points[i] = new Point(x, y);
            }

            return points;
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
        
        private void UnSubAll()
        {
            layerService.MouseDown -= layerServiceLinearArrow_MouseDown;
            layerService.MouseUp -= layerServiceLinearArrow_MouseUp;
            layerService.MouseMove -= layerServiceLinearArrow_MouseMove;

            layerService.MouseDown -= layerServiceAngularArrow_MouseDown;
            layerService.MouseUp -= layerServiceAngularArrow_MouseUp;
            layerService.MouseMove -= layerServiceAngularArrow_MouseMove;

            layerService.MouseDown -= layerServiceRadialArrow_MouseDown;
            layerService.MouseUp -= layerServiceRadialArrow_MouseUp;
            layerService.MouseMove -= layerServiceRadialArrow_MouseMove;
        }
        public List<Arrow> GetArrows()
        {
            return arrowPresenter.GetArrows();
        }

        public void SetArrows(List<Arrow> arrows)
        {
            arrowPresenter.SetArrows(arrows);

            foreach (var arrow in arrows)
            {
                Console.WriteLine(arrow.Name);
                arrowComboBox.Items.Add(arrow);
            }

            arrowComboBox.ComboBox.DisplayMember = "name";
            arrowPresenter.CleanMarkedArrow();
            layerService.Invalidate();
        }
    }
}
