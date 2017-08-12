// <copyright file="Log4NetExtensions.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.Log4netProvider
{
    using System;
    using System.IO;
    using System.Reflection;
    using log4net;
    using log4net.Config;
    using log4net.Repository;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Configuration extensions
    /// </summary>
    public static class Log4NetExtensions
    {
        /// <summary>
        /// Basic configuration for log4net.
        /// </summary>
        /// <param name="loggerFactory">Core Logger Factory</param>
        /// <returns>Logger Factory</returns>
        /// <remarks>
        /// <para>The function firstly tries to configure log4net using a <c>log4net.config</c> file in the directory of the entry assembly using <c>log4net.Config.XmlConfigurator</c>.</para>
        /// <para>If no <c>log4net.config</c> file is found, <c>log4net.Config.BasicConfigurator</c> is used.</para>
        /// </remarks>
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

            Assembly entryAssembly = Assembly.GetEntryAssembly();
            ILoggerRepository repository = LogManager.GetRepository(entryAssembly);

            if (file.Exists)
            {
                if (watch)
                {
                    XmlConfigurator.ConfigureAndWatch(repository, file);
                }
                else
                {
                    XmlConfigurator.Configure(repository, file);
                }
            }
            else
            {
                BasicConfigurator.Configure(repository);
            }

            loggerFactory.AddProvider(new Jmw.Log4netProvider.Log4NetProvider());

            return loggerFactory;
        }
    }
}
