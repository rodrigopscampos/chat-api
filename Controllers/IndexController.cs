using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace whatsapp_api.Controllers
{
    [Route("")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public string Get()
        {
            return "Bem vindo a API do WhatsApp do Senai";
        }
    }
}