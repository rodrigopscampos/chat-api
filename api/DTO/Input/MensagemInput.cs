using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chat_api.DTO.Input
{
    public class MensagemInput
    {
        [Required]
        public string Remetente { get; set; }

        [Required]
        public string Texto { get; set; }

        public string Destinatario { get; set; }

        public bool? Reservada { get; set; }
    }
}
