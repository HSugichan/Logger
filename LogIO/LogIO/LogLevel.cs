using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIO
{
    /// <summary>
    /// Log level
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// None(default)
        /// </summary>
        None = 0,
        /// <summary>
        /// For debug
        /// </summary>
        Debug = 1,
        /// <summary>
        /// For information
        /// </summary>
        Information,
        /// <summary>
        /// For warning
        /// </summary>
        Warning,
        /// <summary>
        /// For error
        /// </summary>
        Error,
        /// <summary>
        /// For crisis
        /// </summary>
        Crisis
    }
}
