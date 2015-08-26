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
    public class Cargo : CData
    {
        public IType GetCargoPorPessoa(IType iType, int idPessoa)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_GetCargoPorPessoa";

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

        public LIType GetPrimeiroNoCargo(IType iType)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "Sp_GetPrimeiroNoCargo";
                return MakeListToGet(iType);
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
