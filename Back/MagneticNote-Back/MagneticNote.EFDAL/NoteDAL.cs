using MagneticNote.IDAL;
using MagneticNote.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.EFDAL
{
    public class NoteDAL : BaseDAL<Note>, INoteDAL
    {
        public NoteDAL(MagneticNoteContainer context) : base(context)
        {
        }

        public IQueryable<Note> SelectByBookGroupId(int bookGroupId)
        {
            var list = from value in context.Note
                       where value.NoteBook.BookGroupId.Equals(bookGroupId)
                       select value;
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByUserId(int userId)
        {
            var list = from note in context.Note
                       where note.NoteBook.UserId.Equals(userId) || note.NoteBook.BookGroup.UserId.Equals(userId)
                       select note;
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByUserId(int userId, int column)
        {
            var list = from value in context.Note
                       where value.NoteBook.UserId.Equals(userId) || value.NoteBook.BookGroup.UserId.Equals(userId)
                       select value;

            if (list != null)
            {
                return list.Take<Note>(column);
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByCondition(string condition)
        {
            var list = from value in context.Note
                       where value.Title.Contains(condition)
                       select value;
            if (list != null)
            {
                return list;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByCondition(string condition, int noteBookId)
        {
            var list = from value in context.Note
                       where value.Title.Contains(condition) && value.NoteBookId.Equals(noteBookId)
                       select value;
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
