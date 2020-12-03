import React, { useState, useEffect } from 'react';

import AccessBar from '../../Components/AccessBar';
import Header from '../../Components/Header';
import Footer from '../../Components/Footer';

import imgEmpresa from '../../assets/Teste.webp';
import imgDelete from '../../assets/delete.webp';
import imgGlobal from '../../assets/global.png';
import InfoVaga from '../../Components/InfoVaga/Index';
import imgDesenvolvimento from '../../assets/web-programming.webp';
import imgLocalizacao from '../../assets/big-map-placeholder-outlined-symbol-of-interface.webp';
import imgSalario from '../../assets/money (1).webp';
import imgTipoContrato from '../../assets/gears.webp';
import imgFuncao from '../../assets/rocket-launch.webp';
import IconEmpresa from '../../assets/building.webp';
import AccessMenu from '../../Components/AccessMenu';

import './style.css';
import { useHistory } from 'react-router-dom';

export default function ListarCandidatosInscritos() {

    const [Candidatos, setCandidatos] = useState([]);
    const [idInscricao, setInscricao] = useState(0);
    let [idVaga, setIdVaga] = useState(0);
    const [Experiencia, setExperiencia] = useState('');
    const [TipoContrato, setTipoContrato] = useState('');
    const [Salario, setSalario] = useState('');
    const[TipoPresenca,setPresenca]=useState('');
    const[Area,setArea]=useState('');
    const[razaoSocial,setRazaoSocial]=useState('');
    const [Cidade, setCidade] = useState('');
    const [TituloVaga, setTituloVaga] = useState('');
    const [DescricaoBeneficio, setDescricaoBeneficio] = useState('');
    const [DescricaoEmpresa, setDescricaoEmpresa] = useState('');
    const [DescricaoVaga, setDescricaoVaga] = useState('');
    const[caminhoImagem,setCaminho]=useState('');

let History=useHistory();

    useEffect(() => {
        idVaga = localStorage.getItem('idVagaSelecionadaAdm');
        listarCandidatos();
        BuscarPorId();
    }, []);

    const DeletarInscricao = (id) => {
        fetch('http://localhost:5000/api/Administrador/DeletarInscricao/' + id, {
            method: 'DELETE',
            headers: {
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(response => response.json())
        .then(dados => {
         alert(dados);
         listarCandidatos();
        })
        .catch(err => console.error(err));
    }

    const BuscarPorId = () => {
        fetch('http://localhost:5000/api/Usuario/BuscarPorId/' + idVaga, {
            method: 'GET',
            headers: {
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(response => response.json()).then(dados => {
            setIdVaga(dados.idVaga);
            setTituloVaga(dados.tituloVaga);
            setRazaoSocial(dados.razaoSocial);
            setPresenca(dados.tipoPresenca);
            setArea(dados.nomeArea);
            setTipoContrato(dados.tipoContrato);
            setSalario(dados.salario.toLocaleString('pt-br',{style: 'currency', currency: 'BRL'}));
            setCidade(dados.localidade);
            setExperiencia(dados.experiencia);
            setDescricaoBeneficio(dados.descricaoBeneficio);
            setDescricaoEmpresa(dados.descricaoEmpresa);
            setDescricaoVaga(dados.descricaoVaga);
            setCaminho(dados.caminhoImagem);
        }).catch(err => console.error(err));
    }

    const listarCandidatos = () => {
        fetch('http://localhost:5000/api/Administrador/ListarCandidatosInscritosAdm/' + idVaga, {
            method: 'GET',
            headers: {
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => response.json())
            .then(dados => {
                setCandidatos(dados);
            })
            .catch(err => console.error(err));
    }

    const DeletarVaga = () => {
        fetch('http://localhost:5000/api/Administrador/DeletarVaga/' + idVaga, {
            method: 'DELETE',
            headers: {
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(response => response.json())
        .then(dados => {
         alert(dados);
         History.push('/perfil');
        }).catch(err => console.error(err));
    }


    return (
        <div className="bodyPartInscricoesAdm">
            <AccessBar />
            <Header />
            <AccessMenu />
            <div className="Meio-Inscricoes">
                <div className="Esquerdo-Inscricoes">
                    {
                        Candidatos.map((item) => {
                            return (
                                <div key={item.idInscricao} className="BoxInscricao">
                                    <div className="Edit-Delete">
                                    <img className="Delete" src={imgDelete} onClick={()=>DeletarInscricao(item.idInscricao)} />
                                </div>
                                    <div className="DadosInscrito">
                                        <img className="imgUsuario" src={'http://localhost:5000/imgPerfil/'+item.caminhoImagem} />
                                        <div className="Column-Inscricao">
                                            <h3>{item.nomeCandidato}</h3>
                                            <p className="NomeCurso">{item.nomeCurso}</p>
                                        </div>
                                    </div>
                                </div>
                            )
                        })
                    } 
                </div> 
                <div className="Direito-Inscricoes">
                    <div className="VagaDescricao">
                        <div className="vaga">
                        <h5 className="ExcluirVagaText" onClick={DeletarVaga}>Excluir vaga</h5>
                            <div className="VagaCompleta">
                                <img src={'http://localhost:5000/imgPerfil/'+caminhoImagem} className="ImagemEmpresa" ></img>
                                <div className="MainVaga">
                                <h3>{TituloVaga}</h3>
                                    <div className="InfoVagas">
                                        <InfoVaga NomeProp={razaoSocial} source={IconEmpresa} />
                                        <InfoVaga NomeProp={Cidade} source={imgLocalizacao} />
                                        <InfoVaga NomeProp={Experiencia} source={imgFuncao} />
                                        <InfoVaga NomeProp={TipoContrato} source={imgTipoContrato} />
                                        <InfoVaga NomeProp={Salario} source={imgSalario} />
                                        <InfoVaga NomeProp={Area} source={imgDesenvolvimento} />
                                        <InfoVaga NomeProp={TipoPresenca} source={imgGlobal}/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="Descricoes">
                            <div className="DescricaoEmpresa">
                                <h3>Descricao da empresa</h3>
                                <p>{DescricaoEmpresa}</p>
                            </div>

                            <div className="DescricaoVaga">
                                <h3>Descricao da vaga</h3>
                                <p>{DescricaoVaga}</p>
                            </div>

                            <div className="DescricaoBeneficios">
                                <h3>Descricao beneficios</h3>
                                <p>{DescricaoBeneficio}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <Footer />
        </div>
    );
}