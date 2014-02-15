using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    [MetadataType(typeof(PaymentTypeModel))]
    public partial class PaymentType
    {

    }

    public class PaymentTypeModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    [MetadataType(typeof(PaymentTypeCaptureFieldModel))]
    public partial class PaymentTypeCaptureField
    {

    }

    public class PaymentTypeCaptureFieldModel
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