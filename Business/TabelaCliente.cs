using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class TabelaCliente

    [Table(Name = "SYS_TabelaCliente")]
    public class TabelaCliente : CType
    {
        #region Construtores
        public TabelaCliente() { }

        public TabelaCliente(int? idTabelaCliente)
        {
            this.IDTabelaCliente = idTabelaCliente;
        }

        public TabelaCliente(Cliente cliente)
        {
            this.Cliente = cliente;
        }

        #endregion

        #region Destrutor
        ~TabelaCliente() { Dispose(); }
        #endregion

        #region Attributes
        private Tabela tabela;
        private Cliente cliente;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTabelaCliente { get; set; }

        [Validate]
        [Property(IsField = true, Name = "IDTipoPessoa")]
        [Operations(UseSave = true, UseGet = true)]
        public int? IDTipoPessoa { get; set; }

        public TipoPessoa TipoPessoa
        {
            get { return (TipoPessoa)Enum.Parse(typeof(TipoPessoa), IDTipoPessoa.ToString()); }
            set { IDTipoPessoa = (int)value; }
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

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTabela { get; set; }

        public Tabela Tabela
        {
            get
            {
                if (this.tabela == null)
                {
                    this.tabela = new Tabela();
                    this.tabela.IDTabela = this.IDTabela;
                }

                if (!this.tabela.IsFull)
                {
                    this.tabela.Transaction = this.Transaction;
                    this.tabela.Get();
                }

                return this.tabela;
            }
            set
            {
                this.tabela = value;
                if (value != null) this.IDTabela = value.IDTabela;
            }
        }

        #endregion
        
        #region Methods
        #endregion
    }
    #endregion
}
