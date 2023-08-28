using MarkingUpDrawingTool.Model;
using OpenCvSharp.Internal.Vectors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.View.ViewInteraface
{
    public interface IBorderView
    {
        event EventHandler<Border> SaveBorder;

        void DrawBorder(Graphics g);
        List<Border> GetBorder();
        void SetBorders(Border border);
    }
}
