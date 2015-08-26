using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class PessoaJuridica

    public class PessoaJuridica : CType
	{        
		#region Construtores
        public PessoaJuridica() { }
        
		public PessoaJuridica(int? idPessoaJuridica) 
		{
            //this.IdPessoa = idPessoaJuridica;
		}

        #endregion

        #region Destrutor
        ~PessoaJuridica() { Dispose(); }
        #endregion

        #region Propriedades
        public string CNPJ { get; set; }

        public new string InscricaoEstadual { get; set; }
        /*
        public string FormatedCNPJ
        {
            get
            {
                if (string.IsNullOrEmpty(CNPJ)) return null;
                if (CNPJ.Length < 14) return null;
                CNPJ = CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
                return Formatar(CNPJ, "##.###.###/####-##");
            }
            set
            {
                if (value.Length < 11) return;
                CNPJ = value.Replace(".", "").Replace("/", "").Replace("-", "");
            }
        }*/

        #endregion
    }
	#endregion
}
