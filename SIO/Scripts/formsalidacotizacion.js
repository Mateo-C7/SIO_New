var versionFupDefecto = 'A';
var idiomaSeleccionado = 'es';
var i = 1;
var IdFUPGuardado = null;
var IdPaisCliente = -1;
var EstadoFUP = "";
var RolUsuario = 0;
var nomUser = "";
var res = [];
var ix = 0;
var lst_paises = [];
var ValFleteCal = [];
var ValSalidaCot = [];
var TerminoNeg = 0;
var MaxDes1 = 10;
var MaxDes2 = 15;
var ultimaVersion = "";
var txtDiscountJustification = "";
var DescuentoFueraRango = 0;
var CartaManual = 0;
var AutorizaVerPrecio = false;
var CartaManualTexto = "";
var valoresTotalesOferta = {};
var valoresInicialesTotaltesOferta = {};
var CambioOferta = {};
var VersionCarta = 1;
var formMon = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD'
});

var formPor = new Intl.NumberFormat('en-US', {
    style: 'percent',
    maximumFractionDigits: 2
});

$(document).on('inserted.bs.tooltip', function (e) {
    var tooltip = $(e.target).data('bs.tooltip');
    $(tooltip.tip).addClass($(e.target).data('tooltip-custom-classes'));
});

$(document).ready(function () {
    
    $.i18n({
        locale: idiomaSeleccionado
    });

    cambiarIdioma();
    ObtenerRol();

    $.i18n().load({
        en: './Scripts/languages/languages_en.json',
        es: './Scripts/languages/languages_es.json',
        br: './Scripts/languages/languages_br.json'
    }).done(function () {
        //cargarPaises();
        //cargarParamDatosGenerales();
        $(".langes").click();
    });

    $("#cboIdPais").change(function () {
        cargarCiudades($(this).val());
    });

    $('[data-toggle="tooltip"]').tooltip({
        animated: 'fade',
        placement: 'bottom',
        html: true
    });

    $('.select-filter').select2();

    $("#btnBusFup").click(function (event) {
        event.preventDefault();
        if ($.isNumeric($("#txtIdFUP").val())) {
            obtenerVersionPorIdFup($("#txtIdFUP").val());
        }
    });

    $("#txtIdFUP").keypress(function (e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        if (code == 13) {
            event.preventDefault();
            if ($.isNumeric($("#txtIdFUP").val())) {
                obtenerVersionPorIdFup($("#txtIdFUP").val());
            }
        }
    });

    $(".PSugerido").attr("style", "display:none");

    ObtenerFupParametro();

});

function cambiarIdioma() {
    $(".langes").click(function () {
        idiomaSeleccionado = 'es';
        $.i18n({
            locale: 'es'
        });
        if ($.isNumeric($("#txtIdFUP").val())) {
            obtenerInformacionFUP($("#txtIdFUP").val(), $('#cboVersion').val(), idiomaSeleccionado);
            $.fn.i18n();
        }
    });
    $(".langbr").click(function () {
        idiomaSeleccionado = 'br';
        $.i18n({
            locale: 'br'
        });
        if ($.isNumeric($("#txtIdFUP").val())) {
            obtenerInformacionFUP($("#txtIdFUP").val(), $('#cboVersion').val(), idiomaSeleccionado);
            $.fn.i18n();
        }
    });
    $(".langen").click(function () {
        idiomaSeleccionado = 'en';
        $.i18n({
            locale: 'en'
        });
        //$.fn.i18n();
        if ($.isNumeric($("#txtIdFUP").val())) {
            obtenerInformacionFUP($("#txtIdFUP").val(), $('#cboVersion').val(), idiomaSeleccionado);
            $.fn.i18n();
            //ObtenerLineasDinamicas();
            //obtenerInformacionFUP2($("#txtIdFUP").val(), versionFupDefecto, idiomaSeleccionado);

        }
    });
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

function obtenerVersionPorIdFup(idFup) {
    mostrarLoad();
    var param = { idFup: idFup };
    var iteracion = 0;
    var idVersionReciente = '';
    var cont = 0;

    $.ajax({
        type: "POST",
        url: "formsalidacotizacion.aspx/obtenerVersionPorIdFup",
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
                        iteracion = 1;
                        idVersionReciente = item.eect_vercot_id;
                        $("#cboVersion").val(idVersionReciente);
                        $("#txtFupVersion").val(idVersionReciente);
                    }
                    $('#cboVersion').get(0).options[$('#cboVersion').get(0).options.length] = new Option(item.eect_vercot_id, item.eect_vercot_id);
                    if (cont == 0) {
                        ultimaVersion = item.eect_vercot_id;
                    }
                    cont++;
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
$(document).on("change", "#cboVersion", function () {
    obtenerInformacionFUP($("#txtIdFUP").val(), $(this).val(), idiomaSeleccionado);
    $('#btnGuardarCot').attr('disabled', 'disabled');
    $('#btnGuardarCalc').attr('disabled', 'disabled');
    $('#btnCotizar').attr('disabled', 'disabled');

    if ($(this).val() == ultimaVersion) {
        $('#btnGuardarCot').removeAttr('disabled');
        $('#btnGuardarCalc').removeAttr('disabled');
        $('#btnCotizar').removeAttr('disabled');
    }
});
function obtenerInformacionFUP(idFup, idVersion, idioma) {
    IdFUPGuardado = idFup;
    mostrarLoad();
    var param = { idFup: idFup, idVersion: idVersion, idioma: idioma };
    console.log($("#txtIdFUP").val());
    console.log(ultimaVersion);
    $("#btnGuardarCot").attr("style", "display:none");
    $("#btnGuardarCalc").attr("style", "display:none");
    $("#btnCotizar").attr("style", "display:none");
    $(".btnCalcular").attr("style", "display:none");

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
            $.each(data, function (index, elem) {
                if (index == 'infoGeneral') {
                    //$("#cboIdPais").val(elem.ID_pais).val(String(elem.ID_pais)).select2().change(cargarCiudades(elem.ID_pais, elem));
                    //$("#cboIdMoneda").val(elem.ID_Moneda).change();
                    $("#txtPais").val(elem.Pais);
                    $("#txtMoneda").val(elem.Moneda);
                    $("#txtEmpresa").val(elem.Cliente);
                    $("#txtContacto").val(elem.Contacto);
                    $("#txtCiudad").val(elem.Ciudad);
                    $("#txtObra").val(elem.Obra);
                    $("#txtTipoNegociacion").val(elem.TipoNegociacionDesc);
                    $("#txtTipoCotizacion").val(elem.TipoCotizaDesc);
                    $("#txtProducto").val(elem.ProductoDesc);

                    cargarObraInformacion(elem.ID_Obra);

                    $("#txtClaseCotizacion").val(elem.ClaseCotizacionDesc);
                    $("#txtTipoVivienda").val(elem.TipoViviendaDesc);
                    
                    $("#divEstadoFup").html(elem.EstadoProceso);
                    $("#txtEstadoCliente").val(elem.EstadoCli);
                    $("#txtFechaCreacion").val(elem.Fecha_crea);
                    $("#txtCreadoPor").val(elem.UsuarioCrea);
                    $("#txtCotizadoPor").val(elem.Cotizador);
                    $("#txtProbabilidad").val(elem.Probabilidad);
                    AutorizaVerPrecio = elem.AutorizaVerPrecio;
                    VersionCarta = elem.VersionCarta;

                    if (RolUsuario == 26) {
                        AutorizaVerPrecio = true;
                    }
                    
                    EstadoFUP = elem.EstadoProceso;
                };
                if (index == 'varSolCartaManual') {
                    ProcesarRegistroSolicitudCartaManual(elem);
                }
            });

            obtener_SalidaCot();
            ObtenerLineasDinamicas(idVersion);
//            obtener_flete();
            SelectTab(event, 'Tab1');
            //MostrarCards();
            $("#divEstadoFup").html(EstadoFUP);
            
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to search FUP by Version and idFup", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
    //.then(ObtenerLineasDinamicas);
}


function CargarResumenOrden(idFup, idVersion) {
    var param = { fup: idFup, version: idVersion, TipoOf: "CT" };
    $.ajax({
        url: "FormFUP.aspx/ObtenerSimuladorProyectoResumen",
        type: "POST",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            LlenarResumenOrden(JSON.parse(result.d));
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

function LlenarResumenOrden(data) {
    $(".camposResumenOrden").html("");
    //$(".camposResumenOrdenInputs");
    if (data.length > 0) {
        $("#txtFecSimu").val(data[0].FechaSimulacion);
        totalesAlum = LlenarAluminioResumenOrden(data.filter(v => v.NivelAccesorios == 1));
        totalesAccesorios = LlenarAccesoriosSeguridad(data.filter(v => v.NivelAccesorios != 1), totalesAlum);
        //LlenarResumen(totalesAlum, totalesAccesorios);
        //$("#btnMostrarResumenOrden").show();
    }
}

function LlenarAluminioResumenOrden(data) {
    var costoFab = 0;
    var factorFlete = 0;
    var costoFabFlete = 0;
    var factorMargen = 0;
    var costoFabFleteMargen = 0;
    var factorImp = 0;
    var costoFabFleteMargenImp = 0;
    var tasa = 0;
    var precioVentaSugerido = 0;
    var costosItem = 0;
    var pesosItem = 0;
    var cantidadPiezastotal = 0;
    var cantidadM2Total = 0;
    data.forEach(element => {
        costoFab += element.CostoxItem;
        factorFlete = element.porcFlete * 100;
        costoFabFlete += element.ValorConFlete;
        factorMargen = element.PorcMargenMinimo * 100;
        costoFabFleteMargen += element.ValorConMargenMinimo;
        factorImp = element.porcImpuesto * 100;
        costoFabFleteMargenImp += element.ValorConImpuestoMinimo;
        tasa = element.tasaMonedaCotizacion;
        precioVentaSugerido += (element.ValorConImpuestoMinimo / element.tasaMonedaCotizacion);
        costosItem += element.CostoxItem;
        pesosItem += element.PesoxItem;
        cantidadPiezastotal += element.CantPiezas;
        cantidadM2Total += element.M2xItem;
    });
    $("#costosNivel1").html(costosItem.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}));
    $("#kilogramosNivel1").html(pesosItem.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}));
    $("#costoKgNivel1").html(formMon.format((costosItem / pesosItem)));
    $("#costoFab1").html(formMon.format(costoFab));
    $("#costoFleteNivel1").html(factorFlete.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}) + "%");
    $("#costoFabFleteNivel1").html(formMon.format(costoFabFlete));
    $("#factorMargenNivel1").html(factorMargen.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}) + "%");
    $("#costoFabFleteMargenNivel1").html(formMon.format(costoFabFleteMargen));
    $("#factorImpVentaNivel1").html(factorImp.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}) + "%");
    $("#costoFabFleteMargenImpNivel1").html(formMon.format(costoFabFleteMargenImp));
    $("#tasaNivel1").html(formMon.format(tasa));
    $("#precioVentaSugeridoNivel1").html(formMon.format(precioVentaSugerido));
    $("#datosAluminioM2").html(cantidadM2Total.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}));
    $("#datosAluminioPiezas").html(cantidadPiezastotal.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}));
    $("#datosAluminioKilogramos").html(pesosItem.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}));
    $("#datosAluminioPiezasM2").html((cantidadPiezastotal / cantidadM2Total).toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}));
    $("#datosAluminioKgM2").html((pesosItem / cantidadM2Total).toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 }));
    return {
        costoFab: costoFab, previoVentaSugerido: precioVentaSugerido,
        costoFabFlete: costoFabFlete,
        costoFabFleteMargen: costoFabFleteMargen,
        costoFabFleteMargenImp: costoFabFleteMargenImp
    };
}

