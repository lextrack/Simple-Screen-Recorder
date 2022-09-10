using Timer = System.Windows.Forms.Timer;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Simple_Screen_Recorder
{
    [DesignerGenerated()]
    public partial class RecorderScreenMainWindow : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components is object)
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecorderScreenMainWindow));
            this.RadioTwoTrack = new System.Windows.Forms.RadioButton();
            this.RadioDesktop = new System.Windows.Forms.RadioButton();
            this.Label2 = new System.Windows.Forms.Label();
            this.BtnStop = new System.Windows.Forms.Button();
            this.btnStartRecording = new System.Windows.Forms.Button();
            this.LbTimer = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.RecState = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BtnExit = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.remuxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeVideoAndDesktopAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.españolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.italianoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.中文简体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOutputRecordings = new System.Windows.Forms.Button();
            this.comboBoxCodec = new System.Windows.Forms.ComboBox();
            this.labelCodec = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RadioTwoTrack
            // 
            this.RadioTwoTrack.AutoSize = true;
            this.RadioTwoTrack.Checked = true;
            this.RadioTwoTrack.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioTwoTrack.ForeColor = System.Drawing.SystemColors.Control;
            this.RadioTwoTrack.Location = new System.Drawing.Point(25, 309);
            this.RadioTwoTrack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioTwoTrack.Name = "RadioTwoTrack";
            this.RadioTwoTrack.Size = new System.Drawing.Size(279, 24);
            this.RadioTwoTrack.TabIndex = 3;
            this.RadioTwoTrack.TabStop = true;
            this.RadioTwoTrack.Text = "System sounds and microphone audio";
            this.RadioTwoTrack.UseVisualStyleBackColor = true;
            // 
            // RadioDesktop
            // 
            this.RadioDesktop.AutoSize = true;
            this.RadioDesktop.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RadioDesktop.ForeColor = System.Drawing.SystemColors.Control;
            this.RadioDesktop.Location = new System.Drawing.Point(25, 339);
            this.RadioDesktop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RadioDesktop.Name = "RadioDesktop";
            this.RadioDesktop.Size = new System.Drawing.Size(156, 24);
            this.RadioDesktop.TabIndex = 4;
            this.RadioDesktop.TabStop = true;
            this.RadioDesktop.Text = "Only system sounds";
            this.RadioDesktop.UseVisualStyleBackColor = true;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Label2.ForeColor = System.Drawing.Color.SkyBlue;
            this.Label2.Location = new System.Drawing.Point(21, 30);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(111, 32);
            this.Label2.TabIndex = 21;
            this.Label2.Text = "Controls";
            // 
            // BtnStop
            // 
            this.BtnStop.FlatAppearance.BorderSize = 2;
            this.BtnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStop.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnStop.ForeColor = System.Drawing.Color.Transparent;
            this.BtnStop.Image = global::Simple_Screen_Recorder.Properties.Resources.stop_button;
            this.BtnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnStop.Location = new System.Drawing.Point(25, 111);
            this.BtnStop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(308, 41);
            this.BtnStop.TabIndex = 2;
            this.BtnStop.Text = "Stop Recording";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnStartRecording
            // 
            this.btnStartRecording.FlatAppearance.BorderSize = 2;
            this.btnStartRecording.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnStartRecording.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnStartRecording.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartRecording.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnStartRecording.ForeColor = System.Drawing.Color.Transparent;
            this.btnStartRecording.Image = global::Simple_Screen_Recorder.Properties.Resources.record_button;
            this.btnStartRecording.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStartRecording.Location = new System.Drawing.Point(25, 66);
            this.btnStartRecording.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnStartRecording.Name = "btnStartRecording";
            this.btnStartRecording.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnStartRecording.Size = new System.Drawing.Size(308, 41);
            this.btnStartRecording.TabIndex = 1;
            this.btnStartRecording.Text = "Start Recording";
            this.btnStartRecording.UseVisualStyleBackColor = true;
            this.btnStartRecording.Click += new System.EventHandler(this.btnStartRecording_Click);
            // 
            // LbTimer
            // 
            this.LbTimer.AutoSize = true;
            this.LbTimer.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.LbTimer.ForeColor = System.Drawing.Color.White;
            this.LbTimer.Location = new System.Drawing.Point(21, 530);
            this.LbTimer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LbTimer.Name = "LbTimer";
            this.LbTimer.Size = new System.Drawing.Size(55, 25);
            this.LbTimer.TabIndex = 29;
            this.LbTimer.Text = "0:0:0";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label5.ForeColor = System.Drawing.SystemColors.Control;
            this.Label5.Location = new System.Drawing.Point(24, 404);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(171, 20);
            this.Label5.TabIndex = 39;
            this.Label5.Text = "System sounds (speaker)";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Label4.ForeColor = System.Drawing.SystemColors.Control;
            this.Label4.Location = new System.Drawing.Point(25, 456);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(89, 20);
            this.Label4.TabIndex = 38;
            this.Label4.Text = "Microphone";
            // 
            // ComboBox2
            // 
            this.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Location = new System.Drawing.Point(25, 427);
            this.ComboBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(308, 25);
            this.ComboBox2.TabIndex = 6;
            // 
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(25, 479);
            this.ComboBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(308, 25);
            this.ComboBox1.TabIndex = 5;
            // 
            // RecState
            // 
            this.RecState.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.SkyBlue;
            this.label6.Location = new System.Drawing.Point(22, 273);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 32);
            this.label6.TabIndex = 40;
            this.label6.Text = "Audio settings";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.SkyBlue;
            this.label7.Location = new System.Drawing.Point(21, 368);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 32);
            this.label7.TabIndex = 41;
            this.label7.Text = "Sound devices";
            // 
            // BtnExit
            // 
            this.BtnExit.FlatAppearance.BorderSize = 2;
            this.BtnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExit.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnExit.ForeColor = System.Drawing.Color.Transparent;
            this.BtnExit.Image = global::Simple_Screen_Recorder.Properties.Resources.log_out_button;
            this.BtnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnExit.Location = new System.Drawing.Point(198, 522);
            this.BtnExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(135, 39);
            this.BtnExit.TabIndex = 42;
            this.BtnExit.Text = "     Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remuxToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.languagesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(357, 25);
            this.menuStrip1.TabIndex = 43;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // remuxToolStripMenuItem
            // 
            this.remuxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem,
            this.mergeVideoAndDesktopAudioToolStripMenuItem});
            this.remuxToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.remuxToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.remuxToolStripMenuItem.Name = "remuxToolStripMenuItem";
            this.remuxToolStripMenuItem.Size = new System.Drawing.Size(127, 21);
            this.remuxToolStripMenuItem.Text = "Merge media files";
            // 
            // mergeVideoDesktopAndMicAudioToolStripMenuItem
            // 
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem.BackColor = System.Drawing.Color.Gray;
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem.Name = "mergeVideoDesktopAndMicAudioToolStripMenuItem";
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem.Text = "Merge all media";
            this.mergeVideoDesktopAndMicAudioToolStripMenuItem.Click += new System.EventHandler(this.mergeVideoDesktopAndMicAudioToolStripMenuItem_Click);
            // 
            // mergeVideoAndDesktopAudioToolStripMenuItem
            // 
            this.mergeVideoAndDesktopAudioToolStripMenuItem.BackColor = System.Drawing.Color.Gray;
            this.mergeVideoAndDesktopAudioToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mergeVideoAndDesktopAudioToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.mergeVideoAndDesktopAudioToolStripMenuItem.Name = "mergeVideoAndDesktopAudioToolStripMenuItem";
            this.mergeVideoAndDesktopAudioToolStripMenuItem.Size = new System.Drawing.Size(267, 22);
            this.mergeVideoAndDesktopAudioToolStripMenuItem.Text = "Merge video and system sounds";
            this.mergeVideoAndDesktopAudioToolStripMenuItem.Click += new System.EventHandler(this.mergeVideoAndDesktopAudioToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // languagesToolStripMenuItem
            // 
            this.languagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.españolToolStripMenuItem,
            this.toolStripMenuItem1,
            this.italianoToolStripMenuItem,
            this.中文简体ToolStripMenuItem});
            this.languagesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.languagesToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            this.languagesToolStripMenuItem.Size = new System.Drawing.Size(85, 21);
            this.languagesToolStripMenuItem.Text = "Languages";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.englishToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.englishToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // españolToolStripMenuItem
            // 
            this.españolToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.españolToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.españolToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.españolToolStripMenuItem.Name = "españolToolStripMenuItem";
            this.españolToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.españolToolStripMenuItem.Text = "Español";
            this.españolToolStripMenuItem.Click += new System.EventHandler(this.españolToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Control;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem1.Text = "Português do Brasil";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // italianoToolStripMenuItem
            // 
            this.italianoToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.italianoToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.italianoToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.italianoToolStripMenuItem.Name = "italianoToolStripMenuItem";
            this.italianoToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.italianoToolStripMenuItem.Text = "Italiano";
            this.italianoToolStripMenuItem.Click += new System.EventHandler(this.italianoToolStripMenuItem_Click);
            // 
            // 中文简体ToolStripMenuItem
            // 
            this.中文简体ToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.中文简体ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.中文简体ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.中文简体ToolStripMenuItem.Name = "中文简体ToolStripMenuItem";
            this.中文简体ToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.中文简体ToolStripMenuItem.Text = "中文(简体)";
            this.中文简体ToolStripMenuItem.Click += new System.EventHandler(this.中文简体ToolStripMenuItem_Click);
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
            this.btnOutputRecordings.Location = new System.Drawing.Point(25, 156);
            this.btnOutputRecordings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOutputRecordings.Name = "btnOutputRecordings";
            this.btnOutputRecordings.Size = new System.Drawing.Size(308, 40);
            this.btnOutputRecordings.TabIndex = 44;
            this.btnOutputRecordings.Text = " Open Recordings Folder";
            this.btnOutputRecordings.UseVisualStyleBackColor = true;
            this.btnOutputRecordings.Click += new System.EventHandler(this.btnOutputRecordings_Click);
            // 
            // comboBoxCodec
            // 
            this.comboBoxCodec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCodec.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxCodec.FormattingEnabled = true;
            this.comboBoxCodec.Location = new System.Drawing.Point(25, 241);
            this.comboBoxCodec.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxCodec.Name = "comboBoxCodec";
            this.comboBoxCodec.Size = new System.Drawing.Size(308, 25);
            this.comboBoxCodec.TabIndex = 45;
            // 
            // labelCodec
            // 
            this.labelCodec.AutoSize = true;
            this.labelCodec.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelCodec.ForeColor = System.Drawing.Color.SkyBlue;
            this.labelCodec.Location = new System.Drawing.Point(25, 206);
            this.labelCodec.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCodec.Name = "labelCodec";
            this.labelCodec.Size = new System.Drawing.Size(84, 32);
            this.labelCodec.TabIndex = 46;
            this.labelCodec.Text = "Codec";
            // 
            // RecorderScreenMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(357, 573);
            this.Controls.Add(this.labelCodec);
            this.Controls.Add(this.comboBoxCodec);
            this.Controls.Add(this.btnOutputRecordings);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.ComboBox2);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.LbTimer);
            this.Controls.Add(this.RadioTwoTrack);
            this.Controls.Add(this.RadioDesktop);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.btnStartRecording);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "RecorderScreenMainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Screen Recorder";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RecorderScreenForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        internal RadioButton RadioTwoTrack;
        internal RadioButton RadioDesktop;
        internal Label Label2;
        internal Button BtnStop;
        internal Button btnStartRecording;
        internal Label LbTimer;
        internal Label Label5;
        internal Label Label4;
        internal ComboBox ComboBox2;
        internal ComboBox ComboBox1;
        internal Timer RecState;
        internal Label label6;
        internal Label label7;
        internal Button BtnExit;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem remuxToolStripMenuItem;
        private ToolStripMenuItem mergeVideoAndDesktopAudioToolStripMenuItem;
        private ToolStripMenuItem mergeVideoDesktopAndMicAudioToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem languagesToolStripMenuItem;
        private ToolStripMenuItem españolToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ToolStripMenuItem 中文简体ToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem italianoToolStripMenuItem;
        internal Button btnOutputRecordings;
        internal ComboBox comboBoxCodec;
        internal Label labelCodec;
    }
}