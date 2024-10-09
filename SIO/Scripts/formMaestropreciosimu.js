var idiomaSeleccionado = 'es';
var RolUsuario = 0;
var nomUser = "";
var lstMonedas = [];
var lstPais = [];
var lstGrupoPais = [];
var lstPlantas = [];
var lstTipomp = [];
var lstCostomp = [];
var lstCostocifmod = [];
var lstItemCot = [];
var lstItemsC = [];
var lstCliente = [];

var formMon = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
});


$(document).on('inserted.bs.tooltip', function (e) {
    var tooltip = $(e.target).data('bs.tooltip');
    $(tooltip.tip).addClass($(e.target).data('tooltip-custom-classes'));
});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip({
        animated: 'fade',
        placement: 'bottom',
        html: true
    });

    doOnLoad();
    ObtenerRol();
    CargarDatosGenerales();
    SelectTab(event, 'Tab1');

});

function doOnLoad() {
    nomUser = "";
}

function Collapse(object) {
    if ($(object).children().attr("class").search("down") == -1) {
        $(object).children().removeClass();
        $(object).children().addClass("fa fa-angle-double-down");
        $("#body" + object.id.replace("collapse", "")).attr("style", "display: none; padding-top: 20px;margin-left: 15px;margin-right: 15px; ");
    }
    else {
        $(object).children().removeClass();
        $(object).children().addClass("fa fa-angle-double-up");
        $("#body" + object.id.replace("collapse", "")).attr("style", "display: normal; padding-top: 20px;margin-left: 15px;margin-right: 15px; ");
    }
}

function sLoading(text) {
    $.LoadingOverlay("show", {
        color: "rgba(0, 0, 0, 0.3)",
        image: "",
        custom: "<div style='color:white; font-size: 100px;' class='fa fa-refresh fa-spin'></div>",
        fade: false
    });
}

function hLoading() {
    $.LoadingOverlay("hide", true);
}

function mostrarLoad() {
    sLoading();
}

function ocultarLoad() {
    hLoading();
}

function llenarComboIdNombre(objeto, data) {
    $(objeto).get(0).options.length = 0;
    $(objeto).get(0).options[0] = new Option($.i18n('select_opcion'), "-1");

    $.each(data, function (index, item) {
        $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option(item.Descripcion, item.Id);
    });
}

function llenarComboId(listaDatos) {
    var stringDatos = "<option value='-1'>" + $.i18n('select_opcion') + "</option>";
    for (i = 0; i < listaDatos.length; i++) {
        stringDatos = stringDatos + "<option value='" + Number(listaDatos[i].id) + "'>" + listaDatos[i].descripcion + "</option>";
    }
    return stringDatos;
}

function llenarComboCliente(listaDatos) {
    var stringDatos = "<option value='-1'>" + $.i18n('select_opcion') + "</option>";
    for (i = 0; i < listaDatos.length; i++) {
        stringDatos = stringDatos + "<option value='" + Number(listaDatos[i].ClienteId) + "'>" + listaDatos[i].Cliente + "</option>";
    }
    return stringDatos;
}

