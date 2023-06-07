using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Model
{
    public class Arrow
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private Point noteStart { get; set; }
        public Point NoteStart { get => noteStart; set => noteStart = value; }
        private Point noteEnd { get; set; }
        public Point NoteEnd { get => noteEnd; set => noteEnd = value; }

        public Arrow()
        {
            name = string.Empty;
            start = new Point();
            end = new Point();
            noteStart = new Point();
            noteEnd = new Point();
        }
        public Arrow(Point _start, Point _end, Point _noteStart, Point _noteEnd, int num)
        {
            start = _start;
            end = _end;
            noteStart = _noteStart;
            noteEnd = _noteEnd;
            name = "Стрелка №" + num.ToString();
        }
        public Arrow(Point _start, Point _end, Point _noteStart, Point _noteEnd)
        {
            start = _start;
            end = _end;
            noteStart = _noteStart;
            noteEnd = _noteEnd;
            name = string.Empty;
        }
    }
    public class ArrowModel
    {
        private List<Arrow> arrows { get; set; }
        public List<Arrow> Arrows { get => arrows; set => arrows = value; }
        private Arrow currentArrow { get; set; }
        public Arrow CurrentArrow { get => currentArrow; set => currentArrow = value; }

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
            arrows.Add(new Arrow(start, end, noteStart, noteEnd, this.arrows.Count + 1));
        }

        public void DeleteArrow(Arrow arrow)
        {
            this.arrows.Remove(arrow);
        }
    }
}
