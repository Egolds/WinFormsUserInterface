using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace yt_DesignUI
{
    public class yt_Button : Control
    {
        private StringFormat SF = new StringFormat();

        private bool MouseEntered = false;
        private bool MousePressed = false;

        Animation CurtainButtonAnim = new Animation();
        Animation RippleButtonAnim = new Animation();

        Point ClickLocation = new Point();

        public yt_Button()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(100, 30);

            BackColor = Color.Tomato;
            ForeColor = Color.White;

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rectCurtain = new Rectangle(0, 0, (int)CurtainButtonAnim.Value, Height - 1);
            Rectangle rectRipple = new Rectangle(
                ClickLocation.X - (int)RippleButtonAnim.Value / 2,
                ClickLocation.Y - (int)RippleButtonAnim.Value / 2,
                (int)RippleButtonAnim.Value,
                (int)RippleButtonAnim.Value
                );

            // Основной прямоугольник (Фон)
            graph.DrawRectangle(new Pen(BackColor), rect);
            graph.FillRectangle(new SolidBrush(BackColor), rect);

            // Рисуем доп. прямоугольник (Наша шторка)
            graph.DrawRectangle(new Pen(Color.FromArgb(60, Color.White)), rectCurtain);
            graph.FillRectangle(new SolidBrush(Color.FromArgb(60, Color.White)), rectCurtain);

            // Стандартное рисование праямоугольника при клике
            //if (MousePressed)
            //{
            //    graph.DrawRectangle(new Pen(Color.FromArgb(30, Color.Black)), rect);
            //    graph.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rect);
            //}

            // Ripple Effect - Волна
            if(RippleButtonAnim.Value > 0 && RippleButtonAnim.Value < RippleButtonAnim.TargetValue)
            {
                graph.DrawEllipse(new Pen(Color.FromArgb(30, Color.Black)), rectRipple);
                graph.FillEllipse(new SolidBrush(Color.FromArgb(30, Color.Black)), rectRipple);
            }
            else if(RippleButtonAnim.Value == RippleButtonAnim.TargetValue)
            {
                
                RippleButtonAnim.Value = 0;
            }

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
        }

        private void ButtonRippleAction()
        {
            RippleButtonAnim = new Animation("ButtonRipple_" + Handle, Invalidate, 0, Width);

            RippleButtonAnim.StepDivider = 14;
            Animator.Request(RippleButtonAnim, true);
        }

        private void ButtonCurtainAction()
        {
            if (MouseEntered)
            {
                CurtainButtonAnim = new Animation("ButtonCurtain_" + Handle, Invalidate, CurtainButtonAnim.Value, Width - 1);
            }
            else
            {
                CurtainButtonAnim = new Animation("ButtonCurtain_" + Handle, Invalidate, CurtainButtonAnim.Value, 0);
            }

            CurtainButtonAnim.StepDivider = 8;
            Animator.Request(CurtainButtonAnim, true);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            ButtonCurtainAction();

            //Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            ButtonCurtainAction();

            //Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            CurtainButtonAnim.Value = CurtainButtonAnim.TargetValue;

            ClickLocation = e.Location;
            ButtonRippleAction();

            //Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            //Invalidate();
        }
    }
}
