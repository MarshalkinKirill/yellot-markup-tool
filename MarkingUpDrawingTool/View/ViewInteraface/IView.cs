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
        //Prjection view
        ToolStripButton GetProjectionTool();
        ToolStripMenuItem GetProjectionSaveTool();
        ToolStripMenuItem GetProjectionDeleteTool();
        ToolStripComboBox GetProjectionComboBox();

        //ProjectionRoi view
        ToolStripButton GetProjectionRoiTool();
        ToolStripMenuItem GetProjectionRoiSaveTool();
        ToolStripMenuItem GetProjectionRoiDeleteTool();
        ToolStripComboBox GetProjectionRoiComboBox();

        //HoleView
        ToolStripButton GetHoleTool();
        ToolStripMenuItem GetHoleSearchTool();
        ToolStripMenuItem GetHoleSaveTool();
        ToolStripMenuItem GetHoleDeleteTool();
        ToolStripComboBox GetHoleComboBox();

        //TableView
        ToolStripMenuItem GetTableTool();
        ToolStripMenuItem GetMainTableTool();
        ToolStripMenuItem GetTableSaveTool();
        ToolStripMenuItem GetTableDeleteTool();
        ToolStripComboBox GetTableComboBox();

        //SizeView
        ToolStripMenuItem GetSizeTool();
        ToolStripMenuItem GetSizeAutoTool();
        ToolStripMenuItem GetSizeDeleteTool();
        ToolStripMenuItem GetSizeSaveTool();
        ToolStripComboBox GetSizeComboBox();

        //ArrowView
        ToolStripMenuItem GetLinearArrowTool();
        ToolStripMenuItem GetAngularArrowTool();
        ToolStripMenuItem GetRadialArrowTool();
        ToolStripMenuItem GetDiametralArrowTool();
        ToolStripMenuItem GetReferenceArrowTool();
        ToolStripMenuItem GetConeArrowTool();
        ToolStripMenuItem GetChamferArrowTool();
        ToolStripMenuItem GetArrowDeleteTool();
        ToolStripMenuItem GetArrowSaveTool();
        ToolStripComboBox GetArrowComboBox();

        //GapView
        ToolStripButton GetGapTool();
        ToolStripMenuItem GetGapSaveTool();
        ToolStripMenuItem GetGapDeleteTool();
        ToolStripComboBox GetGapComboBox();

        //BorderView
        ToolStripButton GetBorderTool();
    }
}