function LlenarAccesoriosSeguridad(data, totalesAlum) {
    var costoFab = 0;
    var precioVentaSugerido = 0;
    var costoTotalFabFlete = totalesAlum.costoFabFlete;
    var costoTotalFabFleteMar = totalesAlum.costoFabFleteMargen;
    var costoTotalFabFleteMarImp = totalesAlum.costoFabFleteMargenImp;
    var costosLog = {

    };
    costosLog[1] = totalesAlum.costoFab;
    data.forEach(element => {
        const nivelAccesorio = element.NivelAccesorios
        costoFab += element.CostoxItem;
        costoTotalFabFlete += element.ValorConFlete;
        costoTotalFabFleteMar += element.ValorConMargenMinimo;
        costoTotalFabFleteMarImp += element.ValorConImpuestoMinimo;
        costosLog[nivelAccesorio] = element.CostoxItem;
        precioVentaSugerido += (element.ValorConImpuestoMinimo / element.tasaMonedaCotizacion);
        $("#costoFab" + nivelAccesorio).html(formMon.format(element.CostoxItem));
        $("#costoFleteNivel" + nivelAccesorio).html((element.porcFlete * 100).toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}) + "%");
        $("#costoFabFleteNivel" + nivelAccesorio).html(formMon.format(element.ValorConFlete));
        $("#factorMargenNivel" + nivelAccesorio).html((element.PorcMargenMinimo * 100).toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}) + "%");
        $("#costoFabFleteMargenNivel" + nivelAccesorio).html(formMon.format(element.ValorConMargenMinimo));
        $("#factorImpVentaNivel" + nivelAccesorio).html((element.porcImpuesto * 100).toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}) + "%");
        $("#costoFabFleteMargenImpNivel" + nivelAccesorio).html(formMon.format(element.ValorConImpuestoMinimo));
        $("#tasaNivel" + nivelAccesorio).html(formMon.format(element.tasaMonedaCotizacion));
        $("#precioVentaSugeridoNivel" + nivelAccesorio).html(formMon.format((element.ValorConImpuestoMinimo / element.tasaMonedaCotizacion)));
        $("#costosNivel" + nivelAccesorio).html(formMon.format(element.CostoxItem));
        $("#kilogramosNivel" + nivelAccesorio).html(element.PesoxItem.toLocaleString('en-US', {minimumFractionDigits: 0, maximumFractionDigits: 2}));
        $("#costoKgNivel" + nivelAccesorio).html(formMon.format((element.CostoxItem / element.PesoxItem)));
    });
    var costoFabTotalNiveles = costoFab + totalesAlum.costoFab;
    $("#costoFabTotal").html(formMon.format(costoFabTotalNiveles));
    $("#precioVentaSugeridoTotal").html(formMon.format((precioVentaSugerido + totalesAlum.previoVentaSugerido)));
    $("#costosTotal").html(formMon.format((costoFab + totalesAlum.costoFab)));
    $("#costoTabFleteTotal").html(formMon.format(costoTotalFabFlete));
    $("#costoTabFleteMarTotal").html(formMon.format(costoTotalFabFleteMar));
    $("#costoTabFleteMarImpTotal").html(formMon.format(costoTotalFabFleteMarImp));
    // Calcular porcentajes de participación
    $.each(costosLog, function (idx, value) {
        $("#pcParticipacionTotalNivel" + idx).html(((value * 100) / costoFabTotalNiveles).toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 }) + "%");
        $("#pcParticipacionN1Nivel" + idx).html(((value * 100) / totalesAlum.costoFab).toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 }) + "%");
    });

    // Llenar precios cotizados extrayendolos del tab 'Salida Cotizacion'
    // Octubre 31 /23 Se cargan los valores que esten en la carta de Cotizacion
    var precioVentaCotizadoTotal = 0;
    $.each(costosLog, function (idx, value) {
        let tempPrecio = Number($("#precioVentaCotizadoSalidaCotNivel" + idx).text().replace(/[^0-9.-]+/g, ""));
        if (!isNaN(tempPrecio)) {
            precioVentaCotizadoTotal += tempPrecio;
        }
        $("#precioVentaSugeridoMNivel" + idx).html($("#precioVentaCotizadoSalidaCotNivel" + idx).text());
    });
    $("#precioVentaSugeridoMTotal").html(formMon.format(precioVentaCotizadoTotal));

    precioVentaCotizadoTotal = 0;
    if (ValSalidaCot.length > 0) {
        $("#precioVentaCotizadoNivel1").html(formMon.format(parseFloat(ValSalidaCot[0].total_valor)));
        $("#precioVentaCotizadoNivel2").html(formMon.format(ValSalidaCot[0].vlr_accesorios_basico));
        $("#precioVentaCotizadoNivel3").html(formMon.format(ValSalidaCot[0].vlr_accesorios_complementario));
        $("#precioVentaCotizadoNivel4").html(formMon.format(ValSalidaCot[0].vlr_accesorios_opcionales + ValSalidaCot[0].vlr_accesorios_adicionales));
        $("#precioVentaCotizadoNivel5").html(formMon.format(ValSalidaCot[0].vlr_sis_seguridad));
        $("#precioVentaCotizadoTotal").html(formMon.format(parseFloat(ValSalidaCot[0].total_propuesta_com)));

    }
    else {
        $.each(costosLog, function (idx, value) {
            precioVentaCotizadoTotal += Number($("#precioVentaCotizadoSalidaCotNivel" + idx).text().replace(/[^0-9.-]+/g, ""));
            $("#precioVentaCotizadoNivel" + idx).html($("#precioVentaCotizadoSalidaCotNivel" + idx).text());
        });
        $("#precioVentaCotizadoTotal").html(formMon.format(precioVentaCotizadoTotal));
    }
}

