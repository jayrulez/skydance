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
    
    public partial class CaptureField
    {
        public CaptureField()
        {
            this.PaymentCaptureFields = new HashSet<PaymentCaptureField>();
        }
    
        public int CaptureFieldId { get; set; }
        public int SubscriberId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Type { get; set; }
        public int OrderNum { get; set; }
    
        public virtual Subscriber Subscriber { get; set; }
        public virtual ICollection<PaymentCaptureField> PaymentCaptureFields { get; set; }
    }
}