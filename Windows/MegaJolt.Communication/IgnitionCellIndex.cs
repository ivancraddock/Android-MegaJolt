namespace MegaJolt.Communication
{
    public class IgnitionCellIndex
    {
        #region IgnitionCellIndex(byte, byte)
        public IgnitionCellIndex(byte loadBin, byte rpmBin)
        {
            LoadBin = loadBin;
            RpmBin = rpmBin;
        }
        #endregion IgnitionCellIndex

        #region PROPERTIES
        #region LoadBin Property
        public byte LoadBin { get; private set; }
        #endregion LoadBin
        #region RpmBin Property
        public byte RpmBin { get; private set; }
        #endregion RpmBin
        #region MapIndex Property
        internal int MapIndex { get { return (LoadBin * 10) + RpmBin; } }
        #endregion MapIndex
        #endregion PROPERTIES

        #region Equals(object)
        public override bool Equals(object obj)
        {
            IgnitionCellIndex that = obj as IgnitionCellIndex;
            if (that == null) return false;
            return this.LoadBin == that.LoadBin &&
                   this.RpmBin == that.RpmBin;
        }
        #endregion Equals
    }
}
