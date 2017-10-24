using System.Collections.Generic;
using System.Linq;
using MagneticNote.IBLL;
using MagneticNote.IDAL;
using MagneticNote.Model.Data;
using System;

namespace MagneticNote.BLL
{
    public class BookGroupBLL : BaseBLL<BookGroup,Model.Entity.BookGroup>,IBookGroupBLL
    {
        public IBookGroupDAL BookGroupDAL { get; set; }

        public BookGroupBLL(IBookGroupDAL bookGroupDAL) : base(bookGroupDAL)
        {
            this.BookGroupDAL = bookGroupDAL;
        }

        public List<Object> SelectByUserId(int userId, bool containsBook=false)
        {
            if(containsBook == false)
            {
                List<Model.Entity.BookGroup> list = BookGroupDAL.Select(t => t.UserId == userId).ToList();
                if (list != null)
                {
                    return EntityToData(list).ToList<Object>();
                }
            }
            else
            {
                var list = BookGroupDAL.SelectByUserId(userId, true);
                List<Object> content = new List<object>();
                for(IEnumerator<Object> enumerator = list.GetEnumerator(); enumerator.MoveNext();)
                {
                    List<Model.Entity.NoteBook> noteBook = (enumerator.Current.GetType().GetProperty("NoteBookList").GetValue(enumerator.Current) as ICollection<Model.Entity.NoteBook>).ToList<Model.Entity.NoteBook>();
                    
                    if(noteBook != null)
                    {
                        content.Add(new {
                            Id = enumerator.Current.GetType().GetProperty("Id").GetValue(enumerator.Current),
                            UserId = enumerator.Current.GetType().GetProperty("UserId").GetValue(enumerator.Current),
                            Name = enumerator.Current.GetType().GetProperty("Name").GetValue(enumerator.Current),
                            NoteBookList = EntityToData((ICollection<Model.Entity.NoteBook>)enumerator.Current.GetType().GetProperty("NoteBookList").GetValue(enumerator.Current))
                        });
                    }
                }
                return content;
            }
            
           

            return null;
        }

        public override BookGroup SelectById(int id)
        {
            return EntityToData(BookGroupDAL.Select(b => b.Id == id).FirstOrDefault());
        }

        protected override BookGroup EntityToData(Model.Entity.BookGroup bookGroup)
        {
            BookGroup value = new BookGroup();
            if(bookGroup == null)
            {
                return value;
            }
            value.Id = bookGroup.Id;
            value.Name = bookGroup.Name;
            value.UserId = bookGroup.UserId;

            return value;
        }

        protected override List<BookGroup> EntityToData(List<Model.Entity.BookGroup> list)
        {
            List<BookGroup> dataList = new List<BookGroup>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(EntityToData(list[i]));
            }

            return dataList;
        }

        protected override Model.Entity.BookGroup DataToEntity(Model.Data.BookGroup bookGroup)
        {
            Model.Entity.BookGroup value = new Model.Entity.BookGroup();
            if (bookGroup == null)
            {
                return value;
            }
            value.Id = bookGroup.Id;
            value.Name = bookGroup.Name;
            value.UserId = bookGroup.UserId;

            return value;
        }

        protected override List<Model.Entity.BookGroup> DataToEntity(List<Model.Data.BookGroup> list)
        {
            List<Model.Entity.BookGroup> dataList = new List<Model.Entity.BookGroup>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(DataToEntity(list[i]));
            }

            return dataList;
        }

        protected NoteBook EntityToData(Model.Entity.NoteBook noteBook)
        {
            NoteBook value = new NoteBook();
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

        protected List<NoteBook> EntityToData(ICollection<Model.Entity.NoteBook> list)
        {
            List<NoteBook> dataList = new List<NoteBook>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(EntityToData(list.ElementAt(i)));
            }

            return dataList;
        }
    }
}
