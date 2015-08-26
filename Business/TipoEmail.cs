using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class TipoEmail

    public class TipoEmail : BasicType
    {
        #region Construtores
        public TipoEmail() { }
        public TipoEmail(int? idTipoEmail)
        {
            this.IDTipoEmail = idTipoEmail;
        }

        #endregion

        #region Destrutor
        ~TipoEmail() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades
        [Property(IsPk = true)]
        public int? IDTipoEmail { get; set; }
        public string Nome { get; set; }
        #endregion

        #region "Methods"
        public static TipoEmail Padrao()
        {
            return new TipoEmail(8);
        }
        
        public void Save()
        {
            Tipo.Save(this, new Email(), "IDTipoEmail", "Nome");
        }

        public void Delete()
        {
            Tipo.Delete(this, new Email(), "IDTipoEmail");
        }

        public void Get()
        {
            if (IDTipoEmail == null) return;
            Tipo.Get(this, new Email(), "IDTipoEmail", "Nome");
        }

        public LIType Find()
        {
            return Tipo.Find(this, new Email(), "IDTipoEmail", "Nome");
        }
        #endregion
    }

    #endregion
}