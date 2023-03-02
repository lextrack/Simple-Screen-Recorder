using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    public partial class MergeAllForm : Form
    {
        string outputFileName = "output-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mkv";

        public MergeAllForm()
        {
            InitializeComponent();

            string outputFolder = Application.StartupPath + @"\OutputFiles";
            Directory.CreateDirectory(outputFolder);

            notifyResultMergeAllMedia = new NotifyIcon();
            notifyResultMergeAllMedia.Icon = SystemIcons.Information;
            notifyResultMergeAllMedia.Visible = false;
        }

        private void MergeVDM_Load(object sender, EventArgs e)
        {
            GetTextsMainAllMerge();
        }

        private void BtnMergeAll_Click(object sender, EventArgs e)
        {
            Process conversionProcess = new Process();
            conversionProcess.StartInfo.FileName = "cmd.exe";
            conversionProcess.StartInfo.Arguments = "/k ffmpeg -i " + txtVideoPath.Text + " -i " + txtAudioDesk.Text + " -i " + txtAudioMic.Text + " -filter_complex amerge -shortest -c:v copy -c:a aac -b:a 320k OutputFiles/" + outputFileName + " & exit /b";
            conversionProcess.Start();

            conversionProcess.EnableRaisingEvents = true;
            conversionProcess.Exited += ConversionProcess_Exited;
        }

        private void ConversionProcess_Exited(object sender, EventArgs e)
        {
            notifyResultMergeAllMedia.BalloonTipTitle = StringsEN.MessageMergeTitle;
            notifyResultMergeAllMedia.BalloonTipText = StringsEN.MessageMerge;
            notifyResultMergeAllMedia.Visible = true;
            notifyResultMergeAllMedia.ShowBalloonTip(5000);
        }

        private void BtnVideo_Click(object sender, EventArgs e)
        {
            var archivo = new OpenFileDialog();
            archivo.InitialDirectory = Directory.GetCurrentDirectory();
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtVideoPath.Text = archivo.FileName;
            }
        }

        private void BtnDeskAudio_Click(object sender, EventArgs e)
        {
            var archivo = new OpenFileDialog();
            archivo.InitialDirectory = Directory.GetCurrentDirectory();
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtAudioDesk.Text = archivo.FileName;
            }
        }

        private void BtnMicAudio_Click(object sender, EventArgs e)
        {
            var archivo = new OpenFileDialog();
            archivo.InitialDirectory = Directory.GetCurrentDirectory();
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtAudioMic.Text = archivo.FileName;
            }
        }

        private void btnOutputF_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "OutputFiles");
        }

        private void GetTextsMainAllMerge()
        {
            this.Text = StringsEN.titleMerge2;
            BtnVideo2.Text = StringsEN.BtnVideo2;
            BtnDeskAudio2.Text = StringsEN.BtnDeskAudio2;
            BtnMergeAll2.Text = StringsEN.BtnMergeAll2;
            btnOutputF2.Text = StringsEN.btnOutputF2;
            BtnMicAudio.Text = StringsEN.BtnMicAudio;
        }

        private void MergeAllForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
