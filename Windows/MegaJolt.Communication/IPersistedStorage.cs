#region USING STATEMENTS
using System.Collections.Generic;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public interface IPersistedStorage
    {
        IEnumerable<string> ReadAll(string location);
        void Write(string location, string data);
    }
}
