using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class CredencialPessoa


    [Table(Name = "SEG_CredencialPessoa", DynamicConectionString = true)]
    public class CredencialPessoa : CType
    {
        #region Construtores
        public CredencialPessoa()
        {
            CarregarConectionString(Cliente.Current());
        }

        public CredencialPessoa(int? idCredencialPessoa)
        {
            CarregarConectionString(Cliente.Current());
            this.IDCredencialPessoa = idCredencialPessoa;
        }

        #endregion

        #region Destrutor
        ~CredencialPessoa() { Dispose(); }
        #endregion

        #region Attributes
        private Estrutura estrutura;
        private Campanha campanha;
        private Usuario usuario;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCredencialPessoa { get; set; }

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
        [Property(IsField = true, Name="IDPessoa")]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDEstrutura { get; set; }

        public Estrutura Estrutura
        {
            get
            {
                if (this.estrutura == null)
                {
                    this.estrutura = new Estrutura();
                    this.estrutura.IDEstrutura = this.IDEstrutura;
                }

                if (!this.estrutura.IsFull)
                {
                    this.estrutura.Transaction = this.Transaction;
                    this.estrutura.Get();
                }

                return this.estrutura;
            }
            set
            {
                this.estrutura = value;
                if (value != null) this.IDEstrutura = value.IDEstrutura;
            }
        }

        #endregion

        public List<ViewCredencialPessoaNivel> ListaByHierarquia(int idHierarquia)
        {
            if (Campanha.Current() == null) return null;
            var idcampanha = (int)Campanha.Current().IDCampanha;
            List<ViewCredencialPessoaNivel> viewCredencialPessoaNiveis = new List<ViewCredencialPessoaNivel>();
            var iViewCredencialPessoaNivel = new DataBase.CredencialPessoa().ListaByHierarquia(new ViewCredencialPessoaNivel(), idHierarquia, idcampanha);
            iViewCredencialPessoaNivel.ForEach(o => viewCredencialPessoaNiveis.Add(((ViewCredencialPessoaNivel)o)));
            return viewCredencialPessoaNiveis;
        }
    }
    #endregion
}