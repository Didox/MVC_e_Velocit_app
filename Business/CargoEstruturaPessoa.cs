using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class CargoEstruturaPessoa

    [Table(Name = "SEG_CargoEstruturaPessoa", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class CargoEstruturaPessoa : CType
    {
        #region Construtores
        public CargoEstruturaPessoa()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public CargoEstruturaPessoa(int? idCargoEstruturaPessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDCargoEstruturaPessoa = idCargoEstruturaPessoa;
        }

        public CargoEstruturaPessoa(Pessoa pessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.Pessoa = pessoa;
        }
        #endregion

        #region Destrutor
        ~CargoEstruturaPessoa() { Dispose(); }
        #endregion

        #region Attributes
        private Pessoa pessoa;
        private CargoEstrutura cargoEstrutura;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCargoEstruturaPessoa { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
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
        [Property(IsField = true, IsForeignKey=true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCargoEstrutura { get; set; }

        public CargoEstrutura CargoEstrutura
        {
            get
            {
                if (this.cargoEstrutura == null)
                {
                    this.cargoEstrutura = new CargoEstrutura();
                    this.cargoEstrutura.IDCargoEstrutura = this.IDCargoEstrutura;
                }

                if (!this.cargoEstrutura.IsFull)
                {
                    this.cargoEstrutura.Transaction = this.Transaction;
                    this.cargoEstrutura.Get();
                }

                return this.cargoEstrutura;
            }
            set
            {
                this.cargoEstrutura = value;
                if (value != null) this.IDCargoEstrutura = value.IDCargoEstrutura;
            }
        }   

        #endregion
        
        #region Methods

        #endregion
    }
    #endregion
}
