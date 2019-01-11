using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models.ViewModels.Reports
{
    public class InOutReport
    {
        [Required]
        public List<int> Agencies { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public List<TransactionTypeInOut> TransactionTypes { get; set; }
        
        [Required]
        [Display(Name = "Food Categories")]
        public List<FoodCategoryType> FoodCategories { get; set; }

        [Required]
        [UIHint("Date")]
        [Display(Name = "From Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [UIHint("Date")]
        [Display(Name = "Thru End Date")]
        public DateTime EndDate { get; set; }
    }
}
