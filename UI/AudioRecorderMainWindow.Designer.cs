namespace Simple_Screen_Recorder.UI
{
    partial class AudioRecorderMainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AudioRecorderMainWindow));
            this.crownGroupBox3 = new ReaLTaiizor.Controls.CrownGroupBox();
            this.btnOutputRecordings = new System.Windows.Forms.Button();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.crownGroupBox2 = new ReaLTaiizor.Controls.CrownGroupBox();
            this.radioMicrophone = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.RadioTwoTrack = new System.Windows.Forms.RadioButton();
            this.Label5 = new System.Windows.Forms.Label();
            this.RadioDesktop = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.ComboBoxMicrophone = new ReaLTaiizor.Controls.CrownComboBox();
            this.ComboBoxSpeaker = new ReaLTaiizor.Controls.CrownComboBox();
            this.RecState = new System.Windows.Forms.Timer(this.components);
            this.LbTimer = new System.Windows.Forms.Label();
            this.crownGroupBox3.SuspendLayout();
            this.crownGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // crownGroupBox3
            // 
            this.crownGroupBox3.BorderColor = System.Drawing.Color.Gray;
            this.crownGroupBox3.Controls.Add(this.btnOutputRecordings);
            this.crownGroupBox3.Controls.Add(this.btnStartRecording);
            this.crownGroupBox3.Controls.Add(this.BtnStop);
            this.crownGroupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.crownGroupBox3.Location = new System.Drawing.Point(18, 4);
            this.crownGroupBox3.Name = "crownGroupBox3";
            this.crownGroupBox3.Size = new System.Drawing.Size(321, 164);
            this.crownGroupBox3.TabIndex = 57;
            this.crownGroupBox3.TabStop = false;
            this.crownGroupBox3.Text = "Controls";
            // 
            // btnOutputRecordings
            // 
            this.btnOutputRecordings.FlatAppearance.BorderSize = 2;
            this.btnOutputRecordings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOutputRecordings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnOutputRecordings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutputRecordings.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOutputRecordings.ForeColor = System.Drawing.Color.Transparent;
            this.btnOutputRecordings.Image = global::Simple_Screen_Recorder.Properties.Resources.folder_button;
            this.btnOutputRecordings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOutputRecordings.Location = new System.Drawing.Point(14, 114);
            this.btnOutputRecordings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOutputRecordings.Name = "btnOutputRecordings";
            this.btnOutputRecordings.Size = new System.Drawing.Size(294, 40);
            this.btnOutputRecordings.TabIndex = 7;
            this.btnOutputRecordings.Text = " Open Recordings Folder";
            this.btnOutputRecordings.UseVisualStyleBackColor = true;
            this.btnOutputRecordings.Click += new System.EventHandler(this.btnOutputRecordings_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.FlatAppearance.BorderSize = 2;
            this.btnStartRecording.FlatAppearance.MouseDownBackColor = System.Drawing.Color.ForestGreen;
            this.btnStartRecording.FlatAppearance.MouseOverBackColor = System.Drawing.Color.ForestGreen;
            this.btnStartRecording.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartRecording.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStartRecording.ForeColor = System.Drawing.Color.Transparent;
            this.btnStartRecording.Image = global::Simple_Screen_Recorder.Properties.Resources.voice_control;
            this.btnStartRecording.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartRecording.Location = new System.Drawing.Point(14, 24);
            this.btnStartRecording.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnStartRecording.Size = new System.Drawing.Size(294, 40);
            this.btnStartRecording.TabIndex = 1;
            this.btnStartRecording.Text = "Start Recording";
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.FlatAppearance.BorderSize = 2;
            this.BtnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkViolet;
            this.BtnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkViolet;
            this.BtnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStop.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnStop.ForeColor = System.Drawing.Color.Transparent;
            this.BtnStop.Image = global::Simple_Screen_Recorder.Properties.Resources.stop2b;
            this.BtnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnStop.Location = new System.Drawing.Point(14, 69);
            this.BtnStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(294, 40);
            this.BtnStop.TabIndex = 2;
            this.BtnStop.Text = "Stop Recording";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // crownGroupBox2
            // 
            this.crownGroupBox2.BorderColor = System.Drawing.Color.Gray;
            this.crownGroupBox2.Controls.Add(this.radioMicrophone);
            this.crownGroupBox2.Controls.Add(this.label6);
            this.crownGroupBox2.Controls.Add(this.Label4);
            this.crownGroupBox2.Controls.Add(this.RadioTwoTrack);
            this.crownGroupBox2.Controls.Add(this.Label5);
            this.crownGroupBox2.Controls.Add(this.RadioDesktop);
            this.crownGroupBox2.Controls.Add(this.label7);
            this.crownGroupBox2.Controls.Add(this.ComboBoxMicrophone);
            this.crownGroupBox2.Controls.Add(this.ComboBoxSpeaker);
            this.crownGroupBox2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.crownGroupBox2.Location = new System.Drawing.Point(18, 174);
            this.crownGroupBox2.Name = "crownGroupBox2";
            this.crownGroupBox2.Size = new System.Drawing.Size(322, 268);
            this.crownGroupBox2.TabIndex = 58;
            this.crownGroupBox2.TabStop = false;
            this.crownGroupBox2.Text = "Audio settings";
            // 
            // radioMicrophone
            // 
            this.radioMicrophone.AutoSize = true;
            this.radioMicrophone.BackColor = System.Drawing.Color.Transparent;
            this.radioMicrophone.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioMicrophone.ForeColor = System.Drawing.SystemColors.Control;
            this.radioMicrophone.Location = new System.Drawing.Point(14, 98);
            this.radioMicrophone.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.radioMicrophone.Name = "radioMicrophone";
            this.radioMicrophone.Size = new System.Drawing.Size(181, 24);
            this.radioMicrophone.TabIndex = 55;
            this.radioMicrophone.TabStop = true;
            this.radioMicrophone.Text = "Microphone audio only";
            this.radioMicrophone.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Crimson;
            this.label6.Location = new System.Drawing.Point(7, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(181, 20);
            this.label6.TabIndex = 40;
            this.label6.Text = "Audio recording method";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label4.ForeColor = System.Drawing.SystemColors.Control;
            this.Label4.Location = new System.Drawing.Point(11, 207);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(158, 20);
            this.Label4.TabIndex = 38;
            this.Label4.Text = "Microphone (Mic/Aux)";
            // 
            // RadioTwoTrack
            // 
            this.RadioTwoTrack.AutoSize = true;
            this.RadioTwoTrack.BackColor = System.Drawing.Color.Transparent;
            this.RadioTwoTrack.Checked = true;
            this.RadioTwoTrack.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioTwoTrack.ForeColor = System.Drawing.SystemColors.Control;
            this.RadioTwoTrack.Location = new System.Drawing.Point(14, 44);
            this.RadioTwoTrack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioTwoTrack.Name = "RadioTwoTrack";
            this.RadioTwoTrack.Size = new System.Drawing.Size(279, 24);
            this.RadioTwoTrack.TabIndex = 54;
            this.RadioTwoTrack.TabStop = true;
            this.RadioTwoTrack.Text = "System sounds and microphone audio";
            this.RadioTwoTrack.UseVisualStyleBackColor = false;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label5.ForeColor = System.Drawing.SystemColors.Control;
            this.Label5.Location = new System.Drawing.Point(11, 155);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(213, 20);
            this.Label5.TabIndex = 6;
            this.Label5.Text = "System sound (Desktop Audio)";
            // 
            // RadioDesktop
            // 
            this.RadioDesktop.AutoSize = true;
            this.RadioDesktop.BackColor = System.Drawing.Color.Transparent;
            this.RadioDesktop.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioDesktop.ForeColor = System.Drawing.SystemColors.Control;
            this.RadioDesktop.Location = new System.Drawing.Point(14, 71);
            this.RadioDesktop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioDesktop.Name = "RadioDesktop";
            this.RadioDesktop.Size = new System.Drawing.Size(261, 24);
            this.RadioDesktop.TabIndex = 53;
            this.RadioDesktop.TabStop = true;
            this.RadioDesktop.Text = "System audio only (Desktop Audio)";
            this.RadioDesktop.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Crimson;
            this.label7.Location = new System.Drawing.Point(8, 132);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 20);
            this.label7.TabIndex = 41;
            this.label7.Text = "Audio devices";
            // 
            // ComboBoxMicrophone
            // 
            this.ComboBoxMicrophone.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ComboBoxMicrophone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBoxMicrophone.FormattingEnabled = true;
            this.ComboBoxMicrophone.Location = new System.Drawing.Point(13, 228);
            this.ComboBoxMicrophone.Name = "ComboBoxMicrophone";
            this.ComboBoxMicrophone.Size = new System.Drawing.Size(294, 26);
            this.ComboBoxMicrophone.TabIndex = 52;
            // 
            // ComboBoxSpeaker
            // 
            this.ComboBoxSpeaker.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ComboBoxSpeaker.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBoxSpeaker.FormattingEnabled = true;
            this.ComboBoxSpeaker.Location = new System.Drawing.Point(13, 176);
            this.ComboBoxSpeaker.Name = "ComboBoxSpeaker";
            this.ComboBoxSpeaker.Size = new System.Drawing.Size(294, 26);
            this.ComboBoxSpeaker.TabIndex = 6;
            // 
            // RecState
            // 
            this.RecState.Tick += new System.EventHandler(this.RecState_Tick);
            // 
            // LbTimer
            // 
            this.LbTimer.AutoSize = true;
            this.LbTimer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbTimer.ForeColor = System.Drawing.Color.White;
            this.LbTimer.Location = new System.Drawing.Point(18, 449);
            this.LbTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LbTimer.Name = "LbTimer";
            this.LbTimer.Size = new System.Drawing.Size(72, 21);
            this.LbTimer.TabIndex = 59;
            this.LbTimer.Text = "00:00:00";
            // 
            // AudioRecorderMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(358, 481);
            this.Controls.Add(this.LbTimer);
            this.Controls.Add(this.crownGroupBox2);
            this.Controls.Add(this.crownGroupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(335, 200);
            this.MaximizeBox = false;
            this.Name = "AudioRecorderMainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Audio recorder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AudioRecorderMainWindow_FormClosed);
            this.Load += new System.EventHandler(this.AudioRecorderMainWindow_Load);
            this.crownGroupBox3.ResumeLayout(false);
            this.crownGroupBox2.ResumeLayout(false);
            this.crownGroupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.CrownGroupBox crownGroupBox3;
        internal Button btnOutputRecordings;
        internal Button btnStartRecording;
        private ReaLTaiizor.Controls.CrownGroupBox crownGroupBox2;
        internal Label label6;
        internal Label Label4;
        internal RadioButton RadioTwoTrack;
        internal Label Label5;
        internal RadioButton RadioDesktop;
        internal Label label7;
        private ReaLTaiizor.Controls.CrownComboBox ComboBoxMicrophone;
        private ReaLTaiizor.Controls.CrownComboBox ComboBoxSpeaker;
        private System.Windows.Forms.Timer RecState;
        internal Label LbTimer;
        internal RadioButton radioMicrophone;
        internal Button BtnStop;
    }
}