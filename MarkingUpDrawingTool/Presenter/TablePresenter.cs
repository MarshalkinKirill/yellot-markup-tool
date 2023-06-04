﻿using MarkingUpDrawingTool.Model;
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
            view.AddTable += AddTable;
            view.AddTableNote += AddTableNote;
            view.SaveTable += SaveTable;
            view.DeleteTable += DeleteTable;
        }

        public void AddTable(object sender, Table table)
        {
            model.CurrentTable = table;
        }

        public void SaveTable(object sender, EventArgs e)
        {
            model.SaveTable();
            Console.WriteLine(model.Tables.Count);
        }

        public void DeleteTable(object sender, Table table)
        {
            model.DeleteTable(table);
        }

        public void AddTableNote(object sender, TableNote tableNote)
        {
            model.CurrentTable.TableNote = new TableNote(tableNote.Name, tableNote.Mass, tableNote.Scale);
            Console.WriteLine(model.CurrentTable.Start.ToString() + " " + model.CurrentTable.End.ToString());
            Console.WriteLine(model.CurrentTable.TableNote.Name);
        }

        public List<Table> GetTables()
        {
            return model.Tables;
        }

        public Table GetMarkedTable()
        {
            return model.CurrentTable;
        }

        public void CleanMarkedTable()
        {
            model.CurrentTable = new Table();
        }
    }
}
