using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    [MetadataType(typeof(SubscriberModel))]
    public partial class Subscriber
    {
    }

    public class SubscriberModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Operating Name")]
        public string OperatingName { get; set; }

        

        [Required]
        [Display(Name = "Address")]
        public string AddressStreet { get; set; }

        [Required]
        [Display(Name = "Town/City")]
        public string AddressCity { get; set; }

        [Required]
        [Display(Name = "Parish")]
        public int ParishId { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Fax Number")]
        public string FaxNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]        
        [Display(Name = "Website")]
        public string Website { get; set; }
        
    }
}