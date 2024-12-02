using NAudio.CoreAudioApi;
using NAudio.Wave;
using Serilog;
using Simple_Screen_Recorder.Properties;
using Simple_Screen_Recorder.Utils;
using System.IO;
using System.Media;

namespace Simple_Screen_Recorder.AudioComp
{
    public class AudioManager : IDisposable
    {
        private IWaveIn? waveIn;
        private WaveFileWriter? writer;
        private readonly string outputFolder;
        private bool isDisposed;
        private SoundPlayer? backgroundPlayer;
        private string? currentFileName;
        private const string TEMP_EXTENSION = ".temp";
        private readonly ILogger _logger;

        public AudioManager(string outputPath)
        {
            outputFolder = outputPath;
            _logger = LoggerConfig.Logger;
            Directory.CreateDirectory(outputFolder);
        }

        public static List<MMDevice> GetAudioDevices(DataFlow dataFlow)
        {
            var deviceEnum = new MMDeviceEnumerator();
            return deviceEnum.EnumerateAudioEndPoints(dataFlow, DeviceState.Active).ToList();
        }

        public void StartMicrophoneRecording(MMDevice device)
        {
            CleanupRecording();
            waveIn = new WasapiCapture(device);
            InitializeRecording("MicrophoneAudio");
        }

        public void StartSystemAudioRecording(MMDevice device)
        {
            CleanupRecording();

            backgroundPlayer = new SoundPlayer(Resources.Background);
            backgroundPlayer.PlayLooping();

            waveIn = new WasapiLoopbackCapture(device);
            InitializeRecording("SystemAudio");
        }

        private void InitializeRecording(string prefix)
        {
            if (waveIn == null) return;

            try
            {
                string timestamp = DateTime.Now.ToString("MM-dd-yyyy.HH.mm.ss");
                currentFileName = $"{prefix}.{timestamp}.wav";
                string tempPath = Path.Combine(outputFolder, currentFileName + TEMP_EXTENSION);

                writer = new WaveFileWriter(tempPath, waveIn.WaveFormat);
                _logger.Information("Started recording: {File}", currentFileName);

                waveIn.DataAvailable += OnDataAvailable;
                waveIn.RecordingStopped += OnRecordingStopped;

                try
                {
                    waveIn.StartRecording();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error starting recording device");
                    CleanupRecording();
                    throw new InvalidOperationException($"Error starting recording: {ex.Message}", ex);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error initializing recording");
                CleanupRecording();
                throw new InvalidOperationException($"Error initializing recording: {ex.Message}", ex);
            }
        }

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            if (writer != null && !isDisposed)
            {
                try
                {
                    writer.Write(e.Buffer, 0, e.BytesRecorded);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error writing audio data");
                    CleanupRecording();
                }
            }
        }

        private void OnRecordingStopped(object? sender, StoppedEventArgs e)
        {
            if (e.Exception != null)
            {
                _logger.Error(e.Exception, "Recording stopped due to error");
            }
            FinalizeRecording();
        }

        private void FinalizeRecording()
        {
            try
            {
                if (writer != null && currentFileName != null)
                {
                    string tempPath = Path.Combine(outputFolder, currentFileName + TEMP_EXTENSION);
                    string finalPath = Path.Combine(outputFolder, currentFileName);

                    writer.Flush();
                    writer.Dispose();
                    writer = null;

                    Thread.Sleep(100);

                    if (File.Exists(tempPath))
                    {
                        if (File.Exists(finalPath))
                        {
                            File.Delete(finalPath);
                        }
                        File.Move(tempPath, finalPath);
                        _logger.Information("Successfully finalized recording: {File}", finalPath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error finalizing recording");
            }
            finally
            {
                CleanupRecording();
            }
        }

        public void StopRecording()
        {
            try
            {
                if (waveIn != null)
                {
                    waveIn.StopRecording();
                    Thread.Sleep(200);
                }

                FinalizeRecording();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error stopping recording");
                throw;
            }
        }

        private void CleanupRecording()
        {
            try
            {
                if (backgroundPlayer != null)
                {
                    backgroundPlayer.Stop();
                    backgroundPlayer.Dispose();
                    backgroundPlayer = null;
                }

                if (waveIn != null)
                {
                    waveIn.DataAvailable -= OnDataAvailable;
                    waveIn.RecordingStopped -= OnRecordingStopped;
                    waveIn.Dispose();
                    waveIn = null;
                }

                if (writer != null)
                {
                    writer.Dispose();
                    writer = null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in cleanup recording");
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                try
                {
                    StopRecording();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error disposing AudioManager");
                }
                finally
                {
                    isDisposed = true;
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}
