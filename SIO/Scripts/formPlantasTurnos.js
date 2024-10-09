var formatDateString = "DD/MM/YYYY";
var formatDateStringSet = 'YYYY-MM-DD';
var formatDateDIVString = "YYYYMMDD"
var plan = [];
var plantas = [];
var resumen = [];
var daysOfWeekSTR = [7, 1, 2, 3, 4, 5, 6];
var NombresDias = ['Dom', 'Lun', 'Martes', 'Mier', 'Juev', 'Vier', 'Sab', 'Dom']
var dtOrd = [];

$(document).ready(function () {
    //Inicializamos variables
    ObtenerPlantas();

    var date = new Date();
    var EndDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    var StartDate = new Date(date.getFullYear(), date.getMonth(), 1);
    $("#txtfi").val(moment(StartDate).format(formatDateStringSet));
    $("#txtff").val(moment(EndDate).format(formatDateStringSet));
    $("#btnInsertar").hide();

    $("#btnConsultar").click(function (event) {
        paintDays();
        Resumen();
        $("#btnInsertar").show();
        cargarOrdenes();
    });

    $("#btnGuardarPlan").click(function (event) {
        event.preventDefault();
        GuardarPlan();
    });

    $("#btnInsertar").click(function (event) {
        event.preventDefault();
        InfoCrear();
    });

    $('.select-filter').select2();

    $(".sfmodal").select2({
        dropdownParent: $("#ModCreacionPlan")
    });

    $("#btnCrearPlan").click(function (event) {
        event.preventDefault();
        PlanearOrden();
    });

    $("#btnGuardarDat").click(function (event) {
        event.preventDefault();
        guardarDat();
    });

    $("#btnBorraDat").click(function (event) {
        event.preventDefault();
        eliminarDat();
    });
});

