using System.Collections.Generic;
using chat_api.Domain.Entities;
using chat_api.DTO.Input;

namespace chat_api.Domain.Interfaces
{
    public interface IRepositorio
    {
        IEnumerable<Usuario> GetUsuarios(int sequencial);
        IEnumerable<Mensagem> GetMensagens(int sequencial);

        bool AddUsuario(UsuarioInput usuario, out string erro);
        void AddMensagem(MensagemInput mensagem);
    }
}