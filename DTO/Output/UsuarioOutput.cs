using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whatsapp_api.Domain.Entities;

namespace whatsapp_api.DTO.Output
{
    public class UsuarioOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public UsuarioOutput(Usuario u)
        {
            this.Id = u.Id;
            this.Nome = u.Nome;
        }
    }
}
