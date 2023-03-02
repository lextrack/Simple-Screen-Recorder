namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    partial class MergeVideoAudioForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeVideoAudioForm));
            BtnMergeAll = new Button();
            txtAudioDesk = new TextBox();
            txtVideoPath = new TextBox();
            BtnDeskAudio = new Button();
            BtnVideo = new Button();
            btnOutputF = new Button();
            notifyResultMergeVideoAudio = new NotifyIcon(components);
            SuspendLayout();
            // 
            // BtnMergeAll
            // 
            BtnMergeAll.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            BtnMergeAll.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            BtnMergeAll.FlatStyle = FlatStyle.Flat;
            BtnMergeAll.ForeColor = Color.Transparent;
            BtnMergeAll.Image = Properties.Resources.mixing_button;
            BtnMergeAll.ImageAlign = ContentAlignment.MiddleLeft;
            BtnMergeAll.Location = new Point(100, 144);
            BtnMergeAll.Name = "BtnMergeAll";
            BtnMergeAll.Size = new Size(147, 39);
            BtnMergeAll.TabIndex = 13;
            BtnMergeAll.Text = "Start mixing";
            BtnMergeAll.TextAlign = ContentAlignment.MiddleRight;
            BtnMergeAll.UseVisualStyleBackColor = true;
            BtnMergeAll.Click += BtnMergeAll_Click;
            // 
            // txtAudioDesk
            // 
            txtAudioDesk.BorderStyle = BorderStyle.FixedSingle;
            txtAudioDesk.Location = new Point(252, 60);
            txtAudioDesk.Name = "txtAudioDesk";
            txtAudioDesk.RightToLeft = RightToLeft.Yes;
            txtAudioDesk.Size = new Size(216, 23);
            txtAudioDesk.TabIndex = 10;
            // 
            // txtVideoPath
            // 
            txtVideoPath.BorderStyle = BorderStyle.FixedSingle;
            txtVideoPath.Location = new Point(252, 21);
            txtVideoPath.Name = "txtVideoPath";
            txtVideoPath.RightToLeft = RightToLeft.Yes;
            txtVideoPath.Size = new Size(216, 23);
            txtVideoPath.TabIndex = 9;
            // 
            // BtnDeskAudio
            // 
            BtnDeskAudio.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            BtnDeskAudio.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            BtnDeskAudio.FlatStyle = FlatStyle.Flat;
            BtnDeskAudio.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnDeskAudio.ForeColor = Color.Transparent;
            BtnDeskAudio.Image = Properties.Resources.sound_waves_button;
            BtnDeskAudio.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDeskAudio.Location = new Point(11, 49);
            BtnDeskAudio.Name = "BtnDeskAudio";
            BtnDeskAudio.Size = new Size(236, 41);
            BtnDeskAudio.TabIndex = 8;
            BtnDeskAudio.Text = "Select an audio file";
            BtnDeskAudio.TextAlign = ContentAlignment.MiddleRight;
            BtnDeskAudio.UseVisualStyleBackColor = true;
            BtnDeskAudio.Click += BtnDeskAudio_Click;
            // 
            // BtnVideo
            // 
            BtnVideo.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            BtnVideo.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            BtnVideo.FlatStyle = FlatStyle.Flat;
            BtnVideo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            BtnVideo.ForeColor = Color.Transparent;
            BtnVideo.Image = Properties.Resources.video_button;
            BtnVideo.ImageAlign = ContentAlignment.MiddleLeft;
            BtnVideo.Location = new Point(11, 10);
            BtnVideo.Name = "BtnVideo";
            BtnVideo.Size = new Size(236, 41);
            BtnVideo.TabIndex = 7;
            BtnVideo.Text = "Select a video file";
            BtnVideo.TextAlign = ContentAlignment.MiddleRight;
            BtnVideo.UseVisualStyleBackColor = true;
            BtnVideo.Click += BtnVideo_Click;
            // 
            // btnOutputF
            // 
            btnOutputF.FlatAppearance.MouseDownBackColor = Color.CornflowerBlue;
            btnOutputF.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btnOutputF.FlatStyle = FlatStyle.Flat;
            btnOutputF.ForeColor = Color.Transparent;
            btnOutputF.Image = Properties.Resources.folder_button;
            btnOutputF.ImageAlign = ContentAlignment.MiddleLeft;
            btnOutputF.Location = new Point(253, 144);
            btnOutputF.Name = "btnOutputF";
            btnOutputF.Size = new Size(147, 39);
            btnOutputF.TabIndex = 14;
            btnOutputF.Text = "Output folder";
            btnOutputF.TextAlign = ContentAlignment.MiddleRight;
            btnOutputF.UseVisualStyleBackColor = true;
            btnOutputF.Click += btnOutputF_Click;
            // 
            // notifyResultMergeVideoAudio
            // 
            notifyResultMergeVideoAudio.Text = "notifyIcon1";
            notifyResultMergeVideoAudio.Visible = true;
            // 
            // MergeVideoAudioForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(481, 200);
            Controls.Add(btnOutputF);
            Controls.Add(BtnMergeAll);
            Controls.Add(txtAudioDesk);
            Controls.Add(txtVideoPath);
            Controls.Add(BtnDeskAudio);
            Controls.Add(BtnVideo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(250, 178);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MergeVideoAudioForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Merge video and system sounds";
            FormClosed += MergeVidDeskForm_FormClosed;
            Load += MergeVD_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button BtnMergeAll;
        private TextBox txtAudioDesk;
        private TextBox txtVideoPath;
        private Button BtnDeskAudio;
        private Button BtnVideo;
        private Button btnOutputF;
        private NotifyIcon notifyResultMergeVideoAudio;
    }
}