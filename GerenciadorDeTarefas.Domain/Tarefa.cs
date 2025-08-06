namespace GerenciadorDeTarefas.Domain
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string? Descricao { get; set; }
        public StatusTarefa Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }

    }

    public enum StatusTarefa
    {
        Pendente,
        EmProgresso,
        Concluida,

    }
}
