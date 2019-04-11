using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chat_api.DTO.Input
{
    public class UsuarioInput
    {
        [Required]
        public string Nome { get; set; }
    }
}
