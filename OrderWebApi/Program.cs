using BusinessLogic.Services;
using DataAccess.Configuration;
using DataAccess.Models;
using DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<OrderProcessingOptions>(builder.Configuration.GetSection("Configuration"));
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddTransient<IRepository<Order, string>, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
