using GerenciadorDeTarefas.Application;
using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Nome da política de CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 

//Serviço para habilitar o uso de Controllers
builder.Services.AddControllers();

//Serviços para a documentação da API (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registro das interfaces e implementações (Injeção de Dependência)
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();

// Configuração do CORS
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//interface do Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

//Rotas definidas dos controllers
app.MapControllers();

app.Run();

public partial class Program { }