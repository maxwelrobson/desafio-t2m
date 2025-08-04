using GerenciadorDeTarefas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Application.DTOs
{
    public record TarefaResponseDTO(
        Guid Id,
        string Titulo,
        string? Descricao,
        StatusTarefa Status,
        DateTime DataCriacao,
        DateTime? DataConclusao
     );
}
