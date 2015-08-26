using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

namespace Didox.Business
{
    #region Class Tipo


    [Table(Name = "SYS_Tipos")]
    public class Tipo : CType
    {
        #region Construtores
        public Tipo() { }

        public Tipo(int? idTipo)
        {
            this.IDTipo = idTipo;
        }

        #endregion

        #region Destrutor
        ~Tipo() { Dispose(); }
        #endregion

        #region Attributes
        private TipoTabelaColuna tipoTabelaColuna;
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTipo { get; set; }

        [Validate]
        [Property(IsField = true)]
        [Operations(UseSave = true)]
        public string Nome { get; set; }

        [Validate]
        [Property(IsField = true, IsForeignKey = true)]
        [Operations(UseSave = true, UseGet = true)]
        public int? IDTipoTabelaColuna { get; set; }
        public TipoTabelaColuna TipoTabelaColuna
        {
            get
            {
                if (this.tipoTabelaColuna == null)
                {
                    this.tipoTabelaColuna = new TipoTabelaColuna();
                    this.tipoTabelaColuna.IDTipoTabelaColuna = this.IDTipoTabelaColuna;
                }

                if (!this.tipoTabelaColuna.IsFull)
                {
                    this.tipoTabelaColuna.Transaction = this.Transaction;
                    this.tipoTabelaColuna.Get();
                }

                return this.tipoTabelaColuna;
            }
            set
            {
                this.tipoTabelaColuna = value;
                if (value != null) this.IDTipoTabelaColuna = value.IDTipoTabelaColuna;
            }
        }

        #endregion

        #region "Methods"

        public static void Save(IType objiTypeValues, IType iType, string namePropertyId, string namePropertyName)
        {
            var tipoTabelaColuna = new TipoTabelaColuna(iType);
            try
            {
                tipoTabelaColuna.IsTransaction = true;
                tipoTabelaColuna.Get();
                tipoTabelaColuna.Save();

                var tipo = new Tipo();
                tipo.Transaction = tipoTabelaColuna.Transaction;
                tipo.IDTipo = (int?)objiTypeValues.GetType().GetProperty(namePropertyId).GetValue(objiTypeValues, null);
                tipo.Nome = (string)objiTypeValues.GetType().GetProperty(namePropertyName).GetValue(objiTypeValues, null);
                tipo.TipoTabelaColuna = tipoTabelaColuna;
                tipo.Save();

                tipoTabelaColuna.Commit();
            }
            catch (Exception err)
            {
                tipoTabelaColuna.Rollback();
                throw err;
            }
        }

        public static void Get(IType objiTypeValues, IType iType, string namePropertyId, string namePropertyName)
        {
            var pTipo = objiTypeValues.GetType().GetProperty(namePropertyId);
            var pName = objiTypeValues.GetType().GetProperty(namePropertyName);

            if (pTipo.GetValue(objiTypeValues, null) == null) throw new TradeVisionValidationError("IDTipo obrigatório para a ação GET");
            var tipoTabelaColuna = new TipoTabelaColuna(iType);
            tipoTabelaColuna.Get();
            if (tipoTabelaColuna.IDTipoTabelaColuna != null)
            {
                var tipo = new Tipo();
                tipo.TipoTabelaColuna = tipoTabelaColuna;
                tipo.IDTipo = (int?)pTipo.GetValue(objiTypeValues, null);
                tipo.Get();

                if (tipo.IDTipo != null)
                    pName.SetValue(objiTypeValues, objiTypeValues.PrepareValueProperty(tipo.Nome, pName.PropertyType), null);
            }
        }

        public static LIType Find(IType objiTypeValues, IType iType, string namePropertyId, string namePropertyName)
        {
            var tipoTabelaColuna = new TipoTabelaColuna(iType);
            tipoTabelaColuna.Get();
            var tiposCType = new LIType();
            if (tipoTabelaColuna.IDTipoTabelaColuna != null)
            {
                var tipoFind = new Tipo();
                tipoFind.TipoTabelaColuna = tipoTabelaColuna;
                var tipos = tipoFind.Find();
                foreach (var t in tipos)
                {
                    var tipo = (Tipo)t;
                    var cType = (IType)Activator.CreateInstance(objiTypeValues.GetType());
                    var pTipo = cType.GetType().GetProperty(namePropertyId);
                    var pName = cType.GetType().GetProperty(namePropertyName);
                    pTipo.SetValue(cType, cType.PrepareValueProperty(tipo.IDTipo, pTipo.PropertyType), null);
                    pName.SetValue(cType, cType.PrepareValueProperty(tipo.Nome, pName.PropertyType), null);
                    tiposCType.Add(cType);
                }
            }

            return tiposCType;
        }

        public static void Delete(IType objiTypeValues, IType iType, string namePropertyId)
        {
            var pTipo = objiTypeValues.GetType().GetProperty(namePropertyId);

            if (pTipo.GetValue(objiTypeValues, null) == null) throw new TradeVisionValidationError("IDTipo obrigatório para a ação GET");
            var tipoTabelaColuna = new TipoTabelaColuna(iType);
            tipoTabelaColuna.Get();
            if (tipoTabelaColuna.IDTipoTabelaColuna != null)
            {
                var tipo = new Tipo();
                tipo.TipoTabelaColuna = tipoTabelaColuna;
                tipo.IDTipo = (int?)pTipo.GetValue(objiTypeValues, null);
                tipo.Delete();
            }
        }
        
        #endregion
    }

    #endregion
}