using System;
using System.Collections.Generic;
using System.Drawing;
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

        public bool reDraw;


        public LayerService()
        {
            layers = new List<Layer>();
            drawProjectionMod = false;
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
    }
}
