using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_api.DTO.Output
{
    public class UsuarioPostOutput
    {
        public string Erro { get; set; }
        public string Token { get; set; }

        public UsuarioPostOutput(string erro = null, string token = null)
        {
            this.Erro = erro;
            this.Token = token;
        }
    }
}
