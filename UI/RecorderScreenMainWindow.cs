using Microsoft.VisualBasic;
using NAudio.Wave;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.ScreenRecorderWin;
using Simple_Screen_Recorder.UI;
using System.Diagnostics;
using System.IO;
using Application = System.Windows.Forms.Application;

namespace Simple_Screen_Recorder
{
    public partial class RecorderScreenMainWindow
    {
        private DateTime TimeRec = new DateTime();
        private string VideoName = "";
        public int ProcessId { get; private set; }

        public RecorderScreenMainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;

            GetTextsMain();

            AudioMic.OpenComp();
            ComboBoxMicrophone.DataSource = AudioMic.cboDIspositivos.DataSource;
            AudioDesktop.OpenComp();
            ComboBoxSpeaker.DataSource = AudioDesktop.cboDIspositivos.DataSource;

            comboBoxCodec.Items.Add("MPEG-4");
            comboBoxCodec.Items.Add("H264 NVENC (Nvidia Graphics Cards)");
            comboBoxCodec.Items.Add("H264 AMF (AMD Graphics Cards)");
            comboBoxCodec.SelectedIndex = 0;

            comboBoxResolution.Items.Add("3840x2160");
            comboBoxResolution.Items.Add("2560x1440");
            comboBoxResolution.Items.Add("1920x1080");
            comboBoxResolution.Items.Add("1600x900");
            comboBoxResolution.Items.Add("1536x864");
            comboBoxResolution.Items.Add("1440x900");
            comboBoxResolution.Items.Add("1366x768");
            comboBoxResolution.Items.Add("1360x768");
            comboBoxResolution.Items.Add("1280x720");
            comboBoxResolution.SelectedIndex = 2;

            comboBoxFps.Items.Add("30");
            comboBoxFps.Items.Add("60");
            comboBoxFps.SelectedIndex = 0;

            ComboBoxFormat.Items.Add(".avi");
            ComboBoxFormat.Items.Add(".mkv");
            ComboBoxFormat.SelectedIndex = 0;
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            switch (ComboBoxFormat.SelectedItem)
            {
                case ".mkv":
                    {
                        LbTimer.ForeColor = Color.IndianRed;
                        TimeRec = DateTime.Now;
                        RecState.Enabled = true;
                        VideoName = "Video." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".mkv";
                        break;
                    }

                case ".avi":
                    {
                        LbTimer.ForeColor = Color.IndianRed;
                        TimeRec = DateTime.Now;
                        RecState.Enabled = true;
                        VideoName = "Video." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".avi";
                        break;
                    }
            }

            if (RadioTwoTrack.Checked == true)
            {
                RecMic();
                RecSpeaker();
            }
            else if (RadioDesktop.Checked == true)
            {
                RecSpeaker();
            }
            else
            {
                RecMic();
            }

            VideoCodecs();
        }


