using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;

namespace Udemy_Project.Controllers
{
    public class TrainerController : Controller
    {
        UdemyEntities context = new UdemyEntities();
        // GET: Trainer
        public ActionResult TrainerHomePage()
        {
            ViewBag.Message = "Welcome to Trainer Home Page";

            return View();
        }

        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpGet]

        public ActionResult GetAllCourse()
        {
            // use tuple to get result both from courseTrainer and CourseUserFeedbacks table
            var CourseList = context.CourseTrainers.ToList();
            //var CourseuserFeedBack = context.CourseUserFeedbacks.ToList();
            return View();
        }

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

        //    var noOfStudentEnrolled = (from CourseMapping in context.CourseMappings
        //                               where CourseMapping.UserId == CourseId
        //                               select CourseMapping).Count();

        //    if(noOfStudentEnrolled == 0)
        //    {
        //        var record = context.CourseTrainers.Find(CourseId);
        //        context.CourseTrainers.Remove(record);
        //        context.SaveChanges();

        //        // need to change CourseUserFeedbacks to courseFeedBack ( new table )
        //        var record1 = context.CourseUserFeedbacks.Find(CourseId);
        //        context.CourseUserFeedbacks.Remove(record1);
        //        context.SaveChanges();
        //    }
            // add functionality : if course is enrolled by student, then it cannot be deleted.
            
        //    return View();
        //}
    }
}