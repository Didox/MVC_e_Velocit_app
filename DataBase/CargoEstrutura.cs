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
    public class CargoEstrutura : CData
    {
        public void Save(IType iType, int idPessoa, int idCargo)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SaveCargoEstrutura";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCargo";
                paran1.Value = idCargo;
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
                Cmd.CommandText = "sp_SaveCargoEstruturaPessoaPai";

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
                Cmd.CommandText = "sp_DeleteCargoEstruturaPessoaPai";

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

        public void DeleteDaCargo(IType iType, int idPessoa, int idCargo)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_DeletePessoaDoCargo";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idPessoa";
                paran1.Value = idPessoa;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idCargo";
                paran2.Value = idCargo;
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
