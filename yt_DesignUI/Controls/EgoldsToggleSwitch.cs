using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using EgoldsUI;

namespace yt_DesignUI
{
    [Designer(typeof(ControlDesignerEx))] // ControlDesignerEx Добавляем для ограничения изменения размеров
    [DefaultEvent("CheckedChanged")]
    public class EgoldsToggleSwitch : Control
    {
        #region -- Свойства --

        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;

                SwitchToggle();

                UpdateSize();
                UpdateStructures();
            }
        }
        private bool _checked = false;

        public Color BackColorON { get; set; } = FlatColors.GreenDark;
        public Color BackColorOFF { get; set; } = FlatColors.Red;

        public override string Text
        {
            get => text;
            set
            {
                text = value;
                UpdateSize();
                UpdateStructures();
            }
        }
        private string text = string.Empty;

        public string TextOnChecked
        {
            get => _textOnChecked;
            set
            {
                _textOnChecked = value;
                UpdateSize();
                UpdateStructures();
            }
        }
        private string _textOnChecked = string.Empty;
        
        #endregion

        #region -- Переменные --

        private Rectangle rectToggleHolder;
        private int ToogleSwitchWidth = 40;

        private Rectangle rectText;

        private int TogglePosX_ON;
        private int TogglePosX_OFF;

        private Animation ToggleAnim = new Animation();

        private StringFormat SF = new StringFormat();

        #endregion

        #region -- События --

        [Description("Возникает при каждом изменении свойства Checked")]
        public event OnCheckedChangedHandler CheckedChanged;
        public delegate void OnCheckedChangedHandler(object sender);

        #endregion

        public EgoldsToggleSwitch()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Center;

            Font = new Font("Verdana", 9F, FontStyle.Regular);
            BackColor = Color.White;

            Cursor = Cursors.Hand;

            Size = new Size(ToogleSwitchWidth, 15);
            UpdateSize();
            UpdateStructures();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            
            UpdateStructures();

            ToggleAnim.Value = Checked == true ? TogglePosX_ON : TogglePosX_OFF;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.Clear(Parent.BackColor);

            Pen penToggleHolder = new Pen(FlatColors.GrayDark, 1.55f);
            Pen penToggle = new Pen(FlatColors.GrayDark, 3);
            
            GraphicsPath gpathToggle = Drawer.RoundedRectangle(rectToggleHolder, rectToggleHolder.Height);
            Rectangle rectToggle = new Rectangle((int)ToggleAnim.Value, rectToggleHolder.Y, rectToggleHolder.Height, rectToggleHolder.Height);
            
            graph.DrawPath(penToggleHolder, gpathToggle);

            if (Checked == true)
            {
                if (Animator.IsWork == false)
                    rectToggle.Location = new Point(TogglePosX_ON, rectToggleHolder.Y);

                graph.FillPath(new SolidBrush(BackColorON), gpathToggle);
            }
            else
            {
                if (Animator.IsWork == false)
                    rectToggle.Location = new Point(TogglePosX_OFF, rectToggleHolder.Y);

                graph.FillPath(new SolidBrush(BackColorOFF), gpathToggle);
            }

            graph.DrawEllipse(penToggle, rectToggle);
            graph.FillEllipse(new SolidBrush(BackColor), rectToggle);

            string drawText = Text;
            if (!string.IsNullOrEmpty(TextOnChecked) && Checked)
                drawText = TextOnChecked;

            graph.DrawString(drawText, Font, new SolidBrush(ForeColor), rectText, SF);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            SwitchToggle(true);
        }

        private void SwitchToggle(bool OnClick = false)
        {
            if (Checked == true)
            {
                ToggleAnim = new Animation("Toggle_" + Handle, Invalidate, ToggleAnim.Value, TogglePosX_OFF);
            }
            else
            {
                ToggleAnim = new Animation("Toggle_" + Handle, Invalidate, ToggleAnim.Value, TogglePosX_ON);
            }

            if (OnClick)
            {
                Checked = !Checked;
            }

            ToggleAnim.StepDivider = 8;
            Animator.Request(ToggleAnim, true);

            CheckedChanged?.Invoke(this);
        }
        
        private void UpdateSize()
        {
            string drawText = Text;
            if (!string.IsNullOrEmpty(TextOnChecked) && Checked) drawText = TextOnChecked;

            Size size = TextRenderer.MeasureText(drawText, Font);
            Width = ToogleSwitchWidth + size.Width + 3;
        }

        private void UpdateStructures()
        {
            rectToggleHolder = new Rectangle(1, 1, ToogleSwitchWidth - 3, Height - 3);

            int rectTextWidth = Width - ToogleSwitchWidth - 3;
            int rectTextX = rectToggleHolder.Right + 6;
            rectText = new Rectangle(rectTextX, rectToggleHolder.Y, rectTextWidth, rectToggleHolder.Height);

            TogglePosX_OFF = rectToggleHolder.X;
            TogglePosX_ON = rectToggleHolder.Right - rectToggleHolder.Height;
        }

        /// <summary>
        /// В этом классе переопределяем SelectionRules, и даем возможность только изменять ширину и перемещать объект
        /// </summary>
        class ControlDesignerEx : ControlDesigner
        {
            public override SelectionRules SelectionRules
            {
                get
                {
                    SelectionRules sr = SelectionRules.Moveable | SelectionRules.Visible;
                    return sr;
                }
            }
        }
    }
}
