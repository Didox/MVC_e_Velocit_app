using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class GrupoPagina

    [Table(Name = "PER_GrupoPagina", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class GrupoPagina : CType
    {
        #region Construtores
        public GrupoPagina()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public GrupoPagina(int? idGrupoPagina)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDGrupoPagina = idGrupoPagina;
        }

        #endregion

        #region Destrutor
        ~GrupoPagina() { Dispose(); }
        #endregion

        #region Attributes
        private Pagina pagina;
        private Grupo grupo;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDGrupoPagina { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
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
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDGrupo { get; set; }

        public Grupo Grupo
        {
            get
            {
                if (this.grupo == null)
                {
                    this.grupo = new Grupo();
                    this.grupo.IDGrupo = this.IDGrupo;
                }

                if (!this.grupo.IsFull)
                {
                    this.grupo.Transaction = this.Transaction;
                    this.grupo.Get();
                }

                return this.grupo;
            }
            set
            {
                this.grupo = value;
                if (value != null) this.IDGrupo = value.IDGrupo;
            }
        }

        #endregion
        
        #region Methods

        public List<Pagina> GetPaginas()
        {
            var paginas = new List<Pagina>();
            var igu = this.Find();
            igu.ForEach(i => paginas.Add(((GrupoPagina)i).Pagina));
            return paginas;
        }

        #endregion
    }
    #endregion
}
