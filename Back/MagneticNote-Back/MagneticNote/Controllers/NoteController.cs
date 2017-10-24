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

        public ActionResult Get()
        {
            String userId = Request["UserId"];
            if (!String.IsNullOrEmpty(userId))
            {
                ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByUserId(Convert.ToInt32(userId)));
                ;
            }

            String noteId = Request["Id"];
            if (!String.IsNullOrEmpty(noteId))
            {
                ResponseHelper.WriteObject(Response, "Note", NoteBLL.SelectById(Convert.ToInt32(noteId)));
                ;
            }

            String condition = Request["Condition"];
            String noteBookId = Request["NoteBookId"];
            if (!String.IsNullOrEmpty(condition))
            {
                if (!String.IsNullOrEmpty(noteBookId))
                {
                    ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByCondition(condition, Convert.ToInt32(noteBookId)));
                }
                ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByCondition(condition));
            }
            else if (!String.IsNullOrEmpty(noteBookId))
            {
                ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByNoteBookId(Convert.ToInt32(noteBookId)));
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
            Note note = JsonConvert.DeserializeObject<Note>(Request["Note"]);
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

        public ActionResult Delete()
        {
            String id = Request["Id"];
            if (!String.IsNullOrEmpty(id))
            {
                if (NoteBLL.Delete(new Note() { Id = Convert.ToInt32(id) }))
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
            Note note = JsonConvert.DeserializeObject<Note>(Request["Note"]);
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