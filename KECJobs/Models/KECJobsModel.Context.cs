﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KECJobsDBContext : DbContext
    {
        public KECJobsDBContext()
            : base("name=KECJobsDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Lookup_Experiences> Lookup_Experiences { get; set; }
        public virtual DbSet<tbl_lookup_PositionType> tbl_lookup_PositionType { get; set; }
        public virtual DbSet<tbl_Lookup_Qualifications> tbl_Lookup_Qualifications { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<CompanyReferences> CompanyReferences { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<SkillDevelopment> SkillDevelopments { get; set; }
        public virtual DbSet<JobOpenings> JobOpenings { get; set; }
        public virtual DbSet<Lookup_Costs> Lookup_Costs1 { get; set; }
        public virtual DbSet<Lookup_Duration> Lookup_Duration { get; set; }
        public virtual DbSet<tbl_Lookup_MaritalStatus> tbl_Lookup_MaritalStatus { get; set; }
    }
}
