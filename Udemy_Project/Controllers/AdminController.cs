using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;
namespace Udemy_Project.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        UdemyEntities4 context = new UdemyEntities4();
        // GET: Admin
        [Authorize(Roles ="Admin")]
        public ActionResult AdminHomePage()
        {
            ViewBag.Message = "Welcome to Admin Home Page";

            return View();
        }

        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]

        public ActionResult RegisterAdmin(User model, RoleMapping roleMapping)
        {
            if (ModelState.IsValid)
            {
                model.UserPassword = PasswordEncrypt.Encrypt(model.UserPassword);

                //role.UserId = model.UserId;
                context.Users.Add(model);
                context.SaveChanges();

                roleMapping.UserId = model.UserId;
                roleMapping.RoleId = 1;

                context.RoleMappings.Add(roleMapping);
                context.SaveChanges();
               // return RedirectToAction("GetAllCourse");
            }
            
                return View();
        }

        public ActionResult sp_getCourse()
        {
            var CourseList = context.sp_getCourse();
            return View(CourseList);
        }

        public ActionResult GetFeedback(int id)
        {
            var CourseuserFeedBack = context.CourseFeedBacks.ToList().Where(a => a.CourseId == id);
            return View(CourseuserFeedBack);
        }

        public ActionResult Delete3(int? CourseId)
        {
            var record = context.CourseTrainers.Find(CourseId);
            var noOfStudentEnrolled = (from CourseMapping in context.CourseMappings
                                       where CourseMapping.CourseId == CourseId
                                       select CourseMapping).Count();

            noOfStudentEnrolled--;

            TempData["noOfStudentEnrolled"] = noOfStudentEnrolled;

            if (noOfStudentEnrolled == 0)
            {
                context.CourseTrainers.Remove(record);
                context.SaveChanges();
                return RedirectToAction("sp_getCourse");
            }
            else
            {
                return View(record);
            }
        }

        public ActionResult EditCourse(int? courseId)
        {
            var record = context.CourseTrainers.Where(a => a.CourseId == courseId).FirstOrDefault();
            return View(record);
        }
        //[HttpPost]
        //public ActionResult EditCourse(int? courseId, CourseTrainer courseDetail)
        //{
        //    var record = context.CourseTrainers.Find(courseId);
        //    record.CourseName = courseDetail.CourseName;
        //    record.CourseDescription = courseDetail.CourseDescription;
        //    record.CourseLevels = courseDetail.CourseLevels;
        //    record.CourseLanguage = courseDetail.CourseLanguage;
        //    record.CourseSkills = courseDetail.CourseSkills;
        //    record.CousrePrice = courseDetail.CousrePrice;

        //    context.SaveChanges();

        //    return RedirectToAction("sp_getCourse");
        //}

        [HttpPost]
        public JsonResult EditCourse1(int? courseId, CourseTrainer courseDetail)
        {
            var record = context.CourseTrainers.Find(courseId);
            record.CourseName = courseDetail.CourseName;
            record.CourseDescription = courseDetail.CourseDescription;
            record.CourseLevels = courseDetail.CourseLevels;
            record.CourseLanguage = courseDetail.CourseLanguage;
            record.CourseSkills = courseDetail.CourseSkills;
            record.CousrePrice = courseDetail.CousrePrice;

            context.SaveChanges();

            return Json(courseDetail, JsonRequestBehavior.AllowGet);
        }

    }
}