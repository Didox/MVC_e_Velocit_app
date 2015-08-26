using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class CargoEstrutura

    [Table(Name = "SEG_CargoEstrutura", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class CargoEstrutura : CType
    {
        #region Construtores
        public CargoEstrutura()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public CargoEstrutura(int? idCargoEstrutura)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDCargoEstrutura = idCargoEstrutura;
        }
        #endregion

        #region Destrutor
        ~CargoEstrutura() { Dispose(); }
        #endregion

        #region Attributes
        private Cargo cargo;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCargoEstrutura { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCargo { get; set; }

        public Cargo Cargo
        {
            get
            {
                if (this.cargo == null)
                {
                    this.cargo = new Cargo();
                    this.cargo.IDCargo = this.IDCargo;
                }

                if (!this.cargo.IsFull)
                {
                    this.cargo.Transaction = this.Transaction;
                    this.cargo.Get();
                }

                return this.cargo;
            }
            set
            {
                this.cargo = value;
                if (value != null) this.IDCargo = value.IDCargo;
            }
        }

        [Property(IsField = true, IsForeignKey=true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDSubnivel { get; set; }

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
        public void Delete(Pessoa _pessoa, Pessoa _pessoaPai)
        {
            this.ExecSql("sp_DeleteCargoEstruturaPessoaPai @idPessoaPai = " + _pessoaPai.IDPessoa + ", @idPessoa = " + _pessoa.IDPessoa + "");
        }

        public void Save(Pessoa _pessoa, Pessoa _pessoaPai)
        {
            this.ExecSql("sp_SaveCargoEstruturaPessoaPai @idPessoaPai = " + _pessoaPai.IDPessoa + ", @idPessoa = " + _pessoa.IDPessoa + "");
        }

        public void Delete(Pessoa _pessoa, Cargo _cargo)
        {
            this.ExecSql("sp_DeletePessoaDoCargo @idPessoa = " + _pessoa.IDPessoa + ", @idCargo = " + _cargo.IDCargo + "");
        }

        public void Save(Pessoa _pessoa, Cargo _cargo)
        {
            this.ExecSql("sp_SaveCargoEstrutura @idCargo = " + _cargo.IDCargo + ", @idPessoa = " + _pessoa.IDPessoa + "");
        }

        public void SaveEstruturaPrimeiroNivel(List<int> idsPessoaNew, int cargoId)
        {
            var pessoasFisicasAdded = new Pessoa().FindAllPessoaPrimeiroNivel(new Cargo(cargoId));
            var idsPessoaOld = new List<int>();
            pessoasFisicasAdded.ForEach(j => idsPessoaOld.Add((int)j.Id));

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

            new Pessoa().DeleteCargos(idsRemove);

            foreach (int id in idsAdd)
                this.Save(new Pessoa(id), new Cargo(cargoId));
        }

        public void SaveCargoEstrutura(int idPessoaPai, List<int> idsPessoaNew)
        {
            var pessoa = new Pessoa(idPessoaPai);
            pessoa.Transaction = this.Transaction;
            var pessoasJuridicasAdded = pessoa.GetCargoPessoasFilhas();
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

            pessoa.DeleteCargosFilhas(idPessoaPai, idsRemove);

            foreach (int id in idsAdd)
                this.Save(new Pessoa(id), new Pessoa(idPessoaPai));
        }
        #endregion
    }
    #endregion
}
