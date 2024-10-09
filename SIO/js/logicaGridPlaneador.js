//Variable global que guarda el estado de la grilla
var globalGridState = null;

//Variable global que guarda el estado de la grilla de materia prima
var globalGridStateMP = null;

//Variable global que guarda el número de columnas estaticas
var numColumnasEstaticas = 9;

//Variable global que guarda el número de columngrups estaticos
var numColumnGroupsEstaticos = 2;

//Variable global codigo ERP
var codigoERP = '';

//Variable global que guarda la tabla de materia prima
var tableMateriaPrimaGlobal = '';

//Carga la grid principal
function cargarGrid() {
    try {
        //Destruye el objeto 
        w2ui['grid2'].destroy();        
    }
    catch (err) {
        console.log("error");
    }

    $(function () {
        $('#grid2').w2grid({
            method: 'GET', // need this to avoid 412 error on Safari
            name: 'grid2',
            show: {
                toolbar: true,
                footer: true,
                toolbarSave: true,
                selectColumn: false,
                lineNumbers: true
            },
            columns: [
                { field: 'recid', caption: 'ID', size: '50px', hidden: true, frozen: true },
                { field: 'idSaldos', caption: 'Identificador', size: '200px', hidden: true, frozen: true },
                { field: 'idPieza', caption: 'ID Pieza', size: '100px', hidden: true, frozen: true },
                { field: 'itemP', caption: 'Item Pieza', size: '50px', style: 'text-align: right; border-right: 2px solid blue;', frozen: true },
                { field: 'explosion', caption: 'Ex.', size: '30px', style: 'text-align: right; border-right: 2px solid blue;', frozen: true },
                { field: 'cantidad', caption: 'Cant.', size: '50px', style: 'text-align: right; border-right: 2px solid blue;', frozen: true },
                { field: 'idFamilia', caption: 'ID Familia', size: '100px', hidden: true, frozen: true },
                { field: 'familia', caption: 'Flia.', size: '50px', style: 'text-align: left; border-right: 2px solid blue;', frozen: true },
                { field: 'nombre', caption: 'Nombre', size: '150px', style: 'text-align: left; border-right: 2px solid blue;', frozen: true }
            ],
            onSave: function (event) {
                globalGridState = w2ui['grid2'].stateSave(true);
                w2ui['grid2'].stateRestore(globalGridState);
            }
        });
    });
}

