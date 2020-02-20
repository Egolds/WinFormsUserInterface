using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EgoldsUI;

namespace yt_DesignUI
{
    public class EgoldsCard : Control
    {
        #region -- Переменные --

        Animation animCurtain;
        private float CurtainHeight;
        private int CurtainMinHeight = 50;

        private bool MouseEntered = false;
        private bool MousePressed = false;

        StringFormat SF = new StringFormat();

        #endregion

        #region -- Свойства --

        public string TextHeader { get; set; } = "Header";
        public Font FontHeader { get; set; } = new Font("Verdana", 12F, FontStyle.Bold);
        public Color ForeColorHeader { get; set; } = Color.White;

        public string TextDescrition { get; set; } = "Your description text for this control";
        public Font FontDescrition { get; set; } = new Font("Verdana", 8.25F, FontStyle.Regular);
        public Color ForeColorDescrition { get; set; } = Color.Black;

        public Color BackColorCurtain { get; set; } = FlatColors.Red;

        #endregion

        public EgoldsCard()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(250, 200);
            CurtainHeight = Height - 60;

            Font = new Font("Verdana", 9F, FontStyle.Regular);
            BackColor = Color.White;

            animCurtain = new Animation();
            animCurtain.Value = CurtainHeight;

            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Near;

            Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Rectangle rectCurtain = new Rectangle(0, 0, Width - 1, (int)animCurtain.Value);
            Rectangle rectDescription = new Rectangle(15, CurtainMinHeight + 5, rect.Width - 30, rect.Height - 100);

            //Фон
            graph.FillRectangle(new SolidBrush(BackColor), rect);

            //Шторка
            graph.DrawRectangle(new Pen(BackColorCurtain), rectCurtain);
            graph.FillRectangle(new SolidBrush(BackColorCurtain), rectCurtain);

            //Обводка
            graph.DrawRectangle(new Pen(FlatColors.Gray), rect);

            if(animCurtain.Value == CurtainMinHeight)
            {
                graph.DrawString(TextDescrition, FontDescrition, new SolidBrush(ForeColorDescrition), rectDescription, SF);
                //debug: //graph.DrawRectangle(new Pen(Color.Blue), rectDescription);
            }

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), 15, Height - 37);
            graph.DrawString(TextHeader, FontHeader, new SolidBrush(ForeColorHeader),
                new Rectangle(15, 15, rectCurtain.Width, rectCurtain.Height));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (Height <= 100)
                Height = 100;
            if (Width <= 100)
                Width = 100;

            CurtainHeight = Height - 60;

            animCurtain = new Animation();
            animCurtain.Value = CurtainHeight;

            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            MouseEntered = true;

            DoCurtainAnimation();

            //Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            DoCurtainAnimation();

            //Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();
        }

        private void DoCurtainAnimation()
        {
            if(MouseEntered == true)
            {
                animCurtain = new Animation("Curtain_" + Handle, Invalidate, animCurtain.Value, CurtainMinHeight);
            }
            else
            {
                animCurtain = new Animation("Curtain_" + Handle, Invalidate, animCurtain.Value, CurtainHeight);
            }

            Animator.Request(animCurtain, true);
        }
    }
}
