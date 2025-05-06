using Emitix.BillingService.Data;
using Emitix.BillingService.Data.UnitOfWork;
using Emitix.BillingService.DTOs.Requests;
using Emitix.BillingService.Repositories;
using Emitix.BillingService.Services.Billing;
using Emitix.BillingService.Services.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Emitix.BillingService.Common;

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
        builder.Services.AddTransient<IBillingService, Services.BillingService>();
        builder.Services.AddTransient<IBillingRepository, BillingRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IValidator<CreateInvoiceDto>, CreateInvoiceValidator>();
        builder.Services.AddScoped<IValidator<CreateInvoiceProductDto>, CreateInvoiceProductValidator>();
    }
}
