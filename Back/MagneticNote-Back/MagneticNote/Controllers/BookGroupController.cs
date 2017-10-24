using MagneticNote.Common;
using MagneticNote.IBLL;
using MagneticNote.Model.Data;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace MagneticNote.Controllers
{
    public class BookGroupController : Controller
    {
        
        public IBookGroupBLL BookGroupBLL { get; set; }

        public BookGroupController(IBookGroupBLL bookGroupBLL)
        {
            this.BookGroupBLL = bookGroupBLL;
        }

        public ActionResult Get()
        {
            String userId = Request["UserId"];
            if (!String.IsNullOrEmpty(userId))
            {
                ResponseHelper.WriteList(Response,"BookGroupList",BookGroupBLL.SelectByUserId(Convert.ToInt32(userId)));
                ;
            }

            String bookGroupId = Request["BookGroupId"];
            if (!String.IsNullOrEmpty(bookGroupId))
            {
                ResponseHelper.WriteObject(Response,"BookGroup",BookGroupBLL.SelectById(Convert.ToInt32(bookGroupId)));
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
            BookGroup bookGroup = JsonConvert.DeserializeObject<BookGroup>(Request["BookGroup"]);

            if (bookGroup != null)
            {
                if (BookGroupBLL.Add(bookGroup))
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
                if (BookGroupBLL.Delete(new BookGroup() { Id = Convert.ToInt32(id) }))
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
            BookGroup bookGroup = JsonConvert.DeserializeObject<BookGroup>(Request["BookGroup"]);

            if (bookGroup != null)
            {
                if (BookGroupBLL.Update(bookGroup))
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
                ResponseHelper.WriteNull(Response);
            }
    
            return null;
        }
    }
}