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
        public int Remetente { get; set; }

        [Required]
        public string Texto { get; set; }

        [Required]
        public int Destinatario { get; set; }
    }
}
