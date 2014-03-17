using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class UserLevel 
    {
        public bool HasRight(int rightId)
        {
            return this.UserRights.Any(r => r.RightId == rightId);
        }

        public bool HasRight(string rightName)
        {
            return this.UserRights.Any(r => r.Name == rightName);
        }
    }

    [MetadataType(typeof(UserModel))]
    public partial class User
    {
        public bool HasRight(string rightName)
        {
            if(this.UserLevel == null)
            {
                return false;
            }

            return this.UserLevel.HasRight(rightName);
        }

        public IList<string> GetUserRights()
        {
            IList<string> rights = new List<string>();

            using (Entities dbContext = new Entities())
            {
                rights = dbContext.Users.Find(this.UserId).UserLevel.UserRights.Select(r => r.Name).ToList();
            }

            return rights;
        }
    }

    public class UserModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string AddressStreet { get; set; }

        [Required]
        [Display(Name = "Town/City")]
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