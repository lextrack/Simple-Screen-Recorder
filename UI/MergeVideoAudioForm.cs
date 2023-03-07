using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    public partial class MergeVideoAudioForm : Form
    {
        string outputFileName = "output-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mkv";

        public MergeVideoAudioForm()
        {
            InitializeComponent();

            string outputFolder = Application.StartupPath + @"\OutputFiles";
            Directory.CreateDirectory(outputFolder);

            notifyResultMergeVideoAudio = new NotifyIcon();
            notifyResultMergeVideoAudio.Icon = SystemIcons.Information;
            notifyResultMergeVideoAudio.Visible = false;
        }

        private void MergeVD_Load(object sender, EventArgs e)
        {
            GetTextsMainMerge();
        }

        public void BtnMergeAll_Click(object sender, EventArgs e)
        {
            Process conversionProcess = new Process();
            conversionProcess.StartInfo.FileName = "cmd.exe";
            conversionProcess.StartInfo.Arguments = $"/c {RecorderScreenMainWindow.ResourcePath} -i " + txtVideoPath.Text + " -i " + txtAudioDesk.Text + " -shortest -c:v copy -c:a aac -b:a 320k OutputFiles/" + outputFileName + " & exit /b";
            conversionProcess.Start();

            conversionProcess.EnableRaisingEvents = true;
            conversionProcess.Exited += ConversionProcess_Exited;
        }

        private void ConversionProcess_Exited(object sender, EventArgs e)
        {
            notifyResultMergeVideoAudio.BalloonTipTitle = StringsEN.MessageMergeTitle;
            notifyResultMergeVideoAudio.BalloonTipText = StringsEN.MessageMerge;
            notifyResultMergeVideoAudio.Visible = true;
            notifyResultMergeVideoAudio.ShowBalloonTip(5000);
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

        public void btnOutputF_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "OutputFiles");
        }

        private void GetTextsMainMerge()
        {
            BtnVideo.Text = StringsEN.BtnVideo;
            BtnDeskAudio.Text = StringsEN.BtnDeskAudio;
            BtnMergeAll.Text = StringsEN.BtnMergeAll;
            btnOutputF.Text = StringsEN.btnOutputF;
            this.Text = StringsEN.titleMerge1;
        }

        private void MergeVidDeskForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
