using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TinCanDotNet.Models.Repositories
{
    public class MemoryRepository<T> : IDataRepository<T>
    {
        private readonly Dictionary<string, T> _data;

        public MemoryRepository(){
            _data = new Dictionary<string, T>();
        }

        public bool Save(string id, T obj)
        {
            _data.Add(id, obj);
            return true;
        }

        public T Get(string id)
        {
            return _data[id];
        }
    }
}