function agregarMateriaPrima() {
    //Se carga el arreglo de columnGroups y se elimina el último registro que es creado automáticamente por la grid
    var columnGroups = w2ui['grid2'].columnGroups;
    var lastColG = columnGroups[columnGroups.length - 1].caption;
    if (lastColG === "") {
        columnGroups.pop();
    }
    var lastEl = columnGroups[columnGroups.length - 1];
    var nombreMP = lastEl.caption;
    var arrayNombreMP = nombreMP.split(" ");
    var arrayNumeroNombreMP = arrayNombreMP[0].split("#");
    var numero = parseInt(arrayNumeroNombreMP[1]);
    var index = numero + 1;
    var mpNew = $('[id$=cboMP] option:selected').text();
    var objMp = { span: 3, caption: '#' + index + ' ' + mpNew };
    columnGroups.push(objMp); 
    
    //Se carga el arreglo de columnas quitandole las primeras columnas correspondientes a id, idSaldos, idPieza cantidad, idFamilia, familia y nombre
    var columns = w2ui['grid2'].columns;
    for (i = 0; i < numColumnasEstaticas; i++) {
        columns.shift();
    }

    var lastCol = columns[columns.length - 1];
    var nombreCol = lastCol.field;
    var num = parseInt(nombreCol.substring(4, nombreCol.length));    

    //Se carga el arreglo de las nuevas columnas para la nueva materia prima desde el indice correspondiente
    var indexCol = num + 1;
    var obj1 = { field: 'cant' + indexCol, caption: 'Cant', style: 'text-align: center', size: '50px', editable: { type: 'text' } };
    var obj2 = { field: 'long' + indexCol, caption: 'Long', style: 'text-align: center', size: '50px', editable: { type: 'text' } };
    var obj3 = { field: 'perf' + indexCol, caption: 'Perf', style: 'text-align: center; border-right: 2px solid blue;', size: '50px', editable: { type: 'text' } };
    columns.push(obj1);
    columns.push(obj2);
    columns.push(obj3);

    //Se carga el arreglo de records
    var records = w2ui['grid2'].records;    

    //se carga la grid principal
    recargarGrid(columnGroups, columns, records);   

    //se carga la grid de materia prima
    var recordsMP = w2ui['gridMP'].records;
    var arrayRecordsMP = [];
    
    ////identificamos si se requiere un registro vacio en la gridMP
    //var objRecordVacio = { recid: 0, recid: ' ', mp: ' ', codigo: ' ' };
    //if (recordsMP.length < 8) {
    //    recordsMP.push(objRecordVacio);
    //}

    for (var j = 0; j < recordsMP.length; j++)
    {
        arrayRecordsMP.push(recordsMP[j].mp);
    }
        
    var value = jQuery.inArray(mpNew, arrayRecordsMP);

    if (value === -1)
    {
        var indexRecordMP = recordsMP.length + 1;
        var objRecord = { recid: indexRecordMP, mp: mpNew, codigo: codigoERP };
        codigoERP = '';
        recordsMP.push(objRecord);

        if (recordsMP[0].recid == 0)
        {
            if (recordsMP.length > 10) {
                recordsMP.shift();
            }
        }
        else if (recordsMP[0].recid != 0 && recordsMP.length < 10)
        {
            var objRecordEmpty = { recid: 0, mp: '', codigo: '' };
            recordsMP.push(objRecordEmpty);
        }
        cargarGridMPEditable();
        //se cargan los records
        w2ui['gridMP'].add(recordsMP);
        console.log(recordsMP);
        //es necesario hacerle refresh a la grid.
        w2ui['gridMP'].refresh();
    }
}

function cargarPlaneador(filtro, idEstado, kambam) {    
    try {
        //Destruye el objeto 
        w2ui['grid2'].destroy();
        w2ui['gridMP'].destroy();
    }
    catch (err) {
        console.log("error");
    }
    cargarGrid();
    var idOfa = $('[id$=cboRaya] option:selected').val();
    var parametros = { idOfa: idOfa, filtro: filtro, kambam: kambam, idEstado: idEstado, columnasEstaticas: numColumnasEstaticas };

    ejecutarAjax(parametros, 'cargarPlaneador', function (result) {
        var data = result;
        console.log(data.d);
        var colGroups = data.d;
        var item = [];
        for (var x = numColumnGroupsEstaticos; x < colGroups.length; x++) {
            item.push(colGroups[x].caption);
        }
        var col = [];
        if (idEstado < 2) {
            for (var i = 0; i < colGroups.length - numColumnGroupsEstaticos; i++) {
                var index = i + 1;
                var obj1 = { field: 'cant' + index, caption: 'Cant', style: 'text-align: center', size: '50px', editable: { type: 'text' } };
                var obj2 = { field: 'long' + index, caption: 'Long', style: 'text-align: center', size: '50px', editable: { type: 'text' } };
                var obj3 = { field: 'perf' + index, caption: 'Perf', style: 'text-align: center; border-right: 2px solid blue;', size: '50px', editable: { type: 'text' } };
                col.push(obj1);
                col.push(obj2);
                col.push(obj3);
            }
        }
        else
        {
            for (var j = 0; j < colGroups.length - numColumnGroupsEstaticos; j++) {
                var index2 = j + 1;
                var obj4 = { field: 'cant' + index2, caption: 'Cant', style: 'text-align: center', size: '50px' };
                var obj5 = { field: 'long' + index2, caption: 'Long', style: 'text-align: center', size: '50px' };
                var obj6 = { field: 'perf' + index2, caption: 'Perf', style: 'text-align: center; border-right: 2px solid blue;', size: '50px' };
                col.push(obj4);
                col.push(obj5);
                col.push(obj6);
            }
        }
        
        //se agrega el grupo de columnas es decir la meteria prima
        w2ui['grid2'].columnGroups = colGroups;
        //se agregan las columnas correspondientes
        w2ui['grid2'].addColumn(col);
        //se carga el archivo donde se guarda el json con los records
        //w2ui['grid2'].load('data/list.json');
        //w2ui['grid2'].load('http://172.21.0.101/ExploDocs/list.json');

        //Se cargan los records

        var records = null;
        var columnGroups = w2ui['grid2'].columnGroups;
        var parametrosRecords = { idOfa: idOfa, filtro: filtro, columnGroups: columnGroups, kambam: kambam, idEstado: idEstado };
        ejecutarAjax(parametrosRecords, 'getRecords', function (result) {
            var data = result;
            var obj = data.d;
            if (obj !== "")
            {
                var json = JSON.parse(obj);
                records = json["records"];

                w2ui['grid2'].add(records);
                //es necesario hacerle refresh a la grid.
                w2ui['grid2'].refresh();
                cargarColumnas();
                var parametrosRecordsMP = { idOfa: idOfa, filtro: filtro, columnGroups: columnGroups, kambam: kambam };
                ejecutarAjax(parametrosRecordsMP, 'getRecordsMP', function (result) {
                    if (idEstado < 2) {
                        cargarGridMPEditable();
                    }
                    else
                    {
                        cargarGridMPNoEditable();
                    }
                    var data = result;
                    var obj = data.d;
                    var json = JSON.parse(obj);
                    var recordsMP = json["records"];
                    //Se cargan los records
                    w2ui['gridMP'].add(recordsMP);
                    //es necesario hacerle refresh a la grid.
                    w2ui['gridMP'].refresh();

                    setVisibleButtons(idOfa);
                });
            }    
            else
            {
                showAlert("No se encontraron registros para esta orden.");
            }
        });        
    });
}

