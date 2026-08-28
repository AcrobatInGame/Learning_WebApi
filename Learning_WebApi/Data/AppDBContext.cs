using Learning_WebApi.Data.Entity;
using Microsoft.EntityFrameworkCore;
// ReSharper disable All

namespace Learning_WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Operation> Operations { get; set; }
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Указываем EF Core, что для сущности User поле Email является уникальным индексом
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}