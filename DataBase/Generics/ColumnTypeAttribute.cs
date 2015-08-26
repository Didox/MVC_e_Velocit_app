using System;

namespace Didox.DataBase.Generics
{
    /// <summary>
    /// Atributo usado para avisar que o campo � requerido.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnTypeAttribute : Attribute { }
}
