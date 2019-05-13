using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_api.Domain.Entities;

namespace chat_api.DTO.Output
{
    public class MensagemOutput
    {
        public int Id { get; set; }
        public string Remetente { get; set; }
        public string Texto { get; set; }
        public string Destinatario { get; }
        public bool Reservada { get; set; }

        public MensagemOutput(Mensagem mensagem)
        {
            this.Id = mensagem.Id;
            this.Remetente = mensagem.Rementente;
            this.Texto = mensagem.Texto;
            this.Destinatario = mensagem.Destinatario;
            this.Reservada = mensagem.Reservada;
        }
    }
}
