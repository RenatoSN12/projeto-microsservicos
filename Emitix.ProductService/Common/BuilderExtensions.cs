using Emitix.ProductService.Data;
using Emitix.ProductService.Data.UnitOfWork;
using Emitix.ProductService.DTOs.Requests;
using Emitix.ProductService.Repositories;
using Emitix.ProductService.Services;
using Emitix.ProductService.Services.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Emitix.ProductService.Common;

public static class BuilderExtensions
{
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    public static void AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlServer(Configuration.ConnectionString);
        });
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IProductService, Services.Products.ProductService>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();
    }
}
