using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class DeJuridicaParaJuridica

    [Table(Name = "NEG_DeJuridicaParaJuridica", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class DeJuridicaParaJuridica : CType
    {
        #region Construtores
        public DeJuridicaParaJuridica()
        {
            CarregarConnectionString(Cliente.Current());
        }

        #endregion

        #region Destrutor
        ~DeJuridicaParaJuridica() { Dispose(); }
        #endregion

        #region Attributes
        private PessoaJuridica pessoaJuridicaDe;
        private PessoaJuridica pessoaJuridicaPara;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDDeJuridicaParaJuridica { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey=true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoaJuridicaDe { get; set; }

        public string RazaoSocialDe
        {
            get { return CType.Exist(PessoaJuridicaDe) ? PessoaJuridicaDe.RazaoSocial : string.Empty; }
        }

        public PessoaJuridica PessoaJuridicaDe
        {
            get
            {
                if (this.pessoaJuridicaDe == null)
                {
                    this.pessoaJuridicaDe = new PessoaJuridica();
                    this.pessoaJuridicaDe.IDPessoaJuridica = this.IDPessoaJuridicaDe;
                }

                if (!this.pessoaJuridicaDe.IsFull)
                {
                    this.pessoaJuridicaDe.Transaction = this.Transaction;
                    this.pessoaJuridicaDe.Get();
                }

                return this.pessoaJuridicaDe;
            }
            set
            {
                this.pessoaJuridicaDe = value;
                if (value != null) this.IDPessoaJuridicaDe = value.IDPessoaJuridica;
            }
        }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoaJuridicaPara { get; set; }

        public string RazaoSocialPara
        {
            get { return CType.Exist(PessoaJuridicaPara) ? PessoaJuridicaPara.RazaoSocial : string.Empty; }
        }

        public PessoaJuridica PessoaJuridicaPara
        {
            get
            {
                if (this.pessoaJuridicaPara == null)
                {
                    this.pessoaJuridicaPara = new PessoaJuridica();
                    this.pessoaJuridicaPara.IDPessoaJuridica = this.IDPessoaJuridicaPara;
                }

                if (!this.pessoaJuridicaPara.IsFull)
                {
                    this.pessoaJuridicaPara.Transaction = this.Transaction;
                    this.pessoaJuridicaPara.Get();
                }

                return this.pessoaJuridicaPara;
            }
            set
            {
                this.pessoaJuridicaPara = value;
                if (value != null) this.IDPessoaJuridicaPara = value.IDPessoaJuridica;
            }
        }

        #endregion

        #region Methods

        public override void Save()
        {
            if (this.Id != null) throw new Exception("Alteração não permitida para testa classe");
            this.IsTransaction = true;
            try
            {
                base.Save();

                var history = new HistoricoEstruturaDeParaJuridico();
                history.Transaction = this.Transaction;
                history.PessoaJuridica = this.PessoaJuridicaPara;
                history.Delete();

                history.PessoaJuridicaAntigo = this.PessoaJuridicaDe;
                history.Ordem = 0;
                history.Save();    

                saveHistoryOfMigrationJuridicPerson2(this.pessoaJuridicaDe, 1);

                this.Commit();
            }
            catch (Exception err)
            {
                this.Rollback();
                throw err;
            }
        }

        private void saveHistoryOfMigrationJuridicPerson2(PessoaJuridica pessoaJuridica, int index)
        {
            var dePara = new HistoricoEstruturaDeParaJuridico();
            dePara.Transaction = this.Transaction;
            dePara.PessoaJuridica = pessoaJuridica;

            foreach (HistoricoEstruturaDeParaJuridico dp in dePara.Find())
            {
                var history = new HistoricoEstruturaDeParaJuridico();
                history.Transaction = this.Transaction;
                history.PessoaJuridica = this.PessoaJuridicaPara;
                history.PessoaJuridicaAntigo = dp.PessoaJuridicaAntigo;
                history.Ordem = index;
                history.Save();
                index++;
            }

        }

        private void saveHistoryOfMigrationJuridicPerson(PessoaJuridica pessoaJuridica, int index)
        {
            var dePara = new DeJuridicaParaJuridica();
            dePara.Transaction = this.Transaction;
            dePara.PessoaJuridicaPara = pessoaJuridica;
            foreach (DeJuridicaParaJuridica dp in dePara.Find())
            {
                var history = new HistoricoEstruturaDeParaJuridico();
                history.Transaction = this.Transaction;
                history.PessoaJuridica = this.PessoaJuridicaPara;
                history.PessoaJuridicaAntigo = dp.PessoaJuridicaDe;
                history.Ordem = index;
                history.Save();
                index++;
                saveHistoryOfMigrationJuridicPerson(dp.PessoaJuridicaDe, index);
            }

        }

        public override void Delete()
        {
            throw new Exception("Exclusão não permitida para testa classe");
        }

        #endregion

    }
    #endregion
}