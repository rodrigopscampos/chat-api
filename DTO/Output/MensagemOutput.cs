using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whatsapp_api.Domain.Entities;

namespace whatsapp_api.DTO.Output
{
    public class MensagemOutput
    {
        public int Id { get; set; }
        public int Remetente { get; set; }
        public string Texto { get; set; }
        public int Destinatario { get; }

        public MensagemOutput(Mensagem mensagem)
        {
            this.Id = mensagem.Id;
            this.Remetente = mensagem.Rementente;
            this.Texto = mensagem.Texto;
            this.Destinatario = mensagem.Destinatario;
        }
    }
}
