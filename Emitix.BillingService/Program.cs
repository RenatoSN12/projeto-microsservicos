using Emitix.BillingService.Common;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;
builder.AddConfiguration();
builder.AddDataContext();

builder.AddServices();

builder.AddDocumentation();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.MapEndpoints();

app.Run();