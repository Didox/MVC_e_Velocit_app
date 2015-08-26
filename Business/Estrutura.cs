using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Estrutura

    [Table(Name = "SEG_Estrutura", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Estrutura : CType
    {
        #region Construtores
        public Estrutura()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Estrutura(int? idEstrutura)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDEstrutura = idEstrutura;
        }
        #endregion

        #region Destrutor
        ~Estrutura() { Dispose(); }
        #endregion

        #region Attributes
        private Hierarquia hierarquia;
        private Pessoa pessoa;   
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEstrutura { get; set; }        

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDHierarquia { get; set; }

        public Hierarquia Hierarquia
        {
            get
            {
                if (this.hierarquia == null)
                {
                    this.hierarquia = new Hierarquia();
                    this.hierarquia.IDHierarquia = this.IDHierarquia;
                }

                if (!this.hierarquia.IsFull)
                {
                    this.hierarquia.Transaction = this.Transaction;
                    this.hierarquia.Get();
                }

                return this.hierarquia;
            }
            set
            {
                this.hierarquia = value;
                if (value != null) this.IDHierarquia = value.IDHierarquia;
            }
        }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDSubNivel { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel1 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel2 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel3 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel4 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel5 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel6 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel7 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel8 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel9 { get; set; }

        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaNivel10 { get; set; }

        #endregion
        
        #region Methods

        public void Save(Pessoa _pessoa, Hierarquia _hierarquia)
        {
            this.ExecSql("sp_SaveEstrutura @idHierarquia = " + _hierarquia.IDHierarquia + ", @idPessoa = " + _pessoa.IDPessoa + "");
        }

        public void Save(Pessoa _pessoa, Pessoa _pessoaPai)
        {
            this.ExecSql("sp_SaveEstruturaPessoaPai @idPessoaPai = " + _pessoaPai.IDPessoa + ", @idPessoa = " + _pessoa.IDPessoa + "");
        }

        public void Delete(Pessoa _pessoa, Pessoa _pessoaPai)
        {
            this.ExecSql("sp_DeleteEstruturaPessoaPai @idPessoaPai = " + _pessoaPai.IDPessoa + ", @idPessoa = " + _pessoa.IDPessoa + "");
        }

        public void Delete(Pessoa _pessoa, Hierarquia hierarquia)
        {
            this.ExecSql("sp_DeletePessoaDaHierarquia @idPessoa = " + _pessoa.IDPessoa + ", @idHierarquia = " + hierarquia.IDHierarquia + "");
        }

        public void SaveEstrutura(int idPessoaPai, List<int> idsPessoaNew)
        {
            var pessoa = new Pessoa(idPessoaPai);
            pessoa.Transaction = this.Transaction;
            var pessoasJuridicasAdded = pessoa.GetHierarquiaPessoasFilhas();
            var idsPessoaOld = new List<int>();
            pessoasJuridicasAdded.ForEach(j => idsPessoaOld.Add((int)j.Id));

            var idsRemove = new List<string>();
            foreach (int id in idsPessoaOld)
            {
                if (!idsPessoaNew.Exists(d => d == id))
                    idsRemove.Add(id.ToString());
            }

            var idsAdd = new List<int>();
            foreach (int id in idsPessoaNew)
            {
                if (!idsPessoaOld.Exists(d => d == id))
                    idsAdd.Add(id);
            }

            pessoa.DeleteHierarquiasFilhas(idPessoaPai, idsRemove);

            foreach (int id in idsAdd)
                this.Save(new Pessoa(id), new Pessoa(idPessoaPai));
        }

        public void SaveEstruturaPrimeiroNivel(List<int> idsPessoaNew, int hierarquiaId)
        {
            var pessoasJuridicasAdded = new Pessoa().FindAllPessoaPrimeiroNivel(new Hierarquia(hierarquiaId));
            var idsPessoaOld = new List<int>();
            pessoasJuridicasAdded.ForEach(j => idsPessoaOld.Add((int)j.Id));

            var idsRemove = new List<string>();
            foreach (int id in idsPessoaOld)
            {
                if (!idsPessoaNew.Exists(d => d == id))
                    idsRemove.Add(id.ToString());
            }

            var idsAdd = new List<int>();
            foreach (int id in idsPessoaNew)
            {
                if (!idsPessoaOld.Exists(d => d == id))
                    idsAdd.Add(id);
            }

            new Pessoa().DeleteHierarquias(idsRemove);

            foreach (int id in idsAdd)
                this.Save(new Pessoa(id), new Hierarquia(hierarquiaId));
        }

        #endregion
    }
    #endregion
}
