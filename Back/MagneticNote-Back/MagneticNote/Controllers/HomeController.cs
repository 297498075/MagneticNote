﻿using Autofac;
using Common;
using MagneticNote.Common;
using MagneticNote.IBLL;
using MagneticNote.Model.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MagneticNote.Controllers
{
    public class HomeController : Controller
    {
        public INoteBLL NoteBLL { get; set; }
        public INoteBookBLL NoteBookBLL { get; set; }
        public IBookGroupBLL BookGroupBLL { get; set; }
        public IUserBLL UserBLL { get; set; }
        public HomeController(INoteBLL noteBLL, INoteBookBLL noteBookBLL,IBookGroupBLL bookGroupBLL,IUserBLL userBLL)
        {
            this.UserBLL = userBLL;
            this.BookGroupBLL = bookGroupBLL;
            this.NoteBookBLL = noteBookBLL;
            this.NoteBLL = noteBLL;
        }

        public ActionResult Get()
        {
            Response.Write("Ready to complete.");
            ;
            return null;
        }

        public ActionResult GetDefaultNote()
        {
            String userId = Request["UserId"];
            if (!String.IsNullOrEmpty(userId))
            {
                String column = Request["Column"];
                if (!String.IsNullOrEmpty(column))
                {
                    ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByUserId(Convert.ToInt32(userId), Convert.ToInt32(column)));
                }

                ResponseHelper.WriteList(Response, "NoteList", NoteBLL.SelectByUserId(Convert.ToInt32(userId)));
            }

            Note[] noteList = { new Note() { Title = "欢迎", Content = "欢迎使用MagneticNote笔记本" } };
            ResponseHelper.WriteObject(Response, "NoteList", noteList);
            
            return null;
        }

        public ActionResult GetAllNoteBook(String userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                ResponseHelper.WriteList(Response, "NoteBookList", NoteBookBLL.SelectByUserId(Convert.ToInt32(userId),true));
            }

            ResponseHelper.WriteNull(Response);
            return null;
        }

        public ActionResult GetBookGroupContainsBook()
        {
            String userId = Request["UserId"];
            if (!String.IsNullOrEmpty(userId))
            {
                ResponseHelper.WriteList(Response, "BookGroupList", BookGroupBLL.SelectByUserId(Convert.ToInt32(userId), true));
            }

            ResponseHelper.WriteNull(Response);
            return null;
        }

        public ActionResult SendRegisterEmail()
        {
            String email = Request["Email"];
            String password = Request["Password"];
            if (email == null || password == null)
            {
                ResponseHelper.WriteFalse(Response);
            }
            else
            {
                User user = new User() { Email = email, Password = password, Account = email };
                String userKey = JsonConvert.SerializeObject(user);
                String key = RedisHelper.StringGet(userKey);

                if(key != null)
                {
                    Mail.Send(email, Mail.GetView(email,key));
                    ResponseHelper.WriteTrue(Response);
                }
                else if(UserBLL.SelectByEmail(email).Id==0)
                {
                    key = getStr(false,16);
                    if (RedisHelper.StringSet(userKey, key, new TimeSpan(1, 0, 0, 0)) &&
                        RedisHelper.StringSet(key, userKey, new TimeSpan(1, 0, 0, 0)))
                    {
                        Mail.Send(email, Mail.GetView(email, key));
                        ResponseHelper.WriteTrue(Response);
                    }
                    else
                    {
                        ResponseHelper.WriteFalse(Response);
                    }
                }
                else
                {
                    ResponseHelper.WriteFalse(Response);
                }
            }
            return null;
        }

        public ActionResult RegisterValidate()
        {
            String key = Request["ValidateKey"];

            if(key == null)
            {
                ResponseHelper.WriteNull(Response);
            }
            else
            {
                var user = RedisHelper.Get<User>(key);

                if (user == null)
                {
                    ResponseHelper.WriteFalse(Response);
                }

                if (UserBLL.Add(user))
                {
                    RedisHelper.Remove(key);
                    RedisHelper.Remove(JsonConvert.SerializeObject(user));

                    ResponseHelper.WriteTrue(Response);
                }
                else
                {
                    ResponseHelper.WriteFalse(Response);
                }
            }
            return null;
        }

        public static String getStr(bool b, int n)//b：是否有复杂字符，n：生成的字符串长度
        {

            string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (b == true)
            {
                str += "!#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";//复杂字符
            }
            StringBuilder SB = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < n; i++)
            {
                SB.Append(str.Substring(rd.Next(0, str.Length), 1));
            }
            return SB.ToString();
        }
    }
}