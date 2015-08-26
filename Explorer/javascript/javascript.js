var repeatColumns    = null
var repeatRows       = null
var actualFolder     = null
var previousFolder   = null
var tblSelectedItem  = null
var pathSelectedItem = null
var typeSelectedItem = null
var nameSelectedItem = null
var actualXml        = null
var rowTable         = null
var colTable         = null
var rowSelectedItem  = null
var colSelectedItem  = null
var creatingFolder   = false
var folder           = null
var forceOnServer    = null
var controlPressed   = false
var typeView         = null
		
function changeButton( obj , className )
{
	obj.className = className
}
	
function openImage( img )
{
	window.open( 'viewImage.htm?img=' + img , '' , 'width=300,height=150,top=0,left=0,scrollbars=yes' )
}	

function replaceAll( str , strSearch , strNew )
{
	var i = str.indexOf( strSearch );
	
	while( i > -1 )
	{
		str = str.replace( strSearch , strNew );
		i = str.indexOf( strSearch , i + strNew.length + 1);
	}
	
	return str;
}

function setCookie( name, value , expires )
{
	c = name + "=" + escape(value) 
    if( ! expires )
	{
		var today = new Date();
		var dateExpire = new Date( today.getTime() + ( 30 * 1000 * 60 * 60 * 24 ) )
		c += ';expires=' + dateExpire.toGMTString()
	}
	
	document.cookie = c
}

function getCookie( name )
{
	if (document.cookie.length>0)
	{
   		start=document.cookie.indexOf( name + "=" )
  		if ( start!=-1 )
   		{ 
    		start += ( name.length + 1 ) 
			strC = document.cookie.substring( start , document.cookie.length )
			end = strC.indexOf(";")
    	
			if ( end == -1 ) 
				end = document.cookie.length
		
			return unescape(strC.substring( 0 , end ))
		} 
	}
}

function addPathCombo( text , value ) {
    var exist = false;
    var selPath = $("#selPath")
    options = selPath.attr('options')   

    for (x = 0; x < options.length; x++)
	{
		if( options[x].value == value )
		{
			options[x].selected = true
			exist = true
			break
		}
	}	
	
	if( ! exist ) {
	    selPath.append('<option value="' + value + '" selected="selected">' + text + '</option>');
	}
}

function removePathCombo( value ) {
    var selPath = $("#selPath")
    options = selPath.attr('options')
    for (x = 0; x < options.length; x++) {
        if (options[x].value == value) {
            $(options[x]).remove()
        }
    }
}

function loadingFolder( folder , forceOnServer ) {
    this.folder = folder;
	this.forceOnServer = forceOnServer;
	var divStatus = $("#divStatus");
	var left = ((document.documentElement.offsetWidth / 2) - 50);
	var top = ((document.documentElement.offsetHeight / 2) - 50);
	divStatus.css("left", left);
	divStatus.css("top", top);

	divStatus.css("display", "block");
	$("#list").html("");
	controlPressed = false	
	setTimeout("showFolder()", 1);
}

function backFolder()
{
	if( actualFolder != "" )
		loadingFolder( previousFolder , false )
}
	
function showFolder() {
    
	actualFolder = folder
	setCookie( "actualFolder" , actualFolder, true )
	previousFolder = actualFolder.substring(0, actualFolder.lastIndexOf("\\"))
	$("#liImgDel").css("display", "none");
	$("#liImgRel").css("display", "none");
	addPathCombo("Raiz:\\" + actualFolder, actualFolder)
	unSelectItem()
	getFolder(folder)
}

function setFtpDataOnHtml() {
    if (getCookie("typeView") == "icons")
        dataIcons()
    else
        dataList()

    $("#tdItensQtd").html("Itens na pasta: " + $(actualXml).find("item").length);
    $("#divStatus").css("display", "none");
}

function getFolder(folder) {

    $.ajax({
        url: "itensFolder.aspx?folder=" + folder,
        success: function(data) {
            actualXml = data;
            setFtpDataOnHtml();
        },
        error: function(data) {
            alert('Não foi possível obter o conteúdo da pasta.');
        }
    });	
}
	
function showActualFolder()
{
	unSelectItem()
		
	if( controlPressed )
		loadingFolder( actualFolder , true )
	else
	{
		if( typeView == "icons" )
			dataIcons()
		else
			dataList()
	}
}

