using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Estado


    [Table(Name = "NEG_Estado", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Estado : CType
    {
        #region Construtores
        public Estado()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Estado(int? idEstado)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDEstado = idEstado;
        }

        #endregion

        #region Destrutor
        ~Estado() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEstado { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet= true)]
        public string Nome { get; set; }

        #endregion

        #region "Methods"
        
        #endregion
    }

    #endregion
}