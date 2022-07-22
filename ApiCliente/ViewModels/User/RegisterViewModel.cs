using System.ComponentModel.DataAnnotations;

namespace ApiCliente.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email é invalido")]
        public string Email { get; set; }
    }
}
