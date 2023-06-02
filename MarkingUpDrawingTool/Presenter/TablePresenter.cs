using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Presenter
{
    public class TablePresenter
    {
        private IView view;
        private TableModel model;

        public TablePresenter(IView _view)
        {
            view = _view;
            model = new TableModel();

            //Подписка на события в форме
            view.TableMarked += TableMarked;
            view.SaveTable += SaveTable;
            view.DeleteTable += DeleteTable;
        }

        public void TableMarked(object sender, Table table)
        {

        }

        public void SaveTable(object sender, EventArgs e)
        {

        }

        public void DeleteTable(object sender, Table table)
        {

        }
    }
}
