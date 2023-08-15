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
        void MainForm_CheckedChanged();
        ToolStripMenuItem GetSaveTool();
        ToolStripMenuItem GetDeleteTool();
        //Prjection view
        ToolStripButton GetProjectionTool();
        ToolStripComboBox GetProjectionComboBox();

        //ProjectionRoi view
        ToolStripButton GetProjectionRoiTool();
        ToolStripComboBox GetProjectionRoiComboBox();

        //HoleView
        ToolStripButton GetHoleTool();
        ToolStripMenuItem GetHoleSearchTool();
        ToolStripComboBox GetHoleComboBox();

        //TableView
        ToolStripMenuItem GetTableTool();
        ToolStripMenuItem GetMainTableTool();
        ToolStripComboBox GetTableComboBox();

        //SizeView
        ToolStripMenuItem GetSizeTool();
        ToolStripMenuItem GetSizeAutoTool();
        ToolStripComboBox GetSizeComboBox();

        //ArrowView
        ToolStripMenuItem GetLinearArrowTool();
        ToolStripMenuItem GetAngularArrowTool();
        ToolStripMenuItem GetRadialArrowTool();
        ToolStripMenuItem GetDiametralArrowTool();
        ToolStripMenuItem GetReferenceArrowTool();
        ToolStripMenuItem GetConeArrowTool();
        ToolStripMenuItem GetChamferArrowTool();
        ToolStripComboBox GetArrowComboBox();

        //GapView
        ToolStripButton GetGapTool();
        ToolStripComboBox GetGapComboBox();

        //BorderView
        ToolStripButton GetBorderTool();

        //SymbolView
        ToolStripButton GetSymbolTool();
        ToolStripComboBox GetSymbolComboBox();
    }
}
