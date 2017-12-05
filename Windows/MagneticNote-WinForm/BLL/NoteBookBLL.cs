using System;
using System.Collections.Generic;
using Common;
using MagneticNote.Model;
using Newtonsoft.Json;

namespace BLL
{
    public class NoteBookBLL : BaseBLL
    {
        public bool AddObject(NoteBook obj)
        {
            return PostResult("NoteBook/Add", JsonConvert.SerializeObject(new { NoteBook = obj }));
        }

        public bool DeleteObjectById(int id)
        {
            IList<Note> list = PostValues<NoteValues>("Note/Get", JsonConvert.SerializeObject(new { NoteBookId = id })).NoteList;

            foreach (Note note in list)
            {
                note.NoteBookId = Info.defaultNoteBook[0].Id;
                PostResult("Note/Update", JsonConvert.SerializeObject(new { Note = note }));
            }

            return PostResult("NoteBook/Delete", JsonConvert.SerializeObject(new { Id = id }));
        }

        public NoteBook SelectObjectById(int id)
        {
            return PostValue<NoteBookValue>("NoteBook/Get", JsonConvert.SerializeObject(new { Id = id })).NoteBook;
        }

        public bool UpdateObject(NoteBook obj)
        {
            return PostResult("NoteBook/Update", JsonConvert.SerializeObject(new { NoteBook = obj }));
        }

        public IList<NoteBook> SelectObjectByGroupId(int id)
        {
            return PostValues<NoteBookValues>("NoteBook/Get", JsonConvert.SerializeObject(new { BookGroupId = id })).NoteBookList;
        }

        public IList<NoteBook> SelectAllObject()
        {
            return PostValues<NoteBookValues>("Home/GetAllNoteBook", JsonConvert.SerializeObject(new { UserId = Common.Info.user.Id })).NoteBookList;
        }

        public int SelectCountById(int id)
        {
            return PostValues<NoteValues>("Note/Get", JsonConvert.SerializeObject(new { NoteBookId = id })).NoteList.Count;
        }
    }
}
