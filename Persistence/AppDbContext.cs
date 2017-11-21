using Microsoft.EntityFrameworkCore;
using PagueVeloz.Models;

namespace PagueVeloz.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<People> Peoples { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>().ToTable("Peoples");
            modelBuilder.Entity<State>().ToTable("States");
            // Logger.Log("DeviceContext: Database & Tables SET.");
        }
    }
}