using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CCS.Models
{
    public class ReportTemplate
    {
        public int TemplateID { get; set; }
        public string TemplateName { get; set; }
        public DateTime Date { get; set; }
        public int TemplateType { get; set; }
        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; }

    }
}
