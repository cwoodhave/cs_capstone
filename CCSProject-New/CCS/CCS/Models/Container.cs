using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class Container
    {
        public int ContainerID { get; set; }
        public int FoodCategoryID { get; set; }
        public string Location { get; set; }
        public int BinNumber { get; set; }
        public string Description { get; set; }
        public decimal Weight { get; set; }
        public DateTime DateCreated { get; set; }
        
        public FoodCategory FoodCategory { get; set; }
    }
}
