using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Pais


    [Table(Name = "NEG_Pais", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Pais : CType
    {
        #region Construtores
        public Pais()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Pais(int? idPais)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDPais = idPais;
        }

        #endregion

        #region Destrutor
        ~Pais() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPais { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Nome { get; set; }

        #endregion

        #region "Methods"
        
        #endregion
    }

    #endregion
}