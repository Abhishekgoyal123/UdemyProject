using System;
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
        UdemyEntities4 context = new UdemyEntities4();
        // GET: User

        ListModel lm = new ListModel();

        [Authorize(Roles = "User")]
        public ActionResult UserHomePage()                                          // instead of first redirecting to UserHomePage() and then redirecting to GetPurchasedCourses(), 
                                                                                    // IS IT BETTER TO KEEP ONLY ONE ACTION METHOD => GetPurchasedCourses()
        {
            // CONFIRM PASSWORD 
            ViewBag.Message = "Welcome to User Home Page";
            string abc = TempData["UserName"].ToString();
            int UserId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();

            var noOfCoursePurchased = (from courseMapping in context.CourseMappings
                                       where courseMapping.UserId == UserId
                                       select courseMapping).Count();

            // check if user has purchased any course or not

            if (noOfCoursePurchased != 0)
            {
                return RedirectToAction("GetPurchasedCourses");
            }
            else
            {
                //redirect to list of all courses
                return RedirectToAction("ListAllCourses");

            }
            //TempData.Keep();
            //return View();
            //return RedirectToAction("GetPurchasedCourses");

        }

        //public ActionResult SearchCourses()
        //{

        //}

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
            TempData.Keep();
            //List<CourseMapping> courseId = new List<CourseMapping>();
            List<int?> courseId = new List<int?>();

            List<CourseTrainer> purchasedCourse = new List<CourseTrainer>();
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
                    purchasedCourse.Add(i);
                }
            }

            Session["abc"] = purchasedCourse;
            TempData.Keep();
            // use foreach to itertate over coursemapping to get list of courseid based on userid
            //var result = context.CourseTrainers.ToList().Where(a=> a.)
            return View(purchasedCourse);
        }

        [HttpPost]
        public ActionResult AddCourseFeedBack(int? courseId, CourseFeedBack courseFeedback)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            if (ModelState.IsValid)
            {
                courseFeedback.CourseId = courseId;
               courseFeedback.UserId = userId;
                
                var result = context.CourseFeedBacks.Add(courseFeedback);

                //CourseFeedBack feedback = new CourseFeedBack();
                TempData["CourseFeedback"] = courseFeedback;
                TempData.Keep();
                context.SaveChanges();
            }
            

            return View();
        }

        public ActionResult DeleteCourseFeedBack(int? courseId)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();

            var isFeedbackExist = (from courseFeedBack in context.CourseFeedBacks
                                   where courseFeedBack.CourseId == courseId
                                   select courseFeedBack).Count();

            if (isFeedbackExist!=0)
            {
                var res = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).FirstOrDefault();
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

        public ActionResult EditCourseFeedback(int? courseId)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            var record = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).FirstOrDefault();
           
            return View(record);

        }

        [HttpPost]
        public ActionResult EditCourseFeedback(int? courseId, CourseFeedBack courseFeedBack)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            var recordToUpdate = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).FirstOrDefault();

            //TempData["RecordToUpdate"] = recordToUpdate.CourseReviews;

            recordToUpdate.CourseReviews = courseFeedBack.CourseReviews;
            recordToUpdate.CourseRatings = courseFeedBack.CourseRatings;

            context.SaveChanges();

            //return RedirectToAction("ShowCourseFeedback");
            return View();
        }

        public ActionResult ShowCourseFeedback(int? courseId)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            var record = context.CourseFeedBacks.ToList().Where(a => a.CourseId == courseId && a.UserId == userId);

            return View(record);
        }

        public ActionResult BuyCourse(CourseMapping courseMapping)
        {
            //List<CourseTrainer> trainers = TempData["Cart"];
           // int? totalPrice = 0;
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();

            var abc = (IEnumerable<CourseTrainer>)TempData["CartList"];
            TempData.Keep();

            foreach(var item in abc)
            {
                courseMapping.UserId = userId;
                courseMapping.CourseId = item.CourseId;
                context.CourseMappings.Add(courseMapping);
                context.SaveChanges();
            }


            return RedirectToAction("GetPurchasedCourses");
        }

        public ActionResult AddToCart(int? courseId)
        {
            TempData["courseId"] = courseId;
            var courseInCart = context.CourseTrainers.Where(m => m.CourseId == courseId).FirstOrDefault();
            ListModel.ctList.Add((CourseTrainer)courseInCart);
            return RedirectToAction("ListAllCourses");

        }

        //public JsonResult addtoCart(int? courseId)
        //{

        //    var courseInCart = context.CourseTrainers.Where(m => m.CourseId == courseId).FirstOrDefault();
        //    ListModel.ctList.Add(courseInCart);
        //    return Json(ListModel.ctList, JsonRequestBehavior.AllowGet);

        //}

        public ActionResult ViewCart()
        {
            // work on remove from cart functionality
            int?totalPrice = 0;

            foreach(var item in ListModel.ctList)
            {
                totalPrice = totalPrice + item.CousrePrice;
                
            }
            TempData["Total Price"] = totalPrice;

            TempData["CartList"] = ListModel.ctList;
            TempData.Keep();
            return View(ListModel.ctList.Distinct());
        }

        public ActionResult RemoveFromCart(int courseId)
        {
            var courseToRemove = ListModel.ctList.Where(m => m.CourseId == courseId).FirstOrDefault();
             bool isremoved = ListModel.ctList.Remove(courseToRemove);
            
            return RedirectToAction("ViewCart");
        }

    }
}