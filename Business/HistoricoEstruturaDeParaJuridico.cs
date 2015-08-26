using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class HistoricoEstruturaDeParaJuridico

    [Table(Name = "NEG_HistoricoEstruturaDeParaJuridico", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class HistoricoEstruturaDeParaJuridico : CType
    {
        #region Construtores
        public HistoricoEstruturaDeParaJuridico()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public HistoricoEstruturaDeParaJuridico(PessoaJuridica pessoaJuridica)
        {
            CarregarConnectionString(Cliente.Current());
            PessoaJuridica = pessoaJuridica;
        }

        #endregion

        #region Destrutor
        ~HistoricoEstruturaDeParaJuridico() { Dispose(); }
        #endregion

        #region Attributes
        private PessoaJuridica pessoaJuridica;
        private PessoaJuridica pessoaJuridicaAntigo;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDHistoricoEstruturaDeParaJuridico { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey=true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoaJuridica { get; set; }

        public string RazaoSocialDe
        {
            get { return CType.Exist(PessoaJuridica) ? PessoaJuridica.RazaoSocial : string.Empty; }
        }


        public PessoaJuridica PessoaJuridica
        {
            get
            {
                if (this.pessoaJuridica == null)
                {
                    this.pessoaJuridica = new PessoaJuridica();
                    this.pessoaJuridica.IDPessoaJuridica = this.IDPessoaJuridica;
                }

                if (!this.pessoaJuridica.IsFull)
                {
                    this.pessoaJuridica.Transaction = this.Transaction;
                    this.pessoaJuridica.Get();
                }

                return this.pessoaJuridica;
            }
            set
            {
                this.pessoaJuridica = value;
                if (value != null) this.IDPessoaJuridica = value.IDPessoaJuridica;
            }
        }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoaJuridicaAntigo { get; set; }

        public string RazaoSocialPara
        {
            get { return CType.Exist(PessoaJuridicaAntigo) ? PessoaJuridicaAntigo.RazaoSocial : string.Empty; }
        }

        public PessoaJuridica PessoaJuridicaAntigo
        {
            get
            {
                if (this.pessoaJuridicaAntigo == null)
                {
                    this.pessoaJuridicaAntigo = new PessoaJuridica();
                    this.pessoaJuridicaAntigo.IDPessoaJuridica = this.IDPessoaJuridicaAntigo;
                }

                if (!this.pessoaJuridicaAntigo.IsFull)
                {
                    this.pessoaJuridicaAntigo.Transaction = this.Transaction;
                    this.pessoaJuridicaAntigo.Get();
                }

                return this.pessoaJuridicaAntigo;
            }
            set
            {
                this.pessoaJuridicaAntigo = value;
                if (value != null) this.IDPessoaJuridicaAntigo = value.IDPessoaJuridica;
            }
        }

        [Validate]
        [Property(IsField = true, IsOrderField=true, Name="Ordem")]
        [Operations(UseSave = true)]
        public int? Ordem { get; set; }

        #endregion

        #region Methods

        #endregion

    }
    #endregion
}