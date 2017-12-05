using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagneticNote.Model;
using Newtonsoft.Json;

namespace BLL
{
    public class NoteBLL : BaseBLL
    {
        public bool AddObject(object obj)
        {
            return PostResult("Note/Add", JsonConvert.SerializeObject(new { Note = obj}));
        }

        public bool DeleteObjectById(int id)
        {
            return PostResult("Note/Delete", JsonConvert.SerializeObject(new { Id = id }));
        }

        public Note SelectObjectById(int id)
        {
            return PostValue<NoteValue>("Note/Get", JsonConvert.SerializeObject(new { Id = id })).Note;
        }

        public bool UpdateObject(Note note)
        {
            return PostResult("Note/Update", JsonConvert.SerializeObject(new { Note = note }));
        }

        public IList<Note> SelectObjectByNoteBookId(int Id)
        {
            return PostValues<NoteValues>("Note/Get", JsonConvert.SerializeObject(new { NoteBookId = Id })).NoteList;
        }

        public IList<Note> SelectObjectByBookGroupId(int id)
        {
            return PostValues<NoteValues>("Note/Get", JsonConvert.SerializeObject(new { BookGroupId = id })).NoteList;
        }

        public IList<Note> SelectAllObject()
        {
            return PostValues<NoteValues>("Note/Get", JsonConvert.SerializeObject(new { UserId = Common.Info.user.Id})).NoteList;
        }

    }
}
