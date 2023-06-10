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
    public interface IHoleView
    {
        event EventHandler<Hole> AddHole;
        event EventHandler SaveHole;
        event EventHandler<Hole> DeleteHole;

        void DrawHole(Graphics g);
        void Hole_KeyDown(object sender, KeyEventArgs e);
        List<Hole> GetHoles();
    }
}
