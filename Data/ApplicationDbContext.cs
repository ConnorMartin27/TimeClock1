using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TimeClock.Models;

namespace TimeClock.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Entry> TimestampEntries { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Entry>()
                .HasOne(te => te.User)
                .WithMany()
                .HasForeignKey(te => te.UserId);
        }
    }
}
