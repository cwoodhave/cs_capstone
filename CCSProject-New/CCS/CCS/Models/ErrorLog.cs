using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class ErrorLog
    {
        public int ErrorLogID { get; set; }
        public string FileName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string FunctionName { get; set; }
        public string LineNumber { get; set; }
        public string ErrorText { get; set; }

    }
}
