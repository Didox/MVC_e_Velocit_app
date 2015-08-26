using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Endereco

    [Table(Name = "NEG_Endereco", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Endereco : CType
    {
        #region Construtores
        public Endereco()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Endereco(int? idEndereco)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDEndereco = idEndereco;
        }

        public Endereco(Pessoa pessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.Pessoa = pessoa;
        }

        #endregion

        #region Destrutor
        ~Endereco() { Dispose(); }
        #endregion

        #region Attributes
        private Estado estado;
        private TipoEndereco tipoEndereco;
        private Pais pais;
        private Pessoa pessoa;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEndereco { get; set; }

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
        [Property(IsField = true, Size = 10, Name = "Cep")]
        [Operations(UseSave = true, UseGet = true)]
        internal int? CepInterno { get; set; }
        public string Cep
        {
            get { return Funcoes.Formatar(CepInterno.ToString(), "00000-000"); }
            set
            {
                try
                {
                    CepInterno = int.Parse(value.Replace("-", ""));
                }
                catch { throw new TradeVisionError("Cep inválido"); }
            }
        }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Bairro { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Cidade { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public string Complemento { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet = true)]
        public int? Numero { get; set; }

        [Validate]
        [ColumnType]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet = true)]
        public int? IDTipoEndereco { get; set; }
        public TipoEndereco TipoEndereco
        {
            get
            {
                if (this.tipoEndereco == null)
                {
                    this.tipoEndereco = new TipoEndereco();
                    this.tipoEndereco.IDTipoEndereco = this.IDTipoEndereco;
                    this.tipoEndereco.Get();
                }

                return this.tipoEndereco;
            }
            set
            {
                this.tipoEndereco = value;
                if (value != null) this.IDTipoEndereco = value.IDTipoEndereco;
            }
        }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEstado { get; set; }

        public Estado Estado
        {
            get
            {
                if (this.estado == null)
                {
                    this.estado = new Estado();
                    this.estado.IDEstado = this.IDEstado;
                }

                if (!this.estado.IsFull)
                {
                    this.estado.Transaction = this.Transaction;
                    this.estado.Get();
                }

                return this.estado;
            }
            set
            {
                this.estado = value;
                if (value != null) this.IDEstado = value.IDEstado;
            }
        }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPais { get; set; }

        public Pais Pais
        {
            get
            {
                if (this.pais == null)
                {
                    this.pais = new Pais();
                    this.pais.IDPais = this.IDPais;
                }

                if (!this.pais.IsFull)
                {
                    this.pais.Transaction = this.Transaction;
                    this.pais.Get();
                }

                return this.pais;
            }
            set
            {
                this.pais = value;
                if (value != null) this.IDPais = value.IDPais;
            }
        }

        #endregion

        #region "Methods"
        
        #endregion
    }

    #endregion
}