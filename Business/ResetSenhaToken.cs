using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Web.Security;

namespace Didox.Business
{
    #region Class ResetSenhaToken

    [Table(Name = "EST_ResetSenhaToken")]
    public class ResetSenhaToken : CType
    {
        #region Construtores
        public ResetSenhaToken() { }

        public ResetSenhaToken(int? idResetSenhaToken)
        {
            this.IDResetSenhaToken = idResetSenhaToken;
        }

        #endregion

        #region Destrutor
        ~ResetSenhaToken() { Dispose(); }
        #endregion

        #region Attributes
        private Usuario usuario;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDResetSenhaToken { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Token { get; set; }

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