//Metodo general para ejecutar las consultas por medio del ajax
function ejecutarAjax(parametros, metodoWeb, callback) {
    $.ajax({
        type: "POST", //Tipo de peticiòn
        url: "WSSIO.asmx/" + metodoWeb + "", // Url y metodo que se invocae
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parametros),
        dataType: "json", //Tipo de dato con el que se realiza la llamada}        
        success: function (data) {
            callback(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown)
        { alert('error'); }
    });
}

function eliminarRegistros() {
    var selection = w2ui.grid2.getSelection();

    if (selection.length > 0) {
        w2confirm('Desea eliminar los registros: ' + selection + ' ?')
            .yes(function () {
                for (i = 0; i < selection.length; i++) {
                    w2ui['grid2'].remove(selection[i]);
                }
                w2ui['grid2'].reset();
                w2ui['grid2'].refresh();
                console.log('YES');
                cargarColumnas();
            })
            .no(function () {
                console.log('NO');
                cargarColumnas();
            });
    }
    else {
        showAlert('Debe seleccionar los registros. Gracias');
    }
}

function popupMateriaPrima(contenidoPopUp) {
    var mp = $('[id$=cboMP] option:selected').text();
    w2popup.open({
        title: 'Materia Prima',
        body: '<div class="w2ui-upper">' +
        '<table>' +
        '<tr><td><strong>Agregar Materia Prima: ' + mp + '</strong></td></tr>' +
        '</table>' +
        '<table border="1px solid blue">' +
        '<tr> ' +
        '<td align="center"><strong>Código</strong></td>' +
        '<td><strong>Nombre</strong></td>' +
        '<td align="right"><strong>Cant. Exist. Inven.</strong></td>' +
        '<td align="center"><strong>Und. Inven.</strong></td>' +
        '<td align="right"><strong>Cant. Exist. Orden</strong></td>' +
        '<td align="center"><strong>Und. Orden Orden.</strong></td>' +
        '<td align="center"><strong>ID Bodega</strong></td>' +
        '<td align="left"><strong>Bodega</strong></td>' +        
        '</tr>' + contenidoPopUp +
        '</table>' +
        '<table>' +
        '<tr>' +
        '<td><strong>Código ERP:</strong></td>' +
        '<td><input type="number" id="txtCodigoERP" name="txtCodigoERP" width="100px" onchange="onChangeCodigoERP()"></td>'+
        '</tr>' +
        '</table>'+
        '</div>',
        buttons: '<button class="w2ui-btn" onclick="cerrarPopUp();">Cerrar</button> ' +
        '<button class="w2ui-btn" onclick="return agregarMateriaPrima();">Agregar</button>',
        width: 700,
        height: 450,
        overflow: 'hidden',
        color: '#333',
        speed: '0.3',
        opacity: '0.8',
        modal: true,
        showClose: true,
        showMax: true,
        onOpen: function (event) { console.log('open'); },
        onClose: function (event) { console.log('close'); cargarColumnas(); },
        onMax: function (event) { console.log('max'); },
        onMin: function (event) { console.log('min'); },
        onKeydown: function (event) { console.log('keydown'); }
    });
}

