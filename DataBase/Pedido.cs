using System;
using Didox.DataBase.Generics;
using System.Collections.Generic;
using Didox.DataBase.Generics.DataManager;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Data.SqlClient;

namespace Didox.DataBase
{
    public class Pagina : CData
    {

        public IType BuscarHomePagina(IType iType, int idCliente)
        {
            Cn = DataBaseGeneric.CreateConnection(BaseType, iType.InstanceConectionString);
            Cn.Open();

            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarHomePagina";

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idCliente";
                paran2.Value = idCliente;
                Cmd.Parameters.Add(paran2);

                OpenConnection(iType);
                DbDataReader dr = Cmd.ExecuteReader();
                if (dr.Read())
                    return SetObject(dr, (IType)Activator.CreateInstance(iType.GetType()));
                return null;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }
    }
}
