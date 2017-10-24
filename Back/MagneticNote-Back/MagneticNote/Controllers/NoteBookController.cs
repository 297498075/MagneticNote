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

        public ActionResult Get()
        {
            String userId = Request["UserId"];
            if(!String.IsNullOrEmpty(userId))
            {
                ResponseHelper.WriteList(Response,"NoteBookList",NoteBookBLL.SelectByUserId(Convert.ToInt32(userId)));
            }

            String bookGroupId = Request["BookGroupId"];
            if(!String.IsNullOrEmpty(bookGroupId))
            {
                ResponseHelper.WriteList(Response,"NoteBookList",NoteBookBLL.SelectByBookGroupId(Convert.ToInt32(bookGroupId)));
            }

            String id = Request["Id"];
            if (!String.IsNullOrEmpty(id))
            {
                ResponseHelper.WriteObject(Response,"NoteBook",NoteBookBLL.SelectById(Convert.ToInt32(id)));
            }
            else
            {
                ResponseHelper.WriteNull(Response);;
            }
            ;
            return null;
        }

        public ActionResult Add()
        {
            NoteBook noteBook = JsonConvert.DeserializeObject<NoteBook>(Request["NoteBook"]);

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

        public ActionResult Delete()
        {
            String id = Request["Id"];
            if (!String.IsNullOrEmpty(id))
            {
                if (NoteBookBLL.Delete(new NoteBook() { Id = Convert.ToInt32(id) }))
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

        public ActionResult Update()
        {
            NoteBook noteBook = JsonConvert.DeserializeObject<NoteBook>(Request["NoteBook"]);

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