using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarkingUpDrawingTool.Model
{
    public class Size
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private string note { get; set; }
        public string Note { get => note; set => note = value; }
        public Size() 
        {
            name = string.Empty;
            start = new Point();
            end = new Point();
            note = string.Empty;
        }
        public Size(Point _start, Point _end)
        {
            name = string.Empty;
            start = _start;
            end = _end;
            note = string.Empty;
        }
        public Size(Point _start, Point _end, string _note, int _num)
        {
            name = "Размер №" + _num.ToString();
            start = _start;
            end = _end;
            note = _note;
        }
    }
    public class SizeModel
    {
        private List<Size> sizes { get; set; }
        public List<Size> Sizes { get => sizes; set => sizes = value; }
        private Size currentSize { get; set; }
        public Size CurrentSize { get => currentSize; set => currentSize = value; }

        public SizeModel() 
        {
            sizes = new List<Size>();
            currentSize = new Size();
        }

        public void SaveSize()
        {
            Point x = new Point(currentSize.Start.X, currentSize.Start.Y);
            Point y = new Point(currentSize.End.X, currentSize.End.Y);
            string note = currentSize.Note;
            sizes.Add(new Size(x , y , note, this.sizes.Count + 1));

        }

        public void DeleteSize(Size size)
        {
            this.sizes.Remove(size);
        }
    }
}
