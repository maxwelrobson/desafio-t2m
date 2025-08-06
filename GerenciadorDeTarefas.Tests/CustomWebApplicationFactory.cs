using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace GerenciadorDeTarefas.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                //Faz uma configuração temporária que consegue ler os User Secrets
                var tempConfig = new ConfigurationBuilder()
                    .AddUserSecrets<CustomWebApplicationFactory>() // Lê os segredos associados a este projeto de teste
                    .Build();

                //Pega a connection string de teste dos segredos
                var testConnectionString = tempConfig.GetConnectionString("TestConnection");

                //Remove as configurações antigas da API para garantir que não usem o banco de "verdade"
                var appSettingsSource = configBuilder.Sources.OfType<JsonConfigurationSource>()
                    .FirstOrDefault(s => s.Path == "appsettings.json");
                if (appSettingsSource != null)
                {
                    configBuilder.Sources.Remove(appSettingsSource);
                }

                //Adiciona a connection string de teste em memória para a API usar
                configBuilder.AddInMemoryCollection(new Dictionary<string, string>
                {
                    // A API vai procurar por "DefaultConnection", com isso ela vai usar a connection string de teste
                    ["ConnectionStrings:DefaultConnection"] = testConnectionString!
                });
            });
        }
    }
}
