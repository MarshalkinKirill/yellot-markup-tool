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
        public Gap() 
        { 
            name = string.Empty;
            start = Point.Empty;
            end = Point.Empty;
        }
        public Gap(Point start, Point end)
        {
            name = string.Empty;
            this.start = start;
            this.end = end;
        }
        public Gap(Point start, Point end, int num)
        {
            this.start = start;
            this.end = end;
            name = "Разрыв №" + num.ToString();
        }
    }
    public class GapModel
    {
        private List<Gap> gaps { get; set; }
        public List<Gap> Gaps { get => gaps; set => gaps = value; }
        private Gap currentGap { get; set; }
        public Gap CurrentGap { get => currentGap; set => currentGap = value; } 

        public GapModel() 
        {
            gaps = new List<Gap>();
            currentGap = new Gap();
        }

        public void SaveGap()
        {
            Point start = new Point(currentGap.Start.X, currentGap.Start.Y);
            Point end = new Point(currentGap.End.X, currentGap.End.Y);
            gaps.Add(new Gap(start, end, this.gaps.Count + 1));
        }

        public void DeleteGap(Gap gap)
        {
            this.Gaps.Remove(gap);
        }
    }
}
