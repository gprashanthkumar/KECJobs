using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KECJobs.Models
{
    /// <summary>
    /// This is a se
    /// </summary>
    public class ExcelEventRegistration
    {
       

        public int RegistrationID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MaritalStatus { get; set; }       
        public string DateofBirth { get; set; }
        public string Gender { get; set; }
        public string Qualification { get; set; }
        public string Positiontype { get; set; }
        public string AreaOfIntrest { get; set; }
        public string EmailAddress { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string ResumeFile { get; set; }

    }
}