import * as VueModule from 'vue';
import Vue3EasyDataTable from 'vue3-easy-data-table';
import * as XLSX from 'https://unpkg.com/xlsx/xlsx.mjs';

const app = VueModule.createApp({
    data() {
        return {
            fleteSeleccionado: {
                Importar: 0,
                RegistroId: 0,
                TransportadorId: 0,
                TransportadorNombre: "",
                CiudadOrigenId: 0,
                CiudadOrigenNombre: "",
                CiudadDestinoId: 0,
                CiudadDestinoNombre: "",
                VehiculoId: 0,
                VehiculoDescripcion: "",
                ValorFlete: 0.0,
                ValorPrima: 0.0,
                Estado: 0,
                FechaCreacion: '1900-01-01T00:00:00',
                CreadoPor: "",
                FechaActualizacion: '1900-01-01T00:00:00',
                ActualizadoPor: ""
            },
            fletesNacionales: [],
            transportadores: [],
            tiposVehiculos: [],
            ciudades: [],
            index: 0,
            tablaFletesNacionales: null,
        }
    },
    mounted() {
        this.ObtenerTiposVehiculos();
        this.ObtenerCiudades();
        this.ObtenerTransportadores();
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

        ObtenerTransportadores: function() {
            this.mostrarLoad();
            fetch('FormFleteNacional.aspx/ObtenerTransportadores', {
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
                this.transportadores = data;
                $("#selectTransportador").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    $("#selectTransportador").append($("<option />").val(element.TransportadorId).text(element.TransportadorNombre));
                });
                $("#selectTransportador").selectpicker();
                this.ObtenerFletesNacionales();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Transportadores");
            });
        },

        ObtenerTiposVehiculos: function() {
            this.mostrarLoad();
            fetch('FormFleteNacional.aspx/ObtenerTiposVehiculos', {
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
                this.tiposVehiculos = data;
                $("#selectTipoVehiculo").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    $("#selectTipoVehiculo").append($("<option />").val(element.TipoVehiculoId).text(element.TipoVehiculoNombre));
                });
                $("#selectTipoVehiculo").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Tipos Vehiculo");
            });
        },

        ObtenerCiudades: function() {
            this.mostrarLoad();
            fetch('FormFleteNacional.aspx/ObtenerCiudades', {
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
                this.ciudades = data;
                $("#selectCiudadOrigen").html('<option value="0">-- Seleccionar --</option>');
                $("#selectCiudadDestino").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    $("#selectCiudadOrigen").append($("<option />").val(element.ciu_id).text(element.ciudad));
                    $("#selectCiudadDestino").append($("<option />").val(element.ciu_id).text(element.ciudad));
                });
                $("#selectCiudadOrigen").selectpicker();
                $("#selectCiudadDestino").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Ciudades");
            });
        },

        ObtenerFletesNacionales: function() {
            this.mostrarLoad();
            fetch('FormFleteNacional.aspx/ObtenerFletes', {
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
                this.fletesNacionales = data;
                this.CargarDataTableFletesNacionales();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Fletes Nacionales");
            });
        },

        CargarDataTableFletesNacionales: function() {
            if(this.tablaFletesNacionales != null) {
                this.tablaFletesNacionales.clear().destroy();
                this.index++;
            }
            this.$nextTick(() => this.tablaFletesNacionales = $("#tableFleteNacional").DataTable());
        },

        CargarInformacionEdicionFlete: function(registroId) {
            this.fletesNacionales.forEach(fleteNacional => {
                if(fleteNacional.RegistroId == registroId) {
                    this.fleteSeleccionado = fleteNacional;
                    this.$nextTick(function(){ 
                        $('#selectTransportador').selectpicker('refresh'); 
                        $('#selectTipoVehiculo').selectpicker('refresh'); 
                        $('#selectEstado').selectpicker('refresh'); 
                        $('#selectCiudadOrigen').selectpicker('refresh'); 
                        $('#selectCiudadDestino').selectpicker('refresh'); 
                    });
                    return false;
                }
            });
            $("#fleteNacionalEdicionModal").modal('show');
        },

        CrearActualizarFleteNacional: function() {
            this.fleteSeleccionado.Importar = 0;
            this.fleteSeleccionado.RegistroId = parseInt(this.fleteSeleccionado.RegistroId);
            this.fleteSeleccionado.TransportadorId = parseInt(this.fleteSeleccionado.TransportadorId);
            this.fleteSeleccionado.CiudadOrigenId = parseInt(this.fleteSeleccionado.CiudadOrigenId);
            this.fleteSeleccionado.CiudadDestinoId = parseInt(this.fleteSeleccionado.CiudadDestinoId);
            this.fleteSeleccionado.VehiculoId = parseInt(this.fleteSeleccionado.VehiculoId);
            this.fleteSeleccionado.Estado = parseInt(this.fleteSeleccionado.Estado);
            if(this.fleteSeleccionado.TransportadorId == 0 || this.fleteSeleccionado.CiudadOrigenId == 0 ||
                this.fleteSeleccionado.CiudadDestinoId == 0 || this.fleteSeleccionado.VehiculoId == 0) {
                toastr.warning("Por favor revisa que los campos (Transportador, Ciudad Destino,  Ciudad Origen y Vehiculo) estén seleccionados", "Guardado Flete Nacional");
                return false;
            }
            var existeFleteDuplicado = false;
            var idFleteDuplicado = 0;
            this.fletesNacionales.forEach(fleteNacional => {
                if(fleteNacional.TransportadorId == this.fleteSeleccionado.TransportadorId &&
                    fleteNacional.CiudadOrigenId == this.fleteSeleccionado.CiudadOrigenId &&
                    fleteNacional.CiudadDestinoId == this.fleteSeleccionado.CiudadDestinoId &&
                    fleteNacional.VehiculoId == this.fleteSeleccionado.VehiculoId) {
                    existeFleteDuplicado = true;
                    idFleteDuplicado = fleteNacional.RegistroId;
                    return false;
                }
            });
            if(existeFleteDuplicado) {
                if(confirm("Ya existe un flete con la mismas ciudades, transportador y vehiculo con Id: " + idFleteDuplicado + " ¿Estás seguro de crear uno nuevo?")) {
                    this.mostrarLoad();
                    this.CrearActualizarFleteNacionalEnviarFetch();
                    this.ocultarLoad();
                }
            } else {
                this.mostrarLoad();
                this.CrearActualizarFleteNacionalEnviarFetch();
                this.ocultarLoad();
            }
        },

        CrearActualizarFleteNacionalEnviarFetch: function() {
            var param = {
                fleteNacional: JSON.parse(JSON.stringify(this.fleteSeleccionado))
            }
            console.log(JSON.parse(JSON.stringify(this.fleteSeleccionado)));
            fetch('FormFleteNacional.aspx/CrearActualizarFleteNacional', {
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
                this.ObtenerFletesNacionales();
                $("#fleteNacionalEdicionModal").modal('hide');
                toastr.success("Flete guardado satisfactoriamente", "Guardado Flete Nacional");
            }).catch(error => {
                toastr.error(error, "Guardado Flete Nacional");
            });
        },

        LimpiarFleteSeleccionado: function() {

            this.fleteSeleccionado = {
                    Importar: 0,
                    RegistroId: 0,
                    TransportadorId: 0,
                    TransportadorNombre: "",
                    CiudadOrigenId: 0,
                    CiudadOrigenNombre: "",
                    CiudadDestinoId: 0,
                    CiudadDestinoNombre: "",
                    VehiculoId: 0,
                    VehiculoDescripcion: "",
                    ValorFlete: 0.0,
                    ValorPrima: 0.0,
                    Estado: 0,
                    FechaCreacion: '1900-01-01T00:00:00',
                    CreadoPor: "",
                    FechaActualizacion: '1900-01-01T00:00:00',
                    ActualizadoPor: ""
                }
                this.$nextTick(function(){ 
                    $('#selectTransportador').selectpicker('refresh'); 
                    $('#selectTipoVehiculo').selectpicker('refresh'); 
                    $('#selectEstado').selectpicker('refresh'); 
                    $('#selectCiudadOrigen').selectpicker('refresh'); 
                    $('#selectCiudadDestino').selectpicker('refresh'); 
                });

            
            $("#fleteNacionalEdicionModal").modal('show');
        },

        BorrarFleteNacional: function(registroId) {
            if(confirm("¿Estas seguro de borrar el flete con Id: " + registroId + "?")) {
                var param = {
                    RegistroId: registroId
                }
                this.mostrarLoad();
                fetch('FormFleteNacional.aspx/BorrarFleteNacional', {
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
                    this.ObtenerFletesNacionales();
                    toastr.success("Flete eliminado satisfactoriamente", "Borrado Flete Nacional");
                }).catch(error => {
                    this.ocultarLoad();
                    toastr.error(error, "Borrado Flete Nacional");
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
            fetch('FormFleteNacional.aspx/ExportarFletesXLS', {
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
                toastr.success("Archivo creado satisfactoriamente", "Exportar Flete Nacional");
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Exportar Flete Nacional");
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
                                    this.fleteSeleccionado.TransportadorNombre = flete["Nombre Transportador"];
                                    this.fleteSeleccionado.CiudadOrigenNombre = flete["Ciudad Origen"];
                                    this.fleteSeleccionado.CiudadDestinoNombre = flete["Ciudad Destino"];
                                    this.fleteSeleccionado.VehiculoDescripcion = flete["Vehiculo"];
                                    if(flete["Estado"] == "Activo" || flete["Estado"] == "ACTIVO") {
                                        this.fleteSeleccionado.Estado = 1;
                                    } else {
                                        this.fleteSeleccionado.Estado = 0;
                                    }
                                    this.fleteSeleccionado.ValorFlete = parseFloat(flete["Valor Flete"]);
                                    this.fleteSeleccionado.ValorPrima = parseFloat(flete["Valor Prima"]);
                                    this.CrearActualizarFleteNacionalEnviarFetch();
                                    this.LimpiarFleteSeleccionado();
                                });
                                toastr.success("Importación completa con éxito, datos procesados: " + rawData.length, "Importar Flete Nacional");
                                this.$refs.filesImportar.files = null;
                                this.$refs.CerrarModalImportarFletes.click();
                                this.ocultarLoad();
                            } else {
                                toastr.error("Nombre no existente o mal escrito de la columna: " + responseColumnValidation.column, "Importar Flete Nacional");
                            }
                        } else {
                            toastr.info("No hay datos dentro del archivo", "Importar Flete Nacional");
                        }
                        //var arraylist = XLSX.utils.sheet_to_json(worksheet, { raw: true });
                        //this.filelist = [];
                        //console.log(this.filelist);
                    }
                } else {
                    toastr.error("El archivo debe ser de excel", "Importar Flete Nacional");
                }
            } else {
                toastr.info("No hay archivo para importar", "Importar Flete Nacional");
            }
        },

        ValidarColumnasImportarFletes: function(row) {
            var response = {
                valid: true,
                column: ""
            };
            if("Ciudad Destino" in row === false) {
                response.valid = false;
                response.column = "Ciudad Destino";
            }
            if("Ciudad Origen" in row === false) {
                response.valid = false;
                response.column = "Ciudad Origen";
            }
            if("Estado" in row === false) {
                response.valid = false;
                response.column = "Estado";
            }
            if("Nombre Transportador" in row === false) {
                response.valid = false;
                response.column = "Nombre Transportador";
            }
            if("Valor Flete" in row === false) {
                response.valid = false;
                response.column = "Valor Flete";
            }
            if("Valor Prima" in row === false) {
                response.valid = false;
                response.column = "Valor Prima";
            }
            if("Vehiculo" in row === false) {
                response.valid = false;
                response.column = "Vehiculo";
            }
            return response;
        }
    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');