using Emitix.ProductService.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200");
    });
});

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
