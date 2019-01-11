using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class Adjustment
    {
        public int AdjustmentID { get; set; }
        public int CCSUserID { get; set; }
        public decimal Weight { get; set; }
        public string FoodCategory { get; set; }
        public string Location { get; set; }
        public bool IsUSDA { get; set; }
        public string USDANumber { get; set; }
        public int Cases { get; set; }

        //Relationship Properties
        public CCSUser User { get; set; }

    }
}
