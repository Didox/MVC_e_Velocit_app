using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class Log

    [Table(Name = "PER_Log", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Log : CType
    {
        #region Construtores
        public Log()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Log(int? idLog)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDLog = idLog;
        }

        public Log(string descricao)
        {
            CarregarConnectionString(Cliente.Current());
            this.Descricao = descricao;
        }
        #endregion

        #region Destrutor
        ~Log() { Dispose(); }
        #endregion

        #region Attributes
        private Usuario usuario;
        private Cliente cliente;
        private Programa programa;
        private Campanha campanha;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDLog { get; set; }

        [Validate]
        [Property(IsField = true,IsText = true, Name = "dsLog")]
        [Operations(UseSave = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings = true, Name = "tpLog")]
        [Operations(UseSave = true, UseGet = true)]
        public TipoLog Tipo { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dtLog")]
        [Operations(UseSave = true)]
        public DateTime? Data { get; set; }
        public string DataFormatada { get { return ((DateTime)Data).ToString("dd/MM/yyyy HH:mm"); } }

        [Validate]
        [Property(IsField = true, Name = "idCredencial")]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDUsuario { get; set; }

        public Usuario Usuario
        {
            get
            {
                if (this.usuario == null)
                {
                    this.usuario = new Usuario();
                    this.usuario.IDUsuario = this.IDUsuario;
                }

                if (!this.usuario.IsFull)
                {
                    this.usuario.Transaction = this.Transaction;
                    this.usuario.Get();
                }

                return this.usuario;
            }
            set
            {
                this.usuario = value;
                if (value != null) this.IDUsuario = value.IDUsuario;
            }
        }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCliente { get; set; }

        public Cliente Cliente
        {
            get
            {
                if (this.cliente == null)
                {
                    this.cliente = new Cliente();
                    this.cliente.IDCliente = this.IDCliente;
                }

                if (!this.cliente.IsFull)
                {
                    this.cliente.Transaction = this.Transaction;
                    this.cliente.Get();
                }

                return this.cliente;
            }
            set
            {
                this.cliente = value;
                if (value != null) this.IDCliente = value.IDCliente;
            }
        }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPrograma { get; set; }

        public Programa Programa
        {
            get
            {
                if (this.programa == null)
                {
                    this.programa = new Programa();
                    this.programa.IDPrograma = this.IDPrograma;
                }

                if (!this.programa.IsFull)
                {
                    this.programa.Transaction = this.Transaction;
                    this.programa.Get();
                }

                return this.programa;
            }
            set
            {
                this.programa = value;
                if (value != null) this.IDPrograma = value.IDPrograma;
            }
        }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCampanha { get; set; }

        public Campanha Campanha
        {
            get
            {
                if (this.campanha == null)
                {
                    this.campanha = new Campanha();
                    this.campanha.IDCampanha = this.IDCampanha;
                }

                if (!this.campanha.IsFull)
                {
                    this.campanha.Transaction = this.Transaction;
                    this.campanha.Get();
                }

                return this.campanha;
            }
            set
            {
                this.campanha = value;
                if (value != null) this.IDCampanha = value.IDCampanha;
            }
        }

        #endregion

        #region "Metodos"

        public Log BuscaUltimoAcesso()
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return null;

            Usuario usuario = Usuario.Current();
            if (usuario == null) return null;

            Programa programa = Programa.Current();
            Campanha campanha = Campanha.Current();

            var log = (Log)new DataBase.Log().BuscaUltimoAcesso(this, (int)cliente.IDCliente,
                (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null), (int)usuario.IDUsuario);
            if (log == null)
            {
                log = new Log();
                log.Data = DateTime.Now;
            }
            return log;
        }

        public int BuscaQuantidadeAcesso()
        {
            Cliente cliente = Cliente.Current();
            if (cliente == null) return 0;

            Usuario usuario = Usuario.Current();
            if (usuario == null) return 0;

            Programa programa = Programa.Current();
            Campanha campanha = Campanha.Current();

            return new DataBase.Log().BuscaQuantidadeAcesso(this, (int)cliente.IDCliente,
                (programa != null ? programa.IDPrograma : null), (campanha != null ? campanha.IDCampanha : null), (int)usuario.IDUsuario);
        }

        #endregion
    }

    #endregion

    #region "Tipos de log"

    public enum TipoLog
    {
        Login = 0,
        Alteracao = 1,
        Excusao = 2
    }
 
    #endregion
}
