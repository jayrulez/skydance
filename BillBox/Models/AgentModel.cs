using BillBox.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    [MetadataType(typeof(AgentModel))]
    public partial class Agent
    {

    }

    public class AgentModel
    {
        public int AgentId;

        [Required]
        //[Index("UK_Username", unique: true)]
        [Display(Name = "Agent Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string AddressStreet { get; set; }

        [Required]
        [Display(Name = "City")]
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
        //[Unique("Agent", "EmailAddress")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }
}