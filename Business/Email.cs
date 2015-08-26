using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Email


    [Table(Name = "NEG_Email", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Email : CType
    {
        #region Construtores
        public Email()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Email(int? idEmail)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDEmail = idEmail;
        }

        public Email(Pessoa pessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.Pessoa = pessoa;
        }

        #endregion

        #region Destrutor
        ~Email() { Dispose(); }
        #endregion

        #region Attributes
        private Pessoa pessoa;
        private TipoEmail tipoEmail;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEmail { get; set; }

        [Validate]
        [Property(IsField = true, Name = "Endereco", DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string EnderecoEmail { get; set; }

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
        [ColumnType]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public int? IDTipoEmail { get; set; }
        public TipoEmail TipoEmail
        {
            get
            {
                if (this.tipoEmail == null)
                {
                    this.tipoEmail = new TipoEmail();
                    this.tipoEmail.IDTipoEmail = this.IDTipoEmail;
                    this.tipoEmail.Get();
                }

                return this.tipoEmail;
            }
            set
            {
                this.tipoEmail = value;
                if (value != null) this.IDTipoEmail = value.IDTipoEmail;
            }
        }

        #endregion

        #region "Methods"

        public override void Save()
        {
            if (!Funcoes.ValidateEmail(this.EnderecoEmail))
                throw new TradeVisionValidationError("Email inválido");
            base.Save();
        }
        
        #endregion
    }

    #endregion
}