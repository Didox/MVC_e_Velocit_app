using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Tabela

    [Table(Name = "SYS_Tabela")]
    public class Tabela : CType
    {
        #region Construtores
        public Tabela() { }

        public Tabela(int? idTabela)
        {
            this.IDTabela = idTabela;
        }

        public Tabela(string descricao)
        {
            this.Descricao = descricao;
        }
        #endregion

        #region Destrutor
        ~Tabela() { Dispose(); }
        #endregion

        #region Attributes
        private List<Campo> campos;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTabela { get; set; }

        [Validate]
        [Property(IsField = true, Size = 150, Name = "dsTabela")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        public List<Campo> Campos
        {
            get
            {
                if (this.IDTabela == null) return new List<Campo>();
                if (this.campos == null)
                {
                    Campo campo = new Campo();
                    campo.Transaction = this.Transaction;
                    campo.IDTabela = this.IDTabela;
                    var campos = Pagina.Find(campo); 
                    this.campos = new List<Campo>();
                    campos.ForEach(c => this.campos.Add(((Campo)c)));
                }
                return this.campos;
            }
        }

        #endregion
        
        #region Methods

        public Campo GetCampo(string nomeCampo)
        {
            var campo = new Campo();
            campo.Tabela = this;
            campo.Nome = nomeCampo;
            campo.Get();

            return campo;
        }

        public void SetCampo(Pessoa pessoa, string nomeCampo, string value)
        {
            var campo = new Campo();
            campo.Tabela = this;
            campo.Nome = nomeCampo;
            campo.Get();
            campo.Valor(pessoa).SetValor(value);
        }

        public override void Delete()
        {
            if (this.IDTabela == null)
                throw new TradeVisionValidationError("Id tabela não encontrado.");

            this.Get();

            if (this.Campos.Count > 0)
                throw new TradeVisionValidationError("A tabela (" + this.Descricao + ") contem campos que precisam ser apagados antes de apagar a tabela");


            try
            {
                this.IsTransaction = true;
                var tabelaCliente = new TabelaCliente();
                tabelaCliente.Transaction = this.Transaction;
                tabelaCliente.Tabela = this;
                tabelaCliente.Delete();

                base.Delete();
                this.Commit();
            }
            catch (Exception err)
            {
                this.Rollback();
                throw err;
            }
        }

        public string RenderTabela(Pessoa pessoa)
        {
            string htmlTabela = string.Empty;
            if (this.IDTabela != null)
            {
                foreach (var campo in this.Campos)
                    htmlTabela += GetHtmlByCampo(campo, pessoa);
            }
            return htmlTabela;
        }

        private string GetHtmlByCampo(Campo campo, Pessoa pessoa)
        {
            var table = string.Empty;
            table += "<div class=\"rol " + (campo.HasError ? "errorField" : "") + " \">" +
                    "<div class=\"col\">" + campo.Label + ": </div>" +
                    "<div class=\"col\">";
            if (this.GetCampo(campo.Nome).IsInput())
            {
                table += "<input type=\"text\" name=\"" + campo.Nome + "\" id=\"" + campo.Nome + "\"";
                if (!string.IsNullOrEmpty(this.GetCampo(campo.Nome).Valor(pessoa).GetValor()))
                {
                    table += "value=\"" + this.GetCampo(campo.Nome).Valor(pessoa).GetValor() + "\"";
                }
                table += " />";
            }
            else if (this.GetCampo(campo.Nome).IsSelect())
            {
                table += "<select id=\"" + campo.Nome + "\" name=\"" + campo.Nome + "\">" +
                   "<option value=\"0\">-- selecione --</option>";
                foreach (var opcao in campo.Opcoes)
                {
                    table += "<option value=\"" + opcao.Opcao + "\"";
                    if (this.GetCampo(campo.Nome).Valor(pessoa).GetValor() == opcao.Opcao)
                    {
                        table += "selected=\"true\"";
                    }
                    table += ">" + opcao.Opcao + "</option>";
                }
                table += "</select>";
            }
            else if (this.GetCampo(campo.Nome).IsCheck())
            {
                foreach (var opcao in campo.Opcoes)
                {
                    table += "<div id=\"" + campo.Nome + campo.IDCampo + "\">";
                    table += "<input type=\"checkbox\" name=\"" + campo.Nome + "\" id=\"" + campo.Nome + campo.IDCampo + "\"";
                    table += "value=\"" + opcao.Opcao + "\"";
                    if (!string.IsNullOrEmpty(this.GetCampo(campo.Nome).Valor(pessoa).GetValor()) && this.GetCampo(campo.Nome).Valor(pessoa).ContainsValor(opcao.Opcao))
                    {
                        table += "checked";
                    }
                    table += ">" + opcao.Opcao + "</div>";
                }
            }
            else if (this.GetCampo(campo.Nome).IsListItem())
            {
                table += "<select id=\"" + campo.Nome + "\" name=\"" + campo.Nome + "\" multiple=\"true\">";
                table += "<option value=\"0\">-- selecione --</option>";
                foreach (var opcao in campo.Opcoes)
                {
                    table += "<option value=\"" + opcao.Opcao + "\"";
                    if (!string.IsNullOrEmpty(this.GetCampo(campo.Nome).Valor(pessoa).GetValor()) && this.GetCampo(campo.Nome).Valor(pessoa).ContainsValor(opcao.Opcao))
                    {
                        table += "selected=\"true\"";
                    }
                    table += ">" + opcao.Opcao + "</option>";
                }
                table += "</select>";
            }
            else if (this.GetCampo(campo.Nome).IsRadio())
            {
                foreach (var opcao in campo.Opcoes)
                {
                    table += "<div id=\"" + campo.Nome + campo.IDCampo + "\">";
                    table += "<input type=\"radio\" name=\"" + campo.Nome + "\" id=\"" + campo.Nome + campo.IDCampo + "\"";
                    table += "value=\"" + opcao.Opcao + "\"";
                    if (this.GetCampo(campo.Nome).Valor(pessoa).GetValor() == opcao.Opcao)
                    {
                        table += "checked";
                    }
                    table += ">" + opcao.Opcao + "</div>";
                }
            }
            else if (this.GetCampo(campo.Nome).IsTextArea())
            {
                table += "<textarea name=\"" + campo.Nome + "\" id=\"" + campo.Nome + "\">";
                if (!string.IsNullOrEmpty(this.GetCampo(campo.Nome).Valor(pessoa).GetValor()))
                {
                    table += this.GetCampo(campo.Nome).Valor(pessoa).GetValor();
                }
                table += "</textarea>";
            }
            else table += "tipo inválido";

            table += "</div></div>";
            return table;
        }

        #endregion
    }
    #endregion
}
