/*!
* Logica del calendario
* Contiene todas las funciones necesarias para el funcionamiento del calendario, este codigo fue realizado completamente sin ayuda de terceros.
* Realizado por:  Jorge Andres Cardona M. - Metrolink
* Para: FORSA
* Este codigo fue desarrollado para el uso del modulo de visitas comerciales, para aplicativo del SIO
* Si se va hacer algun cambio por favor seguir las normas del codigo, comentar y agregar las funciones por debajo de las demas, para asi seguir la secuencia
*/
function ValidaSoloNumeros() {
    if ((event.keyCode < 48) || (event.keyCode > 57))
        event.returnValue = false;
}
function loadki() {//Metodo para que funcione el el udload
    $('#form2').fileUploadUI({
        url: 'FileUpload.ashx',
        method: 'POST',
        uploadTable: $('#files'),
        downloadTable: $('#files'),
        buildUploadRow: function (files, index)
        {
            return $('<tr><td>' + files[index].name + '<\/td>' +
                            '<td class="file_upload_progress"><div><\/div><\/td>' +
                            '<td class="file_upload_cancel">' +
                            '<button class="ui-state-default ui-corner-all" title="Cancel">' +
                            '<span class="ui-icon ui-icon-cancel">Cancel<\/span>' +
                            '<\/button><\/td><\/tr>');
        },
        buildDownloadRow: function (file) {
            return $('<tr><td>' + file.name + '<\/td><\/tr>');
        }
    });
}

