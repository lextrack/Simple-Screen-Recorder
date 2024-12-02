using NAudio.CoreAudioApi;
using Serilog;
using Simple_Screen_Recorder.AudioComp;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.Utils;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.UI
{
    public partial class AudioRecorderMainWindow : Form
    {
        private DateTime TimeRec = new DateTime();
        private string AudioName = "";
        private AudioManager? microphoneManager;
        private AudioManager? systemAudioManager;
        private readonly ILogger _logger = LoggerConfig.Logger;

        public AudioRecorderMainWindow()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("AudioRecorderMainWindow.Initialize");
            try
            {
                InitializeComponent();
                _logger.LogOperationEnd("AudioRecorderMainWindow.Initialize", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when initializing AudioRecorderMainWindow");
                throw;
            }
        }

        private void AudioRecorderMainWindow_Load(object sender, EventArgs e)
        {
            GetTextsMain();
            InitializeAudioComponents();
            LoadUserSettingsCombobox();
        }

        private void InitializeAudioComponents()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("InitializeAudioComponents");
            try
            {
                var inputDevices = AudioManager.GetAudioDevices(DataFlow.Capture);
                ComboBoxMicrophone.DataSource = inputDevices;
                ComboBoxMicrophone.DisplayMember = "FriendlyName";
                _logger.Information("Input devices loaded: {Count}", inputDevices.Count);

                var outputDevices = AudioManager.GetAudioDevices(DataFlow.Render);
                ComboBoxSpeaker.DataSource = outputDevices;
                ComboBoxSpeaker.DisplayMember = "FriendlyName";
                _logger.Information("Loaded output devices: {Count}", outputDevices.Count);

                _logger.LogOperationEnd("InitializeAudioComponents", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error initializing audio devices");
                MessageBox.Show($"Error initializing audio devices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("StartAudioRecording");
            try
            {
                LbTimer.ForeColor = Color.IndianRed;
                TimeRec = DateTime.Now;
                CountRecAudio.Enabled = true;
                AudioName = "Audio." + DateTime.Now.ToString("MM-dd-yyyy.HH.mm.ss");
                _logger.Information("Starting audio recording: {FileName}", AudioName);

                StartAudioRecording();
                DisableElementsUI();
                _logger.LogOperationEnd("StartAudioRecording", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when starting audio recording");
                MessageBox.Show($"Error starting recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableElementsUI();
            }
        }

        private void StartAudioRecording()
        {
            string outputPath = Path.Combine(Application.StartupPath, "AudioRecordings");
            Directory.CreateDirectory(outputPath);
            _logger.Information("Output directory: {Path}", outputPath);

            string selectedOption = comboBoxAudioSourceAudio.SelectedItem.ToString();
            _logger.Information("Selected recording mode: {Mode}", selectedOption);

            try
            {
                if (selectedOption == StringsEN.TwoTrackAudio)
                {
                    var micDevice = (MMDevice)ComboBoxMicrophone.SelectedItem;
                    var speakerDevice = (MMDevice)ComboBoxSpeaker.SelectedItem;
                    _logger.Information("Starting two-track recording - Microphone: {Mic}, System Audio: {Speaker}",
                        micDevice.FriendlyName, speakerDevice.FriendlyName);

                    microphoneManager = new AudioManager(outputPath);
                    systemAudioManager = new AudioManager(outputPath);

                    microphoneManager.StartMicrophoneRecording(micDevice);
                    systemAudioManager.StartSystemAudioRecording(speakerDevice);
                }
                else if (selectedOption == StringsEN.DesktopAudio)
                {
                    var speakerDevice = (MMDevice)ComboBoxSpeaker.SelectedItem;
                    _logger.Information("Starting system audio recording: {Device}", speakerDevice.FriendlyName);

                    systemAudioManager = new AudioManager(outputPath);
                    systemAudioManager.StartSystemAudioRecording(speakerDevice);
                }
                else if (selectedOption == StringsEN.MicrophoneAudio)
                {
                    var micDevice = (MMDevice)ComboBoxMicrophone.SelectedItem;
                    _logger.Information("Starting microphone recording: {Device}", micDevice.FriendlyName);

                    microphoneManager = new AudioManager(outputPath);
                    microphoneManager.StartMicrophoneRecording(micDevice);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when starting audio recording");
                throw;
            }
        }

        private void StopAudioRecording()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("StopAudioRecording");
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
                _logger.LogOperationEnd("StopAudioRecording", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error stopping audio recording");
                MessageBox.Show($"Error stopping recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("StopRecording");
            try
            {
                LbTimer.ForeColor = Color.White;
                LbTimer.Text = "00:00:00";
                CountRecAudio.Enabled = false;
                StopAudioRecording();
                EnableElementsUI();
                _logger.LogOperationEnd("StopRecording", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error when stopping recording");
                MessageBox.Show($"Error stopping recording: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableElementsUI()
        {
            btnStartRecording.Enabled = false;
            ComboBoxMicrophone.Enabled = false;
            ComboBoxSpeaker.Enabled = false;
            BtnBackScreen.Enabled = false;
            comboBoxAudioSourceAudio.Enabled = false;
        }

        private void EnableElementsUI()
        {
            btnStartRecording.Enabled = true;
            ComboBoxMicrophone.Enabled = true;
            ComboBoxSpeaker.Enabled = true;
            BtnBackScreen.Enabled = true;
            comboBoxAudioSourceAudio.Enabled = true;
        }

        private void btnOutputRecordings_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "AudioRecordings");
        }

        private void CountRecAudio_Tick(object sender, EventArgs e)
        {
            var Difference = DateTime.Now.Subtract(TimeRec);
            LbTimer.Text = "Rec: " + Difference.Hours.ToString().PadLeft(2, '0') + ":" +
                           Difference.Minutes.ToString().PadLeft(2, '0') + ":" +
                           Difference.Seconds.ToString().PadLeft(2, '0');
        }

        private void GetTextsMain()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Settings.Default.Languages);

            btnStartRecording.Text = StringsEN.btnStartRecording;
            BtnStop.Text = StringsEN.BtnStop;
            Label4.Text = StringsEN.Label4;
            Label5.Text = StringsEN.Label5;
            label6.Text = StringsEN.Label6;
            label7.Text = StringsEN.Label7;
            btnOutputRecordings.Text = StringsEN.btnOutputRecordings;
            crownGroupBox2.Text = StringsEN.crownGroupBox2;
            crownGroupBox3.Text = StringsEN.crownGroupBox3;
            BtnBackScreen.Text = StringsEN.BtnBackScreen;

            int selectedIndex = comboBoxAudioSourceAudio.SelectedIndex;
            comboBoxAudioSourceAudio.Items.Clear();
            comboBoxAudioSourceAudio.Items.Add(StringsEN.TwoTrackAudio);
            comboBoxAudioSourceAudio.Items.Add(StringsEN.DesktopAudio);
            comboBoxAudioSourceAudio.Items.Add(StringsEN.MicrophoneAudio);
            comboBoxAudioSourceAudio.SelectedIndex = selectedIndex;
        }

        private void LoadUserSettingsCombobox()
        {
            comboBoxAudioSourceAudio.SelectedIndex = Settings.Default.AudioRecordingSourceIndex;
        }

        private void SaveUserSettingsComboboxAudio()
        {
            Settings.Default.AudioRecordingSourceIndex = comboBoxAudioSourceAudio.SelectedIndex;
        }

        private void AudioRecorderMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveUserSettingsComboboxAudio();
            Settings.Default.Save();
        }

        private void BtnBackScreen_Click(object sender, EventArgs e)
        {
            if (btnStartRecording.Enabled == false)
            {
                MessageBox.Show(StringsEN.message2, "Error");
            }
            else
            {
                this.Close();
                RecorderScreenMainWindow backscreenrec = new RecorderScreenMainWindow();
                backscreenrec.Show();
            }
        }
    }
}
