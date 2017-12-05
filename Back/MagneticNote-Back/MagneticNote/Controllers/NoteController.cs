using MagneticNote.Common;
using MagneticNote.IBLL;
using MagneticNote.Model.Data;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace MagneticNote.Controllers
{
    public class NoteController : Controller
    {
        public INoteBLL NoteBLL { get; set; }

        public NoteController(INoteBLL NoteBLL)
        {
            this.NoteBLL = NoteBLL;
        }

        public ActionResult Get(String UserId, String Id, String Condition, String NoteBookId)
        {
            if (!String.IsNullOrEmpty(UserId))
            {
                ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByUserId(Convert.ToInt32(UserId)));
            }

            if (!String.IsNullOrEmpty(Id))
            {
                ResponseHelper.WriteObject(Response, "Note", NoteBLL.SelectById(Convert.ToInt32(Id)));
            }

            if (!String.IsNullOrEmpty(Condition))
            {
                if (!String.IsNullOrEmpty(NoteBookId))
                {
                    ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByCondition(Condition, Convert.ToInt32(NoteBookId)));
                }
                ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByCondition(Condition));
            }
            else if (!String.IsNullOrEmpty(NoteBookId))
            {
                ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByNoteBookId(Convert.ToInt32(NoteBookId)));
            }
            else
            {

                ResponseHelper.WriteNull(Response);
            }
            
            return null;
        }

        public ActionResult Add(String Note)
        {
            Note note = JsonConvert.DeserializeObject<Note>(Note);
            note.CreateDate = DateTime.Now.GetDateTimeFormats()[2];
            note.UpdateDate = DateTime.Now.GetDateTimeFormats()[2];
            if (note != null)
            {
                if (NoteBLL.Add(note))
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
                if (NoteBLL.Delete(new Note() { Id = Convert.ToInt32(Id) }))
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

        public ActionResult Update(String Note)
        {
            Request.ValidateInput();
            Note note = JsonConvert.DeserializeObject<Note>(Note);
            note.UpdateDate = DateTime.Now.GetDateTimeFormats()[2];
            if (note != null && note.Id != 0)
            {
                if (NoteBLL.Update(note))
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