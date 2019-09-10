using chat_api.Domain.Entities;
using System.Linq;

namespace chat_api.DTO.Output
{
    public class UsuarioOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Iniciais { get; set; }

        public UsuarioOutput(Usuario u)
        {
            this.Id = u.Id;
            this.Nome = u.Nome.Trim();

            if (Nome.Count(c => c == ' ') == 0) //Nome Simples
            {
                this.Iniciais = Nome.Substring(0, 2).ToUpper();
            }
            else //Nome Composto
            {
                this.Iniciais =
                    Nome.Substring(0, 1).ToUpper()
                    + Nome.Substring(Nome.LastIndexOf(' ') + 1, 1).ToUpper();
            }
        }
    }
}
