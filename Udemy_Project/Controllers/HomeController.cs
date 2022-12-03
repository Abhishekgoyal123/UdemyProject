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
        UdemyEntities4 context = new UdemyEntities4();
        public ActionResult Index()
        {
            return RedirectToAction("SearchCourses");
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

        public ActionResult SearchCourses()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchCourses(string ProductName)
        {
            TempData["SearchParameter"] = ProductName;
            //TempData.Keep();
            return RedirectToAction("SearchResult");
        }
        //public ActionResult SearchCourses()
        //{
        //    return View();
        //}
        
        public ActionResult SearchResult()
        {
            string searchParameter = TempData["SearchParameter"].ToString();
            IQueryable<CourseTrainer> result = null;

            List<CourseTrainer> resultList = new List<CourseTrainer>();

            var res = searchParameter.Split(' ');

            result = from courseTrainer in context.CourseTrainers
                     select courseTrainer;

            for (int i = 0; i < res.Length; i++)
            {
                foreach (var item in result)
                {
                    if (item.CourseName.Contains(res[i]) || item.CourseDescription.Contains(res[i]) || item.CourseLevels.Contains(res[i]) || item.CourseLanguage.Contains(res[i]) || item.CourseSkills.Contains(res[i]))
                    {
                         resultList.Add(item);
                    }
                }

            }
            return View(resultList.Distinct());
        }

        //public ActionResult Filter()
        //{
        //    List<Role> rolelist = context.CourseTrainers.

        //    rolelist.RemoveAt(0);
        //    ViewData["RoleName"] = new SelectList(rolelist, "RoleName", "RoleName");

        //    return View();
        //}

        public ActionResult BuyCourse(int? courseId)
        {
            return RedirectToAction("AddToCart", "User",(object)courseId);
        }
    }

    
}

    