function dataIcons() {
    typeView = "icons"
    setCookie("typeView", "icons", false)
    $("#rIcon").attr("checked", true);

    objXml = $(actualXml).find("item")
    $("#list").html("");
    repeatColumns = parseInt((document.documentElement.offsetWidth - 280) / 120)
    rowTable = -1
    colTable = 0

    var table = $(document.createElement("table"));
    var tbody = $(document.createElement("tbody"));
    tbody.attr("id", "tBodyFolder")

    table.append(tbody)
    $("#list").append(table)

    for (x = 0; x < objXml.length; x++) {
        if (x % repeatColumns == 0) {
            var row = $(document.createElement("tr"))
            ++rowTable
            colTable = 0
        }

        var cell = $(document.createElement("td"))
        var xml = $(objXml[x])
        var nodeName = xml.find("name").text()
        var nodeImage = xml.find("image").text()
        var nodePath = xml.find("path").text()
        var nodeType = xml.find("type").text()
        var nodeDescription = xml.find("description").text();

        cell.attr("id", nodeName)

        //<TABLE id='tblItem00' row='0' col='0' pathItem='vrv' typeItem='folder' nameItem='vrv'  

        var tableName = "tblItem" + rowTable + '' + colTable;
        var item = "<TABLE id='" + tableName + "' class='tableItem' style='cursor:default' cellSpacing='1' cellPadding='1' width='115'"
        item += "  onclick=\"javascript:selectItem( this )\""
        item += "  ondblClick=\"javascript:checkLink( this )\" >"
        item += "	<TR>"
        item += "		<TD align='center' height=40>"
        item += "			<img alt='" + nodeName + "' src='" + nodeImage + "' border=0></a>"
        item += "		</TD>"
        item += "	</TR>"
        item += "	<TR>"
        item += "		<TD align='center' class='name' height=30 valign=top>"
        item += "			<a title='" + nodeName + "'>"
        item +=                  (nodeName.length > 15) ? nodeName.substring(0, 13) + "..." : nodeName
        item += "			</a>"
        item += "		</TD>"
        item += "	</TR>"
        item += "</TABLE>"



        cell.html(item)
        row.append(cell)
        tbody.append(row)

        var internalTable = $("#" + tableName)
        var paramsTable = { 'row': rowTable, 'col': colTable, 'pathItem': escape(nodePath), 'typeItem': nodeType, 'nameItem': escape(nodeName), 'description': nodeDescription }

        internalTable.attr("rel", JSON_stringify(paramsTable));        
        ++colTable
    }
}

JSON_stringify = function(obj) {
    var t = typeof (obj);
    if (t != "object" || obj === null) {
        // simple data type
        if (t == "string") obj = '"' + obj + '"';
        return String(obj);
    }
    else {
        // recurse array or object
        var n, v, json = [], arr = (obj && obj.constructor == Array);
        for (n in obj) {
            v = obj[n]; t = typeof (v);
            if (t == "string") v = '"' + v + '"';
            else if (t == "object" && v !== null) v = JSON_stringify(v);
            json.push((arr ? "" : '"' + n + '":') + String(v));
        }
        return (arr ? "[" : "{") + String(json) + (arr ? "]" : "}");
    }
};


