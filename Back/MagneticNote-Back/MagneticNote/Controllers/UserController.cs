using MagneticNote.Common;
using MagneticNote.IBLL;
using MagneticNote.Model.Data;
using Newtonsoft.Json;
using System;
using System.Web.Mvc;

namespace MagneticNote.Controllers
{
    public class UserController : Controller
    {
        public IUserBLL UserBLL { get; set; }

        public UserController(IUserBLL UserBLL)
        {
            this.UserBLL = UserBLL;
        }

        public ActionResult Get(String Email, String Password, String Account)
        {
            if (Email != null  && Password != null)
            {
                ResponseHelper.WriteObject(Response, "User", UserBLL.SelectByEmail(Email, Password));
            }

            if (Account != null && Password != null)
            {
                ResponseHelper.WriteObject(Response, "User", UserBLL.SelectByAccount(Account, Password));
            }

            if(Email != null)
            {
                ResponseHelper.WriteObject(Response, "User", UserBLL.SelectByEmail(Email));
            }
            else
            {
                ResponseHelper.WriteNull(Response);
            }
            ;
            return null;
        }

        [ValidateInput(false)]
        public ActionResult Add(String User)
        {
            User user = JsonConvert.DeserializeObject<User>(User);

            if (user != null)
            {
                if (UserBLL.Add(user))
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

        [ValidateInput(false)]
        public ActionResult Update(String User)
        {
            User user = JsonConvert.DeserializeObject<User>(User);

            if (user != null)
            {
                if (UserBLL.Update(user))
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