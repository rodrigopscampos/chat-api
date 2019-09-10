using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;
using chat_api.DTO.Output;
using System;

namespace chat_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class UsuariosController : ControllerBase
    {
        readonly IRepositorio _repositorio;

        public UsuariosController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Consulta a lista de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioOutput>> Get(int usuarioId)
        {
            var usuarios = _repositorio
                .GetUsuarios()
                .Where(u => u.Id != usuarioId)
                .Where(u => u.UltimoRequest > DateTime.Now.AddSeconds(-5));

            AtualizaDataUltimaInteracao(usuarioId);

            return usuarios.Select(u => new UsuarioOutput(u)).ToArray();
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<UsuarioPostOutput> Post([FromBody] UsuarioInput usuario)
        {
            if (_repositorio.TryAddUsuario(usuario, out var id))
            {
                var location = "http://localhost:5000";
                return new UsuarioPostOutput(
                    id,
                    Guid.NewGuid().ToString(),
                    location);
            }
            else
            {
                return new UsuarioPostOutput(sucesso: false, erro: "Nome já utilizado");
            }
        }

        private void AtualizaDataUltimaInteracao(int usuarioId)
        {
            _repositorio.AtualizaDataUltimaInteracao(usuarioId);
        }
    }
}