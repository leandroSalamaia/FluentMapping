namespace ApiCliente.ViewModels.Tarefa
{
    public class CreateOrEditTarefaViewModel
    {
        public int StatusTarefa { get; set; }
        public int TipoTarefa { get; set; }
        public Guid ClienteId { get; set; }
        public string Descricao { get; set; }
    }
}