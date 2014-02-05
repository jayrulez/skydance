using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Billbox.Models
{
    public class AgentUsersContext : DbContext
    {
        public AgentUsersContext()
            : base("Entities")
        {
        }

        public DbSet<AgentUser> AgentUsers { get; set; }
    }

    [Table("AgentUser")]
    public class AgentUserAccount
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Username { get; set; }
    }

    public class AgentUserLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool Autologin { get; set; }
    }
}