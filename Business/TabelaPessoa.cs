using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class TabelaPessoa

    [Table(DynamicConectionString = true, ConectionString = "digitalgest")]
    public class TabelaPessoa : CType
    {
        #region Construtores
        public TabelaPessoa()
        {
            CarregarConectionString(Cliente.Current());
        }

        public TabelaPessoa(int? idTabelaPessoa)
        {
            CarregarConectionString(Cliente.Current());
            this.IDTabelaPessoa = idTabelaPessoa;
        }

        #endregion

        #region Destrutor
        ~TabelaPessoa() { Dispose(); }
        #endregion

        #region Attributes
        private Tabela tabela;
        private Pessoa pessoa;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTabelaPessoa { get; set; }

        [Validate]
        [Property(IsField = true, Size = 1, Name = "tpPessoa")]
        [Operations(UseSave = true)]
        public string TipoPessoa { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoa { get; set; }

        public Pessoa Pessoa
        {
            get
            {
                if (this.pessoa == null)
                {
                    this.pessoa = new Pessoa();
                    this.pessoa.IDPessoa = this.IDPessoa;
                }

                if (!this.pessoa.IsFull)
                {
                    this.pessoa.Transaction = this.Transaction;
                    this.pessoa.Get();
                }

                return this.pessoa;
            }
            set
            {
                this.pessoa = value;
                if (value != null)
                {
                    this.IDPessoa = value.IDPessoa;
                    this.TipoPessoa = value.Tipo;
                }
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
