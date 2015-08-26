using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Telefone


    [Table(Name = "NEG_Telefone", DynamicConnectionString = true, ConnectionString="digitalgest")]
    public class Telefone : CType
    {
        #region Construtores
        public Telefone()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Telefone(int? idTelefone)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDTelefone = idTelefone;
        }

        public Telefone(Pessoa pessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.Pessoa = pessoa;
        }

        #endregion

        #region Destrutor
        ~Telefone() { Dispose(); }
        #endregion

        #region Attributes
        private Pessoa pessoa;
        private TipoTelefone tipoTelefone;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTelefone { get; set; }

        [Validate]
        [Property(IsField = true, DefaultValue="55")]
        [Operations(UseSave = true, UseGet = true)]
        public int? DDI { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet = true)]
        public int? DDD { get; set; }

        [Validate]
        [Property(IsField = true, Name="Numero")]
        [Operations(UseSave = true, UseGet = true)]
        internal int? NumeroInterno { get; set; }

        public string Numero
        {
            get { return Funcoes.Formatar(NumeroInterno.ToString(), "0000-0000"); }
            set
            {
                try
                {
                    NumeroInterno = int.Parse(value.Replace("-", ""));
                }
                catch { throw new TradeVisionError("Número de telefone inválido"); }
            }
        }

        [Validate]
        [ColumnType]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet=true)]
        public int? IDTipoTelefone { get; set; }
        public TipoTelefone TipoTelefone
        {
            get
            {
                if (this.tipoTelefone == null)
                {
                    this.tipoTelefone = new TipoTelefone();
                    this.tipoTelefone.IDTipoTelefone = this.IDTipoTelefone;
                    this.tipoTelefone.Get();
                }

                return this.tipoTelefone;
            }
            set
            {
                this.tipoTelefone = value;
                if (value != null) this.IDTipoTelefone = value.IDTipoTelefone;
            }
        }

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

        #endregion

        #region "Methods"
        
        #endregion
    }

    #endregion
}