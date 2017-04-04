using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KECJobs.Models
{
    public class ExcelJobOpenings
    {
        public int JobOpenID { get; set; }
        public string jobExperience { get; set; }
        public string JobID { get; set; }
        public string Company { get; set; }
        public string jobPosition { get; set; }
        public string Qualification { get; set; }
        public string Locations { get; set; }
        public string ContactDetails { get; set; }
       
        public string ValidFrom { get; set; }
       
        public string ValidTo { get; set; }
        public string Keywords { get; set; }
        public string JobFile { get; set; }
    }
}