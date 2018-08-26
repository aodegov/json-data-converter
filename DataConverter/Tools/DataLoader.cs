using System.Collections.Generic;

namespace DataConverter.Tools
{
    public abstract class DataLoader<T>
    {
        public abstract List<T> LoadData(string file);
    }
}
