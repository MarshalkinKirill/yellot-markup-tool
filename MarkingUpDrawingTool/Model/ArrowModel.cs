using Emgu.CV.Dnn;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Model
{
    public enum ArrowType
    {
        Linear,
        Angular,
        Radial,
        Diametral,
        Reference,
        Cone,
        Chamfer
    }
    public class Arrow
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private ArrowType type { get; set; }
        public ArrowType Type { get => type; set => type = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point center { get; set; }
        public Point Center { get => center; set => center = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private Point noteStart { get; set; }
        public Point NoteStart { get => noteStart; set => noteStart = value; }
        private Point noteEnd { get; set; }
        public Point NoteEnd { get => noteEnd; set => noteEnd = value; }
        private Point origin { get; set; }
        public Point Origin { get => origin; set => origin = value; }

        public Arrow()
        {
            name = string.Empty;
            start = new Point();
            end = new Point();
            center = new Point();
            noteStart = new Point();
            noteEnd = new Point();
            origin = new Point();
        }
        public Arrow(Point _start, Point _end, Point _noteStart, Point _noteEnd, int num, Point origin, ArrowType type, Point center)
        {
            start = _start;
            end = _end;
            noteStart = _noteStart;
            noteEnd = _noteEnd;
            name = "Стрелка №" + num.ToString();
            this.origin = origin;
            this.type = type;
            this.center = center;
        }
        public Arrow(Point _start, Point _end, Point _noteStart, Point _noteEnd, Point origin, ArrowType arrowType)
        {
            start = _start;
            end = _end;
            noteStart = _noteStart;
            noteEnd = _noteEnd;
            name = string.Empty;
            this.origin = origin;
            this.type = arrowType;
            center = new Point();
        }

        public Arrow(Point start, Point end, Point noteStart, Point noteEnd, Point origin, ArrowType type, Point center)
        {
            this.start = start;
            this.end = end;
            this.noteStart = noteStart;
            this.noteEnd = noteEnd;
            this.origin = origin;
            this.type = type;
            this.center = center;
        }
    }
    public class ArrowModel
    {
        private List<Arrow> arrows { get; set; }
        public List<Arrow> Arrows { get => arrows; set => arrows = value; }
        private Arrow currentArrow { get; set; }
        public Arrow CurrentArrow { get => currentArrow; set => currentArrow = value; }

        public Arrow Arrow
        {
            get => default;
            set
            {
            }
        }

        public ArrowModel()
        {
            arrows = new List<Arrow>();
            currentArrow = new Arrow();
        }

        public void SaveArrow()
        {
            Point start = new Point(currentArrow.Start.X, currentArrow.Start.Y);
            Point end = new Point(currentArrow.End.X, currentArrow.End.Y);
            Point noteStart = new Point(currentArrow.NoteStart.X, currentArrow.NoteStart.Y);
            Point noteEnd = new Point(currentArrow.NoteEnd.X, currentArrow.NoteEnd.Y);
            Point origin = new Point(currentArrow.Origin.X, currentArrow.Origin.Y);
            Point center = new Point(currentArrow.Center.X, currentArrow.Center.Y);
            ArrowType type = currentArrow.Type;
            arrows.Add(new Arrow(start, end, noteStart, noteEnd, this.arrows.Count + 1, origin, type, center));
            Console.WriteLine(CurrentArrow.Center.ToString());
            Console.WriteLine(CurrentArrow.Start.ToString() + CurrentArrow.End.ToString());
            Console.WriteLine(currentArrow.Type.ToString());
        }

        public void DeleteArrow(Arrow arrow)
        {
            this.arrows.Remove(arrow);
        }
    }
}
