using System;

namespace wikibus.nancy
{
    /// <summary>
    /// Logger Factory
    /// </summary>
    public class LoggerFactory
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <typeparam name="T">not used</typeparam>
        /// <returns>The Logger</returns>
        public static Logger GetLogger<T>()
        {
            return new Logger();
        }

        /// <summary>
        /// Logger class
        /// </summary>
        public class Logger
        {
            /// <summary>
            /// Gets a value indicating whether [is debug enabled].
            /// </summary>
            public bool IsDebugEnabled
            {
                get { return true; }
            }

            /// <summary>
            /// Gets a value indicating whether [is fatal enabled].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [is fatal enabled]; otherwise, <c>false</c>.
            /// </value>
            public bool IsFatalEnabled
            {
                get { return true; }
            }

            /// <summary>
            /// Gets a value indicating whether [is information enabled].
            /// </summary>
            /// <value>
            /// <c>true</c> if [is information enabled]; otherwise, <c>false</c>.
            /// </value>
            public bool IsInformationEnabled
            {
                get { return true; }
            }

            /// <summary>
            /// Gets a value indicating whether [is warning enabled].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [is warning enabled]; otherwise, <c>false</c>.
            /// </value>
            public bool IsWarningEnabled
            {
                get { return true; }
            }

            /// <summary>
            /// Gets a value indicating whether [is error enabled].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [is error enabled]; otherwise, <c>false</c>.
            /// </value>
            public bool IsErrorEnabled
            {
                get { return true; }
            }

            /// <summary>
            /// Gets a value indicating whether [is error enabled].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [is error enabled]; otherwise, <c>false</c>.
            /// </value>
            public bool IsTraceEnabled
            {
                get { return false; }
            }

            /// <summary>
            /// Debugs the specified format.
            /// </summary>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Debug(string format, params object[] args)
            {
                WriteMessage(format, args);
            }

            /// <summary>
            /// Debugs the specified exception.
            /// </summary>
            /// <param name="exception">The exception.</param>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Debug(Exception exception, string format, params object[] args)
            {
                WriteException(exception, format, args);
            }

            /// <summary>
            /// Information the specified format.
            /// </summary>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Information(string format, params object[] args)
            {
                WriteMessage(format, args);
            }

            /// <summary>
            /// Information the specified exception.
            /// </summary>
            /// <param name="exception">The exception.</param>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Information(Exception exception, string format, params object[] args)
            {
                WriteException(exception, format, args);
            }

            /// <summary>
            /// Warnings the specified format.
            /// </summary>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Warning(string format, params object[] args)
            {
                WriteMessage(format, args);
            }

            /// <summary>
            /// Warnings the specified exception.
            /// </summary>
            /// <param name="exception">The exception.</param>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Warning(Exception exception, string format, params object[] args)
            {
                WriteException(exception, format, args);
            }

            /// <summary>
            /// Errors the specified format.
            /// </summary>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Error(string format, params object[] args)
            {
                WriteMessage(format, args);
            }

            /// <summary>
            /// Errors the specified exception.
            /// </summary>
            /// <param name="exception">The exception.</param>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Error(Exception exception, string format, params object[] args)
            {
                WriteException(exception, format, args);
            }

            /// <summary>
            /// Fatal the specified format.
            /// </summary>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Fatal(string format, params object[] args)
            {
                WriteMessage(format, args);
            }

            /// <summary>
            /// Fatal the specified exception.
            /// </summary>
            /// <param name="exception">The exception.</param>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Fatal(Exception exception, string format, params object[] args)
            {
                WriteException(exception, format, args);
            }

            /// <summary>
            /// Traces the specified format.
            /// </summary>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Trace(string format, params object[] args)
            {
                WriteMessage(format, args);
            }

            /// <summary>
            /// Traces the specified exception.
            /// </summary>
            /// <param name="exception">The exception.</param>
            /// <param name="format">The format.</param>
            /// <param name="args">The arguments.</param>
            public void Trace(Exception exception, string format, params object[] args)
            {
                WriteException(exception, format, args);
            }

            private static void WriteMessage(string format, params object[] args)
            {
                Console.WriteLine(format, args);
            }

            private void WriteException(Exception exception, string format, params object[] args)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
                Console.WriteLine(format, args);
            }
        }
    }
}
