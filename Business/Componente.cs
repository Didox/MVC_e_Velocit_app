using System;
using Didox.DataBase;
using Didox.DataBase.Generics;
using NVelocity;
using System.IO;
using NVelocity.App;
using System.Collections;

namespace Didox.Business
{
    #region Class Componente

    [Table(Name = "EST_Componente")]
    public class Componente : CType
    {
        #region Construtores
        public Componente()
        {
            Velocity.Init();
        }

        public Componente(int? idComponente)
        {
            Velocity.Init();            
            this.IDComponente = idComponente;
        }

        public Componente(string descricao)
        {
            Velocity.Init();
            this.Descricao = descricao;
        }
        #endregion

        #region Destrutor
        ~Componente() { Dispose(); }
        #endregion

        #region Attributes
        #endregion

        #region Propriedades

        [Property(IsPk = true)]
        [Operations(UseSave = true, UseDelete = true, UseGet = true)]
        public int? IDComponente { get; set; }

        [Validate]
        [Property(IsField = true, Size = 150, Name = "dsComponente")]
        [Operations(UseSave = true, UseGet = true)]
        public string Descricao { get; set; }

        [Validate]
        [Property(IsField = true, Size = 80, Name = "ChaveComponente", DontUseLikeWithStrings=true)]
        [Operations(UseSave = true, UseGet = true)]
        public string Chave { get; set; }

        [Validate]
        [Property(IsField = true, IsText = true)]
        [Operations(UseSave = true)]
        public string Conteudo { get; set; }

        #endregion
        
        #region Methods

        public string Login()
        {
            var componente = new Componente();
            componente.Chave = "form_login";
            componente.Get();
            if (componente.Conteudo == null) componente.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            if (Session.Get("[Erro]Login") != null)
            {
                context.Put("erro", Session.Get("[Erro]Login").ToString());
                Session.Invalidate("[Erro]Login");
            }
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }
        
        public string PrimeiroAcesso()
        {
            var componente = new Componente();
            componente.Chave = "primeiro_acesso";
            componente.Get();
            if (componente.Conteudo == null) componente.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            var chaveAtivacaoCampanha = new ChaveAtivacaoCampanha(Campanha.Current());
            chaveAtivacaoCampanha.Get();
            context.Put("chaveAtivacaoCampanha", chaveAtivacaoCampanha);
            if (Session.Get("[Erro]PrimeiroAcesso") != null)
            {
                context.Put("erro", Session.Get("[Erro]PrimeiroAcesso").ToString());
                Session.Invalidate("[Erro]PrimeiroAcesso");
            }
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string AlterarSenha()
        {
            var componente = new Componente();
            componente.Chave = "alterar-senha";
            componente.Get();
            if (componente.Conteudo == null) componente.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            if (Session.Get("[MSG]AlterarSenha") != null)
            {
                context.Put("mensagem", Session.Get("[MSG]AlterarSenha").ToString());
                Session.Invalidate("[MSG]AlterarSenha");
            }
            if (Session.Get("[ERRO]AlterarSenha") != null)
            {
                context.Put("erro", Session.Get("[ERRO]AlterarSenha").ToString());
                Session.Invalidate("[ERRO]AlterarSenha");
            }
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string AlterarCadastro()
        {
            var componente = new Componente();
            componente.Chave = "alterar-cadastro";
            componente.Get();
            if (componente.Conteudo == null) componente.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            context.Put("usuario", Usuario.Current());
            context.Put("tipoInput", new TipoInput());

            if (Session.Get("[MSG]AlterarCadastro") != null)
            {
                context.Put("mensagem", Session.Get("[MSG]AlterarCadastro").ToString());
                Session.Invalidate("[MSG]AlterarCadastro");
            }
            if (Session.Get("[ERRO]AlterarCadastro") != null)
            {
                context.Put("erro", Session.Get("[ERRO]AlterarCadastro").ToString());
                Session.Invalidate("[ERRO]AlterarCadastro");
            }
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string MenuHorizontal()
        {
            var componente = new Componente();
            componente.Chave = "menu_horizontal";
            componente.Get();
            return HtmlMenu(componente);
        }

        public string Menu()
        {
            var componente = new Componente();
            componente.Chave = "menu";
            componente.Get();
            return HtmlMenu(componente);
        }

        private string HtmlMenu(Componente componente)
        {
            if (componente.Conteudo == null) componente.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("menu", Pagina.GetPaginasUsuario());
            context.Put("area", Pagina.GetAreaCorrente(false));
            context.Put("site", Pagina.Site());
            var pagina = Pagina.Current();
            if(pagina != null) context.Put("pagina_corrente", pagina);
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string EsqueciSenha()
        {
            var componente = new Componente();
            componente.Chave = "esqueci_senha";
            componente.Get();
            if (componente.Conteudo == null) componente.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("site", Pagina.Site());
            if (Session.Get("[EsqueciSenha]Login") != null)
            {
                context.Put("mensagem", Session.Get("[EsqueciSenha]Login").ToString());
                Session.Invalidate("[EsqueciSenha]Login");
            }
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        public string AdesaoCombos()
        {
            var componente = new Componente();
            componente.Chave = "adesao";
            componente.Get();
            if (componente.Conteudo == null) componente.Conteudo = "";
            var writer = new StringWriter();
            var context = new VelocityContext();
            context.Put("usuario", Usuario.Current());
            context.Put("site", Pagina.Site());
            Velocity.Evaluate(context, writer, "", componente.Conteudo);
            return writer.GetStringBuilder().ToString();
        }

        #endregion
    }
    #endregion
}