        private void VideoCodecs()
        {
            if (CheckBoxAllMonitors.Checked == true)
            {
                switch (comboBoxCodec.SelectedItem)
                {
                    case "MPEG-4":
                        {
                            int fps = int.Parse((string)comboBoxFps.SelectedItem);
                            ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate " + fps + " -show_region 1 -i desktop -c:v mpeg4 -b:v 10000k Recordings/" + VideoName + "");
                            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                            ProcessId.CreateNoWindow = true;
                            ProcessId.RedirectStandardOutput = true;
                            Process.Start(ProcessId);

                            btnStartRecording.Enabled = false;
                            ComboBoxMicrophone.Enabled = false;
                            ComboBoxSpeaker.Enabled = false;
                            comboBoxCodec.Enabled = false;
                            comboBoxResolution.Enabled = false;
                            comboBoxFps.Enabled = false;
                            RadioTwoTrack.Enabled = false;
                            RadioDesktop.Enabled = false;
                            radioMicrophone.Enabled = false;
                            CheckBoxAllMonitors.Enabled = false;
                            ComboBoxFormat.Enabled = false;
                            break;
                        }

                    case "H264 NVENC (Nvidia Graphics Cards)":
                        {
                            int fps = int.Parse((string)comboBoxFps.SelectedItem);
                            ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate " + fps + " -show_region 1 -i desktop -c:v h264_nvenc -qp 0 Recordings/" + VideoName + "");
                            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                            ProcessId.CreateNoWindow = true;
                            ProcessId.RedirectStandardOutput = true;
                            Process.Start(ProcessId);

                            btnStartRecording.Enabled = false;
                            ComboBoxMicrophone.Enabled = false;
                            ComboBoxSpeaker.Enabled = false;
                            comboBoxCodec.Enabled = false;
                            comboBoxResolution.Enabled = false;
                            comboBoxFps.Enabled = false;
                            RadioTwoTrack.Enabled = false;
                            RadioDesktop.Enabled = false;
                            radioMicrophone.Enabled = false;
                            CheckBoxAllMonitors.Enabled = false;
                            ComboBoxFormat.Enabled = false;
                            break;
                        }

                    case "H264 AMF (AMD Graphics Cards)":
                        {
                            int fps = int.Parse((string)comboBoxFps.SelectedItem);
                            ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate " + fps + " -show_region 1 -i desktop -c:v h264_amf -qp 0 Recordings/" + VideoName + "");
                            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                            ProcessId.CreateNoWindow = true;
                            ProcessId.RedirectStandardOutput = true;
                            Process.Start(ProcessId);

                            btnStartRecording.Enabled = false;
                            ComboBoxMicrophone.Enabled = false;
                            ComboBoxSpeaker.Enabled = false;
                            comboBoxCodec.Enabled = false;
                            comboBoxResolution.Enabled = false;
                            comboBoxFps.Enabled = false;
                            RadioTwoTrack.Enabled = false;
                            RadioDesktop.Enabled = false;
                            radioMicrophone.Enabled = false;
                            CheckBoxAllMonitors.Enabled = false;
                            ComboBoxFormat.Enabled = false;
                            break;
                        }
                }

            }
            else
            {
                switch (comboBoxCodec.SelectedItem)
                {
                    case "MPEG-4":
                        {
                            int fps = int.Parse((string)comboBoxFps.SelectedItem);
                            string? resolution = comboBoxResolution.SelectedItem.ToString();
                            ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate " + fps + " -video_size " + resolution + " -offset_x 0 -offset_y 0 -show_region 1  -i desktop -c:v mpeg4 -b:v 10000k Recordings/" + VideoName + "");
                            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                            ProcessId.CreateNoWindow = true;
                            ProcessId.RedirectStandardOutput = true;
                            Process.Start(ProcessId);

                            btnStartRecording.Enabled = false;
                            ComboBoxMicrophone.Enabled = false;
                            ComboBoxSpeaker.Enabled = false;
                            comboBoxCodec.Enabled = false;
                            comboBoxResolution.Enabled = false;
                            comboBoxFps.Enabled = false;
                            RadioTwoTrack.Enabled = false;
                            RadioDesktop.Enabled = false;
                            radioMicrophone.Enabled = false;
                            CheckBoxAllMonitors.Enabled = false;
                            ComboBoxFormat.Enabled = false;
                            break;
                        }

                    case "H264 NVENC (Nvidia Graphics Cards)":
                        {
                            int fps = int.Parse((string)comboBoxFps.SelectedItem);
                            string? resolution = comboBoxResolution.SelectedItem.ToString();
                            ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate " + fps + " -video_size " + resolution + " -offset_x 0 -offset_y 0 -show_region 1  -i desktop -c:v h264_nvenc -qp 0 Recordings/" + VideoName + "");
                            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                            ProcessId.CreateNoWindow = true;
                            ProcessId.RedirectStandardOutput = true;
                            Process.Start(ProcessId);

                            btnStartRecording.Enabled = false;
                            ComboBoxMicrophone.Enabled = false;
                            ComboBoxSpeaker.Enabled = false;
                            comboBoxCodec.Enabled = false;
                            comboBoxResolution.Enabled = false;
                            comboBoxFps.Enabled = false;
                            RadioTwoTrack.Enabled = false;
                            RadioDesktop.Enabled = false;
                            radioMicrophone.Enabled = false;
                            CheckBoxAllMonitors.Enabled = false;
                            ComboBoxFormat.Enabled = false;
                            break;
                        }

                    case "H264 AMF (AMD Graphics Cards)":
                        {
                            int fps = int.Parse((string)comboBoxFps.SelectedItem);
                            string? resolution = comboBoxResolution.SelectedItem.ToString();
                            ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab -framerate " + fps + " -video_size " + resolution + " -offset_x 0 -offset_y 0 -show_region 1 -i desktop -c:v h264_amf -qp 0 Recordings/" + VideoName + "");
                            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
                            ProcessId.CreateNoWindow = true;
                            ProcessId.RedirectStandardOutput = true;
                            Process.Start(ProcessId);

                            btnStartRecording.Enabled = false;
                            ComboBoxMicrophone.Enabled = false;
                            ComboBoxSpeaker.Enabled = false;
                            comboBoxCodec.Enabled = false;
                            comboBoxResolution.Enabled = false;
                            comboBoxFps.Enabled = false;
                            RadioTwoTrack.Enabled = false;
                            RadioDesktop.Enabled = false;
                            radioMicrophone.Enabled = false;
                            CheckBoxAllMonitors.Enabled = false;
                            ComboBoxFormat.Enabled = false;
                            break;
                        }
                }
            }

        }

