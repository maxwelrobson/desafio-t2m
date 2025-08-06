using GerenciadorDeTarefas.Domain;

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
