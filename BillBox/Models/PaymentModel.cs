using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    [MetadataType(typeof(PaymentModel))]
    public partial class Payment
    {

    }

    public class PaymentModel
    {
        [Display(Name = "Payment Id")]
        public int PaymentId { get; set; }

        [Required]
        [Display(Name = "Subscriber")]
        public int SubscriberId { get; set; }

        [Required]
        [Display(Name = "Invoice Number")]
        public int InvoiceNumber { get; set; }

        [Required]
        [Display(Name = "Agent")]
        public int AgentId { get; set; }

        [Required]
        [Display(Name = "Agent Branch")]
        public int AgentBranchId { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public int Date { get; set; }

        [Required]
        [Display(Name = "Time")]
        public int Time { get; set; }

        [Required]
        [Display(Name = "Status")]
        public int Status { get; set; }
    }
}