using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using whatsapp_api.Domain.Interfaces;
using whatsapp_api.DTO.Input;
using whatsapp_api.DTO.Output;

namespace whatsapp_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        readonly IRepositorio _repositorio;

        public UsuariosController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ActionResult<IEnumerable<UsuarioOutput>> Get()
        {
            var usuarios = _repositorio.GetUsuarios();
            return usuarios.Select(u => new UsuarioOutput(u)).ToArray();
        }

        [HttpPost]
        public ActionResult<UsuarioPostOutput> Post([FromBody] UsuarioInput usuario)
        {
            if (_repositorio.TryAddUsuario(usuario, out var id))
            {
                return new UsuarioPostOutput(id);
            }
            else
            {
                return new UsuarioPostOutput(sucesso: false, erro: "Nome já utilizado");
            }
        }
    }
}