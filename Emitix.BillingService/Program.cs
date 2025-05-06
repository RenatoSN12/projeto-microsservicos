using Emitix.BillingService.Common;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

QuestPDF.Settings.License = LicenseType.Community;
builder.AddConfiguration();
builder.AddDataContext();

builder.AddServices();

builder.AddDocumentation();
    
var app = builder.Build();

app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.MapEndpoints();

app.Run();