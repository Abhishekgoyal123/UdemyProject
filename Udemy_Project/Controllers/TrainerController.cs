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
        UdemyEntities1 context = new UdemyEntities1();
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
            return View(CourseList);
        }

        public ActionResult GetFeedback(int id)
        {
            var CourseuserFeedBack = context.CourseFeedBacks.ToList().Where(a => a.CourseId == id);
            return View(CourseuserFeedBack);
        }

        [HttpPost]

        public ActionResult AddCourse(CourseTrainer entity)
        {
            var result = context.CourseTrainers.Add(entity);
            context.SaveChanges();
            return View();
        }
        
        public ActionResult Delete3(int? CourseId)
        {
            var noOfStudentEnrolled = (from CourseMapping in context.CourseMappings
                                       where CourseMapping.UserId == CourseId
                                       select CourseMapping).Count();

            if (noOfStudentEnrolled == 0)
            {
                var record = context.CourseTrainers.Find(CourseId);
                context.CourseTrainers.Remove(record);
                context.SaveChanges();
            }
            return RedirectToAction("GetAllCourse");
        }

        public ActionResult EditCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditCourse(int? courseId, CourseTrainer courseDetail)
        {
            var record = context.CourseTrainers.Find(courseId);
            record.CourseName = courseDetail.CourseName;
            record.CourseDescription = courseDetail.CourseDescription;
            record.CourseLevels = courseDetail.CourseLevels;
            record.CourseLanguage = courseDetail.CourseLanguage;
            record.CourseSkills = courseDetail.CourseSkills;
            record.CousrePrice = courseDetail.CousrePrice;

            context.SaveChanges();
            
            return RedirectToAction("GetAllCourse");
        }
    }
}