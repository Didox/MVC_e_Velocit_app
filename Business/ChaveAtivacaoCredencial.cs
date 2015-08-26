using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class ChaveAtivacaoCredencial

    [Table(Name = "SEG_ChaveAtivacaoCredencial")]
    public class ChaveAtivacaoCredencial : CType
    {
        #region Construtores
        public ChaveAtivacaoCredencial(){}

        public ChaveAtivacaoCredencial(int? idChaveAtivacaoCredencial)
        {
            this.IDChaveAtivacaoCredencial = idChaveAtivacaoCredencial;
        }

        #endregion

        #region Destrutor
        ~ChaveAtivacaoCredencial() { Dispose(); }
        #endregion

        #region Attributes
        private Usuario usuario;
        private ChaveAtivacao chaveAtivacao;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDChaveAtivacaoCredencial { get; set; }

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
        [Property(IsField = true, Name = "idCredencial", IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDUsuario { get; set; }

        public Usuario Usuario
        {
            get
            {
                if (this.usuario == null)
                {
                    this.usuario = new Usuario();
                    this.usuario.IDUsuario = this.IDUsuario;
                }

                if (!this.usuario.IsFull)
                {
                    this.usuario.Transaction = this.Transaction;
                    this.usuario.Get();
                }

                return this.usuario;
            }
            set
            {
                this.usuario = value;
                if (value != null) this.IDUsuario = value.IDUsuario;
            }
        }

        #endregion
        
        #region Methods

        #endregion
    }
    #endregion
}
