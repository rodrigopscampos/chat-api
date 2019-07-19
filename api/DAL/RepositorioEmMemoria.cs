using System;
using System.Collections.Generic;
using System.Linq;
using chat_api.Domain.Entities;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;

namespace chat_api.DAL
{
    public class RepositorioEmMemoria : IRepositorio
    {
        List<Usuario> _usuarios = new List<Usuario>()
        {
            new Usuario { Id = 1, Nome = "Rodrigo" },
            new Usuario { Id = 2, Nome = "Gabriel" },
            new Usuario { Id = 3, Nome = "Oliver" },
        };

        static int m = 0;
        List<Mensagem> _mensagens = new List<Mensagem>()
        {
            new Mensagem { Id = m++, Rementente = 1, Destinatario = 2, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 2, Destinatario = 1, Texto = Guid.NewGuid().ToString() },
            new Mensagem { Id = m++, Rementente = 1, Destinatario = 2, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 2, Destinatario = 1, Texto = Guid.NewGuid().ToString() },
            new Mensagem { Id = m++, Rementente = 1, Destinatario = 2, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 2, Destinatario = 1, Texto = Guid.NewGuid().ToString() },
            new Mensagem { Id = m++, Rementente = 1, Destinatario = 2, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 2, Destinatario = 1, Texto = Guid.NewGuid().ToString() },

            new Mensagem { Id = m++, Rementente = 1, Destinatario = 3, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 3, Destinatario = 1, Texto = Guid.NewGuid().ToString() },
            new Mensagem { Id = m++, Rementente = 1, Destinatario = 3, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 3, Destinatario = 1, Texto = Guid.NewGuid().ToString() },
            new Mensagem { Id = m++, Rementente = 1, Destinatario = 3, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 3, Destinatario = 1, Texto = Guid.NewGuid().ToString() },
            new Mensagem { Id = m++, Rementente = 1, Destinatario = 3, Texto = Guid.NewGuid().ToString()},
            new Mensagem { Id = m++, Rementente = 3, Destinatario = 1, Texto = Guid.NewGuid().ToString() },
        };

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

        public bool TryAddMensagem(MensagemInput mensagem)
        {
            var id = _mensagens.Any() ? _mensagens.Max(u => u.Id) + 1 : 1;

            _mensagens.Add(new Mensagem
            {
                Id = id,
                Texto = mensagem.Texto,
                Rementente = mensagem.Remetente,
                Destinatario = mensagem.Destinatario
            });

            return true;
        }

        public IEnumerable<Mensagem> GetMensagens(int destinatario, int seqnumInicio)
        {
            return _mensagens
                .Where(m => (m.Destinatario == destinatario || m.Rementente == destinatario) && m.Id > seqnumInicio)
                .OrderBy(m => m.Id);
        }
    }
}
