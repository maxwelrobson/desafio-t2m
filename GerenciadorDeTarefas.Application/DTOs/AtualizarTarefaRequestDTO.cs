using GerenciadorDeTarefas.Domain;

namespace GerenciadorDeTarefas.Application.DTOs
{
    public record AtualizarTarefaRequestDTO(
        string Titulo,
        string? Descricao,
        StatusTarefa Status
    );
}
