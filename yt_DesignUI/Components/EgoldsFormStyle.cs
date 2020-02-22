using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using EgoldsUI;

namespace yt_DesignUI.Components
{
    public partial class EgoldsFormStyle : Component
    {
        #region -- Свойста --

        public Form Form { get; set; }

        private fStyle formStyle = fStyle.None;
        public fStyle FormStyle
        {
            get => formStyle;
            set
            {
                formStyle = value;
                Sign();
            }
        }
        public enum fStyle
        {
            None,

            SimpleDark
        }

        [Description("Указывает, включен ли эффект свечения от курсора при наведении на значки меню окна")]
        public bool EnableControlBoxMouseLight { get; set; }

        [Description("Указывает, включен ли эффект свечения от значков меню окна")]
        public bool EnableControlBoxIconsLight { get; set; }

        [Description("Высота шапки (заголовка)")]
        public int HeaderHeight { get; set; } = 38;

        [Description("Цвет шапки (заголовка)")]
        public Color HeaderColor { get; set; } = Color.DimGray;

        #endregion
        #region -- Переменные --
        
        private Size IconSize = new Size(14, 14);

        private StringFormat SF = new StringFormat();
        private Font Font = new Font("Arial", 8.75F, FontStyle.Regular);

        Pen WhitePen = new Pen(Color.White) { Width = 1.55F };
        Pen GrayPen = new Pen(Color.Gray) { Width = 1.55F };

        bool MousePressed = false; // Кнопка мыши нажата
        Point clickPosition; // Начальная позиция курсора в момент клика
        Point moveStartPosition; // Начальная позиция формы в момент клика

        Rectangle rectBtnClose = new Rectangle();
        Rectangle rectBtnMax = new Rectangle();
        Rectangle rectBtnMin = new Rectangle();

        bool btnCloseHovered = false;
        bool btnMaximizeHovered = false;
        bool btnMinimizeHovered = false;

        #endregion

        public EgoldsFormStyle()
        {
            InitializeComponent();
        }
        public EgoldsFormStyle(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void Sign()
        {
            if(Form != null)
            {
                //Form.Load += Form_Load;
                Form.HandleCreated += Form_HandleCreated;
            }
        }
        
        private void Apply()
        {
            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Center;

            Form.FormBorderStyle = FormBorderStyle.None;

            SetDoubleBuffered(Form);
            
            OffsetControls();
            
            Form.Paint += Form_Paint;
            Form.MouseDown += Form_MouseDown;
            Form.MouseUp += Form_MouseUp;
            Form.MouseMove += Form_MouseMove;
            Form.MouseLeave += Form_MouseLeave;
            Form.SizeChanged += Form_SizeChanged;
            Form.DoubleClick += Form_DoubleClick;
        }
        
        private void OffsetControls()
        {
            Form.Height = Form.Height + HeaderHeight;

            foreach(Control ctrl in Form.Controls)
            {
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + HeaderHeight);
                ctrl.Refresh();
            }
        }

        #region -- Form Events --

        private void Form_Load(object sender, EventArgs e)
        {
            //Apply();
        }
        
        // В этом событии выполняется раньше чем в Load
        private void Form_HandleCreated(object sender, EventArgs e)
        {
            Apply();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            DrawStyle(e.Graphics);
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            Form.Refresh();
        }

