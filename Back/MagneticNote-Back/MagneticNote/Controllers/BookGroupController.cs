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

        public ActionResult Get(String UserId, String BookGroupId)
        {
            if (!String.IsNullOrEmpty(UserId))
            {
                ResponseHelper.WriteList(Response,"BookGroupList",BookGroupBLL.SelectByUserId(Convert.ToInt32(UserId)));
            }

            if (!String.IsNullOrEmpty(BookGroupId))
            {
                ResponseHelper.WriteObject(Response,"BookGroup",BookGroupBLL.SelectById(Convert.ToInt32(BookGroupId)));
            }
            else
            {
                ResponseHelper.WriteNull(Response);
            }
            ;
            return null;
        }

        [ValidateInput(false)]
        public ActionResult Add(String BookGroup)
        {
            BookGroup bookGroup = JsonConvert.DeserializeObject<BookGroup>(BookGroup);

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
                ResponseHelper.WriteNull(Response);
            }
            ;
            return null;
        }

        public ActionResult Delete(String Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                if (BookGroupBLL.Delete(new BookGroup() { Id = Convert.ToInt32(Id) }))
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
        public ActionResult Update(String BookGroup)
        {
            BookGroup bookGroup = JsonConvert.DeserializeObject<BookGroup>(BookGroup);

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