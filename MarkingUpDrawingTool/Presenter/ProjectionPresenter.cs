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
            view.SaveProjection += SaveProjection;
            view.DeleteProjection += DeleteProjection;
        }

        private void OnPointMarked(object sender, Point point)
        {
            model.MarkedPoints.Add(point);
            Console.WriteLine(point.ToString());
        }
        private void SaveProjection(object sender, EventArgs e) 
        {
            model.SaveProjection();
            Console.WriteLine(model.Projections.Count);
        }

        private void DeleteProjection(object sender, Projection projection)
        {
            model.Projections.Remove(projection);
        }

        private void ChooseProjection(object sender, EventArgs e)
        {

        }

        public List<Point> GetPoints() 
        {
            return model.MarkedPoints;
        }

        public void SetPoints(List<Point> _points)
        {
            model.MarkedPoints = _points;
        }

        public List<Projection> GetProjections()
        {
            return model.Projections;
        }
    }
}
