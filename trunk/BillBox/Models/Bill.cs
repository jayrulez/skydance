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
    
    public partial class Bill
    {
        public Bill()
        {
            this.BillCaptureFields = new HashSet<BillCaptureField>();
            this.Payments = new HashSet<Payment>();
        }
    
        public int BillId { get; set; }
        public int SubscriberId { get; set; }
        public int ReceiptNumber { get; set; }
        public int AgentId { get; set; }
        public int AgentBranchId { get; set; }
        public int UserId { get; set; }
        public System.DateTime Date { get; set; }
        public int Status { get; set; }
        public Nullable<double> ProcessingFee { get; set; }
        public Nullable<double> ProcessingFeeGCT { get; set; }
        public Nullable<double> Commission { get; set; }
        public Nullable<double> CommissionGCT { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual AgentBranch AgentBranch { get; set; }
        public virtual Subscriber Subscriber { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BillCaptureField> BillCaptureFields { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
