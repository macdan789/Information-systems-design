using Microsoft.EntityFrameworkCore;
using PIS.DAL.Contracts;
using PIS.DAL.Models;

namespace PIS.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Job> Job { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Workplace> Workplace { get; set; }
        public DbSet<User> User { get; set; }
    }
}