﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SenaiTechVagas.WebApi.Contexts;
using SenaiTechVagas.WebApi.Domains;
using SenaiTechVagas.WebApi.Interfaces;
using SenaiTechVagas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SenaiTechVagas.WebApi.Repositories
{

    public class EmpresaRepository : IEmpresaRepository
    {
        string stringConexao = "Data Source=.\\SQLEXPRESS; Initial Catalog=Db_TechVagas;integrated Security=True";
 
        public bool AtualizarEmpresaPorIdCorpo(int idUsuario, AtualizarEmpresaViewModel EmpresaAtualizada)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Empresa empresaBuscada = ctx.Empresa.FirstOrDefault(e=>e.IdUsuario==idUsuario);
                    if (empresaBuscada == null)
                        return false;

                    if (empresaBuscada.NomeReponsavel != null)
                    {
                        empresaBuscada.NomeReponsavel = EmpresaAtualizada.NomeResponsavel;
                    }
                    if (EmpresaAtualizada.Cnpj != null)
                    {
                        empresaBuscada.Cnpj = EmpresaAtualizada.Cnpj.Trim();
                    }
                    if (EmpresaAtualizada.EmailContato != null)
                    {
                        empresaBuscada.EmailContato = EmpresaAtualizada.EmailContato.Trim();
                    }
                    if (EmpresaAtualizada.NomeFantasia != null)
                    {
                        empresaBuscada.NomeFantasia = EmpresaAtualizada.NomeFantasia;
                    }
                    if (EmpresaAtualizada.RazaoSocial != null)
                    {
                        empresaBuscada.RazaoSocial = EmpresaAtualizada.RazaoSocial;
                    }
                    if (EmpresaAtualizada.Telefone != null)
                    {
                        empresaBuscada.Telefone = EmpresaAtualizada.Telefone.Trim();
                    }
                    if (EmpresaAtualizada.NumFuncionario != empresaBuscada.NumFuncionario)
                    {
                        empresaBuscada.NumFuncionario = EmpresaAtualizada.NumFuncionario;
                    }
                    if (EmpresaAtualizada.NumCnae != null)
                    {
                        empresaBuscada.NumCnae = EmpresaAtualizada.NumCnae.Trim();
                    }
                    if (EmpresaAtualizada.Cep != null)
                    {
                        empresaBuscada.Cep = EmpresaAtualizada.Cep.Trim();
                    }
                    if (EmpresaAtualizada.Logradouro != null)
                    {
                        empresaBuscada.Logradouro = EmpresaAtualizada.Logradouro;
                    }
                    if (EmpresaAtualizada.Complemento != null)
                    {
                        empresaBuscada.Complemento = EmpresaAtualizada.Complemento;
                    }
                    if (EmpresaAtualizada.Localidade != null)
                    {
                        empresaBuscada.Localidade = EmpresaAtualizada.Localidade;
                    }
                    if (EmpresaAtualizada.Estado != null)
                    {
                        empresaBuscada.Uf = EmpresaAtualizada.Estado;
                    }

                    ctx.Update(empresaBuscada);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AdicionarVaga(Vaga vaga)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    vaga.DataExpiracao = DateTime.Now.AddDays(30);
                    vaga.DataPublicacao = DateTime.Now;
                    ctx.Add(vaga);
                    ctx.SaveChanges();
                    var VagaNova=ctx.Vaga.FirstOrDefault(v=>v==vaga);
                    AdicionarTecnologiaPadrao(VagaNova.IdVaga);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AtualizarVaga(int idVaga, AtualizarVagaViewModel vaga)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Vaga vagaBuscada = ctx.Vaga.Find(idVaga);
                    if (vagaBuscada == null)
                        return false;

                    if (vaga.Cep != null)
                        vagaBuscada.Cep = vaga.Cep.Trim();

                    if (vaga.TituloVaga != null)
                        vagaBuscada.TituloVaga = vaga.TituloVaga.Trim();

                    if (vaga.idTipoPresenca != 0)
                        vagaBuscada.IdTipoRegimePresencial = vaga.idTipoPresenca;

                    if (vaga.Complemento != null)
                        vagaBuscada.Complemento = vaga.Complemento;

                    if (vaga.DescricaoBeneficio != null)
                        vagaBuscada.DescricaoBeneficio = vaga.DescricaoBeneficio;

                    if (vaga.DescricaoEmpresa != null)
                        vagaBuscada.DescricaoEmpresa = vaga.DescricaoEmpresa;

                    if (vaga.DescricaoVaga != null)
                        vagaBuscada.DescricaoVaga = vaga.DescricaoVaga;

                    if (vaga.Estado != null)
                        vagaBuscada.Estado = vaga.Estado;

                    if (vaga.Experiencia != null)
                        vagaBuscada.Experiencia = vaga.Experiencia;

                    if (vaga.Localidade != null)
                        vagaBuscada.Localidade = vaga.Localidade;

                    if (vaga.IdArea != vagaBuscada.IdArea&&vaga.IdArea!=0)
                        vagaBuscada.IdArea = vaga.IdArea;

                    if (vaga.Logradouro != null)
                        vagaBuscada.Logradouro = vaga.Logradouro;

                    if (vaga.Salario != 0)
                        vagaBuscada.Salario = vaga.Salario;

                    if (vaga.TipoContrato != null)
                        vagaBuscada.TipoContrato = vaga.TipoContrato;

                    ctx.Update(vagaBuscada);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
        public bool DeletarVaga(int idVaga)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Vaga vagaBuscada = ctx.Vaga.Find(idVaga);
                    if (vagaBuscada == null)
                        return false;

                    List<Inscricao> BuscarInscricoes = ctx.Inscricao.Where(u => u.IdVaga == vagaBuscada.IdVaga).ToList();
                    for (int i = 0; i < BuscarInscricoes.Count; i++)
                    {
                        ctx.Remove(BuscarInscricoes[i]);
                        ctx.SaveChanges();
                    }
                    List<VagaTecnologia> VagaTecnologia = ctx.VagaTecnologia.Where(v => v.IdVaga == vagaBuscada.IdVaga).ToList();
                    for (int i = 0; i < VagaTecnologia.Count; i++)
                    {
                        ctx.Remove(VagaTecnologia[i]);
                        ctx.SaveChanges();
                    }
                    ctx.Remove(vagaBuscada);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception )
                {
                    return false;
                }
            }
        }
        public bool VerificarSeTecnologiaFoiAdicionada(int idTecnologia, int idVaga)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    VagaTecnologia vaga = ctx.VagaTecnologia.FirstOrDefault(v => v.IdTecnologia == idTecnologia && v.IdVaga == idVaga);
                    if (vaga != null)
                        return true;

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public void ExpirarVaga()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    List<Vaga> VagasExpiradas = ctx.Vaga.Where(v => v.DataExpiracao >= DateTime.Now).ToList();
                    for(int i = 0; i < VagasExpiradas.Count; i++)
                    {
                        DeletarVaga(VagasExpiradas[i].IdVaga);
                    }
                    ctx.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public bool AdicionarTecnologiaNaVaga(VagaTecnologia vagaTecnologia)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    VagaTecnologia vaga = ctx.VagaTecnologia.FirstOrDefault(u => u.IdVaga == vagaTecnologia.IdVaga && u.IdTecnologia == 1);
                    if (vaga == null)
                    {
                        ctx.Add(vagaTecnologia);
                        ctx.SaveChanges();
                        return true;
                    }
                    else
                    {
                        ctx.Add(vagaTecnologia);
                        ctx.Remove(vaga);
                        ctx.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool RemoverTecnologiaDaVaga(VagaTecnologia vaga)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var BuscandoVagaTecnologia = ctx.VagaTecnologia.FirstOrDefault(u => u == vaga);
                    if (BuscandoVagaTecnologia == null)
                        return false;

                    int Vaga = ctx.VagaTecnologia.Where(u => u.IdVaga == vaga.IdVaga).Count();
                    if (Vaga == 1)
                        return false;

                    ctx.Remove(BuscandoVagaTecnologia);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool VerificarSeTecnologiaExiste(int idTecnologia)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Tecnologia tce = ctx.Tecnologia.FirstOrDefault(e => e.IdTecnologia== idTecnologia);
                    if (tce == null)
                        return true;
                    else
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AprovarCandidato(int idInscricao)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Inscricao inscricaoBuscada = ctx.Inscricao.Find(idInscricao);
                    if (inscricaoBuscada == null)
                        return false;
                    if (inscricaoBuscada.IdStatusInscricao == 2 && inscricaoBuscada.IdStatusInscricao != 3)
                    {
                        inscricaoBuscada.IdStatusInscricao = 1;
                        ctx.Update(inscricaoBuscada);
                        ctx.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool ReprovarCandidato(int idInscricao)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Inscricao inscricaoBuscada = ctx.Inscricao.Find(idInscricao);
                    if (inscricaoBuscada == null)
                        return false;

                    if (inscricaoBuscada.IdStatusInscricao == 2 && inscricaoBuscada.IdStatusInscricao != 1)
                    {
                        inscricaoBuscada.IdStatusInscricao = 3;
                        ctx.Update(inscricaoBuscada);
                        ctx.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<ListarVagasViewModel> ListarVagasDaEmpresa(int idEmpresa)
        {
            try
            {
                List<ListarVagasViewModel> listvagas = new List<ListarVagasViewModel>();

                // Declara a SqlConnection passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a instrução a ser executada
                    string querySelectAll =
                    "SELECT U.CaminhoImagem,v.DataExpiracao,trp.NomeTipoRegimePresencial,are.NomeArea,v.TituloVaga,e.RazaoSocial,v.IdVaga,t.NomeTecnologia,v.Experiencia,v.TipoContrato,v.Salario,v.Localidade FROM VagaTecnologia" +
                    " INNER JOIN Vaga v on v.IdVaga = VagaTecnologia.IdVaga" +
                    " INNER JOIN Tecnologia t on t.IdTecnologia = VagaTecnologia.IdTecnologia" +
                    " INNER JOIN Empresa e on e.IdEmpresa = v.IdEmpresa"+
                    " INNER JOIN Usuario U ON U.IdUsuario=e.IdUsuario" +
                    " INNER JOIN Area are on are.IdArea=v.IdArea"+
                    " INNER JOIN TipoRegimePresencial trp on trp.IdTipoRegimePresencial=v.IdTipoRegimePresencial" +
                    " WHERE e.IdEmpresa =@IDEmpresa";
                    con.Open();

                    // Declara o SqlDataReader para receber os dados do banco de dados
                    SqlDataReader rdr;

                    // Declara o SqlCommand passando o comando a ser executado e a conexão
                    using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                    {
                        cmd.Parameters.AddWithValue("@IDEmpresa", idEmpresa);
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
                                TipoContrato = rdr["TipoContrato"].ToString(),
                                CaminhoImagem=rdr["CaminhoImagem"].ToString(),
                                Localidade = rdr["Localidade"].ToString(),
                                Salario = Convert.ToDecimal(rdr["Salario"]),
                                RazaoSocial = rdr["RazaoSocial"].ToString(),
                                NomeArea = rdr["NomeArea"].ToString(),
                                TituloVaga = rdr["TituloVaga"].ToString(),
                                TipoPresenca=rdr["NomeTipoRegimePresencial"].ToString(),
                             DataExpiracao = Convert.ToDateTime(rdr["DataExpiracao"]).ToString("dd/MM/yyyy")
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
                                continue;
                            else vm.Tecnologias.Add(NomeTecnologia);
                            // Adiciona a vaga criada à lista de vagas
                            listvagas.Add(vm);
                        }
                    }
                }

                // Retorna a lista de vagas
                return listvagas;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool VerificarSeaVagaPertenceaEmpresa(int idEmpresa,int idVaga)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Vaga vagaBuscada = ctx.Vaga.FirstOrDefault(a => a.IdEmpresa == idEmpresa&&a.IdVaga==idVaga);
                    if (vagaBuscada == null)
                        return true;

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<ListarInscricoesViewModel> ListarCandidatosInscritos(int idVaga)
        {
            try
            {
                List<ListarInscricoesViewModel> listInscricoes = new List<ListarInscricoesViewModel>();

                // Declara a SqlConnection passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a instrução a ser executada
                    string querySelectAll =
                    "SELECT U.CaminhoImagem,Inscricao.IdStatusInscricao,C.IdCandidato,IdInscricao,C.NomeCompleto,C.Telefone,Curso.NomeCurso,U.Email FROM Inscricao" +
                    " INNER JOIN Candidato C ON C.IdCandidato=Inscricao.IdCandidato" +
                    " INNER JOIN Usuario U ON U.IdUsuario=C.IdUsuario" +
                    " INNER JOIN Curso ON Curso.IdCurso=C.IdCurso" +
                    " WHERE Inscricao.IdVaga = @IDVaga AND Inscricao.idStatusInscricao=2";
                    con.Open();

                    // Declara o SqlDataReader para receber os dados do banco de dados
                    SqlDataReader rdr;

                    // Declara o SqlCommand passando o comando a ser executado e a conexão
                    using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                    {
                        cmd.Parameters.AddWithValue("@IDVaga", idVaga);
                        // Executa a query e armazena os dados no rdr
                        rdr = cmd.ExecuteReader();

                        // Enquanto houver registros para serem lidos no rdr, o laço se repete
                        while (rdr.Read())
                        {
                            // Instancia um objeto jogo 
                            ListarInscricoesViewModel vm = new ListarInscricoesViewModel
                            {
                                // Atribui às propriedades os valores das colunas da tabela do banco
                                idInscricao = Convert.ToInt32(rdr["IdInscricao"]),
                                idCandidato = Convert.ToInt32(rdr["idCandidato"]),
                                CaminhoImagem=rdr["CaminhoImagem"].ToString(),
                                NomeCandidato = rdr["NomeCompleto"].ToString(),
                                Telefone = rdr["Telefone"].ToString(),
                                NomeCurso = rdr["NomeCurso"].ToString(),
                                Email = rdr["Email"].ToString()
                            };
                            listInscricoes.Add(vm);
                        }
                    }
                }
                // Retorna a lista de vagas
                return listInscricoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public EmpresaCompletaViewModel BuscarEmpresaPorIdUsuario(int idUsuario)
        {
            try
            {
                // Declara a conexão passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a query que será executada
                    string querySelectById =
                        "SELECT E.*,U.CaminhoImagem FROM Empresa E" +
                        " INNER JOIN Usuario U ON U.IdUsuario=E.IdUsuario" +
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
                            EmpresaCompletaViewModel usuario = new EmpresaCompletaViewModel
                            {
                                IdEmpresa=Convert.ToInt32(rdr["IdEmpresa"]),
                                NomeReponsavel=rdr["NomeReponsavel"].ToString(),
                                EmailContato=rdr["EmailContato"].ToString(),
                                Cnpj=rdr["Cnpj"].ToString(),
                                NomeFantasia=rdr["NomeFantasia"].ToString(),
                                RazaoSocial=rdr["RazaoSocial"].ToString(),
                                Telefone=rdr["Telefone"].ToString(),
                                NumFuncionario=Convert.ToInt32(rdr["NumFuncionario"]),
                                NumCnae=rdr["NumCnae"].ToString(),
                                Cep=rdr["Cep"].ToString(),
                                Logradouro=rdr["Logradouro"].ToString(),
                                Complemento=rdr["Complemento"].ToString(),
                                Localidade=rdr["Localidade"].ToString(),
                                Uf=rdr["Uf"].ToString(),
                                CaminhoImagem=rdr["CaminhoImagem"].ToString()
                            };

                            // Retorna o usuario buscado
                            return usuario;
                        }

                        // Caso o resultado da query não possua registros, retorna null
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<TipoRegimePresencial> ListarTipoPresenca()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    return ctx.TipoRegimePresencial.ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<ListarInscricoesViewModel> ListarCandidatosAprovados(int idVaga)
        {
            try
            {
                List<ListarInscricoesViewModel> listInscricoes = new List<ListarInscricoesViewModel>();
                // Declara a SqlConnection passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a instrução a ser executada
                    string querySelectAll =
                    "SELECT U.CaminhoImagem,Inscricao.IdStatusInscricao,C.IdCandidato,IdInscricao,C.NomeCompleto,C.Telefone,Curso.NomeCurso,U.Email FROM Inscricao" +
                    " INNER JOIN Candidato C ON C.IdCandidato=Inscricao.IdCandidato" +
                    " INNER JOIN Usuario U ON U.IdUsuario=C.IdUsuario" +
                    " INNER JOIN Curso ON Curso.IdCurso=C.IdCurso" +
                    " WHERE Inscricao.IdVaga = @IDVaga AND Inscricao.idStatusInscricao=1";
                    con.Open();

                    // Declara o SqlDataReader para receber os dados do banco de dados
                    SqlDataReader rdr;

                    // Declara o SqlCommand passando o comando a ser executado e a conexão
                    using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                    {
                        cmd.Parameters.AddWithValue("@IDVaga", idVaga);
                        // Executa a query e armazena os dados no rdr
                        rdr = cmd.ExecuteReader();

                        // Enquanto houver registros para serem lidos no rdr, o laço se repete
                        while (rdr.Read())
                        {
                            // Instancia um objeto jogo 
                            ListarInscricoesViewModel vm = new ListarInscricoesViewModel
                            {
                                // Atribui às propriedades os valores das colunas da tabela do banco
                                idInscricao = Convert.ToInt32(rdr["IdInscricao"]),
                                idCandidato = Convert.ToInt32(rdr["idCandidato"]),
                                NomeCandidato = rdr["NomeCompleto"].ToString(),
                                CaminhoImagem=rdr["CaminhoImagem"].ToString(),
                                Telefone = rdr["Telefone"].ToString(),
                                NomeCurso = rdr["NomeCurso"].ToString(),
                                Email = rdr["Email"].ToString()
                            };
                              listInscricoes.Add(vm);
                        }
                    }
                }
                // Retorna a lista de vagas
                return listInscricoes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<Candidato> ListarCandidatosEstagiandoNaEmpresa(int idEmpresa)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    List<Estagio> ListaEstagios = ctx.Estagio.Where(e => e.IdEmpresa == idEmpresa).ToList();
                    List<Candidato> candidatos = new List<Candidato>();
                    for(int i = 0; i < ListaEstagios.Count; i++)
                    {
                        Candidato candidatoBuscado = ctx.Candidato.Find(ListaEstagios[i].IdCandidato);
                        candidatos.Add(candidatoBuscado);
                    }
                    return candidatos;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public void AdicionarTecnologiaPadrao(int idVaga)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var Vaga = ctx.Vaga.Find(idVaga);
                    int idTecnologia = 1;
                    AdicionarTecnologiaNaVaga(new VagaTecnologia { IdVaga=Vaga.IdVaga,IdTecnologia=idTecnologia});
                }
                catch (Exception){}
            }
        }
    }
}
