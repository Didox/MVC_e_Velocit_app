using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class EstruturaPessoa

    [Table(Name = "SEG_EstruturaPessoa", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class EstruturaPessoa : CType
    {
        #region Construtores
        public EstruturaPessoa()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public EstruturaPessoa(int? idEstruturaPessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDEstruturaPessoa = idEstruturaPessoa;
        }

        public EstruturaPessoa(Pessoa pessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.Pessoa = pessoa;
        }
        #endregion

        #region Destrutor
        ~EstruturaPessoa() { Dispose(); }
        #endregion

        #region Attributes
        private Pessoa pessoa;
        private Estrutura estrutura;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEstruturaPessoa { get; set; }

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
                if (value != null) this.IDPessoa = value.IDPessoa;
            }
        }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEstrutura { get; set; }

        public Estrutura Estrutura
        {
            get
            {
                if (this.estrutura == null)
                {
                    this.estrutura = new Estrutura();
                    this.estrutura.IDEstrutura = this.IDEstrutura;
                }

                if (!this.estrutura.IsFull)
                {
                    this.estrutura.Transaction = this.Transaction;
                    this.estrutura.Get();
                }

                return this.estrutura;
            }
            set
            {
                this.estrutura = value;
                if (value != null) this.IDEstrutura = value.IDEstrutura;
            }
        }   

        #endregion
        
        #region Methods

        #endregion
    }
    #endregion
}
