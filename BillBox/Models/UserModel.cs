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

    [MetadataType(typeof(UserModel))]
    public partial class User
    {
    }

    public class UserModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string AddressStreet { get; set; }

        [Required]
        [Display(Name = "City")]
        public string AddressCity { get; set; }

        [Required]
        [Display(Name = "Parish")]
        public int ParishId { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        //[Unique("Agent", "EmailAddress")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Agent")]
        public int AgentId { get; set; }

        [Display(Name = "Branch")]
        public int AgentBranchId { get; set; }

        [Display(Name = "User Level")]
        public int UserLevelId { get; set; }
    }

    public class UpdateUserModel
    {
    }
}