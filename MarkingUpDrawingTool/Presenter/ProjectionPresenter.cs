using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.ViewInteraface;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MarkingUpDrawingTool.Presenter
{
    public class ProjectionPresenter
    {
        private IProjectionView view { get; set; }
        private ProjectionModel model;

        public ProjectionPresenter(IProjectionView view)
        {
            this.view = view;
            model = new ProjectionModel();
            // Подписка на события
            view.PointMarked += OnPointMarked;
            view.ChangePoint += ChangePoint;
            view.SaveProjection += SaveProjection;
            view.DeleteProjection += DeleteProjection;
        }

        private void OnPointMarked(object sender, Point point)
        {
            model.MarkedPoints.Add(point);
            model.Origins.Add(view.LayerService.Origin);
            Console.WriteLine(point.ToString());
            Console.WriteLine(view.LayerService.Origin.ToString());
        }
        private void ChangePoint(object sender, Point point)
        { 
            model.MarkedPoints.Remove(model.MarkedPoints.Last());
            model.Origins.Remove(model.Origins.Last());
            model.MarkedPoints.Add(point);
            model.Origins.Add(view.LayerService.Origin);
        }
        private void SaveProjection(object sender, EventArgs e) 
        {
            model.SaveProjection();
            //Console.WriteLine(model.Projections.Count);
        }

        private void DeleteProjection(object sender, Projection projection)
        {
            model.Projections.Remove(projection);
        }

        public List<Point> GetPoints() 
        {
            return model.MarkedPoints;
        }
        public List<Point> GetOrigins()
        {
            return model.Origins;
        }
        public void SetPoints(List<Point> _points)
        {
            model.MarkedPoints = _points;
        }
        public void SetOrigins(List<Point> _origins)
        {
            model.Origins = _origins;
        }
        public List<Projection> GetProjections()
        {
            return model.Projections;
        }
        public Point GetLastPoint()
        {
            return model.MarkedPoints.Last();
        }
    }
}
