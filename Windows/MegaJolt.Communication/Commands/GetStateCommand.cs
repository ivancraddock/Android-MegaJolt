#region USING STATEMENTS
using System.Collections.Generic;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    internal class GetStateCommand : Command
    {
        #region GetStateCommand()
        public GetStateCommand() : base(0x53) { }
        #endregion GetStateCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 9; } }
        #endregion ResponseLength

        #region BuildResponse(IEnumerable<byte>)
        internal override Response BuildResponse(IEnumerable<byte> data)
        {
            return new GetStateResponse(data);
        }
        #endregion BuildResponse
    }
}
