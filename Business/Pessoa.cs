using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;
using System.Reflection;

namespace Didox.Business
{
    #region Class Pessoa


    [Table(Name = "NEG_Pessoa", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Pessoa : CType
    {
        #region Construtores
        public Pessoa()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Pessoa(int? idPessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDPessoa = idPessoa;
        }

        public Pessoa(string nome)
        {
            CarregarConnectionString(Cliente.Current());
            this.Nome = nome;
        }

        public Pessoa(Campanha campanha)
        {
            CarregarConnectionString(Cliente.Current());            
            this.campanha = campanha;
        }

        public Pessoa(string nome, TipoPessoa tipoPessoa)
        {
            CarregarConnectionString(Cliente.Current());
            this.Nome = nome;
            this.TipoPessoa = tipoPessoa;
        }

        #endregion

        #region Destrutor
        ~Pessoa() { Dispose(); }
        #endregion

        #region Attributes
        private Cargo cargo;
        private PessoaFisica pessoaFisica;
        private PessoaJuridica pessoaJuridica;
        
        private Email email;
        private LIType emails;

        private LIType telefones;
        private Telefone telefoneResidencial;
        private Telefone telefoneCelular;
        private Telefone telefoneComercial;
        private Telefone telefoneFax;

        private LIType enderecos;
        private Endereco enderecoResidencial;
        private Endereco enderecoComercial;

        private LIType estruturasHierarquia;
        private EstruturaPessoa estruturaPessoaHierarquia;

        private LIType estruturasCargo;
        private CargoEstruturaPessoa estruturaPessoaCargo;
        private Campanha campanha;

        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPessoa { get; set; }

        [Validate]
        [Property(IsField = true, Size=60)]
        [Operations(UseSave = true)]
        public string Nome { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseGet=true)]
        internal int? IDTipoPessoa { get; set; }
        public TipoPessoa TipoPessoa 
        {
            get { return (TipoPessoa)Enum.Parse(typeof(TipoPessoa), IDTipoPessoa.ToString()); }
            set { IDTipoPessoa = (int)value; }
        }

        public Cargo Cargo
        {
            get
            {
                if (cargo == null && TipoPessoa == TipoPessoa.Fisica)
                {
                    var cargoEstruturaPessoa = new CargoEstruturaPessoa();
                    cargoEstruturaPessoa.Pessoa = this;
                    cargoEstruturaPessoa.Get();

                    if (cargoEstruturaPessoa.IDCargoEstruturaPessoa != null)
                        if (cargoEstruturaPessoa.CargoEstrutura.IDCargoEstrutura != null)
                            if (cargoEstruturaPessoa.CargoEstrutura.Cargo.IDCargo != null)   
                                cargo = cargoEstruturaPessoa.CargoEstrutura.Cargo;
                }
                return this.cargo;
            }
        }

        public PessoaFisica Fisica
        {
            get
            {
                if (this.pessoaFisica == null)
                {
                    this.pessoaFisica = new PessoaFisica();
                    this.pessoaFisica.Pessoa = this;
                    this.pessoaFisica.Get();
                }
                return this.pessoaFisica;
            }
            set
            {
                this.pessoaFisica = value;
            }
        }

        public PessoaJuridica Juridica
        {
            get
            {
                if (this.pessoaJuridica == null)
                {
                    this.pessoaJuridica = new PessoaJuridica();
                    this.pessoaJuridica.Pessoa = this;
                    this.pessoaJuridica.Get();
                }
                return this.pessoaJuridica;
            }
            set
            {
                this.pessoaJuridica = value;
            }
        }

        public Email Email
        {
            get
            {
                if (this.email == null)
                {
                    this.email = new Email();
                    this.email.Pessoa = this;
                    this.email.Get();
                }
                return this.email;
            }
        }

        public LIType Emails
        {
            get
            {
                if (this.emails == null)
                    this.emails = new Email(this).Find();
                return this.emails;
            }
        }

        public EstruturaPessoa EstruturaPessoaHierarquia
        {
            get
            {
                if (this.estruturaPessoaHierarquia == null)
                {
                    this.estruturaPessoaHierarquia = new EstruturaPessoa();
                    this.estruturaPessoaHierarquia.Pessoa = this;
                    this.estruturaPessoaHierarquia.Get();
                }
                return this.estruturaPessoaHierarquia;
            }
        }

        public LIType EstruturasHierarquia
        {
            get
            {
                if (this.estruturasHierarquia == null)
                    this.estruturasHierarquia = new EstruturaPessoa(this).Find();
                return this.estruturasHierarquia;
            }
        }


        public CargoEstruturaPessoa EstruturaPessoaCargo
        {
            get
            {
                if (this.estruturaPessoaCargo == null)
                {
                    this.estruturaPessoaCargo = new CargoEstruturaPessoa();
                    this.estruturaPessoaCargo.Pessoa = this;
                    this.estruturaPessoaCargo.Get();
                }
                return this.estruturaPessoaCargo;
            }
        }

        public LIType EstruturasCargo
        {
            get
            {
                if (this.estruturasCargo == null)
                    this.estruturasCargo = new CargoEstruturaPessoa(this).Find();
                return this.estruturasCargo;
            }
        }

        public LIType Telefones
        {
            get
            {
                if (this.telefones == null)
                    this.telefones = new Telefone(this).Find();
                return this.telefones;
            }
        }

        public LIType Enderecos
        {
            get
            {
                if (this.enderecos == null)
                    this.enderecos = new Endereco(this).Find();
                return this.enderecos;
            }
        }

        public Telefone TelefoneResidencial
        {
            get
            {
                if (this.telefoneResidencial == null)
                {
                    this.telefoneResidencial = new Telefone();
                    this.telefoneResidencial.TipoTelefone = TipoTelefone.Residencial();
                    this.telefoneResidencial.Pessoa = this;
                    this.telefoneResidencial.Get();
                }
                return this.telefoneResidencial;
            }
        }

        public Telefone TelefoneCelular
        {
            get
            {
                if (this.telefoneCelular == null)
                {
                    this.telefoneCelular = new Telefone();
                    this.telefoneCelular.TipoTelefone = TipoTelefone.Celular();
                    this.telefoneCelular.Pessoa = this;
                    this.telefoneCelular.Get();
                }
                return this.telefoneCelular;
            }
        }

        public Telefone TelefoneComercial
        {
            get
            {
                if (this.telefoneComercial == null)
                {
                    this.telefoneComercial = new Telefone();
                    this.telefoneComercial.TipoTelefone = TipoTelefone.Comercial();
                    this.telefoneComercial.Pessoa = this;
                    this.telefoneComercial.Get();
                }
                return this.telefoneComercial;
            }
        }

        public Telefone TelefoneFax
        {
            get
            {
                if (this.telefoneFax == null)
                {
                    this.telefoneFax = new Telefone();
                    this.telefoneFax.TipoTelefone = TipoTelefone.Fax();
                    this.telefoneFax.Pessoa = this;
                    this.telefoneFax.Get();
                }
                return this.telefoneFax;
            }
        }

        public Endereco EnderecoResidencial
        {
            get
            {
                if (this.enderecoResidencial == null)
                {
                    this.enderecoResidencial = new Endereco();
                    this.enderecoResidencial.TipoEndereco = TipoEndereco.Residencial();
                    this.enderecoResidencial.Pessoa = this;
                    this.enderecoResidencial.Get();
                }
                return this.enderecoResidencial;
            }
        }

        public Endereco EnderecoComercial
        {
            get
            {
                if (this.enderecoComercial == null)
                {
                    this.enderecoComercial = new Endereco();
                    this.enderecoComercial.TipoEndereco = TipoEndereco.Comercial();
                    this.enderecoComercial.Pessoa = this;
                    this.enderecoComercial.Get();
                }
                return this.enderecoComercial;
            }
        }

        public string TVI
        {
            get
            {
                if (!CType.Exist(this)) return null;
                var tviDoc = GetDocumentoTVI();
                var TVIInterno = tviDoc.DescDocumento;
                if (string.IsNullOrEmpty(TVIInterno)) return null;
                if (TVIInterno.Length < 11) return null;
                TVIInterno = TVIInterno.Replace(".", "").Replace("-", "");
                return Funcoes.Formatar(TVIInterno, @"0000\.0000000-00");
            }
        }

        public Campanha Campanha
        {
            get
            {
                if (this.campanha == null)
                {
                    this.campanha = Campanha.Current();
                }
                return this.campanha;
            }
        }

        public Cliente Cliente
        {
            get
            {
                if (CType.Exist(this.Campanha)) return this.Campanha.Programa.Cliente;
                return Cliente.Current();
            }
        }

        #endregion

        #endregion

        #region "Methods"
        public List<int> GetListIdEstruturaHierarquia()
        {
            var ids = new List<int>();
            if (this.EstruturaPessoaHierarquia != null)
            {
                var estrutura = this.EstruturaPessoaHierarquia.Estrutura;
                foreach (PropertyInfo pi in estrutura.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (pi.Name.ToLower().IndexOf("idpessoanivel") != -1)
                    {
                        var value = pi.GetValue(estrutura, null);
                        if (value != null) ids.Add((int)value);
                    }
                }
            }
            return ids;
        }

        public override void Save()
        {            
            base.Save();

            var documentoTVI = new Documento();
            documentoTVI.Transaction = this.Transaction;
            documentoTVI.TipoDocumento = TipoDocumento.TVI();
            documentoTVI.Pessoa = this;
            documentoTVI.Get();
            if (documentoTVI.IDDocumento == null)
            {
                documentoTVI.DescDocumento = CreateTVI();
                documentoTVI.Save();
            }
        }

        private string CreateTVI()
        {
            var cliente = Cliente;
            if (cliente == null) throw new TradeVisionValidationError("Cliente não encontrado");
            string codCliente = cliente.IDCliente.ToString();
            string codPessoa = this.IDPessoa.ToString();
            while (codCliente.Length < 4) codCliente = "0" + codCliente;
            while (codPessoa.Length < 7) codPessoa = "0" + codPessoa;

            string TVI = codCliente + codPessoa;
            int i;
            long codigo, soma;
            i = 2;
            soma = 0;
            codigo = long.Parse(TVI);

            while (codigo > 0)
            {
                soma += i * (codigo % 10);
                codigo = codigo / 10;
                i++;
            }

            long tviDigito = ((soma * 10) % 11) % 10;
            TVI += tviDigito;
            return TVI;
        }

        public Tabela GetTabelaDinamica()
        {
            if (CType.Exist(this.Fisica))
            {
                var tabela = this.Fisica.Tabela;
                if (CType.Exist(tabela)) return tabela;
            }

            if (CType.Exist(this.Juridica))
            {
                var tabela = this.Juridica.Tabela;
                if (CType.Exist(tabela)) return tabela;
            }

            return null;
        }

        public bool PessoaFisica()
        {
            return TipoPessoa == TipoPessoa.Fisica;
        }

        public bool PessoaJuridica()
        {
            return TipoPessoa == TipoPessoa.Juridica;
        }

        public LIType GetAllPorHierarquia(Hierarquia hierarquia)
        {
            return this.FindBySql("Sp_GetAllPessoaPorHierarquia @idHierarquia = " + hierarquia.IDHierarquia);
        }

        public bool EstaNestaHierarquia(int hierarquiaId)
        {
            return new DataBase.Pessoa().EstaNestaHierarquia(this, (int)this.IDPessoa, hierarquiaId);
        }

        public LIType GetPrimeirosNosPessoaHierarquia()
        {
            return this.FindBySql("Sp_GetPrimeirosNosPessoa");
        }

        public LIType GetHierarquiaPessoasFilhas()
        {
            return this.FindBySql("Sp_GetAllPessoaPorPessoaPai @idPessoaPai = " + this.IDPessoa);
        }

        public bool EFilhoDaPessoa(int idPessoaPai)
        {
            return new DataBase.Pessoa().EFilhoDaPessoa(this, idPessoaPai, (int)this.IDPessoa);
        }

        public Hierarquia GetHierarquia()
        {
            var hierarquias = new Hierarquia().FindBySql("sp_GetHierarquiaPorPessoa " + this.IDPessoa);
            if (hierarquias.Count > 0) return (Hierarquia)hierarquias[0];
            return null;
        }

        public Cargo GetCargo()
        {
            var cargos = new Cargo().FindBySql("sp_GetCargoPorPessoa " + this.IDPessoa);
            if (cargos.Count > 0) return (Cargo)cargos[0];
            return null;
        }

        public LIType GetPrimeirosNosPessoaCargo()
        {
            return this.FindBySql("Sp_GetPrimeirosNosCargoPessoa");
        }

        public bool EFilhoDaPessoaCargo(int idPessoaPai)
        {
            return new DataBase.Pessoa().EFilhoDaPessoaCargo(this, idPessoaPai, (int)this.IDPessoa);
        }

        public LIType GetCargoPessoasFilhas()
        {
            return this.FindBySql("Sp_GetAllCargoPessoasFilhasPai @idPessoaPai = " + (int)this.IDPessoa);
        }

        public LIType GetAllPorCargo(Cargo cargo)
        {
            return this.FindBySql("Sp_GetAllPessoaPorCargo @idCargo = " + cargo.IDCargo);
        }

        public bool EstaNesteCargo(int cargoId)
        {
            return new DataBase.Pessoa().EstaNesteCargo(this, (int)this.IDPessoa, cargoId);
        }

        public Pessoa GetPessoaByCampanha(Usuario usuario)
        {
            return (Pessoa)new DataBase.Pessoa().GetPessoaCampanha(this, (int)usuario.IDUsuario, (int)Campanha.IDCampanha);
        }

        public bool ExisteFisica()
        {
            return CType.Exist(this.Fisica);
        }

        public Documento GetDocumentoCPF()
        {
            return GetDocumento(TipoDocumento.CPF());
        }

        public Documento GetDocumentoTVI()
        {
            return GetDocumento(TipoDocumento.TVI());
        }

        public Documento GetDocumentoRg()
        {
            return GetDocumento(TipoDocumento.RG());
        }

        public Documento GetDocumentoCNPJ()
        {
            return GetDocumento(TipoDocumento.CNPJ());
        }

        public Documento GetDocumento(TipoDocumento tipoDocumento)
        {
            var documento = new Documento();
            documento.TipoDocumento = tipoDocumento;
            documento.Pessoa = this;
            documento.Get();
            return documento;
        }

        public LIType FindTop(string data, TipoPessoa tipoPessoa)
        {
            if (tipoPessoa == TipoPessoa.Juridica)
                return this.FindBySql("findPessoasTipoJuridicaTop '" + data + "'");
            else return this.FindBySql("findPessoasTipoFisicaTop '" + data + "'");
        }

        public void DeleteHierarquiasFilhas(int IdPessoa, List<string> idsPessoaRemove)
        {
            if (idsPessoaRemove.Count > 0)
                this.ExecSql("sp_DeleteTodoFilhos '" + IdPessoa + "', '" + string.Join(",", idsPessoaRemove.ToArray()) + "'");
        }

        public void DeleteCargosFilhas(int IdPessoa, List<string> idsPessoaRemove)
        {
            if (idsPessoaRemove.Count > 0)
                this.ExecSql("sp_DeleteFilhosCargo '" + IdPessoa + "', '" + string.Join(",", idsPessoaRemove.ToArray()) + "'");
        }

        public LIType FindAllPessoaPrimeiroNivel(Hierarquia hierarquia)
        {
            return this.FindBySql("sp_findAllPessoaPrimeiroNivel " + hierarquia.IDHierarquia);
        }

        public LIType FindAllPessoaPrimeiroNivel(Cargo cargo)
        {
            return this.FindBySql("sp_findAllPessoaPrimeiroNivelCargo " + cargo.IDCargo);
        }

        internal void DeleteHierarquias(List<string> idsPessoaRemove)
        {
            if (idsPessoaRemove.Count > 0)
                this.ExecSql("sp_DeletePrimeiroNivelHierarquia '" + string.Join(",", idsPessoaRemove.ToArray()) + "'");
        }

        internal void DeleteCargos(List<string> idsPessoaRemove)
        {
            if (idsPessoaRemove.Count > 0)
                this.ExecSql("sp_DeletePrimeiroNivelCargo '" + string.Join(",", idsPessoaRemove.ToArray()) + "'");
        }
    }

    #endregion

    #region Enum TipoCampo

    public enum TipoPessoa
    {
        Juridica = 1,
        Fisica = 2
    }

    #endregion
}