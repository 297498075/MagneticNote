using MagneticNote.Model.Data;
using System;
using System.Collections.Generic;

namespace MagneticNote.IBLL
{
    public interface INoteBLL : IBaseBLL<Note>
    {
        List<Note> SelectByNoteBookId(int noteBookId);
        List<Note> SelectByBookGroupId(int bookGroupId);
        List<Note> SelectByUserId(int userId);
        List<Note> SelectByCondition(String Condition);
        List<Note> SelectByCondition(String Condition, int noteBookId);
        List<Note> SelectByUserId(int userId, int column);
    }
}
