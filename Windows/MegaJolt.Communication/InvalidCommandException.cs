#region USING STATEMENTS
using System;
using System.Runtime.Serialization;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class InvalidCommandException : Exception
    {
        #region InvalidCommandException()
        public InvalidCommandException() { }
        #endregion InvalidCommandException
        #region InvalidCommandException(string)
        public InvalidCommandException(string message) : base(message) { }
        #endregion InvalidCommandException
        #region InvalidCommandException(string, Exception)
        public InvalidCommandException(string message, Exception innerException) : base(message, innerException) { }
        #endregion InvalidCommandException
    }
}
 