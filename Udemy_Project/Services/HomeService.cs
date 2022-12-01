using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Udemy_Project.Models;

namespace Udemy_Project.Services
{
    public class HomeService
    {
        UdemyEntities4 context = new UdemyEntities4();
        public Tuple<List<CourseTrainer>, List<CourseUserFeedback>> ListAllCourses(int id)
        {
            List<CourseTrainer> courseTrainer = new List<CourseTrainer>();
            List<CourseUserFeedback> courseUserFeedback = new List<CourseUserFeedback>();

            
            Tuple<List<CourseTrainer>, List<CourseUserFeedback>> tuple = null;

            courseTrainer = context.CourseTrainers.ToList();

           

            return tuple;
        }
    }
}