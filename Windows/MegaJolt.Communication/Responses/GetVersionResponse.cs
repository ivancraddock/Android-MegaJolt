#region USING STATEMENTS
using System;
using System.Collections.Generic;
using System.Linq;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Responses
{
    public class GetVersionResponse : Response
    {
        #region GetVersionResponse(IEnumerable<byte>)
        internal GetVersionResponse(IEnumerable<byte> data) : base(data) { }
        #endregion GetVersionResponse

        #region Version Property
        public Version Version { get; private set; }
        #endregion Version

        #region Parse(IEnumerable<byte>)
        internal override void Parse(IEnumerable<byte> data)
        {
            byte[] bytes = data.ToArray();
            Version = new Version(bytes[0], bytes[1], bytes[2]);
        }
        #endregion Parse
    }
}
