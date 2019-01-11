using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class Agency
    {
        public int AgencyID { get; set; }

        [Required]
        [Display(Name = "Agency Address")]
        public int AddressID { get; set; }

        [Required]
        [Display(Name = "Agency Name")]
        public string AgencyName { get; set; }

        [Required]
        [Display(Name = "Is A Donor")]
        public bool IsDonor { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        //Relationship Properties
        public Address Address { get; set; }
        public ICollection<DonationTransaction> DonationTransactions { get; set; }
    }
}
