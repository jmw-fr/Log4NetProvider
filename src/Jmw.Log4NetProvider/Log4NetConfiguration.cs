// <copyright file="Log4NetConfiguration.cs" company="Weeger Jean-Marc">
// Copyright Weeger Jean-Marc under MIT Licence. See https://opensource.org/licenses/mit-license.php.
// </copyright>

namespace Jmw.Log4netProvider
{
    using System.IO;
    using System.Reflection;
    using log4net;
    using log4net.Config;
    using log4net.Repository;

    /// <summary>
    /// Log4Net configuration helper class
    /// </summary>
    public class Log4NetConfiguration
    {
        /// <summary>
        /// Configure log4net from the log4net.config file or by default using <c>BasicConfigurator</c>
        /// </summary>
        /// <remarks>
        /// <para>The function firstly tries to configure log4net using a <c>log4net.config</c> file in the directory of the entry assembly using <c>log4net.Config.XmlConfigurator</c>.</para>
        /// <para>If no <c>log4net.config</c> file is found, <c>log4net.Config.BasicConfigurator</c> is used.</para>
        /// </remarks>
        public static void Configure()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            ILoggerRepository repository = LogManager.GetRepository(entryAssembly);

            string log4netFileName = Path.Combine(Path.GetDirectoryName(entryAssembly.Location), "log4net.config");

            if (File.Exists(log4netFileName))
            {
                XmlConfigurator.Configure(repository, new FileInfo(log4netFileName));
            }
            else
            {
                BasicConfigurator.Configure(repository);
            }
        }
    }
}