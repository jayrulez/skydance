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
    
    public partial class PaymentMethodCaptureField
    {
        public PaymentMethodCaptureField()
        {
            this.PaymentPaymentMethodCaptureFields = new HashSet<PaymentPaymentMethodCaptureField>();
        }
    
        public int PaymentMethodCaptureFieldId { get; set; }
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public Nullable<short> Type { get; set; }
        public Nullable<int> OrderNum { get; set; }
    
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<PaymentPaymentMethodCaptureField> PaymentPaymentMethodCaptureFields { get; set; }
    }
}