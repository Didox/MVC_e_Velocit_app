using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Hierarquia


    [Table(Name = "SEG_Hierarquia", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Hierarquia : CType
    {
        #region Construtores
        public Hierarquia()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Hierarquia(int? idHierarquia)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDHierarquia = idHierarquia;
        }

        public Hierarquia(Hierarquia hierarquiaPai)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDHierarquiaPai = hierarquiaPai.IDHierarquia;
        }  

        public Hierarquia(string descricao)
        {
            CarregarConnectionString(Cliente.Current());
            this.Descricao = descricao;
        }

        #endregion

        #region Destrutor
        ~Hierarquia() { Dispose(); }
        #endregion

        #region Attributes
        private Hierarquia hierarquiaPai;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDHierarquia { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dsHierarquia", Size = 30)]
        [Operations(UseSave = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dsColunaEstrutura", Size = 80)]
        [Operations(UseSave = true)]
        public string ColunaEstrutura { get; set; }

        [Validate]
        [Property(IsField = true, IsOrderField=true)]
        [Operations(UseSave = true)]
        public int? Ordem { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDHierarquiaPai { get; set; }

        public Hierarquia HierarquiaPai
        {
            get
            {
                if (this.hierarquiaPai == null)
                {
                    this.hierarquiaPai = new Hierarquia();
                    this.hierarquiaPai.IDHierarquia = this.IDHierarquiaPai;
                }

                if (!this.hierarquiaPai.IsFull)
                {
                    this.hierarquiaPai.Transaction = this.Transaction;
                    this.hierarquiaPai.Get();
                }

                return this.hierarquiaPai;
            }
            set
            {
                this.hierarquiaPai = value;
                if (value != null) this.IDHierarquia = value.IDHierarquiaPai;
            }
        }

        #endregion

        public LIType GetPrimeiroNoHierarquia()
        {
            return this.FindBySql("Sp_GetPrimeiroNoHierarquia");
        }

    }
    #endregion
}