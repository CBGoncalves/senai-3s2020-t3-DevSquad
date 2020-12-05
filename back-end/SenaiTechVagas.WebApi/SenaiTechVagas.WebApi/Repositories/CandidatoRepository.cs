﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SenaiTechVagas.WebApi.Contexts;
using SenaiTechVagas.WebApi.Domains;
using SenaiTechVagas.WebApi.Interfaces;
using SenaiTechVagas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SenaiTechVagas.WebApi.Repositories
{
    public class CandidatoRepository : ICandidatoRepository
    {
        string stringConexao = "Data Source=DESKTOP-0VF65US\\SQLEXPRESS; Initial Catalog=Db_TechVagas;integrated Security=True";

        //Em ordem CRUD - Criar, Ler, Atualizar, Deletar
        public bool AtualizarCandidato(int idUsuario, AtualizarCandidatoViewModel CandidatoAtualizado)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Candidato CandidatoBuscado = ctx.Candidato.FirstOrDefault(c => c.IdUsuario == idUsuario);

                    if (CandidatoBuscado == null)
                    {
                        return false;
                    }

                    if (CandidatoAtualizado.NomeCompleto != null)
                    {
                        CandidatoBuscado.NomeCompleto = CandidatoAtualizado.NomeCompleto;
                    }
                    if (CandidatoAtualizado.Rg != null)
                    {
                        CandidatoBuscado.Rg = CandidatoAtualizado.Rg.Trim();
                    }
                    if (CandidatoAtualizado.Cpf != null)
                    {
                        CandidatoBuscado.Cpf = CandidatoAtualizado.Cpf.Trim();
                    }
                    if (CandidatoAtualizado.Telefone != null)
                    {
                        CandidatoBuscado.Telefone = CandidatoAtualizado.Telefone.Trim();
                    }
                    if (CandidatoAtualizado.LinkLinkedinCandidato != null)
                    {
                        CandidatoBuscado.LinkLinkedinCandidato = CandidatoAtualizado.LinkLinkedinCandidato.Trim();
                    }

                    if (CandidatoAtualizado.IdCurso >= 1)
                    {
                        CandidatoBuscado.IdCurso = CandidatoAtualizado.IdCurso;
                    }

                    ctx.Update(CandidatoBuscado);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<ListarVagasViewModel> ListarInscricoes(int idUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Candidato candidato = ctx.Candidato.FirstOrDefault(c => c.IdUsuario == idUsuario);
                    if (candidato == null)
                        return null;

                    List<Inscricao> ListaDeInscricoes = ctx.Inscricao.Where(v => v.IdCandidato == candidato.IdCandidato).ToList();
                    if (ListaDeInscricoes == null)
                        return null;
                    List<ListarVagasViewModel> listvagas = new List<ListarVagasViewModel>();
                    for (int i = 0; i < ListaDeInscricoes.Count; i++)
                    {
                        // Declara a SqlConnection passando a string de conexão
                        using (SqlConnection con = new SqlConnection(stringConexao))
                        {
                            // Declara a instrução a ser executada
                            string querySelectAll =
                            "SELECT U.CaminhoImagem,inscri.DataInscricao,trp.NomeTipoRegimePresencial,inscri.IdInscricao,are.NomeArea,v.TituloVaga,e.RazaoSocial,v.IdVaga,t.NomeTecnologia,v.Experiencia,v.TipoContrato,v.Salario,v.Localidade FROM VagaTecnologia" +
                            " INNER JOIN Vaga v on v.IdVaga = VagaTecnologia.IdVaga" +
                            " INNER JOIN Tecnologia t on t.IdTecnologia = VagaTecnologia.IdTecnologia" +
                            " INNER JOIN Empresa e on e.IdEmpresa = v.IdEmpresa" +
                            " INNER JOIN Area are on are.IdArea=v.IdArea" +
                            " INNER JOIN Usuario U ON U.IdUsuario=e.IdUsuario" +
                            " INNER JOIN Inscricao inscri on inscri.IdVaga=v.IdVaga" +
                            " INNER JOIN TipoRegimePresencial trp on trp.IdTipoRegimePresencial=v.IdTipoRegimePresencial" +
                            " WHERE v.IdVaga =@IDVaga AND inscri.IdInscricao=@IDInscricao ";
                            con.Open();

                            // Declara o SqlDataReader para receber os dados do banco de dados
                            SqlDataReader rdr;

                            // Declara o SqlCommand passando o comando a ser executado e a conexão
                            using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                            {
                                cmd.Parameters.AddWithValue("@IDVaga", ListaDeInscricoes[i].IdVaga);
                                cmd.Parameters.AddWithValue("@IDInscricao", ListaDeInscricoes[i].IdInscricao);
                                // Executa a query e armazena os dados no rdr
                                rdr = cmd.ExecuteReader();

                                // Enquanto houver registros para serem lidos no rdr, o laço se repete
                                while (rdr.Read())
                                {
                                    bool teveAcao = false;

                                    // Instancia um objeto jogo 
                                    ListarVagasViewModel vm = new ListarVagasViewModel
                                    {
                                        // Atribui às propriedades os valores das colunas da tabela do banco
                                        IdVaga = Convert.ToInt32(rdr["IdVaga"]),
                                        Experiencia = rdr["Experiencia"].ToString(),
                                        CaminhoImagem = rdr["CaminhoImagem"].ToString(),
                                        TipoContrato = rdr["TipoContrato"].ToString(),
                                        IdInscricao = Convert.ToInt32(rdr["IdInscricao"]),
                                        Localidade = rdr["Localidade"].ToString(),
                                        DataInscricao = Convert.ToDateTime(rdr["DataInscricao"]).ToString("dd/MM/yyyy"),
                                        Salario = Convert.ToDecimal(rdr["Salario"]),
                                        RazaoSocial = rdr["RazaoSocial"].ToString(),
                                        NomeArea = rdr["NomeArea"].ToString(),
                                        TituloVaga = rdr["TituloVaga"].ToString(),
                                        TipoPresenca = rdr["NomeTipoRegimePresencial"].ToString()
                                    };
                                    var NomeTecnologia = rdr["NomeTecnologia"].ToString();
                                    vm.Tecnologias = new List<string>();

                                    for (int e = 0; e < listvagas.Count; e++)
                                    {
                                        if (vm.IdVaga == listvagas[e].IdVaga)
                                        {
                                            listvagas[e].Tecnologias.Add(NomeTecnologia);
                                            teveAcao = true;
                                        }
                                    }
                                    if (teveAcao == true)
                                        continue;
                                    else vm.Tecnologias.Add(NomeTecnologia);
                                    // Adiciona a vaga criada à lista de vagas
                                    listvagas.Add(vm);
                                }
                            }
                        }
                    }
                    return listvagas;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public bool RevogarInscricao(int idInscricao, int idCandidato)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Inscricao inscricaoBuscada = ctx.Inscricao.FirstOrDefault(i => i.IdInscricao == idInscricao && i.IdCandidato == idCandidato);
                    if (inscricaoBuscada == null)
                        return false;

                    ctx.Remove(inscricaoBuscada);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool SeInscrever(Inscricao NovaInscricao)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    NovaInscricao.DataInscricao = DateTime.Now;
                    NovaInscricao.IdStatusInscricao = 2;
                    ctx.Add(NovaInscricao);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool VerificarSeInscricaoExiste(int idVaga, int idCandidato)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Inscricao InscricaoBuscada = ctx.Inscricao.FirstOrDefault(e => e.IdCandidato == idCandidato && e.IdVaga == idVaga);
                    if (InscricaoBuscada != null)
                        return true;

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<ListarVagasViewModel> ListarVagasArea(int idArea)
        {
            try
            {
                List<ListarVagasViewModel> listvagas = new List<ListarVagasViewModel>();
                // Declara a SqlConnection passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a instrução a ser executada
                    string querySelectAll =
                   "SELECT U.CaminhoImagem,trp.NomeTipoRegimePresencial,are.NomeArea,v.TituloVaga,e.RazaoSocial,v.IdVaga,t.NomeTecnologia,v.Experiencia,v.TipoContrato,v.Salario,v.Localidade FROM VagaTecnologia" +
                   " INNER JOIN Vaga v on v.IdVaga = VagaTecnologia.IdVaga" +
                   " INNER JOIN Tecnologia t on t.IdTecnologia = VagaTecnologia.IdTecnologia" +
                   " INNER JOIN Empresa e on e.IdEmpresa = v.IdEmpresa" +
                   " INNER JOIN Usuario U ON U.IdUsuario=e.IdUsuario"+
                   " INNER JOIN Area are on are.IdArea=v.IdArea" +
                   " INNER JOIN TipoRegimePresencial trp on trp.IdTipoRegimePresencial=v.IdTipoRegimePresencial" +
                   " WHERE are.IdArea=@IdArea";
                    con.Open();

                    // Declara o SqlDataReader para receber os dados do banco de dados
                    SqlDataReader rdr;

                    // Declara o SqlCommand passando o comando a ser executado e a conexão
                    using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                    {
                        cmd.Parameters.AddWithValue("@IdArea", idArea);

                        // Executa a query e armazena os dados no rdr
                        rdr = cmd.ExecuteReader();

                        // Enquanto houver registros para serem lidos no rdr, o laço se repete
                        while (rdr.Read())
                        {
                            bool teveAcao = false;

                            // Instancia um objeto jogo 
                            ListarVagasViewModel vm = new ListarVagasViewModel
                            {
                                // Atribui às propriedades os valores das colunas da tabela do banco
                                IdVaga = Convert.ToInt32(rdr["IdVaga"]),
                                CaminhoImagem = rdr["CaminhoImagem"].ToString(),
                                Experiencia = rdr["Experiencia"].ToString(),
                                TipoContrato = rdr["TipoContrato"].ToString(),
                                TituloVaga = rdr["TituloVaga"].ToString(),
                                Localidade = rdr["Localidade"].ToString(),
                                Salario = Convert.ToDecimal(rdr["Salario"]),
                                RazaoSocial = rdr["RazaoSocial"].ToString(),
                                NomeArea = rdr["NomeArea"].ToString(),
                                TipoPresenca = rdr["NomeTipoRegimePresencial"].ToString()
                            };
                            var NomeTecnologia = rdr["NomeTecnologia"].ToString();
                            vm.Tecnologias = new List<string>();

                            for (int i = 0; i < listvagas.Count; i++)
                            {
                                if (vm.IdVaga == listvagas[i].IdVaga)
                                {
                                    listvagas[i].Tecnologias.Add(NomeTecnologia);
                                    teveAcao = true;
                                }
                            }
                            if (teveAcao == true)
                                continue;//é do While
                            else vm.Tecnologias.Add(NomeTecnologia);
                            // Adiciona a vaga criada à lista de vagas
                            listvagas.Add(vm);
                        }
                    }
                }
                return listvagas;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public CandidatoCompletoViewModel BuscarCandidatoPorIdUsuario(int idUsuario)
        {
            try{
                // Declara a conexão passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a query que será executada
                    string querySelectById =
                        "SELECT A.IdArea,C.IdCandidato,cur.IdCurso,NomeCompleto,Rg,Cpf,Telefone,C.LinkLinkedinCandidato,U.CaminhoImagem FROM Candidato C" +
                        " INNER JOIN Curso cur ON cur.IdCurso=C.IdCurso" +
                        " INNER JOIN Area A ON A.IdArea=cur.IdArea"+
                        " INNER JOIN Usuario U ON U.IdUsuario=C.IdUsuario" +
                        " WHERE U.IdUsuario = @ID";

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Declara o SqlDataReader para receber os dados do banco de dados
                    SqlDataReader rdr;

                    // Declara o SqlCommand passando o comando a ser executado e a conexão
                    using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                    {
                        // Passa o valor do parâmetro
                        cmd.Parameters.AddWithValue("@ID", idUsuario);

                        // Executa a query e armazena os dados no rdr
                        rdr = cmd.ExecuteReader();

                        // Caso o resultado da query possua registro
                        if (rdr.Read())
                        {
                            // Instancia um objeto usuario 
                            CandidatoCompletoViewModel usuario = new CandidatoCompletoViewModel
                            {
                                // Atribui às propriedades os valores das colunas da tabela do banco
                                IdCandidato = Convert.ToInt32(rdr["IdCandidato"]),
                                idCurso = Convert.ToInt32(rdr["IdCurso"]),
                                IdArea = Convert.ToInt32(rdr["IdArea"]),
                                NomeCompleto = rdr["NomeCompleto"].ToString(),
                                Rg = rdr["Rg"].ToString(),
                                Cpf = rdr["Cpf"].ToString(),
                                Telefone = rdr["Telefone"].ToString(),
                                LinkLinkedinCandidato = rdr["LinkLinkedinCandidato"].ToString(),
                                CaminhoImagem = rdr["CaminhoImagem"].ToString(),

                            };

                            // Retorna o usuario buscado
                            return usuario;
                        }

                        // Caso o resultado da query não possua registros, retorna null
                        return null;
                    }
                }
            }catch(Exception)
            {
                return null;
            }
        }
    }
}
