using Emitix.StockService.Models;
using Microsoft.EntityFrameworkCore;

namespace Emitix.StockService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    public DbSet<ProductStock> Stocks { get; set; }
}