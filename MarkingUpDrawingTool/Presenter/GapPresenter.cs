using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.View;
using MarkingUpDrawingTool.View.ViewInteraface;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkingUpDrawingTool.Presenter
{
    public class GapPresenter
    {
        private IGapView view;
        private GapModel model;

        public GapPresenter(IGapView view)
        {
            this.view = view;
            model = new GapModel();

            // Подписываемся на событие отметки точки
            view.AddGap += AddGap;
            view.SaveGap += SaveGap;
            view.DeleteGap += DeleteGap;
        }

        private void AddGap(object sender, Gap Gap)
        {
            model.CurrentGap = Gap;
        }

        private void SaveGap(object sender, EventArgs e)
        {
            model.SaveGap();
        }

        private void DeleteGap(object sender, Gap Gap)
        {
            model.DeleteGap(Gap);
        }

        public List<Gap> GetGaps()
        {
            return model.Gaps;
        }

        public Gap GetMarkedGap()
        {
            return model.CurrentGap;
        }

        public void CleanMarkedGap()
        {
            model.CurrentGap = new Gap();
        }
    }
}
