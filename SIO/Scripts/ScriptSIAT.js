function asignarId() {
    document.getElementById('ContentPlaceHolder4_lblIdConta').value = document.getElementById('ContentPlaceHolder4_cboContacto').value;
    var combo = document.getElementById('ContentPlaceHolder4_cboContacto');
    document.getElementById('ContentPlaceHolder4_lblNomConta').value = combo.options[combo.selectedIndex].text;
}
function abrirPopup(ventana) {
    $('#fondoOsc').fadeIn('slow');
    $('#' + ventana).fadeIn('slow');
}
function cerrarPopup(popup) {
    $('#' + popup).fadeOut('slow');
    $('#fondoOsc').fadeOut('slow');
}
function bloqEsp(text, tecla) {
    $(text).keypress(function (e) {
        var code = null;
        code = (e.keyCode ? e.keyCode : e.which);
        return (code == tecla) ? false : true;
    });
}
function listaClienteSIAT(source, eventArgs) {
    document.getElementById('ContentPlaceHolder4_lblIdCliente').value = eventArgs.get_value();
}
function listaPais(source, eventArgs) {
    document.getElementById('ContentPlaceHolder4_lblIdPais').value = eventArgs.get_value();
}
function listaCiu(source, eventArgs) {
    document.getElementById('ContentPlaceHolder4_lblIdCiu').value = eventArgs.get_value();
}
function abrirContantos() {//popup para agregar el contacto
    var idcliente = document.getElementById('ContentPlaceHolder4_lblIdCliente').value;
    if (idcliente != '') {
        var opciones = "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no,  width=1242, height=973, top=85, left=140";
        window.open('Contacto.aspx?Modulo=SIAT&idCliente=' + idcliente, "", opciones);
    } else { alert('Por favor seleccione un cliente, gracias!!'); }
}
function abrirCliente() {//popup para agregar el contacto
    var opciones = "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no,  width=1242, height=973, top=85, left=140";
    window.open('Cliente.aspx', "", opciones);
}
function abrirObra() {//popup para agregar el contacto
    var idcliente = document.getElementById('ContentPlaceHolder4_lblIdCliente').value;
    if (idcliente != '') {
        var opciones = "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no,  width=1242, height=973, top=85, left=140";
        window.open('Obra.aspx?idClient=' + idcliente, "", opciones);
    } else { alert('Por favor seleccione un cliente, gracias!!'); }
}
function recargarComboCont(datos) {//con el opener.window llamo al metodo del asp padre
    document.getElementById('ContentPlaceHolder4_cboContacto').innerHTML = datos;
    var combo = document.getElementById('ContentPlaceHolder4_cboContacto');
    document.getElementById('ContentPlaceHolder4_lblIdConta').value = document.getElementById('ContentPlaceHolder4_cboContacto').value;
    document.getElementById('ContentPlaceHolder4_lblNomConta').value = combo.options[combo.selectedIndex].text;
}
function abrirVentana(dato) {//esta funcion me sirver para tomar el id de un div o otra etiqueta
    var idCompleto = $(dato).attr("id"); //tomo el id completo para empezar a separarlo
    var idSeparado = idCompleto.split('.');
    var titulo = document.getElementById('' + idCompleto + '').title; //tomo el title/titulo completo para empezar a separarlo
    if (idSeparado[0] == 'via') {
       // var tituloSep = titulo.split(')');
       // var paiCiuSep2 = tituloSep[1].split('-');
       // var estado = tituloSep[0];
       // var pais = paiCiuSep2[0];
       // var ciudad = paiCiuSep2[1];
        var idV = idSeparado[1];
        var parametros = { idVia: idV };
        var cliente = '';
        var obras = '';
        var ofs = '';
        var tecnicos = '';
        var dTotal = '';
        var dReal = '';
        var dInv = '';
        var dImp = '';
        var dPend = '';
        var pais = '';
        var ciudad = '';
        var estado = '';
        var cotizacion = '';
        var fechaInicio = '';
        var fechaFin = '';

        ejecutarAjax(parametros, 'cargaDatosViaje', function (result) {
            var data = result;
            var viaje = data.d;
            var tabla = '';
            $.each(viaje, function (index, viaje) {
                cliente = viaje.clientes;
                obras = viaje.obras;
                ofs = viaje.ofs;
                tecnicos = viaje.tecnicos;
                dTotal = viaje.dTotal;
                dReal = viaje.dReal;
                dInv = viaje.dInv;
                dImp = viaje.dImp;
                dPend = viaje.dPend;
                pais = viaje.pais;
                ciudad = viaje.ciudad;
                estado = viaje.estado;
                cotizacion = viaje.cotizacion;
                fechaInicio = viaje.fechaInicio;
                fechaFin = viaje.fechaFin;
                //construyo la tabla
                tabla = ' <table> <tr> <td> <div class="stylePromp">&nbsp;&nbsp;Cotización:&nbsp; ' + cotizacion + ' &nbsp;&nbsp;</div></br><div class="stylePromp">&nbsp;&nbsp;Clientes:&nbsp; ' + cliente + ' &nbsp;&nbsp;</div></br>'
                        + '<div class="stylePromp">&nbsp;&nbsp;Obras:&nbsp; ' + obras + ' &nbsp;&nbsp;</div></br><div class="stylePromp">&nbsp;&nbsp;OFs:&nbsp; ' + ofs + ' &nbsp;&nbsp;</div></br> '
                        + '<div class="stylePromp">&nbsp;&nbsp;Pais y Ciudad:&nbsp; ' + pais + '-' + ciudad + ' &nbsp;&nbsp;</div></br> <div class="stylePromp">&nbsp;&nbsp;Estado:&nbsp; ' + estado + ' &nbsp;&nbsp;</div></br> '
                        + '<div class="stylePromp">&nbsp;&nbsp;Tecnicos:&nbsp; ' + tecnicos + ' &nbsp;&nbsp;</div></br> '
                        + '<div class="stylePromp">&nbsp;&nbsp;Dias Totales:&nbsp; ' + dTotal + ' &nbsp;&nbsp;</div><div class="stylePromp">&nbsp;&nbsp;Dias Reales:&nbsp; ' + dReal + ' &nbsp;&nbsp;</div></br> '
                        + '<div class="stylePromp">&nbsp;&nbsp;Fecha Inicio:&nbsp; ' + fechaInicio + ' &nbsp;&nbsp;</div><div class="stylePromp">&nbsp;&nbsp;Fecha Fin:&nbsp; ' + fechaFin + ' &nbsp;&nbsp;</div></br> '
                        //+ '<div class="stylePromp">&nbsp;&nbsp;Dias de Inv.:&nbsp; ' + dInv + ' &nbsp;&nbsp;</div> <div class="stylePromp">&nbsp;&nbsp;Dias de Imp.:&nbsp; ' + dImp + ' &nbsp;&nbsp;</div>  <div class="stylePromp">&nbsp;&nbsp;Dias Pend.:&nbsp; ' + dPend + ' &nbsp;&nbsp;</div> ' 
                        + '</td></tr> </table> ';
            });
            cargarVentana(tabla); //cargo el prom dependiendo de lo que envien el la variable tabla que seria el html 
        });

        /*
        $.ajax({//llamo el metodo desde web service con ajax
        type: "POST", //Tipo de peticiòn
        url: "SiatPlaneacion.aspx/cargaDatosViaje", // Url y metodo que se invocae
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parametros),
        dataType: "json", //Tipo de dato con el que se realiza la llamada
        success: function (data) {
        var viaje = data.d;
        $.each(viaje, function (clave, viaje) {
        clientes = viaje.clientes;
        obras = viaje.obras;
        ofs = viaje.ofs;
        dTotal = viaje.dTotal;
        dReal = viaje.dReal;
        dInv = viaje.dInv;
        dImp = viaje.dImp;
        });
        //construyo la tabla
        var tabla = ' <table> <tr> <td> <div class="stylePromp">&nbsp;&nbsp;Clientes:&nbsp; ' + clientes + ' &nbsp;&nbsp;</div></br>'
        + '<div class="stylePromp">&nbsp;&nbsp;Obras:&nbsp; ' + obras + ' &nbsp;&nbsp;</div></br><div class="stylePromp">&nbsp;&nbsp;OFs:&nbsp; ' + ofs + ' &nbsp;&nbsp;</div></br> '
        + '<div class="stylePromp">&nbsp;&nbsp;Pais:&nbsp; ' + pais + ' &nbsp;&nbsp;</div></br><div class="stylePromp">&nbsp;&nbsp;Estado:&nbsp; ' + estado.replace('(', '') + ' &nbsp;&nbsp;</div> '
        + '<div class="stylePromp">&nbsp;&nbsp;Dias Toltales:&nbsp; ' + dTotal + ' &nbsp;&nbsp;</div> <div class="stylePromp">&nbsp;&nbsp;Dias Reales:&nbsp; ' + dReal + ' &nbsp;&nbsp;</div></br> '
        + '<div class="stylePromp">&nbsp;&nbsp;Dias de Inv.:&nbsp; ' + dInv + ' &nbsp;&nbsp;</div> <div class="stylePromp">&nbsp;&nbsp;Dias de Imp.:&nbsp; ' + dImp + ' &nbsp;&nbsp;</div></br> '
        + '</td></tr> </table> ';
        cargarVentana(tabla); //cargo el prom dependiendo de lo que envien el la variable tabla que seria el html 
        },
        error: function (XMLHttpRequest, textStatus, errorThrown)
        { alert('Error '); }
        });
        */

    } else if (idSeparado[0] == 'act') {
        var tabla = ' <table> <tr> <td> <div class="stylePromp">&nbsp;&nbsp;Observacion:&nbsp; ' + titulo + ' &nbsp;&nbsp;</div> </td></tr> </table> ';
        cargarVentana(tabla); //cargo el prom dependiendo de lo que envien el la variable tabla que seria el html 
    }
}
function cargarVentana(tabla) {//cargo el prom dependiendo de lo que envien en la variable tabla que seria el html 
    var temp = {
        state0: {
            html: tabla,
            buttons: { Cerrar: false },
            focus: 1,
            position: { container: '#elId', x: -300, y: -45, width: 250, arrow: 'rm' },
            submit: function (e, v, m, f) { }
        }
    };
    $.prompt(temp);
    $(".jqi").css({ "width": "400px", "left": "580px", "top": "260px" });
}
//selecionar fila para cambiar el color
function selFilaCamColor(dato) {
    $(".FilaCamColor").css("background", "transparent");
    var id = $(dato).attr('id');
    document.getElementById(id).style.background = '#1fa0e4';
}

//Metodo general para ejecutar las consultas por medio del ajax
function ejecutarAjax(parametros, metodoWeb, callback) {
    $.ajax({
        type: "POST", //Tipo de peticiòn
        url: "WSSIO.asmx/" + metodoWeb + "", // Url y metodo que se invocae
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parametros),
        dataType: "json", //Tipo de dato con el que se realiza la llamada
        success: function (data) {
            callback(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown)
        { alert('Error '); }
    });
}