function GuardarPlan() {
    var fechaG = $("#txtFechaChange").val();
    var idSelPlan = Number($("#hdnIDPlan").val());
    var cantidad = Number($("#txtCantidadChange").val());
    let obj = plan.find(o => o.ppf_id === idSelPlan);

    var param =
    {
        Inicio: fechaG,
        PlanId: idSelPlan,
        Cant: cantidad,
        IdEntrada: obj.EntradaCotId,
        IdOfa: obj.Idofa
    };

    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "PlantasTurnos.aspx/guardarPlan",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (result) {
            plan = JSON.parse(result.d);
            ocultarLoad();
            var modal = $("#ModCreacionCronograma");
            modal.modal('hide');
            paintDays();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load Plan Produccion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function ValidateSumDia(idPlan, fecha) {
    let obj = plan.find(o => o.ppf_id === idPlan);
    let fechaGuardarPlan = moment(fecha, formatDateStringSet);
    const strDiaCompra = fechaGuardarPlan.format(formatDateStringSet);

    var dayPlans = plan.filter(p => p.Fecha.substring(0, 10) == strDiaCompra);
    const MaxValueTotal = dayPlans[0].Metro;
    let sumTotalCant = obj.M2Producir;
    dayPlans.forEach(function (x) {
        if (x.ppf_id != 0) {
            sumTotalCant += x.M2Producir;
        }
    });

    if (sumTotalCant > MaxValueTotal) {
        return false;
    }
    else {
        return true;
    }
}

function dragDrop() {
    $(".divProyecto").draggable({
        helper: 'clone'
    });
    $(".flex-fill").droppable({
        drop: function (event, ui) {
            debugger;
            let id = Number($(ui.draggable).attr('id').replace("did-", ""));
            let fecha = $(this).attr('id').replace("divD-", "")

            if (!ValidateSumDia(id, fecha)) {
                toastr.error("Supera el maximo M2 permitidos", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
//                alert("Supera el maximo permitido")
            }

            ShowOptionModal(id, fecha);
            //ui.draggable.detach();
            //$(this).append(ui.draggable);
        },
        over: function (event, ui) {
            // $(this).css('background', 'orange');
        },
        out: function (event, ui) {
            // $(this).css('background', 'cyan');
        }
    });
}


function ShowOptionModal(idPlan, fecha) {
    let obj = plan.find(o => o.ppf_id === idPlan);
    console.log(obj);
    let fechaGuardarPlan = moment(fecha, formatDateStringSet);
    var modal = $("#ModCreacionCronograma");
    modal.find('.modal-title').text("Orden - " + obj.ofa);
    $("#txtMoFechaChange").val(obj.Fecha.substring(0, 10));
    $("#txtMoFupChange").val(obj.Fup.toString().trim() + " " + obj.Vers);
    $("#txtMoM2PlanChange").val(obj.M2Producir);
    $("#txtMoTipoCotChange").val(obj.TipoCotizacion);
    $("#txtMoProyChange").val(obj.TipoProyecto);
    $("#txtMoM2CerrChange").val(obj.m2Cerrados);
    $("#txtMoClienteChange").val(obj.Cliente);
    $("#txtMoObraChange").val(obj.Obra);
    $("#txtCantidadChange").val(obj.M2Producir);
    $("#txtFechaChange").val(fechaGuardarPlan.format(formatDateStringSet));
    $("#hdnIDPlan").val(idPlan);
    modal.modal('show');
}

function paintDays() {
    var fi = $("#txtfi").val();
    var ff = $("#txtff").val();
    var plantaID = $("#selectPlanta").val();
    if (fi.length > 0 && ff.length > 0) {
        mostrarLoad();
        var param =
        {
            PlantaID: plantaID,
            Inicio: fi,
            Fin: ff,
        };

        $.ajax({
            type: "POST",
            url: "PlantasTurnos.aspx/cargarPlan",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (result) {
                plan = JSON.parse(result.d);
                ocultarLoad();
                CrearControles();
            },
            error: function () {
                ocultarLoad();
                toastr.error("Failed to load Plan Produccion", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
    };
}

function weekOfMonth(date) {
    var dates = date.clone();
    return Math.ceil((dates.date() + (daysOfWeekSTR[dates.startOf('month').isoWeekday()] - 1)) / 7);
}

function selectPlan(idPlan) {
    let obj = plan.find(o => o.ppf_id === idPlan);
    ofa = obj.ofa.trim();
    $(".divProyecto").removeClass("divProyectoSelected");

    $(".divOfa").each(function () {
        if ($(this).text().trim() == ofa.trim()) {
            $(this).parent().parent().addClass("divProyectoSelected");
        }
    });
}

function CrearControles() {

    $("#divHeadSemCalendar").html("");
    $("#divHeaderCalendar").html("");
    $("#divContentCalendar").html("");

    var htmlString = "";
    var htmlContent = "";
    var htmlSemanas = "";

    let vSemanaMes = "";
    let vdia = "";
    let pr = "";

    var fi = $("#txtfi").val();
    var ff = $("#txtff").val();
    let Di = moment(fi, formatDateStringSet);
    let Df = moment(ff, formatDateStringSet);
    const difD = Df.diff(Di, 'days') + 1;
    let weeksDays = [];
    for (let i = 0; i < difD; i++) {
        const strDia = Di.format(formatDateString);
        weeksDays.push(weekOfMonth(Di));
        var NombreDiaSemana = NombresDias[Di.isoWeekday()];
        const strDiaDiv = Di.format(formatDateDIVString);
        const strDiaCompra = Di.format(formatDateStringSet);

        var dayPlans = plan.filter(p => p.Fecha.substring(0, 10) == strDiaCompra);
        let MetrosMax = 0;
        if (dayPlans.length > 0) {
            MetrosMax = dayPlans[0].Metro;
        }

        var HechoDiaPlan = 0;
        dayPlans.forEach(function (x) {
            if (x.ppf_id != 0) {
                HechoDiaPlan += x.M2Producir;
            }
        });

        htmlString += '<div class="flex-fill" style="' + (HechoDiaPlan > MetrosMax ? 'border:1px solid red !important' : '') + '">' + NombreDiaSemana + "&nbsp;" + strDia + ' <br/> ' + MetrosMax + ' M2 / ' + parseFloat(HechoDiaPlan).toFixed(2) + ' M2 </div>';

        var stringDayPlan = dayPlans.map(function (p) {
            if (p.Obra.substring(0, 10) == "APARTAMENT") {
                console.log(p);
            }
            return (p.ppf_id == 0 ? '' : '<div class="divProyecto card" onclick="selectPlan(' + p.ppf_id + ')" id="did-' + p.ppf_id + '"><div class="card-body" >' +
                '<h8 class="card-title divOfa">' + p.ofa.trim() + '</h8>' +
                '<button id="btnResp' + p.ppf_id + '" onclick="InfoShow(' + p.ppf_id + ')" type="button" class="btn btn-sm"><i class="fa fa-comments"></i></button>' +
                '<p class="card-text mb-0">' + p.TipoProyecto + '</p><p class="card-text mb-0">' + p.Cliente.substring(0, 10) + '</p> <p class="card-text mb-0">' + p.Obra.substring(0, 10) + '</p>' +
                '<p class="card-text mb-0">' + p.m2Cerrados + '</p> <p class="card-text mb-0" style="background-color:' + p.Color + ';" >' + p.M2Producir + '</p>' +
                ((p.CerradoIngenieria == "SI") ? ((p.IngM2Modulados > p.M2Sf) ? '<div class="mt-1" data-toggle="tooltip" data-placement="bottom" title="Mas metros de ingeniería"><i class="fa fa-arrow-up" style="color: red;"></i></div>' : (p.IngM2Modulados < p.M2Sf ? '<div class="mt-1" data-toggle="tooltip" data-placement="bottom" title="Menos metros de ingeniería"><i class="fa fa-arrow-down" style="color: red;"></i></div>' : '')) : '') +
                ((p.Validacion == 1 || p.Prioridad == 1 || p.Curado == 1) ? '<p class="card-text mb-0">'
                      + (p.Validacion == 1 ? '<div data-toggle="tooltip" data-placement="bottom" title="Validacion"><i class="fa fa-eye"></i> ' + p.FechaValidacion.substring(0, 10) + '</div>' : '')
                      + (p.Prioridad == 1 ? '<div class="text-danger" data-toggle="tooltip" data-placement="bottom" title="Prioridad"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i></div>' : '')
                      + (p.Curado == 1 ? '<div data-toggle="tooltip" data-placement="bottom" title="Curado"><i class="fa fa-paint-brush"></i></div>' : '')
                      + (p.MarcaQR == 1 ? '<div data-toggle="tooltip" data-placement="bottom" title="Marcacion QR"><i class="fa fa-qrcode"></i></div>' : '')
                + '</p>' : '') + '</div></div>');
        }); 
        let stringPlansCalendar = "";
        if (stringDayPlan.length > 0) {
            stringPlansCalendar = stringDayPlan.join(" ");
        }

        htmlContent += '<div id="divD-' + strDiaDiv + '"  class="flex-fill d-flex flex-column align-items-center">' + stringPlansCalendar + '</div>';
        Di = Di.add(1, 'd');
    }
    let counts = {};
    weeksDays.forEach(function (x) { counts[x] = (counts[x] || 0) + 1; });

    let divsSemana = Object.entries(counts).map(item => {
        const anchoSem = 170 * item[1];
        return '<div class="flex-fill align-items-center" style="width:' + anchoSem + '%" > ' + item[0] + ' </div>';
    });

    if (divsSemana.length > 0) {
        htmlSemanas = divsSemana.join(" ");
    }

    $("#divHeaderCalendar").html(htmlString);
    $("#divContentCalendar").html(htmlContent);
    $("#divHeadSemCalendar").css("width", String(difD * 170) + 'px');
    $("#divHeadSemCalendar").html(htmlSemanas);
    $("#divHeadSemCalendar").css("width", String(difD * 170) + 'px');
    dragDrop();
}

function ObtenerPlantas() {
    $.ajax({
        type: "POST",
        url: "PlantasTurnos.aspx/cargarPlanta",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        success: function (result) {
            plantas = JSON.parse(result.d);
            var stringPlantas = "";
            for (var k = 0; k < plantas.length; k++) {
                stringPlantas += "<option value='" + plantas[k].id + "'>" + plantas[k].descripcion + "</option>";
            }
            $("#selectPlanta").html(stringPlantas);
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load Plan Produccion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function doOnLoad() { }

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

function InfoShow(idPlan) {
    let obj = plan.find(o => o.ppf_id === idPlan);
    console.log(obj);
    var modal = $("#ModInformacion");
    modal.find('.modal-title').text("Informacion Orden - " + obj.ofa);
    
    $("#txtPrioridad").prop("checked", false);
    $("#txtCurado").prop("checked", false);
    $("#txtQR").prop("checked", false);
    $("#txtFecVal").val("");
    $("#IdPlan").val(idPlan);
    $("#txtMoFecha").val(obj.Fecha.substring(0, 10));
    $("#txtMoFup").val(obj.Fup.toString().trim() + " " + obj.Vers);
    $("#txtMoM2Plan").val(obj.M2Producir);
    $("#txtMoTipoCot").val(obj.TipoCotizacion);
    $("#txtMoProy").val(obj.TipoProyecto);
    $("#txtMoM2Cerr").val(obj.m2Cerrados); 
    $("#txtMoM2Sf").val(obj.M2Sf); 
    $("#txtMoM2Ingenieria").val(obj.IngM2Modulados);
    $("#txtMoCliente").val(obj.Cliente);
    $("#txtPendEmba").val(obj.PendEmba);
    $("#txtMoObra").val(obj.Obra);
    $("#txtPendIng").val(obj.PendIeng); 
    $("#txtPendProd").val(obj.PendProdTer);
    $("#txtCerrIng").val(obj.CerradoIngenieria);
    $("#txtPorcOrd").val(parseFloat(obj.PorcOrden * 100).toFixed(2));
    if (obj.FechaValidacion.substring(0, 10) != "1900-01-01") {
        $("#txtFecVal").val(obj.FechaValidacion.substring(0, 10));
    }
    if (obj.Prioridad == true){
        $("#txtPrioridad").prop("checked", true);
    }
    if (obj.ProdFechaIni.substring(0, 10) != "1900-01-01") {
        $("#txtFecVal").val(obj.ProdFechaIni.substring(0, 10));
    }
    $("#txtMoM2Prod").val(obj.ProdM2Produccion);
    if (obj.Curado == 1) {
        $("#txtCurado").prop("checked", true);
    }
    if (obj.MarcaQR == 1) {
        $("#txtQR").prop("checked", true);
    }

    modal.modal('show');
}

function InfoCrear() {

    var modal = $("#ModCreacionPlan");
    modal.find('.modal-title').text("Planear Orden Nueva " );
    $("#txtPrioridad").prop("checked", false);
    $("#txtFechaInicio").val("");
    modal.modal('show');
}


function Resumen() {
    var fi = $("#txtfi").val();
    var ff = $("#txtff").val();
    var plantaID = Number($("#selectPlanta").val());

    var param =
    {
        PlantaID: plantaID,
        Inicio: fi,
        Fin: ff
    };

    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "PlantasTurnos.aspx/cargarResumen",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            resumen = JSON.parse(result.d);
            $.each(resumen, function (index, elem) {
                if (index == 'TipoCotizacion') {
                    llenarResumen(resumen.TipoCotizacion, "#tbTipoCbody")
                }
                if (index == 'Producto') {
                    llenarResumen(resumen.Producto, "#tbProdbody")
                }
                if (index == 'Semana') {
                    llenarResumen(resumen.Semana, "#tbSembody")
                }
            });
            ocultarLoad();
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to load Resumen Produccion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function llenarResumen(data, idtabla) {
    var rowsgrupo = "";
            $.each(data, function (index, elem) {
                rowsgrupo = rowsgrupo + "<tr><td>" + elem.Grupo + "</td>" +
                            '<td align="center" >' + elem.Proyectos + "</a></td>" +
                            '<td align="right" >' + elem.M2Sf + "</td>" +
                            '<td align="center" >' + elem.PorcTotal + "</td>" +
                            "</tr>";
            });
            if (rowsgrupo.trim() == "") {
                rowsgrupo = "<tr></tr>";
            }
            $(idtabla).html(rowsgrupo);
}

function cargarOrdenes() {
    mostrarLoad();

    var param =
    {
        Inicio: $("#txtfi").val(),
        AreadId : Number($("#selectPlanta").val())
    };

    $.ajax({
            type: "POST",
            url: "PlantasTurnos.aspx/cargarOrdenes",
            data: JSON.stringify(param),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            // async: false,
            success: function (result) {
                Ordenes = JSON.parse(result.d);
                $.each(Ordenes, function (index, elem) {
                    if (index == 'Lista') {
                        $("#ListaOrden").get(0).options.length = 0;
                        $("#ListaOrden").get(0).options[0] = new Option("Seleccionar", "-1");

                        for (var k = 0; k < Ordenes.Lista.length; k++) {
                            $("#ListaOrden").get(0).options[$("#ListaOrden").get(0).options.length] = new Option(Ordenes.Lista[k].descripcion, Ordenes.Lista[k].id);
                        }
                    }
                    if (index == 'Datos') {
                        dtOrd = index.Datos;
                    }
                });
                ocultarLoad();
            },
            error: function () {
                ocultarLoad();
                toastr.error("Failed to load Ordenes Sin planear", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        });
}

function guardarDat() {
    var prio = $("#txtPrioridad").prop("checked");
    var prioridad = 0
    if (prio) { prioridad = 1}

    var param =
    {
        Idplan: Number($("#IdPlan").val()),
        prioridad: prioridad,
        FecValida: $("#txtFecVal").val()
    };

    mostrarLoad();
    $.ajax({
        type: "POST",
        url: "PlantasTurnos.aspx/valpri",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(param),
        // async: false,
        success: function (result) {
            let res = 0;
//            res = JSON.parse(result.d);
            ocultarLoad();
            if (res == -1) {
                toastr.error("Failed to Actualizar Prioridad - Validacion", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
            else {
                $("#btnConsultar").click();
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Actualizar Prioridad - Validacion", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}


function eliminarDat() {
    var param =
    {
        PlanId: Number($("#IdPlan").val())
    };

    mostrarLoad();

    $.ajax({
        type: "POST",
        url: "PlantasTurnos.aspx/BorrarPlanId",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(param),
        // async: false,
        success: function (result) {
            let res = 0;
            let res1 = JSON.parse(result.d);
            ocultarLoad();
            if (res == -1) {
                toastr.error("Failed to Borrar Planeado", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
            else {
                $("#btnConsultar").click();
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Borrar Planeado", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}


function PlanearOrden() {
    var param =
    {
        IdOfa : Number($("#ListaOrden").val()), 
        Inicio : $("#txtFechaInicio").val(), 
        PlantaId :Number($("#selectPlanta").val())
    };

    mostrarLoad();

    $.ajax({
        type: "POST",
        url: "PlantasTurnos.aspx/CrearPlanId",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(param),
        // async: false,
        success: function (result) {
            let res = 0;
//            res = JSON.parse(result.d);
            ocultarLoad();
            if (res == -1) {
                toastr.error("Failed to Crear Plan Orden", "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
            else {
                $("#btnConsultar").click();
            }
        },
        error: function () {
            ocultarLoad();
            toastr.error("Failed to Crear Plan Orden", "FUP", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}