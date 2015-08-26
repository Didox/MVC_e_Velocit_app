using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;
using NVelocity.App;
using System.IO;
using NVelocity;
using System.Reflection;

namespace Didox.Business
{
    #region Class Usuario

    [Table(Name = "SEG_Credencial")]
    public class Usuario : CType
    {
        #region Construtores
        public Usuario() { }

        public Usuario(int? idUsuario)
        {
            this.IDUsuario = idUsuario;
        }

        public Usuario(Campanha campanha)
        {
            this.campanha = campanha;
        }

        public Usuario(string nome)
        {
            this.Nome = nome;
        }

        #endregion

        #region Destrutor
        ~Usuario() { Dispose(); }
        #endregion

        #region Attributes
        private Pessoa pessoa;
        private Campanha campanha;
        #endregion

        #region Propriedades

        [Property(IsPk = true, Name = "idCredencial")]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDUsuario { get; set; }

        [Validate]
        [Property(IsField = true, Size = 60, Name = "NomeCompleto")]
        [Operations(UseSave = true, UseGet = true)]
        public string Nome { get; set; }

        [Validate]
        [Property(IsField = true, Size = 20, DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Login { get; set; }

        [Validate]
        [Property(IsField = true, Size = 255, DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Senha { get; set; }

        [Validate]
        [Property(IsField = true, Size = 60, DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Email { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public bool Ativo { get; set; }

        [Property(IsField = true, Size = 5)]
        [Operations(UseSave = true)]
        public string Ramal { get; set; }

        public Pessoa Pessoa
        {
            get
            {
                if (this.pessoa == null)
                {
                    this.pessoa = new Pessoa(Campanha).GetPessoaByCampanha(this);
                    if (!CType.Exist(this.pessoa)) this.pessoa = null;
                }
                return this.pessoa;
            }
        }

        public Campanha Campanha
        {
            get
            {
                if (this.campanha == null)
                {
                    this.campanha = Campanha.Current();
                }
                return this.campanha;
            }
        }

        #endregion

        #region Methods

        public List<int> GetListIdEstruturaCargo()
        {
            var ids = new List<int>();
            if (this.Pessoa != null)
            {
                if (this.Pessoa.EstruturaPessoaCargo != null)
                {
                    var estrutura = this.Pessoa.EstruturaPessoaCargo.CargoEstrutura;
                    foreach (PropertyInfo pi in estrutura.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        if (pi.Name.ToLower().IndexOf("idpessoanivel") != -1)
                        {
                            var value = pi.GetValue(estrutura, null);
                            if (value != null) ids.Add((int)value);
                        }
                }
            }
            return ids;
        }

        public bool Logon(string login, string senha)
        {
            if (string.IsNullOrEmpty(login)) return false;
            if (string.IsNullOrEmpty(senha)) return false;

            this.Login = login;
            this.Senha = senha;
            this.Get();
            if (this.IDUsuario == null) return false;
            if (!this.Ativo) return false;
            AdicionaSessao();

            if (Cliente.Current() != null)
            {
                var log = new Log();
                log.Descricao = "Usuário " + this.Login + " acessou o sitema em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                log.Usuario = this;
                log.Cliente = Cliente.Current();
                log.Programa = Programa.Current();
                log.Campanha = Campanha;
                log.Tipo = TipoLog.Login;
                log.Data = DateTime.Now;
                log.Save();
            }

            return true;
        }

        public void Logoff()
        {
            Session.Invalidate(KeyUsuario());
        }

        public void AdicionaSessao()
        {
            if (this.IDUsuario == null) throw new TradeVisionError404("Usuario não encontrado.");
            Session.Add(KeyUsuario(), this.IDUsuario);
        }

        public static Usuario Current()
        {
            object session = Session.Get(KeyUsuario());
            if (session == null) return null;
            string sessionKey = session.ToString();
            int idUsuario = 0;
            if (!int.TryParse(sessionKey, out idUsuario)) return null;
            if (idUsuario == 0) return null;

            var usuario = new Usuario(idUsuario);
            usuario.Get();
            return usuario;
        }

        private static string KeyUsuario()
        {
            return typeof(Usuario).Name + "Login";
        }

        public Log BuscaUltimoAcesso()
        {
            return new Log().BuscaUltimoAcesso();
        }

        public int BuscaQuantidadeAcesso()
        {
            return new Log().BuscaQuantidadeAcesso();
        }

        public override void Get()
        {
            this.Senha = ConfiguracaoSenha.GetSenhaCripto(this.Senha);
            base.Get();
        }

        public override void Save()
        {
            var usuarioEmail = new Usuario();
            usuarioEmail.Email = this.Email;
            usuarioEmail.Get();

            if (usuarioEmail.IDUsuario != null && usuarioEmail.IDUsuario != this.IDUsuario)
                throw new DidoxFrameworkError("Email já cadastrado.");

            var usuarioLogin = new Usuario();
            usuarioLogin.Login = this.Login;
            usuarioLogin.Get();

            if (usuarioLogin.IDUsuario != null && usuarioLogin.IDUsuario != this.IDUsuario)
                throw new DidoxFrameworkError("Login já cadastrado.");

            this.Senha = ConfiguracaoSenha.GetSenhaCripto(this.Senha);

            base.Save();

            var pessoa = this.Pessoa;
            if (CType.Exist(pessoa))
            {
                pessoa.TipoPessoa = TipoPessoa.Fisica;
                pessoa.Nome = this.Nome;
                pessoa.Save();
            }
            else
            {
                var pessoaCampanha = new PessoaCampanha();
                try
                {
                    pessoaCampanha.IsTransaction = true;
                    pessoaCampanha.Usuario = this;
                    pessoaCampanha.Get();
                    if (CType.Exist(pessoaCampanha)) pessoa = pessoaCampanha.Pessoa;
                    else
                    {
                        pessoa = new Pessoa(Campanha);
                        pessoa.Transaction = pessoaCampanha.Transaction;
                        pessoa.TipoPessoa = TipoPessoa.Fisica;
                        pessoa.Nome = this.Nome;
                        pessoa.Save();
                    }

                    var pessoaCampanhaNovo = new PessoaCampanha();
                    pessoaCampanhaNovo.Transaction = pessoaCampanha.Transaction;
                    pessoaCampanhaNovo.Usuario = this;
                    pessoaCampanhaNovo.Pessoa = pessoa;
                    pessoaCampanhaNovo.Campanha = Campanha;
                    pessoaCampanhaNovo.DataAdesao = DateTime.Now;
                    pessoaCampanhaNovo.Save();
                    pessoaCampanha.Commit();
                }
                catch (Exception err)
                {
                    pessoaCampanha.Rollback();
                    throw err;
                }
            }

            var email = this.Pessoa.Email;
            email.EnderecoEmail = this.Email;
            email.TipoEmail = TipoEmail.Padrao();
            email.Save();
        }

        #endregion

        #region Render Methods

        public string GetUsuariosAdesao(int idHierarquia)
        {
            var hierarquia = new Hierarquia(idHierarquia);
            hierarquia.Get();

            var componente = new Componente();
            componente.Chave = "adesao-usuarios";
            componente.Get();
            if (componente.Conteudo == null) return "";

            Velocity.Init();
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string GetComboAdesao(int idHierarquia, int nivelAtual)
        {
            var usuario = Usuario.Current();
            var componente = new Componente();
            componente.Chave = "adesao-combo";
            componente.Get();
            if (componente.Conteudo == null) return "";

            var estruturas = new List<Hierarquia>();
            Velocity.Init();
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            context.Put("nivelAtual", nivelAtual);
            context.Put("nextNivel", (nivelAtual + 1));
            context.Put("idHierarquia", idHierarquia);
            context.Put("estruturas", estruturas);
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string GetHtmlAlterarEndereco()
        {
            var usuario = Usuario.Current();

            var componente = new Componente();
            componente.Chave = "adesao-alterar-endereco";
            componente.Get();
            if (componente.Conteudo == null) return "";

            Velocity.Init();
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            context.Put("usuario", this);
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string GetHtmlIncluirResponsavelHtml(int idHierarquia, List<Usuario> usuarios)
        {
            var hierarquia = new Hierarquia(idHierarquia);
            hierarquia.Get();

            var componente = new Componente();
            componente.Chave = "adesao-incluir-responsavel";
            componente.Get();
            if (componente.Conteudo == null) return "";

            Velocity.Init();
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            context.Put("usuario", this);
            context.Put("hierarquia", hierarquia);
            context.Put("usuarios", usuarios);
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string GetHtmlIncluirResponsavelHtml(int idHierarquia)
        {
            return GetHtmlIncluirResponsavelHtml(idHierarquia, null);
        }

        

        public void CreateTabelaDinamica()
        {
            var tabela = new Tabela();
            tabela.Descricao = "TabelaPessoaFisica" + Cliente.Current().Nome;
            tabela.Save();

            var tabelaCliente = new TabelaCliente(Cliente.Current());
            tabelaCliente.Tabela = tabela;
            tabelaCliente.TipoPessoa = TipoPessoa.Fisica;
            tabelaCliente.Save();
        }

        public bool ExistePessoa()
        {
            return this.Pessoa != null;
        }

        #endregion
    }
    #endregion
}
