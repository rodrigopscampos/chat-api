using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chat_api.Domain.Entities
{
    public class Mensagem
    {
        public int Id { get; set; }
        public int Rementente { get; set; }
        public string Texto { get; set; }
        public int Destinatario { get; set; }
    }
}
