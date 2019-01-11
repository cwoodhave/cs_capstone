using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class Log
    {
        public int LogID { get; set; }
        public int CCSUserID { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        //Relationship Properties
        public CCSUser User { get; set; }
    }
}
