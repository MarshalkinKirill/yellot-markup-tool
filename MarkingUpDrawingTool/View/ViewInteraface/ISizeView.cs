using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.UiService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrSize = System.Drawing.Size;
using Size = MarkingUpDrawingTool.Model.Size;

namespace MarkingUpDrawingTool.View.ViewInteraface
{
    public interface ISizeView
    {
        LayerService LayerService { get; set; }
        Layer ImageLayer { get; set; }
        event EventHandler<Size> AddSize;
        event EventHandler<string> AddSizeNote;
        event EventHandler DetectSizeNote;
        event EventHandler SaveSize;
        event EventHandler<Size> DeleteSize;
        void SetSizeNote(string sizeNote);
        void SaveSizeNoteForm(object sender, EventArgs e);
        void DrawRectangle(Graphics g);
        void Size_KeyDown(object sender, KeyEventArgs e);
        List<Size> GetSizes();
    }
}