function CargarDatosGenerales() {
    mostrarLoad();

    lstMonedas = [];
    lstPais = [];
    lstGrupoPais = [];
    lstPlantas = [];
    lstTipomp = [];
    lstCostomp = [];
    lstCostocifmod = [];
    lstItemCot = [];
    lstItemsC = [];
    lstCliente = [];

    $.ajax({
        type: "POST",
        url: "FormMaestroPreciosSimu.aspx/obtenerDatosGenerales",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varMoneda') {
                    lstMonedas = elem;
                    //$("#cboMonedaPv").html(llenarComboId(lstMonedas));
                    llenarComboIdNombre(cboMonedaPv,lstMonedas);
                }
                if (index == 'varGrupoPais') {
                    lstGrupoPais = elem;
                    $("#cboGrupoPais").html(llenarComboId(lstGrupoPais));
                    $("#cboGrupoPaisPv").html(llenarComboId(lstGrupoPais));
                }
                if (index == 'varPais') {
                    lstPais = elem;
                    $("#cboPais").html(llenarComboId(lstPais));
                    $("#cboPaisPv").html(llenarComboId(lstPais));
                }
                if (index == 'varNivel') {
                    lstNivel = elem;
                    $("#cboNivel").html(llenarComboId(lstNivel));
                }
                if (index == 'varMargen') {
                    lstCostomp = elem;
                    llenarCostomp(lstCostomp);
                }
                if (index == 'varPrecio') {
                    lstCostocifmod = elem;
                    llenarCostoCifMod(lstCostocifmod);
                }
                if (index == 'varItemCot') {
                    lstItemCot = elem;
                    llenarItemCot(lstItemCot);
                }
                if (index == 'varItemSCotiza') {
                    lstItemsC = elem;
                    $("#cboItemCot").html(llenarComboId(lstItemsC));
                }
                if (index == 'varCliente') {
                    lstCliente = elem;
                    $("#cboCliente").html(llenarComboCliente(lstCliente));
                }

            });
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to general data", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function llenarItemCot(data) {
    var rowsData = "";
    $.each(data, function (index, elem) {
        rowsData = rowsData + "<tr><td>" + elem.Item + "</td>" +
                    "<td align ='left'>>" + elem.Nivel + "</td>" +
                    //"<td><button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"\"> </button></td>" +
                    "</tr>";
    });
    if (rowsData.trim() == "") {
        rowsData = "<tr></tr>";
    }
    $("#tbodyic").html(rowsData);
}

function llenarCostomp(data) {
    var rowsData = "";
    $.each(data, function (index, elem) {
        rowsData = rowsData + "<tr><td align ='center'>" + elem.Nivel + "</td>" +
                    "<td align ='center'>" + elem.GrupoPais + "</td>" +
                    "<td align ='center'>" + elem.Pais + "</td>" +
                    "<td align ='right'>" + elem.Margen + "</td>" +
                    "<td align ='right'>" + elem.MargenMinimo + "</td>" +
                    "<td><button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"EditarMp('" + elem.NivelId + "','" + elem.GrupoPaId + "','" + elem.PaisId + "','" + elem.Margen + "','" + elem.MargenMinimo + "')\"> </button></td>" +
                    "</tr>";
    });
    if (rowsData.trim() == "") {
        rowsData = "<tr></tr>";
    }
    $("#tbodyma").html(rowsData);
}

function llenarCostoCifMod(data) {
    var rowsData = "";
    $.each(data, function (index, elem) {
        rowsData = rowsData + "<tr><td>" + elem.ItemCotizacion + "</td>" +
                    "<td align ='center'>" + elem.GrupoPais + "</td>" +
                    "<td align ='center'>" + elem.Pais + "</td>" +
                    "<td align ='center'>" + elem.Moneda + "</td>" +
                    "<td align ='right'>" + formMon.format(elem.Precio) + "</td>" +
                    "<td align ='center'>" + (elem.ClienteId) + "</td>" +
                    "<td align ='left'>" + (elem.Cliente) + "</td>" +
                    "<td><button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"EditarMo('" + elem.ItemId + "','" + elem.GrupoPaId + "','" + elem.PaisId + "','" + elem.MonedaId + "','" + elem.Precio + "','" + elem.ClienteId + "')\"> </button></td>" +
                    "</tr>";
    });
    if (rowsData.trim() == "") {
        rowsData = "<tr></tr>";
    }
    $("#tbodypr").html(rowsData);
    $('#tab_pr').DataTable();
}


