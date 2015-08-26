using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class TipoDocumento

    public class TipoDocumento : BasicType
    {
        #region Construtores
        public TipoDocumento() { }
        public TipoDocumento(int? idTipoDocumento)
        {
            this.IDTipoDocumento = idTipoDocumento;
        }

        #endregion

        #region Destrutor
        ~TipoDocumento() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades
        [Property(IsPk = true)]
        public int? IDTipoDocumento { get; set; }
        public string Nome { get; set; }
        #endregion

        #region "Methods"

        public static TipoDocumento CPF()
        {
            return new TipoDocumento(13);
        }

        public static TipoDocumento RG()
        {
            return new TipoDocumento(14);
        }

        public static TipoDocumento CNPJ()
        {
            return new TipoDocumento(15);
        }

        public static TipoDocumento TVI()
        {
            return new TipoDocumento(16);
        }

        public void Save()
        {
            Tipo.Save(this, new Documento(), "IDTipoDocumento", "Nome");
        }

        public void Delete()
        {
            Tipo.Delete(this, new Documento(), "IDTipoDocumento");
        }

        public void Get()
        {
            if (IDTipoDocumento == null) return;
            Tipo.Get(this, new Documento(), "IDTipoDocumento", "Nome");
        }

        public LIType Find()
        {
            return Tipo.Find(this, new Documento(), "IDTipoDocumento", "Nome");
        }

        #endregion 
    }

    #endregion
}