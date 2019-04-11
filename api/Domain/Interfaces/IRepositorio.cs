using System.Collections.Generic;
using chat_api.Domain.Entities;
using chat_api.DTO.Input;

namespace chat_api.Domain.Interfaces
{
    public interface IRepositorio
    {
        IEnumerable<Usuario> GetUsuarios();
        bool TryAddUsuario(UsuarioInput usuario, out int id);

        IEnumerable<Mensagem> GetMensagens(int destinatario, int seqnumInicio);
        bool TryAddMensagem(MensagemInput mensagem);
    }
}