#region USING STATEMENTS
using System.Collections.Generic;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    public class GetVersionCommand : Command
    {
        #region GetVersionCommand()
        public GetVersionCommand() : base(0x56) { }
        #endregion GetVersionCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 3; } }
        #endregion ResponseLength

        #region BuildResponse(IEnumerable<byte>)
        internal override Response BuildResponse(IEnumerable<byte> data)
        {
            return new GetVersionResponse(data);
        }
        #endregion BuildResponse
    }
}
