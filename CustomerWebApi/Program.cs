using CustomerWebApi;
using CustomerWebApi.Models;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IRepository<Customer, int>, CustomerRepository>();
builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
