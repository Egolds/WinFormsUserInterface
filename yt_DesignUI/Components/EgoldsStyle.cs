using System.Drawing;
using System.Windows.Forms;

namespace yt_DesignUI.Components
{
    public class EgoldsStyle
    {
        /// <summary>
        /// Определяет внешний вид и поведение границы и строки заголовка формы
        /// </summary>
        public FormBorderStyle FormBorderStyle { get; set; } = FormBorderStyle.None;

        /// <summary>
        /// Высота загоовка
        /// </summary>
        public int HeaderHeight { get; set; }

        /// <summary>
        /// Цвет заголовки
        /// </summary>
        public Color HeaderColor { get; set; }

        /// <summary>
        /// Шрифт текста заголовка (По умолчанию: Segoe UI, 9.75F, Regular)
        /// </summary>
        public Font HeaderTextFont { get; set; } = new Font("Segoe UI", 9.75F, FontStyle.Regular);

        /// <summary>
        /// Цвет текста заголовки (По умолчанию: Color.White)
        /// </summary>
        public Color HeaderTextColor { get; set; } = Color.White;

        /// <summary>
        /// Фоновый цвет формы
        /// </summary>
        public Color BackColor { get; set; } = Color.White;

        /// <summary>
        /// Размеры иконки (По умолчанию: 14, 14)
        /// </summary>
        public Size IconSize { get; set; } = new Size(14, 14);

        /// <summary>
        /// Ширина иконок на кнопках заголовка формы (По умолчанию: 10)
        /// </summary>
        public Size ControlBoxIconsSize { get; set; } = new Size(10, 10);

        /// <summary>
        /// Ширина кнопок заголока формы
        /// </summary>
        public int ControlBoxButtonsWidth { get; set; }

        /// <summary>
        /// Цвет иконок на включенных (Enabled) кнопках заголовка формы (По умолчанию: Color.White)
        /// </summary>
        public Color ControlBoxEnabledIconsColor { get; set; } = Color.White;

        /// <summary>
        /// Цвет иконок на выключенных (Disabled) кнопках заголовка формы (По умолчанию: Color.DarkGray)
        /// </summary>
        public Color ControlBoxDisabledIconsColor { get; set; } = Color.DarkGray;

        /// <summary>
        /// Цвет иконок на кнопках заголовка формы при наведении указателя мыши на кнопку (По умолчанию: Color.Gray)
        /// </summary>
        public Color ControlBoxOnHoverIconsColor { get; set; } = Color.Gray;

        /// <summary>
        /// Если это свойство установлено на True - при наведени указателя мыши на кнопку заголовка, 
        /// для отрисовки иконки кнопки заголовка, будет использован второй цвет ControlBoxHoveredIconsColor (По умолчанию: false)
        /// </summary>
        public bool UseSecondControlBoxIconsColorOnHover { get; set; } = false;
    }
}
