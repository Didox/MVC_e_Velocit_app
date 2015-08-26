using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class RelacionaPagina

    [Table(Name = "EST_RelacionaPagina", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class RelacionaPagina : CType
    {
        #region Construtores
        public RelacionaPagina()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public RelacionaPagina(int? idRelacionaPagina)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDRelacionaPagina = idRelacionaPagina;
        }

        #endregion

        #region Destrutor
        ~RelacionaPagina() { Dispose(); }
        #endregion

        #region Attributes
        private Pagina pagina;
        private Cliente cliente;
        private Programa programa;
        private Campanha campanha;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDRelacionaPagina { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPagina { get; set; }

        public Pagina Pagina
        {
            get
            {
                if (this.pagina == null)
                {
                    this.pagina = new Pagina();
                    this.pagina.IDPagina = this.IDPagina;
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
                if (value != null) this.IDPagina = value.IDPagina;
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

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public bool? Restrito { get; set; }

        [Validate]
        [Property(IsField = true, Name = "PaginaInterna")]
        [Operations(UseSave = true, UseGet = true)]
        public bool? Interna { get; set; }

        [Validate]
        [Property(IsField = true, IsOrderField = true)]
        [Operations(UseSave = true)]
        public int Ordem { get; set; }

        #endregion

        #region Methods
        #endregion
    }
    #endregion
}
