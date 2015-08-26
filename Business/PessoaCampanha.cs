using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class PessoaCampanha


    [Table(Name = "NEG_PessoaCampanha", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class PessoaCampanha : CType
    {
        #region Construtores
        public PessoaCampanha()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public PessoaCampanha(int? idPessoaCampanha)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDPessoaCampanha = idPessoaCampanha;
        }

        public PessoaCampanha(Pessoa pessoa, Campanha campanha)
        {
            CarregarConnectionString(Cliente.Current());
            this.Pessoa = pessoa;
            this.Campanha = campanha;
        }

        #endregion

        #region Destrutor
        ~PessoaCampanha() { Dispose(); }
        #endregion

        #region Attributes
        private Campanha campanha;
        private Pessoa pessoa;
        private Usuario usuario;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoaCampanha { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCampanha { get; set; }

        public Campanha Campanha
        {
            get
            {
                if (this.campanha == null)
                {
                    this.campanha = new Campanha();
                    this.campanha.IDCampanha = this.IDCampanha;
                }

                if (!this.campanha.IsFull)
                {
                    this.campanha.Transaction = this.Transaction;
                    this.campanha.Get();
                }

                return this.campanha;
            }
            set
            {
                this.campanha = value;
                if (value != null) this.IDCampanha = value.IDCampanha;
            }
        }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoa { get; set; }

        public Pessoa Pessoa
        {
            get
            {
                if (this.pessoa == null)
                {
                    this.pessoa = new Pessoa();
                    this.pessoa.IDPessoa = this.IDPessoa;
                }

                if (!this.pessoa.IsFull)
                {
                    this.pessoa.Transaction = this.Transaction;
                    this.pessoa.Get();
                }

                return this.pessoa;
            }
            set
            {
                this.pessoa = value;
                if (value != null) this.IDPessoa = value.IDPessoa;
            }
        }


        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public DateTime? DataAdesao { get; set; }
        public string DataAdesaoFormatada { get { return ((DateTime)DataAdesao).ToString("dd/MM/yyyy"); } }

        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public DateTime? DataExclusao { get; set; }
        public string DataExclusaoFormatada { get { return DataExclusao == null ? "" : ((DateTime)DataExclusao).ToString("dd/MM/yyyy"); } }
        public string DataExclusaoComValidacao { get { return DataExclusao == null ? "Atualmente" : ((DateTime)DataExclusao).ToString("dd/MM/yyyy"); } }

        [Property(IsField = true, Name = "idCredencial")]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDUsuario { get; set; }

        public Usuario Usuario
        {
            get
            {
                if (this.usuario == null)
                {
                    this.usuario = new Usuario();
                    this.usuario.IDUsuario = this.IDUsuario;
                }

                if (!this.usuario.IsFull)
                {
                    this.usuario.Transaction = this.Transaction;
                    this.usuario.Get();
                }

                return this.usuario;
            }
            set
            {
                this.usuario = value;
                if (value != null) this.IDUsuario = value.IDUsuario;
            }
        }

        #endregion

        #region "Methods"
        public override void Save()
        {
            if (!CType.Exist(this.pessoa))
                throw new TradeVisionValidationError("Pessoa obrigatório no cadastro de pessoa campanha");

            var conditions = "idPessoa = " + this.IDPessoa + " and ((DataAdesao <= getdate() and DataExclusao >= getdate()) or (DataAdesao <= getdate() and DataExclusao is null))";
            var listPessoaCampanha = new PessoaCampanha().FindByConditions(conditions);

            if (listPessoaCampanha.Count > 0)
            {
                if (this.IDPessoaCampanha == null || (listPessoaCampanha.Count > 1))
                {
                    var pessoaCampanha = (PessoaCampanha)listPessoaCampanha[0];
                    throw new TradeVisionValidationError("Já existe uma campanha ativa neste periodo (" + pessoaCampanha.DataAdesaoFormatada + " até " + pessoaCampanha.DataExclusaoComValidacao + ")");
                }
            }

            base.Save();
        }
        #endregion
    }

    #endregion
}