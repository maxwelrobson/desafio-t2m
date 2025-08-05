using GerenciadorDeTarefas.Application;
using GerenciadorDeTarefas.Application.DTOs;
using GerenciadorDeTarefas.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GerenciadorDeTarefas.Tests
{
    
    public class TarefaServiceTests
    {
        private readonly Mock<ITarefaRepository> _mockRepo;
        private readonly TarefaService _tarefaService;

        //Construtor para inicializar o Mock e o Serviço para todos os testes
        public TarefaServiceTests()
        {
            _mockRepo = new Mock<ITarefaRepository>();
            _tarefaService = new TarefaService(_mockRepo.Object);
        }

        [Fact]
        public async Task ObterTarefaPorIdAsync_QuandoTarefaExiste_DeveRetornarTarefaResponseDTO()
        {
            //Arrange (Arrumar)
            var idDaTarefa = Guid.NewGuid();
            var tarefaFalsa = new Tarefa
            {
                Id = idDaTarefa,
                Titulo = "Tarefa de Teste",
                Status = StatusTarefa.Pendente,
                DataCriacao = DateTime.UtcNow
            };

            //Cria um mock do repositório
            var mockRepository = new Mock<ITarefaRepository>();

            //Configura o mock para retornar a tarefa falsa quando o método ObterPorIdAsync for chamado
            mockRepository.Setup(mockRepository => mockRepository.ObterPorIdAsync(idDaTarefa))
                .ReturnsAsync(tarefaFalsa);

            //Cria uma instância do serviço de tarefas com o repositório mockado
            var tarefaService = new TarefaService(mockRepository.Object);

            //Act (Agir)
            //Executa o método que será testado
            var resultado = await tarefaService.ObterTarefaPorIdAsync(idDaTarefa);

            //Assert (Afirmar)
            Assert.NotNull(resultado); // Verifica se o resultado não é nulo
            Assert.IsType<TarefaResponseDTO>(resultado); // Verifica se o resultado é do tipo TarefaResponseDTO
            Assert.Equal(idDaTarefa, resultado.Id); // Verifica se o ID da tarefa retornada é o mesmo que o ID da tarefa falsa
        }

        [Fact]
        public async Task CriarTarefaAsync_ComDadosValidos_DeveAdicionarEretornarDTO()
        {
            // Arrange
            var criarDto = new CriarTarefaRequestDTO("Nova Tarefa", "Descrição da nova tarefa");
            // O Moq apenas registra que a chamada foi feita.

            // Act
            var resultado = await _tarefaService.CriarTarefaAsync(criarDto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(criarDto.Titulo, resultado.Titulo);

            // Verifica se o método AdicionarAsync do repositório foi chamado somente uma vez.
            _mockRepo.Verify(repo => repo.AdicionarAsync(It.IsAny<Tarefa>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarTarefaAsync_QuandoTarefaExiste_DeveAtualizarEretornarDTO()
        {
            // Arrange
            var idDaTarefa = Guid.NewGuid();
            var tarefaExistente = new Tarefa { Id = idDaTarefa, Titulo = "Título Antigo", Status = StatusTarefa.Pendente };
            var atualizarDto = new AtualizarTarefaRequestDTO("Título Novo", "Descrição Nova", StatusTarefa.EmProgresso);

            _mockRepo.Setup(repo => repo.ObterPorIdAsync(idDaTarefa)).ReturnsAsync(tarefaExistente);

            // Act
            var resultado = await _tarefaService.AtualizarTarefaAsync(idDaTarefa, atualizarDto);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(atualizarDto.Titulo, resultado.Titulo);
            Assert.Equal(atualizarDto.Status, resultado.Status);

            _mockRepo.Verify(repo => repo.AtualizarAsync(It.IsAny<Tarefa>()), Times.Once);
        }

        [Fact]
        public async Task RemoverTarefaAsync_QuandoTarefaExiste_DeveRetornarTrue()
        {
            // Arrange
            var idDaTarefa = Guid.NewGuid();
            var tarefaExistente = new Tarefa { Id = idDaTarefa, Titulo = "Tarefa para deletar" };

            _mockRepo.Setup(repo => repo.ObterPorIdAsync(idDaTarefa)).ReturnsAsync(tarefaExistente);

            // Act
            var resultado = await _tarefaService.RemoverTarefaAsync(idDaTarefa);

            // Assert
            Assert.True(resultado);
            _mockRepo.Verify(repo => repo.RemoverAsync(idDaTarefa), Times.Once);
        }

    }
}
