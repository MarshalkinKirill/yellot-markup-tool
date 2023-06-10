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
    public interface IGapView
    {
        event EventHandler<Gap> AddGap;
        event EventHandler SaveGap;
        event EventHandler<Gap> DeleteGap;

        void DrawGap(Graphics g);
        void Gap_KeyDown(object sender, KeyEventArgs e);
        List<Gap> GetGaps();
    }
}
