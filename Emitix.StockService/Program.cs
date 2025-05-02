using Emitix.ProductService.Common;
using Emitix.StockService.Common;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();

builder.AddDataContext();

builder.AddServices();

builder.AddDocumentation();
    
var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.MapEndpoints();

app.Run();