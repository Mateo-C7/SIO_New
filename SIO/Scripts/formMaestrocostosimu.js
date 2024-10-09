var idiomaSeleccionado = 'es';
var RolUsuario = 0;
var nomUser = "";
var lstMonedas = [];
var lstPlantas = [];
var lstTipomp = [];
var lstOrigenmp = [];
var lstCostomp = [];
var lstCostocifmod = [];
var table_materiaprima = null;
var table_manoobracif = null;
var currentIndexRow = null;

var formMon = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
});

var formKilo = new Intl.NumberFormat('en-US', { maximumFractionDigits: 2, minimumFractionDigits: 2 });

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

    doOnLoad();
    ObtenerRol();
    CargarDatosGenerales('Tab1');

    $("#cboOrigenMp").change(function () {
        if ($(this).val() == 3) {
            $("#txtCosto2").prop("disabled", false);
        }
        else {
            $("#txtCosto2").prop("disabled", true);
        }
    });

    $('#btnBuscarMp').val('0');
    $('#btnBuscarMo').val('0');

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

function CargarDatosGenerales(tabSel) {
    mostrarLoad();

    $.ajax({
        type: "POST",
        url: "FormMaestroCostoSimu.aspx/obtenerDatosGenerales",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varMoneda') {
                    lstMonedas = elem;
                }
                if (index == 'varTipoMp') {
                    lstTipomp = elem;
                    $("#cboMateriaPrima").html(llenarComboId(lstTipomp));                   
                }

                if (index == 'lstOrigenMp') {
                    lstOrigenmp = elem;
                    $("#cboOrigenMp").html(llenarComboId(lstOrigenmp));
                }

                if (index == 'varPlantas') {
                    lstPlantas = elem;
                    $("#cboPlanta").html(llenarComboId(lstPlantas));
                    $("#cboPlantaMao").html(llenarComboId(lstPlantas));                   
                }

                if (index == 'varCostoMp') {
                    lstCostomp = elem;
                    llenarCostomp(lstCostomp);
                }

                if (index == 'varTipoFormaleta') {
                    lstTipoFormaletas = elem;
                    $("#cboFormaletaMao").html(llenarComboId(lstTipoFormaletas));
                }

                if (index == 'varCostoCifMod') {
                    lstCostocifmod = elem;
                    llenarCostoCifMod(lstCostocifmod);
                }
                
            });
            ocultarLoad();
            /*            SelectTab(event, 'Tab1'); */
            if (tabSel == "") { tabSel = 'Tab1'; }
            SelectTab(event, tabSel);
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

function llenarCostomp(data) {
    var classvigente = "";
    table_materiaprima = $("#tab_mp").DataTable({
        order: [[0, 'desc']]
    });
        $.each(data, function (index, elem) {

            if (elem.Vigente) { classvigente = ' class="table-active" ' } else { classvigente = "" }
            let button = "";
            if (elem.Origen == "Transito") button = "<td><button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"EditarMp('" + elem.FechaVigencia.substring(0, 10) + "','" + elem.PlantaId + "','" + elem.OrigenId + "','" + elem.TipoMpId + "','" + elem.costo + "','" + elem.costo2 + "'," + elem.Kilos + ",'" + elem.ValorLme + "','" + elem.ValorLme2 + "','" + elem.Observaciones + "','" + index + "')\"> </button></td>";
            else button = ""

            table_materiaprima.row.add([
                //"<tr " + classvigente + "><td>" + elem.FechaVigencia.substring(0, 10) + "</td>",
                elem.FechaVigencia.substring(0, 10),
                elem.Planta,
                elem.Origen,
                elem.TipoMateria,
                formKilo.format(elem.Kilos),
                formMon.format(elem.CostoMP_Kilo),
                formMon.format(elem.costo),
                formMon.format(elem.costo2),
                formMon.format(elem.ValorLme),
                formMon.format(elem.ValorLme2),
                elem.Observaciones,
                button
            ]).draw(false);
        });
        table_materiaprima.draw();    
}

