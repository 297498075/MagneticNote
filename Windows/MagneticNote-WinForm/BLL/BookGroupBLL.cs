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
    public class BookGroupBLL
    {
        private BookGroupDAL bookGroupDAL = new BookGroupDAL();
        private NoteBookDAL noteBookDAL = new NoteBookDAL();

        public bool AddObject(object obj)
        {
            if (bookGroupDAL.AddObject(obj))
                return true;
            else
                return false;
        }

        public bool DeleteObjectById(int id)
        {
            List<NoteBook> list = noteBookDAL.SelectObjectByGroupId(id);

            if (list.Count > 0)
            { 
                foreach (NoteBook noteBook in list)
                {
                    noteBook.BookGroupId = Info.defaultNoteBook.BookGroupId;
                    noteBookDAL.UpdateObject(noteBook);
                }
            }

            if (bookGroupDAL.DeleteObjectById(id))
                return true;
            else
                return false;
        }

        public List<BookGroup> SelectObjectsByUserId(int id)
        {
            return bookGroupDAL.SelectObjectsByUserId(id);
        }

        public object SelectObjectById(int id)
        {
            return bookGroupDAL.SelectObjectById(id);
        }

        public bool UpdateObject(Object obj)
        {
            return bookGroupDAL.UpdateObject(obj);
        }
    }
}
