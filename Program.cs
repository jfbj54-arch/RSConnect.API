using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Services;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURAÇÃO DO BANCO (Postgres Railway)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// INJEÇÃO DE DEPENDÊNCIA DOS SERVIÇOS
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// CONTROLLERS
builder.Services.AddControllers();

// SWAGGER (opcional, mas útil)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// SWAGGER SOMENTE EM DESENVOLVIMENTO
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS REDIRECTION (não quebra no Railway)
app.UseHttpsRedirection();

// AUTORIZAÇÃO (se usar)
app.UseAuthorization();

// MAPEIA OS CONTROLLERS
app.MapControllers();

// INICIA A API
app.Run();
