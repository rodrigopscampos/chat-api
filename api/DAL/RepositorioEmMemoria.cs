using System.Collections.Generic;
using System.Linq;
using chat_api.Domain.Entities;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;

namespace chat_api.DAL
{
    public class RepositorioEmMemoria : IRepositorio
    {
        int _msgId = 0;
        int _usuarioId = 0;

        List<Mensagem> _mensagens = new List<Mensagem>();
        Dictionary<string, Usuario> _usuarios = new Dictionary<string, Usuario>();

        public IEnumerable<Usuario> GetUsuarios(int sequencial)
        {
            return _usuarios.Values.Where(u => u.Id > sequencial).ToArray();
        }

        public bool AddUsuario(UsuarioInput usuario, out string erro)
        {
            if (_usuarios.ContainsKey(usuario.Nome))
            {
                erro = "Apelido já está em uso";
                return false;
            }
            else
            {
                var id = ++_usuarioId;
                _usuarios.Add(usuario.Nome, new Usuario { Nome = usuario.Nome, Id = id });
                erro = null;
                return true;
            }
        }

        public void AddMensagem(MensagemInput mensagem)
        {
            _msgId++;

            var m = new Mensagem
            {
                Id = _msgId,
                Texto = mensagem.Texto,
                Rementente = mensagem.Remetente,
                Destinatario = mensagem.Destinatario,
                Reservada = mensagem.Reservada.HasValue ? mensagem.Reservada.Value : false
            };

            _mensagens.Add(m);
        }

        public IEnumerable<Mensagem> GetMensagens(int sequencial)
        {
            return _mensagens.Where(m => m.Id > sequencial).ToArray();
        }
    }
}
