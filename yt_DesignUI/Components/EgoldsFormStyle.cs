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
using System.Drawing.Imaging;
using System.Reflection;

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

                if (Site.DesignMode) return;

                if (StyleUsed == false)
                {
                    // Если стиль не был использован, то:
                    Sign();
                }
                else
                {
                    // Если стиль был использован то:
                    SetStyle(formStyle);
                }
            }
        }
        public enum fStyle // Набор стилей
        {
            None,

            UserStyle,

            SimpleDark,
            TelegramStyle
        }

        [Description("Указывает, может ли пользователь изменять размер окна")]
        public bool AllowUserResize { get; set; }

        [Description("Указывает, включен ли эффект свечения от курсора при наведении на значки меню окна")]
        public bool EnableControlBoxMouseLight { get; set; }

        /// <summary>
        /// Указывает, включен ли эффект свечения от значков меню окна
        /// </summary>
        [Description("Указывает, включен ли эффект свечения от значков меню окна")]
        public bool EnableControlBoxIconsLight { get; set; }

        [Description("Ширина кнопок меню окна")]
        public int ControlBoxButtonsWidth { get; set; } = 20;

        [Description("Высота шапки (заголовка)")]
        public int HeaderHeight { get; set; } = 38;

        [Description("Цвет шапки (заголовка)")]
        public Color HeaderColor { get; set; } = Color.DimGray;

        [Description("Дополнительный цвет шапки (заголовка) используемый для создания градиента (При HeaderColorGradientEnabled = true)")]
        public Color HeaderColorAdditional { get; set; } = Color.White;

        [Description("Указывает, включен ли градинт цета шапки (заголовка)")]
        public bool HeaderColorGradientEnable { get; set; } = false;

        [Description("Определяет направление линейного градиента шапки")]
        public LinearGradientMode HeaderColorGradientMode { get; set; } = LinearGradientMode.Horizontal;

        [Description("Меню быстрого доступа, отображаемое при щелчке правой кнопкой мыши на иконке окна")]
        public ContextMenuStrip ContextMenuForm { get; set; }

        [Description("Шрифт текста шапки (заголовка)")]
        public Font HeaderTextFont { get; set; } = new Font("Segoe UI", 9.75F, FontStyle.Regular);

        [Description("Цвет текста шапки (заголовка)")]
        public Color HeaderTextColor { get; set; } = Color.White;

        public Image HeaderImage { get; set; }

        [Description("Фоновый цвет формы")]
        public Color BackColor { get; set; } = Color.White;

        #endregion
        #region -- Поля --

        private List<Control> FormControls = new List<Control>();
        private Panel MainContainer = null;

        private Dictionary<fStyle, EgoldsStyle> StylesDictionary; // Словарь вида: <Стиль из набора, Объект стиля с параметрами>
        private EgoldsStyle CurrentStyle; // Актуальный стиль, класс с его параметрамы
        private bool StyleUsed = false; // Применялся ли стиль(тема)

        private Size IconSize = new Size(14, 14); // Размер икноки формы
        private Rectangle rectIcon = new Rectangle(); // Структура иконки формы
        private bool IconHovered = false; // Наведен ли курсор на иконку

        private StringFormat SF = new StringFormat();

        private bool MousePressed = false; // Кнопка мыши нажата
        private Point clickPosition; // Начальная позиция курсора в момент клика
        private Point moveStartPosition; // Начальная позиция формы в момент клика
        private bool CanDragForm = false; // Указывает может ли форма перетаскивается

        private MouseButtons LastClickedMouseButton; // Какая кнопка мыши была нажата последний раз

        private Size ControlBoxIconSize = new Size(10, 10); // Размер иконок меню окна

        private Rectangle rectBtnClose = new Rectangle(); // Структура кнопки меню окна Закрыть
        private Rectangle rectBtnMax = new Rectangle(); // Структура кнопки меню окна Развернуть/Свернуть в окно
        private Rectangle rectBtnMin = new Rectangle(); // Структура кнопки меню окна Свернуть

        private bool btnCloseHovered = false; // Наведен ли курсор на кнопку Закрыть
        private bool btnMaximizeHovered = false; // Наведен ли курсор на кнопку Развернуть/Свернуть в окно
        private bool btnMinimizeHovered = false; // Наведен ли курсор на кнопку Свернуть

        private Pen penBtnClose = new Pen(Color.White, 1.55F); // Кисть для кнопки Закрыть
        private Pen penBtnMaximize = new Pen(Color.DarkGray, 1.55F); // Кисть для кнопки Развернуть/Свернуть в окно
        private Pen penBtnMinimize = new Pen(Color.Gray, 1.55F); // Кисть для кнопки Свернуть

        private Rectangle rectHeader = new Rectangle(); // Структура заголовка формы
        private Rectangle rectBorder = new Rectangle(); // Структура обводки

        private int ResizeBorderSize = 4; // Размер невидимой границы при наведении на которую меняется курсор, чтобы изменять размер формы
        private int ResizeAngleBorderOffset = 15; // Смещение от углов, где мы трактуем угловую часть для изменения размера по углам
        private bool IsResizing = false; // Режим изменения размера

        private BorderHoverPositionEnum BorderHoverPosition = BorderHoverPositionEnum.None; // Куда наведен курсор в отношении обводки для изменения формы
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
            DefineStyles();

            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// Определение стилей (тем)
        /// </summary>
        private void DefineStyles()
        {
            StylesDictionary = new Dictionary<fStyle, EgoldsStyle>();

            // Стиль стандартной формы, без параметров, кроме HeaderHeight
            StylesDictionary.Add(fStyle.None,
                new EgoldsStyle()
                {
                    HeaderHeight = 0
                });

            // Стиль пользователя, на основе параметров компонента, параметры устанавливаются в SaveUserStyleConfig()
            StylesDictionary.Add(fStyle.UserStyle,
                new EgoldsStyle()
                {
                    FormBorderStyle = FormBorderStyle.None
                });

            // Стиль SimpleDark
            StylesDictionary.Add(fStyle.SimpleDark,
                new EgoldsStyle()
                {
                    FormBorderStyle = FormBorderStyle.None,
                    BackColor = Color.White,
                    HeaderHeight = 38,
                    HeaderColor = FlatColors.WetAsphalt,
                    HeaderTextColor = Color.White,
                    HeaderTextFont = new Font("Segoe UI", 9.75F, FontStyle.Regular),
                    ControlBoxButtonsWidth = HeaderHeight,
                    ControlBoxIconsSize = new Size(10, 10),
                    UseSecondControlBoxIconsColorOnHover = false, // <-
                    ControlBoxEnabledIconsColor = Color.White
                });

            // Стиль TelegramStyle
            StylesDictionary.Add(fStyle.TelegramStyle,
                new EgoldsStyle()
                {
                    FormBorderStyle = FormBorderStyle.None,
                    BackColor = Color.White,
                    HeaderHeight = 20,
                    HeaderColor = FlatColors.GrayLight2,
                    HeaderTextColor = Color.Gray,
                    HeaderTextFont = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ControlBoxButtonsWidth = 24,
                    ControlBoxIconsSize = new Size(8, 8),
                    UseSecondControlBoxIconsColorOnHover = true,
                    ControlBoxEnabledIconsColor = Color.Gray,
                    ControlBoxOnHoverIconsColor = Color.White
                });
        }

        /// <summary>
        /// Сохранение значения свойста FormBorderStyle стандартной формы в стиль "NoneStyle", выполняется только один раз
        /// </summary>
        private void SaveNoneStyleConfig()
        {
            if (StylesDictionary.ContainsKey(fStyle.None) && StyleUsed == false)
            {
                EgoldsStyle NoneStyle = StylesDictionary[fStyle.None];

                NoneStyle.FormBorderStyle = Form.FormBorderStyle;
                NoneStyle.BackColor = Form.BackColor;
            }
        }

        private void SaveUserStyleConfig()
        {
            if (StylesDictionary.ContainsKey(fStyle.UserStyle) && StyleUsed == false)
            {
                // Стиль пользователя, на основе параметров компонента
                EgoldsStyle UserStyle = StylesDictionary[fStyle.UserStyle];

                UserStyle.FormBorderStyle = FormBorderStyle.None;
                UserStyle.HeaderHeight = HeaderHeight;
                UserStyle.HeaderColor = HeaderColor;
                UserStyle.BackColor = BackColor;
                UserStyle.HeaderTextColor = HeaderTextColor;
                UserStyle.HeaderTextFont = HeaderTextFont;
                UserStyle.ControlBoxButtonsWidth = ControlBoxButtonsWidth;
                UserStyle.ControlBoxIconsSize = ControlBoxIconSize;
            }
        }

        /// <summary>
        /// Подписываем форме новое событие, для применения стилей
        /// </summary>
        private void Sign()
        {
            if (Form != null)
            {
                // Используем событие HandleCreated, так как оно вызывается перед тем как форма загрузится
                Form.HandleCreated += Form_HandleCreated;

                // На случай если изначально форма имела стандартный стиль, тоесть не использовался данный компонент и форма уже загружена
                if (Form.IsHandleCreated)
                {
                    Apply();
                    Form.Refresh();
                }
            }
        }

        /// <summary>
        /// Первоначальная настройка формы
        /// </summary>
        private void Apply()
        {
            SaveNoneStyleConfig();
            SaveUserStyleConfig();

            MigrateControls();
            SetStyle(FormStyle);
            StyleUsed = true;

            SF.Alignment = StringAlignment.Near;
            SF.LineAlignment = StringAlignment.Center;

            Size minimumSize = new Size(100, 50);
            if (Form.MinimumSize.Width < minimumSize.Width || Form.MinimumSize.Height < minimumSize.Height)
                Form.MinimumSize = minimumSize;

            SetDoubleBuffered(Form);

            // Цепляем необходимые события к форме
            Form.Paint += Form_Paint;
            Form.MouseDown += Form_MouseDown;
            Form.MouseUp += Form_MouseUp;
            Form.MouseMove += Form_MouseMove;
            Form.MouseLeave += Form_MouseLeave;
            Form.SizeChanged += Form_SizeChanged;
            Form.DoubleClick += Form_DoubleClick;
            Form.Click += Form_Click;
            Form.BackColorChanged += Form_BackColorChanged;
            
        }
        
        /// <summary>
        /// Изменение стиля
        /// </summary>
        /// <param name="Style">Стиль из набора fStyle</param>
        private void SetStyle(fStyle Style)
        {
            FormWindowState formWindowStateTEMP = Form.WindowState;
            Form.WindowState = FormWindowState.Normal;

            if (StyleUsed)
            {
                //OffsetControls(-HeaderHeight);
                OffsetMainContrainer(-HeaderHeight);
                Form.Height -= HeaderHeight;
            }

            CurrentStyle = StylesDictionary[Style];

            HeaderHeight = CurrentStyle.HeaderHeight;
            HeaderColor = CurrentStyle.HeaderColor;
            BackColor = CurrentStyle.BackColor;
            HeaderTextFont = CurrentStyle.HeaderTextFont;
            HeaderTextColor = CurrentStyle.HeaderTextColor;
            IconSize = CurrentStyle.IconSize;
            ControlBoxIconSize = CurrentStyle.ControlBoxIconsSize;
            ControlBoxButtonsWidth = CurrentStyle.ControlBoxButtonsWidth;

            Form.BackColor = BackColor;

            //OffsetControls(HeaderHeight);
            OffsetMainContrainer(HeaderHeight);

            Form.Height += HeaderHeight;
            Form.Refresh();

            Form.FormBorderStyle = CurrentStyle.FormBorderStyle;

            Form.WindowState = formWindowStateTEMP;
        }

        /// <summary>
        /// Смещение контролов
        /// </summary>
        /// <param name="offsett"></param>
        private void OffsetControls(int offsett)
        {
            foreach (Control ctrl in Form.Controls)
            {
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y + offsett);
                ctrl.Refresh();
            }
        }

        private void MigrateControls()
        {
            // Определение панели-контейнера
            MainContainer = new Panel();
            MainContainer.BackColor = Form.BackColor;
            MainContainer.Location = new Point(1, HeaderHeight + 1);
            ChangeMainContainerSize();

            // Перенос контролов на отдельную панель

            if (FormControls.Count == 0)
                FormControls.AddRange(Form.Controls.OfType<Control>());

            Form.Controls.Clear();

            if (FormControls.Count > 0)
                MainContainer.Controls.AddRange(FormControls.ToArray());

            Form.Controls.Add(MainContainer);
        }

        private void OffsetMainContrainer(int offset)
        {
            MainContainer.Location = new Point(MainContainer.Location.X, MainContainer.Location.Y + offset);
            MainContainer.Refresh();
        }

        private void ChangeMainContainerSize()
        {
            MainContainer.Size = new Size(Form.Width - 2, Form.Height - HeaderHeight - 2);
        }

        #region -- Form Events --

        // В этом событии выполняется раньше чем в Load
        private void Form_HandleCreated(object sender, EventArgs e)
        {
            Apply();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            if (FormStyle != fStyle.None)
                DrawStyle(e.Graphics);
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            Form.Refresh();
            ChangeMainContainerSize();
        }

        private void Form_BackColorChanged(object sender, EventArgs e)
        {
            MainContainer.BackColor = Form.BackColor;
        }

        private void Form_MouseLeave(object sender, EventArgs e)
        {
            if (FormStyle == fStyle.None) return;

            btnCloseHovered = false;
            btnMaximizeHovered = false;
            btnMinimizeHovered = false;
            Form.Invalidate();
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormStyle == fStyle.None) return;

            // Dragging
            if (CanDragForm && e.Button == MouseButtons.Left)
            {
                if (Form.WindowState == FormWindowState.Maximized)
                {
                    float maxWidth = Form.Width;
                    float cursosOnMaxPosition = e.X;
                    float coeff = cursosOnMaxPosition / (maxWidth / 100f) / 100f;

                    // Change WindowState if is Maximized
                    Form.WindowState = FormWindowState.Normal;

                    int XFormOffset = (int)(Form.Width * coeff);

                    Form.Location = new Point(Cursor.Position.X - XFormOffset, Cursor.Position.Y - HeaderHeight / 2);
                    moveStartPosition = Form.Location;
                    clickPosition = Cursor.Position;
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
            if (Form.IsHandleCreated == false) return;

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
            if (FormStyle == fStyle.None) return;

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
            if (FormStyle == fStyle.None) return;

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

        /// <summary>
        /// Рисование стиля
        /// </summary>
        /// <param name="graph"></param>
        private void DrawStyle(Graphics graph)
        {
            graph.SmoothingMode = SmoothingMode.HighQuality;
            graph.InterpolationMode = InterpolationMode.HighQualityBicubic;

            if (HeaderHeight == 0) return;

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

            // Title Image Structure
            Rectangle rectHeaderImage = new Rectangle();
            if (HeaderImage != null)
            {
                int imageHeight = (int)(HeaderHeight * 0.9f); // Высота картинки = 90% от высоты шапки
                int imageWidth = HeaderImage.Width / (HeaderImage.Height / imageHeight); // Получаем ширину с сохранением пропорций
                rectHeaderImage = new Rectangle(rectIcon.Left, HeaderHeight / 2 - imageHeight / 2, imageWidth, imageHeight);
            }

            // Close Button Structure
            rectBtnClose = new Rectangle(rectHeader.Width - ControlBoxButtonsWidth, rectHeader.Y, ControlBoxButtonsWidth, rectHeader.Height);
            // Crosshair Structure
            Rectangle rectCrosshair = new Rectangle(
                rectBtnClose.X + rectBtnClose.Width / 2 - ControlBoxIconSize.Width / 2,
                rectBtnClose.Height / 2 - ControlBoxIconSize.Height / 2,
                ControlBoxIconSize.Width, ControlBoxIconSize.Height);

            // Maximize Button Structure
            rectBtnMax = new Rectangle(rectBtnClose.X - ControlBoxButtonsWidth, rectHeader.Y, ControlBoxButtonsWidth, rectHeader.Height);
            // Maximize Icon Structure
            Rectangle rectMaxButtonIcon = new Rectangle(
                rectBtnMax.X + rectBtnMax.Width / 2 - ControlBoxIconSize.Width / 2,
                rectBtnMax.Height / 2 - ControlBoxIconSize.Height / 2,
                ControlBoxIconSize.Width, ControlBoxIconSize.Height);
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
            rectBtnMin = new Rectangle(rectBtnMax.X - ControlBoxButtonsWidth, rectHeader.Y, ControlBoxButtonsWidth, rectHeader.Height);

            Point point1BtnMin = new Point(
                    rectBtnMin.X + rectBtnMin.Width / 2 - ControlBoxIconSize.Width / 2,
                    rectBtnMin.Height / 2 + ControlBoxIconSize.Height / 2
                    );
            Point point2BtnMin = new Point(
                rectBtnMin.X + rectBtnMin.Width / 2 + ControlBoxIconSize.Width / 2,
                rectBtnMin.Height / 2 + ControlBoxIconSize.Height / 2
                );

            #endregion
            #region - Drawing -
            
            Brush headerBrush = new SolidBrush(HeaderColor);
            if (HeaderColorGradientEnable)
            {
                if (rectHeader.Width > 0 && rectHeader.Height > 0)
                    headerBrush = new LinearGradientBrush(rectHeader, HeaderColor, HeaderColorAdditional, HeaderColorGradientMode);
            }

            // Шапка / Header
            graph.DrawRectangle(new Pen(headerBrush), rectHeader);
            graph.FillRectangle(headerBrush, rectHeader);

            if (HeaderImage != null)
            {
                // Картинка, вместо заголовка и иконки
                graph.DrawImage(HeaderImage, rectHeaderImage);
            }
            else
            {
                // Текст заголовка / Title
                graph.DrawString(Form.Text, HeaderTextFont, new SolidBrush(HeaderTextColor), rectTitleText, SF);

                // Иконка / Icon
                if (Form.ShowIcon)
                {
                    graph.DrawImage(Form.Icon.ToBitmap(), rectIcon);
                }
            }

            if (Form.ControlBox == true)
            {
                #region - Icon Color Changing -

                if (CurrentStyle.UseSecondControlBoxIconsColorOnHover)
                {
                    penBtnClose.Color = btnCloseHovered ? CurrentStyle.ControlBoxOnHoverIconsColor : CurrentStyle.ControlBoxEnabledIconsColor;
                    penBtnMaximize.Color = btnMaximizeHovered ? CurrentStyle.ControlBoxOnHoverIconsColor : CurrentStyle.ControlBoxEnabledIconsColor;
                    penBtnMinimize.Color = btnMinimizeHovered ? CurrentStyle.ControlBoxOnHoverIconsColor : CurrentStyle.ControlBoxEnabledIconsColor;
                }
                else
                {
                    penBtnClose.Color = penBtnMaximize.Color = penBtnMinimize.Color = CurrentStyle.ControlBoxEnabledIconsColor;
                }
                if (Form.MaximizeBox == false)
                    penBtnMaximize.Color = CurrentStyle.ControlBoxDisabledIconsColor;
                if (Form.MinimizeBox == false)
                    penBtnMinimize.Color = CurrentStyle.ControlBoxDisabledIconsColor;

                #endregion

                // Кнопка Х
                #region - Button X -

                graph.DrawRectangle(new Pen(btnCloseHovered ? FlatColors.Red2 : Color.Transparent), rectBtnClose);
                graph.FillRectangle(new SolidBrush(btnCloseHovered ? FlatColors.Red2 : Color.Transparent), rectBtnClose);
                DrawCrosshair(graph, rectCrosshair, penBtnClose);

                #endregion

                // Кнопка [MAX]
                #region - Button Maximize -

                graph.DrawRectangle(new Pen(btnMaximizeHovered && Form.MaximizeBox ? Color.FromArgb(100, Color.Gray) : Color.Transparent), rectBtnMax);
                graph.FillRectangle(new SolidBrush(btnMaximizeHovered && Form.MaximizeBox ? Color.FromArgb(100, Color.Gray) : Color.Transparent), rectBtnMax);

                if (EnableControlBoxIconsLight)
                {
                    if (Form.WindowState == FormWindowState.Maximized)
                    {
                        Drawer.DrawBlurredRectangle(graph, Color.White, rectMaxButtonIconSecond, 8, 20);
                    }
                    Drawer.DrawBlurredRectangle(graph, Color.White, rectMaxButtonIcon, 8, 20);
                }

                // Draw icon
                if (Form.WindowState == FormWindowState.Maximized)
                {
                    graph.DrawRectangle(penBtnMaximize, rectMaxButtonIconSecond);
                    //graph.FillRectangle(new SolidBrush(HeaderColor), rectMaxButtonIcon);
                    graph.FillRectangle(headerBrush, rectMaxButtonIcon);
                }
                graph.DrawRectangle(penBtnMaximize, rectMaxButtonIcon);

                #endregion

                // Кнопка [ _ ]
                #region - Button Minimize -

                graph.DrawRectangle(new Pen(btnMinimizeHovered && Form.MinimizeBox ? Color.FromArgb(100, Color.Gray) : Color.Transparent), rectBtnMin);
                graph.FillRectangle(new SolidBrush(btnMinimizeHovered && Form.MinimizeBox ? Color.FromArgb(100, Color.Gray) : Color.Transparent), rectBtnMin);

                // Draw icon
                if (EnableControlBoxIconsLight)
                {
                    Drawer.DrawBlurredLine(graph, Color.White, point1BtnMin, point2BtnMin, 8, 20);
                }
                graph.DrawLine(penBtnMinimize, point1BtnMin, point2BtnMin);

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

        /// <summary>
        /// Рисование крестика
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="rect"></param>
        /// <param name="pen"></param>
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

        /// <summary>
        /// Включение DoubleBuffered, для предотвращения мерцания
        /// </summary>
        /// <param name="c"></param>
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
