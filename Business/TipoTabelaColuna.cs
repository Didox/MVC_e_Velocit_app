using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class TipoTabelaColuna

    [Table(Name = "SYS_TipoTabelaColuna")]
    public class TipoTabelaColuna : CType
    {
        #region Construtores
        public TipoTabelaColuna() { }

        public TipoTabelaColuna(IType iType)
        {
            this.Tabela = CType.GetTableName(iType);
            this.Coluna = CType.GetPropertyGenericTipo(iType);
        }

        public TipoTabelaColuna(int? idTipoTabelaColuna)
        {
            this.IDTipoTabelaColuna = idTipoTabelaColuna;
        }

        #endregion

        #region Destrutor
        ~TipoTabelaColuna() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTipoTabelaColuna { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings=true)]
        [Operations(UseSave = true, UseGet=true)]
        public string Tabela { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Coluna { get; set; }

        #endregion

        #region "Methods"
        
        #endregion
    }

    #endregion
}