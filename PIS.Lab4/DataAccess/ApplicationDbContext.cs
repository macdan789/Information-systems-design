using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PIS.Lab4.Models;
using System.IO;

namespace PIS.Lab4.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Job> Job { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Workplace> Workplace { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer(@"Server=.;Database=PIS_Lab4_EFCore;Trusted_Connection=True;");
    }
}
