using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Model
{
    public class ProjectionRoi
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private Point origin { get; set; }
        public Point Origin { get => origin; set => origin = value; }

        public ProjectionRoi() 
        {
            name = string.Empty;
            start = new Point();
            end = new Point();
            origin = new Point();
        }

        public ProjectionRoi(Point start, Point end, Point origin)
        {
            name = string.Empty;
            this.start = start;
            this.end = end;
            this.origin = origin;
        }

        public ProjectionRoi(Point start, Point end, int num, Point origin)
        {
            name = "Projection ROI №" + num.ToString();
            this.start = start;
            this.end = end;
            this.origin = origin;
        }
    }
    public class ProjectionRoiModel
    {
        private List<ProjectionRoi> projectionRois { get; set; }
        public List<ProjectionRoi> ProjectionRois { get => projectionRois; set => projectionRois = value; }
        private ProjectionRoi currentProjectionRoi { get; set; }
        public ProjectionRoi CurrentProjectionRoi { get => currentProjectionRoi; set => currentProjectionRoi = value; }

        public ProjectionRoiModel()
        {
            projectionRois = new List<ProjectionRoi>();
            currentProjectionRoi = new ProjectionRoi();
        }

        public void SaveProjectionRoi()
        {
            Point start = new Point(currentProjectionRoi.Start.X, currentProjectionRoi.Start.Y);
            Point end = new Point(currentProjectionRoi.End.X, currentProjectionRoi.End.Y);
            Point origin = new Point(currentProjectionRoi.Origin.X, currentProjectionRoi.Origin.Y);
            ProjectionRoi projectionRoi = new ProjectionRoi(start, end, this.projectionRois.Count + 1, origin);
            projectionRois.Add(projectionRoi);
        }

        public void DeleteProjectionRoi(ProjectionRoi projectionRoi)
        {
            projectionRois.Remove(projectionRoi);
        }
    }
}
