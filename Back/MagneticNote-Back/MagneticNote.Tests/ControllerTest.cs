using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MagneticNote.Controllers;
using MagneticNote.Model.Entity;
using MagneticNote.BLL;
using MagneticNote.EFDAL;

namespace MagneticNote.Tests
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            MagneticNoteContainer context = new MagneticNoteContainer();
            NoteDAL noteDAL = new NoteDAL(context);
            NoteBookDAL noteBookDAL = new NoteBookDAL(context);
            BookGroupDAL bookGroupDAL = new BookGroupDAL(context);
            NoteBLL noteBLL = new NoteBLL(noteDAL);
            NoteBookBLL noteBookBLL = new NoteBookBLL(noteBookDAL);
            BookGroupBLL bookGroupBLL = new BookGroupBLL(bookGroupDAL);

           
            return;
        }

        [TestMethod]
        public void TestGetAllNoteBook()
        {
            MagneticNoteContainer context = new MagneticNoteContainer();
            NoteDAL noteDAL = new NoteDAL(context);
            NoteBookDAL noteBookDAL = new NoteBookDAL(context);
            BookGroupDAL bookGroupDAL = new BookGroupDAL(context);
            UserDAL userDAL = new UserDAL(context);
            NoteBLL noteBLL = new NoteBLL(noteDAL);
            NoteBookBLL noteBookBLL = new NoteBookBLL(noteBookDAL);
            BookGroupBLL bookGroupBLL = new BookGroupBLL(bookGroupDAL);
            UserBLL userBLL = new UserBLL(userDAL);
            var list = new HomeController(noteBLL, noteBookBLL, bookGroupBLL, userBLL).GetAllNoteBook("21");

            Console.Write(list);
            return;
        }
    }
}
