using GerenciadorDeTarefas.Application.DTOs;
using GerenciadorDeTarefas.Domain;
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

        [Fact]
        public async Task Put_ShouldUpdateExistingTask()
        {
            // Arrange
            // Cria uma tarefa para poder atualizá-la
            var novaTarefaDto = new CriarTarefaRequestDTO("Tarefa para Atualizar", "Descrição inicial.");
            var postResponse = await _client.PostAsJsonAsync("/api/tarefas", novaTarefaDto);
            var tarefaCriada = await postResponse.Content.ReadFromJsonAsync<TarefaResponseDTO>();
            Assert.NotNull(tarefaCriada);

            // Cria um DTO para atualizar a tarefa
            var updateDto = new AtualizarTarefaRequestDTO("Tarefa Atualizada!", "Descrição atualizada.", StatusTarefa.Concluida);

            // Act
            // Atualiza a tarefa que acabamos de criar
            var putResponse = await _client.PutAsJsonAsync($"/api/tarefas/{tarefaCriada.Id}", updateDto);

            // Assert - Verifique se a atualização foi bem-sucedida
            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
            var tarefaRetornada = await putResponse.Content.ReadFromJsonAsync<TarefaResponseDTO>();
            Assert.NotNull(tarefaRetornada);
            Assert.Equal("Tarefa Atualizada!", tarefaRetornada.Titulo);
            Assert.Equal(StatusTarefa.Concluida, tarefaRetornada.Status);
        }

        [Fact]
        public async Task Delete_ShouldRemoveExistingTask()
        {
            // Arrange
            // Cria uma tarefa para poder deletá-la
            var novaTarefaDto = new CriarTarefaRequestDTO("Tarefa para Deletar", "Descrição.");
            var postResponse = await _client.PostAsJsonAsync("/api/tarefas", novaTarefaDto);
            var tarefaCriada = await postResponse.Content.ReadFromJsonAsync<TarefaResponseDTO>();
            Assert.NotNull(tarefaCriada);

            // Act
            // Deleta a tarefa
            var deleteResponse = await _client.DeleteAsync($"/api/tarefas/{tarefaCriada.Id}");

            // Assert
            // Verifica se o delete foi bem-sucedido
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode); // 204 No Content é o sucesso para DELETE

            //Tenta buscar a tarefa deletada e verifique se ela não é encontrada (404)
            var getResponse = await _client.GetAsync($"/api/tarefas/{tarefaCriada.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
