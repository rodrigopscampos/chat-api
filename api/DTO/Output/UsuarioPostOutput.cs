using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_api.DTO.Output
{
    public class UsuarioPostOutput
    {
        public bool Sucesso { get; set; }
        public string Erro { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }
        public string Location { get; set; }

        public UsuarioPostOutput(int id, string token, string location)
        {
            this.Id = id;
            this.Sucesso = true;
            this.Token = token;
            this.Location = location;
        }

        public UsuarioPostOutput(bool sucesso, string erro)
        {
            this.Sucesso = sucesso;
            this.Erro = erro;
        }
    }
}