function editVisita(id, thisr, titulocomple, usuario, fechaEditar, ModoVisualizar, event, estado, rol) { //notarec,
    //Variables para general
    var separaid = id.split('.');
    var ids = separaid[0];
    //Variables para Visitas
    var parametros = { idVisita: ids };
    var nomClienter = '';
    var paisClienter = '';
    var ciuClienter = '';
    var motivor = '';
    var asuntor = '';
    var notaBase = '';
    var objetivo = '';
    var procesos = '';
    var fechaInir = '';
    var fechaFinr = '';
    var fechaViajeInir = '';
    var fechaViajeFinr = '';
    var contacto = '';
    var txtHtml = '';
    var FechaActc = '';
    var ValidaCierrex = false;
    var visCancelada = false;
    //Variables para Eventos
    var nomEvenF = '';
    var ciuPaisEveF = '';
    var fechaIniEveF = '';
    var fechaFinEveF = '';
    var remoto = '';
    var soporte = '';
    var obra = '';

    if (ModoVisualizar == 'Eve') {
        var parametrosF = { idEvento: ids };
        //Consulta los eventos/ferias
        ejecutarAjax(parametrosF, 'ConsultarEventoF', function (result) {
            var data = result;
            var eventosF = data.d;
            $.each(eventosF, function (clave, eventosF) {
                nomEvenF = eventosF.nomEve;
                ciuPaisEveF = eventosF.ciudadEve + '/' + eventosF.paisEve;
                fechaIniEveF = eventosF.fechaIniEve;
                fechaFinEveF = eventosF.fechaFinEve;
                participante = eventosF.partici;
                $('#editnomEve').append('<td>Nombre: ' + nomEvenF + ' </td>');
                $('#editciuPaisEve').append('<td>Ciudad/Pais: ' + ciuPaisEveF + ' </td>');
                $('#editfechaIniEve').append('<td>Fecha Inicia : ' + fechaIniEveF + ' </td>');
                $('#editfechaFinEve').append('<td>Fecha Fin : ' + fechaFinEveF + ' </td>');
                $('#editpartici').append('<td>Participantes : ' + participante + ' </td>');
            });
        });
        txtHtml = '<table>' +
             ' <tr><div class="stylePromp" id=editnomEve></div></br></tr>' +
             ' <tr><div class="stylePromp" id=editciuPaisEve></div></br></tr>' +
             ' <tr><div class="stylePromp" id=editfechaIniEve></div></br> <div class="stylePromp" id=editfechaFinEve></div></br></tr>' +
             ' <tr><div class="stylePromp" id=editpartici></div></tr>' +
             ' </table> ';
        GlobalTextpromt(txtHtml, ids, ModoVisualizar, thisr, titulocomple, fechaEditar, usuario, event, estado); //notarec,term,
    }
    else {

        var NotaBaseText = '';
        if (ModoVisualizar == 'Eje' || ModoVisualizar == 'AgeCon')
        {
            TextDownload = '<form method="post" action="WSSIO.asmx" id="form2" enctype="multipart/form-data" class="file_upload">' +
            '<div id =filediv><input type=hidden id=filetext value="" name=nombreFile> <input type=file name=file multiple><button>Cargar</button><div>Examinar</div></div><table id=files></table>';
            NotaBaseText =
                 '<table>' +
                 '<tr>' +
                 '<td valign="top">' +
                 '<table id="tableCotizar">' +
                 '<tr><td> <input type="checkbox" name="chkCotizar" id="chkCotizar" value="cotizar" onclick="cargarCaitzacion()"> Cotizaci&oacute;n FUP</td></tr>' +
                 '<tr id="trFupVersion" style="display: none;"><td>Fup: <input onkeypress=ValidaSoloNumeros() onblur=SalidaEscrituraFupVersion(getEditNoFupv2(),this.value) type="text" id="EditNoFupv2"  class="textboxlr" value="" name="EditNoFupv2"> Versi&oacute;n: <input class="textboxlr" type="text" onblur=SalidaEscrituraFupVersion(getEditNoFupv2(),this.value) id="EditVersion" value="" name="EditVersion"> </td></tr></table> ' +
                 '</td>' +
                 '<td valign="top">' +
                 '<table id="tableCotizarRapido">'+
                 '<tr><td> <input type="checkbox" name="chkCotizarRapida" id="chkCotizarRapida" value="cotizarRapida" onclick="cargarCotizacionRapida()"> Cotizaci&oacute;n R&aacute;pida </td></tr>' +
                 '<tr id="trRapMetroC" style="display: none;"><td>Metros Cuadrados: </td><td><input onkeypress=ValidaSoloNumeros() type=text id=idRapMetroC  class=textboxlr value="" name=idRapMetroC> </td></tr> ' +
                 '<tr id="trRapValorC" style="display: none;"><td>Valor de Cotizaci&oacute;n: </td><td><input onkeypress=ValidaSoloNumeros() type=text id=idRapValorC  class=textboxlr value="" name=idRapValorC> </td></tr> ' +
                 '<tr id="trRapMoneda" style="display: none;"><td>Moneda: </td><td><select id="idRapMoneda" name="idRapMoneda" ></select></td></tr>' +
                 '</table>' + 
                 '</td>' +
                 '</tr>' +
                 '</table>' +
                 '<table><tr id="trComentarios1"><td>Observaciones (Opcional):</td></tr><tr id="trComentarios"><td><textarea id="editconclu" name="editconclu" wrap="soft" rows="4" cols="60"></textarea></td></tr></table>' +                 
                 '<table id="tableEverNote"><tr id="trVinculo"><td>V&iacute;nculo EverNote:</td><td id="td_1"><textarea id="vinculoEverNote" name="vinculoEverNote" wrap="soft" rows="1" cols="30" onchange="setVinculo()"></textarea></td><td id="td_2"><a id="vinculoEverNote1" href="" target="_blank">EverNote</a></td><td id="td_3"><input type="checkbox" name="chkVinculo" id="chkVinculo" onclick="mostrarVinculo()"></td></tr></table>' +
                 '<table><tr id="trArchivo"><td>Adjuntos ' + TextDownload + '</td></tr>' +
                 '<tr><td></td></tr>' +
                 '</form></table >';
        }

//        else if (ModoVisualizar == 'AgeCon') {
//            NotaBaseText = '<table style="width: 450px"><tr> <td style="display: block; border: 1px solid green; height: 110px; overflow-y: scroll;"><div id="grid"></div></td></tr></table>' +
//                '<table><tr><td>Observaciones: <div id="editconclu" name="editconclu" ></div></td></tr>' +
//         '<tr id=trFupVersion style="display: none;"><td>Fup: <input onblur=SalidaEscrituraFupVersion(getEditNoFupv2(),this.value) type=text id=EditNoFupv2  class=textboxlr value="" name=EditNoFupv2> Versi&oacute;n: <input class=textboxlr type=text onblur=SalidaEscrituraFupVersion(getEditNoFupv2(),this.value) id=EditVersion value="" name=EditVersion> </td></tr>  ' +
//                        '</table >';
////            TextDownload = '<form method="post" action="WSSIO.asmx" id="form2" enctype="multipart/form-data" class="file_upload">' +
////            '<div id =filediv><input type=hidden id=filetext value="" name=nombreFile> <input type=file name=file multiple><button>Cargar</button><div>Examinar</div></div><table id=files></table>';
////            NotaBaseText =
////                 '<table>' +
////                 '<tr>' +
////                 '<td valign="top">' +
////                 '<table id="tableCotizar">' +
////                 '<tr><td> <input type="checkbox" name="chkCotizar" id="chkCotizar" value="cotizar" onclick="cargarCaitzacion()"> Cotizaci&oacute;n FUP</td></tr>' +
////                 '<tr id="trFupVersion" style="display: none;"><td>Fup: <input onkeypress=ValidaSoloNumeros() onblur=SalidaEscrituraFupVersion(getEditNoFupv2(),this.value) type="text" id="EditNoFupv2"  class="textboxlr" value="" name="EditNoFupv2"> Versi&oacute;n: <input class="textboxlr" type="text" onblur=SalidaEscrituraFupVersion(getEditNoFupv2(),this.value) id="EditVersion" value="" name="EditVersion"> </td></tr></table> ' +
////                 '</td>' +
////                 '<td valign="top">' +
////                 '<table id="tableCotizarRapido">' +
////                 '<tr><td> <input type="checkbox" name="chkCotizarRapida" id="chkCotizarRapida" value="cotizarRapida" onclick="cargarCotizacionRapida()"> Cotizaci&oacute;n R&aacute;pida </td></tr>' +
////                 '<tr id="trRapMetroC" style="display: none;"><td>Metros Cuadrados: </td><td><input onkeypress=ValidaSoloNumeros() type=text id=idRapMetroC  class=textboxlr value="" name=idRapMetroC> </td></tr> ' +
////                 '<tr id="trRapValorC" style="display: none;"><td>Valor de Cotizaci&oacute;n: </td><td><input onkeypress=ValidaSoloNumeros() type=text id=idRapValorC  class=textboxlr value="" name=idRapValorC> </td></tr> ' +
////                 '<tr id="trRapMoneda" style="display: none;"><td>Moneda: </td><td><select id="idRapMoneda" name="idRapMoneda" ></select></td></tr>' +
////                 '</table>' +
////                 '</td>' +
////                 '</tr>' +
////                 '</table>' +
////                 '<table><tr id="trComentarios1"><td>Observaciones (Opcional):</td></tr><tr id="trComentarios"><td><textarea id="editconclu" name="editconclu" wrap="soft" rows="4" cols="60"></textarea></td></tr></table>' +
////                 '<table id="tableEverNote"><tr id="trVinculo"><td>V&iacute;nculo EverNote:</td><td id="td_1"><textarea id="vinculoEverNote" name="vinculoEverNote" wrap="soft" rows="1" cols="30" onchange="setVinculo()"></textarea></td><td id="td_2"><a id="vinculoEverNote1" href="" target="_blank">EverNote</a></td><td id="td_3"><input type="checkbox" name="chkVinculo" id="chkVinculo" onclick="mostrarVinculo()"></td></tr></table>' +
////                 '<table><tr id="trArchivo"><td>Adjuntos ' + TextDownload + '</td></tr>' +
////                 '<tr><td></td></tr>' +
////                 '</form></table >';
//        }

        txtHtml = ' <table><tr><div class="stylePromp" id=idCliente Style=Display:none></div><div class="stylePromp" id=pais></div> </br> </tr>' +
             ' <tr><div class="stylePromp" id=objetivo></div> </br></tr>' +
             ' <tr><div class="stylePromp" id=obra></div> </br></tr>' +
             ' <tr><div class="stylePromp" id=procesos></div> </br></tr>' +
             ' <tr><div class="stylePromp" id=Motivoyasunt></div> </br> </tr>' +
             ' <tr><div class="stylePromp2" id=fechasViaje></div></tr>' +             
             ' </table>' + NotaBaseText;

        //Consulta la visita
        ejecutarAjax(parametros, 'ConsultarVisita', function (result) {
            var data = result;
            var visitas = data.d;
            $.each(visitas, function (index, visitas) {
                nomClienter = visitas.nomCliente;
                paisClienter = visitas.paisCliente;
                ciuClienter = visitas.ciuCliente;
                motivor = visitas.motivo;
                idClir = visitas.idcli;
                ValidaCierrex = visitas.ValidaCierre;
                visCancelada = visitas.visitaCancelada;
                notaBase = visitas.nota;
                objetivo = visitas.objetivo;
                procesos = visitas.procesos;
                fechaViajeInir = visitas.fechaViajeIni;
                fechaViajeFinr = visitas.fechaViajeFin;
                contacto = visitas.contacto;
                remoto = visitas.remoto;
                soporte = visitas.soporte;
                obra = visitas.obra;

                if (visCancelada) {
                    $('#jqi_state0_buttonCancelarVisita').hide();
                    $('#jqi_state0_buttonGuardar').hide();
                    $('#jqi_state0_buttonAsociarContacto').hide();
                    $('#jqi_state1_buttonCrearContacto').hide();
                    $('#jqi_state0_buttonFinalizarVisita').hide();
                    $('#trArchivo').hide();
                    $('#trComentarios1').hide();
                    $('#trComentarios').hide();
                    $('#tableCotizarRapido').hide();
                    $('#tableCotizar').hide();
                    $('#tableEverNote').hide();
                }

                if (ValidaCierrex) {
//                    $('#jqi_state0_buttonCancelarVisita').hide();
                    $('#jqi_state0_buttonGuardar').hide();
                    $('#jqi_state0_buttonAsociarContacto').hide();
                    $('#jqi_state1_buttonCrearContacto').hide();
                    $('#jqi_state0_buttonFinalizarVisita').hide();
                    $('#trArchivo').hide();
                    $('#trComentarios1').show();
                    $('#trComentarios').show();
                    $('#editconclu').prop("disabled", true);
                    $('#vinculoEverNote').prop("disabled", true);
                    $('#chkCotizar').prop("disabled", true);
                    $('#EditNoFupv2').prop("disabled", true);
                    $('#EditVersion').prop("disabled", true);
                    $('#chkCotizarRapida').prop("disabled", true);
                    $('#idRapMetroC').prop("disabled", true);
                    $('#idRapValorC').prop("disabled", true);
                    $('#idRapMoneda').prop("disabled", true);
                }

                $('#idCliente').append(idClir);
                $('#pais').append('<td>  ' + 'Ciudad/Pais: ' + ciuClienter + '/' + paisClienter + '</td><td style="width: 60px;"></td><td>Contacto: ' + contacto + '  </td>');
                $('#objetivo').append('<td>Objetivo: ' + objetivo + '</td><td style="width: 60px;"></td><td>Motivo: ' + motivor + '  </td>');
                $('#obra').append('<td>Obra: ' + obra + '  </td>');
                $('#procesos').append('<td>Acompa' + String.fromCharCode(241) + 'ante(s): ' + procesos + '  </td>');
                $('#Motivoyasunt').append('<td> ' + soporte + '</td><td style="width: 60px;"></td><td> ' + remoto + '</td>'); //<td>  ' + ' Asunto:  ' + asuntor + '</td>
                $('#fechasViaje').append('<td>Viaja el dia: ' + fechaViajeInir + '</td><td style="width: 61px; "> y  regresa:</td><td>' + fechaViajeFinr + '</td>');

                //Ejecucion
                if (ModoVisualizar == 'Eje') {
                    $('#idRapMoneda').text('');
                    var parametros = { filtro: '' };
                    ejecutarAjax(parametros, 'ConsultarModenas', function (result) {
                        var data = result;
                        var monedas = data.d;
                        var iniSel = '<option value=0>Seleccionar</option>';
                        var comboMon = "";
                        var comboMonComp = "";
                        $.each(monedas, function (clave, valor) {
                            idNomMon = (valor.IdMoneda);
                            idNomMonSep = idNomMon.split(':');
                            comboMon = comboMon + ' <option value=' + idNomMonSep[0] + '>' + idNomMonSep[1] + '</option>';
                            comboMonComp = iniSel + comboMon;
                        });
                        $('#idRapMoneda').append(comboMonComp);


                        var parametrosE = { idvisita: ids };
                        ejecutarAjax(parametrosE, 'ConsultarParaEjecsaactivHisto', function (result) {
                            var data = result;
                            var cierr = data.d;

                            $('#editconclu').text(cierr.Conclusion);
                            $('#editconclu').val(cierr.Conclusion);

                            $('#EditNoFupv2').text(cierr.Fup);
                            $('#EditNoFupv2').val(cierr.Fup);
                            $('#EditVersion').text(cierr.Version);
                            $('#EditVersion').val(cierr.Version);
                            $('#idRapMetroC').text(cierr.Metros);
                            $('#idRapMetroC').val(cierr.Metros);
                            $('#idRapValorC').text(cierr.Valor);
                            $('#idRapValorC').val(cierr.Valor);
                            $('#vinculoEverNote').text(cierr.Vinculo);
                            $('#vinculoEverNote').val(cierr.Vinculo);
                            $("#vinculoEverNote1").attr("href", cierr.Vinculo);
                            $('#idRapMoneda').val(cierr.Moneda);

                            FechaActc = fechaEditar;
                            $('#filetext').val(ids + '_Plano_' + FechaActc);

//                            if ($('#editconclu').text() != "") {
//                                $('#jqi_state0_buttonCancelarVisita').hide();
//                            }

                            if ($('#EditNoFupv2').text() != "" && $('#EditNoFupv2').text() != null) {
                                $('#chkCotizar').attr("checked", true);
                                $('#trFupVersion').show();
                            }

                            if ($('#idRapMetroC').text() != "" && $('#idRapMetroC').text() != null && $('#idRapMetroC').text() != 0) {
                                $('#chkCotizarRapida').attr("checked", true);
                                $('#trRapMetroC').show();
                                $('#trRapValorC').show();
                                $('#trRapMoneda').show();
                            }

                            if ($('#vinculoEverNote').text() != "") {
                                $('#chkVinculo').attr("checked", false);
                                $('#td_1').hide();
                                $('#td_2').show();
                            }

                            else if ($('#vinculoEverNote').text() == "") {
                                $('#chkVinculo').attr("checked", true);
                                $('#td_1').show();
                                $('#td_2').hide();
                            }
                        });
                    });
                }

                //Agenda consolidad
                else if (ModoVisualizar == 'AgeCon') {
                    var parametrosA = { idvisita: ids };
                    ejecutarAjax(parametrosA, 'ConsultarParaEjecsaactivHisto', function (result) {
                        var data = result;
                        var cierr = data.d;
                        $('#editconclu').text(cierr.Conclusion);
                        $('#editconclu').val(cierr.Conclusion);
                        $('#EditNoFupv2').text(cierr.Fup);
                        $('#EditNoFupv2').val(cierr.Fup);
                        $('#EditVersion').text(cierr.Version);
                        $('#EditVersion').val(cierr.Version);
                        $('#idRapMetroC').text(cierr.Metros);
                        $('#idRapMetroC').val(cierr.Metros);
                        $('#idRapValorC').text(cierr.Valor);
                        $('#idRapValorC').val(cierr.Valor);
                        $('#vinculoEverNote').text(cierr.Vinculo);
                        $('#vinculoEverNote').val(cierr.Vinculo);
                        $("#vinculoEverNote1").attr("href", cierr.Vinculo);
                        $('#idRapMoneda').val(cierr.Moneda);
                        $('#editnota').text(notaBase);
                        $('#editobj').text(objetivo);

                        if ($('#EditNoFupv2').text() != "" && $('#EditNoFupv2').text() != null) {
                            $('#chkCotizar').checked = true;
                            $('#trFupVersion').show();
                        }

                        if ($('#idRapMetroC').text() != "" && $('#idRapMetroC').text() != null && $('#idRapMetroC').text() != 0) {
                            $('#chkCotizarRapida').checked = true;
                            $('#trRapMetroC').show();
                            $('#trRapValorC').show();
                            $('#trRapMoneda').show();
                        }
                    });
                }
            });
        });

        GlobalTextpromt(txtHtml, ids, ModoVisualizar, thisr, titulocomple, fechaEditar, usuario, event, estado, rol);
        loadki();
    }
}

