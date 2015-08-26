using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class Cliente

    [Table(Name = "NEG_Cliente")]
    public class Cliente : CType
    {
        #region Construtores
        public Cliente() { }

        public Cliente(int? idCliente)
        {
            this.IDCliente = idCliente;
        }

        public Cliente(string slug)
        {
            this.Slug = slug;
        }

        #endregion

        #region Destrutor
        ~Cliente() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCliente { get; set; }

        [Validate]
        [Property(IsField = true, Size = 20, Name = "dsCliente")]
        [Operations(UseSave = true, UseGet = true)]
        public string Nome { get; set; }

        [Validate]
        [Property(IsField = true, Size = 30, Name = "IPServer")]
        [Operations(UseSave = true)]
        public string IPServidor { get; set; }

        [Validate]
        [Property(IsField = true, Size = 50)]
        [Operations(UseSave = true)]
        public string DBName { get; set; }

        [Validate]
        [Property(IsField = true, Size = 50)]
        [Operations(UseSave = true)]
        public string DBUser { get; set; }

        [Validate]
        [Property(IsField = true, Size = 10)]
        [Operations(UseSave = true)]
        public string DBPassword { get; set; }

        [Validate]
        [Property(IsField = true, Size = 20, Name = "SlugCliente", DontUseLikeWithStrings=true)]
        [Operations(UseSave = true, UseGet=true)]
        public string Slug { get; set; }

        #endregion
        
        #region Methods
        #endregion

        public void BuscaAdicionaCurrent()
        {
            if (this.IDCliente == null && string.IsNullOrEmpty(this.Slug)) throw new TradeVisionError("Objeto Cliente não pode ser vazio.");
            this.Get();
            if (this.IDCliente == null) return;
            Cookie.Add(KeyCliente(), this.IDCliente.ToString());
        }

        public static Cliente Current()
        {
            string cookie = Cookie.Get(KeyCliente());
            int idCliente = 0;
            if (!int.TryParse(cookie, out idCliente)) return null;
            if (idCliente == 0) return null;

            var cliente = new Cliente(idCliente);
            cliente.Get();
            return cliente;
        }

        public static void Dispose()
        {
            Cookie.Invalidate(KeyCliente());
        }

        private static string KeyCliente()
        {
            return typeof(Cliente).Name + "Access";
        }

        public string ConnectionString()
        {
            if (this.IDCliente == null) throw new TradeVisionError404("Cliente não encontrado.");
            return "User ID=" + this.DBUser + ";Initial Catalog=" + this.DBName + ";Data Source=" + this.IPServidor + ";Password=" + this.DBPassword;
        }
    }
    #endregion
}
