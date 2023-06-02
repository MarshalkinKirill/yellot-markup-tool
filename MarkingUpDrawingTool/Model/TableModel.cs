using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Model
{
    public class Table
    {
        private string name { get; set; }
        public string Name { get => name; set => name = value; }
        private Point start { get; set; }
        public Point Start { get => start; set => start = value; }
        private Point end { get; set; }
        public Point End { get => end; set => end = value; }
        private string mass { get; set; }
        private string Mass { get => mass; set => mass = value; }    
        private string scale { get; set; }
        public string Scale { get => scale; set => scale = value; }
        private string partName { get; set; }
        public string PartName { get => partName; set => partName = value; }
        public Table()
        { 
            name = string.Empty;
            start = new Point();
            end = new Point();
            mass = String.Empty;
            scale = String.Empty;
            partName = String.Empty;
        }
        public Table(Point _x, Point _y)
        {
            name = string.Empty;
            start = _x;
            end = _y;
            mass = String.Empty;
            scale = String.Empty;
            partName = String.Empty;
        }
        public Table (Point _x, Point _y, string _mass, string _scale, string _partName, string _name)
        {
            name = string.Empty;
            start = _x;
            end = _y;
            mass = _mass;
            scale = _scale;
            partName = _partName;
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
    }
}
