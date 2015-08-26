var valuesOnPage = null
var idTabAnterior = null;
var mudarTab = false;

$(document).ready(function() {
    idTabAnterior = $("#tabs").find(".ui-tabs-selected").find("a").attr("id");
    valuesOnPage = setJson();
    $("#tabs ul li a").click(function() { validateChangedOnPage(this); });
});

function setJson() {
    var json = new Object();
    $("#tabs").find("input, select, textarea").each(function() {
        var jInput = $(this);
        json[jInput.attr("name")] = jInput.val();
    });

    $("#tabs").find("input[type=radio]:checked").each(function() {
        var jInput = $(this);
        json[jInput.attr("name")] = jInput.val();
    });

    $("#tabs").find("input[type=checkbox]:checked").each(function() {
        var jInput = $(this);
        json[jInput.attr("name")] = jInput.val();
    });
    
    return json;
}

var validateChangedOnPage = function(self) {    
    if (!compareJson(valuesOnPage, setJson()) && !mudarTab) {
        alert("Houve alteracao na pagina, clique em salvar para gravar as informacoes");
        mudarTab = true;
        $("#" + idTabAnterior).trigger("click");
        mudarTab = false;
    }
    else {
        idTabAnterior = $(self).attr("id");
    }
}

var compareJson = function(obj1, obj2) {
    function size(obj) {
        var size = 0;
        for (var keyName in obj) {
            if (keyName != null)
                size++;
        }
        return size;
    }

    if (size(obj1) != size(obj2))
        return false;

    for (var keyName in obj1) {
        var value1 = obj1[keyName];
        var value2 = obj2[keyName];

        if (typeof value1 != typeof value2)
            return false;

        // For jQuery objects:
        if (value1 && value1.length && (value1[0] !== undefined && value1[0].tagName)) {
            if (!value2 || value2.length != value1.length || !value2[0].tagName || value2[0].tagName != value1[0].tagName)
                return false;
        }
        else if (typeof value1 == 'function' || typeof value1 == 'object') {
            var equal = null;
            if (value1 == null && value2 == null) equal = true;
            else equal = compare(value1, value2);
            if (!equal)
                return equal;
        }
        else if (value1 != value2)
            return false;
    }
    return true;
}
