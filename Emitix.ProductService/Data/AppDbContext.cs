using Emitix.ProductService.Models;
using Microsoft.EntityFrameworkCore;

namespace Emitix.ProductService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    public DbSet<Product> Products { get; set; }
    
}