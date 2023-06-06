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
        private Image image { get; set; }
        public Image Image { get { return image; } }
        private Point location { get; set; }
        public Point Location { get { return location; } }
        private Action<Graphics> drawActions { get; set; }
        public Action<Graphics> DrawActions { get { return drawActions; } set { drawActions = value; } }

        public Layer() { }
        public Layer(Image _image, Point _point) 
        {
            image = _image;
            location = _point;
        }
    }

    public class LayerService : PictureBox
    {
        List<Layer> layers;

        private bool drawProjectionMod  { get; set; }
        public bool DrawProjectionMod { get => drawProjectionMod; set => drawProjectionMod = value; }
        private bool drawHoleMod { get; set; }
        public bool DrawHoleMod { get => drawHoleMod; set => drawHoleMod = value; }
        private Point startPoint { get; set; }
        public Point StartPoint { get => startPoint; set => startPoint = value; }
        private Point endPoint { get; set; }
        public Point EndPoint { get => endPoint; set => endPoint = value; }
        private bool drawTableMod { get; set; }
        public bool DrawTableMod { get => drawTableMod; set => drawTableMod = value; }
        private bool drawMainTableMod { get; set; }
        public bool DrawMainTableMod { get => drawMainTableMod; set => drawMainTableMod = value; }
        private bool drawSizeMod { get; set; }
        public bool DrawSizeMod { get => drawSizeMod; set => drawSizeMod = value; }
        private bool drawSizeAutoMod { get; set; }
        public bool DrawSizeAutoMod { get => drawSizeAutoMod; set => drawSizeAutoMod = value; }

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

            foreach (Layer layer in layers)
            {
                if (layer.Image != null)
                {
                    e.Graphics.DrawImage(layer.Image, new Rectangle(layer.Location, layer.Image.Size));
                }

                if (layer.DrawActions != null)
                {
                    layer.DrawActions(e.Graphics); 
                }
            }
        }

        public void ChangeProjectionDrawAction()
        {
            reDraw = !reDraw;
        }

        public Point PointToImageCoordinates(Point point, Layer imageLayer)
        {
            float imageWidth = imageLayer.Image.Width;
            float imageHeight = imageLayer.Image.Height;
            float controlWidth = this.Width;
            float controlHeight = this.Height;

            float scaleX = imageWidth / controlWidth;
            float scaleY = imageHeight / controlHeight;

            int imageX = (int)(point.X * scaleX);
            int imageY = (int)(point.Y * scaleY);

            return new Point(imageX, imageY);
        }

        public void DrawDotRectangle(Graphics g, TablePresenter tablePresenter, Table currentTable)
        {
            List<Table> tables = tablePresenter.GetTables();

            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = DashStyle.Dot;

            foreach (Table table in tables)
            {
                Point start = table.Start;
                Point end = table.End;
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);

                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
            }
            if (DrawTableMod || DrawMainTableMod)
            {
                pen.Color = Color.Purple;
                Table table = currentTable;

                Point start = table.Start;
                Point end = table.End;
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);

                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
            }
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
}
