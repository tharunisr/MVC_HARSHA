﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EfDbCoreFirst.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username can't be blank")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password can't be blank")]
        public string Password { get; set; }
    }
}