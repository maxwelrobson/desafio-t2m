using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Domain
{
    public interface ITarefaRepository
    {
        Task<Tarefa?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Tarefa>> ObterTodasAsync();
        Task AdicionarAsync(Tarefa tarefa);
        Task AtualizarAsync(Tarefa tarefa);
        Task RemoverAsync(Guid id);
    }
}
