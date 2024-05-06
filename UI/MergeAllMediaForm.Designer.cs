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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeAllForm));
            BtnVideo2 = new Button();
            BtnDeskAudio2 = new Button();
            txtVideoPath = new TextBox();
            txtAudioDesk = new TextBox();
            txtAudioMic = new TextBox();
            BtnMicAudio = new Button();
            BtnMergeAll2 = new Button();
            btnOutputF2 = new Button();
            notifyResultMergeAllMedia = new NotifyIcon(components);
            SuspendLayout();
            // 
            // BtnVideo2
            // 
            BtnVideo2.FlatStyle = FlatStyle.Flat;
            BtnVideo2.ForeColor = Color.Transparent;
            BtnVideo2.Image = Properties.Resources.video_button;
            BtnVideo2.ImageAlign = ContentAlignment.MiddleLeft;
            BtnVideo2.Location = new Point(14, 10);
            BtnVideo2.Name = "BtnVideo2";
            BtnVideo2.Size = new Size(236, 41);
            BtnVideo2.TabIndex = 1;
            BtnVideo2.Text = "Select a video file";
            BtnVideo2.TextAlign = ContentAlignment.MiddleRight;
            BtnVideo2.UseVisualStyleBackColor = true;
            BtnVideo2.Click += BtnVideo_Click;
            // 
            // BtnDeskAudio2
            // 
            BtnDeskAudio2.FlatStyle = FlatStyle.Flat;
            BtnDeskAudio2.ForeColor = Color.Transparent;
            BtnDeskAudio2.Image = Properties.Resources.sound_waves_button;
            BtnDeskAudio2.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDeskAudio2.Location = new Point(14, 89);
            BtnDeskAudio2.Name = "BtnDeskAudio2";
            BtnDeskAudio2.Size = new Size(236, 41);
            BtnDeskAudio2.TabIndex = 3;
            BtnDeskAudio2.Text = "Select a system sound file";
            BtnDeskAudio2.TextAlign = ContentAlignment.MiddleRight;
            BtnDeskAudio2.UseVisualStyleBackColor = true;
            BtnDeskAudio2.Click += BtnDeskAudio_Click;
            // 
            // txtVideoPath
            // 
            txtVideoPath.BorderStyle = BorderStyle.FixedSingle;
            txtVideoPath.Font = new Font("Segoe UI", 8F);
            txtVideoPath.Location = new Point(256, 21);
            txtVideoPath.Name = "txtVideoPath";
            txtVideoPath.ReadOnly = true;
            txtVideoPath.RightToLeft = RightToLeft.Yes;
            txtVideoPath.Size = new Size(216, 22);
            txtVideoPath.TabIndex = 2;
            // 
            // txtAudioDesk
            // 
            txtAudioDesk.BorderStyle = BorderStyle.FixedSingle;
            txtAudioDesk.Font = new Font("Segoe UI", 8F);
            txtAudioDesk.Location = new Point(256, 100);
            txtAudioDesk.Name = "txtAudioDesk";
            txtAudioDesk.ReadOnly = true;
            txtAudioDesk.RightToLeft = RightToLeft.Yes;
            txtAudioDesk.Size = new Size(216, 22);
            txtAudioDesk.TabIndex = 3;
            // 
            // txtAudioMic
            // 
            txtAudioMic.BorderStyle = BorderStyle.FixedSingle;
            txtAudioMic.Font = new Font("Segoe UI", 8F);
            txtAudioMic.Location = new Point(256, 60);
            txtAudioMic.Name = "txtAudioMic";
            txtAudioMic.ReadOnly = true;
            txtAudioMic.RightToLeft = RightToLeft.Yes;
            txtAudioMic.Size = new Size(216, 22);
            txtAudioMic.TabIndex = 5;
            // 
            // BtnMicAudio
            // 
            BtnMicAudio.FlatStyle = FlatStyle.Flat;
            BtnMicAudio.ForeColor = Color.Transparent;
            BtnMicAudio.Image = Properties.Resources.mic_button;
            BtnMicAudio.ImageAlign = ContentAlignment.MiddleLeft;
            BtnMicAudio.Location = new Point(14, 49);
            BtnMicAudio.Name = "BtnMicAudio";
            BtnMicAudio.Size = new Size(236, 41);
            BtnMicAudio.TabIndex = 2;
            BtnMicAudio.Text = "Select a mic audio file";
            BtnMicAudio.TextAlign = ContentAlignment.MiddleRight;
            BtnMicAudio.UseVisualStyleBackColor = true;
            BtnMicAudio.Click += BtnMicAudio_Click;
            // 
            // BtnMergeAll2
            // 
            BtnMergeAll2.FlatStyle = FlatStyle.Flat;
            BtnMergeAll2.ForeColor = Color.Transparent;
            BtnMergeAll2.Image = Properties.Resources.mixing_button;
            BtnMergeAll2.ImageAlign = ContentAlignment.MiddleLeft;
            BtnMergeAll2.Location = new Point(95, 150);
            BtnMergeAll2.Name = "BtnMergeAll2";
            BtnMergeAll2.Size = new Size(155, 39);
            BtnMergeAll2.TabIndex = 4;
            BtnMergeAll2.Text = "Start mixing";
            BtnMergeAll2.TextAlign = ContentAlignment.MiddleRight;
            BtnMergeAll2.UseVisualStyleBackColor = true;
            BtnMergeAll2.Click += BtnMergeAll_Click;
            // 
            // btnOutputF2
            // 
            btnOutputF2.FlatStyle = FlatStyle.Flat;
            btnOutputF2.ForeColor = Color.Transparent;
            btnOutputF2.Image = Properties.Resources.folder_button;
            btnOutputF2.ImageAlign = ContentAlignment.MiddleLeft;
            btnOutputF2.Location = new Point(256, 150);
            btnOutputF2.Name = "btnOutputF2";
            btnOutputF2.Size = new Size(155, 39);
            btnOutputF2.TabIndex = 5;
            btnOutputF2.Text = "Output folder";
            btnOutputF2.TextAlign = ContentAlignment.MiddleRight;
            btnOutputF2.UseVisualStyleBackColor = true;
            btnOutputF2.Click += btnOutputF_Click;
            // 
            // notifyResultMergeAllMedia
            // 
            notifyResultMergeAllMedia.Text = "notifyIcon1";
            notifyResultMergeAllMedia.Visible = true;
            // 
            // MergeAllForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(484, 201);
            Controls.Add(btnOutputF2);
            Controls.Add(BtnMergeAll2);
            Controls.Add(txtAudioMic);
            Controls.Add(BtnMicAudio);
            Controls.Add(txtAudioDesk);
            Controls.Add(txtVideoPath);
            Controls.Add(BtnDeskAudio2);
            Controls.Add(BtnVideo2);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Location = new Point(100, 110);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MergeAllForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Merge all";
            FormClosed += MergeAllForm_FormClosed;
            Load += MergeVDM_Load;
            ResumeLayout(false);
            PerformLayout();
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
        private NotifyIcon notifyResultMergeAllMedia;
    }
}