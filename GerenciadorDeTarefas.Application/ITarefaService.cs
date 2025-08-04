using GerenciadorDeTarefas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
