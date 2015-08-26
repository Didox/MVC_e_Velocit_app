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
    public class Pessoa : CData
    {
        public bool EstaNesteCargo(IType iType, int idPessoa, int idCargo)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "Sp_PessoaEstaNesteCargo";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCargo";
                paran1.Value = idCargo;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idPessoa";
                paran2.Value = idPessoa;
                Cmd.Parameters.Add(paran2);

                SetObjectToGet(iType);
                return iType.Id != null;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public bool EstaNestaHierarquia(IType iType, int idPessoa, int idHierarquia)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "Sp_PessoaEstaNestaHierarquia";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idHierarquia";
                paran1.Value = idHierarquia;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idPessoa";
                paran2.Value = idPessoa;
                Cmd.Parameters.Add(paran2);

                SetObjectToGet(iType);
                return iType.Id != null;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public bool EFilhoDaPessoaCargo(IType iType, int idPessoaPai, int idPessoa)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "Sp_PessoaEFilhoDaPessoaCargo";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idPessoaPai";
                paran1.Value = idPessoaPai;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idPessoa";
                paran2.Value = idPessoa;
                Cmd.Parameters.Add(paran2);

                SetObjectToGet(iType);
                return iType.Id != null;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public bool EFilhoDaPessoa(IType iType, int idPessoaPai, int idPessoa)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "Sp_PessoaEFilhoDaPessoa";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idPessoaPai";
                paran1.Value = idPessoaPai;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@idPessoa";
                paran2.Value = idPessoa;
                Cmd.Parameters.Add(paran2);

                SetObjectToGet(iType);
                return iType.Id != null;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public IType GetPessoaCampanha(IType iType, int IDUsuario, int idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "Sp_GetPessoaPorCampanha";

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCampanha";
                paran1.Value = idCampanha;
                Cmd.Parameters.Add(paran1);

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idCredencial";
                paran.Value = IDUsuario;
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
