using System;

namespace Didox.DataBase.Generics
{
    /// <summary>
    /// Atributo para opera��es CRUD, [Operations(useSave, useDelete, useGet)]
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OperationsAttribute : Attribute
    {
        #region Properties
        
        public bool UseSave { get; set; }
        public bool UseDelete { get; set; }
        public bool UseGet { get; set; }

        #endregion

        #region Constructor

        public OperationsAttribute() { }
        #endregion
    }
}
