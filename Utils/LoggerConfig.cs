using Serilog;
using System.IO;

namespace Simple_Screen_Recorder.Utils
{
    public static class LoggerConfig
    {
        private static ILogger? _logger;

        public static ILogger Logger => _logger ??= CreateLogger();

        private static ILogger CreateLogger()
        {
            string logPath = Path.Combine(Application.StartupPath, "Logs");
            Directory.CreateDirectory(logPath);

            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(
                    Path.Combine(logPath, "log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.Debug()
                .CreateLogger();
        }
    }

    public static class LoggingExtensions
    {
        public static void LogOperationStart(this ILogger logger, string operationName, string details = "")
        {
            logger.Information("Starting operation: {OperationName}. {Details}", operationName, details);
        }
        public static void LogOperationEnd(this ILogger logger, string operationName, TimeSpan duration)
        {
            logger.Information("Operation completed: {OperationName}. Duration: {Duration}ms",
                operationName, duration.TotalMilliseconds);
        }
        public static void LogAudioDeviceInfo(this ILogger logger, string deviceType, string deviceName)
        {
            logger.Information("Audio device {DeviceType} selected: {DeviceName}",
                deviceType, deviceName);
        }
        public static void LogRecordingSettings(this ILogger logger, string codec, int fps, string bitrate, string format)
        {
            logger.Information("Recording settings - Codec: {Codec}, FPS: {FPS}, Bitrate: {Bitrate}, Format: {Format}",
                codec, fps, bitrate, format);
        }
    }
}
