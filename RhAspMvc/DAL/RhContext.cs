using RhAspMvc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RhAspMvc.DAL
{
    public class RhContext : DbContext
    {
        public RhContext() : base("RhContext")
        {

        }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<Specialite> Specialites { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        
    }
}