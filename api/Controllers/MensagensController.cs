using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;
using chat_api.DTO.Output;

namespace chat_api.Controllers
{
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
        /// <param name="destinatario">ID do destinatários das mensagens</param>
        /// <param name="seqnum">ID da última mensagem consultada, para filtrar apenas as novas</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<MensagemOutput>> Get(int destinatario, int seqnum)
        {
            return _repositorio.GetMensagens(destinatario, seqnum)
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
            if (!_repositorio.TryAddMensagem(mensagem))
            {
                return BadRequest(new MensagemPostOutput(sucesso: false, erro: "Destinatário não exite"));
            }

            return Ok();
        }
    }
}
