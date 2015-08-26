using System;
using Didox.DataBase;
using Didox.DataBase.Generics;

namespace Didox.Business
{
    #region Class Campanha

    [Table(Name = "NEG_Campanha")]
    public class Campanha : CType
    {
        #region Construtores
        public Campanha() { }

        public Campanha(int? idCampanha)
        {
            this.IDCampanha = idCampanha;
        }

        public Campanha(string slug)
        {
            this.Slug = slug;
        }

        public Campanha(Programa programa)
        {
            this.Programa = programa;
        }

        #endregion

        #region Destrutor
        ~Campanha() { Dispose(); }
        #endregion

        #region Attributes
        private Programa programa;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDCampanha { get; set; }

        [Validate]
        [Property(IsField = true, Size = 100, Name = "dsCampanha")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseDelete=true, UseGet=true)]
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
                this.IDPrograma = value.IDPrograma;
            }
        }

        [Validate]
        [Property(IsField = true, Size = 20, Name = "SlugCampanha", DontUseLikeWithStrings = true)]
        [Operations(UseSave = true, UseGet=true)]
        public string Slug { get; set; }

        [Validate]
        [Property(IsField = true, Name = "dtInicio")]
        [Operations(UseSave = true)]
        internal DateTime? DataInicio { get; set; }
        public string DataInicioFormatada
        {
            get { return ((DateTime)DataInicio).ToString("dd/MM/yyyy"); }
            set { DataInicio = DateTime.Parse(value); }
        }

        [Validate]
        [Property(IsField = true, Name = "dtFim")]
        [Operations(UseSave = true)]
        internal DateTime? DataFim { get; set; }
        public string DataFimFormatada
        {
            get { return ((DateTime)DataFim).ToString("dd/MM/yyyy"); }
            set { DataFim = DateTime.Parse(value); }
        }

        #endregion
        
        #region Methods
        #endregion

        public void BuscaAdicionaCurrent()
        {
            if (this.IDCampanha == null && string.IsNullOrEmpty(this.Slug)) throw new TradeVisionError("Objeto Campanha não pode ser vazio.");
            this.Get();
            if (this.IDCampanha == null) return;
            Cookie.Add(KeyCampanha(), this.IDCampanha.ToString());
        }

        public static void Dispose()
        {
            Cookie.Invalidate(KeyCampanha());
        }

        public static Campanha Current()
        {
            string cookie = Cookie.Get(KeyCampanha());
            int idCampanha = 0;
            if (!int.TryParse(cookie, out idCampanha)) return null;
            if (idCampanha == 0) return null;

            var campanha = new Campanha(idCampanha);
            campanha.Get();
            return campanha;
        }

        private static string KeyCampanha()
        {
            return typeof(Campanha).Name + "Access";
        }
    }
    #endregion
}
