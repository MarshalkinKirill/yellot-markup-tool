using MarkingUpDrawingTool.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View.ViewInteraface
{
    public interface IProjectionRoiView
    {
        event EventHandler<ProjectionRoi> AddProjectionRoi;
        event EventHandler SaveProjectionRoi;
        event EventHandler<ProjectionRoi> DeleteProjectionRoi;

        void DrawProjectionRoi(Graphics g);
        void ProjectionRoi_KeyDown(object sender, KeyEventArgs e);
        List<ProjectionRoi> GetProjectionRois();
        void SetProjectionRois(List<ProjectionRoi> projectionRois);
    }
}