function traerDatosContactoLite() {
    var txt = '';
    var parametros = { idcliente: $('#idCliente').text() };
    //Trae los datos del contacto lite
    ejecutarAjax(parametros, 'ContatosLite', function (result) {
        var data = result;
        var contac = data.d;
        var nombrec = '';
        var telefonoc = '';
        var direccionc = '';
        var emailc = '';
        var cargoc = '';
        var idclic = '';
        $.each(contac, function (clave, contac) {
            nombrec = contac.nombre;
            telefonoc = contac.telefono;
            cargoc = contac.cargo;
            direccionc = contac.direccion;
            emailc = contac.email;
            idclic = contac.idcli;
            $('#editNombrecont').val(nombrec);
            $('#editCargocont').val(cargoc);
            $('#editTelefonocont').val(telefonoc);
            $('#editDireccioncont').val(direccionc);
            $('#editEmailcont').val(emailc);
            $('#idclienteedit').val(idclic);
            txt = '<table><tr><td>Nombre :</td><td> <input type=hidden id=idclienteedit name=idclienteedit  ><input type=text id="editNombrecont" name="editNombrecont" value="' + nombrec + '"> </td> <td>Cargo :</td><td> <input type=text id="editCargocont" name="editCargocont" > </td>   </tr>' +
                '<tr><td>Telefono :</td><td> <input type=text id="editTelefonocont" name="editTelefonocont" > </td> <td>Direccion :</td><td> <input type=text id="editDireccioncont" name="editDireccioncont" > </td>   </tr>' +
                '<tr><td>Email :</td><td> <input type=text id="editEmailcont" name="editTelefonocont" > </td> <td></td><td> </td>   </tr>' +
                '</table >';
        });
    });
}

