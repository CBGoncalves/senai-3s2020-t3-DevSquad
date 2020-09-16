﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class CandidatoController : ControllerBase
    {
        // Este controlador está na sequência CRUD -  Criar, Ler, Atualizar e Deletar

        private ICandidatoRepository _candidatoRepository { get; set; }

        public CandidatoController()
        {
            _candidatoRepository = new CandidatoRepository();
        }

        [HttpPost]
        public IActionResult CadastrarCandidato(CadastrarCandidatoViewModel NovoCandidato)
        {
            try
            {
                if (_candidatoRepository.CadastrarCandidato(NovoCandidato))
                {
                    return Ok("Novo candidato inserido com sucesso!");
                }
                else
                {
                    return BadRequest("Um erro ocorreu ao receber a sua requisição.");
                }

            }
            catch (Exception)
            {
                return BadRequest("Uma exceção ocorreu. Tente novamente.");
            }
        }

        [HttpGet]
        public IActionResult ListarCandidatos()
        {
            try
            {
                return Ok(_candidatoRepository.ListarCandidatos());
            }
            catch(Exception)
            {
                return BadRequest("Uma exceção ocorreu. Tente novamente.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                return Ok(_candidatoRepository.BuscarPorId(id));
            }
            catch(Exception)
            {
                return BadRequest("Uma exceção ocorreu. Tente novamente.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCandidato(int id, Candidato candidato)
        {
            try
            {
                return Ok(_candidatoRepository.AtualizarCandidato(id, candidato));
            }
            catch
            {
                return BadRequest("Uma exceção ocorreu. Tente novamente.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeltarCandidato(int id)
        {
            try
            {
                return Ok(_candidatoRepository.DeletarCandidato(id));
            }
            catch
            {
                return BadRequest("Uma exceção ocorreu. Tente novamente.");
            }
        }
    }
}
