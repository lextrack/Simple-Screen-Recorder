using Microsoft.VisualBasic;
using NAudio.Wave;
using Simple_Screen_Recorder.AudioComp;
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
        private const string DateFormat = "MM-dd-yyyy.HH.mm.ss";
        private DateTime TimeRec = DateTime.MinValue;
        private string VideoName = "";
        public int ProcessId { get; private set; }
        public static string ResourcePath = Path.Combine(Directory.GetCurrentDirectory(), @"FFmpegResources\ffmpeg");

        public RecorderScreenMainWindow()
        {
            InitializeComponent();
        }

        private void InitializeForm()
        {
            GetTextsMain();
            CheckMonitors();
            OpenAudioComponents();
            InitializeComboBoxes();
            CreateOutputFolder();
            SetKeyPreview();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void SetKeyPreview()
        {
            this.KeyPreview = true;
        }

        private void CreateOutputFolder()
        {
            string outputFolderPath = Path.Combine(Application.StartupPath, "OutputFiles");

            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }
        }

        private void InitializeComboBoxes()
        {
            comboBoxCodec.Items.AddRange(new[] { "H264 (Default)", "MPEG-4", "H264 NVENC (Nvidia)", "H264 AMF (AMD)" });
            comboBoxCodec.SelectedIndex = 0;

            comboBoxFps.Items.AddRange(new[] { "30", "60" });
            comboBoxFps.SelectedIndex = 1;

            comboBoxBitrate.Items.AddRange(new[] { "2000k", "4000k", "6000k", "8000k", "10000k", "15000k", "20000k" });
            comboBoxBitrate.SelectedIndex = 2;


            ComboBoxFormat.Items.AddRange(new[] { ".avi", ".mkv", ".wmv" });
            ComboBoxFormat.SelectedIndex = 0;
        }

        private void OpenAudioComponents()
        {
            ScreenAudioMic.OpenComp();
            ComboBoxMicrophone.DataSource = ScreenAudioMic.cboDIspositivos.DataSource;

            ScreenAudioDesktop.OpenComp();
            ComboBoxSpeaker.DataSource = ScreenAudioDesktop.cboDIspositivos.DataSource;
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            var format = ComboBoxFormat.SelectedItem.ToString();
            VideoName = $"Video.{DateTime.Now.ToString(DateFormat)}.{format.TrimStart('.')}";
            LbTimer.ForeColor = Color.IndianRed;
            TimeRec = DateTime.Now;
            CountRecVideo.Enabled = true;

            RecordAudio();
            VideoCodecs();
            DisableElementsUI();
        }

        private void CheckMonitors()
        {
            var monitorNames = Screen.AllScreens.Select((screen, index) =>
            {
                var prefix = screen.Primary ? "Main monitor" : $"Monitor {index + 1}";
                return $"{prefix} ({screen.Bounds.Width}x{screen.Bounds.Height})";
            }).ToArray();

            comboBoxMonitors.DataSource = monitorNames;
            comboBoxMonitors.SelectedIndex = 0;
        }

        private void RefreshMonitors_Click(object sender, EventArgs e)
        {
            CheckMonitors();
        }

        private void CheckBoxAllMonitors_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxAllMonitors.Checked)
            {
                comboBoxMonitors.Enabled = false;
            }
            else
            {
                comboBoxMonitors.Enabled = true;
            }
        }

        private void RecordAudio()
        {
            if (RadioTwoTrack.Checked)
            {
                if (WaveIn.DeviceCount > 0)
                {
                    RecMic();
                }
                else
                {
                    MessageBox.Show(StringsEN.message3, "Error");
                }

                RecSpeaker();
            }
            else if (RadioDesktop.Checked)
            {
                RecSpeaker();
            }
            else
            {
                if (WaveIn.DeviceCount > 0)
                {
                    RecMic();
                }
                else
                {
                    MessageBox.Show(StringsEN.message3, "Error");
                }
            }
        }

        private void VideoCodecs()
        {
            int fps = int.Parse((string)comboBoxFps.SelectedItem);
            string Bitrate = (string)comboBoxBitrate.SelectedItem;

            Screen selectedScreen = CheckBoxAllMonitors.Checked ? Screen.PrimaryScreen : Screen.AllScreens[comboBoxMonitors.SelectedIndex];
            Rectangle bounds = selectedScreen.Bounds;

            string codecArguments = "";
            switch (comboBoxCodec.SelectedItem)
            {
                case "H264 (Default)":
                    codecArguments = $"-c:v h264_mf -qp 0 -b:v {Bitrate} Recordings/{VideoName}";
                    break;

                case "MPEG-4":
                    codecArguments = $"-c:v mpeg4 -b:v {Bitrate} -preset medium Recordings/{VideoName}";
                    break;

                case "H264 NVENC (Nvidia)":
                    codecArguments = $"-c:v h264_nvenc -qp 0 -b:v {Bitrate} Recordings/{VideoName}";
                    break;

                case "H264 AMF (AMD)":
                    codecArguments = $"-c:v h264_amf -qp 0 -b:v {Bitrate} Recordings/{VideoName}";
                    break;
            }

            string getScreen = $"-f gdigrab -framerate {fps} -offset_x {bounds.Left} -offset_y {bounds.Top} -video_size {bounds.Width}x{bounds.Height} -i desktop {codecArguments}";

            ProcessStartInfo ProcessId = new("cmd.exe", $"/c {ResourcePath} {getScreen}");
            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessId.CreateNoWindow = true;
            ProcessId.RedirectStandardOutput = true;
            Process.Start(ProcessId);
        }

        private void DisableElementsUI()
        {
            comboBoxMonitors.Enabled = false;
            btnStartRecording.Enabled = false;
            ComboBoxMicrophone.Enabled = false;
            ComboBoxSpeaker.Enabled = false;
            comboBoxCodec.Enabled = false;
            comboBoxFps.Enabled = false;
            RadioTwoTrack.Enabled = false;
            RadioDesktop.Enabled = false;
            radioMicrophone.Enabled = false;
            CheckBoxAllMonitors.Enabled = false;
            ComboBoxFormat.Enabled = false;
            RefreshMonitors.Enabled = false;
            menuStrip1.Enabled = false;
            comboBoxBitrate.Enabled = false;
        }

        private void StopRec()
        {
            btnStartRecording.Enabled = true;
            comboBoxCodec.Enabled = true;
            ComboBoxMicrophone.Enabled = true;
            ComboBoxSpeaker.Enabled = true;
            comboBoxMonitors.Enabled = true;
            RadioTwoTrack.Enabled = true;
            RadioDesktop.Enabled = true;
            radioMicrophone.Enabled = true;
            comboBoxFps.Enabled = true;
            CheckBoxAllMonitors.Enabled = true;
            ComboBoxFormat.Enabled = true;
            RefreshMonitors.Enabled = true;
            menuStrip1.Enabled = true;
            comboBoxBitrate.Enabled = true;

            CheckAudioStop();
            CheckFfmpegProcces();
        }

        private void CheckAudioStop()
        {
            if (RadioTwoTrack.Checked == true)
            {
                if (ScreenAudioMic.waveIn is object)
                {
                    ScreenAudioMic.waveIn.StopRecording();
                }

                if (ScreenAudioDesktop.waveIn is object)
                {
                    ScreenAudioDesktop.waveIn.StopRecording();
                }
            }
            else if (ScreenAudioDesktop.waveIn is object)
            {
                ScreenAudioDesktop.waveIn.StopRecording();
            }
            else if (ScreenAudioMic.waveIn is object)
            {
                ScreenAudioMic.waveIn.StopRecording();
            }

            var soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.Stop();
        }

        private void CheckFfmpegProcces()
        {
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

        private static void RecMic()
        {
            ScreenAudioMic.Cleanup();
            ScreenAudioMic.CreateWaveInDevice();
            ScreenAudioMic.outputFilename = "MicrophoneAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            ScreenAudioMic.writer = new WaveFileWriter(Path.Combine(ScreenAudioMic.outputFolder, ScreenAudioMic.outputFilename), ScreenAudioMic.waveIn.WaveFormat);
            ScreenAudioMic.waveIn.StartRecording();
        }

        private static void RecSpeaker()
        {
            ScreenAudioDesktop.Cleanup();
            ScreenAudioDesktop.CreateWaveInDevice();

            var soundPlayer = new System.Media.SoundPlayer(Properties.Resources.Background);
            soundPlayer.PlayLooping();

            ScreenAudioDesktop.outputFilename = "SystemAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            ScreenAudioDesktop.writer = new WaveFileWriter(Path.Combine(ScreenAudioDesktop.outputFolder, ScreenAudioDesktop.outputFilename), ScreenAudioDesktop.waveIn.WaveFormat);
            ScreenAudioDesktop.waveIn.StartRecording();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                LbTimer.ForeColor = Color.White;
                LbTimer.Text = "00:00:00";
                CountRecVideo.Enabled = false;
                StopRec();

            }
            catch (Exception)
            {
                return;
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (btnStartRecording.Enabled == false)
            {
                System.Windows.MessageBox.Show(StringsEN.message2, "Error");
            }
            else
            {
                Application.Exit();
            }
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
            crownGroupBox1.Text = StringsEN.crownGroupBox1;
            crownGroupBox2.Text = StringsEN.crownGroupBox2;
            crownGroupBox3.Text = StringsEN.crownGroupBox3;
            audioToolStripMenuItem.Text = StringsEN.audioToolStripMenuItem;
            radioMicrophone.Text = StringsEN.radioMicrophone;
            labelFps.Text = StringsEN.labelFps;
            CheckBoxAllMonitors.Text = StringsEN.CheckBoxAllMonitors;
            labelFormat.Text = StringsEN.labelFormat;
            labelMonitorSelector.Text = StringsEN.labelMonitorSelector;
            btnMergedFiles.Text = StringsEN.btnMergedFiles;
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

        private void 日本語ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "ja-JP";
            GetTextsMain();
        }

        private void deutschToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "de-DE";
            GetTextsMain();
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "fr-FR";
            GetTextsMain();
        }

        private void العربيةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.Languages = "ar-SA";
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

        private void CountRecVideo_Tick(object sender, EventArgs e)
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
            MergeVideoAudioForm NewMergeVD = new();
            NewMergeVD.Show();
        }

        private void audioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AudioRecorderMainWindow NewAudioRecording = new();
            NewAudioRecording.Show();
            this.Hide();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm NewAbout = new();
            NewAbout.ShowDialog();
        }

        private void btnOutputRecordings_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "Recordings");
        }

        private void btnMergedFiles_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Path.Combine(Application.StartupPath, "OutputFiles"));
        }

        private void RecorderScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}