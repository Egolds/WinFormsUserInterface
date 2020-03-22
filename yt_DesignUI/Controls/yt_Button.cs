using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace yt_DesignUI
{
    public class yt_Button : Control
    {
        #region -- Свойства --

        [Description("Текст, отображаемый при наведении курсора")]
        public string TextHover { get; set; }

        private bool roundingEnable = false;
        [Description("Вкл/Выкл закругление объекта")]
        public bool RoundingEnable
        {
            get => roundingEnable;
            set
            {
                roundingEnable = value;
                Refresh();
            }
        }

        private int roundingPercent = 100;
        [DisplayName("Rounding [%]")]
        [DefaultValue(100)]
        [Description("Указывает радиус закругления объекта в процентном соотношении")]
        public int Rounding
        {
            get => roundingPercent;
            set
            {
                if (value >= 0 && value <= 100)
                {
                    roundingPercent = value;

                    Refresh();
                }
            }
        }

        [Description("Вкл/Выкл эффект нажатия кнопки.")]
        public bool UseDownPressEffectOnClick { get; set; }

        #endregion

        #region -- Переменные --

        private StringFormat SF = new StringFormat();

        private bool MouseEntered = false;
        private bool MousePressed = false;

        Animation CurtainButtonAnim = new Animation();
        Animation RippleButtonAnim = new Animation();
        Animation TextSlideAnim = new Animation();

        Point ClickLocation = new Point();

        #endregion

        public yt_Button()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(100, 30);

            Font = new Font("Verdana", 8.25F, FontStyle.Regular);

            Cursor = Cursors.Hand;

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
            Rectangle rectText = new Rectangle((int)TextSlideAnim.Value, rect.Y, rect.Width, rect.Height);
            Rectangle rectTextHover = new Rectangle((int)TextSlideAnim.Value - rect.Width, rect.Y, rect.Width, rect.Height);

            // Закругление
            float roundingValue = 0.1F;
            if (RoundingEnable && roundingPercent > 0)
            {
                roundingValue = Height / 100F * roundingPercent;
            }
            GraphicsPath rectPath = Drawer.RoundedRectangle(rect, roundingValue);

            // Основной прямоугольник (Фон)
            graph.DrawPath(new Pen(BackColor), rectPath);
            graph.FillPath(new SolidBrush(BackColor), rectPath);

            graph.SetClip(rectPath);

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
            if (RippleButtonAnim.Value > 0 && RippleButtonAnim.Value < RippleButtonAnim.TargetValue)
            {
                graph.DrawEllipse(new Pen(Color.FromArgb(30, Color.Black)), rectRipple);
                graph.FillEllipse(new SolidBrush(Color.FromArgb(30, Color.Black)), rectRipple);
            }
            else if (RippleButtonAnim.Value == RippleButtonAnim.TargetValue)
            {
                // Тут можно добавить проверку MousePressed, если false тогда обнуляем
                if (MousePressed == false)
                {
                    RippleButtonAnim.Value = 0;
                }
                else
                {
                    graph.DrawEllipse(new Pen(Color.FromArgb(30, Color.Black)), rectRipple);
                    graph.FillEllipse(new SolidBrush(Color.FromArgb(30, Color.Black)), rectRipple);
                }
            }

            // Рисуем текст
            if (string.IsNullOrEmpty(TextHover))
            {
                graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, SF);
            }
            else
            {
                graph.DrawString(Text, Font, new SolidBrush(ForeColor), rectText, SF);
                graph.DrawString(TextHover, Font, new SolidBrush(ForeColor), rectTextHover, SF);
            }
        }

        private void TextSlideAction()
        {
            if (MouseEntered)
            {
                TextSlideAnim = new Animation("TextSlide_" + Handle, Invalidate, TextSlideAnim.Value, Width - 1);
            }
            else
            {
                TextSlideAnim = new Animation("TextSlide_" + Handle, Invalidate, TextSlideAnim.Value, 0);
            }

            TextSlideAnim.StepDivider = 8;
            Animator.Request(TextSlideAnim, true);
        }

        private void ButtonRippleAction()
        {
            RippleButtonAnim = new Animation("ButtonRipple_" + Handle, Invalidate, 0, Width * 2);

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
            TextSlideAction();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            MouseEntered = false;

            ButtonCurtainAction();
            TextSlideAction();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            MousePressed = true;

            CurtainButtonAnim.Value = CurtainButtonAnim.TargetValue;

            ClickLocation = e.Location;
            ButtonRippleAction();

            Location = new Point(Location.X, Location.Y + 2);

            Focus();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            MousePressed = false;

            Invalidate();

            Location = new Point(Location.X, Location.Y - 2);
        }
    }
}
