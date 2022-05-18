namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    partial class MergeAllForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeAllForm));
            this.BtnVideo2 = new System.Windows.Forms.Button();
            this.BtnDeskAudio2 = new System.Windows.Forms.Button();
            this.txtVideoPath = new System.Windows.Forms.TextBox();
            this.txtAudioDesk = new System.Windows.Forms.TextBox();
            this.txtAudioMic = new System.Windows.Forms.TextBox();
            this.BtnMicAudio = new System.Windows.Forms.Button();
            this.BtnMergeAll2 = new System.Windows.Forms.Button();
            this.btnOutputF2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnVideo2
            // 
            this.BtnVideo2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVideo2.ForeColor = System.Drawing.Color.Transparent;
            this.BtnVideo2.Image = global::Simple_Screen_Recorder.Properties.Resources.video_button;
            this.BtnVideo2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVideo2.Location = new System.Drawing.Point(11, 10);
            this.BtnVideo2.Name = "BtnVideo2";
            this.BtnVideo2.Size = new System.Drawing.Size(219, 41);
            this.BtnVideo2.TabIndex = 0;
            this.BtnVideo2.Text = "Select a video file";
            this.BtnVideo2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnVideo2.UseVisualStyleBackColor = true;
            this.BtnVideo2.Click += new System.EventHandler(this.BtnVideo_Click);
            // 
            // BtnDeskAudio2
            // 
            this.BtnDeskAudio2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeskAudio2.ForeColor = System.Drawing.Color.Transparent;
            this.BtnDeskAudio2.Image = global::Simple_Screen_Recorder.Properties.Resources.sound_waves_button;
            this.BtnDeskAudio2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDeskAudio2.Location = new System.Drawing.Point(11, 49);
            this.BtnDeskAudio2.Name = "BtnDeskAudio2";
            this.BtnDeskAudio2.Size = new System.Drawing.Size(219, 41);
            this.BtnDeskAudio2.TabIndex = 1;
            this.BtnDeskAudio2.Text = "Select a system sound file";
            this.BtnDeskAudio2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnDeskAudio2.UseVisualStyleBackColor = true;
            this.BtnDeskAudio2.Click += new System.EventHandler(this.BtnDeskAudio_Click);
            // 
            // txtVideoPath
            // 
            this.txtVideoPath.Location = new System.Drawing.Point(236, 20);
            this.txtVideoPath.Name = "txtVideoPath";
            this.txtVideoPath.Size = new System.Drawing.Size(210, 23);
            this.txtVideoPath.TabIndex = 2;
            // 
            // txtAudioDesk
            // 
            this.txtAudioDesk.Location = new System.Drawing.Point(236, 59);
            this.txtAudioDesk.Name = "txtAudioDesk";
            this.txtAudioDesk.Size = new System.Drawing.Size(210, 23);
            this.txtAudioDesk.TabIndex = 3;
            // 
            // txtAudioMic
            // 
            this.txtAudioMic.Location = new System.Drawing.Point(236, 98);
            this.txtAudioMic.Name = "txtAudioMic";
            this.txtAudioMic.Size = new System.Drawing.Size(210, 23);
            this.txtAudioMic.TabIndex = 5;
            // 
            // BtnMicAudio
            // 
            this.BtnMicAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMicAudio.ForeColor = System.Drawing.Color.Transparent;
            this.BtnMicAudio.Image = global::Simple_Screen_Recorder.Properties.Resources.mic_button;
            this.BtnMicAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnMicAudio.Location = new System.Drawing.Point(11, 88);
            this.BtnMicAudio.Name = "BtnMicAudio";
            this.BtnMicAudio.Size = new System.Drawing.Size(219, 41);
            this.BtnMicAudio.TabIndex = 4;
            this.BtnMicAudio.Text = "Select a mic audio file";
            this.BtnMicAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnMicAudio.UseVisualStyleBackColor = true;
            this.BtnMicAudio.Click += new System.EventHandler(this.BtnMicAudio_Click);
            // 
            // BtnMergeAll2
            // 
            this.BtnMergeAll2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMergeAll2.ForeColor = System.Drawing.Color.Transparent;
            this.BtnMergeAll2.Image = global::Simple_Screen_Recorder.Properties.Resources.mixing_button;
            this.BtnMergeAll2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnMergeAll2.Location = new System.Drawing.Point(90, 144);
            this.BtnMergeAll2.Name = "BtnMergeAll2";
            this.BtnMergeAll2.Size = new System.Drawing.Size(140, 39);
            this.BtnMergeAll2.TabIndex = 6;
            this.BtnMergeAll2.Text = "Start mixing";
            this.BtnMergeAll2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnMergeAll2.UseVisualStyleBackColor = true;
            this.BtnMergeAll2.Click += new System.EventHandler(this.BtnMergeAll_Click);
            // 
            // btnOutputF2
            // 
            this.btnOutputF2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutputF2.ForeColor = System.Drawing.Color.Transparent;
            this.btnOutputF2.Image = global::Simple_Screen_Recorder.Properties.Resources.folder_button;
            this.btnOutputF2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOutputF2.Location = new System.Drawing.Point(236, 144);
            this.btnOutputF2.Name = "btnOutputF2";
            this.btnOutputF2.Size = new System.Drawing.Size(140, 39);
            this.btnOutputF2.TabIndex = 15;
            this.btnOutputF2.Text = "Output folder";
            this.btnOutputF2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOutputF2.UseVisualStyleBackColor = true;
            this.btnOutputF2.Click += new System.EventHandler(this.btnOutputF_Click);
            // 
            // MergeAllForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(458, 195);
            this.Controls.Add(this.btnOutputF2);
            this.Controls.Add(this.BtnMergeAll2);
            this.Controls.Add(this.txtAudioMic);
            this.Controls.Add(this.BtnMicAudio);
            this.Controls.Add(this.txtAudioDesk);
            this.Controls.Add(this.txtVideoPath);
            this.Controls.Add(this.BtnDeskAudio2);
            this.Controls.Add(this.BtnVideo2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(310, 256);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MergeAllForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Merge all";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MergeAllForm_FormClosed);
            this.Load += new System.EventHandler(this.MergeVDM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BtnVideo2;
        private Button BtnDeskAudio2;
        private TextBox txtVideoPath;
        private TextBox txtAudioDesk;
        private TextBox txtAudioMic;
        private Button BtnMicAudio;
        private Button BtnMergeAll2;
        private Button btnOutputF2;
    }
}