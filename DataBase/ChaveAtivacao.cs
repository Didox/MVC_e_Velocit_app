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
    public class ChaveAtivacao : CData
    {

        public IType Validate(IType iType, int idCampanha, string chave, string senha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCampanha";
                paran1.Value = idCampanha;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@chave";
                paran2.Value = chave;
                Cmd.Parameters.Add(paran2);

                if (senha != null)
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = "Sp_SYS_ValidateChaveAtivacao";

                    DbParameter paran3 = Cmd.CreateParameter();
                    paran3.ParameterName = "@senha";
                    paran3.Value = senha;
                    Cmd.Parameters.Add(paran3); 
                }
                else
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.CommandText = "Sp_SYS_ValidateChaveAtivacaoSemSenha";
                }

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
