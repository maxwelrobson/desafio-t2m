using GerenciadorDeTarefas.Application;
using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- Se��o de Configura��o de Servi�os ---

// 1. Adiciona o servi�o para habilitar o uso de Controllers
builder.Services.AddControllers();

// 2. Servi�os para a documenta��o da API (Swagger/OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Registro das nossas interfaces e implementa��es (Inje��o de Depend�ncia)
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();


// --- Fim da Se��o de Servi�os ---


var app = builder.Build();

// --- Se��o de Configura��o do Pipeline HTTP ---

// Configure the HTTP request pipeline.
// Em ambiente de desenvolvimento, habilitamos a interface do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 4. Mapeia as rotas definidas nos seus Controllers
app.MapControllers();

// --- Fim da Se��o de Pipeline ---

app.Run();