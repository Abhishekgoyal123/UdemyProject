//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int ProductUniqueId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Descrition { get; set; }
        public decimal Price { get; set; }
        public int Sub_CategoryId { get; set; }
        public int Manufacturer_Id { get; set; }
    
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}