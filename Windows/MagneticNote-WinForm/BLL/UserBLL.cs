using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entity;
using Common;
using System.IO;

namespace BLL
{
    public class UserBLL
    {
        public bool AddObject(object obj)
        {      
            return userDAL.AddObject(obj);
        }

        public bool DeleteObjectById(int id)
        {
            return userDAL.DeleteObjectById(id);
        }

        public object SelectObjectById(int id)
        {
            return userDAL.SelectObjectById(id);
        }

        public bool UpdateObject(Object obj)
        {
            return userDAL.UpdateObject(obj);
        }

        public bool Login(User user)
        {
            User value = null;
            if (user.Name != null)
                value = userDAL.SelectObjectByName(user.Name);
            if (user.Email != null)
                value = userDAL.SelectObjectByEmail(user.Email);
            if(value != null && value.Password.Equals(user.Password))
            {
                Info.user = value;
                Info.defaultNoteBook = noteBookDAL.SelectObjectByGroupId(userDefaultBookGroupIdDAL.SelectObjectByUserId(value.Id).DefaultBookGroupId).First<NoteBook>();

                return true;
            }

            return false;
        }

        public bool Register(User user)
        {
            return userDAL.AddUser(user);
        }
    }
}
