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
    
    public partial class Lookup_Duration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lookup_Duration()
        {
            this.tbl_SkillDevelopment = new HashSet<SkillDevelopment>();
        }
    
        public int DurationId { get; set; }
        public string DurationDesc { get; set; }
        public Nullable<bool> Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SkillDevelopment> tbl_SkillDevelopment { get; set; }
    }
}
