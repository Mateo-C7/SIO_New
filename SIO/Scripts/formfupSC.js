var listaAltura = [];
var listaEqu = [];
var IdFUPGuardado = null;
var EstadoFUP = "";
var RequiereEnviar = 99;
var CantGraba = 0;
var OrdParte = 0;
var cantidadMuro = 1;
var cantidadLosa = 1;
var cantidadOrdenReferencia = 1;
var cantidadEnrasePuerta = 1;
var cantidadEnraseVentana = 1;
var versionFupDefecto = 'A';
var idiomaSeleccionado = 'es';
var i = 1;
var ContEquipos = 1;
var RolUsuario = 0;
var ContCom = 0;
var nomUser = "";
var ptoOrigen = -1;
var ptoDestino = -1;
var datFletes;

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

    $.i18n().load({
        en: './Scripts/languages/languages_en.json',
        es: './Scripts/languages/languages_es.json',
        br: './Scripts/languages/languages_br.json'
    }).done(function () {
        cargarPaises();
        cargarListaAltura();
        cargarParamDatosGenerales();
        $(".langes").click();
    });


    $("#cboIdPais").change(function () {
        cargarCiudades($(this).val());
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
        AgregarAdaptacion(-1);
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
    });

    $("#tbComentarios").on("click", ".btnDelComentario", function (event) {
        $(this).closest("tr").remove();
    });

    $("#cboIdCiudad").change(function () {
        cargarClientes($(this).val());
    });

    $("#cboIdEmpresa").change(function () {
        cargarContactos_Obras($(this).val());
    });
    $("#cboIdObra").change(function () {
        $("#cboIdEstrato").val(-1).change();
        $("#cboIdTipoVivienda").val(-1).change();
        cargarObraInformacion($(this).val());
    });

    $('.select-filter').select2();

    $("#btnNuevo").click(function (event) {
        event.preventDefault();
        $(location).attr('href', 'FormFUP.aspx')
    });

    $("#btnFupBlanco").click(function () {
        window.open('Rapido/FUP_Impreso.pdf', '_blank');
    });
    $("#btnPTFListaCH").click(function () {
        window.open('Rapido/FUP_Impreso.pdf', '_blank');
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

    $("#btnBusOf").click(function (event) {
        event.preventDefault();
        if ($("#txtIdOrden").val().trim() != "") {
            obtenerVersionPorOF($("#txtIdOrden").val());
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

});

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
                    obtenerParteAnexosPTF(IdFUPGuardado, $('#cboVersion').val());
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

function llenarComboDescripcion(listaDatos) {
    var stringDatos = "<option value='-1'>" + $.i18n('select_opcion') + "</option>";
    for (i = 0; i < listaDatos.length; i++) {
        stringDatos = stringDatos + "<option value='" + listaDatos[i].descripcion + "'>" + listaDatos[i].descripcion + "</option>";
    }
    return stringDatos;
}

function CargarDatosGenerales() {
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerInfoGeneral",
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
                $("#TipoArchivoModal").html("");

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
                $("#TipoArchivoModal").html(llenarComboId(data.listatax));
                //listacot = ClaseCotizacion,
                //    listaneg = TipoNegociacion, selectTipoNegociacion
                //listavac = tipoVaciado, selectTipoVaciado
                //listasg = sistemaSeguridad, selectSistemaSeguridad
                //listaav = alinVertical, selectAlineacionVertical
                //listatf = TipoTMFachada, selectTipoFMFachada  selectTipoFMFachadaCliente
                //listatu = tipoUnion txtTipoUnionCliente,
                //    listadu = detUnion, selectDetalleUnionCliente  selectDetalleUnion
                //listafc = formaConstruccion, selectFormaConstruccion
                //listatn = terminoNegociacion y TerminoNegociacion2
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
                url: "FormFUP.aspx/obtenerInfoGeneralPorNegociacion",
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

                    $("#cboTipoCotizacion").html(llenarComboId(data.listatcot));
                    $("#selectProducto").html(llenarComboId(data.listaprod));
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
            url: "FormFUP.aspx/obtenerInfoGeneralPorNegociacion",
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

                $("#cboTipoCotizacion").html(llenarComboId(data.listatcot));
                $("#selectProducto").html(llenarComboId(data.listaprod));

                if (typeof fupConsultado != "undefined") {
                    $("#cboTipoCotizacion").val(fupConsultado.TipoCotizacion);
                    $("#selectProducto").val(fupConsultado.Producto);
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

function cargarPaises() {
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerListadoPaises",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
            var data = JSON.parse(msg.d);
            llenarComboIdNombre("#cboIdPais",data);
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
            url: "FormFUP.aspx/ObtenerLineasDinamicas",
            data: JSON.stringify({ item: param }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (result) {
                ocultarLoad();
                CrearControles(JSON.parse(result.d));
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
    $('[data-toggle="tooltip"]').tooltip({
        animated: 'fade',
        placement: 'bottom',
        html: true
    });
}

function PrepararReporteFUP() {
    if (parseInt(IdFUPGuardado) > 0) {
        window.open("ReporteFUP.aspx" + "?IdFUP=" + IdFUPGuardado + "&VerFUP=" +$('#cboVersion').val() + "");
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
        url: "FormFUP.aspx/ObtenerVentaCierreComercial",
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
        url: "FormFUP.aspx/ObtenerEquipos",
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
        url: "FormFUP.aspx/ObtenerAdaptacion",
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
                if ((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre")
                    && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) {
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
                '<table class="col-md-12"><tr><td width="97%"><h5 class="box-title">' + r.DescipcionItem + '</h5></td>' +
                '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.IdItemParte + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                '<div id="body' + r.IdItemParte + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

        }
        else if (r.TipoRegistro == "2" || r.TipoRegistro == "3") {
            cardBody += '<div class="row item' + (r.ObsRequerida == true ? ' reqObservacion' : '') + (r.TipoRegistro == "2" && r.VaLista == true ? ' ValidaLista' : '') + '" id="row-' + r.IdItemParte + '">';

            //            cardBody += '<div width="20.160" class="form-group">';
            cardBody += '<div class="form-group">';

            if (r.TipoAyuda == "1")
                cardBody += '<button type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="top" data-original-title="<img src=\'' + r.TextoAyuda + '\'></img>"><i class="fa fa-info-circle fa-lg"></i></button>';
            else
                cardBody += '<button type="button" role="button" title="' + r.TextoAyuda + '" class="btn  btn-link divAyuda" data-placement="top"><i class="fa fa-info-circle fa-lg"></i></button>';

            cardBody +=
                '</div>' +
                '<div class="form-group col-sm-3 border-bottom" style="margin-top: 3px;">' +
                '<label style="labDinamico">' + r.DescipcionItem + '</label>' +
                '</div>';

            cardBody +=
                '<div class="form-group onoffswitch">' +
                '<input type="checkbox" onchange="sinoitemChange(this, \'row-' + r.IdItemParte + '\', ' + r.VaAdicional + ')"' + (r.SiNoItem == true ? 'checked' : '') + ' name="onoffswitch" ' + (r.Bloq_SINO_item == true ? 'disabled' : '') + '  class="onoffswitch-checkbox sinoItem" id="SiNo' + r.IdItemParte + '-' + i + '">' +
                '<label class="onoffswitch-label" for="SiNo' + r.IdItemParte + '-' + i + '">' +
                '<span class="onoffswitch-inner"></span>' +
                '<span class="onoffswitch-switch"></span>' +
                '</label>' +
                '</div>';

            if (r.TipoRegistro == "3")
                cardBody += '<div class="col-sm-2"><input readonly type="text" class="form-control form-lista-fup" data-toggle="tooltip" title="' + r.Defecto_ItemTextoLista + '" value="' + r.Defecto_ItemTextoLista + '"  /></div>';

            if (r.VaAdicional) {
                cardBody += '<div class="form-group onoffswitch">' +
                    '<input type="checkbox" onchange="sinoadicionalChange(this, \'row-' + r.IdItemParte + '\')" ' + (r.SiNoAdicional == true ? 'checked' : '') + '  name="onoffswitch" ' + (r.Bloq_SINO_Add == true ? 'disabled' : '') + ' class="onoffswitch-checkbox SiNoAdicional" id="SiNoAdicional' + i + '">' +
                    '<label class="onoffswitch-label" for="SiNoAdicional' + i + '">' +
                    '<span class="onoffswitch-inner"></span>' +
                    '<span class="onoffswitch-switch"></span>' +
                    '</label>' +
                    '</div><div class="col-sm-1"><input type="text" ' + (r.SiNoAdicional == true ? '' : 'disabled') + ' class="form-control CantAdicional" placeholder="cant." value="' + r.CantAdicional + '"  /></div>' +
                    '<div class="col-sm-2" style="padding-left: 0px;">';

                if (r.VaLista) {
                    cardBody += '<select class="form-control ComboAdi " ' + (r.SiNoAdicional == true ? '' : 'disabled') + ' id="ComboAcc' + i + '"  ><option value="-1">seleccione una opcion</option>';

                    $.each(r.dominio, function (index, option) {
                        cardBody += "<option " + (r.TextoLista == option.fdom_CodDominio ? 'selected' : '') + " value = '" + option.fdom_CodDominio + "'>" + option.fdom_Descripcion + "</option>"
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
                    cardBody += "<option " + (r.TextoLista == option.fdom_CodDominio ? 'selected' : '') + " value = '" + option.fdom_CodDominio + "'>" + option.fdom_Descripcion + "</option>"
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

    });

    if ((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre")
        && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1)) {
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

function sinoadicionalChange(object, id) {

    if ($("#" + id).children().find(".sinoItem").prop("checked") == false)
        $("#" + id).children().find(".Observacion").prop("disabled", !object.checked);

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
                url: "FormFUP.aspx/GuardarLineasDinamicas",
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
            url: "FormFUP.aspx/guardarVentaCierreComercial",
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
    });

    return flag;
}

function validarCantidadEquipos() {
    var flag = true;
    var cant="", desc= "";

    $("#tbEquipos tbody").find("tr").each(function (i, r) {
        cant = $(r).find(".EqCant").val();
        desc = $(r).find(".EqDesc").val();

        if (cant == "") {
            $(r).find(".EqCant").css("border", "2px solid crimson");
            flag = false;
        }
        
        if ( desc == "") {
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
        if (validarCantidadEquipos()) {EsValido =1;}
        else{EsValido = 0;}
    }
//    if (validarDescAdicional()) {EsValido =1;}
//        else{EsValido = 0;}

    if (EsValido == 1 ) {

        var countEquipos = $("#tbEquipos tbody").find("tr").length;
        var countAdapta  = $("#tbAdaptaciones tbody").find("tr").length;
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
                    url: "FormFUP.aspx/GuardarEquiposAdaptacion",
                    data: JSON.stringify({ listaItem: datos_tablas }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    // async: false,
                    success: function (result) {
                        toastr.success("Guardado correcto Equipo y Adaptaciones.");
                        ValidarEstado();
                    },
                    error: function () {
                        toastr.error("Error al guardar Equipo y Adaptaciones ");
                    }
                });

    }
    else
    {
       toastr.error("Faltan datos por Diligenciar");
    }
}

function validarDescAdicional() {
    var flag = true;
    var desc= "";

    $("#tbAdaptaciones tbody").find("tr").each(function (e, r) {
        desc = $(r).find(".DespAdaptacion").val();
        if ( desc == "") {
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

    if(validarDescAdicional()){

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
                url: "FormFUP.aspx/GuardarAdaptacion",
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

function cargarListaAltura() {
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerAlturaLibre",
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

function PruebaWebMethod() {
    $.ajax({
        type: 'POST',
        url: 'FormFUP.aspx/search',
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
        url: "FormFUP.aspx/obtenerListadoCiudadesMoneda",
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
                    llenarComboIdNombre("#cboIdCiudad",elem);
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

function cargarClientes(idCiudad, fupConsultado) {
    var param = { idCiudad: idCiudad };
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerListadoClientesCiudad",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);
            $.each(data, function (index, elem) {
                if (index == 'varCliente') {
                    llenarComboIdNombre("#cboIdEmpresa",elem);
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

function cargarContactos_Obras(idCliente, fupConsultado) {
    var param = { idCliente: idCliente };
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerListadoContactos_Obras_porCliente",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            //debugger;
            var data = JSON.parse(msg.d);

            $.each(data, function (index, elem) {
                if (index == 'varContacto') {
                    llenarComboIdNombre("#cboIdContacto",elem);
                };
                if (index == 'varObra') {
                    llenarComboIdNombre("#cboIdObra",elem);
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
            '<div class="col-1 text-center" >#' + String(cantidadOrdenReferencia) + '</div >' +
            ' <div class="col-5">' +
                '<input type="text" class="txtOrdenReferencia fuparr fuplist col-4" onblur="ValidarReferencia(this)" /> <span></span> ' +
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

function EliminarEnrasePuerta(control) {
    $(control).parent().parent().remove();
    cantidadEnrasePuerta -= 1;
}

function EliminarEnraseVentana(control) {
    $(control).parent().parent().remove();
    cantidadEnraseVentana -= 1;
}

function cargarParamDatosGenerales() {
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerParametrosDatosGenerales",
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
            $("#cboResponsablePlanoTipoForsa").get(0).options.length = 0;
            $("#cboResponsablePlanoTipoForsa").get(0).options[0] = new Option($.i18n('select_opcion'), "-1");

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
                    $("#cboPlanosPlanoTipoForsa").html("");
                    $("#cboPlanosPlanoTipoForsa").html(llenarComboId(listaPlanptf));
                };
                if (index == 'varResponsablePTF') {
                    $.each(elem, function (i, item) {
                        $("#cboResponsablePlanoTipoForsa").get(0).options[$("#cboResponsablePlanoTipoForsa").get(0).options.length] = new Option(item.fdom_Descripcion, item.fdom_CodDominio);
                    });
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
        url: "FormFUP.aspx/obtenerDatosObra",
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
}

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
//    alert(JSON.stringify(param));
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/GuardarFUP",
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
                }
                else {
                    toastr.warning("Error guardando FUP","FUP");
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
        if ($(this).val() != "2") {
            $("#selectSistemaSeguridad option[value=2]").attr("disabled", "disabled");
        }
        else {
            $("#selectSistemaSeguridad option[value=2]").removeAttr('disabled');
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
        }
        else {
            $("#txtAlturaCap1").val("No Aplica");
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

    $('#cboVersion').get(0).options.length = 0;

    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerVersionPorIdFup",
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
        '<div class="col-1 text-center" >#1</div>' +
        '<div class="col-5">' +
        ' <input type="text" class="txtOrdenReferencia fuparr fuplist col-4" onblur="ValidarReferencia(this)" /> <span></span>' +
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
                    '<div class="col-1 text-center" >#' + String(cuentat3 + 1) + '</div >' +
                    ' <div class="col-5">' +
                    '<input type="text" class="txtOrdenReferencia col-4" onblur="ValidarReferencia(this)" value="' + item_tabla.valor + '" /> <span></span>' +
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

        $(".fupadap").removeAttr("disabled");
        $(".fuplist").removeAttr("disabled");
        $("#txtNroEquipos").attr("disabled", "disabled");


        //Orden de Referencia Solo para Adaptacion y Listado
        if (tipo_cotizacion != "2" && tipo_cotizacion != "3") {
            $(".divvarof").hide();
        }
        else {
            $(".divvarof").show();
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
        else if (tipo_cotizacion == "3" || tipo_cotizacion == "7" ) {
            $(".fuplist").not('select, button').val("0");
            $("#titleEquipos").text("Equipos y Adaptaciones")
            $("#tbEquipos").attr("style", "display: normal")

            $(".fuplist").removeAttr("disabled");
            $(".fupadap").removeAttr("disabled");
            $(".fupadap").find('input[type="text"]').val("0");
            $('.fupadap').not("select, button").val("0");
            $(".fuparr").not('select, button').val("0");
            $(".fuparr").find('input[type="select"]').val("-1");
            $(".divarrlist").hide();
        }
        else {
            var tipo_negociacion = $("#selectTipoNegociacion").val();
            if (tipo_negociacion != "3" && tipo_negociacion != "4" 
             && tipo_negociacion != "5" && tipo_cotizacion != "7") {

                $(".fupadap").removeAttr("disabled");
                $(".fupadap").not('select, button').val("");
                $(".fuparr").not('select, button').val("");

                //$(".fuplist").removeAttr("disabled");
                $(".divarrlist").show();
                $(".fuplist").not('select, button').val("");
            }

            $("#titleEquipos").text("Equipos y Adaptaciones")
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

function obtenerInformacionFUP(idFup, idVersion, idioma) {
    IdFUPGuardado = idFup;
    mostrarLoad();
    var param = { idFup: idFup, idVersion: idVersion, idioma: idioma };
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerInformacionPorFupVersion",
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
                    $("#cboTipoCotizacion").val(elem.TipoCotizacion);
                    $("#selectProducto").val(elem.Producto);
                    CargarDatosGeneralesNegociacionLoad(elem)
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
                    $("#selectAlturaLibre").val(elem.AlturaLibre);
                    cargarDatosDependeAlturaLibre(elem);
                    $("#cboClaseCotizacion").val(elem.ClaseCotizacion).change();
                    $("#divEstadoFup").html(elem.EstadoProceso);
                    $("#txtEstadoCliente").val(elem.EstadoCli);
                    $("#txtFechaCreacion").val(elem.Fecha_crea);
                    $("#txtCreadoPor").val(elem.UsuarioCrea);
                    $("#txtCotizadoPor").val(elem.Cotizador);
                    $("#selectTipoUnionCliente").val(elem.TipoUnionCliente);
                    $("#selectTerminoNegociacion").val(elem.TerminoNegociacion);
                    $("#selectTerminoNegociacion2").val(elem.TerminoNegociacion);
                    $("#txtProbabilidad").val(elem.Probabilidad);
                    $("#txtOtros").val(elem.otros);

                    EstadoFUP = elem.EstadoProceso;

                    if (elem.TipoNegociacion == 1 || elem.TipoNegociacion == 2) {
                        $(".divarrlist").show();
                    }
                    else if (elem.TipoNegociacion == 3 || elem.TipoNegociacion == 4 || elem.TipoNegociacion == 5) {
                        $(".divarrlist").hide();
                    }
                    else {
                        $(".divarrlist").show();
                    }

                    //Orden de Referencia Solo para Adaptacion y Listado
                    if (elem.TipoNegociacion != "2" && elem.TipoNegociacion != "3") {
                        $(".divvarof").hide();
                    }
                    else {
                        $(".divvarof").show();
                    }

                    $("#txtNroEquipos").attr("disabled", "disabled");

                    if (elem.TipoCotizacion == 2) {
                        $("#txtNroEquipos").removeAttr("disabled");
                        $(".divvarof").show();
                        $(".divarrlist").show();
                        $("#titleEquipos").text("Adaptaciones")
                        $("#tbEquipos").attr("style", "display: none")
                    }
                    else if (elem.TipoCotizacion == 3 || elem.TipoCotizacion == 7) {
                        $(".divvarof").show();
                        $(".fupadap").removeAttr("disabled");
                        $(".divarrlist").hide();
                        $("#titleEquipos").text("Equipos y Adaptaciones")
                        $("#tbEquipos").attr("style", "display: normal")
                    }
                    else {
                        $("#titleEquipos").text("Equipos y Adaptaciones")
                        $("#tbEquipos").attr("style", "display: normal")
                        if (elem.TipoNegociacion != "3" && elem.TipoNegociacion != "4" &&
                                elem.TipoNegociacion != "5" && elem.TipoNegociacion != "7") {
                            $(".divarrlist").show();
                            $(".fupadap").removeAttr("disabled");
                        }

                        $("#titleEquipos").text("Equipos y Adaptaciones")
                        $("#tbEquipos").attr("style", "display: normal")
                        // $(".fupadap").find('input[type="text"]').val("");
                    }

                    if (elem.TipoCotizacion > 2) {
                        $("#headerEquipos").attr("style", "display:none");
                    }
                    else {
                        $("#headerEquipos").attr("style", "display:normal");
                    }
                };
                if (index == 'ordenFabricacion') {
                    $("#txtIdOrden").val(elem);
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
                    LlenarAnexoSalcot(elem);
                };
                if (index == 'varComentariosSC') {
                    LlenarComentarioSalcot(elem);
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
            });

            ObtenerLineasDinamicas();

            MostrarCards();
            ValidarEstado();
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
            url: "FormFUP.aspx/guardarAprobacionFUP",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                ocultarLoad();
                toastr.success("Guardado correctamente");
                ValidarEstado();
                obtenerParteAprobacionFUP(IdFUPGuardado, $('#cboVersion').val());
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
    else
    {
        toastr.error("Faltan Datos en Aprobacion", "FUP", {
            "timeOut": "0",
            "extendedTImeout": "0"
        });
    }
}

function LlenarAprobacion(elem) {
    $("#txtNumeroModulaciones").val(elem.Modulaciones);
    $("#txtNumeroCambios").val(elem.Cambios);
    $("#txtObservacionAprobacion").val(elem.rct_observacion);
    $("#cboVoBoFup").val(elem.VistoBueno).change();
    $("#cboMotivoRechazoFup").val("-1").change();
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

function LlenarAprobacionFUP(data) {
    $.each(data, function (index, elem) {
        if (index == 'varDataAprobacion') {
            LlenarAprobacion(elem);
        }
        if (index == 'varDataRechazo') {
            LlenarDevolucion(elem);
        }
    });
}

function obtenerParteAprobacionFUP(idFup, idVersion) {
    var param = { idFup: idFup, idVersion: idVersion };
    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/obtenerAprobacionesFUP",
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

function UploadFielModalShow(title, optionDefault, zona, EventoId) {
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
    UploadFielModalShow('Cargar Documentos Planos TF', 0, 'Planos TF',  Evento_id);
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

    function validarCamposAprobacionFup() {
        var flag = true;

        $('#cboVoBoFup').css("border", "");
        $('#txtNumeroModulaciones').css("border", "");
        $('#txtNumeroCambios').css("border", "");
        $('#cboMotivoRechazoFup').css("border", "");
        $('#txtObservacionAprobacion').css("border", "");

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
            }
        }

        return flag;
    }

    function LlenarAnexoFup(data){
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

    function obtenerParteAnexosFUP(idFup, idVersion) {
        var param = { idFup: idFup, idVersion: idVersion, TipoAnexo: 0};

        $("#ParteAnexosFUP").show();

        $.ajax({
            type: "POST",
            url: "FormFUP.aspx/obtenerAnexosFUP",
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

    function LlenarAnexoSalcot(data) {
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
        $("#tbodyanexos_salidaCot").html(rowsAnexosFup);
    }

    function obtenerAnexosSalidaCotFUP(idFup, idVersion, TipoAnexo) {
        var param = { idFup: idFup, idVersion: idVersion, TipoAnexo: 6 };

        $("#tbodyanexos_salidaCot").show();

        $.ajax({
            type: "POST",
            url: "FormFUP.aspx/obtenerAnexosFUP",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                var data = JSON.parse(msg.d);
                LlenarAnexoSalcot(data);
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
            url: "FormFUP.aspx/obtenerAnexosFUP",
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


    function obtenerVersionPorOF(idOrdenFabricacion) {
        mostrarLoad();
        var param = { idOrdenFabricacion: idOrdenFabricacion };
        var iteracion = 0;
        var idVersionReciente = '';

        $('#cboVersion').get(0).options.length = 0;
        $('#cboVersion').get(0).options[0] = new Option(versionFupDefecto, versionFupDefecto);

        $.ajax({
            type: "POST",
            url: "FormFUP.aspx/obtenerVersionPorOrdenFabricacion",
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
            url: "FormFUP.aspx/ValidarReferencia",
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
                url: "FormFUP.aspx/guardarRecotizacionFUP",
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
                url: "FormFUP.aspx/guardarDevComercial",
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
            url: "FormFUP.aspx/obtenerDevComercial",
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
    }

    function MostrarCards() {
        $("#ParteAprobacionFUP").show();
        $("#ParteAnexosFUP").show();
        $("#parteSalidaCot").show();
        $("#ParteSolicitudRecotizacion").show();
        $("#ParteVentaCierreCotizacion").show();
        $("#ParteFechaSolicitud").show();
        $("#PartePlanosTipoForsa").show();
        $("#ParteFletesFUP").show();
        $("#ParteOrdenFabricacion").show();
        $("#ParteAlcanceOferta").attr("style", "display:normal");
        $("#ParteModuladosCerrados").show();
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
            url: "FormFUP.aspx/guardarSalidaCotizacion",
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
                    else
                        obtenerFechaSolicitud(IdFUPGuardado, $('#cboVersion').val());
                }
                else {
                    toastr.warning("Error guardando " + textoOpcion );
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
            $("#txtTotalValorSC").val(elem.total_valor);
            $("#txtSistemaTrepanteAccsc").val(elem.m2_sis_seguridad);
            $("#txtAccSistemaSegSC").val(elem.vlr_sis_seguridad);
            $("#txtAccBasicosSC").val(elem.vlr_accesorios_basico);
            $("#txtAccComplSC").val(elem.vlr_accesorios_complementario);
            $("#txtAccOpcionalesSC").val(elem.vlr_accesorios_opcionales);
            $("#txtAccAdicionalesSC").val(elem.vlr_accesorios_adicionales);
            $("#txtOtrosProductoSC").val(elem.vlr_otros_productos);
            $("#txtTotalPropuestaCom").val(elem.total_propuesta_com);
            $("#txtTotalPropuestaComF").val(elem.total_propuesta_com);
            $("#txtContenedor20").val(elem.vlr_Contenedor20);
            $("#txtContenedor40").val(elem.vlr_Contenedor40);
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
            $("#txtTotalM_MF").val(elem.total_m2);
            $("#txtTotalValorMF").val(elem.total_valor);
            $("#txtSistemaTrepanteAccmf").val(elem.m2_sis_seguridad);
            $("#txtAccSistemaSegmf").val(elem.vlr_sis_seguridad);
            $("#txtAccBasicosmf").val(elem.vlr_accesorios_basico);
            $("#txtAccComplmf").val(elem.vlr_accesorios_complementario);
            $("#txtAccOpcionalesmf").val(elem.vlr_accesorios_opcionales);
            $("#txtAccAdicionalesmf").val(elem.vlr_accesorios_adicionales);
            $("#txtOtrosProductomf").val(elem.vlr_otros_productos);
            $("#txtTotalPropuestaComMF").val(elem.total_propuesta_com);
        }
        else {
            $("#txtM2EquipoBasemf").val("0");
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
        var fec1 ="", fec2 ="";
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
            url: "FormFUP.aspx/ObtenerPlanosTipoForsa",
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

    function GuardarFechaSolicitud() {
        var param = {
            idFup: IdFUPGuardado,
            idVersion: $('#cboVersion').val(),
            FechafirmaContrato: $('#dtfirmaContrato').val(),
            Fechacontractual: $('#dtfechacontractual').val(),
            FechaFormalizaPago: $('#dtFormalizaPago').val(),
            FechaPlazo: $('#dtPlazo').val()
        };

        mostrarLoad();
        $.ajax({
            type: "POST",
            url: "FormFUP.aspx/GuardarFechaSolicitud",
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
        ocultarLoad();
    }

    function LlenarFechasSol(data) {
        var fec1 = "", fec2 = "", fec3 = "";
        $.each(data, function (index, elem) {
            if (elem.FechaFirmaContrato.substring(0, 10) != "1900-01-01") {
                fec1 = (elem.FechaFirmaContrato);
                fec1 = fec1.substring(0, 10);
            }
            if (elem.FechaContractual.substring(0, 10) != "1900-01-01") {
                fec2 = (elem.FechaContractual);
                fec2 = fec2.substring(0, 10);
            }
            if (elem.FechaAnticipado.substring(0, 10) != "1900-01-01") {
                fec3 = (elem.FechaAnticipado);
                fec3 = fec3.substring(0, 10);
            }
            $("#dtfirmaContrato").val(fec1);
            $("#dtfechacontractual").val(fec2);
            $("#dtFormalizaPago").val(fec3);
            $("#dtPlazo").val(elem.Plazo);
            $("#NumberDiasistecnica").val(elem.DiasAsistec);
            $("#Numberm2Cerrados").val(elem.m2Cerrados);
            $("#NumberTotalCierre").val(elem.ValorFinal);
            $("#NumberTotalFacturacion").val(elem.valorFacturacion);
            $("#m2Modulados").val(elem.m2Modulados);
            $("#vrTotalModulado").val(elem.valorFactModulado);
        });
    }

    function obtenerFechaSolicitud(idFup, idVersion) {
        IdFUPGuardado = idFup;

        var param = { pFupID: idFup, pVersion: idVersion};
        $.ajax({
            type: "POST",
            url: "FormFUP.aspx/ObtenerFechaSolicitud",
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
            url: "FormFUP.aspx/ActualizarEstado",
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
                url: "FormFUP.aspx/ParamSF",
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
        }
    }

    function MostrarControl() {
        // Botones Datos Generales
        $("#selectTipoNegociacion").prop("disabled", true);
        $("#cboTipoCotizacion").prop("disabled", true);
        $("#selectProducto").prop("disabled", true);
        var vers = $('#cboVersion').val();

        if (vers == null) {vers = "";}
        if ((EstadoFUP == "" || EstadoFUP == "Elaboracion") && (vers == "A" || vers == ""))
        {
            $("#selectTipoNegociacion").prop("disabled", false);
            $("#cboTipoCotizacion").prop("disabled", false);
            $("#selectProducto").prop("disabled", false);
        }

        if ((EstadoFUP == "" || EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre")
            && (["1","2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1))
        {
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
            && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1))
        {
            $(".fupapro").show();
        }

        // Botones Salida Cotizacion
//        if ((EstadoFUP == "Aprobado")  // || EstadoFUP == "Cierre Comercial")
//            && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1))
//        {
//            $(".fupsalco").show();
//        }

        // Botones Salida Cotizacion - Ciclo Cierre comercial
        if ((EstadoFUP == "Cierre Comercial")
            && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1))
        {
            $(".fupsalcie").show();
        }

        // Botones ReCotizacion y guardar Precierre
        if ((EstadoFUP == "Cotizado")
            && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1))
        {
            $(".fupcot").show();
        }

        // Botones Precierre
        if ((EstadoFUP == "Pre-Cierre Elaboracion")
            && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1))
        {
            $(".fupprc").show();
        }

        // Botones Fechas Salida Cotizacion
        if ((EstadoFUP == "Aval para SF Elaboracion")
            && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1))
        {
            $(".fupave").show();
        }

        if (EstadoFUP == "Aval para SF") 
        {
            $(".fupsf").show();
        }

        // Botones Fechas Salida Cotizacion
        if (EstadoFUP == "Solicitud Facturacion" || EstadoFUP == "Orden Fabricacion" || EstadoFUP == "Modulacion Final") {
            $(".fupofa").show();
        }

        // Botones Generar Orden Fabricacion
        if ((EstadoFUP == "Solicitud Facturacion" || EstadoFUP == "Orden Fabricacion")
             && (["1", "2", "9"].indexOf(RolUsuario) > -1))
        {
            if ($("#cboTipoCotizacion").val() == 5)
                $(".fupofa1").show();
            else if(["75%", "100%"].indexOf($("#txtProbabilidad").val()) > -1) { 
                $(".fupofa1").show();
            }
        }

        // Botones Generar M2 Modulados Final
        if ((EstadoFUP == "Orden Fabricacion")
            && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
            $(".fupmodfin").show();
        }

        // Planos Tipo Forsa
        if (["1", "3"].indexOf(RolUsuario) > -1) {
            $(".varPTFCom").show();
        }
        if (["1", "26"].indexOf(RolUsuario) > -1) {
            $(".varPTFSopCom").show();
        }

    }

    function OcultarControl() {
        $(".fupgen").hide();    
        $(".fupapro").hide();
//        $(".fupsalco").hide();
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
    }

    function ObtenerRol() {
        OcultarControl();
        mostrarLoad();

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
            url: "FormFUP.aspx/ObtenerEstado",
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

                    if ((EstadoFUP == "Elaboracion" || EstadoFUP == "Devolucion" || EstadoFUP == "Pre-Cierre")
                        && (["1", "2", "3", "9", "30", "33", "34", "40"].indexOf(RolUsuario) > -1))
                    {
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
                    url: "FormFUP.aspx/obtenerPartePorPv",
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
            $("#tbodyOrdenFabricacion").append("<tr>" +
                            "<td>" + r.TIPO + "</td><td>" + r.ORDEN + "</td><td>" + r.PRODUCIDO_EN + "</td>" +
                            "<td>" + r.M2 + "</td><td>" + r.VALOR + "</td><td>" + r.VERSION + "</td>" +
                            "<td>" + r.PARTE + "</td><td>" + r.FECHA + "</td><td>" + r.RESPONSABLE + "</td>" +
                            "<td>" + r.m2Prod + "</td><td>" + r.m2Diferencia + "</td>" +
                            "</tr> ")
        });

    }

    function ObtenerOrdenFabricacion() {
        mostrarLoad();

        $.ajax({
            type: "POST",
            url: "FormFUP.aspx/ObtenerOrdenFabricacion",
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
                url: "FormFUP.aspx/guardarOrdenFabricacion",
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
                    toastr.error("Failed GuardarVentaCierreComercial", "FUP", {
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
            url: "FormFUP.aspx/CalcularFlete",
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
            url: "FormFUP.aspx/CalcularFlete",
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
                        llenarFlete(data,1);
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

        if (evento == 2) /*  Obtener datos fletes almacenado*/
        {
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
            url: "FormFUP.aspx/ObtenerFlete",
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
        var objg = {};
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
            url: "FormFUP.aspx/GuardarFlete",
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
        ptoOrigen=-1;
        ptoDestino=-1
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
        var vComenta;
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
            url: "FormFUP.aspx/GuardarComentarios",
            data: JSON.stringify({ listaComentario: datos_tablas }),
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

            td = "<td class='text-center'>" + elem.Fecha.substring(0, 10) + "</td><td>" + elem.Comentario + "</td>" +
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
            url: "FormFUP.aspx/ObtenerComentarios",
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
        var objg = {};
        var isNew = 1;
        $("[data-PTF]").each(function (index) {
            var prop = $(this).attr("data-PTF");
            var thisval = $(this).val();
            if (thisval == "")
                thisval = $(this).text();
            objg[prop] = thisval;
        });

        var param = {
            idFup: IdFUPGuardado,
            idVersion: $('#cboVersion').val(),
            planPTF: objg
        };

        $.ajax({
            type: "POST",
            url: "FormFUP.aspx/guardarPlanoTipoForsa",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (msg) {
                //var nuevaVersionFup = JSON.parse(msg.d);
                ocultarLoad();
                obtenerPlanoTipoForsa(IdFUPGuardado, $('#cboVersion').val());
                obtenerParteAnexosFUP(IdFUPGuardado, $('#cboVersion').val());
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


    function validarPlanoTipoForsa() {
        var flag = true;

        return flag;
    }