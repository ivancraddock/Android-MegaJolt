namespace MegaJolt.Communication
{
    public class Bin<T>
    {
        #region Bin(T, T)
        public Bin(T lowValue, T highValue)
        {
            LowValue = lowValue;
            HighValue = highValue;
        }
        #endregion Bin

        #region PROPERTIES
        #region LowValue Property
        public T LowValue { get; internal set; }
        #endregion LowValue
        #region HighValue Property
        public T HighValue { get; internal set; }
        #endregion HighValue
        #endregion PROPERTIES

        #region Equals(object)
        public override bool Equals(object obj)
        {
            Bin<T> that = obj as Bin<T>;
            if (that == null) return false;
            return this.LowValue.Equals(that.LowValue) &&
                   this.HighValue.Equals(that.HighValue);
        }
        #endregion Equals
    }
}
