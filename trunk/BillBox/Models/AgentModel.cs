using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    public class CreateAgentModel
    {
        public int AgentId;

        [Required]
        [Display(Name = "Name")]
        public string Name;

        [Required]
        [Display(Name = "Street")]
        public string AddressStreet;

        [Required]
        [Display(Name = "City")]
        public string AddressCity;

        [Required]
        [Display(Name = "Parish")]
        public int ParishId;

        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber;

        [Required]
        [Display(Name = "Fax Number")]
        public string FaxNumber;

        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress;
    }
}