function ProcesarRegistroSolicitudCartaManual(listaSolicitudesCartaManual) {
    $("#divAdvertenciaCartaManual").hide();
    $("#divAdvertenciaCartaManual").html("");
    if (listaSolicitudesCartaManual.length > 0) {
        ultimoRegistro = listaSolicitudesCartaManual[listaSolicitudesCartaManual.length - 1];
        CartaManual = 1;
        if (ultimoRegistro.FecSolicitud.substring(0, 10) != "1900-01-01" && ultimoRegistro.FecCancela.substring(0, 10) == "1900-01-01"
            && ultimoRegistro.FecNegacion.substring(0, 10) ==  "1900-01-01" && ultimoRegistro.FecAprobacion.substring(0, 10) == "1900-01-01") {
            CartaManualTexto = "<b><u>Deshabilitado debido a que la carta fue solicitada de forma manual o está en proceso de autorización</u></b>";
        } else if (ultimoRegistro.FecAprobacion.substring(0, 10) != "1900-01-01") {
            CartaManualTexto = "<b><u>CARTA MANUAL AUTORIZADA, Los valores de la carta Automatica pueden NO COINCIDIR con la Salida de Cotizacion</u></b>";
        }
    }
    else { CartaManual = 0;}
}

function obtenerInformacionFUP2(idFup, idVersion, idioma) {
    IdFUPGuardado = idFup;
    mostrarLoad();
    var param = { idFup: idFup, idVersion: idVersion, idioma: idioma };
    $(".DatosGen").remove();
    $("#btnGuardarCot").attr("style", "display:none");
    $("#btnGuardarCalc").attr("style", "display:none");

    obtener_SalidaCot();
    ObtenerLineasDinamicas();
    ocultarLoad();
}

