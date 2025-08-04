using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorDeTarefas.Domain;

namespace GerenciadorDeTarefas.Application.DTOs
{
    public record AtualizarTarefaRequestDTO(
        string Titulo,
        string? Descricao,
        StatusTarefa Status
    );
}
