using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Udemy_Project.Services;
using Udemy_Project.Models;
using System.Threading.Tasks;

namespace Udemy_Project.Services
{
    public class TrainerCRUD : Icrud<CourseTrainer, int>
    {
        UdemyEntities4 context = new UdemyEntities4();


        Task<CourseTrainer> Icrud<CourseTrainer, int>.CreateAsync(CourseTrainer entity)
        {
            //var result = context.CourseTrainers.Add(entity);
            //context.SaveChanges();
            //return
                throw new NotImplementedException();
        }

        Task<CourseTrainer> Icrud<CourseTrainer, int>.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<CourseTrainer>> Icrud<CourseTrainer, int>.GetAsync()
        {
            throw new NotImplementedException();
        }

        Task<CourseTrainer> Icrud<CourseTrainer, int>.GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<CourseTrainer> Icrud<CourseTrainer, int>.UpdateAsync(int id, CourseTrainer entity)
        {
            throw new NotImplementedException();
        }

        public void GetFeedback(int id)
        {
            var CourseuserFeedBack = context.CourseFeedBacks.ToList().Where(a => a.CourseId == id);
        }
    }
}