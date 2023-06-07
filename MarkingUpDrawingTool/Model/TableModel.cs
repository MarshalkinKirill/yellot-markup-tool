using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace MarkingUpDrawingTool.Model
{
    public class TableNote
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private string mass { get; set; }
        public string Mass { get => mass; set => mass = value; }
        private string scale { get; set; }
        public string Scale { get => scale; set => scale = value; }

        public TableNote() 
        {
            name = string.Empty;
            mass = string.Empty;
            scale = string.Empty;
        }
        public TableNote(string _name, string _mass, string _scale)
        {
            name = _name;
            mass = _mass;
            scale = _scale;
        }
    }
    public class Table
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private TableNote tableNote { get; set; }
        public TableNote TableNote { get => tableNote; set => tableNote = value; }
        public Table()
        { 
            name = string.Empty;
            start = new Point();
            end = new Point();
            tableNote = new TableNote();
        }
        public Table(Point _x, Point _y)
        {
            name = String.Empty;
            start = _x;
            end = _y;
            tableNote = new TableNote();
        }
        public Table (Point _x, Point _y, string _mass, string _scale, string _partName, int num)
        {
            name = "Таблица №" + num.ToString();
            start = _x;
            end = _y;
            tableNote = new TableNote(_partName, _mass, _scale);
        }

        public Table(Point _x, Point _y, TableNote _tableNote, int num)
        {
            name = "Таблица №" + num.ToString();
            start = _x;
            end = _y;
            tableNote = _tableNote;
        }
    }
    public class TableModel
    {   
        private List<Table> tables { get; set; }
        public List<Table> Tables { get => tables; set => tables = value; }
        private Table currentTable { get; set; }
        public Table CurrentTable { get => currentTable; set => currentTable = value; }

        public TableModel() 
        {
            tables = new List<Table>();
            currentTable = new Table();
        }

        public void SaveTable()
        {
            Point x = new Point(currentTable.Start.X, currentTable.Start.Y);
            Point y = new Point(currentTable.End.X, currentTable.End.Y);
            TableNote note = currentTable.TableNote;
            Table table = new Table(x, y, note, this.Tables.Count + 1);
            tables.Add(table);
        }

        public void DeleteTable(Table table)
        {
            this.Tables.Remove(table);
        }
    }
}