function onChangeCodigoERP()
{
    if (document.getElementById("txtCodigoERP").value !== '' || document.getElementById("txtCodigoERP").value !== '0')
    {
        codigoERP = document.getElementById("txtCodigoERP").value;
    }
    else
    {
        codigoERP = '';
    }
}

function recargarGrid(colGroups, col, records) {
    cargarGrid();
    //se agrega el grupo de columnas es decir la meteria prima
    w2ui['grid2'].columnGroups = colGroups;
    //se agregan las columnas correspondientes
    w2ui['grid2'].addColumn(col);
    //se cargan los records
    w2ui['grid2'].add(records);
    //es necesario hacerle refresh a la grid.
    w2ui['grid2'].refresh();
    //Se cierra el pop up que haya abierto
    cerrarPopUp();    
}



function showAlert(msg) {
    w2alert(msg)
        .ok(function ()
        {
            console.log('ok');
            //cargarColumnas();
        });
}

function addColumnas(items) {
    var cbo = $('[id$=cboColumns]');  
    cbo.val([]);
    cbo.append(new Option('Seleccione', '0'));
    for (i = 0; i < items.length; i++)
    {
        cbo.append(new Option(items[i], i+1));
    }   
}

function deleteColumn(event)
{
    var column = $('[id$=cboColumns] option:selected').text();
    if (column !== "Seleccione")
    {
        w2confirm('Desea eliminar la columna: ' + column + ' ?')
            .yes(function () {
                precargarGrid(column);
                $('[id$=cboColumns] option:selected').remove();
                $('[id$=cboColumns]').val("0");
                console.log('YES');
            })
            .no(function () {
                $('[id$=cboColumns]').val("0");
                console.log('NO');
            });
    }
}

