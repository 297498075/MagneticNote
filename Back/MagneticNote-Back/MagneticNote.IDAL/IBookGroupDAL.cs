using MagneticNote.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.IDAL
{
    public interface IBookGroupDAL : IBaseDAL<BookGroup>
    {
        IEnumerable<Object> SelectByUserId(int userId, bool containsBook = false);
    }
}
