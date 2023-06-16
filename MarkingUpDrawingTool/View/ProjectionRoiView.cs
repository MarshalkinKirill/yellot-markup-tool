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
    public class ProjectionRoiView : IProjectionRoiView
    {
        private IView MainForm;
        private ProjectionRoiPresenter ProjectionRoiPresenter;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        private ProjectionRoi currentProjectionRoi { get; set; }
        public ProjectionRoi CurrentProjectionRoi { get => currentProjectionRoi; set => currentProjectionRoi = value; }

        private ToolStripButton ProjectionRoiTool;
        private ToolStripMenuItem ProjectionRoiDeleteTool;
        private ToolStripMenuItem ProjectionRoiSaveTool;
        private ToolStripComboBox ProjectionRoiComboBox;

        public event EventHandler<ProjectionRoi> AddProjectionRoi;
        public event EventHandler SaveProjectionRoi;
        public event EventHandler<ProjectionRoi> DeleteProjectionRoi;

        public ProjectionRoiView(IView mainForm)
        {
            this.MainForm = mainForm;
            this.ProjectionRoiPresenter = new ProjectionRoiPresenter(this);
            this.layerService = MainForm.LayerService;

            ProjectionRoiTool = MainForm.GetProjectionRoiTool();
            ProjectionRoiSaveTool = MainForm.GetProjectionRoiSaveTool();
            ProjectionRoiDeleteTool = MainForm.GetProjectionRoiDeleteTool();
            ProjectionRoiComboBox = MainForm.GetProjectionRoiComboBox();

            ProjectionRoiTool.Click += ProjectionRoiTool_Click;
            ProjectionRoiSaveTool.Click += ProjectionRoiSaveTool_Click;
            ProjectionRoiDeleteTool.Click += ProjectionRoiDeleteTool_Click;
            ProjectionRoiComboBox.SelectedIndexChanged += ProjectionRoiComboBox_SelectedIndexChanged;
        }

        public void ProjectionRoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawProjectionRoiMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    ProjectionRoiSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    ProjectionRoiDeleteTool_Click(this, e);
                }
            }
        }

        private void ProjectionRoiTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawProjectionRoiMod)
            {
                MainForm.MainForm_CheckedChanged();
            }
            layerService.DrawProjectionRoiMod = !layerService.DrawProjectionRoiMod;
            SaveDrawProjectionRoiMod();
        }

        private void SaveDrawProjectionRoiMod()
        {
            if (layerService.DrawProjectionRoiMod)
            {
                ProjectionRoiTool.Checked = true;

                layerService.MouseMove += LayerServiceProjectionRoi_MouseMove;
                layerService.MouseDown += LayerServiceProjectionRoi_MouseDown;
                layerService.MouseUp += LayerServiceProjectionRoi_MouseUp;
            }
            else
            {
                ProjectionRoiTool.Checked = false;

                layerService.MouseMove -= LayerServiceProjectionRoi_MouseMove;
                layerService.MouseDown -= LayerServiceProjectionRoi_MouseDown;
                layerService.MouseUp -= LayerServiceProjectionRoi_MouseUp;
            }
        }

        private void LayerServiceProjectionRoi_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawProjectionRoiMod && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
            }
            if (layerService.DrawProjectionRoiMod && e.Button == MouseButtons.Right)
            {
                if (layerService.EndPoint != Point.Empty)
                {
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                }
            }
        }

        private void LayerServiceProjectionRoi_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawProjectionRoiMod && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left)
            {
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                AddProjectionRoi?.Invoke(this, new ProjectionRoi(layerService.StartPoint, layerService.EndPoint, layerService.Origin));

                currentProjectionRoi = ProjectionRoiPresenter.GetMarkedProjectionRoi();

                layerService.Invalidate();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void LayerServiceProjectionRoi_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawProjectionRoiMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y); ;
                AddProjectionRoi?.Invoke(this, new ProjectionRoi(layerService.StartPoint, layerService.EndPoint, layerService.Origin));
                currentProjectionRoi = ProjectionRoiPresenter.GetMarkedProjectionRoi();
                layerService.Invalidate();
            }
        }

        private void ProjectionRoiSaveTool_Click(object sender, EventArgs e)
        {
            if (layerService.DrawProjectionRoiMod)
            {
                SaveProjectionRoi?.Invoke(sender, e);
                ProjectionRoiComboBox.Items.Clear();
                List<ProjectionRoi> projectionRois = ProjectionRoiPresenter.GetProjectionRois();
                foreach (ProjectionRoi projectionRoi in projectionRois)
                {
                    ProjectionRoiComboBox.Items.Add(projectionRoi);
                }

                ProjectionRoiComboBox.ComboBox.DisplayMember = "name";
                ProjectionRoiPresenter.CleanMarkedProjectionRoi();
            }
        }

        private void ProjectionRoiDeleteTool_Click(object sender, EventArgs e)
        {
            DeleteProjectionRoi?.Invoke(sender, currentProjectionRoi);
            currentProjectionRoi = new ProjectionRoi();
            ProjectionRoiComboBox.Items.Clear();
            List<ProjectionRoi> projectionRois = ProjectionRoiPresenter.GetProjectionRois();
            foreach (var projectionRoi in projectionRois)
            {
                Console.WriteLine(projectionRoi.Name);
                ProjectionRoiComboBox.Items.Add(projectionRoi);
            }

            ProjectionRoiComboBox.ComboBox.DisplayMember = "name";
            ProjectionRoiPresenter.CleanMarkedProjectionRoi();
            layerService.Invalidate();
        }

        private void ProjectionRoiComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (ProjectionRoi)comboBox.SelectedItem;
            currentProjectionRoi = selectedObject;
            Console.WriteLine("Выбрана " + selectedObject.Name);
            Console.WriteLine(currentProjectionRoi.Name.ToString() + " " + currentProjectionRoi.Start + currentProjectionRoi.End);
            AddProjectionRoi?.Invoke(sender, currentProjectionRoi);
            layerService.Invalidate();
        }

        public void DrawProjectionRoi(Graphics g)
        {
            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = DashStyle.Dot;

            List<ProjectionRoi> projectionRois = ProjectionRoiPresenter.GetProjectionRois();

            foreach (var projectionRoi in projectionRois)
            {
                g.TranslateTransform(projectionRoi.Origin.X, projectionRoi.Origin.Y);
                Point start = new Point(projectionRoi.Start.X - (projectionRoi.Origin.X), projectionRoi.Start.Y - (projectionRoi.Origin.Y));
                Point end = new Point(projectionRoi.End.X - (projectionRoi.Origin.X), projectionRoi.End.Y - (projectionRoi.Origin.Y));
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                g.TranslateTransform(-projectionRoi.Origin.X, -projectionRoi.Origin.Y);
            }
            if (layerService.DrawProjectionRoiMod && currentProjectionRoi != null)
            {
                pen.Color = Color.Purple;
                currentProjectionRoi = ProjectionRoiPresenter.GetMarkedProjectionRoi();
                g.TranslateTransform(currentProjectionRoi.Origin.X, currentProjectionRoi.Origin.Y);
                Point start = new Point(currentProjectionRoi.Start.X - (currentProjectionRoi.Origin.X), currentProjectionRoi.Start.Y - (currentProjectionRoi.Origin.Y));
                Point end = new Point(currentProjectionRoi.End.X - (currentProjectionRoi.Origin.X), currentProjectionRoi.End.Y - (currentProjectionRoi.Origin.Y));
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                g.TranslateTransform(-currentProjectionRoi.Origin.X, -currentProjectionRoi.Origin.Y);
            }
        }

        public List<ProjectionRoi> GetProjectionRois()
        {
            return ProjectionRoiPresenter.GetProjectionRois();
        }
    }
}
