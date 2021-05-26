using CognizantChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace CognizantChallenge
{
    public class DatabaseContext : DbContext
    {
        private const string _schema = "cognizant_challenge";
        public DbSet<TaskRecord> Tasks { get; set; }

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskRecord>(entity =>
            {
                entity.ToTable("task_records", _schema);
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
