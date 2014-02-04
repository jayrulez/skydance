//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Billbox.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AgentBranch
    {
        public AgentBranch()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public int BranchId { get; set; }
        public int AgentId { get; set; }
        public string Name { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public int ParishId { get; set; }
        public string ContactNumber { get; set; }
        public string FaxNumber { get; set; }
        public string EmailAddress { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Parish Parish { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}