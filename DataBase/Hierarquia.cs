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
    public class Hierarquia : CData
    {
        public IType BuscarPorUsuario(IType iType, int idUsuario, int idCampanha)
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

                SetObjectToGet(iType);
                return iType;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public IType GetHierarquiaPorPessoa(IType iType, int idPessoa)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetHierarquiaPorPessoa";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idPessoa";
                paran.Value = idPessoa;
                Cmd.Parameters.Add(paran);

                SetObjectToGet(iType);
                return iType;
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
