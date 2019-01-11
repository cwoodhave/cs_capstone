using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public enum FoodCategoryType
    {
        Regular,
        USDA,
        [Display(Name ="Grocery Rescue")]
        Grocery,
        [Display(Name = "Pantry Pack")]
        Pantry,
        Internal //For Beans, Milk, etc.
    }


    public class FoodCategory
    {
        public int FoodCategoryID { get; set; }

        [Display(Name = "Food Category Type")]
        public FoodCategoryType FoodCategoryType { get; set; }
        public string Description { get; set; }

        //Relationship Properties
        public ICollection<Container> Containers { get; set; }
        public ICollection<TransactionLineItem> TransactionLineItems { get; set; }
    }
}
