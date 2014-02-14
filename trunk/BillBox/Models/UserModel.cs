﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool Autologin { get; set; }
    }

    public class UserModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserModel
    {
    }
}