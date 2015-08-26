using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class CampoValor

    [Table(Name = "SYS_CampoValor", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class CampoValor : CType
    {
        #region Construtores
        public CampoValor()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public CampoValor(int? idCampoValor)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDCampoValor = idCampoValor;
        }

        #endregion

        #region Destrutor
        ~CampoValor() { Dispose(); }
        #endregion

        #region Attributes
        private Campo campo;
        private Pessoa pessoa;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCampoValor { get; set; }

        [Validate]
        [Property(IsField = true, IsText=true)]
        [Operations(UseSave = true)]
        public string Valor { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey=true)]
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
        public int? IDCampo { get; set; }

        public Campo Campo
        {
            get
            {
                if (this.campo == null)
                {
                    this.campo = new Campo();
                    this.campo.IDCampo = this.IDCampo;
                }

                if (!this.campo.IsFull)
                {
                    this.campo.Transaction = this.Transaction;
                    this.campo.Get();
                }

                return this.campo;
            }
            set
            {
                this.campo = value;
                if (value != null) this.IDCampo = value.IDCampo;
            }
        }

        #endregion
        
        #region Methods

        public object ValorConvertido()
        {
            switch (this.campo.Tipo)
            {
                case TipoCampo.Inteiro:
                    return int.Parse(this.Valor);
                case TipoCampo.Boleano:
                    return Convert.ToBoolean(this.Valor);
                case TipoCampo.Moeda:
                    return Convert.ToDouble(this.Valor);
                case TipoCampo.Data:
                    return Convert.ToDateTime(this.Valor);
                default:
                    return this.Valor;
            }
        }

        public void SetValor(string value)
        {
            this.Valor = value;
        }

        public string GetValor()
        {
            return this.Valor;
        }

        public bool ContainsValor(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return false;
            return this.Valor.IndexOf(valor) != -1;
        }

        public override void Save()
        {
            if (string.IsNullOrEmpty(this.Valor))
            {
                if (!string.IsNullOrEmpty(campo.ValorDefault))
                    campo.Valor(this.Pessoa).SetValor(campo.ValorDefault);
                else if (!this.Campo.PermiteNulo) throw new TradeVisionValidationError("O campo " + this.Campo.Label + " é obrigatório.");
            }
            else
            {
                if (this.Campo.Tipo == TipoCampo.Data)
                {
                    DateTime data;
                    if (!DateTime.TryParse(this.Valor, out data))
                        throw new TradeVisionValidationError("O campo " + this.Campo.Label + " é obrigatório uma data valida.");
                }

                if (this.Campo.Tipo == TipoCampo.Boleano)
                {
                    Boolean boolean;
                    if (!Boolean.TryParse(this.Valor, out boolean))
                        throw new TradeVisionValidationError("O campo " + this.Campo.Label + " precisa ser verdadeiro ou falso.");
                }

                if (this.Campo.Tipo == TipoCampo.Inteiro)
                {
                    int inteiro;
                    if (!int.TryParse(this.Valor, out inteiro))
                        throw new TradeVisionValidationError("O campo " + this.Campo.Label + " precisa ser um valor inteiro.");
                }

                if (this.Campo.Tipo == TipoCampo.Moeda)
                {
                    double moeda;
                    if (!double.TryParse(this.Valor, out moeda))
                        throw new TradeVisionValidationError("O campo " + this.Campo.Label + " precisa ser um valor do tipo moeda.");
                }
            }

            base.Save();
        }

        #endregion
    }
    #endregion
}
