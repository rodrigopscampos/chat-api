using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whatsapp_api.Domain.Interfaces;
using whatsapp_api.DTO.Input;
using whatsapp_api.DTO.Output;

namespace whatsapp_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MensagensController : ControllerBase
    {
        readonly IRepositorio _repositorio;

        
        public MensagensController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ActionResult<IEnumerable<MensagemOutput>> Get(int destinatario, int seqnum)
        {
            return _repositorio.GetMensagens(destinatario, seqnum)
                .Select(m => new MensagemOutput(m))
                .ToArray();
        }

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
