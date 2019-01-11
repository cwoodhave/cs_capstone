using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace CCS.Models
{
    public enum IsAdmin
    {
        Yes,
        No,
    }

    public class CCSUser : IdentityUser
    {
        //public int CCSUserID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        //[Required]
        //[DisplayName("User Name")]
        //public string UserIDName { get; set; }

        //[Required]
        //[DisplayName("User Password")]
        //[DataType(DataType.Password)]
        //public string UserPassword { get; set; }

        [DisplayName("Is this user admin?")]
        public bool IsAdmin { get; set; }


        //Relationship Properties
        public ICollection<Log> Logs { get; set; }
        public ICollection<Adjustment> Adjustments { get; set; }

    }
}