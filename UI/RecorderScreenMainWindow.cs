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
            InitializeAudioComponents();
            InitializeComboBoxes();
            CreateOutputFolder();
            SetKeyPreview();
            LoadUserSettingsCombobox();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void CreateOutputFolder()
        {
            string outputFolderPath = Path.Combine(Application.StartupPath, "OutputFiles");

            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }
        }

        private void InitializeAudioComponents()
        {
            ScreenAudioMic.OpenComp();
            ComboBoxMicrophone.DataSource = ScreenAudioMic.cboDIspositivos.DataSource;

            ScreenAudioDesktop.OpenComp();
            ComboBoxSpeaker.DataSource = ScreenAudioDesktop.cboDIspositivos.DataSource;
        }

        private void InitializeComboBoxes()
        {
            comboBoxCodec.Items.AddRange(new[] { "H264 (Default)", "MPEG-4", "H264 NVENC (Nvidia)", "H264 AMF (AMD)" });
            comboBoxCodec.SelectedIndex = 0;

            comboBoxFps.Items.AddRange(new[] { "30", "60" });
            comboBoxFps.SelectedIndex = 0;

            comboBoxBitrate.Items.AddRange(new[] { "2000k", "4000k", "6000k", "8000k", "10000k", "15000k", "20000k", "30000k" });
            comboBoxBitrate.SelectedIndex = 0;


            ComboBoxFormat.Items.AddRange(new[] { ".mkv", ".avi" });
            ComboBoxFormat.SelectedIndex = 0;
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
            comboBoxMonitors.Enabled = !CheckBoxAllMonitors.Checked;
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            var format = ComboBoxFormat.SelectedItem.ToString();
            VideoName = $"Video.{DateTime.Now.ToString(DateFormat)}.{format.TrimStart('.')}";
            LbTimer.ForeColor = Color.IndianRed;
            TimeRec = DateTime.Now;
            CountRecVideo.Enabled = true;

            DateTime startTimestamp = DateTime.Now.AddMilliseconds(100);

            Task audioTask = RecordAudio(startTimestamp);
            Task videoTask = Task.Run(() => VideoCodecs(startTimestamp));

            Task.WhenAll(audioTask, videoTask).ContinueWith(_ =>
            {
                DisableElementsUI();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task StartRecordingProcess(string codec, int fps, string bitrate, string screenArgs, DateTime startTimestamp)
        {
            TimeSpan delay = startTimestamp - DateTime.Now;

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay);
            }

            try
            {
                string ffmpegArgs = $"{ResourcePath} -f gdigrab -framerate {fps} {screenArgs} -c:v {codec} -b:v {bitrate} -pix_fmt yuv420p -vsync 1 -loglevel info -hide_banner Recordings/{VideoName}";

                ProcessStartInfo processInfo = new("cmd.exe", $"/c {ffmpegArgs}")
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    Verb = "runas"
                };

                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VideoCodecs(DateTime startTimestamp)
        {
            int fps = int.Parse((string)comboBoxFps.SelectedItem);
            string bitrate = (string)comboBoxBitrate.SelectedItem;
            string codecArgs;
            string codec;

            if (CheckBoxAllMonitors.Checked)
            {
                codecArgs = "-i desktop";
            }
            else
            {
                Screen selectedScreen = Screen.AllScreens[comboBoxMonitors.SelectedIndex];
                Rectangle bounds = selectedScreen.Bounds;
                codecArgs = $"-offset_x {bounds.Left} -offset_y {bounds.Top} -video_size {bounds.Width}x{bounds.Height} -i desktop";
            }

            switch (comboBoxCodec.SelectedItem.ToString())
            {
                case "H264 (Default)":
                    codec = "h264_mf";
                    break;
                case "MPEG-4":
                    codec = "mpeg4 -preset medium";
                    break;
                case "H264 NVENC (Nvidia)":
                    codec = "h264_nvenc";
                    break;
                case "H264 AMF (AMD)":
                    codec = "h264_amf";
                    break;
                default:
                    codec = "h264_mf";
                    break;
            }

            Task.Run(() => StartRecordingProcess(codec, fps, bitrate, codecArgs, startTimestamp)).Wait();
        }


        private async Task RecordAudio(DateTime startTimestamp)
        {
            TimeSpan delay = startTimestamp - DateTime.Now;

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay);
            }

            string selectedOption = comboBoxAudioSource.SelectedItem.ToString();

            if (selectedOption == StringsEN.TwoTrack)
            {
                RecordTwoTracks();
            }
            else if (selectedOption == StringsEN.Desktop)
            {
                RecordDesktopAudio();
            }
            else if (selectedOption == StringsEN.Microphone)
            {
                RecordMicrophone();
            }
        }


        private void CheckFfmpegProcces()
        {
            var ffmpegProcesses = Process.GetProcessesByName("ffmpeg");

            foreach (var process in ffmpegProcesses)
            {
                try
                {
                    if (process.MainModule.FileName.Contains(ResourcePath))
                    {
                        process.Kill();
                        process.WaitForExit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to stop ffmpeg process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void StopRecordingProcess()
        {
            EnableElementsUI();
            CheckAudioStop();
            CheckFfmpegProcces();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                LbTimer.ForeColor = Color.White;
                LbTimer.Text = "00:00:00";
                CountRecVideo.Enabled = false;
                StopRecordingProcess();

            }
            catch (Exception)
            {
                return;
            }

        }

        private void RecordTwoTracks()
        {
            if (WaveIn.DeviceCount == 0)
            {
                MessageBox.Show(StringsEN.message3, "Error");
                return;
            }

            RecMic();
            RecSpeaker();
        }

        private void RecordDesktopAudio()
        {
            RecSpeaker();
        }

        private void RecordMicrophone()
        {
            if (WaveIn.DeviceCount == 0)
            {
                MessageBox.Show(StringsEN.message3, "Error");
                return;
            }

            RecMic();
        }

        private void CheckAudioStop()
        {
            string selectedOption = comboBoxAudioSource.SelectedItem.ToString();

            if (selectedOption == StringsEN.TwoTrack)
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
            else if (selectedOption == StringsEN.Desktop)
            {
                if (ScreenAudioDesktop.waveIn is object)
                {
                    ScreenAudioDesktop.waveIn.StopRecording();
                }
            }
            else if (selectedOption == StringsEN.Microphone)
            {
                if (ScreenAudioMic.waveIn is object)
                {
                    ScreenAudioMic.waveIn.StopRecording();
                }
            }

            var soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.Stop();
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

            var soundPlayer = new System.Media.SoundPlayer(Resources.Background);
            soundPlayer.PlayLooping();

            ScreenAudioDesktop.outputFilename = "SystemAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            ScreenAudioDesktop.writer = new WaveFileWriter(Path.Combine(ScreenAudioDesktop.outputFolder, ScreenAudioDesktop.outputFilename), ScreenAudioDesktop.waveIn.WaveFormat);
            ScreenAudioDesktop.waveIn.StartRecording();
        }

        #region Some shit about UI
        private void EnableElementsUI()
        {
            btnStartRecording.Enabled = true;
            comboBoxCodec.Enabled = true;
            ComboBoxMicrophone.Enabled = true;
            ComboBoxSpeaker.Enabled = true;
            comboBoxFps.Enabled = true;
            CheckBoxAllMonitors.Enabled = true;
            ComboBoxFormat.Enabled = true;
            RefreshMonitors.Enabled = true;
            menuStrip1.Enabled = true;
            comboBoxBitrate.Enabled = true;
            comboBoxAudioSource.Enabled = true;

            comboBoxMonitors.Enabled = !CheckBoxAllMonitors.Checked;
        }

        private void DisableElementsUI()
        {
            comboBoxMonitors.Enabled = false;
            btnStartRecording.Enabled = false;
            ComboBoxMicrophone.Enabled = false;
            ComboBoxSpeaker.Enabled = false;
            comboBoxCodec.Enabled = false;
            comboBoxFps.Enabled = false;
            CheckBoxAllMonitors.Enabled = false;
            ComboBoxFormat.Enabled = false;
            RefreshMonitors.Enabled = false;
            menuStrip1.Enabled = false;
            comboBoxBitrate.Enabled = false;
            comboBoxAudioSource.Enabled = false;
        }
        #endregion

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

        #region Translations's code

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

            remuxToolStripMenuItem.Text = StringsEN.remuxToolStripMenuItem;
            btnOutputRecordings.Text = StringsEN.btnOutputRecordings;
            labelCodec.Text = StringsEN.labelCodec;
            crownGroupBox1.Text = StringsEN.crownGroupBox1;
            crownGroupBox2.Text = StringsEN.crownGroupBox2;
            crownGroupBox3.Text = StringsEN.crownGroupBox3;
            audioToolStripMenuItem.Text = StringsEN.audioToolStripMenuItem;

            labelFps.Text = StringsEN.labelFps;
            CheckBoxAllMonitors.Text = StringsEN.CheckBoxAllMonitors;
            labelFormat.Text = StringsEN.labelFormat;
            labelMonitorSelector.Text = StringsEN.labelMonitorSelector;
            btnMergedFiles.Text = StringsEN.btnMergedFiles;

            int selectedIndex = comboBoxAudioSource.SelectedIndex;
            comboBoxAudioSource.Items.Clear();
            comboBoxAudioSource.Items.Add(StringsEN.TwoTrack);
            comboBoxAudioSource.Items.Add(StringsEN.Desktop);
            comboBoxAudioSource.Items.Add(StringsEN.Microphone);
            comboBoxAudioSource.SelectedIndex = selectedIndex;

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

        private void LoadUserSettingsCombobox()
        {
            comboBoxCodec.Text = Settings.Default.SelectedCodec;
            ComboBoxFormat.Text = Settings.Default.SelectedFormat;
            comboBoxFps.Text = Settings.Default.SelectedFramerate;
            comboBoxBitrate.Text = Settings.Default.SelectedBitrate;
            comboBoxAudioSource.SelectedIndex = Settings.Default.AudioSourceIndex;
        }

        private void SaveUserSettingsComboboxRec()
        {
            Settings.Default.SelectedCodec = comboBoxCodec.Text;
            Settings.Default.SelectedFormat = ComboBoxFormat.Text;
            Settings.Default.SelectedFramerate = comboBoxFps.Text;
            Settings.Default.SelectedBitrate = comboBoxBitrate.Text;
            Settings.Default.AudioSourceIndex = comboBoxAudioSource.SelectedIndex;
        }

        #endregion

        private void RecorderScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveUserSettingsComboboxRec();
            Settings.Default.Save();
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

        private void SetKeyPreview()
        {
            this.KeyPreview = true;
        }
    }
}