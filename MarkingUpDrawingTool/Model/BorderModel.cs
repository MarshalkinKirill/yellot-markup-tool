﻿using System;
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
        public Border()
        {
            start = Point.Empty;
            end = Point.Empty;
        }
        public Border(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }
    }
    public class BorderModel
    {
        private Border currentBorder { get; set; }
        public Border CurrentBorder { get => currentBorder; set => currentBorder = value; }
        public BorderModel()
        {
            currentBorder = new Border();
        }

        public void SaveBorder(Border border)
        {
            Point start = new Point(border.Start.X, border.Start.Y);
            Point end = new Point(border.End.X, border.End.Y);
            currentBorder = new Border(start, end);
        }
    }
}
