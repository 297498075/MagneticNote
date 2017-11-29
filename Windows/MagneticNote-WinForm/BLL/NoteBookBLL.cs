using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model.MagneticNote;
using Common;

namespace BLL
{
    public class NoteBookBLL
    {
        private NoteBookDAL noteBookDAL = new NoteBookDAL();
        private NoteDAL noteDAL = new NoteDAL();

        public bool AddObject(NoteBook obj)
        {
            return noteBookDAL.AddObject(obj);
        }

        public bool DeleteObjectById(int id)
        {
            List<Note> list = noteDAL.SelectObjectByNoteBookId(id);

            foreach(Note note in list)
            {
                note.NoteBookId = Info.defaultNoteBook.Id;
                noteDAL.UpdateObject(note);
            }

            return noteBookDAL.DeleteObjectById(id);
        }

        public object SelectObjectById(int id)
        {
            return noteBookDAL.SelectObjectById(id);
        }

        public bool UpdateObject(NoteBook obj)
        {
            return noteBookDAL.UpdateObject(obj);
        }

        public NoteBook SelectObjectByName(string v)
        {
            return noteBookDAL.SelectObjectByName(v);
        }

        public IEnumerable<NoteBook> SelectObjectByGroupId(int id)
        {
            return noteBookDAL.SelectObjectByGroupId(id);
        }

        public List<NoteBook> SelectAllObject()
        {
            return noteBookDAL.SelectAllObject();
        }

        public int SelectCountById(int id)
        {
            return noteBookDAL.SelectCountById(id);
        }
    }
}
