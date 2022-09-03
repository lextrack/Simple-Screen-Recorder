using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    public partial class MergeVidDeskForm : Form
    {
        public MergeVidDeskForm()
        {
            InitializeComponent();
        }
        private void MergeVD_Load(object sender, EventArgs e)
        {
            GetTextsMainMerge();
        }

        public void BtnMergeAll_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", "/k ffmpeg -i " + txtVideoPath.Text + " -i " + txtAudioDesk.Text + " -c:v copy -c:a aac -b:a 320k output.mkv & exit /b");
        }

        private void BtnVideo_Click(object sender, EventArgs e)
        {
            var archivo = new OpenFileDialog();
            archivo.InitialDirectory = "Recordings";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtVideoPath.Text = archivo.FileName;
            }
        }

        private void BtnDeskAudio_Click(object sender, EventArgs e)
        {
            var archivo = new OpenFileDialog();
            archivo.InitialDirectory = "Recordings";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                txtAudioDesk.Text = archivo.FileName;
            }
        }

        public void btnOutputF_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Application.StartupPath);
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
