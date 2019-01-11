using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models.ViewModels.Reports
{
    public class GroceryQueryModel
    {
        [Display(Name = "Agency")]
        public string Agency { get; set; }

        public decimal Bakery { get; set; }

        public decimal Dairy { get; set; }

        public decimal Deli { get; set; }

        [Display(Name ="Dry Grocery")]
        public decimal DryGrocery { get; set; }

        [Display(Name = "Frozen (Non-Meat)")]
        public decimal Frozen { get; set; }

        public decimal Meat { get; set; }

        [Display(Name = "Non-Food")]
        public decimal NonFood { get; set; }

        public decimal Produce { get; set; }
    }
}
