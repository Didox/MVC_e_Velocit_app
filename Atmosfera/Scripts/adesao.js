var site = null
var nivelAtualCurrent = null

$(document).ready(function() {
    CarregarComboOperacaoAdesao(0, 0);
});

var CarregarListaUsuariosAdesao = function(self, nivelAtual) {
    $("#lista_usuarios").html("Carregando ...")
    jQuery.ajax({
        url: site + "/carregarlistausuariosadesao?idHierarquia=" + self.value + "&nivelAtual=" + nivelAtual,
        success: function(data) {
            $("#lista_usuarios").html(data)
        }
    });
}


var CarregarComboOperacaoAdesao = function(idHierarquia, nivelAtual, self) {
    nivelAtualCurrent = nivelAtual
    if (idHierarquia == -1 && self != undefined) {
        nivelSelect = $(self).attr("rel")
        nivelSelect = parseInt(nivelSelect)
        $("#combos #operacao" + (nivelSelect - 1)).trigger("change");
        return;
    }
    $("#load").html("Carregando lista ...")
    jQuery.ajax({
        url: site + "/carregarcomboadesao?idHierarquia=" + idHierarquia + "&nivelAtual=" + nivelAtual,
        success: function(data) {
            for (idx = nivelAtual; idx <= 10; idx++) {
                if (idx > 0) {
                    $("#combos #operacao" + idx).remove()
                }
            }

            $("#load").html("")
            $("#combos").append(data)
        }
    });
}


var changeStatusUsuario = function(self, idUsuario) {
    jQuery.ajax({
        url: site + "/alterarstatususuario?status=" + self.value + "&idUsuario=" + idUsuario,
        success: function(data) {
            alert(data);
        }
    });
}


var resetSenhaUsuario = function(self, idUsuario) {
    jQuery.ajax({
        url: site + "/ResetSenhaUsuario?status=" + self.value + "&idUsuario=" + idUsuario,
        success: function(data) {
            alert(data);
        }
    });
}

var envioSenhaUsuario = function(self, idUsuario) {
    jQuery.ajax({
        url: site + "/EnvioSenhaUsuario?status=" + self.value + "&idUsuario=" + idUsuario,
        success: function(data) {
            alert(data);
        }
    });
}

var salvarAlteracaoUsuario = function(self, idUsuario) {
    var trId = $(self).parent().parent().attr("id")
    var array = new Array()
    var index = 0
    $("#" + trId + " .editavel input").each(function() {
        $(this).parent().html($(this).val())
        array[index] = $(this).val()
        index++;
    })

    var celEdit = $(self).parent()
    celEdit.html(celEdit.attr("rel"))
    var indexNome = 0
    var indexEmail = 1
    jQuery.ajax({
        url: site + "/SalvarAlteracoesUsuario?nome=" + array[indexNome] + "&email=" + array[indexEmail] + "&idUsuario=" + idUsuario,
        success: function(data) {
            if (data != "1")
                alert(data);
        }
    });
}

var alterarUsuario = function(self, idUsuario) {
    var trId = $(self).parent().parent().attr("id")
    $("#" + trId + " .editavel").each(function() {
        var value = $(this).html()
        $(this).html("<input type=\"text\" value=\"" + value + "\">")
    })

    $("#" + trId + " .editavel input").get(0).focus();
    var celEdit = $(self).parent()
    celEdit.attr("rel", celEdit.html())
    celEdit.html("<a href=\"javascript:void(0);\" title=\"Salvar\" onclick=\"salvarAlteracaoUsuario(this, " + idUsuario + ")\"><img border=\"0\" src=\"" + site + "/Publico/Ambev/imagens/btn_salvar.png\" ></a>")
}

var tdChangeEndereco = null
var alterarEnderecoUsuario = function(self, idUsuario) {
    tdChangeEndereco = $(self).parent().parent()
    var celEdit = $(self).parent()
    celEdit.attr("rel", celEdit.html())
    $(self).parent().html("Editando")
    jQuery.ajax({
        url: site + "/AlterarEnderecoUsuarioHtml?idUsuario=" + idUsuario,
        success: function(data) {
            tdChangeEndereco.after("<tr><td colspan=\"12\">" + data + "</td></tr>")
        }
    });
}

var tdChangeIncluirResponsavel = null
var incluirResponsavelUsuario = function(self, idUsuario, idHierarquia) {
    tdChangeIncluirResponsavel = $(self).parent().parent()
    var celEdit = $(self).parent()
    celEdit.attr("rel", celEdit.html())
    $(self).parent().html("Editando")
    jQuery.ajax({
        url: site + "/IncluirResponsavelHtml?idUsuario=" + idUsuario + "&idHierarquia=" + idHierarquia,
        success: function(data) {
            tdChangeIncluirResponsavel.after("<tr><td colspan=\"12\">" + data + "</td></tr>")
        }
    });
}