function cargarPaises(IdPais) {
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
            lst_paises = data;

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
                    $("#txtIdUnidadesConstruir").val(elem.TotalUnidades);
                    $("#txtIdEstrato").val(elem.IdEstratoSocioEconomico);
                    $("#txtIdTipoVivienda").val(elem.ObraTipoVivienda).change();
                    $("#txtIdUnidadesConstruirForsa").val(elem.TotalUnidadesForsa);
                    $("#txtIdMetrosCuadradosVivienda").val(elem.M2Vivienda);

                    //$("#txtIdUnidadesConstruir").val(elem.TotalUnidades);
                    //$("#cboIdEstrato").val(elem.IdEstratoSocioEconomico).change();
                    //$("#cboIdTipoVivienda").val(elem.ObraTipoVivienda).change();
                    //$("#txtIdUnidadesConstruirForsa").val(elem.TotalUnidadesForsa);
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
                if (index == 'varMoneda') {
                    $.each(elem, function (i, item) {
                        $("#cboIdMoneda").get(0).options[$("#cboIdMoneda").get(0).options.length] = new Option(item.Descripcion, item.Id);
                    });
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

function toTitleCase(str) {
    return str.toLowerCase().replace(/(^|[\s\xA0])([a-z])/g, function (match) {
        return match.toUpperCase();
    });
}

function obtener_resumen_calculadora() {
    mostrarLoad();
    var param =
    {
        pFupID: IdFUPGuardado,
        pVersion: $('#cboVersion').val(), //versionFupDefecto,
        Idioma: idiomaSeleccionado
    };

    $.ajax({
        type: "POST",
        url: "FormSalidaCotizacion.aspx/ObtenerCalculadoraComercialResumen",
        data: JSON.stringify({ item: param }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            renderizar_resumen_calculadora(JSON.parse(result.d));
        },
        error: function () {
            ocultarLoad();
            if (error.msg != "") {
                toastr.error("Failed to load dynamic controls", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        }
    });
}

function renderizar_resumen_calculadora(data) {
    var tabCom = '';

    var ordenada = data.sort(function (a, b) {
        return a.Orden - b.Orden;
    });

    ordenada.forEach(function (item, i) {
        switch (item.TipoRegistro) {
            case 13:
                tabCom = tabCom + "<tr><td>" + item.Item + "</td>" +
                    "<td align ='right'>" + formMon.format(item.Unitario) + "</td>" +
                    "<td align ='right'>" + (item.Descuento) + "%</td>" +
                    '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>' +
                    "</tr>";

                break;
            case 6:
                tabCom = tabCom + "<tr><td colspan = 3>" + item.Item + "</td>" +
                    '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                break;
            case 7:
                var vIva = parseFloat(item.Valor);
                if ($("#cboIdPais").val() == 8) {

                    var vIvacal = parseFloat($('#txTotA4').val()) * parseFloat(item.Impuesto);;
                    if (vIvacal > vIva) { vIva = vIvacal; }
                }
                tabCom = tabCom + "<tr><td colspan = 3>" + item.Item + "</td>" +
                    '<td align ="right" >' + vIva.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                break;
            case 11:
                tabCom = tabCom + "<tr><td colspan = 3>" + item.Item + "</td>" +
                    '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                break;
            case 9:
                tabCom = tabCom + "<tr class='table-secondary'><td colspan = 3>" + item.Item + "</td>" +
                    '<td align ="right" id="txTotB">' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td></tr>';
                break;
        }
    });

    if (tabCom.trim() == "") {
        tabCom = "<tr></tr>";
    }

    ocultarLoad();
    $("#tbodycar").html(tabCom);
    toastr.info('Resumen Calculadora Comercial actualizado');
}

function construirJson(data) {
    $(".DatosGen").remove();
    var content = '';
    var cerraTitulo = false;
    var cDiv = '</div>'
    var numItems = 0;
    var ItemID = '';
    var cons = 0;
    var tit_1 = "";
    var tit_14 = "";
    var tit_15 = "";
    var tit_20 = "";
    var tit_30 = "";
    var tit_40 = "";
    var consEqui = 0;
    var tabCot = '';
    var tabCom = '';
    var tabSug = '';
    var detEquipo = '';
    var pCantidad = '';
    res = [];
    var tEquipo = 'EQUIPO';

    switch (idiomaSeleccionado.toUpperCase()) {
        case 'ES':
            tEquipo = 'EQUIPO';
            break;
        case 'EN':
            tEquipo = 'SET';
            break;
        case 'PT':
            tEquipo = 'JOGO';
            break;
        case 'BR':
            tEquipo = 'JOGO';
            break;
    }


    var ordenada = data.sort(function (a, b) {
        return a.Orden - b.Orden;
    });

    ordenada.forEach(function (item, i) {
        numItems += 1;
        ItemID = item.item_id * 1000 + numItems;

        switch (item.TipoRegistro) {
            case 1:
                if (item.IdGrupo != 1) {

                    if (cerraTitulo) {
                        if ((consEqui != 0) || (item.Consecutivo == 0 && item.IdGrupo > 2)) {
                            content += '</tbody></table>' + cDiv;
                        }
                        content += cDiv + cDiv + cDiv + cDiv + cDiv + cDiv;
                    }

                    content += '<div class="card DatosGen"><div class="card-header">' +
                        '<a class="collapsed card-link" data-toggle="collapse" href="#collapseItem' + i + '" data-i18n="">' + toTitleCase(item.Item) + '</a>' +
                        '</div>' +
                        '<div id="collapseItem' + i + '" class="collapse" data-parent="#accordion"><div class="card-body"><span class="font-weight-bold" style="font-size: 15px;" id="totalTodosNiveles' + i + '"></span>';

                    cerraTitulo = true;
                }
                break;
            case 2:
                add_Resumen(2, item);
                if (item.IdGrupo != 1) {
                    if (item.Orden == 200140) { tit_14 = item.Item; }
                    if (item.Orden == 200150 || item.Orden == 210150) { tit_15 = item.Item; }
                    if (item.Orden == 200180 || item.Orden == 210180) { tit_20 = item.Item; }
                    if (item.Orden == 300290 || item.Orden == 400510 || item.Orden == 200200) {
                        tit_20 = item.Item;
                        cons = 0;
                        consEqui = 0;
                    }

                    if (item.Orden < 20014) {
                        tit_1 = item.Item;

                        content += '<div class="row nopadding">';
                        var titulos = item.Item.split("|");

                        titulos.forEach(function (titulo, a) {
                            if (titulo.replace(' ', '').replace('.', '') != 'VRTOTAL' && titulo.replace(' ', '').replace('.', '') != 'VR TOTAL') {
                                if (a == 0) {
                                    var col = 4;

                                    if (titulos.length == 4)
                                        col = 6
                                }
                                else {
                                    if (titulo.replace(' ', '').startsWith('CANT'))
                                        col = 1;
                                    else
                                        col = 2;
                                }
                                content += '<div class="col-' + col + '"><label><strong>' + titulo + '</strong></label></div>'
                            }
                        })
                        content += cDiv;
                    }
                    else {

                        if ((item.Consecutivo != 0) || (item.Consecutivo == 0 && item.IdGrupo > 2)) {

                            if (item.Consecutivo == 0 && item.IdGrupo > 2) {
                                content += '</tbody></table>';
                                content += '<table class="table table-sm table-hover"> <thead> '
                            }

                            if (cons == 0) {
                                var titulos = [];
                                if (consEqui == item.Consecutivo) {
                                    if (tit_20 == "") titulos = tit_20.split("|");
                                    else titulos = tit_20.split("|");
                                    cons == 2;
                                }
                                else {
                                    if (consEqui != 0) {
                                        content += '</tbody></table>';
                                    }
                                    titulos = tit_15.split("|");
                                    consEqui = item.Consecutivo;
                                    content += '<table class="table table-sm table-hover"> <thead> ' +
                                                '<tr class="table-primary"><th colspan =8 > ' + tEquipo +' # ' + item.Consecutivo + '</th></tr>';
                                }

                                content +=
                                        '	<tr class="table-primary">' +
                                        '		<th class="text-center" width="5%" ></th>' +
                                        '		<th class="text-center" width="27%" >' + titulos[0] + '</th>' +
                                        '		<th class="text-center" width="15%" >' + titulos[1] + '</th>' +
                                        '		<th class="text-center" width="15%" >' + titulos[2] + '</th>' +
                                        '		<th class="text-center" width="15%" >' + titulos[3] + '</th>' +
                                        '		<th class="text-center" width="5%" >DESCUENTO</th>' +
                                '		<th class="text-center" width="15%" >' + titulos[5] + '</th>' +
                                '		<th class="text-center" width="3%" ></th>' +
                                        '	</tr>';

                                if (cons == 0) { content += '</thead><tbody>'; }

                                cons = 1;
                            }

                            if (!(item.Consecutivo == 0 && item.IdGrupo > 2)) {
                                if (item.Incluir == 1) {
                                    content += '<tr data-idCartaCierre="' + item.IdCartaCierre + '" data-cartacierre="' + item.item_id + '" data-icotiza="' + item.ItemCotiza_id + '" data-idet="' + item.Item_det + '" data-id-tipo-registro="' + item.TipoRegistro + '" data-id-grupo="' + item.IdGrupo + '" data-grupo1="' + item.Grupo1 + '" data-grupo2="' + item.Grupo2 + '" data-cons="' + item.Consecutivo + '"><td class="text-center incluir"><input type="checkbox" checked="checked" class="incluir" onchange="setValorConDescuentoEnCero(' + item.IdGrupo + ', \'' + item.Grupo1 + '\', \'' + item.Grupo2 + '\', this)"></td><td>';
                                } else {
                                    content += '<tr data-idCartaCierre="' + item.IdCartaCierre + '" data-cartacierre="' + item.item_id + '" data-icotiza="' + item.ItemCotiza_id + '" data-idet="' + item.Item_det + '" data-id-tipo-registro="' + item.TipoRegistro + '" data-id-grupo="' + item.IdGrupo + '" data-grupo1="' + item.Grupo1 + '" data-grupo2="' + item.Grupo2 + '" data-cons="' + item.Consecutivo + '"><td class="text-center incluir"><input type="checkbox" class="incluir" onchange="setValorConDescuentoEnCero(' + item.IdGrupo + ', \'' + item.Grupo1 + '\', \'' + item.Grupo2 + '\', this)"></td><td>';
                                }
                                if (item.Item_det.length == 0)
                                    content += item.Item;
                                else
                                    content += item.Item_det;
                                
                                if (item.CantidadOrig.toFixed(2) == 0)
                                    pCantidad ='';
                                else
                                    pCantidad = item.CantidadOrig.toFixed(2);

                                content += '</td>';
                                content += '<td class="text-center">' + item.UnidadMedida + ' </td>'
                                content += '<td class="text-right cantidad">' + pCantidad + '</td>'
                                content += '<td class="text-right valor">' + item.ValorOrig.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>'
                                content += '<td align="center" class="descuento"><input data-id-carta="' + item.IdCartaCierre + '" id="txdsc_oferta' + item.item_id + '" onblur="calcularDescuentoOfertaEconomica(this,' + item.item_id + ',' + item.Unitario + ', ' + item.IdGrupo + ', \'' + item.Grupo1 + '\', \'' + item.Grupo2 + '\')" class="form-control descuento NumeroSalcot" size="4" min="0" max="100" type="number" onkeydown="return event.key != \'Enter\';" value="' + item.Descuento + '" \></td>';
                                content += '<td class="text-right valorConDescuento" id="txtValorOferta-' + item.IdCartaCierre + '">' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>'
                                content += '<td></td>';
                                content += '</tr>';
                            }
                        }
                        
                    }
                }
                break;
            case 3:
                tabCot = tabCot + "<tr><td>" + item.Item +
                   "<td align ='right' id='precioVentaCotizadoSalidaCotNivel" + item.Item.match(/\d+/) + "'>" + formMon.format(item.Unitario) + "</td>" +
                   "</tr>";

                break;
            case 13:
                add_Resumen(1, item);
                tabCom = tabCom + "<tr><td>" + item.Item + "</td>" +
                    "<td align ='right'>" + formMon.format(item.Unitario) + "</td>" +
                    "<td align ='right'>" + (item.Descuento) + "%</td>" +
                    '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>' +
                    "</tr>";
                
                break;
            case 6:
                TerminoNeg = item.TerminoNegociacion
                tabCot = tabCot + "<tr><td>" + item.Item +
                    '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                 tabCom = tabCom + "<tr><td colspan = 3>" + item.Item + "</td>" +
                     '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                 break;
            case 7:
                // Se le calcula impuesto por defecto para colombia
                var vIva = parseFloat(item.Valor);
                if ($("#cboIdPais").val() == 8) {

                    var vIvacal = parseFloat($('#txTotA4').val()) * parseFloat(item.Impuesto);;
                    if (vIvacal > vIva) { vIva = vIvacal;}
                }
                tabCot = tabCot + "<tr><td>" + item.Item +
                         '<td align ="right" >' + vIva.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                tabCom = tabCom + "<tr><td colspan = 3>" + item.Item + "</td>" +
                         '<td align ="right" >' + vIva.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                break;
            case 11:
                tabCot = tabCot + "<tr><td>" + item.Item +
                    '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                tabCom = tabCom + "<tr><td colspan = 3>" + item.Item + "</td>" +
                    '<td align ="right" >' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + ' </td></tr>';
                break;

            case 4:
                if (item.Cantidad.toFixed(2) == 0)
                    pCantidad = '';
                else
                    pCantidad = item.Cantidad.toFixed(2);

                if (((item.Consecutivo != 0) || (item.Consecutivo == 0 && item.IdGrupo > 2)) && item.item_id != 135 ) {
                    content += '<tr class="table-secondary totalesOferta" data-grupo1="' + item.Grupo1 + '" data-grupo2="' + item.Grupo2 + '" data-id-tipo-registro="' + item.TipoRegistro + '" data-id-grupo="' + item.IdGrupo + '" data-cons="' + item.Consecutivo + '"><td Colspan = 3>' + item.Item + '</td>';
                    content += '<td class="text-right totalesOfertaCantidad">' + item.Cantidad.toFixed(2) + '</td>'
                    content += '<td class="text-right">' + item.ValorOrig.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>';
                    content += '<td></td>';
                    content += '<td class="text-right totalesOfertaValor">' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>';
                    content += '<td><button type="button" class="btn btn-sm btn-primary btnsGuardarCalculadora" onclick="GuardarOfertaEconomica(' + item.IdGrupo + ', \'' + item.Grupo1 + '\', \'' + item.Grupo2 + '\')"><i class="fa fa-save" style="margin-left: -200%;"></button></td>';
                    content += '</tr>';
                    var tempKeyTotal = item.IdGrupo + "-" + item.TipoRegistro + "-" + item.Grupo1 + "-" + item.Grupo2;
                    valoresTotalesOferta[tempKeyTotal] = item.Valor;
                    valoresInicialesTotaltesOferta[tempKeyTotal] = item.Valor;
                    CambioOferta[tempKeyTotal] = 1;
                }
                cons = 0;
                break;
            case 5:
                if ((item.Consecutivo != 0) || (item.Consecutivo == 0 && item.IdGrupo > 2)) {

                    if (item.Consecutivo != 0 && item.IdGrupo > 2 && cons < 2) {
                        content += '<tr class="table-secondary"><td colspan =8 > ' + tEquipo +' # ' + item.Consecutivo + '</td></tr>';
                        cons = 2;
                    }

                    if (item.Incluir == true) {
                        content += '<tr data-idCartaCierre="' + item.IdCartaCierre + '" data-cartacierre="' + item.item_id + '" data-icotiza="' + item.ItemCotiza_id + '" data-idet="' + item.Item_det + '" data-id-tipo-registro="' + item.TipoRegistro + '" data-id-grupo="' + item.IdGrupo + '" data-grupo1="' + item.Grupo1 + '" data-grupo2="' + item.Grupo2 + '" data-cons="' + item.Consecutivo + '"><td class="text-center incluir"><input type="checkbox" checked="checked" class="incluir" onchange="setValorConDescuentoEnCero(' + item.IdGrupo + ', \'' + item.Grupo1 + '\', \'' + item.Grupo2 + '\', this)"></td><td>';
                    } else {
                        content += '<tr data-idCartaCierre="' + item.IdCartaCierre + '" data-cartacierre="' + item.item_id + '" data-icotiza="' + item.ItemCotiza_id + '" data-idet="' + item.Item_det + '" data-id-tipo-registro="' + item.TipoRegistro + '" data-id-grupo="' + item.IdGrupo + '" data-grupo1="' + item.Grupo1 + '" data-grupo2="' + item.Grupo2 + '" data-cons="' + item.Consecutivo + '"><td class="text-center incluir"><input type="checkbox" class="incluir" onchange="setValorConDescuentoEnCero(' + item.IdGrupo + ', \'' + item.Grupo1 + '\', \'' + item.Grupo2 + '\', this)"></td><td>';
                    }

                    if (item.Item_det.length == 0)
                        content += item.Item;
                    else
                        content += item.Item_det;
                    if (item.CantidadOrig.toFixed(2) == 0)
                        pCantidad = '';
                    else
                        pCantidad = item.CantidadOrig.toFixed(2);

                    content += '</td>';
                    content += '<td class="text-center">' + item.UnidadMedida + ' </td>'
                    content += '<td class="text-right cantidad">' + pCantidad + '</td>'
                    content += '<td class="text-right valor">' + item.ValorOrig.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>'
                    content += '<td align="center" class="descuento"><input data-id-carta="' + item.IdCartaCierre + '" id="txdsc_oferta' + item.item_id + '" onblur="calcularDescuentoOfertaEconomica(this,' + item.item_id + ', ' + item.Unitario + ', ' + item.IdGrupo + ', \'' + item.Grupo1 + '\', \'' + item.Grupo2 + '\')" class="form-control descuento NumeroSalcot" size="4" min="0" max="100" type="number" onkeydown="return event.key != \'Enter\';" value="' + item.Descuento + '" \></td>';
                    content += '<td class="text-right valorConDescuento" id="txtValorOferta-' + item.IdCartaCierre + '">' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td>';
                    content += '<td></td>';
                    content += '<tr>';
                }
                break;
            case 8:
                content += '<div class="row nopadding" data-item="' + item.item_id + '" data-tipo="' + item.TipoRegistro + '" data-icotiza="' + item.ItemCotiza_id + '" data-idet="' + item.Item_det + '">';
                content += '<div class="col-9 nopadding Observacion "><label style="font-size: 10px;">' + item.Item + '</label><textarea class="form-control col-sm-12 Observacion " rows="5">' + item.Observacion + '</textarea></div>';
                content += cDiv;
                break;
            case 9:
                if (item.item_id == 113) {
                    tabCot = tabCot + "<tr class='table-secondary'><td>" + item.Item +
                             '<td align ="right" id="txTotA">' + item.Unitario.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td></tr>';
                }
                else{
                    tabCot = tabCot + "<tr class='table-secondary'><td>" + item.Item +
                             '<td align ="right" id="txTotA">' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td></tr>';
                }
                
                tabCom = tabCom + "<tr class='table-secondary'><td colspan = 3>" + item.Item + "</td>" +
                         '<td align ="right" id="txTotB">' + item.Valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }) + '</td></tr>';
                break;
            case 12:
                if (item.Item_det == "contenedor 20ST")
                    $('#txtContenedor20').val(item.Cantidad);
                else
                    $('#txtContenedor40').val(item.Cantidad);
                break;
            case 30:
                if ((item.item_id == 80) || (item.item_id == 118)) {
                    tabSug = tabSug + "<tr><td>" + item.Item +
                       "<td align ='right'>" + formMon.format(item.Unitario) + "</td>" +
                       "<td align ='right'>" + formMon.format(item.ValorMinimo) + "</td>" +
                       "<td hidden=hidden><input type=text hidden=hidden disabled=disabled value=" + ((item.DsctoMax.toFixed(1) < 0.1 || item.DsctoMax.toFixed(1) > 0.25) ? "1" : "0") + " id=txtRequireDiscountJustification /></td>" +
                       "<td align ='center' rowspan = '2' valign='middle' class=" + ((item.DsctoMax.toFixed(1) < 0.1 || item.DsctoMax.toFixed(1) > 0.25) ? "discountOutOfRanges" : "") + ">" + formPor.format(item.DsctoMax) + "</td>" +
                       "<td align ='center' rowspan = '2' valign='middle' >" + ((item.Cantidad != 0) ? item.Cantidad.toFixed(2) : "$0.00") + "</td>" +
                       "</tr>";
                    tabSug = tabSug + "<tr class= 'table-dark' ><td>" + item.Item + " (M2)" +
                           "<td align ='right'>" + ((item.Cantidad != 0) ? formMon.format(item.Unitario / item.Cantidad.toFixed(2)) : "$0.00") + "</td>" +
                           "<td align ='right'>" + ((item.Cantidad != 0) ? formMon.format(item.ValorMinimo / item.Cantidad.toFixed(2)) : "$0.00") + "</td>" +
                           "</tr>";
                    if (item.DsctoMax.toFixed(1) < 0.1 || item.DsctoMax.toFixed(1) > 0.25) {
                        DescuentoFueraRango = 1;
                    }

                }
                else {
                    tabSug = tabSug + "<tr><td>" + item.Item +
                       "<td align ='right' id=''>" + formMon.format(item.Unitario) + "</td>" +
                       "<td align ='right'>" + formMon.format(item.ValorMinimo) + "</td>" +
                       "<td align ='center'>" + formPor.format(item.DsctoMax) + "</td>" +
                       "<td align ='center'></td>" +
                       "</tr>";
                }
                break;
        }
    });

    if (tabCot.trim() == "") {
        tabCot = "<tr></tr>";
    }
    $("#tbodycot").html(tabCot);

    if (tabCom.trim() == "") {
        tabCom = "<tr></tr>";
    }

    $("#tbodycar").html(tabCom);

    // Se valida que para la carta manual se cargue la informacion de la salida de cotizacion
    if (CartaManual == 1) {
        tabSug == "";

    }
    if (tabSug.trim() == "") {
        tabSug = "<tr></tr>";
    }
    $("#tbodysug").html(tabSug);

    $("#DatosGen").append(content);
    calcular(4, 0);
    actualizarTotalGlobal();
//    calcular_flete_loc();

}

function add_Resumen(tipo, item){
    ix += 1;
    var linea = {
        tipo: tipo,
        id: ix,
        unitario: parseFloat(item.Unitario),
        descuento: parseFloat(item.Descuento),
        valor: parseFloat(item.Valor),
        itemid: item.item_id,
        tipo_reg: item.TipoRegistro,
        item_cotiza: item.ItemCotiza_id,
        item_det: item.Item_det,
        valorMinimo: parseFloat(item.ValorMinimo)
    };

    res.push(linea);
}

function setValorConDescuentoEnCero(idGrupo, grupo1, grupo2, input) {
    var trParent = $(input).parent().parent();
    if ($(input).is(":checked")) { 
        var itemValorUnitario = Number($($(trParent).children('.valor')).text().replace(/[^0-9.-]+/g, ''));
        itemValorUnitario = isNaN(itemValorUnitario) ? 0 : itemValorUnitario
        var discount = $($(trParent).children('.descuento').children('.descuento')).val();
        var newUnitValue = itemValorUnitario - (itemValorUnitario * (discount / 100));
        $($(trParent).children('.valorConDescuento')).text(newUnitValue.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
    } else {
        $($(trParent).children('.valorConDescuento')).text("0".toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
    }
    actualizarTotalesDescuentoOfertaEconomica(idGrupo, grupo1, grupo2);

}

function calcularDescuentoOfertaEconomica(input, idItem, itemValorUnitario, idGrupo, grupo1, grupo2) {
    var idCarta = $(input).data('id-carta');
    var discount = $(input).val();
    discount = parseFloat(discount);
    var incluir = $(input).parent().parent().children('.incluir').children('.incluir').is(":checked");
    if (discount != null && discount != undefined && !isNaN(discount) && incluir) {
        var newUnitValue = itemValorUnitario - (itemValorUnitario * (discount / 100));
        $(input).parent().parent().children('.valorConDescuento').text(newUnitValue.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
        actualizarTotalesDescuentoOfertaEconomica(idGrupo, grupo1, grupo2);
    }
}

function actualizarTotalGlobal() {
    var totalNiveles = 0;
    $.each(valoresTotalesOferta, function (key, value) {
        totalNiveles += value;
    });
    $("#totalTodosNiveles").html("Total de todos los niveles: " + totalNiveles.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
}

function activarDesactivarImpresionPorOfertaEconomica() {
    if (checkInitialValues()) {
        $("#btnImprimir").prop("disabled", false);
    } else {
        $("#btnImprimir").prop("disabled", true);
    }
}

function checkInitialValues() {
    var keys1 = Object.keys(CambioOferta);

    for (var i = 0; i < keys1.length; i++) {
        var key = keys1[i];

        if (CambioOferta[key] !== 1 ) {
            return false;
        }
    }

    return true;
}

function InitialValuesCambio() {
    var keys1 = Object.keys(CambioOferta);

    for (var i = 0; i < keys1.length; i++) {
        var key = keys1[i];
        CambioOferta[key] = 0;
    }

}


function actualizarTotalesDescuentoOfertaEconomica(idGrupo, grupo1, grupo2) {
    var tipoRegistro = 4; // Esto es porque los registro de totales en el SP son de tipo 4
    var cantidadTotal = 0;
    var valorTotal = 0;
    $("[data-cartacierre]").each(function (i, row) {
        var idGrupoRow = $(row).data('id-grupo');
        var grupo1Row = $(row).data('grupo1');
        var grupo2Row = $(row).data('grupo2');
        if (idGrupoRow == idGrupo && grupo1Row == grupo1 && grupo2Row == grupo2) {
            var incluir = $($(row).children('.incluir')).children('.incluir').is(":checked");
            if (incluir) {
                var cantidadRow = parseFloat($($(row).children('.cantidad')).text());
                cantidadRow = isNaN(cantidadRow) ? 0 : cantidadRow
                cantidadTotal += cantidadRow;
                var totalRow = Number($($(row).children('.valorConDescuento')).text().replace(/[^0-9.-]+/g, ''));
                totalRow = isNaN(totalRow) ? 0 : totalRow
                valorTotal += totalRow;
            }
        }
    });
    $(".totalesOferta").each(function (i, row) {
        var idGrupoTotal = $(row).data('id-grupo');
        var tipoRegistroRow = $(row).data('id-tipo-registro');
        var grupo1Row = $(row).data('grupo1');
        var grupo2Row = $(row).data('grupo2');
        if (idGrupoTotal == idGrupo && grupo1Row == grupo1 && grupo2Row == grupo2) {
            if (tipoRegistroRow == tipoRegistro) {
                $($(row).children('.totalesOfertaCantidad')).text(cantidadTotal.toFixed(2));
                $($(row).children('.totalesOfertaValor')).text(valorTotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
                var keyTotal = idGrupo + "-" + tipoRegistroRow + "-" + grupo1Row + "-" + grupo2Row;
                valoresTotalesOferta[keyTotal] = valorTotal;
                CambioOferta[keyTotal] = 0;
                actualizarTotalGlobal();
                activarDesactivarImpresionPorOfertaEconomica();
                return true;
            }
        }
    });
}

function calcular(tipo, indice) {
    var valor = 0;
    var total = 0;
    var totunit = 0;
    var totdesc = 0;

    if (tipo < 3) {
        var objIndex = res.findIndex((obj => obj.id == indice));

        if (tipo == 1 || tipo == 2) { totdesc = parseFloat($('#txdsc' + indice).val()); }
        if (tipo == 2) {            
            totdesc = (1 - (parseFloat($('#txvlr' + indice).val()) / parseFloat(res[objIndex].unitario))) * 100;
        }

        var multiplicando = 0;
        if (tipo == 1) {

            multiplicando = parseFloat(res[objIndex].unitario) + parseFloat(res[objIndex + 1].unitario);
        } else {
            multiplicando = parseFloat(res[objIndex].unitario);
        }

        valor = multiplicando * (1 - (parseFloat(totdesc) / 100));
        if (tipo == 2) {
            valor = parseFloat(res[objIndex].unitario - (parseFloat(res[objIndex].unitario * (totdesc / 100))));
        }
        /*DescuentoFueraRango = 0;
        if(valor < res[objIndex].valorMinimo)
        {
            toastr.error("El Valor no puede quedar por debajo del Valor Minimo: " + res[objIndex].valorMinimo, "FUP - Carta Cotizacion", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
            DescuentoFueraRango = 1;
        }*/
        res[objIndex].valor = valor;
        res[objIndex].descuento = totdesc;

        //$('#txdsc' + indice).val(res[objIndex].descuento.toFixed(2));
        $('#txvlr' + indice).html(res[objIndex].valor.toLocaleString('en-US', { style: 'currency', currency: 'USD' }));
    }
    else{
        switch (tipo) {
            case 3:
                valor = parseFloat($('#txfleA').val());
                $('#txfleB').val(valor);
                break;
            case 4:
                valor = parseFloat($('#txfleB').val());
                $('#txfleA').val(valor);
                break;
            case 5:
                valor = parseFloat($('#txivaA').val());
                $('#txivaB').val(valor);
                break;
            case 6:
                valor = parseFloat($('#txivaB').val());
                $('#txivaA').val(valor);
                break;
        }
    }
    
    total = 0;
    totdesc = 0;
    totunit = 0;
    res.forEach(function (it, i) {
        totunit += parseFloat(it.unitario);
        total += parseFloat(it.valor);
        totdesc += (parseFloat(it.unitario) * parseFloat(it.descuento) / 100);
    });

        $('#txTotA4').val(totunit.toFixed(2));
        $('#txTotB4').val(totunit.toFixed(2));

        $('#txdscA').val(totdesc.toFixed(2));
        $('#txdscB').val(totdesc.toFixed(2));

        total += parseFloat($('#txivaA').val()) + parseFloat($('#txfleA').val());

        $('#txTotA7').val(total.toFixed(2));
        $('#txTotB7').val(total.toFixed(2));
    
}


function guardar_flete(TipoFlete) {
    mostrarLoad();
    var objg = {};
    var isNew = 1;

    if (TipoFlete == 2) // Solo Calcular flete Local
    {
        ValFleteCal["puerto_origen_id"] = 224;
        ValFleteCal["puerto_destino_id"] = parseInt($("#cboIdCiudad").val());
        ValFleteCal["agente_carga_id"] = -1;
    }

    var param = {
        idFup: IdFUPGuardado,
        idVersion: $('#cboVersion').val(),
        flete: ValFleteCal
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

function AutorizarVerPrecio() {
    var param = {
        fupId: IdFUPGuardado,
        version: $("#cboVersion").val()
    }
    $.ajax({
        type: "POST",
        url: "FormSalidaCotizacion.aspx/AutorizarVerPrecio",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            toastr.success("Autorizar ver precio con Exito");
            var data = JSON.parse(result.d);
            if (data == true) {
                $("#BtnNegarVerPrecio").show();
                $("#BtnAutorizarVerPrecio").hide();
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Autorizar ver precio", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function NegarVerPrecio() {
    var param = {
        fupId: IdFUPGuardado,
        version: $("#cboVersion").val()
    }
    $.ajax({
        type: "POST",
        url: "FormSalidaCotizacion.aspx/NegarVerPrecio",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            toastr.success("Negar ver precio con Exito");
            var data = JSON.parse(result.d);
            if (data == true) {
                $("#BtnNegarVerPrecio").hide();
                $("#BtnAutorizarVerPrecio").show();
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Negar ver precio", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ObtenerLineasDinamicas(idVersion) {
    mostrarLoad();
    var param =
    {
        pFupID: IdFUPGuardado,
        pVersion: $('#cboVersion').val(), //versionFupDefecto,
        Idioma: idiomaSeleccionado
    };

    $.ajax({
        type: "POST",
        url: "FormSalidaCotizacion.aspx/ObtenerLineasDinamicas",
        data: JSON.stringify({ item: param }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            var data = JSON.parse(result.d);
            construirJson(data);

            CargarResumenOrden(IdFUPGuardado, idVersion);

            ocultarLoad();
            $(".ResumenSim").attr("style", "display:none");
            $(".PSugerido").attr("style", "display:none");
            $("#btnImprimir").attr("style", "display:none");
            $("#btnImprimirOrg").attr("style", "display:none");
            $("#btnGuardarCalc").attr("style", "display:none");
            $("#btnGuardarCot").attr("style", "display:none");
            $("#chxDetalle").attr("style", "display:none");
            $(".btnsGuardarCalculadora").attr("style", "display:none");
            if (CartaManual == 0) {
                $("#btnImprimir").attr("style", "display:normal");
                $("#btnImprimirOrg").attr("style", "display:normal");
                $("#btnGuardarCalc").attr("style", "display:normal");
                $("#btnGuardarCot").attr("style", "display:normal");
                $("#chxDetalle").attr("style", "display:normal");
                if (VersionCarta != 1) {
                    $(".btnsGuardarCalculadora").attr("style", "display:normal");
                }
            }
            if (["1", "2","9", "30", "24", "26", "39"].indexOf(RolUsuario) > -1) {
                $(".ResumenSim").attr("style", "display:normal");
                $(".PSugerido").attr("style", "display:normal");

                if (["1", "24", "26", "39"].indexOf(RolUsuario) > -1) {
                    $(".PSugeridobtn").attr("style", "display:normal");
                }
            }

            if ((EstadoFUP == "Aprobado")
			    && (["1", "24", "26", "33", "13"].indexOf(RolUsuario) > -1)) {
                if (CartaManual == 1) {
                    $("#divAdvertenciaCartaManual").show();
                    $("#divAdvertenciaCartaManual").html(CartaManualTexto);
                    $("#btnCotizar").hide();
                    $("#divAdvertenciaCartaManual").attr("style", "display:normal");
                }
                else {
                    $("#btnCotizar").attr("style", "display:normal");
                }
            };
            if ((EstadoFUP == "Cotizado")
                && (["1", "2", "3", "9", "30", "33", "34", "40","54"].indexOf(RolUsuario) > -1)){
                $(".btnCalcular").attr("style", "display:normal");
            };
            if (["1", "2", "5", "9", "24", "25", "26", "39"].indexOf(RolUsuario) > -1) {
                if (AutorizaVerPrecio) {
                    $(".PSugerido").attr("style", "display:normal");
                }
            };

        },
        error: function () {
            if (error.msg !="")
            {
                toastr.error("Failed to load dynamic controls", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        }
    });
}

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

                if (data != null) {
                    if (data.NoValido != 0) {
                        ValFleteCal = [];
                    } else {
                        ValFleteCal = data;
                        $('#txtContenedor20').val(ValFleteCal.cantidad_t1);
                        $('#txtContenedor40').val(ValFleteCal.cantidad_t2);
                        $('#vrFleteLocalTotal').val(ValFleteCal.vr_totalflete);
                    }
                } else {
                    toastr.error("Failed to Obtener Flete, data have a null value", "FUP");
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

function obtener_SalidaCot() {
    mostrarLoad();
    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val())
    };

    $.ajax({
        type: "POST",
        url: "FormSalidaCotizacion.aspx/ObtenerSalidaCot",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            ocultarLoad();
            if (result.d != null) {
                var data = JSON.parse(result.d);

                if (data != null && data.length > 0 ) {
                    ValSalidaCot = data;
                    $("#precioVentaCotizadoNivel1").html(formMon.format(parseFloat(ValSalidaCot[0].total_valor)));
                    $("#precioVentaCotizadoNivel2").html(formMon.format(ValSalidaCot[0].vlr_accesorios_basico));
                    $("#precioVentaCotizadoNivel3").html(formMon.format(ValSalidaCot[0].vlr_accesorios_complementario));
                    $("#precioVentaCotizadoNivel4").html(formMon.format(ValSalidaCot[0].vlr_accesorios_opcionales + ValSalidaCot[0].vlr_accesorios_adicionales));
                    $("#precioVentaCotizadoNivel5").html(formMon.format(ValSalidaCot[0].vlr_sis_seguridad));
                    $("#precioVentaCotizadoTotal").html(formMon.format(parseFloat(ValSalidaCot[0].total_propuesta_com)));
                }
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Obtener Salida Manual", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
};

function GuardarOfertaEconomica(idGrupo, grupo1, grupo2) {
    mostrarLoad();
    var data = [];
    $("[data-cartacierre]").each(function (i, row) {
        var idGrupoRow = $(row).data('id-grupo');
        var grupo1Row = $(row).data('grupo1');
        var grupo2Row = $(row).data('grupo2');
        if (idGrupoRow == idGrupo && grupo1Row == grupo1 && grupo2Row == grupo2) {
            var valores =
            {
                IdFUP: IdFUPGuardado,
                Version: $('#cboVersion').val(),
                item_id: $(row).attr('data-cartacierre'),
                Item_det: $(row).attr('data-idet'),
                ItemCotiza_id: $(row).attr('data-icotiza'),
                IdCartaCierre: $(row).attr('data-idCartaCierre'),
                Consecutivo: $(row).attr('data-Cons')
        }
            valores.Valor = $($(row).children('.valor')).text();
            valores.Valor = Number(valores.Valor.replace(/[^0-9.-]+/g, ''));
            valores.Descuento = $($(row).children('.descuento')).children('.descuento').val();
            if (valores.Descuento == null || valores.Descuento == undefined) {
                valores.Descuento = 0;
            }
            valores.Cantidad = $($(row).children('.cantidad')).text();
            valores.Cantidad = parseFloat(valores.Cantidad);
            if (valores.Cantidad == null || valores.Cantidad == undefined || isNaN(valores.Cantidad)) {
                valores.Cantidad = 0;
            }
            valores.Incluir = $($(row).children('.incluir')).children('.incluir').is(":checked");
            data.push(valores);
        }
    });

    $.ajax({
        type: "POST",
        url: "FormSalidaCotizacion.aspx/GuardarLineasDinamicas",
        data: JSON.stringify({ data: data }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            toastr.success('Guardado Correctamente.');
            obtener_resumen_calculadora();
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

function GuardarCot() {
    const rolesCotizador = [1, 24, 26];
    if ($("#txtRequireDiscountJustification").val() == 1) {
        if (rolesCotizador.indexOf(RolUsuario) != -1) {
            $("#btnContinueOperationCoti").attr("onclick", "ContinueSendAjaxGuardarCot()");
            $("#justifyDiscountModal").modal("show");
        } else {
            SendAjaxGuardarCot();
        }
    } else {
        SendAjaxGuardarCot();
    }
}

function SendAjaxGuardarCot() {
    mostrarLoad();
    var data = [];

    $("[data-item]").each(function (i, row) {
        var valores =
        {
            IdFUP: IdFUPGuardado,
            Version: $('#cboVersion').val(),
            item_id: $(row).attr('data-item'),
            Item_det: $(row).attr('data-idet'),
            ItemCotiza_id: $(row).attr('data-icotiza')
        }
        switch (parseInt($(row).attr("data-tipo"))) {
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
        url: "FormSalidaCotizacion.aspx/GuardarLineasDinamicas",
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
    /*if (DescuentoFueraRango == 1){
        EnviarNotificacion(77);
    }*/
    //if (val_flete != 0){
    //    guardar_flete(2);
    //}
}

function ContinueSendAjaxGuardarCot() {
    var discountJustification = $("#txtJustifyDiscountComment").val();
    if (discountJustification.length > 0) {
        var param =
        {
            IdFup: IdFUPGuardado,
            Version: $('#cboVersion').val(), //versionFupDefecto,
            Justificacion: discountJustification
        }
        /*$.ajax({
            type: "POST",
            url: "FormSalidaCotizacion.aspx/",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                ocultarLoad();
                toastr.success('Justificacion guardada Correctamente.');
            },
            error: function () {
                if (error.msg != "") {
                    ocultarLoad();
                    toastr.error("Failed to Process Justificacion", "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                }
            }
        }); */
        SendAjaxGuardarCot();
        EnviarNotificacion(77);
    } else {
        toastr.success('La justificación no puede quedar vacía');
    }
}

/* Inicio grupo de Cotizar */
function Cotizar() {
    const rolesCotizador = [1, 24, 26];
    if ($("#txtRequireDiscountJustification").val() == 1) {
        if (rolesCotizador.indexOf(RolUsuario) != -1) {
            $("#btnContinueOperationCoti").attr("onclick", "ContinueSendAjaxCotizar()");
            $("#justifyDiscountModal").modal("show");
        } else {
            SendAjaxCotizar();
        }
    } else {
        SendAjaxCotizar();
    }
}

function SendAjaxCotizar() {
    var param =
    {
        IdFup: IdFUPGuardado,
        Version: $('#cboVersion').val() //versionFupDefecto,
    }
    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "FormSalidaCotizacion.aspx/Cotizar",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            ocultarLoad();
            toastr.success('Salida Cotizacion cargada Correctamente.');
        },
        error: function () {
            if (error.msg != "") {
                ocultarLoad();
                toastr.error("Failed to Process Cotizar", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        }
    });
}

function ContinueSendAjaxCotizar() {
    var discountJustification = $("#txtJustifyDiscountComment").val();
    if (discountJustification.length > 0) {
        var param =
        {
            IdFup: IdFUPGuardado,
            Version: $('#cboVersion').val(), //versionFupDefecto,
            Justificacion: discountJustification
        }
        /*$.ajax({
            type: "POST",
            url: "FormSalidaCotizacion.aspx/",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                ocultarLoad();
                toastr.success('Justificacion guardada Correctamente.');
            },
            error: function () {
                if (error.msg != "") {
                    ocultarLoad();
                    toastr.error("Failed to Process Justificacion", "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                }
            }
        }); */
        SendAjaxCotizar();
        EnviarNotificacion(77);
    } else {
        toastr.success('La justificación no puede quedar vacía');
    }
}
/* Fin grupo de Cotizar */

function ImprimirSC(Original) {
    var idiomaCarta = "ES";
    if (parseInt(IdFUPGuardado) > 0) {
        var detallado = 0;
        idiomaCarta = idiomaSeleccionado.toUpperCase();

        if ($("#txtDetalle").prop("checked") == true || VersionCarta == 2) { detallado = 1 };
        if (VersionCarta == 2 && idiomaCarta == "BR") { idiomaCarta = "PT" };
        window.open("ReporteCartaCotiza.aspx" + "?IdFUP=" + IdFUPGuardado + "&VerFUP=" + $('#cboVersion').val() + "&Idioma=" + idiomaCarta.toUpperCase() + "&Detallado=" + detallado + " &CartaVersion=" + VersionCarta + "&SalidaOriginal=" + Original);
        if (DescuentoFueraRango == 1){
            EnviarNotificacion(77);
        }
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
            CreaOF = data.CreaOF;
            
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

function GuardarCalc() {
    EnviarNotificacion(109); //Enviar correo de Carta
    InitialValuesCambio();
    activarDesactivarImpresionPorOfertaEconomica();
    $("#btnImprimir").prop("disabled", false);

}

function EnviarNotificacion(IdEvento) {
    mostrarLoad();
    var param =
    {
        fup: IdFUPGuardado,
        version: $('#cboVersion').val(),
        pEvento: IdEvento,
        mensajeAdd: idiomaSeleccionado
    }

    $.ajax({
        type: "POST",
        url: "FormFUP.aspx/CorreoFUP",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (msg) {
            ocultarLoad();
        },
        error: function (e) {
            ocultarLoad();
            toastr.error("Failed Enviar Notificacion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function calcular_flete_loc() {

    mostrarLoad();
    var Caloto = 229; // para el flete local se calcula desde GUACHENE hasta la ciudad de la obra

    var param = {
        idFup: IdFUPGuardado,
        idVersion: $.trim($('#cboVersion').val()),
        idPtoCargue: Caloto,
        idPtoDescargue: parseInt($("#cboIdCiudad").val()),
        idTerminoNegociacion: parseInt(TerminoNeg),
        Cant1: parseInt($('#txtContenedor20').val()),
        Cant2: parseInt($('#txtContenedor40').val()),
        Cant3: 0,
        Cant4: 0,
        ValorCot: parseFloat($('#txTotA4').val())
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
                    ValFleteCal = data;
                    $('#txfleA').val(data.vr_totalflete);
                    $('#txfleB').val(data.vr_totalflete);
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

function doOnLoad() { }