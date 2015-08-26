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
    public class CredencialPessoa : CData
    {

        public LIType ListaByHierarquia(IType iType, int idPessoa, int idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "Sp_Ambev_Lista_RelacionaPessoa";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idPessoa";
                paran1.Value = idPessoa;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idcampanha";
                paran2.Value = idCampanha;
                Cmd.Parameters.Add(paran2);

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
