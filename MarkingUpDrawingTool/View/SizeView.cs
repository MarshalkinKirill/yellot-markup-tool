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
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DrSize = System.Drawing.Size;
using Size = MarkingUpDrawingTool.Model.Size;

namespace MarkingUpDrawingTool.View
{
    public class SizeView : ISizeView
    {
        private IView mainForm;
        private SizePresenter sizePresenter;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        private Layer imageLayer { get; set; }
        public Layer ImageLayer { get => imageLayer; set => imageLayer = value; }
        private Size currentSize { get; set; }
        public Size CurrentSize { get => currentSize; set => currentSize = value; }

        private ToolStripMenuItem sizeTool;
        private ToolStripMenuItem sizeAutoTool;
        private ToolStripMenuItem sizeDeleteTool;
        private ToolStripMenuItem sizeSaveTool;
        private ToolStripComboBox sizeComboBox;

        public event EventHandler<Size> AddSize;
        public event EventHandler<string> AddSizeNote;
        public event EventHandler DetectSizeNote;
        public event EventHandler SaveSize;
        public event EventHandler<Size> DeleteSize;
        public SizeView(IView _mainForm) 
        {
            mainForm = _mainForm;
            sizePresenter = new SizePresenter(this);
            layerService = mainForm.LayerService;

            sizeTool = mainForm.GetSizeTool();
            sizeAutoTool = mainForm.GetSizeAutoTool();
            sizeSaveTool = mainForm.GetSaveTool();
            sizeDeleteTool = mainForm.GetDeleteTool();
            sizeComboBox = mainForm.GetSizeComboBox();

            sizeTool.Click += SizeTool_Click;
            sizeAutoTool.Click += SizeAutoTool_Click;
            sizeSaveTool.Click += SizeSaveTool_Click;
            sizeDeleteTool.Click += SizeDeleteTool_Click;
            sizeComboBox.SelectedIndexChanged += SizeComboBox_SelectedIndexChanged;
        }

