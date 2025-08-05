using GerenciadorDeTarefas.Domain;
using GerenciadorDeTarefas.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program> //<Program> se refere ao Program.cs da API
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                //remove a configuração do banco de dados real
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITarefaRepository));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddScoped<ITarefaRepository>(CodePagesEncodingProvider =>
                {
                    var config = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["ConnectionStrings:DefaultConnection"] = "Server=localhost;Port=5432;Database=TarefasDb_Tests;User Id=postgres;Password=maxwel13;"
                    })
                    .Build();

                    return new TarefaRepository(config);
                });
            });
        }
    }
}
