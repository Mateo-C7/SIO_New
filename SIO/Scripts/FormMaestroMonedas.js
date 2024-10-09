//*****************
//Global Variables
//****************
var DataTable_Money = null;

$(document).ready(function () {
    getDataMoney();
    fillMonthSelect();
    fillYearSelect();
    fillMoneySelect();
});

function fillMoneySelect() {
    var data = getDataCurrencies();
    if (data != null) {
        $("#modalCreateAndUpdateMoneyCmb").html("");
        $(data).each(function (i, r) {
            $("#modalCreateAndUpdateMoneyCmb")
                .append('<option value="' + r.MonedaId + '">' + r.MonedaDescripcion + '</option>');
        });
    }
}

function fillMonthSelect() {
    var data = getDataMonth();
    if (data != null) {
        $("#modalCreateAndUpdateMoneyMesCmb").html("");
        $(data).each(function (i, r) {
            $("#modalCreateAndUpdateMoneyMesCmb")
                .append('<option value="' + r.MesId + '">' + r.MesNombre + '</option>');
        });
    }
}

function fillYearSelect() {
    var data = getDataYear();
    if (data != null) {
        $("#modalCreateAndUpdateMoneyAnioCmb").html("");
        $(data).each(function (i, r) {
            $("#modalCreateAndUpdateMoneyAnioCmb")
                .append('<option value="' + r.Anio + '">' + r.Anio + '</option>');
        });
    }
}

