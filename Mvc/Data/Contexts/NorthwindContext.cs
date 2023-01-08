using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using Mvc.Models.Northwind;

namespace Mvc.Data.Contexts
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }
        public DbSet<Territories> Territories { get; set; }
        public DbSet<Employees> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // associations
            modelBuilder.Entity<Territories>()
                .HasMany(t => t.Employees)
                .WithMany(e => e.Territories)
                .Map(te =>
                {
                    te.ToTable("EmployeeTerritories");
                    te.MapLeftKey("TerritoryID");
                    te.MapRightKey("EmployeeID");
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}