namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            LbAbout2 = new Label();
            LbAbout1 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // LbAbout2
            // 
            LbAbout2.AutoSize = true;
            LbAbout2.ForeColor = SystemColors.Control;
            LbAbout2.Location = new Point(22, 184);
            LbAbout2.Name = "LbAbout2";
            LbAbout2.Size = new Size(182, 30);
            LbAbout2.TabIndex = 9;
            LbAbout2.Text = "This software is made possible by\r\nTutorialesVbNET and Flaticon.";
            // 
            // LbAbout1
            // 
            LbAbout1.AutoSize = true;
            LbAbout1.Font = new Font("Segoe UI", 9F);
            LbAbout1.ForeColor = SystemColors.Control;
            LbAbout1.Location = new Point(22, 106);
            LbAbout1.Name = "LbAbout1";
            LbAbout1.Size = new Size(257, 60);
            LbAbout1.TabIndex = 8;
            LbAbout1.Text = "You can find this project on GitHub, its name\r\nis \"Simple-Screen-Recorder\", and my nickname\r\nis \"Lextrack\". Keep an eye on this project, more\r\nupdates coming soon!";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10F);
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(93, 20);
            label3.Name = "label3";
            label3.Size = new Size(151, 38);
            label3.TabIndex = 7;
            label3.Text = "Simple Screen Recorder\r\nv1.2.8";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(22, 78);
            label2.Name = "label2";
            label2.Size = new Size(151, 15);
            label2.TabIndex = 6;
            label2.Text = "Copyright © 2024 Lextrack.";
            // 
            // label1
            // 
            label1.Image = (Image)resources.GetObject("label1.Image");
            label1.Location = new Point(22, 9);
            label1.Name = "label1";
            label1.Size = new Size(54, 54);
            label1.TabIndex = 5;
            // 
            // AboutForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(302, 233);
            Controls.Add(LbAbout2);
            Controls.Add(LbAbout1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AboutForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            FormClosed += AboutForm_FormClosed;
            Load += AboutForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LbAbout2;
        private Label LbAbout1;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}