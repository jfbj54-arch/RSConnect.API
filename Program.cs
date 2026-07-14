using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Services;
using RSConnect.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// PEGAR CONNECTION STRING DO RAILWAY
var connectionString = builder.Configuration["DATABASE_URL"];

// CONFIGURAÇÃO DO BANCO (Postgres Railway)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// INJEÇÃO DE DEPENDÊNCIA DOS SERVIÇOS E REPOSITÓRIOS
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔥 ESSENCIAL PARA SERVIR HTML NO RAILWAY
app.UseDefaultFiles();   // procura index.html automaticamente
app.UseStaticFiles();    // libera wwwroot

app.UseAuthorization();

app.MapControllers();

// 🔥 Fallback para qualquer rota não-API
app.MapFallbackToFile("index.html");

app.Run();
