var listaAltura = [];
var listaAlturaPro = [];
var listaEqu = [];
var listaTipoAnexo = [];
var listaTipoAnexoArmado = [];
var IdFUPGuardado = null;
var EstadoFUP = "";
var RequiereEnviar = 99;
var RequiereCT = -1;
var ExisteCT = -1;
var Autogestion = -1;
var CantGraba = 0;
var OrdParte = 0;
var cantidadMuro = 1;
var cantidadLosa = 1;
var cantidadOrdenReferencia = 1;
var cantidadOrdenCliente = 1;
var cantidadOrdenCuota = 1;
var cantidadLeasing = 1;
var cantidadEnrasePuerta = 1;
var cantidadEnraseVentana = 1;
var versionFupDefecto = 'A';
var idiomaSeleccionado = 'es';
var i = 1;
var ContEquipos = 1;
var ContAdicion = 1;
var RolUsuario = 0;
var CreaOF = 0;
var ContCom = 0;
var nomUser = "";
var ptoOrigen = -1;
var ptoDestino = -1;
var datFletes;
var IdClienteParametro = 0;
var IdPaisCliente = -1;
var FecFacturar = '19000101';
var cumplecondicionPago = false;
var cantidadComentario = 0;
var cantidadComentarioConsiderationObservation = 0;
var cantidadComentarioSegLogistico = 0;
var cantidadComentarioHallazgos = 0;
var listaProductos = [];
var listaPrecios = [];
var AlumCompleto = 0;
var EscaCompleto = 0;
var AcceCompleto = 0;
var datCierre = [];
var fijoCiere = '[{"nivel":1, "m2s1":0.0, "valors1":0.0, "valord1":0.0, "valorc1":0.0, "m2s2":0.0, "valors2":0.0, "valord2":0.0, "valorc2":0.0},{"nivel":2, "m2s1":0.0, "valors1":0.0, "valord1":0.0, "valorc1":0.0, "m2s2":0.0, "valors2":0.0, "valord2":0.0, "valorc2":0.0},{"nivel":3, "m2s1":0.0, "valors1":0.0, "valord1":0.0, "valorc1":0.0, "m2s2":0.0, "valors2":0.0, "valord2":0.0, "valorc2":0.0},{"nivel":4, "m2s1":0.0, "valors1":0.0, "valord1":0.0, "valorc1":0.0, "m2s2":0.0, "valors2":0.0, "valord2":0.0, "valorc2":0.0},{"nivel":5, "m2s1":0.0, "valors1":0.0, "valord1":0.0, "valorc1":0.0, "m2s2":0.0, "valors2":0.0, "valord2":0.0, "valorc2":0.0},{"nivel":6, "m2s1":0.0, "valors1":0.0, "valord1":0.0, "valorc1":0.0, "m2s2":0.0, "valors2":0.0, "valord2":0.0, "valorc2":0.0}]';
datCierre = JSON.parse(fijoCiere);

$(document).on('inserted.bs.tooltip', function (e) {
    var tooltip = $(e.target).data('bs.tooltip');
    $(tooltip.tip).addClass($(e.target).data('tooltip-custom-classes'));
});

$(document).ready(function () {
    $.i18n({
        locale: idiomaSeleccionado //'es'
    });

    // $.i18n().locale = 'en';

    cambiarIdioma();
    CargarDatosGenerales();
    CargarDatosGeneralesNegociacion();
    AjustarBotonCierre();
    seleccionarAltura();
    SumarTotalesSalidaCot();
    SumarTotalesSalidaMf();
    AgregarEspesorMuroLosa();
    AgregarOrdenEnrases();
    seleccionTipoProducto();
    seleccionALturaLibre();
    TipoNegocio();
    cargarArchivos();
    ocultarCards();
    ocultarLoad();
    CargarParteOrden();
    ObtenerRol();
    AgregarCliente();
    AgregarCuota();
    SumarValorCuotas();
    SumarValorLeasing();
    TotalesCondicionesPago();
    doOnLoad();

    $.i18n().load({
        en: './Scripts/languages/languages_en.json?v=202204',
        es: './Scripts/languages/languages_es.json?v=202204',
        br: './Scripts/languages/languages_br.json?v=202204'
    }).done(function () {
        cargarPaises();
        cargarPaisesClon();
        cargarListaAltura();
        cargarParamDatosGenerales();
        $(".langes").click();
    });


    $("#cboIdPais").change(function () {
        cargarCiudades($(this).val());
    });
    $("#cboIdPaisClon").change(function () {
        cargarCiudadesClon($(this).val());
    });

    $("#add_Equipo").click(function () {
        ContEquipos++;

        var cqs = $.i18n('Equipos');

        var cqs2 = $.i18n('pararealizarEquipos');

        var td = "<td id='consecutivo" + (ContEquipos) + "' class='text-center'><label style='margin-top: 8px' class='EqConsecutivo'>" + (ContEquipos) + "</label></td><td><input type='number' id='txtCant" + ContEquipos + "' placeholder='Cant' class='EqCant' style='margin-top:6px' /></td><td><label style='margin-top:8px' data-i18n='[html]Equipos' >" + cqs + "</label></td>" +
			"<td><select id='cmbTipoEquipo" + ContEquipos + "' placeholder='Tipo Equipo' class='form-control EqSelect'>" +
			llenarComboId(listaEqu) + "</select></td>" +
			'<td><label style="margin-top:8px">de </label></td><td><label class="lblTipoProductoEquipos" style="margin-top:8px"></label></td><td><label style="margin-top:8px" data-i18n="[html]pararealizarEquipos">' + cqs2 + '</label></td>' +
			"<td><input type='text' id='txtDescEquipo" + ContEquipos + "' placeholder='Descripcion' class='form-control EqDesc' /></td>" +
			'<td><button class="btnDelEquipo btn btn-sm " id="' + ContEquipos + '" ><i class="fa fa-minus-square" style="font-size:14px;color:red"></i></button></td>';

        $('#tbEquipos .bodyEquipos').append('<tr class="trAgregado" id="add_Equipo' + (ContEquipos) + '">' + td + '</tr>');

        $(".cmbAdaptacion option[id='" + ContEquipos + "']").remove();
        $(".cmbAdaptacion").append("<option class='" + ContEquipos + "' id='" + ContEquipos + "'>" + ContEquipos + "</option>")

        $(".lblTipoProductoEquipos").text($("#selectProducto option:selected").text());
    });

    $("#add_Adapta").click(function () {
        ContAdicion++;
        AgregarAdaptacion(-1);
        if (ContAdicion >= 2 && $("#selectProducto").val() == "17") {
            $(".forsapro2").hide();
        }
    });

    $('[data-toggle="tooltip"]').tooltip({
        animated: 'fade',
        placement: 'bottom',
        html: true
    });

    $("#tbEquipos").on("click", ".btnDelEquipo", function (event) {
        event.preventDefault();
        EliminarEquipo(this);
        event.stopPropagation();
    });

    $("#tbAdaptaciones").on("click", ".btnDelAdaptacion", function (event) {
        $(this).closest("tr").remove();
        ContAdicion--;
        if (ContAdicion < 2 && $("#selectProducto").val() == "17") {
            $(".forsapro2").show();
        }
    });

    $("#tbComentarios").on("click", ".btnDelComentario", function (event) {
        $(this).closest("tr").remove();
    });

    $("#cboIdCiudad").change(function () {
        cargarClientes($(this).val());
    });
    $("#cboIdCiudadClon").change(function () {
        cargarClientesClon($(this).val());
    });

    $("#cboIdEmpresa").change(function () {
        cargarContactos_Obras($(this).val());
        // Visible contrato MRV para cuando el Cliente es MRV
        if ($(this).val() == "2897") {
            $(".Solomrv").show();
        }
        else {
            $(".Solomrv").hide();
        }
    });
    $("#cboIdEmpresaClon").change(function () {
        cargarContactos_ObrasClon($(this).val());
    });

    $("#cboIdObra").change(function () {
        $("#cboIdEstrato").val(-1).change();
        $("#cboIdTipoVivienda").val(-1).change();
        cargarObraInformacion($(this).val());
    });

    $('.select-filter').select2();

    $(".sfmodal").select2({
        dropdownParent: $("#ClonarFupModal")
    });

    $("#btnDuplicar").click(function (event) {
        event.preventDefault();
        if ($.isNumeric($("#txtIdFUP").val())) {
            ClonarFupModal();
        }
    });

    $("#btnDuplicaFup").click(function (event) {
        obtenerDuplicadoFUP();
    });

    $("#btnNuevo").click(function (event) {
        event.preventDefault();
        $(location).attr('href', 'FormPrototypeReport.aspx')
        $(".SoloUpd").hide();
    });

    $("#btnFupBlanco").click(function () {
        window.open('Rapido/FUP_Impreso.pdf', '_blank');
    });
    $("#btnPTFListaCH").click(function (event) {
        event.preventDefault();
        window.open('Rapido/SCI-FR-20- LISTA CHEQUEO REQUISITOS MINIMOS PLANOS.xlsx', '_blank');
    });


    $("#btnBusFup").click(function (event) {
        event.preventDefault();
        if ($.isNumeric($("#txtIdFUP").val())) {
            obtenerVersionPorIdFup($("#txtIdFUP").val());
        }
    });

    $("#txtIdFUP").keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            if ($.isNumeric($("#txtIdFUP").val())) {
                obtenerVersionPorIdFup($("#txtIdFUP").val());
            }
        }
    });

    $("#cboEstadoPlanoTipoForsa").change(function () {
        var idTipovobo = Number($(this).val());
        $(".ActaSeguimientoDFT").hide();
        $(".ActaSeguimientoDFTPrograma").hide();
        $(".ActaSeguimientoDFTAval").hide();
        switch (idTipovobo) {
            case 1:     //Solicitud DFT
                $(".ActaSeguimientoDFT").show();
                break;
            case 2:     //Programación
                $(".ActaSeguimientoDFTPrograma").show();
                break;
            case 4:     //Aval Técnico
                $(".ActaSeguimientoDFTAval").show();
                break;
            default:
                $(".ActaSeguimientoDFT").hide();
                $(".ActaSeguimientoDFTPrograma").hide();
                $(".ActaSeguimientoDFTAval").hide();
                break;
        }
    });

    $("#btnBusOf").click(function (event) {
        event.preventDefault();
        if ($("#txtIdOrden").val().trim() != "") {
            obtenerVersionPorOF($("#txtIdOrden").val());
        }
    });

    $("#btnBusOrdenCliente").click(function (event) {
        event.preventDefault();
        if ($("#txtIdOrdenCliente").val().trim() != "") {
            obtenerFupPorIdOrdenCliente($("#txtIdOrdenCliente").val());
        }
    });

    $("#txtIdOrdenCliente").keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            if ($.isNumeric($("#txtIdOrdenCliente").val())) {
                obtenerFupPorIdOrdenCliente($("#txtIdOrdenCliente").val());
            }
        }


    });


    $("#txtIdOrden").keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            if ($("#txtIdOrden").val().trim() != "") {
                obtenerVersionPorOF($("#txtIdOrden").val());
            }
        }
    });

    $("#btnGuardarAprobacion").click(function () {
        guardarAprobacionFUP();
    });

    //$("#btnGuardarAlturaFormaleta").click(function () {
    //    guardarAlturaFormaleta();
    //});

    $("#cboVersion").change(function () {
        obtenerInformacionFUP($("#txtIdFUP").val(), $(this).val(), idiomaSeleccionado);
    });

    $("#cboEstadoSolRecotizacion").change(function () {
        if ($('#cboEstadoSolRecotizacion').val() == '1') {
            $('#txtObservacionRecotizacion').prop("disabled", false);
            $('#btnGuardarRecotizacion').prop("disabled", false);
            $('#cboTipoRecotizacionFup').prop("disabled", false);
        } else {
            $('#txtObservacionRecotizacion').prop("disabled", true);
            $('#btnGuardarRecotizacion').prop("disabled", true);
            $('#cboTipoRecotizacionFup').prop("disabled", true);
        }
    });

    $("#btnGuardarRecotizacion").click(function () {
        guardarRecotizacionFUP();
    });

    $("#btnGuardarPTF").click(function () {
        GuardarPTF();
    });

    $("#selectCopia").change(function () {
        if ($(this).val() == "1") {
            $(".vaCopia").show();
            $(".divvarof").show();
        }
        else {
            $(".divContentOrdenReferencia").html("");
            $("#txtFupCopia").val("");
            $(".vaCopia").hide();
            $(".divvarof").hide();
        }
    });


    ObtenerClienteParametro();

    ObtenerFupParametro();

});

function doOnLoad() { }

