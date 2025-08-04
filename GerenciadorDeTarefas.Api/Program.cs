using GerenciadorDeTarefas.Application;
using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- Seção de Configuração de Serviços ---

// 1. Adiciona o serviço para habilitar o uso de Controllers
builder.Services.AddControllers();

// 2. Serviços para a documentação da API (Swagger/OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3. Registro das nossas interfaces e implementações (Injeção de Dependência)
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();


// --- Fim da Seção de Serviços ---


var app = builder.Build();

// --- Seção de Configuração do Pipeline HTTP ---

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

// --- Fim da Seção de Pipeline ---

app.Run();