function construirJson(data) {
    mostrarLoad();
    $("#DinamycSpace .ParteCabeceraDinamica").remove();
    var content = '';
    var cerraTitulo = false;
    var cerraT1 = false;
    var cerraT2 = false;
    var cDiv = '</div>'
    var numItems = 0;
    var suma = 0;

    data.forEach(function (item, i) {
        numItems += 1;

        switch (item.TipoRegistro) {
            case 1:
                if (cerraT1) {
                    content += '</tbody><tfoot class="thead-light"><tr><th colspan = 6></th><th>' + suma + '</th></tr></tfoot>';
                    content += '</table>';
                    cerraT1 = false;
                }
                if (cerraT2) {
                    content += '</tbody><tfoot class="thead-light"><tr><th colspan = 4></th><th >' + suma + '</th></tr></tfoot>';
                    content += '</table>';
                    cerraT2 = false;
                }
                suma = 0;
                if (cerraTitulo) {
                    content += cDiv + cDiv + cDiv ;
                }

                content +=
                    '<div class="col-md-12 ParteCabeceraDinamica" style="padding-top: 6x;" id="Parte' + item.Item + '"><div id="header' + item.Item + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                    '<table class="col-md-12 table-sm"><tr><td width="5%"><input type="checkbox" class"Enviar" data-item = "' + item.Item + '"/></td><td width="75%"><h5 class="box-title">  ITEM : ' + item.Item + ' - ' + item.Descripcion + '</h5></td><td width="20%"><h5 class="box-title">' + item.costo + '</h5></td>' +
                    '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + item.Item + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                    '<div id="body' + item.Item + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'
                cerraTitulo = true;
                break;
            case 2:
                if (!cerraT1) {
                    content += '<table class="table table-sm table-striped table-hover"><thead class="thead-light"><tr><th>COD MP</th><th>MATERIA PRIMA</th><th>DESC LIB</th><th>UNIDAD</th><th>PESO</th><th>COSTO ERP</th><th>COSTO</th></tr></thead><tbody>';
                    cerraT1 = true;
                    suma = 0;
                }
                content += '<tr><td>' + item.Mp_Cod_UnoEE + '</td><td>' + item.Mp_Nombre + '</td><td>' + item.Mp_Desc_Lib + '</td><td>' + item.Unidad +
                            '</td><td>' + item.peso + '</td><td>' + item.CostoERP + '</td><td >' + item.costo + '</td></tr>';
                suma += item.costo;
                break;
            case 3:
                if (!cerraT2) {
                    if (cerraT1) {
                        content += '</tbody><tfoot class="thead-light"><tr><th colspan = 6></th><th>' + suma + '</th></tr></tfoot>';
                        content += '</table>';
                        cerraT1 = false;
                        suma = 0;
                    }
                    content += '<table class="table table-sm table-striped table-hover"><thead class="thead-light"><tr><th>CENTRO DE TRABAJO</th><th>SEGMENTO DE COSTO</th><th>TARIFA</th><th>UNIDADES</th><th>COSTO</th></tr></thead><tbody>';
                    cerraT2 = true;
                    suma = 0;
                }
                content += '<tr><td>' + item.IdCentroTrabajo + '</td><td>' + item.SegmentoCosto + '</td><td>' + item.Tarifa + '</td><td>' + item.Unidades + '</td><td>' + item.costo + '</td></tr>';
                suma += item.costo;
                break;
        }
    });

    if (cerraT1) {
        content += '</tbody><tfoot class="thead-light"><tr><th colspan = 6></th><th>' + suma + '</th></tr></tfoot>';
        content += '</table>';
        cerraT1 = false;
    }
    if (cerraT2) {
        content += '</tbody><tfoot class="thead-light"><tr><th colspan = 4></th><th >' + suma + '</th></tr></tfoot>';
        content += '</table>';
        cerraT2 = false;
    }


    $("#DinamycSpace").append(content);

}

function calcular(object) {
    event.preventDefault();
    var row = $(object).parent().parent();

    var tipo = 1;
    if ($(object).hasClass("descuento"))
        tipo = 2;

    if ($(row).attr("data-tipo") == "3") {
        var valor = $($(row).children(".valor").children(".valor")).val();
        var unitario = $($(row).children(".unitario").children(".unitario")).val();
        var descuento = $($(row).children(".descuento").children(".descuento")).val();
        if (tipo == 1) {
            $($(row).children(".descuento").children(".descuento")).val((1 - (valor / unitario)) * 100);
        }
        else {
            $($(row).children(".valor").children(".valor")).val(unitario -  ((unitario * descuento ) / 100));
        }
    }

    if ($(object).hasClass("incluir"))
        row = $(object).parent().parent().parent();

    var flag = false;
    var result = 0;
    var prev = row;
    var next = row;
    while (!flag) {
        var prev = $(prev).prev();

        if (prev.length == 0 || ($(prev).attr("data-tipo") != "3" && $(prev).attr("data-tipo") != "5" && $(prev).attr("data-tipo") != "6" && $(prev).attr("data-tipo") != "7")) {
            flag = true;
            break;
        }
        else {
            result += calcularValor(prev);
        }
    }

    var flag = false;

    while (!flag) {
        var next = $(next).next();

        if (next.length == 0 || ($(next).attr("data-tipo") != "3" && $(next).attr("data-tipo") != "5" && $(next).attr("data-tipo") != "6" && $(next).attr("data-tipo") != "7")) {
            flag = true;
            break;
        }
        else {
            result += calcularValor(next);
        }
    }

    result += calcularValor(row);
    if ($(row).attr("data-tipo") == "6" || $(row).attr("data-tipo") == "7") {
        var subtotal = $(row).prevAll('div[data-tipo="4"]')[0]
        result += ($($(subtotal).children(".valorTotal").children(".valorTotal")).val() * 1);
    }

    var total = $(row).nextAll('div[data-tipo="4"]')[0]

    $($(total).children(".valorTotal").children(".valorTotal")).val(result);
}