function precargarGrid(column) {
    //Se carga el arreglo de columnGroups y se elimina el último registro que es creado automáticamente por la grid
    var columnGroups = w2ui['grid2'].columnGroups;
    var columnGroupsAux = [];

    for (var a = 0; a < columnGroups.length; a++)
    {
        columnGroupsAux.push(columnGroups[a]);
    }
    var index = 0;
    var lastColG = columnGroups[columnGroups.length - 1].caption;
    if (lastColG === "") {
        columnGroups.pop();
        columnGroupsAux.pop();
    }
    for (var i = 0; i < columnGroups.length; i++)
    {
        if (columnGroups[i].caption === column)
        {
            index = i;
            columnGroupsAux.splice(i, 1);
        }
    }

    var nombColumnMp = column;
    //Obtenemos la posición a partir del espacio
    var posicionNombColumnMp = nombColumnMp.indexOf(' ');//pos 2 o 3

    //Obtenemos el nombre de la MP, sin el numero de columna ni espacios (left,right)
    nombColumnMP = nombColumnMp.substring(posicionNombColumnMp).trim();

    //Variable para acumular el numero de Mp en el grid2
    var acum = 0

    //validar si se elimina la MP del gridMP, si el acum es 1 se elimina.
    for (var j = 0; j < columnGroups.length; j++) {

        var nombColumnGrid2 = columnGroups[j].caption;

        //Obtenemos la posición a partir del espacio
        var posicionNombColumnGrid2 = nombColumnGrid2.indexOf(' ');//pos 2 o 3

        //Obtenemos el nombre de la MP, sin el numero de columna ni espacios (left,right)
        nombColumnGrid2 = nombColumnGrid2.substring(posicionNombColumnGrid2).trim();

        if (acum < 2) {
                
            if (nombColumnGrid2 === nombColumnMP)
        {
            acum += 1;
        }
        }
    }


    //Se seleccionan los records que harán parte de la grid de materia prima
    var arrayNombreColumn = column.split(" ");
    var cadena = '';
    for (var x = 1; x < arrayNombreColumn.length; x++)
    {
        cadena += arrayNombreColumn[x] + " ";
    }
    var nombreColumn = cadena.trim();
    var recordsMP = [];
    
    for (var j = 2; j < columnGroupsAux.length; j++) {
        var nombre = columnGroupsAux[j].caption;
        var arrayNombre = nombre.split(" ");
        for (var y = 1; y < arrayNombre.length; y++) {
            cadena += arrayNombre[y] + " ";
        }
        nombre = cadena.trim();
        recordsMP.push(nombre);       
    }

    var value = jQuery.inArray(nombreColumn, recordsMP);
    if (value === -1) {
        var recordsMPOriginal = w2ui['gridMP'].records;
        var recordsMPOriginalAux = [];

        for (var h = 0; h < recordsMPOriginal.length; h++) {
            recordsMPOriginalAux.push(recordsMPOriginal[h]);
        }


        //Elimina la materia prima de GridMp, correspondiente a la que se elimina de grid2
        if (acum === 1)
        {
            for (var k = 0; k < recordsMPOriginal.length; k++) {

                var nombreMP = recordsMPOriginal[k].mp;

                if (nombreMP === nombreColumn) {
                    recordsMPOriginalAux.splice(k, 1);
                }
            }
        }
      

        cargarGridMPEditable();        
        //se cargan los records
        if (recordsMPOriginalAux[0].recid != 0) {
            if (recordsMPOriginalAux.length <= 9 || recordsMPOriginalAux.length == 11) {
                w2ui['gridMP'].add({ recid: 0, mp: '', codigo: '' });
            }
        }
        else {
            if (recordsMPOriginalAux.length > 10)
            {
                recordsMPOriginalAux.shift();
            }
        }
        
        w2ui['gridMP'].add(recordsMPOriginalAux);
        //es necesario hacerle refresh a la grid.
        w2ui['gridMP'].refresh();
    }

    //Se selecciona el index exacto del columngroup, se borran los dos primeros agrupadores
    index = index - 2;
    
    //Se carga el arreglo de columnas quitandole las primeras columnas correspondientes a id, idSaldos, idPieza, cantidad, idFamilia, familia y nombre
    var columns = w2ui['grid2'].columns;
    
    for (var z = 0; z < numColumnasEstaticas; z++) {
        columns.shift();
    }

    //Se calcula el indice de las columnas a crear
    var indexColumn = (index * 3);
    columns.splice(indexColumn, 3);    

    //Se carga el arreglo de records
    var records = w2ui['grid2'].records;    

    w2ui['grid2'].destroy();
    recargarGrid(columnGroupsAux, columns, records);
}

function cargarColumnas()
{
    var columnGroups = w2ui['grid2'].columnGroups;
    var lastCol = columnGroups[columnGroups.length - 1].caption;
    if (lastCol === "")
    {
        columnGroups.pop();
    }
    var item = [];
    for (var x = numColumnGroupsEstaticos; x < columnGroups.length; x++) {
        item.push(columnGroups[x].caption);
    }
    addColumnas(item);
}

//Carga la grid de materia prima editable
function cargarGridMPEditable() {
    try {
        //Destruye el objeto 
        w2ui['gridMP'].destroy();

    }
    catch (err) {
        console.log("error");
    }

    $(function () {
        $('#gridMP').w2grid({
            method: 'GET', // need this to avoid 412 error on Safari
            name: 'gridMP',
            show: {
                toolbar: true,
                footer: false,
                toolbarSave: true,                
                selectColumn: false,
                lineNumbers: false
            },
            columns: [
                { field: 'recid', caption: 'ID', size: '50px', hidden: true, frozen: true },
                { field: 'mp', caption: 'Materia Prima', style: 'text-align: left', size: '200px', frozen: true },
                { field: 'codigo', caption: 'Código', style: 'text-align: center', size: '100px', editable: { type: 'text' }, frozen: true }
            ], 
            onSave: function (event) {
                globalGridStateMP = w2ui['gridMP'].stateSave(true);
                w2ui['gridMP'].stateRestore(globalGridStateMP);
            }
        });
    });
}

