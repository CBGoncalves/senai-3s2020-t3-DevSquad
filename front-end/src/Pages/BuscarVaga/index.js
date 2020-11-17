import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";

import AccessBar from "../../Components/AccessBar";
import Header from "../../Components/Header";
import AccessMenu from "../../Components/AccessMenu";
import Footer from "../../Components/Footer";
import Input from "../../Components/Input";

import imgEmpresa from "../../assets/Teste.webp";
import Tag from "../../Components/Tag/Index";
import InfoVaga from "../../Components/InfoVaga/Index";
import imgDesenvolvimento from "../../assets/web-programming.webp";
import imgLocalizacao from "../../assets/big-map-placeholder-outlined-symbol-of-interface.webp";
import imgSalario from "../../assets/money (1).webp";
import imgTipoContrato from "../../assets/gears.webp";
import imgFuncao from "../../assets/rocket-launch.webp";
import IconEmpresa from "../../assets/building.webp";

import banner from "../../assets/bannerBuscarVagas.webp";

import "./style.css";

export default function BuscarVaga() {
  let history = useHistory();
  const [ListVagas, SetListVagas] = useState([]);
  let [idVaga, SetIdVaga] = useState(0);

  useEffect(() => {
    listarVagas();
  }, []);

  function BuscarVagaPeloId(event) {
    localStorage.setItem("idVagaSelecionada", idVaga);
    history.push("/VisualizarVagaCandidato");
  }

  const listarVagas = () => {
    fetch("http://localhost:5000/api/Usuario/ListarTodasAsVagas", {
      method: "GET",
      headers: {
        authorization: "Bearer " + localStorage.getItem("token"),
      },
    })
      .then((response) => response.json())
      .then((dados) => {
        SetListVagas(dados);
      })
      .catch((err) => console.error(err));
  };

  const filtroExperiencia = (valorParaSerFiltrado) => {
    const resultado = ListVagas.filter(filtro => filtro.Experiencia == valorParaSerFiltrado);
    console.log(resultado);
    return resultado;
  };

  function FiltrarEstagio() {
    const arrayFiltrado = []
    for (var i = 0; listarVagas.length; i++) {
      if (listarVagas[i].experiencia == "Estagio") {
        arrayFiltrado.push(listarVagas[i])
      }
    }
  }

  const filtroTipoContrato = (valorParaSerFiltrado) => {
    const resultado = ListVagas.filter(filtro => filtro.TipoContrato == valorParaSerFiltrado);
    console.log(resultado);
    return resultado;
  };

  return (
    <body>
      <AccessBar />
      <Header />
      <AccessMenu />

      <div className="content-searchJobs">
        <div>
          <Input
            label="Busque sua vaga aqui"
            type="text"
            placeholder="Ex.: Desenvolvedor"
          />
          <button class="fa fa-search btn-search"></button>
          <img
            src={banner}
            alt="Pessoa utilizando um computador, que está em cima de uma mesa"
            className="imgBackground-searchJobs"
          />
        </div>

        <div className="main-content-search-jobs">
          <div id="filter-searchJobs">
            <button className="btn-active" id="btn-all" onClick={"all"}>
              Todas as vagas
            </button>
            <p><strong>Filtrar por tipo de contrato:</strong></p>
            <button className="btn-filter" onClick={filtroTipoContrato("clt")}>
              CLT
            </button>
            <button className="btn-filter" onClick={filtroTipoContrato("estagio")}>
              Estágio
            </button>
            <button className="btn-filter" onClick={filtroTipoContrato("pj")}>
              PJ
            </button>
            <p><strong>Filtrar por experiência:</strong></p>
            <button className="btn-filter" onClick={filtroExperiencia("junior")}>
              Júnior
            </button>
            <button className="btn-filter" onClick={filtroExperiencia("pleno")}>
              Pleno
            </button>
            <button className="btn-filter" onClick={filtroExperiencia("senior")}>
              Sênior
            </button>
          </div>

          <div className="vagas">
            {ListVagas.map((item) => {
              return (
                <div
                  key={item.idVaga}
                  className="vaga"
                  onClick={(event) => {
                    idVaga = item.idVaga;
                    BuscarVagaPeloId();
                  }}
                >
                  <div className="VagaCompleta">
                    <img
                      src={imgEmpresa}
                      className="ImagemEmpresa"
                      alt=""
                    ></img>
                    <div className="MainVaga">
                      <h3>Titulo da vaga</h3>
                      <div className="InfoVagas">
                        <InfoVaga
                          NomeProp={item.razaoSocial}
                          source={IconEmpresa}
                        />
                        <InfoVaga
                          NomeProp={item.localidade}
                          source={imgLocalizacao}
                        />
                        <InfoVaga
                          NomeProp={item.experiencia}
                          source={imgFuncao}
                        />
                        <InfoVaga
                          NomeProp={item.tipoContrato}
                          source={imgTipoContrato}
                        />
                        <InfoVaga NomeProp={item.salario} source={imgSalario} />
                        <InfoVaga
                          NomeProp={item.nomeArea}
                          source={imgDesenvolvimento}
                        />
                      </div>
                      <div className="TecnologiasVaga">
                        {item.tecnologias.map((tec) => {
                          return <Tag key={tec} NomeTag={tec}></Tag>;
                        })}
                      </div>
                    </div>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>
      <Footer />
    </body>
  );
}