function ValidarBdD(txtbusqueda) {
    var nombreBasec = "";
    var parametros = { idcliente: $('#idCliente').text()};
    ejecutarAjax(parametros, 'ValidarBaseD', function (result) {
        var data = result;
        var contac = data.d;
        var parametros1 = { idcliente: $('#idCliente').text(), textAbuscar: txtbusqueda };
        $.each(contac, function (clave, contac) {
            nombreBasec = contac.validanombase;
            $('#Nombd').val(nombreBasec);
            var item2 = '';
            if (nombreBasec == "SIO") {
                ejecutarAjax(parametros1, 'ContatosSIOcampos', function (result) {
                    var data = result;
                    var contac = data.d;
                    var nombreBasec = '';
                    var item3 = '';
                    $.each(contac, function (clave, contac) {
                        idclic = contac.idcli;
                        textTresRespo = contac.nombre;
                        tresResp = textTresRespo.split(';');
                        $('#editactivi1').text('');
                        item3 = ' <option value=0>Seleccionar</option>';
                        $.each(tresResp, function (clave, valor) {
                            sep3 = (clave, valor).split(':');
                            item3 = item3 + ' <option value=' + sep3[0] + '>' + sep3[1] + '</option>';
                        });
                        $('#editactivi1').append(item3);
                    });
                });
            }
            if (nombreBasec == "LITE") {
                var parametros2 = { idcliente: $('#idCliente').text(), textAbuscar: txtbusqueda };
                var item2 = '';
                ejecutarAjax(parametros2, 'ContatosLitecampos', function (result) {
                    var data = result;
                    var contac = data.d;
                    var nombreBasec = '';
                    $('#editactivi1').text('');
                    item2 = ' <option value=0>Seleccionar</option>';
                    $.each(contac, function (clave, contac) {
                        idclic = contac.idcli;
                        nombrec = contac.nombre;
                        telefonoc = contac.telefono;
                        direccionc = contac.direccion;
                        emailc = contac.email;
                        item2 = item2 + ' <option value=' + idclic + '>' + nombrec + '</option>';
                    });
                    $('#editactivi1').append(item2);

                });
            }
        });
    });
    return nombreBasec;
}

