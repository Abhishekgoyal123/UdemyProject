using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;
namespace Udemy_Project.Controllers
{
    public class AdminController : Controller
    {
        UdemyEntities context = new UdemyEntities();
        // GET: Admin
        public ActionResult AdminHomePage()
        {
            ViewBag.Message = "Welcome to Admin Home Page";

            return View();
        }

        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpGet]

        //public ActionResult GetAllCourse()
        //{
        //    // use tuple to get result both from courseTrainer and CourseUserFeedbacks table
        //    var CourseList = context.CourseTrainers.ToList();
        //    var CourseuserFeedBack = context.CourseUserFeedbacks.ToList();
        //    return View();
        //}

        [HttpPost]

        public ActionResult AddCourse(CourseTrainer entity)
        {
            var result = context.CourseTrainers.Add(entity);
            context.SaveChanges();
            return View();
        }

        //[HttpPost]
        //public ActionResult DeleteCourse(int CourseId)
        //{
        //    // add functionality : if course is enrolled by student, then it cannot be deleted.
        //    var record = context.CourseTrainers.Find(CourseId);
        //    context.CourseTrainers.Remove(record);
        //    context.SaveChanges();

        //    var record1 = context.CourseUserFeedbacks.Find(CourseId);
        //    context.CourseUserFeedbacks.Remove(record1);
        //    context.SaveChanges();
        //    return View();
        //}


    }
}