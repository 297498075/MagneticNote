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

        public IQueryable<Note> SelectByNoteBookId(int notebookId)
        {
            var list = from note in context.Note
                       where note.NoteBookId.Equals(notebookId)
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

        public IQueryable<Note> SelectByBookGroupIdAndColumn(int bookGroupId, int start, int end)
        {
            var list = from note in context.Note
                       where note.NoteBook.BookGroupId.Equals(bookGroupId)
                       select note;
            if (list != null)
            {
                return list.Skip(start).Take(end);
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByNoteBookIdAndColumn(int notebookId, int start, int end)
        {
            var list = from note in context.Note
                       where note.NoteBookId.Equals(notebookId)
                       select note;
            if (list != null)
            {
                return list.Skip(start).Take(end);
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByConditionAndColumn(string condition, int start, int end)
        {
            var list = from value in context.Note
                       where value.Title.Contains(condition)
                       select value;
            if (list != null)
            {
                return list.Skip(start).Take(end);
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByConditionAndColumn(string condition, int noteBookId, int start, int end)
        {
            var list = from value in context.Note
                       where value.NoteBookId == noteBookId && value.Title.Contains(condition)
                       select value;
            if (list != null)
            {
                return list.Skip(start).Take(end);
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Note> SelectByUserIdAndColumn(int userId, int start, int end)
        {
            var list = from value in context.Note
                       where value.NoteBook.BookGroup.UserId == userId && value.NoteBook.UserId == userId
                       select value;
            if (list != null)
            {
                return list.Skip(start).Take(end);
            }
            else
            {
                return null;
            }
        }
    }
}
