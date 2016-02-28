#region USING STATEMENTS
using System;
using System.Runtime.Serialization;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class InvalidResponseException : Exception
    {
        #region InvalidResponseException()
        public InvalidResponseException() { }
        #endregion InvalidResponseException
        #region InvalidResponseException(string)
        public InvalidResponseException(string message) : base(message) { }
        #endregion InvalidResponseException
        #region InvalidResponseException(string, Exception)
        public InvalidResponseException(string message, Exception innerException) : base(message, innerException) { }
        #endregion InvalidResponseException
    }
}
 