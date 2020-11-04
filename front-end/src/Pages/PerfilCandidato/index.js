import React from 'react';

import Header from '../../Components/Header';
import Footer from '../../Components/Footer';
import AccessBar from '../../Components/AccessBar';
import Select from '../../Components/Select/Index';
import Input from '../../Components/Input/index';

import imgPadrao from '../../assets/android-character-symbol.png';

import './style.css';

export default function perfilCandidato() {
    return (
        <div className="bodyPartVizualizarPerfil">

            <AccessBar />
            <Header />
            <div className="meioPerfil">
                <div className="EsquerdoPerfil">
                    <img className="imgperfil" src={imgPadrao} alt="perfil" />
                    <h3>Matador de herobraine 99</h3>
                    <p>Candidato</p>
                    <div className="BotoesPerfil">
                        <button className="btPerfil"><h3>Alterar dados</h3></button>
                        <button className="btPerfil"><h3>Alterar senha</h3></button>
                    </div>
                </div>
                <div className="DireitoPerfil">
                    <div className="BoxPerfilCandidato">
                        <div className="flexBoxPerfilCandidato">
                            <img src={imgPadrao} />
                            <h3>Android</h3>
                        </div>
                        <h3>Area de desenvolvimento</h3>
                        <h3>Salario:3.000</h3>
                    </div>
                </div>
            </div>
            <div className="peliculaPerfilCandidato"></div>
            <div className="modalPerfilCandidato">
                <h2>Editar seus dados pessoais</h2>
                <form>
                    <Input className="InputCadastro" name="Telefone" label="Telefone" />
                    <Input className="InputCadastro" name="Linkedin" label="Linkedin" />
                    <Select label="Area" Name="Area"></Select>
                    <Select label="Curso" Name="Curso"></Select>
                    <div className="btEditarEstagioDiv">
                        <button className="btVaga"><h3>Editar</h3></button>
                    </div>
                </form>
            </div>
            <Footer />
        </div>
    );
}