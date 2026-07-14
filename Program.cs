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


// ⭐ MIGRATIONS AUTOMÁTICAS (ESSENCIAL NO RAILWAY)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();   // cria tabelas automaticamente
}


// ⭐ SERVIR ARQUIVOS HTML
app.UseDefaultFiles();   // procura index.html
app.UseStaticFiles();    // libera wwwroot

app.UseAuthorization();

app.MapControllers();

// ⭐ Fallback para SPA
app.MapFallbackToFile("index.html");

app.Run();
