using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Udemy_Project.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult UserHomePage()
        {
            ViewBag.Message = "Welcome to User Home Page";

            return View();
        }

        public ActionResult AddCourseFeedBack()
        {
            return View();
        }
    }
}