namespace ApiCliente.Models
{
    public class Contato
    {
        public Guid Id { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
