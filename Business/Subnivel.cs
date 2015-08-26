using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class Subnivel

    [Table(Name = "SEG_Subnivel", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Subnivel : CType
    {
        #region Construtores
        public Subnivel()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Subnivel(int? idSubnivel)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDSubnivel = idSubnivel;
        }

        public Subnivel(string descricao)
        {
            CarregarConnectionString(Cliente.Current());
            this.Descricao = descricao;
        }
        #endregion

        #region Destrutor
        ~Subnivel() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDSubnivel { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet = true, UseDelete = true)]
        public int? IDNivel { get; set; }

        [Validate]
        [Property(IsField = true, Size = 150, Name = "dsSubnivel")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        #endregion
        
        #region Methods

        #endregion
    }
    #endregion
}
