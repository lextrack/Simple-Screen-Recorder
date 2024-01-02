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
            ConfigureAudioComponents();
        }

        private void ConfigureAudioComponents()
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

            if (RadioTwoTrack.Checked == true)
            {
                if (WaveIn.DeviceCount > 0)
                {
                    RecMic();
                }
                else
                {
                    System.Windows.MessageBox.Show(StringsEN.message3, "Error");
                }

                RecSpeaker();
            }
            else if (RadioDesktop.Checked == true)
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
                    System.Windows.MessageBox.Show(StringsEN.message3, "Error");
                }
            }

            ProcessStartInfo ProcessId = new("cmd.exe", $"/c {RecorderScreenMainWindow.ResourcePath} -f gdigrab AudioRecordings/" + AudioName + "");
            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessId.CreateNoWindow = true;
            ProcessId.RedirectStandardOutput = true;
            Process.Start(ProcessId);

            CheckElementsAudioRecordings();

        }

        private void CheckElementsAudioRecordings()
        {
            btnStartRecording.Enabled = false;
            ComboBoxMicrophone.Enabled = false;
            ComboBoxSpeaker.Enabled = false;
            RadioTwoTrack.Enabled = false;
            RadioDesktop.Enabled = false;
            radioMicrophone.Enabled = false;
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
            RadioTwoTrack.Enabled = true;
            RadioDesktop.Enabled = true;
            radioMicrophone.Enabled = true;
            BtnBackScreen.Enabled = true;

            if (RadioTwoTrack.Checked == true)
            {
                if (AudioRecorderMic.waveIn is object)
                {
                    AudioRecorderMic.waveIn.StopRecording();
                }

                if (AudioRecorderDesktop.waveIn is object)
                {
                    AudioRecorderDesktop.waveIn.StopRecording();
                }
            }
            else if (AudioRecorderDesktop.waveIn is object)
            {
                AudioRecorderDesktop.waveIn.StopRecording();
            }
            else if (AudioRecorderMic.waveIn is object)
            {
                AudioRecorderMic.waveIn.StopRecording();
            }

            var soundPlayer = new System.Media.SoundPlayer();
            soundPlayer.Stop();

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
            RadioDesktop.Text = StringsEN.RadioDesktop;
            RadioTwoTrack.Text = StringsEN.RadioTwoTrack;
            btnOutputRecordings.Text = StringsEN.btnOutputRecordings;
            crownGroupBox2.Text = StringsEN.crownGroupBox2;
            crownGroupBox3.Text = StringsEN.crownGroupBox3;
            radioMicrophone.Text = StringsEN.radioMicrophone;
            BtnBackScreen.Text = StringsEN.BtnBackScreen;
        }

        private void AudioRecorderMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
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
