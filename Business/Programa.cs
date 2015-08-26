using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class Programa

    [Table(Name = "NEG_Programa")]
    public class Programa : CType
    {
        #region Construtores
        public Programa() { }

        public Programa(int? idPrograma)
        {
            this.IDPrograma = idPrograma;
        }

        public Programa(string slug)
        {
            this.Slug = slug;
        }

        public Programa(Cliente cliente)
        {
            this.Cliente = cliente;
        }

        #endregion

        #region Destrutor
        ~Programa() { Dispose(); }
        #endregion

        #region Attributes
        private Cliente cliente;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDPrograma { get; set; }

        [Validate]
        [Property(IsField = true, Size = 100, Name = "dsPrograma")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true, UseDelete=true, UseGet=true)]
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
                this.IDCliente = value.IDCliente;
            }
        }

        [Validate]
        [Property(IsField = true, Size = 20, Name = "SlugPrograma", DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet=true)]
        public string Slug { get; set; }

        #endregion
        
        #region Methods
        #endregion

        public void BuscaAdicionaCurrent()
        {
            if (this.IDPrograma == null && string.IsNullOrEmpty(this.Slug)) throw new TradeVisionError("Objeto Programa não pode ser vazio.");
            this.Get();
            if (this.IDPrograma == null) return;
            Cookie.Add(KeyPrograma(), this.IDPrograma.ToString());
        }

        public static void Dispose()
        {
            Cookie.Invalidate(KeyPrograma());
        }

        public static Programa Current()
        {
            string cookie = Cookie.Get(KeyPrograma());
            int idPrograma = 0;
            if (!int.TryParse(cookie, out idPrograma)) return null;
            if (idPrograma == 0) return null;

            var programa = new Programa(idPrograma);
            programa.Get();
            return programa;
        }

        private static string KeyPrograma()
        {
            return typeof(Programa).Name + "Access";
        }
    }
    #endregion
}
