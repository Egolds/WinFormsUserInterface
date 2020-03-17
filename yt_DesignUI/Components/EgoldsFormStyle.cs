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
                if (StyleUsed == false)
                {
                    Sign();
                }
                else
                {

                }
            }
        }
        public enum fStyle
        {
            None,

            SimpleDark,
            TelegramStyle
        }

        [Description("Указывает, может ли пользователь изменять размер окна")]
        public bool AllowUserResize { get; set; }

        [Description("Указывает, включен ли эффект свечения от курсора при наведении на значки меню окна")]
        public bool EnableControlBoxMouseLight { get; set; }

        [Description("Указывает, включен ли эффект свечения от значков меню окна")]
        public bool EnableControlBoxIconsLight { get; set; }

        [Description("Высота шапки (заголовка)")]
        public int HeaderHeight { get; set; } = 38;

        [Description("Цвет шапки (заголовка)")]
        public Color HeaderColor { get; set; } = Color.DimGray;

        [Description("Меню быстрого доступа, отображаемое при щелчке правой кнопкой мыши на иконке окна")]
        public ContextMenuStrip ContextMenuForm { get; set; }

        public Font HeaderFont { get; set; } = new Font("Segoe UI", 9.75F, FontStyle.Regular);

        #endregion
        #region -- Переменные --

        private bool StyleUsed = false;
        
        private Size IconSize = new Size(14, 14);
        private Rectangle rectIcon = new Rectangle();
        private bool IconHovered = false;

        private StringFormat SF = new StringFormat();

        private Pen WhitePen = new Pen(Color.White, 1.55F);
        private Pen GrayPen = new Pen(Color.Gray, 1.55F);

        private bool MousePressed = false; // Кнопка мыши нажата
        private Point clickPosition; // Начальная позиция курсора в момент клика
        private Point moveStartPosition; // Начальная позиция формы в момент клика
        private bool CanDragForm = false; // Указывает может ли форма перетаскивается

        private MouseButtons LastClickedMouseButton; // Какая кнопка мыши была нажата последний раз

        private Rectangle rectBtnClose = new Rectangle();
        private Rectangle rectBtnMax = new Rectangle();
        private Rectangle rectBtnMin = new Rectangle();

        private bool btnCloseHovered = false;
        private bool btnMaximizeHovered = false;
        private bool btnMinimizeHovered = false;

        private Rectangle rectHeader = new Rectangle();
        private Rectangle rectBorder = new Rectangle();

        private int ResizeBorderSize = 4;
        private int ResizeAngleBorderOffset = 10;
        private bool IsResizing = false; // Режим изменения размера

        private BorderHoverPositionEnum BorderHoverPosition = BorderHoverPositionEnum.None;
        enum BorderHoverPositionEnum
        {
            None, // Не наведен
            Left, Top, Right, Bottom, // Стороны
            TopLeft, TopRight, BottomLeft, BottomRight // Углы
        }

        private int ResizeStartRight = 0;
        private int ResizeStartBottom = 0;

        // Указывает, нужно ли восстановить позицию окна,
        // которая была, перед разворачиванием на весь экран (Maximize), с помощью перетаскивания окна вверх экана
        private bool FormNeedReposition = false;
        
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
            if (Form != null)
            {
                Form.HandleCreated += Form_HandleCreated;
            }
        }
        
        private void Apply()
        {
            StyleUsed = true;

            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Center;

            Form.FormBorderStyle = FormBorderStyle.None;

            Size minimumSize = new Size(100, 50);
            if (Form.MinimumSize.Width < minimumSize.Width || Form.MinimumSize.Height < minimumSize.Height)
                Form.MinimumSize = minimumSize;

            SetDoubleBuffered(Form);
            
            OffsetControls();
            
            Form.Paint += Form_Paint;
            Form.MouseDown += Form_MouseDown;
            Form.MouseUp += Form_MouseUp;
            Form.MouseMove += Form_MouseMove;
            Form.MouseLeave += Form_MouseLeave;
            Form.SizeChanged += Form_SizeChanged;
            Form.DoubleClick += Form_DoubleClick;
            Form.Click += Form_Click;
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
            // Dragging
            if (CanDragForm && e.Button == MouseButtons.Left)
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
            // Hovering
            else 
            {
                // Close Button Hovering
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

                // Maximize Button Hovering
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

                // Minimize Button Hovering
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

                // Icon Hovering
                if (rectIcon.Contains(e.Location))
                {
                    IconHovered = true;
                }
                else
                {
                    IconHovered = false;
                }
            }

            // On hover on border for resize
            if (AllowUserResize && IsResizing == false && Form.WindowState == FormWindowState.Normal)
            {
                if (rectBorder.Top + ResizeBorderSize >= e.Location.Y)
                {
                    // Левый верхний угол
                    if (e.Location.X <= ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNWSE;
                        BorderHoverPosition = BorderHoverPositionEnum.TopLeft;
                    }
                    // Правый верхний угол
                    else if (e.Location.X >= rectBorder.Width - ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNESW;
                        BorderHoverPosition = BorderHoverPositionEnum.TopRight;
                    }
                    else
                    {
                        Form.Cursor = Cursors.SizeNS;
                        BorderHoverPosition = BorderHoverPositionEnum.Top;
                    }
                }
                else if (rectBorder.Bottom - ResizeBorderSize <= e.Location.Y)
                {
                    // Левый нижний угол
                    if (e.Location.X <= ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNESW;
                        BorderHoverPosition = BorderHoverPositionEnum.BottomLeft;
                    }
                    // Правый нижний угол
                    else if (e.Location.X >= rectBorder.Width - ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNWSE;
                        BorderHoverPosition = BorderHoverPositionEnum.BottomRight;
                    }
                    else
                    {
                        Form.Cursor = Cursors.SizeNS;
                        BorderHoverPosition = BorderHoverPositionEnum.Bottom;
                    }
                }
                else if (rectBorder.Left + ResizeBorderSize >= e.Location.X)
                {
                    // Левый верхний угол
                    if (e.Location.Y <= ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNWSE;
                        BorderHoverPosition = BorderHoverPositionEnum.TopLeft;
                    }
                    // Левый нижний угол
                    else if (e.Location.Y >= rectBorder.Height - ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNESW;
                        BorderHoverPosition = BorderHoverPositionEnum.BottomLeft;
                    }
                    else
                    {
                        Form.Cursor = Cursors.SizeWE;
                        BorderHoverPosition = BorderHoverPositionEnum.Left;
                    }
                }
                else if (rectBorder.Right - ResizeBorderSize <= e.Location.X)
                {
                    // Правый верхний угол
                    if (e.Location.Y <= ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNESW;
                        BorderHoverPosition = BorderHoverPositionEnum.TopRight;
                    }
                    // Правый нижний угол
                    else if (e.Location.Y >= rectBorder.Height - ResizeAngleBorderOffset)
                    {
                        Form.Cursor = Cursors.SizeNWSE;
                        BorderHoverPosition = BorderHoverPositionEnum.BottomRight;
                    }
                    else
                    {
                        Form.Cursor = Cursors.SizeWE;
                        BorderHoverPosition = BorderHoverPositionEnum.Right;
                    }
                }
                else if (Form.Cursor != Cursors.Default)
                {
                    Form.Cursor = Cursors.Default;
                    BorderHoverPosition = BorderHoverPositionEnum.None;
                }
            }
            // Resize
            else if (AllowUserResize && IsResizing && Form.WindowState == FormWindowState.Normal)
            {
                // Resize
                switch (BorderHoverPosition)
                {
                    // Стороны / Sides
                    case BorderHoverPositionEnum.Left:
                        Form.Location = new Point(Cursor.Position.X, Form.Location.Y);
                        Form.Width = Form.Width - (Form.Right - ResizeStartRight);
                        break;

                    case BorderHoverPositionEnum.Top:
                        Form.Location = new Point(Form.Location.X, Cursor.Position.Y);
                        Form.Height = Form.Height - (Form.Bottom - ResizeStartBottom);
                        break;

                    case BorderHoverPositionEnum.Right:
                        Form.Width = Cursor.Position.X - Form.Left;
                        break;

                    case BorderHoverPositionEnum.Bottom:
                        Form.Height = Cursor.Position.Y - Form.Top;
                        break;


                    // Углы / Angles
                    case BorderHoverPositionEnum.TopLeft:
                        Form.Location = new Point(Form.Location.X, Cursor.Position.Y);
                        Form.Height = Form.Height - (Form.Bottom - ResizeStartBottom);

                        Form.Location = new Point(Cursor.Position.X, Form.Location.Y);
                        Form.Width = Form.Width - (Form.Right - ResizeStartRight);
                        break;

                    case BorderHoverPositionEnum.TopRight:
                        Form.Location = new Point(Form.Location.X, Cursor.Position.Y);
                        Form.Height = Form.Height - (Form.Bottom - ResizeStartBottom);

                        Form.Width = Cursor.Position.X - Form.Left;
                        break;

                    case BorderHoverPositionEnum.BottomLeft:
                        Form.Height = Cursor.Position.Y - Form.Top;

                        Form.Location = new Point(Cursor.Position.X, Form.Location.Y);
                        Form.Width = Form.Width - (Form.Right - ResizeStartRight);
                        break;

                    case BorderHoverPositionEnum.BottomRight:
                        Form.Height = Cursor.Position.Y - Form.Top;
                        Form.Width = Cursor.Position.X - Form.Left;
                        break;

                    
                }
            }
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            MousePressed = false;
            CanDragForm = false;
            IsResizing = false;

            if (AllowUserResize && BorderHoverPosition != BorderHoverPositionEnum.None)
                return;

            // Если окно поднять вверх -> разворачиваем на весь экран
            if (Cursor.Position.Y == Screen.FromHandle(Form.Handle).WorkingArea.Y
                && Form.WindowState == FormWindowState.Normal)
            {
                Form.WindowState = FormWindowState.Maximized;
                FormNeedReposition = true;
            }

            // Огранечение по Y
            if (Form.Location.Y < Screen.FromHandle(Form.Handle).WorkingArea.Y)
            {
                Form.Location = new Point(Form.Location.X, Screen.FromHandle(Form.Handle).WorkingArea.Y);
            }

            // Нажатия на кнопки управления окном
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

                        if (FormNeedReposition)
                        {
                            FormNeedReposition = false;
                            Form.Location = moveStartPosition;
                        }
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

            // Контекстное меню при нажатии на иконку
            if (e.Button == MouseButtons.Right && IconHovered)
            {
                if (ContextMenuForm != null)
                {
                    ContextMenuForm.Show(Cursor.Position);
                }
            }
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            MousePressed = true;

            if (AllowUserResize && BorderHoverPosition != BorderHoverPositionEnum.None)
            {
                if (e.Button == MouseButtons.Left)
                {
                    IsResizing = true;
                    ResizeStartRight = Form.Right;
                    ResizeStartBottom = Form.Bottom;
                    return;
                }
            }

            if (e.Location.Y <= HeaderHeight
                && !rectBtnClose.Contains(e.Location)
                && !rectBtnMax.Contains(e.Location)
                && !rectBtnMin.Contains(e.Location))
            {
                CanDragForm = true;
                clickPosition = Cursor.Position;
                moveStartPosition = Form.Location;
            }

            LastClickedMouseButton = e.Button;
        }

        private void Form_DoubleClick(object sender, EventArgs e)
        {
            if (BorderHoverPosition != BorderHoverPositionEnum.None)
                return;

            if (MousePressed && LastClickedMouseButton == MouseButtons.Left && rectHeader.Contains(Form.PointToClient(Cursor.Position)))
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

        private void Form_Click(object sender, EventArgs e)
        {
            Form.Focus();
        }

        #endregion

        private void DrawStyle(Graphics graph)
        {
            graph.SmoothingMode = SmoothingMode.HighQuality;
            
            #region - Structures Defining -

            // Header Structure
            rectHeader = new Rectangle(0, 0, Form.Width - 1, HeaderHeight);

            // Border Structure
            rectBorder = new Rectangle(0, 0, Form.Width - 1, Form.Height - 1);

            // Icon Structure
            rectIcon = new Rectangle(
                rectHeader.Height / 2 - IconSize.Width / 2,
                rectHeader.Height / 2 - IconSize.Height / 2,
                IconSize.Width, IconSize.Height
                );

            // Title Structure
            Rectangle rectTitleText = new Rectangle(Form.ShowIcon ? rectIcon.Right + 5 : rectIcon.Left, rectHeader.Y, rectHeader.Width, rectHeader.Height);
            
            // Close Button Structure
            rectBtnClose = new Rectangle(rectHeader.Width - rectHeader.Height, rectHeader.Y, rectHeader.Height, rectHeader.Height);
            // Crosshair Structure
            Rectangle rectCrosshair = new Rectangle(
                rectBtnClose.X + rectBtnClose.Width / 2 - 5, 
                rectBtnClose.Height / 2 - 5, 
                10, 10);

            // Maximize Button Structure
            rectBtnMax = new Rectangle(rectBtnClose.X - rectHeader.Height, rectHeader.Y, rectHeader.Height, rectHeader.Height);
            // Maximize Icon Structure
            Rectangle rectMaxButtonIcon = new Rectangle(
                rectBtnMax.X + rectBtnMax.Width / 2 - 5,
                rectBtnMax.Height / 2 - 5,
                10, 10);
            // Second Maximize Icon Structure [in Maximized state]
            Rectangle rectMaxButtonIconSecond = rectMaxButtonIcon;

            if (Form.WindowState == FormWindowState.Maximized)
            {
                //Inflate - изменяет размер и одновременно положение (В данном случае -1 по ширине и +2 по X, -1 по высоте и +2 по Y)

                rectMaxButtonIconSecond.Inflate(-1, -1);
                rectMaxButtonIconSecond.Offset(1, -1);

                rectMaxButtonIcon.Inflate(-1, -1);
                rectMaxButtonIcon.Offset(-1, 1);
            }

            // Minimize Button Structure
            rectBtnMin = new Rectangle(rectBtnMax.X - rectHeader.Height, rectHeader.Y, rectHeader.Height, rectHeader.Height);

            #endregion
            #region - Drawing -

            // Шапка / Header
            graph.DrawRectangle(new Pen(HeaderColor), rectHeader);
            graph.FillRectangle(new SolidBrush(HeaderColor), rectHeader);
            
            // Текст заголовка / Title
            graph.DrawString(Form.Text, HeaderFont, new SolidBrush(Color.White), rectTitleText, SF);
            
            // Иконка / Icon
            if(Form.ShowIcon)
            {
                graph.DrawImage(Form.Icon.ToBitmap(), rectIcon);
            }

            if (Form.ControlBox == true)
            {
                // Кнопка Х
                #region - Button X -

                graph.DrawRectangle(new Pen(btnCloseHovered ? FlatColors.Red : HeaderColor), rectBtnClose);
                graph.FillRectangle(new SolidBrush(btnCloseHovered ? FlatColors.Red : HeaderColor), rectBtnClose);
                DrawCrosshair(graph, rectCrosshair, WhitePen);

                #endregion

                // Кнопка [MAX]
                #region - Button Maximize -

                graph.DrawRectangle(new Pen(btnMaximizeHovered && Form.MaximizeBox ? Color.FromArgb(100, Color.Gray) : HeaderColor), rectBtnMax);
                graph.FillRectangle(new SolidBrush(btnMaximizeHovered && Form.MaximizeBox ? Color.FromArgb(100, Color.Gray) : HeaderColor), rectBtnMax);

                if (EnableControlBoxIconsLight)
                {
                    if (Form.WindowState == FormWindowState.Maximized)
                    {
                        Drawer.DrawBlurredRectangle(graph, Color.White, rectMaxButtonIconSecond, 8, 20);
                    }
                    Drawer.DrawBlurredRectangle(graph, Color.White, rectMaxButtonIcon, 8, 20);
                }

                if (Form.WindowState == FormWindowState.Maximized)
                {
                    graph.DrawRectangle(Form.MaximizeBox ? WhitePen : GrayPen, rectMaxButtonIconSecond);
                    graph.FillRectangle(new SolidBrush(HeaderColor), rectMaxButtonIcon);
                }
                graph.DrawRectangle(Form.MaximizeBox ? WhitePen : GrayPen, rectMaxButtonIcon);

                #endregion

                // Кнопка [ _ ]
                #region - Button Minimize -

                graph.DrawRectangle(new Pen(btnMinimizeHovered && Form.MinimizeBox ? Color.FromArgb(100, Color.Gray) : HeaderColor), rectBtnMin);
                graph.FillRectangle(new SolidBrush(btnMinimizeHovered && Form.MinimizeBox ? Color.FromArgb(100, Color.Gray) : HeaderColor), rectBtnMin);
                Point p1 = new Point(rectBtnMin.X + rectBtnMin.Width / 2 - 5, rectBtnMin.Height / 2 + 5);
                Point p2 = new Point(rectBtnMin.X + rectBtnMin.Width / 2 + 5, rectBtnMin.Height / 2 + 5);

                if (EnableControlBoxIconsLight)
                {
                    Drawer.DrawBlurredLine(graph, Color.White, p1, p2, 8, 20);
                }
                graph.DrawLine(Form.MinimizeBox ? WhitePen : GrayPen, p1, p2);

                #endregion

                // Свечение от курсора
                #region - Mouse Light on ControlBox -

                if (EnableControlBoxMouseLight && (btnCloseHovered || btnMaximizeHovered || btnMinimizeHovered))
                {
                    Point cursorPoint1 = Form.PointToClient(Cursor.Position);
                    Point cursorPoint2 = new Point(cursorPoint1.X, cursorPoint1.Y + 1);
                    Drawer.DrawBlurredLine(graph, Color.White, cursorPoint1, cursorPoint2, 7, 70);
                }

                #endregion
            }

            // Обводка / Border
            graph.DrawRectangle(new Pen(Color.Gray), rectBorder);

            #endregion
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
