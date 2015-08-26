using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class TipoTelefone

    public class TipoTelefone : BasicType
    {
        #region Construtores
        public TipoTelefone() { }
        public TipoTelefone(int? idTipoTelefone)
        {
            this.IDTipoTelefone = idTipoTelefone;
        }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades
        [Property(IsPk = true)]
        public int? IDTipoTelefone { get; set; }
        public string Nome { get; set; }
        #endregion

        #region "Methods"

        public static TipoTelefone Celular()
        {
            return new TipoTelefone(3);
        }

        public static TipoTelefone Residencial()
        {
            return new TipoTelefone(4);
        }

        public static TipoTelefone Fax()
        {
            return new TipoTelefone(6);
        }

        public static TipoTelefone Comercial()
        {
            return new TipoTelefone(5);
        }

        public void Save()
        {
            Tipo.Save(this, new Telefone(), "IDTipoTelefone", "Nome");
        }

        public void Delete()
        {
            Tipo.Delete(this, new Telefone(), "IDTipoTelefone");
        }

        public void Get()
        {
            if (IDTipoTelefone == null) return;
            Tipo.Get(this, new Telefone(), "IDTipoTelefone", "Nome");
        }

        public LIType Find()
        {
            return Tipo.Find(this, new Telefone(), "IDTipoTelefone", "Nome");
        }

        #endregion
    }

    #endregion
}