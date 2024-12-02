using Serilog;
using Simple_Screen_Recorder.Langs;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.Utils;
using System.Diagnostics;
using System.IO;

namespace Simple_Screen_Recorder.ScreenRecorderWin
{
    public partial class MergeVideoAudioForm : Form
    {
        string outputFileName = "output-" + DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss") + ".mkv";
        private readonly ILogger _logger = LoggerConfig.Logger;

        public MergeVideoAudioForm()
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("MergeVideoAudioForm.Initialize");
            try
            {
                InitializeComponent();
                string outputFolder = Application.StartupPath + @"\OutputFiles";
                Directory.CreateDirectory(outputFolder);
                _logger.Information("Output directory created/verified: {Path}", outputFolder);

                notifyResultMergeVideoAudio = new NotifyIcon();
                notifyResultMergeVideoAudio.Icon = SystemIcons.Information;
                notifyResultMergeVideoAudio.Visible = false;

                _logger.LogOperationEnd("MergeVideoAudioForm.Initialize", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error initializing MergeVideoAudioForm");
                throw;
            }
        }

        private void MergeVD_Load(object sender, EventArgs e)
        {
            GetTextsMainMerge();
        }

        public void BtnMergeAll_Click(object sender, EventArgs e)
        {
            var startTime = DateTime.Now;
            _logger.LogOperationStart("MergeVideoAudio");

            try
            {
                if (!ValidateInputFiles())
                {
                    return;
                }

                _logger.Information("Starting file merge\r\n - Video: {VideoPath}, Audio: {AudioPath}",
                    txtVideoPath.Text,
                    txtAudioDesk.Text);

                Process conversionProcess = new Process();
                conversionProcess.StartInfo.FileName = "cmd.exe";
                string ffmpegCommand = $"/c {RecorderScreenMainWindow.ResourcePath} -i " +
                                     txtVideoPath.Text +
                                     " -i " + txtAudioDesk.Text +
                                     " -c:v copy -c:a aac -b:a 320k OutputFiles/" +
                                     outputFileName + " & exit /b";

                conversionProcess.StartInfo.Arguments = ffmpegCommand;
                _logger.Information("FFmpeg command: {Command}", ffmpegCommand);

                conversionProcess.Start();
                _logger.Information("Merge process started with ID: {ProcessId}", conversionProcess.Id);

                conversionProcess.EnableRaisingEvents = true;
                conversionProcess.Exited += ConversionProcess_Exited;

                _logger.LogOperationEnd("MergeVideoAudio", DateTime.Now - startTime);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error starting the merge process");
                MessageBox.Show($"Error while performing merge: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputFiles()
        {
            if (string.IsNullOrEmpty(txtVideoPath.Text) || string.IsNullOrEmpty(txtAudioDesk.Text))
            {
                _logger.Warning("Empty file paths - Video: {VideoPath}, Audio: {AudioPath}",
                    txtVideoPath.Text, txtAudioDesk.Text);
                MessageBox.Show("Please select video and audio files", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!File.Exists(txtVideoPath.Text))
            {
                _logger.Warning("Video file not found: {Path}", txtVideoPath.Text);
                MessageBox.Show("Video file does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!File.Exists(txtAudioDesk.Text))
            {
                _logger.Warning("Audio file not found: {Path}", txtAudioDesk.Text);
                MessageBox.Show("Audio file does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                notifyResultMergeVideoAudio.BalloonTipTitle = StringsEN.MessageMergeTitle;
                notifyResultMergeVideoAudio.BalloonTipText = StringsEN.MessageMerge;
                notifyResultMergeVideoAudio.Visible = true;
                notifyResultMergeVideoAudio.ShowBalloonTip(5000);
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