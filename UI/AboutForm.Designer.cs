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
            this.LbAbout2 = new System.Windows.Forms.Label();
            this.LbAbout1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LbAbout2
            // 
            this.LbAbout2.AutoSize = true;
            this.LbAbout2.ForeColor = System.Drawing.SystemColors.Control;
            this.LbAbout2.Location = new System.Drawing.Point(16, 182);
            this.LbAbout2.Name = "LbAbout2";
            this.LbAbout2.Size = new System.Drawing.Size(182, 30);
            this.LbAbout2.TabIndex = 9;
            this.LbAbout2.Text = "This software is made possible by\r\nTutorialesVbNET and Flaticon.";
            // 
            // LbAbout1
            // 
            this.LbAbout1.AutoSize = true;
            this.LbAbout1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LbAbout1.ForeColor = System.Drawing.SystemColors.Control;
            this.LbAbout1.Location = new System.Drawing.Point(16, 106);
            this.LbAbout1.Name = "LbAbout1";
            this.LbAbout1.Size = new System.Drawing.Size(257, 60);
            this.LbAbout1.TabIndex = 8;
            this.LbAbout1.Text = "You can find this project on GitHub, its name\r\nis \"Simple-Screen-Recorder\", and m" +
    "y nickname\r\nis \"Lextrack\". Keep an eye on this project, more\r\nupdates coming soo" +
    "n!";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(87, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 38);
            this.label3.TabIndex = 7;
            this.label3.Text = "Simple Screen Recorder\r\nv1.2.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(16, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Copyright © 2023 Lextrack.";
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 54);
            this.label1.TabIndex = 5;
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(284, 233);
            this.Controls.Add(this.LbAbout2);
            this.Controls.Add(this.LbAbout1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AboutForm_FormClosed);
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LbAbout2;
        private Label LbAbout1;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}