var setAcaoCancelarEndereco = function(idUsuario) {
    $("#dvAlterarEndereco" + idUsuario + " #botao_cancelar").click(function() {
        $(this).parent().parent().parent().parent().parent().parent().parent().remove()
        var td = $("#usuarioLinha" + idUsuario + " .salvarEndereco")
        td.html(td.attr("rel"))
    });
}

var cancelarResponsavel = function(self, idUsuario) {
    $(self).parent().parent().parent().parent().parent().parent().parent().remove()
    var td = $("#usuarioLinha" + idUsuario + " .incluirResponsavel")
    td.html(td.attr("rel"))
    botao_cancelar = null
    
    //$("#combos #operacao" + nivelAtualCurrent).trigger("change")
}


var tdIncluirResponsavel = null
var idUsuarioCurrent = null
var botao_cancelar = null
incluirResponsavel = function(self, idUsuario) {    
    if (botao_cancelar != null) {
        alert("Existe uma linha em edição, por favor finalize a mesma.")
        return
    }
    
    tdIncluirResponsavel = $("#dvIncluirResponsavel" + idUsuario + " form")
    botao_cancelar = $("#dvIncluirResponsavel" + idUsuario + " #botao_cancelar")
    idUsuarioCurrent = idUsuario
    data = tdIncluirResponsavel.serialize()
    jQuery.ajax({
        type: 'POST',
        url: site + "/IncluirResponsavel",
        data: data,
        success: function(data) {
            if (data.indexOf("table") == -1) {
                alert(data)
                botao_cancelar.trigger("click")
            }
            else {
                var td = tdIncluirResponsavel.parent().parent()
                td.html(data)
                var dvBotaoCancel = td.find("#dvBotaoCancel")
                dvBotaoCancel.append(botao_cancelar)
                dvBotaoCancel.find("#botao_cancelar").attr("value", "Concluir")
            }
        }
    });
    return false;
}

incluirResponsavelLine = function(idUsuario, idHierarquia) {
    var data = "idHierarquia=" + idHierarquia + "&idUsuario=" + idUsuario
    jQuery.ajax({
        type: 'POST',
        url: site + "/IncluirResponsavel",
        data: data,
        success: function(data) {
            alert(data)
        }
    });
    return false;
}


var tdAddResponsavel = null
incluirResponsavelHierarquiaHtml = function(self, idHierarquia) {
    tdAddResponsavel = $(self).parent()
    tdAddResponsavel.attr("rel", tdAddResponsavel.html())
    jQuery.ajax({
        url: site + "/IncluirResponsavelHtml?idHierarquia=" + idHierarquia,
        success: function(data) {
            tdAddResponsavel.attr("align", "")
            tdAddResponsavel.html(data)
        }
    });
}

cancelarResponsavelHierarquia = function(self) {
    var tdAddResponsavel = $(self).parent().parent().parent().parent().parent().parent()
    tdAddResponsavel.attr("align", "center")
    tdAddResponsavel.html(tdAddResponsavel.attr("rel"))
    botao_cancelar = null

    $("#operacao" + nivelAtualCurrent).trigger("change")
}

incluirResponsavelHierarquia = function(self, idUsuario) {
    if (botao_cancelar != null) {
        alert("Existe uma linha em edição, por favor finalize a mesma.")
        return
    }

    botao_cancelar = $("#dvIncluirResponsavel" + idUsuario + " #botao_cancelar")
    tdIncluirResponsavel = $(self).parent().parent().parent()
    data = tdIncluirResponsavel.serialize()
    jQuery.ajax({
        type: 'POST',
        url: site + "/IncluirResponsavel",
        data: data,
        success: function(data) {
            if (data.indexOf("table") == -1) {
                alert(data)
                $("#operacao" + nivelAtualCurrent).trigger("change")
            }
            else {
                var td = tdIncluirResponsavel.parent().parent()
                td.html(data)

                var dvBotaoCancel = td.find("#dvBotaoCancel")
                dvBotaoCancel.append(botao_cancelar)
                dvBotaoCancel.find("#botao_cancelar").attr("value", "Concluir")
            }
        }
    });
    return false;
}



setAcaoSalvarEndereco = function(idUsuario) {
    $("#dvAlterarEndereco" + idUsuario + " #botao_salvar").click(function() {
        $("#dvAlterarEndereco" + idUsuario + " #botao_cancelar").trigger("click")
    })
}