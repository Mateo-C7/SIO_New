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
var lstTurno = [];

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

    //$('.select-filter').select2();
    $("#btnBuscar").val("0");

    doOnLoad();
    ObtenerRol();
    LlenarComboMes(cboMesTurno);
    if (localStorage.getItem("filterSetAlreadyProdCalendarioTurnos") == "1") {
        $("#txtFechaIni").val(localStorage.getItem("ProdCalendarioTurnosFecIni"));
        $("#txtFechaFin").val(localStorage.getItem("ProdCalendarioTurnosFecFin"));
        CargarDatosporFecha();
    } else {
        CargarDatosGenerales();
    }
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    today = dd + '/' + mm + '/' + yyyy;
    $("#txtFechaIni").val(today);
    var otoday = new Date();
    var dd = String(otoday.getDate()).padStart(2, '0');
    var mm = String(otoday.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = otoday.getFullYear();
    otoday = dd + '/' + mm + '/' + yyyy;
    $("#txtFechaFin").val(otoday);

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

function LlenarComboMes(objeto) {
    $(objeto).get(0).options.length = 0;
    $(objeto).get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
    for (i = 1; i <= 12; i++) {
        switch (i) {
            case 1:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Enero", i);
                break;
            case 2:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Febrero", i);
                break;
            case 3:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Marzo", i);
                break;
            case 4:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Abril", i);
                break;
            case 5:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Mayo", i);
                break;
            case 6:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Junio", i);
                break;
            case 7:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Julio", i);
                break;
            case 8:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Agosto", i);
                break;
            case 9:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Septiembre", i);
                break;
            case 10:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Octubre", i);
                break;
            case 11:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Noviembre", i);
                break;
            case 12:
                $(objeto).get(0).options[$(objeto).get(0).options.length] = new Option("Diciembre", i);
                break;
        }
    }

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
    var stringDatos = "<option value='-1'>" + $.i18n(' - Todos - ') + "</option>";
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
        url: "FormMaestroProdCalendarioTurnos.aspx/obtenerDatosGenerales",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varTurnos') {
                    lstTurno = elem;
                    //llenarCostoCifMod(lstTurno);
                }

            });
            ocultarLoad();
            SelectTab(event, 'Tab1');
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

