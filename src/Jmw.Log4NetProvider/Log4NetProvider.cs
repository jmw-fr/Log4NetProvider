// <copyright file="Log4NetProvider.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.Log4netProvider
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ILoggerProvider implementation for log4net.
    /// </summary>
    public class Log4NetProvider : ILoggerProvider
    {
        /// <summary>
        /// Dictionary of loggers already allocated
        /// </summary>
        private IDictionary<string, ILogger> loggers = new Dictionary<string, ILogger>();

        /// <inheritdoc />
        public ILogger CreateLogger(string name)
        {
            if (!loggers.ContainsKey(name))
            {
                lock (loggers)
                {
                    if (!loggers.ContainsKey(name))
                    {
                        loggers[name] = new Log4NetAdapter(name);
                    }
                }
            }

            return loggers[name];
        }

        /// <inheritdoc />
        public void Dispose()
        {
            loggers.Clear();
            loggers = null;
        }
    }
}
