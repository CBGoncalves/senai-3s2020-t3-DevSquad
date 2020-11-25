import React, { useState, useEffect } from 'react';

import Header from '../../Components/Header';
import Footer from '../../Components/Footer';
import AccessBar from '../../Components/AccessBar';
import Input from '../../Components/Input/index';
import AccessMenu from '../../Components/AccessMenu';

import imgPadrao from '../../assets/android-character-symbol.png';

import './style.css';

export default function PerfilEmpresa() {
    const [NomeResponsavel, SetNomeResponsavel] = useState('');
    const [CNPJ, SetCNPJ] = useState('');
    const [NomeFantasia, SetNomeFantasia] = useState('');
    const [RazaoSocial, SetRazaoSocial] = useState('');
    const [Telefone, SetTelefone] = useState('');
    const [NumFuncionario, SetNumFuncionario] = useState('');
    const [NumCNAE, SetNumCNAE] = useState('');
    const [CEP, SetCEP] = useState('');
    const [Logradouro, SetLogradouro] = useState('');
    const [Complemento, SetComplemento] = useState('');
    const [EmailContato, SetEmailContato] = useState('');
    const [Estado, SetEstado] = useState('');
    const [Cidade, SetCidade] = useState('');
    const [NovaSenha, SetNovaSenha] = useState('');
    const [Candidatos, SetCandidato] = useState([]);

    useEffect(() => {
        listarCandidatos();
        BuscarEmpresaPorId();
    }, []);

    const BuscarEmpresaPorId = () => {
        fetch('http://localhost:5000/api/Empresa/BuscarEmpresaPorId', {
            method: 'GET',
            headers: {
                'content-type': 'application/json',
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(response => response.json()).then(dados => {
            SetNomeResponsavel(dados.nomeReponsavel)
            SetCNPJ(dados.cnpj);
            SetNomeFantasia(dados.nomeFantasia);
            SetRazaoSocial(dados.razaoSocial);
            SetTelefone(dados.telefone);
            SetNumFuncionario(dados.numFuncionario);
            SetNumCNAE(dados.numCnae)
            SetCEP(dados.cep)
            SetLogradouro(dados.logradouro)
            SetComplemento(dados.complemento)
            SetEmailContato(dados.emailContato)
            SetEstado(dados.uf)
            SetCidade(dados.localidade)
        }).catch(err => console.error(err));
    }

    const EditarDadosDaEmpresa = () => {
        const form = {
            nomeResponsavel: NomeResponsavel,
            cnpj: CNPJ,
            nomeFantasia: NomeFantasia,
            razaoSocial: RazaoSocial,
            telefone: Telefone,
            numFuncionario: NumFuncionario,
            numCnae: NumCNAE,
            cep: CEP,
            logradouro: Logradouro,
            complemento: Complemento,
            emailContato: EmailContato,
            estado: Estado,
            localidade: Cidade
        };
        fetch('http://localhost:5000/api/Empresa/AtualizarEmpresa', {
            method: 'PUT',
            body: JSON.stringify(form),
            headers: {
                'content-type': 'application/json',
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(function (respose) {
            if (respose.status !== 200) {
                alert("Não foi possivel editar os dados da empresa");
            } else {
                alert("Editada com sucesso");
            }
        }).catch(err => console.error(err));
    }

    const AlterarSenha = () => {
        const form = {
            senha: NovaSenha
        };
        fetch('http://localhost:5000/api/Usuario/AlterarSenha', {
            method: 'PUT',
            body: JSON.stringify(form),
            headers: {
                'content-type': 'application/json',
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        }).then(function (respose) {
            if (respose.status !== 200) {
                alert("Não foi possivel alterar a senha");
            } else {
                alert("Senha alterada com sucesso com sucesso");
            }
        }).catch(err => console.error(err));
    }

    const listarCandidatos = () => {
        fetch('http://localhost:5000/api/Empresa/ListarCandidatosEstagiando', {
            method: 'GET',
            headers: {
                authorization: 'Bearer ' + localStorage.getItem('token')
            }
        })
            .then(response => response.json())
            .then(dados => {
                SetCandidato(dados);
            })
            .catch(err => console.error(err));
    }

    function ApareceEditarDadosEmpresa() {
        let idEditarPelicula = document.getElementById("peliculaPerfilEmpresa");
        let idModalVaga = document.getElementById("modalPerfilEmpresa");
        if (idEditarPelicula.classList == "peliculaPerfilEmpresa none") {
            idEditarPelicula.classList.remove("none");
            idModalVaga.classList.remove("none");
        }
    }

    function btn_fecharEditarDadosEmpresa() {
        let idModalVaga = document.getElementById("modalPerfilEmpresa");
        let idEditarPelicula = document.getElementById("peliculaPerfilEmpresa");
        if (idEditarPelicula.classList != "peliculaPerfilEmpresa none") {
            idEditarPelicula.classList.add("none");
            idModalVaga.classList.add("none");
        }
    }

    function ApareceAlterarSenhaEmpresa() {
        let idEditarPelicula = document.getElementById("peliculaAlterarSenhaEmpresa");
        let idModalVaga = document.getElementById("modalAlterarSenhaEmpresa");
        if (idEditarPelicula.classList == "peliculaAlterarSenhaEmpresa none")
            idEditarPelicula.classList.remove("none");
        idModalVaga.classList.remove("none");
    }

    function btn_fecharAlterarSenhaEmpresa() {
        let idModalVaga = document.getElementById("modalAlterarSenhaEmpresa");
        let idEditarPelicula = document.getElementById("peliculaAlterarSenhaEmpresa");
        if (idEditarPelicula.classList != "AlterarSenhaEmpresa none") {
            idEditarPelicula.classList.add("none");
            idModalVaga.classList.add("none");
        }
    }


    return (
        <div className="bodyPartVizualizarPerfil">
            <AccessBar />
            <Header />
            <AccessMenu />
            <div className="meioPerfil">
                <div className="EsquerdoPerfil">
                    <div className="imgPefilTexto">
                        <img className="imgperfil" src={imgPadrao} alt="perfil" />
                        <h3>Apple</h3>
                        <p>Empresa</p>
                    </div>
                    <div className="BotoesPerfilEmpresa">
                        <button className="btPerfil" onClick={ApareceEditarDadosEmpresa}><h3>Alterar dados</h3></button>
                        <button className="btPerfil" onClick={ApareceAlterarSenhaEmpresa} ><h3>Alterar senha</h3></button>
                    </div>
                </div>
                <div className="DireitoPerfil">
                    {
                        Candidatos.map((item) => {
                            return (
                                <div className="BoxPerfilCandidato">
                                    <div className="flexBoxPerfilCandidato">
                                        <img src={imgPadrao} />
                                        <h3>{item.nomeCompleto}</h3>
                                    </div>
                                    <h3>{item.cpf}</h3>
                                    <h3>{item.telefone}</h3>
                                </div>
                            )
                        })
                    }
                </div>
            </div>
            <div id="peliculaPerfilEmpresa" className="peliculaPerfilEmpresa none" onClick={btn_fecharEditarDadosEmpresa}></div>
            <div id="modalPerfilEmpresa" className="modalPerfilEmpresa none">
                <h2>Editar seus dados pessoais</h2>
                <form>
                    <Input className="InputCadastro" value={NomeResponsavel} name="NomeResponsavel" label="Nome do responsável" onChange={e => SetNomeResponsavel(e.target.value)} />
                    <Input className="InputCadastro" value={RazaoSocial} name="RazaoSocial" label="Razão social" onChange={e => SetRazaoSocial(e.target.value)} />
                    <Input className="InputCadastro" value={NomeFantasia} name="NomeFantasia" label="Nome fantasia" onChange={e => SetNomeFantasia(e.target.value)} />
                    <Input className="InputCadastro" value={CNPJ} name="CNPJ" label="CNPJ" onChange={e => SetCNPJ(e.target.value)} />
                    <Input className="InputCadastro" value={EmailContato} name="EmailConato" label="Email para contato" onChange={e => SetEmailContato(e.target.value)} />
                    <Input className="InputCadastro" value={Telefone} name="Telefone" label="Telefone" onChange={e => SetTelefone(e.target.value)} />
                    <Input className="InputCadastro" value={NumFuncionario} name="NumFuncionarios" label="Número de fúncionarios" onChange={e => SetNumFuncionario(e.target.value)} />
                    <Input className="InputCadastro" value={NumCNAE} name="NumCNAE" label="Número do CNAE" onChange={e => SetNumCNAE(e.target.value)} />
                    <Input className="InputCadastro" value={CEP} name="CEP" label="CEP" onChange={e => SetCEP(e.target.value)} />
                    <Input className="InputCadastro" value={Logradouro} name="Logradouro" label="Logradouro" onChange={e => SetLogradouro(e.target.value)} />
                    <Input className="InputCadastro" value={Complemento} name="Complemento" label="Complemento" onChange={e => SetComplemento(e.target.value)} />
                    <Input className="InputCadastro" value={Estado} name="Estado" label="Estado" onChange={e => SetEstado(e.target.value)} />
                    <Input className="InputCadastro" value={Cidade} name="Cidade" label="Cidade" onChange={e => SetCidade(e.target.value)} />
                    <div className="btEditarEstagioDiv">
                        <button className="btVaga" onClick={EditarDadosDaEmpresa}><h3>Editar</h3></button>
                    </div>
                </form>
            </div>

            <div id="peliculaAlterarSenhaEmpresa" className="peliculaAlterarSenhaEmpresa none" onClick={btn_fecharAlterarSenhaEmpresa}></div>
            <div id="modalAlterarSenhaEmpresa" className="modalAlterarSenhaEmpresa none">
                <h2>Alterar senha</h2>
                <form>
                    <Input className="InputCadastro" name="NovaSenha" label="Nova senha" onChange={e => SetNovaSenha(e.target.value)} />
                    <button className="btVaga" onClick={AlterarSenha}>Alterar senha</button>
                </form>
            </div>
            <Footer />
        </div>
    );
}