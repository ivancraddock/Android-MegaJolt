namespace MegaJolt.Communication
{
    public enum LoadType
    {
        Unknown,
        TPS,
        MAP,
    }

    public static class LoadTypeExtention
    {
        #region LoadTypeLabel(this)
        public static string LoadTypeLabel(this LoadType loadType)
        {
            return loadType == LoadType.TPS ? "TPS %" : "KPa";
        }
        #endregion LoadTypeLabel
        #region LoadTypeMaximum(this, AspirationType)
        public static byte LoadTypeMaximum(this LoadType loadType, AspirationType aspirationType)
        {
            if (loadType == LoadType.TPS) return 100;
            if (aspirationType == AspirationType.Normal) return 102;
            return 255;
        }
        #endregion LoadTypeMaximum
    }
}
