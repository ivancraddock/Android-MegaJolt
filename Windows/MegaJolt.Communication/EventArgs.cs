#region USING STATEMENTS
using System;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class EventArgs<T> : EventArgs
    {
        #region EventArgs(T)
        public EventArgs(T value)
        {
            Value = value;
        }
        #endregion EventArgs

        #region Value Property
        public T Value { get; private set; }
        #endregion Value
    }
}
