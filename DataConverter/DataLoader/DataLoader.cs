using System.Collections.Generic;

namespace DataConverter.DataLoader
{
    public abstract class DataLoader<T>
    {
        public abstract List<T> LoadData(string file);
    }
}
