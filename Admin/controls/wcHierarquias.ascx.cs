using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;
using System.Web.UI.MobileControls;
using Didox.DataBase.Generics;

public partial class controls_wcHierarquias : System.Web.UI.UserControl
{
    private Pessoa pessoa = null;
    protected void Page_Load(object sender, EventArgs e) { }

    public void GetHierarquias(Pessoa pessoa, TipoPessoa tipoPessoa)
    {
        tdTriviewHierarquia.InnerHtml = string.Empty;
        tdTriviewHierarquia.Controls.Clear();
        if (CType.Exist(pessoa))
        {
            this.pessoa = pessoa;
            List<List<int>> estruturaHierarquia = new List<List<int>>();
            if (tipoPessoa == TipoPessoa.Fisica)
            {
                var pessoaFisicaJuridica = new PessoaFisicaJuridica(pessoa.Fisica).Find();
                foreach (var pj in pessoaFisicaJuridica)
                {
                    var pessoaJuridica = ((PessoaFisicaJuridica)pj).PessoaJuridica.Pessoa;
                    estruturaHierarquia.Add(pessoaJuridica.GetListIdEstruturaHierarquia());
                }
            }
            else estruturaHierarquia.Add(pessoa.GetListIdEstruturaHierarquia());

            if (estruturaHierarquia.Count > 0)
            {
                foreach(var eh in estruturaHierarquia)
                    loadTree((List<int>)eh);
            }
            if (tdTriviewHierarquia.Controls.Count < 1)
                tdTriviewHierarquia.InnerHtml = "Pessoa " + pessoa.Nome + ", não está cadastrado na estrutura de hierarquias";
            return;
        }
        tdTriviewHierarquia.InnerHtml = "Você não está cadastrado em uma estrutura de hierarquias";
    }

    private void loadTree(List<int> estruturaHierarquiaIds)
    {
        if (estruturaHierarquiaIds.Count < 1) return;
        var trvPessoas = new TreeView();
        trvPessoas.ShowLines = true;
        tdTriviewHierarquia.Controls.Add(trvPessoas); 
        addNode(trvPessoas.Nodes, estruturaHierarquiaIds, 0);
    }

    private void addNode(TreeNodeCollection treeNodeCollection, List<int> ids, int index)
    {
        if (ids.Count == index) return;
        var pessoa = new Pessoa(ids[index]);
        pessoa.Get();

        var node = new TreeNode(pessoa.Nome + " - " + pessoa.GetHierarquia().Descricao, pessoa.IDPessoa.ToString());
        node.Expanded = true;
        treeNodeCollection.Add(node);

        addNode(node.ChildNodes, ids, index + 1);
    }
}