function CargarDatosporFecha() {
    mostrarLoad();
    lstTurno = [];
    var FecIni = "";
    var FecFin = "";

    if ($("#btnBuscar").val() == "1") {
        FecIni = $("#txtFechaIni").val();
        FecFin = $("#txtFechaFin").val();
    } else { // Se accede a las fechas guardadas en el localStorage
        FecIni = localStorage.getItem("ProdCalendarioTurnosFecIni");
        FecFin = localStorage.getItem("ProdCalendarioTurnosFecFin");
    }

    var param = {
        FechaIni: FecIni,
        FechaFin: FecFin
    };       

    $.ajax({
        type: "POST",
        url: "FormMaestroProdCalendarioTurnos.aspx/obtenerDatosporFecha",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varTurnos') {
                    lstTurno = elem;
                    llenarCostoCifMod(lstTurno);
                }

            });
            ocultarLoad();
            SelectTab(event, 'Tab1');
            localStorage.setItem("filterSetAlreadyProdCalendarioTurnos", "1");
            localStorage.setItem("ProdCalendarioTurnosFecIni", FecIni);
            localStorage.setItem("ProdCalendarioTurnosFecFin", FecFin);
        },
        error: function () {
            ocultarLoad();
            $("#btnBuscar").val("0");
            toastr.error("Failed to general data", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function llenarCostoCifMod(data) {
    var rowsData = "";
    $("#tbodypr").html(rowsData);

    $.each(data, function (index, elem) {
        rowsData = rowsData + "<tr><td>" + elem.pct_id + "</td>" +
            "<td align ='center'>" + elem.pct_Fecha.substring(0, 10) + "</td>" +
            "<td align ='center'>" + elem.pct_CantTurnos + "</td>" +
            "<td align ='center'>" + elem.pct_CantTurnosBr + "</td>" +
            "<td align ='center'>" + elem.pct_SemanaMes + "</td>" +
            "<td align ='center'>" + elem.pct_Mes + "</td>" +
            //"<td align ='center'>" + elem.pct_usuarioCrea + "</td>" +
            //"<td align ='left'>" + elem.pct_fechaCrea + "</td>" +
            "<td><button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"EditarMo('" + elem.pct_id + "','" + elem.pct_Fecha + "','" + elem.pct_CantTurnos + "','" + elem.pct_SemanaMes + "','" + elem.pct_Mes + "','" + elem.pct_CantTurnosBr + "')\"> </button></td>" +
            "</tr>";
    });

    if (rowsData.trim() == "") {
        rowsData = "<tr></tr>";
    }
    $("#tbodypr").html(rowsData);

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

function btnCntrlInsertaf() {
    mostrarLoad();

    var filterSetAlready = $("#btnBuscar").val();

    var param = {
        pct_id: $("#txtId").val(),
        pct_Fecha: $("#txtFecha").val(),
        pct_CantTurnos: $("#txtCantTurnos").val(),
        pct_CantTurnosBr: $("#txtCantTurnosBr").val(),
        pct_SemanaMes: $("#txtSemanaMes").val(),
        pct_Mes: $("#cboMesTurno").val(),
        pct_usuarioCrea: nomUser
    };

    $.ajax({
        type: "POST",
        url: "FormMaestroProdCalendarioTurnos.aspx/GuardarProdCalendarioTurnos",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            toastr.success("Guardado Correctamente Producto Calendario Turnos", "Producto Calendario Turnos");
            if (filterSetAlready == "1" ||
                (localStorage.getItem("filterSetAlreadyProdCalendarioTurnos") != null && localStorage.getItem("filterSetAlreadyProdCalendarioTurnos") == "1")) {
                CargarDatosporFecha();
            } else {
                CargarDatosGenerales();
            }            
            var modal = $("#ModControlInsertar");
            modal.modal('hide');
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Guardar Producto Calendario Turnos", "Producto Calendario Turnos", {
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

function EditarMo(pct_id, pct_Fecha, pct_CantTurnos, pct_SemanaMes, pct_Mes, pct_CantTurnosBr) {
    $("#txtId").val(pct_id);
    $("#txtId").prop("disabled", true);
    $("#txtFecha").val(pct_Fecha.substring(0, 10));
    $("#txtFecha").prop("disabled", true);
    $("#txtCantTurnos").val(pct_CantTurnos);
    $("#txtCantTurnos").prop("disabled", false);
    $("#txtCantTurnosBr").val(pct_CantTurnosBr);
    $("#txtCantTurnosBR").prop("disabled", false);
    $("#txtSemanaMes").val(pct_SemanaMes);
    $("#txtSemanaMes").prop("disabled", true);
    var select = document.getElementById("cboMesTurno");
    // recorremos todos los valores del select
    for (var i = 0; i < select.length; i++) {
        if (select.options[i].text == pct_Mes) {
            // seleccionamos el valor que coincide
            select.selectedIndex = i;
        }
    }
    $("#cboMesTurno").prop("disabled", true);   
    ControlInsertarShow('Producto Calendario Turnos', 0, 1, 'Modificar Producto Calendario Turnos')
}

function InsertarPv() {
    $("#txtId").val(0);
    $("#txtId").prop("disabled", true)
    //var hoy = new Date.now();
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    today = dd + '/' + mm + '/' + yyyy;
    $("#txtFecha").val(today);
    $("#txtFecha").prop("disabled", false)
    $("#txtCantTurnos").val(0);
    $("#txtSemanaMes").val(0);
    $("#txtSemanaMes").prop("disabled", false)
    $("#cboMesTurno").val(-1).change();
    $("#cboMesTurno").prop("disabled", false);
    ControlInsertarShow('Producto Calendario Turnos', 0, 0, 'Agregar Producto Calendario Turnos')
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
        $("#ModControlInsertar").val(optionDefault).prop("disabled", true)
    }
    else {
        $("#ModControlInsertar").val(-1).prop("disabled", false)
    }
    $("#padreCambio").val(Respuesta);

    if (Titulo.length > 0) {
        $("#txtTituloObsMao").val(Titulo)
        $("#txtTituloObsMao").prop("disabled", true);
    }
    else
        $("#txtTituloObsMao").prop("disabled", false);
    //if (Respuesta == 0)
    $("#AreaControl").hide();
    //else {
    //    $("#txtFechaVigencia").val("Comentario Padre : " + Respuesta);
    //    $("#AreaControlMao").show();
    //}
    //$("#txtObsCntrCm").val("");
    var modal = $("#MaoControlInsertar");
    modal.find('.modal-title').text(title);
    modal.modal('show');
}

function Buscar(tabName) {
    $("#btnBuscar").val("1");
    CargarDatosporFecha();
}
