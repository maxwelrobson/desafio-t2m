using GerenciadorDeTarefas.Application.DTOs;
using GerenciadorDeTarefas.Domain;

namespace GerenciadorDeTarefas.Application
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<TarefaResponseDTO> CriarTarefaAsync(CriarTarefaRequestDTO tarefaDTO)
        {
            //Mapeia o DTO para a Entidade
            var tarefa = new Tarefa
            {
                Id = Guid.NewGuid(),
                Titulo = tarefaDTO.Titulo,
                Descricao = tarefaDTO.Descricao,
                Status = StatusTarefa.Pendente, //status de inicio
                DataCriacao = DateTime.UtcNow
            };

            //Chama repositório para adicionar a tarefa no banco de dados
            await _tarefaRepository.AdicionarAsync(tarefa);

            //Retorna o DTO de resposta
            return new TarefaResponseDTO(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Descricao,
                tarefa.Status,
                tarefa.DataCriacao,
                tarefa.DataConclusao
            );
        }

        public async Task<TarefaResponseDTO?> AtualizarTarefaAsync(Guid id, AtualizarTarefaRequestDTO tarefaDTO)
        {
            //Busca a tarefa pelo ID
            var tarefaExistente = await _tarefaRepository.ObterPorIdAsync(id);
            if (tarefaExistente == null)
            {
                return null; // Tarefa não encontrada
            }

            //Atualiza os campos da tarefa existente
            tarefaExistente.Titulo = tarefaDTO.Titulo;
            tarefaExistente.Descricao = tarefaDTO.Descricao;
            tarefaExistente.Status = tarefaDTO.Status;

            //Se a tarefa for concluída, define a data de conclusão
            if (tarefaExistente.Status == StatusTarefa.Concluida)
            {
                tarefaExistente.DataConclusao = DateTime.UtcNow;
            }
            else
            {
                tarefaExistente.DataConclusao = null;  // Se não for concluída, usa data de conclusão como null
            }

            //Chama repositório para atualizar a tarefa no banco de dados
            await _tarefaRepository.AtualizarAsync(tarefaExistente);

            //Retorna o DTO de resposta atualizado
            return new TarefaResponseDTO(
                tarefaExistente.Id,
                tarefaExistente.Titulo,
                tarefaExistente.Descricao,
                tarefaExistente.Status,
                tarefaExistente.DataCriacao,
                tarefaExistente.DataConclusao
            );
        }

        public async Task<bool> RemoverTarefaAsync(Guid id)
        {
            //Busca a tarefa pelo ID
            var tarefaExistente = await _tarefaRepository.ObterPorIdAsync(id);
            if (tarefaExistente is null)
            {
                return false; // Tarefa não encontrada
            }

            await _tarefaRepository.RemoverAsync(id);
            return true; // Tarefa removida com sucesso
        }

        public async Task<TarefaResponseDTO?> ObterTarefaPorIdAsync(Guid id)
        {
            //Busca a tarefa pelo ID
            var tarefa = await _tarefaRepository.ObterPorIdAsync(id);
            if (tarefa is null) return null; // Tarefa não encontrada

            return new TarefaResponseDTO(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Descricao,
                tarefa.Status,
                tarefa.DataCriacao,
                tarefa.DataConclusao
            );
        }

        public async Task<IEnumerable<TarefaResponseDTO>> ObterTodasAsTarefasAsync()
        {
            //Busca todas as tarefas
            var tarefas = await _tarefaRepository.ObterTodasAsync();
            //Mapeia as tarefas para o DTO de resposta
            return tarefas.Select(tarefa => new TarefaResponseDTO(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Descricao,
                tarefa.Status,
                tarefa.DataCriacao,
                tarefa.DataConclusao
            ));
        }
    }
}