function ShowComboTranslation() {
    $("Selmullang").each(function (index) {
        fj = $(this).parent().attr("id");
        if ($(this).parent().attr("id") != "ContentPlaceHolder4_cboBusqClientes1") {
            $(this).attr('data-i18n', '[html]' + $(this).text().replace(/[^-A-Za-z0-9]+/g, '_').toLowerCase());
            console.log("'" + $(this).text().replace(/[^-A-Za-z0-9]+/g, '_').toLowerCase() + "':'" + $(this).text() + "'");
        }
    });

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

function AgregarAdaptacion(id) {
    var e = $('#tbAdaptaciones tbody tr').length + 1;

    var cqs = $.i18n('FormaletaAdicional');

    var cqs2 = $.i18n('ParaRealizarFormaleta');

    var td = "<td><select id='cmbEquipo" + e + "' placeholder='# Equipo' class='form-control cmbAdaptacion'></select></td>" +
		'<td><label style="margin-top:8px" data-i18n="[html]FormaletaAdicional">' + cqs + ' </label></td><td><label class="lblTipoProductoEquipos" style="margin-top:8px"></label></td><td><label style="margin-top:8px" data-i18n="[html]ParaRealizarFormaleta">' + cqs2 + ' </label></td>' +
		"<td><input type='text' id='txtDescAdapt0' placeholder='Descripcion' class='form-control DespAdaptacion' /></td>" +
		'<td><button class="btnDelAdaptacion btn btn-sm btn-link" id="' + e + '" ><i class="fa fa-minus-square" style="font-size:14px;color:red"></i></button></td>';

    $('#tbAdaptaciones').append('<tr class="trAgregado" id="add_Adapta' + (e) + '">' + td + '</tr>');

    $(".lblTipoProductoEquipos").text($("#selectProducto option:selected").text());

    var $options = $("#cmbEquipo1 > option").clone();

    $('#cmbEquipo' + e).append($options);

    //if(id!=-1)
}

function cargarArchivos() {
    $("#btnUploadFiles").click(function () {
        var TipoAnexo = 0;
        var ZonaAnexo = "";
        if ($("#TipoArchivoModal").val() < "1") {
            $("#TipoArchivoModal").css("border", "2px solid crimson");
            toastr.warning("Faltan Datos por ingresar");
        }
        else {
            mostrarLoad();
            var fileUpload = $("#rutaArchivo").get(0);
            var files = fileUpload.files;
            var test = new FormData();
            var evePTF = 0;

            for (var i = 0; i < files.length; i++) {
                test.append(files[i].name, files[i]);
            }
            evePTF = $('#EventoPTF').val();

            if (evePTF == null || evePTF == "") { evePTF = 0; }
            TipoAnexo = $("#TipoArchivoModal").val();
            ZonaAnexo = $('#zonaArchivo').val();
            test.append('idfup', IdFUPGuardado);
            test.append('version', $('#cboVersion').val());
            test.append('tipo', $("#TipoArchivoModal").val());
            test.append('zona', $('#zonaArchivo').val());
            test.append('EventoPTF', evePTF);


            $.ajax({
                url: "UploadHandler.ashx",
                type: "POST",
                contentType: false,
                processData: false,
                data: test,
                // dataType: "json",
                success: function (result) {
                    ocultarLoad();
                    var res = JSON.parse(result);
                    if (res.conf.id == 1) {
                        toastr.success(res.conf.descripcion);
                        //res.lista
                    }
                    else {
                        toastr.error(res.conf.descripcion);
                    }
                    obtenerParteAnexosFUP(IdFUPGuardado, $('#cboVersion').val());
                    if (TipoAnexo == 6) {
                        obtenerAnexosSalidaCotFUP(IdFUPGuardado, $('#cboVersion').val(), 6);
                    }
                    if (TipoAnexo == 10) {
                        obtenerAnexosSalidaCotFUP(IdFUPGuardado, $('#cboVersion').val(), 10);
                    }
                    if (TipoAnexo == 11) {
                        obtenerAnexosSalidaCotFUP(IdFUPGuardado, $('#cboVersion').val(), 11);
                    }
                    if (TipoAnexo == 25) {
                        obtenerAnexosSalidaCotFUP(IdFUPGuardado, $('#cboVersion').val(), 25);
                    }
                    if (TipoAnexo == 28 || TipoAnexo == 29) {
                        obtenerArmado(IdFUPGuardado, $('#cboVersion').val());
                    }
                    if (ZonaAnexo == "Planos TF") {
                        obtenerParteAnexosPTF(IdFUPGuardado, $('#cboVersion').val());
                    }
                    obtenerDetalleCondicionesPago(IdFUPGuardado, $('#cboVersion').val());
                    obtenerDetalleDocumentosCierre(IdFUPGuardado, $('#cboVersion').val());
                },
                error: function (err) {
                    ocultarLoad();
                    toastr.error(err.statusText, "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                }
            });
        }
    });

    $("#btnCntrlCmb").click(function () {
        var anx = 0;
        var cons = cantidadComentario + 1;
        var flag = 0;

        mostrarLoad();
        var fileUpload = $("#rutaArchivo2").get(0);
        var files = fileUpload.files;
        var fdata = new FormData();
        var subprocesoX = 0;
        var estadoX = 0;

        if ($("#EsDFT").val() == 1) {
            subprocesoX = parseInt($('#cmbSubProceso2').val());
            estadoX = parseInt($('#cmbEstadoDft').val());
        } else {
            subprocesoX = parseInt($('#cmbSubProceso').val());
            //estadoX = parseInt($('#cmbEstadoDft').val());
        }

        for (var i = 0; i < files.length; i++) {
            anx = 1;
            fdata.append(files[i].name, files[i]);
        }

        fdata.append('idfup', IdFUPGuardado);
        fdata.append('version', $('#cboVersion').val());
        fdata.append('tipo', 27);
        fdata.append('zona', "Control Cambio");
        fdata.append('EventoPTF', cons);

        var objg = {
            IdFUP: parseInt(IdFUPGuardado),
            Version: $.trim($('#cboVersion').val()),
            cons: parseInt(cons),
            padre: parseInt($('#padreCambio').val()),
            Comentario: $('#txtObsCntrCm').val(),
            Estado: EstadoFUP,
            Titulo: $('#txtTituloObs').val(),
            EsDFT: parseInt($('#EsDFT').val()),
            EstadoDFT: estadoX,
            SubprocesoDFT: subprocesoX
        };
        var param = {
            Item: objg,
            flag: flag
        };
        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/GuardarControlCambio",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                toastr.success("Correct Control Cambio", "FUP");
                cantidadComentario = cantidadComentario + 1;
                if (anx > 0) {
                    mostrarLoad();
                    $.ajax({
                        url: "UploadHandler.ashx",
                        type: "POST",
                        contentType: false,
                        processData: false,
                        data: fdata,
                        // dataType: "json",
                        success: function (result) {
                            ocultarLoad();
                            var res = JSON.parse(result);
                            if (res.conf.id == 1) {
                                toastr.success(res.conf.descripcion);
                                //res.lista
                            }
                            else {
                                toastr.error(res.conf.descripcion);
                            }
                            obtenerParteAnexosFUP(IdFUPGuardado, $('#cboVersion').val());
                            obtenerControlCambio(IdFUPGuardado, $('#cboVersion').val());
                        },
                        error: function (err) {
                            ocultarLoad();
                            toastr.error(err.statusText, "FUP", {
                                "timeOut": "0",
                                "extendedTImeout": "0"
                            });
                        }
                    });
                }
                else {
                    obtenerControlCambio(IdFUPGuardado, $('#cboVersion').val());
                }
            },
            error: function () {
                ocultarLoad();
                toastr.error("Failed to Control Cambio", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });

    }
	);

}

$(document).on("click", "#btnCntrlCmbC_O", function () {
    var anx = 0;
    var cons = 0;
    var tipoEntrada = $('#tipoEntrada').val();
    if (tipoEntrada == 1) {
        cons = cantidadComentarioConsiderationObservation + 1;
    } else if (tipoEntrada == 2) {
        cons = cantidadComentarioSegLogistico + 1;
    } else if (tipoEntrada == 3) {
        cons = cantidadComentarioHallazgos + 1;
    } else if (tipoEntrada == 4) {

    }
    
    var flag = 0;

    mostrarLoad();
    var fileUpload = $("#rutaArchivo2C_O").get(0);
    var files = fileUpload.files;
    var fdata = new FormData();
    var subprocesoX = parseInt($('#cmbSubProcesoC_O').val());
    var estadoX = 0;

    for (var i = 0; i < files.length; i++) {
        anx = 1;
        fdata.append(files[i].name, files[i]);
    }

    fdata.append('idfup', IdFUPGuardado);
    fdata.append('version', $('#cboVersion').val());
    fdata.append('tipo', 27);
    if(tipoEntrada == 1) {
        fdata.append('zona', "Consideracion u Observacion");
    } else if (tipoEntrada == 2) {
        fdata.append('zona', "Seguimiento Logistico");
    } else if (tipoEntrada == 3) {
        fdata.append('zona', "Hallazgo en Obra");
    } else if (tipoEntrada == 4) {
        fdata.append('zona', "Bitacora de Obra");
    }
    fdata.append('EventoPTF', cons);

    var objg = {
        IdFUP: parseInt(IdFUPGuardado),
        Version: $.trim($('#cboVersion').val()),
        cons: parseInt(cons),
        padre: parseInt($('#padreCambioC_O').val()),
        Comentario: $('#txtObsCntrCmC_O').val(),
        Estado: EstadoFUP,
        Titulo: $('#txtTituloObsC_O').val(),
        EsDFT: parseInt($('#EsDFTC_O').val()),
        EstadoDFT: estadoX,
        SubprocesoDFT: subprocesoX,
        FecDespacho: $('#txtFecDespachoSegLogistico').val(),
        FecEntrega: $('#txtFecEntregaSegLogistico').val(),
        TipoEntrada: tipoEntrada,
        TipoConsideracionObservacion: $('#cmbSubProcesoC_O').val(),
        AreaSolicitada: $('#cmbAreaSolicitada').val(),
        SolucionadoEnObra: $('#cmbSolucionadoEnObra').val(),
        GeneroCosto: $('#cmbGeneroCosto').val()
    };

    var param = {
        Item: objg,
        flag: flag
    };
    ajaxUrl = "FormPrototypeReport.aspx/GuardarConsiderationObservation";
    $.ajax({
        type: "POST",
        url: ajaxUrl,
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            toastr.success("Entrada guardada correctamente", "FUP");
            cantidadComentario = cantidadComentario + 1;
            if (anx > 0) {
                mostrarLoad();
                $.ajax({
                    url: "UploadHandler.ashx",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fdata,
                    // dataType: "json",
                    success: function (result) {
                        ocultarLoad();
                        var res = JSON.parse(result);
                        if (res.conf.id == 1) {
                            toastr.success(res.conf.descripcion);
                            //res.lista
                        }
                        else {
                            toastr.error(res.conf.descripcion);
                        }
                        // Espacio para recargar las entradas
                    },
                    error: function (err) {
                        ocultarLoad();
                        toastr.error(err.statusText, "FUP", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                    }
                });
            }
            else {
                obtenerControlCambio(IdFUPGuardado, $('#cboVersion').val());
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Hubo un error", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });

});


$("#btnAddObservationConsideration").click(function () {
    var anx = 0;
    var cons = cantidadComentarioConsiderationObservation + 1;
    var flag = 0;

    mostrarLoad();
    var fileUpload = $("#rutaArchivo2ConsiderationObservation").get(0);
    var files = fileUpload.files;
    var fdata = new FormData();
    var evidenceType = $('#cmbSubProceso2ConsiderationObservation').val();

    for (var i = 0; i < files.length; i++) {
        anx = 1;
        fdata.append(files[i].name, files[i]);
    }

    fdata.append('idfup', IdFUPGuardado);
    fdata.append('version', $('#cboVersion').val());

    var objg = {
        IdFUP: parseInt(IdFUPGuardado),
        Version: $.trim($('#cboVersion').val()),
        cons: parseInt(cons),
        padre: parseInt($('#padreCambio').val()),
        Comentario: $('#txtObsCntrCmConsiderationObservation').val(),
        Estado: EstadoFUP,
        Titulo: $('#txtTituloConsiderationObservation').val(),
        EsDFT: parseInt($('#EsDFT').val()),
        EstadoDFT: estadoX,
        SubprocesoDFT: subprocesoX
    };
    var param = {
        Item: objg,
        flag: flag
    };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardarControlCambio",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            toastr.success("Correct Control Cambio", "FUP");
            cantidadComentario = cantidadComentario + 1;
            if (anx > 0) {
                mostrarLoad();
                $.ajax({
                    url: "UploadHandler.ashx",
                    type: "POST",
                    contentType: false,
                    processData: false,
                    data: fdata,
                    // dataType: "json",
                    success: function (result) {
                        ocultarLoad();
                        var res = JSON.parse(result);
                        if (res.conf.id == 1) {
                            toastr.success(res.conf.descripcion);
                            //res.lista
                        }
                        else {
                            toastr.error(res.conf.descripcion);
                        }
                        obtenerParteAnexosFUP(IdFUPGuardado, $('#cboVersion').val());
                        obtenerControlCambio(IdFUPGuardado, $('#cboVersion').val());
                    },
                    error: function (err) {
                        ocultarLoad();
                        toastr.error(err.statusText, "FUP", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                    }
                });
            }
            else {
                obtenerControlCambio(IdFUPGuardado, $('#cboVersion').val());
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Control Cambio", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
});


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
        stringDatos = stringDatos + "<option value='" + Number(listaDatos[i].id) + "'>";

        switch (idiomaSeleccionado) {
            case "br":
                stringDatos = stringDatos + listaDatos[i].descripcionPO;
                break;
            case "en":
                stringDatos = stringDatos + listaDatos[i].descripcionEN;
                break;
            default:
                stringDatos = stringDatos + listaDatos[i].descripcion;
                break;
        }
        stringDatos = stringDatos + "</option>";
    }
    return stringDatos;
}

function llenarComboIdconDisable(listaDatos, condidisable) {
    var stringDatos = "<option value='-1'>" + $.i18n('select_opcion') + "</option>";
    for (i = 0; i < listaDatos.length; i++) {
        if (Number(listaDatos[i].id) == condidisable) {
            stringDatos = stringDatos + "<option disabled = 'disabled' value='" + Number(listaDatos[i].id) + "'>";
        }
        else {
            stringDatos = stringDatos + "<option value='" + Number(listaDatos[i].id) + "'>";
        }

        switch (idiomaSeleccionado) {
            case "br":
                stringDatos = stringDatos + listaDatos[i].descripcionPO;
                break;
            case "en":
                stringDatos = stringDatos + listaDatos[i].descripcionEN;
                break;
            default:
                stringDatos = stringDatos + listaDatos[i].descripcion;
                break;
        }
        stringDatos = stringDatos + "</option>";
    }
    return stringDatos;
}

function llenarComboDescripcion(listaDatos) {
    var stringDatos = "<option value='-1'>" + $.i18n('select_opcion') + "</option>";
    for (i = 0; i < listaDatos.length; i++) {
        stringDatos = stringDatos + "<option value='" + listaDatos[i].descripcion + "'>";
        switch (idiomaSeleccionado) {
            case "br":
                stringDatos = stringDatos + listaDatos[i].descripcionPO;
                break;
            case "en":
                stringDatos = stringDatos + listaDatos[i].descripcionEN;
                break;
            default:
                stringDatos = stringDatos + listaDatos[i].descripcion;
                break;
        }
    }
    return stringDatos;
}

function CargarDatosGenerales() {
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerInfoGeneral",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            $("#selectTipoNegociacion").html("");
            $("#selectTipoVaciado").html("");
            $("#selectSistemaSeguridad").html("");
            $("#selectAlineacionVertical").html("");
            $("#selectTipoFMFachada").html("");
            $("#selectTipoFMFachadaCliente").html("");
            $("#selectTipoUnionCliente").html("");
            $("#selectDetalleUnionCliente").html("");
            $("#selectDetalleUnion").html("");
            $("#selectFormaConstruccion").html("");
            $("#selectTerminoNegociacion").html("");
            $("#selectTerminoNegociacion2").html("");
            $("#selectTerminoNegociacion3").html("");
            $("#TipoArchivoModal").html("");
            $("#cboCondicionesPago").html("");

            $("#selectTipoNegociacion").html(llenarComboId(data.listaneg));
            $("#selectTipoVaciado").html(llenarComboId(data.listavac));
            $("#selectSistemaSeguridad").html(llenarComboId(data.listasg));
            $("#selectAlineacionVertical").html(llenarComboId(data.listaav));
            $("#selectTipoFMFachada").html(llenarComboId(data.listatf));
            $("#selectTipoFMFachadaCliente").html(llenarComboId(data.listatf));
            $("#selectTipoUnionCliente").html(llenarComboId(data.listatu));
            $("#selectDetalleUnionCliente").html(llenarComboId(data.listadu));
            $("#selectDetalleUnion").html(llenarComboId(data.listadu));
            $("#selectFormaConstruccion").html(llenarComboId(data.listafc));
            $("#selectTerminoNegociacion").html(llenarComboId(data.listatn));
            $("#selectTerminoNegociacion2").html(llenarComboId(data.listatn));
            $("#selectTerminoNegociacion3").html(llenarComboId(data.listatn));
            $("#TipoArchivoModal").html(llenarComboId(data.listatax));
            listaTipoAnexo = data.listatax;
            listaTipoAnexoArmado = data.listataxAr;

            $("#cboCondicionesPago").html(llenarComboId(data.listacondpago));

            // Nueva Opcion del sistema de segurida - Septiembre 2021
            $("#selectSistemaSeguridad option[value=2]").attr("disabled", "disabled");
            $("#selectSistemaSeguridad option[value=4]").attr("disabled", "disabled");

            CrearDetalleDocumentosCierre();

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

function CargarDatosGeneralesNegociacion() {
    mostrarLoad();
    $("#selectTipoNegociacion").change(function () {
        var idTipoNegociacion = Number($(this).val());
        if (idTipoNegociacion != -1) {
            $.ajax({
                type: "POST",
                url: "FormPrototypeReport.aspx/obtenerInfoGeneralPorNegociacion",
                data: JSON.stringify({
                    tipoNegociacion: idTipoNegociacion
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                // async: false,
                success: function (msg) {
                    ocultarLoad();
                    var data = JSON.parse(msg.d);
                    $("#cboTipoCotizacion").html("");
                    $("#selectProducto").html("");
                    listaProductos = data.listaprod;
                    $("#cboTipoCotizacion").html(llenarComboId(data.listatcot));
                    $("#selectProducto").html(llenarComboId(listaProductos));

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
    });
}

function CargarDatosGeneralesNegociacionLoad(fupConsultado) {
    mostrarLoad();

    var idTipoNegociacion = fupConsultado.TipoNegociacion;
    if (idTipoNegociacion != -1) {
        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/obtenerInfoGeneralPorNegociacion",
            data: JSON.stringify({
                tipoNegociacion: idTipoNegociacion
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                var data = JSON.parse(msg.d);

                $("#cboTipoCotizacion").html("");
                $("#selectProducto").html("");
                listaProductos = data.listaprod;
                $("#cboTipoCotizacion").html(llenarComboId(data.listatcot));
                $("#selectProducto").html(llenarComboId(data.listaprod));

                if (typeof fupConsultado != "undefined") {
                    $("#cboTipoCotizacion").val(fupConsultado.TipoCotizacion).change();
                    $("#selectProducto").val(fupConsultado.Producto).change();

                    if (fupConsultado.TipoCotizacion == "1") {
                        $("#selectProducto option[value=1]").attr("disabled", "disabled");
                    }

                    // FORSA PRO 
                    if ((fupConsultado.TipoCotizacion == "1") && ($("#selectProducto").val() == "17")) {
                        // Combo Alineacion Vertical
                        $("#selectAlineacionVertical option[value=2]").attr("disabled", "disabled");
                        $("#selectAlineacionVertical option[value=4]").attr("disabled", "disabled");
                        // Combo Tipo fachada
                        $("#selectTipoFMFachada option[value=2]").attr("disabled", "disabled");
                        // Combo Detalle de Union
                        $("#selectDetalleUnion option[value=2]").attr("disabled", "disabled");
                        $("#selectDetalleUnion option[value=3]").attr("disabled", "disabled");
                        // Ocultar Secciones NO Forsa PRO
                        $(".forsapro").hide();
                        ComboAlturaLibre(listaAlturaPro);
                        $('#selectDesnivel').val("3");
                    }

                    $("#selectAlturaLibre").val(fupConsultado.AlturaLibre).change();
                    cargarDatosDependeAlturaLibre(fupConsultado);
                    cargarDatosDependeTipoFachada(fupConsultado);

                }
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
}

function AjustarBotonCierre() {
    $("#cboVoBoFup").change(function () {
        var idTipovobo = Number($(this).val());
        if ((EstadoFUP == "Cierre Comercial")
			&& (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
            if (idTipovobo != 1) {
                $("#btnGuardarAprobacion").show();
            }
            else {
                $("#btnGuardarAprobacion").hide();
            };
        }
    });
}


function cambiarIdioma() {
    $(".langes").click(function () {
        idiomaSeleccionado = 'es';
        $.i18n({
            locale: 'es'
        });
        $.fn.i18n();
        if (IdFUPGuardado != null) {
            ObtenerLineasDinamicas();
        }

    });
    $(".langbr").click(function () {
        idiomaSeleccionado = 'br';
        $.i18n({
            locale: 'br'
        });
        $.fn.i18n();
        if (IdFUPGuardado != null) {
            ObtenerLineasDinamicas();
        }
    });
    $(".langen").click(function () {
        idiomaSeleccionado = 'en';
        $.i18n({
            locale: 'en'
        });
        $.fn.i18n();
        if (IdFUPGuardado != null) {
            ObtenerLineasDinamicas();
        }
    });
}

function cargarPaises(IdPais) {
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoPaises",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);

            llenarComboIdNombre("#cboIdPais", data);
            if (typeof IdPais != "undefined") {
                $("#cboIdPais").val(IdPais);
            }
            if (IdPaisCliente != -1) {
                $("#cboIdPais").val(IdPaisCliente);
            }


        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load countries", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}
function cargarPaisesClon() {
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoPaises",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            llenarComboIdNombre("#cboIdPaisClon", data);
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load countries", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ObtenerLineasDinamicas() {
    mostrarLoad();
    if (parseInt(IdFUPGuardado) > 0) {
        var param =
		{
		    pFupID: parseInt(IdFUPGuardado),
		    pVersion: $('#cboVersion').val(), //versionFupDefecto,
		    TipoCotizacion: 1,
		    Idioma: idiomaSeleccionado
		};

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/ObtenerLineasDinamicas",
            data: JSON.stringify({ item: param }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (result) {
                ocultarLoad();
                CrearControles(JSON.parse(result.d));
                $('[data-toggle="tooltip"]').tooltip({
                    animated: 'fade',
                    placement: 'bottom',
                    html: true
                });
            },
            error: function () {
                ocultarLoad();
                toastr.error("Failed to load dynamic controls", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    };

}

function PrepararReporteFUP() {
    if (parseInt(IdFUPGuardado) > 0) {
        window.open("ReporteFUP.aspx" + "?IdFUP=" + IdFUPGuardado + "&VerFUP=" + $('#cboVersion').val() + "");
    }
}


function LlenarVentaCierreCom(data) {
    if (data != undefined && data != null) {
        $("#VentaCierreObservacion").val(data.observacion);
        $("#VentaCierreObservacionM2").val(data.observacionVar_m2);
    }
}

function ObtenerVentaCierreComercial() {
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerVentaCierreComercial",
        data: JSON.stringify({
            pFupID: IdFUPGuardado,
            pVersion: $('#cboVersion').val()
        }),
        contentType: "application/json; charset=utf-8",
        // async: false,
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            if (result.d != null) {
                var data = JSON.parse(result.d);
                LlenarVentaCierreCom(data);
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to ObtenerVentaCierreComercial", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ObtenerEquiposyAdaptacion() {
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerEquipos",
        data: JSON.stringify({
            pFupID: IdFUPGuardado,
            pVersion: $('#cboVersion').val()
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (result) {
            ocultarLoad();
            LlenarEquipos(JSON.parse(result.d));
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load dynamic controls", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });

    mostrarLoad();
    var data;
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerAdaptacion",
        data: JSON.stringify({
            pFupID: IdFUPGuardado,
            pVersion: $('#cboVersion').val()//versionFupDefecto,
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (result) {
            ocultarLoad();
            data = JSON.parse(result.d);
            LlenarAdaptaciones(data);
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load dynamic controls", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });

    $(".lblTipoProductoEquipos").text($("#selectProducto option:selected").text());
}

function LlenarEquipos(data) {
    $("#tbEquipos .trAgregado").remove();

    ContEquipos = 1;

    $.each(data, function (e, r) {
        if (e == 0) {
            $("#txtCant1").val(r.Cantidad);
            $("#cmbTipoEquipo1").val(r.TipoEquipo);
            $("#consecutivo1 .EqConsecutivo").text(r.Consecutivo);
            $("#txtDescEquipo1").val(r.Descripcion);
        }
        else {
            ContEquipos = r.Consecutivo - 1;
            $("#add_Equipo").click();
            $("#txtCant" + (ContEquipos)).val(r.Cantidad);
            $("#cmbTipoEquipo" + (ContEquipos)).val(r.TipoEquipo);
            $("#consecutivo" + (ContEquipos) + " .EqConsecutivo").text(r.Consecutivo);
            $("#txtDescEquipo" + (ContEquipos)).val(r.Descripcion);
        }
    });
}

function LlenarAdaptaciones(data) {
    $("#tbAdaptaciones .trAgregado").remove();

    $.each(data, function (e, r) {

        if (e == 0) {
            var row = $("#add_Adapta1");

            $(row).find(".cmbAdaptacion option[id='" + r.Consecutivo + "']").prop("selected", true);
            $(row).find(".DespAdaptacion").val(r.Descripcion);
        }
        else {
            $("#add_Adapta").click();
            var row = $("#add_Adapta" + ($('#tbAdaptaciones tbody tr').length));

            $(row).find(".cmbAdaptacion option[id='" + r.Consecutivo + "']").prop("selected", true);
            $(row).find(".DespAdaptacion").val(r.Descripcion);;
        }
    });
}

function EliminarEquipo(object) {

    toastr.options.timeOut = "0";
    toastr.closeButton = true;

    toastr.warning("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
		'Si elimina el equipo de consecutivo ' + $(object).attr("id") + ', se eliminaran tambien sus adaptaciones. ¿desea continuar?',
		{
		    closeButton: false,
		    allowHtml: true,
		    onShown: function (toast) {
		        $("#ConfimarDelAdaptacion").click(function () {
		            $("#" + $(".cmbAdaptacion ." + object.id + ":selected").parent().parent().parent().attr("id")).remove()
		            $(".cmbAdaptacion option[id='" + object.id + "']").remove();
		            $(object).closest("tr").remove();
		            ContEquipos--;
		        });
		    }
		});

    toastr.options.timeOut = "5000";
    toastr.closeButton = false;

};

function CrearControles(data) {
    $("#DinamycSpace .ParteCabeceraDinamica").remove();

    var cardCabecera = '';
    var cardFoot = '';
    var cardBody = '';
    var idParteDinamica = '';
    var OrdenParte = 0;

    $.each(data, function (i, r) {

        if (r.TipoRegistro == "1") {

            if (cardBody != "") {
                if (((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre") && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) ||
					((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion") && (["54"].indexOf(RolUsuario) > -1)) ||
					((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion") && (["26"].indexOf(RolUsuario) > -1) && Autogestion == 1)
                    ) {
                    cardBody += '<div class="row justify-content-end"><button onclick="GuardadoDinamico(' + idParteDinamica + ')" class="btn btn-primary fupgen fupgenpt' + OrdenParte.toString().trim() + ' " type="button" value="Guardar ' + cardCabecera + '"><i class="fa fa-save"></i> <spam> ' + cardCabecera + '</spam></button></div></div></div></div>';
                }
                else {
                    cardBody += '</div></div></div>';
                }
            }
            cardCabecera = r.DescipcionItem;
            idParteDinamica = r.IdItemParte;
            OrdenParte = r.fcp_OrdenParte

            cardBody +=
				'<div class="col-md-12 ParteCabeceraDinamica" style="padding-top: 6x;" id="Parte' + r.IdItemParte + '"><div id="header' + r.IdItemParte + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
				'<table class="col-md-12 table-sm"><tr><td width="97%"><h5 class="box-title">' + r.DescipcionItem + '</h5></td>' +
				'<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.IdItemParte + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
				'<div id="body' + r.IdItemParte + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

        }
        else if (r.TipoRegistro == "2" || r.TipoRegistro == "3" || r.TipoRegistro == "31" || r.TipoRegistro == "32") {
            //            cardBody += '<div class="row item' + (r.ObsRequerida == true ? ' reqObservacion' : '') + (r.TipoRegistro == "2" && r.VaLista == true ? ' ValidaLista' : '') + '" id="row-' + r.IdItemParte + '">';
            cardBody += '<div class="row item' + (r.ObsRequerida == true ? ' reqObservacion' : '') + (r.VaLista == true ? ' ValidaLista' : '') + '" id="row-' + r.IdItemParte + '">';

            //            cardBody += '<div width="20.160" class="form-group">';
            cardBody += '<div class="form-group">';

            if (r.TipoAyuda == "1") {
                if (r.TipoRegistro == "31") {
                    cardBody += '<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" data-original-title="<img style=\'width:250%\' src= \'' + r.TextoAyuda + '\'></img>"><i class="fa fa-info-circle fa-lg"></i></button>';
                }
                else {
                    cardBody += '<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" data-original-title="<img style=\'width:120%\' src= \'' + r.TextoAyuda + '\'></img>"><i class="fa fa-info-circle fa-lg"></i></button>';
                }
            }
            else
                cardBody += '<button type="button" role="button" title="' + r.TextoAyuda + '" class="btn  btn-link divAyuda" data-placement="top"><i class="fa fa-info-circle fa-lg"></i></button>';

            if (r.TipoRegistro == "31") {
                cardBody += '</div>' + '<div class="form-group col-sm-3 border-bottom" style="margin-top: 3px;">';
                cardBody += '<select class="form-control ComboSeg Lista " ' + (r.Bloq_SINO_item == true ? 'disabled' : '') + ' id="ComboAcc' + i + '"  ><option value="-1">seleccione una opcion</option>';

                $.each(r.dominio, function (index, option) {
                    cardBody += "<option " + (r.TextoLista == option.fdom_CodDominio ? 'selected' : '') + " value = '" + option.fdom_CodDominio + "'>";
                    switch (idiomaSeleccionado) {
                        case "br":
                            cardBody += option.fdom_DescripcionPO;
                            break;
                        case "en":
                            cardBody += option.fdom_DescripcionEN;
                            break;
                        default:
                            cardBody += option.fdom_Descripcion;
                            break;
                    }

                    cardBody += "</option>";
                });
                cardBody += '</select>';
                cardBody += '</div>';
            }
            else {
                cardBody +=
				'</div>' +
				'<div class="form-group col-sm-3 border-bottom" style="margin-top: 3px;">' +
				'<label style="labDinamico">' + r.DescipcionItem + '</label>' +
				'</div>';
            }

            if (r.TipoRegistro != "32") {
                cardBody +=
                    '<div class="form-group onoffswitch">' +
                    '<input type="checkbox" onchange="sinoitemChange(this, \'row-' + r.IdItemParte + '\', ' + r.VaAdicional + ')"' + (r.SiNoItem == true ? 'checked' : '') + ' name="onoffswitch" ' + (r.Bloq_SINO_item == true ? 'disabled' : '') + '  class="onoffswitch-checkbox sinoItem" id="SiNo' + r.IdItemParte + '-' + i + '">' +
                    '<label class="onoffswitch-label" for="SiNo' + r.IdItemParte + '-' + i + '">' +
                    '<span class="onoffswitch-inner"></span>' +
                    '<span class="onoffswitch-switch"></span>' +
                    '</label>' +
                    '</div>';
            }

            if (r.TipoRegistro == "3")
                cardBody += '<div class="col-sm-2"><input readonly type="text" class="form-control form-lista-fup" data-toggle="tooltip" title="' + r.Defecto_ItemTextoLista + '" value="' + r.Defecto_ItemTextoLista + '"  /></div>';

            if (r.TipoRegistro == "31")
                cardBody += '<div class="col-sm-2"><input readonly type="text" class="form-control form-lista-fup" data-toggle="tooltip" title="' + r.Defecto_ItemTextoLista + '" value="' + r.Defecto_ItemTextoLista + '"  /></div>';

            if (r.VaAdicional) {
                cardBody += '<div class="form-group onoffswitch">' +
					'<input type="checkbox" onchange="sinoadicionalChange(this, \'row-' + r.IdItemParte + '\', ' + r.TipoRegistro + ')" ' + (r.SiNoAdicional == true ? 'checked' : '') + '  name="onoffswitch" ' + (r.Bloq_SINO_Add == true ? 'disabled' : '') + ' class="onoffswitch-checkbox SiNoAdicional" id="SiNoAdicional' + i + '">' +
					'<label class="onoffswitch-label" for="SiNoAdicional' + i + '">' +
					'<span class="onoffswitch-inner"></span>' +
					'<span class="onoffswitch-switch"></span>' +
					'</label></div>';
                if (r.TipoRegistro != "32") {
                    cardBody += '<div class="col-sm-1"><input type="text" ' + (r.SiNoAdicional == true ? '' : 'disabled') + ' class="form-control CantAdicional" placeholder="cant." value="' + r.CantAdicional + '"  /></div>' +
                                '<div class="col-sm-2" style="padding-left: 15px;">'
                }
                else {
                    cardBody += '<div class="col-sm-2" style="padding-left: 0px;">';
                }
                if (r.VaLista) {
                    cardBody += '<select class="form-control ComboAdi ' + (r.IdItemParte == 98 ? ' especial ' : '') + ' " ' + (r.SiNoAdicional == true ? '' : 'disabled') + ' id="ComboAcc' + i + '"  ><option value="-1">seleccione una opcion</option>';

                    $.each(r.dominio, function (index, option) {
                        cardBody += "<option " + (r.TextoLista == option.fdom_CodDominio ? 'selected' : '') + " value = '" + option.fdom_CodDominio + "'>";
                        switch (idiomaSeleccionado) {
                            case "br":
                                cardBody += option.fdom_DescripcionPO;
                                break;
                            case "en":
                                cardBody += option.fdom_DescripcionEN;
                                break;
                            default:
                                cardBody += option.fdom_Descripcion;
                                break;
                        }

                        cardBody += "</option>";
                    });
                    cardBody += '</select>';
                }
                else {
                    cardBody += '<label style="padding-top: 7px;">' + r.UnidadMedida + '</label>';
                }
                cardBody += '</div>';
            }


            if (r.TipoRegistro == "2")
                cardBody += '<div class="form-group col-sm-3">';

            if (r.TipoRegistro == "2" && r.VaLista) {
                cardBody += '<select class="form-control Lista" ' + (r.SiNoItem == true ? '' : 'disabled') + ' id="Combo' + i + '"  ><option value="-1">seleccione una opcion</option>';

                $.each(r.dominio, function (index, option) {
                    cardBody += "<option " + (r.TextoLista == option.fdom_CodDominio ? 'selected' : '') + " value = '" + option.fdom_CodDominio + "'>";
                    switch (idiomaSeleccionado) {
                        case "br":
                            cardBody += option.fdom_DescripcionPO;
                            break;
                        case "en":
                            cardBody += option.fdom_DescripcionEN;
                            break;
                        default:
                            cardBody += option.fdom_Descripcion;
                            break;
                    }

                    cardBody += "</option>";
                });

                cardBody += '</select>';
            }

            if (r.TipoRegistro == "2")
                cardBody += '</div>';

            var Tipo_Cotizacion = $("#cboTipoCotizacion").val();
            if (Tipo_Cotizacion == "3" || Tipo_Cotizacion == "7") {
                cardBody +=
					'<div class="form-group col-sm-2" >' +
					(r.VaObservacion ? '<input placeholder="Cantidad" type="text" ' + (r.SiNoItem == true || r.SiNoAdicional == true ? '' : 'disabled') + ' class="form-control Observacion" id="Observacion' + i + '" value="' + r.Observacion + '"  />' : '') +
					'</div>';
            }
            else {
                cardBody +=
					'<div class="form-group ' + ((r.VaAdicional) ? 'col-sm-2" style="padding-left: 0px;"' : 'col-sm-3"') + ' >' +
					(r.VaObservacion ? '<input placeholder="Descripcion" type="text" ' + (r.SiNoItem == true || r.SiNoAdicional == true ? '' : 'disabled') + ' class="form-control Observacion" id="Observacion' + i + '" value="' + r.Observacion + '"  />' : '') +
					'</div>';
            }

            cardBody += '</div>';

        }
        else if (r.TipoRegistro == "8") {
            cardBody += '<div class="row item" style="padding-bottom: 15px;" id="' + r.IdItemParte + '"><label style="font-size: 10px;">' + r.DescipcionItem + '</label><textarea class="form-control col-sm-12 Observacion" rows="5">' + r.Observacion + '</textarea></div>';
            //'</div></div></div>';
        }
        else if (r.TipoRegistro == "4") {
            cardBody += '<div class="row border-bottom"><h6 style="font-size: 10px;">' + r.DescipcionItem + '</h6></div>';
        }
        else if (r.TipoRegistro == "5") {
            vTitulos = r.DescipcionItem.split("|");
            cardBody += '<div class="row item" style="line-height: 8px; font-weight: bold;"><div width="30.160" class="form-group"></div><div class="col-sm-3"></div><div class="col-sm-3 text-center"><h7>' + vTitulos[0] + '</h7></div><div class="col-sm-3 text-center"><h7>' + vTitulos[1] + '</h7></div><div class="col-ms-3 text-center"><h7>' + vTitulos[2] + '</h7></div></div>';
        }
        else if (r.TipoRegistro == "6") {
            vTitulos = r.DescipcionItem.split("|");
            cardBody += '<div class="row item" style="line-height: 6px; font-weight: bold;"><div class="col-sm-3"></div><div class="col-sm-1 text-center" ><h8>' + vTitulos[0] + '</h8></div><div class="col-sm-2" class="text-center"><h8>' + vTitulos[1] + '</h8></div><h8>' + vTitulos[2] + '</h8><div class="col-sm-1"><h8>' + vTitulos[3] + '</h8></div><div class="col-sm-1"><h8>' + vTitulos[4] + '</h8></div><div class="col-sm-3 text-center"><h8>' + vTitulos[5] + '</h8></div></div>';
        }
        else if (r.TipoRegistro == "9") {
            cardBody += '<div class="row item" style="padding-bottom: 15px;" id="' + r.IdItemParte + '"><label style="font-size: 10px; font-weight:bold;">' + r.DescipcionItem + '</label><textarea class="form-control col-sm-12 Observacion" rows="2" disabled>' + r.Observacion + '</textarea></div>';
        }
    });

    if (((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre")
		&& (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) ||
		((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion") && (["54"].indexOf(RolUsuario) > -1)) ||
        ((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion") && (["26"].indexOf(RolUsuario) > -1) && Autogestion == 1)
        ) {
        cardFoot += '<div class="row justify-content-end"><button onclick="GuardadoDinamico(' + idParteDinamica + ')" class="btn btn-primary fupgen fupgenpt' + OrdenParte + '" type="button" value="Guardar ' + cardCabecera + '"><i class="fa fa-save"></i> <spam> ' + cardCabecera + '</spam></button></div>';
    }
    //$("#DinamycSpace").append(cardCabecera + cardBody + cardFoot);
    $("#LineasDimanicas").append(cardBody + cardFoot);
}

function sinoitemChange(object, id, VaAdicional) {
    if (VaAdicional) {
        if ($("#" + id).children().find(".SiNoAdicional").prop("checked") == false)
            $("#" + id).children().find(".Observacion").prop("disabled", !object.checked);
    }
    else {
        $("#" + id).children().find(".Observacion").prop("disabled", !object.checked);
    }

    $("#" + id).children().find(".Lista").prop("disabled", !object.checked);
    //$("#" + id).children().find(".SiNoAdicional").prop("disabled", !object.checked);
    //$("#" + id).children().find(".CantAdicional").prop("disabled", !object.checked);

    if (!object.checked) {
        if ($("#" + id).children().find(".SiNoAdicional").prop("checked") == false)
            $("#" + id).children().find(".Observacion").val("");
        $("#" + id).children().find(".Lista").val(-1)
        //$("#" + id).children().find(".SiNoAdicional").prop("checked", false);
        //$("#" + id).children().find(".CantAdicional").val("");
    }
}

function sinoadicionalChange(object, id, TipoRegistro) {
    if (TipoRegistro != 32) {
        if ($("#" + id).children().find(".sinoItem").prop("checked") == false)
            $("#" + id).children().find(".Observacion").prop("disabled", !object.checked);
    }
    else {
        $("#" + id).children().find(".Observacion").prop("disabled", !object.checked);
    }
    $("#" + id).children().find(".CantAdicional").prop("disabled", !object.checked);
    $("#" + id).children().find(".ComboAdi").prop("disabled", !object.checked);

    if (!object.checked) {
        if ($("#" + id).children().find(".SiNoAdicional").prop("checked") == false)
            $("#" + id).children().find(".Observacion").val("");

        $("#" + id).children().find(".CantAdicional").val("");
        $("#" + id).children().find(".ComboAdi").val("-1");
        $("#" + id).children().find(".Observacion").css("border", "");
    }
}

function ActualizarOrdenFab() {
    ObtenerOrdenFabricacion()
}

function GuardadoDinamico(id) {
    if (validarObservacionDinamica(id)) {
        mostrarLoad();
        var flag = 0;
        var pFupID = IdFUPGuardado;
        var pVersion = $('#cboVersion').val();
        var pItemparte_id = "";
        var pItemSiNo = "";
        var pItemTextoLista = "";
        var pAdicionalSiNo = "";
        var pAdicionalCantidad = "";
        var Descripcion = "";
        var param;
        var lista;
        var datos_tablas = [];

        $("#body" + id).find(".item").each(function (i, r) {
            pItemparte_id = "";
            pItemSiNo = "";
            pItemTextoLista = "";
            pAdicionalSiNo = "";
            pAdicionalCantidad = "";
            Descripcion = "";
            pItemSiNo = ($(r).children().find(".sinoItem").prop("checked"));
            pItemTextoLista = $(r).children().find(".ComboAdi").val();
            if (pItemTextoLista == null || pItemTextoLista == "")
                pItemTextoLista = $(r).children().find(".Lista").val();
            pAdicionalSiNo = $(r).children().find(".SiNoAdicional").prop("checked");
            pAdicionalCantidad = $(r).children().find(".CantAdicional").val();
            Descripcion = $(r).find(".Observacion").val();
            pItemparte_id = r.id.replace("row-", "");
            if (pItemparte_id != null && pItemparte_id != undefined) {
                var obj_tabla = {
                    pFupID: pFupID,
                    pVersion: pVersion,
                    pItemparte_id: pItemparte_id,
                    pItemSiNo: pItemSiNo,
                    pItemTextoLista: pItemTextoLista,
                    pAdicionalSiNo: pAdicionalSiNo,
                    pAdicionalCantidad: pAdicionalCantidad,
                    Descripcion: Descripcion
                };

                datos_tablas.push(obj_tabla);
            }
        });

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/GuardarLineasDinamicas",
            data: JSON.stringify({ listaItem: datos_tablas }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (result) {
                ocultarLoad();
                toastr.success('Guardado Correctamente.');
                ValidarEstado();
            },
            error: function () {
                toastr.warning('Guardado con Errores.');
            }
        });
    }
    else {
        toastr.warning("Por favor verifique los campos señalados en rojo ya que estos son obligatorios.");
    }
}

function validarObservacionCierre() {
    var flag = true;

    if ($('#VentaCierreObservacion').val() == "") {
        $('#VentaCierreObservacion').css("border", "2px solid crimson");
        flag = false;
    }
    else {
        $('#VentaCierreObservacion').css("border", "");
    }
    if ($('#VentaCierreObservacionM2').val() == "") {
        $('#VentaCierreObservacionM2').css("border", "2px solid crimson");
        flag = false;
    }
    else {
        $('#VentaCierreObservacionM2').css("border", "");
    }

    return flag;
}

function GuardarVentaCierreComercial() {
    if (validarObservacionCierre()) {
        mostrarLoad();
        var param = {
            idFup: IdFUPGuardado,
            idVersion: $('#cboVersion').val(),
            observacion: $('#VentaCierreObservacion').val(),
            observacionVar_m2: $('#VentaCierreObservacionM2').val()
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/guardarVentaCierreComercial",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                //var nuevaVersionFup = JSON.parse(msg.d);
                ocultarLoad();
                toastr.success("Guardado correctamente");
                ValidarEstado();
            },
            error: function (e) {
                ocultarLoad();
                toastr.error("Failed GuardarVentaCierreComercial", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
}

function validarObservacionDinamica(id) {
    var flag = true;

    $("#body" + id).find(".reqObservacion").each(function (i, r) {
        if (($(r).children().find(".sinoItem").prop("checked")) && $(r).children().find(".Observacion").val() == "") {
            $(r).children().find(".Observacion").css("border", "2px solid crimson");
            flag = false;
        }
        else {
            $(r).children().find(".Observacion").css("border", "");
        }
    });

    $("#body" + id).find(".ValidaLista").each(function (i, r) {
        if (($(r).children().find(".sinoItem").prop("checked")) && $(r).children().find(".Lista").val() <= 0) {
            $(r).children().find(".Lista").css("border", "2px solid crimson");
            flag = false;
        }
        else {
            $(r).children().find(".Lista").css("border", "");
        }
        if (($(r).children().find(".SiNoAdicional").prop("checked")) && $(r).children().find(".ComboAdi").val() <= 0) {
            if ($(r).children().find(".especial").val() <= 0) {
                $(r).children().find(".ComboAdi").css("border", "");
            }
            else {
                $(r).children().find(".ComboAdi").css("border", "2px solid crimson");

                flag = false;
            }
        }
        else {
            $(r).children().find(".ComboAdi").css("border", "");
        }
        if (($(r).children().find(".SiNoAdicional").prop("checked")) && $(r).children().find(".CantAdicional").val() <= 0) {
            if ($(r).children().find(".especial").val() <= 0) {
                $(r).children().find(".CantAdicional").css("border", "");
            }
            else {
                $(r).children().find(".CantAdicional").css("border", "2px solid crimson");
                flag = false;
            }
        }
        else {
            $(r).children().find(".CantAdicional").css("border", "");
        }

        if (($(r).children().find(".ComboSeg").val() <= 0) && (($(r).children().find(".sinoItem").prop("checked")))) {
            $(r).children().find(".ComboSeg").css("border", "2px solid crimson");
            flag = false;
        }
        else {
            $(r).children().find(".ComboSeg").css("border", "");
        }
    });

    return flag;
}

function validarCantidadEquipos() {
    var flag = true;
    var cant = "", desc = "";

    $("#tbEquipos tbody").find("tr").each(function (i, r) {
        cant = $(r).find(".EqCant").val();
        desc = $(r).find(".EqDesc").val();

        if (cant == "") {
            $(r).find(".EqCant").css("border", "2px solid crimson");
            flag = false;
        }

        if (desc == "") {
            $(r).find(".EqDesc").css("border", "2px solid crimson");
            flag = false;
        }
    });
    return flag;
}

function GuardadoEquiposyAdap() {
    var pFupID = IdFUPGuardado
    var pVersion = $('#cboVersion').val();
    var TipoEquipo = "";
    var Consecutivo = "";
    var Cantidad = "";
    var Descripcion = "";
    var param;
    var flag = 0;
    var EsValido = 1;
    var datos_tablas = [];

    if ($("#tbEquipos").attr("style").search("none") == -1) {
        if (validarCantidadEquipos()) { EsValido = 1; }
        else { EsValido = 0; }
    }
    //    if (validarDescAdicional()) {EsValido =1;}
    //        else{EsValido = 0;}

    if (EsValido == 1) {

        var countEquipos = $("#tbEquipos tbody").find("tr").length;
        var countAdapta = $("#tbAdaptaciones tbody").find("tr").length;
        var msg = "";

        if ($("#tbEquipos").attr("style").search("none") == -1) {
            $("#tbEquipos tbody").find("tr").each(function (i, r) {
                TipoEquipo = $(r).find(".EqSelect").val();
                Consecutivo = $(r).find(".EqConsecutivo").text();
                Cantidad = $(r).find(".EqCant").val();
                Descripcion = $(r).find(".EqDesc").val();

                var obj_tabla = {
                    pFupID: pFupID,
                    pVersion: pVersion,
                    TipoEquipo: TipoEquipo,
                    Consecutivo: Consecutivo,
                    Cantidad: Cantidad,
                    Descripcion: Descripcion
                };

                datos_tablas.push(obj_tabla);
            });
        }

        $("#tbAdaptaciones tbody").find("tr").each(function (e, r) {
            TipoEquipo = "0";
            Cantidad = "0";
            Consecutivo = $(r).find(".cmbAdaptacion").val();
            Descripcion = $(r).find(".DespAdaptacion").val();

            if (Descripcion != null && Descripcion != "") {
                var obj_tabla = {
                    pFupID: pFupID,
                    pVersion: pVersion,
                    TipoEquipo: TipoEquipo,
                    Consecutivo: Consecutivo,
                    Cantidad: Cantidad,
                    Descripcion: Descripcion
                };
                datos_tablas.push(obj_tabla);
            }
        });

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/GuardarEquiposAdaptacion",
            data: JSON.stringify({ listaItem: datos_tablas }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (result) {
                toastr.success("Guardado correcto Equipo y Adicionales.");
                ValidarEstado();
            },
            error: function () {
                toastr.error("Error al guardar Equipo y Adicionales ");
            }
        });

    }
    else {
        toastr.error("Faltan datos por Diligenciar");
    }
}

function validarDescAdicional() {
    var flag = true;
    var desc = "";

    $("#tbAdaptaciones tbody").find("tr").each(function (e, r) {
        desc = $(r).find(".DespAdaptacion").val();
        if (desc == "") {
            $(r).find(".DespAdaptacion").css("border", "2px solid crimson");
            flag = false;
        }
    });
    return flag;
}

function GuardaAdaptacion() {
    var pFupID = IdFUPGuardado
    var pVersion = $('#cboVersion').val();
    var Consecutivo = "";
    var TipoEquipo = "0";
    var Cantidad = "0";
    var Descripcion = "";
    var param;
    var flag;

    var countAdapta = $("#tbAdaptaciones tbody").find("tr").length;

    if (validarDescAdicional()) {

        $("#tbAdaptaciones tbody").find("tr").each(function (e, r) {
            Consecutivo = $(r).find(".cmbAdaptacion").val();
            Descripcion = $(r).find(".DespAdaptacion").val();

            param = {
                pFupID: pFupID,
                pVersion: pVersion,
                TipoEquipo: TipoEquipo,
                Consecutivo: Consecutivo,
                Cantidad: Cantidad,
                Descripcion: Descripcion
            }

            $.ajax({
                type: "POST",
                url: "FormPrototypeReport.aspx/GuardarAdaptacion",
                data: JSON.stringify({ item: param }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                // async: false,
                success: function (result) {
                    flag = 1;

                },
                error: function () {
                    flag = 0;

                    toastr.error("Error al guardar Adaptacion");
                }
            })
				.done(function () {
				    if (flag = 1) {
				        if (e == countAdapta - 1) {
				            ValidarEstado();
				            toastr.success("Guardado correcto de Adicionales");
				        }
				    }
				    else {
				        toastr.error("Error al guardar ");
				    }
				});
        });
    }
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

function GuardarOrdenCliente() {
    var pFupID = IdFUPGuardado
    var pVersion = $('#cboVersion').val();
    var Consecutivo = "";
    var param;
    var flag;
    var OrdenCliente = 1;
    var datos_tablas = [];

    $(".txtOrdenCliente").each(function (index) {
        var vcom = $(this).parent().parent().find('.ComentarioPedCli').val()
        var obj_tabla = { tipo_tabla: 8, consecutivo: index, valor: $(this).val(), Comentario: vcom };
        datos_tablas.push(obj_tabla);
    });

    param = {
        pFupId: pFupID,
        pidVersion: pVersion,
        ListaOrdenes: datos_tablas
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardaOrdenCliente",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (result) {
            flag = 1;

        },
        error: function () {
            flag = 0;

            toastr.error("Error al guardar Orden Cliente");
        }
    })
        .done(function () {
            if (flag = 1) {
                toastr.success("Guardado correcto de Orden Cliente");
            }
            else {
                toastr.error("Error al guardar ");
            }
        });


}

function cargarListaAltura() {
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerAlturaLibre",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);
            listaAltura = data;
            $("#selectAlturaLibre").get(0).options.length = 0;
            $("#selectAlturaLibre").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");

            $.each(data, function (index, item) {
                $("#selectAlturaLibre").get(0).options[$("#selectAlturaLibre").get(0).options.length] = new Option((String)(item.fall_AlturaLibre), item.fall_id);
                if (item.fall_AlturaLibre >= 240 && item.fall_AlturaLibre <= 280) {
                    listaAlturaPro.push(item);
                }
            });
        },
        error: function () {
            toastr.error("No se pudo cargar Altura Libre", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function seleccionarAltura() {
    $("#selectAlturaLibre").change(function () {
        var textoSeleccionadoAltura = $("#selectAlturaLibre option:selected").text();
        if (textoSeleccionadoAltura.trim().toLowerCase() == "variable") {
            $(".divAlturaLibreOtro").hide();
            $(".divAlturaLibreVariable").show();
            $("#txtAlturaInternaSugerida").val("DP Modulacion");
            $("#txtAlturaUnion").val("DP Modulacion");
            $("#txtTipoUnion").val("DP Modulacion");
            //$("#txtAlturaInternaSugeridaCliente").val("DP Modulacion");
            //$("#txtAlturaUnionCliente").val("DP Modulacion");
            //$("#txtTipoUnionCliente").val("DP Modulacion");
            $("#txtAlturaCap1").val("DP Modulacion");
            //$("#txtAlturaCap2").val("DP Modulacion");
            //$("#txtAlturaCapCliente1").val("DP Modulacion");
            //$("#txtAlturaCapCliente2").val("DP Modulacion");
            //$(".alturacap1").html("Depende de Modulacion");
            //$(".alturacap2").html("Depende de Modulacion");
        }
        else if (textoSeleccionadoAltura.trim().toLowerCase() == "otro") {
            $(".divAlturaLibreOtro").show();
            $(".divAlturaLibreVariable").hide();
            $("#txtAlturaInternaSugerida").val("DP Modulacion");
            $("#txtAlturaUnion").val("DP Modulacion");
            $("#txtTipoUnion").val("DP Modulacion");
            //$("#txtAlturaInternaSugeridaCliente").val("DP Modulacion");
            //$("#txtAlturaUnionCliente").val("DP Modulacion");
            //$("#txtTipoUnionCliente").val("DP Modulacion");
            $("#txtAlturaCap1").val("DP Modulacion");
            //$("#txtAlturaCap2").val("DP Modulacion");
            //$("#txtAlturaCapCliente1").val("DP Modulacion");
            //$("#txtAlturaCapCliente2").val("DP Modulacion");
            //$(".alturacap1").html("Depende de Modulacion");
            //$(".alturacap2").html("Depende de Modulacion");
        }
        else {
            $(".divAlturaLibreOtro").hide();
            $(".divAlturaLibreVariable").hide();
            $("#txtAlturaCap1").val("DP Modulacion");
            var valorsel = $(this).val();
            var alturaSel = null;
            for (i = 0; i < listaAltura.length; i++) {
                if (listaAltura[i].fall_id == Number(valorsel)) {
                    alturaSel = listaAltura[i];
                    break;
                }
            }

            if (alturaSel != null) {

                $("#txtAlturaInternaSugerida").val(alturaSel.fall_AlturaFM);
                $("#txtAlturaUnion").val(alturaSel.fall_AlturaUml);
                $("#txtTipoUnion").val(alturaSel.fall_TipoUml);
                //$("#txtAlturaInternaSugeridaCliente").val(alturaSel.fall_AlturaFM);
                //$("#txtAlturaUnionCliente").val(alturaSel.fall_AlturaUml);
                //$("#txtTipoUnionCliente").val(alturaSel.fall_TipoUml);

                //$("#txtAlturaCap1").val("");
                //$("#txtAlturaCap2").val("");
                //$("#txtAlturaCapCliente1").val("");
                //$("#txtAlturaCapCliente2").val("");

                $(".txtValorLosa").each(function (index) {
                    if (index == 0) {
                        var valor1losa = Number($(this).val().trim());
                        if (valor1losa > 0) {
                            var alturaUnion1 = Number(alturaSel.fall_AlturaUml);
                            var totalSumaLosa1 = String(valor1losa + alturaUnion1);
                            //$("#txtAlturaCap1").val(totalSumaLosa1);
                            //$("#txtAlturaCapCliente1").val(totalSumaLosa1);
                        }
                        else {
                            //$("#txtAlturaCap1").val("");
                            //$("#txtAlturaCapCliente1").val("");
                        }
                    }
                    else if (index == 1) {
                        var valor2losa = Number($(this).val().trim());
                        if (valor2losa > 0) {
                            var alturaUnion2 = Number(alturaSel.fall_AlturaUml);
                            var totalSumaLosa2 = String(valor2losa + alturaUnion2);
                            //$("#txtAlturaCap2").val(totalSumaLosa2);
                            //$("#txtAlturaCapCliente2").val(totalSumaLosa2);
                        }
                        else {
                            //$("#txtAlturaCap2").val("");
                            //$("#txtAlturaCapCliente2").val("");
                        }
                    }
                });
            }
        }

    });

}

function ComboAlturaLibre(data) {
    $("#selectAlturaLibre").get(0).options.length = 0;
    $("#selectAlturaLibre").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
    $.each(data, function (index, item) {
        $("#selectAlturaLibre").get(0).options[$("#selectAlturaLibre").get(0).options.length] = new Option((String)(item.fall_AlturaLibre), item.fall_id);
    });
}

function PruebaWebMethod() {
    $.ajax({
        type: 'POST',
        url: 'FormPrototypeReport.aspx/search',
        data: '{ }',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        // async: false,
        success: function (msg) {
            // alert(msg.d)
        }
    });
    return false;
}

function cargarCiudades(idPais, fupConsultado) {
    var param = { idPais: idPais };
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoCiudadesMoneda",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {

            //debugger;
            ocultarLoad();
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varCiudad') {
                    llenarComboIdNombre("#cboIdCiudad", elem);
                }
            });

            if (typeof fupConsultado != "undefined") {
                $("#cboIdCiudad").val(fupConsultado.ID_ciudad).select2().change(cargarClientes(fupConsultado.ID_ciudad, fupConsultado));
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load cities", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}
function cargarCiudadesClon(idPais, fupConsultado) {
    var param = { idPais: idPais };
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoCiudadesMoneda",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {

            //debugger;
            ocultarLoad();
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varCiudad') {
                    llenarComboIdNombre("#cboIdCiudadClon", elem);
                }
            });

            if (typeof fupConsultado != "undefined") {
                $("#cboIdCiudadClon").val(fupConsultado.ID_ciudad).select2().change(cargarClientes(fupConsultado.ID_ciudad, fupConsultado));
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load cities", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function cargarClientes(idCiudad, fupConsultado) {
    var param = { idCiudad: idCiudad };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoClientesCiudad",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varCliente') {
                    llenarComboIdNombre("#cboIdEmpresa", elem);
                }
            });

            if (typeof fupConsultado != "undefined") {
                $("#cboIdEmpresa").val(fupConsultado.ID_Cliente).change(cargarContactos_Obras(fupConsultado.ID_Cliente, fupConsultado));
            }
        },
        error: function () {
            toastr.error("Failed to load clients", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}
function cargarClientesClon(idCiudad, fupConsultado) {
    var param = { idCiudad: idCiudad };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoClientesCiudad",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varCliente') {
                    llenarComboIdNombre("#cboIdEmpresaClon", elem);
                }
            });

            if (typeof fupConsultado != "undefined") {
                $("#cboIdEmpresaClon").val(fupConsultado.ID_Cliente).change(cargarContactos_ObrasClon(fupConsultado.ID_Cliente, fupConsultado));
            }
        },
        error: function () {
            toastr.error("Failed to load clients", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function cargarContactos_Obras(idCliente, fupConsultado) {
    var param = { idCliente: idCliente };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoContactos_Obras_porCliente",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);

            $.each(data, function (index, elem) {
                if (index == 'varContacto') {
                    llenarComboIdNombre("#cboIdContacto", elem);
                };
                if (index == 'varObra') {
                    llenarComboIdNombre("#cboIdObra", elem);
                };
            });

            if (typeof fupConsultado != "undefined") {
                $("#cboIdContacto").val(fupConsultado.ID_Contacto).change();
                $("#cboIdObra").val(fupConsultado.ID_Obra).change(cargarObraInformacion(fupConsultado.ID_Obra, fupConsultado));
            }
        },
        error: function () {
            toastr.error("Failed to load contacts", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}
function cargarContactos_ObrasClon(idCliente, fupConsultado) {
    var param = { idCliente: idCliente };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerListadoContactos_Obras_porCliente",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);

            $.each(data, function (index, elem) {
                if (index == 'varContacto') {
                    llenarComboIdNombre("#cboIdContactoClon", elem);
                };
                if (index == 'varObra') {
                    llenarComboIdNombre("#cboIdObraClon", elem);
                };
            });

            if (typeof fupConsultado != "undefined") {
                $("#cboIdContactoClon").val(fupConsultado.ID_Contacto).change();
                $("#cboIdObraClon").val(fupConsultado.ID_Obra).change(cargarObraInformacion(fupConsultado.ID_Obra, fupConsultado));
            }
        },
        error: function () {
            toastr.error("Failed to load contacts", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function AgregarEspesorMuroLosa() {

    $("#btnAgregarEspesorMuro").click(function (event) {
        event.preventDefault();
        cantidadMuro += 1;
        var filaNuevaMuro = '<div class="row ">' +
			'<div class="col-3 text-center" ># ' + String(cantidadMuro) + '</div >' +
			' <div class="col-4">' +
			'<input type="number" required class="txtValorMuro" /> ' +
			'</div>' +
			' <div class="col-2">' +
			' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarMuro(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
			' </div>' +
			'</div >';

        $(".divContentoEspesorMuro").append(filaNuevaMuro);
    });

    $("#btnAgregarEspesorLosa").click(function (event) {
        event.preventDefault();
        cantidadLosa += 1;

        var filaNuevaLosa = '<div class="row ">' +
			'<div class="col-3 text-center" ># ' + String(cantidadLosa) + '</div >' +
			' <div class="col-4">' +
			'<input type="number" required class="txtValorLosa" />' +
			'</div>' +
			' <div class="col-2">' +
			' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarLosa(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
			' </div>' +
			'</div >';

        $(".divContentEspesorLosa").append(filaNuevaLosa);

    });
}

function AgregarOrdenEnrases() {
    $("#btnAgregarOrdenReferencia").click(function (event) {
        event.preventDefault();
        cantidadOrdenReferencia += 1;
        var filaNuevaReferencia = '<div class="row ">' +
			'<div class="col-2 text-center" >#' + String(cantidadOrdenReferencia) + '</div >' +
			' <div class="col-5">' +
				'<input type="text" class="txtOrdenReferencia fuparr fuplist " onblur="ValidarReferencia(this)" /> <span></span> ' +
			'</div>' +
			' <div class="col-1">' +
			' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarOrdenReferencia(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
			' </div>' +
			'</div >';

        $(".divContentOrdenReferencia").append(filaNuevaReferencia);
    });
    $("#btnAgregarEnrasePuertas").click(function (event) {
        event.preventDefault();
        cantidadEnrasePuerta += 1;
        var filaNuevaEnrasePuerta = '<div class="row ">' +
			'<div class="col-3 text-center" ># ' + String(cantidadEnrasePuerta) + '</div >' +
			' <div class="col-4">' +
			'<input type="number" required class="txtEnrasePuertas" /> ' +
			'</div>' +
			' <div class="col-2">' +
			' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarEnrasePuerta(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
			' </div>' +
			'</div >';

        $(".divContentEnrasePuertas").append(filaNuevaEnrasePuerta);
    });
    $("#btnAgregarEnraseVentanas").click(function (event) {
        event.preventDefault();
        cantidadEnraseVentana += 1;
        var filaNuevaEnraseVentana = '<div class="row ">' +
			'<div class="col-3 text-center" ># ' + String(cantidadEnraseVentana) + '</div >' +
			' <div class="col-4">' +
			'<input type="number" class="txtEnraseVentanas" /> ' +
			'</div>' +
			' <div class="col-3">' +
			' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarEnraseVentana(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
			' </div>' +
			'</div >';

        $(".divContentEnraseVentanas").append(filaNuevaEnraseVentana);
    });

    $("#btnAddComenta").click(function (event) {
        event.preventDefault();
        ContCom = ContCom + 1;
        var fecHoy = new Date().toISOString().substring(0, 10);
        //var td = "<td class='text-center'>" + $('#cboVersion').val() + "</td><td class='text-center'>" + fecHoy + "</td><td>" + nomUser + "</td><td><input type='text' id='txtComentario" + ContCom + "' placeholder='Observaciones' class='form-control' /></td>" +
        //    '<td><button class="btn btn-sm btn-link btnDelComentario " > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button></td>';
        var td = "<td class='text-center'>" + fecHoy + "</td>"
			+ "<td><input type='text' id='txtComentario" + ContCom + "' placeholder='Comentarios' class='form-control txtComent' /></td>" +
			'<td><button class="btn btn-sm btn-link btnDelComentario " > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button></td>';

        $('#tbodycomentarioSC').append('<tr id="comentario' + (ContCom) + '" >' + td + '</tr>');
    });
}

function AgregarCliente() {
    $("#btnAgregarCliente").click(function (event) {
        event.preventDefault();
        cantidadOrdenCliente += 1;
        td = '<th width ="25%">Orden Compra / Pedido # ' + String(cantidadOrdenCliente) + '</th> ' +
			 ' <th width ="25%"><input type="text" min="0" class="txtOrdenCliente" /></th> ' +
			 ' <td width ="40%"><input type="text" class="ComentarioPedCli" />  </td>' +
			 ' <th width ="10%"><button class="btn btn-sm btn-link " onclick="EliminarOrdenCliente(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button> <button  type="button" class="fa fa-upload" data-toggle="tooltip" title="Cargar" onclick="UploadFielModalShow(\'Agregar Documento\',3,\'Fechas - Solicitud Facturacion - Documentos Cliente\')"> </button> ' +
			 ' </tr>';
        $('#tbodyPedidoCliente').append('<tr id="PedidoCliente' + (cantidadOrdenCliente) + '" >' + td + '</tr>');
    });
}

function AgregarCuota() {
    $("#btnAgregarCuota").click(function (event) {
        event.preventDefault();
        cantidadOrdenCuota += 1;
        td = '<tr> ' +
			' <td > ' + String(cantidadOrdenCuota) + '</td> ' +
			' <td class="text-center" ><input class="dtFechaCuota" type="date" /></td> ' +
			' <td ><input type="number" min="0" class="txtValorCuota" onblur ="SumarValorCuotas()" /></td> ' +
			' <td class="text-center" ><input class="txLeasing" type="text" /></td> ' +
			' <td ><button class="btn btn-sm btn-link " onclick="EliminarOrdenCuota(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button></td> ' +
			' </tr> '

        $('#tbodyCondicionesCuotas').append(td);
        TotalesCondicionesPago();
    });

    $("#btnAgregarleasing").click(function (event) {
        event.preventDefault();
        cantidadLeasing += 1;
        td = '<tr> ' +
			' <td > ' + String(cantidadLeasing) + '</td> ' +
			' <td class="text-center" ><input class="dtFechaCuota" type="date" /></td> ' +
			' <td ><input type="number" min="0" class="txtValorLeasing" onblur ="SumarValorLeasing()" /></td> ' +
			' <td class="text-center" ><input class="txLeasing" type="text" /></td> ' +
			' <td ><button class="btn btn-sm btn-link " onclick="EliminarOrdenLeasing(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button></td> ' +
			' </tr> '

        $('#tbodyCondicionesLeasing').append(td);
        TotalesCondicionesPago();
    });
}


function EliminarMuro(control) {
    $(control).parent().parent().remove();
    cantidadMuro -= 1;
}

function EliminarLosa(control) {
    $(control).parent().parent().remove();
    cantidadLosa -= 1;
}

function EliminarOrdenReferencia(control) {
    $(control).parent().parent().remove();
    cantidadOrdenReferencia -= 1;
}

function EliminarOrdenCliente(control) {
    $(control).parent().parent().remove();
    cantidadOrdenCliente -= 1;
}

function EliminarOrdenCuota(control) {
    $(control).parent().parent().remove();
    cantidadOrdenCuota -= 1;
}
function EliminarOrdenLeasing(control) {
    $(control).parent().parent().remove();
    cantidadLeasing -= 1;
}


function EliminarEnrasePuerta(control) {
    $(control).parent().parent().remove();
    cantidadEnrasePuerta -= 1;
}

function EliminarEnraseVentana(control) {
    $(control).parent().parent().remove();
    cantidadEnraseVentana -= 1;
}

function ReplicarValoresdeCierreComercial() {
    //$("#VlrNiv1m2B").val($("#VlrNiv1m2").val())
    //$("#VlrNoDcto1CuentaB").val($("#VlrNoDcto1Cuenta").val())
    $("#VlrDcto1B").val($("#VlrDcto1").val())
    $("#VlrDcto1CuentaB").val($("#VlrDcto1Cuenta").val())

    //$("#VlrNoDcto2CuentaB").val($("#VlrNoDcto2Cuenta").val())
    $("#VlrDcto2B").val($("#VlrDcto2").val())
    $("#VlrDcto2CuentaB").val($("#VlrDcto2Cuenta").val())

    //$("#VlrNoDcto3CuentaB").val($("#VlrNoDcto3Cuenta").val())
    $("#VlrDcto3B").val($("#VlrDcto3").val())
    $("#VlrDcto3CuentaB").val($("#VlrDcto3Cuenta").val())

    //$("#VlrNoDcto4CuentaB").val($("#VlrNoDcto4Cuenta").val())
    $("#VlrDcto4B").val($("#VlrDcto4").val())
    $("#VlrDcto4CuentaB").val($("#VlrDcto4Cuenta").val())

    //$("#VlrNoDcto5CuentaB").val($("#VlrNoDcto5Cuenta").val())
    $("#VlrDcto5B").val($("#VlrDcto5").val())
    $("#VlrDcto5CuentaB").val($("#VlrDcto5Cuenta").val())

    evalDesctoenCargue(2);
}

function cargarParamDatosGenerales() {
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerParametrosDatosGenerales",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);
            $("#cboIdEstrato").get(0).options.length = 0;
            $("#cboIdEstrato").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
            $("#cboIdTipoVivienda").get(0).options.length = 0;
            $("#cboIdTipoVivienda").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
            $("#cboClaseCotizacion").get(0).options.length = 0;
            $("#cboClaseCotizacion").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
            $("#cboVoBoFup").get(0).options.length = 0;
            $("#cboVoBoFup").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
            $("#cboMotivoRechazoFup").get(0).options.length = 0;
            $("#cboMotivoRechazoFup").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
            $("#cboTipoRecotizacionFup").get(0).options.length = 0;
            $("#cboTipoRecotizacionFup").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
            $("#cboEstadoPlanoTipoForsa").get(0).options.length = 0;
            $("#cboEstadoPlanoTipoForsa").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");
            //$("#cboResponsablePlanoTipoForsa").get(0).options.length = 0;
            //$("#cboResponsablePlanoTipoForsa").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");

            $("#cboIdMoneda").get(0).options.length = 0;
            $("#cboIdMoneda").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");


            $.each(data, function (index, elem) {
                if (index == 'varEstrato') {
                    $.each(elem, function (i, item) {
                        $("#cboIdEstrato").get(0).options[$("#cboIdEstrato").get(0).options.length] = new Option(item.Descripcion, item.Id);
                    });
                };
                if (index == 'varTipoVivienda') {
                    $.each(elem, function (i, item) {
                        $("#cboIdTipoVivienda").get(0).options[$("#cboIdTipoVivienda").get(0).options.length] = new Option(item.fdom_Descripcion, item.fdom_CodDominio);
                    });
                };
                if (index == 'varClaseCotizacion') {
                    $.each(elem, function (i, item) {
                        $("#cboClaseCotizacion").get(0).options[$("#cboClaseCotizacion").get(0).options.length] = new Option(item.texto, item.clase_cot_id);
                    });
                };
                if (index == 'varVoBoFup') {
                    $.each(elem, function (i, item) {
                        $("#cboVoBoFup").get(0).options[$("#cboVoBoFup").get(0).options.length] = new Option(item.fdom_Descripcion, item.fdom_CodDominio);
                    });
                };
                if (index == 'varMotivoRechazoFUP') {
                    $.each(elem, function (i, item) {
                        $("#cboMotivoRechazoFup").get(0).options[$("#cboMotivoRechazoFup").get(0).options.length] = new Option(item.fdom_Descripcion, item.fdom_CodDominio);
                    });
                };
                if (index == 'varTipoRecotizacionFUP') {
                    $.each(elem, function (i, item) {
                        $("#cboTipoRecotizacionFup").get(0).options[$("#cboTipoRecotizacionFup").get(0).options.length] = new Option(item.fdom_Descripcion, item.fdom_CodDominio);
                    });
                };
                if (index == 'varEventoPFT') {
                    listaEveptf = data.varEventoPFT;
                    $("#cboEstadoPlanoTipoForsa").html("");
                    $("#cboEstadoPlanoTipoForsa").html(llenarComboId(listaEveptf));
                };
                if (index == 'varPlanPFT') {
                    listaPlanptf = data.varPlanPFT;
                };
                if (index == 'varEstadoDFT') {
                    listaEstadoDft = data.varEstadoDFT;
                    $("#cmbEstadoDft").html("");
                    $("#cmbEstadoDft").html(llenarComboId(listaEstadoDft));
                };
                if (index == 'varSubpDFT') {
                    listaSubpDft = data.varSubpDFT;
                    $("#cmbSubProceso2").html("");
                    $("#cmbSubProceso2").html(llenarComboId(listaSubpDft));
                    $("#cmbSubProceso2").val(2);
                    $("#cmbSubProceso2").prop("disabled", true);
                    $("#cmbSubProceso").html("");
                    $("#cmbSubProceso").html(llenarComboIdconDisable(listaSubpDft, 2));

                };

                if (index == 'varResponsablePTF') {
                    //$.each(elem, function (i, item) {
                    //	$("#cboResponsablePlanoTipoForsa").get(0).options[$("#cboResponsablePlanoTipoForsa").get(0).options.length] = new Option(item.fdom_Descripcion, item.fdom_CodDominio);
                    //});
                };
                if (index == 'varTipoEquipo') {
                    listaEqu = data.varTipoEquipo;
                    $("#cmbTipoEquipo1").html("");
                    $("#cmbTipoEquipo1").html(llenarComboId(listaEqu));
                };
                if (index == 'varMoneda') {
                    $.each(elem, function (i, item) {
                        $("#cboIdMoneda").get(0).options[$("#cboIdMoneda").get(0).options.length] = new Option(item.Descripcion, item.Id);
                    });
                };
                if (index == 'varDevComer') {
                    listaDevcom = data.varDevComer;
                    $("#cboMotivodev").html("");
                    $("#cboMotivodev").html(llenarComboId(listaDevcom));
                };

            });
        },
        error: function () {
            toastr.error("Failed to load datos cargarParamDatosGenerales", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function cargarObraInformacion(idObra, fupConsultado) {
    var param = { idObra: idObra };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerDatosObra",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'obraInfo') {
                    $("#txtIdMetrosCuadradosVivienda").val(elem.M2Vivienda);
                    $("#txtIdUnidadesConstruir").val(elem.TotalUnidades);
                    $("#cboIdEstrato").val(elem.IdEstratoSocioEconomico).change();
                    $("#cboIdTipoVivienda").val(elem.ObraTipoVivienda).change();
                    $("#txtIdUnidadesConstruirForsa").val(elem.TotalUnidadesForsa);
                    $("#IdPaisObraFlete").html(elem.IdPais);
                    $("#IdCiuObraFlete").html(elem.IdCiudad);
                    $("#LblCiuObraFlete").html(elem.NombreCiudad);
                    if (elem.IdPais == 8) {
                        $("#LVehic").html("VEHICULOS");
                        $("#LTipo1").html("Sencillo");
                        $("#LTipo3").html("Sencillo");
                        $("#LTipo2").html("Tractomula");
                        $("#LTipo4").html("Tractomula");
                        $("#pnlExportacion").hide = true;
                    }
                    else {
                        $("#LVehic").html("CONTENEDORES");
                        $("#LTipo1").html("Contenedor De 20");
                        $("#LTipo3").html("Contenedor De 20");
                        $("#LTipo2").html("Contenedor De 40");
                        $("#LTipo4").html("Contenedor De 40");
                        $("#pnlExportacion").hide = false;
                    }
                };
                if (index == 'ptoOrigen') {
                    listapto = data.ptoOrigen;
                    $("#selectPuertoCargue").html("");
                    $("#selectPuertoCargue").html(llenarComboId(listapto));
                    if (ptoOrigen != -1) { $("#selectPuertoCargue").val(ptoOrigen) }
                };
                if (index == 'ptoDestino') {
                    listapto = data.ptoDestino;
                    $("#selectPuertoDescargue").html("");
                    $("#selectPuertoDescargue").html(llenarComboId(listapto));
                    if (ptoDestino != -1) { $("#selectPuertoDescargue").val(ptoDestino) }
                };
            });
        },
        error: function () {
            toastr.error("Failed to load ObraInformacion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function guardarFUP() {
    var objg = {};
    var isNew = 1;
    //debugger;
    $("[data-modelo]").each(function (index) {
        var prop = $(this).attr("data-modelo");
        var thisval = $(this).val();
        objg[prop] = thisval;
    });

    objg["ReqCliente"] = objg["ReqCliente"] == "1" ? 'true' : 'false';
    objg["CapPernado"] = objg["CapPernado"] == "1" ? 'true' : 'false';

    if (objg["EspesorJunta"].trim().length == 0) {
        objg["EspesorJunta"] = 0;
    }

    if (objg["NumeroEquipos"].trim().length == 0) {
        objg["NumeroEquipos"] = 0;
    }

    //Obtenemos los datos de las tablas
    // Matrices de objetos
    var espesoMuro = 0;
    var espesoLosa = 0;
    var ordenRefer = 0;
    var datos_tablas = [];
    $(".txtValorMuro").each(function (index) {
        var obj_tabla = { tipo_tabla: 1, consecutivo: index, valor: $(this).val() };
        var valorMuro = $.trim($(this).val());
        if (valorMuro.length == 0) { } else { espesoMuro = 1 };
        datos_tablas.push(obj_tabla);
    });

    $(".txtValorLosa").each(function (index) {
        var obj_tabla = { tipo_tabla: 2, consecutivo: index, valor: $(this).val() };
        var valorLosa = $.trim($(this).val());
        if (valorLosa.length == 0) { } else { espesoLosa = 1 };
        datos_tablas.push(obj_tabla);
    });

    var invalido = true;
    $(".txtOrdenReferencia").each(function (index) {
        var valorOrdenReferencia = $.trim($(this).val());
        if (objg["TipoCotizacion"] == "2") {
            if ((valorOrdenReferencia.length == 0 || valorOrdenReferencia == "0")) {
                invalido = false;
            } else {
                ordenRefer = 1;
            }
        }

        var obj_tabla = { tipo_tabla: 3, consecutivo: index, valor: $(this).val() };
        datos_tablas.push(obj_tabla);
    });

    var txtOtro = $.trim(objg["otros"]);

    if ((invalido == false) && (txtOtro.length == 0)) {
        toastr.warning("Debe digitar valores para la orden de referencia o diligenciar el campo Otros ");
        return false;
    }
    invalido = false;
    var mensalida = "";
    if ($('#cboIdPais').val() == null || $('#cboIdPais').val() == 0 || $('#cboIdPais').val() == -1) {
        mensalida = "Pais";
        invalido = true;
    }
    if ($('#cboIdCiudad').val() == null || $('#cboIdCiudad').val() == 0 || $('#cboIdCiudad').val() == -1) {
        mensalida = mensalida + " Ciudad ";
        invalido = true;
    }
    if ($('#cboIdEmpresa').val() == null || $('#cboIdEmpresa').val() == 0 || $('#cboIdEmpresa').val() == -1) {
        mensalida = mensalida + " Empresa";
        invalido = true;
    }
    if ($('#cboIdContacto').val() == null || $('#cboIdContacto').val() == 0 || $('#cboIdContacto').val() == -1) {
        mensalida = mensalida + " Contacto";
        invalido = true;
    }
    if ($('#cboIdObra').val() == null || $('#cboIdObra').val() == 0 || $('#cboIdObra').val() == -1) {
        mensalida = mensalida + " Obra";
        invalido = true;
    }
    if ($('#txtIdUnidadesConstruir').val() == null || $('#txtIdUnidadesConstruir').val() < 1) {
        mensalida = mensalida + " Unidades para construir";
        invalido = true;
    }
    if ($('#txtIdUnidadesConstruirForsa').val() == null || $('#txtIdUnidadesConstruirForsa').val() < 1) {
        mensalida = mensalida + " Unidades para construir FORSA";
        invalido = true;
    }
    if ($('#cboIdMoneda').val() == null || $('#cboIdMoneda').val() == 0 || $('#cboIdMoneda').val() == -1) {
        mensalida = mensalida + " Moneda";
        invalido = true;
    }
    if ($('#cboClaseCotizacion').val() == null || $('#cboClaseCotizacion').val() == 0 || $('#cboClaseCotizacion').val() == -1) {
        mensalida = mensalida + " Clase de Cotización";
        invalido = true;
    }
    if ($('#selectTipoNegociacion').val() == null || $('#selectTipoNegociacion').val() == 0 || $('#selectTipoNegociacion').val() == -1) {
        mensalida = mensalida + " Tipo de Negociación";
        invalido = true;
    }
    if ($('#cboTipoCotizacion').val() == null || $('#cboTipoCotizacion').val() == 0 || $('#cboTipoCotizacion').val() == -1) {
        mensalida = mensalida + " Tipo de Cotización";
        invalido = true;
    }
    if ($('#selectProducto').val() == null || $('#selectProducto').val() == 0 || $('#selectProducto').val() == -1) {
        mensalida = mensalida + " Producto";
        invalido = true;
    }
    if ($('#cboIdEstrato').val() == null || $('#cboIdEstrato').val() == 0 || $('#cboIdEstrato').val() == -1) {
        mensalida = mensalida + " Estrato";
        invalido = true;
    }
    if ($('#cboIdTipoVivienda').val() == null || $('#cboIdTipoVivienda').val() == 0 || $('#cboIdTipoVivienda').val() == -1) {
        mensalida = mensalida + " Tipo de Vivienda";
        invalido = true;
    }
    if ($('#selectTipoNegociacion').val() == 1) {
        switch ($('#cboTipoCotizacion').val()) {
            case "1":  //Nuevo
                if ($('#selectTipoVaciado').val() == null || $('#selectTipoVaciado').val() == 0 || $('#selectTipoVaciado').val() == -1) {
                    mensalida = mensalida + " Vaciado";
                    invalido = true;
                }
                if ($('#txtCantidadPisos').val() == null || parseInt($('#txtCantidadPisos').val()) < 1) {
                    mensalida = mensalida + " Cantidad Máx. Pisos";
                    invalido = true;
                }
                if ($('#txtCantidadFundicionesPiso').val() == null || $('#txtCantidadFundicionesPiso').val() < 1) {
                    mensalida = mensalida + " Cant. Fundiciones / Piso";
                    invalido = true;
                }
                if ($('#selectSistemaSeguridad').val() == null || $('#selectSistemaSeguridad').val() == 0 || $('#selectSistemaSeguridad').val() == -1) {
                    mensalida = mensalida + " Sist. Seguridad";
                    invalido = true;
                }
                if ($('#selectAlineacionVertical').val() == null || $('#selectAlineacionVertical').val() == 0 || $('#selectAlineacionVertical').val() == -1) {
                    mensalida = mensalida + " Alineacion Vertical";
                    invalido = true;
                }
                if ($('#selectAlturaLibre').val() == null || $('#selectAlturaLibre').val() == 0 || $('#selectAlturaLibre').val() == -1) {
                    mensalida = mensalida + " Altura Libre";
                    invalido = true;
                }
                if ($('#selectTipoFMFachada').val() == null || $('#selectTipoFMFachada').val() == 0 || $('#selectTipoFMFachada').val() == -1) {
                    mensalida = mensalida + " Tipo de FM Fachada";
                    invalido = true;
                }
                if ($('#selectDetalleUnion').val() == null || $('#selectDetalleUnion').val() == -1) {
                    mensalida = mensalida + " Detalle Union";
                    invalido = true;
                }
                if ($('#selectFormaConstruccion').val() == null || $('#selectFormaConstruccion').val() == 0 || $('#selectFormaConstruccion').val() == -1) {
                    mensalida = mensalida + " Forma de Construccion";
                    invalido = true;
                }
                if ($('#selectDesnivel').val() == null || $('#selectDesnivel').val() == 0 || $('#selectDesnivel').val() == -1) {
                    mensalida = mensalida + " Desnivel";
                    invalido = true;
                }
                if ($('#selectJuntaDilatacion').val() == null || $('#selectJuntaDilatacion').val() == 0 || $('#selectJuntaDilatacion').val() == -1) {
                    mensalida = mensalida + " Junta de Dilatacion entre Muros";
                    invalido = true;
                }
                break;
            case "2": //Adaptaciones
                if ($('#selectTipoVaciado').val() == null || $('#selectTipoVaciado').val() == 0 || $('#selectTipoVaciado').val() == -1) {
                    mensalida = mensalida + " Vaciado";
                    invalido = true;
                }
                if (ordenRefer = 0) {
                    mensalida = mensalida + " Orden de Referencia";
                    invalido = true;
                }
                if ($('#selectAlineacionVertical').val() == null || $('#selectAlineacionVertical').val() == 0 || $('#selectAlineacionVertical').val() == -1) {
                    mensalida = mensalida + " Alineacion Vertical";
                    invalido = true;
                }
                if ($('#selectDetalleUnion').val() == null || $('#selectDetalleUnion').val() == -1) {
                    mensalida = mensalida + " Detalle Union";
                    invalido = true;
                }
                break;
        }
    }
    if ($('#selectCopia').val() == "1" && $('#txtFupCopia').val() == "") {
        mensalida = mensalida + " Fup de Copia ";
        invalido = true;
    }
    if (invalido == true) {
        toastr.warning("Falta información. " + mensalida);
        return true;
    } else {
        invalido = false;
    }

    $(".txtEnrasePuertas").each(function (index) {
        var obj_tabla = { tipo_tabla: 4, consecutivo: index, valor: $(this).val() };
        datos_tablas.push(obj_tabla);
    });

    $(".txtEnraseVentanas").each(function (index) {
        var obj_tabla = { tipo_tabla: 5, consecutivo: index, valor: $(this).val() };
        datos_tablas.push(obj_tabla);
    });

    objg["datos_tablas"] = datos_tablas;

    if (IdFUPGuardado == null) {
        objg["IdFUP"] = 0;
    } else {
        objg["IdFUP"] = IdFUPGuardado;
        isNew = 0;
    }
    var cbVersi = $('#cboVersion').val();

    if (cbVersi == null) {
        objg["Version"] = versionFupDefecto;
    } else {
        objg["Version"] = cbVersi;
    }

    var param = { fup: objg };
    var fj = (JSON.stringify(param));
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardarFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            ocultarLoad();
            var data = JSON.parse(msg.d);
            //IdFUPGuardado = data;
            if (data != null && data != '') {
                IdFUPGuardado = data.IdFUP;
                if (Number(IdFUPGuardado) > 0) {
                    toastr.success("Guardado correctamente");
                    $("#txtIdFUP").val(IdFUPGuardado);
                    if (isNew > 0) {
                        $('#cboVersion').get(0).options.length = 0;
                        $('#cboVersion').get(0).options[0] = new Option(versionFupDefecto, versionFupDefecto);
                    }
                    if ($("#txtFechaSolicitaCliente").text != 'dd-mm-yyyy') {
                        GrabaFechasCliente(1);
                    }
                    if (data.TipoCotizacion == 3) { getListaPrecios(); }
                    ObtenerLineasDinamicas();
                    EstadoFUP = data.EstadoProceso;
                    $("#divEstadoFup").html(data.EstadoProceso);
                    $("#txtEstadoCliente").val(data.EstadoCli);
                    $("#txtFechaCreacion").val(data.Fecha_crea);
                    $("#txtCreadoPor").val(data.UsuarioCrea);
                    $("#txtCotizadoPor").val(data.Cotizador);
                    MostrarCards();
                    var dataPais = $('#cboIdPais').select2('data')[0].text;
                    $(".divPaisSC").html(dataPais);
                    ValidarEstado();
                    $(".SoloUpd").show();
                }
                else {
                    toastr.warning("Error guardando FUP", "FUP");
                }
            }
            else {
                toastr.warning("Error guardando", "FUP");
            }
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed to save FUP", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function mostrarLoad() {
    sLoading();
}

function ocultarLoad() {
    hLoading();
}

function seleccionTipoProducto() {
    $("#selectProducto").change(function () {
        var optionsSG = "";
        // Combo Sistema de Seguridad
        if (($(this).val() != "2") && ($(this).val() != "20")) {
            $("#selectSistemaSeguridad option[value=5]").attr("disabled", "disabled");

            //$("#selectSistemaSeguridad option[value=2]").attr("disabled", "disabled");
            //// Cambio de forsa PRO $(this).val() == "17"
            //if ($(this).val() == "17") {
            //    $("#selectSistemaSeguridad option[value=4]").attr("disabled", "disabled");
            //}
            //else {
            //    $("#selectSistemaSeguridad option[value=4]").removeAttr('disabled');
            //}

        }
        else {
            $("#selectSistemaSeguridad option[value=5]").removeAttr("disabled");
            //$("#selectSistemaSeguridad option[value=2]").removeAttr('disabled');
            //$("#selectSistemaSeguridad option[value=4]").removeAttr('disabled');
        }

        // FORSA PRO 
        if ($("#cboTipoCotizacion").val() == "1") {
            if ($(this).val() == "17") {
                // Combo Alineacion Vertical
                $("#selectAlineacionVertical option[value=2]").attr("disabled", "disabled");
                $("#selectAlineacionVertical option[value=4]").attr("disabled", "disabled");
                // Combo Tipo fachada
                $("#selectTipoFMFachada option[value=2]").attr("disabled", "disabled");
                // Combo Detalle de Union
                $("#selectDetalleUnion option[value=2]").attr("disabled", "disabled");
                $("#selectDetalleUnion option[value=3]").attr("disabled", "disabled");
                // Ocultar Secciones NO Forsa PRO
                $(".forsapro").hide();
                ComboAlturaLibre(listaAlturaPro);
                $('#selectDesnivel').val("3");
            }
            else {
                // Combo Alineacion Vertical
                $("#selectAlineacionVertical option[value=2]").removeAttr('disabled');
                $("#selectAlineacionVertical option[value=4]").removeAttr('disabled');
                // Combo Tipo fachada
                $("#selectTipoFMFachada option[value=2]").removeAttr('disabled');
                // Combo Detalle de Union
                $("#selectDetalleUnion option[value=2]").removeAttr('disabled');
                $("#selectDetalleUnion option[value=3]").removeAttr('disabled');
                // Visible Secciones NO Forsa PRO
                $(".forsapro").show();
                ComboAlturaLibre(listaAltura);
                //                $('#selectDesnivel').val("-1");
            }
        }


    });
}

function seleccionALturaLibre() {
    $("#txtNroEquipos").val("0");
    $(".divAlturaLibreOtro").hide();
    $(".divAlturaLibreVariable").hide();
    $("#txtAlturaInternaSugerida").attr("disabled", "disabled");
    $("#txtAlturaUnion").attr("disabled", "disabled");
    $("#txtAlturaCap1").attr("disabled", "disabled");
    $("#txtTipoUnion").attr("disabled", "disabled");
    $("#txtNroEquipos").attr("disabled", "disabled");
    $("#selectJuntaDilatacion").val("2");
    $(".divEspesorJuntas").hide();
    $(".fupdis").attr("disabled", "disabled");
    $(".divReqCliente").hide();

    $("#selectReqCliente").change(function () {
        if ($(this).val() == "1") {
            $(".divReqCliente").show();
        }
        else {
            $(".divReqCliente").hide();
        }
    });

    $("#selectTipoFMFachada").change(function () {
        var valselFM = Number($(this).val());
        if (valselFM == 1) {
            $("#txtAlturaCap1").val("DP Modulacion");
            $("#CapPernadolbl").show();
            $("#selectCAPPernado").show();
        }
        else {
            $("#txtAlturaCap1").val("No Aplica");
            $("#CapPernadolbl").hide();
            $("#selectCAPPernado").hide();
        }
    });

    $("#selectTipoFMFachadaCliente").change(function () {
        var valselFM = Number($(this).val());
        if (valselFM == 1) {
            $("#AlturaCAP1Cliente").val("DP Modulacion");
        }
        else {
            $("#AlturaCAP1Cliente").val("No Aplica");
        }
    });

    $("#selectSistemaSeguridad").change(function () {
        var opcionalineacion = "";
        if ($(this).val() == "2") {
            $("#selectAlineacionVertical option[value=3]").attr("disabled", "disabled");
            $("#selectAlineacionVertical option[value=4]").attr("disabled", "disabled");
        }
        else {

            $("#selectAlineacionVertical option[value=3]").removeAttr('disabled');
            $("#selectAlineacionVertical option[value=4]").removeAttr('disabled');
        }
    });

    $("#selectJuntaDilatacion").change(function () {
        if ($(this).val() == "1") {
            $(".divEspesorJuntas").show();
        }
        else {
            $(".divEspesorJuntas").hide();
        }
    });

    $("#collapseTwo").collapse('hide')

    $("#selectReqCliente").val("2");
    $(".divReqCliente").hide();
}

function obtenerVersionPorIdFup(idFup) {
    mostrarLoad();
    var param = { idFup: idFup };
    var iteracion = 0;
    var idVersionReciente = '';
    LimpiarDescuentosCierre()
    $('#cboVersion').get(0).options.length = 0;

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerVersionPorIdFup",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            if (msg.d != null) {
                var data = JSON.parse(msg.d);
                $.each(data, function (index, item) {
                    if (iteracion == 0) {
                        $('#cboVersion').get(0).options.length = 0;
                        iteracion = 1;
                        idVersionReciente = item.eect_vercot_id;
                    }
                    $('#cboVersion').get(0).options[$('#cboVersion').get(0).options.length] = new Option(item.eect_vercot_id, item.eect_vercot_id);
                });

                if (idVersionReciente != '') {
                    obtenerInformacionFUP(idFup, idVersionReciente, idiomaSeleccionado);
                }
                else {
                    toastr.warning("El FUP no existe o no tiene Permisos para Consultarlo", "Busqueda FUP");
                }
                $('#txtVersionDFT').val($('#cboVersion').val());
            }
            else {
                toastr.warning("search FUP Not Exists", "FUP");
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to search FUP", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function obtenerFupPorIdOrdenCliente(idOrdenCliente) {
    mostrarLoad();
    var param = { idOrdenCliente: idOrdenCliente };
    var iteracion = 0;
    var idVersionReciente = '';

    $('#cboVersion').get(0).options.length = 0;
    $('#cboVersion').get(0).options[0] = new Option(versionFupDefecto, versionFupDefecto);

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerFUPporOrdenCliente",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            $.each(data, function (index, item) {
                if (iteracion == 0) {
                    $('#cboVersion').get(0).options.length = 0;
                    iteracion = 1;
                    idVersionReciente = item.eect_vercot_id;
                    $("#txtIdFUP").val(item.eect_fup_id);
                }
                $('#cboVersion').get(0).options[$('#cboVersion').get(0).options.length] = new Option(item.eect_vercot_id, item.eect_vercot_id);
            });

            if (idVersionReciente != '') {
                obtenerInformacionFUP($("#txtIdFUP").val(), idVersionReciente, idiomaSeleccionado);
            }

        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to search FUP", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function LlenarTablasFUP(elem) {
    var cuentat1 = 0;
    var cuentat2 = 0;
    var cuentat3 = 0;
    var cuentat4 = 0;
    var cuentat5 = 0;

    $(".divContentoEspesorMuro").html("");
    $(".divContentEspesorLosa").html("");
    $(".divContentOrdenReferencia").html("");
    $(".divContentEnrasePuertas").html("");
    $(".divContentEnraseVentanas").html("");


    var stringHTMLt1 = ' <div class="row ">' +
        '<div class="col-3 text-center" ># 1</div>' +
        '<div class="col-4">' +
        '<input type="number" required class="txtValorMuro" />' +
        '</div>' +
        '<div class="col-3">' +
        '</div>' +
        '</div >';

    var stringHTMLt2 = ' <div class="row">' +
        '<div class="col-3 text-center" ># 1</div>' +
        '<div class="col-4">' +
        '<input type="number" required class="txtValorLosa" />' +
        '</div>' +
        '<div class="col-3">' +
        '</div>' +
        '</div >';

    var stringHTMLt3 = ' <div class="row ">' +
        '<div class="col-2 text-center" >#1</div>' +
        '<div class="col-5">' +
        ' <input type="text" class="txtOrdenReferencia fuparr fuplist" onblur="ValidarReferencia(this)" /> <span></span>' +
        ' </div >';

    var stringHTMLt4 = ' <div class="row ">' +
        '<div class="col-3 text-center" ># 1</div>' +
        '<div class="col-4">' +
        '<input type="number" required class="txtEnrasePuertas" />' +
        ' </div>' +
        '</div >';

    var stringHTMLt5 = ' <div class="row ">' +
        '<div class="col-3 text-center" ># 1</div>' +
        '<div class="col-4">' +
        '<input type="number" required class="txtEnraseVentanas" />' +
        '</div>' +
        '</div >';

    $(".divContentoEspesorMuro").html(stringHTMLt1);
    $(".divContentEspesorLosa").html(stringHTMLt2);
    $(".divContentOrdenReferencia").html(stringHTMLt3);
    $(".divContentEnrasePuertas").html(stringHTMLt4);
    $(".divContentEnraseVentanas").html(stringHTMLt5);

    $.each(elem, function (i, item_tabla) {
        if (item_tabla.tipo_tabla == 1) {
            if (cuentat1 == 0) {
                $(".txtValorMuro").val(item_tabla.valor);
                cantidadMuro = 0;
            }
            else {
                var filaNuevaMuro = '<div class="row ">' +
                    '<div class="col-3 text-center" ># ' + String(cuentat1 + 1) + '</div >' +
                    ' <div class="col-4">' +
                    '<input type="number" required class="txtValorMuro" value="' + item_tabla.valor + '" /> ' +
                    '</div>' +
                    ' <div class="col-2">' +
                    ' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarMuro(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
                    ' </div>' +
                    '</div >';

                $(".divContentoEspesorMuro").append(filaNuevaMuro);
            }

            cuentat1 += 1;
            cantidadMuro += 1;
        }
        if (item_tabla.tipo_tabla == 2) {
            if (cuentat2 == 0) {
                $(".txtValorLosa").val(item_tabla.valor);
            }
            else {
                var filaNuevaLosa = '<div class="row ">' +
                    '<div class="col-3 text-center" ># ' + String(cuentat2 + 1) + '</div >' +
                    ' <div class="col-4">' +
                    '<input type="number" required class="txtValorLosa" value="' + item_tabla.valor + '" />' +
                    '</div>' +
                    ' <div class="col-2">' +
                    ' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarLosa(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
                    ' </div>' +
                    '</div >'

                $(".divContentEspesorLosa").append(filaNuevaLosa);
            }
            cuentat2 += 1;
            cantidadLosa += 1;
        }
        if (item_tabla.tipo_tabla == 3) {
            if (cuentat3 == 0) {
                $(".txtOrdenReferencia").val(item_tabla.valor);
                cantidadOrdenReferencia = 0;
            }
            else {

                var filaNuevaReferencia = '<div class="row ">' +
                    '<div class="col-2 text-center" >#' + String(cuentat3 + 1) + '</div >' +
                    ' <div class="col-5">' +
                    '<input type="text" class="txtOrdenReferencia " onblur="ValidarReferencia(this)" value="' + item_tabla.valor + '" /> <span></span>' +
                    '</div>' +
                    ' <div class="col-1">' +
                    ' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0" onclick="EliminarOrdenReferencia(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
                    ' </div>' +
                    '</div >';

                $(".divContentOrdenReferencia").append(filaNuevaReferencia);
            }
            cuentat3 += 1;
            cantidadOrdenReferencia += 1;
        }
        if (item_tabla.tipo_tabla == 4) {
            if (cuentat4 == 0) {
                $(".txtEnrasePuertas").val(item_tabla.valor);
                cantidadEnrasePuerta = 0;
            }
            else {

                var filaNuevaEnrasePuerta = '<div class="row ">' +
                    '<div class="col-3 text-center" ># ' + String(cuentat4 + 1) + '</div >' +
                    ' <div class="col-4">' +
                    '<input type="number" required class="txtEnrasePuertas" value="' + item_tabla.valor + '" /> ' +
                    '</div>' +
                    ' <div class="col-3">' +
                    ' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarEnrasePuerta(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
                    ' </div>' +
                    '</div >';

                $(".divContentEnrasePuertas").append(filaNuevaEnrasePuerta);
            }
            cuentat4 += 1;
            cantidadEnrasePuerta += 1;
        }
        if (item_tabla.tipo_tabla == 5) {
            if (cuentat5 == 0) {
                $(".txtEnraseVentanas").val(item_tabla.valor);
                cantidadEnraseVentana = 0;
            }
            else {
                var filaNuevaEnraseVentana = '<div class="row ">' +
                    '<div class="col-3 text-center" ># ' + String(cuentat5 + 1) + '</div >' +
                    ' <div class="col-4">' +
                    '<input type="number" class="txtEnraseVentanas" value="' + item_tabla.valor + '" /> ' +
                    '</div>' +
                    ' <div class="col-3">' +
                    ' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0 " onclick="EliminarEnraseVentana(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
                    ' </div>' +
                    '</div >';

                $(".divContentEnraseVentanas").append(filaNuevaEnraseVentana);
            }
            cuentat5 += 1;
            cantidadEnraseVentana += 1;
        }
    });
}

function LlenarOrdenCliente(elem) {
    cantidadOrdenCliente = 0;

    $("#tbodyPedidoCliente").html("");

    var td1 = "";

    $.each(elem, function (i, elem) {
        cantidadOrdenCliente = cantidadOrdenCliente + 1;

        td = '<td width ="25%"> Orden Compra / Pedido Cliente # ' + String(cantidadOrdenCliente) + '</td> ' +
             ' <td width ="25%"><input type="text" min="0" class="txtOrdenCliente" value = "' + elem.valor + '"> </input></td> ' +
             ' <td width ="40%"><input type="text" class="ComentarioPedCli" value = "' + elem.Comentario + '"/> </td>' +
             ' <td width ="10%"><button class="btn btn-sm btn-link " onclick="EliminarOrdenCliente(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i>' +
                      '</button><button  type="button" class="fa fa-upload" data-toggle="tooltip" title="Cargar" onclick="UploadFielModalShow(\'Agregar Documento\',3,\'Fechas - Solicitud Facturacion - Documentos Cliente\')"> </button> ' +
                      "<button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                      "<button type=\"button\" class=\"fa fa-trash fupborrar \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button>"
        ' </td>';
        $('#tbodyPedidoCliente').append('<tr id="PedidoCliente' + (cantidadOrdenCliente) + '" >' + td + '</tr>');

    });

}

function TipoNegocio() {
    $("#selectTipoNegociacion").change(function () {
        var tipo_negociacion = $(this).val();
        //Equipo Nuevo
        //Se habilita todo de nuevo
        if (tipo_negociacion == "1" || tipo_negociacion == "2") {
            //$(".fuparr").removeAttr("disabled");
            $(".divarrlist").show();
            $(".fuparr").not('select, button').val("");
        }
            //Si es arrendatario se pone invisible el div divarrlist
        else //if (tipo_negociacion == "3" || tipo_negociacion == "4" || tipo_negociacion == "5") 
        {
            $(".fuparr").not('select, button').val("");
            $(".divarrlist").hide();
            $(".divvarof").hide();
        }
        /*        else {
                    //$(".fuparr").removeAttr("disabled");
                    $(".divarrlist").show();
                    $(".fuparr").not('select, button').val("");
                }*/
    });

    $("#cboTipoCotizacion").change(function () {
        var tipo_cotizacion = $(this).val();
        $("#selectProducto").html("");
        $("#selectProducto").html(llenarComboId(listaProductos));

        $(".fupadap").removeAttr("disabled");
        $(".fuplist").removeAttr("disabled");
        $("#txtNroEquipos").attr("disabled", "disabled");
        $(".fupgenlist").hide();
        //Orden de Referencia Solo para Adaptacion y Listado
        if (tipo_cotizacion != "2" && tipo_cotizacion != "3") {
            if (tipo_cotizacion == "1") {
                $(".SeCopia").show();
            }
            else {
                $(".SeCopia").hide();

            }
            if (tipo_cotizacion == "1" && $("#selectCopia").val() == 1) {
                $(".divvarof").show();
                $(".vaCopia").show();
            }
            else {
                $(".divvarof").hide();
                $(".vaCopia").hide();
            }
        }
        else {
            $(".divvarof").show();
            $(".SeCopia").hide();
        }


        // forsa Pro 
        if (tipo_cotizacion != "1") {
            $("#selectProducto option[value=17]").attr("disabled", "disabled");
            $("#selectProducto option[value=1]").removeAttr("disabled");
        }
        else {
            $("#selectProducto option[value=1]").attr("disabled", "disabled");
            $("#selectProducto option[value=17]").removeAttr("disabled");
        }
        //adaptacion
        if (tipo_cotizacion == "2") {
            $("#txtNroEquipos").removeAttr("disabled");

            //$(".fuplist").removeAttr("disabled");
            $(".divarrlist").show();
            $(".fuplist").not("select, button").val("");

            $('.fupadap').not("select, button").val("");
            $(".fuplist").not('select, button').val("");
            $(".fuparr").not('select, button').val("");
            //            $(".fupadap").attr("disabled", "disabled").off('click');

            $("#titleEquipos").text("Adaptaciones")
            $("#tbEquipos").attr("style", "display: none")

            $(".fuplist").removeAttr("disabled");
            $(".fuplist").find('input[type="text"]').val("");
        }
            //listado
        else if (tipo_cotizacion == "3" || tipo_cotizacion >= "7") {
            $(".fuplist").not('select, button').val("0");
            $("#titleEquipos").text("Equipos y Adicionales")
            $("#tbEquipos").attr("style", "display: normal")

            $(".fuplist").removeAttr("disabled");
            $(".fupadap").removeAttr("disabled");
            $(".fupadap").find('input[type="text"]').val("0");
            $('.fupadap').not("select, button").val("0");
            $(".fuparr").not('select, button').val("0");
            $(".fuparr").find('input[type="select"]').val("-1");
            $(".divarrlist").hide();
            $(".fupgenlist").show();
        }
        else {
            var tipo_negociacion = $("#selectTipoNegociacion").val();
            if (tipo_negociacion < "3") {

                $(".fupadap").removeAttr("disabled");
                $(".fupadap").not('select, button').val("");
                $(".fuparr").not('select, button').val("");

                //$(".fuplist").removeAttr("disabled");
                $(".divarrlist").show();
                $(".fuplist").not('select, button').val("");
            }

            $("#titleEquipos").text("Equipos y Adicionales")
            $("#tbEquipos").attr("style", "display: normal")
        }

        if (tipo_cotizacion > 2) {
            $("#headerEquipos").attr("style", "display:none");
            $(".fupadap").find('input[type="text"]').val("0");
            $('.fupadap').not("select, button").val("0");
            $(".fuparr").not('select, button').val("0");
        }
        else {
            $("#headerEquipos").attr("style", "display:normal");
        }

        if ($("#selectCopia").val() == "1") {
            $(".vaCopia").show();
        }
        else {
            $(".vaCopia").hide();
        }

    });
    // Tipo Condicion Pago
    $("#cboCondicionesPago").change(function () {
        var condicionPago = $(this).val();
        $(".divarCuotas").hide();
        $(".divarLeasing").hide();

        //Se habilita todo de nuevo
        if (condicionPago == "2") {
            $(".divarCuotas").show();
        }
            //else if (condicionPago == "3")
        else {
            if (condicionPago == "3") {
                document.getElementById("titulocondiciones").innerHTML = "CONDICIONES LEASING";
                document.getElementById("titulototalcondiciones").innerHTML = "TOTAL LEASING";
            } else if (condicionPago == "4") {
                document.getElementById("titulocondiciones").innerHTML = "CONDICIONES CARTA CREDITO";
                document.getElementById("titulototalcondiciones").innerHTML = "TOTAL CARTA CREDITO";
            } else {
                document.getElementById("titulocondiciones").innerHTML = "CONDICIONES CONTADO";
                document.getElementById("titulototalcondiciones").innerHTML = "TOTAL CONTADO";
            }
            $(".divarLeasing").show();
        }
        //LlenarCondicionesPago();
        ObtenerCondicionesPago(IdFUPGuardado, $('#cboVersion').val());
        obtenerDetalleCondicionesPago(IdFUPGuardado, $('#cboVersion').val());
        ocultarLoad();
    });
}

function cargarDatosDependeAlturaLibre(fupConsultado) {
    $("#txtAlturaLibreMinima").val(fupConsultado.AlturaLibreMinima);
    $("#txtAlturaLibreMaxima").val(fupConsultado.AlturaLibreMaxima);
    $("#txtAlturaLibreCual").val(fupConsultado.AlturaLibreCual);
    $("#txtAlturaInternaSugerida").val(fupConsultado.AlturaInternaSugerida);
    $("#selectTipoFMFachada").val(fupConsultado.TipoFachada);
    $("#txtAlturaUnion").val(fupConsultado.AlturaUnion);
    $("#txtTipoUnion").val(fupConsultado.TipoUnion);
    $("#selectDetalleUnion").val(fupConsultado.DetalleUnion);
    $("#txtAlturaCap1").val(fupConsultado.AlturaCAP1);
    $("#txtAlturaCap2").val(fupConsultado.AlturaCAP2);
    var reqcliente = (fupConsultado.ReqCliente == true || fupConsultado.ReqCliente == "true") ? "1" : "2";
    if (reqcliente == "1") {
        $(".divReqCliente").show();
    }
    else {
        $(".divReqCliente").hide();
    }
    $("#selectReqCliente").val(reqcliente);
    $("#txtAlturaInternaSugeridaCliente").val(fupConsultado.AlturaIntSugeridaCliente);
    $("#selectTipoFMFachadaCliente").val(fupConsultado.TipoFachadaCliente);
    $("#txtAlturaUnionCliente").val(fupConsultado.AlturaUnionCliente);
    $("#txtTipoUnionCliente").val(fupConsultado.TipoUnionCliente);
    $("#selectDetalleUnionCliente").val(fupConsultado.DetalleUnionCliente);
    $("#txtAlturaCapCliente1").val(fupConsultado.AlturaCAP1Cliente);
    $("#txtAlturaCapCliente2").val(fupConsultado.AlturaCAP2Cliente);
}

function cargarDatosDependeTipoFachada(fupConsultado) {

    var reqCapPernado = (fupConsultado.CapPernado == true || fupConsultado.CapPernado == "true") ? "1" : "2";
    $("#selectCAPPernado").val(reqCapPernado);
    if (reqCapPernado == "1") {
        $(".divCAPPernado").show();
        $("#CapPernadolbl").show();
        $("#selectCAPPernado").show();
    }
    else {
        $(".divCAPPernado").hide();
        $("#CapPernadolbl").hide();
        $("#selectCAPPernado").hide();
    }
}

// Variable para imprimir el nivel de complejidad en la parte superior
var fupAprobado = false;
function obtenerInformacionFUP(idFup, idVersion, idioma) {

    // Desahabilito todos los botones de guardado
    $(".controlarDisponibilidadAprobacion").attr("hidden", "hidden");

    IdFUPGuardado = idFup;
    mostrarLoad();
    cargarcombosAvalesdefault();
    CrearDetalleDocumentosCierre();
    LimpiarDescuentosCierre()
    var param = { idFup: idFup, idVersion: idVersion, idioma: idioma };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerInformacionPorFupVersion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            seleccionALturaLibre();
            var idCiudad = 0;
            var idCliente = 0;
            var idContacto = 0;
            var idObra = 0;



            $.each(data, function (index, elem) {
                if (index == 'infoGeneral') {
                    $("#cboIdPais").val(elem.ID_pais).val(String(elem.ID_pais)).select2().change(cargarCiudades(elem.ID_pais, elem));
                    $("#cboIdMoneda").val(elem.ID_Moneda).change();
                    $("#selectTipoNegociacion").val(elem.TipoNegociacion);
                    //$("#cboTipoCotizacion").val(elem.TipoCotizacion);
                    //$("#selectProducto").val(elem.Producto).change();
                    CargarDatosGeneralesNegociacionLoad(elem);
                    $("#selectTipoVaciado").val(elem.TipoVaciado);
                    $("#txtCantidadPisos").val(elem.MaxPisos);
                    $("#txtCantidadFundicionesPiso").val(elem.FundicionPisos);
                    $("#txtNroEquipos").val(elem.NumeroEquipos);
                    $("#selectSistemaSeguridad").val(elem.SistemaSeguridad);
                    $("#selectAlineacionVertical").val(elem.AlineacionVertical);
                    $("#selectFormaConstruccion").val(elem.FormaConstructiva);
                    $("#txtDistMinEdificaciones").val(elem.DistanciaEdifica);
                    $("#selectJuntaDilatacion").val(elem.DilatacionMuro).change();
                    $("#txtEspesorJuntas").val(elem.EspesorJunta);
                    $("#selectDesnivel").val(elem.Desnivel);
                    //$("#selectAlturaLibre").val(elem.AlturaLibre).change();
                    //cargarDatosDependeAlturaLibre(elem);
                    //cargarDatosDependeTipoFachada(elem);
                    $("#cboClaseCotizacion").val(elem.ClaseCotizacion).change();
                    $("#divEstadoFup").html(elem.EstadoProceso);
                    $("#txtEstadoCliente").val(elem.EstadoCli);
                    $("#txtFechaCreacion").val(elem.Fecha_crea);
                    $("#txtCreadoPor").val(elem.UsuarioCrea);
                    $("#txtCotizadoPor").val(elem.Cotizador);
                    $("#selectTipoUnionCliente").val(elem.TipoUnionCliente);
                    $("#selectTerminoNegociacion").val(elem.TerminoNegociacion);
                    $("#selectTerminoNegociacion2").val(elem.TerminoNegociacion);
                    $("#selectTerminoNegociacion3").val(elem.TerminoNegociacion);
                    $("#txtProbabilidad").val(elem.Probabilidad);
                    $("#txtOtros").val(elem.otros);
                    var fecsol = elem.FecSolicitaCliente.substring(0, 10);
                    $('#txtFechaSolicitaCliente').val(elem.FecSolicitaCliente.substring(0, 10));
                    $("#btnGrabaFechaSolicita").Enable = true;
                    $('#txtFechaEntregaCotizaCliente').val(elem.FecEntregaCliente.substring(0, 10));
                    FecFacturar = elem.FecFacturar;

                    $('#txtAlumIC1').prop('checked', false);
                    $('#txtAlumNa1').prop('checked', false);
                    $('#txtAlumIC2').prop('checked', false);
                    $('#txtAlumNa2').prop('checked', false);
                    $('#txtAlumIC3').prop('checked', false);
                    $('#txtAlumNa3').prop('checked', false);
                    AlumCompleto = elem.ArmadoAluminio;
                    EscaCompleto = elem.ArmadoEscalera;
                    AcceCompleto = elem.ArmadoAccesorio;

                    if (elem.EstadoProceso == "Aprobado") {
                        $("#trComplejidad").removeAttr("hidden");
                        fupAprobado = true;
                    } else {
                        $("#trComplejidad").attr("hidden", "hidden");
                        fupAprobado = false;
                    }
                    if (elem.EstadoProceso == "Aprobado" || elem.EstadoProceso == "Cotizado" || elem.EstadoProceso == "Recotizado" ||
                        elem.EstadoProceso == "Cierre Comercial" || elem.EstadoProceso == "Solicitud Facturacion" || elem.EstadoProceso == "Orden Fabricacion" ||
                        elem.EstadoProceso == "Pre-Cierre Elaboracion" || elem.EstadoProceso == "Pre-Cierre" || elem.EstadoProceso == "Aval para SF Elaboracion" ||
                        elem.EstadoProceso == "Aval para SF" || elem.EstadoProceso == "Modulacion Final") {
                        $(".controlarDisponibilidadAprobacion").removeAttr("hidden");
                    }

                    // Condicional para habilitar las consideraciones y observaciones
                    if (elem.EstadoProceso == "Modulacion Final" || elem.EstadoProceso == "Orden Fabricacion") {
                        $('#containerBtnCreateConsiderationObservation').show();
                    } else {
                        $('#containerBtnCreateConsiderationObservation').hide();
                    }


                    if (elem.ArmadoAluminio == 2) { $('#txtAlumNa1').prop('checked', true); }
                    else { if (elem.ArmadoAluminio == 1) { $('#txtAlumIC1').prop('checked', true); } }

                    if (elem.ArmadoEscalera == 2) { $('#txtAlumNa2').prop('checked', true); }
                    else { if (elem.ArmadoEscalera == 1) { $('#txtAlumIC2').prop('checked', true); } }

                    if (elem.ArmadoAccesorio == 2) { $('#txtAlumNa3').prop('checked', true); }
                    else { if (elem.ArmadoAccesorio == 1) { $('#txtAlumIC3').prop('checked', true); } }

                    EstadoFUP = elem.EstadoProceso;

                    if (elem.TipoNegociacion == 1 || elem.TipoNegociacion == 2) {
                        $(".divarrlist").show();
                    }
                    else if (elem.TipoNegociacion >= 3) {
                        $(".divarrlist").hide();
                    }
                    else {
                        $(".divarrlist").show();
                    }

                    //Orden de Referencia Solo para Adaptacion y Listado
                    if (elem.TipoNegociacion != "2" && elem.TipoNegociacion != "3") {
                        if (elem.TipoNegociacion == "1" && elem.EquipoCopia == 1) {
                            $(".divvarof").show();
                        }
                        else {
                            $(".divvarof").hide();
                        }
                    }
                    else {
                        $(".divvarof").show();
                    }

                    $("#txtNroEquipos").attr("disabled", "disabled");
                    $(".fupgenlist").hide();

                    if (elem.TipoCotizacion == 2) {
                        $("#txtNroEquipos").removeAttr("disabled");
                        $(".divvarof").show();
                        $(".divarrlist").show();
                        $("#titleEquipos").text("Adaptaciones")
                        $("#tbEquipos").attr("style", "display: none")
                    }
                    else if (elem.TipoCotizacion >= 3) {
                        $(".divvarof").show();
                        $(".fupadap").removeAttr("disabled");
                        $(".divarrlist").hide();
                        $("#titleEquipos").text("Equipos y Adicionales")
                        $("#tbEquipos").attr("style", "display: normal")
                        $(".fupgenlist").show();
                    }
                    else {
                        $("#titleEquipos").text("Equipos y Adicionales")
                        $("#tbEquipos").attr("style", "display: normal")
                        if (elem.TipoNegociacion < "3") {
                            $(".divarrlist").show();
                            $(".fupadap").removeAttr("disabled");
                        }

                        $("#titleEquipos").text("Equipos y Adicionales")
                        $("#tbEquipos").attr("style", "display: normal")
                        // $(".fupadap").find('input[type="text"]').val("");
                    }

                    if (elem.TipoCotizacion > 2) {
                        $("#headerEquipos").attr("style", "display:none");
                    }
                    else {
                        $("#headerEquipos").attr("style", "display:normal");
                    }
                    if (elem.TipoCotizacion == 3) { getListaPrecios(); }

                    // Proceso de Fup Copia
                    $("#selectCopia").val(elem.EquipoCopia).change();
                    $("#txtFupCopia").val(elem.FupCopia);
                    $("#txtProductoCopia").val(elem.ProductoCopia);
                };
                if (index == 'ordenFabricacion') {
                    $("#txtIdOrden").val(elem);
                };
                if (index == 'ordenCotizacion') {
                    $("#txtOrdenCotizacion").val(elem);
                };
                if (index == 'infoGeneralTablas') {
                    LlenarTablasFUP(elem);
                };
                if (index == 'listaEqui') {
                    LlenarEquipos(elem);
                };
                if (index == 'listaAdd') {
                    LlenarAdaptaciones(elem);
                };
                if (index == 'salcot') {
                    LlenarSalidaCotizacion(elem);
                };
                if (index == 'anexosalcot') {
                    LlenarAnexoSalcot(elem, 6);
                };
                if (index == 'varComentariosSC') {
                    LlenarComentarioSalcot(elem);
                };
                if (index == 'anexosalfin') {
                    LlenarAnexoSalcot(elem, 10);
                };
                if (index == 'anexoDocFac') {
                    LlenarAnexoSalcot(elem, 11);
                };
                if (index == 'varDevComer') {
                    LlenarDevolCom(elem);
                };

                if (index == 'listaReCotizacion') {
                    llenarListaRecotizacion(elem);
                };
                if (index == 'varDataAprobacion') {
                    LlenarAprobacion(elem);
                }
                if (index == 'varDataRechazo') {
                    LlenarDevolucion(elem);
                }
                if (index == 'varFecSeg') {
                    LlenarFecSeg(elem);
                }
                if (index == 'listaAnexos') {
                    LlenarAnexoFup(elem);
                };
                if (index == 'listaPrecierre') {
                    LlenarVentaCierreCom(elem);
                };
                if (index == 'listaPTF') {
                    LlenarPTF(elem);
                };
                if (index == 'listaFecSol') {
                    LlenarFechasSol(elem);
                };
                if (index == 'listaOF') {
                    LlenarParteOF(elem);
                };
                if (index == 'listaPlantaOF') {
                    $("#cmbPlantaOrdenes").html(llenarComboId(elem));
                    $("#cmbPlantaOrdenes").val("-1").change();
                };
                if (index == 'varFlete') {
                    limpiar_flete();
                    llenarFlete(elem, 2);
                };
                if (index == 'varEventoPFT') {
                    listaEveptf = data.varEventoPFT;
                    $("#cboEstadoPlanoTipoForsa").html("");
                    $("#cboEstadoPlanoTipoForsa").html(llenarComboId(listaEveptf));
                };
                if (index == 'varAnexPTF') {
                    LlenarAnexoPTF(elem);
                };
                if (index == 'varModfinal') {
                    LlenarModulafinal(elem);

                };
                if (index == "varOrdenCliente") {
                    LlenarOrdenCliente(elem)
                };
                if (index == "varAnexoFinal") {
                    LlenarAnexoFinal(elem, 25);
                };
                if (index == 'varAnexoCondPago') {
                    LlenarDetalleCondicionesPago(elem);
                    ValCondicionPago();
                };
                if (index == 'varAnexoDocumentosCierre') {
                    LlenarDetalleDocumentosCierre(elem);
                    LlenarAnexoDeCierre(elem);
                };
                if (index == 'varAvales') {
                    LlenarAvalesFabricacion(elem);
                };
                if (index == 'varControlCambio') {
                    LlenarControlCambio(elem);
                }
                if (index == 'varArmado') {
                    LlenarArmado(elem);
                }
                if (index == 'varSegLogistico') {
                    LlenarSegLogistico(elem);
                }
                if (index == 'varHallazgosObra')
                    LlenarHallazgosObra(elem);

            });

            ObtenerLineasDinamicas();
            MostrarCards();
            ValidarEstado();
            $(".SoloUpd").show();

        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to search FUP by Version and idFup", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function guardarAprobacionFUP() {
    var objg = {};

    if (validarCamposAprobacionFup()) {
        mostrarLoad();
        $("[data-modelo-aprobacion]").each(function (index) {
            var prop = $(this).attr("data-modelo-aprobacion");
            var thisval = $(this).val();
            objg[prop] = thisval;
        });

        if (IdFUPGuardado == null) {
            objg["IdFUP"] = 0;
        } else {
            objg["IdFUP"] = IdFUPGuardado;
        }
        if ($('#cboVersion').val() == null) {
            objg["Version"] = versionFupDefecto;
        } else {
            objg["Version"] = $('#cboVersion').val();
        }

        objg["estado"] = EstadoFUP;

        var param = {
            item: objg
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/guardarAprobacionFUP",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                toastr.success("Guardado correctamente");
                ValidarEstado();
                obtenerParteAprobacionFUP(IdFUPGuardado, $('#cboVersion').val());
                // Para Fups Aprobados y Equipo nuevo se genera CT automaticamente
                if ($('#cboVoBoFup').val() == 1 && $("#cboTipoCotizacion").val() == "1") {
                    guardarOrdenCotizacion();
                }
            },
            error: function (e) {
                ocultarLoad();
                toastr.error("Failed guardar aprobacion", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
    else {
        toastr.error("Faltan Datos en Aprobacion", "FUP", {
            "timeOut": "0",
            "extendedTImeout": "0"
        });
    }
}


function guardarFecSeg() {
    var objg = {};
    mostrarLoad();
    $("[datamodel-fecseg]").each(function (index) {
        var prop = $(this).attr("datamodel-fecseg");
        var thisval = $(this).val();
        objg[prop] = thisval;
    });

    if (IdFUPGuardado == null) {
        objg["IdFUP"] = 0;
    } else {
        objg["IdFUP"] = IdFUPGuardado;
    }
    if ($('#cboVersion').val() == null) {
        objg["Version"] = versionFupDefecto;
    } else {
        objg["Version"] = $('#cboVersion').val();
    }


    var param = {
        item: objg
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/guardarFecSeg",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            toastr.success("Guardado correctamente");
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed guardar Seguimiento Fechas Cotizacion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });


}

function guardarEnAnalisis() {
    event.preventDefault();
    var objg = {};

    mostrarLoad();


    if (IdFUPGuardado == null) {
        objg["IdFUP"] = 0;
    } else {
        objg["IdFUP"] = IdFUPGuardado;
    }
    if ($('#cboVersion').val() == null) {
        objg["Version"] = versionFupDefecto;
    } else {
        objg["Version"] = $('#cboVersion').val();
    }
    //objg["estado"] = EstadoFUP;
    if ($('#txtAnalisis').prop("checked") == false) { objg["EnAnalisis"] = "0"; } else { objg["EnAnalisis"] = "1"; }


    var param = {
        item: objg
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/guardarEnAnalisis",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            toastr.success("Guardado correctamente");
            //ValidarEstado();
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed guardar En Analisis", "Aprobación FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function LlenarAprobacion(elem) {
    $("#txtNumeroModulaciones").val(elem.Modulaciones);
    $("#txtNumeroCambios").val(elem.Cambios);
    $("#txtObservacionAprobacion").val(elem.rct_observacion);
    $("#cboVoBoFup").val(elem.VistoBueno).change();
    $("#cboMotivoRechazoFup").val("-1").change();
    $("#txtAlturaFormaletaAproba").val(elem.AlturaFormaleta);
    $("#txtAnalisis").prop('checked', false);
    if (elem.EnAnalisis == "1")
        $("#txtAnalisis").prop('checked', true);
    $("#txtDetallesAproba").val(elem.DescDetalles);
    $("#txtOrdenRefAproba").val(elem.NoReferencia);
    $("#txtTipoSisSeg").val(elem.DescSisSeguridad);
    $("#txtM2CtAproba").val(elem.CtM2);
    $("#txtPiezasCtAproba").val(elem.CtPiezas);
    $("#txtPiezasM2Aproba").val(elem.CtM2Piezas);
    $("#txtNivelComplejidad").val(elem.NivelComplejidad);
    if (fupAprobado) {
        $("#txtComplejidadGeneral").html(elem.NivelComplejidad);
    }
    $("#txtidNivelComplejidad").val(elem.IdNivelComplejidad);

    $("#txtNumeroModulacionesSC").val(elem.Modulaciones);
    $("#txtNumeroCambiosSC").val(elem.Cambios);

    $("#txtEstadoDft").val(elem.EstadoDft);
    $("#txtRequiereReco").val(elem.RequiereRecotizaDft);

    if (elem.FecAprobClienteDft == "1900-01-01T00:00:00") {
        $("#txtFecAprobaClienteDFT").val("");
    } else {
        $("#txtFecAprobaClienteDFT").val(elem.FecAprobClienteDft.substr(0, 10));
    }

    if (elem.FecAprobDft == "1900-01-01T00:00:00") {
        $("#txtFecAprobaDFT").val("");
    } else {
        $("#txtFecAprobaDFT").val(elem.FecAprobDft.substr(0, 10));
    }

    $("#txtObservacionAprobacionDFT").val(elem.ObservacionDft);
    $("#txtVersionAprobDFT").val(elem.VersionAprobDft);

    NivelComplejidad();

}

function NivelComplejidad() {
    var nivel = $("#txtNivelComplejidad").val();
    var pzm2 = 0.0
    if (parseFloat($("#txtPiezasM2Aproba").val()) > 0) {
        pzm2 = parseFloat($("#txtPiezasCtAproba").val()) / parseFloat($("#txtPiezasM2Aproba").val());
    }

    if ($("#cboTipoCotizacion").val() > 0 && $("#cboTipoCotizacion").val() < 4) {
        if ($("#cboTipoCotizacion").val() == 3) {  // Solo para listados
            if (parseFloat($("#txtOrdenRefAproba").val()) < 4) { nivel = "Media" }
            else { nivel = "Alta" }

            if (pzm2 > 3 && nivel == "Media") { nivel = "Alta" }

        }
        else { // Equipos Nuevos y Adaptaciones
            nivel = "Baja"

            if (parseFloat($("#txtNumeroModulaciones").val()) > 3 && parseFloat($("#txtNumeroModulaciones").val()) < 6) { nivel = "Media" }
            if (parseFloat($("#txtNumeroCambios").val()) > 6 && parseFloat($("#txtNumeroCambios").val()) < 11) { nivel = "Media" }
            if (parseFloat($("#txtOrdenRefAproba").val()) > 0 && parseFloat($("#txtOrdenRefAproba").val()) < 4) { nivel = "Media" }
            if (pzm2 > 2) { nivel = "Media" }
            if (parseFloat($("#txtNumeroModulaciones").val()) > 5) { nivel = "Alta" }
            if (parseFloat($("#txtNumeroCambios").val()) > 10) { nivel = "Alta" }
            if (parseFloat($("#txtOrdenRefAproba").val()) > 3) { nivel = "Alta" }
            if (pzm2 > 3) { nivel = "Alta" }
            if (($("#txtDetallesAproba").val().includes("culata") || $("#txtDetallesAproba").val().includes("inclinada"))) { nivel = "Alta" }
            if ($("#txtTipoSisSeg").val().includes("trepante")) { nivel = "Alta" }
        }
    }
    else { nivel = "Por Definir" }

    let idnc = 0;
    if (nivel == "Alta") idnc = 3;
    if (nivel == "Media") idnc = 2;
    if (nivel == "Baja") idnc = 1;

    $("#txtNivelComplejidad").val(nivel);
    $("#txtidNivelComplejidad").val(idnc);


}

function LlenarDevolucion(elem) {
    var rowsDetalleAprobacion = "";
    $.each(elem, function (i, item) {
        rowsDetalleAprobacion = rowsDetalleAprobacion + "<tr><td>" + item.rct_fecha_rechazo.substring(0, 10) + "</td>" +
                            "<td>" + item.tre_descripcion + "</td>" +
                            "<td>" + item.rct_observacion + "</td>" +
                            "<td>" + item.estado + "</td>" +
                            "</tr>";
    });
    if (rowsDetalleAprobacion.trim() == "") {
        rowsDetalleAprobacion = "<tr></tr>";
    }
    $("#tbodyDetailsDev").html(rowsDetalleAprobacion);
}

function LlenarFecSeg(elem) {
    if (elem.FecSolicitaCliente.substring(0, 10) != '1900-01-01')
        $("#txtFecSolCliente").val(elem.FecSolicitaCliente.substring(0, 10));
    else
        $("#txtFecSolCliente").val("");
    if (elem.FecAprobado.substring(0, 10) != '1900-01-01')
        $("#txtFecAprobacion").val(elem.FecAprobado.substring(0, 10));
    else
        $("#txtFecAprobacion").val("");
    $("#txtDiasEvento1").val(elem.AlDias);
    if (elem.AlPlaneda.substring(0, 10) != '1900-01-01')
        $("#txtFec1Evento1").val(elem.AlPlaneda.substring(0, 10));
    else
        $("#txtFec1Evento1").val("");
    if (elem.AlConfirma.substring(0, 10) != '1900-01-01')
        $("#txtFec2Evento1").val(elem.AlConfirma.substring(0, 10));
    else
        $("#txtFec2Evento1").val("");
    if (elem.AlReal.substring(0, 10) != '1900-01-01')
        $("#txtFec3Evento1").val(elem.AlReal.substring(0, 10));
    else
        $("#txtFec3Evento1").val("");
    if (elem.AlAprobado.substring(0, 10) != '1900-01-01')
        $("#txtFec4Evento1").val(elem.AlAprobado.substring(0, 10));
    else
        $("#txtFec4Evento1").val("");
    $("#txtDiasEvento2").val(elem.AccDias);
    if (elem.AccPlaneda.substring(0, 10) != '1900-01-01')
        $("#txtFec1Evento2").val(elem.AccPlaneda.substring(0, 10));
    else
        $("#txtFec1Evento2").val("");
    if (elem.AccConfirma.substring(0, 10) != '1900-01-01')
        $("#txtFec2Evento2").val(elem.AccConfirma.substring(0, 10));
    else
        $("#txtFec2Evento2").val("");
    if (elem.AccReal.substring(0, 10) != '1900-01-01')
        $("#txtFec3Evento2").val(elem.AccReal.substring(0, 10));
    else
        $("#txtFec3Evento2").val("");
    if (elem.AccAprobado.substring(0, 10) != '1900-01-01')
        $("#txtFec4Evento2").val(elem.AccAprobado.substring(0, 10));
    else
        $("#txtFec4Evento2").val("");
}

function CalcularFecPlan(tipo) {
    var fecha = new Date();
    var dias = 0;
    var sfecha = "";
    if (tipo == 1) {
        fecha = new Date($("#txtFecAprobacion").val());
        dias = parseInt($("#txtDiasEvento1").val());
        fecha.setDate(fecha.getDate() + dias);
        sfecha = fecha.toISOString().slice(0, 10);
        $("#txtFec1Evento1").val(sfecha);
    }
    if (tipo == 2) {
        fecha = new Date($("#txtFec4Evento1").val());
        dias = parseInt($("#txtDiasEvento2").val());
        fecha.setDate(fecha.getDate() + dias);
        sfecha = fecha.toISOString().slice(0, 10);
        $("#txtFec1Evento2").val(sfecha);
    }


}
function LlenarAprobacionFUP(data) {
    $.each(data, function (index, elem) {
        if (index == 'varDataAprobacion') {
            LlenarAprobacion(elem);
        }
        if (index == 'varDataRechazo') {
            LlenarDevolucion(elem);
        }
        if (index == 'varFecSeg') {
            LlenarFecSeg(elem);
        }
    });
}

function obtenerParteAprobacionFUP(idFup, idVersion) {
    var param = { idFup: idFup, idVersion: idVersion };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerAprobacionesFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarAprobacionFUP(data);
        },
        error: function () {
            toastr.error("Failed to load details approb", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function UploadFielModalShow(title, optionDefault, zona, EventoId, EsArmado) {
    if (EsArmado == undefined) { EsArmado = 0 };

    if (EsArmado == 1) {
        $("#TipoArchivoModal").html("");
        $("#TipoArchivoModal").html(llenarComboId(listaTipoAnexoArmado));
    }
    else {
        $("#TipoArchivoModal").html("");
        $("#TipoArchivoModal").html(llenarComboId(listaTipoAnexo));
    }

    if (optionDefault) {
        $("#TipoArchivoModal").val(optionDefault).prop("disabled", true)
    }
    else {
        $("#TipoArchivoModal").val(-1).prop("disabled", false)
    }

    $("#rutaArchivo").val();
    $("#zonaArchivo").val(zona);
    $("#EventoPTF").val(EventoId);

    var modal = $("#UploadFilesModal");
    modal.find('.modal-title').text(title);
    modal.modal('show');
}

function subirAnexoptf(Evento_id) {
    UploadFielModalShow('Cargar Documentos Planos TF', 0, 'Planos TF', Evento_id);
}

function CargarDetalleCondPago(Evento_id) {
    UploadFielModalShow('Cargar Documentos Detalle Condición de Pago', Evento_id, 'Detalle Condiciones de Pago', 0);
}

function CargarDetalleDocumentosCierre(Evento_id) {
    UploadFielModalShow('Cargar Documentos de Cierre', Evento_id, 'Documentos de Cierre', 0);
}

function ClonarFupModal() {
    var modal = $("#ClonarFupModal");
    modal.find('Duplicar FUP').text('Duplicar FUP');
    modal.modal('show');
}

function ReportModalShow(title, optionDefault) {
    if (optionDefault) {
        $("#ModReporteFup").val(optionDefault).prop("disabled", true)
    }
    else {
        $("#ModReporteFup").val(-1).prop("disabled", false)
    }
    PrepararReporteFUP();
    var modal = $("#ModReporteFup")
    modal.find('.modal-title').text(title)
    modal.modal('show');
}

function ControlCambioShow(title, optionDefault, Respuesta, Titulo, dft) {
    if (optionDefault) {
        $("#ModControlCambios").val(optionDefault).prop("disabled", true)
    }
    else {
        $("#ModControlCambios").val(-1).prop("disabled", false)
    }
    $("#padreCambio").val(Respuesta);
    $("#EsDFT").val(dft);

    if (Titulo.length > 0) {
        $("#txtTituloObs").val(Titulo)
        $("#txtTituloObs").prop("disabled", true);
    }
    else
        $("#txtTituloObs").prop("disabled", false);

    if (dft == 1) {
        $(".Rdft").hide();
        $(".Rdft2").show();
    }
    else {
        $(".Rdft").show();
        $(".Rdft2").hide();
    }

    if (Respuesta == 0)
        $("#AreaControl").hide();
    else {
        $("#AreaControl").val("Comentario Padre : " + Respuesta);
        $("#AreaControl").show();
    }
    $("#txtObsCntrCm").val("");
    var modal = $("#ModControlCambios");
    modal.find('.modal-title').text(title);
    modal.modal('show');
}

function validarCamposAprobacionFup() {
    var flag = true;

    $('#cboVoBoFup').css("border", "");
    $('#txtNumeroModulaciones').css("border", "");
    $('#txtNumeroCambios').css("border", "");
    $('#cboMotivoRechazoFup').css("border", "");
    $('#txtObservacionAprobacion').css("border", "");
    $('#txtAlturaFormaletaAproba').css("border", "");

    if ($.isNumeric($('#cboVoBoFup').val()) == false || $('#cboVoBoFup').val() == -1) {
        $('#cboVoBoFup').css("border", "2px solid crimson");
        flag = false;
    } else {
        if ($('#txtObservacionAprobacion').val().trim() == "") {
            $('#txtObservacionAprobacion').css("border", "2px solid crimson");
            flag = false;
        }
        if ($('#cboVoBoFup').val() == 1) {
            if ($.isNumeric($('#txtNumeroModulaciones').val()) == false) {
                $('#txtNumeroModulaciones').css("border", "2px solid crimson");
                flag = false;
            }
            if ($.isNumeric($('#txtNumeroCambios').val()) == false) {
                $('#txtNumeroCambios').css("border", "2px solid crimson");
                flag = false;
            }
            if ($.isNumeric($('#txtAlturaFormaletaAproba').val()) == false) {
                $('#txtAlturaFormaletaAproba').css("border", "2px solid crimson");
                flag = false;
            }

        }
        if ($('#cboVoBoFup').val() == 2) {
            if ($.isNumeric($('#cboMotivoRechazoFup').val()) == false || $('#cboMotivoRechazoFup').val() == -1) {
                $('#cboMotivoRechazoFup').css("border", "2px solid crimson");
                flag = false;
            }
            if (flag && $.isNumeric($('#txtNumeroModulaciones').val()) == false) {
                $('#txtNumeroModulaciones').val("0");
            }
            if (flag && $.isNumeric($('#txtNumeroCambios').val()) == false) {
                $('#txtNumeroCambios').val("0");
            }
            if (flag && $.isNumeric($('#txtAlturaFormaletaAproba').val()) == false) {
                $('#txtAlturaFormaletaAproba').val("0");
            }
        }
    }
    return flag;
}

function LimpiarDescuentosCierre() {
    //$("#dtfirmaContrato").val(fec1);
    //$("#dtfechacontractual").val(fec2);
    //$("#dtFormalizaPago").val(fec3);
    //$("#dtPlazo").val(elem.Plazo);
    $("#NumberDiasistecnica").val("0");
    $("#Numberm2Cerrados").val("0");
    $("#NumberTotalCierre").val("0");

    $("#NumberTotalCierre2").val("0");
    $("#NumberTotalCierre3").val("0");

    $("#NumberTotalFacturacion").val("0");
    $("#m2Modulados").val("0");
    $("#vrTotalModulado").val("0");
    $("#VlrDcto1Cuenta").val("0");
    $("#VlrDcto2Cuenta").val("0");
    $("#VlrDcto3Cuenta").val("0");
    $("#VlrDcto4Cuenta").val("0");
    $("#VlrDcto5Cuenta").val("0");
    $("#VlrNiv1m2").val("0");
    $("#VlrDcto1").val("0");
    $("#VlrDcto2").val("0");
    $("#VlrDcto3").val("0");
    $("#VlrDcto4").val("0");
    $("#VlrDcto5").val("0");
    $("#VlrDcto1CuentaB").val("0");
    $("#VlrDcto2CuentaB").val("0");
    $("#VlrDcto3CuentaB").val("0");
    $("#VlrDcto4CuentaB").val("0");
    $("#VlrDcto5CuentaB").val("0");
    $("#VlrDcto1B").val("0");
    $("#VlrDcto2B").val("0");
    $("#VlrDcto3B").val("0");
    $("#VlrDcto4B").val("0");
    $("#VlrDcto5B").val("0");

    $("#Numberm2N1A").val("0");
    $("#Numberm2N13A").val("0");
    $("#Numberm2N1B").val("0");
    $("#Numberm2N13B").val("0");
}

function LlenarAnexoFup(data) {
    var rowsAnexosFup = "";
    $.each(data, function (index, elem) {
        rowsAnexosFup = rowsAnexosFup + "<tr><td>" + elem.tan_desc_esp + "</td>" +
            "<td>" + elem.Anexo + "</a></td>" +
            "<td>" + elem.plano_descripcion + "</td>" +
            "<td>" + elem.estado + "</td>" +
            "<td>" + elem.fecha_crea + "</td>" +
            "<td><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button></td>" +
            "<td><button type=\"button\" class=\"fa fa-trash fupborrar \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
            "</tr>";
    });
    if (rowsAnexosFup.trim() == "") {
        rowsAnexosFup = "<tr></tr>";
    }
    $("#tbodyAnexosFup").html(rowsAnexosFup);
}

function LlenarDetalleDocumentosCierre(data) {

    CrearDetalleDocumentosCierre();

    $.each(data, function (index, elem) {
        var botones = "";
        botones = "<button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                  "<button type=\"button\" class=\"fa fa-trash fupborrar \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button>";
        switch (elem.tan_desc_esp) {
            case "Carta de Cierre":
                {
                    document.getElementById("CC1").innerHTML = elem.Anexo;
                    botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('9')\"> </button>";
                    $("#CC3").html(botones);
                }
                break;
                //case "Orden de Compra":
                //    {
                //        document.getElementById("OC1").innerHTML=elem.Anexo;
                //        botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('21')\"> </button>";
                //        $("#OC3").html(botones);
                //    }
                //    break;
            case "Contrato":
                {
                    document.getElementById("CO1").innerHTML = elem.Anexo;
                    botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('20')\"> </button>";
                    botones += '<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Confirmar que el proyecto cuenta con los documentos legales revisados y aprobados."><i class="fa fa-info-circle fa-lg"></i></button>'
                    $("#CO3").html(botones);
                }
                break;
            case "Oferta Mercantil":
                {
                    document.getElementById("OM1").innerHTML = elem.Anexo;
                    botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('24')\"> </button>";
                    botones += '<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Oferta Mercantil"><i class="fa fa-info-circle fa-lg"></i></button>'
                    $("#OM3").html(botones);
                }
                break;
            case "Carta Final Modulado":
                {
                    document.getElementById("FM1").innerHTML = elem.Anexo;
                    botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('10')\"> </button>";
                    $("#FM3").html(botones);
                }
                break;
        }
    });
}

function CrearDetalleDocumentosCierre() {
    var rowsDetalleDocumentosCierre = "";
    rowsDetalleDocumentosCierre = rowsDetalleDocumentosCierre + "<tr><td>" + "Carta de Cierre" + "</td>" +
        "<td Id='CC1'></td>" +
        "<td Id='CC2'></td>" +
        "<td Id='CC3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleDocumentosCierre('9')\"> </button></td>" +
        "</tr>";
    //rowsDetalleDocumentosCierre = rowsDetalleDocumentosCierre + "<tr><td>" + "Orden de Compra" + "</td>" +
    //    "<td Id='OC1'></td>" +
    //    "<td Id='OC2'></td>" +
    //    "<td Id='OC3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleDocumentosCierre('21')\"> </button></td>" +
    //    "</tr>";
    rowsDetalleDocumentosCierre = rowsDetalleDocumentosCierre + "<tr><td>" + "Contrato" + "</td>" +
        "<td Id='CO1'></td>" +
        "<td Id='CO2'></td>" +
        "<td Id='CO3'>" +
        "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleDocumentosCierre('20')\"> </button>" +
        '<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Es requerido para valores mayores a US$50mil"><i class="fa fa-info-circle fa-lg"></i></button></td>'
    "</tr>";
    rowsDetalleDocumentosCierre = rowsDetalleDocumentosCierre + "<tr><td>" + "Oferta Mercantil" + "</td>" +
        "<td Id='OM1'></td>" +
        "<td Id='OM2'></td>" +
        "<td Id='OM3'>" +
        "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleDocumentosCierre('26')\"> </button>" +
        '<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Nuevo Documento de Oferta Mercantil"><i class="fa fa-info-circle fa-lg"></i></button></td>'
    "</tr>";
    rowsDetalleDocumentosCierre = rowsDetalleDocumentosCierre + "<tr><td>" + "Carta Final Modulado" + "</td>" +
    "<td Id='FM1'></td>" +
    "<td Id='FM2'></td>" +
    "<td Id='FM3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleDocumentosCierre('10')\"> </button></td>" +
    "</tr>";

    if (rowsDetalleDocumentosCierre.trim() == "") {
        rowsDetalleDocumentosCierre = "<tr></tr>";
    }
    $("#tbodydetalleDocumentosCierre").html(rowsDetalleDocumentosCierre);
}

function LlenarDetalleCondicionesPago(data) {

    CrearDetalleCondicionesPago();
    $.each(data, function (index, elem) {
        var condicionPago = 0;
        condicionPago = $("#cboCondicionesPago").val();
        var botones = "";
        botones = "<button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                  "<button type=\"button\" class=\"fa fa-trash fupborrar \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button>";
        var AnexXX = ""
        if ((typeof elem != "undefined" || typeof elem == Object) && condicionPago > 0) {
            if (elem.Anexo != "undefined" && elem.Anexo != "") {
                switch (elem.tan_desc_esp) {
                    case "Notificación SWIFT":
                        {
                            if (typeof document.getElementById("NS1") != "undefined" && document.getElementById("NS1") != null) {
                                document.getElementById("NS1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('19')\"> </button>";
                            $("#NS3").html(botones);
                        }
                        break;
                    case "Poliza SegurExpo":
                        {
                            if (typeof document.getElementById("PS1") != "undefined" && document.getElementById("PS1") != null) {
                                document.getElementById("PS1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('12')\"> </button>";
                            $("#PS3").html(botones);
                        }
                        break;
                    case "Carta Fianza":
                        {
                            if (typeof document.getElementById("CF1") != "undefined" && document.getElementById("CF1") != null) {
                                document.getElementById("CF1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('13')\"> </button>";
                            $("#CF3").html(botones);
                        }
                        break;
                    case "Fiador":
                        {
                            if (typeof document.getElementById("FI1") != "undefined" && document.getElementById("FI1") != null) {
                                document.getElementById("FI1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('23')\"> </button>";
                            $("#FI3").html(botones);
                        }
                        break;
                    case "Garantía Real":
                        {
                            if (typeof document.getElementById("GR1") != "undefined" && document.getElementById("GR1") != null) {
                                document.getElementById("GR1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('14')\"> </button>";
                            $("#GR3").html(botones);
                        }
                        break;
                    case "Carta de Aprobacion Leasing":
                        {
                            if (typeof document.getElementById("AL1") != "undefined" && document.getElementById("AL1") != null) {
                                document.getElementById("AL1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('15')\"> </button>";
                            $("#AL3").html(botones);
                        }
                        break;
                    case "Carta de Facturacion Leasing":
                        {
                            if (typeof document.getElementById("FL1") != "undefined" && document.getElementById("FL1") != null) {
                                document.getElementById("FL1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('16')\"> </button>";
                            $("#FL3").html(botones);
                        }
                        break;
                    case "Carta de Aprobacion Despacho":
                        {
                            if (typeof document.getElementById("AD1") != "undefined" && document.getElementById("AD1") != null) {
                                document.getElementById("AD1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('17')\"> </button>";
                            $("#AD3").html(botones);
                        }
                        break;
                    case "Carta de Recibido Satisfaccion":
                        {
                            if (typeof document.getElementById("RS1") != "undefined" && document.getElementById("RS1") != null) {
                                document.getElementById("RS1").innerHTML = elem.Anexo;
                            }
                            botones += "<button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('18')\"> </button>";
                            $("#RS3").html(botones);
                        }
                        break;
                }
            }
        }
    });
}

function CrearDetalleCondicionesPago() {
    var rowsDetalleCondicionesPago = "";
    var condicionPago = 0;

    var xlista = 0;
    condicionPago = $("#cboCondicionesPago").val();
    if (condicionPago == 1) {
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Notificación SWIFT" + "</td>" +
            "<td Id='NS1'></td>" +
            "<td Id='NS2'></td>" +
            "<td Id='NS3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('19')\"> </button></td>" +
            "</tr>";
    }
    if (condicionPago == 2) {
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Notificación SWIFT" + "</td>" +
            "<td Id='NS1'></td>" +
            "<td Id='NS2'></td>" +
            "<td Id='NS3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('19')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Poliza SegurExpo" + "</td>" +
            "<td Id='PS1'></td>" +
            "<td Id='PS2'></td>" +
            "<td Id='PS3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('12')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Carta Fianza" + "</td>" +
            "<td Id='CF1'></td>" +
            "<td Id='CF2'></td>" +
            "<td Id='CF3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('13')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Fiador" + "</td>" +
            "<td Id='FI1'></td>" +
            "<td Id='FI2'></td>" +
            "<td Id='FI3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('23')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Garantía Real" + "</td>" +
            "<td Id='GR1'></td>" +
            "<td Id='GR2'></td>" +
            "<td Id='GR3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('14')\"> </button></td>" +
            "</tr>";
    }
    if (condicionPago == 3) {
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Notificación SWIFT" + "</td>" +
            "<td Id='NS1'></td>" +
            "<td Id='NS2'></td>" +
            "<td Id='NS3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('19')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Carta de Aprobacion Leasing" + "</td>" +
            "<td Id='AL1'></td>" +
            "<td Id='AL2'></td>" +
            "<td Id='AL3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('15')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Carta de Facturacion Leasing" + "</td>" +
            "<td Id='FL1'></td>" +
            "<td Id='FL2'></td>" +
            "<td Id='FL3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('16')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Carta de Aprobacion Despacho" + "</td>" +
            "<td Id='AD1'></td>" +
            "<td Id='AD2'></td>" +
            "<td Id='AD3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('17')\"> </button></td>" +
            "</tr>";
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Carta de Recibido Satisfaccion" + "</td>" +
            "<td Id='RS1'></td>" +
            "<td Id='RS2'></td>" +
            "<td Id='RS3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('18')\"> </button></td>" +
            "</tr>";
    }
    if (condicionPago == 4) {
        rowsDetalleCondicionesPago = rowsDetalleCondicionesPago + "<tr><td>" + "Notificación SWIFT" + "</td>" +
            "<td Id='NS1'></td>" +
            "<td Id='NS2'></td>" +
            "<td Id='NS3'><button type=\"button\" class=\"fa fa-upload\" data-toggle=\"tooltip\" title=\"Cargar\" onclick=\"CargarDetalleCondPago('19')\"> </button></td>" +
            "</tr>";
    }


    if (rowsDetalleCondicionesPago.trim() == "") {
        rowsDetalleCondicionesPago = "<tr></tr>";
    }
    $("#tbodydetalleCondicionesPago").html(rowsDetalleCondicionesPago);
}

function obtenerParteAnexosFUP(idFup, idVersion) {
    var param = { idFup: idFup, idVersion: idVersion, TipoAnexo: 0 };

    $("#ParteAnexosFUP").show();

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerAnexosFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarAnexoFup(data);
        },
        error: function () {
            toastr.error("Failed to load details anexos", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function obtenerDetalleCondicionesPago(idFup, idVersion) {
    var param = { idFup: idFup, idVersion: idVersion, TipoPago: 3 };
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerDetalleCondicionesPago",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarDetalleCondicionesPago(data);
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load payment terms detail", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function obtenerDetalleDocumentosCierre(idFup, idVersion) {
    var param = { idFup: idFup, idVersion: idVersion, TipoPago: 3 };
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerDetalleDocumentosCierre",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarDetalleDocumentosCierre(data);
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load : closing documents", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function LlenarAnexoSalcot(data, TipoAnexo) {
    var rowsAnexosFup = "";
    $.each(data, function (index, elem) {
        rowsAnexosFup = rowsAnexosFup + "<tr><td>" + elem.Anexo + "</a></td>" +
                    "<td>" + elem.fecha_crea + "</a></td>" +
                    "<td><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button></td>" +
                    "</tr>";
    });
    if (rowsAnexosFup.trim() == "") {
        rowsAnexosFup = "<tr></tr>";
    }
    if (TipoAnexo == 6)
        $("#tbodyanexos_salidaCot").html(rowsAnexosFup);
    if (TipoAnexo == 10)
        $("#tbodyanexos_salidaCot2").html(rowsAnexosFup);
    if (TipoAnexo == 25)
        $("#tbodyanexos_M2Final").html(rowsAnexosFup);
}

function LlenarAnexoFinal(data, TipoAnexo) {
    var rowsAnexosFup = "";
    $.each(data, function (index, elem) {
        rowsAnexosFup = rowsAnexosFup + "<tr><td>" + elem.Anexo + "</a></td>" +
                    "<td>" + elem.fecha_crea + "</a></td>" +
                    "<td><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button></td>" +
                    "</tr>";
    });
    if (TipoAnexo == 25)
        $("#tbodyanexos_M2Final").html(rowsAnexosFup);

}

function obtenerAnexosSalidaCotFUP(idFup, idVersion, TipoAnexo) {
    var param = { idFup: idFup, idVersion: idVersion, TipoAnexo: TipoAnexo };
    if (TipoAnexo == 6)
        $("#tbodyanexos_salidaCot").show();
    if (TipoAnexo == 10)
        $("#tbodyanexos_salidaCot2").show();
    if (TipoAnexo == 11)
        $("#tbodyanexos_salidaCot3").show();
    if (TipoAnexo == 25)
        $("#tbodyanexos_M2Final").show();

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerAnexosFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarAnexoSalcot(data, TipoAnexo);
        },
        error: function () {
            toastr.error("Failed to load details anexos", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function obtenerParteAnexosPTF(idFup, idVersion) {
    var param = { idFup: idFup, idVersion: idVersion, TipoAnexo: -1 };

    $("#ParteAnexosFUP").show();

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerAnexosFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarAnexoPTF(data);
        },
        error: function () {
            toastr.error("Failed to load details anexos", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function LlenarAnexoCierre(idFup, idVersion) {
    var param = { idFup: idFup, idVersion: idVersion, TipoAnexo: -1 };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerAnexosCierre",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarAnexoDeCierre(data);
        },
        error: function () {
            toastr.error("Failed to load details anexos", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}



function obtenerVersionPorOF(idOrdenFabricacion) {
    mostrarLoad();
    var param = { idOrdenFabricacion: idOrdenFabricacion };
    var iteracion = 0;
    var idVersionReciente = '';

    $('#cboVersion').get(0).options.length = 0;
    $('#cboVersion').get(0).options[0] = new Option(versionFupDefecto, versionFupDefecto);

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerVersionPorOrdenFabricacion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            $.each(data, function (index, item) {
                if (iteracion == 0) {
                    $('#cboVersion').get(0).options.length = 0;
                    iteracion = 1;
                    idVersionReciente = item.eect_vercot_id;
                    $("#txtIdFUP").val(item.eect_fup_id);
                }
                $('#cboVersion').get(0).options[$('#cboVersion').get(0).options.length] = new Option(item.eect_vercot_id, item.eect_vercot_id);
            });

            if (idVersionReciente != '') {
                obtenerInformacionFUP($("#txtIdFUP").val(), idVersionReciente, idiomaSeleccionado);
            }

        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to search FUP", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ValidarReferencia(textReferencia) {
    var referencia = textReferencia.value;
    var param = { referencia: referencia };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ValidarReferencia",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            if (data.conf.id == 1) {
                $(textReferencia).next().text(data.referencia.hecha_en);
            }
            else {
                $(textReferencia).val("");
                $(textReferencia).next().text("");
                toastr.error("Referencia no encontrada", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }

        },
        error: function () {
            toastr.error("Failed to validate", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }

    });
}

function ValidarCopia(textReferencia) {
    var FupReferencia = textReferencia.value;
    var param = { referencia: FupReferencia };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ValidarCopiaFup",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            var cuentat3 = 0;
            $(".divContentOrdenReferencia").html("");
            cantidadOrdenReferencia = 0;
            $.each(data, function (i, item) {
                if (item.Fup != -1) {
                    if (cuentat3 == 0) {
                        $("#txtProductoCopia").val(item.ProductoCopia);
                        $(".txtOrdenReferencia").val(item.OrdenReferencia);
                        cantidadOrdenReferencia = 0;
                    }
                    else {

                        var filaNuevaReferencia = '<div class="row ">' +
                            '<div class="col-2 text-center" >#' + String(cuentat3 + 1) + '</div >' +
                            ' <div class="col-5">' +
                            '<input type="text" class="txtOrdenReferencia " onblur="ValidarReferencia(this)" value="' + item.OrdenReferencia + '" /> <span></span>' +
                            '</div>' +
                            ' <div class="col-1">' +
                            ' <button class="btn btn-sm btn-link fuparr fuplist fupgen fupgenpt0" onclick="EliminarOrdenReferencia(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button>' +
                            ' </div>' +
                            '</div >';

                        $(".divContentOrdenReferencia").append(filaNuevaReferencia);
                    }
                    cuentat3 += 1;
                    cantidadOrdenReferencia += 1;
                }
                else {
                    toastr.error("FUP de Referencia no encontrado", "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                }

            });

        },
        error: function () {
            toastr.error("Failed to validate", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }

    });
}

function guardarRecotizacionFUP() {
    if (validarRecotizacionFUP()) {
        mostrarLoad();
        var param = {
            idFup: IdFUPGuardado,
            idVersion: $('#cboVersion').val(),
            observacion: $('#txtObservacionRecotizacion').val(),
            idTipoRecotizacion: $('#cboTipoRecotizacionFup').val()
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/guardarRecotizacionFUP",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                toastr.success("Guardado correctamente");
                obtenerVersionPorIdFup(IdFUPGuardado);
            },
            error: function (e) {
                ocultarLoad();
                toastr.error("Failed guardar recotización", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
}

function validarRecotizacionFUP() {
    var flag = true;
    $('#cboTipoRecotizacionFup').css("border", "");
    $('#txtObservacionRecotizacion').css("border", "");

    if ($('#txtObservacionRecotizacion').val().trim() == "") {
        $('#txtObservacionRecotizacion').css("border", "2px solid crimson");
        flag = false;
    }
    if ($.isNumeric($('#cboTipoRecotizacionFup').val()) == false || $('#cboTipoRecotizacionFup').val() == -1) {
        $('#cboTipoRecotizacionFup').css("border", "2px solid crimson");
        flag = false;
    }
    return flag;
}

function guardarDevComercial() {
    if (validarDevComercial()) {
        mostrarLoad();
        var param = {
            idFup: IdFUPGuardado,
            idVersion: $('#cboVersion').val(),
            TipoRechazo: $('#cboMotivodev').val(),
            Observacion: $('#txtObservaciondevsc').val(),
            estado: EstadoFUP
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/guardarDevComercial",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                toastr.success("Guardado correctamente");
                ValidarEstado();
                obtenerDevComercial();

            },
            error: function (e) {
                ocultarLoad();
                toastr.error("Failed guardar Devolucion Comercial", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
}

function validarDevComercial() {
    var flag = true;
    $('#cboMotivodev').css("border", "");
    $('#txtObservaciondevsc').css("border", "");

    if ($('#txtObservaciondevsc').val().trim() == "") {
        $('#txtObservaciondevsc').css("border", "2px solid crimson");
        flag = false;
    }
    if ($.isNumeric($('#cboMotivodev').val()) == false || $('#cboMotivodev').val() == -1) {
        $('#cboMotivodev').css("border", "2px solid crimson");
        flag = false;
    }
    return flag;
}

function LlenarDevolCom(elem) {
    var rowsDetalleAprobacion = "";
    $.each(elem, function (i, item) {
        rowsDetalleAprobacion = rowsDetalleAprobacion + "<tr><td>" + item.rct_fecha_rechazo.substring(0, 10) + "</td>" +
                            "<td>" + item.tre_descripcion + "</td>" +
                            "<td>" + item.rct_observacion + "</td>" +
                            "<td>" + item.estado + "</td>" +
                            "</tr>";
    });
    if (rowsDetalleAprobacion.trim() == "") {
        rowsDetalleAprobacion = "<tr></tr>";
    }
    $("#tbodyDevolucionsc").html(rowsDetalleAprobacion);
}

function obtenerDevComercial() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $('#cboVersion').val()
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerDevComercial",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            if (msg.d != null) {
                var data = JSON.parse(msg.d);
                LlenarDevolCom(data);
            }
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed obtener Devolucion Comercial", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ocultarCards() {
    $("#EventoPTF").hide();
    $("#ParteAprobacionFUP").hide();
    $("#ParteAnexosFUP").hide();
    $("#ParteSolicitudRecotizacion").hide();
    $("#ParteVentaCierreCotizacion").hide();
    $("#parteSalidaCot").hide();
    $('#txtObservacionRecotizacion').prop("disabled", true);
    $('#btnGuardarRecotizacion').prop("disabled", true);
    $('#cboTipoRecotizacionFup').prop("disabled", true);
    $("#parteSalidaCot").prop("disabled", true);
    $("#PartePlanosTipoForsa").hide();
    $("#ParteFletesFUP").hide();
    $("#ParteFechaSolicitud").hide();
    $("#ParteOrdenFabricacion").hide();
    $("#ParteModuladosCerrados").hide();
    $("#parteProcesoCartaCotizacion").hide();
    $("#ParteControlCambio").hide();
    $("#ParteArmado").hide();
    $(".divarCuotas").hide();
    $(".divarLeasing").hide();
    $(".SoloUpd").hide();
}

function MostrarCards() {
    $("#ParteAlcanceOferta").attr("style", "display:normal");
    $("#ParteAnexosFUP").show();
    $("#ParteSolicitudRecotizacion").show();
    $("#PartePlanosTipoForsa").show();
    //if ((["50%","75%", "100%"].indexOf($("#txtProbabilidad").val()) > -1) && (FecFacturar != '19000101')) {
    //    $("#ParteControlCambio").show();
    //}
    $("#ParteControlCambio").show();
    $("#ParteArmado").show();
    if (RolUsuario != 54) {
        $("#ParteAprobacionFUP").show();
        $("#parteSalidaCot").show();
        $("#ParteVentaCierreCotizacion").show();
        $("#ParteFechaSolicitud").show();
        $("#ParteFletesFUP").show();
        $("#ParteOrdenFabricacion").show();
        $("#ParteModuladosCerrados").show();
    }
}

function guardar_salida_cot_call(tipo) {
    var objg = {};
    var isNew = 1;
    var textoOpcion = "";

    if (tipo == 1) {
        textoOpcion = "Salida Cotizacion";
        $("[data-modelosc]").each(function (index) {
            var prop = $(this).attr("data-modelosc");
            var thisval = $(this).val();
            if (thisval.length == 0) {
                validoSalida = false;
            }
            objg[prop] = thisval;
        });
    }
    else {
        textoOpcion = "Salida Modulacion";
        $("[data-modelomf]").each(function (index) {
            var prop = $(this).attr("data-modelomf");
            var thisval = $(this).val();
            if (thisval.length == 0) {
                validoSalida = false;
            }
            objg[prop] = thisval;
        });
    }

    objg["fupid"] = IdFUPGuardado;
    objg["version"] = $.trim($('#cboVersion').val());
    objg["tipoSalida"] = tipo;


    mostrarLoad();

    var param = { fupsal: objg };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/guardarSalidaCotizacion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            ocultarLoad();
            var data = (msg.d);
            //IdFUPGuardado = data;
            if (data != null && data != '') {
                toastr.success("Guardado " + textoOpcion + " correctamente");
                ValidarEstado();
                $(".fupsalco").hide();
                if (tipo == 1)
                    obtenerAnexosSalidaCotFUP(IdFUPGuardado, $('#cboVersion').val(), 6);
                else {
                    obtenerFechaSolicitud(IdFUPGuardado, $('#cboVersion').val());
                    ObtenerCondicionesPago(IdFUPGuardado, $('#cboVersion').val());
                    ObtenerAvalesFabricacion(IdFUPGuardado, $('#cboVersion').val());
                    obtenerAnexosSalidaCotFUP(IdFUPGuardado, $('#cboVersion').val(), 10);
                }
                obtenerAnexosSalidaCotFUP(IdFUPGuardado, $('#cboVersion').val(), 25);
                //obtenerAnexosSalidaM2Final(IdFUPGuardado, $('#cboVersion').val(), 25);
            }
            else {
                toastr.warning("Error guardando " + textoOpcion);
            }
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed to save FUP", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });

}

function guardar_salida_cot(tipo) {
    var objg = {};
    var isNew = 1;
    var validoSalida = true;

    if (tipo == 1) {
        $("[data-modelosc]").each(function (index) {
            var prop = $(this).attr("data-modelosc");
            var thisval = $(this).val();
            if (thisval.length == 0) {
                validoSalida = false;
            }
            objg[prop] = thisval;
        });
    }
    else {
        $("[data-modelomf]").each(function (index) {
            var prop = $(this).attr("data-modelomf");
            var thisval = $(this).val();
            if (thisval.length == 0) {
                validoSalida = false;
            }
            objg[prop] = thisval;
        });
    }
    if (validoSalida == false) {
        toastr.warning("Falta diligenciar datos");
        return false;
    }

    toastr.options.timeOut = "0";
    toastr.closeButton = true;

    toastr.warning("<br /><br /><button class='btn btn-default' type='button' id='ConfimarSalCot' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
            '¿Esta seguro de guardar los valores de salida? Recuerde que no se pueden modificar los datos ni se puede modificar el estado del Fup',
            {
                closeButton: false,
                allowHtml: true,
                onShown: function (toast) {
                    $("#ConfimarSalCot").click(function () {
                        guardar_salida_cot_call(tipo);
                    });
                }
            });

    toastr.options.timeOut = "5000";
    toastr.closeButton = false;

}

function LlenarSalidaCotizacion(elem) {
    if (elem != undefined || elem != null) {
        $("#txtM2EquipoBase").val(elem.m2_equipo);
        $("#txtValEquipoBase").val(elem.vlr_equipo);
        $("#txtM2Adicionales").val(elem.m2_adicionales);
        $("#txtValAdicionales").val(elem.vlr_adicionales);
        $("#txtDetArqM2SC").val(elem.m2_Detalle_arquitectonico);
        $("#txtDetArqValorSC").val(elem.vlr_Detalle_arquitectonico);
        $("#txtTotalMSC").val(elem.total_m2);
        $("#VlrNiv1m2").val(parseFloat(elem.total_m2).toFixed(2));     //--SolFac
        $("#txtTotalValorSC").val(elem.total_valor);
        $("#VlrNoDcto1Cuenta").val(elem.total_valor);                    //--SolFac
        $("#VlrDcto1Cuenta").val(elem.total_valor);                      //--SolFac            
        $("#txtSistemaTrepanteAccsc").val(elem.m2_sis_seguridad);
        //$("#VlrNoDcto5Cuenta").val(elem.m2_sis_seguridad);
        $("#txtAccSistemaSegSC").val(elem.vlr_sis_seguridad);
        $("#VlrNoDcto5Cuenta").val(elem.vlr_sis_seguridad);             //--SolFac
        $("#VlrDcto5Cuenta").val(elem.vlr_sis_seguridad);               //--SolFac            
        $("#txtAccBasicosSC").val(elem.vlr_accesorios_basico);
        $("#VlrNoDcto2Cuenta").val(elem.vlr_accesorios_basico);         //--SolFac
        $("#VlrDcto2Cuenta").val(elem.vlr_accesorios_basico);           //--SolFac           
        $("#txtAccComplSC").val(elem.vlr_accesorios_complementario);
        $("#VlrNoDcto3Cuenta").val(elem.vlr_accesorios_complementario);    //--SolFac
        $("#VlrDcto3Cuenta").val(elem.vlr_accesorios_complementario);      //--SolFac            
        $("#txtAccOpcionalesSC").val(elem.vlr_accesorios_opcionales);
        //$("#txt").val(elem.vlr_accesorios_opcionales);                //--SolFac

        $("#VlrNoDcto4Cuenta").val(elem.vlr_accesorios_opcionales + elem.vlr_otros_productos);    //--SolFac
        $("#VlrDcto4Cuenta").val(elem.vlr_accesorios_opcionales);      //--SolFac            

        $("#txtOtrosProductoSC").val(elem.vlr_otros_productos);
        $("#txtTotalPropuestaCom").val(elem.total_propuesta_com);
        $("#txtTotalPropuestaComF").val(elem.total_propuesta_com);
        $("#txtContenedor20").val(elem.vlr_Contenedor20);
        $("#txtContenedor40").val(elem.vlr_Contenedor40);
        //$("#txtAlturaFormaleta").val(elem.AlturaFormaleta);

        $("txtNumeroModulacionesSC").val(elem.NumeroModulacionesSC);
        $("txtNumeroCambiosSC").val(elem.NumeroCambiosSC);

        datCierre[0].m2s1 = parseFloat(elem.total_m2);
        datCierre[0].valors1 = parseFloat(elem.total_valor);
        datCierre[1].valors1 = elem.vlr_accesorios_basico;
        datCierre[2].valors1 = elem.vlr_accesorios_complementario;
        datCierre[3].valors1 = elem.vlr_accesorios_opcionales + elem.vlr_otros_productos;
        datCierre[4].valors1 = elem.vlr_sis_seguridad;
    }
    else {
        $("#txtM2EquipoBase").val("0");
        $("#txtValEquipoBase").val("0");
        $("#txtM2Adicionales").val("0");
        $("#txtValAdicionales").val("0");
        $("#txtDetArqM2SC").val("0");
        $("#txtDetArqValorSC").val("0");
        $("#txtTotalMSC").val("0");
        $("#txtTotalValorSC").val("0");
        $("#txtSistemaTrepanteAccsc").val("0");
        $("#txtAccSistemaSegSC").val("0");
        $("#txtAccBasicosSC").val("0");
        $("#txtAccComplSC").val("0");
        $("#txtAccOpcionalesSC").val("0");
        $("#txtAccAdicionalesSC").val("0");
        $("#txtOtrosProductoSC").val("0");
        $("#txtTotalPropuestaCom").val("0");
        $("#txtTotalPropuestaComF").val("0");
        $("#txtContenedor20").val("0");
        $("#txtContenedor40").val("0");
        //$("#txtAlturaFormaleta").val("0");
        $("txtNumeroModulacionesSC").val("0");
        $("txtNumeroCambiosSC").val("0");
    }
}

function LlenarModulafinal(elem) {
    if (elem != undefined || elem != null) {
        $("#txtM2EquipoBasemf").val(elem.m2_equipo);
        $("#txtValEquipoBasemf").val(elem.vlr_equipo);
        $("#txtM2Adicionalesmf").val(elem.m2_adicionales);
        $("#txtValAdicionalesmf").val(elem.vlr_adicionales);
        $("#txtDetArqM2SCmf").val(elem.m2_Detalle_arquitectonico);
        $("#txtDetArqValorSCmf").val(elem.vlr_Detalle_arquitectonico);
        $("#txtTotalM_MF").val(parseFloat(elem.total_m2).toFixed(2));
        $("#txtTotalValorMF").val(elem.total_valor);
        $("#txtSistemaTrepanteAccmf").val(elem.m2_sis_seguridad);
        $("#txtAccSistemaSegmf").val(elem.vlr_sis_seguridad);
        $("#txtAccBasicosmf").val(elem.vlr_accesorios_basico);
        $("#txtAccComplmf").val(elem.vlr_accesorios_complementario);
        $("#txtAccOpcionalesmf").val(elem.vlr_accesorios_opcionales);
        $("#txtAccAdicionalesmf").val(elem.vlr_accesorios_adicionales);
        $("#txtOtrosProductomf").val(elem.vlr_otros_productos);
        $("#txtTotalPropuestaComMF").val(elem.total_propuesta_com);

        $("#VlrNiv1m2B").val(parseFloat(elem.total_m2).toFixed(2));
        $("#VlrNoDcto1CuentaB").val(elem.total_valor);
        $("#VlrNoDcto2CuentaB").val(elem.vlr_accesorios_basico);
        $("#VlrNoDcto3CuentaB").val(elem.vlr_accesorios_complementario);
        $("#VlrNoDcto4CuentaB").val(elem.vlr_accesorios_opcionales);
        //$("#VlrNiv5m2B").val(elem.m2_sis_seguridad);
        $("#VlrNoDcto5CuentaB").val(elem.vlr_sis_seguridad);

        datCierre[0].m2s2 = parseFloat(elem.total_m2);
        datCierre[0].valors2 = parseFloat(elem.total_valor);
        datCierre[1].valors2 = elem.vlr_accesorios_basico;
        datCierre[2].valors2 = elem.vlr_accesorios_complementario;
        datCierre[3].valors2 = elem.vlr_accesorios_opcionales;
        datCierre[4].valors2 = elem.vlr_sis_seguridad;
    }
    else {
        $("#txtM2EquipoBasemf").val("0");
        $("#txtValEquipoBasemf").val("0");
        $("#txtM2Adicionalesmf").val("0");
        $("#txtValAdicionalesmf").val("0");
        $("#txtDetArqM2SCmf").val("0");
        $("#txtDetArqValorSCmf").val("0");
        $("#txtTotalM_MF").val("0");
        $("#txtTotalValorMF").val("0");
        $("#txtSistemaTrepanteAccscmf").val("0");
        $("#txtAccSistemaSegmf").val("0");
        $("#txtAccBasicosmf").val("0");
        $("#txtAccComplmf").val("0");
        $("#txtAccOpcionalesmf").val("0");
        $("#txtAccAdicionalesmf").val("0");
        $("#txtOtrosProductomf").val("0");
        $("#txtTotalPropuestaComMF").val("0");
    }
}


function LlenarPTF(data) {
    var rowsAnexosFup = "";
    var fec1 = "", fec2 = "";
    $.each(data, function (index, elem) {
        fec1 = "";
        fec2 = "";
        if (elem.FechaCierre.substring(0, 10) != "1900-01-01") {
            fec1 = elem.FechaCierre.substring(0, 10);
        }
        if (elem.FechaActa.substring(0, 10) != "1900-01-01") {
            fec2 = elem.FechaActa.substring(0, 10);
        }
        rowsAnexosFup = rowsAnexosFup + "<tr><td>" + elem.Evento + "</td>" +
                    "<td>" + elem.Fecha.substring(0, 10) + "</td>" +
                    "<td>" + fec1 + "</td>" +
                    "<td>" + fec2 + "</td>" +
                    "<td>" + elem.Usuario + "</td>";

        if (RolUsuario != 3)
            rowsAnexosFup = rowsAnexosFup + "<td>" + elem.Responsable + "</td>";
        else
            rowsAnexosFup = rowsAnexosFup + "<td></td>";

        rowsAnexosFup = rowsAnexosFup + "<td>" + elem.Observacion + "</td>" + "<td>" + elem.plano + "</td>";

        if ((elem.vaAnexo == 1) && (elem.Usuario == nomUser))
            rowsAnexosFup = rowsAnexosFup + "<td>" + '<button type="button" class="btn btn-small fa fa-upload" data-toggle="modal" onclick="subirAnexoptf(' + elem.Evento_id + ')"></button>' + "</td></tr>";
        else
            rowsAnexosFup = rowsAnexosFup + +"<td></td></tr>";
    });
    if (rowsAnexosFup.trim() == "") {
        rowsAnexosFup = "<tr></tr>";
    }
    $("#tbodyPtfFup").html(rowsAnexosFup);
}

function obtenerPlanoTipoForsa(idFup, idVersion) {
    var param = { pFupID: idFup, pVersion: idVersion };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerPlanosTipoForsa",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarPTF(data);
        },
        error: function () {
            toastr.error("Failed to load PTF", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function calculardescto(tipo, salida, nivel) {
    var valor = 0;
    var total = 0;
    var totunit = 0;
    var totdesc = 0;
    var valc2 = 0;
    var suf = "";

    // Se toma el valor de la Salida como base  - Salida Cotizacion (1) o Modulacion Final (2)
    if (salida == 1) {
        totunit = datCierre[nivel - 1].valors1;
    }
    else {
        suf = "B";
        if (datCierre[nivel - 1].m2s2 == 0) {
            totunit = datCierre[nivel - 1].valors1;
        }
        else {
            totunit = datCierre[nivel - 1].valors2;
        }
        if (datCierre[nivel - 1].valorc2 == 0) {
            valc2 = totunit * datCierre[nivel - 1].valord1 / 100;
            datCierre[nivel - 1].valorc2 = valc2;
        }

    }

    // Se toma el valor del contro, que se modifico y se calcula el descuento 
    // Tipo (1) el cambio se generó desde el text de Descuento, tipo (2) desde el text de Valor
    if (tipo == 1) {
        totdesc = parseFloat($('#VlrDcto' + nivel + suf).val());
    }
    if (tipo == 2) {

        if (totunit == 0) {
            totdesc = 0;
            totunit = parseFloat($('#VlrDcto' + nivel + 'Cuenta' + suf).val());
        }
        else {
            totdesc = (1 - (parseFloat($('#VlrDcto' + nivel + 'Cuenta' + suf).val()) / parseFloat(totunit))) * 100;
        }

    }

    // Se Calcula el valor final
    valor = totunit * (1 - (parseFloat(totdesc) / 100));

    // Se guarda en el arreglo en memoria para facilidad de calculo
    if (salida == 1) {
        datCierre[nivel - 1].valorc1 = valor;
        datCierre[nivel - 1].valord1 = totdesc;
    }
    else {
        datCierre[nivel - 1].valorc2 = valor;
        datCierre[nivel - 1].valord2 = totdesc;
    }
    $('#VlrDcto' + nivel + suf).val(totdesc.toFixed(2));
    $('#VlrDcto' + nivel + 'Cuenta' + suf).val(valor.toFixed(2));

    datCierre[5].m2s1 = 0;
    datCierre[5].valors1 = 0;
    datCierre[5].valord1 = 0;
    datCierre[5].valorc1 = 0;
    datCierre[5].m2s2 = 0;
    datCierre[5].valors2 = 0;
    datCierre[5].valord2 = 0;
    datCierre[5].valorc2 = 0;

    // Calcula en el TOTAL 
    for (k = 0; k < 5; k++) {
        datCierre[5].m2s1 += datCierre[k].m2s1;
        datCierre[5].valors1 += datCierre[k].valors1;
        datCierre[5].valorc1 += datCierre[k].valorc1;
        datCierre[5].m2s2 += datCierre[k].m2s2;
        datCierre[5].valors2 += datCierre[k].valors2;
        datCierre[5].valorc2 += datCierre[k].valorc2;
    }

    if (salida == 1) {
        $('#VlrTotalm2s1').val(datCierre[5].m2s1.toFixed(2));
        $('#VlrTotalAntesDescto').val(datCierre[5].valors1.toFixed(2));
        totdesc = 0;
        if (datCierre[5].valors1 > 0) {
            totdesc = (1 - (parseFloat(datCierre[5].valorc1) / parseFloat(datCierre[5].valors1))) * 100;
        }
        $('#VlrTotalDescto').val(totdesc.toFixed(2));
        $('#VlrTotalDespuesDescto').val(datCierre[5].valorc1.toFixed(2));
        $("#VlrNiv1m2").val(datCierre[5].m2s1.toFixed(2));
        $("#NumberTotalCierre").val(datCierre[5].valorc1.toFixed(2));
        $("#Numberm2Cerrados").val(datCierre[5].m2s1.toFixed(2));
        if (datCierre[5].valors2 = 0) {
            $("#NumberTotalCierre2").val(datCierre[5].valorc1.toFixed(2));
            $("#NumberTotalCierre3").val(datCierre[5].valorc1.toFixed(2));
        }
        if (totdesc > 10) { $(".CondicionalTotalDescuento").show(); }
        else { $(".CondicionalTotalDescuento").hide(); }

    }
    else {
        $('#VlrTotalm2s2').val(datCierre[5].m2s2.toFixed(2));
        $('#VlrTotalAntesDesctoB').val(datCierre[5].valors2.toFixed(2));
        totdesc = 0;
        if (datCierre[5].valors2 > 0) {
            totdesc = (1 - (parseFloat(datCierre[5].valorc2) / parseFloat(datCierre[5].valors2))) * 100;
        }
        $('#VlrTotalDesctoB').val(totdesc.toFixed(2));
        $('#VlrTotalDespuesDesctoB').val(datCierre[5].valorc2.toFixed(2));
        $("#Numberm2CerradosB").val(datCierre[5].m2s2.toFixed(2));
        $("#NumberTotalCierreB").val(datCierre[5].valorc2.toFixed(2));
        if (datCierre[5].valors2 > 0) {
            $("#NumberTotalCierre2").val(datCierre[5].valorc2.toFixed(2));
            $("#NumberTotalCierre3").val(datCierre[5].valorc2.toFixed(2));
        }
    }
}

function SumarM2() {
    var sum2Txt = $(".sumM2SalidaCot");
    var k;
    var sumaValor = 0;
    for (k = 0; k < sum2Txt.length; k++) {
        var valortxt = $(sum2Txt[k]).val().trim();
        if (valortxt.length > 0) {
            sumaValor = sumaValor + Number(valortxt);
        }
    }
    $("#txtTotalMSC").val(sumaValor);
}

function SumarVal() {
    var sumVTxt = $(".sumValSalidaCot");
    var l;
    var sumaValores = 0;
    for (l = 0; l < sumVTxt.length; l++) {
        var valortxtv = $(sumVTxt[l]).val().trim();
        if (valortxtv.length > 0) {
            sumaValores = sumaValores + Number(valortxtv);
        }
    }
    $("#txtTotalValorSC").val(sumaValores);
}

function SumarValTot() {
    var sumVTxt2 = $(".sumValSalidaCot2");
    var l2;
    var sumaValores2 = 0;
    for (l2 = 0; l2 < sumVTxt2.length; l2++) {
        var valortxtv2 = $(sumVTxt2[l2]).val().trim();
        if (valortxtv2.length > 0) {
            sumaValores2 = sumaValores2 + Number(valortxtv2);
        }
    }
    sumaValores2 = sumaValores2 + Number($("#txtTotalValorSC").val());
    $("#txtTotalPropuestaCom").val(sumaValores2);
    $("#txtTotalPropuestaComF").val(sumaValores2);
}

function SumarTotalesSalidaCot() {
    $(".sumM2SalidaCot").blur(function () {
        SumarM2();
    });

    $(".sumValSalidaCot").blur(function () {
        SumarVal();
        SumarValTot();
    });

    $(".sumValSalidaCot2").blur(function () {
        SumarVal();
        SumarValTot();
    });
}

function SumarM2_mf() {
    var sum2Txt = $(".sumM2SalidaMF");
    var k;
    var sumaValor = 0;
    for (k = 0; k < sum2Txt.length; k++) {
        var valortxt = $(sum2Txt[k]).val().trim();
        if (valortxt.length > 0) {
            sumaValor = sumaValor + Number(valortxt);
        }
    }
    $("#txtTotalM_MF").val(sumaValor);
}

function SumarVal_mf() {
    var sumVTxt = $(".sumValSalidaMF");
    var l;
    var sumaValores = 0;
    for (l = 0; l < sumVTxt.length; l++) {
        var valortxtv = $(sumVTxt[l]).val().trim();
        if (valortxtv.length > 0) {
            sumaValores = sumaValores + Number(valortxtv);
        }
    }
    $("#txtTotalValorMF").val(sumaValores);
}

function SumarValTot_mf() {
    var sumVTxt2 = $(".sumValSalidaMF2");
    var l2;
    var sumaValores2 = 0;
    for (l2 = 0; l2 < sumVTxt2.length; l2++) {
        var valortxtv2 = $(sumVTxt2[l2]).val().trim();
        if (valortxtv2.length > 0) {
            sumaValores2 = sumaValores2 + Number(valortxtv2);
        }
    }
    sumaValores2 = sumaValores2 + Number($("#txtTotalValorMF").val());
    $("#txtTotalPropuestaComMF").val(sumaValores2);
}

function SumarTotalesSalidaMf() {
    $(".sumM2SalidaMF").blur(function () {
        SumarM2_mf();
    });

    $(".sumValSalidaMF").blur(function () {
        SumarVal_mf();
        SumarValTot_mf();
    });

    $(".sumValSalidaMF2").blur(function () {
        SumarVal_mf();
        SumarValTot_mf();
    });
}

function sumarDescVlrCto() {
    sumDescVlrTto();
    var VsumDVTxt = $("#VlrTotalDespuesDescto").val();
    var sumVTxt = $("#VlrTotalAntesDescto").val();
    var l;
    var sumaValores = 0;
    if (VsumDVTxt > 0) {
        if (sumVTxt > 0) {
            sumaValores = parseFloat((1 - (Number(VsumDVTxt) / Number(sumVTxt))) * 100).toFixed(2);
        }
    }

    $("#VlrTotalDescto").val(sumaValores);
}

function sumDescVlrTto() {
    var sumVTxt = $(".sumDescVlrTto");
    var sumVTxtND = $(".sumDescVlrNDTto");

    var l;
    var sumaValores = 0;
    for (l = 0; l < sumVTxt.length; l++) {
        var valortxtv = $(sumVTxt[l]).val().trim();
        if (valortxtv.length > 0) {
            sumaValores = sumaValores + Number(valortxtv);
        }
    }
    $("#VlrTotalDespuesDescto").val(parseFloat(sumaValores).toFixed(2));
    $("#NumberTotalCierre").val(parseFloat(sumaValores).toFixed(2));
    $("#NumberTotalCierre2").val(parseFloat(sumaValores).toFixed(2));
    $("#NumberTotalCierre3").val(parseFloat(sumaValores).toFixed(2));
    sumaValores = 0;
    for (l = 0; l < sumVTxtND.length; l++) {
        var valortxtv = $(sumVTxtND[l]).val().trim();
        if (valortxtv.length > 0) {
            sumaValores = sumaValores + Number(valortxtv);
        }
    }
    $("#VlrTotalAntesDescto").val(parseFloat(sumaValores).toFixed(2));

}

function desc1(tipo) {
    var ValConDesc = 0;
    var valOrigen = ($("#VlrNoDcto1Cuenta").val());
    var valDcto = ($("#VlrDcto1").val());
    var valDestino = $("#VlrDcto1Cuenta").val();

    if (tipo == 1 && (valDcto != undefined && valDcto != "")) {
        ValConDesc = valOrigen * (1 - (valDcto / 100));
        $("#VlrDcto1Cuenta").val(parseFloat(Math.trunc(ValConDesc)).toFixed(2));
    }
    if (tipo == 2 && valOrigen != valDestino) {
        ValConDesc = calcDesc(valDestino, valOrigen);
        $("#VlrDcto1").val(parseFloat(ValConDesc).toFixed(2))
    }
}
function desc1B(tipo) {
    var ValConDesc = 0;
    var valOrigen = ($("#VlrNoDcto1CuentaB").val());
    var valDcto = ($("#VlrDcto1B").val());
    var valDestino = $("#VlrDcto1CuentaB").val();

    if (tipo == 1 && (valDcto != undefined && valDcto != "")) {
        ValConDesc = valOrigen * (1 - (valDcto / 100));
        $("#VlrDcto1Cuenta").val(parseFloat(Math.trunc(ValConDesc)).toFixed(2));
    }
    if (tipo == 2 && valOrigen != valDestino) {
        ValConDesc = calcDesc(valDestino, valOrigen);
        $("#VlrDcto1B").val(parseFloat(ValConDesc).toFixed(2))
    }
}

function desc2(tipo) {
    var ValConDesc = 0;
    var valOrigen = ($("#VlrNoDcto2Cuenta").val());
    var valDcto = ($("#VlrDcto2").val());
    var valDestino = $("#VlrDcto2Cuenta").val();

    if (tipo == 1 && (valDcto != undefined && valDcto != "")) {
        ValConDesc = valOrigen * (1 - (valDcto / 100));
        $("#VlrDcto2Cuenta").val(parseFloat(Math.trunc(ValConDesc)).toFixed(2));
    }
    if (tipo == 2 && valOrigen != valDestino) {
        ValConDesc = calcDesc(valDestino, valOrigen);
        $("#VlrDcto2").val(parseFloat(ValConDesc).toFixed(2))
    }

}

function desc3(tipo) {
    var ValConDesc = 0;
    var valOrigen = ($("#VlrNoDcto3Cuenta").val());
    var valDcto = ($("#VlrDcto3").val());
    var valDestino = $("#VlrDcto3Cuenta").val();

    if (tipo == 1 && (valDcto != undefined && valDcto != "")) {
        ValConDesc = valOrigen * (1 - (valDcto / 100));
        $("#VlrDcto3Cuenta").val(parseFloat(Math.trunc(ValConDesc)).toFixed(2));
    }
    if (tipo == 2 && valOrigen != valDestino) {
        ValConDesc = calcDesc(valDestino, valOrigen);
        $("#VlrDcto3").val(parseFloat(ValConDesc).toFixed(2))
    }
}

function desc4(tipo) {
    var ValConDesc = 0;
    var valOrigen = ($("#VlrNoDcto4Cuenta").val());
    var valDcto = ($("#VlrDcto4").val());
    var valDestino = $("#VlrDcto4Cuenta").val();

    if (tipo == 1 && (valDcto != undefined && valDcto != "")) {
        ValConDesc = valOrigen * (1 - (valDcto / 100));
        $("#VlrDcto4Cuenta").val(parseFloat(Math.trunc(ValConDesc)).toFixed(2));
    }
    if (tipo == 2 && valOrigen != valDestino) {
        ValConDesc = calcDesc(valDestino, valOrigen);
        $("#VlrDcto4").val(parseFloat(ValConDesc).toFixed(2))
    }
}

function desc5(tipo) {
    var ValConDesc = 0;
    var valOrigen = ($("#VlrNoDcto5Cuenta").val());
    var valDcto = ($("#VlrDcto5").val());
    var valDestino = $("#VlrDcto5Cuenta").val();

    if (tipo == 1 && (valDcto != undefined && valDcto != "")) {
        ValConDesc = valOrigen * (1 - (valDcto / 100));
        $("#VlrDcto5Cuenta").val(parseFloat(Math.trunc(ValConDesc)).toFixed(2));
    }
    if (tipo == 2 && valOrigen != valDestino) {
        ValConDesc = calcDesc(valDestino, valOrigen);
        $("#VlrDcto5").val(parseFloat(ValConDesc).toFixed(2))
    }
}

function evalDescto() {
    var valDcto = ($("#VlrTotalDescto").val());
    if (valDcto > 10) { $(".CondicionalTotalDescuento").show() }
    else
    {
        $(".CondicionalTotalDescuento").hide()
    }
}

function evalDesctoenCargue(tipo) {
    calculardescto(2, tipo, 1);
    calculardescto(2, tipo, 2);
    calculardescto(2, tipo, 3);
    calculardescto(2, tipo, 4);
    calculardescto(2, tipo, 5);
    evalDescto();
}

function TotalesCondicionesPago() {
    $(".txtValorCuota").blur(function () {
        SumarValorCuotas();
    });

    $(".txtValorLeasing").blur(function () {
        SumarValorLeasing();
    });
}

function SumarValorCuotas() {
    var sumVTxt2 = $(".txtValorCuota");
    var k;
    var sumaValor = 0;
    for (k = 0; k < sumVTxt2.length; k++) {
        var valortxt = $(sumVTxt2[k]).val().trim();
        if (valortxt.length > 0) {
            sumaValor = sumaValor + Number(valortxt);
        }
    }

    $("#txTotalCuotas").val(parseFloat(sumaValor).toFixed(2));
}

function SumarValorLeasing() {
    var sumVTxt2 = $(".txtValorLeasing");
    var k;
    var sumaValor = 0;
    for (k = 0; k < sumVTxt2.length; k++) {
        var valortxt = $(sumVTxt2[k]).val().trim();
        if (valortxt.length > 0) {
            sumaValor = sumaValor + Number(valortxt);
        }
    }
    $("#txTotalLeasing").val(parseFloat(sumaValor).toFixed(2));
}

function llenarListaRecotizacion(elem) {
    var rowsAnexosFup = "";
    $.each(elem, function (index, data) {
        rowsAnexosFup = rowsAnexosFup + "<tr><td>" + data.Fecha + "</td>" +
            "<td>" + data.Version + "</td>" +
            "<td>" + data.Motivo + "</td>" +
            "<td>" + data.Observacion + "</td>" +
            "<td></td>" +
            "</tr>";
    });
    if (rowsAnexosFup.trim() == "") {
        rowsAnexosFup = "<tr></tr>";
    }
    $("#tbodyRecotizacionFup").html(rowsAnexosFup);
}

function calcDesc(Valor1, Valor2) {
    Valor1 = (Valor1 == undefined || Valor1 == "" ? 0 : Valor1)
    Valor2 = (Valor2 == undefined || Valor2 == "" ? 0 : Valor2)
    if (Valor2 == 0)
        return 0
    else
        return ((1 - (Valor1 / Valor2)) * 100)
}

function GuardarFechaSolicitudV2() {
    var objItem = {
    };
    objItem["IdFUP"] = IdFUPGuardado;
    objItem["Version"] = $('#cboVersion').val();
    objItem["FechaFirmaContrato"] = $('#dtfirmaContrato').val();
    objItem["FechaContractual"] = $('#dtfechacontractual').val();
    objItem["FechaFormalizaPago"] = $('#dtFormalizaPago').val();
    objItem["Plazo"] = $('#dtPlazo').val();
    objItem["vlr_dcto_n1"] = $("#VlrDcto1Cuenta").val(); // calcDesc($("#VlrDcto1Cuenta").val(), $("#VlrNoDcto1Cuenta").val());
    objItem["vlr_dcto_n2"] = $("#VlrDcto2Cuenta").val(); // calcDesc($("#VlrDcto2Cuenta").val(), $("#VlrNoDcto2Cuenta").val());
    objItem["vlr_dcto_n3"] = $("#VlrDcto3Cuenta").val(); // calcDesc($("#VlrDcto3Cuenta").val(), $("#VlrNoDcto3Cuenta").val());
    objItem["vlr_dcto_n4"] = $("#VlrDcto4Cuenta").val(); // calcDesc($("#VlrDcto4Cuenta").val(), $("#VlrNoDcto4Cuenta").val());
    objItem["vlr_dcto_n5"] = $("#VlrDcto5Cuenta").val(); // calcDesc($("#VlrDcto5Cuenta").val(), $("#VlrNoDcto5Cuenta").val());
    objItem["ObservaMayordscto"] = $('#ObservaMayorDcto').val();
    objItem["FechaAprobacionPlanos"] = $('#dtfechaAprobacionPlanos').val();
    objItem["FechaPactadaPlan"] = $('#dtfechaPactadaPlan').val();
    objItem["ObservaFechas"] = $('#ObservaFechas').val();
    objItem["TerminoNeg"] = parseInt($('#selectTerminoNegociacion3').val());
    objItem["vlr_dcto_mf_n1"] = $("#VlrDcto1CuentaB").val(); // calcDesc($("#VlrDcto1Cuenta").val(), $("#VlrNoDcto1Cuenta").val());
    objItem["vlr_dcto_mf_n2"] = $("#VlrDcto2CuentaB").val(); // calcDesc($("#VlrDcto2Cuenta").val(), $("#VlrNoDcto2Cuenta").val());
    objItem["vlr_dcto_mf_n3"] = $("#VlrDcto3CuentaB").val(); // calcDesc($("#VlrDcto3Cuenta").val(), $("#VlrNoDcto3Cuenta").val());
    objItem["vlr_dcto_mf_n4"] = $("#VlrDcto4CuentaB").val(); // calcDesc($("#VlrDcto4Cuenta").val(), $("#VlrNoDcto4Cuenta").val());
    objItem["vlr_dcto_mf_n5"] = $("#VlrDcto5CuentaB").val(); // calcDesc($("#VlrDcto5Cuenta").val(), $("#VlrNoDcto5Cuenta").val());
    objItem["ObservaDscto"] = $('#ObservaDesc').val();


    var param = {
        item: objItem
    };

    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardarFechaSolicitudV2",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //var nuevaVersionFup = JSON.parse(msg.d);
            ocultarLoad();
            toastr.success("Guardado correctamente");
            //GuardarCondicionPago();
            ValidarEstado();
            obtenerFechaSolicitud(IdFUPGuardado, $('#cboVersion').val());
            ObtenerCondicionesPago(IdFUPGuardado, $('#cboVersion').val());
            ObtenerAvalesFabricacion(IdFUPGuardado, $('#cboVersion').val());
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Guardar Fecha Solicitud V2", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
    ocultarLoad();
}

function GuardarCondicionPago() {
    var pFupID = IdFUPGuardado;
    var pVersion = $('#cboVersion').val();
    var param;
    var datos_tablas = [];
    var condicionPago = 0;

    var xlista = 0;
    condicionPago = $("#cboCondicionesPago").val();

    if (condicionPago == 2) {
        if (parseFloat($("#txTotalCuotas").val()) != parseFloat($("#NumberTotalCierre2").val())) {
            toastr.error("TOTAL CUOTAS debe ser igual a VLR TOTAL CIERRE", "CUOTAS CREDITO", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
            return;
        }
        $("#tbodyCondicionesCuotas").find("tr").each(function (e, r) {
            xlista = xlista + 1
            var obj_tabla = {
                TipoPago: condicionPago,
                Consecutivo: xlista,
                Fecha: $(r).find('.dtFechaCuota').val(),
                Condicion: $(r).find('.txLeasing').val(),
                Valor: $(r).find('.txtValorCuota').val()
            };
            datos_tablas.push(obj_tabla);
        });
    }
        //else if (condicionPago == 3) {
    else {
        if (parseFloat($("#txTotalLeasing").val()) != parseFloat($("#NumberTotalCierre3").val()).toFixed(2)) {
            if (condicionPago == 3) {
                toastr.error("TOTAL LEASING debe ser igual a VLR TOTAL DE CIERRE", "CUOTAS LEASING", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                })
            } if (condicionPago == 4) {
                toastr.error("TOTAL CARTA DE CRÉDITO debe ser igual a VLR TOTAL DE CIERRE", "CUOTAS CARTA DE CRÉDITO", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                })
            } else {
                toastr.error("TOTAL CONTADO debe ser igual a VLR TOTAL DE CIERRE", "CUOTAS CONTADO", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                })
            };
            return;
        }
        $("#tbodyCondicionesLeasing").find("tr").each(function (e, r) {
            xlista = xlista + 1
            var valorX = $(r).find('.txtValorLeasing').val();
            var obj_tabla = {
                TipoPago: condicionPago,
                Consecutivo: xlista,
                Fecha: $(r).find('.dtFechaCuota').val(),
                Condicion: $(r).find('.txLeasing').val(),
                Valor: $(r).find('.txtValorLeasing').val()
            };
            datos_tablas.push(obj_tabla);
        });
    }
    //else {
    //    var obj_tabla = {
    //        TipoPago: condicionPago,
    //        Consecutivo: xlista,
    //        Fecha: "1900/01/01",
    //        Condicion: "",
    //        Valor: 0
    //    };
    //    datos_tablas.push(obj_tabla);
    //}
    param = {
        pFupId: pFupID,
        pidVersion: pVersion,
        ListaCondiciones: datos_tablas
    };
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardarCondicion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //var nuevaVersionFup = JSON.parse(msg.d);
            ocultarLoad();
            toastr.success("Guardado correctamente");
            ValCondicionPago();
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Guardar Condicion Pago", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
    ocultarLoad();
}

function GuardarAvalesFabricacion() {
    var objItem = {
    };
    var cboSinoJuridico = "1";
    var cboSiNoTesoreria = "1";
    var cboSiNoGerencia = "1";
    var cboSiNoVice = "1";
    var cboSinoJuridicoB = "1";
    var cboSiNoTesoreriaB = "1";
    var cboSiNoGerenciaB = "1";
    var cboSiNoViceB = "1";
    if ($('#SiNoJuridico').val() != null) {
        cboSinoJuridico = $('#SiNoJuridico').val();
    }
    if ($('#SiNoTesoreria').val() != null) {
        cboSiNoTesoreria = $('#SiNoTesoreria').val();
    }
    if ($('#SiNoGerencia').val() != null) {
        cboSiNoGerencia = $('#SiNoGerencia').val();
    }
    if ($('#SiNoVice').val() != null) {
        cboSiNoVice = $('#SiNoVice').val();
    }
    if ($('#SiNoJuridicoB').val() != null) {
        cboSinoJuridicoB = $('#SiNoJuridicoB').val();
    }
    if ($('#SiNoTesoreriaB').val() != null) {
        cboSiNoTesoreriaB = $('#SiNoTesoreriaB').val();
    }
    if ($('#SiNoGerenciaB').val() != null) {
        cboSiNoGerenciaB = $('#SiNoGerenciaB').val();
    }
    if ($('#SiNoViceB').val() != null) {
        cboSiNoViceB = $('#SiNoViceB').val();
    }
    if ($('#dtFormalizaPagoAproba').val() == null) {
        $('#dtFormalizaPagoAproba').val('1900/01/01');
    }
    if ($('#dtFirmaContratoAproba').val() == null) {
        $('#dtFirmaContratoAproba').val('1900/01/01');
    }
    objItem["IdFUP"] = IdFUPGuardado;
    objItem["Version"] = $('#cboVersion').val();
    objItem["AutorizaJuridico"] = cboSinoJuridico;
    objItem["AutorizaJuridicoDesp"] = cboSinoJuridicoB;
    objItem["AutorizaTesoreria"] = cboSiNoTesoreria;
    objItem["AutorizaTesoreria2"] = cboSiNoTesoreriaB;
    objItem["AutorizaGercom"] = cboSiNoGerencia;
    objItem["AutorizaGercomDesp"] = cboSiNoGerenciaB;
    objItem["AutorizaVicecom"] = cboSiNoVice;
    objItem["AutorizaVicecomDesp"] = cboSiNoViceB;
    objItem["AutorizaJuridico_Observacion"] = $('#ObservaJuridico').val();
    objItem["AutorizaJuridico_ObservacionDesp"] = $('#ObservaJuridicoB').val();
    objItem["AutorizaTesoreria_Observacion"] = $('#ObservaTesoreria').val();
    objItem["AutorizaTesoreria2_Observacion"] = $('#ObservaTesoreriaB').val();
    objItem["AutorizaGercom_Observacion"] = $('#ObservaGerencia').val();
    objItem["AutorizaGercom_ObservacionDesp"] = $('#ObservaGerenciaB').val();
    objItem["AutorizaVicecom_Observacion"] = $('#ObservaVice').val();
    objItem["AutorizaVicecom_ObservacionDesp"] = $('#ObservaViceB').val();
    objItem["cmJuridico"] = $('#cmJuridicoA').val();
    objItem["cmJuridico2"] = $('#cmJuridicoB').val();
    objItem["cmTesoreria"] = $('#cmTesoreriaA').val();
    objItem["cmTesoreria2"] = $('#cmTesoreriaB').val();
    objItem["cmGercom"] = $('#cmGerenciaA').val();
    objItem["cmGercom2"] = $('#cmGerenciaB').val();
    objItem["cmVicecom"] = $('#cmViceA').val();
    objItem["cmVicecom2"] = $('#cmViceB').val();
    //Nuevos campos que van a los viejos
    objItem["firmaContratoAproba"] = $('#dtfirmaContratoAproba').val();
    objItem["FormalizaPagoAproba"] = $('#dtFormalizaPagoAproba').val();

    var param = {
        item: objItem
    };
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardarAvalesFabricacion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //var nuevaVersionFup = JSON.parse(msg.d);
            ocultarLoad();
            toastr.success("Avales de Fabricacion Guardados correctamente");
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Guardar Avales de Fabricacion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
    ocultarLoad();
}

function cargarcombosAvalesdefault() {
    $("#SiNoJuridico").val("1");
    $("#SiNoTesoreria").val("1");
    $("#SiNoGerencia").val("1");
    $("#SiNoVice").val("1");
    $("#SiNoJuridicoB").val("1");
    $("#SiNoTesoreriaB").val("1");
    $("#SiNoGerenciaB").val("1");
    $("#SiNoViceB").val("1");
}

function reactioncombosAvalesdefault() {
    //$("#SiNoJuridico").blur(function () {
    if ($('#SiNoJuridico').val() != null) {
        if ($('#SiNoJuridico').val() == 2) {
            //$('#txtSiNoJuridico')
        } else {
            $('#dtfirmaContratoAproba').val('1900/01/01')
        };
        $('#cmJuridicoA').val(1);
    }
    //});
    //$("#SiNoTesoreria").blur(function () {
    if ($('#SiNoTesoreria').val() != null) {
        if ($('#SiNoTesoreria').val() == 2) {
            $('#txtSiNoTesoreria').style.color = "red";
        } else {
            $('#dtFormalizaPagoAproba').val('1900/01/01')
        };
        $('#cmTesoreriaA').val(1);
    }
    //});

}

function LlenarFechasSol(data) {
    LimpiarDescuentosCierre()
    var fec1 = "", fec2 = "", fec3 = "", fec4 = "", fec5 = "", fec11 = "", fec31 = "", fec41 = "";
    $.each(data, function (index, elem) {
        if (elem.FechaFirmaContrato.substring(0, 10) != "1900-01-01") {
            fec1 = (elem.FechaFirmaContrato);
            fec1 = fec1.substring(0, 10);
            fec11 = (elem.FechaFirmaContrato);
            fec11 = fec1.substring(0, 10);
        }
        if (elem.FechaContractual.substring(0, 10) != "1900-01-01") {
            fec2 = (elem.FechaContractual);
            fec2 = fec2.substring(0, 10);
            //fec21 = (elem.FechaContractual);
            //fec21 = fec21.substring(0, 10);
        }
        if (elem.FechaAnticipado.substring(0, 10) != "1900-01-01") {
            fec3 = (elem.FechaAnticipado);
            fec3 = fec3.substring(0, 10);
            fec31 = (elem.FechaAnticipado);
            fec31 = fec31.substring(0, 10);
        }
        if (elem.FechaAprobacionPlanos.substring(0, 10) != "1900-01-01") {
            fec4 = (elem.FechaAprobacionPlanos);
            fec4 = fec4.substring(0, 10);
            fec41 = (elem.FechaAprobacionPlanos);
            fec41 = fec41.substring(0, 10);
        }
        if (elem.FechaPactadaPlan.substring(0, 10) != "1900-01-01") {
            fec5 = (elem.FechaPactadaPlan);
            fec5 = fec5.substring(0, 10);
        }
        $("#dtfirmaContrato").val(fec1);
        $("#dtfechacontractual").val(fec2);
        $("#dtFormalizaPago").val(fec3);
        $("#dtPlazo").val(elem.Plazo);
        $('#dtfirmaContratoAproba').val(fec11);
        $('#dtFormalizaPagoAproba').val(fec31);
        $('#dtFechaAval').val(fec41);
        $("#NumberDiasistecnica").val(elem.DiasAsistec);
        $("#NumberDiasconsumidos").val(elem.DiasConsumidos);
        $("#Numberm2Cerrados").val(elem.m2Cerrados);
        $("#NumberTotalCierre").val(elem.ValorFinal);

        $("#NumberTotalFacturacion").val(elem.valorFacturacion.toFixed(2));
        $("#Numberm2Facturados").val(elem.m2Modulados.toFixed(2));
        $("#vrTotalModulado").val(elem.valorFactModulado.toFixed(2));
        $("#VlrDcto1Cuenta").val(elem.vlr_dcto_n1.toFixed(2));
        $("#VlrDcto2Cuenta").val(elem.vlr_dcto_n2.toFixed(2));
        $("#VlrDcto3Cuenta").val(elem.vlr_dcto_n3.toFixed(2));
        $("#VlrDcto4Cuenta").val(elem.vlr_dcto_n4.toFixed(2));
        $("#VlrDcto5Cuenta").val(elem.vlr_dcto_n5.toFixed(2));
        $('#SiNoJuridico').val(elem.AutorizaJuridico);
        $("#ObservaJuridico").val(elem.AutorizaJuridico_Observacion);
        $('#SiNoTesoreria').val(elem.AutorizaTesoreria);
        $("#ObservaTesoreria").val(elem.AutorizaTesoreria_Observacion);
        $("#SiNoGerencia").val(elem.AutorizaGercom);
        $("#ObservaGerencia").val(elem.AutorizaGercom_Observacion);
        $("#SiNoVice").val(elem.AutorizaVicecom);
        $("#ObservaVice").val(elem.AutorizaVicecom_Observacion);

        $('#SiNoJuridicoB').val(elem.AutorizaJuridicoDesp);
        $("#ObservaJuridicoB").val(elem.AutorizaJuridicoDesp_Observacion);
        $('#SiNoTesoreriaB').val(elem.AutorizaTesoreriaDesp);
        $("#ObservaTesoreriaB").val(elem.AutorizaTesoreriaDesp_Observacion);
        $("#SiNoGerenciaB").val(elem.AutorizaGercomDesp);
        $("#ObservaGerenciaB").val(elem.AutorizaGercomDesp_Observacion);
        $("#SiNoViceB").val(elem.AutorizaVicecomDesp);
        $("#ObservaViceB").val(elem.AutorizaVicecomDesp_Observacion);

        $('#cboCondicionesPago').val(elem.MetodoPago).change();
        $('#ObservaMayorDcto').val(elem.ObservaMayordscto);
        $("#dtfechaAprobacionPlanos").val(fec4);
        $("#dtfechaPactadaPlan").val(fec5);
        $('#ObservaFechas').val(elem.ObservaFechas);
        $('#ObservaDesc').val(elem.ObservaDscto);
        $("#VlrDcto1CuentaB").val(elem.vlr_dcto_mf_n1.toFixed(2));
        $("#VlrDcto2CuentaB").val(elem.vlr_dcto_mf_n2.toFixed(2));
        $("#VlrDcto3CuentaB").val(elem.vlr_dcto_mf_n3.toFixed(2));
        $("#VlrDcto4CuentaB").val(elem.vlr_dcto_mf_n4.toFixed(2));
        $("#VlrDcto5CuentaB").val(elem.vlr_dcto_mf_n5.toFixed(2));

        var s2 = 0
        s2 = parseFloat(elem.vlr_dcto_mf_n1 + elem.vlr_dcto_mf_n2 + elem.vlr_dcto_mf_n3 + elem.vlr_dcto_mf_n4 + elem.vlr_dcto_mf_n5);
        if (s2 > 0) {
            $("#NumberTotalCierre2").val(s2.toFixed(2));
            $("#NumberTotalCierre3").val(s2.toFixed(2));
        }
        else {
            $("#NumberTotalCierre2").val(elem.ValorFinal);
            $("#NumberTotalCierre3").val(elem.ValorFinal);
        }

        var calPorc = 0;
        var calPorc1 = 0;

        if (parseFloat(elem.m2Cerrados) > 0) {
            calPorc = parseFloat(elem.vlr_dcto_n1) / parseFloat(elem.m2Cerrados);
            calPorc1 = (parseFloat(elem.vlr_dcto_n1) + parseFloat(elem.vlr_dcto_n2) + parseFloat(elem.vlr_dcto_n3)) / parseFloat(elem.m2Cerrados);
        }
        $("#Numberm2N1A").val(calPorc.toFixed(2));
        $("#Numberm2N13A").val(calPorc1.toFixed(2));

        calPorc = 0;
        calPorc1 = 0;
        if (parseFloat(elem.m2Modulados) > 0) {
            calPorc = parseFloat(elem.vlr_dcto_mf_n1) / parseFloat(elem.m2Modulados);
            calPorc1 = (parseFloat(elem.vlr_dcto_mf_n1) + parseFloat(elem.vlr_dcto_mf_n2) + parseFloat(elem.vlr_dcto_mf_n3)) / parseFloat(elem.m2Modulados);
        } else {
            if (parseFloat(elem.m2Cerrados) > 0) {
                calPorc = parseFloat(elem.vlr_dcto_mf_n1) / parseFloat(elem.m2Cerrados);
                calPorc1 = (parseFloat(elem.vlr_dcto_mf_n1) + parseFloat(elem.vlr_dcto_mf_n2) + parseFloat(elem.vlr_dcto_mf_n3)) / parseFloat(elem.m2Cerrados);
            }

        }
        //De: Maira Gomez <mairagomez@forsa.net.co>
        //Enviado el: jueves, 5 de mayo de 2022 10:39 a. m.
        //Realizar el cálculo de los campos  Vlr M2 -NI Final y Vlr M2 N1 + N2 +N3 Final, 
        //con los M2 de la salida de cotización, 
        //UNICAMENTE cuando el campo de Modulados finales este vacío y se haya diligenciado los valores en Cierre Final:
        //if ($("#VlrNiv1m2B").val() == "") {
        $("#Numberm2N1B").val(calPorc.toFixed(2));
        $("#Numberm2N13B").val(calPorc1.toFixed(2));
        //} 

        datCierre[0].m2s1 = elem.m2Cerrados;
        datCierre[0].valorc1 = elem.vlr_dcto_n1;
        datCierre[1].valorc1 = elem.vlr_dcto_n2;
        datCierre[2].valorc1 = elem.vlr_dcto_n3;
        datCierre[3].valorc1 = elem.vlr_dcto_n4;
        datCierre[4].valorc1 = elem.vlr_dcto_n5;
        datCierre[0].m2s2 = elem.m2Modulados;
        datCierre[0].valorc2 = elem.vlr_dcto_mf_n1;
        datCierre[1].valorc2 = elem.vlr_dcto_mf_n2;
        datCierre[2].valorc2 = elem.vlr_dcto_mf_n3;
        datCierre[3].valorc2 = elem.vlr_dcto_mf_n4;
        datCierre[4].valorc2 = elem.vlr_dcto_mf_n5;

        evalDesctoenCargue(1);
        evalDesctoenCargue(2);
    });
}

function obtenerFechaSolicitud(idFup, idVersion) {
    IdFUPGuardado = idFup;

    var param = {
        pFupID: idFup, pVersion: idVersion
    };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerFechaSolicitud",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarFechasSol(data);
        },
        error: function () {
            toastr.error("Failed to load Fechas Aprobación " + idFup + " - " + idVersion, "FUP " + msg, {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function LlenarAvalesFabricacion(data) {
    $("#tbodytab_Avales").children().remove();
    var rowsAvales = "";
    $.each(data, function (index, elem) {
        rowsAvales = rowsAvales + "<tr><td>" + elem.Tipo + "</td>" +
                    "<td>" + elem.Condicion + "</td>" +
                    "<td>" + elem.Fecha + "</td>" +
                    "<td>" + elem.Observaciones + "</a></td>" +
                    "<td>" + elem.Usuario + "</td>" +
                    "</tr>";
    });
    if (rowsAvales.trim() == "") {
        rowsAvales = "<tr></tr>";
    }
    $("#tbodytab_Avales").html(rowsAvales);

    //$.each(data, function (index, elem) {
    //	$('#SiNoJuridico').val(elem.AutorizaJuridico);
    //	$("#ObservaJurídico").val(elem.AutorizaJuridico_Observacion);
    //	$('#SiNoTesoreria').val(elem.AutorizaTesoreria);
    //	$("#ObservaTesoreria").val(elem.AutorizaTesoreria_Observacion);
    //	$("#SiNoAnticipo").val(elem.AutorizaTesoreria2);
    //	$("#ObservaAnticipo").val(elem.AutorizaTesoreria2_Observacion);
    //	$("#SiNoGerencia").val(elem.AutorizaGercom);
    //	$("#ObservaGerencia").val(elem.AutorizaGercom_Observacion);
    //	$("#SiNoVice").val(elem.AutorizaVicecom);
    //	$("#ObservaVice").val(elem.AutorizaVicecom_Observacion);
    //});
}

function LlenarCondicionesPago(elem) {
    $("#tbodyCondicionesCuotas").html("");
    $('#tbodyCondicionesLeasing').html("");
    cantidadOrdenCuota = 0;
    cantidadLeasing = 0;
    var fec1 = "";
    cumplecondicionPago = false;
    $.each(elem, function (i, elem) {
        condicionPago = elem.TipoPago;
        if (elem.Fecha.substring(0, 10) != "1900-01-01") {
            fec1 = (elem.Fecha);
            fec1 = fec1.substring(0, 10);
        }
        fec1 = moment(elem.Fecha).format('YYYY-MM-DD');
        if (condicionPago == 2) {
            cantidadOrdenCuota += 1;
            td = '<tr> ' +
                ' <td > ' + elem.Consecutivo + '</td> ' +
                ' <td class="text-center" ><input class="dtFechaCuota" type="date" value = "' + fec1 + '"/></td> ' +
                ' <td ><input type="number" min="0" class="txtValorCuota NumeroSalcot" onblur ="SumarValorCuotas()" value = "' + elem.Valor + '"/></td> ' +
                ' <td class="text-center" ><input class="txLeasing" type="text" value = "' + elem.Condicion + '"/></td> ' +
                ' <td ><button class="btn btn-sm btn-link " onclick="EliminarOrdenCuota(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button></td> ' +
                ' </tr> '
            $('#tbodyCondicionesCuotas').append(td);
            SumarValorCuotas();
        }
        if (condicionPago == 3) {
            cantidadLeasing += 1;
            td = '<tr> ' +
                ' <td > ' + elem.Consecutivo + '</td> ' +
                ' <td class="text-center" ><input class="dtFechaCuota" type="date" value = "' + fec1 + '"/></td> ' +
                ' <td ><input type="number" min="0" class="txtValorLeasing NumeroSalcot" onblur ="SumarValorLeasing()" value = "' + elem.Valor + '"/></td> ' +
                ' <td class="text-center" ><input class="txLeasing" type="text" value = "' + elem.Condicion + '"/></td> ' +
                ' <td ><button class="btn btn-sm btn-link " onclick="EliminarOrdenLeasing(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button></td> ' +
                ' </tr> '
            $('#tbodyCondicionesLeasing').append(td);
            SumarValorLeasing();
        }
        if (condicionPago == 1 || condicionPago == 4) {
            cantidadLeasing += 1;
            td = '<tr> ' +
                ' <td > ' + elem.Consecutivo + '</td> ' +
                ' <td class="text-center" ><input class="dtFechaCuota" type="date" value = "' + fec1 + '"/></td> ' +
                ' <td ><input type="number" min="0" class="txtValorLeasing NumeroSalcot" onblur ="SumarValorLeasing()" value = "' + elem.Valor + '"/></td> ' +
                ' <td class="text-center" ><input class="txLeasing" type="text" value = "' + elem.Condicion + '"/></td> ' +
                ' <td ><button class="btn btn-sm btn-link " onclick="EliminarOrdenLeasing(this)" > <i class="fa fa-minus-square" style="font-size:14px;color:red"></i> </button></td> ' +
                ' </tr> '
            $('#tbodyCondicionesLeasing').append(td);
            SumarValorLeasing();
            //cumplecondicionPago = true;
        }
    });
}

function ObtenerCondicionesPago(idFup, idVersion) {
    IdFUPGuardado = idFup;

    var param = {
        pFupID: idFup, pVersion: idVersion
    };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerCondicionesPago",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarCondicionesPago(data);
        },
        error: function () {
            toastr.error("Failed to load Fechas Aprobación " + idFup + " - " + idVersion, "FUP " + msg, {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}
function ObtenerAvalesFabricacion(idFup, idVersion) {
    IdFUPGuardado = idFup;
    var param = {
        pFupID: idFup, pVersion: idVersion
    };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerAvalesFabricacion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarAvalesFabricacion(data);
        },
        error: function () {
            toastr.error("Failed to load Avales Fabricación " + idFup + " - " + idVersion, "FUP " + msg, {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ActualizarEstado(pEvento) {

    if (pEvento == 2 && EstadoFUP == "Pre-Cierre")
        pEvento = 8;

    var param = {
        idFup: IdFUPGuardado,
        idVersion: $('#cboVersion').val(),
        Estado: EstadoFUP,
        pEvento: pEvento
    };

    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ActualizarEstado",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //var nuevaVersionFup = JSON.parse(msg.d);
            ocultarLoad();
            toastr.success("Enviado correctamente");
            ValidarEstado();
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Actualizar Estado", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function CorreoNotificacion(pEvento) {

    var param = {
        idFup: IdFUPGuardado,
        idVersion: $('#cboVersion').val(),
        pEvento: pEvento
    };

    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/CorreoNotificacion",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //var nuevaVersionFup = JSON.parse(msg.d);
            ocultarLoad();
            toastr.success("Correo Enviado correctamente");
            ValidarEstado();
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Enviar", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ParametrosSF() {
    if (parseInt(IdFUPGuardado) > 0) {
        var param = {
            idFup: IdFUPGuardado,
            idVersion: $('#cboVersion').val(),
            pCliente: $("#cboIdEmpresa option:selected").text(),
            pObra: $("#cboIdObra option:selected").text(),
            pProducido: "Colombia",
            pPais: $("#cboIdPais").val(),
            pCiudad: $("#cboIdCiudad").val(),
            pMoneda: $("#cboIdMoneda option:selected").text()
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/ParamSF",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                toastr.success("Entro parametros");
            },
            error: function (e) {
                toastr.error("Failed Establecer Sesion", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
}

function LlamarSF() {
    if (parseInt(IdFUPGuardado) > 0) {
        ParametrosSF();
        window.open("SolicitudFacturacionNew.aspx");
        //window.open("SolicitudFacturacionGBI.aspx");
    }
}

function LlamarCartaCot() {
    if (parseInt(IdFUPGuardado) > 0) {
        ParametrosSF();
        window.open("formsalidacotizacion.aspx?fup=" + IdFUPGuardado);
    }
}

//function LlamarSC() {
//    if (parseInt(IdFUPGuardado) > 0) {
//        ParametrosSF();
//        window.open("SolicitudCierre.aspx");
//    }
//}

function MostrarControl() {
    // Botones Datos Generales
    $("#selectTipoNegociacion").prop("disabled", true);
    $("#cboTipoCotizacion").prop("disabled", true);
    $("#selectProducto").prop("disabled", true);
    var vers = $('#cboVersion').val();

    if (vers == null) {
        vers = "";
    }
    if ((EstadoFUP == "" || EstadoFUP == "Elaboracion") && (vers == "A" || vers == "")) {
        $("#selectTipoNegociacion").prop("disabled", false);
        $("#cboTipoCotizacion").prop("disabled", false);
        $("#selectProducto").prop("disabled", false);
    }

    if ((EstadoFUP == "" || EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre")
        && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) {
        $(".fupgenpt0").show();
        if ($("#cboTipoCotizacion").val() == 3 || $("#cboTipoCotizacion").val() >= 7) {
            $(".fupgenlist").show();
        }
        if (RequiereEnviar <= CantGraba) {
            $(".fupgenenv").show();
        }
    }

    if ((EstadoFUP == "" || EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion")
        && (["54"].indexOf(RolUsuario) > -1)) {
        $(".fupgenpt0").show();
        if (RequiereEnviar <= CantGraba) {
            $(".fupgenenv").show();
        }
    }

    if ((EstadoFUP == "" || EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion")
        && (["26"].indexOf(RolUsuario) > -1) && Autogestion == 1) {
        $(".fupgenpt0").show();
        if (RequiereEnviar <= CantGraba) {
            $(".fupgenenv").show();
        }
    }

    if (["1", "2", "9", "26"].indexOf(RolUsuario) > -1) {
        $(".fupborrar").show();
    }

    // Botones Aprobacion
    if ((EstadoFUP == "Guardado")
        && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
        $(".fupapro").show();
    }

    // Botones Salida Cotizacion
    if ((EstadoFUP == "Aprobado" || EstadoFUP == "Cierre Comercial")
        && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
        $(".fupOrdcot2").show();

        if (RequiereCT == 1 && ExisteCT == 0 && EstadoFUP == "Aprobado") {
            $(".msjSimu").show();
        }
        else {
            $(".msjSimu").hide();
        }

        if ((RequiereCT == 0 || EstadoFUP == "Cierre Comercial") || (RequiereCT == 1 && ExisteCT > 0)) {
            $(".fupsalco").show();
        }

    }

    if ((["1", "26"].indexOf(RolUsuario) > -1)) {
        $(".fupOrdcot2").show();
    }

    // Botones Orden Cotizacion
    if ((["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
        $(".fupOrdcot").show();
    }

    // Botones Salida Cotizacion - Ciclo Cierre comercial
    if ((EstadoFUP == "Cierre Comercial")
        && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
        $(".fupsalcie").show();
    }

    //De: Maira Gomez <mairagomez@forsa.net.co>
    //Enviado el: jueves, 5 de mayo de 2022 10:39 a. m.
    //Este botón debe aparecer únicamente para los perfiles de las asistentes comerciales y la auxiliar comercial:
    if (["1", "9"].indexOf(RolUsuario) > -1) {
        $(".fupReplicarValCierre").show();
    } else {
        $(".fupReplicarValCierre").hide();
    }

    //De: Catalina Gutierrez <catalinagutierrez@forsa.net.co> 
    //Enviado el: jueves, 12 de mayo de 2022 11:17 a. m.
    //•	Generar nota en la pestaña de DFT, para que cuando el comercial realice la solicitud le aparezca la advertencia:
    //   Para solicitar la Definición técnica, el proyecto debe encontrarse en 50% de probabilidad y considerar MESFAC en el Acta de seguimiento Comercial.
    //
    if (["1", "4", "24", "26"].indexOf(RolUsuario) > -1) {
        if ((["50%", "75%", "100%"].indexOf($("#txtProbabilidad").val()) > -1) && (FecFacturar != '19000101')) {
            $(".controlcambiosDFT").show();
        } else {
            $(".controlcambiosDFT").hide();
        }
    } else {
        $(".controlcambiosDFT").hide();
    }

    // Botones ReCotizacion y guardar Precierre
    if ((EstadoFUP == "Cotizado")
        && (["1", "2", "3", "9", "30", "33", "34", "40", "54"].indexOf(RolUsuario) > -1)) {
        $(".fupcot").show();
    }

    // Botones Precierre
    if ((EstadoFUP == "Pre-Cierre Elaboracion")
        && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) {
        $(".fupprc").show();
    }

    // Botones Fechas Salida Cotizacion
    if ((EstadoFUP == "Aval para SF Elaboracion")
        && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) {
        $(".fupave").show();
    }

    if (EstadoFUP == "Aval para SF") {
        $(".fupsf").show();
    }

    // Botones Fechas Salida Cotizacion
    if (EstadoFUP == "Solicitud Facturacion" || EstadoFUP == "Orden Fabricacion" || EstadoFUP == "Modulacion Final") {
        $(".fupofa").show();
    }

    // Botones Generar Orden Fabricacion
    if ((EstadoFUP == "Solicitud Facturacion" || EstadoFUP == "Orden Fabricacion")
         && ((["1", "2"].indexOf(RolUsuario) > -1) || (["9"].indexOf(RolUsuario) > -1) && CreaOF == 1)) {
        if ($("#cboTipoCotizacion").val() == 5)
            $(".fupofa1").show();
        else if ((["75%", "100%"].indexOf($("#txtProbabilidad").val()) > -1) && (FecFacturar != '19000101')) {
            $(".fupofa1").show();
        }
    }

    // Botones Generar M2 Modulados Final
    if (((EstadoFUP == "Orden Fabricacion") || (EstadoFUP == "Modulacion Final"))
        && (["1", "4", "24", "25", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
        $(".fupmodfin").show();
    }

    // Botones Fechas Salida Cotizacion - Cargue de Carta Final Modulada
    if (EstadoFUP == "Modulacion Final") {
        $(".fupcfm").show();
    }

    // Planos Tipo Forsa
    if (["1", "3", "26", "54"].indexOf(RolUsuario) > -1) {
        $(".varPTFCom").show();
    }
    if (["1", "26"].indexOf(RolUsuario) > -1) {
        $(".varPTFSopCom").show();
    }
    ValCondicionPago();

    // Avales Cierre comercial
    $(".fupAval").hide();
    //Roles de Tesorería y Jurídico para avales
    if ((["1", "10", "55", "39", "2", "9"].indexOf(RolUsuario) > -1)
        && (EstadoFUP == "Aval para SF" || EstadoFUP == "Solicitud Facturacion" || EstadoFUP == "Orden Fabricacion" || EstadoFUP == "Modulacion Final")) {
        $(".fupAval").show();

        //Roles de Tesorería y Jurídico no guardan
        if ((["10", "55"].indexOf(RolUsuario) > -1)) {
            $(".f-noTesoJur").hide();
        }

        if (["1", "55", "9"].indexOf(RolUsuario) > -1) {
            $(".avalJuridico").show();
        }
        if (["1", "10"].indexOf(RolUsuario) > -1) {
            $(".avalTesoreria").show();
        }
        if (["1", "39"].indexOf(RolUsuario) > -1) {
            $(".avalVicecomercial").show();
        }
        if (["1", "2"].indexOf(RolUsuario) > -1) {
            $(".avalGercomercial").show();
        }

    }


    // Solo debe actualizar la fecha pactada Plan - Planeador Produccion
    if (["1", "36"].indexOf(RolUsuario) > -1) {
        $(".fecPactada").prop("disabled", false);
    }
    else {
        $(".fecPactada").prop("disabled", true);
    }


    // Visible Secciones NO Forsa PRO
    if ($("#selectProducto").val() == "17") {
        $(".forsapro").hide();
        if (ContAdicion >= 2) {
            $(".forsapro2").hide();
        }
    }

    //    // Visible contrato MRV para cuando el Cliente es MRV
    //    if ($('#cboIdEmpresa').val() == "2897") {
    //        $(".Solomrv").show();
    //        }
    //    else {
    //        $(".Solomrv").hide();
    //    }
}

function OcultarControl() {
    $(".fupgen").hide();
    $(".fupapro").hide();
    $(".fupsalco").hide();
    $(".fupsalcie").hide();
    $(".fupcot").hide();
    $(".fupprc").hide();
    $(".fupsf").hide();
    $(".fupave").hide();
    $(".fupofa").hide();
    $(".fupofa1").hide();
    $(".fupborrar").hide();
    $(".varflete").hide();
    $(".varPTFCom").hide();
    $(".varPTFSopCom").hide();
    $(".fupmodfin").hide();
    $(".fupcfm").hide();
    $(".CondicionalTotalDescuento").hide();
    $(".avalJuridico").hide();
    $(".avalTesoreria").hide();
    $(".avalGercomercial").hide();
    $(".avalVicecomercial").hide();
    $(".fupAval").hide();
    $(".cumpleCondPago").hide();
    $(".msjSimu").hide();
    $(".SeCopia").hide();
    $(".vaCopia").hide();
    $(".ActaSeguimientoDFT").hide();
    $(".ActaSeguimientoDFTPrograma").hide();
    $(".ActaSeguimientoDFTAval").hide();
}

function ObtenerRol() {
    OcultarControl();
    mostrarLoad();

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerRol",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            RolUsuario = data.Rol;
            nomUser = data.username;
            CreaOF = data.CreaOF;
            MostrarControl();
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

function ValidarEstado() {
    OcultarControl();
    mostrarLoad();

    var param = {
        pFupID: IdFUPGuardado,
        pVersion: $('#cboVersion').val()
    };
    mostrarLoad();
    CantGraba = 0;
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerEstado",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                EstadoFUP = elem.EstadoActual;
                RequiereEnviar = parseInt(elem.RequiereElabora);
                CantGraba += parseInt(elem.CantidadGrabada);
                OrdParte = elem.OrdenParte;
                RequiereCT = elem.RequiereCT;
                ExisteCT = elem.ExisteCT;
                Autogestion = elem.Autogestion;

                if (((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre") && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) ||
                    ((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion") && (["54"].indexOf(RolUsuario) > -1)) ||
                    ((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion") && (["26"].indexOf(RolUsuario) > -1) && Autogestion == 1)
                    ) {
                    switch (OrdParte) {
                        case 1:
                            $(".fupgenpt1").show();
                            if (CantGraba > 0)
                                $(".fupgenpt2").show();
                            break;
                        case 2:
                            $(".fupgenpt2").show();
                            if (CantGraba > 0)
                                $(".fupgenpt3").show();
                            break;
                        case 3:
                            $(".fupgenpt3").show();
                            if (CantGraba > 0)
                                $(".fupgenpt4").show();
                            break;
                        case 4:
                            $(".fupgenpt4").show();
                            if (CantGraba > 0)
                                $(".fupgenpt5").show();
                            break;
                        case 5:
                            $(".fupgenpt5").show();
                            break;
                    }
                }
            });

            MostrarControl();
            $("#divEstadoFup").html(EstadoFUP);
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Obtener Estado", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function CargarDatosParteOrden(object) {
    $("#PesosOrdenes").val($("#" + object.id + " option:selected").attr("data-VALOR"));
    $("#M2Ordenes").val($("#" + object.id + " option:selected").attr("data-M2"));
}

function CargarParteOrden() {
    $("#cmbPlantaOrdenes").change(function () {

        var idPedidoVenta = Number($(this).val());
        if (idPedidoVenta != -1) {
            mostrarLoad();
            $.ajax({
                type: "POST",
                url: "FormPrototypeReport.aspx/obtenerPartePorPv",
                data: JSON.stringify({
                    PedidoVenta: idPedidoVenta
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                // async: false,
                success: function (result) {
                    ocultarLoad();
                    if (result.d != null) {
                        $("#cmbParteOrdenes").children().remove();

                        var data = JSON.parse(result.d);

                        $("#cmbParteOrdenes").append("<option value='-1' > Seleccione </option>");
                        $(data).each(function (i, r) {
                            $("#cmbParteOrdenes").append("<option data-Sf_id='" + r.Sf_id + "' data-M2='" + r.M2 + "' data-VALOR='" + r.VALOR + "' value='" + r.Parte + "' >" + r.Parte + "</option>")
                        });
                    }
                },
                error: function () {
                    ocultarLoad();
                    toastr.error("Failed to ObtenerOrdenFabricacion", "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                }
            });
        }
    });
}

function LlenarParteOF(data) {
    $("#tbodyOrdenFabricacion").children().remove();
    $("#cmbParteOrdenes").children().remove();
    $("#cmbPlantaOrdenes").children().remove();
    $.each(data, function (i, r) {
        if ((["9"].indexOf(RolUsuario) > -1) && CreaOF == 1) {
            $("#tbodyOrdenFabricacion").append("<tr>" +
                            "<td>" + r.TIPO + "</td><td>" + r.ORDEN + "</td><td>" + r.PRODUCIDO_EN + "</td>" +
                            "<td class=\"mrv\">" + r.M2 + "</td><td class=\"mrv\">" + r.VALOR + "</td><td class=\"mrv\">" + r.VERSION + "</td>" +
                            "<td class=\"mrv\">" + r.PARTE + "</td><td>" + r.FECHA + "</td><td>" + r.RESPONSABLE + "</td>" +
                            "<td class=\"mrv\">" + r.m2Prod + "</td><td class=\"mrv\">" + r.m2Diferencia + "</td> " +
                            "<td class=\"mrv\"> <a target='blank' href=SeguimientoDespachos.aspx?idofa=" + r.Id_Ofa + "><i class='fa fa-eye fa-2x'></i> </a> </td>" +
                            "<td class=\"mrv\"> <a target='blank' href=AnulacionOrdenes.aspx?Orden=" + r.ORDEN + "><i class='fa fa-trash fa-2x'></i> </a> </td>" +
                            "</tr> ")
        }
        else {
            $("#tbodyOrdenFabricacion").append("<tr>" +
                            "<td>" + r.TIPO + "</td><td>" + r.ORDEN + "</td><td>" + r.PRODUCIDO_EN + "</td>" +
                            "<td class=\"mrv\">" + r.M2 + "</td><td class=\"mrv\">" + r.VALOR + "</td><td class=\"mrv\">" + r.VERSION + "</td>" +
                            "<td class=\"mrv\">" + r.PARTE + "</td><td>" + r.FECHA + "</td><td>" + r.RESPONSABLE + "</td>" +
                            "<td class=\"mrv\">" + r.m2Prod + "</td><td class=\"mrv\">" + r.m2Diferencia + "</td> " +
                            "<td class=\"mrv\"> <a target='blank' href=SeguimientoDespachos.aspx?idofa=" + r.Id_Ofa + "><i class='fa fa-eye fa-2x'></i> </a> </td>" +
                            "<td class=\"mrv\">  </td>" +
                                "</tr> ")
        }
    });

    if (RolUsuario == 54) { $(".mrv").hide(); }
    else {
        $(".mrv").show();
    }
}

function ObtenerOrdenFabricacion() {
    mostrarLoad();

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerOrdenFabricacion",
        data: JSON.stringify({
            pFupID: IdFUPGuardado,
            pVersion: $('#cboVersion').val()
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (result) {
            ocultarLoad();
            if (result.d != null) {
                var data = JSON.parse(result.d);
                LlenarParteOF(data[0]);
                $("#cmbPlantaOrdenes").html(llenarComboId(data[1]));
                $("#cmbPlantaOrdenes").val("-1").change();
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to ObtenerOrdenFabricacion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function GuardarOrdenFab() {
    if ($('#cmbParteOrdenes').val() > 0 || $('#cmbPlantaOrdenes') > 0) {
        mostrarLoad();

        var param = {
            idFup: IdFUPGuardado,
            idVersion: $('#cboVersion').val(),
            FabricadoEn: $('#cmbPlantaOrdenes').text(),
            idParte: $('#cmbParteOrdenes').val(),
            sf_id: $('#cmbParteOrdenes option:selected').attr("data-Sf_id")
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/guardarOrdenFabricacion",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                ValidarEstado();
                ObtenerOrdenFabricacion();
                toastr.success("Guardado correctamente");
            },
            error: function (e) {
                ocultarLoad();
                toastr.error("Failed Guardar Orden de Fabricación", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
    else {
        toastr.warning("Complete la información de la orden", "FUP", {
            "timeOut": "0",
            "extendedTImeout": "0"
        });
    }
}

function DescargarArchivo(NombreArchivo, Ruta) {
    if (NombreArchivo != null) {
        var test = new FormData();
        window.location = "DownloadHandler.ashx?NombreArchivo=" + NombreArchivo + " &Ruta=" + Ruta;
    }
}

function BorrarArchivo(NombreArchivo, Ruta, IdPlano) {
    if (NombreArchivo != null) {
        toastr.options.timeOut = "0";
        toastr.closeButton = true;

        toastr.warning("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelFile' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
            'Se va a eliminar el archivo ' + NombreArchivo + ' . ¿desea continuar?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelFile").click(function () {
                            BorrarArchivoFisico(NombreArchivo, Ruta, IdPlano);
                        });
                    }
                });

        toastr.options.timeOut = "5000";
        toastr.closeButton = false;
    }
};

function BorrarArchivoFisico(NombreArchivo, Ruta, IdPlano) {
    if (NombreArchivo != null) {
        mostrarLoad();
        var param = new FormData();

        param.append('idfup', IdFUPGuardado);
        param.append('version', $('#cboVersion').val());
        param.append('idplano', IdPlano);
        param.append('NombreArchivo', NombreArchivo);
        param.append('Ruta', Ruta);

        $.ajax({
            url: "DeleteHandler.ashx",
            type: "POST",
            contentType: false,
            processData: false,
            data: param,
            success: function (result) {
                ocultarLoad();
                var res = JSON.parse(result);
                if (res.conf.id == 1) {
                    toastr.success(res.conf.descripcion);
                    //res.lista
                }
                else {
                    toastr.error(res.conf.descripcion);
                }
                obtenerParteAnexosFUP(IdFUPGuardado, $('#cboVersion').val());
                obtenerDetalleCondicionesPago(IdFUPGuardado, $('#cboVersion').val());
                obtenerDetalleDocumentosCierre(IdFUPGuardado, $('#cboVersion').val());
            },
            error: function (err) {
                ocultarLoad();
                toastr.error(err.statusText, "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
};

function calcular_flete_loc() {
    mostrarLoad();
    var Caloto = 224; // para el flete local se calcula desde Caloto hasta la ciudad de la obra
    var valorcot = $("#txtTotalPropuestaCom").val();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        idPtoCargue: 224,
        idPtoDescargue: parseInt($("#IdCiuObraFlete").html()),
        idTerminoNegociacion: parseInt($('#selectTerminoNegociacion').val()),
        Cant1: parseInt($('#txtContenedor20').val()),
        Cant2: parseInt($('#txtContenedor40').val()),
        Cant3: 0,
        Cant4: 0,
        ValorCot: parseFloat(valorcot)
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/CalcularFlete",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            ocultarLoad();
            if (result.d != null) {
                var data = JSON.parse(result.d);
                if (data.NoValido != 0) {
                    toastr.error("Failed to Calcular Flete " + data.MsgValidacion, "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                }
                else {
                    llenarFlete(data, 1);
                }
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Calcular Flete", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function calcular_flete() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        idPtoCargue: parseInt($('#selectPuertoCargue').val()),
        idPtoDescargue: parseInt($('#selectPuertoDescargue').val()),
        idTerminoNegociacion: parseInt($('#selectTerminoNegociacion2').val()),
        Cant1: parseInt($('#fletetxtCant1').val()),
        Cant2: parseInt($('#fletetxtCant2').val()),
        Cant3: parseInt($('#fletetxtCant3').val()),
        Cant4: parseInt($('#fletetxtCant4').val()),
        ValorCot: 0
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/CalcularFlete",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            ocultarLoad();
            if (result.d != null) {
                var data = JSON.parse(result.d);
                if (data.NoValido != 0) {
                    toastr.error("Failed to Calcular Flete " + data.MsgValidacion, "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                }
                else {
                    llenarFlete(data, 1);
                }
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Calcular Flete", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function llenarFlete(data, evento) {
    $("#IdTransp").html(data.transportador_id);
    $("#lblTransp").val(data.transportador_texto);
    $("#IdAgentCarga").html(data.agente_carga_id);
    $("#lblAgentCarga").val(data.agente_carga_texto);

    $("#lblVrTipo1").val(data.vr_origen_t1);
    $("#lblVrTipo2").val(data.vr_origen_t2);
    $("#lblVrTipo3").val(data.vr_origen_t3);
    $("#lblVrTipo4").val(data.vr_origen_t4);

    //$("#LTFPD").val(data.agente_carga_texto);
    $("#lblLTF").val(data.leadTime);

    $("#LVrFlete").val(data.vr_flete);
    $("#LVrTotalFlete").val(data.vr_totalflete);
    $("#vrFleteLocalTotal").val(data.vr_totalflete);

    $("#VrTransInterno").html(data.vr_transint);
    $("#VrGastPtoOrig").html(data.vr_gastos_origen);
    $("#VrDespAduana").html(data.vr_aduana_origen);
    $("#VrSeguro").html(data.vr_seguro);
    $("#VrFleteInt").html(data.vr_internacional);
    $("#VrGastosPtoDest").html(data.vr_gastos_destino);
    $("#VrDespAduanalDest").html(data.vr_aduana_destino);
    $("#vrTranspAduaDest").html(data.vr_transpaduanadest);
    $("#vrTipo3").html(data.vr_destino_t1);
    $("#vrTipo4").html(data.vr_destino_t2);
    $("#VrInternal1").html(data.vr_internacional_t1);
    $("#VrInternal2").html(data.vr_internacional_t2);

    $('#fletetxtCant1').val(data.cantidad_t1);
    $('#fletetxtCant2').val(data.cantidad_t2);
    $('#fletetxtCant3').val(data.cantidad_t3);
    $('#fletetxtCant4').val(data.cantidad_t4);

    if (evento == 2) /*  Obtener datos fletes almacenado*/ {
        $('#selectPuertoCargue').val(data.puerto_origen_id);
        $('#selectPuertoDescargue').val(data.puerto_destino_id);
        ptoOrigen = data.puerto_origen_id;
        ptoDestino = data.puerto_destino_id;
        $('#selectTerminoNegociacion2').val(data.termino_negociacion_id);
    }
};

function obtener_flete() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val())
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerFlete",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            ocultarLoad();
            if (result.d != null) {
                var data = JSON.parse(result.d);
                if (data.NoValido != 0) {
                    Alert(data.MsgValidacion);
                }
                else {
                    llenar_flete(data, 2);
                }
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Obtener Flete", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function guardar_flete(TipoFlete) {
    mostrarLoad();
    var objg = {
    };
    var isNew = 1;
    $("[data-flete]").each(function (index) {
        var prop = $(this).attr("data-flete");
        var thisval = $(this).val();
        if (thisval == "")
            thisval = $(this).text();
        objg[prop] = thisval;
    });

    if (TipoFlete == 2) // Solo Calcular flete Local
    {
        objg["puerto_origen_id"] = 224;
        objg["puerto_destino_id"] = parseInt($("#IdCiuObraFlete").html());
        objg["agente_carga_id"] = -1;

    }

    var param = {
        idFup: IdFUPGuardado,
        idVersion: $('#cboVersion').val(),
        flete: objg
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardarFlete",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            toastr.success("Flete Guardado con Exito");
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Guardar Flete", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function limpiar_flete() {
    ptoOrigen = -1;
    ptoDestino = -1
    $('#IdTransp').val(0);
    $('#lblTransp').val(0);
    $('#IdAgentCarga').val(0);
    $('#lblAgentCarga').val(0);
    //$('#selectTerminoNegociacion2').html("");
    $('#IdPaisObraFlete').val(0);
    $('#IdCiuObraFlete').val(0);
    $('#LblCiuObraFlete').val(0);
    $('#LTipo1').val(0);
    $('#LTipo2').val(0);
    $('#fletetxtCant1').val(0);
    $('#vr_origen_t1').val(0);
    $('#fletetxtCant2').val(0);
    $('#vr_origen_t2').val(0);
    $('#fletetxtCant3').val(0);
    $('#vr_origen_t3').val(0);
    $('#fletetxtCant4').val(0);
    $('#vr_origen_t4').val(0);
    $('#lblLTF').val(0);
    $('#LVrFlete').val(0);
    $('#LVrTotalFlete').val(0);
    $('#VrTransInterno').val(0);
    $('#VrGastPtoOrig').val(0);
    $('#VrDespAduana').val(0);
    $('#VrSeguro').val(0);
    $('#VrFleteInt').val(0);
    $('#VrGastosPtoDest').val(0);
    $('#VrDespAduanalDest').val(0);
    $('#vrTranspAduaDest').val(0);
    $('#LTipo3').val(0);
    $('#vrTipo3').val(0);
    $('#LTipo4').val(0);
    $('#VrTipo4').val(0);
    $('#VrInternal1').val(0);
    $('#VrInternal2').val(0);

};

function GuardarComentario(tipo) {
    mostrarLoad();
    var vComenta = "";
    var vConse = 0;
    var datos_tablas = [];
    var co = $("#tbodycomentarioSC").find("tr").length;

    $("#tbodycomentarioSC").find("tr").each(function (i, r) {
        vComenta = $(r).find(".txtComent").val();
        vConse = vConse + 1;

        if (vComenta != null && vComenta != undefined) {
            var obj_tabla = {
                IdFUP: IdFUPGuardado,
                Version: $('#cboVersion').val(),
                Idtipo: tipo,
                comentario: vComenta,
                consecutivo: vConse
            };

            datos_tablas.push(obj_tabla);
        }
    });

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GuardarComentarios",
        data: JSON.stringify({
            listaComentario: datos_tablas
        }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            toastr.success("Comentario Guardado con Exito");
            obtenerComentarioSalcot();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Guardar Comentario", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function LlenarComentarioSalcot(data) {
    var td = "";
    ContCom = 0;
    $('#tbodycomentarioSC').html("");
    $.each(data, function (index, elem) {
        ContCom = ContCom + 1;

        td = "<td class='text-center'>" + elem.Fecha.substring(0, 10) + "</td><td class= 'txtComent' >" + elem.Comentario + "</td>" +
        '<td></td>';
        $('#tbodycomentarioSC').append('<tr id="comentario' + (ContCom) + '" >' + td + '</tr>');

    });
};

function obtenerComentarioSalcot() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        tipo: 1
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerComentarios",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (result) {
            ocultarLoad();
            if (result.d != null) {
                var data = JSON.parse(result.d);
                LlenarComentarioSalcot(data);
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Obtener Flete", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function GuardarPTF() {
    mostrarLoad();
    var objg = {
    };
    var isNew = 1;
    var idTipovobo = $('#cboEstadoPlanoTipoForsa').val();
    var PlanoNuevoRecotiza = $('#cboPlanosNuevosCot').val();
    var ReqRecotiza = $('#cboRequiereCotiza').val();
    var FechaProgramaSCI = $('#dtProgramadaSCI').val();
    objg = {
        Evento: $('#cboEstadoPlanoTipoForsa').val(),
        Plano: PlanoNuevoRecotiza,
        FechaCierre: $("#dtFechaAval").val(),
        Responsable: "-1",
        Observacion: $('#txtObservacionPFT').val(),
        Recotiza: ReqRecotiza,
        FecProgramadaSCI: FechaProgramaSCI
    }

    var param = {
        idFup: IdFUPGuardado,
        idVersion: $('#cboVersion').val(),
        planPTF: objg
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/guardarPlanoTipoForsa",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //var nuevaVersionFup = JSON.parse(msg.d);
            ocultarLoad();
            obtenerPlanoTipoForsa(IdFUPGuardado, $('#cboVersion').val());
            obtenerParteAnexosFUP(IdFUPGuardado, $('#cboVersion').val());
            obtenerControlCambio(IdFUPGuardado, $('#cboVersion').val());
            toastr.success("Guardado correctamente");
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed guardar PTF", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function LlenarAnexoPTF(data) {
    var rowsAnexosptf = "";
    $.each(data, function (index, elem) {
        rowsAnexosptf = rowsAnexosptf + "<tr><td>" + elem.fecha_crea + "</td>" +
                    "<td>" + elem.plano_descripcion + "</a></td>" +
                    "<td>" + elem.Eventoptf + "</td>" +
                    "<td>" + elem.Anexo + "</td>" +
                    "<td><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button></td>" +
                    "</tr>";
    });
    if (rowsAnexosptf.trim() == "") {
        rowsAnexosptf = "<tr></tr>";
    }
    $("#tbodyAnexoPTF").html(rowsAnexosptf);
}

function LlenarAnexoDeCierre(data) {
    var rowsAnexoscierre = "";
    $.each(data, function (index, elem) {
        elem.tipo
        rowsAnexoscierre = rowsAnexoscierre + "<tr><td>" + elem.tan_desc_esp + "</td>" +
                    "<td>" + elem.Anexo + "</a></td>" +
                    "<td>" + elem.fecha_crea + "</td>" +
                    "<td><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button></td>" +
                    "<td><button type=\"button\" class=\"fa fa-trash fupborrar \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
                    "</tr>";
    });
    if (rowsAnexoscierre.trim() == "") {
        rowsAnexoscierre = "<tr></tr>";
    }
    $("#tbodyAnexoCierre").html(rowsAnexoscierre);
}


function validarPlanoTipoForsa() {
    var flag = true;

    return flag;
}

function guardarOrdenCotizacion() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        FUsuario: nomUser
    };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/guardarOrdenCotizacionFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            if (data != null && data != '') {
                $("#txtOrdenCotizacion").val(data);
                toastr.success("Orden Cotización Guardada correctamente");
            }
            else {
                toastr.warning("Error Generando Orden Cotización", "FUP");
            }

        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed save Orden Cotización", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });

}

function obtenerDuplicadoFUP() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        idCliente: $.trim($('#cboIdEmpresaClon').val()),
        idContacto: $.trim($('#cboIdContactoClon').val()),
        idObra: $.trim($('#cboIdObraClon').val()),
        FUsuario: nomUser
    }
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerDuplicadoFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            ocultarLoad();
            var data = JSON.parse(msg.d);
            //IdFUPGuardado = data;
            if (data != null && data != '') {
                IdFUPGuardado = data.IdFUP;
                if (Number(IdFUPGuardado) > 0) {
                    toastr.success("Duplicado realizado correctamente");
                    $("#txtIdFUP").val(IdFUPGuardado);
                    obtenerVersionPorIdFup(IdFUPGuardado);
                    ValidarEstado();
                }
                else {
                    toastr.warning("Error Duplicando FUP", "FUP");
                }
            }
            else {
                toastr.warning("Error Duplicando", "FUP");
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Clone FUP", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function explosionarOrdenCotizacion() {
    mostrarLoad();
    if ($("#txtOrdenCotizacion").val() != "") {
        var param = {
            idFup: IdFUPGuardado,
            idVersion: $.trim($('#cboVersion').val())
        };
        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/explosionarOrdenCotizacionFUP",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                var data = JSON.parse(msg.d);
                if (data != null && data != '') {
                    if (data == "0" || data == "0") {
                        toastr.success("Orden Cotización fué Explosionada correctamente");
                        ValidarEstado();
                    }
                    else if (data == "-1")
                        toastr.warning("No existe Orden Cotización");
                    else if (data == "-2")
                        toastr.warning("No existen Items cargados a la Orden de Cotización");
                }
            },
            error: function (e) {
                ocultarLoad();
                toastr.error("Failed Exploded Orden Cotización", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
    else {
        toastr.warning("No se ha generado Orden Cotización ");
    }
}

function LlamarReporteSimulador() {
    if ($("#txtOrdenCotizacion").val() != "") {
        ParametrosSimulador();
        window.open("ReporteSimulador.aspx");
    }
}

function LlamarReporteListadoCT() {
    if (parseInt(IdFUPGuardado) > 0) {
        window.open("ReporteListaCT.aspx" + "?IdFUP=" + IdFUPGuardado + "&VerFUP=" + $('#cboVersion').val() + "");
    }
}

function ParametrosSimulador() {
    if ($("#txtOrdenCotizacion").val() != "") {
        var param = {
            OrdenCotizacion: $("#txtOrdenCotizacion").val()
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/ParamSimulador",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                toastr.success("Entro parametros");
            },
            error: function (e) {
                toastr.error("Failed Establecer Sesion", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
}

function ParametrosListadoCT() {
    if ($("#txtOrdenCotizacion").val() != "") {
        var param = {
            OrdenCotizacion: $("#txtOrdenCotizacion").val()
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/ParamSimulador",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                toastr.success("Entro parametros");
            },
            error: function (e) {
                toastr.error("Failed Establecer Sesion", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
}

function ObtenerClienteParametro() {
    mostrarLoad();
    IdPaisCliente = -1;

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerCliente",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            if (typeof data != "undefined" || typeof data == Object) {
                if (data.ID_pais != null) {
                    IdPaisCliente = data.ID_pais;
                    cargarPaises(data.ID_pais);

                    $("#cboIdPais").val(data.ID_pais).val(String(data.ID_pais)).select2().change(cargarCiudades(data.ID_pais, data));
                }
            }
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Obtener ClienteParametro", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ObtenerFupParametro() {
    var data = getUrlVars()["fup"];
    if (typeof data != "undefined") {
        $("#txtIdFUP").val(data);
        obtenerVersionPorIdFup(data);
    }
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

function SolicitarAvalesSF() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        pValorCierre: $("#NumberTotalCierre").val(),
        pMoneda: $("#cboIdMoneda option:selected").text()
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/SolicitudAvales",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            toastr.success("Se envió Solicitud Avales para Facturacion");
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Enviar Solicitud Avales Facturacion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ValCondicionPago() {
    var condicionPago = 0;
    condicionPago = $("#cboCondicionesPago").val();
    cumplecondicionPago = 0;
    // Botones Fechas Salida Cotizacion
    if (EstadoFUP == "Solicitud Facturacion" || EstadoFUP == "Orden Fabricacion" || EstadoFUP == "Modulacion Final") {
        cumplecondicionPago = 1;
    }
    else {
        if ((condicionPago == 2) && (parseFloat($("#txTotalCuotas").val()) == parseFloat($("#NumberTotalCierre2").val()))) {
            cumplecondicionPago = 1;
        }

        if ((condicionPago == 3) && (parseFloat($("#txTotalLeasing").val()) == parseFloat($("#NumberTotalCierre3").val()))) {
            cumplecondicionPago = 1;
        }
        if ((condicionPago == 1) && (parseFloat($("#txTotalLeasing").val()) == parseFloat($("#NumberTotalCierre3").val()))) {
            cumplecondicionPago = 1;
        }
        if ((condicionPago == 4) && (parseFloat($("#txTotalLeasing").val()) == parseFloat($("#NumberTotalCierre3").val()))) {
            cumplecondicionPago = 1;
        }
        //if ((condicionPago == 1) || (condicionPago == 4)) {
        //    cumplecondicionPago = 1;
        //}
    }

    if (cumplecondicionPago) {
        $(".cumpleCondPago").show();
    } else {
        $(".cumpleCondPago").hide();
    }
}

function GrabaFechasCliente(tipo) {
    mostrarLoad();
    var mensajeTipo = "";
    if (tipo == 1) mensajeTipo = "Fecha En que Solicitó el cliente";
    if (tipo == 2) mensajeTipo = "Fecha En que Entrego Cotizacion al cliente";
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        FecSolicita: $('#txtFechaSolicitaCliente').val(),
        FecEntrega: $('#txtFechaEntregaCotizaCliente').val()
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/GrabaFechasCliente",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            toastr.success("Se Guardó " + mensajeTipo);
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Guardando " + mensajeTipo, "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function LlenarControlCambio(lista) {
    $("#DinamycChange .Comentario").remove();
    cantidadComentario = 0;

    var cardCabecera = '<div class="box-header border-bottom border-primary Comentario" style="z-index: 2;"><table class="col-md-12 table-sm"><thead><tr><th width="12%">FECHA</th><th width="45%">TITULO</th><th width="15%">ESTADO FUP</th><th width="15%">ESTADO DEF.TEC.</th><th width="15%">USUARIO</th>' +
                                                                        '<td width="3%"></td></tr></thead></table></div>';
    var cardFoot = '';
    var cardBody = '';
    var idParteDinamica = '';
    var OrdenParte = 0;

    $.each(lista, function (i, r) {
        cantidadComentario = cantidadComentario + 1;
        if (r.Padre == "0") {

            if (cardBody != "") {
                cardBody += '</div></div></div><div class="row item" ></div>';
            }
            idParteDinamica = r.Cons;
            OrdenParte = r.fcp_OrdenParte

            cardBody +=
                '<div class="col-md-12 Comentario" style="padding-top: 6x;" id="Parte' + r.Nivel + '"><div id="header' + r.Nivel + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                '<table class="col-md-12 table-sm"><tr ' + (r.EsDft ? 'class="table-primary"' : '') + '><td width="12%">' + r.Fecha.substring(0, 10) + '</td><td width="45%">' + r.Titulo + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td width="15%">' + r.Usuario + '</td>' +
                '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.Nivel + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                '<div id="body' + r.Nivel + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

            cardBody += '<div class="row item" id="' + r.Nivel + '"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 Observacion" rows="2" disabled>' + r.Comentario + '</textarea></div>';
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr ><td width="10%" >Anexo</td><td width="80%">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="' + r.Nivel + '"><table class="col-md-12 table-sm table-bordered">' + vLineaAnexo + '</table></div>';
            }

            cardBody += '<div class="row col-md-3"><button id="btnResp' + r.Cons + '" onclick="ControlCambioShow(\'Respuesta Control\',0,' + r.Id + ',\'' + r.Titulo + '\',' + r.EsDft + ')" type="button" class="btn btn-primary btn-sm"><i class="fa fa-comments"></i><b>Responder</b></button></div>';
            cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><thead><tr><th width="12%" colspan = "2" class="text-center" >FECHA</th><th width="43%">RESPUESTA</th><th width="15%">ESTADO FUP</th><th width="15%">ESTADO DEF.TEC.</th><th colspan = "2" width="15%">USUARIO</th></tr></thead></table></div></div>';

        }
        else {
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr><td width="12%" colspan = "2" class="text-center" >Anexo</td><td colspan = "4">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td width="13%">' + r.Usuario + '</td><td width="2%"></tr>' + vLineaAnexo + '</tbody></table></div></div>';
            }
            else {
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td colspan = "2" width="15%">' + r.Usuario + '</td></tbody></table></div></div>';
            }
        }
    });
    $("#DinamycChange").append(cardCabecera + cardBody);
}

function LlenarObservationConsideration(lista) {
    //$("#DinamycChange .Comentario").remove();
    cantidadComentarioConsiderationObservation = 0;

    var cardFoot = '';
    var cardBody = '';
    var idParteDinamica = '';
    var OrdenParte = 0;

    $.each(lista, function (i, r) {
        cantidadComentarioConsiderationObservation = cantidadComentarioConsiderationObservation + 1;
        if (r.Padre == "0") {

            if (cardBody != "") {
                cardBody += '</div></div></div><div class="row itemConsiderationObservation" ></div>';
            }
            idParteDinamica = r.Cons;
            OrdenParte = r.fcp_OrdenParte

            cardBody +=
                '<div class="col-md-12 Comentario" style="padding-top: 6x;" id="ParteConsiderationObservation' + r.Nivel + '"><div id="headerConsiderationObservation' + r.Nivel + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                '<table class="col-md-12 table-sm"><tr ' + (r.EsDft ? 'class="table-primary"' : '') + '><td width="12%">' + r.Fecha.substring(0, 10) + '</td><td width="35%">' + r.Titulo + '</td><td width="10%">' + r.TipoEvidencia + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td width="15%">' + r.Usuario + '</td>' +
                '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.Nivel + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                '<div id="bodyConsiderationObservation' + r.Nivel + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

            cardBody += '<div class="row item" id="rowItemConsiderationObservation' + r.Nivel + '"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 ObservationConsideration" rows="2" disabled>' + r.Comentario + '</textarea></div>';
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr ><td width="10%" >Anexo</td><td width="80%">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="' + r.Nivel + '"><table class="col-md-12 table-sm table-bordered">' + vLineaAnexo + '</table></div>';
            }

            cardBody += '<div class="row col-md-3"><button id="btnRespConsiderationObservation' + r.Cons + '" onclick="ControlCambioShowC_O(\'Respuesta Consideracion u Observacion\',0,' + r.Id + ',\'' + r.Titulo + '\',' + r.EsDft + ',1)" type="button" class="btn btn-primary btn-sm"><i class="fa fa-comments"></i><b>Responder</b></button></div>';
            cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><thead><tr><th width="12%" colspan = "2" class="text-center" >FECHA</th><th width="43%">RESPUESTA</th><th width="15%">ESTADO FUP</th><th width="15%">ESTADO DEF.TEC.</th><th colspan = "2" width="15%">USUARIO</th></tr></thead></table></div></div>';

        }
        else {
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr><td width="12%" colspan = "2" class="text-center" >Anexo</td><td colspan = "4">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td width="13%">' + r.Usuario + '</td><td width="2%"></tr>' + vLineaAnexo + '</tbody></table></div></div>';
            }
            else {
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td colspan = "2" width="15%">' + r.Usuario + '</td></tbody></table></div></div>';
            }
        }
    });
    $("#DinamycChangeObservationConsideration").append(cardCabecera + cardBody);
}

function LlenarHallazgosObra(lista) {
    //$("#DinamycChange .Comentario").remove();
    cantidadComentarioSegLogistico = 0;

    var cardCabecera = '<div class="box-header border-bottom border-primary Comentario" style="z-index: 2;"><table class="col-md-12 table-sm"><thead><tr><th width="12%">FECHA</th><th width="22%">TITULO</th><th width="15%">FEC. DESPACHO</th><th width="15%">FEC. ENTREGA</th><th width="15%">ESTADO</th><th width="20%">USUARIO</th><th width="3%"></th>' +
                                                                        '<td width="3%"></td></tr></thead></table></div>';
    var cardFoot = '';
    var cardBody = '';
    var idParteDinamica = '';
    var OrdenParte = 0;

    $.each(lista, function (i, r) {
        cantidadComentarioSegLogistico = cantidadComentarioConsiderationObservation + 1;
        if (r.Padre == "0") {

            if (cardBody != "") {
                cardBody += '</div></div></div><div class="row itemSegLogistico" ></div>';
            }
            idParteDinamica = r.Cons;
            OrdenParte = r.fcp_OrdenParte

            cardBody +=
                '<div class="col-md-12 Comentario" style="padding-top: 6x;" id="ParteSegLogistico' + r.Nivel + '"><div id="headerSegLogistico' + r.Nivel + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                '<table class="col-md-12 table-sm"><tr ' + (r.EsDft ? 'class="table-primary"' : '') + '><td width="12%">' + r.Fecha.substring(0, 10) + '</td><td width="22%">' + r.Titulo + '</td><td width="15%">' + r.FecDespacho + '</td><td width="15%">' + r.FecEntrega + '</td><td width="15%">' + r.Estado + '</td><td width="20%">' + r.Usuario + '</td>' +
                '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.Nivel + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                '<div id="body' + r.Nivel + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

            cardBody += '<div class="row item" id="' + r.Nivel + '"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 SegLogistico" rows="2" disabled>' + r.Comentario + '</textarea></div>';
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr ><td width="10%" >Anexo</td><td width="80%">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="SegLogistico' + r.Nivel + '"><table class="col-md-12 table-sm table-bordered">' + vLineaAnexo + '</table></div>';
            }

            cardBody += '<div class="row col-md-3"><button id="btnRespSegLogistico' + r.Cons + '" onclick="ControlCambioShowC_O(\'Respuesta Seguimiento Logistico\',0,' + r.Id + ',\'' + r.Titulo + '\',' + r.EsDft + ',2)" type="button" class="btn btn-primary btn-sm"><i class="fa fa-comments"></i><b>Responder</b></button></div>';
            cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><thead><tr><th width="12%" colspan = "2" class="text-center" >FECHA</th><th width="43%">RESPUESTA</th><th width="15%">ESTADO FUP</th><th width="15%">ESTADO DEF.TEC.</th><th colspan = "2" width="15%">USUARIO</th></tr></thead></table></div></div>';

        }
        else {
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr><td width="12%" colspan = "2" class="text-center" >Anexo</td><td colspan = "4">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td width="13%">' + r.Usuario + '</td><td width="2%"></tr>' + vLineaAnexo + '</tbody></table></div></div>';
            }
            else {
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td colspan = "2" width="15%">' + r.Usuario + '</td></tbody></table></div></div>';
            }
        }
    });
    $("#DinamycChangeHallazgos").append(cardCabecera + cardBody);
}

function LlenarSegLogistico(lista) {
    //$("#DinamycChange .Comentario").remove();
    cantidadComentarioSegLogistico = 0;

    var cardCabecera = '<div class="box-header border-bottom border-primary Comentario" style="z-index: 2;"><table class="col-md-12 table-sm"><thead><tr><th width="12%">FECHA</th><th width="22%">TITULO</th><th width="15%">FEC. DESPACHO</th><th width="15%">FEC. ENTREGA</th><th width="15%">ESTADO</th><th width="20%">USUARIO</th><th width="3%"></th>' +
                                                                        '<td width="3%"></td></tr></thead></table></div>';
    var cardFoot = '';
    var cardBody = '';
    var idParteDinamica = '';
    var OrdenParte = 0;

    $.each(lista, function (i, r) {
        cantidadComentarioSegLogistico = cantidadComentarioConsiderationObservation + 1;
        if (r.Padre == "0") {

            if (cardBody != "") {
                cardBody += '</div></div></div><div class="row itemSegLogistico" ></div>';
            }
            idParteDinamica = r.Cons;
            OrdenParte = r.fcp_OrdenParte

            cardBody +=
                '<div class="col-md-12 Comentario" style="padding-top: 6x;" id="ParteSegLogistico' + r.Nivel + '"><div id="headerSegLogistico' + r.Nivel + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                '<table class="col-md-12 table-sm"><tr ' + (r.EsDft ? 'class="table-primary"' : '') + '><td width="12%">' + r.Fecha.substring(0, 10) + '</td><td width="22%">' + r.Titulo + '</td><td width="15%">' + r.FecDespacho + '</td><td width="15%">' + r.FecEntrega + '</td><td width="15%">' + r.Estado + '</td><td width="20%">' + r.Usuario + '</td>' +
                '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.Nivel + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                '<div id="body' + r.Nivel + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

            cardBody += '<div class="row item" id="' + r.Nivel + '"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 SegLogistico" rows="2" disabled>' + r.Comentario + '</textarea></div>';
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr ><td width="10%" >Anexo</td><td width="80%">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="SegLogistico' + r.Nivel + '"><table class="col-md-12 table-sm table-bordered">' + vLineaAnexo + '</table></div>';
            }

            cardBody += '<div class="row col-md-3"><button id="btnRespSegLogistico' + r.Cons + '" onclick="ControlCambioShowC_O(\'Respuesta Seguimiento Logistico\',0,' + r.Id + ',\'' + r.Titulo + '\',' + r.EsDft + ',2)" type="button" class="btn btn-primary btn-sm"><i class="fa fa-comments"></i><b>Responder</b></button></div>';
            cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><thead><tr><th width="12%" colspan = "2" class="text-center" >FECHA</th><th width="43%">RESPUESTA</th><th width="15%">ESTADO FUP</th><th width="15%">ESTADO DEF.TEC.</th><th colspan = "2" width="15%">USUARIO</th></tr></thead></table></div></div>';

        }
        else {
            if (r.Anexos.length > 0) {
                var vAnexo = JSON.parse(r.Anexos);
                var vLineaAnexo = "";
                jQuery.each(vAnexo, function (j, ane) {
                    vLineaAnexo += '<tr><td width="12%" colspan = "2" class="text-center" >Anexo</td><td colspan = "4">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                });
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td width="13%">' + r.Usuario + '</td><td width="2%"></tr>' + vLineaAnexo + '</tbody></table></div></div>';
            }
            else {
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td colspan = "2" width="15%">' + r.Usuario + '</td></tbody></table></div></div>';
            }
        }
    });
    $("#DinamycChangeSegLogistico").append(cardCabecera + cardBody);
}

$(document).on('change', '#cmbSolucionadoEnObra', function () {
    var selectedValue = $('#cmbSolucionadoEnObra').val();
    if(selectedValue == 0) {
        $('#divTxtObservaciones').hide();
    } else {
        $('#divTxtObservaciones').show();
    }
    $('#txtObsCntrCmC_O').val("");
});

function ControlCambioShowC_O(title, optionDefault, Respuesta, Titulo, dft, tipoEntrada) {
    $('#tipoEntrada').val(tipoEntrada);
    if (optionDefault) {
        $("#ModConsiderationObservation").val(optionDefault).prop("disabled", true)
    }
    else {
        $("#ModConsiderationObservation").val(-1).prop("disabled", false)
    }
    $("#padreCambioC_O").val(Respuesta);
    $("#EsDFTC_O").val(dft);

    if (Titulo.length > 0) {
        $("#txtTituloObsC_O").val(Titulo)
        $("#txtTituloObsC_O").prop("disabled", true);
    }
    else
        $("#txtTituloObsC_O").prop("disabled", false);

    if (Respuesta == 0)
        $("#AreaControlC_O").hide();
    else {
        $("#AreaControlC_O").val("Comentario Padre : " + Respuesta);
        $("#AreaControlC_O").show();
    }

    $('#CamposConsiderationObservation').hide();
    $('#camposSegLogistico').hide();
    $('#camposHallazgos').hide();
    if (tipoEntrada == 1) {
        $('#CamposConsiderationObservation').show();
        $('#observacionesTitle').html("Detalles");
        $('#divTxtObservaciones').show();
    } else if (tipoEntrada == 2) {
        $('#camposSegLogistico').show();
        $('#observacionesTitle').html("Detalles");
        $('#divTxtObservaciones').show();
    } else if (tipoEntrada == 3){
        $('#camposHallazgos').show();
        $('#observacionesTitle').html("Detalle de la solución");
        $('#divTxtObservaciones').hide();
    } else if (tipoEntrada == 4) {

    }

    $("#txtObsCntrCmC_O").val("");
    var modal = $("#ModConsiderationObservation");
    modal.find('.modal-titleC_O').text(title);
    modal.modal('show');
}


function obtenerControlCambio(idFup, idVersion) {
    var param = {
        idFup: idFup, idVersion: idVersion
    };
    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/ObtenerControlCambio",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarControlCambio(data);
        },
        error: function () {
            toastr.error("Failed to load Control Cambios", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function LlenarArmado(data) {
    var rowsAnexos1 = "";
    var rowsAnexos2 = "";
    var rowsAnexos3 = "";
    var rowsAnexos4 = "";
    var rowsAnexos5 = "";
    var rowsAnexos6 = "";
    $.each(data, function (index, elem) {
        if (elem.id_tipoAnexo == 28) {
            rowsAnexos1 = rowsAnexos1 + "<tr><td colspan='2'>" + elem.Anexo + "</td>" +
                        "<td class=\"text-center\">" + elem.fecha_crea + "</td>" +
                        "<td class=\"text-center\"><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                        "<button type=\"button\" class=\"fa fa-trash fupmodfin \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
                        "</tr>";

            rowsAnexos2 = rowsAnexos2 + "<tr><td>" + elem.tan_desc_esp + "</td><td>" + elem.Anexo + "</td>" +
                    "<td class=\"text-center\">" + elem.fecha_crea + "</td>" +
                    "<td class=\"text-center\"><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                        "<button type=\"button\" class=\"fa fa-trash fupmodfin \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
                "</tr>";
        }
        if (elem.id_tipoAnexo == 30) {
            rowsAnexos3 = rowsAnexos3 + "<tr><td colspan='2'>" + elem.Anexo + "</td>" +
                        "<td class=\"text-center\">" + elem.fecha_crea + "</td>" +
                        "<td class=\"text-center\"><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                        "<button type=\"button\" class=\"fa fa-trash fupmodfin \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
                        "</tr>";

            rowsAnexos4 = rowsAnexos4 + "<tr><td>" + elem.tan_desc_esp + "</td><td>" + elem.Anexo + "</td>" +
                    "<td class=\"text-center\">" + elem.fecha_crea + "</td>" +
                    "<td class=\"text-center\"><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                    "<button type=\"button\" class=\"fa fa-trash fupmodfin \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
                    "</tr>";
        }
        if (elem.id_tipoAnexo == 29) {
            rowsAnexos5 = rowsAnexos5 + "<tr><td colspan='2'>" + elem.Anexo + "</td>" +
                        "<td class=\"text-center\">" + elem.fecha_crea + "</td>" +
                        "<td class=\"text-center\"><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                        "<button type=\"button\" class=\"fa fa-trash fupmodfin \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
                        "</tr>";

            rowsAnexos6 = rowsAnexos6 + "<tr><td>" + elem.tan_desc_esp + "</td><td>" + elem.Anexo + "</td>" +
                        "<td class=\"text-center\">" + elem.fecha_crea + "</td>" +
                        "<td class=\"text-center\"><button type=\"button\" class=\"fa fa-download\" data-toggle=\"tooltip\" title=\"Descargar\" onclick=\"DescargarArchivo('" + elem.Anexo + "','" + elem.Ruta + "')\"> </button>" +
                        "<button type=\"button\" class=\"fa fa-trash  fupmodfin \" data-toggle=\"tooltip\" title=\"Borrar\" onclick=\"BorrarArchivo('" + elem.Anexo + "','" + elem.Ruta + "','" + elem.id_plano + "')\"> </button></td>" +
                        "</tr>";
        }
    });
    $("#tbodyanexos_armado1").html(rowsAnexos1);
    $("#tbodyanexos_armado2").html(rowsAnexos3);
    $("#tbodyanexos_armado3").html(rowsAnexos5);

    if (AlumCompleto == 1) { $("#tbodyArmado1").html(rowsAnexos2); } else {
        $("#tbodyArmado1").html("");
    }
    if (EscaCompleto == 1) { $("#tbodyArmado2").html(rowsAnexos4); } else {
        $("#tbodyArmado2").html("");
    }
    if (AcceCompleto == 1) { $("#tbodyArmado3").html(rowsAnexos6); } else {
        $("#tbodyArmado3").html("");
    }

}

function obtenerArmado(idFup, idVersion) {
    var param = {
        idFup: idFup, idVersion: idVersion, TipoPago: 5
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/obtenerDetalleDocumentosCierre",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            var data = JSON.parse(msg.d);
            LlenarArmado(data);
        },
        error: function () {
            toastr.error("Failed to load details anexos Armado", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function GrabarTablaArmado() {
    var alum = 0, esca = 0, acce = 0, error = 0, msgError = "";

    if ($('#txtAlumNa1').prop("checked") == true && $('#txtAlumIC1').prop("checked") == true) {
        error = 1;
        msgError = "Solo se debe Seleccionar Una Opcion de Alumino Completo o No Aplica ";
    }
    if ($('#txtAlumNa2').prop("checked") == true && $('#txtAlumIC2').prop("checked") == true) {
        error = 1;
        msgError = msgError + " Solo se debe Seleccionar Una Opcion de Escalera Completo o No Aplica ";
    }
    if ($('#txtAlumNa3').prop("checked") == true && $('#txtAlumIC3').prop("checked") == true) {
        error = 1;
        msgError = msgError + " Solo se debe Seleccionar Una Opcion de Accesorios Completo o No Aplica "

    }

    if (error == 0) {

        if ($('#txtAlumNa1').prop("checked") == true) { alum = 2; }
        else {
            if ($('#txtAlumIC1').prop("checked") == true) {
                alum = 1;
            }
        }

        if ($('#txtAlumNa2').prop("checked") == true) { esca = 2; }
        else {
            if ($('#txtAlumIC2').prop("checked") == true) {
                esca = 1;
            }
        }

        if ($('#txtAlumNa3').prop("checked") == true) { acce = 2; }
        else {
            if ($('#txtAlumIC3').prop("checked") == true) {
                acce = 1;
            }
        }

        var param = {
            idFup: IdFUPGuardado,
            idVersion: $.trim($('#cboVersion').val()),
            ArmAlum: alum,
            ArmEsca: esca,
            ArmAcce: acce
        };

        $.ajax({
            type: "POST",
            url: "FormPrototypeReport.aspx/GrabaTablaArmado",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                //	            var data = JSON.parse(msg.d);
                toastr.success("Se Guardó Satisfactoriamente ");
                obtenerArmado(IdFUPGuardado, $.trim($('#cboVersion').val()));
            },
            error: function () {
                toastr.error("Failed to save Tabla Armado", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    }
    else {
        toastr.error("Error al guardar las Tablas de Armado " + msgError, "FUP", {
            "timeOut": "0",
            "extendedTImeout": "0"
        });
    }
}


function SimuListado() {
    event.preventDefault();
    var param = {
        idPais: $('#cboIdPais').val(),
        idCiudad: $('#cboIdCiudad').val(),
        idCliente: $('#cboIdEmpresa').val(),
        idProyecto: -1,
        fup: IdFUPGuardado,
        user: {
            idUsuario: nomUser
        },
        version: $('#cboVersion').val(),
        tipoCotizacion: 'LISTADO',
        cliente: $('#cboIdEmpresa option:selected').text(),
        contacto: $('#cboIdContacto option:selected').text(),
        ciudad: $('#cboIdCiudad option:selected').text(),
        idioma: idiomaSeleccionado,
        Precios: listaPrecios
    };

    var dataJSON = JSON.stringify(param);

    // create the form
    var form = document.createElement('form');
    form.setAttribute('method', 'post');
    form.setAttribute('action', ' http://forsa.toscanagroup.com.co/cotizar_listado.php');
    form.setAttribute("target", "_blank");

    // create hidden input containing JSON and add to form
    var hiddenField = document.createElement("input");
    hiddenField.setAttribute("type", "hidden");
    hiddenField.setAttribute("name", "Forsa3D");
    hiddenField.setAttribute("value", dataJSON);
    form.appendChild(hiddenField);

    // add form to body and submit
    document.body.appendChild(form);
    form.submit();
}

function getListaPrecios() {
    mostrarLoad();
    var param = {
        idfup: IdFUPGuardado
    };

    $.ajax({
        type: "POST",
        url: "FormPrototypeReport.aspx/getListaPrecios",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ocultarLoad();
            listaPrecios = JSON.parse(msg.d);
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Obtener Precios para Listado ", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}
