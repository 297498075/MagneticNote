using MagneticNote.Common;
using MagneticNote.IBLL;
using MagneticNote.Model.Data;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MagneticNote.Controllers
{
    public class NoteBookController : Controller
    {
        public INoteBookBLL NoteBookBLL { get; set; }

        public NoteBookController(INoteBookBLL NoteBookBLL)
        {
            this.NoteBookBLL = NoteBookBLL;
        }

        public ActionResult Get(String UserId, String BookGroupId, String Id)
        {
            if(!String.IsNullOrEmpty(UserId))
            {
                ResponseHelper.WriteList(Response,"NoteBookList",NoteBookBLL.SelectByUserId(Convert.ToInt32(UserId)));
            }

            if(!String.IsNullOrEmpty(BookGroupId))
            {
                ResponseHelper.WriteList(Response,"NoteBookList",NoteBookBLL.SelectByBookGroupId(Convert.ToInt32(BookGroupId)));
            }

            if (!String.IsNullOrEmpty(Id))
            {
                ResponseHelper.WriteObject(Response,"NoteBook",NoteBookBLL.SelectById(Convert.ToInt32(Id)));
            }
            else
            {
                ResponseHelper.WriteNull(Response);;
            }
            ;
            return null;
        }

        [ValidateInput(false)]
        public ActionResult Add(String NoteBook)
        {
            NoteBook noteBook = JsonConvert.DeserializeObject<NoteBook>(NoteBook);

            if (noteBook != null)
            {
                if (NoteBookBLL.Add(noteBook))
                {
                    ResponseHelper.WriteTrue(Response);
                }
                else
                {
                    ResponseHelper.WriteFalse(Response);
                }
            }
            else
            {
                ResponseHelper.WriteNull(Response);;
            }
            ;
            return null;
        }

        public ActionResult Delete(String Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                if (NoteBookBLL.Delete(new NoteBook() { Id = Convert.ToInt32(Id) }))
                {
                    ResponseHelper.WriteTrue(Response);
                }
                else
                {
                    ResponseHelper.WriteFalse(Response);
                }
            }
            else
            {
                ResponseHelper.WriteNull(Response);;
            }
            ;
            return null;
        }

        [ValidateInput(false)]
        public ActionResult Update(String NoteBook)
        {
            NoteBook noteBook = JsonConvert.DeserializeObject<NoteBook>(NoteBook);

            if (noteBook != null && noteBook.Id != 0)
            {
                if (NoteBookBLL.Update(noteBook))
                {
                    ResponseHelper.WriteTrue(Response);
                }
                else
                {
                    ResponseHelper.WriteFalse(Response);
                }
            }
            else
            {
                ResponseHelper.WriteNull(Response);;
            }
            ;
            return null;
        }
    }
}