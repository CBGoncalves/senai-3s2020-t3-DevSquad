﻿using SenaiTechVagas.WebApi.Domains;
using SenaiTechVagas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiTechVagas.WebApi.Interfaces
{
    interface ICandidatoRepository
    {
        bool AtualizarCandidato(int IdCandidato, AtualizarCandidatoViewModel CandidatoAtualizado);             
        List<VagaTecnologia> ListarInscricoes(int idUsuario);
        bool SeInscrever(Inscricao NovaInscricao);
        bool RevogarInscricao(int idInscricao,int idCandidato);
        /*------------VERIFICAÇÕES INICIO-------------*/
        Candidato BuscarCandidatoPorIdUsuario(int idUsuario);
        bool VerificarSeInscricaoExiste(int idVaga, int idCandidato);
        /*------------VERIFICAÇÕES FIM------------------*/

    }
}
