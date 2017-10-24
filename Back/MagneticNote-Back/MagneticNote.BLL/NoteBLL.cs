using MagneticNote.IBLL;
using MagneticNote.IDAL;
using MagneticNote.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagneticNote.BLL
{
    public class NoteBLL : BaseBLL<Note,Model.Entity.Note>, INoteBLL
    {
        public INoteDAL NoteDAL { get; set; }

        public NoteBLL(INoteDAL noteDAL) : base(noteDAL)
        {
            this.NoteDAL = noteDAL;
        }

        public override Note SelectById(int id)
        {
            return EntityToData(NoteDAL.Select(u=>u.Id==id).FirstOrDefault());
        }

        public List<Note> SelectByNoteBookId(int noteBookId)
        {
            List<Model.Entity.Note> list = NoteDAL.Select(n => n.NoteBookId==noteBookId).ToList();
            if (list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        public List<Note> SelectByBookGroupId(int bookGroupId)
        {
            List<Model.Entity.Note> list = NoteDAL.SelectByBookGroupId(bookGroupId).ToList();
            if (list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        public List<Note> SelectByUserId(int userId)
        {
            List<Model.Entity.Note> list = NoteDAL.SelectByUserId(userId).ToList();
            if (list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        public List<Note> SelectByUserId(int userId, int column)
        {
            List<Model.Entity.Note> list = NoteDAL.SelectByUserId(userId, column).ToList();
            if (list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        public List<Note> SelectByCondition(string Condition)
        {
            List<Model.Entity.Note> list = NoteDAL.SelectByCondition(Condition).ToList();
            if (list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        public List<Note> SelectByCondition(string Condition, int noteBookId)
        {
            List<Model.Entity.Note> list = NoteDAL.SelectByCondition(Condition, noteBookId).ToList();
            if(list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        protected override Note EntityToData(Model.Entity.Note note)
        {
            Note value = new Note();
            if(note == null)
            {
                return value;
            }
            value.Id = note.Id;
            value.Content = note.Content;
            value.CreateDate = note.CreateDate;
            value.UpdateDate = note.UpdateDate;
            value.Title = note.Title;
            value.NoteBookId = note.NoteBookId;

            return value;
        }

        protected override List<Note> EntityToData(List<Model.Entity.Note> list)
        {
            List<Note> dataList = new List<Note>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(EntityToData(list[i]));
            }

            return dataList;
        }

        protected override Model.Entity.Note DataToEntity(Note note)
        {
            Model.Entity.Note value = new Model.Entity.Note();
            if (note == null)
            {
                return value;
            }
            value.Id = note.Id;
            value.Content = note.Content;
            value.CreateDate = note.CreateDate;
            value.UpdateDate = note.UpdateDate;
            value.Title = note.Title;
            value.NoteBookId = note.NoteBookId;

            return value;
        }

        protected override List<Model.Entity.Note> DataToEntity(List<Note> list)
        {
            List<Model.Entity.Note> dataList = new List<Model.Entity.Note>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(DataToEntity(list[i]));
            }

            return dataList;
        }
    }
}
