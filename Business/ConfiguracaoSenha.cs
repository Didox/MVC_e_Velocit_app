using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Web.Security;
using System.Security.Cryptography;

namespace Didox.Business
{
    #region Class ConfiguracaoSenha

    [Table(Name = "EST_ConfiguracaoSenha")]
    public class ConfiguracaoSenha : CType
    {
        #region Construtores
        public ConfiguracaoSenha() { }

        public ConfiguracaoSenha(int? idConfiguracaoSenha)
        {
            this.IDConfiguracaoSenha = idConfiguracaoSenha;
        }

        #endregion

        #region Destrutor
        ~ConfiguracaoSenha() { Dispose(); }
        #endregion

        #region Attributes
        private Cliente cliente;
        private Programa programa;
        private Campanha campanha;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDConfiguracaoSenha { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCliente { get; set; }

        public Cliente Cliente
        {
            get
            {
                if (this.cliente == null)
                {
                    this.cliente = new Cliente();
                    this.cliente.IDCliente = this.IDCliente;
                }

                if (!this.cliente.IsFull)
                {
                    this.cliente.Transaction = this.Transaction;
                    this.cliente.Get();
                }

                return this.cliente;
            }
            set
            {
                this.cliente = value;
                if (value != null) this.IDCliente = value.IDCliente;
            }
        }

        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPrograma { get; set; }

        public Programa Programa
        {
            get
            {
                if (this.programa == null)
                {
                    this.programa = new Programa();
                    this.programa.IDPrograma = this.IDPrograma;
                }

                if (!this.programa.IsFull)
                {
                    this.programa.Transaction = this.Transaction;
                    this.programa.Get();
                }

                return this.programa;
            }
            set
            {
                this.programa = value;
                if (value != null) this.IDPrograma = value.IDPrograma;
            }
        }

        [Property(IsField = true, IsForeignKey = true)]
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
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public bool? SenhaCriptografada { get; set; }

        #endregion

        #region Methods
        public static string GetSenhaCripto(string senha)
        {
            return senha;


            var cry = new Cryption();                
            if (string.IsNullOrEmpty(senha)) return senha;
            if (ConfiguracaoSenha.SenhaEstaCriptografada())
            {
                senha = cry.EncryptString(senha);
                //senha = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "sha1");
            }
            else
            {
                senha = cry.EncryptString(senha);
            }

            return senha;
        }

        public static string GetSenhaDescripto(string senha)
        {
            return senha;


            if (string.IsNullOrEmpty(senha)) return senha;
            //if (!ConfiguracaoSenha.SenhaEstaCriptografada())
            //{
                var cry = new Cryption();
                senha = cry.DecryptString(senha);
                //senha = FormsAuthentication.HashPasswordForStoringInConfigFile(senha, "sha1");
            //}

            return senha;
        }

        public static bool SenhaEstaCriptografada()
        {
            var configuracaoSenha = new ConfiguracaoSenha();
            configuracaoSenha.Programa = Programa.Current();
            configuracaoSenha.Cliente = Cliente.Current();
            configuracaoSenha.Campanha = Campanha.Current();
            configuracaoSenha.Get();
            if (configuracaoSenha.IDConfiguracaoSenha != null && (bool)configuracaoSenha.SenhaCriptografada)
                return true;
            return false;
        }
        #endregion
    }
    #endregion
}
