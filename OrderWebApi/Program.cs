using BusinessLogic.Configuration;
using DataAccess.Repositories;
using OrderWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.Configure<OrderProcessingServiceOptions>(builder.Configuration.GetSection("Configuration"));
builder.Services.AddTransient<IRepository<Order, string>, OrderRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
