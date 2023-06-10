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
    public interface ITableView
    {
        event EventHandler<Table> AddTable;
        event EventHandler<TableNote> AddTableNote;
        event EventHandler SaveTable;
        event EventHandler<Table> DeleteTable;

        void DrawTable(Graphics g);
        void Table_KeyDown(object sender, KeyEventArgs e);
        void SetTableNote(TableNote note);
        List<Table> GetTables();
    }
}
