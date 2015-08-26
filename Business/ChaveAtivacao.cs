using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class ChaveAtivacao

    [Table(Name = "SEG_ChaveAtivacao")]
    public class ChaveAtivacao : CType
    {
        #region Construtores
        public ChaveAtivacao(){}

        public ChaveAtivacao(int? idChaveAtivacao)
        {
            this.IDChaveAtivacao = idChaveAtivacao;
        }

        #endregion

        #region Destrutor
        ~ChaveAtivacao() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true, Name="idChave")]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDChaveAtivacao { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings=true, Size=10)]
        [Operations(UseSave = true, UseGet = true)]
        public string Chave { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings = true, Size = 10)]
        [Operations(UseSave = true, UseGet = true)]
        public string Senha { get; set; }

        [Property(IsField = true, Name = "DtAtivacao")]
        [Operations(UseSave = true, UseGet = true)]
        public DateTime? DataAtivacao { get; set; }

        #endregion
        
        #region Methods

        #endregion

        public static bool Validate(string chave, string senha)
        {
            var campanha = Campanha.Current();
            if (campanha == null) return false;
            var itype = new Didox.DataBase.ChaveAtivacao().Validate(new ChaveAtivacao(), (int)campanha.IDCampanha, chave, senha);
            return itype.Id != null;
        }
    }
    #endregion
}
