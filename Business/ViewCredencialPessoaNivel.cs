using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class Grupo

    [Table(DynamicConectionString = true, ConnectionString = "digitalgest")]
    public class ViewCredencialPessoaNivel : CType
    {
        #region Construtores
        public ViewCredencialPessoaNivel()
        {
            CarregarConectionString(Cliente.Current());
        }

        #endregion

        #region Destrutor
        ~ViewCredencialPessoaNivel() { Dispose(); }
        #endregion

        #region Attributes
        private Estrutura estrutura;
        private Usuario usuario;
        private Subnivel subnivel;
        private Hierarquia hierarquia;
        #endregion

        #region Propriedades

        [Property(IsField = true)]
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
        public int? IDSubnivel { get; set; }

        public Subnivel Subnivel
        {
            get
            {
                if (this.subnivel == null)
                {
                    this.subnivel = new Subnivel();
                    this.subnivel.IDSubnivel = this.IDSubnivel;
                }

                if (!this.subnivel.IsFull)
                {
                    this.subnivel.Transaction = this.Transaction;
                    this.subnivel.Get();
                }

                return this.subnivel;
            }
            set
            {
                this.subnivel = value;
                if (value != null) this.IDSubnivel = value.IDSubnivel;
            }
        }

        [Property(IsField = true, Name="idPessoa")]
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

        [Property(IsField = true, Name="idCredencial")]
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

                if (!this.usuario.IsFull && this.usuario.IDUsuario != null)
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
        
        #region Methods

        #endregion
    }
    #endregion
}
