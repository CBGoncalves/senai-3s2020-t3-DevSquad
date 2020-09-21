﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SenaiTechVagas.WebApi.Domains;
using SenaiTechVagas.WebApi.Interfaces;
using SenaiTechVagas.WebApi.Repositories;
using SenaiTechVagas.WebApi.ViewModels;

namespace SenaiTechVagas.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository usuarioRepository { get; set; }

        public LoginController() { usuarioRepository = new UsuarioRepository(); }

        /// <summary>
        /// O usuario passa um email e senha e recebe um token de volta e é considerado logado pelo sistema
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult LoginUsuario(LoginViewModel login)
        {           
            Usuario usuarioLogar = usuarioRepository.Logar(login.Email, login.Senha);
            if (usuarioLogar == null)
                return BadRequest("Suas credenciais não são validas");

            if (usuarioLogar.IdTipoUsuario == 4)
                return BadRequest("Voce foi banido por tempo indeterminado,em caso de engano entre em contato com o senai");

            var claims = new[]
              {
                new Claim(JwtRegisteredClaimNames.Email, usuarioLogar.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioLogar.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioLogar.IdTipoUsuario.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("SenaiTechVagas-chave-autenticacao"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "SenaiTechVagas.WebApi",         // emissor do token
                audience: "SenaiTechVagas.WebApi",       // destinatário do token
                claims: claims,                          // dados definidos acima
                expires: DateTime.Now.AddMinutes(30),    // tempo de expiração
                signingCredentials: creds                // credenciais do token
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}