function ConsultaContactoxBase(txtbusqueda) {
    ValidarBdD(txtbusqueda)
}

function GlobalTextpromt(txt, idvisita, ModoVisualizar, thisr, titulocomple, fechaEditar, usuario, event, estado, rol) { // notarec,term,
    var txtv1 = '<table ><tr><td><input type=text id="editbusqueda" name="editbusqueda" onkeyup=ConsultaContactoxBase(this.value)><input type=hidden id=Nombd ><select  id="editactivi1" name="editactivi1" ></select></td> </tr>' +
                '</table >';
    var txtv2 = '<table ><tr><td>Nombre :</td><td> <input type=hidden id=idclienteedit name=idclienteedit > <input type=text id="editNombrecont" name="editNombrecont" > </td> <td>Cargo :</td><td> <input type=text id="editCargocont" name="editCargocont" > </td>   </tr>' +
                '<tr><td>Telefono :</td><td> <input type=text id="editTelefonocont" name="editTelefonocont" > </td> <td>Direccion :</td><td> <input type=text id="editDireccioncont" name="editDireccioncont"  > </td>   </tr>' +
                '<tr><td>Email :</td><td> <input type=email id="editEmailcont" name="editEmailcont" > </td> <td></td><td> </td>   </tr>' +
                '</table >';
    var txtv3 = '<div id=CargaPagaspxf ></div ><div id=CargaPagaspx ></div >';

    var statesdemo;

    if (ModoVisualizar == "Eve") {
        statesdemo = {
            state0: {
                title: 'Eventos',
                focus: 4,
                html: txt,
                buttons: { 'Salir': false },
                submit: function (e, v, m, f) {
                }
            }
        };
    }
    else {
        statesdemo = {
            state0: {
                title: 'VISITA ' + titulocomple + ' ' + fechaEditar + '  /  ' + estado,
                focus: 4,
                html: txt,
                buttons: { 'Cancelar Visita': 30, 'Guardar': 25, 'Asociar Contacto': 1, 'Finalizar Visita': 11, 'Salir': false },
                submit: function (e, v, m, f) {
                    if (v == 30) {//boton cancelar visita
                        if (ModoVisualizar == 'Eje' || ModoVisualizar == 'Ag') {
                            if (confirm("Realmente desea Cancelar la Visita ?")) {
                                cancela = 'true';
                            }
                            else {
                                cancela = 'false';
                            }
                            if (cancela == 'true') {
                                var parametroCan = { idVisitaa: idvisita };
                                //Cancela la visita
                                ejecutarAjax(parametroCan, 'cancelarVisita', function (result) {
                                    var data = result;
                                    var cancelar = data.d;
                                    if (cancelar != 'OK') {
                                        alert('Error al cancelar la visita');
                                    }
                                    else
                                    {
                                        var parametro = { idVisita: idvisita };
                                        ejecutarAjax(parametro, 'consultarcolorvisita', function (result) {
                                            var data = result;
                                            var Valida = data.d;
                                            $.each(Valida, function (clave, Valida) {
                                                var colorr = Valida.colorVisita;
                                                var estado = Valida.estadoVisita;
                                                event.color = colorr;
                                                event.estado = estado;
                                                event.constraint = 'availableForMeeting';
                                                $('#calendar').fullCalendar('updateEvent', event);
                                            });
                                        });
                                    }
                                });
                            }
                        }
                    }
                    if (v == 1) {
                        if (ModoVisualizar == 'Cierr' || ModoVisualizar == 'Eje') {
                            e.preventDefault();
                            $.prompt.goToState('state1');
                            ValidarBdD('');
                        }
                    }
                    if (v == 25) {
                        if (ModoVisualizar == 'Ag') {
                            var colorDeVisita = event.colorVisita;
                            if (colorDeVisita == '#C72020') {
                                fechaEditar = 'NULL';
                            }
                            if (f.editnota != null) {
                                thisr.text(titulocomple);
                                GuardarNotaajax(titulocomple, fechaEditar, usuario, 'NOCorreo');
                            }
                        }

                        else if (ModoVisualizar == 'Eje') {
                            var concluR = $('#editconclu').val();
                            var vinculo = $('#vinculoEverNote').val();
                            var Fup = '';
                            var version = '';
                            var metroC = '';
                            var valorC = '';                            
                            var idMoneda = '';
                            var msj = '';
                            if ($("#chkCotizar").is(':checked')) {
                                Fup = $('#EditNoFupv2').val();
                                version = $('#EditVersion').val();
                                if ($("#EditNoFupv2").css("background-color") == 'rgb(255, 194, 189)' || $("#EditVersion").css("background-color") == 'rgb(255, 194, 189)') {
                                    msj += 'Actividad Cotizar no guardada. Verifique el FUP y la Versión\n\r';
                                    Fup = '';
                                    version = '';
                                }
                            }
                            if ($("#chkCotizarRapida").is(':checked')) {
                                metroC = $('#idRapMetroC').val();
                                valorC = $('#idRapValorC').val();
                                var comboMone = document.getElementById('idRapMoneda');
                                idMoneda = comboMone.options[comboMone.selectedIndex].value;
                                if (metroC.trim() == "" || valorC.trim() == "" || idMoneda == "0") {
                                    msj += 'Actividad Cotización Rápida no guardada. Los campos no pueden quedar vacíos';
                                    metroC = '';
                                    valorc = '';
                                }
                            }
                            //if (concluR.trim() == "") {
                              //  alert('Por favor escriba las Observaciones, gracias!');
                            //}
//                            else
//                            {
//                                var parametrosre = { idVisitaa: idvisita };                                                                  
//                                ejecutarAjax(parametrosre, 'VerificarFechaActual', function (result) {
//                                    var data = result;
//                                    ejecuta = data.d;
//                                    if (ejecuta == 'True')
//                                    {
//                                        try
//                                        {                                            
//                                            GuardarajaxEje(titulocomple, concluR, fechaEditar, usuario, thisr, event, Fup, version, metroC, valorC, idMoneda, vinculo);
//                                            if (msj != '')
//                                                alert(msj);
//                                        }
//                                        catch (e) { }
//                                    }
//                                    else { alert('No debe ejecutar Fechas Futuras'); }
//                                });
                            //                            }
                            try {
                                GuardarajaxEje(titulocomple, concluR, fechaEditar, usuario, thisr, event, Fup, version, metroC, valorC, idMoneda, vinculo);
                                if (msj != '')
                                    alert(msj);
                            }
                            catch (e) { alert('Ha ocurrido un error. Intente Nuevamente gracias.'); }

                        }                       
                    }
                    if (v == 11) {
                        var cerrar = "NO";
                        var fup = '';
                        var version = '';
                        var metro = '';
                        var valor = '';
                        var moneda = '';
                        var evernote = '';

                        var conclusion = $('#editconclu').val();
                        evernote = $('#vinculoEverNote').val();
                        var msj = '';
                        if ($("#chkCotizar").is(':checked')) {
                            fup = $('#EditNoFupv2').val();
                            version = $('#EditVersion').val();
                            if ($("#EditNoFupv2").css("background-color") == 'rgb(255, 194, 189)' || $("#EditVersion").css("background-color") == 'rgb(255, 194, 189)' || fup.trim() == '' || version.trim() == '' || fup.trim() == '0') {
                                msj += 'Actividad Cotizar no puede ser guardada. Verifique el FUP y la Versión\n\r';
                                fup = '';
                                version = '';
                                cerrar = 'NO';
                            }
                        }
                        if ($("#chkCotizarRapida").is(':checked')) {
                            metro = $('#idRapMetroC').val();
                            valor = $('#idRapValorC').val();
                            var comboMone = document.getElementById('idRapMoneda');
                            moneda = comboMone.options[comboMone.selectedIndex].value;
                            if (metro.trim() == "" || valor.trim() == "" || moneda == "0") {
                                msj += 'Actividad Cotización Rápida no puede ser guardada. Los campos no pueden quedar vacíos';
                                metro = '';
                                valor = '';
                                cerrar = 'NO';
                            }
                        }

                        if (conclusion.trim() == "") {
                            cerrar = 'NO';
                            alert('Las Observaciones deben ser diligenciados');
                        }
                        else if(msj.trim() != "")
                        {
                            cerrar = 'NO';
                            alert(msj);
                        }
                        //else if(rol == 1)
                        //{
                        //    if (evernote.trim() == "")
                        //    {
                        //        cerrar = 'NO';
                        //        alert('Para finalizar la visita debes copiar el vínculo EverNote asociado. Gracias!');
                        //    }                        
                        //}
                        else {
                            cerrar = 'SI';
                            var parametros = { idvisitaa: idvisita };
                            //Consulta si tiene contacto
                            ejecutarAjax(parametros, 'consultarTieneContacto', function (result) {
                                var data = result;
                                var hayConta = data.d;
                                if (hayConta == true) {
                                    CheckCierr(idvisita, event, usuario, cerrar, conclusion, fup, version, metro, valor, moneda, evernote);
                                }
                                else {
                                    alert('No tiene contacto asociado!');
                                }
                            });
                        }
                    }
                }
            },
            state1: {
                title: 'Consulta Contacto',
                focus: 2,
                html: txtv1,
                buttons: { 'Asociar Contacto': 40, 'Crear Contacto': 2, 'Salir': false },
                submit: function (e, v, m, f) {
                    e.preventDefault();
                    if (v == 40) {
                        var combofrsr = document.getElementById('editactivi1');
                        ContactoId = combofrsr.options[combofrsr.selectedIndex].value;
                        if (ContactoId == 0 || ContactoId == 'undefined') {
                            if ($('#Nombd').val() != "SIO") {
                                //ir a ventana edicion LITE
                                $.prompt.goToState('state2');
                                txtv2 = traerDatosContactoLite();
                            }
                            else {
                                $('#CargaPagaspxf').text('');
                                $('#CargaPagaspxf').append('<input type=button onclick=RefrespagSa($("#idCliente").text()) value="Gestionar Contactos">');
                                $.prompt.goToState('state3');
                            }
                        }
                        else {
                            if ($('#Nombd').val() == "SIO") {
                                GuardarcontactoSIO(idvisita, ContactoId);
                            } else { GuardarcontactoSIO(idvisita, '0'); }
                        }
                    }
                    if (v == 2) {
                        if ($('#Nombd').val() == "SIO") {
                            $('#CargaPagaspxf').text('');
                            $('#CargaPagaspxf').append('<input type=button onclick=RefrespagSa($("#idCliente").text()) value="Gestionar Contactos">');
                            $.prompt.goToState('state3');
                        }
                        else {
                            $.prompt.goToState('state2');
                            txtv2 = traerDatosContactoLite();
                        }
                    }
                    if (v == false) {
                        $.prompt.goToState('state0');
                    }
                }
            },
            state2: {
                title: 'Contacto',
                focus: 1,
                html: txtv2,
                buttons: { 'Guardar': 5, 'Salir': false },
                submit: function (e, v, m, f) {
                    e.preventDefault();
                    if (v == false)
                        $.prompt.goToState('state1');

                    if (v == 5) {
                        GuardarContactoLite($("#editNombrecont").val(), $("#editCargocont").val(), $("#editTelefonocont").val(), $("#editDireccioncont").val(), $("#editEmailcont").val());
                        if (validaGuarClose() == true) {
                            $.prompt.goToState('state1');
                        } else {
                            alert('Los campos no puende estar vacios, gracias!');
                        }
                    }
                }
            },
            state3: {
                title: 'Contacto SIO',
                focus: 1,
                html: txtv3,
                buttons: { 'Volver': 6, 'Salir': false },
                submit: function (e, v, m, f) {
                    e.preventDefault();
                    if (v == 6) {
                        $.prompt.goToState('state1');
                        ConsultaContactoxBase('');
                    }
                }
            }
        };
    }

    $.prompt(statesdemo);

    //Se ocultan los botones

    if (ModoVisualizar == 'AgeCon') {
        $('#jqi_state0_buttonCrearContacto').hide();
        $('#jqi_state0_buttonAsociarContacto').hide();
        $('#jqi_state0_buttonCancelarVisita').hide();
        $('#jqi_state0_buttonGuardar').hide();
        $('#jqi_state0_buttonFinalizarVisita').hide();
        $('#trArchivo').hide();
    }

    if (ModoVisualizar == 'Cierr') {
//        $('#jqi_state0_buttonCancelarVisita').hide();
        $('#jqi_state0_buttonFinalizarVisita').hide();
        $('#trArchivo').hide();
    }

    if (ModoVisualizar == 'Eje') {
        $('#jqi_state0_buttonFinalizarVisita').hide();
        $('#trArchivo').hide();
     }

    if (ModoVisualizar == 'Ag') {
        $('#jqi_state0_buttonCrearContacto').hide();
        $('#jqi_state0_buttonAsociarContacto').hide();
        $('#jqi_state0_buttonGuardar').hide();
        $('#jqi_state0_buttonFinalizarVisita').hide();
        $('#trArchivo').hide();
    }

    if (ModoVisualizar == "Eve") {
        $(".jqi").css({ "width": "370px" });
    }

    else {
        $(".jqi").css({ "width": "550px" });
    }
}

