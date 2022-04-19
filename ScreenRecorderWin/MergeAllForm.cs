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

        }

        private void BtnMergeAll_Click(object sender, EventArgs e)
        {

            Process.Start("cmd.exe", "/k ffmpeg -i " + txtVideoPath.Text + " -i " + txtAudioDesk.Text + " -i " + txtAudioMic.Text + " -filter_complex amerge -c:v copy -c:a aac -b:a 320k output.mkv & exit /b");
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
    }
}
