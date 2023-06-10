using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View;
using MarkingUpDrawingTool.View.ViewInterface;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using CvPoint = OpenCvSharp.Point;
using Point = System.Drawing.Point;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInteraface;

namespace MarkingUpDrawingTool.Presenter
{
    public class HolePresenter
    {
        private IHoleView view;
        private HoleModel model;

        public HolePresenter(IHoleView view)
        {
            this.view = view;
            model = new HoleModel();
            // Подписываемся на события
            view.AddHole += AddHole;
            view.SaveHole += SaveHole;
            view.DeleteHole += DeleteHole;
        }

        public void AddHole(object sender, Hole _hole)
        {
            model.AddHole(_hole.Center, _hole.Radius);
            Console.WriteLine("Center point and radius");
            Console.WriteLine(_hole.Center.ToString());
            Console.WriteLine(_hole.Radius.ToString());
            Console.WriteLine("////////////");
        }

        public void SaveHole(object sender, CircleSegment _circleSegment)
        {
            Hole _hole = new Hole(new Point(((int)_circleSegment.Center.X), (int)_circleSegment.Center.Y), _circleSegment.Radius, model.Holes.Count);
            model.SaveHole(_hole);
        }

        public void SaveHole(object sender, EventArgs e)
        {
            model.SaveHole();
            Console.WriteLine("Кол-во отверстий: " + model.Holes.Count.ToString());
        }

        public void DeleteHole(object sender, Hole _hole)
        {
            model.Holes.Remove(_hole);
        }

        public List<Hole> GetHoles()
        {
            return model.Holes;
        }

        public void SetHoles(List<Hole> _holes)
        {
            model.Holes = _holes;
        }

        public Hole GetMarkedHole()
        {
            return new Hole(model.Center, model.Radius);
        }

        public Point GetMarkedCenter()
        {
            return model.Center;
        }

        public float GetMarkedRadius()
        {
            return model.Radius;
        }

        public void CleanMarkedHole()
        {
            model.Center = Point.Empty;
            model.Radius = 0;
        }
        public List<CircleSegment> DetectHoles(Layer imageLayer)
        {
            Bitmap bm = new Bitmap(imageLayer.Image);
            Console.WriteLine(bm);
            Mat image = bm.ToMat();

            // Преобразование изображения в оттенки серого
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            // Бинаризация изображения для выделения черных областей
            Mat binary = new Mat();
            Cv2.Threshold(gray, binary, 1, 255, ThresholdTypes.Binary);

            // Поиск контуров на бинарном изображении
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // Создание пустого изображения для отображения найденных окружностей
            Mat circlesImage = Mat.Zeros(image.Size(), MatType.CV_8UC1);

            // Поиск окружностей с помощью преобразования Хафа
            CircleSegment[] circles = Cv2.HoughCircles(
                gray,
                HoughModes.Gradient,
                1,
                3, // Минимальное расстояние между окружностями (увеличьте, чтобы избежать дублирования)
                param1: 30, // Первый параметр преобразования Хафа
                param2: 60, // Второй параметр преобразования Хафа (увеличьте, чтобы уменьшить ложные обнаружения)
                minRadius: 5, // Минимальный радиус окружности
                maxRadius: 110 // Максимальный радиус окружности
            );

            circles = RemoveDuplicates(circles);

            return circles.ToList();
        }

        // Удаление дубликатов окружностей
        private CircleSegment[] RemoveDuplicates(CircleSegment[] circles)
        {
            List<CircleSegment> uniqueCircles = new List<CircleSegment>();

            foreach (CircleSegment circle in circles)
            {
                bool isDuplicate = false;
                foreach (CircleSegment uniqueCircle in uniqueCircles)
                {
                    double distance = Math.Sqrt(Math.Pow(circle.Center.X - uniqueCircle.Center.X, 2) + Math.Pow(circle.Center.Y - uniqueCircle.Center.Y, 2));
                    if (distance < uniqueCircle.Radius)
                    {
                        isDuplicate = true;
                        break;
                    }
                }

                if (!isDuplicate)
                {
                    uniqueCircles.Add(circle);
                }
            }

            return uniqueCircles.ToArray();
        }
    }
}