function RefrespagSa(idcliente) {//popup para agregar el contacto
    var opciones = "toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no,  width=1242, height=973, top=85, left=140";
    window.open('Contacto.aspx?idCliente=' + idcliente, "", opciones);
}

function validaGuarClose() {
    var a, b, c, d, e = false;
    var granval = false;
    if (document.getElementById('editNombrecont').value.length > 0)
    { a = true; }
    if (document.getElementById('editCargocont').value.length > 0)
    { b = true; }
    if (document.getElementById('editTelefonocont').value.length > 0)
    { c = true; }
    if (document.getElementById('editDireccioncont').value.length > 0)
    { d = true; }
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/.test(document.getElementById('editEmailcont').value))
    { e = true; }
    if (a == true && b == true && c == true && d == true && e == true)
    { granval = true; }
    else
    { granval = false; }
    return granval;
}

function GuardarContactoLite(Nombrec, Cargoc, Telefonoc, Direccionc, Emailc) {
    var parametros = { Nombre: Nombrec, Cargo: Cargoc, Telefono: Telefonoc, Direccion: Direccionc, Email: Emailc, idcliente: $("#idCliente").text() };
    //Guarda el contanto lite
    ejecutarAjax(parametros, 'GuardarContactoNuevoLite', function (result) { });
}

