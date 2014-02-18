using BillBox.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    [MetadataType(typeof(PaymentMethodModel))]
    public partial class PaymentMethod
    {

    }

    public class PaymentMethodModel
    {
        [Required]
        [Display(Name = "Name")]
        //[Unique(typeof(Repository<PaymentType>), typeof(PaymentType), "Name", ErrorMessage = "Payment type already exists.")]
        public string Name { get; set; }
    }

    [MetadataType(typeof(PaymentMethodCaptureFieldModel))]
    public partial class PaymentMethodCaptureField
    {

    }

    public class PaymentMethodCaptureFieldModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int Type { get; set; }

        [Required]
        [Display(Name = "Order")]
        public int OrderNum { get; set; }
    }
}