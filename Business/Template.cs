using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using NVelocity;
using System.IO;
using NVelocity.App;
using System.Collections;

namespace Didox.Business
{
    #region Class Template

    [Table(Name = "EST_Template", DynamicConnectionString = true, ConnectionString = "digitalgest")]
    public class Template : CType
    {
        #region Construtores
        public Template()
        {
            CarregarConnectionString(Cliente.Current());
        }

        public Template(int? idTemplate)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDTemplate = idTemplate;
        }

        public Template(int? idTemplate, string conteudo)
        {
            CarregarConnectionString(Cliente.Current());
            this.IDTemplate = idTemplate;
            this.Conteudo = conteudo;
        }

        public Template(string descricao)
        {
            CarregarConnectionString(Cliente.Current());
            this.Descricao = descricao;
        }
        #endregion

        #region Destrutor
        ~Template() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDTemplate { get; set; }

        [Validate]
        [Property(IsField = true, Size=50, Name = "dsTemplate")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, Size = 80, Name = "ChaveTemplate",DontUseLikeWithStrings=true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Chave { get; set; }

        [Validate]
        [Property(IsField = true, Name = "ConteudoTemplate", IsText = true)]
        [Operations(UseSave = true)]
        public string Conteudo { get; set; }

        #endregion
        
        #region Methods

        public string Include(string nome)
        {
            Velocity.Init();
            var template = new Template();
            template.Chave = nome;
            template.Get();
            if (template.Conteudo == null) template.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("template", new Template());
            context.Put("componente", new Componente());
            var usuario = Usuario.Current();
            if (usuario != null) context.Put("usuario", usuario);
            context.Put("site", Pagina.Site());
            context.Put("area", Pagina.GetAreaCorrente(false));
            Velocity.Evaluate(context, writer, "", template.Conteudo.Replace("&#39;", "'"));
            return writer.GetStringBuilder().ToString();
        }

        #endregion

        public string GetConteudoRenderizado()
        {
            return GetConteudoRenderizado(null);
        }

        public string GetConteudoRenderizado(Hashtable parans)
        {
            Velocity.Init();
            var context = new VelocityContext(parans);
            context.Put("template", new Template());
            context.Put("componente", new Componente());
            context.Put("site", Pagina.Site());
            context.Put("area", Pagina.GetAreaCorrente(false));
            var usuario = Usuario.Current();
            if (usuario != null) context.Put("usuario", usuario);
            var writer = new StringWriter();
            Velocity.Evaluate(context, writer, "", this.Conteudo.Replace("&#39;", "'"));
            return writer.GetStringBuilder().ToString();
        }
    }
    #endregion
}
