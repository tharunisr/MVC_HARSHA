using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EfDbCoreFirst.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Username can't be blank")]
        public string Username {  get; set; }
        [Required(ErrorMessage ="Password can't be blank")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password can't be blank")]
        [Compare("Password",ErrorMessage ="Should be same as Password ")]
        public string ConfirmPassword {  get; set; }


        [Required(ErrorMessage ="Email can't be blank")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime? DateOfBirth {  get; set; }
        public string Address {  get; set; }
        public string City {  get; set; }
        public string Country { get; set; }

    }
}