import { createApp, ref, onMounted, computed } from 'vue';
import * as Vuei18n from 'vue-i18n';

// import translations

//import es from './translation/es.json';
//import en from './translation/en.json';
//import pt from './translation/pt.json';

const esModule = await import("./translation/es.json", { with: { type: "json" } });
const ptModule = await import("./translation/pt.json", { with: { type: "json" } });
const enModule = await import("./translation/en.json", { with: { type: "json" } });

const es = esModule.default;
const en = enModule.default;
const pt = ptModule.default;

const i18n = Vuei18n.createI18n({
    locale: "es",
    fallbackLocale: "es",
    globalInjection: true,
    messages: { es, en, pt },
});


const app = createApp({
    data() {
        return {
            fuentes: [],
            tipofuentes: [],
            tipos: [],
            subTipoSolicitudes: [],
            subTipoSolicitudesTH: [],
            subTipoQuejas: [],
            pqrs: {},
            selectedOrden: '',
            fup: {},
            error: {},
            isDragging: false,
            files: [],
            listpqrss: [],
            idCreado: null,
            listapaises: [],
            listaCiudades: [],
            listaClientes: [],
            lisHallazgos: [],
            usuarioId: null,
            rolId: null,
            calculedRolId: null
        }
    },
    mounted() {
        this.mostrarLoad();
        fetch('FormPQRS.aspx/ObtenerFuentes', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=utf-8'
            }
        })
            .then(res => {
                // a non-200 response code
                if (!res.ok) {
                    this.ocultarLoad();
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }

                return res.json();
            })
            .then(json => {
                // set the response data
                this.ocultarLoad();
                var res = JSON.parse(json.d);
                this.fuentes = res.fuentes;
                this.tipofuentes = res.tipofuentes;
                this.tipos = res.tipos;
                this.subTipoQuejas = res.subTipoQuejas;
                this.subTipoSolicitudes = res.subTipoSolicitudes;
                this.subTipoSolicitudesTH = res.subTipoSolicitudesTH;
                this.pqrs = { TipoPQRSId: -1, IdTipoFuenteReclamo: -1 };
            })
            .catch(err => {
                this.ocultarLoad();
                this.error.value = err;
                // In case a custom JSON error response was provided
                if (err.json) {
                    return err.json.then(json => {
                        // set the JSON response message
                        this.error.value.message = json.message;
                    });
                }
            });

        fetch('FormPQRSConsulta.aspx/ObtenerPermisos', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=utf-8'
            }
        })
        .then(res => {
            // a non-200 response code
            if (!res.ok) {
                this.ocultarLoad();
                // create error instance with HTTP status text
                const error = new Error(res.statusText);
                error.json = res.json();
                throw error;
            }

            return res.json();
        })
        .then(json => {
            // set the response data
            this.ocultarLoad();
            var respuesta = JSON.parse(json.d)
            //this.fuentes = respuesta.fuentes;
            //this.permiso = respuesta.permiso;
            //this.numordenes = respuesta.numordenes;
            this.usuarioId = respuesta.usuarioId;
            this.rolId = respuesta.rolId;
            this.ManageRolByUserId(respuesta.usuarioId, respuesta.rolId);
            //this.tipos = respuesta.tipos;
        })
        .catch(err => {
            this.ocultarLoad();
            this.error.value = err;
            // In case a custom JSON error response was provided
            if (err.json) {
                return err.json.then(json => {
                    // set the JSON response message
                    this.error.value.message = json.message;
                });
            }
        });

        
    },
    methods: {
        ManageRolByUserId: function(userId, rolId) {
            // Buscamos rol de administrador PQRS
            /*
            * 1 = Administrador
            * 2 = Logistica
            * 3 = Sólo consulta
            *
            */
            this.calculedRolId = null;
            if([840, 1019, 1160, 5473, 432, 1358, 213, 3118].indexOf(userId) > -1) {
                this.calculedRolId = 1;
            } else if ([794, 590].indexOf(userId) > -1) { 
                this.calculedRolId = 3;
            } else if (rolId == 15) {
                this.calculedRolId = 2;
            }
        },
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
        onChangeTipo: function () {
            this.fup = {};
            this.idCreado = null;
            this.selectedOrden = '';
            this.MostrarCliente();
        },
        BorrarFup: function(event) {
            this.fup = {};
            this.pqrs.NombreRespuesta = "";
            this.pqrs.DireccionRespuesta = "";
            this.pqrs.TelefonoRespuesta = "";
            this.pqrs.EmailRespuesta = "";
            this.selectedOrden = "";
            this.lisHallazgos = [];
            this.MostrarCliente();
        },
        ObtenerFUP: function (event) {
            if(this.selectedOrden == "") {
                return false;
            }
            var orden = JSON.stringify({ idOrdenFabricacion: this.selectedOrden });
            this.mostrarLoad();
            fetch('FormPQRS.aspx/obtenerVersionPorOrdenFabricacion', {
                method: 'POST',
                body: orden,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    // a non-200 response code
                    if (!res.ok) {
                        this.ocultarLoad();
                        // create error instance with HTTP status text
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    // set the response data
                    this.ocultarLoad();
                    this.fup = JSON.parse(json.d);
                    if(this.fup.DireccionRespuesta != "" && this.fup.DireccionRespuesta != null && this.fup.DireccionRespuesta != undefined) {
                        this.pqrs.NombreRespuesta = this.fup.NombreRespuesta;
                        this.pqrs.DireccionRespuesta = this.fup.DireccionRespuesta;
                        this.pqrs.TelefonoRespuesta =this.fup.TelefonoRespuesta;
                        this.pqrs.EmailRespuesta = this.fup.EmailRespuesta;
                    }
                    this.ObtenerHallazgos();
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    // In case a custom JSON error response was provided
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                        });
                    }
                });
        },
        DescargarArchivo: function (NombreArchivo, Ruta) {
            if (NombreArchivo != null) {
                var test = new FormData();
                window.location = "DownloadHandler.ashx?NombreArchivo=" + NombreArchivo + " &Ruta=" + Ruta;
            }
        },
        ObtenerHallazgos: function() {
            var orden = JSON.stringify({ idOrdenFabricacion: this.selectedOrden });
            this.mostrarLoad();
            fetch('FormPQRS.aspx/ObtenerHallazgosPorOrdenFabricacion', {
                method: 'POST',
                body: orden,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    // a non-200 response code
                    if (!res.ok) {
                        this.ocultarLoad();
                        // create error instance with HTTP status text
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    this.ocultarLoad();
                    this.lisHallazgos = JSON.parse(json.d);
                    this.lisHallazgos.forEach(element => {
                        element.Anexos = JSON.parse(element.Anexos);
                    });
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    // In case a custom JSON error response was provided
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                        });
                    }
                });
        },
        MostrarFUPCreacion: function () {
            var modal = $("#ModalCreatePQRS")
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },
        validateEmail: function () {
            if (this.pqrs.EmailRespuesta != undefined && this.pqrs.EmailRespuesta != null) {
                if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(this.pqrs.EmailRespuesta) == false) {
                    this.pqrs.EmailRespuesta = '';
                }
            }
        },
        GuardarPQRS: function () {
            if (this.pqrs == undefined || this.pqrs == null) {
                toastr.error("Debe digitar datos", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.pqrs.Detalle == undefined || this.pqrs.Detalle == null) || (this.pqrs.Detalle != undefined && this.pqrs.Detalle != null && this.pqrs.Detalle.length == 0)) {
                toastr.error("Debe digitar una observación", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.pqrs.NombreRespuesta == undefined || this.pqrs.NombreRespuesta == null) || (this.pqrs.NombreRespuesta != undefined && this.pqrs.NombreRespuesta != null && this.pqrs.NombreRespuesta.length == 0)) {
                toastr.error("Debe digitar un nombre", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.pqrs.DireccionRespuesta == undefined || this.pqrs.DireccionRespuesta == null) || (this.pqrs.DireccionRespuesta != undefined && this.pqrs.DireccionRespuesta != null && this.pqrs.DireccionRespuesta.length == 0)) {
                toastr.error("Debe digitar una dirección", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.pqrs.EmailRespuesta == undefined || this.pqrs.EmailRespuesta == null) || (this.pqrs.EmailRespuesta != undefined && this.pqrs.EmailRespuesta != null && this.pqrs.EmailRespuesta.length == 0)) {
                toastr.error("Debe digitar un email", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.pqrs.TelefonoRespuesta == undefined || this.pqrs.TelefonoRespuesta == null) || (this.pqrs.TelefonoRespuesta != undefined && this.pqrs.TelefonoRespuesta != null && this.pqrs.TelefonoRespuesta.length == 0)) {
                toastr.error("Debe digitar un telefono", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.files.length > 5) {
                toastr.error("No elija mas de 5 archivos", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.pqrs.TipoPQRSId == 2) {

                if (this.pqrs.IdFuenteReclamo == undefined || this.pqrs.IdFuenteReclamo == null) {
                    toastr.error("Debe elegir la fuente", "PQRS", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }
            }

            if (this.pqrs.IdTipoFuenteReclamo == undefined || this.pqrs.IdTipoFuenteReclamo == null || 
                this.pqrs.IdTipoFuenteReclamo == -1 || this.pqrs.IdTipoFuenteReclamo == "-1") {
                toastr.error("Debe elegir el tipo de contacto", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.fup.IdFup != undefined && this.fup.IdFup != null){
                this.pqrs.NroOrden = this.selectedOrden;
                this.pqrs.IdFup = this.fup.IdFup;
                this.pqrs.IdOrden = this.fup.IdOrden;
                this.pqrs.Version = this.fup.Version;
                this.pqrs.IdCliente = this.fup.IdCliente;
            }

            if(this.pqrs.IdCliente != -1) {
                this.pqrs.otroCliente = "";
            }

            if (this.files.length > 0) {
                this.getFiles(this.files).then(
                    data => {
                        var archivosSave = [];
                        for (var i = 0; i < data.length; i++) {
                            var fileSave = data[i];
                            archivosSave.push({ base64: fileSave.base64StringFile, nameFile: fileSave.fileName, type: fileSave.fileType });
                        }
                        this.pqrs.archivos = archivosSave;
                        this.savePQRSFetch(this.pqrs);
                    }
                );
            }
            else {
                this.savePQRSFetch(this.pqrs);
            }
        },
        savePQRSFetch: function (pqrs) {
            var ordenesParaAsociar = "";
            if(pqrs.IdFuenteReclamo == 5) {
                this.lisHallazgos.forEach(element => {
                    if(element.Asociar) {
                        ordenesParaAsociar += element.Id + ",";
                    }
                });
            }
            var orden = JSON.stringify({ pqrs: pqrs, ordenes: ordenesParaAsociar });
            this.mostrarLoad();
            fetch('FormPQRS.aspx/GuardarPQRS', {
                method: 'POST',
                body: orden,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    // a non-200 response code
                    if (!res.ok) {
                        this.ocultarLoad();
                        // create error instance with HTTP status text
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    // set the response data
                    this.ocultarLoad();
                    var modal = $("#ModalCreatePQRS");
                    modal.modal('hide');
                    this.pqrs = { TipoPQRSId: -1 };
                    this.selectedOrden = '';
                    this.fup = {};
                    this.lisHallazgos = [];
                    this.files = [];
                    this.idCreado = json.d;
                    var modal = $("#ModalPQRSCreado");
                    modal.modal('show');
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    // In case a custom JSON error response was provided
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        //Function for drag and drop files
        getFiles: function (files) {
            return Promise.all(files.map(file => this.getFile(file)));
        },

        getFile: function (file) {
            var reader = new FileReader();
            return new Promise((resolve, reject) => {
                reader.onerror = () => { reader.abort(); reject(new Error("Error parsing file")); }
                reader.onload = function () {

                    //This will result in an array that will be recognized by C#.NET WebApi as a byte[]
                    let bytes = Array.from(new Uint8Array(this.result));

                    //if you want the base64encoded file you would use the below line:
                    let base64StringFile = btoa(bytes.map((item) => String.fromCharCode(item)).join(""));

                    //Resolve the promise with your custom file structure
                    resolve({
                        bytes: bytes,
                        base64StringFile: base64StringFile,
                        fileName: file.name,
                        fileType: file.type
                    });
                }
                reader.readAsArrayBuffer(file);
            });
        },

        onChange() {
            this.files = [...this.$refs.file.files];
        },

        generateThumbnail(file) {
            let fileSrc = URL.createObjectURL(file);
            setTimeout(() => {
                URL.revokeObjectURL(fileSrc);
            }, 1000);
            return fileSrc;
        },

        makeName(name) {
            return (
                name.split(".")[0].substring(0, 3) +
                "..." +
                name.split(".")[name.split(".").length - 1]
            );
        },

        remove(i) {
            this.files.splice(i, 1);
        },

        dragover(e) {
            e.preventDefault();
            this.isDragging = true;
        },

        dragleave() {
            this.isDragging = false;
        },

        drop(e) {
            e.preventDefault();
            this.$refs.file.files = e.dataTransfer.files;
            this.onChange();
            this.isDragging = false;
        },

        ChangeLanguage: function (lang) {
            window.AppPQRS.__VUE_I18N__.global.locale = lang;
        },

        TranslationCombo: function (text) {
            return window.AppPQRS.__VUE_I18N__.global.t(text);
        },

        MostrarCliente: function () {
            fetch('FormPQRS.aspx/obtenerListadoPaises', {
                method: 'POST',

                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    // a non-200 response code
                    if (!res.ok) {
                        //this.ocultarLoad();
                        // create error instance with HTTP status text
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    // set the response data
                   // this.ocultarLoad();
                    this.listapaises = JSON.parse(json.d);
                    var data = JSON.parse(json.d);
                    data.forEach(element => {
                        $("#cmbPaises").append($("<option />").val(element.Id).text(element.Nombre));
                    });
                    $("#cmbPaises").selectpicker();
                   // var modal = $("#MostrarModalCliente");
                   // modal.modal({ backdrop: 'static', keyboard: false }, 'show');

                })
                .catch(err => {
                  //  this.ocultarLoad();
                    this.error.value = err;
                    // In case a custom JSON error response was provided
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        onChangePais() {
            var orden = JSON.stringify({ idPais: this.pqrs.IdPais });
            this.mostrarLoad();
            fetch('FormPQRS.aspx/obtenerListadoCiudadesMoneda', {
                method: 'POST',
                body: orden,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    // a non-200 response code
                    if (!res.ok) {
                        this.ocultarLoad();
                        // create error instance with HTTP status text
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    // set the response data
                    this.ocultarLoad();
                    this.listaCiudades = JSON.parse(json.d);
                    var data = JSON.parse(json.d);
                    $("#cmbCiudad").html("");
                    data.forEach(element => {
                        $("#cmbCiudad").append($("<option />").val(element.Id).text(element.Nombre));
                    });
                    $("#cmbCiudad").selectpicker('refresh');
                    //  var modal = $("#MostrarModalCliente");
                    //  modal.modal({ backdrop: 'static', keyboard: false }, 'show');

                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    // In case a custom JSON error response was provided
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        onChangeCiudad() {
            var orden = JSON.stringify({ idCiudad: this.pqrs.IdCiudad });
            this.mostrarLoad();
            fetch('FormPQRS.aspx/obtenerListadoClientesCiudad', {
                method: 'POST',
                body: orden,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    // a non-200 response code
                    if (!res.ok) {
                        this.ocultarLoad();
                        // create error instance with HTTP status text
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    // set the response data
                    this.ocultarLoad();
                    this.listaClientes = JSON.parse(json.d);
                    var data = JSON.parse(json.d);
                    $("#cmbEmpresa").html("<option value=-1>Seleccionar Otro Cliente</option> ");
                    data.forEach(element => {
                        $("#cmbEmpresa").append($("<option />").val(element.Id).text(element.Nombre));
                    });
                    $("#cmbEmpresa").selectpicker('refresh');
                    this.pqrs.IdCliente = -1;
                    //  var modal = $("#MostrarModalCliente");
                    //  modal.modal({ backdrop: 'static', keyboard: false }, 'show');

                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    // In case a custom JSON error response was provided
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                        });
                    }
                });


        },        

        AceptarCliente: function(){
            var modal = $("#MostrarModalCliente")
            modal.modal('hide');
        },

        LlenarHallazgosObra: function() {

        var cardCabecera = '<div class="box-header border-bottom border-primary Comentario" style="z-index: 2;">' + 
            '<table class="col-md-12 table-sm"><thead><tr>' +
            '<th width:="5%">Asociar?</th>'
            '<th width="12%">Fecha</th>' +
            '<th width="27%">Título</th>' +
            '<th width="10%">Sol. en Obra</th>' +
            '<th width="10%">Gen. Costo</th>' +
            '<th width="15%">Estado</th>' +
            '<th width="12%">Usuario</th><th width="8%">Orden Fabr</th><th width="3%">' +
            '</th><td width="3%"></td></tr></thead></table></div>';

        var cardFoot = '';
        var cardBody = '';
        var idParteDinamica = '';
        var OrdenParte = 0;

        this.lisHallazgos.forEach(r => {
            if (r.Padre == "0") {

                if (cardBody != "") {
                    cardBody += '</div></div></div><div class="row itemHallazgoObra" ></div>';
                }
                idParteDinamica = r.Cons;
                OrdenParte = r.fcp_OrdenParte

                cardBody +=
                    '<div class="col-md-12 Comentario" style="padding-top: 6x;padding-left: 0px;padding-right: 0px;" id="ParteHallazgoObra' + r.Nivel + '"><div id="headerHallazgoObra' + r.Nivel + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                    '<table class="col-md-12 table-sm"><tr ' + (r.EsDft ? 'class="table-primary"' : '') + '><td></td><td width="12%">' + r.Fecha.substring(0, 10) + '</td><td width="27%">' + r.Titulo + '</td><td width="10%">' + (r.SolucionadoEnObra ? 'Sí' : 'No') + '</td><td width="10%">' + (r.GeneroCosto ? 'Sí' : 'No') + '</td><td width="15%">' + r.Estado + '</td><td width="12%">' + r.Usuario + '</td><td width="8%" class=text-right>' + r.HallazgoOrdenFabricacion + '</td>' +
                    '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.Nivel + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                    '<div id="body' + r.Nivel + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

            
                cardBody += '<div class="row item" id="' + r.Nivel + '"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 SegLogistico" rows="2" disabled>' + r.Comentario + '</textarea></div>';
            
                if (r.Anexos.length > 0) {
                    var vAnexo = JSON.parse(r.Anexos);
                    var vLineaAnexo = "";
                    jQuery.each(vAnexo, function (j, ane) {
                        if (ane.tipo_plano == "Hallazgo en Obra") {
                            vLineaAnexo += '<tr ><td width="10%" >Anexo</td><td width="80%">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                        }
                    });
                    cardBody += '<div class="row item" id="SegLogistico' + r.Nivel + '"><table class="col-md-12 table-sm table-bordered">' + vLineaAnexo + '</table></div>';
                }

                cardBody += '<div class="row col-md-3"><button id="btnRespSegLogistico' + r.Cons + '" onclick="ControlCambioShowC_O(\'Respuesta Hallazgo en obra\',\'Reply Findings on site\',\'Resposta Situações em Obra\',0,' + r.Id + ',\'' + r.Titulo + '\',' + r.EsDft + ',3)" type="button" class="btn btn-primary btn-sm"><i class="fa fa-comments"></i><b>Responder</b></button></div>';
                cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><thead><tr><th width="12%" colspan = "2" class="text-center" >FECHA</th><th width="15%">PLAN ACCION</th><th width="33%">RESPUESTA</th><th width="10%">FEC. IMPLEMENTACION</th><th width="10%">ESTADO FUP</th><th colspan = "2" width="20%">USUARIO</th></tr></thead></table></div></div>';

            }
            else {
                if (r.Anexos.length > 0) {
                    var vAnexo = JSON.parse(r.Anexos);
                    var vLineaAnexo = "";
                    jQuery.each(vAnexo, function (j, ane) {
                        if(ane.tipo_plano == "Hallazgo en Obra")
                        {
                            vLineaAnexo += '<tr><td width="12%" colspan = "2" class="text-center" >Anexo</td><td colspan = "4">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                        }
                    });
                    cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="15%">' + (r.TipoConsideracionObservacion ? "Correctivo para la orden" : "Mejora en el proceso") + '</td><td width="33%">' + r.Comentario + '</td><td width="10%">' + r.FecDespacho.substring(0, 10) + '</td><td width="10%">' + r.Estado + '</td><td width="18%">' + r.Usuario + '</td><td width="2%"></tr>' + vLineaAnexo + '</tbody></table></div></div>';
                }
                else {
                    cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="15%">' + (r.TipoConsideracionObservacion ? "Correctivo para la orden" : "Mejora en el proceso") + '</td><td width="33%">' + r.Comentario + '</td><td width="10%">' + r.FecDespacho.substring(0, 10) + '</td><td width="10%">' + r.Estado + '</td><td colspan = "2" width="20%">' + r.Usuario + '</td></tbody></table></div></div>';
                }
            }
        });
        $("#DinamycChangeHallazgos").html(cardCabecera + cardBody);
        }
    }
});

window.AppPQRS = app;
app.use(i18n);
app.mount('#app');

