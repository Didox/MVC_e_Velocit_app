using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Campo

    [Table(Name = "SYS_Campo")]
    public class Campo : CType
    {
        #region Construtores
        public Campo() { }

        public Campo(int? idCampo)
        {
            this.IDCampo = idCampo;
        }

        public Campo(string label, int idTabela)
        {
            this.Label = label;
            this.IDTabela = idTabela;
        }

        #endregion

        #region Destrutor
        ~Campo() { Dispose(); }
        #endregion

        #region Attributes
        private Tabela tabela;
        private CampoValor valor;
        private List<CampoOpcao> opcoes;
        #endregion

        #region Propriedades

        public bool HasError
        {
            get
            {
                string keyErro = "[HasErro]Campo" + this.IDCampo.ToString();
                var objectErro = Session.Get(keyErro);
                if (objectErro == null) return false;
                bool hasErro = (bool)objectErro;
                Session.Invalidate(keyErro);
                return hasErro;
            }
            set
            {
                Session.Add("[HasErro]Campo" + this.IDCampo.ToString(), value);
            }
        }
        

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCampo { get; set; }

        [Validate]
        [Property(IsField = true, DontUseLikeWithStrings=true)]
        [Operations(UseSave = true, UseGet=true)]
        public string Nome { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public string Label { get; set; }

        [Validate]
        [Property(IsField = true, IsOrderField=true)]
        [Operations(UseSave = true)]
        public int Ordem { get; set; }        

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public TipoCampo Tipo { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public TipoInput TipoInput { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public int? Tamanho { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public bool PermiteNulo { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public string ValorDefault { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTabela { get; set; }

        public Tabela Tabela
        {
            get
            {
                if (this.tabela == null)
                {
                    this.tabela = new Tabela();
                    this.tabela.IDTabela = this.IDTabela;
                }

                if (!this.tabela.IsFull)
                {
                    this.tabela.Transaction = this.Transaction;
                    this.tabela.Get();
                }

                return this.tabela;
            }
            set
            {
                this.tabela = value;
                if (value != null) this.IDTabela = value.IDTabela;
            }
        }

        public List<CampoOpcao> Opcoes
        {
            get
            {
                if (this.opcoes == null)
                {
                    CampoOpcao opcao = new CampoOpcao();
                    opcao.Transaction = this.Transaction;
                    opcao.Campo = this;
                    var opcoes = CampoOpcao.Find(opcao);
                    this.opcoes = new List<CampoOpcao>();
                    opcoes.ForEach(o => this.opcoes.Add(((CampoOpcao)o)));
                }
                return this.opcoes;
            }
        }

        public CampoValor Valor(Pessoa pessoa)
        {
            if (this.valor == null)
            {
                this.valor = new CampoValor();
                this.valor.Pessoa = pessoa;
                if (this.valor.Pessoa != null)
                {
                    this.valor.Campo = this;
                    this.valor.Transaction = this.Transaction;
                    this.valor.Get();
                }
            }

            return this.valor;
        }

        public bool IsInput()
        {
            return this.TipoInput == TipoInput.Text;
        }

        public bool IsSelect()
        {
            return this.TipoInput == TipoInput.Select;
        }

        public bool IsRadio()
        {
            return this.TipoInput == TipoInput.Radio;
        }

        public bool IsTextArea()
        {
            return this.TipoInput == TipoInput.TextArea;
        }

        public bool IsCheck()
        {
            return this.TipoInput == TipoInput.Check;
        }

        public bool IsListItem()
        {
            return this.TipoInput == TipoInput.ListItem;
        }

        #endregion
        
        #region Methods

        public override void Delete()
        {
            if (this.IDCampo == null)
                throw new TradeVisionValidationError("Id campo não encontrado.");

            this.Get();

            if (this.Opcoes.Count > 0)
                throw new TradeVisionValidationError("A campo (" + this.Nome + ") contem opções que precisam ser apagados antes de apagar o campo");
            try
            {
                this.IsTransaction = true;
                var campoValor = new CampoValor();
                campoValor.Transaction = this.Transaction;
                campoValor.Campo = this;
                campoValor.Delete();

                base.Delete();
                this.Commit();
            }
            catch (Exception err)
            {
                this.Rollback();
                throw err;
            }
        }
        #endregion
    }

    #endregion

    #region Enum TipoCampo

    public enum TipoCampo
    {
        Inteiro = 1,
        Texto = 2,
        Moeda = 3,
        TextoGrande = 4,
        Boleano = 5,
        Data = 6
    }

    public enum TipoInput
    {
        Text = 1,
        Select = 2,
        Radio = 3,
        Check = 4,
        TextArea = 5,
        ListItem = 6
    }

    #endregion
}
