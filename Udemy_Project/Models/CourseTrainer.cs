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

    public partial class CourseTrainer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CourseTrainer()
        {
            this.CourseFeedBacks = new HashSet<CourseFeedBack>();
            this.CourseMappings = new HashSet<CourseMapping>();
            this.CourseUserFeedbacks = new HashSet<CourseUserFeedback>();
        }
    
        public int CourseId { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Course Name cannot be more than 20 characters")]
        public string CourseName { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Course description cannot be more than 100 characters")]
        public string CourseDescription { get; set; }
        [Required]
        public string CourseLevels { get; set; }
        [Required]
        public string CourseLanguage { get; set; }
        [Required]
        public string CourseSkills { get; set; }
        [Required]
        public Nullable<int> CousrePrice { get; set; }
        public string CourseThumbNail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseFeedBack> CourseFeedBacks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseMapping> CourseMappings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseUserFeedback> CourseUserFeedbacks { get; set; }
    }
}
