using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class PessoaFisica


    [Table(Name = "BAS_PessoaFisica", DynamicConectionString = true)]
    public class PessoaFisica : CType
	{        
		#region Construtores
        public PessoaFisica()
        {
            CarregarConectionString(Cliente.Current());
        }
        
		public PessoaFisica(int? idPessoaFisica) 
		{
            CarregarConectionString(Cliente.Current());
            this.IdPessoaFisica = idPessoaFisica;
		}

        #endregion

        #region Destrutor
        ~PessoaFisica() { Dispose(); }
        #endregion

        #region Attributes
        private Usuario usuario;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IdPessoaFisica { get; set; }

        [Validate]
        [Property(IsField = true)]
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
        [Property(IsField = true, Name = "cpfPessoaFisica")]
        [Operations(UseSave = true, UseGet = true)]
        public string CPF { get; set; }

        [Validate]
        [Property(IsField = true, Size=10, Name = "rgPessoaFisica")]
        [Operations(UseSave = true)]        
        public string RG { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dtPessoaFisica")]
        [Operations(UseSave = true)]  
        public DateTime DtNasc { get; set; }

        [Validate]
        [Property(IsField = true, Name = "sexoPessoaFisica")]
        [Operations(UseSave = true)]
        public TipoSexo Sexo { get; set; }

        public string CPFFormatado
        {
            get
            {
                if (CPF == null) return string.Empty;
                if (CPF.Length < 11) return string.Empty;
                return Funcoes.Formatar(CPF.ToString(), "###.###.###-##");
            }
            set
            {
                if (value.Length < 11) return;
                CPF = value.Replace(".", "").Replace("-", "");
            }
        }

        #endregion
    }
	#endregion

    #region "Tipo de Sexo"
    public enum TipoSexo
    {
        Feminino = 0,
        Masculino = 1
    }
	#endregion
}
