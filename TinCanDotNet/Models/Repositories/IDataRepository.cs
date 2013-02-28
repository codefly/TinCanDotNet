using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinCanDotNet.Models.Repositories
{
    public interface IDataRepository<T>
    {
        bool Save(string id, T obj);
        T Get(string id);
    }
}
