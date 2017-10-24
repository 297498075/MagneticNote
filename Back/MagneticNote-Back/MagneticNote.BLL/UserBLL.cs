using MagneticNote.IBLL;
using MagneticNote.IDAL;
using MagneticNote.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagneticNote.BLL
{
    public class UserBLL : BaseBLL<User,Model.Entity.User>,IUserBLL
    {
        public IUserDAL UserDAL { get; set; }

        public UserBLL(IUserDAL userDAL):base(userDAL)
        {
            this.UserDAL = userDAL;
        }

        public override User SelectById(int id)
        {
            return EntityToData(UserDAL.Select(u=>u.Id==id).FirstOrDefault());
        }

        public User SelectByEmail(string email)
        {
            return EntityToData(UserDAL.Select(u => u.Email==email).FirstOrDefault());
        }

        public User SelectByEmail(string email, string password)
        {
            return EntityToData(UserDAL.Select(u => u.Email==email && u.Password==password).FirstOrDefault());
        }

        public User SelectByAccount(string account)
        {
            return EntityToData(UserDAL.Select(u => u.Account==account).FirstOrDefault());
        }

        public User SelectByAccount(string account, string password)
        {
            return EntityToData(UserDAL.Select(u => u.Account==account && u.Password==password).FirstOrDefault());
        }

        protected override User EntityToData(Model.Entity.User user)
        {
            User value = new User();
            if(user == null)
            {
                return value;
            }
            value.Id = user.Id;
            value.Account = user.Account;
            value.Email = user.Email;
            value.Password = user.Password;

            return value;
        }

        protected override List<User> EntityToData(List<Model.Entity.User> list)
        {
            List<User> dataList = new List<User>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(EntityToData(list[i]));
            }

            return dataList;
        }

        protected override Model.Entity.User DataToEntity(Model.Data.User user)
        {
            Model.Entity.User value = new Model.Entity.User();
            if (user == null)
            {
                return value;
            }
            value.Id = user.Id;
            value.Account = user.Account;
            value.Email = user.Email;
            value.Password = user.Password;

            return value;
        }

        protected override List<Model.Entity.User> DataToEntity(List<Model.Data.User> list)
        {
            List<Model.Entity.User> dataList = new List<Model.Entity.User>();

            for (int i = 0; i < list.Count(); i++)
            {
                dataList.Add(DataToEntity(list[i]));
            }

            return dataList;
        }
    }
}
