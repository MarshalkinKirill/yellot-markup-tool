﻿using MarkingUpDrawingTool.Model;
using MarkingUpDrawingTool.Presenter;
using MarkingUpDrawingTool.View.UiService;
using MarkingUpDrawingTool.View.ViewInteraface;
using MarkingUpDrawingTool.View.ViewInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkingUpDrawingTool.View
{
    public class BorderView : IBorderView
    {
        private IView mainForm;
        private BorderPresenter borderPresenter;
        private LayerService layerService { get; set; }
        public LayerService LayerService { get => layerService; set => layerService = value; }
        private Border currentBorder { get; set; }
        public Border CurrentBorder { get => currentBorder; set => currentBorder = value; }

        private ToolStripButton borderTool;

        public event EventHandler<Border> SaveBorder;

        public BorderView(IView mainForm)
        {
            this.mainForm = mainForm;
            borderPresenter = new BorderPresenter(this);
            layerService = mainForm.LayerService;

            borderTool = mainForm.GetBorderTool();

            borderTool.Click += BorderTool_Click;
        }

        private void BorderTool_Click(object sender, EventArgs e)
        {
            layerService.DrawBorderMod = !layerService.DrawBorderMod;
            SaveDrawBorderMod();
        }

        private void SaveDrawBorderMod()
        {
            if (layerService.DrawBorderMod)
            {
                borderTool.Checked = true;

                layerService.MouseDown += layerServiceBorder_MouseDown;
                layerService.MouseUp += layerServiceBorder_MouseUp;
                layerService.MouseMove += layerServiceBorder_MouseMove;
            }
            else
            {
                borderTool.Checked = false;

                layerService.MouseDown -= layerServiceBorder_MouseDown;
                layerService.MouseUp -= layerServiceBorder_MouseUp;
                layerService.MouseMove -= layerServiceBorder_MouseMove;
            }
        }

        private void layerServiceBorder_MouseDown(object sender, MouseEventArgs e)
        {
            borderPresenter.CleanMarkedBorder();
            if (layerService.DrawBorderMod && e.Button == MouseButtons.Left)
            {
                layerService.StartPoint = e.Location;
            }
            if (layerService.DrawBorderMod && e.Button == MouseButtons.Right)
            {
                if (layerService.EndPoint != Point.Empty)
                {
                    layerService.StartPoint = Point.Empty;
                    layerService.EndPoint = Point.Empty;
                }
            }
        }

        private void layerServiceBorder_MouseUp(object sender, MouseEventArgs e)
        {
            if (layerService.DrawBorderMod && layerService.StartPoint != Point.Empty && e.Button == MouseButtons.Left)
            {
                layerService.EndPoint = e.Location;
                SaveBorder?.Invoke(this, new Border(layerService.StartPoint, layerService.EndPoint));

                currentBorder = borderPresenter.GetMarkedBorder();

                layerService.Invalidate();
                layerService.StartPoint = Point.Empty;
                layerService.EndPoint = Point.Empty;
            }
        }

        private void layerServiceBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (layerService.DrawBorderMod && layerService.StartPoint != Point.Empty)
            {
                layerService.EndPoint = e.Location;
                SaveBorder?.Invoke(this, new Border(layerService.StartPoint, layerService.EndPoint));
                currentBorder = borderPresenter.GetMarkedBorder();
                layerService.Invalidate();
            }
        }

        public void DrawBorder(Graphics g)
        {
            Pen pen = new Pen(Color.Red, 3);
            pen.DashStyle = DashStyle.Dot;
            currentBorder = borderPresenter.GetMarkedBorder();
            if (!layerService.DrawBorderMod)
            {
                Point start = currentBorder.Start;
                Point end = currentBorder.End;
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
            }
            else
            {
                pen.Color = Color.Purple;
                Point start = currentBorder.Start;
                Point end = currentBorder.End;
                int width = Math.Abs(start.X - end.X);
                int height = Math.Abs(start.Y - end.Y);
                int x = Math.Min(start.X, end.X);
                int y = Math.Min(start.Y, end.Y);
                pen.DashStyle = DashStyle.Dot;
                // Рисование прямоугольника с пунктирными границами
                g.DrawRectangle(pen, x, y, width, height);
            }
        }

        public List<Border> GetBorder()
        {
            List<Border> borderList = new List<Border>();
            borderList.Add(borderPresenter.GetMarkedBorder());
            return borderList ;
        }
    }
}