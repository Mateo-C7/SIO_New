var idiomaSeleccionado = 'es';
var IdFUPGuardado = null;
var RolUsuario = 0;
var nomUser = "";
var IdOfaP = 10516935;

var formMon = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    currencyDisplay: 'narrowSymbol',
    maximumFractionDigits: 2,
    minimunFractionDigits: 2
});

var formNum = new Intl.NumberFormat('en-US', {
    style: 'decimal',
    maximumFractionDigits: 4,
    minimunFractionDigits: 4
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

    $('.select-filter').select2();

    $("#btnBusFup").click(function (event) {
        event.preventDefault();
        obtenerRayasPorOrden(IdOfaP,1);
        
    });

    $("#cboRaya").change(function () {
        obtenerRayasPorOrden($(this).val(),2);
    });

    $('#checkAll').click(function () {
        $('input:checkbox').prop('checked', this.checked);
    });

    doOnLoad();
    ObtenerRol();
    CargarDatosGenerales();

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
        $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option(item.Nombre, item.Id);
    });
}

function llenarComboId(listaDatos) {
    var stringDatos = "<option value='-1'>" + $.i18n('select_opcion') + "</option>";
    for (i = 0; i < listaDatos.length; i++) {
        stringDatos = stringDatos + "<option value='" + Number(listaDatos[i].id) + "'>" + listaDatos[i].descripcion + "</option>";
    }
    return stringDatos;
}

function obtenerRayasPorOrden(idOrden, tipo) {
    ObtenerLineasDinamicas(idOrden, tipo);
}

function CargarDatosGenerales() {
    mostrarLoad();
    var param =
    {
        pOrden: IdOfaP
    };
    $.ajax({
        type: "POST",
        url: "FormListadoItemsCosto.aspx/CargueRayasOrden",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = JSON.parse(msg.d);
            $("#txtOrden").val(data.Orden.descripcion);
            $("#txtOrden").attr("disabled", "disabled");
            $("#cboRaya").html("");
            $("#cboRaya").html(llenarComboId(data.Rayas));

            
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
                    content += '</tbody><tfoot class="thead-light"><tr><th colspan = 6></th><th>' + formMon.format(suma) + '</th></tr></tfoot>';
                    content += '</table>';
                    cerraT1 = false;
                }
                if (cerraT2) {
                    content += '</tbody><tfoot class="thead-light"><tr><th colspan = 4></th><th >' + formMon.format(suma) + '</th></tr></tfoot>';
                    content += '</table>';
                    cerraT2 = false;
                }
                suma = 0;
                if (cerraTitulo) {
                    content += cDiv + cDiv + cDiv ;
                }
                
                content +=
                    '<div class="col-md-12 ParteCabeceraDinamica" style="padding-top: 6x;" id="Parte' + item.Item + '"><div id="header' + item.Item + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                    '<table class="col-md-12 table-sm"><tr><td width="5%">' + (item.CostoCero > 0 ? '' : '<input type="checkbox" class"Enviar" data-item = "' + item.Item + '"/>') + '</td><td width="45%"><h5 class="box-title">  ITEM : ' + item.Item + ' - ' + item.Descripcion + '</h5></td><td class=""><h5 class="box-title">PESO TOTAL: ' + formNum.format(item.peso) + '</h5></td><td width="20%"><h5 class="box-title">' + formMon.format(item.costo) + '</h5></td>' +
                    '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + item.Item + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                    '<div id="body' + item.Item + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'
                cerraTitulo = true;
                break;
            case 2:
                if (!cerraT1) {
                    content += '<table class="table table-sm table-striped table-hover"><thead class="thead-light"><tr><th>COD MP</th><th>MATERIA PRIMA</th><th>DESC LIB</th><th>UND</th><th class="text-center">CANT</th><th class="text-center">PESO</th><th class="text-right">COSTO ERP</th><th class="text-right">COSTO</th></tr></thead><tbody>';
                    cerraT1 = true;
                    suma = 0;
                }
                //                content += '<tr><td>' + item.Mp_Cod_UnoEE + '</td><td>' + item.Mp_Nombre + '</td><td>' + item.Mp_Desc_Lib + '</td><td>' + item.Unidad +
                content += '<tr' + (item.Mp_Cod_UnoEE > 0 ? '' :' class="table-danger" ') + '><td>' + item.Mp_Cod_UnoEE + '</td><td>' + item.Mp_Nombre + '</td><td>' + item.Mp_Desc_Lib + '</td><td>' + item.Unidad +
                            '</td><td class="text-center">' + formNum.format(item.Cantidad) + '</td><td aling="right" class="text-center">' + formNum.format(item.peso) + '</td><td aling="right" class="text-right">' + formMon.format(item.CostoERP) + '</td><td class="text-right">' + formMon.format(item.costo) + '</td></tr>';
                suma += item.costo;
                break;
            case 3:
                if (!cerraT2) {
                    if (cerraT1) {
                        content += '</tbody><tfoot class="thead-light"><tr><th colspan = 7></th><th>' + formMon.format(suma) + '</th></tr></tfoot>';
                        content += '</table>';
                        cerraT1 = false;
                        suma = 0;
                    }
                    content += '<table class="table table-sm table-striped table-hover"><thead class="thead-light"><tr><th>CENTRO DE TRABAJO</th><th>SEGMENTO DE COSTO</th><th class="text-right">TARIFA</th><th class="text-right">HORAS</th><th class="text-right">COSTO</th></tr></thead><tbody>';
                    cerraT2 = true;
                    suma = 0;
                }
                content += '<tr><td>' + item.IdCentroTrabajo + '</td><td>' + item.SegmentoCosto + '</td><td class="text-right">' + formMon.format(item.Tarifa) + '</td><td class="text-right">' + formNum.format(item.Unidades) + '</td><td aling="right" class="text-right">' + formMon.format(item.costo) + '</td></tr>';
                suma += item.costo;
                break;
        }
    });

    if (cerraT1) {
        content += '</tbody><tfoot class="thead-light"><tr><th colspan = 6></th><th aling="right">' + formMon.format(suma) + '</th></tr></tfoot>';
        content += '</table>';
        cerraT1 = false;
    }
    if (cerraT2) {
        content += '</tbody><tfoot class="thead-light"><tr><th colspan = 4></th><th >' + formMon.format(suma) + '</th></tr></tfoot>';
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
        error: function (jqXHR) {
            toastr.error("Error al Cargar los datos de Items de Costo - " + jqXHR.responseText, "Listado Item Costo", {
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
    if (data.length > 0) {
        mostrarLoad();
        var param =
        {
            items: data,
            pOrden: $("#cboRaya").val(),
            pDestino: $("#cboDestino").val()
        };
        $.ajax({
            type: "POST",
            url: "FormListadoItemsCosto.aspx/EnviarWebService",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                ocultarLoad();
                $('#modalBodyCargueMensaje').html(msg.d);
                $('#cargueMensaje').modal('show');
                /*toastr.warning("Cargue Items de Costo Al ERP \n" + msg.d, "\nListado Item Costo", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });*/
            },
            error: function (jqXHR) {
                ocultarLoad();
                toastr.error("Error al Cargar los datos de Items de Costo Al ERP\n" + jqXHR.responseText, "Listado Item Costo", {
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