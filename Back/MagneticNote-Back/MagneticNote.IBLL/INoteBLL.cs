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
        List<Note> SelectByUserIdAndColumn(int userId, int start, int end);
        List<Note> SelectByConditionAndColumn(String Condition, int start, int end);
        List<Note> SelectByConditionAndColumn(String Condition, int noteBookId, int start, int end);
        List<Note> SelectByNoteBookIdAndColumn(int noteBookId, int start, int end);
        List<Note> SelectByBookGroupIdAndColumn(int bookGroupId, int start, int end);
    }
}