function calcularValor(row) {
    event.preventDefault();
    if ($(row).attr("data-tipo") == "3") {
        //var valor = $($(row).children(".valor").children(".valor")).val();
        var valor = $($(row).children(".unitario").children(".unitario")).val();
        var descuento = $($(row).children(".descuento").children(".descuento")).val();

        return (valor == undefined || valor == "" ? 0 : valor) - ((valor == undefined || valor == "" ? 0 : valor) * ((descuento == undefined || descuento == "" ? 0 : descuento) / 100))
    }
    if ($(row).attr("data-tipo") == "5") {
        var valor = $($(row).children(".valor").children(".valor")).val();
        var descuento = $($(row).children(".descuento").children(".descuento")).val();
        var cantidad = $($(row).children(".cantidad").children(".cantidad")).val();
        var incluir = $($(row).children('.incluir')).children().children('.incluir').prop("checked");

        valor = (valor == undefined || valor == "" ? 0 : valor);

        if (incluir)
            return (valor == undefined || valor == "" ? 0 : valor) - ((valor == undefined || valor == "" ? 0 : valor) * ((descuento == undefined || descuento == "" ? 0 : descuento) / 100))
        else
            return 0;

    }
    if ($(row).attr("data-tipo") == "6" || $(row).attr("data-tipo") == "7") {
        var valor = $($(row).children(".valor").children(".valor")).val();
        return (valor == undefined || valor == "" ? 0 : valor) * 1;
    }
}
function guardarCot() {
    mostrarLoad();
    var data = [];


    $("[data-item]").each(function (i, row) {
        var valores =
        {
            IdFUP: IdFUPGuardado,
            Version: $('#cboVersion').val(), //versionFupDefecto,
            item_id: $(row).attr('data-item'),
            Item_det: $(row).attr('data-idet'),
            ItemCotiza_id : $(row).attr('data-icotiza')
        }

        switch (parseInt($(row).attr("data-tipo"))) {
            case 3:
                valores.Valor = $($(row).children('.valor')).children('.valor').val();
                valores.Descuento = $($(row).children('.descuento')).children('.descuento').val();
                break;
            //case 4:
            //    valores.Valor = $($(row).children('.valorTotal')).children('.valorTotal').val();
            //    break;
            case 5:
                valores.Valor = $($(row).children('.valor')).children('.valor').val();
                valores.Descuento = $($(row).children('.descuento')).children('.descuento').val();
                valores.Cantidad = $($(row).children('.cantidad')).children('.cantidad').val();
                valores.Incluir = $($(row).children('.incluir')).children().children('.incluir').prop("checked");
                break;
            case 6:
                valores.Valor = $($(row).children('.valor')).children('.valor').val();
                valores.Descuento = 0;
                break;
            case 7:
                valores.Valor = $($(row).children('.Observacion')).children('.Observacion').val();
                break;
            case 8:
                valores.Observacion = $($(row).children('.Observacion')).children('.Observacion').val();
                break;
        }
        data.push(valores);
    });


    $.ajax({
        type: "POST",
        url: "FormListadoItemsCosto.aspx/GuardarLineasDinamicas",
        data: JSON.stringify({ data: data }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            toastr.success('Guardado Correctamente.');
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to save dynamic controls", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });


}

function ObtenerLineasDinamicas(IdOrden , Tipo) {
    mostrarLoad();
    var param =
    {
        pOrden: IdOrden
    };

    $.ajax({
        type: "POST",
        url: "FormListadoItemsCosto.aspx/ObtenerLineasDinamicas",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var data = JSON.parse(result.d);
            construirJson(data);

            ocultarLoad();
        },
        error: function () {
            toastr.error("Failed to load dynamic controls", "Listado Item Costo", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function cargarItems() {
    event.preventDefault();
    mostrarLoad();
    var param =
    {
        pOrden: IdOfaP
    };

    $.ajax({
        type: "POST",
        url: "FormListadoItemsCosto.aspx/CargueItemsCosto",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ocultarLoad();
            toastr.success("Cargado Correctamente");
            CargarDatosGenerales();
            $("#DinamycSpace .ParteCabeceraDinamica").remove();
        },
        error: function () {
            toastr.error("Error al Cargar los datos de Items de Costo", "Listado Item Costo", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function EnviarItemsErp() {
    event.preventDefault();
    var data = [];

    $("[data-item]").each(function (i, row) {
        if ($(row).is(":checked")) {
            data.push($(row).attr('data-item'));
        }
    });
    var x = data.toString();
    if (x.length > 1) {
        mostrarLoad();
        var param =
        {
            items: x
        };
        $.ajax({
            type: "POST",
            url: "FormListadoItemsCosto.aspx/EnviarWebService",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                ocultarLoad();
                toastr.warnig("Cargue Items de Costo Al ERP " + msg.d, "Listado Item Costo", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            },
            error: function () {
                ocultarLoad();
                toastr.error("Error al Cargar los datos de Items de Costo Al ERP", "Listado Item Costo", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
    else {
        toastr.error("Error debe seleccionar los Items para cargar al ERP", "Listado Item Costo", {
            "timeOut": "0",
            "extendedTImeout": "0"
        });
    }
}

function ObtenerRol() {
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/ObtenerRol",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            RolUsuario = data.Rol;
            nomUser = data.username;
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Obtener Rol", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function SelectTab(evt, tabName) {
    evt.preventDefault();
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
    evt.stopPropagation();
}

function EditarMp(NivelId, GrupoPais, Pais, Margen, MargenMinimo) {
    //$("#txtFechaVigencia").val(FecVigMp);
    //$("#txtFechaVigencia").prop("disabled", true)
    $("#cboNivel").val(NivelId).change();
    $("#cboNivel").prop("disabled", true)
    $("#cboGrupoPais").val(GrupoPais).change();
    $("#cboGrupoPais").prop("disabled", true)
    $("#cboPais").val(Pais).change();
    $("#cboPais").prop("disabled", true)
    $("#txtPorcentajeMargen").val(Margen);
    $("#txtPorcentajeMargenMin").val(MargenMinimo);
    ControlInsertarShow('Porcentaje Márgenes por Nivel', 0, 1, 'Modificar Porcentaje Márgen por Nivel')
}

function EditarMo(ItemCot, GrupoPais, Pais, MonedaPv, PrecioPv, ClienteId) {
    $("#cboItemCot").val(ItemCot).change();
    $("#cboItemCot").prop("disabled", true)
    $("#cboGrupoPaisPv").val(GrupoPais).change();
    $("#cboGrupoPaisPv").prop("disabled", true)
    $("#cboPaisPv").val(Pais).change();
    $("#cboPaisPv").prop("disabled", true)
    $("#cboMonedaPv").val(MonedaPv).change();
    $("#cboMonedaPv").prop("disabled", false)
    $("#txtPrecioPv").val(PrecioPv);
    if (ClienteId = -1) {
        $("#cboCliente").val(0).change();
    }
    else {
        $("#cboCliente").val(ClienteId).change();
    }
    //$("#cboCliente").val(ClienteId).change();
    $("#cboCliente").prop("disabled", false)
    ControlInsertarShowMo('Precios de Venta por Grupo / Región', 0, 1, 'Modificar Precio de Venta por Grupo / Región')
}

function InsertarIt() {
}

function InsertarPj() {
    $("#cboNivel").val(-1).change();
    $("#cboNivel").prop("disabled", false)
    $("#cboGrupoPais").val(-1).change();
    $("#cboGrupoPais").prop("disabled", false)
    $("#cboPais").val(-1).change();
    $("#cboPais").prop("disabled", false)
    $("#txtPorcentajeChatarra").val(0);
    $("#txtPorcentajeChatarraPiezas").val(0);
    ControlInsertarShow('Porcentaje Márgenes por Nivel', 0, 0, 'Agregar Porcentaje Márgen por Nivel')
}

function InsertarPv() {
    $("#cboItemCot").val(-1).change();
    $("#cboItemCot").prop("disabled", false)
    $("#cboGrupoPaisPv").val(-1).change();
    $("#cboGrupoPaisPv").prop("disabled", false)
    $("#cboPaisPv").val(-1).change();
    $("#cboPaisPv").prop("disabled", false)
    $("#cboMonedaPv").val(-1).change();
    $("#cboMonedaPv").prop("disabled", false)
    $("#txtPrecioPv").val(0);
    $("#cboCliente").val(-1).change();
    $("#cboCliente").prop("disabled", false)
     ControlInsertarShowMo('Precios de Venta por Grupo / Región', 0, 0, 'Agregar Precio de Venta por Grupo / Región')
}

function ControlInsertarShow(title, optionDefault, Respuesta, Titulo) {
    if (optionDefault) {
        $("#ModControlInsertar").val(optionDefault).prop("disabled", true)
    }
    else {
        $("#ModControlInsertar").val(-1).prop("disabled", false)
    }
    $("#padreCambio").val(Respuesta);

    if (Titulo.length > 0) {
        $("#txtTituloObs").val(Titulo)
        $("#txtTituloObs").prop("disabled", true);
    }
    else
        $("#txtTituloObs").prop("disabled", false);
    //if (Respuesta == 0)
    $("#AreaControl").hide();
    //else {
    //    $("#txtFechaVigencia").val("Comentario Padre : " + Respuesta);
    //    $("#AreaControl").show();
    //}
    //$("#txtObsCntrCm").val("");
    var modal = $("#ModControlInsertar");
    modal.find('.modal-title').text(title);
    modal.modal('show');
}

function ControlInsertarShowMo(title, optionDefault, Respuesta, Titulo) {
    if (optionDefault) {
        $("#MaoControlInsertar").val(optionDefault).prop("disabled", true)
    }
    else {
        $("#MaoControlInsertar").val(-1).prop("disabled", false)
    }
    $("#padreCambioMao").val(Respuesta);

    if (Titulo.length > 0) {
        $("#txtTituloObsMao").val(Titulo)
        $("#txtTituloObsMao").prop("disabled", true);
    }
    else
        $("#txtTituloObsMao").prop("disabled", false);
    //if (Respuesta == 0)
    $("#AreaControlMao").hide();
    //else {
    //    $("#txtFechaVigencia").val("Comentario Padre : " + Respuesta);
    //    $("#AreaControlMao").show();
    //}
    //$("#txtObsCntrCm").val("");
    var modal = $("#MaoControlInsertar");
    modal.find('.modal-title').text(title);
    modal.modal('show');
}


function btnCntrlInsertaf() {
    mostrarLoad();

    var param = {
        NivelId: $("#cboNivel").val(),
        GrupoPaId: $("#cboGrupoPais").val(),
        PaisId: $("#cboPais").val(),
        Margen: $("#txtPorcentajeMargen").val(),
        MargenMinimo: $("#txtPorcentajeMargenMin").val()            
    };

    $.ajax({
        type: "POST",
        url: "FormMaestroPreciosSimu.aspx/GuardarMargenesNivel",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            toastr.success("Guardado Correctamente % Margen Nivel", "% Margen Nivel");
            CargarDatosGenerales();
            var modal = $("#ModControlInsertar");
            modal.modal('hide');
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Guardar % Margen Nivel", "% Margen Nivel", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });

}


function btnCntrlInsertaMaf() {
    mostrarLoad();

    var param = {
        ItemId: $("#cboItemCot").val(),
        GrupoPaId: $("#cboGrupoPaisPv").val(),
        PaisId: $("#cboPaisPv").val(),
        MonedaId: $("#cboMonedaPv").val(),
        Precio: $("#txtPrecioPv").val(),
        ClienteId: $("#cboCliente").val()
        };

    $.ajax({
        type: "POST",
        url: "FormMaestroPreciosSimu.aspx/GuardarPreciosVentaRegion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            toastr.success("Guardado Correctamente Precios Venta Region", "Precios Venta Region");
            CargarDatosGenerales();
            var modal = $("#MaoControlInsertar");
            modal.modal('hide');
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Guardar Precios Venta Region", "Precios Venta Region", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}
