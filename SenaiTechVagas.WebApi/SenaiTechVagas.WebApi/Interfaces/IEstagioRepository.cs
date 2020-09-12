﻿using SenaiTechVagas.WebApi.Domains;
using SenaiTechVagas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiTechVagas.WebApi.Interfaces
{
    interface IEstagioRepository:InterfaceBase
    {
        List<Estagio> ListarEstagios();
        bool CadastrarEstagio(Estagio estagio);
        bool DeletarPorId(int idEstagio);
        Estagio BuscarPorId(int idEstagio);
        bool AtualizarPorIdCorpo(int idEstagio, Estagio estagioAtualizado);
    }
}
