$(document).ready(function() {
    $("#dvEsqueciSenhaCentral #voltar").click(function() {
        $("#dvLoginCentral").css("display", "block");
        $("#fmEsqueciSenha").css("display", "none");
        $("#texto_voltar").css("display", "none"); 
    });

    $("#dvLoginCentral #clique_aqui").click(function() {
        $("#dvLoginCentral").css("display", "none");
        $("#fmEsqueciSenha").css("display", "block");
        $("#texto_voltar").css("display", "block"); 
    });
})