function dataList()
{
		typeView = "list"
		setCookie( "typeView" , "list", false )
		$("rList").attr("checked", true);		
		objXml = $(actualXml).find("item")
		$("#list").html("");
		repeatRows = parseInt( ( document.documentElement.offsetHeight - 30 ) / 30 )
		rowTable = -1
		colTable = 0
		
		var table = $(document.createElement( "table" ))
		var tbody = $(document.createElement( "tbody" ))
		tbody.attr("id", "tBodyFolder");
	
		for( x=0; x <repeatRows; x++ )
		{
			var tr = $(document.createElement( "tr" ))
			tr.attr("id", "tr" + ++rowTable)
			tbody.append(tr)
		}
	
		table.append( tbody )
		$("#list").append(table)
	
		rowTable = -1
	
		for( x=0; x < objXml.length; x++ )			
		{
			if( rowTable == ( repeatRows - 1 ) )
			{
				rowTable= -1
				colTable++
			}
				
			var row = $("#tr" + ++rowTable )
			var cell = $(document.createElement("td"))

			var xml = $(objXml[x])
			var nodeName = xml.find("name").text()
			var nodeImage = xml.find("image").text()
			var nodePath = xml.find("path").text()
			var nodeType = xml.find("type").text()
			var nodeDescription = xml.find("description").text();
			
			cell.attr("id", nodeName)
			
			var tableName = "tblItem" + rowTable + '' + colTable;
			var item = "<TABLE width='250' id='" + tableName + "' class='tableItem' style='cursor:default' cellSpacing='0' cellPadding='0'"
			item +=    "  onclick=\"javascript:selectItem( this )\""
			item +=    "  ondblClick=\"javascript:checkLink( this )\" >"
			item +=	   "	<TR>"
			item +=    "		<TD align='left' width='20'>"
			item +=    "			<img alt='" + nodeName + "' src='" + nodeImage + "' border=0 width='20' height='20'>&nbsp;</a>"
			item +=    "		</TD>"
			item +=	   "		<TD align='left' class='name' >"
			item +=    "			<a title='" + nodeName + "'>"
			item +=                      nodeName
			item +=    "			</a>"
			item +=    "		</TD>"
			item +=    "	</TR>"
			item +=    "</TABLE>"

			cell.html(item)
			row.append(cell)
			
			var internalTable = $("#" + tableName)
			var paramsTable = { 'row': rowTable, 'col': colTable, 'pathItem': escape(nodePath), 'typeItem': nodeType, 'nameItem': escape(nodeName), 'description': nodeDescription }
			internalTable.attr("rel", JSON_stringify(paramsTable));  
		}
}

function alterView( type )
{	
	if( type == "icons" )
		dataIcons()
	else
		dataList()
}

		
function checkLink( obj ) {
    obj = $(obj)
    json = $.parseJSON(obj.attr("rel"))
    if (json.typeItem == "file")
        window.open(unescape(json.pathItem))

    else if (json.typeItem == "folder")
        loadingFolder(unescape(json.pathItem), false)
	else
	    openImage(json.pathItem) 
}
		
function resizeWindow()
{
	showActualFolder()
}

function selectItem( obj ) {
    obj = $(obj)
    json = $.parseJSON(obj.attr("rel"))
	unSelectItem()
	pathSelectedItem = json.pathItem
	typeSelectedItem = json.typeItem
	nameSelectedItem = json.nameItem
	rowSelectedItem = json.row
	colSelectedItem = json.col


	$(".tableItem").removeClass("unSelectedItem")
	obj.addClass("selectedItem")
	tblSelectedItem = obj

	$("#liImgDel").css("display", "block");
	$("#liImgRel").css("display", "block");
	$("#tdNameItem").html(unescape(json.nameItem));
	$("#tdDescription").html(json.description);
	$("#tdNameItem").html("");
	
	if (json.typeItem == "image") {
	    $("#imgPreview").attr("src", unescape(json.pathItem));
	    $("#trImagePreview").css("display", "block")
	}
	
	var textImgDel      = ( json.typeItem == "folder" ) ? "Excluir essa pasta" : "Excluir esse arquivo"
	$("#txtImgDel").html(textImgDel)
	obj.focus()
}

function unSelectItem()
{
	if( tblSelectedItem != null ) {

	    $(".selectedItem").removeClass("selectedItem")
	    tblSelectedItem.addClass("unSelectedItem")
	    $("#liImgDel").css("display", "none");
	    $("#liImgRel").css("display", "none");
	}
	
	tblSelectedItem  = null
	pathSelectedItem = null
	typeSelectedItem = null
	nameSelectedItem = null
	rowSelectedItem  = null
	colSelectedItem  = null
	
	
	var folder = actualFolder.split( "\\" )
	$("#tdNameItem").html((actualFolder == "") ? "Raiz" : folder[folder.length - 1]);
	$("#tdDescription").html("Pasta de arquivos");
	if (actualXml != null)
	    $("#tdItensQtd").html("Itens na pasta: " + $(actualXml).find("item").length);

    $("#trImagePreview").css("display", "none");
	$("#imgPreview").attr("src", "imagens/white.jpg");		
}

function deleteItem()
{
	if( typeSelectedItem == "folder" )
		var msg = "Deseja realmente deletar o diretório \"" + unescape( nameSelectedItem ).toUpperCase() + "\" e todo o seu conteúdo?"
	else 
		var msg = "Deseja realmente deletar o arquivo \"" + unescape( nameSelectedItem ).toUpperCase() + "\"?"
	
	if( confirm( msg ) )
		do_Delete()
}

