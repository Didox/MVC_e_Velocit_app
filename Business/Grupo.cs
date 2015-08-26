using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class Grupo

    [Table(Name = "PER_Grupo", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Grupo : CType
    {
        #region Construtores
        public Grupo()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Grupo(int? idGrupo)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDGrupo = idGrupo;
        }

        public Grupo(string descricao)
        {
            CarregarConnectionString(Cliente.Current());
            this.Descricao = descricao;
        }
        #endregion

        #region Destrutor
        ~Grupo() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDGrupo { get; set; }

        [Validate]
        [Property(IsField = true, Size = 150, Name = "dsGrupo")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        #endregion
        
        #region Methods

        #endregion
    }
    #endregion
}
