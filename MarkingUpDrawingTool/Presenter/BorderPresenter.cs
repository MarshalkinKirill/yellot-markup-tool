using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View;
using MarkingUpDrawingTool.View.ViewInteraface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace MarkingUpDrawingTool.Presenter
{
    public class BorderPresenter
    {
        private IBorderView view;
        private BorderModel model;

        public BorderPresenter(IBorderView view)
        {
            this.view = view;
            model = new BorderModel();

            // Подписываемся на события
            view.SaveBorder += SaveBorder;
        }

        public BorderView BorderView
        {
            get => default;
            set
            {
            }
        }

        public BorderModel BorderModel
        {
            get => default;
            set
            {
            }
        }

        private void SaveBorder(object sender, Border border)
        {
            Console.WriteLine(border.Start.ToString() + "" + border.End.ToString());

            model.SaveBorder(border);
            Console.WriteLine(model.CurrentBorder.Start.ToString() + "" + model.CurrentBorder.End.ToString());
        }

        public Border GetMarkedBorder()
        {
            return model.CurrentBorder;
        }

        public void CleanMarkedBorder()
        {
            model.CurrentBorder = new Border();
        }
    }
}
