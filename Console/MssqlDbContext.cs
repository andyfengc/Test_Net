using Console.Db;

namespace Console
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MssqlDbContext : DbContext
    {
        public DbSet<Temp> Temps { get; set; }
        public MssqlDbContext()
            : base("name=MssqlDbContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
