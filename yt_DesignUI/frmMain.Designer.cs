namespace yt_DesignUI
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.egoldsGoogleTextBox1 = new yt_DesignUI.EgoldsGoogleTextBox();
            this.egoldsProgressBar1 = new yt_DesignUI.EgoldsProgressBar();
            this.yt_Button3 = new yt_DesignUI.yt_Button();
            this.egoldsCard1 = new yt_DesignUI.EgoldsCard();
            this.yt_Button1 = new yt_DesignUI.yt_Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.egoldsFormStyle1 = new yt_DesignUI.Components.EgoldsFormStyle(this.components);
            this.cmbStyle = new System.Windows.Forms.ComboBox();
            this.egoldsToggleSwitch1 = new yt_DesignUI.EgoldsToggleSwitch();
            this.btnGradient = new yt_DesignUI.yt_Button();
            this.egoldsToggleSwitch2 = new yt_DesignUI.EgoldsToggleSwitch();
            this.yt_Button4 = new yt_DesignUI.yt_Button();
            this.yt_Button5 = new yt_DesignUI.yt_Button();
            this.yt_Button6 = new yt_DesignUI.yt_Button();
            this.yt_Button7 = new yt_DesignUI.yt_Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.yt_Button2 = new yt_DesignUI.yt_Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Paint += new System.Windows.Forms.PaintEventHandler(this.button1_Paint);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // egoldsGoogleTextBox1
            // 
            this.egoldsGoogleTextBox1.BackColor = System.Drawing.Color.White;
            this.egoldsGoogleTextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.egoldsGoogleTextBox1.BorderColorNotActive = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.egoldsGoogleTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.egoldsGoogleTextBox1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.egoldsGoogleTextBox1.FontTextPreview = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.egoldsGoogleTextBox1.ForeColor = System.Drawing.Color.Black;
            this.egoldsGoogleTextBox1.Location = new System.Drawing.Point(12, 330);
            this.egoldsGoogleTextBox1.Name = "egoldsGoogleTextBox1";
            this.egoldsGoogleTextBox1.Size = new System.Drawing.Size(157, 40);
            this.egoldsGoogleTextBox1.TabIndex = 8;
            this.egoldsGoogleTextBox1.TextInput = "";
            this.egoldsGoogleTextBox1.TextPreview = "Input text";
            this.egoldsGoogleTextBox1.UseSystemPasswordChar = false;
            // 
            // egoldsProgressBar1
            // 
            this.egoldsProgressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.egoldsProgressBar1.BackColorProgressLeft = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.egoldsProgressBar1.BackColorProgressRight = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(188)))), ((int)(((byte)(156)))));
            this.egoldsProgressBar1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.egoldsProgressBar1.Location = new System.Drawing.Point(12, 261);
            this.egoldsProgressBar1.Name = "egoldsProgressBar1";
            this.egoldsProgressBar1.Size = new System.Drawing.Size(157, 20);
            this.egoldsProgressBar1.Step = 10;
            this.egoldsProgressBar1.TabIndex = 7;
            this.egoldsProgressBar1.Text = "egoldsProgressBar1";
            this.egoldsProgressBar1.Value = 73;
            this.egoldsProgressBar1.ValueMaximum = 100;
            this.egoldsProgressBar1.ValueMinimum = 0;
            // 
            // yt_Button3
            // 
            this.yt_Button3.BackColor = System.Drawing.Color.Tomato;
            this.yt_Button3.BackColorAdditional = System.Drawing.Color.Gray;
            this.yt_Button3.BackColorGradientEnabled = false;
            this.yt_Button3.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.yt_Button3.BorderColor = System.Drawing.Color.Tomato;
            this.yt_Button3.BorderColorEnabled = false;
            this.yt_Button3.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.yt_Button3.BorderColorOnHoverEnabled = false;
            this.yt_Button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yt_Button3.ForeColor = System.Drawing.Color.White;
            this.yt_Button3.Location = new System.Drawing.Point(719, 226);
            this.yt_Button3.Name = "yt_Button3";
            this.yt_Button3.RippleColor = System.Drawing.Color.Black;
            this.yt_Button3.RoundingEnable = false;
            this.yt_Button3.Size = new System.Drawing.Size(141, 30);
            this.yt_Button3.TabIndex = 6;
            this.yt_Button3.Text = "Hover me";
            this.yt_Button3.TextHover = "You are Awesome!";
            this.yt_Button3.UseDownPressEffectOnClick = false;
            this.yt_Button3.UseRippleEffect = true;
            this.yt_Button3.UseZoomEffectOnHover = false;
            // 
            // egoldsCard1
            // 
            this.egoldsCard1.BackColor = System.Drawing.Color.White;
            this.egoldsCard1.BackColorCurtain = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.egoldsCard1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.egoldsCard1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.egoldsCard1.FontDescrition = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.egoldsCard1.FontHeader = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.egoldsCard1.ForeColorDescrition = System.Drawing.Color.Black;
            this.egoldsCard1.ForeColorHeader = System.Drawing.Color.White;
            this.egoldsCard1.Location = new System.Drawing.Point(12, 12);
            this.egoldsCard1.Name = "egoldsCard1";
            this.egoldsCard1.Size = new System.Drawing.Size(157, 177);
            this.egoldsCard1.TabIndex = 1;
            this.egoldsCard1.Text = "egoldsCard1";
            this.egoldsCard1.TextDescrition = "Your description text for this control";
            this.egoldsCard1.TextHeader = "Header";
            // 
            // yt_Button1
            // 
            this.yt_Button1.BackColor = System.Drawing.Color.Tomato;
            this.yt_Button1.BackColorAdditional = System.Drawing.Color.Gray;
            this.yt_Button1.BackColorGradientEnabled = false;
            this.yt_Button1.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.yt_Button1.BorderColor = System.Drawing.Color.Tomato;
            this.yt_Button1.BorderColorEnabled = false;
            this.yt_Button1.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.yt_Button1.BorderColorOnHoverEnabled = false;
            this.yt_Button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yt_Button1.ForeColor = System.Drawing.Color.White;
            this.yt_Button1.Location = new System.Drawing.Point(12, 195);
            this.yt_Button1.Name = "yt_Button1";
            this.yt_Button1.RippleColor = System.Drawing.Color.Black;
            this.yt_Button1.RoundingEnable = false;
            this.yt_Button1.Size = new System.Drawing.Size(157, 31);
            this.yt_Button1.TabIndex = 0;
            this.yt_Button1.Text = "yt_Button1";
            this.yt_Button1.TextHover = null;
            this.yt_Button1.UseDownPressEffectOnClick = false;
            this.yt_Button1.UseRippleEffect = true;
            this.yt_Button1.UseZoomEffectOnHover = false;
            this.yt_Button1.Click += new System.EventHandler(this.yt_Button1_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(12, 287);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(157, 37);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(97, 48);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.testToolStripMenuItem.Text = "Test";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.contextExitBtn);
            // 
            // egoldsFormStyle1
            // 
            this.egoldsFormStyle1.AllowUserResize = false;
            this.egoldsFormStyle1.BackColor = System.Drawing.Color.White;
            this.egoldsFormStyle1.ContextMenuForm = null;
            this.egoldsFormStyle1.ControlBoxButtonsWidth = 60;
            this.egoldsFormStyle1.EnableControlBoxIconsLight = false;
            this.egoldsFormStyle1.EnableControlBoxMouseLight = false;
            this.egoldsFormStyle1.Form = this;
            this.egoldsFormStyle1.FormStyle = yt_DesignUI.Components.EgoldsFormStyle.fStyle.SimpleDark;
            this.egoldsFormStyle1.HeaderColor = System.Drawing.Color.Violet;
            this.egoldsFormStyle1.HeaderColorAdditional = System.Drawing.Color.RoyalBlue;
            this.egoldsFormStyle1.HeaderColorGradientEnable = true;
            this.egoldsFormStyle1.HeaderColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.egoldsFormStyle1.HeaderHeight = 48;
            this.egoldsFormStyle1.HeaderImage = null;
            this.egoldsFormStyle1.HeaderTextColor = System.Drawing.Color.White;
            this.egoldsFormStyle1.HeaderTextFont = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            // 
            // cmbStyle
            // 
            this.cmbStyle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyle.FormattingEnabled = true;
            this.cmbStyle.Location = new System.Drawing.Point(706, 27);
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.Size = new System.Drawing.Size(154, 21);
            this.cmbStyle.TabIndex = 14;
            this.cmbStyle.SelectedIndexChanged += new System.EventHandler(this.cmbStyle_SelectedIndexChanged);
            // 
            // egoldsToggleSwitch1
            // 
            this.egoldsToggleSwitch1.BackColor = System.Drawing.Color.White;
            this.egoldsToggleSwitch1.BackColorOFF = System.Drawing.Color.Silver;
            this.egoldsToggleSwitch1.BackColorON = System.Drawing.Color.DodgerBlue;
            this.egoldsToggleSwitch1.Checked = true;
            this.egoldsToggleSwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.egoldsToggleSwitch1.Font = new System.Drawing.Font("Verdana", 9F);
            this.egoldsToggleSwitch1.Location = new System.Drawing.Point(12, 382);
            this.egoldsToggleSwitch1.Name = "egoldsToggleSwitch1";
            this.egoldsToggleSwitch1.Size = new System.Drawing.Size(69, 15);
            this.egoldsToggleSwitch1.TabIndex = 17;
            this.egoldsToggleSwitch1.Text = "OFF";
            this.egoldsToggleSwitch1.TextOnChecked = "ON";
            // 
            // btnGradient
            // 
            this.btnGradient.BackColor = System.Drawing.Color.Violet;
            this.btnGradient.BackColorAdditional = System.Drawing.Color.MediumTurquoise;
            this.btnGradient.BackColorGradientEnabled = true;
            this.btnGradient.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.btnGradient.BorderColor = System.Drawing.Color.Tomato;
            this.btnGradient.BorderColorEnabled = false;
            this.btnGradient.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.btnGradient.BorderColorOnHoverEnabled = false;
            this.btnGradient.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGradient.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnGradient.ForeColor = System.Drawing.Color.White;
            this.btnGradient.Location = new System.Drawing.Point(719, 190);
            this.btnGradient.Name = "btnGradient";
            this.btnGradient.RippleColor = System.Drawing.Color.Black;
            this.btnGradient.RoundingEnable = true;
            this.btnGradient.Size = new System.Drawing.Size(141, 30);
            this.btnGradient.TabIndex = 18;
            this.btnGradient.Text = "Flat Button";
            this.btnGradient.TextHover = null;
            this.btnGradient.UseDownPressEffectOnClick = false;
            this.btnGradient.UseRippleEffect = true;
            this.btnGradient.UseZoomEffectOnHover = false;
            // 
            // egoldsToggleSwitch2
            // 
            this.egoldsToggleSwitch2.BackColor = System.Drawing.Color.White;
            this.egoldsToggleSwitch2.BackColorOFF = System.Drawing.Color.Silver;
            this.egoldsToggleSwitch2.BackColorON = System.Drawing.Color.DodgerBlue;
            this.egoldsToggleSwitch2.Checked = false;
            this.egoldsToggleSwitch2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.egoldsToggleSwitch2.Font = new System.Drawing.Font("Verdana", 9F);
            this.egoldsToggleSwitch2.Location = new System.Drawing.Point(12, 403);
            this.egoldsToggleSwitch2.Name = "egoldsToggleSwitch2";
            this.egoldsToggleSwitch2.Size = new System.Drawing.Size(74, 15);
            this.egoldsToggleSwitch2.TabIndex = 19;
            this.egoldsToggleSwitch2.Text = "OFF";
            this.egoldsToggleSwitch2.TextOnChecked = "ON";
            // 
            // yt_Button4
            // 
            this.yt_Button4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.yt_Button4.BackColorAdditional = System.Drawing.Color.Gainsboro;
            this.yt_Button4.BackColorGradientEnabled = true;
            this.yt_Button4.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.yt_Button4.BorderColor = System.Drawing.Color.DimGray;
            this.yt_Button4.BorderColorEnabled = true;
            this.yt_Button4.BorderColorOnHover = System.Drawing.Color.Black;
            this.yt_Button4.BorderColorOnHoverEnabled = true;
            this.yt_Button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yt_Button4.ForeColor = System.Drawing.Color.Black;
            this.yt_Button4.Location = new System.Drawing.Point(719, 58);
            this.yt_Button4.Name = "yt_Button4";
            this.yt_Button4.RippleColor = System.Drawing.Color.Black;
            this.yt_Button4.Rounding = 40;
            this.yt_Button4.RoundingEnable = true;
            this.yt_Button4.Size = new System.Drawing.Size(141, 27);
            this.yt_Button4.TabIndex = 22;
            this.yt_Button4.Text = "Gray button";
            this.yt_Button4.TextHover = null;
            this.yt_Button4.UseDownPressEffectOnClick = true;
            this.yt_Button4.UseRippleEffect = true;
            this.yt_Button4.UseZoomEffectOnHover = false;
            this.yt_Button4.Click += new System.EventHandler(this.yt_Button4_Click);
            // 
            // yt_Button5
            // 
            this.yt_Button5.BackColor = System.Drawing.Color.LightGreen;
            this.yt_Button5.BackColorAdditional = System.Drawing.Color.SeaGreen;
            this.yt_Button5.BackColorGradientEnabled = true;
            this.yt_Button5.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.yt_Button5.BorderColor = System.Drawing.Color.DimGray;
            this.yt_Button5.BorderColorEnabled = true;
            this.yt_Button5.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.yt_Button5.BorderColorOnHoverEnabled = false;
            this.yt_Button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yt_Button5.ForeColor = System.Drawing.Color.White;
            this.yt_Button5.Location = new System.Drawing.Point(719, 91);
            this.yt_Button5.Name = "yt_Button5";
            this.yt_Button5.RippleColor = System.Drawing.Color.Black;
            this.yt_Button5.Rounding = 40;
            this.yt_Button5.RoundingEnable = true;
            this.yt_Button5.Size = new System.Drawing.Size(141, 27);
            this.yt_Button5.TabIndex = 23;
            this.yt_Button5.Text = "Green button";
            this.yt_Button5.TextHover = null;
            this.yt_Button5.UseDownPressEffectOnClick = true;
            this.yt_Button5.UseRippleEffect = true;
            this.yt_Button5.UseZoomEffectOnHover = false;
            // 
            // yt_Button6
            // 
            this.yt_Button6.BackColor = System.Drawing.Color.SkyBlue;
            this.yt_Button6.BackColorAdditional = System.Drawing.Color.RoyalBlue;
            this.yt_Button6.BackColorGradientEnabled = true;
            this.yt_Button6.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.yt_Button6.BorderColor = System.Drawing.Color.DimGray;
            this.yt_Button6.BorderColorEnabled = true;
            this.yt_Button6.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.yt_Button6.BorderColorOnHoverEnabled = false;
            this.yt_Button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yt_Button6.ForeColor = System.Drawing.Color.White;
            this.yt_Button6.Location = new System.Drawing.Point(719, 124);
            this.yt_Button6.Name = "yt_Button6";
            this.yt_Button6.RippleColor = System.Drawing.Color.Black;
            this.yt_Button6.Rounding = 40;
            this.yt_Button6.RoundingEnable = true;
            this.yt_Button6.Size = new System.Drawing.Size(141, 27);
            this.yt_Button6.TabIndex = 24;
            this.yt_Button6.Text = "Zoom button";
            this.yt_Button6.TextHover = null;
            this.yt_Button6.UseDownPressEffectOnClick = true;
            this.yt_Button6.UseRippleEffect = true;
            this.yt_Button6.UseZoomEffectOnHover = true;
            // 
            // yt_Button7
            // 
            this.yt_Button7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.yt_Button7.BackColorAdditional = System.Drawing.Color.Gainsboro;
            this.yt_Button7.BackColorGradientEnabled = false;
            this.yt_Button7.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.yt_Button7.BorderColor = System.Drawing.Color.DimGray;
            this.yt_Button7.BorderColorEnabled = true;
            this.yt_Button7.BorderColorOnHover = System.Drawing.Color.Black;
            this.yt_Button7.BorderColorOnHoverEnabled = true;
            this.yt_Button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.yt_Button7.ForeColor = System.Drawing.Color.Black;
            this.yt_Button7.Location = new System.Drawing.Point(719, 157);
            this.yt_Button7.Name = "yt_Button7";
            this.yt_Button7.RippleColor = System.Drawing.Color.Black;
            this.yt_Button7.Rounding = 40;
            this.yt_Button7.RoundingEnable = true;
            this.yt_Button7.Size = new System.Drawing.Size(141, 27);
            this.yt_Button7.TabIndex = 25;
            this.yt_Button7.Text = "Flat button";
            this.yt_Button7.TextHover = null;
            this.yt_Button7.UseDownPressEffectOnClick = true;
            this.yt_Button7.UseRippleEffect = true;
            this.yt_Button7.UseZoomEffectOnHover = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // yt_Button2
            // 
            this.yt_Button2.BackColor = System.Drawing.Color.Tomato;
            this.yt_Button2.BackColorAdditional = System.Drawing.Color.Gray;
            this.yt_Button2.BackColorGradientEnabled = false;
            this.yt_Button2.BackColorGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.yt_Button2.BorderColor = System.Drawing.Color.Tomato;
            this.yt_Button2.BorderColorEnabled = false;
            this.yt_Button2.BorderColorOnHover = System.Drawing.Color.Tomato;
            this.yt_Button2.BorderColorOnHoverEnabled = false;
            this.yt_Button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.yt_Button2.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.yt_Button2.ForeColor = System.Drawing.Color.White;
            this.yt_Button2.Location = new System.Drawing.Point(202, 348);
            this.yt_Button2.Name = "yt_Button2";
            this.yt_Button2.RippleColor = System.Drawing.Color.Black;
            this.yt_Button2.RoundingEnable = true;
            this.yt_Button2.Size = new System.Drawing.Size(217, 49);
            this.yt_Button2.TabIndex = 26;
            this.yt_Button2.Text = "yt_Button2";
            this.yt_Button2.TextHover = null;
            this.yt_Button2.UseDownPressEffectOnClick = false;
            this.yt_Button2.UseRippleEffect = true;
            this.yt_Button2.UseZoomEffectOnHover = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(872, 459);
            this.Controls.Add(this.yt_Button2);
            this.Controls.Add(this.yt_Button7);
            this.Controls.Add(this.yt_Button6);
            this.Controls.Add(this.yt_Button5);
            this.Controls.Add(this.yt_Button4);
            this.Controls.Add(this.cmbStyle);
            this.Controls.Add(this.egoldsToggleSwitch2);
            this.Controls.Add(this.btnGradient);
            this.Controls.Add(this.egoldsToggleSwitch1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.egoldsGoogleTextBox1);
            this.Controls.Add(this.egoldsProgressBar1);
            this.Controls.Add(this.yt_Button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.egoldsCard1);
            this.Controls.Add(this.yt_Button1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private yt_Button yt_Button1;
        private EgoldsCard egoldsCard1;
        private System.Windows.Forms.Button button1;
        private yt_Button yt_Button3;
        private EgoldsProgressBar egoldsProgressBar1;
        private EgoldsGoogleTextBox egoldsGoogleTextBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private Components.EgoldsFormStyle egoldsFormStyle1;
        private System.Windows.Forms.ComboBox cmbStyle;
        private EgoldsToggleSwitch egoldsToggleSwitch1;
        private EgoldsToggleSwitch egoldsToggleSwitch2;
        private yt_Button btnGradient;
        private yt_Button yt_Button4;
        private yt_Button yt_Button5;
        private yt_Button yt_Button6;
        private yt_Button yt_Button7;
        private System.Windows.Forms.Timer timer1;
        private yt_Button yt_Button2;
    }
}

