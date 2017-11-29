﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model.MagneticNote;

namespace BLL
{
    public class NoteBLL
    {
        private NoteDAL noteDAL = new NoteDAL();
        private NoteBookDAL noteBookDAL = new NoteBookDAL();

        public bool AddObject(object obj)
        {
            if (noteDAL.AddObject(obj))
                return true;
            else
                return false;
        }

        public bool DeleteObjectById(int id)
        {
            if (noteDAL.DeleteObjectById(id))
                return true;
            else
                return false;
        }

        public object SelectObjectById(int id)
        {
            return noteDAL.SelectObjectById(id);
        }

        public Note CreateNote(int noteBookId)
        {
            Note note = new Note() { Content = String.Empty, Title = String.Empty, CreateDate = DateTime.Now, NoteBookId = noteBookId, UpdateDate = DateTime.Now };

            return AddObject(note) == true ? note : null;
        }

        public bool UpdateObject(Note note)
        {
            return noteDAL.UpdateObject(note);
        }

        public List<Note> SelectObjectByNoteBookId(int Id)
        {
            return noteDAL.SelectObjectByNoteBookId(Id);
        }

        public List<Note> SelectObjectByBookGroupId(int id)
        {
            return noteDAL.SelectObjectByBookGroupId(id);
        }

        public List<Note> SelectAllObject()
        {
            return noteDAL.SelectAllObject();
        }
    }
}
