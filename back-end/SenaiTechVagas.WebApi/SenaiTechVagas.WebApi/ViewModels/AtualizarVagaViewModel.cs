﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiTechVagas.WebApi.ViewModels
{
    public class AtualizarVagaViewModel
    {
        public string DescricaoVaga { get; set; }
        public string TituloVaga { get; set; }
        public int IdArea { get; set; }
        public string DescricaoEmpresa { get; set; }
        public string DescricaoBeneficio { get; set; }
        public string Experiencia { get; set; }
        public string TipoContrato { get; set; }
        public decimal Salario { get; set; }
        public string Localidade { get; set; }
        public string Estado { get; set; }

        [StringLength(8, MinimumLength = 8)]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public int idTipoPresenca { get; set; }
    }
}
