using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using NVelocity;
using NVelocity.App;

namespace Didox.Business
{
    #region Class Pagina

    [Table(Name = "EST_Pagina", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Pagina : CType
    {
        #region Construtores
        public Pagina()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Pagina(int? idPagina)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDPagina = idPagina;
        }

        public Pagina(int? idPagina, Template template)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDPagina = idPagina;
            this.template = template;
        }

        public Pagina(string slug)
        {
            CarregarConnectionString(Cliente.Current());
            this.Slug = slug;
        }
        #endregion

        #region Destrutor
        ~Pagina() { Dispose(); }
        #endregion

        #region Constants
        public const string ERROR404 = "404-Erro";
        public const string ERROR403 = "403-Erro";
        #endregion

        #region Attributes
        private LIType paginas;
        private Pagina pagina;
        private Template template;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPagina { get; set; }

        [Validate]
        [Property(IsField = true, Size=50, Name = "dsPagina")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, Size = 50, Name = "noPagina")]
        [Operations(UseSave = true, UseGet = true)]
        public string Nome { get; set; }

        [Validate]
        [Property(IsField = true, Size = 100, Name = "SlugPagina", DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Slug { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTemplate { get; set; }

        public Template Template
        {
            get
            {
                if (this.template == null)
                {
                    this.template = new Template();
                    this.template.IDTemplate = this.IDTemplate;
                }

                if (!this.template.IsFull)
                {
                    this.template.Transaction = this.Transaction;
                    this.template.Get();
                }

                return this.template;
            }
            set
            {
                this.template = value;
                this.IDTemplate = value.IDTemplate;
            }
        }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPaginaPai { get; set; }

        public Pagina PaginaPai
        {
            get
            {
                if (this.pagina == null)
                {
                    this.pagina = new Pagina();
                    this.pagina.IDPagina = this.IDPaginaPai;
                }

                if (!this.pagina.IsFull)
                {
                    this.pagina.Transaction = this.Transaction;
                    this.pagina.Get();
                }

                return this.pagina;
            }
            set
            {
                this.pagina = value;
                this.IDPaginaPai = value.IDPagina;
            }
        }

        public LIType Paginas
        {
            get
            {
                if (this.paginas == null)
                {
                    Pagina pagina = new Pagina();
                    pagina.Transaction = this.Transaction;
                    pagina.PaginaPai = this;
                    this.paginas = Pagina.Find(pagina);
                }
                return this.paginas;
            }
        }

        #endregion
        
        #region Methods
        #endregion


        public static Pagina HomeCliente()
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Usuario user = Usuario.Current();

            Pagina pagina = null;
            if (user != null)
                pagina = (Pagina)new DataBase.Pagina().BuscarHomePaginaRestrito(new Pagina(), (int)cliente.IDCliente, null, null, user.IDUsuario);
            else pagina = (Pagina)new DataBase.Pagina().BuscarHomePagina(new Pagina(), (int)cliente.IDCliente, null, null);

            return validaPagina(pagina);
        }

        public static Pagina HomePrograma()
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Programa programa = Programa.Current();
            if (programa == null) return null;

            Usuario user = Usuario.Current();
            
            Pagina pagina = null;
            if (user != null)
                pagina = (Pagina)new DataBase.Pagina().BuscarHomePaginaRestrito(new Pagina(), (int)cliente.IDCliente, programa.IDPrograma, null, user.IDUsuario);
            else pagina = (Pagina)new DataBase.Pagina().BuscarHomePagina(new Pagina(), (int)cliente.IDCliente, programa.IDPrograma, null);

            return validaPagina(pagina);
        }

        public static Pagina HomeCampanha()
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Programa programa = Programa.Current();
            if (programa == null) return null;

            Campanha campanha = Campanha.Current();
            if (campanha == null) return null;

            Usuario user = Usuario.Current();

            Pagina pagina = null;
            if (user != null)
                pagina = (Pagina)new DataBase.Pagina().BuscarHomePaginaRestrito(new Pagina(), (int)cliente.IDCliente, programa.IDPrograma, campanha.IDCampanha, user.IDUsuario);
            else pagina = (Pagina)new DataBase.Pagina().BuscarHomePagina(new Pagina(), (int)cliente.IDCliente, programa.IDPrograma, campanha.IDCampanha);

            return validaPagina(pagina);
        }

