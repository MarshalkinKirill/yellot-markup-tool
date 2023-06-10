using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.Presenter;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInteraface;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View
{
    public class ProjectionView : IProjectionView
    {
        private IView mainForm;
        private ProjectionPresenter projectionPresenter;
        private LayerService layerService;
        private Projection currentProjection { get; set; }
        public Projection CurrentArrow { get { return currentProjection; } set { currentProjection = value; } }

        private ToolStripButton projectionTool;
        private ToolStripMenuItem projectionSaveTool;
        private ToolStripMenuItem projectionDeleteTool;
        private ToolStripComboBox projectionComboBox;

        public event EventHandler<Point> PointMarked;
        public event EventHandler SaveProjection;
        public event EventHandler<Projection> DeleteProjection;

        public ProjectionView(IView mainForm)
        {
            this.mainForm = mainForm;
            projectionPresenter = new ProjectionPresenter(this);
            layerService = mainForm.LayerService;

            projectionTool = mainForm.GetProjectionTool();
            projectionSaveTool = mainForm.GetProjectionSaveTool();
            projectionDeleteTool = mainForm.GetProjectionDeleteTool();
            projectionComboBox = mainForm.GetProjectionComboBox();

            projectionTool.Click += ProjectionTool_Click;
            projectionSaveTool.Click += ProjectionSaveTool_Click;
            projectionDeleteTool.Click += ProjectionDeleteTool_Click;
            projectionComboBox.SelectedIndexChanged += ProjectionComboBox_SelectedIndexChanged;

            projectionComboBox.KeyDown += Projection_KeyDown;
        }

        public void Projection_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawProjectionMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    ProjectionSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    ProjectionDeleteTool_Click(this, e);
                }
            }
        }

        private void ProjectionTool_Click(object sender, EventArgs e)
        {
            layerService.DrawProjectionMod = !layerService.DrawProjectionMod; // Инвертируем режим рисования

            if (layerService.DrawProjectionMod)
            {
                projectionTool.Checked = true;
                layerService.MouseMove += layerServiceProjection_MouseMove;
                layerService.MouseDown += layerServiceProjection_MouseDown;
                layerService.MouseUp += layerServiceProjection_MouseUp;
            }
            else
            {
                projectionTool.Checked = false;
                layerService.MouseMove -= layerServiceProjection_MouseMove;
                layerService.MouseDown -= layerServiceProjection_MouseDown;
                layerService.MouseUp -= layerServiceProjection_MouseUp;
            }
        }

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

                PointMarked?.Invoke(this, e.Location);
                layerService.Invalidate();
            }
        }

        private void layerServiceProjection_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawProjectionMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = e.Location;
                layerService.Invalidate();
            }
        }

        private void ProjectionSaveTool_Click(object sender, EventArgs e)
        {
            SaveProjection?.Invoke(this, e);
            projectionComboBox.Items.Clear();
            var objects = projectionPresenter.GetProjections();
            foreach (var obj in objects)
            {
                projectionComboBox.Items.Add(obj);
            }

            projectionComboBox.ComboBox.DisplayMember = "name";
            projectionPresenter.GetPoints().Clear();
        }

        private void ProjectionDeleteTool_Click(object sender, EventArgs e)
        {
            DeleteProjection?.Invoke(this, currentProjection);

            projectionComboBox.Items.Clear();
            var objects = projectionPresenter.GetProjections();
            foreach (var obj in objects)
            {
                projectionComboBox.Items.Add(obj);
            }

            projectionComboBox.ComboBox.DisplayMember = "name";
            projectionPresenter.GetPoints().Clear();
        }

        private void ProjectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Projection)comboBox.SelectedItem;

            currentProjection = selectedObject;
            Console.WriteLine("Выбрана " + selectedObject.ToString());
            Console.WriteLine("Кол-во точек " + selectedObject.Points.Count);
            projectionPresenter.SetPoints(selectedObject.Points);
            layerService.Invalidate();
        }

        public void DrawProjection(Graphics g)
        {
            if (projectionPresenter.GetPoints().Count > 1)
            {
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

        public List<Projection> GetProjections()
        {
            return projectionPresenter.GetProjections();
        }
    }
}
