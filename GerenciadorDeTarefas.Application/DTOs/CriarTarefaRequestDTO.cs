namespace GerenciadorDeTarefas.Application.DTOs
{
    public record CriarTarefaRequestDTO(
    string Titulo,
    string? Descricao
);
}
