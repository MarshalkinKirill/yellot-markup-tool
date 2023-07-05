using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Model
{
    public class Gap
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private Point startOrigin { get; set; }
        public Point StartOrigin { get => startOrigin; set => startOrigin = value; }
        private Point endOrigin { get; set; }
        public Point EndOrigin { get => endOrigin; set => endOrigin = value; }
        public Gap() 
        { 
            name = string.Empty;
            start = Point.Empty;
            end = Point.Empty;
            startOrigin = Point.Empty;
            endOrigin = Point.Empty;
        }
        public Gap(Point start, Point end, Point startOrigin, Point endOrigin)
        {
            name = string.Empty;
            this.start = start;
            this.end = end;
            this.startOrigin = startOrigin;
            this.endOrigin = endOrigin;
        }
        public Gap(Point start, Point end, Point startOrigin, Point endOrigin, int num)
        {
            this.start = start;
            this.end = end;
            this.startOrigin = startOrigin;
            this.endOrigin = endOrigin;
            name = "Разрыв №" + num.ToString();
        }

    }
    public class GapModel
    {
        private List<Gap> gaps { get; set; }
        public List<Gap> Gaps { get => gaps; set => gaps = value; }
        private Gap currentGap { get; set; }
        public Gap CurrentGap { get => currentGap; set => currentGap = value; }

        public Gap Gap
        {
            get => default;
            set
            {
            }
        }

        public GapModel() 
        {
            gaps = new List<Gap>();
            currentGap = new Gap();
        }

        public void SaveGap()
        {
            Point start = new Point(currentGap.Start.X, currentGap.Start.Y);
            Point end = new Point(currentGap.End.X, currentGap.End.Y);
            Point startOrigin = new Point(currentGap.StartOrigin.X, currentGap.StartOrigin.Y);
            Point endOrigin = new Point(currentGap.EndOrigin.X, currentGap.EndOrigin.Y);
            gaps.Add(new Gap(start, end, startOrigin, endOrigin, this.gaps.Count + 1));
        }

        public void DeleteGap(Gap gap)
        {
            this.Gaps.Remove(gap);
        }
    }
}