        public void Size_KeyDown(object sender, KeyEventArgs e)
        {
            if (layerService.DrawSizeMod || layerService.DrawSizeAutoMod)
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    SizeSaveTool_Click(this, e);
                }
                if (e.Control && e.KeyCode == Keys.D)
                {
                    SizeDeleteTool_Click(this, e);
                }
            }
        }
        public void SizeTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawSizeMod)
            {
                mainForm.MainForm_CheckedChanged();
            }
            layerService.DrawSizeMod = !layerService.DrawSizeMod;
            layerService.DrawSizeAutoMod = false;
            SaveDrawSizeMod();
        }

        public void SizeAutoTool_Click(object sender, EventArgs e)
        {
            if (!layerService.DrawSizeAutoMod)
            {
                mainForm.MainForm_CheckedChanged();
            }
            layerService.DrawSizeAutoMod = !layerService.DrawSizeAutoMod;
            layerService.DrawSizeMod = false;
            SaveDrawSizeMod();
        }

        public void SaveDrawSizeMod()
        {
            imageLayer = mainForm.GetImageLayer();
            if (layerService.DrawSizeMod || layerService.DrawSizeAutoMod)
            {
                if (layerService.DrawSizeMod)
                {
                    sizeTool.Checked = true;
                    sizeAutoTool.Checked = false;
                }
                else
                {
                    sizeAutoTool.Checked = true;
                    sizeTool.Checked = false;
                }

                layerService.MouseDown += layerServiceSize_MouseDown;
                layerService.MouseUp += layerServiceSize_MouseUp;
                layerService.MouseMove += layerServiceSize_MouseMove;
            }
            else
            {
                sizeTool.Checked = false;
                sizeAutoTool.Checked = false;

                layerService.MouseDown -= layerServiceSize_MouseDown;
                layerService.MouseUp -= layerServiceSize_MouseUp;
                layerService.MouseMove -= layerServiceSize_MouseMove;
            }
        }

        private void layerServiceSize_MouseDown(object sender, MouseEventArgs e)
        {
            if ((layerService.DrawSizeMod || layerService.DrawSizeAutoMod) && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                layerService.StartPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
            }/*
            if ((layerService.DrawSizeMod || layerService.DrawSizeAutoMod) && e.Button == MouseButtons.Right)
            {
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }*/
        }

        private void layerServiceSize_MouseUp(object sender, MouseEventArgs e)
        {
            if ((layerService.DrawSizeMod || layerService.DrawSizeAutoMod) && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left || e.Button == (MouseButtons.Left | MouseButtons.Right))
            {
                if (layerService.DrawSizeAutoMod)
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddSize?.Invoke(this, new Size(layerService.StartPoint, layerService.EndPoint, layerService.Origin));
                    DetectSizeNote?.Invoke(this, e);

                    SizeNoteForm noteForm = new SizeNoteForm(this, sizePresenter.GetMarkedSize().Note);
                    noteForm.ShowDialog();
                }

                if (layerService.DrawSizeMod)
                {
                    layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                    AddSize?.Invoke(this, new Size(layerService.StartPoint, layerService.EndPoint, layerService.Origin));

                    SizeNoteForm noteForm = new SizeNoteForm(this, sizePresenter.GetMarkedSize().Note);
                    noteForm.ShowDialog();
                }
                currentSize = sizePresenter.GetMarkedSize();
                Console.WriteLine(currentSize.Name.ToString() + " " + currentSize.Note + " " + currentSize.Start + currentSize.End);
                layerService.Invalidate();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceSize_MouseMove(object sender, MouseEventArgs e)
        {
            if ((layerService.DrawSizeMod || layerService.DrawSizeAutoMod) && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = new Point(Math.Abs(layerService.Origin.X) + e.Location.X, Math.Abs(layerService.Origin.Y) + e.Location.Y);
                AddSize?.Invoke(this, new Size(layerService.StartPoint, layerService.EndPoint, layerService.Origin));
                currentSize = sizePresenter.GetMarkedSize();
                layerService.Invalidate();
            }
        }

        public void SizeSaveTool_Click(object sender, EventArgs e)
        {
            if ((layerService.DrawSizeMod || layerService.DrawSizeAutoMod))
            {
                SaveSize?.Invoke(sender, e);
                sizeComboBox.Items.Clear();
                List<Size> sizes = sizePresenter.GetSizes();
                foreach (Size size in sizes)
                {
                    sizeComboBox.Items.Add(size);
                }

                sizeComboBox.ComboBox.DisplayMember = "name";
                sizePresenter.CleanMarkedSize();
            }
        }

        public void SizeDeleteTool_Click(object sender, EventArgs e)
        {
            DeleteSize?.Invoke(sender, currentSize);
            currentSize = new Size();
            sizeComboBox.Items.Clear();
            List<Size> sizes = sizePresenter.GetSizes();
            foreach (var size in sizes)
            {
                Console.WriteLine(size.Name);
                sizeComboBox.Items.Add(size);
            }

            sizeComboBox.ComboBox.DisplayMember = "name";
            sizePresenter.CleanMarkedSize();
            layerService.Invalidate();
        }

        public void SizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox comboBox = (ToolStripComboBox)sender;
            var selectedObject = (Size)comboBox.SelectedItem;
            currentSize = selectedObject;
            Console.WriteLine("Выбрана " + selectedObject.Name);
            Console.WriteLine(currentSize.Name.ToString() + " " + currentSize.Note + " " + currentSize.Start + currentSize.End);

            layerService.Invalidate();
        }

        public void SetSizeNote(string note)
        {
            AddSizeNote?.Invoke(this, note);
        }

        public void DrawRectangle(Graphics g)
        {
            List<Size> sizes = sizePresenter.GetSizes();

            Pen pen = new Pen(Color.Red, 3);

            foreach (Size size in sizes)
            {
                g.TranslateTransform(size.Origin.X, size.Origin.Y);
                Point start = new Point(size.Start.X - (size.Origin.X), size.Start.Y - (size.Origin.Y));
                Point end = new Point(size.End.X - (size.Origin.X), size.End.Y - (size.Origin.Y));

                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);

                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                g.TranslateTransform(-size.Origin.X, -size.Origin.Y);
            }
            if (layerService.DrawSizeMod && currentSize != null || layerService.DrawSizeAutoMod && currentSize != null)
            {
                pen.Color = Color.Purple;
                Size size = currentSize;

                g.TranslateTransform(size.Origin.X, size.Origin.Y);
                Point start = new Point(size.Start.X - (size.Origin.X), size.Start.Y - (size.Origin.Y));
                Point end = new Point(size.End.X - (size.Origin.X), size.End.Y - (size.Origin.Y));

                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);

                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
                g.TranslateTransform(-size.Origin.X, -size.Origin.Y);
            }
        }

        public List<Size> GetSizes()
        {
            return sizePresenter.GetSizes();
        }

        public void SaveSizeNoteForm(object sender, EventArgs e)
        {
            SizeSaveTool_Click(sender, e);
        }

        public void SetSizes(List<Size> sizes)
        {
            sizePresenter.SetSizes(sizes);

            foreach (var size in sizes)
            {
                Console.WriteLine(size.Name);
                sizeComboBox.Items.Add(size);
            }

            sizeComboBox.ComboBox.DisplayMember = "name";
            sizePresenter.CleanMarkedSize();
            layerService.Invalidate();
        }
    }
}
