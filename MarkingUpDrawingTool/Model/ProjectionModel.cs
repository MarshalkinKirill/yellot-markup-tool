using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.Model
{
    public class Projection : EventArgs
    {
        private string name;
        private List<Point> points;
        public List<Point> Points { get => points; set => points = value; }
        private List<Point> origins { get; set; }
        public List<Point> Origins { get => origins; set => origins = value; }

        public Projection() 
        {
            points = new List<Point>();
            name = string.Empty;
            origins = new List<Point>();
        }

        public Projection(List<Point> points, int number, List<Point> origin)
        {
            this.points = points;
            name = "Проекция №" + (number + 1).ToString();
            this.origins = origin;
        }

        public override string ToString()
        {
            return name;
        }
    }
    public class ProjectionModel
    {
        private List<Point> markedPoints;
        public List<Point> MarkedPoints { get => markedPoints;  set => markedPoints = value; }

        private List<Projection> projections;
        public List<Projection> Projections { get => projections; set => projections = value; }
        private List<Point> origins;
        public List<Point> Origins { get => origins; set => origins = value; }


        public ProjectionModel()
        {
            markedPoints = new List<Point>();
            projections = new List<Projection>();
            origins = new List<Point>();
        }

        public void SaveProjection()
        {
            List<Point> _points = new List<Point>(markedPoints);
            List<Point> origin = new List<Point>(origins);
            projections.Add(new Projection(_points, projections.Count(), origin));
        }

        public void AddPoint(Point point, Point origin)
        {
            markedPoints.Add(point);
            origins.Add(origin);
        }

    }
}
