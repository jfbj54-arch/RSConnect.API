using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Services;
using RSConnect.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// PEGAR CONNECTION STRING DO RAILWAY
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

// CONFIGURAÇÃO DO BANCO (Postgres Railway)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// INJEÇÃO DE DEPENDÊNCIA DOS SERVIÇOS E REPOSITÓRIOS
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// CONTROLLERS
builder.Services.AddControllers();

// SWAGGER
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// SWAGGER EM DESENVOLVIMENTO
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