//Carga la grid de materia prima NO editable
function cargarGridMPNoEditable() {
    try {
        //Destruye el objeto 
        w2ui['gridMP'].destroy();
    }
    catch (err) {
        console.log("error");
    }

    $(function () {
        $('#gridMP').w2grid({
            method: 'GET', // need this to avoid 412 error on Safari
            name: 'gridMP',
            show: {
                toolbar: true,
                footer: false,
                toolbarSave: true,
                selectColumn: false,
                lineNumbers: false
            },
            columns: [
                { field: 'recid', caption: 'ID', size: '50px', hidden: true, frozen: true },
                { field: 'mp', caption: 'Materia Prima', style: 'text-align: left', size: '200px', frozen: true },
                { field: 'codigo', caption: 'Código', style: 'text-align: center', size: '100px', frozen: true }
            ],
            onSave: function (event) {
                globalGridStateMP = w2ui['gridMP'].stateSave(true);
                w2ui['gridMP'].stateRestore(globalGridStateMP);
            }
        });
    });
}

function guardarExplosion(filtro, idEstado, kambam)
{
    var msj = "";
    w2ui.gridMP.save();
    var records = w2ui['gridMP'].records;
    var parametros = { records: records };
    ejecutarAjax(parametros, 'validarCodigoMateriaPrima', function (result) {
        var data = result.d;
        if (data !== "") {
            msj = data;
            showAlert(msj);
        }
        else {
            w2ui.grid2.save();
            var idOfaP = $('[id$=cboNoSolicitud] option:selected').val();
            var idOfa = $('[id$=cboRaya] option:selected').val();
            var idFamilia = $('[id$=cboFamilia] option:selected').val();
            var columnGroups = w2ui['grid2'].columnGroups;
            var columns = w2ui['grid2'].columns;
            var records = w2ui['grid2'].records;
            var recordsMateriaPrima = w2ui['gridMP'].records;
            var parametros = { idOfaP: idOfaP, idOfa: idOfa, columnGroups: columnGroups, columns: columns, records: records, recordsMateriaPrima: recordsMateriaPrima, idFamilia: idFamilia };
            ejecutarAjax(parametros, 'guardarExplosion', function (result) {
                data = result.d;
                var msj = '';
                if (data === 'OK') {
                    msj = 'Registro guardado con éxito.';
                    cargarPlaneador(filtro, idEstado, kambam);
                }
                else if (data === '') {
                    msj = 'No se guardaron registros nuevos.';
                }
                else {
                    msj = data;
                }
               
                showAlert(msj);
            });
        }
    });
}

function updateExplosion()
{
    var msj = "";
    w2ui.gridMP.save();
    var records = w2ui['gridMP'].records;
    var parametros = { records: records };
    ejecutarAjax(parametros, 'validarCodigoMateriaPrima', function (result) {
        var data = result.d;
        if (data !== "") {
            msj = data;
            showAlert(msj);
        }
        else {
            w2ui.grid2.save();
            var idOfaP = $('[id$=cboNoSolicitud] option:selected').val();
            var idOfa = $('[id$=cboRaya] option:selected').val();
            var columnGroups = w2ui['grid2'].columnGroups;
            var columns = w2ui['grid2'].columns;
            var records = w2ui['grid2'].records;
            var recordsMateriaPrima = w2ui['gridMP'].records;
            var idExploPrincipal = $('[id$=lblIdExploPrincipal]').val();
            var parametros = { idOfa: idOfa, columnGroups: columnGroups, columns: columns, records: records, recordsMateriaPrima: recordsMateriaPrima, idExploPrincipal: idExploPrincipal };
            ejecutarAjax(parametros, 'updateExplosion', function (result) {
                data = result.d;
                var msj = '';
                if (data === 'OK') {
                    msj = 'Registro actualizado con éxito.';
                }
                else if (data === '') {
                    msj = 'Ha ocurrido un error. Intente nuevamente. Gracias';
                }
                else {
                    msj = data;
                }
                setVisibleButtons(idOfa);
                showAlert(msj);
            });
        }
    });
}

