namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    partial class MergeVidDeskForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeVidDeskForm));
            this.BtnMergeAll = new System.Windows.Forms.Button();
            this.txtAudioDesk = new System.Windows.Forms.TextBox();
            this.txtVideoPath = new System.Windows.Forms.TextBox();
            this.BtnDeskAudio = new System.Windows.Forms.Button();
            this.BtnVideo = new System.Windows.Forms.Button();
            this.btnOutputF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnMergeAll
            // 
            this.BtnMergeAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnMergeAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnMergeAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMergeAll.ForeColor = System.Drawing.Color.Transparent;
            this.BtnMergeAll.Image = global::Simple_Screen_Recorder.Properties.Resources.mixing_button;
            this.BtnMergeAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnMergeAll.Location = new System.Drawing.Point(91, 144);
            this.BtnMergeAll.Name = "BtnMergeAll";
            this.BtnMergeAll.Size = new System.Drawing.Size(140, 39);
            this.BtnMergeAll.TabIndex = 13;
            this.BtnMergeAll.Text = "Start mixing";
            this.BtnMergeAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnMergeAll.UseVisualStyleBackColor = true;
            this.BtnMergeAll.Click += new System.EventHandler(this.BtnMergeAll_Click);
            // 
            // txtAudioDesk
            // 
            this.txtAudioDesk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAudioDesk.Location = new System.Drawing.Point(237, 59);
            this.txtAudioDesk.Name = "txtAudioDesk";
            this.txtAudioDesk.Size = new System.Drawing.Size(210, 23);
            this.txtAudioDesk.TabIndex = 10;
            // 
            // txtVideoPath
            // 
            this.txtVideoPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVideoPath.Location = new System.Drawing.Point(237, 20);
            this.txtVideoPath.Name = "txtVideoPath";
            this.txtVideoPath.Size = new System.Drawing.Size(210, 23);
            this.txtVideoPath.TabIndex = 9;
            // 
            // BtnDeskAudio
            // 
            this.BtnDeskAudio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnDeskAudio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnDeskAudio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeskAudio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnDeskAudio.ForeColor = System.Drawing.Color.Transparent;
            this.BtnDeskAudio.Image = global::Simple_Screen_Recorder.Properties.Resources.sound_waves_button;
            this.BtnDeskAudio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnDeskAudio.Location = new System.Drawing.Point(12, 49);
            this.BtnDeskAudio.Name = "BtnDeskAudio";
            this.BtnDeskAudio.Size = new System.Drawing.Size(219, 41);
            this.BtnDeskAudio.TabIndex = 8;
            this.BtnDeskAudio.Text = "Select a system sound file";
            this.BtnDeskAudio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnDeskAudio.UseVisualStyleBackColor = true;
            this.BtnDeskAudio.Click += new System.EventHandler(this.BtnDeskAudio_Click);
            // 
            // BtnVideo
            // 
            this.BtnVideo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnVideo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnVideo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVideo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnVideo.ForeColor = System.Drawing.Color.Transparent;
            this.BtnVideo.Image = global::Simple_Screen_Recorder.Properties.Resources.video_button;
            this.BtnVideo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVideo.Location = new System.Drawing.Point(12, 10);
            this.BtnVideo.Name = "BtnVideo";
            this.BtnVideo.Size = new System.Drawing.Size(219, 41);
            this.BtnVideo.TabIndex = 7;
            this.BtnVideo.Text = "Select a video file";
            this.BtnVideo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnVideo.UseVisualStyleBackColor = true;
            this.BtnVideo.Click += new System.EventHandler(this.BtnVideo_Click);
            // 
            // btnOutputF
            // 
            this.btnOutputF.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOutputF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOutputF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutputF.ForeColor = System.Drawing.Color.Transparent;
            this.btnOutputF.Image = global::Simple_Screen_Recorder.Properties.Resources.folder_button;
            this.btnOutputF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOutputF.Location = new System.Drawing.Point(237, 144);
            this.btnOutputF.Name = "btnOutputF";
            this.btnOutputF.Size = new System.Drawing.Size(140, 39);
            this.btnOutputF.TabIndex = 14;
            this.btnOutputF.Text = "Output folder";
            this.btnOutputF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOutputF.UseVisualStyleBackColor = true;
            this.btnOutputF.Click += new System.EventHandler(this.btnOutputF_Click);
            // 
            // MergeVidDeskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(458, 195);
            this.Controls.Add(this.btnOutputF);
            this.Controls.Add(this.BtnMergeAll);
            this.Controls.Add(this.txtAudioDesk);
            this.Controls.Add(this.txtVideoPath);
            this.Controls.Add(this.BtnDeskAudio);
            this.Controls.Add(this.BtnVideo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(310, 256);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MergeVidDeskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Merge video and system sounds";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MergeVidDeskForm_FormClosed);
            this.Load += new System.EventHandler(this.MergeVD_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button BtnMergeAll;
        private TextBox txtAudioDesk;
        private TextBox txtVideoPath;
        private Button BtnDeskAudio;
        private Button BtnVideo;
        private Button btnOutputF;
    }
}