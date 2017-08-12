// <copyright file="Log4NetExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.Log4netProvider
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Configuration extensions
    /// </summary>
    public static class Log4NetExtensions
    {
        /// <summary>
        /// Basic configuration for log4net using <see cref="Log4NetConfiguration.Configure"/> and adds a log4net provider to <see cref="ILoggerFactory" />.
        /// </summary>
        /// <param name="loggerFactory">Core Logger Factory</param>
        /// <returns>Logger Factory</returns>
        /// <seealso cref="Log4NetConfiguration" />
        public static ILoggerFactory AddLog4Net(this ILoggerFactory loggerFactory)
        {
            return loggerFactory.AddLog4Net("log4net.config");
        }

        /// <summary>
        ///   Add Log4Net Logger using the given config file.
        /// </summary>
        /// <param name="loggerFactory">The logger factory to add Log4net to.</param>
        /// <param name="filename">The name of the file. This should be nex to the entry assembly.</param>
        /// <param name="watch">Should Log4net watch the config file for changes</param>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory loggerFactory, string filename, bool watch = false)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string log4netFileName = Path.Combine(Path.GetDirectoryName(entryAssembly.Location), filename);

            return loggerFactory.AddLog4Net(new FileInfo(log4netFileName), watch);
        }

        /// <summary>
        ///   Add Log4Net Logger using the given config file.
        /// </summary>
        /// <param name="loggerFactory">The logger factory to add Log4net to.</param>
        /// <param name="file">The file to laod config from.</param>
        /// <param name="watch">Should Log4net watch the config file for changes</param>
        public static ILoggerFactory AddLog4Net(this ILoggerFactory loggerFactory , FileInfo file, bool watch = false)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            Log4NetConfiguration.Configure(file, watch);

            loggerFactory.AddProvider(new Jmw.Log4netProvider.Log4NetProvider());

            return loggerFactory;
        }
    }
}
