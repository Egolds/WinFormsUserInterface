// Это форма для тестов
// This from for tests


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using yt_DesignUI.Controls;

namespace yt_DesignUI
{
    public partial class frmMain : ShadowedForm
    {
        public frmMain()
        {
            InitializeComponent();

            buttonAnim.Value = button1.Width;
            Animator.Start();

            //DoubleBuffered = true;
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Моя кнопка!");
        }

        bool on = false;

        private void button1_Click(object sender, EventArgs e)
        {
            //Graphics graph = pictureBox1.CreateGraphics();

            //Pen pen = new Pen(new SolidBrush(EgoldsUI.FlatColors.GrayDark), 3);
            //Pen penButton = new Pen(new SolidBrush(EgoldsUI.FlatColors.GrayDark), 3);

            //Rectangle rect = new Rectangle(10, 10, 250, 98);
            //Rectangle rectButton = new Rectangle(10, 10, rect.Height, rect.Height);
            //int radius = 98;

            //var gp = new GraphicsPath();
            //graph.SmoothingMode = SmoothingMode.HighQuality;
            //graph.Clear(Color.White);

            //gp.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            //gp.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            //gp.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            //gp.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            //gp.CloseFigure();

            //var gpv = RoundedRect(rect, 98);

            //graph.DrawPath(pen, gpv);
            //graph.FillPath(new SolidBrush(Color.Tomato), gpv);

            //graph.DrawEllipse(penButton, rectButton);
            //graph.FillEllipse(new SolidBrush(Color.White), rectButton);
        }
        
        private void egoldsToogleSwitch2_Validated(object sender, EventArgs e)
        {

        }


        Animation buttonAnim = new Animation();
        int BtnWidthTarget = 150;
        int BtnWidthBase = 75;

        private void test_button_animation(bool MouseEnter)
        {
            if (MouseEnter)
            {
                buttonAnim = new Animation("test", button1.Invalidate, button1.Width, BtnWidthTarget);
            }
            else
            {
                buttonAnim = new Animation("test", button1.Invalidate, button1.Width, BtnWidthBase);
            }

            buttonAnim.StepDivider = 6;
            Animator.Request(buttonAnim, true);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            test_button_animation(true);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            test_button_animation(false);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            button1.Width = (int)buttonAnim.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            egoldsProgressBar1.Value = trackBar1.Value;
        }

        private void contextExitBtn(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
