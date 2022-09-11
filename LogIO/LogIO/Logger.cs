using LogIO.Properties;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LogIO
{
    /// <summary>
    /// Utillity logger
    /// Ref. https://qiita.com/yun_bow/items/24f38179d1e92c9b7fd3
    /// </summary>
    public class Logger
    {
        private readonly static Logger _logger = new Logger();
        /// <summary>
        /// Output file path.
        /// </summary>
        public string LogFilePath => _logFile?.FullName;

        FileInfo _logFile = null;
        private readonly object _lockFile = new object();
        /// <summary>
        /// Getter instance (singleton)
        /// </summary>
        /// <returns></returns>
        public static Logger GetInstance() => _logger;

        private readonly StringBuilder _stringBuilder = new StringBuilder();
        /// <summary>
        /// unit: Byte
        /// Minimum size is 100 KB. 
        /// Default size is 1 MB
        /// </summary>
        public int MaxFileSize
        {
            get => _maxFileSize;
            set
            {
                if (value > 100 * 1024)
                    _maxFileSize = value;
            }
        }
        /// <summary>
        /// unit: Byte
        /// Minimum size is 100 Byte. 
        /// Maximum size is 1 MB. 
        /// Default size is 10 KB.
        /// </summary>
        public int BufferSize
        {
            get => _bufferSize;
            set
            {
                if (100 < value && value < 1 * 1024 * 1024)
                {
                    _bufferSize = value;
                    _stringBuilder.Capacity = _bufferSize;
                }
            }
        }

        private LogLevel _logLevel = LogLevel.None;
        /// <summary>
        /// Setter log level
        /// </summary>
        /// <param name="logLevel">log level</param>
        public void SetLogLevel(LogLevel logLevel) => _logLevel = logLevel;
        Func<string, string> _encrypt = null;
        private int _maxFileSize = 1 * 1024 * 1024;
        private int _bufferSize = 10 * 1024;
        /// <summary>
        /// Set encrypter
        /// </summary>
        /// <param name="encryptionFunc">encrypt function</param>
        public void SetEncryptFunc(Func<string, string> encryptionFunc) => _encrypt = encryptionFunc;
        /// <summary>
        /// Enable to encrypt
        /// </summary>
        public bool EnableEncryption { get; set; } = false;
        /// <summary>
        /// Enable to output log to file.(default false)
        /// </summary>
        public bool EnableOutputFile { get; set; } = false;
        /// <summary>
        /// Enable to output log to console.(default false)
        /// </summary>
        public bool EnableOutputConsole { get; set; } = false;
        /// <summary>
        /// Enable to output log to viewer.(default false)
        /// </summary>
        public bool EnableOutputViewer { get; set; } = false;

        private Action<string> _outLog = null;

        /// <summary>
        /// To out log to EXE UI.
        /// </summary>
        /// <param name="outLog"></param>
        public void SetOutLogFunc(Action<string> outLog) => _outLog = outLog;

        private Logger()
        {
#if DEBUG
            SetLogLevel(LogLevel.Trace);
#else
            SetLogLevel(LogLevel.None);
#endif
            ChangeLogFile($@"log\{DateTime.Now.ToString("yyMMdd_HHmmss")}_log.log");
        }

        /// <summary>
        /// Change file path.
        /// </summary>
        /// <param name="fileName"></param>
        public void ChangeLogFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) ||
                fileName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new Exception(Resources.MsgErrInvalidPath);

            fileName = Path.GetFullPath(fileName);

            // ログファイルを生成する
            lock (_lockFile)
            {
                _logFile = new FileInfo(fileName);

            }

        }
        /// <summary>
        /// Out exception log
        /// </summary>
        /// <param name="msg">Message</param>
        public void Error(string msg) => Out(LogLevel.Error, msg);

        /// <summary>
        /// Out exception message
        /// </summary>
        /// <param name="ex">Exception</param>
        public void Error(Exception ex) => Out(LogLevel.Error, $"{ex.Message}");

        /// <summary>
        /// Out critical exception message and stack trace
        /// </summary>
        /// <param name="ex">Exception</param>
        public void Critical(Exception ex) => Out(LogLevel.Crisis, $"{ex.Message} ({ex.StackTrace})");

        /// <summary>
        /// Out warning log
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="sync">Synced flush</param>
        public void Warn(string msg, bool sync = true) => Out(LogLevel.Warning, msg, sync);
        /// <summary>
        /// Out information log
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="sync">Synced flush</param>
        public void Info(string msg, bool sync = true) => Out(LogLevel.Information, msg, sync);

        /// <summary>
        /// Out debug log
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="sync">Synced flush</param>
        public void Debug(string msg, bool sync = true) => Out(LogLevel.Debug, msg, sync);
        /// <summary>
        /// Out trace log
        /// </summary>
        /// <param name="msg">Message</param>
        /// <param name="sync">Synced flush</param>
        public void Trace(string msg, bool sync = true) => Out(LogLevel.Trace, msg, sync);

        readonly System.TimeZoneInfo _timeZoneInfo = System.TimeZoneInfo.Local;
        /// <summary>
        /// Output log
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="msg">Message</param>
        /// <param name="sync">Synced flush</param>
        private void Out(LogLevel level, string msg, bool sync = true)
        {
            if (!EnableOutputFile &&
                !EnableOutputConsole &&
                !EnableOutputViewer)//None output is enable
                return;
#if !DEBUG
            if (_logLevel == LogLevel.None || level < _logLevel)
                return;
#endif
            if (sync)
                Flush();

            if (string.IsNullOrWhiteSpace(msg))
                return;

            int treadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

            if (msg.Any(x => char.IsControl(x)))
                msg = ConvertControlChar(msg);

            string fullMsg =
#if DEBUG
                "[DEBUG (LogIO.dll)] " +
#endif
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.ff)}{_timeZoneInfo.DisplayName}]" +
                $" [0x{treadId:x4}] [{level}] {msg}{Environment.NewLine}";

            if (sync)
                WriteSync(fullMsg);
            else
                WriteAsync(fullMsg);

            lock (_lockFile)
            {

                _logFile.Refresh();
                if (_logFile.Exists &&
                    _logFile.Length > (long)MaxFileSize)
                {
                    RotateLogFile();
                }

            }
        }

        private string ConvertControlChar(string inputStr)
        {
            var outputStr = Regex.Replace(inputStr, @"\p{Cc}", str =>
            {
                int offset = str.Value[0];
                if (Enum.IsDefined(typeof(ControlCharacters), (byte)offset))
                    return $"<{(ControlCharacters)offset}>";
                else
                    return $"<{offset:X2}>";
            });
            return outputStr;
        }

        private void WriteSync(string line)
        {
            //Viewerに出すログは暗号化しない
            string fullMsg = line;
            if (EnableEncryption && _encrypt != null)
                fullMsg = _encrypt(fullMsg);

            lock (_lockFile)
            {
                if (EnableOutputFile)
                {
                    if (!Directory.Exists(_logFile.DirectoryName))
                        Directory.CreateDirectory(_logFile.DirectoryName);

                    File.AppendAllText(_logFile.FullName, fullMsg);
                }

                if (EnableOutputConsole)
                    Console.Write(line);

                if (EnableOutputViewer)
                    _outLog?.Invoke(line);
            }
        }

        readonly object _lockBuffrr = new object();
        private void WriteAsync(string line)
        {
            lock (_lockBuffrr)
            {
                _stringBuilder.Append(line);

                if (_stringBuilder.Length < BufferSize * 0.8)
                    return;
            }

            Flush();
        }
        /// <summary>
        /// Flush buffer data.
        /// </summary>
        public void Flush()
        {
            string text;
            lock (_lockBuffrr)
            {
                if (_stringBuilder.Length < 1)
                    return;
                text = _stringBuilder.ToString();
                _stringBuilder.Clear();
            }
            WriteSync(text);
        }
        private void RotateLogFile()
        {
            string oldFilePath = _logFile.FullName;

            for (int i = 1; i < (1 << 10); i++)
            {
                oldFilePath = $@"{_logFile.DirectoryName}\{Path.GetFileNameWithoutExtension(_logFile.FullName)}({i}).log";

                if (!File.Exists(oldFilePath))
                    break;
            }
            File.Move(LogFilePath, oldFilePath);
        }

        ~Logger()
        {
            Flush();
        }
    }
}