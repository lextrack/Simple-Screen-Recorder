using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.UI;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    public partial class MergeAllForm : Form
    {
        public MergeAllForm()
        {
            InitializeComponent();
        }

        private void MergeVDM_Load(object sender, EventArgs e)
        {
            GetTextsMainAllMerge();
        }

        private void BtnMergeAll_Click(object sender, EventArgs e)
        {

            Process.Start("cmd.exe", $"/k {AudioDesktop.ResourcePath} -i " + txtVideoPath.Text + " -i " + txtAudioDesk.Text + " -i " + txtAudioMic.Text + " -filter_complex amerge -c:v copy -c:a aac -b:a 320k output.mkv & exit /b");
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
            Process.Start("explorer.exe", Application.StartupPath);
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
