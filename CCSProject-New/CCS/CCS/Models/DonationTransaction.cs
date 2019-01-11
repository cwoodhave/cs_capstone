using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{

    public enum TransactionTypeInOut
    {
        [Display(Name = "Incoming")]
        In,
        [Display(Name = "Outgoing")]
        Out
    }


    public class DonationTransaction
    {

        public int DonationTransactionID { get; set; }

        [Required(ErrorMessage = "Please select an agency")]
        public int AgencyID { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format")]
        [Required(ErrorMessage = "Please enter donation date")]
        [Display(Name = "Donation Date")]
        public DateTime TimeStamp { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public TransactionTypeInOut TransactionType { get; set; }

        [Required(ErrorMessage = "Please select a food category")]
        [Display(Name = "Food Category")]
        public FoodCategoryType FoodCategory { get; set; }


        //Relationship Properties
        public ICollection<TransactionLineItem> TransactionLineItems { get; set; }
        public Agency Agency { get; set; }

    }
}
