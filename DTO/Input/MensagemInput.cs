using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace whatsapp_api.DTO.Input
{
    public class MensagemInput
    {
        public int Remetente { get; set; }
        public string Texto { get; set; }
        public int Destinatario { get; set; }
    }
}
