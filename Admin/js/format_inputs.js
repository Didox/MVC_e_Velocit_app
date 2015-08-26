var formatValue = function(field, format, e) {
    var strVerify = ""
    var retorno = false
    var key = e.keyCode;
    if (key == 0) key = e.charCode;

    if (key == 8 || key == 46 || key == 192 || key == 27 || key == 13 || key == 192 || key == 9) return true;

    var keychar = String.fromCharCode(key);
    for (x = 0; x < format.length; x++) {
        if (isNaN(format.charAt(x))) {
            strVerify += x + "|" + format.charAt(x) + "|"
        }
    }

    if (!isNaN(keychar) || format[1, field.value.length] == "#") {
        retorno = true
        var strValue = field.value
        if (strValue.length < format.length) {
            strVerify = strVerify.split("|")

            for (x = 0; x < (strVerify.length - 1); x += 1) {
                tam = strValue.length + ""
                if (tam == strVerify[x]) {
                    if (strVerify[x + 1] != "#")
                        strValue += strVerify[x + 1]
                    field.value = strValue
                    break
                }
            }
        }
        else retorno = false
    }

    return retorno
}

var initDatePicker = function() {
    $(".datepicker").datepicker({
        dateFormat: 'dd/mm/yy',
        monthNames: ['Janeiro', 'Fevereiro', 'Marco', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        dayNamesMin: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab']
    });
}

$(document).ready(function() {
    $(".integer").each(function() {
        $(this).keypress(function(event) {
            return formatValue(this, '999999999999999999', event);
        });
    });

    $(".data").each(function() {
        $(this).addClass("datepicker");
        $(this).keypress(function(event) {
            return formatValue(this, '99/99/9999', event);
        });
    });

    /*$(".rg").each(function() {
        $(this).keypress(function(event) {
            return formatValue(this, '99.999.999-#', event);
        });
    });*/

    $(".cpf").each(function() {
        $(this).keypress(function(event) {
            return formatValue(this, '999.999.999-99', event);
        });
    });

    $(".cnpj").each(function() {
        $(this).keypress(function(event) {
            return formatValue(this, '99.999.999/9999-99', event);
        });
    });

    initDatePicker();
});