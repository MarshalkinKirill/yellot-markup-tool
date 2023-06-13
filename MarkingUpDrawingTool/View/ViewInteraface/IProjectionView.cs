using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.UiService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View.ViewInteraface
{
    public interface IProjectionView
    {
        LayerService LayerService { get; set; }
        event EventHandler<Point> PointMarked;
        event EventHandler<Point> ChangePoint;
        event EventHandler SaveProjection;
        event EventHandler<Projection> DeleteProjection;

        void DrawProjection(Graphics g);
        void Projection_KeyDown(object sender, KeyEventArgs e);
        List<Projection> GetProjections();
    }
}
