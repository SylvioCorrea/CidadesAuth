using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CidadesAuth.Models;
using CidadesAuth.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CidadesAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ICidadesRepository cidadesRepository;
        private IConfiguration config;
        public TokenController(ICidadesRepository cidadesRepository, IConfiguration config)
        {
            this.cidadesRepository = cidadesRepository;
            this.config = config;
        }

        //Este metodo devolve o token jwt necessario para autenticacao
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            if(usuario != null)
            {
                Usuario user = await cidadesRepository.GetUsuario(usuario);
                if (user != null)
                {
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id_Usuario", user.Id_Usuario.ToString()),
                        new Claim("Nome", user.Nome)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
            }
            return BadRequest("Usuario ou senha invalidos");
        }
    }
}
