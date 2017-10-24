using MagneticNote.IDAL;
using MagneticNote.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.EFDAL
{
    public class BookGroupDAL : BaseDAL<BookGroup>, IBookGroupDAL
    {
        public BookGroupDAL(MagneticNoteContainer context) : base(context)
        {
        }

        public IEnumerable<Object> SelectByUserId(int userId, bool containsBook = false)
        {
            var list = from bookGroup in context.BookGroup
                       where bookGroup.UserId == userId
                       select new { Id=bookGroup.Id, Name=bookGroup.Name, UserId=bookGroup.UserId, NoteBookList=bookGroup.NoteBook };
            
            if(list != null)
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
