using Microsoft.VisualBasic;
using NAudio.Wave;
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

            AudioRecMic.OpenComp();
            ComboBoxMicrophone.DataSource = AudioRecMic.cboDIspositivos.DataSource;
            AudioRecDesktop.OpenComp();
            ComboBoxSpeaker.DataSource = AudioRecDesktop.cboDIspositivos.DataSource;
        }

        private void btnStartRecording_Click(object sender, EventArgs e)
        {
            LbTimer.ForeColor = Color.IndianRed;
            TimeRec = DateTime.Now;
            RecState.Enabled = true;
            AudioName = "Audio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".avi";

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

            ProcessStartInfo ProcessId = new("cmd.exe", "/c ffmpeg -f gdigrab AudioRecordings/" + AudioName + "");
            ProcessId.WindowStyle = ProcessWindowStyle.Hidden;
            ProcessId.CreateNoWindow = true;
            ProcessId.RedirectStandardOutput = true;
            Process.Start(ProcessId);

            btnStartRecording.Enabled = false;
            ComboBoxMicrophone.Enabled = false;
            ComboBoxSpeaker.Enabled = false;
            RadioTwoTrack.Enabled = false;
            RadioDesktop.Enabled = false;

        }

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

        public void StopRec()
        {
            btnStartRecording.Enabled = true;
            ComboBoxMicrophone.Enabled = true;
            ComboBoxSpeaker.Enabled = true;
            RadioTwoTrack.Enabled = true;
            RadioDesktop.Enabled = true;

            if (RadioTwoTrack.Checked == true)
            {
                if (AudioRecMic.waveIn is object)
                {
                    AudioRecMic.waveIn.StopRecording();
                }

                if (AudioRecDesktop.waveIn is object)
                {
                    AudioRecDesktop.waveIn.StopRecording();
                }
            }
            else if (AudioRecDesktop.waveIn is object)
            {
                AudioRecDesktop.waveIn.StopRecording();
            }
            else if (AudioRecMic.waveIn is object)
            {
                AudioRecMic.waveIn.StopRecording();
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

        public void RecMic()
        {
            AudioRecMic.Cleanup();
            AudioRecMic.CreateWaveInDevice();
            AudioRecMic.outputFilename = "MicrophoneAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".aac";
            AudioRecMic.writer = new WaveFileWriter(Path.Combine(AudioRecMic.outputFolder, AudioRecMic.outputFilename), AudioRecMic.waveIn.WaveFormat);
            AudioRecMic.waveIn.StartRecording();
        }

        public void RecSpeaker()
        {
            AudioRecDesktop.Cleanup();
            AudioRecDesktop.CreateWaveInDevice();
            GrabadorPantalla.My.MyProject.Computer.Audio.Play("Background.wav", AudioPlayMode.BackgroundLoop);
            AudioRecDesktop.outputFilename = "SystemAudio." + Strings.Format(DateTime.Now, "MM-dd-yyyy.HH.mm.ss") + ".aac";
            AudioRecDesktop.writer = new WaveFileWriter(Path.Combine(AudioRecDesktop.outputFolder, AudioRecDesktop.outputFilename), AudioRecDesktop.waveIn.WaveFormat);
            AudioRecDesktop.waveIn.StartRecording();
        }

        private void btnOutputRecordings_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "AudioRecordings");
        }

        private void RecState_Tick(object sender, EventArgs e)
        {
            var Difference = DateTime.Now.Subtract(TimeRec);
            LbTimer.Text = "Rec: " + Difference.Hours.ToString() + ":" + Difference.Minutes.ToString() + ":" + Difference.Seconds.ToString();
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
        }

        private void AudioRecorderMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
