using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MagneticNote.Model;
using Newtonsoft.Json;

namespace BLL
{
    public class BookGroupBLL : BaseBLL
    {
        public bool AddObject(object obj)
        {
            if (this.PostResult("BookGroup/Add", JsonConvert.SerializeObject(new { BookGroup = obj})))
                return true;
            else
                return false;
        }

        public bool DeleteObjectById(int id)
        {
            IList<NoteBook> list = PostValues<NoteBookValues>("NoteBook/Get", JsonConvert.SerializeObject(new { BookGroupId = id })).NoteBookList;

            if (list.Count > 0)
            { 
                foreach (NoteBook noteBook in list)
                {
                    noteBook.BookGroupId = Info.defaultNoteBook[0].BookGroupId;
                    PostResult("NoteBook/Update", JsonConvert.SerializeObject(new { NoteBook = noteBook}));
                }
            }

            if (PostResult("BookGroup/Delete", JsonConvert.SerializeObject(new { Id = id })))
                return true;
            else
                return false;
        }

        public IList<BookGroup> SelectObjectsByUserId(int id)
        {
            return PostValues<BookGroupValues>("BookGroup/Get", JsonConvert.SerializeObject(new { UserId = id })).BookGroupList;
        }

        public BookGroup SelectObjectById(int id)
        {
            return PostValue<BookGroupValue>("BookGroup/Get", JsonConvert.SerializeObject(new { Id = id })).BookGroup;
        }

        public bool UpdateObject(Object obj)
        {
            return PostResult("BookGroup/Update", JsonConvert.SerializeObject(new { BookGroup = obj }));
        }
    }
}
