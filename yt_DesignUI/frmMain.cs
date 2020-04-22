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
using yt_DesignUI.Components;
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


            if (cmbStyle.Items.Count == 0)
            {
                EgoldsFormStyle.fStyle selectedStyle = egoldsFormStyle1.FormStyle;
                cmbStyle.DataSource = Enum.GetValues(typeof(EgoldsFormStyle.fStyle));
                cmbStyle.SelectedItem = selectedStyle;
            }
        }

        private void yt_Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Моя кнопка!");
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

        int i = 0;
        private void yt_Button5_Click(object sender, EventArgs e)
        {
            i++;
            yt_Button1.Text = i.ToString();
        }

        private void cmbStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            egoldsFormStyle1.FormStyle = (EgoldsFormStyle.fStyle)cmbStyle.SelectedItem;
        }

        private void btnFlatButton_Click(object sender, EventArgs e)
        {
            egoldsToggleSwitch1.Checked = !egoldsToggleSwitch1.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (btnGradient.BackColorGradientMode)
            {
                case LinearGradientMode.ForwardDiagonal:
                    btnGradient.BackColorGradientMode = LinearGradientMode.Vertical;
                    break;

                case LinearGradientMode.Vertical:
                    btnGradient.BackColorGradientMode = LinearGradientMode.BackwardDiagonal;
                    break;

                case LinearGradientMode.BackwardDiagonal:
                    btnGradient.BackColorGradientMode = LinearGradientMode.Horizontal;
                    break;

                case LinearGradientMode.Horizontal:
                    btnGradient.BackColorGradientMode = LinearGradientMode.ForwardDiagonal;
                    break;
            }

            btnGradient.Refresh();
        }
    }
}
