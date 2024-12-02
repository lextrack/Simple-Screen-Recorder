using Serilog;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.Utils;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    public partial class MergeAllForm : Form
    {
        string outputFileName = "output-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mkv";
        private readonly ILogger _logger = LoggerConfig.Logger;

        public MergeAllForm()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("MergeAllForm.Initialize");
            try
            {
                InitializeComponent();

                string outputFolder = Application.StartupPath + @"\OutputFiles";
                Directory.CreateDirectory(outputFolder);
                _logger.Information("Output directory created/verified: {Path}", outputFolder);

                notifyResultMergeAllMedia = new NotifyIcon();
                notifyResultMergeAllMedia.Icon = SystemIcons.Information;
                notifyResultMergeAllMedia.Visible = false;

                _logger.LogOperationEnd("MergeAllForm.Initialize", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error initializing MergeAllForm");
                throw;
            }
        }

        private void MergeVDM_Load(object sender, EventArgs e)
        {
            GetTextsMainAllMerge();
        }

        private void BtnMergeAll_Click(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("MergeAll");

            try
            {
                if (!ValidateInputFiles())
                {
                    return;
                }

                _logger.Information("Starting file merge - Video: {VideoPath}, Desktop Audio: {AudioDesk}, Mic Audio: {AudioMic}",
                    txtVideoPath.Text,
                    txtAudioDesk.Text,
                    txtAudioMic.Text);

                Process conversionProcess = new Process();
                conversionProcess.StartInfo.FileName = "cmd.exe";
                string ffmpegCommand = $"/c {RecorderScreenMainWindow.ResourcePath} -i " + txtVideoPath.Text +
                                     " -i " + txtAudioDesk.Text +
                                     " -i " + txtAudioMic.Text +
                                     " -filter_complex amerge -c:v copy -c:a aac -b:a 320k OutputFiles/" +
                                     outputFileName + " & exit /b";

                conversionProcess.StartInfo.Arguments = ffmpegCommand;
                _logger.Information("FFmpeg Command: {Command}", ffmpegCommand);

                conversionProcess.Start();
                _logger.Information("Merge process started with ID: {ProcessId}", conversionProcess.Id);

                conversionProcess.EnableRaisingEvents = true;
                conversionProcess.Exited += ConversionProcess_Exited;

                _logger.LogOperationEnd("MergeAll", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error starting merge process");
                MessageBox.Show($"Error performing merge: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputFiles()
        {
            if (!File.Exists(txtVideoPath.Text))
            {
                _logger.Warning("Video file not found: {Path}", txtVideoPath.Text);
                MessageBox.Show("Video file does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!File.Exists(txtAudioDesk.Text))
            {
                _logger.Warning("Desktop audio file not found: {Path}", txtAudioDesk.Text);
                MessageBox.Show("Desktop audio file does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!File.Exists(txtAudioMic.Text))
            {
                _logger.Warning("Microphone audio file not found: {Path}", txtAudioMic.Text);
                MessageBox.Show("Microphone audio file does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void ConversionProcess_Exited(object sender, EventArgs e)
        {
            try
            {
                var outputPath = Path.Combine("OutputFiles", outputFileName);
                if (File.Exists(outputPath))
                {
                    var fileInfo = new FileInfo(outputPath);
                    _logger.Information("Merge completed successfully. Generated file: {Path}, Size: {Size}MB",
                        outputPath,
                        Math.Round(fileInfo.Length / (1024.0 * 1024.0), 2));
                }
                else
                {
                    _logger.Warning("Merge process finished but output file not found: {Path}", outputPath);
                }

                notifyResultMergeAllMedia.BalloonTipTitle = StringsEN.MessageMergeTitle;
                notifyResultMergeAllMedia.BalloonTipText = StringsEN.MessageMerge;
                notifyResultMergeAllMedia.Visible = true;
                notifyResultMergeAllMedia.ShowBalloonTip(5000);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error processing merge completion");
            }
        }

        private void BtnVideo_Click(object sender, EventArgs e)
        {
            try
            {
                string recordingsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Recordings");
                if (!Directory.Exists(recordingsFolder))
                {
                    Directory.CreateDirectory(recordingsFolder);
                    _logger.Information("Recordings directory created: {Path}", recordingsFolder);
                }

                using (var archivo = new OpenFileDialog())
                {
                    archivo.InitialDirectory = recordingsFolder;
                    archivo.Filter = "Video files|*.avi;*.mkv;*.wmv|All files|*.*";
                    archivo.Multiselect = false;

                    if (archivo.ShowDialog() == DialogResult.OK)
                    {
                        txtVideoPath.Text = archivo.FileName;
                        _logger.Information("Video file selected: {Path}", archivo.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error selecting video file");
                MessageBox.Show("Error selecting video file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeskAudio_Click(object sender, EventArgs e)
        {
            try
            {
                string recordingsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Recordings");
                if (!Directory.Exists(recordingsFolder))
                {
                    Directory.CreateDirectory(recordingsFolder);
                    _logger.Information("Recordings directory created: {Path}", recordingsFolder);
                }

                using (var archivo = new OpenFileDialog())
                {
                    archivo.InitialDirectory = recordingsFolder;
                    archivo.Filter = "Audio files|*.wav;*.aac|All files|*.*";
                    archivo.Multiselect = false;

                    if (archivo.ShowDialog() == DialogResult.OK)
                    {
                        txtAudioDesk.Text = archivo.FileName;
                        _logger.Information("Audio file selected: {Path}", archivo.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error selecting audio file");
                MessageBox.Show("Error selecting audio file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMicAudio_Click(object sender, EventArgs e)
        {
            try
            {
                string recordingsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Recordings");
                if (!Directory.Exists(recordingsFolder))
                {
                    Directory.CreateDirectory(recordingsFolder);
                    _logger.Information("Recordings directory created: {Path}", recordingsFolder);
                }

                using (var archivo = new OpenFileDialog())
                {
                    archivo.InitialDirectory = recordingsFolder;
                    archivo.Filter = "Audio files|*.wav;*.aac|All files|*.*";
                    archivo.Multiselect = false;

                    if (archivo.ShowDialog() == DialogResult.OK)
                    {
                        txtAudioMic.Text = archivo.FileName;
                        _logger.Information("Audio file selected: {Path}", archivo.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error selecting audio file");
                MessageBox.Show("Error selecting audio file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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