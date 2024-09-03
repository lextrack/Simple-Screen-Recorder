using Microsoft.VisualBasic;
using NAudio.Wave;
using Simple_Screen_Recorder.AudioComp;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.ScreenRecorderWin;
using Simple_Screen_Recorder.UI;
using System.ComponentModel;
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
        public static string ResourcePath = Path.Combine(Directory.GetCurrentDirectory(), @"FFmpegResources\ffmpeg");
        private Rectangle? SelectedCustomArea = null;
        private bool IsCustomAreaSelected = false;

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
            ButtonCustomArea.Enabled = !CheckBoxAllMonitors.Checked;
            if (CheckBoxAllMonitors.Checked)
            {
                comboBoxMonitors.SelectedIndex = 0;
                ResetCustomAreaSelection();
            }

        }

        private void ButtonCustomArea_Click(object sender, EventArgs e)
        {
            using (var areaSelector = new AreaSelector())
            {
                if (areaSelector.ShowDialog() == DialogResult.OK)
                {
                    SelectedCustomArea = areaSelector.SelectedArea;
                    IsCustomAreaSelected = true;

                    comboBoxMonitors.Enabled = false;
                    CheckBoxAllMonitors.Checked = false;
                    CheckBoxAllMonitors.Enabled = false;
                    comboBoxMonitors.Text = $"Custom Area ({SelectedCustomArea.Value.Width}x{SelectedCustomArea.Value.Height})";
                }
                else
                {
                    ResetCustomAreaSelection();
                }
            }
        }

        private void ResetCustomAreaSelection()
        {
            SelectedCustomArea = null;
            IsCustomAreaSelected = false;
            comboBoxMonitors.Enabled = !CheckBoxAllMonitors.Checked;
            CheckBoxAllMonitors.Enabled = true;
            ButtonCustomArea.Enabled = !CheckBoxAllMonitors.Checked;
            if (!CheckBoxAllMonitors.Checked)
            {
                comboBoxMonitors.SelectedIndex = 0;
            }
        }

        private async void btnStartRecording_Click(object sender, EventArgs e)
        {
            var format = ComboBoxFormat.SelectedItem.ToString();
            VideoName = $"Video.{DateTime.Now.ToString(DateFormat)}.{format.TrimStart('.')}";

            string codecArgs;
            if (CheckBoxAllMonitors.Checked)
            {
                codecArgs = "-i desktop";
            }
            else if (IsCustomAreaSelected && SelectedCustomArea.HasValue)
            {
                Rectangle area = SelectedCustomArea.Value;
                codecArgs = $"-video_size {area.Width}x{area.Height} -offset_x {area.Left} -offset_y {area.Top} -i desktop";
            }
            else
            {
                int selectedIndex = comboBoxMonitors.SelectedIndex;
                Screen selectedScreen = Screen.AllScreens[selectedIndex];
                Rectangle bounds = selectedScreen.Bounds;
                codecArgs = $"-offset_x {bounds.Left} -offset_y {bounds.Top} -video_size {bounds.Width}x{bounds.Height} -i desktop";
            }

            string codec = DetermineCodec();

            DateTime startTimestamp = DateTime.Now.AddMilliseconds(500);

            TimeRec = DateTime.Now;
            LbTimer.ForeColor = Color.IndianRed;
            CountRecVideo.Enabled = true;

            Task audioTask = RecordAudio(startTimestamp);
            Task videoTask = Task.Run(() => StartRecordingProcess(codec, int.Parse((string)comboBoxFps.SelectedItem), (string)comboBoxBitrate.SelectedItem, codecArgs, startTimestamp));

            await Task.WhenAll(audioTask, videoTask);

            DisableElementsUI();
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
                string ffmpegArgs = $"{ResourcePath} -f gdigrab -framerate {fps} {screenArgs} -c:v {codec} -b:v {bitrate} -pix_fmt yuv420p -fps_mode cfr -loglevel info -hide_banner Recordings/{VideoName}";

                ProcessStartInfo processInfo = new("cmd.exe", $"/c {ffmpegArgs}")
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string DetermineCodec()
        {
            return comboBoxCodec.SelectedItem.ToString() switch
            {
                "H264 (Default)" => "h264_mf",
                "MPEG-4" => "mpeg4 -preset medium",
                "H264 NVENC (Nvidia)" => "h264_nvenc",
                "H264 AMF (AMD)" => "h264_amf",
                _ => "h264_mf", //Default
            };
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
            Cursor.Current = Cursors.WaitCursor;

            var ffmpegProcesses = Process.GetProcessesByName("ffmpeg");

            foreach (var process in ffmpegProcesses)
            {
                try
                {
                    if (!process.HasExited)
                    {
                        if (process.MainModule != null && process.MainModule.FileName.Contains(ResourcePath))
                        {
                            process.CloseMainWindow();

                            if (!process.WaitForExit(3000))
                            {
                                process.Kill();
                                process.WaitForExit();
                            }
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show($"The process has been completed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Win32Exception ex)
                {
                    MessageBox.Show($"Error while trying to access the ffmpeg process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to stop ffmpeg process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Cursor.Current = Cursors.Default;
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
                ResetCustomAreaSelection();

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

        #region Enable/disableUI
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
            ButtonCustomArea.Enabled = !CheckBoxAllMonitors.Checked;
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
            ButtonCustomArea.Enabled = false;
        }
        #endregion

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
            ButtonCustomArea.Text = StringsEN.ButtonCustomArea;

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