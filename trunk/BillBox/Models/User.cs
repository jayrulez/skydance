//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillBox.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public int UserId { get; set; }
        public int UserLevelId { get; set; }
        public Nullable<int> AgentId { get; set; }
        public Nullable<int> AgentBranchId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> PasswordExpireAt { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public int ParishId { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual AgentBranch AgentBranch { get; set; }
        public virtual Parish Parish { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual UserLevel UserLevel { get; set; }
    }
}
