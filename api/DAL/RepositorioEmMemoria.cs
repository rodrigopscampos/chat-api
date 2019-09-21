using System.Collections.Generic;
using System.Linq;
using chat_api.Domain.Entities;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;

namespace chat_api.DAL
{
    public class RepositorioEmMemoria : IRepositorio
    {
        List<Mensagem> _mensagens = new List<Mensagem>();
        int _mensagemId = 0;

        List<Usuario> _usuarios = new List<Usuario>();
        int _usuarioId = 0;

        public void AddMensagem(MensagemInput mensagem)
        {
            var entidade = new Mensagem()
            {
                Destinatario = mensagem.Destinatario,
                Rementente = mensagem.Remetente,
                Reservada = mensagem.Reservada.HasValue
                 ? mensagem.Reservada.Value
                 : false,
                Texto = mensagem.Texto,
                Id = ++_mensagemId
            };

            _mensagens.Add(entidade);
        }

        public bool AddUsuario(UsuarioInput usuario, out string erro)
        {
            if (_usuarios.Any(u => u.Nome == usuario.Nome))
            {
                erro = "Nome já está em uso";
                return false;
            }

            var entidade = new Usuario()
            {
                Nome = usuario.Nome,
                Id = ++_usuarioId
            };

            _usuarios.Add(entidade);
            erro = null;
            return true;
        }

        public IEnumerable<Mensagem> GetMensagens(int sequencial)
        {
            return _mensagens.Where(m => m.Id > sequencial);
        }

        public IEnumerable<Usuario> GetUsuarios(int sequencial)
        {
            return _usuarios.Where(u => u.Id > sequencial);
        }
    }
}
