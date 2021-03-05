using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogIO
{
    public class Logger
    {
        private readonly static Logger _logger = new Logger();
        private readonly FmLogViewer _fmLogViewer = FmLogViewer.GetFmLogViewer();

        private string LogFilePath => _logFile?.FullName;

        FileInfo _logFile = null;
        private readonly object _lockObj = new object();
        public static Logger GetInstance() => _logger;

        private readonly StringBuilder _stringBuilder = new StringBuilder();
        /// <summary>
        /// unit: Byte
        /// Minimum size is 100 KB. 
        /// Default size is 10 MB
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
                    _stringBuilder.Capacity = value;
                }
            }
        }

        private LogLevel _logLevel = LogLevel.None;
        public void SetLogLevel(LogLevel logLevel) => _logLevel = logLevel;
        Func<string, string> _encrypt = null;
        private int _maxFileSize = 10 * 1024 * 1024;
        private int _bufferSize = 10 * 1024;

        public void SetEncryptFunc(Func<string, string> encriptionFunc) => _encrypt = encriptionFunc;
        public bool EnableEncryption { get; set; } = false;
        public bool EnableOutputFile { get; set; } = true;
        public bool EnableOutputConsole { get; set; } = false;
        private Logger()
        {
            MaxFileSize = 10 * 1024;

            _fmLogViewer.ChangedLogFile += (logfile) => ChangeLogFile(logfile);

            ChangeLogFile($@"log\{DateTime.Now.ToString("yyMMdd_HHmmss")}_log.log");
        }

        public void ChangeLogFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) ||
                fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0)
                return;

            // ログファイルを生成する
            lock (_lockObj)
            {
                _logFile = new FileInfo(fileName);
                if (!Directory.Exists(_logFile.DirectoryName))
                    Directory.CreateDirectory(_logFile.DirectoryName);

                _fmLogViewer.SetLogFilename(fileName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Error(string msg) => Out(LogLevel.Error, msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex) => Out(LogLevel.Error, ex.Message + Environment.NewLine + ex.StackTrace);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sync"></param>
        public void Warn(string msg, bool sync = true) => Out(LogLevel.Warning, msg, sync);
        public void Info(string msg, bool sync = true) => Out(LogLevel.Information, msg, sync);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sync"></param>
        public void Debug(string msg, bool sync = true) => Out(LogLevel.Debug, msg);
        readonly System.TimeZoneInfo _timeZoneInfo = System.TimeZoneInfo.Local;
        /// <summary>
        /// ログを出力する
        /// </summary>
        /// <param name="level">ログレベル</param>
        /// <param name="msg">メッセージ</param>
        /// <param name="sync">Synced flush</param>
        private void Out(LogLevel level, string msg, bool sync = true)
        {
            if (!EnableOutputFile && !EnableOutputConsole)//Neither file or console is enable to output
                return;
#if !DEBUG
            if (level < _logLevel)
                return;
#endif

            if (string.IsNullOrWhiteSpace(msg))
                return;

            int treadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            string fullMsg =
#if DEBUG
                "[DEBUG BUILD (LogIO.dll)]" +
#endif
                $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff")}{_timeZoneInfo.DisplayName}]" +
                $"[{treadId}][{level}] {msg}{Environment.NewLine}";
            if (EnableEncryption && _encrypt != null)
                fullMsg = _encrypt(fullMsg);

            lock (_lockObj)
            {
                if (sync)
                    WriteSync(fullMsg);
                else
                {
                    WriteAsync(fullMsg);
                }
            }
            _logFile = new FileInfo(LogFilePath);
            if (_logFile.Exists &&
                _logFile.Length > (long)MaxFileSize * 1024)
            {
                RotateLogFile();
            }

        }
        private void WriteSync(string line)
        {
            _stringBuilder.Append(line);

            Flush();
        }

        private void WriteAsync(string line)
        {
            _stringBuilder.Append(line);
            if (_stringBuilder.Length > BufferSize * 0.8)
                Flush();
        }
        public void Flush()
        {
            if (_stringBuilder.Length < 1)
                return;
            var text = _stringBuilder.ToString();

            _fmLogViewer.AppendText(text);

            if (EnableOutputFile)
                File.AppendAllText(LogFilePath, text);
            if (EnableOutputConsole)
                Console.Write(text);

            _stringBuilder.Clear();
        }
        private void RotateLogFile()
        {
            lock (_lockObj)
            {
                string oldFilePath = _logFile.FullName;

                for (int i = 1; i < 1000; i++)
                {
                    oldFilePath = $@"{_logFile.DirectoryName}\{Path.GetFileNameWithoutExtension(_logFile.FullName)}({i}).log";

                    if (!File.Exists(oldFilePath))
                        break;
                }
                File.Move(LogFilePath, oldFilePath);
            }
        }
        public void Show(int x, int y) => Show(new System.Drawing.Point(x, y));
        public void Show(System.Drawing.Point point)
        {
            if (_fmLogViewer.Visible)
                return;

            _fmLogViewer.Location = point;
            _fmLogViewer.Visible = true;
        }
        public void Hide() => _fmLogViewer.Visible = false;
    }
}