        private static Pagina validaPagina(Pagina pagina)
        {
            if (pagina.IDPagina == null) return null;
            pagina.AdicionaCurrent();
            return pagina;
        }

        public static Pagina GetPaginaCliente(string slugPagina)
        {
            var paginaErro = GetPaginaErro(slugPagina);
            if (paginaErro != null) return paginaErro;

            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Usuario user = Usuario.Current();

            Pagina pagina = null;
            if (user != null)
                pagina = (Pagina)new DataBase.Pagina().BuscarPaginaRestrita(new Pagina(), slugPagina, (int)cliente.IDCliente, null, null, user.IDUsuario);
            else pagina = (Pagina)new DataBase.Pagina().BuscarPagina(new Pagina(), slugPagina, (int)cliente.IDCliente, null, null);

            return validaPagina(pagina);
        }

        private static Pagina GetPaginaErro(string slugPagina)
        {
            if (slugPagina == Pagina.ERROR404 || slugPagina == Pagina.ERROR403)
            {
                string area = string.Empty;
                Cliente cliente = Cliente.Current();
                if (cliente != null) area += "-" + cliente.Slug;

                Programa programa = Programa.Current();
                if (programa != null) area += "-" + programa.Slug;

                Campanha campanha = Campanha.Current();
                if (campanha != null) area += "-" + campanha.Slug;

                var pagina = (Pagina)new DataBase.Pagina().BuscarPagina(new Pagina(), slugPagina, 
                    (int)cliente.IDCliente, (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null) );
                if (pagina.IDPagina != null) return pagina;

                return new Pagina(0, new Template(0, slugPagina));
            }
            return null;
        }

        public static Pagina GetPaginaPrograma(string slugPagina)
        {
            var paginaErro = GetPaginaErro(slugPagina);
            if (paginaErro != null) return paginaErro;

            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Programa programa = Programa.Current();
            if (programa == null) return null;

            Usuario user = Usuario.Current();

            Pagina pagina = null;
            if (user != null)
                pagina = (Pagina)new DataBase.Pagina().BuscarPaginaRestrita(new Pagina(), slugPagina, (int)cliente.IDCliente, programa.IDPrograma, null, user.IDUsuario);
            else pagina = (Pagina)new DataBase.Pagina().BuscarPagina(new Pagina(), slugPagina, (int)cliente.IDCliente, programa.IDPrograma, null);

            return validaPagina(pagina);
        }

        public static Pagina GetPaginaCampanha(string slugPagina)
        {
            var paginaErro = GetPaginaErro(slugPagina);
            if (paginaErro != null) return paginaErro;

            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Programa programa = Programa.Current();
            if (programa == null) return null;

            Campanha campanha = Campanha.Current();
            if (campanha == null) return null;

            Usuario user = Usuario.Current();

            Pagina pagina = null;
            if (user != null)
            {
                pagina = (Pagina)new DataBase.Pagina().BuscarPaginaRestrita(new Pagina(), slugPagina,
                    (int)cliente.IDCliente, programa.IDPrograma, campanha.IDCampanha, user.IDUsuario);
            }
            else
            {
                pagina = (Pagina)new DataBase.Pagina().BuscarPagina(new Pagina(), slugPagina,
                    (int)cliente.IDCliente, programa.IDPrograma, campanha.IDCampanha);
            }

            return validaPagina(pagina);
        }

        public static List<Pagina> GetPaginasUsuario()
        {
            var usuario = Usuario.Current();
            var paginas = new List<Pagina>();

            Cliente cliente = Cliente.Current();
            if (cliente == null) return paginas;

            Programa programa = Programa.Current();
            Campanha campanha = Campanha.Current();

            var iPaginas = new DataBase.Pagina().BuscarPaginasUsuario(new Pagina(), (usuario != null ? usuario.IDUsuario : null), cliente.IDCliente,
                (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null));
            iPaginas.ForEach(ip => paginas.Add((Pagina)ip));
            return paginas;            
        }

