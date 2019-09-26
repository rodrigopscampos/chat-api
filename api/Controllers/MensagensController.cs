using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;
using chat_api.DTO.Output;
using Microsoft.AspNetCore.Authorization;

namespace chat_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class MensagensController : ControllerBase
    {
        readonly IRepositorio _repositorio;

        public MensagensController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Consulta as mensagens postadas
        /// </summary>
        /// <param name="sequencial">ID do destinatários das mensagens</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MensagemOutput>> Get(int sequencial)
        {
            return _repositorio.GetMensagens(sequencial)
                .Select(m => new MensagemOutput(m))
                .ToArray();
        }

        /// <summary>
        /// Posta uma nova mensagem
        /// </summary>
        /// <param name="mensagem"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post(MensagemInput mensagem)
        {
            _repositorio.AddMensagem(mensagem);
            return Ok();
        }
    }
}
