using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Udemy_Project.Models
{
    public class Payment
    {

        [Required]
        public string CardNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Range(100, 999)]
        public int Cvv { get; set; }
    }
}