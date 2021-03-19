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
        /// All (None filter)
        /// </summary>
        All,
        /// <summary>
        /// For performance log
        /// </summary>
        Trace,
        /// <summary>
        /// For debug log
        /// </summary>
        Debug,
        /// <summary>
        /// For information log (send/recieve data, access date-time, etc.)
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
        Crisis,
        /// <summary>
        /// No out log
        /// </summary>
        None = 99,
    }
}
