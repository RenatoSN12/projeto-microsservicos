using Emitix.StockService.Data;
using Emitix.StockService.Data.UnitOfWork;
using Emitix.StockService.DTOs.Requests;
using Emitix.StockService.Repositories;
using Emitix.StockService.Services;
using Emitix.StockService.Services.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Emitix.StockService.Common;

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
        builder.Services.AddTransient<IStockService, Services.StockService>();
        builder.Services.AddTransient<IProductStockRepository, ProductStockRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IValidator<UpdateProductStockDto>, UpdateProductStockValidator>();
    }
}