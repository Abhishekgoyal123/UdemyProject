﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Udemy_Project.Models;
using Udemy_Project;

namespace Udemy_Project.Controllers
{
    
    public class UserController : Controller
    {
        UdemyEntities1 context = new UdemyEntities1();
        // GET: User
        
        public ActionResult UserHomePage()                                          // instead of first redirecting to UserHomePage() and then redirecting to GetPurchasedCourses(), 
                                                                                    // IS IT BETTER TO KEEP ONLY ONE ACTION METHOD => GetPurchasedCourses()
        {
            // CONFIRM PASSWORD 
            ViewBag.Message = "Welcome to User Home Page";
            string abc = TempData["UserName"].ToString();
            int UserId = Convert.ToInt32(TempData["UserId"]);

            var noOfCoursePurchased = (from courseMapping in context.CourseMappings
                                       where courseMapping.UserId == UserId
                                       select courseMapping).Count();

            // check if user has purchased any course or not

            if (noOfCoursePurchased!=0)
            {
                return RedirectToAction("GetPurchasedCourses");
            }
            else
            {
                //redirect to list of all courses
                return RedirectToAction("ListAllCourses");
                
            }
            TempData.Keep();
            //return View();
            //return RedirectToAction("GetPurchasedCourses");

        }

        public ActionResult ListAllCourses()
        {
            var CourseList = context.CourseTrainers.ToList();

            return View(CourseList);
            
        }

        public ActionResult AddCourseFeedBack()
        {
            return View();
        }

        // MERGE GetPurchasedCourses INTO USERHOMEPAGE()
        public ActionResult GetPurchasedCourses()
        {
            
            int userId = Convert.ToInt32(TempData["UserId"]);
            //List<CourseMapping> courseId = new List<CourseMapping>();
            List<int?> courseId = new List<int?>();

            List<CourseTrainer> purchasedCourse = new List<CourseTrainer>();
            // use list or array for storing courseId
             courseId = (from courseMapping in context.CourseMappings
                           where courseMapping.UserId == userId
                           select courseMapping.CourseId).ToList();

            foreach(var  item in courseId)
            {
                var abc = (from courseTrainer in context.CourseTrainers
                                  where courseTrainer.CourseId == item
                                  select courseTrainer).ToList();

                foreach (var i in abc)
                {
                    purchasedCourse.Add(i);
                }
            }
            // use foreach to itertate over coursemapping to get list of courseid based on userid
            //var result = context.CourseTrainers.ToList().Where(a=> a.)
            return View(purchasedCourse);
        }

        [HttpPost]
        public ActionResult AddCourseFeedBack(int? courseId,CourseFeedBack courseFeedback)
        {
            courseFeedback.CourseId = courseId;
            
            var result = context.CourseFeedBacks.Add(courseFeedback);
            context.SaveChanges();
            
            return View();
        }

        public ActionResult DeleteCourseFeedBack(int? courseId)
        {
            
            var isFeedbackExist = (from courseFeedBack in context.CourseFeedBacks
                                   where courseFeedBack.CourseId == courseId
                                   select courseFeedBack).Count();

            if(isFeedbackExist == 1)
            {
                var res = context.CourseFeedBacks.Where(a => a.CourseId == courseId).FirstOrDefault();
                //var recordToDelete = context.CourseFeedBacks.Find(res);
                context.CourseFeedBacks.Remove(res);
                context.SaveChanges();
            }

            return RedirectToAction("ShowCourseFeedback");
        }

        //public ActionResult DeleteFeedBack(int? courseId)
        //{

        //    var isFeedbackExist = (from courseFeedBack in context.CourseFeedBacks
        //                           where courseFeedBack.CourseId == courseId
        //                           select courseFeedBack).Count();

        //    if (isFeedbackExist == 1)
        //    {
        //        var recordToDelete = context.CourseFeedBacks.Find(courseId);
        //        context.CourseFeedBacks.Remove(recordToDelete);
        //        context.SaveChanges();

        //    }

        //    return RedirectToAction("GetPurchasedCourses");
        //}

        public ActionResult EditCourseFeedback()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditCourseFeedback(int? courseId, CourseFeedBack courseFeedBack)
        {
            var recordToUpdate = context.CourseFeedBacks.Where(a => a.CourseId == courseId).FirstOrDefault();
            recordToUpdate.CourseReviews = courseFeedBack.CourseReviews;
            recordToUpdate.CourseRatings = courseFeedBack.CourseRatings;

            context.SaveChanges();
            
            return RedirectToAction("ShowCourseFeedback");
        }

        public ActionResult ShowCourseFeedback(int? courseId)
        {
            var record = context.CourseFeedBacks.ToList().Where(a => a.CourseId == courseId);
            return View(record);
        }

        public ActionResult BuyMultipleCourse(int? courseId, CourseMapping courseMapping)
        {
            int? totalPrice = 0;
            int userId = Convert.ToInt32(TempData["UserId"]);

            var priceArray = (from courseTrainer in context.CourseTrainers
                         where courseTrainer.CourseId == courseId
                         select courseTrainer.CousrePrice).ToArray();

            for(int i = 0; i < priceArray.Count(); i++)
            {
                courseMapping.UserId = userId;
                courseMapping.CourseId = priceArray[i];

                totalPrice = totalPrice + priceArray[i];
            }

            return RedirectToAction("GetPurchasedCourses");
        }

        public ActionResult AddToCart(int? courseId)
        {
            List<CourseTrainer> list  = new List<CourseTrainer>();
            var courseInCart = context.CourseTrainers.Where(m => m.CourseId == courseId).ToList();
            
            foreach(var item in courseInCart)
            {
                list.Add(item);
            }
            return View(list);

        }
    }
}