function renameFolder() {
    var json = $.parseJSON($(".selectedItem").attr("rel"));
    $(".selectedItem a").parent().html("<input id='textRename' type='text' style='width:110px' value='" + unescape(json.nameItem) + "' onblur='do_Rename(this)'>")    
    $("#textRename").focus();
}

function do_Rename(self) {
    removePathCombo(unescape(pathSelectedItem))
    $.ajax({
        url: "renameItem.aspx?oldFile=" + unescape(pathSelectedItem) + "&newFile=" + $(self).val() + "&type=" + typeSelectedItem,    
        success: function(data) {
            showFolder()
        },
        error: function(data) {
            alert('Não foi renomear a pasta.')
        }
    });      
}

function do_Delete()
{
    $.ajax({
        url: "deleteItem.aspx?item=" + unescape(pathSelectedItem) + "&type=" + typeSelectedItem,
        success: function(data) {
            var children = $(actualXml).children().find("item")
            for (x = 0; x < children.length; x++) {
                var node = $(children[x])
                if (node.find("name").text() == unescape(nameSelectedItem)) {                
                    node.remove()
                }
            }

            removePathCombo(unescape(pathSelectedItem))
            tblSelectedItem.parent().remove()
        },
        error: function(data) {
            alert('Não foi possível deletar o item selecionado.')
        }
    });
}

function newFolder()
{
	creatingFolder = true
	unSelectItem();

	var tBodyFolder = $("#tBodyFolder")	
	if( typeView == "icons" )
	{
		var newRow = false
		var r = tBodyFolder.children()[tBodyFolder.children().length - 1]
		if( r == null || $(r).children().length == repeatColumns )
		{
			newRow = true
			colTable = 0
			rowTable++
		}
		
		var item = "<TABLE id='tblItem" + rowTable + '' + colTable + "' class='selectedItem' style='cursor:default' cellSpacing='1' cellPadding='1' width='115'"
		item +=    "  onclick=\"javascript:selectItem( this )\""
		item +=    "  ondblClick=\"javascript:checkLink( this )\" >"
		item +=	   "	<TR>"
		item +=    "		<TD align='center' height=40>"
		item +=	   "			<img id='imgNewFolder" + rowTable + '' + colTable + "' src='icones/folder.gif' border=0>"
		item +=    "		</TD>"
		item +=    "	</TR>"
		item +=    "	<TR>"
		item +=	   "		<TD align='center' class='name' height=30 valign=top id='tdNewFolder" + rowTable + '' + colTable + "'>"
		item +=    "		<input maxlength=30 id='txtNewFolder' size=10 style='font-size:10;border:1px solid #0053a5' value='Nova Pasta' onblur=\"javascript:createFolder( this )\" onkeypress=\"javascript:return checkInputKey( this )\" >"
		item +=    "		</TD>"
		item +=    "	</TR>"
		item +=    "</TABLE>"
		
		var cell  = $(document.createElement( "td" ))
		cell.attr( "id" , "newTd" )
		cell.html(item)
			
		if( newRow )
		{
			var row = $(document.createElement( "tr" ))
			row.append( cell )
			tBodyFolder.append( row )	
		}
		else
			$(r).append( cell )
	}
	else
	{
		rowTable++
		if( rowTable == repeatRows )
		{
			rowTable = 0
			colTable++
		}
			
		var td = $(document.createElement( "td" ))
		td.attr( "id" , "newTd" )
			
		var item = "<TABLE id='tblItem" + rowTable + '' + colTable + "'"
			item +=    "  class='selectedItem' style='cursor:default' cellSpacing='0' cellPadding='0'"
			item +=    "  onclick=\"javascript:selectItem( this )\""
			item +=    "  ondblClick=\"javascript:checkLink( this )\" >"
			item +=	   "	<TR>"
			item +=    "		<TD align='left' width='20'>"
			item +=	   "			<img id='imgNewFolder" + rowTable + '' + colTable + "' src='icones/folder.gif' width=20 height=20 border=0>"
			item +=    "		</TD>"
			item +=	   "		<TD align='left' class='name' id='tdNewFolder" + rowTable + '' + colTable + "'>"
			item +=    "			<input maxlength=30 id='txtNewFolder' size=30 style='font-size:10;border:1px solid #0053a5' value='Nova Pasta' onblur=\"javascript:createFolder( this )\" onkeypress=\"javascript:return checkInputKey( this )\" >"
			item +=    "		</TD>"
			item +=    "	</TR>"
			item +=    "</TABLE>"	
		
		td.html(item)

		if (tBodyFolder.children()[0] == null) {
		    var tr = $(document.createElement("tr"))
		    tr.append(td)
		    tBodyFolder.append(tr)
		}
		else if (rowTable != repeatRows) {
		    $(tBodyFolder.children()[rowTable]).append(td)
		}
		else {
		    $(tBodyFolder.children()[0]).append(td)
		    rowTable = 0
		}
	}
	
	$( '#txtNewFolder' ).select()
	$( '#txtNewFolder' ).focus()
		
}

