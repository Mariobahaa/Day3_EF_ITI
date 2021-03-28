using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.EntityManagers
{
    interface IManager<T>
    {
        bool Add(T item);
        bool Delete(string ID);
        List<T> GetAll();
        //T GetByID(long ID);
        bool Update(T item);
    }
}
