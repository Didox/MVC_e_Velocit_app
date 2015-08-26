using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class CampoOpcao

    [Table(Name = "SYS_CampoOpcao")]
    public class CampoOpcao : CType
    {
        #region Construtores
        public CampoOpcao() { }

        public CampoOpcao(int? idCampoOpcao)
        {
            this.IDCampoOpcao = idCampoOpcao;
        }

        public CampoOpcao(string opcao, int idCampo)
        {
            this.Opcao = opcao;
            this.IDCampo = idCampo;
        }

        #endregion

        #region Destrutor
        ~CampoOpcao() { Dispose(); }
        #endregion

        #region Attributes
        private Campo campo;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCampoOpcao { get; set; }

        [Validate]
        [Property(IsField = true, IsText=true)]
        [Operations(UseSave = true)]
        public string Opcao { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
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

        #endregion
    }
    #endregion
}
