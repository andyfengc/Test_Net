using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console.Models;

namespace Console.Lottery
{
    public class LotteryContext : DbContext
    {
        public DbSet<L649> L649s { get; set; }
    }
}
