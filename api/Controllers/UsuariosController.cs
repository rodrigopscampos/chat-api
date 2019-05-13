using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;
using chat_api.DTO.Output;

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
        public ActionResult<IEnumerable<UsuarioOutput>> Get([FromQuery] int sequencial = 0)
        {
            var usuarios = _repositorio.GetUsuarios(sequencial);
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
            string erro;
            if (!_repositorio.AddUsuario(usuario, out erro))
            {
                return BadRequest(new UsuarioPostOutput(sucesso: false, erro: erro));
            }

            return Ok(new UsuarioPostOutput(sucesso: true));
        }
    }
}