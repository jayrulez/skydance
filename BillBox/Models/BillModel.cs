using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    [MetadataType(typeof(BillModel))]
    public partial class Bill
    {
    }

    public class BillModel
    {
        [Display(Name = "Bill Id")]
        public int BillId { get; set; }

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
        [Display(Name = "Status")]
        public int Status { get; set; }

        public List<Payment> Payments;
    }

    [MetadataType(typeof(PaymentModel))]
    public partial class Payment { }

    public class PaymentModel
    {
        [Display(Name = "Payment Method")]
        public int PaymentMethodId { get; set; }

        public int BillId;
        public double Amount;
    }
}