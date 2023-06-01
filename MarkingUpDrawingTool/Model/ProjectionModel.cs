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

        public Projection() 
        {
            points = new List<Point>();
            name = string.Empty;
        }

        public Projection(List<Point> _points, int number)
        {
            points = _points;
            name = "Проекция №" + (number + 1).ToString();
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



        public ProjectionModel()
        {
            markedPoints = new List<Point>();
            projections = new List<Projection>();
        }

        public void SaveProjection()
        {
            List<Point> _points = new List<Point>(markedPoints);
            projections.Add(new Projection(_points, projections.Count()));
        }

        public void AddPoint(Point point)
        {
            markedPoints.Add(point);
        }

    }
}
