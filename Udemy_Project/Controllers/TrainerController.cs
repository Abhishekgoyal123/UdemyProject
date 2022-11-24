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

            string abc = TempData["UserName"].ToString();
            int userId = Convert.ToInt32(TempData["UserId"]);
            //TempData.Keep();
            return RedirectToAction("GetPublishedCourse");
            
        }

        public ActionResult AddCourse()
        {
            return View();
        }


        [HttpGet]

        public ActionResult GetPublishedCourse()
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            //List<CourseMapping> courseId = new List<CourseMapping>();
            List<int?> courseId = new List<int?>();

            List<CourseTrainer> publishedCourse = new List<CourseTrainer>();
            // use list or array for storing courseId
            courseId = (from courseMapping in context.CourseMappings
                        where courseMapping.UserId == userId
                        select courseMapping.CourseId).ToList();

            foreach (var item in courseId)
            {
                var abc = (from courseTrainer in context.CourseTrainers
                           where courseTrainer.CourseId == item
                           select courseTrainer).ToList();

                foreach (var i in abc)
                {
                    publishedCourse.Add(i);
                }
            }
            TempData.Keep();
            return View(publishedCourse);
        }

        public ActionResult GetFeedback(int id)
        {
            var CourseuserFeedBack = context.CourseFeedBacks.ToList().Where(a => a.CourseId == id);
            return View(CourseuserFeedBack);
        }

        [HttpPost]

        public ActionResult AddCourse(CourseTrainer entity, CourseMapping courseMapping)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            var result = context.CourseTrainers.Add(entity);
            context.SaveChanges();

            courseMapping.UserId = userId;
            courseMapping.CourseId = entity.CourseId;
            context.CourseMappings.Add(courseMapping);
            context.SaveChanges();

            return RedirectToAction("GetPublishedCourse");
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