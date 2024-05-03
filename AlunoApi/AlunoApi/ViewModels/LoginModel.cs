using System.ComponentModel.DataAnnotations;

namespace AlunoApi.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email é obrigátorio")]
        [EmailAddress(ErrorMessage = "Formato de email inválido")]

        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no minimo {2} e no máximo {1} caracteres.", MinimumLength = 10)]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
