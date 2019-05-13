using System.Collections.Generic;
using chat_api.Domain.Entities;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;

namespace chat_api.DAL
{
    public class RepositorioMySql : IRepositorio
    {
        public void AddMensagem(MensagemInput mensagem)
        {
            throw new System.NotImplementedException();
        }

        public bool AddUsuario(UsuarioInput usuario, out string erro)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Mensagem> GetMensagens(int sequencial)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Usuario> GetUsuarios(int sequencial)
        {
            throw new System.NotImplementedException();
        }
    }
}