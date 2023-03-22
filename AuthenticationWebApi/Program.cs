using BusinessLogic.Services;
using DataAccess.MicroServiceDbContexts;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<UserAccountDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddTransient<IUserAccountRepository, UserAccountRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
