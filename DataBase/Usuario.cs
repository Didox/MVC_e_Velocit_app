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
    public class Usuario : CData
    {

        public IType BuscarHomePaginaRestrito(IType iType, int idUsuario, int idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BUSCA_HIERARQUIA_POR_CREDENCIAL";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idcredencial";
                paran.Value = idUsuario;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idcampanha";
                paran1.Value = idCampanha;
                Cmd.Parameters.Add(paran1);

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
