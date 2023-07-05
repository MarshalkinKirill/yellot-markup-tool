using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.ViewInteraface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Presenter
{
    public class SymbolPresenter
    {
        private ISymbolView view;
        private SymbolModel model;

        public SymbolPresenter(ISymbolView view)
        {
            this.view = view;
            model = new SymbolModel();

            //Подписка на события в форме
            view.AddSymbol += AddSymbol;
            view.SaveSymbol += SaveSymbol;
            view.DeleteSymbol += DeleteSymbol;
        }

        public View.SymbolView SymbolView
        {
            get => default;
            set
            {
            }
        }

        public SymbolModel SymbolModel
        {
            get => default;
            set
            {
            }
        }

        private void AddSymbol(object sender, Symbol symbol)
        {
            model.CurrentSymbol = symbol;
        }

        private void SaveSymbol(object sender, EventArgs e)
        {
            model.SaveSymbol();
        }

        private void DeleteSymbol(object sender, Symbol symbol)
        {
            model.DeleteSymbol(symbol);
        }

        public List<Symbol> GetSymbols()
        {
            return model.Symbols;
        }

        public Symbol GetMarkedSymbol()
        {
            return model.CurrentSymbol;
        }

        public void CleanMarkedSymbol()
        {
            model.CurrentSymbol = new Symbol();
        }
    }
}
