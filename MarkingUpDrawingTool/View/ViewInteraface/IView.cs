using MarkingUpDrawingTool.Model;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvPoint = OpenCvSharp.Point;
using Point = System.Drawing.Point;

namespace MarkingUpDrawingTool.View.ViewInterface
{
    public interface IView
    {
        //Prjection view
        event EventHandler<Point> PointMarked;
        event EventHandler SaveProjection;
        event EventHandler<Projection> DeleteProjection;
        void DrawLine(Graphics g);

        //HoleView
        event EventHandler<Hole> AddHole;
        event EventHandler SaveHole;
        event EventHandler<Hole> DeleteHole;
    }
}
