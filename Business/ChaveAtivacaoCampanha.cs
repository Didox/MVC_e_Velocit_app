using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class ChaveAtivacaoCampanha

    [Table(Name = "SEG_ChaveAtivacaoCampanha")]
    public class ChaveAtivacaoCampanha : CType
    {
        #region Construtores
        public ChaveAtivacaoCampanha(){}

        public ChaveAtivacaoCampanha(int? idChaveAtivacaoCampanha)
        {
            this.IDChaveAtivacaoCampanha = idChaveAtivacaoCampanha;
        }

        public ChaveAtivacaoCampanha(Campanha campanha)
        {
            this.campanha = campanha;
        }

        #endregion

        #region Destrutor
        ~ChaveAtivacaoCampanha() { Dispose(); }
        #endregion

        #region Attributes
        private Campanha campanha;
        private ChaveAtivacao chaveAtivacao;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDChaveAtivacaoCampanha { get; set; }

        [Validate]
        [Property(IsField = true, Name = "idChave", IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDChaveAtivacao { get; set; }

        public ChaveAtivacao ChaveAtivacao
        {
            get
            {
                if (this.chaveAtivacao == null)
                {
                    this.chaveAtivacao = new ChaveAtivacao();
                    this.chaveAtivacao.IDChaveAtivacao = this.IDChaveAtivacao;
                }

                if (!this.chaveAtivacao.IsFull)
                {
                    this.chaveAtivacao.Transaction = this.Transaction;
                    this.chaveAtivacao.Get();
                }

                return this.chaveAtivacao;
            }
            set
            {
                this.chaveAtivacao = value;
                if (value != null) this.IDChaveAtivacao = value.IDChaveAtivacao;
            }
        }
        

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCampanha { get; set; }

        public Campanha Campanha
        {
            get
            {
                if (this.campanha == null)
                {
                    this.campanha = new Campanha();
                    this.campanha.IDCampanha = this.IDCampanha;
                }

                if (!this.campanha.IsFull)
                {
                    this.campanha.Transaction = this.Transaction;
                    this.campanha.Get();
                }

                return this.campanha;
            }
            set
            {
                this.campanha = value;
                if (value != null) this.IDCampanha = value.IDCampanha;
            }
        }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public bool PossuiSenha { get; set; }

        #endregion
        
        #region Methods

        #endregion
    }
    #endregion
}
