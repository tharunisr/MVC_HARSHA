using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EfDbCoreFirst.CustomValidation;

namespace EfDbCoreFirst.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product ID")]
        public long ProductID { get; set; }



        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name can't be blank")]
        [RegularExpression(@"^[A-Za-z ]*$",ErrorMessage ="Alphabets only")]
        [MaxLength(50,ErrorMessage ="Max length should be 50")]
        [MinLength(2,ErrorMessage ="Atleast 2 character")]
        public string ProductName { get; set; }



        [Display(Name = "Price")]
        [Required(ErrorMessage ="Price can't be blank")]
        [Range(0,1000000,ErrorMessage ="Price should be 0 to 1000000")]
        [DivisibleBy10(ErrorMessage ="Price should be divisible by 1000")]
        public Nullable<decimal> Price { get; set; }



        [Column("DateOfPurchase",TypeName ="DateTime")]
        [Display(Name ="Date Of Purchase")]
        public Nullable<System.DateTime> DateOfPurchase { get; set; }



        [Display(Name = "Availability Status")]
        [Required(ErrorMessage ="Availability Status can't be blank")]
        public string AvailabilityStatus { get; set; }



        [Display(Name = "Category Id")]
        [Required(ErrorMessage = "Category can't be blank")]
        public Nullable<long> CategoryID { get; set; }



        [Display(Name = "Brand Id")]
        [Required(ErrorMessage = "Brand can't be blank")]
        public Nullable<long> BrandID { get; set; }



        [Display(Name = "Active")]
        public Nullable<bool> Active { get; set; }



        [Display(Name = "Photo")]
        public string Photo { get; set; }



        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}