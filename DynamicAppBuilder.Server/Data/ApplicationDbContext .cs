using Microsoft.EntityFrameworkCore;
using DynamicAppBuilder.Server.Models;

namespace DynamicAppBuilder.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Props> Props { get; set; }
        public DbSet<ControlProperties> ControlProperties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ControlProperties>()
            .OwnsOne(c => c.coordinates);

            modelBuilder.Entity<ControlProperties>()
                .HasOne<Props>()  
                .WithMany()  
                .HasForeignKey(c => c.PropId)   
                .OnDelete(DeleteBehavior.Cascade);  
        }

    }
}
