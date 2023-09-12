using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Model
{
    public class Border
    {
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private Point origin { get; set; }
        public Point Origin { get =>  origin; set => origin = value; }
        public Border()
        {
            start = Point.Empty;
            end = Point.Empty;
            origin = Point.Empty;
        }
        public Border(Point start, Point end, Point origin)
        {
            this.start = start;
            this.end = end;
            this.origin = origin;
        }
    }
    public class BorderModel
    {
        private Border currentBorder { get; set; }
        public Border CurrentBorder { get => currentBorder; set => currentBorder = value; }
        private Border border { get; set; }
        public Border Border { get => border; set => border = value; }
        public BorderModel()
        {
            currentBorder = new Border();
            border = new Border();
        }

        public void SaveBorder(Border border)
        {
            Point start = new Point(border.Start.X, border.Start.Y);
            Point end = new Point(border.End.X, border.End.Y);
            Point origin = new Point(border.Origin.X, border.Origin.Y);
            currentBorder = new Border(start, end, origin);
        }
        public void AddBorder(Border border)
        {
            Point start = new Point(border.Start.X, border.Start.Y);
            Point end = new Point(border.End.X, border.End.Y);
            Point origin = new Point(border.Origin.X, border.Origin.Y);
            this.border = new Border(start, end, origin);
        }
    }
}
