using BusinessLogic.Services;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using ProductWebApi;
using ProductWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ProductDbContext>(o => o.UseMySQL(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