        public static string Site()
        {
            return ConfigurationManager.AppSettings["Site"].ToString();
        }

        public static string GetAreaCorrenteComPagina()
        {
            return GetAreaCorrente(true);
        }

        public static string GetAreaCorrente(bool comPagina)
        {
            List<string> areas = new List<string>();
            var cliente = Cliente.Current();
            var programa = Programa.Current();
            var campanha = Campanha.Current();

            if (cliente != null) areas.Add(cliente.Slug);
            if (programa != null) areas.Add(programa.Slug);
            if (campanha != null) areas.Add(campanha.Slug);

            if (comPagina)
            {
                var pagina = Pagina.Current();
                if (pagina != null) areas.Add(pagina.Slug);
            }

            return string.Join("/", areas.ToArray());
        }

        public static void Dispose()
        {
            Cookie.Invalidate(KeyPagina());
        }

        public void BuscaAdicionaCurrent()
        {
            if (this.IDPagina == null && string.IsNullOrEmpty(this.Slug)) throw new TradeVisionError("Objeto Pagina não pode ser vazio.");
            this.Get();
            if (this.IDPagina == null) return;
            AdicionaCurrent();
        }

        public void AdicionaCurrent()
        {
            Cookie.Add(KeyPagina(), this.IDPagina.ToString());
        }

        private static string KeyPagina()
        {
            return typeof(Pagina).Name + "Access";
        }

        public static Pagina Current()
        {
            string cookie = Cookie.Get(KeyPagina());
            int idPagina = 0;
            if (!int.TryParse(cookie, out idPagina)) return null;
            if (idPagina == 0) return null;

            var pagina = new Pagina(idPagina);
            pagina.Get();
            return pagina;
        }

        public static LIType GetPaginas(string dsPagina, bool restrito)
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Programa programa = Programa.Current();
            Campanha campanha = Campanha.Current();

            return new DataBase.Pagina().BuscarPaginas(new Pagina(), dsPagina, restrito, cliente.IDCliente,
                (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null));;
        }

        public static int GetQuantidadePaginas(bool restrito)
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return 0;

            Programa programa = Programa.Current();
            Campanha campanha = Campanha.Current();

            return new DataBase.Pagina().GetQuantidadePaginas(new Pagina(), restrito, cliente.IDCliente,
                (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null));
        }

        public LIType GetPaginasPai()
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return new LIType();

            Programa programa = Programa.Current();
            Campanha campanha = Campanha.Current();

            return new DataBase.Pagina().GetPaginasPai(this, this.IDPagina, cliente.IDCliente,
                (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null)); ;
        }

        public string GetPaginasFilhas(int idPaginaPai)
        {
            var paginaPai = new Pagina(idPaginaPai);
            paginaPai.Get();
            if (paginaPai.IDPagina == null) return "";

            Cliente cliente = Cliente.Current();
            if (cliente == null) return "";
            
            Usuario usuario = Usuario.Current();
            if (usuario == null) return "";

            var componente = new Componente();
            componente.Chave = "submenu";
            componente.Get();
            if (componente.Conteudo == null) return "";

            Programa programa = Programa.Current();
            Campanha campanha = Campanha.Current();

            var paginas = new DataBase.Pagina().GetPaginasFilhas(this, idPaginaPai, usuario.IDUsuario, cliente.IDCliente,
                (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null)); ;
            if (paginas.Count < 1) return "";

            Velocity.Init();
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("area", Pagina.GetAreaCorrente(false));
            context.Put("site", Pagina.Site());
            context.Put("paginaPai", paginaPai);
            context.Put("submenu", paginas);
            var pagina = Pagina.Current();
            if (pagina != null) context.Put("pagina_corrente", pagina);
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }
    }
    #endregion
}
