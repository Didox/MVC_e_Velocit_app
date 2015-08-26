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

        public IType BuscarHomePaginaRestrito(IType iType, int idCliente, int? idPrograma, int? idCampanha, int? idUsuario)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarHomePaginaRestrito";

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

        public IType BuscarPaginaRestrita(IType iType, string slugPagina, int idCliente, int? idPrograma, int? idCampanha, int? idUsuario)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarPaginaRestrito";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idCliente";
                paran.Value = idCliente;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idUsuario";
                paran1.Value = (int)idUsuario;
                Cmd.Parameters.Add(paran1);

                DbParameter paran2 = Cmd.CreateParameter();
                paran2.ParameterName = "@slugPagina";
                paran2.Value = slugPagina;
                Cmd.Parameters.Add(paran2);

                if (idPrograma != null)
                {
                    DbParameter paran3 = Cmd.CreateParameter();
                    paran3.ParameterName = "@idPrograma";
                    paran3.Value = (int)idPrograma;
                    Cmd.Parameters.Add(paran3);
                }

                if (idCampanha != null)
                {
                    DbParameter paran3 = Cmd.CreateParameter();
                    paran3.ParameterName = "@idCampanha";
                    paran3.Value = (int)idCampanha;
                    Cmd.Parameters.Add(paran3);
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

        public IType BuscarHomePagina(IType iType, int idCliente, int? idPrograma, int? idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarHomePagina";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idCliente";
                paran.Value = idCliente;
                Cmd.Parameters.Add(paran);

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

        public IType BuscarPagina(IType iType, string slugPagina, int idCliente, int? idPrograma, int? idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarPagina";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idCliente";
                paran.Value = idCliente;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@slugPagina";
                paran1.Value = slugPagina;
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

        public LIType BuscarPaginasUsuario(IType iType, int? idUsuario, int? idCliente, int? idPrograma, int? idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarPaginasUsuario";

                if (idUsuario != null)
                {
                    DbParameter paran = Cmd.CreateParameter();
                    paran.ParameterName = "@idUsuario";
                    paran.Value = (int)idUsuario;
                    Cmd.Parameters.Add(paran);
                }

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCliente";
                paran1.Value = (int)idCliente;
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

                return MakeListToGet(iType);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public LIType BuscarPaginas(IType iType, string dsPagina, bool restrito, int? idCliente, int? idPrograma, int? idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarPaginas";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@dsPagina";
                paran.Value = dsPagina;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCliente";
                paran1.Value = (int)idCliente;
                Cmd.Parameters.Add(paran1);

                if (restrito)
                {
                    DbParameter paran3 = Cmd.CreateParameter();
                    paran3.ParameterName = "@restrito";
                    paran3.Value = restrito;
                    Cmd.Parameters.Add(paran3);
                }

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

                return MakeListToGet(iType);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }
        }

        public int GetQuantidadePaginas(IType iType, bool restrito, int? idCliente, int? idPrograma, int? idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarQuantidadePaginas";

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@restrito";
                paran.Value = restrito;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCliente";
                paran1.Value = (int)idCliente;
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

        public LIType GetPaginasPai(IType iType, int? idPagina, int? idCliente, int? idPrograma, int? idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarPaginasPai";

                if (idPagina != null)
                {
                    DbParameter paran = Cmd.CreateParameter();
                    paran.ParameterName = "@idPagina";
                    paran.Value = (int)idPagina;
                    Cmd.Parameters.Add(paran);
                }

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCliente";
                paran1.Value = (int)idCliente;
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

                return MakeListToGet(iType);
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                Cn.Close();
                Cn.Dispose();
            }

        }

        public LIType GetPaginasFilhas(IType iType, int? idPaginaPai, int? idUsuario, int? idCliente, int? idPrograma, int? idCampanha)
        {
            try
            {
                Cmd = DataBaseGeneric.CreateCommand(BaseType);
                Cmd.Connection = Cn;
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.CommandText = "sp_SYS_BuscarPaginasFilhas";

                DbParameter paran0 = Cmd.CreateParameter();
                paran0.ParameterName = "@idPaginaPai";
                paran0.Value = (int)idPaginaPai;
                Cmd.Parameters.Add(paran0);

                DbParameter paran = Cmd.CreateParameter();
                paran.ParameterName = "@idUsuario";
                paran.Value = (int)idUsuario;
                Cmd.Parameters.Add(paran);

                DbParameter paran1 = Cmd.CreateParameter();
                paran1.ParameterName = "@idCliente";
                paran1.Value = (int)idCliente;
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
