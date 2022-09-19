using Microsoft.VisualBasic;
using NAudio.Wave;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.ScreenRecorderWin;
using Simple_Screen_Recorder.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace Simple_Screen_Recorder
{
    public partial class RecorderScreenMainWindow
    {
        private DateTime TimeRec = new DateTime();
        private string VideoName = "";

        public RecorderScreenMainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetTextsMain();

            AudioMic.OpenComp();
            ComboBox1.DataSource = AudioMic.cboDIspositivos.DataSource;
            AudioDesktop.OpenComp();
            ComboBox2.DataSource = AudioDesktop.cboDIspositivos.DataSource;

            comboBoxCodec.Items.Add("MPEG-4");
            comboBoxCodec.Items.Add("H264 NVENC (Nvidia Graphics Cards)");
            comboBoxCodec.SelectedIndex = 0;

            comboBoxResolution.Items.Add("1920x1080");
            comboBoxResolution.Items.Add("1600x900");
            comboBoxResolution.Items.Add("1536x864");
            comboBoxResolution.Items.Add("1366x768");
            comboBoxResolution.SelectedIndex = 0;
        }
        public int ProcessId { get; private set; }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            LbTimer.ForeColor = Color.IndianRed;
            TimeRec = DateTime.Now;
            RecState.Enabled = true;
            VideoName = "Video." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".avi";

            if (RadioTwoTrack.Checked == true)
            {
                RecMic();
                RecSpeaker();
            }
            else
            {
                RecSpeaker();
            }

            VideoCodecs();
        }

        public void StopRec()
        {
            btnStartRecording.Enabled = true;
            comboBoxCodec.Enabled = true;
            ComboBox1.Enabled = true;
            ComboBox2.Enabled = true;
            comboBoxResolution.Enabled = true;

            if (RadioTwoTrack.Checked == true)
            {
                if (AudioMic.waveIn is object)
                {
                    AudioMic.waveIn.StopRecording();
                }

                if (AudioDesktop.waveIn is object)
                {
                    AudioDesktop.waveIn.StopRecording();
                }
            }
            else if (AudioDesktop.waveIn is object)
            {
                AudioDesktop.waveIn.StopRecording();
            }

            GrabadorPantalla.My.MyProject.Computer.Audio.Stop();

            foreach (Process proceso in Process.GetProcesses())
            {
                if (proceso.ProcessName == "ffmpeg")
                {
                    proceso.Kill();
                }
            }

            Process proc = Process.GetProcessById(ProcessId);
            proc.Kill();
        }

        #region Codecs and resolutions
        public void VideoCodecs()
        {
            switch (comboBoxCodec.SelectedItem)
            {
                case "MPEG-4":
                    {
                        switch (comboBoxResolution.SelectedItem)
                        {
                            case "1920x1080":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1920x1080 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v mpeg4 -b:v 10000k Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }

                            case "1600x900":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1600x900 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v mpeg4 -b:v 10000k Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }

                            case "1536x864":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1536x864 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v mpeg4 -b:v 10000k Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }

                            case "1366x768":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1366x768 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v mpeg4 -b:v 10000k Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }
                        }

                        break;
                    }

                case "H264 NVENC (Nvidia Graphics Cards)":
                    {
                        switch (comboBoxResolution.SelectedItem)
                        {
                            case "1920x1080":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1920x1080 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v h264_nvenc -qp 0 Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }

                            case "1600x900":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1600x900 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v h264_nvenc -qp 0 Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }

                            case "1536x864":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1536x864 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v h264_nvenc -qp 0 Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }

                            case "1366x768":
                                {
                                    ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate 30 -video_size 1366x768 -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v h264_nvenc -qp 0 Recordings/" + VideoName + "");
                                    ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                                    ProcessId.CreateNoWindow = true;
                                    ProcessId.RedirectStandardOutput = true;
                                    Process.Start(ProcessId);

                                    btnStartRecording.Enabled = false;
                                    ComboBox1.Enabled = false;
                                    ComboBox2.Enabled = false;
                                    comboBoxCodec.Enabled = false;
                                    comboBoxResolution.Enabled = false;
                                    break;
                                }
                        }

                        break;
                    }
            }

        }
        #endregion

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                LbTimer.ForeColor = Color.White;
                LbTimer.Text = "0:0:0";
                RecState.Enabled = false;
                StopRec();

            }
            catch (Exception)
            {

                return;
            }
        }

        public void RecMic()
        {
            AudioMic.Cleanup();
            AudioMic.CreateWaveInDevice();
            AudioMic.outputFilename = "AudioMicrophone." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            AudioMic.writer = new WaveFileWriter(Path.Combine(AudioMic.outputFolder, AudioMic.outputFilename), AudioMic.waveIn.WaveFormat);
            AudioMic.waveIn.StartRecording();
        }

        public void RecSpeaker()
        {
            AudioDesktop.Cleanup();
            AudioDesktop.CreateWaveInDevice();
            GrabadorPantalla.My.MyProject.Computer.Audio.Play("Background.wav", AudioPlayMode.BackgroundLoop);
            AudioDesktop.outputFilename = "AudioSystem." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            AudioDesktop.writer = new WaveFileWriter(Path.Combine(AudioDesktop.outputFolder, AudioDesktop.outputFilename), AudioDesktop.waveIn.WaveFormat);
            AudioDesktop.waveIn.StartRecording();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var Difference = DateTime.Now.Subtract(TimeRec);
            LbTimer.Text = "Recording: " + Difference.Hours.ToString() + ":" + Difference.Minutes.ToString() + ":" + Difference.Seconds.ToString();
        }

        private void mergeVideoDesktopAndMicAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeAllForm NewMergeVDM = new();
            NewMergeVDM.Show();
        }

        private void mergeVideoAndDesktopAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeVidDeskForm NewMergeVD = new();
            NewMergeVD.Show();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm NewAbout = new();
            NewAbout.ShowDialog();
        }

        private void RecorderScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void btnOutputRecordings_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "Recordings");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region TranslationCode

        private void GetTextsMain()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Settings.Default.Languages);
            Label2.Text = StringsEN.Label2;
            aboutToolStripMenuItem.Text = StringsEN.aboutToolStripMenuItem;
            BtnExit.Text = StringsEN.BtnExit;
            btnStartRecording.Text = StringsEN.btnStartRecording;
            BtnStop.Text = StringsEN.BtnStop;
            Label4.Text = StringsEN.Label4;
            Label5.Text = StringsEN.Label5;
            label6.Text = StringsEN.Label6;
            label7.Text = StringsEN.Label7;
            languagesToolStripMenuItem.Text = StringsEN.languagesToolStripMenuItem;
            mergeVideoAndDesktopAudioToolStripMenuItem.Text = StringsEN.mergeVideoAndDesktopAudioToolStripMenuItem;
            mergeVideoDesktopAndMicAudioToolStripMenuItem.Text = StringsEN.mergeVideoDesktopAndMicAudioToolStripMenuItem;
            RadioDesktop.Text = StringsEN.RadioDesktop;
            RadioTwoTrack.Text = StringsEN.RadioTwoTrack;
            remuxToolStripMenuItem.Text = StringsEN.remuxToolStripMenuItem;
            btnOutputRecordings.Text = StringsEN.btnOutputRecordings;
            labelCodec.Text = StringsEN.labelCodec;
            labelResolution.Text = StringsEN.labelResolution;
        }

        private void españolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "es-ES";
            GetTextsMain();
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "en-US";
            GetTextsMain();
        }

        private void 中文简体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "zh-CN";
            GetTextsMain();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "pt-BR";
            GetTextsMain();
        }

        private void italianoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "it-IT";
            GetTextsMain();
        }

        #endregion
    }
}