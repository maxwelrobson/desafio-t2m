using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Application.DTOs
{
    public record CriarTarefaRequestDTO(
    string Titulo,
    string? Descricao
);
}
