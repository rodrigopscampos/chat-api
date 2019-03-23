using System.Collections.Generic;
using whatsapp_api.Domain.Entities;
using whatsapp_api.DTO.Input;

namespace whatsapp_api.Domain.Interfaces
{
    public interface IRepositorio
    {
        IEnumerable<Usuario> GetUsuarios();
        bool TryAddUsuario(UsuarioInput usuario, out int id);
    }
}