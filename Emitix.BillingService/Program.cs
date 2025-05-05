using Emitix.BillingService.Common;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
    });
});

QuestPDF.Settings.License = LicenseType.Community;
builder.AddConfiguration();
builder.AddDataContext();

builder.AddServices();

builder.AddDocumentation();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors("AllowSpecificOrigins");

app.MapEndpoints();

app.Run();