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
    public interface IArrowView
    {
        event EventHandler<Arrow> AddArrow;
        event EventHandler SaveArrow;
        event EventHandler<Arrow> DeleteArrow;

        void DrawArrow(Graphics g);
        void Arrow_KeyDown(object sender, KeyEventArgs e);
        List<Arrow> GetArrows();
    }
}
