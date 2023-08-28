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
    public interface IHoleView
    {
        LayerService LayerService { get; set; }
        event EventHandler<Hole> AddHole;
        event EventHandler SaveHole;
        event EventHandler<Hole> DeleteHole;

        void DrawHole(Graphics g);
        void Hole_KeyDown(object sender, KeyEventArgs e);
        List<Hole> GetHoles();
        void SetHoles(List<Hole> holes);
    }
}
