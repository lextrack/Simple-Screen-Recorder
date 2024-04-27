using Microsoft.VisualBasic;
using NAudio.Wave;
using Simple_Screen_Recorder.AudioComp;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.UI
{
    public partial class AudioRecorderMainWindow : Form
    {
        private DateTime TimeRec = new DateTime();
        private string AudioName = "";
        public int ProcessId { get; private set; }

        public AudioRecorderMainWindow()
        {
            InitializeComponent();
        }

        private void AudioRecorderMainWindow_Load(object sender, EventArgs e)
        {
            GetTextsMain();
            OpenAudioComponents();
            LoadUserSettingsCombobox();
        }

        private void OpenAudioComponents()
        {
            AudioRecorderMic.OpenComp();
            ComboBoxMicrophone.DataSource = AudioRecorderMic.cboDIspositivos.DataSource;
            AudioRecorderDesktop.OpenComp();
            ComboBoxSpeaker.DataSource = AudioRecorderDesktop.cboDIspositivos.DataSource;
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            LbTimer.ForeColor = Color.IndianRed;
            TimeRec = DateTime.Now;
            CountRecAudio.Enabled = true;
            AudioName = "Audio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss");

            RecordAudio();
            DisableElementsUI();
            RecordFfmpegInitial();

        }

        private void RecordFfmpegInitial()
        {
            ProcessStartInfo ProcessId = new("cmd.exe", $"/c {RecorderScreenMainWindow.ResourcePath} -f gdigrab AudioRecordings/" + AudioName + "");
            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessId.CreateNoWindow = true;
            ProcessId.RedirectStandardOutput = true;
            Process.Start(ProcessId);
        }

        private void DisableElementsUI()
        {
            btnStartRecording.Enabled = false;
            ComboBoxMicrophone.Enabled = false;
            ComboBoxSpeaker.Enabled = false;
            BtnBackScreen.Enabled = false;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                LbTimer.ForeColor = Color.White;
                LbTimer.Text = "00:00:00";
                CountRecAudio.Enabled = false;
                StopAudioRec();

            }
            catch (Exception)
            {
                return;
            }
        }

        private void StopAudioRec()
        {
            btnStartRecording.Enabled = true;
            ComboBoxMicrophone.Enabled = true;
            ComboBoxSpeaker.Enabled = true;
            BtnBackScreen.Enabled = true;

            CheckAudioStop();
            CheckFfmpegProcces();
        }

        private void RecordAudio()
        {
            string selectedOption = comboBoxAudioSourceAudio.SelectedItem.ToString();

            if (selectedOption == StringsEN.TwoTrackAudio)
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
            else if (selectedOption == StringsEN.DesktopAudio)
            {
                RecSpeaker();
            }
            else if (selectedOption == StringsEN.MicrophoneAudio)
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


        private void CheckAudioStop()
        {
            string selectedOption = comboBoxAudioSourceAudio.SelectedItem.ToString();

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
            AudioRecorderMic.Cleanup();
            AudioRecorderMic.CreateWaveInDevice();
            AudioRecorderMic.outputFilename = "MicrophoneAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            AudioRecorderMic.writer = new WaveFileWriter(Path.Combine(AudioRecorderMic.outputFolder, AudioRecorderMic.outputFilename), AudioRecorderMic.waveIn.WaveFormat);
            AudioRecorderMic.waveIn.StartRecording();
        }

        private static void RecSpeaker()
        {
            AudioRecorderDesktop.Cleanup();
            AudioRecorderDesktop.CreateWaveInDevice();

            var soundPlayer = new System.Media.SoundPlayer(Properties.Resources.Background);
            soundPlayer.PlayLooping();

            AudioRecorderDesktop.outputFilename = "SystemAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".wav";
            AudioRecorderDesktop.writer = new WaveFileWriter(Path.Combine(AudioRecorderDesktop.outputFolder, AudioRecorderDesktop.outputFilename), AudioRecorderDesktop.waveIn.WaveFormat);
            AudioRecorderDesktop.waveIn.StartRecording();
        }

        private void btnOutputRecordings_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "AudioRecordings");
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
            this.Close();
            RecorderScreenMainWindow backscreenrec = new RecorderScreenMainWindow();
            backscreenrec.Show();
        }

        private void CountRecAudio_Tick(object sender, EventArgs e)
        {
            var Difference = DateTime.Now.Subtract(TimeRec);
            LbTimer.Text = "Rec: " + Difference.Hours.ToString().PadLeft(2, '0') + ":" + Difference.Minutes.ToString().PadLeft(2, '0') + ":" + Difference.Seconds.ToString().PadLeft(2, '0');
        }
    }
}
