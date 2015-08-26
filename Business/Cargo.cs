using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Cargo

    [Table(Name = "SEG_Cargo", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Cargo : CType
    {
        #region Construtores
        public Cargo()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Cargo(int? idCargo)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDCargo = idCargo;
        }

        public Cargo(string descricao)
        {
            CarregarConnectionString(Cliente.Current());
            this.Descricao = descricao;
        }

        #endregion

        #region Destrutor
        ~Cargo() { Dispose(); }
        #endregion

        #region Attributes
        private Cargo cargoPai;
        #endregion

        #region Propriedades

        [Property(IsPk = true, IsForeignKey= true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCargo { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dsCargo", Size = 30)]
        [Operations(UseSave = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dsColunaEstrutura", Size = 80)]
        [Operations(UseSave = true)]
        public string ColunaEstrutura { get; set; }

        [Validate]
        [Property(IsField = true, IsOrderField = true)]
        [Operations(UseSave = true)]
        public int? Ordem { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCargoPai { get; set; }

        public Cargo CargoPai
        {
            get
            {
                if (this.cargoPai == null)
                {
                    this.cargoPai = new Cargo();
                    this.cargoPai.IDCargo = this.IDCargoPai;
                }

                if (!this.cargoPai.IsFull)
                {
                    this.cargoPai.Transaction = this.Transaction;
                    this.cargoPai.Get();
                }

                return this.cargoPai;
            }
            set
            {
                this.cargoPai = value;
                if (value != null) this.IDCargo = value.IDCargoPai;
            }
        }

        #endregion

        #region Methods
        
        public LIType GetPrimeiroNoCargo()
        {
            return this.FindBySql("Sp_GetPrimeiroNoCargo");
        }

        #endregion
    }
    #endregion
}