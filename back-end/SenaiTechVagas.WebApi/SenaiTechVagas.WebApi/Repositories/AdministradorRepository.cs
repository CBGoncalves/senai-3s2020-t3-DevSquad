﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SenaiTechVagas.WebApi.Contexts;
using SenaiTechVagas.WebApi.Domains;
using SenaiTechVagas.WebApi.Interfaces;
using SenaiTechVagas.WebApi.Utils;
using SenaiTechVagas.WebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SenaiTechVagas.WebApi.Repositories
{
    public class AdministradorRepository :EmpresaRepository,IAdministradorRepository
    {
        string stringConexao = "Data Source=.\\SQLEXPRESS; Initial Catalog=Db_TechVagas;integrated Security=True";
        public bool AtualizarCurso(int id, Curso curso)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Curso cursoBuscado = ctx.Curso.Find(id);
                    cursoBuscado.NomeCurso = curso.NomeCurso;
                    cursoBuscado.TipoCurso = curso.TipoCurso;
                    ctx.Curso.Update(cursoBuscado);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool CadastrarCurso(Curso curso)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    ctx.Add(curso);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public string CadastrarEstagio(CadastrarEstagioViewModel estagio)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var Candidato = ctx.Candidato.FirstOrDefault(u=>u.IdUsuario==estagio.IdUsuario);
                    if (Candidato == null)
                        return "Candidato não encontardo";

                    var resposta=VerificarSeExiste(Candidato.IdCandidato);
                    if (resposta == true)
                        return "Estágio ja cadastrado";

                    Estagio estage = new Estagio() {
                        IdCandidato = Candidato.IdCandidato,
                        IdEmpresa = estagio.IdEmpresa,
                        PeriodoEstagio = estagio.PeriodoEstagio,
                        DataCadastro=DateTime.Now
                    };
                    ctx.Add(estage);
                    ctx.SaveChanges();
                    return "Estágio casdastrado com sucesso";
                }
                catch (Exception)
                {
                    return "Erro no sistema";
                }
            }
        }

        public bool AtualizarEstagio(int idEstagio,int estagioAtualizado)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Estagio estagioBuscado = ctx.Estagio.Find(idEstagio);
                    if (estagioBuscado == null)
                        return false;

                    estagioBuscado.PeriodoEstagio = estagioAtualizado;
                    ctx.Update(estagioBuscado);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool DeletarEstagioPorId(int idEstagio)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Estagio estagioBuscado = ctx.Estagio.Find(idEstagio);
                    if (estagioBuscado == null)
                        return false;

                    ctx.Remove(estagioBuscado);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<ListarEstagiosViewModel> ListarEstagios()
        {
            try
            {
                List<ListarEstagiosViewModel> listEstagios = new List<ListarEstagiosViewModel>();

                // Declara a SqlConnection passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a instrução a ser executada
                    string querySelectAll =
                   " SELECT U.IdUsuario,U.CaminhoImagem,Estagio.DataCadastro,Curso.NomeCurso,Estagio.IdEstagio,PeriodoEstagio,E.RazaoSocial,C.NomeCompleto,A.NomeArea,C.Telefone,U.Email FROM Estagio" +
                   " INNER JOIN Empresa E on E.IdEmpresa = Estagio.IdEmpresa" +
                   " INNER JOIN Candidato C on C.IdCandidato = Estagio.IdCandidato" +
                   " INNER JOIN Usuario U on U.IdUsuario = C.IdUsuario" +
                   " INNER JOIN Curso ON Curso.idCurso=C.idCurso"+
                   " INNER JOIN Area A ON A.IdArea=Curso.IdArea";
                    con.Open();
                    // Declara o SqlDataReader para receber os dados do banco de dados
                    SqlDataReader rdr;

                    // Declara o SqlCommand passando o comando a ser executado e a conexão
                    using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                    {
                        // Executa a query e armazena os dados no rdr
                        rdr = cmd.ExecuteReader();

                        // Enquanto houver registros para serem lidos no rdr, o laço se repete
                        while (rdr.Read())
                        {
                            // Instancia um objeto jogo 
                            ListarEstagiosViewModel vm = new ListarEstagiosViewModel
                            {
                                // Atribui às propriedades os valores das colunas da tabela do banco
                                idEstagio = Convert.ToInt32(rdr["IdEstagio"]),
                                NomeCompleto = (rdr["NomeCompleto"]).ToString(),
                                IdUsuario=Convert.ToInt32(rdr["IdUsuario"]),
                                EmailCandidato = (rdr["Email"]).ToString(),
                                CaminhoImagem = rdr["CaminhoImagem"].ToString(),
                                PeriodoEstagio = Convert.ToInt32(rdr["PeriodoEstagio"]),
                                Telefone = (rdr["Telefone"]).ToString(),
                                RazaoSocial = (rdr["RazaoSocial"]).ToString(),
                                NomeArea = (rdr["NomeArea"]).ToString(),
                                NomeCurso = (rdr["NomeCurso"]).ToString()
                            };
                            var DataCadastro = Convert.ToDateTime(rdr["DataCadastro"]);
                            var resultado = tempoDeEstagio(DataCadastro, DateTime.Now);
                            vm.TempoEstagiado = resultado;
                            if (vm.TempoEstagiado >= vm.PeriodoEstagio)
                                vm.StatusEstagio = "Estagio encerrado";
                            else
                                vm.StatusEstagio = "Estagiando";
                            listEstagios.Add(vm);
                        }
                    }
                }
                // Retorna a lista de vagas
                return listEstagios;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        static int tempoDeEstagio(DateTime dataInicioEstagio, DateTime dataAtual)
        {
            long elapsedTicks = dataAtual.Ticks - dataInicioEstagio.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            double tempoEmDiasDouble = elapsedSpan.TotalDays;
            int tempoEmDiasInt = Convert.ToInt32(tempoEmDiasDouble);
            return tempoEmDiasInt / 30;
        }

        public bool VerificarSeExiste(int idCandidato)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Estagio estagioBuscado = ctx.Estagio.FirstOrDefault(e => e.IdCandidato == idCandidato);
                    if (estagioBuscado != null)
                        return true;

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public int[] ContadorCadastros()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var Empresa = ctx.Empresa.ToList().Count;
                    var Candidato = ctx.Candidato.ToList().Count;
                    var Estagios = ctx.Estagio.ToList().Count;
                    List<int> Cont = new List<int>();
                    Cont.Add(Empresa);
                    Cont.Add(Estagios);
                    Cont.Add(Candidato);
                    return Cont.ToArray();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool AtualizarTipoUsuario(int id, TipoUsuario tipoUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    TipoUsuario tipoUsuarioBuscado = ctx.TipoUsuario.Find(id);
                    tipoUsuarioBuscado.NomeTipoUsuario = tipoUsuario.NomeTipoUsuario;
                    ctx.TipoUsuario.Update(tipoUsuarioBuscado);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool CadastrarTipoUsuario(TipoUsuario tipoUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    ctx.Add(tipoUsuario);
                    ctx.SaveChanges();
                    return true;

                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<TipoUsuario> ListarTipoUsuario()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    return ctx.TipoUsuario.ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public bool AtualizarStatusInscricao(int id, StatusInscricao status)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var statusBuscado = ctx.StatusInscricao.Find(id);
                    if (statusBuscado == null)
                        return false;

                    if (status.NomeStatusInscricao != null)
                        statusBuscado.NomeStatusInscricao = status.NomeStatusInscricao;

                    ctx.Update(statusBuscado);
                    ctx.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool CadastrarStatusInscricao(StatusInscricao statusInscricao)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    ctx.Add(statusInscricao);
                    ctx.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public List<StatusInscricao> ListarStatusInscricao()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                return ctx.StatusInscricao.ToList();
            }
        }
        public bool AtualizarTecnologia(int id, Tecnologia tecnologia)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Tecnologia tecnologiaBuscada = ctx.Tecnologia.Find(id);
                    tecnologiaBuscada.NomeTecnologia = tecnologia.NomeTecnologia;
                    ctx.Tecnologia.Update(tecnologiaBuscada);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool CadastrarTecnologia(Tecnologia tecnologia)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    ctx.Add(tecnologia);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public List<Candidato> ListarCandidatos()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {

                try
                {
                    return ctx.Candidato.Select(u=>
                    new Candidato { NomeCompleto=u.NomeCompleto,IdUsuario=u.IdUsuario,IdUsuarioNavigation=
                    new Usuario { Email=u.IdUsuarioNavigation.Email,CaminhoImagem=u.IdUsuarioNavigation.CaminhoImagem,IdTipoUsuario=u.IdUsuarioNavigation.IdTipoUsuario} })
                    .Where(u=>u.IdUsuarioNavigation.IdTipoUsuario!=4).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        
        public bool DeletarInscricao(int idInscricao)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Inscricao inscricaoBuscada = ctx.Inscricao.Find(idInscricao);
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
        public bool DeletarEmpresaPorId(int idUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Empresa empresaBuscado = ctx.Empresa.FirstOrDefault(u=>u.IdUsuario==idUsuario);
                    if (empresaBuscado == null)
                        return false;

                    List<Estagio> ListaDeEstagios = ctx.Estagio.Where(i => i.IdEmpresa == empresaBuscado.IdEmpresa).ToList();
                    for (int i = 0; i < ListaDeEstagios.Count; i++)
                    {

                        if (DeletarEstagioPorId(ListaDeEstagios[i].IdEmpresa))
                            continue;

                        break;
                    }
                    List<VagaTecnologia> ListaDeVagaTecnologia = ctx.VagaTecnologia.Where(i => i.IdVagaNavigation.IdEmpresa == empresaBuscado.IdEmpresa).ToList();
                    for (int i = 0; i < ListaDeVagaTecnologia.Count; i++)
                    {
                        VagaTecnologia vaga=new VagaTecnologia(){
                          IdTecnologia=ListaDeVagaTecnologia[i].IdTecnologia,
                          IdVaga=ListaDeVagaTecnologia[i].IdVaga
                        };
                        if (RemoverTecnologiaDaVaga(vaga))
                            continue;

                        break;
                    }

                    List<Vaga> ListaDeVaga = ctx.Vaga.Where(i => i.IdEmpresa == empresaBuscado.IdEmpresa).ToList();
                    for (int i = 0; i < ListaDeVaga.Count; i++)
                    {
                        if (DeletarVaga(ListaDeVaga[i].IdVaga))
                            continue;
                    }
                    DeletarUsuarioEmpresaCandidato(empresaBuscado.IdUsuario);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        public bool DeletarCandidato(int IdUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Candidato CandidatoBuscado = ctx.Candidato.FirstOrDefault(u => u.IdUsuario == IdUsuario);
                    if (CandidatoBuscado == null)
                        return false;

                    List<Inscricao> listaDeInscricao = ctx.Inscricao.
                        Where(l => l.IdCandidato == CandidatoBuscado.IdCandidato).ToList();
                    for (int i = 0; i < listaDeInscricao.Count; i++)
                    {
                        DeletarInscricao(listaDeInscricao[i].IdInscricao);
                    }
                    Estagio estagioBuscado = ctx.Estagio.FirstOrDefault(e => e.IdCandidato == CandidatoBuscado.IdCandidato);
                    if (estagioBuscado != null)
                    {
                        ctx.Remove(estagioBuscado);
                        ctx.SaveChanges();
                    }
                    DeletarUsuarioEmpresaCandidato(CandidatoBuscado.IdUsuario);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Foi necessario criar esse metodo para poder deletar empresa/Candidato e usuario,no mesmo metodo estava dando erro
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public bool DeletarUsuarioEmpresaCandidato(int idUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Usuario usuarioBuscado = ctx.Usuario.Find(idUsuario);
                    Empresa empresaBuscada = ctx.Empresa.FirstOrDefault(e => e.IdUsuario == idUsuario);
                    if (empresaBuscada != null)
                    {
                        ctx.Remove(empresaBuscada);
                        ctx.Remove(usuarioBuscado);
                        ctx.SaveChanges();
                        if (usuarioBuscado.CaminhoImagem != "Teste.webp")
                        {
                        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "imgPerfil/");
                        string CaminhoDoArquivo = pathToSave + usuarioBuscado.CaminhoImagem;
                        File.Delete(CaminhoDoArquivo);
                        }
                        return true;
                    }

                    Candidato candidatoBuscado = ctx.Candidato.FirstOrDefault(e => e.IdUsuario == idUsuario);
                    if (candidatoBuscado != null)
                    {
                        ctx.Remove(candidatoBuscado);
                        ctx.Remove(usuarioBuscado);
                        ctx.SaveChanges();
                        if (usuarioBuscado.CaminhoImagem != "user.png")
                        {
                            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "imgPerfil/");
                            string CaminhoDoArquivo = pathToSave + usuarioBuscado.CaminhoImagem;
                            File.Delete(CaminhoDoArquivo);
                        }
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
        public List<Empresa> ListarEmpresa()
        {
            using(DbSenaiContext ctx=new DbSenaiContext())
            {
                try
                {
                    return ctx.Empresa.Select(u =>
                    new Empresa { RazaoSocial=u.RazaoSocial,IdUsuario=u.IdUsuario,EmailContato=u.EmailContato,IdUsuarioNavigation=
                    new Usuario { CaminhoImagem=u.IdUsuarioNavigation.CaminhoImagem,IdTipoUsuario=u.IdUsuarioNavigation.IdTipoUsuario} })
                    .Where(u=>u.IdUsuarioNavigation.IdTipoUsuario!=4).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public bool DeletarVagaEmpresa(int idVaga)
        {
            try
            {
            return DeletarVaga(idVaga);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<Usuario> ListaDebanidos()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    return ctx.Usuario.Where(u => u.IdTipoUsuario == 4).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public bool DesbanirUsuario(int idUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Empresa empresaBuscada = ctx.Empresa.Include(u => u.IdUsuarioNavigation).FirstOrDefault(e => e.IdUsuario == idUsuario);
                    if (empresaBuscada != null)
                    {
                        if (empresaBuscada.IdUsuarioNavigation.IdTipoUsuario != 3)
                        {
                            empresaBuscada.IdUsuarioNavigation.IdTipoUsuario = 3;
                            ctx.Update(empresaBuscada);
                            ctx.SaveChanges();
                            return true;
                        }
                        return false;
                    }
                    Candidato candidatoBuscado = ctx.Candidato.Include(u => u.IdUsuarioNavigation).FirstOrDefault(e => e.IdUsuario == idUsuario);
                    if (candidatoBuscado != null)
                    {
                        if (candidatoBuscado.IdUsuarioNavigation.IdTipoUsuario != 2)
                        {
                            candidatoBuscado.IdUsuarioNavigation.IdTipoUsuario = 2;
                            ctx.Update(candidatoBuscado);
                            ctx.SaveChanges();
                            return true;
                        }
                    }
                    Usuario ColaboradorBuscado = ctx.Usuario.Find(idUsuario);
                    if (empresaBuscada == null && candidatoBuscado == null && ColaboradorBuscado != null && ColaboradorBuscado.IdTipoUsuario == 4)
                    {
                        ColaboradorBuscado.IdTipoUsuario = 1;
                        ctx.Update(ColaboradorBuscado);
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
        public bool BanirUsuario(int idUsuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Usuario usuarioBuscadoCandidato = ctx.Usuario.Include(u => u.Candidato).FirstOrDefault(u => u.IdUsuario == idUsuario);
                    Usuario usuarioBuscadoEmpresa = ctx.Usuario.Include(u => u.Empresa).FirstOrDefault(u => u.IdUsuario == idUsuario);
                    Usuario usuarioBuscadoColaborador = ctx.Usuario.Find(idUsuario);
                    if (usuarioBuscadoCandidato == null && usuarioBuscadoEmpresa == null && usuarioBuscadoColaborador == null)
                        return false;

                    if (usuarioBuscadoCandidato.IdTipoUsuario != 4 && usuarioBuscadoCandidato.Candidato != null)
                    {
                        Candidato candidato = ctx.Candidato.Include(u => u.Inscricao).FirstOrDefault(u => u.IdUsuarioNavigation.IdUsuario == usuarioBuscadoCandidato.IdUsuario);
                        for (int i = 0; i < candidato.Inscricao.Count; i++)
                        {
                            Inscricao inscricao = ctx.Inscricao.FirstOrDefault(u => u.IdCandidato == candidato.IdCandidato);
                            if (inscricao == null)
                                break;
                            DeletarInscricao(inscricao.IdInscricao);
                        }
                        usuarioBuscadoCandidato.IdTipoUsuario = 4;
                        ctx.Update(usuarioBuscadoCandidato);
                        ctx.SaveChanges();
                        return true;
                    }
                    else if (usuarioBuscadoEmpresa.IdTipoUsuario != 4 && usuarioBuscadoEmpresa.Empresa != null)
                    {
                        Empresa empresaBuscada = ctx.Empresa.Include(u => u.Vaga).FirstOrDefault(u => u.IdUsuarioNavigation.IdUsuario == usuarioBuscadoEmpresa.IdUsuario);
                        for (int i = 0; i < empresaBuscada.Vaga.Count; i++)
                        {
                            Vaga vagaBuscada = ctx.Vaga.FirstOrDefault(u => u.IdEmpresa == empresaBuscada.IdEmpresa);
                            if (vagaBuscada == null)
                                break;

                            DeletarVaga(vagaBuscada.IdVaga);
                        }
                        usuarioBuscadoEmpresa.IdTipoUsuario = 4;
                        ctx.Update(usuarioBuscadoEmpresa);
                        ctx.SaveChanges();
                        return true;
                    }
                    else if (usuarioBuscadoColaborador.IdTipoUsuario != 4 && usuarioBuscadoColaborador.IdTipoUsuario == 1)
                    {
                        usuarioBuscadoColaborador.IdTipoUsuario = 4;
                        ctx.Update(usuarioBuscadoColaborador);
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

        public bool CadastrarAdministardor(Usuario usuario)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    usuario.CaminhoImagem = "user.png";
                    usuario.IdTipoUsuario = 1;
                    ctx.Add(usuario);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DeletarAdministrador(int id)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Usuario usuario = ctx.Usuario.Find(id);
                    if (usuario.IdTipoUsuario !=1 || usuario == null || usuario.IdUsuario == 1)
                        return false;
                    else
                    {
                        ctx.Remove(usuario);
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

        public List<Usuario> ListarAdministradores()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    return ctx.Usuario.Where(v => v.IdTipoUsuario == 1).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool CadastrarArea(Area area)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    ctx.Add(area);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AtualizarArea(int idArea, Area area)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    Area areaBuscada = ctx.Area.Find(idArea);
                    if (area.NomeArea != null)
                        areaBuscada.NomeArea = area.NomeArea;

                    ctx.Update(areaBuscada);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AlterarSenhaDoUsuario(string email, string NovaSenha)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var usuario = ctx.Usuario.FirstOrDefault(u => u.Email == email);
                    if (usuario == null)
                        return false;
                    else
                    {
                        usuario.Senha = Crypter.Criptografador(NovaSenha);
                        ctx.Update(usuario);
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

        public List<ListarInscricoesViewModel> ListarCandidatosInscritosEmpresa(int idVaga)
        {
            try
            {
                return ListarCandidatosInscritos(idVaga);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Usuario> ListarEmailsCandidato()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var a = ctx.Usuario.Where(u => u.IdTipoUsuario == 2).Select(c => new Usuario {IdUsuario=c.IdUsuario, Email = c.Email }).ToList();
                    return a;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public List<Empresa> ListarNomeEmpresas()
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    return ctx.Empresa.Select(u => new Empresa { IdEmpresa = u.IdEmpresa, RazaoSocial = u.RazaoSocial }).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool AdicionarTipoPresenca(TipoRegimePresencial trp)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    if (trp.NomeTipoRegimePresencial != null)
                    {
                        ctx.Add(trp);
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

        public bool AtualizarTipoPresenca(int id,TipoRegimePresencial trp)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    TipoRegimePresencial tipo = ctx.TipoRegimePresencial.Find(id);
                    if (trp.NomeTipoRegimePresencial != null&&tipo!=null)
                    {
                        ctx.Update(trp);
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

        public string BuscarImagemPerfilAdm(int idAms)
        {
            using (DbSenaiContext ctx = new DbSenaiContext())
            {
                try
                {
                    var usuario = ctx.Usuario.Find(idAms);
                    return usuario.CaminhoImagem;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public EmpresaCompletaViewModel BuscarEmpresaPorIdUsuarioAdm(int idUsuario)
        {
            try
            {
               return BuscarEmpresaPorIdUsuario(idUsuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<ListarVagasViewModel> ListarVagasDaEmpresaAdm(int idEmpresa)
        {
            try
            {
                return ListarVagasDaEmpresa(idEmpresa);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public CandidatoCompletoViewModel BuscarCandidatoPorIdUsuario(int idUsuario)
        {
            try
            {
                // Declara a conexão passando a string de conexão
                using (SqlConnection con = new SqlConnection(stringConexao))
                {
                    // Declara a query que será executada
                    string querySelectById =
                        "SELECT  A.NomeArea,cur.NomeCurso,A.IdArea,C.IdCandidato,cur.IdCurso,NomeCompleto,Rg,Cpf,Telefone,C.LinkLinkedinCandidato,U.CaminhoImagem FROM Candidato C" +
                        " INNER JOIN Curso cur ON cur.IdCurso=C.IdCurso" +
                        " INNER JOIN Area A ON A.IdArea=cur.IdArea" +
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
                                NomeArea = rdr["NomeArea"].ToString(),
                                NomeCurso = rdr["NomeCurso"].ToString(),
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
            }
            catch (Exception)
            {
                return null;
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

                     List<ListarVagasViewModel> ListVaga = new List<ListarVagasViewModel>();
                    for(int i = 0; i < ListaDeInscricoes.Count; i++)
                    {
                        Vaga v = ctx.Vaga.Select(u => 
                        new Vaga {TituloVaga=u.TituloVaga,IdVaga=u.IdVaga,IdAreaNavigation=
                        new Area { NomeArea=u.IdAreaNavigation.NomeArea},IdEmpresaNavigation=
                        new Empresa { IdUsuarioNavigation=
                        new Usuario { CaminhoImagem=u.IdEmpresaNavigation.IdUsuarioNavigation.CaminhoImagem} } })
                        .FirstOrDefault(u => u.IdVaga == ListaDeInscricoes[i].IdVaga);
                        ListVaga.Add(new ListarVagasViewModel()
                        {
                            TituloVaga = v.TituloVaga,
                            IdVaga = v.IdVaga,
                            IdInscricao = ListaDeInscricoes[i].IdInscricao,
                            CaminhoImagem = v.IdEmpresaNavigation.IdUsuarioNavigation.CaminhoImagem,
                            NomeArea = v.IdAreaNavigation.NomeArea
                        });
                    }
                    return ListVaga;
                    
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool DeletarUsuarioBanido(int idUsuario)
        {
            using (DbSenaiContext ctx=new DbSenaiContext())
            {
                try
                {
                    Candidato c = ctx.Candidato.FirstOrDefault(u=>u.IdUsuario==idUsuario);
                    if (c == null)
                    {
                        Empresa e = ctx.Empresa.FirstOrDefault(u => u.IdUsuario == idUsuario);
                            if (e == null)
                            return false;

                        DeletarEmpresaPorId(e.IdUsuario);
                        return true;
                    }
                    else
                    {
                        DeletarCandidato(c.IdUsuario);
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
