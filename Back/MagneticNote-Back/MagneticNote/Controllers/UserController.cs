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

        public ActionResult Get()
        {
            String email = Request["Email"] as String;
            String password = Request["Password"] as String;
            if (email != null  && password != null)
            {
                ResponseHelper.WriteObject(Response, "User", UserBLL.SelectByEmail(email, password));
            }
            String account = Request["Account"] as String;
            if (account != null && password != null)
            {
                ResponseHelper.WriteObject(Response, "User", UserBLL.SelectByAccount(account, password));
            }

            if(email != null)
            {
                ResponseHelper.WriteObject(Response, "User", UserBLL.SelectByEmail(email));
            }
            else
            {
                ResponseHelper.WriteNull(Response);
            }
            ;
            return null;
        }

        public ActionResult Add()
        {
            User user = JsonConvert.DeserializeObject<User>(Request["User"]);

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
                ResponseHelper.WriteNull(Response);;
            }
            ;
            return null;
        }

        public ActionResult Update()
        {
            User user = JsonConvert.DeserializeObject<User>(Request["User"]);

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
                ResponseHelper.WriteNull(Response);;
            }
            ;
            return null;
        }
    }
}