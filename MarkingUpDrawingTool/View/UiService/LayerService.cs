using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.Presenter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View.UiService
{
    public class Layer
    {

        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Image image { get; set; }
        public Image Image { get { return image; } }
        private Point location { get; set; }
        public Point Location { get => location; set => location = value; }
        private Action<Graphics> drawActions { get; set; }
        public Action<Graphics> DrawActions { get { return drawActions; } set { drawActions = value; } }

        public Layer() 
        {
            this.location = new Point(0, 0);
        }
        public Layer(Image image, Point point, string name) 
        {
            this.image = image;
            this.location = point;
            this.name = name;
        }
        public Layer(Point location)
        {
            this.location = location;
        }
    }

    public class LayerService : PictureBox
    {
        List<Layer> layers;
        private Point origin { get; set; } // Начало координат
        public Point Origin { get => origin; set => origin = value; }   

        private Point startPoint { get; set; }
        public Point StartPoint { get => startPoint; set => startPoint = value; }
        private Point endPoint { get; set; }
        public Point EndPoint { get => endPoint; set => endPoint = value; }
        private Point centerPoint { get; set; }
        public Point CenterPoint { get => centerPoint; set => centerPoint = value; }
        private Point startNote { get; set; }
        public Point StartNote { get => startNote; set => startNote = value; }
        private Point endNote { get; set; }
        public Point EndNote { get => endNote; set => endNote = value; } 
        private bool drawProjectionMod  { get; set; }
        public bool DrawProjectionMod { get => drawProjectionMod; set => drawProjectionMod = value; }
        private bool drawHoleMod { get; set; }
        public bool DrawHoleMod { get => drawHoleMod; set => drawHoleMod = value; }
        private bool drawTableMod { get; set; }
        public bool DrawTableMod { get => drawTableMod; set => drawTableMod = value; }
        private bool drawMainTableMod { get; set; }
        public bool DrawMainTableMod { get => drawMainTableMod; set => drawMainTableMod = value; }
        private bool drawSizeMod { get; set; }
        public bool DrawSizeMod { get => drawSizeMod; set => drawSizeMod = value; }
        private bool drawSizeAutoMod { get; set; }
        public bool DrawSizeAutoMod { get => drawSizeAutoMod; set => drawSizeAutoMod = value; }
        private bool drawLinearArrowMod { get; set; }
        public bool DrawLinearArrowMod { get => drawLinearArrowMod; set => drawLinearArrowMod = value; }
        private bool drawAngularArrowMod { get; set; }
        public bool DrawAngularArrowMod { get => drawAngularArrowMod; set => drawAngularArrowMod = value; }
        private bool drawRadialArrowMod { get; set; }
        public bool DrawRadialArrowMod { get => drawRadialArrowMod; set => drawRadialArrowMod = value; }
        private bool drawDiametralArrowMod { get; set; }
        public bool DrawDiametralArrowMod { get => drawDiametralArrowMod; set => drawDiametralArrowMod = value; }
        private bool drawReferenceArrowMod { get; set; }
        public bool DrawReferenceArrowMod { get => drawReferenceArrowMod; set => drawReferenceArrowMod = value; }
        private bool drawConeArrowMod { get; set; }
        public bool DrawConeArrowMod { get => drawConeArrowMod; set => drawConeArrowMod = value; }
        private bool drawChamferArrowMod { get; set; }
        public bool DrawChamferArrowMod { get => drawChamferArrowMod; set => drawChamferArrowMod = value; }
        private bool drawGapMod { get; set; }
        public bool DrawGapMod { get => drawGapMod; set => drawGapMod = value; }
        private bool drawBorderMod { get; set; }
        public bool DrawBorderMod { get => drawBorderMod; set => drawBorderMod = value; }
        private bool drawProjectionRoiMod { get; set; }
        public bool DrawProjectionRoiMod { get => drawProjectionRoiMod; set => drawProjectionRoiMod = value; }

        public bool reDraw;

        public LayerService()
        {
            layers = new List<Layer>();
            drawProjectionMod = false;
            drawHoleMod = false;
            drawTableMod = false;
            drawMainTableMod = false;
            startPoint = Point.Empty;
            endPoint = Point.Empty;
            reDraw = false;
            origin = new Point(0, 0);
        }

        public void AddLayer(Layer layer)
        {
            layers.Add(layer);
        }

        public void RemoveLayer(Layer layer)
        {
            layers.Remove(layer);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Применяем смещение начала координат
            e.Graphics.TranslateTransform(origin.X, origin.Y);

            foreach (Layer layer in layers)
            {
                if (layer.Image != null)
                {
                    e.Graphics.DrawImage(layer.Image, new Rectangle(layer.Location, layer.Image.Size));
                }

                if (layer.DrawActions != null)
                {
                    //e.Graphics.TranslateTransform(layer.Location.X, layer.Location.Y);
                    //Console.WriteLine("LOCATION _ " + layer.Location.ToString());
                    layer.DrawActions(e.Graphics);
                }
            }
        }

        public void ScrollLayers(ScrollOrientation orientation, int newValue)
        {
            foreach (Layer layer in layers)
            {
                if (orientation == ScrollOrientation.HorizontalScroll)
                {
                    layer.Location = new Point(layer.Location.X + newValue, layer.Location.Y);
                    //Console.WriteLine(layer.Location.ToString());
                }
                else if (orientation == ScrollOrientation.VerticalScroll)
                {
                    layer.Location = new Point(layer.Location.X, layer.Location.Y + newValue);
                    //Console.WriteLine(layer.Location.ToString());
                }
            }
        }

        public void vPanel_Scroll(object sender, ScrollEventArgs e)
        {
            int scrollValue = -e.NewValue; // Получаем текущее значение скроллинга

            // Изменяем начало координат
            origin = new Point(origin.X, scrollValue);
            //Console.WriteLine(origin.ToString());
            // Обновляем отображение
            Invalidate();
        }

        public void hPanel_Scroll(object sender, ScrollEventArgs e)
        {
            int scrollValue = -e.NewValue; // Получаем текущее значение скроллинга

            // Изменяем начало координат
            origin = new Point(scrollValue, origin.Y);
            //Console.WriteLine(origin.ToString());
            // Обновляем отображение
            Invalidate();
        }

        public void RefreshDrawMods()
        {
            startPoint = Point.Empty;
            endPoint = Point.Empty;
            endNote = Point.Empty;
            startNote = Point.Empty;
            CenterPoint = Point.Empty;
            drawLinearArrowMod = false;
            drawAngularArrowMod = false;
            drawRadialArrowMod = false;
            drawDiametralArrowMod = false;
            drawReferenceArrowMod = false;
            drawConeArrowMod = false;
            drawChamferArrowMod = false;
            drawBorderMod = false;
            drawGapMod = false;
            drawHoleMod = false;
            drawMainTableMod = false;
            drawTableMod = false;
            drawProjectionMod = false;
            drawProjectionRoiMod = false;
            drawSizeAutoMod = false;
            drawSizeMod = false;
        }

        public byte[] BitmapToBytes(Bitmap bitmap)
        {
            // Создаем пустой массив байтов
            byte[] bytes;

            // Используем блок using для гарантированного освобождения ресурсов
            using (var stream = new MemoryStream())
            {
                // Сохраняем изображение в поток в формате PNG
                bitmap.Save(stream, ImageFormat.Png);

                // Получаем массив байтов из потока
                bytes = stream.ToArray();
            }

            return bytes;
        }
    }

    public class LayerServiceControl : Control
    {
        private Panel panel;
        private List<Layer> layers;

        public LayerServiceControl()
        {
            DoubleBuffered = true;

            panel = new Panel();
            panel.AutoScroll = true;
            panel.Dock = DockStyle.Fill;
            panel.Paint += Panel_Paint;

            layers = new List<Layer>();

            Controls.Add(panel);
        }

        public void AddLayer(Layer layer)
        {
            layers.Add(layer);
            panel.Invalidate();
        }

        public void RemoveLayer(Layer layer)
        {
            layers.Remove(layer);
            panel.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Рисование пользовательского элемента управления (если необходимо)
            foreach (Layer layer in layers)
            {
                if (layer.Image != null)
                {
                    e.Graphics.DrawImage(layer.Image, layer.Location);
                }

                if (layer.DrawActions != null)
                {
                    layer.DrawActions(e.Graphics);
                }
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            foreach (Layer layer in layers)
            {
                if (layer.Image != null)
                {
                    e.Graphics.DrawImage(layer.Image, layer.Location);
                }

                if (layer.DrawActions != null)
                {
                    layer.DrawActions(e.Graphics);
                }
            }
        }
    }
}
