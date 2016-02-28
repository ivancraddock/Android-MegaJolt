#region USING STATEMENTS
using System.Collections;
using System.Collections.Generic;
#endregion USING STATEMENTS

namespace MegaJolt.Communication
{
    public class Bins<T> : IEnumerator<Bin<T>>, IEnumerable<Bin<T>>
    {
        #region Bins()
        public Bins()
        {
            BinCollection = new List<Bin<T>>(10);
            int index = 0;
            while (index < 10)
            {
                BinCollection.Add(new Bin<T>(default(T), default(T)));
                index++;
            }
            CurrentIndex = -1;
        }
        #endregion Bins

        #region PROPERTIES
        #region BinCollection Property
        private List<Bin<T>> BinCollection { get; set; }
        #endregion BinCollection
        #region Indexer Property
        public Bin<T> this[int index] { get { return BinCollection[index]; } }
        #endregion Indexer
        #region CurrentIndex Property
        private int CurrentIndex { get; set; }
        #endregion CurrentIndex
        #region Current Property
        public Bin<T> Current
        {
            get { return BinCollection[CurrentIndex]; }
        }
        #endregion Current
        #region IEnumerator.Current Property
        object IEnumerator.Current
        {
            get { return Current; }
        }
        #endregion IEnumerator.Current
        #endregion PROPERTIES

        #region METHODS
        #region Dispose()
        public void Dispose()
        {
            
        }
        #endregion Dispose
        #region MoveNext()
        public bool MoveNext()
        {
            if (CurrentIndex == BinCollection.Count - 1) return false;
            CurrentIndex++;
            return true;
        }
        #endregion MoveNext
        #region Reset()
        public void Reset()
        {
            CurrentIndex = -1;
        }
        #endregion Reset
        #region GetEnumerator()
        public IEnumerator<Bin<T>> GetEnumerator()
        {
            Reset();
            return this;
        }
        #endregion GetEnumerator
        #region IEnumerable.GetEnumerator()
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion IEnumerable.GetEnumerator
        #endregion METHODS
    }
}