        private void Form_MouseLeave(object sender, EventArgs e)
        {
            btnCloseHovered = false;
            btnMaximizeHovered = false;
            btnMinimizeHovered = false;
            Form.Invalidate();
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (MousePressed)
            {
                if (Form.WindowState == FormWindowState.Maximized)
                {
                    // Change WindowState if is Maximized
                    Form.WindowState = FormWindowState.Normal;
                    Form.Location = new Point(Cursor.Position.X - Form.Width / 2, Cursor.Position.Y - HeaderHeight / 2);
                    moveStartPosition = Form.Location;
                }
                else
                {
                    // Moving
                    Size frmOffset = new Size(Point.Subtract(Cursor.Position, new Size(clickPosition)));
                    Form.Location = Point.Add(moveStartPosition, frmOffset);
                }
            }
            else
            {
                // Close Button
                if (rectBtnClose.Contains(e.Location))
                {
                    if (btnCloseHovered == false)
                    {
                        btnCloseHovered = true;

                        if (EnableControlBoxMouseLight == false)
                            Form.Invalidate();
                    }

                    if (EnableControlBoxMouseLight == true)
                        Form.Invalidate();
                }
                else
                {
                    if (btnCloseHovered == true)
                    {
                        btnCloseHovered = false;
                        Form.Invalidate();
                    }
                }

                // Maximize Button
                if (rectBtnMax.Contains(e.Location))
                {
                    if (btnMaximizeHovered == false)
                    {
                        btnMaximizeHovered = true;

                        if (EnableControlBoxMouseLight == false)
                            Form.Invalidate();
                    }

                    if (EnableControlBoxMouseLight == true)
                        Form.Invalidate();
                }
                else
                {
                    if (btnMaximizeHovered)
                    {
                        btnMaximizeHovered = false;
                        Form.Invalidate();
                    }
                }

                // Minimize Button
                if (rectBtnMin.Contains(e.Location))
                {
                    if (btnMinimizeHovered == false)
                    {
                        btnMinimizeHovered = true;

                        if (EnableControlBoxMouseLight == false)
                            Form.Invalidate();
                    }

                    if (EnableControlBoxMouseLight == true)
                        Form.Invalidate();
                }
                else
                {
                    if (btnMinimizeHovered)
                    {
                        btnMinimizeHovered = false;
                        Form.Invalidate();
                    }
                }
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;

            // Если окно полнять вверх -> разворачиваем на весь экран
            if (Cursor.Position.Y == Screen.FromHandle(Form.Handle).WorkingArea.Y
                && Form.WindowState == FormWindowState.Normal)
            {
                Form.WindowState = FormWindowState.Maximized;
            }

            // Огранечение по Y
            if (Form.Location.Y < Screen.FromHandle(Form.Handle).WorkingArea.Y)
            {
                Form.Location = new Point(Form.Location.X, Screen.FromHandle(Form.Handle).WorkingArea.Y);
            }

            if (e.Button == MouseButtons.Left && Form.ControlBox == true)
            {
                // Close Button Click
                if (rectBtnClose.Contains(e.Location))
                    Form.Close();

                // Max Button Click
                if (rectBtnMax.Contains(e.Location) && Form.MaximizeBox == true)
                {
                    if (Form.WindowState == FormWindowState.Maximized)
                    {
                        Form.WindowState = FormWindowState.Normal;
                    }
                    else if (Form.WindowState == FormWindowState.Normal)
                    {
                        Form.WindowState = FormWindowState.Maximized;
                    }
                }

                // Min Button Click
                if (rectBtnMin.Contains(e.Location) && Form.MinimizeBox == true)
                    Form.WindowState = FormWindowState.Minimized;
            }
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Location.Y <= HeaderHeight
                && !rectBtnClose.Contains(e.Location)
                && !rectBtnMax.Contains(e.Location)
                && !rectBtnMin.Contains(e.Location))
            {
                MousePressed = true;
                clickPosition = Cursor.Position;
                moveStartPosition = Form.Location;
            }
        }

        private void Form_DoubleClick(object sender, EventArgs e)
        {
            if (MousePressed)
            {
                if (Form.WindowState == FormWindowState.Maximized)
                {
                    Form.WindowState = FormWindowState.Normal;
                }
                else if (Form.WindowState == FormWindowState.Normal)
                {
                    Form.WindowState = FormWindowState.Maximized;
                }
            }
        }

        #endregion

