using System.Collections.Generic;
using System.Linq;
using whatsapp_api.Domain.Entities;
using whatsapp_api.Domain.Interfaces;
using whatsapp_api.DTO.Input;

namespace whatsapp_api.DAL
{
    public class RepositorioEmMemoria : IRepositorio
    {
        List<Usuario> _usuarios = new List<Usuario>();

        public IEnumerable<Usuario> GetUsuarios() => _usuarios;

        public bool TryAddUsuario(UsuarioInput usuario, out int id)
        {
            if (_usuarios.Any(u => u.Nome == usuario.Nome))
            {
                id = 0;
                return false;
            }

            id = _usuarios.Any() ? _usuarios.Max(u => u.Id) + 1 : 1;

            _usuarios.Add(new Usuario()
            {
                Nome = usuario.Nome,
                Id = id
            });

            return true;
        }
    }
}
