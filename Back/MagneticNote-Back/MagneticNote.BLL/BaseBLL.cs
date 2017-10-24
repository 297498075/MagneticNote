using System;
using System.Collections.Generic;
using MagneticNote.IBLL;
using MagneticNote.IDAL;

namespace MagneticNote.BLL
{
    public abstract class BaseBLL<D,E> : IBaseBLL<D> where D:class,new() where E:class,new()
    {
        public IBaseDAL<E> baseDAL { get; set; }

        public BaseBLL(IBaseDAL<E> baseDAL)
        {
            this.baseDAL = baseDAL;
        }

        public bool Add(D obj)
        {
            E value = DataToEntity(obj);
            return baseDAL.Add(value);
        }

        public bool Delete(D obj)
        {
            E value = DataToEntity(obj);
            return baseDAL.Delete(value);
        }

        public abstract D SelectById(int id);

        public bool Update(D obj)
        {
            E value = DataToEntity(obj);
            return baseDAL.Update(value);
        }

        protected abstract D EntityToData(E value);

        protected abstract E DataToEntity(D value);

        protected abstract List<E> DataToEntity(List<D> list);

        protected abstract List<D> EntityToData(List<E> list);
    }
}