        private void DrawStyle(Graphics graph)
        {
            graph.SmoothingMode = SmoothingMode.HighQuality;

            Rectangle rectHeader = new Rectangle(0, 0, Form.Width - 1, HeaderHeight);
            Rectangle rectBorder = new Rectangle(0, 0, Form.Width - 1, Form.Height - 1);
            
            Rectangle rectIcon = new Rectangle(
                rectHeader.Height / 2 - IconSize.Width / 2,
                rectHeader.Height / 2 - IconSize.Height / 2,
                IconSize.Width, IconSize.Height
                );

            Rectangle rectTitleText = new Rectangle(rectIcon.Right + 5, rectHeader.Y, rectHeader.Width, rectHeader.Height);

            rectBtnClose = new Rectangle(rectHeader.Width - rectHeader.Height, rectHeader.Y, rectHeader.Height, rectHeader.Height);
            Rectangle rectCrosshair = new Rectangle(
                rectBtnClose.X + rectBtnClose.Width / 2 - 5, 
                rectBtnClose.Height / 2 - 5, 
                10, 10);

            rectBtnMax = new Rectangle(rectBtnClose.X - rectHeader.Height, rectHeader.Y, rectHeader.Height, rectHeader.Height);
            Rectangle rectMaxButtonIcon = new Rectangle(
                rectBtnMax.X + rectBtnMax.Width / 2 - 5,
                rectBtnMax.Height / 2 - 5,
                10, 10);

            rectBtnMin = new Rectangle(rectBtnMax.X - rectHeader.Height, rectHeader.Y, rectHeader.Height, rectHeader.Height);

            // Шапка
            graph.DrawRectangle(new Pen(HeaderColor), rectHeader);
            graph.FillRectangle(new SolidBrush(HeaderColor), rectHeader);

            // Текст заголовка
            graph.DrawString(Form.Text, Font, new SolidBrush(Color.White), rectTitleText, SF);

            // Иконка
            graph.DrawImage(Form.Icon.ToBitmap(), rectIcon);

            if (Form.ControlBox == true)
            {
                // Кнопка Х
                graph.DrawRectangle(new Pen(btnCloseHovered ? FlatColors.Red : HeaderColor), rectBtnClose);
                graph.FillRectangle(new SolidBrush(btnCloseHovered ? FlatColors.Red : HeaderColor), rectBtnClose);
                DrawCrosshair(graph, rectCrosshair, WhitePen);

                // Кнопка [MAX]
                graph.DrawRectangle(new Pen(btnMaximizeHovered && Form.MaximizeBox ? FlatColors.Gray : HeaderColor), rectBtnMax);
                graph.FillRectangle(new SolidBrush(btnMaximizeHovered && Form.MaximizeBox ? FlatColors.Gray : HeaderColor), rectBtnMax);

                if (EnableControlBoxIconsLight)
                {
                    Drawer.DrawBlurredRectangle(graph, Color.White, rectMaxButtonIcon, 8, 20);
                }

                graph.DrawRectangle(Form.MaximizeBox ? WhitePen : GrayPen, rectMaxButtonIcon);

                // Кнопка [ _ ]
                graph.DrawRectangle(new Pen(btnMinimizeHovered && Form.MinimizeBox ? FlatColors.Gray : HeaderColor), rectBtnMin);
                graph.FillRectangle(new SolidBrush(btnMinimizeHovered && Form.MinimizeBox ? FlatColors.Gray : HeaderColor), rectBtnMin);
                Point p1 = new Point(rectBtnMin.X + rectBtnMin.Width / 2 - 5, rectBtnMin.Height / 2 + 5);
                Point p2 = new Point(rectBtnMin.X + rectBtnMin.Width / 2 + 5, rectBtnMin.Height / 2 + 5);

                if (EnableControlBoxIconsLight)
                {
                    Drawer.DrawBlurredLine(graph, Color.White, p1, p2, 8, 20);
                }

                graph.DrawLine(Form.MinimizeBox ? WhitePen : GrayPen, p1, p2);

                // Свечение от курсора
                if (EnableControlBoxMouseLight && (btnCloseHovered || btnMaximizeHovered || btnMinimizeHovered))
                {
                    Point cursorPoint1 = Form.PointToClient(Cursor.Position);
                    Point cursorPoint2 = new Point(cursorPoint1.X, cursorPoint1.Y + 1);
                    Drawer.DrawBlurredLine(graph, Color.White, cursorPoint1, cursorPoint2, 10, 70);
                }
            }

            // Обводка
            graph.DrawRectangle(new Pen(Color.Black), rectBorder);
        }

        private void DrawCrosshair(Graphics graph, Rectangle rect, Pen pen)
        {
            if (EnableControlBoxIconsLight)
            {
                Drawer.DrawBlurredLine(
                    graph,
                    Color.White,
                    new Point(rect.X, rect.Y),
                    new Point(rect.X + rect.Width, rect.Y + rect.Height),
                    8,
                    20);

                Drawer.DrawBlurredLine(graph,
                    Color.White,
                    new Point(rect.X + rect.Width, rect.Y),
                    new Point(rect.X, rect.Y + rect.Height),
                    8,
                    20);
            }

            graph.DrawLine(
                pen, 
                rect.X, 
                rect.Y, 
                rect.X + rect.Width, 
                rect.Y + rect.Height);

            graph.DrawLine(
                pen, 
                rect.X + rect.Width, 
                rect.Y, 
                rect.X, 
                rect.Y + rect.Height);
        }

        public static void SetDoubleBuffered(Control c)
        {
            if (SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo pDoubleBuffered =
                  typeof(Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            pDoubleBuffered.SetValue(c, true, null);
        }
    }
}
