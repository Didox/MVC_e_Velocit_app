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
    public class Estrutura : CData
    {
        public void Save(IType iType, int idPessoa, int idHierarquia)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SaveEstrutura";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idHierarquia";
                paran1.Value = idHierarquia;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idPessoa";
                paran2.Value = idPessoa;
                Cmd.Parameters.Add(paran2);

                OpenConnectionTrans(iType);
                Cmd.ExecuteNonQuery();                
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }


        public void SavePai(IType iType, int idPessoa, int idPessoaPai)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SaveEstruturaPessoaPai";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idPessoaPai";
                paran1.Value = idPessoaPai;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idPessoa";
                paran2.Value = idPessoa;
                Cmd.Parameters.Add(paran2);

                OpenConnectionTrans(iType);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public void DeletePai(IType iType, int idPessoa, int idPessoaPai)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_DeleteEstruturaPessoaPai";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idPessoaPai";
                paran1.Value = idPessoaPai;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idPessoa";
                paran2.Value = idPessoa;
                Cmd.Parameters.Add(paran2);

                OpenConnectionTrans(iType);
                Cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public void DeleteDaHierarquia(IType iType, int idPessoa, int idHierarquia)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_DeletePessoaDaHierarquia";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idPessoa";
                paran1.Value = idPessoa;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idHierarquia";
                paran2.Value = idHierarquia;
                Cmd.Parameters.Add(paran2);

                OpenConnectionTrans(iType);
                Cmd.ExecuteNonQuery();
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
