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
        UdemyEntities1 context = new UdemyEntities1();
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
        //    List<CourseFeedBack> courseFeedBack = new List<CourseFeedBack>();

        //    Tuple<List<CourseTrainer>, List<CourseFeedBack>> tuple = null;

        //    courseTrainer = context.CourseTrainers.ToList();

        //    ViewBag.ID = id;

        //    if (id == null || id == 0)
        //    {
        //        courseFeedBack = context.CourseFeedBacks.ToList();
        //    }
        //    else
        //        courseFeedBack = context.CourseFeedBacks.ToList().Where(a => a.CourseId == id).ToList();

        //    tuple = new Tuple<List<CourseTrainer>, List<CourseFeedBack>>(courseTrainer, courseFeedBack);
        //    return View(tuple);
        //}

        //public ActionResult ShowReviews(int? id)
        //{
        //    //return RedirectToAction("ListAllCourses", new { id = id });
        //    return View(new { id = id });

        //}

        [HttpGet]
        public ActionResult ListAllCourses()
        {
            var CourseList = context.CourseTrainers.ToList();
            
            return View(CourseList);
        }

        public ActionResult GetFeedback(int id)
        {
            var CourseuserFeedBack = context.CourseFeedBacks.ToList().Where(a => a.CourseId == id);
            return View(CourseuserFeedBack);
        }
    }
}