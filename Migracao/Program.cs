using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Didox.Business;
using System.Data.OleDb;
using System.Data;
using System.Reflection;
using Didox.DataBase.Generics;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using System.Text.RegularExpressions;


namespace Migracao
{
    class Program
    {
        static void Main(string[] args)
        {
            //MigracaoUsuario.Importar("pj");
        }

        #region Importador
        class MigracaoUsuario
        {
            public MigracaoUsuario() { }

            private static DataTable header = null;
            private static DataTable table = null;

            public static void Importar(string xlsName)
            {
                LoadXls(xlsName);

                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Usuario usuario = null;
                    PessoaJuridica pessoaJuridica = null;
                    saveDadosStaticos(ref usuario, ref pessoaJuridica, i);
                    if (CType.Exist(usuario))
                    {
                        var pessoa = usuario.Pessoa;
                        if (CType.Exist(pessoa))
                            saveDadosDinamicos(ref pessoa, i);
                    }

                    if (CType.Exist(pessoaJuridica))
                    {
                        var pessoa = pessoaJuridica.Pessoa;
                        if (CType.Exist(pessoa))
                            saveDadosDinamicos(ref pessoa, i);
                    }
                }
            }

            private static LIType LoadModels()
            {
                var tabelas = new LIType();
                var campanha = new Campanha(201);
                campanha.Get();

                if (campanha.IDCampanha == null)
                    throw new TradeVisionValidationError("Campanha não encontrada");

                tabelas.Add(new Usuario(campanha));
                tabelas.Add(new PessoaJuridica(campanha));

                var classesDidoxBusiness = CType.GetAllClassesOfAssembly(new Usuario());
                var listaTabelas = getListaTabelasXLS();
                foreach (string tabela in listaTabelas)
                {
                    foreach (IType iType in classesDidoxBusiness)
                    {
                        if (CType.GetTableName(iType).ToLower() == tabela.ToLower())
                        {
                            if (!tabelas.Exists(it => it.GetType().Name == iType.GetType().Name))
                            {
                                tabelas.Add(iType);
                                break;
                            }
                        }
                    }
                }
                return tabelas;
            }

            private static List<string> LoadTabelasDinamicas()
            {
                var tabelas = new List<string>();
                var listaTabelas = getListaTabelasXLS();
                foreach (string tabela in listaTabelas)
                {
                    if (tabela.IndexOf("DadoDinamico[") != -1)
                    {
                        tabelas.Add(tabela);
                    }
                }
                return tabelas;
            }

            private static List<string> getListaTabelasXLS()
            {
                var tabelas = new List<string>();
                if (header.Rows.Count > 0)
                {
                    for (int i = 0; i < header.Columns.Count; i++)
                    {
                        var coluna = header.Rows[0][i].ToString();
                        coluna = Regex.Replace(coluna, @"\..*", "");
                        if (!tabelas.Exists(t => t == coluna)) tabelas.Add(coluna);
                    }
                }
                return tabelas;
            }

            private static void saveDadosDinamicos(ref Pessoa pessoa, int i)
            {
                foreach (var tabela in LoadTabelasDinamicas())
                {                    
                    var nomeTabela = Regex.Replace(tabela, @".*?\[|\]", "");
                    var table = new Tabela();
                    table.Descricao = nomeTabela;
                    table.Get();

                    foreach (var campo in table.Campos)
                    {
                        string valor = GetValueXls(tabela + "." + campo.Nome, i);
                        if (!string.IsNullOrEmpty(valor))
                        {
                            var value = campo.Valor(pessoa);
                            value.SetValor(valor);
                            value.Save();
                        }
                    }
                }
            }

