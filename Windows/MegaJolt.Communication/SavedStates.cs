#region USING STATEMENTS
using System;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    [Flags]
    public enum SavedStates
    {
        Dirty = 0,
        SavedToFile = 1,
        SavedToController = 2,
        WrittenToFlash = 4,
        Clean = SavedToFile | SavedToController | WrittenToFlash,
    }
}
