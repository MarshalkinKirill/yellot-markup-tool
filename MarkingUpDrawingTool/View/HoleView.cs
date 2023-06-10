using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.Presenter;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInteraface;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using CvPoint = OpenCvSharp.Point;
using Point = System.Drawing.Point;
using System.Windows.Forms;
using OpenCvSharp;
using System.Collections.Generic;
using System.Drawing;

namespace MarkingUpDrawingTool.View
{
    public class HoleView : IHoleView
    {
        private IView mainForm;
        private HolePresenter holePresenter;
        private LayerService layerService;
        private Hole currentHole { get; set; }
        public Hole CurrentHole { get { return currentHole; } set { currentHole = value; } }
        private Layer imageLayer { get; set; }
        public Layer ImageLayer { get => imageLayer; set => imageLayer = value; }

        private ToolStripButton holeTool;
        private ToolStripMenuItem holeSearchTool;
        private ToolStripMenuItem holeSaveTool;
        private ToolStripMenuItem holeDeleteTool;
        private ToolStripComboBox holeComboBox;

        public event EventHandler<Hole> AddHole;
        public event EventHandler SaveHole;
        public event EventHandler<Hole> DeleteHole;

        public HoleView(IView _mainForm)
        {
            mainForm = _mainForm;
            holePresenter = new HolePresenter(this);
            layerService = mainForm.LayerService;

            holeTool = mainForm.GetHoleTool();
            holeSearchTool = mainForm.GetHoleSearchTool();
            holeSaveTool = mainForm.GetHoleSaveTool();
            holeDeleteTool = mainForm.GetHoleDeleteTool();
            holeComboBox = mainForm.GetHoleComboBox();

            holeTool.Click += HoleTool_Click;
            holeSearchTool.Click += HoleSearchTool_Click;
            holeSaveTool.Click += HoleSaveTool_Click;
            holeDeleteTool.Click += HoleDeleteTool_Click;
            holeComboBox.SelectedIndexChanged += HoleComboBox_SelectedIndexChanged;

            holeComboBox.KeyDown += Hole_KeyDown;
        }

        public void Hole_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawHoleMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    HoleSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    HoleDeleteTool_Click(this, e);
                }
            }
        }

        private void HoleTool_Click(object sender, EventArgs e)
        {
            layerService.DrawHoleMod = !layerService.DrawHoleMod; // Инвертируем режим рисования

            if (layerService.DrawHoleMod)
            {
                holeTool.Checked = true;
                layerService.MouseMove += layerServiceHole_MouseMove;
                layerService.MouseDown += layerServiceHole_MouseDown;
                layerService.MouseUp += layerServiceHole_MouseUp;
            }
            else
            {
                holeTool.Checked = false;
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
                AddHole?.Invoke(this, new Hole(layerService.StartPoint, dist));
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

        private void HoleSearchTool_Click(object sender, EventArgs e)
        {
            List<CircleSegment> _circles = holePresenter.DetectHoles(mainForm.GetImageLayer());
            foreach (var _circle in _circles)
            {
                Point _center = new Point((int)_circle.Center.X, (int)_circle.Center.Y);

                AddHole?.Invoke(this, new Hole(_center, _circle.Radius));
                SaveHole?.Invoke(this, e);
                layerService.Invalidate();

                holeComboBox.Items.Clear();
                List<Hole> _holes = holePresenter.GetHoles();
                foreach (var _hole in _holes)
                {
                    holeComboBox.Items.Add(_hole);
                    Console.WriteLine(_hole.ToString());
                }

                holeComboBox.ComboBox.DisplayMember = "name";
                holePresenter.CleanMarkedHole();
            }
        }

        private void HoleSaveTool_Click(object sender, EventArgs e)
        {
            if (layerService.DrawHoleMod)
            {
                SaveHole?.Invoke(this, e);
                holeComboBox.Items.Clear();
                List<Hole> _holes = holePresenter.GetHoles();
                foreach (var _hole in _holes)
                {
                    holeComboBox.Items.Add(_hole);
                    Console.WriteLine(_hole.ToString());
                }

                holeComboBox.ComboBox.DisplayMember = "name";
                holePresenter.CleanMarkedHole();
            }
        }

        private void HoleDeleteTool_Click(object sender, EventArgs e)
        {
            DeleteHole?.Invoke(this, currentHole);
            currentHole = new Hole();
            holeComboBox.Items.Clear();
            List<Hole> _holes = holePresenter.GetHoles();
            foreach (var _hole in _holes)
            {
                holeComboBox.Items.Add(_hole);
                Console.WriteLine(_hole.ToString());
            }

            holeComboBox.ComboBox.DisplayMember = "name";
            holePresenter.CleanMarkedHole();
            layerService.Invalidate();
        }

        private void HoleComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        public void DrawHole(Graphics g)
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

        public List<Hole> GetHoles()
        {
            return holePresenter.GetHoles();
        }
    }
}
