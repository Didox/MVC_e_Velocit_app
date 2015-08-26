var AddEmail = function() {
    var emailIndice = parseInt($("#hiddenEmailCount").val());
    var email = $("#dvEmail").html();

    email = email.replace(/name=".*?"|id=".*?"/g, "");
    email = email.replace(/class="Email"/g, "id=\"txtEmail-" + emailIndice + "\" name=\"txtEmail-" + emailIndice + "\" ");
    email = email.replace(/class="TiposEmail"/g, "id=\"ddlTiposEmail-" + emailIndice + "\" name=\"ddlTiposEmail-" + emailIndice + "\" ");

    $("#tdEmails").append("<div id=\"dvEmail-" + emailIndice + "\" style=\"display:none\">" + email + "</div>");
    $("#dvEmail-" + emailIndice).fadeIn(400);
    emailIndice++;
    $("#hiddenEmailCount").val(emailIndice);
}

var AddTelefone = function() {
    var telefoneIndice = parseInt($("#hiddenTelefoneCount").val());
    var telefone = $("#dvTelefone").html();

    telefone = telefone.replace(/name=".*?"|id=".*?"/g, "");
    telefone = telefone.replace(/class="DDITelefone"/g, "id=\"txtDDITelefone-" + telefoneIndice + "\" name=\"txtDDITelefone-" + telefoneIndice + "\" ");
    telefone = telefone.replace(/class="DDDTelefone"/g, "id=\"txtDDDTelefone-" + telefoneIndice + "\" name=\"txtDDDTelefone-" + telefoneIndice + "\" ");
    telefone = telefone.replace(/class="NumeroTelefone"/g, "id=\"txtNumeroTelefone-" + telefoneIndice + "\" name=\"txtNumeroTelefone-" + telefoneIndice + "\" ");
    telefone = telefone.replace(/class="TiposTelefone"/g, "id=\"ddlTiposTelefone-" + telefoneIndice + "\" name=\"ddlTiposTelefone-" + telefoneIndice + "\" ");

    $("#tdTelefones").append("<div id=\"dvTelefone-" + telefoneIndice + "\" style=\"display:none\">" + telefone + "</div>");
    $("#dvTelefone-" + telefoneIndice).fadeIn(400);
    telefoneIndice++;
    $("#hiddenTelefoneCount").val(telefoneIndice);
}

var AddEndereco = function() {
    var enderecoIndice = parseInt($("#hiddenEnderecoCount").val());
    var endereco = $("#tbEndereco").html();

    endereco = endereco.replace(/name=".*?"|id=".*?"/g, "");
    endereco = endereco.replace(/class="TipoEndereco"/g, "id=\"ddlTipoEndereco-" + enderecoIndice + "\" name=\"ddlTipoEndereco-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="Endereco"/g, "id=\"txtEndereco-" + enderecoIndice + "\" name=\"txtEndereco-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="EnderecoNumero"/g, "id=\"txtEnderecoNumero-" + enderecoIndice + "\" name=\"txtEnderecoNumero-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="Bairro"/g, "id=\"txtBairro-" + enderecoIndice + "\" name=\"txtBairro-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="Cidade"/g, "id=\"txtCidade-" + enderecoIndice + "\" name=\"txtCidade-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="Estado"/g, "id=\"ddlEstado-" + enderecoIndice + "\" name=\"ddlEstado-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="Pais"/g, "id=\"ddlPais-" + enderecoIndice + "\" name=\"ddlPais-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="Cep"/g, "id=\"txtCep-" + enderecoIndice + "\" name=\"txtCep-" + enderecoIndice + "\" ");
    endereco = endereco.replace(/class="Complemento"/g, "id=\"txtComplemento-" + enderecoIndice + "\" name=\"txtComplemento-" + enderecoIndice + "\" ");

    $("#dvEnderecos").append("<table id=\"tbEndereco-" + enderecoIndice + "\" style=\"display:none\">" + endereco + "</table>");
    $("#tbEndereco-" + enderecoIndice).fadeIn(400);
    enderecoIndice++;
    $("#hiddenEnderecoCount").val(enderecoIndice);
}

var GetTelefones = function() {
    if (jsonTelefones == undefined) return;
    var i = 0;
    $(jsonTelefones).each(function() {
        AddTelefone()
        var txtDDITelefone = $("#txtDDITelefone-" + i);
        var txtDDDTelefone = $("#txtDDDTelefone-" + i);
        var txtNumeroTelefone = $("#txtNumeroTelefone-" + i);
        var ddlTiposTelefone = $("#ddlTiposTelefone-" + i);

        txtDDITelefone.val(this.Telefone.DDI)
        txtDDDTelefone.val(this.Telefone.DDD)
        var numero = this.Telefone.NumeroInterno;
        numero = numero.substring(0, 4) + "-" + numero.substring(4, numero.length)
        txtNumeroTelefone.val(numero)
        ddlTiposTelefone.val(this.Telefone.IDTipoTelefone)
        i++;
    });
}

var GetEmails = function() {
    if (jsonEmails == undefined) return;
    var i = 0;
    $(jsonEmails).each(function() {
        AddEmail()
        var txtEmail = $("#txtEmail-" + i);
        var ddlTiposEmail = $("#ddlTiposEmail-" + i);
        
        txtEmail.val(this.Email.EnderecoEmail)
        ddlTiposEmail.val(this.Email.IDTipoEmail)
        i++;
    });
}

var GetEnderecos = function() {
    if (jsonEnderecos == undefined) return;
    var i = 0;
    $(jsonEnderecos).each(function() {
        AddEndereco()
        var ddlTipoEndereco = $("#ddlTipoEndereco-" + i);
        var txtEndereco = $("#txtEndereco-" + i);
        var txtEnderecoNumero = $("#txtEnderecoNumero-" + i);
        var txtBairro = $("#txtBairro-" + i);
        var txtCidade = $("#txtCidade-" + i);
        var ddlEstado = $("#ddlEstado-" + i);
        var ddlPais = $("#ddlPais-" + i);
        var txtCep = $("#txtCep-" + i);
        var txtComplemento = $("#txtComplemento-" + i);

        ddlTipoEndereco.val(this.Endereco.IDTipoEndereco);
        txtEndereco.val(this.Endereco.Descricao);
        txtEnderecoNumero.val(this.Endereco.Numero);
        txtBairro.val(this.Endereco.Bairro);
        txtCidade.val(this.Endereco.Cidade);
        ddlEstado.val(this.Endereco.IDEstado);
        ddlPais.val(this.Endereco.IDPais);
        var cep = this.Endereco.CepInterno;        
        cep = cep.substring(0, 5) + "-" + cep.substring(5, cep.length)
        txtCep.val(cep);
        txtComplemento.val(this.Endereco.Complemento);
        i++;
    });
}

var RemoverTelefone = function(self) {
    $(self).parent().fadeOut(400, function() { $(this).remove(); });
}

var RemoverEmail = function(self) {
    $(self).parent().fadeOut(400, function() { $(this).remove(); });
}

var RemoverEndereco = function(self) {
    $(self).parent().parent().parent().fadeOut(400, function() { $(this).remove(); });
}