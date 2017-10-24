using MagneticNote.IDAL;
using MagneticNote.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.EFDAL
{
    public class UserDAL : BaseDAL<User>, IUserDAL
    {
        public UserDAL(MagneticNoteContainer context)
            : base(context)
        {
        }

        public User SelectByEmail(string email)
        {
            var user = from value in context.User
                       where value.Email.Equals(email)
                       select value;
            if (user != null && user.Count<User>() != 0)
            {
                return user.FirstOrDefault<User>();
            }
            else
            {
                return null;
            }
        }

        public User SelectByEmail(string email, string password)
        {
            var user = from value in context.User
                       where value.Email.Equals(email) && value.Password.Equals(password)
                       select value;
            if (user != null && user.Count<User>() != 0)
            {
                return user.FirstOrDefault<User>();
            }
            else
            {
                return null;
            }
        }

        public User SelectByAccount(string account)
        {
            var user = from value in context.User
                       where value.Account.Equals(account)
                       select value;
            if (user != null && user.Count<User>() != 0)
            {
                return user.FirstOrDefault<User>();
            }
            else
            {
                return null;
            }
        }

        public User SelectByAccount(string account, string password)
        {
            var user = from value in context.User
                       where value.Account.Equals(account) && value.Password.Equals(password)
                       select value;
            if (user != null && user.Count<User>() != 0)
            {
                return user.FirstOrDefault<User>();
            }
            else
            {
                return null;
            }
        }

        public new bool Add(User user)
        {
            SqlParameter email = new SqlParameter("Email", user.Email);
            SqlParameter password = new SqlParameter("Password", user.Password);
            SqlParameter returnCode = new SqlParameter("ReturnCode", SqlDbType.Int);
            returnCode.Direction = ParameterDirection.Output;
            try
            {
                context.Database.ExecuteSqlCommand("EXEC [dbo].[CreateUser] @ReturnCode output,@Email,@Password", new SqlParameter[] { email, password, returnCode });
            }
            catch (Exception e)
            {
                e.ToString();
            }
            if (returnCode.Value.Equals(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
