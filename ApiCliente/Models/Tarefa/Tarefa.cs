namespace ApiCliente.Models.Tarefa;

public class Tarefa
{
    public Guid Id { get; set; }
    public int StatusTarefa { get; set; }
    public int TipoTarefa { get; set; }
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public string Descricao { get; set; }
}