        public void StopRec()
        {
            btnStartRecording.Enabled = true;
            comboBoxCodec.Enabled = true;
            ComboBoxMicrophone.Enabled = true;
            ComboBoxSpeaker.Enabled = true;
            comboBoxResolution.Enabled = true;
            RadioTwoTrack.Enabled = true;
            RadioDesktop.Enabled = true;
            radioMicrophone.Enabled = true;
            comboBoxFps.Enabled = true;
            CheckBoxAllMonitors.Enabled = true;
            ComboBoxFormat.Enabled = true;

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
            else if (AudioMic.waveIn is object)
            {
                AudioMic.waveIn.StopRecording();
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

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                LbTimer.ForeColor = Color.White;
                LbTimer.Text = "00:00:00";
                RecState.Enabled = false;
                StopRec();

            }
            catch (Exception)
            {

                return;
            }
        }

        public static void RecMic()
        {
            AudioMic.Cleanup();
            AudioMic.CreateWaveInDevice();
            AudioMic.outputFilename = "MicrophoneAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            AudioMic.writer = new WaveFileWriter(Path.Combine(AudioMic.outputFolder, AudioMic.outputFilename), AudioMic.waveIn.WaveFormat);
            AudioMic.waveIn.StartRecording();
        }

        public static void RecSpeaker()
        {
            AudioDesktop.Cleanup();
            AudioDesktop.CreateWaveInDevice();
            GrabadorPantalla.My.MyProject.Computer.Audio.Play("Background.wav", AudioPlayMode.BackgroundLoop);
            AudioDesktop.outputFilename = "SystemAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            AudioDesktop.writer = new WaveFileWriter(Path.Combine(AudioDesktop.outputFolder, AudioDesktop.outputFilename), AudioDesktop.waveIn.WaveFormat);
            AudioDesktop.waveIn.StartRecording();
        }

        private void CheckBoxAllMonitors_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAllMonitors.Checked)
            {
                comboBoxResolution.Enabled = false;
            }
            else
            {
                comboBoxResolution.Enabled = true;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var Difference = DateTime.Now.Subtract(TimeRec);
            LbTimer.Text = "Rec: " + Difference.Hours.ToString().PadLeft(2, '0') + ":" + Difference.Minutes.ToString().PadLeft(2, '0') + ":" + Difference.Seconds.ToString().PadLeft(2, '0');
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

        private void audioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AudioRecorderMainWindow NewAudioRecording = new();
            NewAudioRecording.Show();
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
            crownGroupBox1.Text = StringsEN.crownGroupBox1;
            crownGroupBox2.Text = StringsEN.crownGroupBox2;
            crownGroupBox3.Text = StringsEN.crownGroupBox3;
            audioToolStripMenuItem.Text = StringsEN.audioToolStripMenuItem;
            radioMicrophone.Text = StringsEN.radioMicrophone;
            labelFps.Text = StringsEN.labelFps;
            CheckBoxAllMonitors.Text = StringsEN.CheckBoxAllMonitors;
            labelFormat.Text = StringsEN.labelFormat;
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

        private void ukranianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "uk-UA";
            GetTextsMain();
        }

        #endregion

        private void RecorderScreenMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnStartRecording.Enabled == true && e.KeyCode == Keys.F9)
            {
                btnStartRecording.PerformClick();
            }
            else if (e.KeyCode == Keys.F9)
            {
                BtnStop.PerformClick();
            }

            if (e.KeyCode == Keys.F10)
            {
                btnOutputRecordings.PerformClick();
            }

            if (e.KeyCode == Keys.Escape)
            {
                BtnExit.PerformClick();
            }

        }
    }
}