function getDataMonth() {
    var dataRet = null;
    $.ajax({
        type: "POST",
        url: "FormMaestroMonedas.aspx/getMonths",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        beforeSend: function () {
            sLoading();
        },
        success: function (data) {
            hLoading();
            dataRet = JSON.parse(data.d);
        },
        error: function (err) {
            toastr.error(err.statusText, "Maestro Monedas Meses", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
    return dataRet;
}

function getDataCurrencies() {
    var dataRet = null;
    $.ajax({
        type: "POST",
        url: "FormMaestroMonedas.aspx/getActiveCurrencies",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        beforeSend: function () {
            sLoading();
        },
        success: function (data) {
            hLoading();
            dataRet = JSON.parse(data.d);
        },
        error: function (err) {
            toastr.error(err.statusText, "Maestro Monedas Monedas", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
    return dataRet;
}

function getDataYear() {
    var dataRet = null;
    $.ajax({
        type: "POST",
        url: "FormMaestroMonedas.aspx/getYears",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        beforeSend: function () {
            sLoading();
        },
        success: function (data) {
            hLoading();
            dataRet = JSON.parse(data.d);
        },
        error: function (err) {
            toastr.error(err.statusText, "Maestro Monedas Años", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
    return dataRet;
}

function getDataMoney() {
    $.ajax({
        type: "POST",
        url: "FormMaestroMonedas.aspx/getDataMoney",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        // async: false,
        beforeSend: function () {
            sLoading();
        },
        success: function(data) {
            hLoading();
            createMoneyDataTableObject(JSON.parse(data.d));
        },
        error: function (err) {
            toastr.error(err.statusText, "Maestro Monedas", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function fillDataTableMoney(data) {
    $("#data-money-tbody").html("");
    $.each(data, function (index, moneyData) {
        var row = "<tr>";
        row += "<td>" + moneyData.MonedaDescripcion + "</td>";
        row += "<td>" + moneyData.MonedaAnio + "</td>";
        row += "<td>" + moneyData.MonedaMes + "</td>";
        row += "<td>" + moneyData.MonedaTrmValor + "</td>";
        row += "<td>" + moneyData.MonedaTrmPeriodo + "</td>";
        row += "<td>" + moneyData.MonedaFechaRegistro.substring(0, 10) + "</td>";
        row += "<td>" + moneyData.MonedaTrmUsuario + "</td>";
        row += "<td><button class=\"btn btn-sm btn-outline-dark text-center\" type=\"button\" onclick=\"fillAndShowUpdateMoneyModal(";
        row += "'" + moneyData.MonedaTrmId + "','" + moneyData.MonedaId + "','" + moneyData.MonedaAnio + "','";
        row += moneyData.MonedaMes + "','" + moneyData.MonedaTrmValor + "','" + moneyData.MonedaTrmPeriodo + "','";
        row += moneyData.MonedaFechaRegistro.substring(0, 10) + "','" + "1'";
        row += ")\"><i class=\"fa fa-edit\" style=\"text-align: center !important; width:90% !important\"></i></button></td>";
        row += "</tr>";
        $("#data-money-tbody").append(row);
    });
}

function fillAndShowUpdateMoneyModal(id, monedaId, anio, mes, valor, periodo, fecha, modo) {
    if (modo == "1") {
        $("#modalCreateAndUpdateMoney-title").html("Actualizar moneda");
        $("#modalCreateAndUpdateMoneyCmb").val(monedaId);
        $("#modalCreateAndUpdateMoneyCmb").attr("disabled", "disabled");
        $("#modalCreateAndUpdateMoneyMoneyId").val(id);
        $("#modalCreateAndUpdateMoneyAnioCmb").val(anio);
        $("#modalCreateAndUpdateMoneyAnioCmb").attr("disabled", "disabled");
        $("#modalCreateAndUpdateMoneyMesCmb").val(mes);
        $("#modalCreateAndUpdateMoneyMesCmb").attr("disabled", "disabled");
        $("#modalCreateAndUpdateMoneyValorTRMtxt").val(valor);
        $("#modalCreateAndUpdateMoneyBtnSubmit").html("Actualizar moneda");
        $("#modalCreateAndUpdateMoney").modal('show');
    } else {
        $("#modalCreateAndUpdateMoney-title").html("Crear moneda");
        $("#modalCreateAndUpdateMoneyBtnSubmit").html("Crear moneda");
        $("#modalCreateAndUpdateMoneyCmb").removeAttr("disabled");
        $("#modalCreateAndUpdateMoneyAnioCmb").removeAttr("disabled");
        $("#modalCreateAndUpdateMoneyMesCmb").removeAttr("disabled");
        $("#modalCreateAndUpdateMoneyValorTRMtxt").val(0);
        $("#modalCreateAndUpdateMoneyMoneyId").val(0);
        $("#modalCreateAndUpdateMoney").modal('show');
    }
}

function CreateUpdateMoney() {
    var objg = {
        MonedaTrmId: $("#modalCreateAndUpdateMoneyMoneyId").val(),
        MonedaId: $("#modalCreateAndUpdateMoneyCmb").val(),
        MonedaTrmValor: $("#modalCreateAndUpdateMoneyValorTRMtxt").val(),
        MonedaAnio: $("#modalCreateAndUpdateMoneyAnioCmb").val(),
        MonedaMes: $("#modalCreateAndUpdateMoneyMesCmb").val()
    };
    var param = {
        item: objg
    };
    $.ajax({
        type: "POST",
        url: "FormMaestroMonedas.aspx/CreateUpdateCurrency",
        data: JSON.stringify(param),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        beforeSend: function () {
            sLoading();
        },
        success: function (data) {
            hLoading();
            getDataMoney();
            $("#modalCreateAndUpdateMoney").modal('hide');
            toastr.success("Success!", "Maestro Monedas Create/Update", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        },
        error: function (err) {
            toastr.error(err.statusText, "Maestro Monedas Create/Update", {
                "timeOut": "0",
                "extendedTImeout": "0"
            });
        }
    });
}

function createMoneyDataTableObject(data) {
    if (DataTable_Money != null) {
        DataTable_Money.clear().destroy();
        DataTable_Money = null;
    }
    fillDataTableMoney(data);
    DataTable_Money = $("#table-money").DataTable({
        order: []
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