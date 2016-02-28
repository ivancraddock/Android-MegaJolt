#region USING STATEMENTS
using System.Collections.Generic;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    public class GetGlobalConfigurationCommand : Command
    {
        #region GetGlobalConfigurationCommand()
        public GetGlobalConfigurationCommand() : base(0x67) { }
        #endregion GetGlobalConfigurationCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 64; } }
        #endregion ResponseLength

        #region BuildResponse(IEnumerable<byte>)
        internal override Response BuildResponse(IEnumerable<byte> data)
        {
            return new GetGlobalConfigurationResponse(data);
        }
        #endregion BuildResponse
    }
}
