var menuCurrent = null;
var CarregaPaginasFilhas = function(self, site) {
    menuCurrent = $(self);
    var id = menuCurrent.attr("rel");
    var menu = $("#submenu" + id);
    if (menu.size() < 1) {
        menuCurrent.parent().append("<div class=\"submenu\" id=\"submenu" + menuCurrent.attr("rel") + "\">Carregando...</div>");
        jQuery.ajax({
            url: site + "/carregarpaginasfilhas?idPaginaPai=" + id,
            success: function(data) {
                var menu = $("#submenu" + menuCurrent.attr("rel"));
                if (data.length > 1) {                    
                    menu.html(data);
                    menu.hide();
                    menu.fadeIn();
                }
                else menu.remove();
            }
        });
    }
    else {
        if (menu.css("display") == "none")
            menu.fadeIn();
        else menu.fadeOut();
    }
}