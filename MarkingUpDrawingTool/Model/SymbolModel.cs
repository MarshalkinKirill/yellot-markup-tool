using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Model
{
    public class Symbol
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private Point origin { get; set; }
        public Point Origin { get => origin; set => origin = value; }

        public Symbol()
        {
            name = string.Empty;
            start = new Point();
            end = new Point();
            origin = new Point();
        }

        public Symbol(Point start, Point end, Point origin)
        {
            name = string.Empty;
            this.start = start;
            this.end = end;
            this.origin = origin;
        }

        public Symbol(Point start, Point end, int num, Point origin)
        {
            name = "Symbol №" + num.ToString();
            this.start = start;
            this.end = end;
            this.origin = origin;
        }
    }
    public class SymbolModel
    {
        private List<Symbol> symbols { get; set; }
        public List<Symbol> Symbols { get => symbols; set => symbols = value; }
        private Symbol currentSymbol { get; set; }
        public Symbol CurrentSymbol { get => currentSymbol; set => currentSymbol = value; }

        public Symbol Symbol
        {
            get => default;
            set
            {
            }
        }

        public SymbolModel()
        {
            symbols = new List<Symbol>();
            currentSymbol = new Symbol();
        }

        public void SaveSymbol()
        {
            Point start = new Point(currentSymbol.Start.X, currentSymbol.Start.Y);
            Point end = new Point(currentSymbol.End.X, currentSymbol.End.Y);
            Point origin = new Point(currentSymbol.Origin.X, currentSymbol.Origin.Y);
            Symbol symbol = new Symbol(start, end, this.symbols.Count + 1, origin);
            symbols.Add(symbol);
        }

        public void DeleteSymbol(Symbol symbol)
        {
            symbols.Remove(symbol);
        }
    }
}
