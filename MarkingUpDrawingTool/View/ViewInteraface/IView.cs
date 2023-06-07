using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.UiService;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CvPoint = OpenCvSharp.Point;
using Point = System.Drawing.Point;

namespace MarkingUpDrawingTool.View.ViewInterface
{
    public interface IView
    {
        LayerService LayerService { get; set; }
        Layer GetImageLayer();
        //Prjection view
        event EventHandler<Point> PointMarked;
        event EventHandler SaveProjection;
        event EventHandler<Projection> DeleteProjection;
        void DrawLine(Graphics g);

        //HoleView
        event EventHandler<Hole> AddHole;
        event EventHandler SaveHole;
        event EventHandler<Hole> DeleteHole;

        //TableView
        event EventHandler<Table> AddTable;
        event EventHandler<TableNote> AddTableNote;
        event EventHandler SaveTable;
        event EventHandler<Table> DeleteTable;

        //SizeView
        ToolStripMenuItem GetSizeTool();
        ToolStripMenuItem GetSizeAutoTool();
        ToolStripMenuItem GetSizeDeleteTool();
        ToolStripMenuItem GetSizeSaveTool();
        ToolStripComboBox GetSizeComboBox();

        //ArrowView
        ToolStripButton GetArrowTool();
        ToolStripMenuItem GetArrowDeleteTool();
        ToolStripMenuItem GetArrowSaveTool();
        ToolStripComboBox GetArrowComboBox();
    }
}
