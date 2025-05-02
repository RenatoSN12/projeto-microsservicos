using Emitix.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace Emitix.ProductService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    public DbSet<Product> Products { get; set; }
    
}