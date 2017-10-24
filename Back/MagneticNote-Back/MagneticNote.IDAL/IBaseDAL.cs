using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.IDAL
{
    public interface IBaseDAL<T> where T:class , new ()
    {
        bool Add(T obj);
        bool Delete(T obj);
        IQueryable<T> Select(Expression<Func<T,bool>> whereLambda);
        IQueryable<T> Select(Expression<Func<T, bool>> whereLambda, int column);
        bool Update(T obj);
    }
}
