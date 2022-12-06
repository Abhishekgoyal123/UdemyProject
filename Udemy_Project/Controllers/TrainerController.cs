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
        UdemyEntities4 context = new UdemyEntities4();
        // GET: Trainer
        [Authorize(Roles ="Trainer")]
        public ActionResult TrainerHomePage()
        {
            ViewBag.Message = "Welcome to Trainer Home Page";

           // string userName = TempData["UserName"].ToString();
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            
            var noOfCoursePublished = (from courseMapping in context.CourseMappings
                                       where courseMapping.UserId == userId
                                       select courseMapping).Count();

            if (noOfCoursePublished != 0)
            {
                return RedirectToAction("GetPublishedCourse");
            }
            else
            {
                return RedirectToAction("AddCourse");
            }
        }

        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpGet]

        public ActionResult GetPublishedCourse()
        {
             //string userName = TempData["UserName"].ToString();
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
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

        public ActionResult GetFeedback(int? courseId)
        {
            var CourseFeedBack = context.CourseFeedBacks.ToList().Where(a => a.CourseId == courseId);

            var feedbackCount = (from courseFeedBack in context.CourseFeedBacks
                                 where courseFeedBack.CourseId == courseId 
                                 select courseFeedBack).Count();

            if(feedbackCount == 0)
            {
                return RedirectToAction("GetPublishedCourse");
            }
            else
            {
                return View(CourseFeedBack);

            }
           
        }

        [HttpPost]
        public ActionResult AddCourse(CourseTrainer entity, CourseMapping courseMapping)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
           // string UserName = TempData["UserName"].ToString();
            TempData.Keep();
            if (ModelState.IsValid)
            {
                var courseTOAdd = context.CourseTrainers.Add(entity);
                context.SaveChanges();

                courseMapping.UserId = userId;
                courseMapping.CourseId = entity.CourseId;
                context.CourseMappings.Add(courseMapping);
                context.SaveChanges();
            }
            
            return RedirectToAction("GetPublishedCourse");
        }
        
        public ActionResult Delete3(int? CourseId)
        {
            var recordToDelete = context.CourseTrainers.Find(CourseId);
            var noOfStudentEnrolled = (from CourseMapping in context.CourseMappings
                                       where CourseMapping.CourseId == CourseId
                                       select CourseMapping).Count();

            noOfStudentEnrolled = noOfStudentEnrolled - 1;

            TempData["noOfStudentEnrolled"] = noOfStudentEnrolled;

            if (noOfStudentEnrolled == 0)
            {
                context.CourseTrainers.Remove(recordToDelete);
                context.SaveChanges();
                return RedirectToAction("GetPublishedCourse");
            }
            else
            {
                return View(recordToDelete);
            }
         
        }
        public JsonResult delete()
        {
            int noOfStudentEnrolled = Convert.ToInt32(TempData["noOfStudentEnrolled"]);
            TempData.Keep();
            return Json(noOfStudentEnrolled, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCourse(int? courseId)
        {
            var record = context.CourseTrainers.Find(courseId);
            return View(record);
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
            
            return RedirectToAction("GetPublishedCourse");
        }

        public ActionResult CourseStats(int? CourseId)
        {
            TempData["CourseId"] = CourseId;
            var noOfStudentEnrolled = (from CourseMapping in context.CourseMappings
                                       where CourseMapping.CourseId == CourseId
                                       select CourseMapping).Count();

            noOfStudentEnrolled = noOfStudentEnrolled - 1;

            TempData["noOfStudentEnrolled"] = noOfStudentEnrolled;

            var AverageRatings = (from courseFeedback in context.CourseFeedBacks
                                 where courseFeedback.CourseId == CourseId
                                 select courseFeedback.CourseRatings).Average();

            if(AverageRatings == null)
            {
                TempData["averageRatings"] = 0;
            }
            else
                TempData["averageRatings"] = AverageRatings;

            return View();
        }

        public JsonResult noOfStudentEnrolled()
        {
            int noOfStudentEnrolled = Convert.ToInt32(TempData["noOfStudentEnrolled"]);
            TempData.Keep();

            return Json(noOfStudentEnrolled, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AverageRatings()
        {
            int AverageRatings = Convert.ToInt32(TempData["averageRatings"]);
            TempData.Keep();
            return Json(AverageRatings, JsonRequestBehavior.AllowGet);
        }
 
    }
}