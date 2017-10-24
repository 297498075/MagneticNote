using MagneticNote.Model.Data;
using System;
using System.Collections.Generic;

namespace MagneticNote.IBLL
{
    public interface IBookGroupBLL : IBaseBLL<BookGroup>
    {
        List<Object> SelectByUserId(int userId, bool containsBook=false);
    }
}
