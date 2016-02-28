#region USING STATEMENTS
using System.Collections.Generic;
using System.IO;

#endregion USING STATEMENTS

namespace MegaJolt.Communication.WindowsPC
{
    public class FileStorage : IPersistedStorage
    {
        #region ReadAll(string)
        public IEnumerable<string> ReadAll(string location)
        {
            List<string> fileContents = new List<string>();
            using (StreamReader reader = new StreamReader(location))
            {
                while (!reader.EndOfStream)
                {
                    fileContents.Add(reader.ReadLine());
                }
            }
            return fileContents;
        }
        #endregion ReadAll
        #region Write(string, string)
        public void Write(string location, string data)
        {
            using (StreamWriter writer = new StreamWriter(location))
            {
                writer.Write(data);
            }
        }
        #endregion Write
    }
}
