using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Documento

    [Table(Name = "CAD_Documento", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Documento : CType
    {
        #region Construtores
        public Documento()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Documento(int? idDocumento)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDDocumento = idDocumento;
        }

        #endregion

        #region Destrutor
        ~Documento() { Dispose(); }
        #endregion

        #region Attributes
        private Pessoa pessoa;
        private TipoDocumento tipoDocumento;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDDocumento { get; set; }

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
        [Property(IsField = true, IsOrderField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTipoDocumento { get; set; }
        public TipoDocumento TipoDocumento
        {
            get
            {
                if (this.tipoDocumento == null)
                {
                    this.tipoDocumento = new TipoDocumento();
                    this.tipoDocumento.IDTipoDocumento = this.IDTipoDocumento;
                    this.tipoDocumento.Get();
                }

                return this.tipoDocumento;
            }
            set
            {
                this.tipoDocumento = value;
                if (value != null) this.IDTipoDocumento = value.IDTipoDocumento;
            }
        }

        [Validate]
        [Property(IsField = true, Name = "Documento", Size = 40)]
        [Operations(UseSave = true)]
        public string DescDocumento { get; set; }

        [Property(IsField = true, Size = 20)]
        [Operations(UseSave = true)]
        public string DocNumero { get; set; }

        [Property(IsField = true, Size = 20)]
        [Operations(UseSave = true)]
        public string DocComplemento { get; set; }

        [Property(IsField = true, Size = 5)]
        [Operations(UseSave = true)]
        public string DocDV { get; set; }

        #endregion

        #region "Methods"
        public override void Save()
        {
            if (this.IDTipoDocumento == TipoDocumento.CPF().IDTipoDocumento)
            {
                this.DocNumero = DescDocumento.Substring(0, DescDocumento.Length - 2);
                this.DocDV = DescDocumento.Substring(DescDocumento.Length - 2, 2);
                this.DocComplemento = string.Empty;
            }
            else if (this.IDTipoDocumento == TipoDocumento.RG().IDTipoDocumento)
            {
                this.DocNumero = string.Empty;
                this.DocDV = string.Empty;
                this.DocComplemento = string.Empty;
            }
            else if (this.IDTipoDocumento == TipoDocumento.CNPJ().IDTipoDocumento)
            {
                this.DocNumero = DescDocumento.Substring(0, DescDocumento.Length - 6);
                this.DocComplemento = DescDocumento.Substring(DescDocumento.Length - 6, 4);
                this.DocDV = DescDocumento.Substring(DescDocumento.Length - 2, 2);
            }
            else if (this.IDTipoDocumento == TipoDocumento.TVI().IDTipoDocumento)
            {
                this.DocNumero = DescDocumento.Substring(0, 4);
                this.DocComplemento = DescDocumento.Substring(4, 7);
                this.DocDV = DescDocumento.Substring(DescDocumento.Length - 1, 1);
            }
            
            base.Save();
        }
        
        #endregion
    }

    #endregion
}