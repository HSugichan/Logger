﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LogIO.Properties;

namespace LogIO
{
    /// <summary>
    /// Utillity logger
    /// </summary>
    public class Logger
    {
        private readonly static Logger _logger = new Logger();
        private readonly FmLogViewer _fmLogViewer = FmLogViewer.GetFmLogViewer();

        private string LogFilePath => _logFile?.FullName;

        FileInfo _logFile = null;
        private readonly object _lockObj = new object();
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
                    _stringBuilder.Capacity = value;
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
        /// Enable to output log as file.(default false)
        /// </summary>
        public bool EnableOutputFile { get; set; } = false;
        /// <summary>
        /// Enable to output log as file.(default false)
        /// </summary>
        public bool EnableOutputConsole { get; set; } = false;

        private Logger()
        {
            _fmLogViewer.ChangedLogFile += (logfile) => ChangeLogFile(logfile);

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

            // ログファイルを生成する
            lock (_lockObj)
            {
                _logFile = new FileInfo(fileName);

                _fmLogViewer.SetLogFilename(fileName);
            }
        }
        /// <summary>
        /// Out exception log
        /// </summary>
        /// <param name="msg">Message</param>
        public void Error(string msg) => Out(LogLevel.Error, msg);

        /// <summary>
        /// Out exception message and stack trace
        /// </summary>
        /// <param name="ex">Exception</param>
        public void Error(Exception ex) => Out(LogLevel.Error, ex.Message + Environment.NewLine + ex.StackTrace);

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
        public void Debug(string msg, bool sync = true) => Out(LogLevel.Debug, msg);

        readonly System.TimeZoneInfo _timeZoneInfo = System.TimeZoneInfo.Local;
        /// <summary>
        /// Output log
        /// </summary>
        /// <param name="level">Log level</param>
        /// <param name="msg">Message</param>
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


            lock (_lockObj)
            {
                int treadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                string fullMsg =
#if DEBUG
                "[DEBUG BUILD (LogIO.dll)]" +
#endif
                $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ff")}{_timeZoneInfo.DisplayName}]" +
                    $"[{treadId}][{level}] {msg}{Environment.NewLine}";
                if (EnableEncryption && _encrypt != null)
                    fullMsg = _encrypt(fullMsg);

                if (sync)
                    WriteSync(fullMsg);
                else
                    WriteAsync(fullMsg);

                _logFile = new FileInfo(LogFilePath);//To update file length
                if (_logFile.Exists &&
                    _logFile.Length > (long)MaxFileSize)
                {
                    RotateLogFile();
                }

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
        /// <summary>
        /// Flush buffer data.
        /// </summary>
        public void Flush()
        {
            if (_stringBuilder.Length < 1)
                return;
            var text = _stringBuilder.ToString();

            _fmLogViewer.AppendText(text);

            if (EnableOutputFile)
            {
                if (!Directory.Exists(_logFile.DirectoryName))
                    Directory.CreateDirectory(_logFile.DirectoryName);

                File.AppendAllText(_logFile.FullName, text);
            }

            if (EnableOutputConsole)
                Console.Write(text);

            _stringBuilder.Clear();
        }
        private void RotateLogFile()
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
        /// <summary>
        /// Show Logger control
        /// </summary>
        /// <param name="x">Position X</param>
        /// <param name="y">Position Y</param>
        public void Show(int x, int y) => Show(new System.Drawing.Point(x, y));
        /// <summary>
        /// Show Logger control
        /// </summary>
        /// <param name="point">Position</param>
        public void Show(System.Drawing.Point point)
        {
            if (_fmLogViewer.Visible)
                return;

            _fmLogViewer.Location = point;
            _fmLogViewer.Visible = true;
        }
        /// <summary>
        /// Hide Logger control
        /// </summary>
        public void Hide() => _fmLogViewer.Visible = false;
    }
}