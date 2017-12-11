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
        IQueryable<Note> SelectByNoteBookId(int notebookId);
        IQueryable<Note> SelectByCondition(String condition);
        IQueryable<Note> SelectByCondition(String condition, int noteBookId);

        IQueryable<Note> SelectByBookGroupIdAndColumn(int bookGroupId, int start, int end);
        IQueryable<Note> SelectByNoteBookIdAndColumn(int notebookId, int start, int end);
        IQueryable<Note> SelectByConditionAndColumn(String condition, int start, int end);
        IQueryable<Note> SelectByConditionAndColumn(String condition, int noteBookId, int start, int end);
        IQueryable<Note> SelectByUserIdAndColumn(int userId, int start, int end);
    }
}
