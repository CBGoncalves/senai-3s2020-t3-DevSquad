import React from 'react';

import imgDelete from '../../assets/delete.png'
import imgEdit from '../../assets/black-ink-pen.png'

import imgEnterprise from '../../assets/enterprise.png'
import imgCertificate from '../../assets/certificate.png';
import imgWorker from '../../assets/worker.png'

import './style.css'

import imgPadrao from '../../assets/android-character-symbol.png';
import Tag from '../../Components/Tag/Index';
import AccessBar from '../../Components/AccessBar';
import Header from '../../Components/Header';
import Footer from '../../Components/Footer';

export default function Estagio() {
    return (
        <body>
            <AccessBar />
            <Header />
            <div className="bodyPartEstagio">
                <br/>
                <div className="Estatisticas">
                    <div className="Empresascadastradas">
                        <img src={imgEnterprise}/>
                        <div className="EstatiscaColumn">
                            <h5>30</h5>
                            <br/>
                            <h5>Empresas cadastradas</h5>
                        </div>
                    </div>
                    <div className="Candidatoscontratados">
                        <img src={imgCertificate}/>
                    <div className="EstatiscaColumn">
                            <h5>30</h5>
                            <br/>
                            <h5>Candidatos contratados</h5>
                        </div>
                    </div>
                    <div className="Candidatoscadastrados">
                        <img src={imgWorker}/>
                    <div className="EstatiscaColumn">
                            <h5>30</h5>
                            <br/>
                            <h5>Candidatos cadastrados</h5>
                        </div>
                    </div>
                </div>
<br/>
                <select className="div-select">
                <option>Filtre sua busca por meses</option>
                <option>3Meses</option>
                <option>6Meses</option>
                <option>9Meses</option>
                <option>12Meses</option>
                 </select>
                <div className="ListaEstagios">
                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>

                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>

                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>
                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>

                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>

                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>
                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>

                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>

                    <div className="Estagio">
                        <div className="Ferramentas">
                            <img className="Edit" src={imgEdit} />
                            <img className="Delete" src={imgDelete} />
                        </div>
                        <div className="CabecaEstagio">
                            <img src={imgPadrao} alt="ImagemPerfil" />
                            <h3>Usuario1</h3>
                            <hr className="hr" />
                            <h5> Cursando 2°termo de desenvolvimento</h5>
                        </div>
                        <div className="CorpoEstagio">
                            <Tag NomeTag={"Email@exemplo.com"}></Tag>
                            <Tag NomeTag={"Telefone"}></Tag>
                            <Tag NomeTag={"Status:Desempregado"}></Tag>
                            <Tag NomeTag={"Periodo de estagio:9meses"}></Tag>
                            <Tag NomeTag={"Tempo de estagio:10meses"}></Tag>
                            <Tag NomeTag={"Empresa contratante:Microsoft"}></Tag>
                            <a className="Link" href="teste">Ver perfil</a>
                        </div>
                    </div>
                </div>
            </div>
            <Footer />
        </body>
    );
}