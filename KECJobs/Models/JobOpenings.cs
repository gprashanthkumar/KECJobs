//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KECJobs.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobOpenings
    {
        public int JobOpenID { get; set; }
        public int jobExperienceID { get; set; }
        public string JobID { get; set; }
        public string Company { get; set; }
        public string jobPosition { get; set; }
        public string Qualification { get; set; }
        public string Locations { get; set; }
        public string ContactDetails { get; set; }
        public Nullable<System.DateTime> ValidFrom { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public string Keywords { get; set; }
    
        public virtual Lookup_Experiences tbl_Lookup_Experiences { get; set; }
    }
}