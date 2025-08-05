using GerenciadorDeTarefas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Tests
{
    public class TarefasControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        // Construtor que recebe a fábrica personalizada para criar o cliente HTTP
        public TarefasControllerIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType() // Testa se o endpoint GET retorna sucesso e o tipo de conteúdo correto
        {
            // Arrange
            var url = "/api/tarefas";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Garante que o status code é 2xx (sucesso)
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }

        [Fact]
        public async Task Post_Then_Get_ShouldCreateAndReturnTask() // Testa se o POST cria uma tarefa e o GET a retorna corretamente
        {
            // Arrange
            // cria uma nova tarefa via post
            var novaTarefaDto = new CriarTarefaRequestDTO("Teste de Integração", "Testando o fluxo completo.");
            var postResponse = await _client.PostAsJsonAsync("/api/tarefas", novaTarefaDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            var tarefaCriada = await postResponse.Content.ReadFromJsonAsync<TarefaResponseDTO>();
            Assert.NotNull(tarefaCriada);
            Assert.Equal("Teste de Integração", tarefaCriada.Titulo);

            // Act
            // buscar a tarefa criada via get
            var getResponse = await _client.GetAsync($"/api/tarefas/{tarefaCriada.Id}");

            // Assert
            getResponse.EnsureSuccessStatusCode();
            var tarefaBuscada = await getResponse.Content.ReadFromJsonAsync<TarefaResponseDTO>();
            Assert.NotNull(tarefaBuscada);
            Assert.Equal(tarefaCriada.Id, tarefaBuscada.Id);
        }
    }
}
