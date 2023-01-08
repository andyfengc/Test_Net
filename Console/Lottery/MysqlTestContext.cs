using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Models;
using MySql.Data.Entity;

namespace Console.Lottery
{
    public class MysqlTestContext : DbContext
    {
        public MysqlTestContext()
        {
            //Database.SetInitializer<MysqlTestContext>(new CreateDatabaseIfNotExists<MysqlTestContext>());
        }
        public DbSet<L649> L649s { get; set; }

    }
}
