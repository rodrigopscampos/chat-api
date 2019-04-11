using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_api.DTO.Output
{
    public class MensagemPostOutput
    {
        public bool Sucesso { get; set; }
        public string Erro { get; set; }

        public MensagemPostOutput(bool sucesso, string erro)
        {
            this.Sucesso = sucesso;
            this.Erro = erro;
        }
    }
}
