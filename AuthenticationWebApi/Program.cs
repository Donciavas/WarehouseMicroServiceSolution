using JwtAuthenticationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<JwtTokenHandler>();
<<<<<<< HEAD
builder.Services.AddSingleton<UserAccountService>();

=======
>>>>>>> 68bc333f127a027f9a4cf4a25ef82e7b064597f2
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