function llenarCostoCifMod(data) {
    table_manoobracif = $("#tab_mc").DataTable({
        order: [[0, 'desc']]
    });
    $.each(data, function (index, elem) {
        table_manoobracif.row.add([
            elem.FechaVigencia.substring(0, 10),
            elem.Planta,
            elem.TipoFormaleta,
            formMon.format(elem.CostoMOD),
            formMon.format(elem.CostoCIF),
            (elem.pChatarra * 100),
            (elem.pChatarra240 * 100),
            "<button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"EditarMo('" + elem.FechaVigencia.substring(0, 10) + "','" + elem.PlantaId + "','" + elem.CostoMOD + "','" + elem.CostoCIF + "','" + elem.pChatarra + "','" + elem.pChatarra240 + "','" + elem.TipoFormaletaId + "','" + index + "')\"> </button>"
        ]).draw(false);
    });
    table_manoobracif.draw();
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

function EditarMp(FecVigMp, PlantaMp, OrigenId, TpMateriaMp, CostoMp, Costo2, Kilos, ValorLme, ValorLme2, Observaciones, indexRow) {
    currentIndexRow = parseInt(indexRow);
    $("#txtFechaVigencia").val(FecVigMp);
    $("#txtFechaVigencia").prop("disabled", true)
    $("#cboPlanta").val(PlantaMp).change();
    $("#cboPlanta").prop("disabled", true)
    $("#cboOrigenMp").val(OrigenId).change();
    $("#cboOrigenMp").prop("disabled", true)
    $("#cboMateriaPrima").val(TpMateriaMp).change();
    $("#cboMateriaPrima").prop("disabled", true)
    $("#txtKilos").val(Kilos);
    $("#txtCosto").val(CostoMp);
    $("#txtCosto2").val(Costo2);
    $("#txtValorLme").val(ValorLme);
    $("#txtValorLme2").val(ValorLme2);
    $("#txtObservaciones").val(Observaciones);
    ControlInsertarShow('Costos Materia Prima', 0, 1, 'Modificar Costos de Materia Prima')
}

function EditarMo(FecVigMp, PlantaMp, CostoMOD, CostoCIF, Chatarra, ChatarraP, FormaletaId, indexRow) {
    currentIndexRow = parseInt(indexRow);
    Chatarra = Chatarra * 100;
    ChatarraP = ChatarraP * 100;
    $("#txtFechaVigenciaMao").val(FecVigMp);
    $("#txtFechaVigenciaMao").prop("disabled", true)
    $("#cboPlantaMao").val(PlantaMp).change();
    $("#cboPlantaMao").prop("disabled", true)
    $("#txtCostoMod").val(CostoMOD);
    $("#txtCostoCif").val(CostoCIF);
    $("#txtPorcentajeChatarra").val(Chatarra);
    $("#txtPorcentajeChatarraPiezas").val(ChatarraP);
    $("#cboFormaletaMao").val(FormaletaId).change();
    ControlInsertarShowMo('Costos Materia Prima', 0, 1, 'Modificar Costos de Mano de Obra')
}

function InsertarMp() {
    $("#txtFechaVigencia").val("");
    $("#txtFechaVigencia").prop("disabled", false)
    $("#cboPlanta").val(1).change();
    $("#cboPlanta").prop("disabled", true)
    $('#cboOrigenMp option:eq(1)').attr('disabled', true);
    $('#cboOrigenMp option:eq(2)').attr('disabled', true);
    $("#cboOrigenMp").val(-1).change();
    $("#cboOrigenMp").prop("disabled", false)
    $("#cboMateriaPrima").val(-1).change();
    $("#cboMateriaPrima").prop("disabled", false)
    $("#txtKilos").val(0);
    $("#txtCosto").val(0);
    $("#txtCosto2").val(0);
    $("#txtValorLme").val(0);
    $("#txtValorLme2").val(0);
    $("#txtObservaciones").val("");
    currentIndexRow = null;
    ControlInsertarShow('Costos Materia Prima', 0, 0, 'Agregar Costo de Materia Prima');
}

function InsertarMo() {
    $("#txtFechaVigenciaMao").val("");
    $("#txtFechaVigenciaMao").prop("disabled", false)
    $("#cboPlantaMao").val(1).change();
    $("#cboPlantaMao").prop("disabled", true)
    $("#txtCostoMod").val(0);
    $("#txtCostoCif").val(0);
    $("#txtPorcentajeChatarra").val(0);
    $("#txtPorcentajeChatarraPiezas").val(0);
    currentIndexRow = null;
    ControlInsertarShowMo('Costos Mano de Obra', 0, 0, 'Agregar Costo de Mano de Obra');
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
    $("#AreaControl").hide();
    if ($("#cboOrigenMp").val() == 3)  // || $("#cboOrigenMp").text == "Por Consumo")
    { $("#txtCosto2").prop("disabled", false) }
    else
    { $("#txtCosto2").prop("disabled", true) }

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
    var param = {
        FechaVigencia: $("#txtFechaVigencia").val(),
        Planta: $("#cboPlanta").val(),
        Origen: $("#cboOrigenMp").val(),
        MateriaPrima: $("#cboMateriaPrima").val(),
        CostoMp: $("#txtCosto").val(),
        Costo2: $("#txtCosto2").val(),
        kilos: $("#txtKilos").val(),
        ValorLme: $("#txtValorLme").val(),
        ValorLme2: $("#txtValorLme2").val(),
        Observaciones: $("#txtObservaciones").val()
    };

    let button = "";
    if ($("#cboOrigenMp option:selected").text() == "Transito")
        button = "<td><button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"EditarMp('" + $("#txtFechaVigencia").val().substring(0, 10) + "','" + $("#cboPlanta").val() + "','" + $("#cboOrigenMp").val() + "','" + $("#cboMateriaPrima").val() + "','" + $("#txtCosto").val() + "','" + $("#txtCosto2").val() + "'," + $("#txtKilos").val() + ",'" + $("#txtValorLme").val() + "','" + $("#txtValorLme2").val() + "','" + $("#txtObservaciones").val() + "','" + currentIndexRow + "')\"> </button></td>";
    else button = ""

    let rowData = [
        //"<tr " + classvigente + "><td>" + elem.FechaVigencia.substring(0, 10) + "</td>",
        $("#txtFechaVigencia").val().substring(0, 10),
        $("#cboPlanta option:selected").text(),
        $("#cboOrigenMp option:selected").text(),
        $("#cboMateriaPrima option:selected").text(),
        formKilo.format($("#txtKilos").val()),
        formMon.format($("#txtCosto").val() / $("#txtKilos").val()),
        formMon.format($("#txtCosto").val()),
        formMon.format($("#txtCosto2").val()),
        formMon.format($("#txtValorLme").val()),
        formMon.format($("#txtValorLme2").val()),
        $("#txtObservaciones").val(),
        button
    ]

    if (param.FechaVigencia != "" && param.MateriaPrima != "-1" && param.Origen != "-1") {
        if (currentIndexRow != null) {
            table_materiaprima.row(currentIndexRow).data(rowData); // Actualizamos el valor en la grilla
        } else {
            table_materiaprima.row.add(rowData);
        }
        $.ajax({
            type: "POST",
            url: "FormMaestroCostoSimu.aspx/GuardarCostosMateriaPrima",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                toastr.success("Creado Correctamente Costo de MP", "Costos MP");
            },
            error: function () {
                toastr.error("Failed to Crear Costo de MP", "Costos MP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
        var modal = $("#ModControlInsertar");
        modal.modal('hide');
    } else {
        toastr.error("Los campos (Origen, Tipo de Materia Prima y Fecha) son obligatorios", "Información", {
            "timeOut": "0",
            "extendedTImeout": "0"
        });
    }

}

function btnCntrlInsertaMaf(){
    var param = {
        FechaVigencia: $("#txtFechaVigenciaMao").val(),
        PlantaId: $("#cboPlantaMao").val(),
        CostoMOD: $("#txtCostoMod").val(),
        CostoCIF: $("#txtCostoCif").val(),
        Chatarra: $("#txtPorcentajeChatarra").val(),
        Chatarra240: $("#txtPorcentajeChatarraPiezas").val(),
        TipoFormaleta: $("#cboFormaletaMao").val()
    };

    let rowData = [
        $("#txtFechaVigenciaMao").val().substring(0, 10),
        $("#cboPlantaMao option:selected").text(),
        $("#cboFormaletaMao option:selected").text(),
        formMon.format($("#txtCostoMod").val()),
        formMon.format($("#txtCostoCif").val()),
        ($("#txtPorcentajeChatarra").val()),
        ($("#txtPorcentajeChatarraPiezas").val()),
        "<button type=\"button\" class=\"fa fa-edit\" data-toggle=\"tooltip\" title=\"Editar\" onclick=\"EditarMo('" + $("#txtFechaVigenciaMao").val().substring(0, 10) + "','" + $("#cboPlantaMao").val() + "','" + $("#txtCostoMod").val() + "','" + $("#txtCostoCif").val() + "','" + ($("#txtPorcentajeChatarra").val() / 100) + "','" + ($("#txtPorcentajeChatarraPiezas").val() / 100) + "','" + $("#cboFormaletaMao").val() + "','" + currentIndexRow + "')\"> </button>"
    ];

    if (param.FechaVigencia != "" && param.TipoFormaleta != "-1") {
        if (currentIndexRow != null) {
            table_manoobracif.row(currentIndexRow).data(rowData); // Actualizamos el valor en la grilla
        } else {
            table_manoobracif.row.add(rowData);
        }
        $.ajax({
            type: "POST",
            url: "FormMaestroCostoSimu.aspx/GuardarCostosManoDeObra",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                toastr.success("Guardado Correctamente Costo de MP", "Costos MP");
            },
            error: function (msg) {
                toastr.error("Failed to Guardar Costo de MP", "Costos MP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
        var modal = $("#MaoControlInsertar");
        modal.modal('hide');
    } else {
        toastr.error("Los campos (Fecha de Vigencia y Tipo de Formaleta) son obligatorios", "Notificacion", {
            "timeOut": "0",
            "extendedTImeout": "0"
        });
    }
}

function Buscar(tabName) {
    switch (tabName) {
        case 'MateriaPrima': CargarDatosporFechaMateriaPrima(); $('#btnBuscarMp').val('1'); break;
        case 'ManoObra': CargarDatosporFechaManoObra(); $('#btnBuscarMo').val('1'); break;
    }
    
}

function CargarDatosporFechaMateriaPrima() {
    mostrarLoad();
    lstTurno = [];
    var param = {
        FechaIni: $("#txtFechaIniMp").val(),
        FechaFin: $("#txtFechaFinMp").val()
    };

    $.ajax({
        type: "POST",
        url: "FormMaestroCostoSimu.aspx/obtenerDatosporFechaMateriaPrima",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varTurnos') {
                    lstTurno = elem;
                    llenarCostomp(lstTurno);
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

function CargarDatosporFechaManoObra() {
    mostrarLoad();
    lstTurno = [];
    var param = {
        FechaIni: $("#txtFechaIniMo").val(),
        FechaFin: $("#txtFechaFinMo").val()
    };

    $.ajax({
        type: "POST",
        url: "FormMaestroCostoSimu.aspx/obtenerDatosporFechaManoObra",
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
            SelectTab(event, 'Tab2');
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
