﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp50
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Database1Entities : DbContext
    {
        public Database1Entities()
            : base("name=Database1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<date> date { get; set; }
        public virtual DbSet<employee> employee { get; set; }
        public virtual DbSet<employee_type> employee_type { get; set; }
        public virtual DbSet<expense> expense { get; set; }
        public virtual DbSet<final_price> final_price { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<postal_code> postal_code { get; set; }
        public virtual DbSet<products> products { get; set; }
    }
}
