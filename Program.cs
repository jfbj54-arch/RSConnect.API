using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Repositories;
using RSConnect.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Porta obrigatória para Railway (AGORA É 8888)
builder.WebHost.UseKestrel();
builder.WebHost.UseUrls("http://0.0.0.0:8888");

// Banco de dados PostgreSQL do Railway
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=hayabusa.proxy.rlwy.net;Port=49725;Database=railway;Username=postgres;Password=ozvxHYcQqWFMriiSpmzSPiMeBoXPySNV;SSL Mode=Require;Trust Server Certificate=True")
);

// CORS liberado
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Injeção de dependência
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
