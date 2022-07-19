namespace ApiCliente.ViewModels.Cliente
{
    public class CreateOrEditClienteViewModel
    {
        public int TipoCliente { get; set; }
        public CreatePessoaFisicaViewModel PessoaFisica { get; set; }
        public CreatePessoaJuridicaViewModel PessoaJuridica { get; set; }
        public IList<CreateContatoViewModel> Contatos { get; set; }
        public CreateEnderecoViewModel Endereco { get; set; }
    }
}
