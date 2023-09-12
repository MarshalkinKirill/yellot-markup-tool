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
            view.AddBorder += AddBorder;
        }

        private void SaveBorder(object sender, Border border)
        {
            model.SaveBorder(border);
        }
        private void AddBorder(object sender, Border border)
        {
            model.AddBorder(border);
        }
        public Border GetMarkedBorder()
        {
            return model.Border;
        }

        public Border GetCurrentBorder()
        {
            return model.CurrentBorder;
        }
        public void SetBorders(Border border)
        {
            model.CurrentBorder = border;
        }

        public void CleanMarkedBorder()
        {
            model.Border = new Border();
        }
    }
}
