using Microsoft.VisualBasic;
using NAudio.Wave;
using Simple_Screen_Recorder.ScreenRecorderWin;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder
{
    public partial class RecorderScreenForm
    {
        private DateTime TimeRec = new DateTime();
        private string VideoName = "";

        public RecorderScreenForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AudioMic.OpenComp();
            ComboBox1.DataSource = AudioMic.cboDIspositivos.DataSource;
            AudioDesktop.OpenComp();
            ComboBox2.DataSource = AudioDesktop.cboDIspositivos.DataSource;
        }
        public int ProcessId { get; private set; }
        public void btnStartRecording_Click(object sender, EventArgs e)
        {
            LbTimer.ForeColor = Color.IndianRed;
            TimeRec = DateTime.Now;
            RecState.Enabled = true;
            VideoName = "Video." + Strings.Format(DateTime.Now, "dd-MM-yyyy.HH.mm.ss") + ".avi";
            if (RadioTwoTrack.Checked == true)
            {
                RecMic();
                RecSpeaker();
            }
            else
            {
                RecSpeaker();
            }

            var process = Process.Start("cmd.exe", "/k ffmpeg -hide_banner -f gdigrab -framerate 60 -i desktop -crf 0 -preset medium -color_range 2 -b:v 15000k -tune zerolatency Recordings/" + VideoName + "");
            this.ProcessId = process.Id;

            btnStartRecording.Enabled = false;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            try
            {
                LbTimer.ForeColor = Color.Black;
                RecState.Enabled = false;
                StopRec();

            }
            catch (Exception)
            {

                MessageBox.Show("You are not recording anything", "Error");
            }

            btnStartRecording.Enabled=true;
        }

        public void StopRec()
        {

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

        public void RecMic()
        {
            AudioMic.Cleanup();
            AudioMic.CreateWaveInDevice();
            AudioMic.outputFilename = "AudioMic." + Strings.Format(DateTime.Now, "dd-MM-yyyy.HH.mm.ss") + ".wav";
            AudioMic.writer = new WaveFileWriter(Path.Combine(AudioMic.outputFolder, AudioMic.outputFilename), AudioMic.waveIn.WaveFormat);
            AudioMic.waveIn.StartRecording();
        }

        public void RecSpeaker()
        {
            AudioDesktop.Cleanup();
            AudioDesktop.CreateWaveInDevice();
            GrabadorPantalla.My.MyProject.Computer.Audio.Play("Background.wav", AudioPlayMode.BackgroundLoop);
            AudioDesktop.outputFilename = "AudioSystem." + Strings.Format(DateTime.Now, "dd-MM-yyyy.HH.mm.ss") + ".wav";
            AudioDesktop.writer = new WaveFileWriter(Path.Combine(AudioDesktop.outputFolder, AudioDesktop.outputFilename), AudioDesktop.waveIn.WaveFormat);
            AudioDesktop.waveIn.StartRecording();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            var Difference = DateTime.Now.Subtract(TimeRec);
            LbTimer.Text = "Rec: " + Difference.Hours.ToString() + ":" + Difference.Minutes.ToString() + ":" + Difference.Seconds.ToString();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void mergeVideoDesktopAndMicAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeAllForm NewMergeVDM = new();
            NewMergeVDM.ShowDialog();
        }

        private void mergeVideoAndDesktopAudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeVidDeskForm NewMergeVD = new();
            NewMergeVD.ShowDialog();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About NewAbout = new();
            NewAbout.ShowDialog();
        }

    }
}