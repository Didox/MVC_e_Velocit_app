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
    public class Log : CData
    {

        public IType BuscaUltimoAcesso(IType iType, int idCliente, int? idPrograma, int? idCampanha, int? idUsuario)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscaUltimoAcesso";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idCliente";
                paran.Value = idCliente;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idUsuario";
                paran1.Value = (int)idUsuario;
                Cmd.Parameters.Add(paran1);

                if (idPrograma != null)
                {
                    DbParameter paran2 = Cmd.CreateParameter();
                    paran2.ParameterName = "@idPrograma";
                    paran2.Value = (int)idPrograma;
                    Cmd.Parameters.Add(paran2);
                }

                if (idCampanha != null)
                {
                    DbParameter paran2 = Cmd.CreateParameter();
                    paran2.ParameterName = "@idCampanha";
                    paran2.Value = (int)idCampanha;
                    Cmd.Parameters.Add(paran2);
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

        public int BuscaQuantidadeAcesso(IType iType, int idCliente, int? idPrograma, int? idCampanha, int? idUsuario)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscaQuantidadeAcesso";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idCliente";
                paran.Value = idCliente;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idUsuario";
                paran1.Value = (int)idUsuario;
                Cmd.Parameters.Add(paran1);

                if (idPrograma != null)
                {
                    DbParameter paran2 = Cmd.CreateParameter();
                    paran2.ParameterName = "@idPrograma";
                    paran2.Value = (int)idPrograma;
                    Cmd.Parameters.Add(paran2);
                }

                if (idCampanha != null)
                {
                    DbParameter paran2 = Cmd.CreateParameter();
                    paran2.ParameterName = "@idCampanha";
                    paran2.Value = (int)idCampanha;
                    Cmd.Parameters.Add(paran2);
                }

                OpenConnection(iType);
                return Convert.ToInt32(Cmd.ExecuteScalar());
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
