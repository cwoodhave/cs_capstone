using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class TransactionLineItem
    {
        public int TransactionLineItemID { get; set; }

        [Required(ErrorMessage = "Please select a donation category")]
        public int FoodCategoryID { get; set; }

        [Required(ErrorMessage = "Please select a donation type")]
        public int DonationTypeID { get; set; }

        [Required]
        public int DonationTransactionID { get; set; }

        [Required(ErrorMessage = "Please input a weight")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a weight greater than 0")] //double.Epsilon checks for decimals greater than 0
        public decimal Weight { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a quantity greater than 0")]
        public int? Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a cases value greater than 0")]
        public int? Cases { get; set; }

        //Relationship Properties
        public FoodCategory FoodCategory { get; set; }
        public DonationType DonationType { get; set; }
        public DonationTransaction DonationTransaction { get; set; }
    }
}
