import * as VueModule from 'vue';
import Vue3EasyDataTable from 'vue3-easy-data-table';
import * as Vuei18n from 'vue-i18n';


// import translations
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

const app = VueModule.createApp({
    data() {
        return {
            fuentes: [],
            tipos: [],
            permiso: {},
            nameCriteria: "",
            fuenteCriteria: "",
            estadoCriteria: "",
            tipoPQRSCriteria: "",
            nombreClienteCriteria: "",
            nombrePaisCriteria: "",
            direccionRespuestaCriteria: "",
            emailCriteria: "",
            telefonoRespuestaCriteria: "",
            ordenPrecedenteCriteria: "",
            showNameFilter: false,
            showEmailFilter: false,
            showFuenteFilter: false,
            showEstadoFilter: false,
            showTipoPQRSFilter: false,
            showClienteFilter: false,
            showPaisFilter: false,
            showDireccionRespuestaFilter: false,
            showTelefonoRespuestaFilter: false,
            showOrdenProcFilter: false,
            filterOptions: [],
            selectedOrden: '',
            filters: {},
            error: {},
            isDragging: false,
            files: [],
            listpqrss: [],
            pqrsHistorico: [],
            pqrsArchivo: [],
            headers: [],
            colorTheme: "#f48225",
            procesos: [],
            procesosPQRS: [],
            procesosAsignadosPQRS: [],
            isStatusShown: false,
            isAnswerShown: false,
            EstadoIDHistorico: 0,
            PQRSSelAsignacion: 0,
            pqrsRespuesta: {},
            mesajeRespuesta: "",
            pqrsRespuestaHistorico: {},
            pqrsProcedente: {},
            pqrsGenerarOrden: {},
            tipoNC: [],
            pqrsProcedenteHistorico: {},
            pqrs: {},
            enviarComunicado: {},
            numordenes: [],
            listadosRequeridos: {},
            implementacionObra: {},
            archivos: [],
            cierre: {},
            pqrsprod: {},
            pqrscomobra: {},
            plantas: [],
            listadosCargados: [],
            pqrsRespuestaCliente: {},
            pqrsProduccionHistorico: {},
            Asuntopqrs: "",
            PQRSDTOConsulta: {},
            archivosRadicadoComunicado: {},
            estados: {},
            pqrsProcesoAclaracionTemp: null,
            rolId: null,
            usuarioId: null,
            calculedRolId: 1,
            pqrsAsociadosOrden: null
        }
    },
    mounted() {
        window.AppPQRS.component("EasyDataTable", Vue3EasyDataTable);
        // Reading of query parameters, if param PQRSId is null, the page doesn't load
        var urlParams = new URLSearchParams(window.location.search);
        var PQRSId = urlParams.get('IdPQRS');
        if (PQRSId == null) {
            PQRSId ='';
        }

        this.filterOptions = VueModule.computed(() => {
            var filterOptionsArray = [];
            filterOptionsArray.push({
                field: 'NroOrden',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.includes(`${criteria}`));
                    return res;
                },
                criteria: this.nameCriteria,
            });
            filterOptionsArray.push({
                field: 'Fuente',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.toLowerCase().includes(`${criteria.toLowerCase()}`));
                    return res;
                },
                criteria: this.fuenteCriteria,
            });
            filterOptionsArray.push({
                field: 'Estado',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.toLowerCase().includes(`${criteria.toLowerCase()}`));
                    return res;
                },
                criteria: this.estadoCriteria,
            });
            filterOptionsArray.push({
                field: 'TipoPQRS',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.toLowerCase().includes(`${criteria.toLowerCase()}`));
                    return res;
                },
                criteria: this.tipoPQRSCriteria,
            });
            filterOptionsArray.push({
                field: 'Cliente',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.toLowerCase().includes(`${criteria.toLowerCase()}`));
                    return res;
                },
                criteria: this.nombreClienteCriteria,
            });
            filterOptionsArray.push({
                field: 'Pais',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.toLowerCase().includes(`${criteria.toLowerCase()}`));
                    return res;
                },
                criteria: this.nombrePaisCriteria,
            });
            filterOptionsArray.push({
                field: 'DireccionRespuesta',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.toLowerCase().includes(`${criteria.toLowerCase()}`));
                    return res;
                },
                criteria: this.direccionRespuestaCriteria,
            });
            filterOptionsArray.push({
                field: 'EmailRespuesta',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.includes(`${criteria}`));
                    return res;
                },
                criteria: this.emailCriteria,
            });
            filterOptionsArray.push({
                field: 'TelefonoRespuesta',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.includes(`${criteria}`));
                    return res;
                },
                criteria: this.telefonoRespuestaCriteria,
            });
            filterOptionsArray.push({
                field: 'OrdenProcedente',
                comparison: (value, criteria) => {
                    if (criteria == undefined || criteria == null || criteria == '' || criteria == "") {
                        return true;
                    }
                    var res = (value != null && criteria != null &&
                        typeof value === 'string' && value.includes(`${criteria}`));
                    return res;
                },
                criteria: this.ordenPrecedenteCriteria,
            });
            return filterOptionsArray;
        });

        this.headers = [
            { text: "Id", value: "IdPQRS", sortable: true, width: 60 },
            { text: "Orden Origen", value: "NroOrden", sortable: true, width: 60 },
            { text: "Fuente", value: "Fuente", sortable: true },
            { text: "Estado", value: "Estado", sortable: true },
            { text: "Tipo", value: "TipoPQRS", sortable: true },
            { text: "Cliente", value: "Cliente", sortable: true },
            { text: "Pais", value: "Pais", sortable: true },
            { text: "Dirección Respuesta", value: "DireccionRespuesta", sortable: true },
            { text: "Email", value: "EmailRespuesta", sortable: true },
            { text: "Telefono", value: "TelefonoRespuesta", sortable: true },
            { text: "Orden Garantia", value: "OrdenProcedente", sortable: true, width: 60 },
            { text: "Acciones", value: "operation",  width: 180 }
        ];

        this.mostrarLoad();
        var firstdate = new Date('2023-01-01');
        var stringFirstDate = moment(firstdate).format('YYYY-MM-DD');
        var currentDate = new Date();
        var stringCurrentDate = moment(currentDate).format('YYYY-MM-DD');

        this.filters = { desde: stringFirstDate, hasta: stringCurrentDate, nombre: '', orden: '', fuente: -1, idpqrs: PQRSId, TipoPQRSId: -1, estado: -1 };
        this.cierre = { planAccion: '', fechaCierrePlan: stringCurrentDate };
        this.Asuntopqrs = " ";

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
                this.fuentes = respuesta.fuentes;
                this.permiso = respuesta.permiso;
                this.numordenes = respuesta.numordenes;
                this.tipos = respuesta.tipos;
                this.estados = respuesta.estados;
                this.usuarioId = respuesta.usuarioId;
                this.rolId = respuesta.rolId;
                this.ManageRolByUserId(respuesta.usuarioId, respuesta.rolId);
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

        this.ObtenerPQRSpor();
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

        ObtenerPQRSAsociadosAOrden: function (nroOrden) {
            var data = JSON.stringify({ OrderNumber: nroOrden });
            fetch('FormPQRSResumen.aspx/GetAsociatedPQRSToOrder', {
                method: 'POST',
                body: data,
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
                    var respuesta = JSON.parse(json.d)
                    this.pqrsAsociadosOrden = respuesta;
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

        MostrarHistoricoPQRS: function (idpqrshistorica) {
            this.isStatusShown = false;

            this.EstadoIDHistorico = 0;
            var orden = JSON.stringify({ idpqrs: idpqrshistorica });

            fetch('FormPQRSConsulta.aspx/ObtenerPQRSHistorico', {
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
                    this.pqrsHistorico = JSON.parse(json.d);
                    var modal = $("#ModalPQRSHistorico")
                    modal.modal({ backdrop: 'static', keyboard: false }, 'show');
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

        MostrarArchivosPQRS: function (idpqrsArchivo) {
            var orden = JSON.stringify({ idpqrs: idpqrsArchivo });
            fetch('FormPQRSConsulta.aspx/ObtenerPQRSArchivo', {
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
                    this.pqrsArchivo = JSON.parse(json.d);
                    this.isStatusShown = true;
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

        ObtenerPlantas: function() {
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerPlantas', {
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
                    this.ocultarLoad();
                    this.plantas = JSON.parse(json.d);
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

        ObtenerPQRSpor: function () {
            this.mostrarLoad();
            var orden = JSON.stringify({ filtros: this.filters });

            fetch('FormPQRSConsulta.aspx/ObtenerPQRSpor', {
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
                    this.listpqrss = JSON.parse(json.d);
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

        AsignacionPQRS: function (itemPQRS) {
            this.procesos = [];
            this.PQRSSelAsignacion = 0;
            var tipo = JSON.stringify({ tipopqrs: itemPQRS.TipoPQRSId });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerProcesos', {
                method: 'POST',
                body: tipo,
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
                    this.procesos = JSON.parse(json.d);
                    var modal = $("#ModalPQRSAsignacionProceso")
                    modal.modal({ backdrop: 'static', keyboard: false }, 'show');
                    this.PQRSSelAsignacion = itemPQRS.IdPQRS;
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

        ProcesosAsignadosPQRS: function (itemPQRS) {
            var orden = JSON.stringify({ idpqrs: itemPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerProcesosAsignados', {
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
                    this.procesosPQRS = JSON.parse(json.d);
                    this.isStatusShown = true;
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

        ReclamoProcedentePQRS: function (itemPQRS) {
            var orden = JSON.stringify({ idPQRS: itemPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerProcedenciaHistorico', {
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
                    this.pqrsProcedenteHistorico = JSON.parse(json.d);
                    this.isStatusShown = true;
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

        RespuestaProcesosPQRS: function (itemPQRS) {
            var orden = JSON.stringify({ idLog: itemPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerRespuestaProcesos', {
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
                    this.pqrsRespuestaHistorico = JSON.parse(json.d);
                    this.isStatusShown = true;
                    this.isAnswerShown = true;
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

        MostrarDetalleHistorico: function (IdPQRS, EstadoID, IdLog) {
            this.pqrsArchivo = [];
            this.procesosPQRS = [];
            this.pqrsRespuestaHistorico = {};
            this.pqrsRespuestaCliente = {};
            this.pqrsProduccionHistorico = {};
            this.isAnswerShown = false;
            this.EstadoIDHistorico = EstadoID;
            this.pqrsProcedenteHistorico = {};
            this.PQRSDTOConsulta = {};
            if (EstadoID == 1) {
                this.MostrarArchivosPQRS(IdPQRS);
            }
            else if (EstadoID == 2) {
                this.ProcesosAsignadosPQRS(IdPQRS);
            }
            else if (EstadoID == 3) {
                this.RespuestaProcesosPQRS(IdLog);
            }
            else if (EstadoID == 4) {
                this.ReclamoProcedentePQRS(IdPQRS);
            }
            //Repuesta Cliente
            else if (EstadoID == 6) {
                this.RespuestaClienteLog(IdLog);
            }
            //Produccion
            else if (EstadoID == 10) {
                this.ProduccionLog(IdPQRS);
            }
            //elaboracion
            else if (EstadoID == 0) {
                this.RadicacionElaboracionLog(IdPQRS);
            }
            
        },

        GuardarProcesos: function () {
            const ProcesosSel = this.procesos.filter(obj => {
                return obj.Seleccionado == true;
            });

            if (ProcesosSel.length > 0) {
                var orden = JSON.stringify({ procesos: { PQRSId: this.PQRSSelAsignacion, procesos: ProcesosSel } });
                this.mostrarLoad();
                fetch('FormPQRSConsulta.aspx/GuardarProcesos', {
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
                        var modal = $("#ModalPQRSAsignacionProceso")
                        modal.modal('hide');
                        this.procesos = [];
                        this.PQRSSelAsignacion = 0;
                        this.ObtenerPQRSpor();
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
            }
            else {
                toastr.warning('Debe seleccionar procesos.');
            }

        },

        PreguntaAsignarProceso: function () {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿desea asignar a los procesos seleccionados?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion").click(function () {
                            parentWindow.GuardarProcesos();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        MostrarAgregarRespuestaProceso: function (IdPQRS) {
            this.pqrsRespuesta.PQRSId = IdPQRS;
            this.pqrsRespuesta.Mensaje = "";
            this.pqrsRespuesta.PQRSIdproceso = "-1";
            this.files = [];
            var orden = JSON.stringify({ idpqrs: IdPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerProcesosAsignadosEmail', {
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
                    this.procesosAsignadosPQRS = JSON.parse(json.d);
                    var modal = $("#ModalAgregarRespuestaProceso");
                    modal.modal({ backdrop: 'static', keyboard: false }, 'show');
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

        onChangeProd() {

            this.files = [...this.$refs.fileProd.files];
        },

        BuscarAclaracionProceso() {
            this.pqrsProcesoAclaracionTemp = null;
            this.procesosAsignadosPQRS.forEach(element => {
                if(element.PQRSProcesoId == this.pqrsRespuesta.PQRSIdproceso) {
                    this.pqrsProcesoAclaracionTemp = element.InformacionAclaracion;
                    return false;
                }
            }); 
        },

        onChangeComunicado() {

            this.files = [...this.$refs.fileComunicado.files];
        },


        onChangeImpl() {

            this.files = [...this.$refs.fileInputImpl.files];
        },

        onChangeListados() {

            this.files = [...this.$refs.fileInputListados.files];
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

        dropProd(e) {
            e.preventDefault();
            this.$refs.fileProd.files = e.dataTransfer.files;
            this.onChangeProd();
            this.isDragging = false;
        },

        dropComunicado(e) {
            e.preventDefault();
            this.$refs.fileProd.files = e.dataTransfer.files;
            this.onChangeProd();
            this.isDragging = false;
        },

        dropImpl(e) {
            e.preventDefault();
            this.$refs.fileInputImpl.files = e.dataTransfer.files;
            this.onChangeImpl();
            this.isDragging = false;
        },

        dropListados(e) {
            e.preventDefault();
            this.$refs.fileInputListados.files = e.dataTransfer.files;
            this.onChangeListados();
            this.isDragging = false;
        },

        ValidarGuardarPQRSRespuesta: function () {
            if (this.pqrsRespuesta == undefined || this.pqrsRespuesta == null) {
                toastr.error("debe agregar un mensaje ", "pqrsRespuesta", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }
            if ((this.pqrsRespuesta.Mensaje == undefined || this.pqrsRespuesta.Mensaje == null) || (this.pqrsRespuesta.Mensaje != undefined && this.pqrsRespuesta.Mensaje != null && this.pqrsRespuesta.Mensaje.length == 0)) {
                toastr.error("Debe agregar un mensaje ", "mesajeRespuesta", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.pqrsRespuesta.PQRSIdproceso == undefined || this.pqrsRespuesta.PQRSIdproceso == null) || this.pqrsRespuesta.PQRSIdproceso == "-1") {
                toastr.error("Debe elegir un proceso ", "mesajeRespuesta", {
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

            if (this.files.length > 0) {
                this.getFiles(this.files).then(
                    data => {
                        var archivosSave = [];
                        for (var i = 0; i < data.length; i++) {
                            var fileSave = data[i];
                            archivosSave.push({ base64: fileSave.base64StringFile, nameFile: fileSave.fileName, type: fileSave.fileType });
                        }
                        this.pqrsRespuesta.archivos = archivosSave;
                        this.PreguntaGuardarPQRSRespuesta(this.pqrsRespuesta);
                    }
                );
            }
            else {
                this.PreguntaGuardarPQRSRespuesta(this.pqrsRespuesta);
            }
        },

        GuardarPQRSRespuestaProceso: function (pqrsRespuesta) {
            var orden = JSON.stringify({ pqrsRespuestaproceso: this.pqrsRespuesta });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GuardarPQRSRespuesta', {
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
                    var modal = $("#ModalAgregarRespuestaProceso")
                    modal.modal('hide');
                    this.files = [];
                    this.pqrsRespuesta = {};
                    this.ObtenerPQRSpor();
                    this.pqrsProcesoAclaracionTemp = null;
                    // this.fup = JSON.parse(json.d);
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

        ReclamoProcedente: function (idPQRS) {
            // this.pqrsProcedente = [];
            this.procesos = [];
            this.tipoNC = [];
            this.PQRSSelAsignacion = 0;
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerDatosProcedencia', {
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
                    var dataRes = JSON.parse(json.d);
                    this.procesos = dataRes.procesos;
                    this.tipoNC = dataRes.tiponc;
                    this.pqrsProcedente.EsProcedente = 'false';
                    this.pqrsProcedente.existeOrden = 'false';
                    this.pqrsProcedente.requierelistados = 'false';
                    this.pqrsProcedente.RequierelistadosAcero = false;
                    this.pqrsProcedente.RequierelistadosAluminio = false;
                    this.pqrsProcedente.requiereplanos = 'false';
                    this.pqrsProcedente.RequiereplanosAluminio = false;
                    this.pqrsProcedente.RequiereplanosAcero =false;
                    this.pqrsProcedente.IdPQRS = idPQRS;
                    this.ObtenerPlantas();
                    var modal = $("#ModalReclamoProcedente")
                    modal.modal({ backdrop: 'static', keyboard: false }, 'show');
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

        GenerarOrdenMostrarModal: function(idPQRS) {
            this.pqrsGenerarOrden.solucionadoEnObra = 'false';
            this.pqrsGenerarOrden.EsProcedente = 'false';
            this.pqrsGenerarOrden.existeOrden = 'false';
            this.pqrsGenerarOrden.requierelistados = 'false';
            this.pqrsGenerarOrden.RequierelistadosAcero = false;
            this.pqrsGenerarOrden.RequierelistadosAluminio = false;
            this.pqrsGenerarOrden.requiereplanos = 'false';
            this.pqrsGenerarOrden.RequiereplanosAluminio = false;
            this.pqrsGenerarOrden.RequiereplanosAcero =false;
            this.pqrsGenerarOrden.IdPQRS = idPQRS;
            this.ObtenerPlantas();
            var modal = $("#ModalGenerarOrden")
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        ValidarGenerarOrden: function() {
            var validoCorre = true, validoProcesos = true, validoOrden = true;
            if(this.pqrsGenerarOrden.solucionadoEnObra == 'false') {
                if (this.pqrsGenerarOrden.existeOrden == 'true') {
                    if (((this.pqrsGenerarOrden.Idordenprocedente == undefined || this.pqrsGenerarOrden.Idordenprocedente == null) || this.pqrsGenerarOrden.Idordenprocedente == "-1")) {
                        toastr.error("Debe escoger una orden existente", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                        validoOrden = false;
                        return false;
                    }
                } else {
                    if (((this.pqrsGenerarOrden.OrdenGarantiaOMejora == undefined || this.pqrsGenerarOrden.OrdenGarantiaOMejora == null) || this.pqrsGenerarOrden.OrdenGarantiaOMejora == "-1")) {
                        toastr.error("Debe escoger si es una orden de mejora o garantía", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                        validoOrden = false;
                        return false;
                    }
                    if (((this.pqrsGenerarOrden.IdPlanta == undefined || this.pqrsGenerarOrden.IdPlanta == null) || this.pqrsGenerarOrden.IdPlanta == "-1")) {
                        toastr.error("Debe escoger una planta", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                        validoOrden = false;
                        return false;
                    }
                }

                let requiereAlguno = false;
                if (this.pqrsGenerarOrden.requierelistados == 'true' || this.pqrsGenerarOrden.requierelistados == true) {
                    if(this.pqrsGenerarOrden.RequierelistadosAcero == true) {
                        requiereAlguno = true;
                        if ((this.pqrsGenerarOrden.RequierelistadosAceroCorreos == undefined || this.pqrsGenerarOrden.RequierelistadosAceroCorreos == null) || (this.pqrsGenerarOrden.RequierelistadosAceroCorreos != undefined && this.pqrsGenerarOrden.RequierelistadosAceroCorreos != null && this.pqrsGenerarOrden.RequierelistadosAceroCorreos.length == 0)) {
                            toastr.error("Debe agregar al menos un correo para notificar en los listados de acero", {
                                "timeOut": "0",
                                "extendedTImeout": "0"
                            });
                            validoCorre = false;
                            return false;
                        } else {
                            var dtArray = [];
                            dtArray = this.pqrsGenerarOrden.RequierelistadosAceroCorreos.split(";");
                            if (dtArray.length != 0) {
                                var valido = -1;
                                dtArray.forEach((res, indice) => {
                                    const emailRegex = new RegExp(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/);
                                    if (!emailRegex.test(res))
                                        validoCorre = false;
                                    else
                                        validoCorre = true;
                                });

                                if (!validoCorre) {
                                    toastr.error("Alguna cuenta de correo no es valida, recuerde que debe estar separada por (;) ", {
                                        "timeOut": "0",
                                        "extendedTImeout": "0"
                                    });
                                    return false;
                                }


                            }
                            else {
                                toastr.error("Debe agregar correos validos separados por (;) ", {
                                    "timeOut": "0",
                                    "extendedTImeout": "0"
                                });
                                validoCorre = false;
                                return false;
                            }
                        }
                    }
                    if(this.pqrsGenerarOrden.RequierelistadosAluminio == true) {
                        requiereAlguno = true;
                        if ((this.pqrsGenerarOrden.RequierelistadosAluminioCorreos == undefined || this.pqrsGenerarOrden.RequierelistadosAluminioCorreos == null) || (this.pqrsGenerarOrden.RequierelistadosAluminioCorreos != undefined && this.pqrsGenerarOrden.RequierelistadosAluminioCorreos != null && this.pqrsGenerarOrden.RequierelistadosAluminioCorreos.length == 0)) {
                            toastr.error("Debe agregar al menos un correo para notificar en los listados de aluminio", {
                                "timeOut": "0",
                                "extendedTImeout": "0"
                            });
                            validoCorre = false;
                            return false;
                        } else {
                            var dtArray = [];
                            dtArray = this.pqrsGenerarOrden.RequierelistadosAluminioCorreos.split(";");
                            if (dtArray.length != 0) {
                                var valido = -1;
                                dtArray.forEach((res, indice) => {
                                    const emailRegex = new RegExp(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/);
                                    if (!emailRegex.test(res))
                                        validoCorre = false;
                                    else
                                        validoCorre = true;
                                });

                                if (!validoCorre) {
                                    toastr.error("Alguna cuenta de correo no es valida, recuerde que debe estar separada por (;) ", {
                                        "timeOut": "0",
                                        "extendedTImeout": "0"
                                    });
                                    return false;
                                }


                            }
                            else {
                                toastr.error("Debe agregar correos validos separados por (;) ", {
                                    "timeOut": "0",
                                    "extendedTImeout": "0"
                                });
                                validoCorre = false;
                                return false;
                            }
                        }
                    }

                }
                else {
                    if ((this.pqrsGenerarOrden.requierelistadosDescripcion == undefined || this.pqrsGenerarOrden.requierelistadosDescripcion == null || this.pqrsGenerarOrden.requierelistadosDescripcion == "") || (this.pqrsGenerarOrden.requierelistadosDescripcion != undefined && this.pqrsGenerarOrden.requierelistadosCorreos != null && this.pqrsGenerarOrden.requierelistadosCorreos.length == 0)) {
                        toastr.error("Debe agregar una descripcion si no requiere listados ", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                        validoCorre = false;
                        return false;
                    }
                }

                if (this.pqrsGenerarOrden.requiereplanos == 'true' || this.pqrsGenerarOrden.requiereplanos == true) {
                    
                    if(this.pqrsGenerarOrden.RequiereplanosAcero == true) {
                        requiereAlguno = true;
                        if ((this.pqrsGenerarOrden.RequiereplanosAceroCorreos == undefined || this.pqrsGenerarOrden.RequiereplanosAceroCorreos == null) || (this.pqrsGenerarOrden.RequiereplanosAceroCorreos != undefined && this.pqrsGenerarOrden.RequiereplanosAceroCorreos != null && this.pqrsGenerarOrden.RequiereplanosAceroCorreos.length == 0)) {
                            toastr.error("Debe agregar al menos un correo para notificar en los planos de acero", {
                                "timeOut": "0",
                                "extendedTImeout": "0"
                            });
                            validoCorre = false;
                            return false;
                        } else {
                            var dtArray = [];
                            dtArray = this.pqrsGenerarOrden.RequiereplanosAceroCorreos.split(";");
                            if (dtArray.length != 0) {
                                var valido = -1;
                                dtArray.forEach((res, indice) => {
                                    const emailRegex = new RegExp(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/);
                                    if (!emailRegex.test(res))
                                        validoCorre = false;
                                    else
                                        validoCorre = true;
                                });

                                if (!validoCorre) {
                                    toastr.error("Alguna cuenta de correo no es valida, recuerde que debe estar separada por (;) ", {
                                        "timeOut": "0",
                                        "extendedTImeout": "0"
                                    });
                                    return false;
                                }


                            }
                            else {
                                toastr.error("Debe agregar correos validos separados por (;) ", {
                                    "timeOut": "0",
                                    "extendedTImeout": "0"
                                });
                                validoCorre = false;
                                return false;
                            }
                        }
                    }
                    if(this.pqrsGenerarOrden.RequiereplanosAluminio == true) {
                        requiereAlguno = true;
                        if ((this.pqrsGenerarOrden.RequiereplanosAluminioCorreos == undefined || this.pqrsGenerarOrden.RequiereplanosAluminioCorreos == null) || (this.pqrsGenerarOrden.RequiereplanosAluminioCorreos != undefined && this.pqrsGenerarOrden.RequiereplanosAluminioCorreos != null && this.pqrsGenerarOrden.RequiereplanosAluminioCorreos.length == 0)) {
                            toastr.error("Debe agregar al menos un correo para notificar en los planos de aluminio", {
                                "timeOut": "0",
                                "extendedTImeout": "0"
                            });
                            validoCorre = false;
                            return false;
                        } else {
                            var dtArray = [];
                            dtArray = this.pqrsGenerarOrden.RequiereplanosAluminioCorreos.split(";");
                            if (dtArray.length != 0) {
                                var valido = -1;
                                dtArray.forEach((res, indice) => {
                                    const emailRegex = new RegExp(/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/);
                                    if (!emailRegex.test(res))
                                        validoCorre = false;
                                    else
                                        validoCorre = true;
                                });

                                if (!validoCorre) {
                                    toastr.error("Alguna cuenta de correo no es valida, recuerde que debe estar separada por (;) ", {
                                        "timeOut": "0",
                                        "extendedTImeout": "0"
                                    });
                                    return false;
                                }


                            }
                            else {
                                toastr.error("Debe agregar correos validos separados por (;) ", {
                                    "timeOut": "0",
                                    "extendedTImeout": "0"
                                });
                                validoCorre = false;
                                return false;
                            }
                        }
                    }

                }
                else {
                    if ((this.pqrsGenerarOrden.RequierePlanosDescripcion == undefined || this.pqrsGenerarOrden.RequierePlanosDescripcion == null || this.pqrsGenerarOrden.RequierePlanosDescripcion == "") || (this.pqrsGenerarOrden.RequierePlanosDescripcion != undefined && this.pqrsGenerarOrden.RequierePlanosDescripcion != null && this.pqrsGenerarOrden.RequierePlanosDescripcion.length == 0)) {
                        toastr.error("Debe agregar una descripcion si no requiere listados ", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                        validoCorre = false;
                        return false;
                    }
                }

                if(this.pqrsGenerarOrden.requierelistados == 'true' || this.pqrsGenerarOrden.requierelistados == true
                    || this.pqrsGenerarOrden.requiereplanos == 'true' || this.pqrsGenerarOrden.requiereplanos == true) {
                    if(!requiereAlguno) {
                        toastr.error("Debe requerir al menos alguna categoria de planos o listados ", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                        validoCorre = false;
                        return false;
                    }
                }

                if (validoCorre == validoOrden) {
                    this.PreguntarGenerarOrden();
                }
            }
            else {
                this.PreguntarGenerarOrden();
            }
        },

        PreguntarGenerarOrden: function() {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion1' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿esta seguro de guardar el reclamo procedente?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion1").click(function () {
                            parentWindow.GenerarOrden();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        GenerarOrden: function() {
            if(!this.pqrsGenerarOrden.RequiereplanosAcero) { this.pqrsGenerarOrden.RequiereplanosAceroCorreos = '' };
            if(!this.pqrsGenerarOrden.RequiereplanosAluminio) { this.pqrsGenerarOrden.RequiereplanosAluminioCorreos = '' };
            if(!this.pqrsGenerarOrden.RequierelistadosAcero) { this.pqrsGenerarOrden.RequierelistadosAceroCorreos = '' };
            if(!this.pqrsGenerarOrden.RequierelistadosAluminio) { this.pqrsGenerarOrden.RequierelistadosAluminioCorreos = '' };
            var orden = JSON.stringify({ pqrs: this.pqrsGenerarOrden });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GenerarOrden', {
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
                    var modal = $("#ModalGenerarOrden");
                    modal.modal('hide');
                    this.pqrsGenerarOrden = {};
                    this.ObtenerPQRSpor();
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

        ExisteOrdenChange:function() {
            this.pqrsProcedente.OrdenGarantiaOMejora = null;
        },

        noExisteOrdenChange:function() {
            this.pqrsProcedente.Idordenprocedente = null;
        },

        PreguntaProcedencia: function () {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion1' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿esta seguro de guardar el reclamo procedente?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion1").click(function () {
                            parentWindow.GuardarPQRSProcedencia();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        ValidarPQRSProcedencia: function () {
            var validoCorre = true, validoProcesos = true, validoOrden = true;
            const ProcesosSel = this.procesos.filter(obj => {
                return obj.Seleccionado == true;
            });
            this.pqrsProcedente.procesos = ProcesosSel;

            if (this.pqrsProcedente.EsProcedente == 'true' || this.pqrsProcedente.EsProcedente == true) {

                if (ProcesosSel.length > 0) {
                    validoProcesos = true;
                }
                else {
                    validoProcesos = false;
                    toastr.warning('Debe seleccionar procesos.');
                }

            }

            if ((this.pqrsProcedente.DescripcionNoProcedente == undefined || this.pqrsProcedente.DescripcionNoProcedente == null) || (this.pqrsProcedente.DescripcionNoProcedente != undefined && this.pqrsProcedente.DescripcionNoProcedente != null && this.pqrsProcedente.DescripcionNoProcedente.length == 0)) {
                toastr.error("Debe indicar una razón poque es o no procedente", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                validoOrden = false;
                return false;
            }

            if (validoProcesos == validoCorre == validoOrden) {
                this.PreguntaProcedencia();
            }
            
        },

        GuardarPQRSProcedencia: function () {

            var orden = JSON.stringify({ pqrs: this.pqrsProcedente });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GuardarReclamoProcedente', {
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
                    var modal = $("#ModalReclamoProcedente")
                    modal.modal('hide');
                    this.procesos = [];
                    this.pqrsProcedente = {};
                    this.ObtenerPQRSpor();
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

        MostrarEnviarComunicadoCliente: function (itemPQRS) {
            this.pqrs = itemPQRS;
            if (itemPQRS.NroOrden != '') {
                this.ObtenerPQRSAsociadosAOrden(itemPQRS.NroOrden);
            } else {
                this.pqrsAsociadosOrden = null;
            }
            this.MostrarArchivosPQRS(this.pqrs.IdPQRS);
            this.enviarComunicado.Asunto = "FORSA__Seguimiento a PQRS # " + this.pqrs.IdPQRS + "  / Acompanhamento SAC # "+ this.pqrs.IdPQRS + " /  Follow up to PQRS # " + this.pqrs.IdPQRS;
            this.enviarComunicado.para = this.pqrs.EmailRespuesta;
            var modal = $("#ModalEnviarComunicado");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        validarEnviarMensajeCliente: function (enviarComunicado) {
            this.enviarComunicado = enviarComunicado;
//            this.enviarComunicado.asunto = "Respuesta Forsa a Solicitud #" + this.pqrs.IdPQRS;
            var validoCorre = true
            var dtArray = [];

            if (this.enviarComunicado.para != "" && this.enviarComunicado.para != undefined) {
                dtArray = this.enviarComunicado.para.split(";");
            }

            if (dtArray.length != 0) {
                dtArray.forEach((res, indice) => {
                    const emailRegex = new RegExp(/^[A-Za-z0-9_!#$%&'*+\/=?`{|}~^.-]+@[A-Za-z0-9.-]+$/, "gm");
                    if (!emailRegex.test(res))
                        validoCorre = false;
                    else
                        validoCorre = true;
                });

                if (!validoCorre) {
                    toastr.error("Alguna cuenta de correo no es valida, recuerde que debe estar separada por (;) ", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }
            } else {
                toastr.error("El correo debe tener al menos (1) destinatario", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            dtArray = [];
            dtArray = (this.enviarComunicado.Cc != undefined ? this.enviarComunicado.Cc.split(";") : '');
            if (dtArray.length != 0) {
                dtArray.forEach((res, indice) => {
                    const emailRegex = new RegExp(/^[A-Za-z0-9_!#$%&'*+\/=?`{|}~^.-]+@[A-Za-z0-9.-]+$/, "gm");
                    if (!emailRegex.test(res))
                        validoCorre = false;
                    else
                        validoCorre = true;
                });

                if (!validoCorre) {
                    toastr.error("alguna cuenta de correo no es valida, recuerde que debe estar separada por (;) ", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }


            }

            if ((this.enviarComunicado.Asunto == undefined || this.enviarComunicado.Asunto == null) || (this.enviarComunicado.Asunto != undefined && this.enviarComunicado.Asunto != null && this.enviarComunicado.Asunto.length == 0)) {
                toastr.error("Debe ingresar un asunto ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });

                return false;
            }

            if ((this.enviarComunicado.mensaje == undefined || this.enviarComunicado.mensaje == null) || (this.enviarComunicado.mensaje != undefined && this.enviarComunicado.mensaje != null && this.enviarComunicado.mensaje.length == 0)) {
                toastr.error("Debe ingresar un mensaje ", {
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

            if (this.files.length > 0) {
                this.getFiles(this.files).then(
                    data => {
                        var archivosSave = [];
                        for (var i = 0; i < data.length; i++) {
                            var fileSave = data[i];
                            archivosSave.push({ base64: fileSave.base64StringFile, nameFile: fileSave.fileName, type: fileSave.fileType });
                        }
                        this.enviarComunicado.archivos = archivosSave;
                        this.PreguntavalidarEnviarMensajeCliente(this.enviarComunicado);
                    }
                );
            }
            else {
                this.PreguntavalidarEnviarMensajeCliente(this.enviarComunicado);
            }




        },

        enviarMensajeCliente: function (enviarComunicado) {
            this.enviarComunicado.Id = this.pqrs.IdPQRS;
            var tempArr = [];
            var archivosRadicadoComunicadoTemp = JSON.parse(JSON.stringify(this.archivosRadicadoComunicado));
            $.each(archivosRadicadoComunicadoTemp, function(index, element) {
                tempArr.push({Id:parseInt(index), Enviar:element});
            });
            this.enviarComunicado.archivosRadicadoEnviar = tempArr;
            var orden = JSON.stringify({ comunicadoCliente: this.enviarComunicado });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/EnviarComunicadoCliente', {
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
                    var modal = $("#ModalEnviarComunicado");
                    modal.modal('hide');
                    this.enviarComunicado = {};
                    this.pqrs = {};
                    this.ObtenerPQRSpor();
                    // this.fup = JSON.parse(json.d);
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

        ChangeLanguage: function (lang) {
            window.AppPQRS.__VUE_I18N__.global.locale = lang;
        },

        TranslationCombo: function (text) {
            return window.AppPQRS.__VUE_I18N__.global.t(text);
        },

        MostrarListadoRequerido: function (itemPQRS) {
            this.files = [];
            this.listadosRequeridos = {};
            this.listadosRequeridos.IdPQRS = itemPQRS.IdPQRS;

            var orden = JSON.stringify({ idPQRS: itemPQRS.IdPQRS });
            this.mostrarLoad();

            fetch('FormPQRSConsulta.aspx/ObtenerUsuariosListados', {
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
                    this.listadosCargados = JSON.parse(json.d);
                    var modal = $("#ModalAgregarListadosRequerido");
                    modal.modal({ backdrop: 'static', keyboard: false }, 'show');
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

        ValidarGuardarListadosRequeridos: function () {


            if (this.files.length > 5) {
                toastr.error("No elija mas de 5 archivos", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.files.length == 0) {
                toastr.error("Por favor ingrese un archivo a cargar", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.files.length > 0) {
                this.getFiles(this.files).then(
                    data => {
                        var archivosSave = [];
                        for (var i = 0; i < data.length; i++) {
                            var fileSave = data[i];
                            archivosSave.push({ base64: fileSave.base64StringFile, nameFile: fileSave.fileName, type: fileSave.fileType });
                        }
                        this.listadosRequeridos.archivos = archivosSave;
                        this.PreguntaGuardarListadosRequeridos(this.listadosRequeridos);
                    }
                );
            }
            else {
                this.PreguntaGuardarListadosRequeridos(this.listadosRequeridos);
            }

        },

        GuardarListado: function (Requeridos) {
            var orden = JSON.stringify({ pqrsListadoReq: this.listadosRequeridos });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GuardarListadosRequeridos', {
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
                    var modal = $("#ModalAgregarListadosRequerido")
                    modal.modal('hide');
                    this.files = [];
                    this.listadosRequeridos = {};
                    this.ObtenerPQRSpor();

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

        MostrarImplementacionObra: function (itemPQRS) {
            this.implementacionObra = {};
            this.implementacionObra.IdPQRS = itemPQRS.IdPQRS;
            this.files = [];
            var modal = $("#ModalImplementacionObra");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        ValidarGuardarImplementacionObra: function (files) {

            this.files = files;
            if (this.files.length > 5) {
                toastr.error("No elija mas de 5 archivos", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.files.length == 0) {
                toastr.error("Por favor ingrese un archivo a cargar", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.files.length > 0) {
                this.getFiles(this.files).then(
                    data => {
                        var archivosSave = [];
                        for (var i = 0; i < data.length; i++) {
                            var fileSave = data[i];
                            archivosSave.push({ base64: fileSave.base64StringFile, nameFile: fileSave.fileName, type: fileSave.fileType });
                        }
                        this.implementacionObra.archivos = archivosSave;
                        this.PreguntaGuardarImplementacionObra(this.implementacionObra);
                    }
                );
            }
            else {
                this.PreguntaGuardarImplementacionObra(this.implementacionObra);
            }

        },

        GuardarImplementacionObra: function (obra) {
            var orden = JSON.stringify({ pqrsObra: this.implementacionObra });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GuardarimplmentacionObra', {
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
                    var modal = $("#ModalImplementacionObra")
                    modal.modal('hide');
                    this.files = [];
                    this.implementacionObra = {};
                    this.ObtenerPQRSpor();

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

        MostrarCierreObra: function (itemPQRS) {
            this.pqrs = itemPQRS;
            var modal = $("#ModalCierreObra");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        validarEnviarCierreReclamacion: function (cierre) {
            this.cierre = cierre;
            var date = new Date(this.cierre.fechaCierrePlan);

            if (date.toString() === 'Invalid Date' && this.cierre.fechaCierrePlan !== date.toISOString().slice(0, 10)) {
                toastr.error("Debe agregar una fecha valida ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.cierre.planAccion == undefined || this.cierre.planAccion == null) || (this.cierre.planAccion != undefined && this.cierre.planAccion != null && this.cierre.planAccion.length == 0)) {
                toastr.error("Debe agregar una descripción del plan de acción ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.cierre.planAccionDescripcion == undefined || this.cierre.planAccionDescripcion == null) || (this.cierre.planAccionDescripcion != undefined && this.cierre.planAccionDescripcion != null && this.cierre.planAccionDescripcion.length == 0)) {
                toastr.error("Debe agregar una descripción para el plan de acción ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }
            this.PreguntavaenviarCierreReclamacion(cierre);

        },
        enviarCierreReclamacion: function (cierre) {
            var orden = JSON.stringify({ planAccion: cierre.planAccion, fechaCierre: cierre.fechaCierrePlan, idpqrs: this.pqrs.IdPQRS, descripcionPlanAccion: cierre.planAccionDescripcion });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GuardarCierreReclamacion', {
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
                    var modal = $("#ModalCierreObra");
                    modal.modal('hide');
                    this.enviarComunicado = {};
                    this.ObtenerPQRSpor();
                    // this.fup = JSON.parse(json.d);
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
        MostrarProduccion: function (itemPQRS) {
            this.files = [];
            this.pqrsprod = { IdPQRS: itemPQRS.IdPQRS };
            var modal = $("#ModalProduccion");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        MostrarComprobanteObra: function (itemPQRS) {
            this.files = [];
            this.pqrscomobra = { IdPQRS: itemPQRS.IdPQRS };
            var modal = $("#ModalComprobanteObra");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        CerrarComprobanteObra: function() {
            if (this.files.length > 5) {
                toastr.error("No elija mas de 5 archivos", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.files.length > 0) {
                this.getFiles(this.files).then(
                    data => {
                        var archivosSave = [];
                        for (var i = 0; i < data.length; i++) {
                            var fileSave = data[i];
                            archivosSave.push({ base64: fileSave.base64StringFile, nameFile: fileSave.fileName, type: fileSave.fileType });
                        }
                        this.pqrscomobra.archivos = archivosSave;
                        this.GuardarComprobanteData(this.pqrscomobra);
                    }
                );
            }
            else {
                this.GuardarComprobanteData(this.pqrscomobra);
            }
        },

        GuardarComprobanteData: function (itemPQRSSave) {
            var orden = JSON.stringify({ prod: itemPQRSSave });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GuardarPQRSComprobante', {
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
                    var modal = $("#ModalComprobanteObra")
                    modal.modal('hide');
                    this.files = [];
                    this.pqrscomobra = {};
                    this.ObtenerPQRSpor();
                    // this.fup = JSON.parse(json.d);
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

        cerrarProduccion: function () {

            /*if (this.pqrsprod.fecha_plan_alum == undefined || this.pqrsprod.fecha_plan_alum == null) {
                toastr.error("Debe seleccionar la fecha planeada de entrega para producción de aluminio ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });

                return false;
            }

            if (this.pqrsprod.fecha_plan_acero == undefined || this.pqrsprod.fecha_plan_acero == null) {
                toastr.error("Debe seleccionar la fecha planeada de entrega para producción de acero ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });

                return false;
            }*/

            if (this.pqrsprod.fecha_req_alum == undefined || this.pqrsprod.fecha_req_acero == null) {
                toastr.error("Debe seleccionar la fecha planeada de entrega para producción de acero ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });

                return false;
            }

            //if (this.pqrsprod.fecha_desp_alum == undefined || this.pqrsprod.fecha_desp_alum == null) {
            //    toastr.error("Debe seleccionar la fecha planeada de despacho para producción de aluminio ", {
            //        "timeOut": "0",
            //        "extendedTImeout": "0"
            //    });

            //    return false;
            //}

            //if (this.pqrsprod.fecha_desp_acero == undefined || this.pqrsprod.fecha_desp_acero == null) {
            //    toastr.error("Debe seleccionar la fecha planeada de despacho para producción de acero ", {
            //        "timeOut": "0",
            //        "extendedTImeout": "0"
            //    });

            //    return false;
            //}

            //if (this.pqrsprod.fecha_ent_obra == undefined || this.pqrsprod.fecha_ent_obra == null) {
            //    toastr.error("Debe seleccionar la fecha planeada de despacho para producción de acero ", {
            //        "timeOut": "0",
            //        "extendedTImeout": "0"
            //    });

            //    return false;
            //}
            this.GuardarProduccionData(this.pqrsprod);
        },

        GuardarProduccionData: function (itemPQRSSave) {
            var orden = JSON.stringify({ prod: itemPQRSSave });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/GuardarPQRSProduccion', {
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
                    var modal = $("#ModalProduccion")
                    modal.modal('hide');
                    this.files = [];
                    this.pqrsprod = {};
                    this.ObtenerPQRSpor();
                    // this.fup = JSON.parse(json.d);
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

        PreguntaGuardarPQRSRespuesta: function (pqrsRespuesta) {
            this.pqrsRespuesta = pqrsRespuesta;

            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion2' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿esta seguro de guardar la respuesta?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion2").click(function () {
                            parentWindow.GuardarPQRSRespuestaProceso(this.pqrsRespuesta);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        PreguntavaenviarCierreReclamacion: function (cierre) {
            this.cierre = cierre;
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion3' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿esta seguro de cerrar la reclamación?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion3").click(function () {
                            parentWindow.enviarCierreReclamacion(cierre);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        PreguntaGuardarImplementacionObra: function (implementacionObra) {
            this.implementacionObra = implementacionObra;
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion4' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿está seguro de haber cargado todos los archivos necesarios?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion4").click(function () {
                            parentWindow.GuardarImplementacionObra(this.implementacionObra);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        PreguntaGuardarListadosRequeridos: function (listadosRequeridos) {
            this.listadosRequeridos = listadosRequeridos;
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion5' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿está seguro de haber cargado todos los archivos necesarios?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion5").click(function () {
                            parentWindow.GuardarListado(this.listadosRequeridos);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        PreguntavalidarEnviarMensajeCliente: function (enviarComunicado) {
            this.enviarComunicado = enviarComunicado;
            this.enviarComunicado.asunto = this.Asuntopqrs;
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion6' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿está seguro de enviar el comunicado adjunto?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion6").click(function () {
                            parentWindow.enviarMensajeCliente(this.enviarComunicado);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        RadicarPQRS: function (itemPQRS) {
            this.PQRSSelAsignacion = 0;
            var tipo = JSON.stringify({ idpqrs: itemPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/RadicarPQRS', {
                method: 'POST',
                body: tipo,
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
                    this.ObtenerPQRSpor();
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

        AnularPQRS: function (itemPQRS) {
            this.PQRSSelAsignacion = 0;
            var tipo = JSON.stringify({ idpqrs: itemPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/AnularPQRS', {
                method: 'POST',
                body: tipo,
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
                    this.ObtenerPQRSpor();
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

        RespuestaClienteLog: function (IdPQRS) {
            var orden = JSON.stringify({ idLog: IdPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/RespuestaClienteHistorico', {
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
                    this.pqrsRespuestaCliente = JSON.parse(json.d);
                    this.isStatusShown = true;
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

        ProduccionLog: function (IdPQRS) {
            var orden = JSON.stringify({ idPQRS: IdPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/RespuestaProduccionHistorico', {
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
                    this.pqrsProduccionHistorico = JSON.parse(json.d);
                    this.isStatusShown = true;
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

        PreguntarAvanzarEstadoListadosCompletos: function(idpqrs) {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarBorrarArchivo' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás cambiar el estado a Listados Completos?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarBorrarArchivo").click(function () {
                            parentWindow.ListadosCompletos(idpqrs);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        AskClosePQRS: function (pqrsId) {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Está seguro de cerrar este PQRS?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion").click(function () {
                            parentWindow.ClosePQRS(pqrsId);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        ClosePQRS: function(pqrsId) {
            var body = JSON.stringify({ pqrsId: pqrsId });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ClosePQRS', {
                method: 'POST',
                body: body,
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
                    this.ObtenerPQRSpor();
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

        ListadosCompletos: function(idpqrs) {
            var orden = JSON.stringify({ idpqrs: idpqrs });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ListadosCompletos', {
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
                    this.PQRSDTOConsulta = JSON.parse(json.d);
                    this.isStatusShown = true;
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

        RadicacionElaboracionLog: function (IdPQRS) {
            var orden = JSON.stringify({ idPQRS: IdPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/RespuestaElaboracionHistorico', {
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
                    this.PQRSDTOConsulta = JSON.parse(json.d);
                    this.isStatusShown = true;
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

        MostrarResumen: function (idpqrs) {
            // create the form
            var form = document.createElement('form');
            form.setAttribute('method', 'post');
            form.setAttribute('action', ' FormPQRSResumen.aspx?IdPQRS='+idpqrs);
            form.setAttribute("target", "_blank");

                    // add form to body and submit
            document.body.appendChild(form);
            form.submit();
        },
        DescargarArchivo: function(NombreArchivo, Ruta) {
            if (NombreArchivo != null) {
                var test = new FormData();
                window.location = "DownloadHandler.ashx?NombreArchivo=&Ruta=" + Ruta;
            }
        },
        bodyRowClassNameFunction(row) {
            // Lógica para determinar el color de fondo para cada fila
            // Puedes retornar diferentes colores basados en propiedades de la fila
            if(row.IsInvolucred == 1 || this.calculedRolId == 1 || this.calculedRolId == 3) {
                return row.ColorClase;
            } else {
                return 'd-none';
            }
        }
    }
});


app.config.compilerOptions.isCustomElement = (tag) => {
    return tag.startsWith('easydata') // (return true)
}

window.Vue = VueModule;
window.AppPQRS = app;
app.use(i18n);
app.mount('#app');

$(".menu-bar").on('click', function() {
    $("#wrapper").toggleClass("openSidebarYt");
    $(".sidebarYt").toggleClass("sidebarYtOpened");
});