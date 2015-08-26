using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Teste


    [Table(Name = "SEG_Teste", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Teste : CType
    {
        #region Construtores
        public Teste()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Teste(int? idTeste)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDTeste = idTeste;
        }

        public Teste(string descricao)
        {
            CarregarConnectionString(Cliente.Current());
            this.Descricao = descricao;
        }

        #endregion

        #region Destrutor
        ~Teste() { Dispose(); }
        #endregion

        #region Attributes
        private Teste TestePai;
        #endregion

        #region Propriedades

        [Property(IsPk = true, IsForeignKey= true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTeste { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dsTeste", Size = 30)]
        [Operations(UseSave = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, Size = 80)]
        [Operations(UseSave = true)]
        public string Oliveira { get; set; }

        [Validate]
        [Property(IsField = true, IsOrderField = true)]
        [Operations(UseSave = true)]
        public int? Assi { get; set; }

        #endregion

        #region Methods


        #endregion
    }
    #endregion
}