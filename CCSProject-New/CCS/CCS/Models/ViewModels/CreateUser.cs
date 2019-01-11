using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models.ViewModels
{
    public class CreateUser
    {
        [Required]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}
