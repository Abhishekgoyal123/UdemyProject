using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;

namespace Udemy_Project.Controllers
{
    public class UserController : Controller
    {
        UdemyEntities1 context = new UdemyEntities1();
        // GET: User
        public ActionResult UserHomePage()
        {
            ViewBag.Message = "Welcome to User Home Page";
            string abc = TempData["UserName"].ToString();
            return View();
        }

        public ActionResult AddCourseFeedBack()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddCourseFeedBack(CourseFeedBack courseFeedback)
        {
            
            var result = context.CourseFeedBacks.Add(courseFeedback);
            context.SaveChanges();
            
            return View();
        }
    }
}