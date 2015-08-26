using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class UsuarioPessoa


    [Table(Name = "SEG_CredencialPessoa", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class UsuarioPessoa : CType
    {
        #region Construtores
        public UsuarioPessoa()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public UsuarioPessoa(int? idUsuarioPessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDUsuarioPessoa = idUsuarioPessoa;
        }

        #endregion

        #region Destrutor
        ~UsuarioPessoa() { Dispose(); }
        #endregion

        #region Attributes
        private Pessoa pessoa;
        private Campanha campanha;
        private Usuario usuario;
        #endregion

        #region Propriedades

        [Property(IsPk = true, Name = "IDCredencialPessoa")]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDUsuarioPessoa { get; set; }

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

        #endregion
        /*
        public List<ViewCredencialPessoaNivel> ListaByHierarquia(int idHierarquia)
        {
            if (Campanha.Current() == null) return null;
            var idcampanha = (int)Campanha.Current().IDCampanha;
            List<ViewCredencialPessoaNivel> viewCredencialPessoaNiveis = new List<ViewCredencialPessoaNivel>();
            var iViewCredencialPessoaNivel = new DataBase.CredencialPessoa().ListaByHierarquia(new ViewCredencialPessoaNivel(), idHierarquia, idcampanha);
            iViewCredencialPessoaNivel.ForEach(o => viewCredencialPessoaNiveis.Add(((ViewCredencialPessoaNivel)o)));
            return viewCredencialPessoaNiveis;
        }*/
    }
    #endregion
}