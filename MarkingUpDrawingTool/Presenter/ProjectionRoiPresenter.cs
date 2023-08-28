using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.ViewInteraface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MarkingUpDrawingTool.Presenter
{
    public class ProjectionRoiPresenter
    {
        private IProjectionRoiView view;
        private ProjectionRoiModel model;

        public ProjectionRoiPresenter(IProjectionRoiView view)
        {
            this.view = view;
            model = new ProjectionRoiModel();

            //Подписка на события в форме
            view.AddProjectionRoi += AddProjectionRoi;
            view.SaveProjectionRoi += SaveProjectionRoi;
            view.DeleteProjectionRoi += DeleteProjectionRoi;
        }

        private void AddProjectionRoi(object sender, ProjectionRoi projectionRoi)
        {
            model.CurrentProjectionRoi = projectionRoi;
        }

        private void SaveProjectionRoi(object sender, EventArgs e)
        {
            model.SaveProjectionRoi();
        }

        private void DeleteProjectionRoi(object sender, ProjectionRoi projectionRoi)
        {
            model.DeleteProjectionRoi(projectionRoi);
        }

        public List<ProjectionRoi> GetProjectionRois()
        {
            return model.ProjectionRois;
        }

        public void SetProjectionRois(List<ProjectionRoi> projectionRois)
        {
            model.ProjectionRois = projectionRois;
        }

        public ProjectionRoi GetMarkedProjectionRoi() 
        {
            return model.CurrentProjectionRoi;
        }

        public void CleanMarkedProjectionRoi()
        {
            model.CurrentProjectionRoi = new ProjectionRoi();
        }
    }
}
