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
    public interface ISymbolView
    {
        event EventHandler<Symbol> AddSymbol;
        event EventHandler SaveSymbol;
        event EventHandler<Symbol> DeleteSymbol;

        void DrawSymbol(Graphics g);
        void Symbol_KeyDown(object sender, KeyEventArgs e);
        List<Symbol> GetSymbols();
    }
}
