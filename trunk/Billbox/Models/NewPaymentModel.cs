using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    public class NewPaymentModel
    {
        [Required]
        [Display(Name = "Subscriber")]
        public int SubscriberId;

        public CaptureField[] CaptureFields;
    }
}