function solicitudMateriaPrima()
{
    var msj = "";
    w2ui.gridMP.save();
    var records = w2ui['gridMP'].records;
    var parametros = { records: records };
    ejecutarAjax(parametros, 'validarCodigoMateriaPrima', function (result) {
        var data = result.d;
        if (data !== "") {
            msj = data;
            showAlert(msj);
        }
        else {
            var columnGroups = w2ui['grid2'].columnGroups;
            var records = w2ui['grid2'].records;
            var recordsMateriaPrima = w2ui['gridMP'].records;
            var idExploPrincipal = $('[id$=lblIdExploPrincipal]').val();
            var idPlanta = $('[id$=cboPlanta] option:selected').val();
            var parametros = { records: records, columnGroups: columnGroups, recordsMateriaPrima: recordsMateriaPrima, idPlanta: idPlanta };
            ejecutarAjax(parametros, 'calcularResumenMateriaPrima', function (result) {
                data = result.d;
                var tableEncabezado = '<table border="1px solid blue"><tr><td><strong>Materia Prima</strong></td><td><strong>Familia</strong></td><td align="rigth"><strong>Total</strong></td></tr>';
                var tableFin = '</table>';
                var table = '';
                var registros = '';
                for (var i = 0; i < data.length; i++) {
                    registros += '<tr><td>' + data[i].materiaPrima + '</td><td>' + data[i].familia + '</td><td>' + data[i].result + '</td></tr>';
                }
                table = tableEncabezado + registros + tableFin;
                //popUpSolicitudMateriaPrima(table);
            });
        }
    });    
}

function popUpSolicitudMateriaPrima(table) {
    var idEstado = $('[id$=lblIdEstado]').val();
    var button = '';
    if (idEstado !== '4') {
        button = '<button class="w2ui-btn" onclick="return agregarSolicitudMateriaPrima();">Solicitud Materia Prima</button>';
    }
    w2popup.open({
        title: 'Resumen Solicitud Materia Prima',
        body: '<div class="w2ui-upper">' + table + '</div>',
        buttons: '<button class="w2ui-btn" onclick="cerrarPopUp();">Cerrar</button> ' +
        button,
        width: 300,
        height: 400,
        overflow: 'hidden',
        color: '#333',
        speed: '0.3',
        opacity: '0.8',
        modal: true,
        showClose: true,
        showMax: true,
        onOpen: function (event) { console.log('open'); },
        onClose: function (event) { console.log('close'); cargarColumnas(); },
        onMax: function (event) { console.log('max'); },
        onMin: function (event) { console.log('min'); },
        onKeydown: function (event) { console.log('keydown'); }
    });
}

function agregarSolicitudMateriaPrima()
{
    var columnGroups = w2ui['grid2'].columnGroups;
    var records = w2ui['grid2'].records;
    var recordsMateriaPrima = w2ui['gridMP'].records;
    var idOfa = $('[id$=cboRaya] option:selected').val();
    var idOfaPadre = $('[id$=cboNoSolicitud] option:selected').val();
    var idExploPrincipal = $('[id$=lblIdExploPrincipal]').val();
    var idPlanta = $('[id$=cboPlanta] option:selected').val();
    var parametros = { records: records, columnGroups: columnGroups, recordsMateriaPrima: recordsMateriaPrima, idOfa: idOfa, idOfaPadre: idOfaPadre, idExploPrincipal: idExploPrincipal, idPlanta: idPlanta };
    ejecutarAjax(parametros, 'agregarSolicitudMateriaPrima', function (result) {
        data = result.d;
        var msj = '';
        if (data === 'OK') {
            msj = 'Solicitud de materia prima generada correctamente.';
        }
        else if (data === '') {
            msj = 'Ha ocurrido un error, en la solicitud de materia prima';
        }
        else {
            msj = data;
        }
        setVisibleButtons(idOfa);
        cerrarPopUp();
        data = result.d;
        showAlert(data);
          
        //recargar la pagin pendiente revisar 
        //location.reload();
     
    });
}

