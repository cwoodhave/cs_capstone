using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class Address
    {
        public int AddressID { get; set; }

        [Required]
        [Display(Name = "City")]
        public string CityName { get; set; }

        [Required]
        [Display(Name = "State")]
        public string StateShortName { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        [Display(Name = "Street Address Line 1")]
        public string StreetAddress1 { get; set; }

        [Display(Name = "Street Address Line 2")]
        public string StreetAddress2 { get; set; }

        //Relationship Properties
        public ICollection<Agency> Agencies { get; set; }
    }
}
