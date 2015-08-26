using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class TipoEndereco

    public class TipoEndereco : BasicType
    {
        #region Construtores
        public TipoEndereco() { }

        public TipoEndereco(int? idTipoEndereco)
        {
            this.IDTipoEndereco = idTipoEndereco;
        }

        #endregion

        #region Destrutor
        ~TipoEndereco() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades
        [Property(IsPk = true)]
        public int? IDTipoEndereco { get; set; }
        public string Nome { get; set; }
        #endregion

        #region "Methods"

        public static TipoEndereco Residencial()
        {
            return new TipoEndereco(1);
        }

        public static TipoEndereco Comercial()
        {
            return new TipoEndereco(2);
        }

        public void Save()
        {
            Tipo.Save(this, new Endereco(), "IDTipoEndereco", "Nome");
        }

        public void Delete()
        {
            Tipo.Delete(this, new Endereco(), "IDTipoEndereco");
        }

        public void Get()
        {
            if (IDTipoEndereco == null) return;
            Tipo.Get(this, new Endereco(), "IDTipoEndereco", "Nome");
        }

        public LIType Find()
        {
            return Tipo.Find(this, new Endereco(), "IDTipoEndereco", "Nome");
        }
        
        #endregion
    }

    #endregion
}