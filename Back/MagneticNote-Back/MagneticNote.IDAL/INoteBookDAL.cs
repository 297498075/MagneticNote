using MagneticNote.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.IDAL
{
    public interface INoteBookDAL : IBaseDAL<NoteBook>
    {
        IQueryable<NoteBook> SelectByUserId(int userId, bool isALL = false);
        int SelectNoteCountById(int id);
    }
}
