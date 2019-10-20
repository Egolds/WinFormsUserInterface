using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using EgoldsUI;

namespace yt_DesignUI
{
    [DefaultProperty("TextPreview")]
    public class EgoldsGoogleTextBox : Control
    {
        #region -- Переменные --

        StringFormat SF = new StringFormat();

        int TopBorderOffset = 0;

        TextBox tbInput = new TextBox();

        Animation LocationTextPreviewAnim = new Animation();
        Animation FontSizeTextPreviewAnim = new Animation();

        #endregion

        #region -- Свойства --

        public string TextPreview { get; set; } = "Input text";
        public Font FontTextPreview { get; set; } = new Font("Arial", 8, FontStyle.Bold);

        public Color BorderColor { get; set; } = FlatColors.Blue;
        public Color BorderColorNotActive { get; set; } = FlatColors.GrayDark;

        public string TextInput
        {
            get => tbInput.Text;
            set => tbInput.Text = value;
        }

        public bool UseSystemPasswordChar
        {
            get => tbInput.UseSystemPasswordChar;
            set => tbInput.UseSystemPasswordChar = value;
        }

        [Browsable(false)]
        public new string Text { get; set; }

        #endregion

        public EgoldsGoogleTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            DoubleBuffered = true;

            Size = new Size(150, 40);
            Font = new Font("Arial", 11.25F, FontStyle.Regular);
            ForeColor = Color.Black;
            BackColor = Color.White;

            Cursor = Cursors.IBeam;

            SF.Alignment = StringAlignment.Center;
            SF.LineAlignment = StringAlignment.Center;

            AdjustTextBoxInput();
            Controls.Add(tbInput);

            LocationTextPreviewAnim.Value = tbInput.Location.Y;
            FontSizeTextPreviewAnim.Value = Font.Size;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            TextPreviewAction(TextInput.Length > 0);
        }

        private void AdjustTextBoxInput()
        {
            tbInput = new TextBox();
            tbInput.Name = "InputBox";
            tbInput.BorderStyle = BorderStyle.None;
            tbInput.BackColor = BackColor;
            tbInput.ForeColor = ForeColor;
            tbInput.Font = Font;
            tbInput.Visible = false;

            int offset = TextRenderer.MeasureText(TextPreview, FontTextPreview).Height / 2;
            tbInput.Location = new Point(5, Height / 2 - offset);
            tbInput.Size = new Size(Width - 10, tbInput.Height);

            tbInput.LostFocus += TbInput_LostFocus;
        }

        private void TbInput_LostFocus(object sender, EventArgs e)
        {
            TextPreviewAction(false);
        }

        #region -- Обновление свойств tbInput --
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            tbInput.BackColor = BackColor;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            tbInput.ForeColor = ForeColor;

        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            tbInput.Font = Font;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            tbInput.Size = new Size(Width - 10, tbInput.Height);
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;

            graph.Clear(Parent.BackColor);

            TopBorderOffset = graph.MeasureString(TextPreview, FontTextPreview).ToSize().Height / 2;

            Font FontTextPreviewActual = new Font(FontTextPreview.FontFamily, FontSizeTextPreviewAnim.Value, FontTextPreview.Style);

            if(tbInput.Visible == false && FontTextPreviewActual.Size <= FontTextPreview.Size)
            {
                tbInput.Visible = true;
                tbInput.Focus();
            }
            else if (tbInput.Visible == true && FontTextPreviewActual.Size > FontTextPreview.Size)
            {
                tbInput.Visible = false;
            }

            Rectangle rectBase = new Rectangle(0, TopBorderOffset, Width - 1, Height - 1 - TopBorderOffset);

            Size TextPreviewRectSize = graph.MeasureString(TextPreview, FontTextPreviewActual).ToSize();
            Rectangle rectTextPreview = new Rectangle(5, (int)LocationTextPreviewAnim.Value, TextPreviewRectSize.Width + 3, TextPreviewRectSize.Height);

            // Обводка
            graph.DrawRectangle(new Pen(tbInput.Focused == true ? BorderColor : BorderColorNotActive), rectBase);
            
            // Заголовок/Описание
            graph.DrawRectangle(new Pen(Parent.BackColor), rectTextPreview);
            graph.FillRectangle(new SolidBrush(Parent.BackColor), rectTextPreview);

            // Цвет внутри
            graph.FillRectangle(new SolidBrush(BackColor), rectBase);

            graph.DrawString(TextPreview, FontTextPreviewActual, new SolidBrush(tbInput.Focused == true ? BorderColor : BorderColorNotActive), rectTextPreview, SF);
        }

        private void TextPreviewAction(bool OnTop)
        {
            if (OnTop)
            {
                if (tbInput.Visible == false)
                {
                    LocationTextPreviewAnim = new Animation("TextPreviewLocationY" + Handle, Invalidate, LocationTextPreviewAnim.Value, 0);
                    FontSizeTextPreviewAnim = new Animation("TextPreviewFontSize" + Handle, Invalidate, FontSizeTextPreviewAnim.Value, FontTextPreview.Size);
                }
                else
                {
                    tbInput.Focus();
                    return;
                }
            }
            else
            {
                if (TextInput.Length == 0)
                {
                    LocationTextPreviewAnim = new Animation("TextPreviewLocationY" + Handle, Invalidate, LocationTextPreviewAnim.Value, tbInput.Location.Y);
                    FontSizeTextPreviewAnim = new Animation("TextPreviewFontSize" + Handle, Invalidate, FontSizeTextPreviewAnim.Value, Font.Size);
                }
                else
                {
                    return;
                }
            }

            LocationTextPreviewAnim.StepDivider = 4;
            FontSizeTextPreviewAnim.StepDivider = 4;

            Animator.Request(LocationTextPreviewAnim, true);
            Animator.Request(FontSizeTextPreviewAnim, true);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            TextPreviewAction(true);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            TextPreviewAction(true);
        }
    }
}
