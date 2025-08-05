using GerenciadorDeTarefas.Application;
using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Serviço para habilitar o uso de Controllers
builder.Services.AddControllers();

//Serviços para a documentação da API (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro das interfaces e implementações (Injeção de Dependência)
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//interface do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Rotas definidas dos controllers
app.MapControllers();

app.Run();

public partial class Program { }