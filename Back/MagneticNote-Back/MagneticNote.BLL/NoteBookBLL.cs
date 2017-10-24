using MagneticNote.IBLL;
using MagneticNote.IDAL;
using MagneticNote.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagneticNote.BLL
{
    public class NoteBookBLL : BaseBLL<NoteBook,Model.Entity.NoteBook>,INoteBookBLL
    {
        public INoteBookDAL NoteBookDAL { get; set; }
        public NoteBookBLL(INoteBookDAL noteBookDAL):base(noteBookDAL)
        {
            this.NoteBookDAL = noteBookDAL;
        }

        public override NoteBook SelectById(int id)
        {
            return EntityToData(NoteBookDAL.Select(n=>n.Id==id).FirstOrDefault());
        }

        public List<NoteBook> SelectByUserId(int userId, bool isALL=false)
        {

            List<Model.Entity.NoteBook> list = null;
            if(isALL == false)
            {
                list = NoteBookDAL.SelectByUserId(userId).ToList();
            }
            else
            {
                list = NoteBookDAL.SelectByUserId(userId, true).ToList();
                
            }

            if (list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        public List<NoteBook> SelectByBookGroupId(int bookGroupId)
        {
            List<Model.Entity.NoteBook> list = NoteBookDAL.Select(n => n.BookGroupId== bookGroupId).ToList();
            if (list != null)
            {
                return EntityToData(list);
            }

            return null;
        }

        public int SelectNoteCountById(int id)
        {
            return NoteBookDAL.SelectNoteCountById(id);
        }

        protected override NoteBook EntityToData(Model.Entity.NoteBook noteBook)
        {
            NoteBook value = new NoteBook();
            if(noteBook == null)
            {
                return value;
            }
            value.Id = noteBook.Id;
            value.Name = noteBook.Name;
            value.UserId = noteBook.UserId;
            value.BookGroupId = noteBook.BookGroupId;

            return value;
        }

        protected override List<NoteBook> EntityToData(List<Model.Entity.NoteBook> list)
        {
            List<NoteBook> dataList = new List<NoteBook>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(EntityToData(list[i]));
            }

            return dataList;
        }

        protected override Model.Entity.NoteBook DataToEntity(NoteBook noteBook)
        {
            Model.Entity.NoteBook value = new Model.Entity.NoteBook();
            if (noteBook == null)
            {
                return value;
            }
            value.Id = noteBook.Id;
            value.Name = noteBook.Name;
            value.UserId = noteBook.UserId;
            value.BookGroupId = noteBook.BookGroupId;

            return value;
        }

        protected override List<Model.Entity.NoteBook> DataToEntity(List<NoteBook> list)
        {
            List<Model.Entity.NoteBook> dataList = new List<Model.Entity.NoteBook>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(DataToEntity(list[i]));
            }

            return dataList;
        }
    }
}
