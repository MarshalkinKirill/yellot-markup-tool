using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInteraface;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DrSize = System.Drawing.Size;
using Size = MarkingUpDrawingTool.Model.Size;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using CvPoint = OpenCvSharp.Point;
using Point = System.Drawing.Point;
using Tesseract;
using Emgu.CV;
using Emgu.CV.Structure;
using Mat = OpenCvSharp.Mat;
using EmguMat = Emgu.CV.Mat;
using System.IO;
using System.Reflection;
using Rect = OpenCvSharp.Rect;
using TesRect = Tesseract.Rect;
using Bitmap = System.Drawing.Bitmap;

namespace MarkingUpDrawingTool.Presenter
{
    public class SizePresenter
    {
        private ISizeView view;
        private SizeModel model;

        public SizePresenter(ISizeView view)
        {
            this.view = view;
            model = new SizeModel();

            //Подписка на события View
            view.AddSize += AddSize;
            view.AddSizeNote += AddSizeNote;
            view.DetectSizeNote += DetectSizeNote;
            view.SaveSize += SaveSize;
            view.DeleteSize += DeleteSize;
        }

        public void AddSize(object sender, Size size) 
        {
            model.CurrentSize = size;
        }

        public void AddSizeNote(object sender, string note)
        {
            model.CurrentSize.Note = note;
            Console.WriteLine(model.CurrentSize.Note + " " + model.CurrentSize.Start.ToString() + " " + model.CurrentSize.End.ToString());
        }

        public void DetectSizeNote(object sender, EventArgs e)
        {
            Point start = model.CurrentSize.Start;
            Point end = model.CurrentSize.End;
            model.CurrentSize.Note = this.DetectText(view.ImageLayer, start, end);
        }

        public void SaveSize(object sender, EventArgs e)
        {
            model.SaveSize();
        }

        public void DeleteSize(object sender, Size size)
        {
            model.DeleteSize(size);
        }

        public List<Size> GetSizes()
        {
            return model.Sizes;
        }

        public Size GetMarkedSize()
        {
            return model.CurrentSize;
        }

        public void CleanMarkedSize()
        {
            model.CurrentSize = new Size();
        }

        public string DetectText(Layer imageLayer, Point start,  Point end)
        {
            Bitmap bm = new Bitmap(imageLayer.Image);
            Console.WriteLine(bm);
            Mat image = bm.ToMat();

            // Обрезка изображения по указанным точкам диагонали
            Rect roi = new Rect();
            if (start.X < end.X && start.Y < end.Y)
            {
                roi = new Rect(start.X, start.Y, Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            }
            if (start.X < end.X && start.Y > end.Y)
            {
                Console.WriteLine("-1");
                roi = new Rect(start.X, start.Y - Math.Abs(end.Y - start.Y), Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            }
            if (start.X > end.X && start.Y > end.Y)
            {
                Console.WriteLine("0");
                roi = new Rect(end.X, end.Y, Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            }
            if (start.X > end.X && start.Y < end.Y)
            {
                Console.WriteLine("1");
                roi = new Rect(end.X, end.Y - Math.Abs(end.Y - start.Y), Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            }

            //Rect roi = new Rect(start.X, start.Y, Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
            Mat croppedImage = new Mat(image, roi);

            // Преобразование изображения в формат Pix для использования в Tesseract
            Pix pixImage = PixConverter.ToPix(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(croppedImage));
            pixImage.Save("qwe.png");
            using (var engine = new TesseractEngine("./tessdata", "rus+osd+equ", EngineMode.Default))
            {
                //engine.SetVariable("tessedit_char_whitelist", "0123456789");
                // Распознавание текста
                using (var page = engine.Process(pixImage))
                {
                    string text = page.GetText().Trim();
                    return text;
                }
            }
        }

    }
}
