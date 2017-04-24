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
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            Log4NetConfiguration.Configure();

            loggerFactory.AddProvider(new Jmw.Log4netProvider.Log4NetProvider());

            return loggerFactory;
        }
    }
}