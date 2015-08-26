using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Didox.Business;

public partial class controls_wcCargos : System.Web.UI.UserControl
{
    private Usuario usuario = null;
    protected void Page_Load(object sender, EventArgs e) { }

    public void GetCargos(int idUsuario)
    {
        usuario = new Usuario(idUsuario);
        usuario.Get();

        dvTreeview.Visible = false;
        trvPessoas.Nodes.Clear();
        addNode(trvPessoas.Nodes, usuario.GetListIdEstruturaCargo(), 0);

        if (trvPessoas.Nodes.Count <= 0)
        {
            dvTreeview.Visible = true;
            dvTreeview.InnerHtml = "Usuário " + usuario.Nome + ", não está cadastrado na estrutura de cargos";
        }
    }

    private void addNode(TreeNodeCollection treeNodeCollection, List<int> ids, int index)
    {
        if (ids.Count == index) return;
        var pessoa = new Pessoa(ids[index]);
        pessoa.Get();

        var node = new TreeNode(pessoa.Nome + " - " + pessoa.GetCargo().Descricao, pessoa.IDPessoa.ToString());
        node.Expanded = true;
        treeNodeCollection.Add(node);

        addNode(node.ChildNodes, ids, index + 1);
    } 
}
