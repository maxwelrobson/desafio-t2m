using GerenciadorDeTarefas.Application.DTOs;

namespace GerenciadorDeTarefas.Application
{
    public interface ITarefaService
    {
        Task<TarefaResponseDTO> CriarTarefaAsync(CriarTarefaRequestDTO tarefaDTO);
        Task<TarefaResponseDTO?> AtualizarTarefaAsync(Guid id, AtualizarTarefaRequestDTO tarefaDTO);
        Task<bool> RemoverTarefaAsync(Guid id);
        Task<TarefaResponseDTO?> ObterTarefaPorIdAsync(Guid id);
        Task<IEnumerable<TarefaResponseDTO>> ObterTodasAsTarefasAsync();
    }
}
