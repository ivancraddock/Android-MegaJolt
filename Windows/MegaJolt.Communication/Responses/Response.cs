using System.Collections.Generic;

namespace MegaJolt.Communication.Responses
{
    public abstract class Response
    {
        #region Response(IEnumerable<byte>)
        internal Response(IEnumerable<byte> data)
        {
            Parse(data);
        }
        #endregion Response

        #region Parse(IEnumerable<byte>)
        internal abstract void Parse(IEnumerable<byte> data);
        #endregion Parse
    }
}
