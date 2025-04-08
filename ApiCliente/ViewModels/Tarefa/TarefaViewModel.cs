namespace ApiCliente.ViewModels.Tarefa
{
    public class TarefaViewModel
    {
        public Guid Id { get; set; }
        public int StatusTarefa { get; set; }
        public int TipoTarefa { get; set; }
        public Guid ClienteId { get; set; }
        public string Descricao { get; set; }
    }
}