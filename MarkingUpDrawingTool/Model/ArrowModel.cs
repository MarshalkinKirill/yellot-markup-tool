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
        private Point inflection { get; set; }
        public Point Inflection { get => inflection; set => inflection = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }

        public Arrow()
        {
            name = string.Empty;
            start = new Point();
            inflection = new Point();
            end = new Point();
        }
        public Arrow(Point _start, Point _inflection, Point _end, int num)
        {
            start = _start;
            inflection = _inflection;
            end = _end;
            name = "Стрелка №" + num.ToString();
        }
        public Arrow(Point _start, Point _end, int num)
        {
            start = _start;
            end = _end;
            name = "Стрелка №" + num.ToString();
        }
    }
    public class ArrowModel
    {
        

        public ArrowModel()
        {
            
        }

        
    }
}
