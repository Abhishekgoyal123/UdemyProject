//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Udemy_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CourseFeedBack
    {
        public Nullable<int> CourseId { get; set; }
        [Required]
        public string CourseReviews { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Please rate Course out of 5")]
        public Nullable<int> CourseRatings { get; set; }
        public Nullable<int> UserId { get; set; }
        public int ReviewId { get; set; }
    
        public virtual CourseTrainer CourseTrainer { get; set; }
    }
}
