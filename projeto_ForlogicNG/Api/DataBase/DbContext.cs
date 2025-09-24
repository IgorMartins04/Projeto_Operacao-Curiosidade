using Microsoft.EntityFrameworkCore;
using ProjetoForlogic.Models;

namespace ProjetoForlogic.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogView> LogsView { get; set; }
        public DbSet<InactiveCount> InactiveCount { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InactiveCount>().HasNoKey();

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Logs"); 
            });

            modelBuilder.Entity<LogView>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_Logs"); 
            });
        }
    }
}
