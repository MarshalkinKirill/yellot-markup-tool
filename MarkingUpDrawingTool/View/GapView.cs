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
    public class GapView : IGapView
    {
        private IView mainForm;
        private GapPresenter gapPresenter;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        private Gap currentGap { get; set; }
        public Gap CurrentGap { get => currentGap ; set => currentGap = value; }

        private ToolStripButton gapTool;
        private ToolStripMenuItem gapDeleteTool;
        private ToolStripMenuItem gapSaveTool;
        private ToolStripComboBox gapComboBox;

        public event EventHandler<Gap> AddGap;
        public event EventHandler SaveGap;
        public event EventHandler<Gap> DeleteGap;

        public GapView(IView mainForm)
        {
            this.mainForm = mainForm;
            gapPresenter = new GapPresenter(this);
            layerService = mainForm.LayerService;

            gapTool = mainForm.GetGapTool();
            gapSaveTool = mainForm.GetGapSaveTool();
            gapDeleteTool = mainForm.GetGapDeleteTool();
            gapComboBox = mainForm.GetGapComboBox();

            gapTool.Click += GapTool_Click;
            gapSaveTool.Click += GapSaveTool_Click;
            gapDeleteTool.Click += GapDeleteTool_Click;
            gapComboBox.SelectedIndexChanged += GapComboBox_SelectedIndexChanged;
        }

        public void Gap_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawGapMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    GapSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    GapDeleteTool_Click(this, e);
                }
            }
        }
        private void GapTool_Click(object sender, EventArgs e)
        {
            layerService.DrawGapMod = !layerService.DrawGapMod;
            SaveDrawGapMod();
        }

        private void SaveDrawGapMod()
        {
            if (layerService.DrawGapMod)
            {
                gapTool.Checked = true;

                layerService.MouseDown += layerServiceGap_MouseDown;
                layerService.MouseUp += layerServiceGap_MouseUp;
                layerService.MouseMove += layerServiceGap_MouseMove;
            }
            else
            {
                gapTool.Checked = false;

                layerService.MouseDown -= layerServiceGap_MouseDown;
                layerService.MouseUp -= layerServiceGap_MouseUp;
                layerService.MouseMove -= layerServiceGap_MouseMove;
            }
        }

        private void layerServiceGap_MouseDown(object sender, MouseEventArgs e)
        {
            if (layerService.DrawGapMod && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = e.Location;
            }
            if (layerService.DrawGapMod && e.Button == MouseButtons.Right)
            {
                if (layerService.EndPoint != Point.Empty)
                {
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                }
            }
        }

        private void layerServiceGap_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawGapMod && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left)
            {
                layerService.EndPoint = e.Location;
                AddGap?.Invoke(this, new Gap(layerService.StartPoint, layerService.EndPoint));

                currentGap = gapPresenter.GetMarkedGap();

                layerService.Invalidate();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceGap_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawGapMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = e.Location;
                AddGap?.Invoke(this, new Gap(layerService.StartPoint, layerService.EndPoint));
                currentGap = gapPresenter.GetMarkedGap();
                layerService.Invalidate();
            }
        }

        private void GapSaveTool_Click(object sender, EventArgs e)
        {
            if (layerService.DrawGapMod)
            {
                SaveGap?.Invoke(sender, e);
                gapComboBox.Items.Clear();
                List<Gap> gaps = gapPresenter.GetGaps();
                foreach (Gap gap in gaps)
                {
                    gapComboBox.Items.Add(gap);
                }

                gapComboBox.ComboBox.DisplayMember = "name";
                gapPresenter.CleanMarkedGap();
            }
        }

        private void GapDeleteTool_Click(object sender, EventArgs e)
        {
            Console.WriteLine(currentGap.Name.ToString() + " " + currentGap.Start + currentGap.End);

            DeleteGap?.Invoke(sender, currentGap);
            currentGap = new Gap();
            gapComboBox.Items.Clear();
            List<Gap> gaps = gapPresenter.GetGaps();
            foreach (var gap in gaps)
            {
                Console.WriteLine(gap.Name);
                gapComboBox.Items.Add(gap);
            }

            gapComboBox.ComboBox.DisplayMember = "name";
            gapPresenter.CleanMarkedGap();
            layerService.Invalidate();
        }

        private void GapComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Gap)comboBox.SelectedItem;
            currentGap = selectedObject;
            Console.WriteLine("Выбрана " + selectedObject.Name);
            Console.WriteLine(currentGap.Name.ToString() + " " + currentGap.Start + currentGap.End);
            AddGap?.Invoke(sender, currentGap);
            layerService.Invalidate();
        }

        public void DrawGap(Graphics g)
        {
            List<Gap> gaps = gapPresenter.GetGaps();

            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = DashStyle.Dot;

            foreach (var gap in gaps)
            {
                Point start = gap.Start;
                Point end = gap.End;
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                
                
                // Настраиваем стиль пера для пунктирной линии
                pen = new Pen(Color.Red);
                //pen.DashStyle = DashStyle.Dash;


                /////////////////////////////////////////////
                /*int deltaX = end.X - start.X;
                int deltaY = end.Y - start.Y;

                // Находим середину между началом и концом молнии
                Point midPoint = new Point(start.X + deltaX / 2, start.Y + deltaY / 2);

                // Вычисляем координаты точек изгиба
                Point bend1 = new Point(midPoint.X - deltaY / 4, midPoint.Y + deltaX / 4);
                Point bend2 = new Point(midPoint.X + deltaY / 4, midPoint.Y - deltaX / 4);

                // Рисуем линии молнии

                g.DrawLine(pen, start, bend1);
                g.DrawLine(pen, bend1, midPoint);
                g.DrawLine(pen, midPoint, bend2);
                g.DrawLine(pen, bend2, end);*/
                // Рисуем пунктирный прямоугольник
                //g.DrawRectangle(pen, rect);

                // Рисуем молнию внутри прямоугольника
                //g.DrawPath(pen, lightningPath);
                //g.DrawLines(pen, lightningPoints);
                ///////////////////////////////////////////////////////
                int deltaX = end.X - start.X;
                int deltaY = end.Y - start.Y;

                // Находим середину между началом и концом молнии
                Point midPoint = new Point(start.X + deltaX / 2, start.Y + deltaY / 2);

                // Определяем длину и ширину прямоугольника
                width = Math.Abs(deltaX);
                height = Math.Abs(deltaY);

                // Определяем границы прямоугольника
                int minX = Math.Min(start.X, end.X);
                int minY = Math.Min(start.Y, end.Y);
                int maxX = minX + width;
                int maxY = minY + height;

                // Ограничиваем точки изгиба в пределах прямоугольника
                Point bend1 = new Point(
                    Math.Max(minX, Math.Min(midPoint.X - height / 4, maxX)),
                    Math.Max(minY, Math.Min(midPoint.Y + width / 4, maxY))
                );
                Point bend2 = new Point(
                    Math.Max(minX, Math.Min(midPoint.X + height / 4, maxX)),
                    Math.Max(minY, Math.Min(midPoint.Y - width / 4, maxY))
                );

                // Рисуем линии молнии
                pen = new Pen(Color.Red, 2);
                pen.DashStyle = DashStyle.Dot;
                g.DrawLine(pen, start, bend1);
                g.DrawLine(pen, bend1, midPoint);
                g.DrawLine(pen, midPoint, bend2);
                g.DrawLine(pen, bend2, end);
            }
            if (layerService.DrawGapMod)
            {
                pen.Color = Color.Purple;
                Gap gap = currentGap;

                Point start = gap.Start;
                Point end = gap.End;
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);

                pen = new Pen(Color.Purple, 2);

                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                
                // Настраиваем стиль пера для пунктирной линии

                int deltaX = end.X - start.X;
                int deltaY = end.Y - start.Y;

                // Находим середину между началом и концом молнии
                Point midPoint = new Point(start.X + deltaX / 2, start.Y + deltaY / 2);

                // Определяем длину и ширину прямоугольника
                width = Math.Abs(deltaX);
                height = Math.Abs(deltaY);

                // Определяем границы прямоугольника
                int minX = Math.Min(start.X, end.X);
                int minY = Math.Min(start.Y, end.Y);
                int maxX = minX + width;
                int maxY = minY + height;

                // Ограничиваем точки изгиба в пределах прямоугольника
                Point bend1 = new Point(
                    Math.Max(minX, Math.Min(midPoint.X - height / 4, maxX)),
                    Math.Max(minY, Math.Min(midPoint.Y + width / 4, maxY))
                );
                Point bend2 = new Point(
                    Math.Max(minX, Math.Min(midPoint.X + height / 4, maxX)),
                    Math.Max(minY, Math.Min(midPoint.Y - width / 4, maxY))
                );

                // Рисуем линии молнии
                pen = new Pen(Color.Purple, 2);
                //pen.DashStyle = DashStyle.Dot;
                g.DrawLine(pen, start, bend1);
                g.DrawLine(pen, bend1, midPoint);
                g.DrawLine(pen, midPoint, bend2);
                g.DrawLine(pen, bend2, end);
            }
        }
    }
}
