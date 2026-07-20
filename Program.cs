using Microsoft.EntityFrameworkCore;
using Npgsql;
using RSConnect.API.Data;

var builder = WebApplication.CreateBuilder(args);

// PEGAR DATABASE_URL DO RENDER
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new Exception("DATABASE_URL não encontrada nas variáveis de ambiente!");
}

// CONVERTER URL PARA CONNECTION STRING
var uri = new Uri(databaseUrl);
var userInfo = uri.UserInfo.Split(':');

var connectionStringBuilder = new NpgsqlConnectionStringBuilder
{
    Host = uri.Host,
    Port = uri.Port,
    Username = userInfo[0],
    Password = userInfo[1],
    Database = uri.AbsolutePath.TrimStart('/'),
    SslMode = SslMode.Require,
    TrustServerCertificate = true
};

var connectionString = connectionStringBuilder.ToString();

// CONFIGURAÇÃO DO BANCO
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// INJEÇÃO DE DEPENDÊNCIA
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Adiciona controllers
builder.Services.AddControllers();

// Libera CORS para o frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// MIGRATIONS AUTOMÁTICAS
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
