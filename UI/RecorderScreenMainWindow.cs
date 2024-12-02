using Microsoft.Win32;
using NAudio.CoreAudioApi;
using Serilog;
using Simple_Screen_Recorder.AudioComp;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.ScreenRecorderWin;
using Simple_Screen_Recorder.UI;
using Simple_Screen_Recorder.Utils;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
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
        private AudioManager? microphoneManager;
        private AudioManager? systemAudioManager;
        private readonly ILogger _logger = LoggerConfig.Logger;
        private const string RECORDING_STATE_FILE = "recording_state.json";
        private bool isRecording = false;
        private Process? ffmpegProcess;

        public RecorderScreenMainWindow()
        {
            InitializeComponent();
        }

        private void InitializeForm()
        {
            var startTime = DateTime.Now;
            try
            {
                _logger.LogOperationStart("InitializeForm");

                CheckForInterruptedRecording();

                GetTextsMain();
                CheckMonitors();
                InitializeAudioComponents();
                InitializeComboBoxes();
                CreateOutputFolder();
                SetKeyPreview();
                LoadUserSettingsCombobox();

                SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
                Application.ApplicationExit += Application_ApplicationExit;

                _logger.LogOperationEnd("InitializeForm", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error initializing form");
                throw;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void CreateOutputFolder()
        {
            try
            {
                string outputFolderPath = Path.Combine(Application.StartupPath, "OutputFiles");
                string recordingsFolderPath = Path.Combine(Application.StartupPath, "Recordings");

                if (!Directory.Exists(outputFolderPath))
                {
                    Directory.CreateDirectory(outputFolderPath);
                    _logger.Information("Created OutputFiles directory: {Path}", outputFolderPath);
                }

                if (!Directory.Exists(recordingsFolderPath))
                {
                    Directory.CreateDirectory(recordingsFolderPath);
                    _logger.Information("Created Recordings directory: {Path}", recordingsFolderPath);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating output directories");
                MessageBox.Show("Error creating necessary folders. Please make sure the application has write permissions.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeAudioComponents()
        {
            try
            {
                var inputDevices = AudioManager.GetAudioDevices(DataFlow.Capture);
                ComboBoxMicrophone.DataSource = inputDevices;
                ComboBoxMicrophone.DisplayMember = "FriendlyName";

                var outputDevices = AudioManager.GetAudioDevices(DataFlow.Render);
                ComboBoxSpeaker.DataSource = outputDevices;
                ComboBoxSpeaker.DisplayMember = "FriendlyName";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing audio devices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComboBoxes()
        {
            comboBoxCodec.Items.AddRange(new[] { "H264 (Default)", "MPEG-4", "H264 NVENC (Nvidia)", "H264 AMF (AMD)" });
            comboBoxCodec.SelectedIndex = 0;

            comboBoxFps.Items.AddRange(new[] { "30", "60" });
            comboBoxFps.SelectedIndex = 0;

            comboBoxBitrate.Items.AddRange(new[] { "2000k", "4000k", "6000k", "8000k", "10000k", "15000k", "20000k" });
            comboBoxBitrate.SelectedIndex = 1;


            ComboBoxFormat.Items.AddRange(new[] { ".mkv" });
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
            try
            {
                var startTime = DateTime.Now;
                _logger.LogOperationStart("StartRecording");

                var format = ComboBoxFormat.SelectedItem.ToString();
                VideoName = $"Video.{DateTime.Now.ToString(DateFormat)}.{format.TrimStart('.')}";
                _logger.Information("Video file name: {VideoName}", VideoName);

                isRecording = true;
                SaveRecordingState();

                string codecArgs;
                if (CheckBoxAllMonitors.Checked)
                {
                    _logger.Information("Recording mode: All monitors");
                    codecArgs = "-i desktop";
                }
                else if (IsCustomAreaSelected && SelectedCustomArea.HasValue)
                {
                    Rectangle area = SelectedCustomArea.Value;
                    _logger.Information("Recording mode: Custom area {Width}x{Height} in position ({X},{Y})",
                        area.Width, area.Height, area.X, area.Y);
                    codecArgs = $"-video_size {area.Width}x{area.Height} -offset_x {area.Left} -offset_y {area.Top} -i desktop";
                }
                else
                {
                    int selectedIndex = comboBoxMonitors.SelectedIndex;
                    Screen selectedScreen = Screen.AllScreens[selectedIndex];
                    Rectangle bounds = selectedScreen.Bounds;
                    _logger.Information("Recording mode: Monitor {Index}({Width}x{Height})",
                        selectedIndex, bounds.Width, bounds.Height);
                    codecArgs = $"-offset_x {bounds.Left} -offset_y {bounds.Top} -video_size {bounds.Width}x{bounds.Height} -i desktop";
                }

                string codec = DetermineCodec();
                _logger.LogRecordingSettings(
                    codec,
                    int.Parse((string)comboBoxFps.SelectedItem),
                    (string)comboBoxBitrate.SelectedItem,
                    format
                );

                DateTime startTimestamp = DateTime.Now.AddMilliseconds(500);
                _logger.Information("Programmed start time: {StartTime}", startTimestamp);

                TimeRec = DateTime.Now;
                LbTimer.ForeColor = Color.IndianRed;
                CountRecVideo.Enabled = true;

                _logger.Information("Starting audio and video recording tasks");
                Task audioTask = RecordAudio(startTimestamp);
                Task videoTask = Task.Run(() => StartRecordingProcess(
                    codec,
                    int.Parse((string)comboBoxFps.SelectedItem),
                    (string)comboBoxBitrate.SelectedItem,
                    codecArgs,
                    startTimestamp));

                await Task.WhenAll(audioTask, videoTask);
                _logger.Information("Recording tasks started correctly");

                DisableElementsUI();
                _logger.LogOperationEnd("StartRecording", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when starting recording");
                MessageBox.Show($"Error when starting recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                isRecording = false;
                _logger.Error(ex, "Error when starting recording");

                try
                {
                    EnableElementsUI();
                    LbTimer.ForeColor = Color.White;
                    CountRecVideo.Enabled = false;
                }
                catch (Exception uiEx)
                {
                    _logger.Error(uiEx, "Error restoring the UI after a save error");
                }
            }
        }

        private void SaveRecordingState()
        {
            try
            {
                var state = new RecordingState
                {
                    VideoName = VideoName,
                    StartTime = TimeRec,
                    WasRecording = isRecording
                };

                string statePath = Path.Combine(Application.StartupPath, RECORDING_STATE_FILE);
                string jsonState = JsonSerializer.Serialize(state);
                File.WriteAllText(statePath, jsonState);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error saving recording state");
            }
        }

        private void CheckForInterruptedRecording()
        {
            try
            {
                string statePath = Path.Combine(Application.StartupPath, RECORDING_STATE_FILE);
                if (File.Exists(statePath))
                {
                    var state = JsonSerializer.Deserialize<RecordingState>(File.ReadAllText(statePath));
                    if (state.WasRecording)
                    {
                        _logger.Warning("Detected interrupted recording: {VideoName}", state.VideoName);

                        var result = MessageBox.Show(
                            "An interrupted recording was detected, do you want to try to recover the files?",
                            "Interrupted Recording",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            RecoverInterruptedRecording(state);
                        }
                    }

                    // Limpiar el archivo de estado
                    File.Delete(statePath);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error checking for interrupted recording");
            }
        }

        private class RecordingState
        {
            public string VideoName { get; set; }
            public DateTime StartTime { get; set; }
            public bool WasRecording { get; set; }
        }


        private void RecoverInterruptedRecording(RecordingState state)
        {
            try
            {
                string recordingsPath = Path.Combine(Application.StartupPath, "Recordings");
                string[] relevantFiles = Directory.GetFiles(recordingsPath,
                    $"*{state.StartTime.ToString(DateFormat)}*");

                if (relevantFiles.Length == 0)
                {
                    _logger.Information("No files found that need recovery");
                    MessageBox.Show(
                        "The recording files were successfully closed and do not need to be recovered.",
                        "Recovery",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // Solo crear la carpeta Interrupted si hay archivos .temp para mover
                bool hasCorruptedFiles = relevantFiles.Any(f => f.EndsWith(".temp") ||
                    (Path.GetExtension(f) == ".mkv" && !IsValidVideoFile(f)));

                if (!hasCorruptedFiles)
                {
                    _logger.Information("Archivos encontrados pero parecen ser válidos");
                    MessageBox.Show(
                        "The recording files appear to be in good condition and do not require recovery.",
                        "Recovery",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                string corruptedPath = Path.Combine(recordingsPath, "Interrupted");
                Directory.CreateDirectory(corruptedPath);

                foreach (string file in relevantFiles)
                {
                    try
                    {
                        string fileName = Path.GetFileName(file);
                        if (file.Contains("Interrupted")) continue;

                        // Si es un archivo .temp, intentar primero repararlo
                        if (file.EndsWith(".temp"))
                        {
                            try
                            {
                                string finalPath = file.Replace(".temp", ".wav");
                                File.Move(file, finalPath);
                                _logger.Information("Successfully repaired audio file: {File}", finalPath);
                                continue;
                            }
                            catch (Exception ex)
                            {
                                _logger.Error(ex, "Could not repair audio file, moving to Interrupted: {File}", file);
                            }
                        }

                        string destinationPath = Path.Combine(corruptedPath, fileName);
                        if (File.Exists(destinationPath))
                        {
                            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                            string extension = Path.GetExtension(fileName);
                            int counter = 1;
                            while (File.Exists(destinationPath))
                            {
                                fileName = $"{fileNameWithoutExt}_{counter}{extension}";
                                destinationPath = Path.Combine(corruptedPath, fileName);
                                counter++;
                            }
                        }

                        File.Move(file, destinationPath);
                        _logger.Information("Moved corrupted file to interrupted folder: {FileName}", fileName);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error processing file: {File}", file);
                    }
                }

                MessageBox.Show(
                    "The recording files have been processed.\n" +
                    "- Audio files attempted to be repaired.\n" +
                    "- The corrupted files have been moved to the folder 'Interrupted'.",
                    "Recovery Completed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error recovering interrupted recording");
                MessageBox.Show(
                    "Error when trying to recover files.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool IsValidVideoFile(string filePath)
        {
            try
            {
                using var fs = File.OpenRead(filePath);
                byte[] header = new byte[4];
                if (fs.Read(header, 0, header.Length) < header.Length)
                    return false;

                if (header[0] == 0x1A && header[1] == 0x45 &&
                    header[2] == 0xDF && header[3] == 0xA3)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend && isRecording)
            {
                _logger.Warning("System entering suspend mode while recording");
                try
                {
                    if (LbTimer.InvokeRequired)
                    {
                        LbTimer.Invoke(new Action(() =>
                        {
                            CountRecVideo.Enabled = false;
                            LbTimer.Text = "00:00:00";
                            LbTimer.ForeColor = Color.White;
                        }));
                    }
                    else
                    {
                        CountRecVideo.Enabled = false;
                        LbTimer.Text = "00:00:00";
                        LbTimer.ForeColor = Color.White;
                    }

                    TimeRec = DateTime.MinValue;

                    StopRecordingProcess();
                    isRecording = false;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error handling system suspend");
                }
            }
            else if (e.Mode == PowerModes.Resume)
            {
                _logger.Information("System resuming from suspend mode");
            }
        }


        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (isRecording)
            {
                _logger.Warning("Application exiting while recording");
                StopRecordingProcess();
            }

            SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;
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
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                ffmpegProcess = Process.Start(processInfo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to start FFmpeg process");
                MessageBox.Show($"Failed to start recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnFfmpegExited(object? sender, EventArgs e)
        {
            try
            {
                _logger.Information("FFmpeg process exited naturally");

                if (microphoneManager != null || systemAudioManager != null)
                {
                    CheckAudioStop();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error handling FFmpeg exit");
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
            try
            {
                TimeSpan delay = startTimestamp - DateTime.Now;
                if (delay > TimeSpan.Zero)
                {
                    await Task.Delay(delay);
                }

                string outputPath = Path.Combine(Application.StartupPath, "Recordings");
                string selectedOption = comboBoxAudioSource.SelectedItem.ToString();

                if (selectedOption == StringsEN.TwoTrack)
                {
                    StartTwoTracksRecording(outputPath);
                }
                else if (selectedOption == StringsEN.Desktop)
                {
                    StartDesktopRecording(outputPath);
                }
                else if (selectedOption == StringsEN.Microphone)
                {
                    StartMicrophoneRecording(outputPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting audio recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartTwoTracksRecording(string outputPath)
        {
            var micDevice = (MMDevice)ComboBoxMicrophone.SelectedItem;
            var speakerDevice = (MMDevice)ComboBoxSpeaker.SelectedItem;

            microphoneManager = new AudioManager(outputPath);
            systemAudioManager = new AudioManager(outputPath);

            microphoneManager.StartMicrophoneRecording(micDevice);
            systemAudioManager.StartSystemAudioRecording(speakerDevice);
        }

        private void StartDesktopRecording(string outputPath)
        {
            var speakerDevice = (MMDevice)ComboBoxSpeaker.SelectedItem;
            systemAudioManager = new AudioManager(outputPath);
            systemAudioManager.StartSystemAudioRecording(speakerDevice);
        }

        private void StartMicrophoneRecording(string outputPath)
        {
            var micDevice = (MMDevice)ComboBoxMicrophone.SelectedItem;
            microphoneManager = new AudioManager(outputPath);
            microphoneManager.StartMicrophoneRecording(micDevice);
        }


        private void CheckFfmpegProcces()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("CheckFfmpegProcess");
            try
            {
                if (ffmpegProcess != null)
                {
                    _logger.Information("FFmpeg process found: {ProcessId}", ffmpegProcess.Id);

                    try
                    {
                        var taskKillProcess = Process.Start(new ProcessStartInfo
                        {
                            FileName = "taskkill",
                            Arguments = $"/PID {ffmpegProcess.Id}",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        });
                        taskKillProcess?.WaitForExit(1000);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warning(ex, "Taskkill attempt failed for FFmpeg process");
                    }

                    if (!ffmpegProcess.HasExited)
                    {
                        _logger.Warning("FFmpeg process still running after taskkill, forcing termination");
                        try
                        {
                            ffmpegProcess.Kill(true);
                            ffmpegProcess.WaitForExit(1000);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "Error force-killing FFmpeg process");
                        }
                    }

                    _logger.Information("FFmpeg process terminated");
                    ffmpegProcess = null;
                }
                else
                {
                    foreach (var process in Process.GetProcessesByName("ffmpeg"))
                    {
                        try
                        {
                            _logger.Warning("Found orphaned FFmpeg process: {ProcessId}", process.Id);
                            process.Kill(true);
                            process.WaitForExit(1000);
                            _logger.Information("Terminated orphaned FFmpeg process: {ProcessId}", process.Id);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "Error terminating orphaned FFmpeg process");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in CheckFfmpegProcess");
            }
            finally
            {
                _logger.LogOperationEnd("CheckFfmpegProcess", DateTime.Now - startTime);
            }
        }

        private async Task StopRecordingProcess()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("StopRecordingProcess");

            try
            {
                CheckFfmpegProcces();
                await Task.Delay(500);

                CheckAudioStop();
                await Task.Delay(500);

                EnableElementsUI();

                _logger.Information("Recording process stopped successfully");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when stopping the recording process");
            }
            finally
            {
                _logger.LogOperationEnd("StopRecordingProcess", DateTime.Now - startTime);
            }
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

        private void CheckAudioStop()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("CheckAudioStop");
            try
            {
                if (microphoneManager != null)
                {
                    _logger.Information("Stopping microphone recording");
                    microphoneManager.StopRecording();
                    microphoneManager.Dispose();
                    microphoneManager = null;
                }
                if (systemAudioManager != null)
                {
                    _logger.Information("Stopping system audio recording");
                    systemAudioManager.StopRecording();
                    systemAudioManager.Dispose();
                    systemAudioManager = null;
                }
                _logger.Information("Audio recording successfully stopped");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error stopping audio recording");
                MessageBox.Show($"Error stopping audio recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _logger.LogOperationEnd("CheckAudioStop", DateTime.Now - startTime);
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