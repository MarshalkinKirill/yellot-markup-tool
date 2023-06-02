using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MarkingUpDrawingTool.Model
{
    public class Hole
    {
        private string name;
        private Point center { get; set; }
        public Point Center { get => center; set => center = value; }
        private float radius { get; set; }
        public float Radius { get => radius; set => radius = value; }

        public Hole() 
        {
            name = string.Empty;
            center = new Point();
            radius = 0;
        }
        public Hole(Point _center, float _radious) 
        {
            center = _center;
            radius = _radious;
        }
        public Hole(Point _center, float _radious, int _number) 
        {
            name = "Hole №" + _number.ToString();
            center = _center;
            radius = _radious;
        }
        public override string ToString()
        {
            return name;
        }
    }

    public class HoleModel
    {
        private List<Hole> holes { get; set; }
        public List<Hole> Holes { get => holes; set => holes = value; }
        private Point center { get; set; }
        public Point Center { get => center; set => center = value; }
        private float radius { get; set; }
        public float Radius { get => radius; set => radius = value; }

        public HoleModel() 
        {
            holes = new List<Hole>();
            center = Point.Empty;
            radius = 0;
        }

        public void SaveHole()
        {
            Point _center = new Point(center.X, center.Y);
            float _radius = radius;
            holes.Add(new Hole(_center, _radius, holes.Count));
        }

        public void SaveHole(Hole _hole)
        {
            holes.Add(_hole);
        }

        public void AddHole(Point _center, float _radius)
        {
            center = _center;
            radius = _radius;
        }
    }
}
