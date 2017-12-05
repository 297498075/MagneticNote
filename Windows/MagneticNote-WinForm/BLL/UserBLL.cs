using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.IO;
using Newtonsoft.Json;
using MagneticNote.Model;

namespace BLL
{
    public class UserBLL : BaseBLL
    {
        public bool Login(User user)
        {
            User value = null;
            if (user.Email != null)
            {
                value = PostValue<UserValue>("User/Get", JsonConvert.SerializeObject(new { Email = user.Email })).User;
            }

            if(value != null && value.Password.Equals(user.Password))
            {
                Info.user = value;
                Info.defaultNoteBook = PostValue<NoteBookValues>("NoteBook/Get", JsonConvert.SerializeObject(new { UserId = value.Id })).NoteBookList;

                return true;
            }

            return false;
        }

        public bool Register(User user)
        {
            return PostResult("Home/SendRegisterEmail", JsonConvert.SerializeObject(new { Email = user.Email, Password = user.Password }));
        }
    }
}