function getEditNoFupv2() {
    return document.getElementById("EditNoFupv2").value;
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

function SalidaEscrituraFupVersion(Fupc, Versionc) {
    if (Fupc.trim() != "" && Versionc.trim() != "")
    {
        var parametros = { Fup: Fupc, Version: Versionc };
        //Valida el fup y la version
        ejecutarAjax(parametros, 'ValidarFupVersion', function (result) {
            var data = result;
            var Valida = data.d;
            $.each(Valida, function (clave, Valida) {
                if (Valida.ValidaFupvarsion == true) {
                    $("#EditNoFupv2").css({ "background-color": "rgb(179, 255, 171)" });
                    $("#EditVersion").css({ "background-color": "rgb(179, 255, 171)" });
                }
                else {
                    $("#EditNoFupv2").css({ "background-color": "rgb(255, 194, 189)" });
                    $("#EditVersion").css({ "background-color": "rgb(255, 194, 189)" });
                }
            });
        });
    }
    else {
        $("#EditNoFupv2").css({ "background-color": "rgb(255, 194, 189)" });
        $("#EditVersion").css({ "background-color": "rgb(255, 194, 189)" });
    }
}

function CheckCierr(idr, event, usu, cerrar, conclusion, fup, version, metro, valor, moneda, evernote) {
    var okd = 'false';
    if (cerrar == 'NO') {
        alert('No se puede finalizar la visita, revise la información ingresada. Gracias!');
        okd = 'false';
    }
    else {
        if (confirm("Realmente desea Cerrar la Visita ?")) {
            okd = 'true';
        }
        else {
            okd = 'false';
        }
    }

    if (okd == 'true') {
        var parametros = { idvisita: idr, usuario: usu, conclusion: conclusion, fup: fup, version: version , metro: metro, valor: valor, moneda: moneda, evernote: evernote};
        var $contenidoAjax = $('div#contenidoAjax').html('<div class="overlay" /> <div class="overlayContent">  <asp:Label ID="lblEnviando" runat="server" Text="Enviando..." Font-Names="Arial" Font-Size="14pt"></asp:Label> <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"  width="30" /> </div>');
        //Cierra la visita
        ejecutarAjax(parametros, 'CheckCierr', function (result) {
            //Trae el color dependiendo del estado de la visita
            var parametros = { idVisita: idr };
            ejecutarAjax(parametros, 'consultarcolorvisita', function (result) {
                var data = result;
                var Valida = data.d;
                $.each(Valida, function (clave, Valida) {
                    var colorr = Valida.colorVisita;
                    var estado = Valida.estadoVisita;
                    event.color = colorr;
                    event.estado = estado;
                    event.constraint = 'availableForMeeting';
                    $('#calendar').fullCalendar('updateEvent', event);
                });
                //Envia el correo de cierre
                var parametros = { idvisita: idr, usuario: usu };
                ejecutarAjax(parametros, 'EnviarCorreoCierr', function (result) {
                    $contenidoAjax.html('');
                    $.prompt.close();
                });
            });
        });
    }
}

//Guardar Contacto SIO
function GuardarcontactoSIO(idvisitaR, idContactoSIO) {
    var parametros = { idvisita: idvisitaR, idContacto: idContactoSIO };
    ejecutarAjax(parametros, 'GuardarDatoCierrContactoSIO', function (result) {
        alert('El contacto ha sido asociado!!');
        $.prompt.close();
    });
}

function GuardarNotaajax(tituloC, fechaagendac, usuarioc, correo) {
    var generaltext = tituloC; //el titulo completo
    var separa = generaltext.split('.'); //se separo por puntos (.)
    var idvisitaR = separa[0]//tome el primero de la sepacion por un solo punto
    var parametros = { idvisita: idvisitaR, fechaAgenda: fechaagendac, usuario: usuarioc }; //nota: $.trim(notaR),
    var $contenidoAjax = $('div#contenidoAjax').html('<div class="overlay" /> <div class="overlayContent">  <asp:Label ID="lblEnviando" runat="server" Text="Enviando..." Font-Names="Arial" Font-Size="14pt"></asp:Label> <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"  width="30" /> </div>');
    //Guarda
    ejecutarAjax(parametros, 'GuardarEventos', function (result) {
        $contenidoAjax.html('');
    });
}

function GuardarajaxEje(titulo, conclusionR, fechaagendac, usuarioc, thisc, event, fupC, VersionC, metroC, valorC, monedaC, vinculo) {
    var tituloS = titulo.split('.');
    var idvisitaR = tituloS[0];
    var parametros12 = { idvisita: idvisitaR, conclusion: conclusionR, fechaAgenda: fechaagendac, usuario: usuarioc, fup: fupC, version: VersionC, metro: metroC, valor: valorC, moneda: monedaC, evernote: vinculo};
    var $contenidoAjax = $('div#contenidoAjax').html('<div class="overlay" /> <div class="overlayContent">  <asp:Label ID="lblEnviando" runat="server" Text="Enviando..." Font-Names="Arial" Font-Size="14pt"></asp:Label> <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"  width="30" /> </div>');
    //Guarda la ejecucion
    ejecutarAjax(parametros12, 'GuardarEjecucion', function (result) {
        var colorr = '';
        var estado = '';
        var parametros = { idVisita: idvisitaR };
        ejecutarAjax(parametros, 'consultarcolorvisita', function (result) {
            var data = result;
            var Valida = data.d;
            $.each(Valida, function (clave, Valida) {
                colorr = Valida.colorVisita;
                estado = Valida.estadoVisita;
                event.color = colorr;
                event.estado = estado;
                event.constraint = 'availableForMeeting';
                $('#calendar').fullCalendar('updateEvent', event);
            });
            //Envia el correo de la ejecucion
            ejecutarAjax(parametros12, 'EnviarCorreoEje', function (result) {
                $contenidoAjax.html('');
            });
        });
    });   
}

function cargarCotizacionRapida()
{
    var x = document.getElementById("chkCotizarRapida").checked;
    if (x == true) {
        $('#trRapMetroC').show();
        $('#trRapValorC').show();
        $('#trRapMoneda').show();       
    }
    else
    {
        $('#trRapMetroC').hide();
        $('#trRapValorC').hide();
        $('#trRapMoneda').hide();
    }
}

function cargarCaitzacion()
{
    var x = document.getElementById("chkCotizar").checked;
    if (x == true) {
        $('#trFupVersion').show();
    }
    else {
        $('#trFupVersion').hide();
    }
}

function validaFinalizarVisita()
{
    if ($("#chkCotizar").is(':checked') || $("#chkCotizarRapida").is(':checked')) {
        var metroC = $('#idRapMetroC').val();
        var valorC = $('#idRapValorC').val();
        var comboMone = document.getElementById('idRapMoneda');
        var idMoneda = comboMone.options[comboMone.selectedIndex].value;
        var fup = $('#EditNoFupv2').val();
        var version = $('#EditVersion').val();
        if ($("#EditNoFupv2").css("background-color") == 'rgb(255, 194, 189)' || $("#EditVersion").css("background-color") == 'rgb(255, 194, 189)' || fup.trim() == '' || version.trim() == '') {
            return false;
        }
        else if (metroC.trim() == '' || valorC.trim() == '' || idMoneda == "0") {
            return false;
        }
        else {
            return true;
        }
    }
    else
    {
        return true;
    }
}

function setVinculo() {
    var x = document.getElementById("vinculoEverNote").value;
    if (x.trim() != "") {
        $("#vinculoEverNote1").attr("href", x);
        $("#td_2").show();
    }
    else {
        $("#td_2").hide();
    }
}

function mostrarVinculo()
{
    if ($("#chkVinculo").is(':checked')) {
        $("#td_1").show();
    }
    else {
        $("#td_1").hide();
    }
}
