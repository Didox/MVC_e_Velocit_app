#if($lista_usuario_parceiro.Count > 0)
<table>
<tr>
<th>Nome</th>
<th>Responsável</th>
<th>E-mail</th>
<th>Login</th>
<th>Senha</th>
<th>Último Acesso</th>
<th>Status</th>
<th>Reset Senha</th>
<th>Envio Senha</th>
<th>Editar</th>
<th>Incluir Resp.</th>
<th>Endereço</th>
</tr>
#foreach($item in $lista_usuario_parceiro)
  #if($item.Usuario.Nome)
  <tr id="usuarioLinha$item.Usuario.IDUsuario">
    <td>$item.Estrutura.Descricao - $item.Subnivel.Descricao</td>
    <td class="editavel">$item.Usuario.Nome</td>
    <td class="editavel">$item.Usuario.Email</td>
    <td>$item.Usuario.Login</td>
    <td>$item.Usuario.Senha</td>
    <td>$item.Usuario.BuscaUltimoAcesso().DataFormatada</td>
    <td>
      <select name="status_usuario" onchange="changeStatusUsuario(this, $item.Usuario.IDUsuario)"  id="status_usuario" >
        <option value="1" #if($item.Usuario.Ativo) selected="true" #end >Ativo</option>
        <option value="0"  #if(!$item.Usuario.Ativo) selected="true" #end >Inativo</option>
      </select>
    </td>
    <td width="30px" align="center">
        <a href="javascript:void(0);" title="Resetar senha" onclick="resetSenhaUsuario(this, $item.Usuario.IDUsuario)">
            <img border="0" src="$site/Publico/Ambev/imagens/reset_senha.gif" >
        </a>
    </td>
    <td width="30px" align="center">
        <a href="javascript:void(0);" title="Enviar senha" onclick="envioSenhaUsuario(this, $item.Usuario.IDUsuario)">
            <img border="0" src="$site/Publico/Ambev/imagens/enviar_senha.gif" >
        </a>
    </td>
    <td width="30px" align="center">
        <a href="javascript:void(0);" title="Editar responsável" onclick="alterarUsuario(this, $item.Usuario.IDUsuario)">
            <img border="0" src="$site/Publico/Ambev/imagens/editar_p.gif" >
        </a>
    </td>
    <td class="incluirResponsavel" width="30px" align="center">
        <a href="javascript:void(0);" title="Incluir Responsavel a hierarquia ($item.Estrutura.Descricao - $item.Subnivel.Descricao)" onclick="incluirResponsavelUsuario(this, $item.Usuario.IDUsuario, $item.IDHierarquia)">
            <img border="0" src="$site/Publico/Ambev/imagens/incluir.gif" >
        </a>
    </td>
    <td class="salvarEndereco" width="30px" align="center">
        <a href="javascript:void(0);" title="Incluir ou alterar endereço do usuário" onclick="alterarEnderecoUsuario(this, $item.Usuario.IDUsuario)">
            <img border="0" src="$site/Publico/Ambev/imagens/editar_p.gif" >
        </a>
    </td>
  </tr>
  #end
#end
</table> 
#end