function cerrarPopUp() {
    try {
        $('[id$=cboMP]').val("0");
        w2popup.close();

    }
    catch (err) { console.log('error: no hay pop up abierto'); }
}
function setVisibleButtons(idOfa) {
    var estado = 0;
    var descEstado = "NUEVO";
    var parametros = {
        idOfa: idOfa,
        //idFamilia: idFamilia

        idFamilia: $('[id$=cboFamilia] option:selected').val()
    };
    ejecutarAjax(parametros, 'getEstadoExplosionador', function (result) {
        data = result.d;
        var idEstado = data.id;
        var descEstado = data.descripcion;
        if (data !== 0) {
            $('[id$=lblIdEstado]').text(idEstado);
            $('[id$=lblEstado]').text(descEstado);
            estado = idEstado;

            //SIN ESTADO
            if (estado === 0) {
                $('[id$=btnNuevo]').show();
                $('[id$=btnGuardarExplosion]').show();
                $('[id$=btnActualizar]').hide();
                $('[id$=btnConfirmar]').hide();
                $('[id$=btnAnular]').hide();
                $('[id$=btnSolicitudMateriaPrima]').hide();

                $('[id$=cboMP]').prop("disabled", false);
                $('[id$=cboColumns]').prop("disabled", false);
                $('[id$=btnEliminarRegitro]').prop("disabled", false);
            }

            //GUARDADO
            else if (estado === 1) {
                $('[id$=btnNuevo]').show();
                $('[id$=btnGuardarExplosion]').show();
                $('[id$=btnActualizar]').hide();
                $('[id$=btnConfirmar]').show();
                $('[id$=btnAnular]').show();
                $('[id$=btnSolicitudMateriaPrima]').hide();

                $('[id$=cboMP]').prop("disabled", false);
                $('[id$=cboColumns]').prop("disabled", false);
                $('[id$=btnEliminarRegitro]').prop("disabled", false);
                $('[id$=lblEstado]').text("GUARDADO");
            }

            //CONFIRMADO
            else if (estado === 2) {
                $('[id$=btnNuevo]').show();
                $('[id$=btnGuardarExplosion]').hide();
                $('[id$=btnActualizar]').hide();
                $('[id$=btnConfirmar]').hide();
                $('[id$=btnAnular]').show();
                $('[id$=btnSolicitudMateriaPrima]').show();

                $('[id$=cboMP]').prop("disabled", true);
                $('[id$=cboColumns]').prop("disabled", true);
                $('[id$=btnEliminarRegitro]').prop("disabled", true);
                $('[id$=lblEstado]').text("CONFIRMADO");
            }

            //ANULADO
            else if (estado === 3) {
                $('[id$=btnNuevo]').show();
                $('[id$=btnGuardarExplosion]').hide();
                $('[id$=btnActualizar]').hide();
                $('[id$=btnConfirmar]').hide();
                $('[id$=btnAnular]').hide();
                $('[id$=btnSolicitudMateriaPrima]').hide();

                $('[id$=cboMP]').prop("disabled", true);
                $('[id$=cboColumns]').prop("disabled", true);
                $('[id$=btnEliminarRegitro]').prop("disabled", true);
                $('[id$=lblEstado]').text("ANULADO");
            }

            //SOLICITADO
            else if (estado === 4) {
                $('[id$=btnNuevo]').show();
                $('[id$=btnGuardarExplosion]').hide();
                $('[id$=btnActualizar]').hide();
                $('[id$=btnConfirmar]').hide();
                $('[id$=btnAnular]').hide();
                $('[id$=btnSolicitudMateriaPrima]').hide();

                $('[id$=cboMP]').prop("disabled", true);
                $('[id$=cboColumns]').prop("disabled", true);
                $('[id$=btnEliminarRegitro]').prop("disabled", true);
                $('[id$=lblEstado]').text("SOLICITADO");
            }
        }
    });
}