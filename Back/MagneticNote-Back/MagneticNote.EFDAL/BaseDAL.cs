using MagneticNote.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MagneticNote.Model.Entity;
using System.Linq.Expressions;

namespace MagneticNote.EFDAL
{
    public abstract class BaseDAL<T> : IBaseDAL<T> where T:class , new ()
    {
        public MagneticNoteContainer context { get; set; }

        public BaseDAL(MagneticNoteContainer context)
        {
            this.context = context;
        }

        public bool Add(T obj)
        {
            context.Set<T>().Add(obj);
            context.SaveChanges();

            return true;
    }

        public bool Delete(T obj)
        {
            context.Set<T>().Attach(obj);
            context.Set<T>().Remove(obj);
            context.SaveChanges();

            return true;
        }

        public bool Update(T obj)
        {
            context.Set<T>().Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
            context.SaveChanges();

            return true;
        }

        public IQueryable<T> Select(Expression<Func<T, bool>> whereLambda)
        {
            return context.Set<T>().Where(whereLambda);
        }

        public IQueryable<T> Select(Expression<Func<T, bool>> whereLambda, int column)
        {
            return context.Set<T>().Where(whereLambda).Take(column);
        }
    }
}
