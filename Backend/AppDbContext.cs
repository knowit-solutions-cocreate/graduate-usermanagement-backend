using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>(entity => {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("NOW()"); 

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NOW()");

        });

        // Apply UTC conversion to all DateTime fields
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()) {
            foreach (var property in entityType.GetProperties()) {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?)) {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                        v => v));
                }
            }
        }
    }
}