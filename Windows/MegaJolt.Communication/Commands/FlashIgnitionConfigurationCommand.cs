#region USING STATEMENTS

using System;
using System.Collections.Generic;
using MegaJolt.Communication.Responses;
#endregion USING STATEMENTS

namespace MegaJolt.Communication.Commands
{
    public class FlashIgnitionConfigurationCommand : Command
    {
        #region FlashIgnitionConfigurationCommand()
        public FlashIgnitionConfigurationCommand() : base(0x57) { }
        #endregion FlashIgnitionConfigurationCommand

        #region ResponseLength Property
        internal override int ResponseLength { get { return 0; } }
        #endregion ResponseLength

        #region BuildResponse(IEnumerable<byte>)
        internal override Response BuildResponse(IEnumerable<byte> data)
        {
            throw new InvalidOperationException("No response is expected for the Flash Ignition Configuration Command");
        }
        #endregion BuildResponse
    }
}