function createFolder( folder ){		
	if( existFolder( folder.value ) || folder.value == "" )
	{
		alert( "Já existe um diretório com esse nome ou nenhum foi especificado." )
		$('txtNewFolder').select()
		return
	}
	
	pathFolder = actualFolder + '\\' + folder.value
	pathFolder = ( pathFolder.indexOf( "\\" ) == 0 )? pathFolder.substring( 1 , pathFolder.length ) : pathFolder


	$.ajax({
	    url: "createFolder.aspx?folder=" + pathFolder,
	    success: function(data) {
	        $(folder).css("display", "none")

	        var tbl = $('#tblItem' + rowTable + '' + colTable)
	        var paramsTable = { 'row': rowTable, 'col': colTable, 'pathItem': escape(pathFolder), 'typeItem': 'folder', 'nameItem': escape(folder.value), 'description': 'Pasta de arquivos' }
	        tbl.attr("rel", JSON_stringify(paramsTable));
	        $('#newTd').attr("id", folder.value)

	        var title = "<a title='" + folder.value + "' >" + folder.value + "</a>"
	        if (typeView == "icons")
	            title = "<a title='" + folder.value + "' >" + ((folder.value.length > 15) ? folder.value.substring(0, 13) + "..." : folder.value) + "</a>"

	        $('#tdNewFolder' + +rowTable + '' + colTable).html(title)
	        $('#imgNewFolder' + +rowTable + '' + colTable).attr("alt", folder.value)

	        var item = $(actualXml.createElement("item"))
	        var name = $(actualXml.createElement("name"))
	        name.text(folder.value)

	        var image = $(actualXml.createElement("image"))
	        image.text("icones/folder.gif")

	        var path = $(actualXml.createElement("path"))
	        path.text(pathFolder)

	        var type = $(actualXml.createElement("type"))
	        type.text("folder")

	        var description = $(actualXml.createElement("description"))
	        description.text("Pasta de arquivos")

	        item.append(name)
	        item.append(image)
	        item.append(path)
	        item.append(type)
	        item.append(description)

	        $(actualXml.documentElement).append(item)

	        selectItem(tbl)
	        creatingFolder = false
	        if (typeView == "icons")
	            colTable++
	    },
	    error: function(data) {
	        alert("Ocorreu um problema ao tentar criar o novo diretório.")
	    }
	});
}

function existFolder( folder )
{
	var el = $(actualXml).find( "item" )
	var exist = false;
	
	for( x=0; x< el.length; x++ ) {
	    var text = $(el[x]).find("name").text().toLowerCase();
	    if (text == folder.toLowerCase())
		{
			exist = true
			break
		}
	}
	
	return exist
}

function uploaded()
{
    $("#upload").css("display", "block")
    $("#uploadBar").css("display", "none")	
	loadingFolder( actualFolder , true )
}  

function uploading()
{
	$("#upload").css("display", "none")
	$("#uploadBar").css("display", "block")
}

function uploadError()
{
	alert( "Erro ao efetuar o upload. Por favor tente novamente." )
	$("#upload").css("display", "block")
	$("#uploadBar").css("display", "none")
}

function checkInputKey( folder )
{
	var digitsNotAllowed = "\\\/:*?<>|\""

	var key = folder.keyCode
	var keyChar = String.fromCharCode( key ).toLowerCase()
	
	if( digitsNotAllowed.indexOf( keyChar ) > -1 )
		return false
		
	if( key == 13 )
		window.focus()

}

function display( objTable , objImage , origianlImg , modifiedImg ) {
    var objTable = $("#" + objTable)
    if (objTable.css("display") == "block") {
        objTable.css("display", "none")
        $("#" + objImage).attr("src", "imagens/lateral/" + modifiedImg)
    }
    else {
        objTable.css("display", "block")
        $("#" + objImage).attr("src", "imagens/lateral/" + origianlImg)
    }
}