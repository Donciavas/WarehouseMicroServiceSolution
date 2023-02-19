using BusinessLogic.Configuration;
using BusinessLogic.Services;
using DataAccess.Repositories;
using OrderWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<OrderProcessingOptions>(builder.Configuration.GetSection("Configuration"));
builder.Services.AddScoped<IService<Order, string>, OrderService>();
builder.Services.AddTransient<IRepository<Order, string>, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
