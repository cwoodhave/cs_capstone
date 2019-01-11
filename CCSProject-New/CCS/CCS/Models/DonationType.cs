using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class DonationType
    {
        public int DonationTypeID { get; set; }

        [Display(Name = "Donation Type")]
        public string Type { get; set; }

        //Relationship Properties
        public ICollection<TransactionLineItem> TransactionLineItems { get; set; }
    }
}
