using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View;
using MarkingUpDrawingTool.View.ViewInteraface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Presenter
{
    public class ArrowPresenter
    {
        private IArrowView view;
        private ArrowModel model;

        public ArrowPresenter(IArrowView view)
        {
            this.view = view;
            model = new ArrowModel();

            // Подписываемся на события
            view.AddArrow += AddArrow;
            view.SaveArrow += SaveArrow;
            view.DeleteArrow += DeleteArrow;
        }

        private void AddArrow(object sender, Arrow arrow)
        {
            model.CurrentArrow = arrow;
        }

        private void SaveArrow(object sender, EventArgs e)
        {
            model.SaveArrow();
        }

        private void DeleteArrow(object sender, Arrow arrow)
        {
            model.DeleteArrow(arrow);
        }

        public List<Arrow> GetArrows()
        {
            return model.Arrows;
        }

        public Arrow GetMarkedArrow()
        {
            return model.CurrentArrow;
        }

        public void CleanMarkedArrow()
        {
            model.CurrentArrow = new Arrow();
        }
    }
}
