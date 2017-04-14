// <copyright file="Log4NetAdapter.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.Log4netProvider
{
    using System;
    using System.Reflection;
    using log4net;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ILogger implementation for log4net.
    /// </summary>
    public class Log4NetAdapter : ILogger
    {
        /// <summary>
        /// log4net Logger
        /// </summary>
        private ILog logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log4NetAdapter"/> class.
        /// </summary>
        /// <param name="loggerName">Logger Name</param>
        public Log4NetAdapter(string loggerName)
        {
            logger = LogManager.GetLogger(Assembly.GetEntryAssembly(), loggerName);
        }

        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return logger.IsDebugEnabled;
                case LogLevel.Information:
                    return logger.IsInfoEnabled;
                case LogLevel.Warning:
                    return logger.IsWarnEnabled;
                case LogLevel.Error:
                    return logger.IsErrorEnabled;
                case LogLevel.Critical:
                    return logger.IsFatalEnabled;
                default:
                    throw new ArgumentException($"Unknown log level {logLevel}.", nameof(logLevel));
            }
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (!string.IsNullOrEmpty(message))
            {
                switch (logLevel)
                {
                    case LogLevel.Debug:
                        logger.Debug(message, exception);
                        break;
                    case LogLevel.Information:
                        logger.Info(message, exception);
                        break;
                    case LogLevel.Warning:
                        logger.Warn(message, exception);
                        break;
                    case LogLevel.Error:
                        logger.Error(message, exception);
                        break;
                    case LogLevel.Critical:
                        logger.Fatal(message, exception);
                        break;
                    default:
                        logger.Warn($"Unknown log level {logLevel}.");
                        break;
                }
            }
        }
    }
}
