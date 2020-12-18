import React, { useState } from "react";
import { useHistory } from "react-router-dom";

import AccessBar from "../../Components/AccessBar";
import Header from "../../Components/Header";
import AccessMenu from "../../Components/AccessMenu";
import Input from "../../Components/Input";
import BlueButton from "../../Components/BlueButton";
import Footer from "../../Components/Footer";

import imagemCadastroEmpresa from "../../assets/imgCadastroEmpresa.webp";
import Userimg from "../../assets/Teste.webp";

import "./style.css";

export default function CadastroEmpresa() {
  const [Email, SetEmail] = useState("");
  const [Senha, SetSenha] = useState("");
  const [ConfirmarSenha, SetConfirmarSenha] = useState("");
  let [CEP, SetCEP] = useState("");
  let [Logradouro, SetLogradouro] = useState("");
  let [Estado, SetEstado] = useState("");
  let [Cidade, SetCidade] = useState("");

  const history = useHistory();

  const emailRegex = /^\S+@\S+\.\S+$/g;
  const validaCep = /^[0-9]{8}$/g;
  const senhaRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%&\*_-])(?=.{9,15})/g;

  const verificacaoCep = validaCep.test(CEP);
  const verificacaoEmail = emailRegex.test(Email);
  const verificacaoSenha = senhaRegex.test(Senha);

  let redBox = document.querySelector("#confirmPassword-cadastro");
  let result = document.querySelector(".password-matching-text");
  let instructions = document.querySelector(".password-instructions-text");

  function buscarCep(valor) {
    if (verificacaoCep) {
      const URL = `https://viacep.com.br/ws/${valor}/json/`;
      fetch(URL)
        .then((resposta) => resposta.json())
        .then((data) => {
          if (data.logradouro || data.localidade || data.uf !== undefined) {
            document.getElementById("rua").value = data.logradouro;
            document.getElementById("cidade").value = data.localidade;
            document.getElementById("uf").value = data.uf;
            SetLogradouro(data.logradouro);
            SetCidade(data.localidade);
            SetEstado(data.uf);
          } else {
            alert("O CEP não existe");
          }
        })
        .catch((erro) => console.error(erro));
    } else {
      alert("O CEP deve conter apenas 8 números");
    }
  }

  const escreverResultado = () => {
    if (Senha !== ConfirmarSenha) {
      redBox.style.border = "solid red 1px";
      redBox.style.boxShadow = "3px 3px 3px gray";
      result.style.color = "red";
      result.innerText = "As senhas não conferem";
    } else {
      redBox.style.border = "unset";
      redBox.style.boxShadow = "unset";
      result.style.color = "unset";
      result.innerText = "As senhas conferem";
    }

    if (verificacaoSenha !== true) {
      redBox.style.border = "solid red 1px";
      redBox.style.boxShadow = "3px 3px 3px gray";
      instructions.style.color = "red";
      instructions.innerText = 
      `A senha deve conter, no mínimo, 9 caracteres, e no máximo 15, dentre eles:
      • 1 letra minúscula
      • 1 letra maiúscula
      • 1 número
      • 1 caractere especial`;
    } else {
      redBox.style.border = "unset";
      redBox.style.boxShadow = "unset";
      instructions.style.color = "unset";
      instructions.innerText = "";
    }
  };

  return (
    <body>
      <AccessBar />
      <Header />
      <AccessMenu />

      <div className="registerCompany">
        <div className="box-form">
          <div className="form-content">
            <h1>Cadastre-se como Empresa</h1>
            <p>
              Bem-vindo ao cadastro de empresa. Ficamos felizes de tê-la na
              nossa plataforma
            </p>
            <div className="imgCadastroPerfil">
            <img className="imagemCadastro" src={Userimg} alt="Imagem de perfil" />
              <br />
              <button className="btSelecionar">
                <label htmlFor="ButtonImage" className="lbBt">
                  Selecione uma imagem
                </label>
              </button>
            </div>
            <form className="form">
              <input
                type="file"
                className="none"
                id="ButtonImage"
                onChange={(event) => {
                }}
              />
              <Input
                id="responsibleName"
                name={"responsibleName"}
                className="cadastre"
                label="Nome do responsável:"
                type="text"
                placeholder="Barão de Mauá"
                maxLength={65}
                minLength={5}
                required
              />
              <Input
                id="cnpjCadastro"
                name="cnpjCadastro"
                className="cadastre"
                label="CNPJ:"
                type="text"
                placeholder="00.000.000/0001-00"
                maxLength={14}
                minLength={14}
                required
              />

              <Input
              id="emailContatoCadastro"
                name="emailContatoCadastro"
                className="cadastre"
                label="E-mail para contato:"
                type="text"
                placeholder="contato@company.com"
                maxLength={254}
                minLength={5}
                required
              />

              <Input
              id="companyFakeNameCadastro"
                name="companyFakeNameCadastro"
                className="cadastre"
                label="Nome fantasia:"
                type="text"
                placeholder="CPTM"
                maxLength={50}
                required
              />

              <Input
              id="companyNameCadastro"
                name="companyNameCadastro"
                className="cadastre"
                label="Razão social:"
                type="text"
                placeholder="São Paulo Railway Company Ltd."
                maxLength={50}
                minLength={5}
                required
              />
              <Input
              id="phoneNumberCadastro"
                name="phoneNumberCadastro"
                className="cadastre"
                label="Telefone da empresa:"
                type="tel"
                placeholder="(11)4002-8922"
                maxLength={11}
                minLength={10}
                required
              />

              <Input
              id="workersCompanyNumberCadastro"
                name="workersCompanyNumberCadastro"
                className="cadastre"
                label="Número de funcionários:"
                type="number"
                maxLength={4}
                minLength={1}
                required
              />

              <Input
              id="cnaeNumberCadastro"
                name="cnaeNumberCadastro"
                className="cadastre"
                label="Número CNAE:"
                type="text"
                placeholder="00.00-0-0"
                maxLength={7}
                minLength={7}
                required
              />

              <div className="Input">
                <label htmlFor="cep">CEP:</label>
                <br />
                <input
                  type="text"
                  className="cadastre"
                  id="cep"
                  maxLength={8}
                  minLength={8}
                  onBlur={(e) => {
                    e.preventDefault();
                    buscarCep(e.target.value);
                  }}
                  onChange={(e) => SetCEP(e.target.value)}
                />
              </div>

              <Input
                id="rua"
                name="address"
                className="cadastre"
                label="Logradouro da empresa:"
                type="text"
                maxLength={155}
                minLength={5}
              />

              <div className="Input">
                <label htmlFor="ComplementoCadastroEmpresa">Complemento:</label>
                <br />
                <input
                id="ComplementoCadastroEmpresa"
                  type="text"
                  name="address2"
                  maxLength={255}
                  className="cadastre"
                />
              </div>

              <div className="Input">
                <label>Cidade:</label>
                <br />
                <input
                  type="text"
                  className="cadastre"
                  id="cidade"
                  required
                  disabled
                  maxLength={150}
                  minLength={5}
                />
              </div>

              <div className="Input">
                <label>UF:</label>
                <br />
                <input
                  type="text"
                  className="cadastre"
                  id="uf"
                  required
                  disabled
                  maxLength={2}
                  minLength={2}
                />
              </div>

              <Input
              id="EmailUserCadastroEmpresa"
                name="EmailUserCadastroEmpresa"
                className="cadastre"
                label="Email de acesso:"
                placeholder="email@company.com"
                type="text"
                maxLength={254}
                minLength={5}
                required
              />

              <Input
                id="password-cadastro"
                name="password-cadastro"
                className="cadastre"
                label="Senha de acesso:"
                type="password"
                maxLength={15}
                minLength={9}
                required
                autocomplete="new-password"
                onKeyUp={() => escreverResultado()}
                onChange={(e) => SetSenha(e.target.value)}
              />

              <Input
                id="confirmPassword-cadastro"
                name="confirmPassword-cadastro"
                className="cadastre"
                label="Confirme a senha:"
                type="password"
                maxLength={15}
                minLength={9}
                required
                onKeyUp={() => escreverResultado()}
                onChange={(e) => SetConfirmarSenha(e.target.value)}
              />

              <p className="password-matching-text"></p>
              <p className="password-instructions-text"></p>

              <div>
                <label htmlFor="PerguntaCadastroEmpresa" className="select-cadastroCandidato-title">
                  Pergunta de seguranca
                </label>
                <br />
                <select
                  id="PerguntaCadastroEmpresa"
                  className="select-cadastroCandidato"
                  required
                >
                  <option value="0">Selecione uma pergunta de segurança</option>
                  <option value="Como se chama o seu cachorro">
                    Como se chama o seu cachorro
                  </option>
                  <option value="Qual o seu sobrenome">
                    Qual o seu sobrenome
                  </option>
                  <option value="Qual o nome da sua mãe/pai">
                    Qual o nome da sua mãe/pai
                  </option>
                  <option value="Para qual país você gostaria de viajar">
                    Para qual país você gostaria de viajar
                  </option>
                  <option value="Qual era sua matéria preferida na escola">
                    Qual era sua matéria preferida na escola
                  </option>
                  <option value="De onde vem sua família">
                    De onde vem sua família
                  </option>
                  <option value="Do que você mais gosta de fazer nas suas horas vagas">
                    Do que você mais gosta de fazer nas suas horas vagas
                  </option>
                  <option value="Qual a palavra que te define como pessoa">
                    Qual a palavra que te define como pessoa
                  </option>
                  <option value="Qual o ano mais importante da sua vida">
                    Qual o ano mais importante da sua vida
                  </option>
                </select>
              </div>

              <Input
              id="RespostaCadastroEmpresa"
                name="RespostaCadastroEmpresa"
                className="cadastre"
                label="Resposta de segurança:"
                type="text"
                placeholder="Meu cachorro se chama..."
                required
                maxLength={30}
                minLength={5}
              />
              <p>Ao cadastrar-se, você aceita os nossos termos de uso.</p>

              <div className="form-button">
                <BlueButton type="submit" name="Criar conta" Onclick={()=>history.push("/login")}>
                  Criar conta
                </BlueButton>
              </div>
            </form>
          </div>
        </div>

        <div className="box-img">
          <img
            src={imagemCadastroEmpresa}
            alt="Pessoa acessando sua conta, com uma película transparente de cor azul."
          />
        </div>
      </div>
      <Footer />
    </body>
  );
}