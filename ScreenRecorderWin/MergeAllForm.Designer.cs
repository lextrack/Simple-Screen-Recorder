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
            this.BtnVideo = new System.Windows.Forms.Button();
            this.BtnDeskAudio = new System.Windows.Forms.Button();
            this.txtVideoPath = new System.Windows.Forms.TextBox();
            this.txtAudioDesk = new System.Windows.Forms.TextBox();
            this.txtAudioMic = new System.Windows.Forms.TextBox();
            this.BtnMicAudio = new System.Windows.Forms.Button();
            this.BtnMergeAll = new System.Windows.Forms.Button();
            this.btnOutputF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnVideo
            // 
            this.BtnVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVideo.ForeColor = System.Drawing.Color.Transparent;
            this.BtnVideo.Image = global::Simple_Screen_Recorder.Properties.Resources.video_button;
            this.BtnVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVideo.Location = new System.Drawing.Point(11, 12);
            this.BtnVideo.Name = "BtnVideo";
            this.BtnVideo.Size = new System.Drawing.Size(168, 33);
            this.BtnVideo.TabIndex = 0;
            this.BtnVideo.Text = "Select video";
            this.BtnVideo.UseVisualStyleBackColor = true;
            this.BtnVideo.Click += new System.EventHandler(this.BtnVideo_Click);
            // 
            // BtnDeskAudio
            // 
            this.BtnDeskAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeskAudio.ForeColor = System.Drawing.Color.Transparent;
            this.BtnDeskAudio.Image = global::Simple_Screen_Recorder.Properties.Resources.sound_waves_button;
            this.BtnDeskAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDeskAudio.Location = new System.Drawing.Point(11, 51);
            this.BtnDeskAudio.Name = "BtnDeskAudio";
            this.BtnDeskAudio.Size = new System.Drawing.Size(168, 33);
            this.BtnDeskAudio.TabIndex = 1;
            this.BtnDeskAudio.Text = "Select system sounds";
            this.BtnDeskAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnDeskAudio.UseVisualStyleBackColor = true;
            this.BtnDeskAudio.Click += new System.EventHandler(this.BtnDeskAudio_Click);
            // 
            // txtVideoPath
            // 
            this.txtVideoPath.Location = new System.Drawing.Point(185, 18);
            this.txtVideoPath.Name = "txtVideoPath";
            this.txtVideoPath.Size = new System.Drawing.Size(189, 23);
            this.txtVideoPath.TabIndex = 2;
            // 
            // txtAudioDesk
            // 
            this.txtAudioDesk.Location = new System.Drawing.Point(185, 57);
            this.txtAudioDesk.Name = "txtAudioDesk";
            this.txtAudioDesk.Size = new System.Drawing.Size(189, 23);
            this.txtAudioDesk.TabIndex = 3;
            // 
            // txtAudioMic
            // 
            this.txtAudioMic.Location = new System.Drawing.Point(185, 96);
            this.txtAudioMic.Name = "txtAudioMic";
            this.txtAudioMic.Size = new System.Drawing.Size(189, 23);
            this.txtAudioMic.TabIndex = 5;
            // 
            // BtnMicAudio
            // 
            this.BtnMicAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMicAudio.ForeColor = System.Drawing.Color.Transparent;
            this.BtnMicAudio.Image = global::Simple_Screen_Recorder.Properties.Resources.mic_button;
            this.BtnMicAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnMicAudio.Location = new System.Drawing.Point(11, 90);
            this.BtnMicAudio.Name = "BtnMicAudio";
            this.BtnMicAudio.Size = new System.Drawing.Size(168, 33);
            this.BtnMicAudio.TabIndex = 4;
            this.BtnMicAudio.Text = "     Select mic audio";
            this.BtnMicAudio.UseVisualStyleBackColor = true;
            this.BtnMicAudio.Click += new System.EventHandler(this.BtnMicAudio_Click);
            // 
            // BtnMergeAll
            // 
            this.BtnMergeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMergeAll.ForeColor = System.Drawing.Color.Transparent;
            this.BtnMergeAll.Image = global::Simple_Screen_Recorder.Properties.Resources.mixing_button;
            this.BtnMergeAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnMergeAll.Location = new System.Drawing.Point(44, 142);
            this.BtnMergeAll.Name = "BtnMergeAll";
            this.BtnMergeAll.Size = new System.Drawing.Size(125, 33);
            this.BtnMergeAll.TabIndex = 6;
            this.BtnMergeAll.Text = "Start mixing";
            this.BtnMergeAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnMergeAll.UseVisualStyleBackColor = true;
            this.BtnMergeAll.Click += new System.EventHandler(this.BtnMergeAll_Click);
            // 
            // btnOutputF
            // 
            this.btnOutputF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutputF.ForeColor = System.Drawing.Color.Transparent;
            this.btnOutputF.Image = global::Simple_Screen_Recorder.Properties.Resources.folder_button;
            this.btnOutputF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOutputF.Location = new System.Drawing.Point(186, 142);
            this.btnOutputF.Name = "btnOutputF";
            this.btnOutputF.Size = new System.Drawing.Size(125, 33);
            this.btnOutputF.TabIndex = 15;
            this.btnOutputF.Text = "Output folder";
            this.btnOutputF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOutputF.UseVisualStyleBackColor = true;
            this.btnOutputF.Click += new System.EventHandler(this.btnOutputF_Click);
            // 
            // MergeAllForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(386, 187);
            this.Controls.Add(this.btnOutputF);
            this.Controls.Add(this.BtnMergeAll);
            this.Controls.Add(this.txtAudioMic);
            this.Controls.Add(this.BtnMicAudio);
            this.Controls.Add(this.txtAudioDesk);
            this.Controls.Add(this.txtVideoPath);
            this.Controls.Add(this.BtnDeskAudio);
            this.Controls.Add(this.BtnVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(400, 260);
            this.MaximizeBox = false;
            this.Name = "MergeAllForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Merge all";
            this.Load += new System.EventHandler(this.MergeVDM_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BtnVideo;
        private Button BtnDeskAudio;
        private TextBox txtVideoPath;
        private TextBox txtAudioDesk;
        private TextBox txtAudioMic;
        private Button BtnMicAudio;
        private Button BtnMergeAll;
        private Button btnOutputF;
    }
}