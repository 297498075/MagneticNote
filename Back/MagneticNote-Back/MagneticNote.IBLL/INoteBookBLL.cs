using MagneticNote.Model.Data;
using System;
using System.Collections.Generic;

namespace MagneticNote.IBLL
{
    public interface INoteBookBLL : IBaseBLL<NoteBook>
    {
        List<NoteBook> SelectByUserId(int userId, bool isAll=false);
        List<NoteBook> SelectByBookGroupId(int bookGroupId);
        int SelectNoteCountById(int id);
    }
}