            private static void saveDadosStaticos(ref Usuario usuario, ref PessoaJuridica pessoaJuridica, int i)
            {
                foreach (IType tabela in LoadModels())
                {
                    var isToSave = false;
                    Pessoa pessoa = null;

                    if (!(tabela is Usuario))
                    {
                        if (usuario != null)
                            pessoa = usuario.Pessoa;
                    }

                    if (!(tabela is PessoaJuridica))
                    {
                        if (pessoaJuridica != null)
                            pessoa = pessoaJuridica.Pessoa;
                    }

                    foreach (PropertyInfo pi in tabela.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic))
                    {
                        if (pi.Name.ToLower() == "pessoa" && CType.Exist(pessoa))
                            pi.SetValue(tabela, pessoa, null);
                        else if (pi.Name.ToLower() == "usuario" && CType.Exist(usuario))
                            pi.SetValue(tabela, usuario, null);
                        else if (pi.Name.ToLower() == "pessoajuridica" && CType.Exist(pessoaJuridica))
                            pi.SetValue(tabela, pessoaJuridica, null);
                        else
                        {
                            var propName = CType.GetTableName(tabela) + "." + CType.GetPropertyName(pi);
                            string valorXls = GetValueXls(propName, i);
                            if (!string.IsNullOrEmpty(valorXls))
                            {
                                object value = null;
                                try { value = tabela.PrepareValueProperty(valorXls, pi.PropertyType); }
                                catch { } // igora valores inválidos
                                if (value != null)
                                {
                                    isToSave = true;
                                    pi.SetValue(tabela, value, null);
                                }
                            }
                        }
                    }

                    if (isToSave)
                    {
                        var tableSave = ((CType)tabela);
                        tableSave.Get();
                        if (!CType.Exist(tableSave)) tableSave.Save();
                        if (tableSave is Usuario) usuario = (Usuario)tableSave;
                        if (tableSave is PessoaJuridica) pessoaJuridica = (PessoaJuridica)tableSave;
                    }
                }
            }

            private static void LoadXls(string xlsName)
            {
                var tables = LoadDataTable(xlsName);
                header = tables[0];
                table = tables[1];
            }

            private static string GetValueXls(string propName, int index)
            {
                if (header.Rows.Count > 0)
                {
                    for (int i = 0; i < header.Columns.Count; i++)
                    {
                        var coluna = header.Rows[0][i].ToString();
                        if (coluna.ToLower() == propName.ToLower())
                        {
                            return table.Rows[index][i].ToString();
                        }
                    }
                }
                return null;
            }

            private static List<DataTable> LoadDataTable(string xlsName)
            {
                string pathWithXls = @"C:\xls\TradeVision\" + xlsName + ".xls", plan = "Plan1";
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;";
                connectionString += String.Format("Data Source={0};", pathWithXls);
                connectionString += "Extended Properties='Excel 8.0;HDR=NO;'";

                OleDbConnection cn = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand("Select * from [" + plan + "$A1:BZ1]", cn);
                try
                {
                    var tabelas = new List<DataTable>();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dtHeader = new DataTable();
                    da.Fill(dtHeader);
                    tabelas.Add(dtHeader);

                    cmd.CommandText = "Select * from [" + plan + "$A2:BZ10000]";
                    DataTable dtDados = new DataTable();
                    da.Fill(dtDados);
                    tabelas.Add(dtDados);

                    return tabelas;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cn.Close();
                    cn.Dispose();
                    cmd.Dispose();
                }
            }
        }

        #endregion

        #region Geradores

        private static void criarTiposDocumentos()
        {
            var tipoDocumento = new TipoDocumento();
            tipoDocumento.Nome = "CPF";
            tipoDocumento.Save();

            tipoDocumento = new TipoDocumento();
            tipoDocumento.Nome = "RG";
            tipoDocumento.Save();

            tipoDocumento = new TipoDocumento();
            tipoDocumento.Nome = "CNPJ";
            tipoDocumento.Save();

            tipoDocumento = new TipoDocumento();
            tipoDocumento.Nome = "TVI";
            tipoDocumento.Save();
        }

        private static void criarTiposEnderecos()
        {
            var tipoEndereco = new TipoEndereco();
            tipoEndereco.Nome = "Residencial";
            tipoEndereco.Save();

            tipoEndereco = new TipoEndereco();
            tipoEndereco.Nome = "Comercial";
            tipoEndereco.Save();
        }

        private static void criarTiposEmails()
        {
            var tipoEmail = new TipoEmail();
            tipoEmail.Nome = "Padrão";
            tipoEmail.Save();
        }

        private static void criarTiposTelefones()
        {
            var tipoTelefone = new TipoTelefone();
            tipoTelefone.Nome = "Celular";
            tipoTelefone.Save();

            tipoTelefone = new TipoTelefone();
            tipoTelefone.Nome = "Residencial";
            tipoTelefone.Save();

            tipoTelefone = new TipoTelefone();
            tipoTelefone.Nome = "Comercial";
            tipoTelefone.Save();

            tipoTelefone = new TipoTelefone();
            tipoTelefone.Nome = "Fax";
            tipoTelefone.Save();
        }
        #endregion
    }
}
