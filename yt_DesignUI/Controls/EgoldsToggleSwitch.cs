using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EgoldsUI;

namespace yt_DesignUI
{
    public class EgoldsToggleSwitch : Control
    {
        #region -- Переменные --

        Rectangle rect;

        int TogglePosX_ON;
        int TogglePosX_OFF;

        Animation ToggleAnim;

        #endregion

        #region -- Свойства --

        public bool Checked { get; set; } = false;

        public Color BackColorON { get; set; } = FlatColors.Red;

        #endregion

        public EgoldsToggleSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(40, 15);

            Font = new Font("Verdana", 9F, FontStyle.Regular);
            BackColor = Color.White;

            rect = new Rectangle(1, 1, Width - 3, Height - 3);
            TogglePosX_OFF = rect.X;
            TogglePosX_ON = rect.Width - rect.Height;

            ToggleAnim = new Animation();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            ToggleAnim.Value = Checked == true ? TogglePosX_ON : TogglePosX_OFF;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Size = new Size(40, 15);
            rect = new Rectangle(1, 1, Width - 3, Height - 3);
            TogglePosX_OFF = rect.X;
            TogglePosX_ON = rect.Width - rect.Height;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.Clear(Parent.BackColor);

            Pen TSPen = new Pen(FlatColors.GrayDark, 3);
            Pen TSPenToggle = new Pen(FlatColors.GrayDark, 3);

            GraphicsPath rectGP = RoundedRectangle(rect, rect.Height);
            Rectangle rectToggle = new Rectangle((int)ToggleAnim.Value, rect.Y, rect.Height, rect.Height);

            graph.DrawPath(TSPen, rectGP);

            if(Checked == true)
            {
                if (Animator.IsWork == false)
                {
                    rectToggle.Location = new Point(TogglePosX_ON, rect.Y);
                }

                graph.FillPath(new SolidBrush(BackColorON), rectGP);
            }
            else
            {
                if (Animator.IsWork == false)
                {
                    rectToggle.Location = new Point(TogglePosX_OFF, rect.Y);
                }

                graph.FillPath(new SolidBrush(BackColor), rectGP);
            }
            
            graph.DrawEllipse(TSPenToggle, rectToggle);
            graph.FillEllipse(new SolidBrush(Color.White), rectToggle);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            SwitchToggle();
        }

        private void SwitchToggle()
        {
            if(Checked == true)
            {
                ToggleAnim = new Animation("Toggle_" + Handle, Invalidate, ToggleAnim.Value, TogglePosX_OFF);
            }
            else
            {
                ToggleAnim = new Animation("Toggle_" + Handle, Invalidate, ToggleAnim.Value, TogglePosX_ON);
            }

            Checked = !Checked;

            ToggleAnim.StepDivider = 8;
            Animator.Request(ToggleAnim, true);
        }

        private GraphicsPath RoundedRectangle(Rectangle rect, int RoundSize)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddArc(rect.X, rect.Y, RoundSize, RoundSize, 180, 90);
            gp.AddArc(rect.X + rect.Width - RoundSize, rect.Y, RoundSize, RoundSize, 270, 90);
            gp.AddArc(rect.X + rect.Width - RoundSize, rect.Y + rect.Height - RoundSize, RoundSize, RoundSize, 0, 90);
            gp.AddArc(rect.X, rect.Y + rect.Height - RoundSize, RoundSize, RoundSize, 90, 90);

            gp.CloseFigure();

            return gp;
        }
    }
}
