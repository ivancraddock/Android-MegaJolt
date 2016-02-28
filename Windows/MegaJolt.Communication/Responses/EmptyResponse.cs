using System.Collections.Generic;

namespace MegaJolt.Communication.Responses
{
    public class EmptyResponse : Response
    {
        #region EmptyResponse()
        internal EmptyResponse() : base(new byte[] { }) { }
        #endregion EmptyResponse

        #region Parse(IEnumerable<byte>)
        internal override void Parse(IEnumerable<byte> data) { }
        #endregion Parse
    }
}
