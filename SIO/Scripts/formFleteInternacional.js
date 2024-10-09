import * as VueModule from 'vue';
import Vue3EasyDataTable from 'vue3-easy-data-table';
import * as XLSX from 'https://unpkg.com/xlsx/xlsx.mjs';

const app = VueModule.createApp({
    data() {
        return {
            tablaFletesInternacionales: null,
            fletesInternacionales: [],
            puertos: [],
            tiposContenedores: [],
            agentesCarga: [],
            fleteSeleccionado: {}
        }
    },
    mounted() {
        this.ObtenerAgentes();
        this.ObtenerPuertos();
        this.ObtenerTiposContenedores();
        $("#selectEstado").selectpicker();
    },
    methods: {
        sLoading: function () {
            $.LoadingOverlay("show", {
                color: "rgba(0, 0, 0, 0.3)",
                image: "",
                custom: "<div style='color:white; font-size: 100px;' class='fa fa-refresh fa-spin'></div>",
                fade: false
            });
        },

        hLoading: function () {
            $.LoadingOverlay("hide", true);
        },

        mostrarLoad: function () {
            this.sLoading();
        },

        ocultarLoad: function () {
            this.hLoading();
        },

        ObtenerFletesInternacionales: function() {
            this.mostrarLoad();
            fetch('FormFleteInternacional.aspx/ObtenerFletes', {
                method: 'POST',
                body: null,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                if (!res.ok) {
                    this.ocultarLoad();
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
            }).then(json => {
                this.ocultarLoad();
                var data = JSON.parse(json.d);
                this.fletesInternacionales = data;
                this.CargarDataTableFletesInternacionales();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Fletes Internacionales");
            });
        },

        ObtenerPuertos: function() {
            this.mostrarLoad();
            fetch('FormFleteInternacional.aspx/ObtenerPuertos', {
                method: 'POST',
                body: null,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                if (!res.ok) {
                    this.ocultarLoad();
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
            }).then(json => {
                this.ocultarLoad();
                var data = JSON.parse(json.d);
                this.puertos = data;
                $("#selectPuertoOrigen").html('<option value="0">-- Seleccionar --</option>');
                $("#selectPuertoDestino").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    if(element.PuertoDescripcion.includes("COLOMBI") ){
                        $("#selectPuertoOrigen").append($("<option />").val(element.PuertoId).text(element.PuertoDescripcion));
                    }
                    $("#selectPuertoDestino").append($("<option />").val(element.PuertoId).text(element.PuertoDescripcion));
                });
                $("#selectPuertoOrigen").selectpicker();
                $("#selectPuertoDestino").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Puertos");
            });
        },

        ObtenerAgentes: function() {
            this.mostrarLoad();
            fetch('FormFleteInternacional.aspx/ObtenerAgentes', {
                method: 'POST',
                body: null,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                if (!res.ok) {
                    this.ocultarLoad();
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
            }).then(json => {
                this.ocultarLoad();
                var data = JSON.parse(json.d);
                this.agentesCarga = data;
                $("#selectAgenteCarga").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    $("#selectAgenteCarga").append($("<option />").val(element.AgenteId).text(element.AgenteNombre));
                });
                $("#selectAgenteCarga").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Agentes");
            });
        },

        ObtenerTiposContenedores: function() {
            this.mostrarLoad();
            fetch('FormFleteInternacional.aspx/ObtenerTiposContenedores', {
                method: 'POST',
                body: null,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                if (!res.ok) {
                    this.ocultarLoad();
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
            }).then(json => {
                this.ocultarLoad();
                var data = JSON.parse(json.d);
                this.tiposContenedores = data;
                $("#selectTipoContenedor").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    $("#selectTipoContenedor").append($("<option />").val(element.TipoContenedorId).text(element.TipoContenedorNombre));
                });
                $("#selectTipoContenedor").selectpicker();
                this.ObtenerFletesInternacionales();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Contenedores");
            });
        },

        CargarInformacionEdicionFlete: function(registroId) {
            this.fletesInternacionales.forEach(fleteInternacional => {
                if(fleteInternacional.RegistroId == registroId) {
                    this.fleteSeleccionado = fleteInternacional;
                    this.$nextTick(function(){ 
                        $('#selectTipoContenedor').selectpicker('refresh'); 
                        $('#selectPuertoDestino').selectpicker('refresh'); 
                        $('#selectEstado').selectpicker('refresh'); 
                        $('#selectPuertoOrigen').selectpicker('refresh'); 
                        $('#selectAgenteCarga').selectpicker('refresh'); 
                    });
                    return false;
                }
            });
            $("#fleteInternacionalEdicionModal").modal('show');
        },

        LimpiarFleteSeleccionado: function() {

                this.fleteSeleccionado = {
                    Importar: 0,
                    RegistroId: 0,
                    AgenteCargaId: 0,
                    AgenteCargaNombre: "",
                    PuertoOrigenId: 0,
                    PuertoOrigenNombre: "",
                    PuertoDestinoId: 0,
                    PuertoDestinoNombre: "",
                    TipoContenedorId: 0,
                    TipoContenedorNombre: "",
                    DespachoAduanal: 0.0,
                    GastosPuertoOrigen: 0.0,
                    FleteInternacionalValor: 0.0,
                    LeadTimeCIF: 0.0,
                    Estado: 0
                }
                this.$nextTick(function(){ 
                    $('#selectTipoContenedor').selectpicker('refresh'); 
                    $('#selectPuertoDestino').selectpicker('refresh'); 
                    $('#selectEstado').selectpicker('refresh'); 
                    $('#selectPuertoOrigen').selectpicker('refresh'); 
                    $('#selectAgenteCarga').selectpicker('refresh'); 
                });

            $("#fleteInternacionalEdicionModal").modal('show');
        },

        CrearActualizarFleteNacional: function() {
            this.fleteSeleccionado.RegistroId = parseInt(this.fleteSeleccionado.RegistroId);
            this.fleteSeleccionado.AgenteCargaId = parseInt(this.fleteSeleccionado.AgenteCargaId);
            this.fleteSeleccionado.PuertoOrigenId = parseInt(this.fleteSeleccionado.PuertoOrigenId);
            this.fleteSeleccionado.PuertoDestinoId = parseInt(this.fleteSeleccionado.PuertoDestinoId);
            this.fleteSeleccionado.TipoContenedorId = parseInt(this.fleteSeleccionado.TipoContenedorId);
            this.fleteSeleccionado.Estado = parseInt(this.fleteSeleccionado.Estado);
            if(this.fleteSeleccionado.AgenteCargaId == 0 || this.fleteSeleccionado.PuertoOrigenId == 0 ||
                this.fleteSeleccionado.PuertoDestinoId == 0 || this.fleteSeleccionado.TipoContenedorId == 0) {
                toastr.warning("Por favor revisa que los campos (Agente Carga, Puerto Destino,  Puerto Origen y Tipo Contenedor) estén seleccionados", "Guardado Flete Nacional");
                return false;
            }
            var existeFleteDuplicado = false;
            var idFleteDuplicado = 0;
            this.fletesInternacionales.forEach(fleteInternacional => {
                if(fleteInternacional.TransportadorId == this.fleteSeleccionado.TransportadorId &&
                    fleteInternacional.CiudadOrigenId == this.fleteSeleccionado.CiudadOrigenId &&
                    fleteInternacional.CiudadDestinoId == this.fleteSeleccionado.CiudadDestinoId &&
                    fleteInternacional.VehiculoId == this.fleteSeleccionado.VehiculoId) {
                    existeFleteDuplicado = true;
                    idFleteDuplicado = fleteInternacional.RegistroId;
                    return false;
                }
            });
            if(existeFleteDuplicado) {
                if(confirm("Ya existe un flete con los mismos puertos, agente y contenedor con Id: " + idFleteDuplicado + " ¿Estás seguro de crear uno nuevo?")) {
                    this.mostrarLoad();
                    this.CrearActualizarFleteInternacionalEnviarFetch();
                    this.ocultarLoad();
                }
            } else {
                this.mostrarLoad();
                this.CrearActualizarFleteInternacionalEnviarFetch();
                this.ocultarLoad();
            }
        },

        CrearActualizarFleteInternacionalEnviarFetch: function() {
            var param = {
                fleteInternacional: JSON.parse(JSON.stringify(this.fleteSeleccionado))
            }
            fetch('FormFleteInternacional.aspx/CrearActualizarFleteNacional', {
                method: 'POST',
                body: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
            }).then(json => {
                // set the response data
                this.ObtenerFletesInternacionales();
                $("#fleteInternacionalEdicionModal").modal('hide');
                toastr.success("Flete guardado satisfactoriamente", "Guardado Flete Internacional");
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Guardado Flete Internacional");
            });
        },

        CargarDataTableFletesInternacionales: function() {
            if(this.tablaFletesInternacionales != null) {
                this.tablaFletesInternacionales.clear().destroy();
                this.index++;
            }
            this.$nextTick(() => this.tablaFletesInternacionales = $("#tableFleteInternacional").DataTable());
        },

        BorrarFleteInternacional: function(registroId) {
            if(confirm("¿Estas seguro de borrar el flete con Id: " + registroId + "?")) {
                var param = {
                    RegistroId: registroId
                }
                this.mostrarLoad();
                fetch('FormFleteInternacional.aspx/BorrarFleteNacional', {
                    method: 'POST',
                    body: JSON.stringify(param),
                    headers: {
                        'Accept': 'application/json',
                        'Content-type': 'application/json; charset=utf-8'
                    }
                }).then(res => {
                    // a non-200 response code
                    if (!res.ok) {
                        this.ocultarLoad();
                        // create error instance with HTTP status text
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }
                    return res.json();
                }).then(json => {
                    // set the response data
                    this.ocultarLoad();
                    this.ObtenerFletesInternacionales();
                    toastr.success("Flete eliminado satisfactoriamente", "Borrado Flete Internacional");
                }).catch(error => {
                    this.ocultarLoad();
                    toastr.error(error, "Borrado Flete Internacional");
                });
            }
        },

        DescargarArchivo: function (NombreArchivo, Ruta) {
            if (NombreArchivo != null) {
                var test = new FormData();
                window.location = "DownloadHandler.ashx?NombreArchivo=" + NombreArchivo + " &Ruta=" + Ruta;
            }
        },

        ExportarXLSFletes: function() {
            this.mostrarLoad();
            fetch('FormFleteInternacional.aspx/ExportarFletesXLS', {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                // a non-200 response code
                if (!res.ok) {
                    this.ocultarLoad();
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
            }).then(json => {
                // set the response data
                this.ocultarLoad();
                var data = JSON.parse(json.d);
                this.DescargarArchivo(data.fileName, data.path);
                toastr.success("Archivo creado satisfactoriamente", "Exportar Flete Internacional");
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Exportar Flete Internacional");
            });
        },

        ImportarFletes: function () {
            let file = null;
            for (var i = 0; i < this.$refs.filesImportar.files.length; i++ ){
                file = this.$refs.filesImportar.files[i];
            }
            if (file != null) {
                if (file.name.includes(".xls") || file.name.includes(".xlsx")) {
                    let fileReader = new FileReader();
                    fileReader.readAsArrayBuffer(file);
                    fileReader.onload = (e) => {
                        this.arrayBuffer = fileReader.result;
                        var data = new Uint8Array(this.arrayBuffer);
                        var arr = new Array();
                        for (var i = 0; i != data.length; ++i)
                            arr[i] = String.fromCharCode(data[i]);
                        var bstr = arr.join("");
                        var workbook = XLSX.read(bstr, { type: "binary" });
                        var first_sheet_name = workbook.SheetNames[0];
                        var worksheet = workbook.Sheets[first_sheet_name];
                        var rawData = XLSX.utils.sheet_to_json(worksheet, { raw: true });
                        if(rawData.length > 0) {
                            var responseColumnValidation = this.ValidarColumnasImportarFletes(rawData[0]);
                            if(responseColumnValidation.valid) {
                                this.mostrarLoad();
                                rawData.forEach((flete, index) => {
                                    this.fleteSeleccionado.Importar = 1;
                                    this.fleteSeleccionado.RegistroId = 0;
                                    this.fleteSeleccionado.AgenteCargaNombre = flete["Agente Carga"];
                                    this.fleteSeleccionado.PuertoOrigenNombre = flete["Puerto Origen"];
                                    this.fleteSeleccionado.PuertoDestinoNombre = flete["Puerto Destino"];
                                    this.fleteSeleccionado.TipoContenedorNombre = flete["Contenedor"];
                                    if(flete["Estado"] == "Activo" || flete["Estado"] == "ACTIVO") {
                                        this.fleteSeleccionado.Estado = 1;
                                    } else {
                                        this.fleteSeleccionado.Estado = 0;
                                    }
                                    this.fleteSeleccionado.DespachoAduanal = parseFloat(flete["Despacho Aduanal"]);
                                    this.fleteSeleccionado.GastosPuertoOrigen = parseFloat(flete["Gastos Puerto Origen"]);
                                    this.fleteSeleccionado.FleteInternacionalValor = parseFloat(flete["Valor Flete"]);
                                    this.fleteSeleccionado.LeadTimeCIF = parseFloat(flete["Lead Time CIF"]);
                                    debugger;
                                    this.CrearActualizarFleteInternacionalEnviarFetch();
                                    this.LimpiarFleteSeleccionado();
                                });
                                toastr.success("Importación completa con éxito, datos procesados: " + rawData.length, "Importar Flete Internacional");
                                this.$refs.filesImportar.files = null;
                                this.$refs.CerrarModalImportarFletes.click();
                                this.ocultarLoad();
                            } else {
                                toastr.error("Nombre no existente o mal escrito de la columna: " + responseColumnValidation.column, "Importar Flete Internacional");
                            }
                        } else {
                            toastr.info("No hay datos dentro del archivo", "Importar Flete Internacional");
                        }
                        //var arraylist = XLSX.utils.sheet_to_json(worksheet, { raw: true });
                        //this.filelist = [];
                        //console.log(this.filelist);
                    }
                } else {
                    toastr.error("El archivo debe ser de excel", "Importar Flete Internacional");
                }
            } else {
                toastr.info("No hay archivo para importar", "Importar Flete Internacional");
            }
        },

        ValidarColumnasImportarFletes: function(row) {
            var response = {
                valid: true,
                column: ""
            };
            if("Agente Carga" in row === false) {
                response.valid = false;
                response.column = "Agente Carga";
            }
            if("Puerto Origen" in row === false) {
                response.valid = false;
                response.column = "Puerto Origen";
            }
            if("Puerto Destino" in row === false) {
                response.valid = false;
                response.column = "Puerto Destino";
            }
            if("Contenedor" in row === false) {
                response.valid = false;
                response.column = "Contenedor";
            }
            if("Despacho Aduanal" in row === false) {
                response.valid = false;
                response.column = "Despacho Aduanal";
            }
            if("Gastos Puerto Origen" in row === false) {
                response.valid = false;
                response.column = "Gastos Puerto Origen";
            }
            if("Valor Flete" in row === false) {
                response.valid = false;
                response.column = "Valor Flete";
            }
            if("Lead Time CIF" in row === false) {
                response.valid = false;
                response.column = "Lead Time CIF";
            }
            if("Estado" in row === false) {
                response.valid = false;
                response.column = "Estado";
            }
            return response;
        }
    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');