using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;

namespace Udemy_Project.Controllers
{
    public class HomeController : Controller
    {
        UdemyEntities context = new UdemyEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public ActionResult ListAllCourses(int? id)
        //{
        //    List<CourseTrainer> courseTrainer = new List<CourseTrainer>();
        //    List<CourseUserFeedback> courseUserFeedback = new List<CourseUserFeedback>();


        //    Tuple<List<CourseTrainer>, List<CourseUserFeedback>> tuple = null;

        //    courseTrainer = context.CourseTrainers.ToList();

        //    ViewBag.ID = id;

        //    if (id == null || id == 0)
        //    {
        //        courseUserFeedback = context.CourseUserFeedbacks.ToList();
        //    }
        //    else
        //        courseUserFeedback = context.CourseUserFeedbacks.ToList().Where(a => a.CourseUserId == id).ToList();

        //    tuple = new Tuple<List<CourseTrainer>, List<CourseUserFeedback>>(courseTrainer, courseUserFeedback);
        //    return View(tuple);
        //}

        public ActionResult ShowReviews(int? id)
        {
            return RedirectToAction("Index", new { id = id });

        }
    }
}