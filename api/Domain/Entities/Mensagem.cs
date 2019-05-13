using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_api.Domain.Entities
{
    public class Mensagem
    {
        public int Id { get; set; }
        public string Rementente { get; set; }
        public string Texto { get; set; }
        public string Destinatario { get; set; }
        public bool Reservada { get; set; }
    }
}
