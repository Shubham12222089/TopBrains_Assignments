using AuthService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<JwtService>();

var app = builder.Build();

app.MapControllers();
app.Run();