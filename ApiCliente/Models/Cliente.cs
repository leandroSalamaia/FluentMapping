namespace ApiCliente.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public int TipoCliente { get; set; }
        public PessoaFisica PessoaFisica { get; set; }
        public PessoaJuridica PessoaJuridica { get; set; }
        public IList<Contato> Contatos { get; set; }
        public Endereco Endereco { get; set; }
    }
}
