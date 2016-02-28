using System;

namespace MegaJolt.Communication
{
    public class LogEventArgs : EventArgs
    {
        #region LogEventArgs(LogLevel, string)
        public LogEventArgs(LogLevel level, string message)
        {
            Level = level;
            Message = message;
        }
        #endregion LogEventArgs

        #region Level Property
        public LogLevel Level { get; private set; }
        #endregion Level
        #region Message Property
        public string Message { get; private set; }
        #endregion Message
    }
}
