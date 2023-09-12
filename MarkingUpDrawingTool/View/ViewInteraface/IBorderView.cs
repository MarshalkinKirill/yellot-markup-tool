using MarkingUpDrawingTool.Model;
using OpenCvSharp.Internal.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View.ViewInteraface
{
    public interface IBorderView
    {
        event EventHandler<Border> SaveBorder;
        event EventHandler<Border> AddBorder;

        void DrawBorder(Graphics g);
        List<Border> GetBorder();
        void Border_KeyDown(object sender, KeyEventArgs e);
        void SetBorders(Border border);
    }
}
