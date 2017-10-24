using MagneticNote.IDAL;
using MagneticNote.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.EFDAL
{
    public class NoteBookDAL : BaseDAL<NoteBook>,INoteBookDAL
    {
        public NoteBookDAL(MagneticNoteContainer context) : base(context)
        {
        }

        public int SelectNoteCountById(int id)
        {
            var list = from note in context.Note
                       where note.NoteBookId.Equals(id)
                       select note.Id;
            if (list != null)
            {
                return list.Count();
            }
            else
            {
                return 0;
            }
        }

        public IQueryable<NoteBook> SelectByUserId(int userId, bool isALL = false)
        {
            IQueryable<NoteBook> list = null;
            
            if(isALL == false)
            {
                list = from noteBook in context.NoteBook
                       where noteBook.UserId == userId
                       select noteBook;
            }
            else
            {
                list = from noteBook in context.NoteBook
                       join bookGroup in context.BookGroup on userId equals bookGroup.UserId
                       where noteBook.UserId == userId || bookGroup.Id == noteBook.BookGroupId
                       select noteBook;
            }

            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
