#region USING STATEMENTS
using System.Collections.Generic;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    public class GetIgnitionConfigurationCommand : Command
    {
        #region GetIgnitionConfigurationCommand()
        public GetIgnitionConfigurationCommand() : base(0x43) { }
        #endregion GetIgnitionConfigurationCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 150; } }
        #endregion ResponseLength

        #region BuildResponse(IEnumerable<byte>)
        internal override Response BuildResponse(IEnumerable<byte> data)
        {
            return new GetIgnitionConfigurationResponse(data);
        }
        #endregion BuildResponse
    }
}
