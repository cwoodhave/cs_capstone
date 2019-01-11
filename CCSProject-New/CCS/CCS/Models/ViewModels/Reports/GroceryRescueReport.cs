using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models.ViewModels.Reports
{
    public class GroceryRescueReport
    {
        public string Agency { get; set; }

        public string Phone { get; set; }

        public string Contact { get; set; }
        [UIHint("email")]
        public string Email { get; set; }
        [Display(Name = "MC#")]
        public string MCNum { get; set; }
        [Display(Name = "RE#")]
        public string RENum { get; set; }
        
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
