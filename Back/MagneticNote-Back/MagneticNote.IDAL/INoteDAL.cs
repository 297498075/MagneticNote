using MagneticNote.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagneticNote.IDAL
{
    public interface INoteDAL : IBaseDAL<Note>
    {
        IQueryable<Note> SelectByBookGroupId(int bookGroupId);
        IQueryable<Note> SelectByUserId(int userId);
        IQueryable<Note> SelectByCondition(String condition);
        IQueryable<Note> SelectByCondition(String condition, int noteBookId);
        IQueryable<Note> SelectByUserId(int userId, int column);
    }
}
