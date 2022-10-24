using Microsoft.EntityFrameworkCore;
using PIS.DAL.Models;

namespace PIS.DAL
{
    public class ApplicationDbContext : DbContext
    {
        private const string _connectionString = @"Server=.;Database=PIS_Lab5_EFCore;Trusted_Connection=True;";

        public DbSet<Job> Job { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Workplace> Workplace { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer(_connectionString);
    }
}
