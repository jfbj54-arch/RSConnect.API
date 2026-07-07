using Microsoft.EntityFrameworkCore;
using RSConnect.API.Data;
using RSConnect.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext com a connection string do Railway
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra os serviços
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Adiciona controllers
builder.Services.AddControllers();

// Adiciona Swagger (opcional, mas útil)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativa Swagger somente em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Permite requisições HTTP
app.UseHttpsRedirection();

// Permite autorização (se usar)
app.UseAuthorization();

// Mapeia controllers
app.MapControllers();

// Inicia a aplicação
app.Run();
