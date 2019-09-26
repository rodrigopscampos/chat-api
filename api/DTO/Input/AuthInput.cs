using System.ComponentModel.DataAnnotations;

namespace chat_api.DTO.Input
{
    public class AuthInput
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
