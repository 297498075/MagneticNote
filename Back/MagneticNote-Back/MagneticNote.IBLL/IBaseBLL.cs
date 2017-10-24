using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.IBLL
{
    public interface IBaseBLL<T> where T : class, new()
    {
        bool Add(T obj);
        bool Delete(T obj);
        T SelectById(int id);
        bool Update(T obj);
    }
}
