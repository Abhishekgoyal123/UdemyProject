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

       // ListModel lm = new ListModel();

        [Authorize(Roles = "User")]
        public ActionResult UserHomePage()                                          
        {
            // CONFIRM PASSWORD 
            ViewBag.Message = "Welcome to User Home Page";
            string UserName = TempData["UserName"].ToString();
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
            
        }
        public ActionResult ListAllCourses()
        {
            string UserName = TempData["UserName"].ToString();
            TempData.Keep();
            var CourseList = context.CourseTrainers.ToList();

            return View(CourseList);
        }

        public ActionResult AddCourseFeedBack(int? courseId)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            int noOfFeedback = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).Count();
            
            if (noOfFeedback == 0)
            {
                return View();
            }
            return View("Error");
        }

        public ActionResult GetPurchasedCourses()
        {
            string UserName = TempData["UserName"].ToString();
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

            TempData["purchasedCourse"] = purchasedCourse;
            
            return View(purchasedCourse);
        }

        [HttpPost]
        public ActionResult AddCourseFeedBack(int? courseId, CourseFeedBack courseFeedback)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();

            int noOfFeedback = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).Count();
            TempData["noOfFeedback"] = noOfFeedback;
            if (ModelState.IsValid && noOfFeedback ==0)
            {
                courseFeedback.CourseId = courseId;
                courseFeedback.UserId = userId;

                var result = context.CourseFeedBacks.Add(courseFeedback);
                context.SaveChanges();
                
                return View();

            }
            else
                return RedirectToAction("GetPurchasedCourses");
        }

        public ActionResult DeleteCourseFeedBack(int? courseId)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            var recordToDelete = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).FirstOrDefault();

            var FeedbackExist = (from courseFeedBack in context.CourseFeedBacks
                                   where courseFeedBack.CourseId == courseId
                                   select courseFeedBack).Count();

            if (FeedbackExist!=0)
            {
                context.CourseFeedBacks.Remove(recordToDelete);
                context.SaveChanges();
                return RedirectToAction("GetPurchasedCourses");
            }

            return RedirectToAction("GetPurchasedCourses");
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
            var recordToEdit = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).FirstOrDefault();
           
            return View(recordToEdit);
        }

        [HttpPost]
        public ActionResult EditCourseFeedback(int? courseId, CourseFeedBack courseFeedBack)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            var recordToUpdate = context.CourseFeedBacks.Where(a => a.CourseId == courseId && a.UserId == userId).FirstOrDefault();

            recordToUpdate.CourseReviews = courseFeedBack.CourseReviews;
            recordToUpdate.CourseRatings = courseFeedBack.CourseRatings;

            context.SaveChanges();

            return RedirectToAction("GetPurchasedCourses");
            //return View();
        }

        public ActionResult ShowCourseFeedback(int? courseId)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();
            var record = context.CourseFeedBacks.ToList().Where(a => a.CourseId == courseId && a.UserId == userId);

            var FeedbackCount = (from courseFeedBack in context.CourseFeedBacks
                                   where courseFeedBack.CourseId == courseId && courseFeedBack.UserId == userId
                                   select courseFeedBack).Count();

            TempData["FeedbackCount"] = FeedbackCount;
            if (FeedbackCount != 0)
            {
                return View(record);
            }
            else
            {
                return RedirectToAction("GetPurchasedCourses");
            }
        }

        public ActionResult BuyCourse(CourseMapping courseMapping)
        {
            int userId = Convert.ToInt32(TempData["UserId"]);
            TempData.Keep();

            var cartList = (IEnumerable<CourseTrainer>)TempData["CartList"];
            TempData.Keep();
           
            var purchasedCourseList = (IEnumerable<CourseTrainer>)TempData["purchasedCourse"];
            foreach (var item in cartList)
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
            int flag=0;
            TempData["courseId"] = courseId;
            var courseInCart = context.CourseTrainers.Where(m => m.CourseId == courseId).FirstOrDefault();
            var abc = (IEnumerable<CourseTrainer>)TempData["purchasedCourse"];
            TempData.Keep();
            foreach(var item in abc)
            {
                if (courseInCart.CourseId == item.CourseId)
                {
                    flag = 1;
                    break;
                }
                
            }
            TempData["flag"] = flag;
            if (flag == 0)
            {
                ListModel.ctList.Add((CourseTrainer)courseInCart);
            }
            return RedirectToAction("ListAllCourses");

        }

        public ActionResult ViewCart()
        {
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
             
            return RedirectToAction("ViewCart");
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
    }
}