using Learning_WebApi.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Learning_WebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Operation> Operations { get; set; }
}