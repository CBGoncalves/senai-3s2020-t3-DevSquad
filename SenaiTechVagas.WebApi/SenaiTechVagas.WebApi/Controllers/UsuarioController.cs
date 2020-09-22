﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiTechVagas.WebApi.Domains;
using SenaiTechVagas.WebApi.Interfaces;
using SenaiTechVagas.WebApi.Repositories;
using SenaiTechVagas.WebApi.ViewModels;

namespace SenaiTechVagas.WebApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository usuarioRepository { get; set; }

        public UsuarioController()
        {
            usuarioRepository = new UsuarioRepository();
        }
        /// <summary>
        /// Recebe um tipo cadastrar 'CandidatoCandidatoViewModel' preenchendo os dados 
        /// necessários para que, então, seja adicionado um novo objeto no banco de dados.
        /// </summary>
        /// <param name="NovoCandidato">Do tipo 'CadastrarCandidatoViewModel', que se refere
        /// ao dados inseridos pelo usuário na requisição
        /// </param>
        /// <returns>Retorna um HTTP Code (201) e a menssagem: Novo candidato inserido com
        /// sucesso, caso contrário, retorna um HTTP Code (400) e a mensagem: Um erro 
        /// ocorreu ao receber a sua requisição.
        /// </returns>
        [HttpPost("CadastrarCandidato")]
        public IActionResult CadastrarCandidato(CadastrarCandidatoViewModel NovoCandidato)
        {
            try
            {
                if (usuarioRepository.VerificarSeEmailJaFoiCadastrado(NovoCandidato.Email))
                    return BadRequest("Esse email ja foi cadastrado em nosso sistema");

                if (usuarioRepository.VerificarSeCandidatoJaFoiCadastrado(NovoCandidato.Cpf))
                    return BadRequest("Esse cpf ja foi cadastrado em nosso sistema");

                if (usuarioRepository.CadastrarCandidato(NovoCandidato))
                    return Ok("Novo candidato inserido com sucesso!");
                else
                    return BadRequest("Um erro ocorreu ao receber a sua requisição.");
            }
            catch (Exception)
            {
                return BadRequest("Uma exceção ocorreu. Tente novamente.");
            }
        }

        /// <summary>
        /// Insere um novo objeto empresa na tabela Empresa com as informações 
        /// passadas na requisição
        /// </summary>
        /// <param name="empresa">Dados de um objeto, do tipo empresa, que é 
        /// passado na requisição.</param>
        /// <returns>Retorna um HTTP Code (201) e a mensagem: Novo candidato 
        /// inserido com sucesso!, caso contrário, retorna um HTTP Code (400)
        /// e a mensagem,podendo ser "Uma exceção ocorreu. Tente novamente.", 
        /// caso ocorra uma exceção, ou "Um erro ocorreu ao receber a sua 
        /// requisição.", caso algum erro tenha acontecido ao executar o método
        /// implementado em ../Repositories/EmpresaRepository.cs.</returns>
        [HttpPost("CadastrarEmpresa")]
        public IActionResult CadastrarEmpresa(CadastrarEmpresaViewModel empresa)
        {
            try
            {
                if (usuarioRepository.VerificarSeEmailJaFoiCadastrado(empresa.Email))
                    return BadRequest("Esse email ja foi cadastrado em nosso sistema");

                if (usuarioRepository.VerificarSeEmpresaJaFoiCadastrada(empresa.Cnpj))
                    return BadRequest("Esse cnpj ja foi cadastrado em nosso sistema");

                if (usuarioRepository.CadastrarEmpresa(empresa))
                    return Ok("Nova empresa cadastrada com sucesso!");
                else
                    return BadRequest("Um erro ocorreu e nao foi possivel efetuar o cadastro.");
            }
            catch (Exception)
            {
                return BadRequest("Uma exceção ocorreu. Tente novamente.");
            }
        }

        /// <summary>
        /// Método que atualiza a senha do usuário pelo identificador e o objeto.
        /// </summary>
        /// <param name="usuarioAtualizado">Objeto do usuário</param>
        /// <returns>Retorna um usuário atualizado pelo id e o objeto</returns>
        [Authorize(Roles = "1")]
        [HttpPut("AtualizarUsuario")]
        public IActionResult AtualizaDadosUsuario(AtualizarUsuarioViewModel usuarioAtualizado)
        {
            try
            {
                var idUsuario = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                if (usuarioRepository.AtualizarUsuario(idUsuario, usuarioAtualizado))
                    return Ok("Usuario atuakizado com sucesso");
                else
                    return BadRequest("Não foi possivel atualizar os dados do usuario veja se preencheu corretamente");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Lista todas as vagas publicadas
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("ListarTodasAsVagas")]
        public IActionResult ListarVagasEmGeral()
        {
            try
            {
                return Ok(usuarioRepository.ListarVagasEmGeral());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Lista vagas pelo filtro tipo de contrato
        /// </summary>
        /// <param name="TipoContrato"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("TipoContrato/{TipoContrato}")]
        public IActionResult ListarVagasFiltroTipoContarto(string TipoContrato)
        {
            try
            {
                return Ok(usuarioRepository.ListarFiltroTipoContrato(TipoContrato));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Lista vagas pelo filtro por nivel de experiencia
        /// </summary>
        /// <param name="NivelExperiencia"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("NivelExperiencia/{NivelExperiencia}")]
        public IActionResult ListarVagas(string NivelExperiencia)
        {
            try
            {
                return Ok(usuarioRepository.ListarFiltroNivelExperiencia(NivelExperiencia));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Lista vagas pela barra de pesquisa nome da tecnologia
        /// </summary>
        /// <param name="NomeTecnologia"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Tecnologia/{NomeTecnologia}")]
        public IActionResult ListarVagasPelaTecnologia(string NomeTecnologia)
        {
            try
            {
                return Ok(usuarioRepository.ListarPesquisaTecnologia(NomeTecnologia));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
