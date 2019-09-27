using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using chat_api.Domain.Interfaces;
using chat_api.DTO.Input;
using chat_api.DTO.Output;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Cryptography;

namespace chat_api.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult<UsuarioPostOutput> Post([FromBody] UsuarioInput usuario)
        {
            string erro;
            if (!_repositorio.AddUsuario(usuario, out erro))
            {
                return BadRequest(new UsuarioPostOutput(erro: erro));
            }

            var token = GerarTokenParaUsuario(usuario.Nome);
            return Ok(new UsuarioPostOutput(token: token));
        }

        private static string GerarTokenParaUsuario(string nmUsuario)
        {
            var geradorToken = new JwtSecurityTokenHandler();

            //precisa ter no mínimo 16 caracteres
            var key = Encoding.ASCII.GetBytes("a-senha-precisa-ser-grande");

            var descricaoToken = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = "chat-api",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("cli", nmUsuario)
                })
            };

            var token = geradorToken.CreateToken(descricaoToken);
            var tokenStr = geradorToken.WriteToken(token);
            return tokenStr;
        }
    }
}