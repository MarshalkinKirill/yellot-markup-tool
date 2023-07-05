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
        private Point origin { get; set; }
        public Point Origin { get => origin; set => origin = value; }

        public Hole() 
        {
            name = string.Empty;
            center = new Point();
            radius = 0;
            origin = new Point();
        }
        public Hole(Point _center, float _radious, Point origin) 
        {
            center = _center;
            radius = _radious;
            this.origin = origin;
        }
        public Hole(Point _center, float _radious, int _number, Point origin) 
        {
            name = "Hole №" + _number.ToString();
            center = _center;
            radius = _radious;
            this.origin = origin;
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
        private Point origin { get; set; }
        public Point Origin { get => origin; set => origin = value; }

        public Hole Hole
        {
            get => default;
            set
            {
            }
        }

        public HoleModel() 
        {
            holes = new List<Hole>();
            center = Point.Empty;
            radius = 0;
            origin = Point.Empty;
        }

        public void SaveHole()
        {
            Point center = new Point(this.center.X, this.center.Y);
            float radius = this.radius;
            Point origin = new Point(this.origin.X, this.origin.Y);
            holes.Add(new Hole(center, radius, holes.Count, origin));
        }

        public void SaveHole(Hole _hole)
        {
            holes.Add(_hole);
        }

        public void AddHole(Point _center, float _radius, Point origin)
        {
            center = _center;
            radius = _radius;
            this.origin = origin;
        }
    }
}
