import * as VueModule from 'vue';
import Vue3EasyDataTable from 'vue3-easy-data-table';

const app = VueModule.createApp({
    data() {
        return {
            idPQRS: 0,
            tipoPQRS: 0,
            infoPQRSNroOrden: "",
            infoPQRSFuente: "",
            infoPQRSIdFuenteReclamo: "",
            infoPQRSDetalle: "",
            infoPQRSDetalleLastSaved: "",
            infoPQRSEstado: "",
            infoPQRSEstadoID: 0,
            infoPQRSDireccionRespuesta: "",
            infoPQRSDireccionRespuestaLastSaved: "",
            infoPQRSEmailRespuesta: "",
            infoPQRSEmailRespuestaLastSaved: "",
            infoPQRSTelefonoRespuesta: "",
            infoPQRSTelefonoRespuestaLastSaved: "",
            infoPQRSFechaCreacion: "",
            infoPQRSUsuarioCreacion: "",
            infoPQRSNombreRespuesta: "",
            infoPQRSNombreRespuestaLastSaved: "",
            infoPQRSClienteNombre: "",
            infoPQRSOtroClienteNombre: "",
            infoPQRSOtroClienteNombreLastSaved: "",
            infoPQRSDescripcionProcedencia: "",
            infoPQRSOrdenGarantiaOMejora: "",
            infoPQRSIdOrdenProcedente: 0,
            infoPQRSIdCiudad: 0,
            tempInfoPQRSIdCiudad: 0,
            canSkipStates: false,
            infoPQRSIsInvolucred: false,
            infoPQRSIdPais: 0,
            infoPQRSIdCliente: null,
            infoPQRSIdClienteTemp: null,
            mostrarCamposCliente: true,
            infoFupPais: "",
            infoFupCiudad: "",
            infoFupEmpresa: "",
            infoFupContacto: "",
            infoFupObra: "",
            infoFupCliente: "",
            infoFupID: null,
            infoFupId_Ofa: "",
            infoPQRSTipo: "",
            infoPQRSSubTipo: "",
            infoPQRSOrdenProcedente: "",
            fechaRadicado: "",
            usuarioRadicado: "",
            radicado: [],
            comunicados: [],
            listados: [],
            planos: [],
            armado: [],
            archivosImplementacionObra: [],
            archivosCierre: [],
            usuarioAsignacionProcesos: "",
            fechaAsignacionProcesos: "",
            asignacionProcesos: {},
            procesosAdmin: [],
            procesosAdminCopiaCancelar: [],
            respuestasProcesos: [],
            fechaRespuesta: "",
            usuarioRespuesta: "",
            noConformidades: [],
            usuarioNoConformidades: "",
            fechaNoConformidades: "",
            esProcedente: null,
            plantas: [],
            mostrarDatosFup: false,
            addFilesRadicado: [],
            pqrsPosiblesEstados: [],
            pqrsSiguienteEstadoID: 0,
            pqrsSiguienteEstadoDesc: "",
            descripcionListado: "",
            descripcionPlanos: "",
            descripcionPlanoArmado: "",
            correosListado: "",
            isDragging: false,
            Asuntopqrs: "",
            funcionalidadesBotones: {
                editarInformacionPQRS: false,
                añadirArchivosRadicado: false,
                añadirProcesosAsignacion: false,
                solicitarInformacionProcesosAsignados: false,
                añadirArchivosListados: false,
                añadirArchivosComprobanteObra: false,
                añadirArchivosCierre: false,
                adicionarRespuestasProcesos: false,
                eliminarArchivosRadicado: false,
                eliminarArchivosListados: false,
                eliminarArchivosImplementacion: false,
                eliminarArchivosCierre: false,
                eliminarArchivosRespuestas: false,
                editarFechasProduccion: false,
                eliminarArchivosComprobacion: false
            },
            // Cosas de PQRSConsulta
            procesos: [],
            procesosToAdd: [],
            ProcesoAddProcesoToPNCTemp: "",
            tipoNC: [],
            pqrsProcedente: {},
            pqrsGenerarOrden: {},
            mostrarBtnSiguienteEstado: false,
            rolUsuario: 0,
            idPQRSProcesoTemp: 0,
            nombrePQRSProcesoTemp: "",
            correosPQRSProcesoTemp: "",
            idRespuestaTemp: 0,
            adicionRespuestaProceso: {},
            pqrsprod: {},
            files: [],
            enviarComunicado: {},
            PQRSSelAsignacion: 0,
            numordenes: [],
            listadosRequeridos: {},
            listadosCargados: [],
            cierre: {},
            comprobacionCierreYaRealizado: '',
            implementacionObra: {},
            fechaPlanAlum: "",
            fechaReqAlum: "",
            fechaPlanAcero: "",
            fechaReqAcero: "",
            fechaDespAlum: "",
            fechaDespAcero: "",
            archivosProduccion: [],
            pqrscomobra: {},
            archivosRadicadoComunicado: {},
            archivosComprobanteComunicado: {},
            cantidadComentario: 0,
            procesosAsignadosPQRS: [],
            pqrsRespuesta: {},
            pqrsProcesoAclaracionTemp: null,
            mostrarBtnAnularPQRS: false,
            rolId: null,
            usuarioId: null,
            calculedRolId: null,
            debeCargarListadosOPlanosOArmado: {
                listados: {},
                planos: {},
                armado: {}
            },
            lisHallazgos: [],
            bitacoraPQRS: [],
            bitacoraEventos: [],
            semaforo: "",
            FechaEntregaObra: "",
            FechaEntregaObraEdit: "",
            EditingFechaEntregaObra: false,
            noConformidadEdit: {},
            noConformidadEditNC: {},
            noConformidadDetails: {},
            lastDatePlanAccion: null,
            hallazgosNoProcedentes: {},
            familiasGarantias: {},
            currentFamiliaGarantia: null,
            clasificacionesPlanAccion: {},
            textoOtroProductosGarantia: "",
            pqrsAsociadosOrden: null,
            puedeSerCerrada: null,
            previousFamiliaGarantiaId: null
        }
    },
    mounted() {
        // Reading of query parameters, if param PQRSId is null, the page doesn't load
        var urlParams = new URLSearchParams(window.location.search);
        var PQRSId = urlParams.get('IdPQRS');
        this.ObtenerBitacoraEventos();
        this.ObtenerFamiliasGarantias();
        this.ObtenerClasificacionesPlanAccion();
        if (PQRSId != null) {
            this.idPQRS = PQRSId;
            this.CargarIdPQRS();
        }
        $('#editResumenDataModal').on('hidden.bs.modal', this.RestorePQRSLastSavedData);
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

        doOnLoad: function () { },

        CargarIdPQRS: function () {
            this.CargarPosiblesEstados();
            this.ObtenerRolUsuario();
            this.ObtenerNumOrdenes();
        },

        ObtenerPQRSAsociadosAOrden: function () {
            var data = JSON.stringify({ OrderNumber: this.infoPQRSNroOrden });
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

        drop(e) {
            e.preventDefault();
            this.$refs.file.files = e.dataTransfer.files;
            this.onChange();
            this.isDragging = false;
        },

        onChange() {

            this.files = [...this.$refs.file.files];
        },

        ObtenerNumOrdenes: function () {
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

        ManageRolByUserId: function (userId, rolId) {
            // Buscamos rol de administrador PQRS
            /*
            * 1 = Administrador
            * 2 = Rol de Logistica
            * 3 = Sólo consulta
            *
            */
            this.canSkipStates = false;
            this.calculedRolId = null;
            if ([840, 1019, 1160, 5473, 432, 1358, 213, 3118].indexOf(userId) > -1) {
                this.calculedRolId = 1;
            } else if ([794, 590].indexOf(userId) > -1) {
                this.calculedRolId = 3;
            } else if (rolId == 15) {
                this.calculedRolId = 2;
            }

            // Sólo Lorena y Melba  Orejuela pueden saltar estados de forma manual
            if([840, 1358, 213].indexOf(userId) > -1) {
                this.canSkipStates = true;
            }
        },

        ObtenerBitacoraEventos: function () {
            fetch('FormPQRSResumen.aspx/ObtenerBitacoraEventos', {
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
                    this.bitacoraEventos = JSON.parse(json.d);
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

        ObtenerFamiliasGarantias: function () {
            fetch('FormPQRSResumen.aspx/ObtenerFamiliasGarantias', {
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
                    this.familiasGarantias = JSON.parse(json.d);
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

        ObtenerClasificacionesPlanAccion: function () {
            fetch('FormPQRSResumen.aspx/ObtenerClasificacionesPlanAccion', {
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
                    this.clasificacionesPlanAccion = JSON.parse(json.d);
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

        ObtenerHallazgos: function () {
            var orden = JSON.stringify({ idpqrs: parseInt(this.idPQRS), orden: this.infoPQRSNroOrden });
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ObtenerHallazgos', {
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
                        this.hallazgosNoProcedentes[element.Id] = false;
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

        RestorePQRSLastSavedData() {
            this.infoPQRSDetalle = this.infoPQRSDetalleLastSaved;
            this.infoPQRSDireccionRespuesta = this.infoPQRSDireccionRespuestaLastSaved;
            this.infoPQRSEmailRespuesta = this.infoPQRSEmailRespuestaLastSaved;
            this.infoPQRSTelefonoRespuesta = this.infoPQRSTelefonoRespuestaLastSaved;
            this.infoPQRSNombreRespuesta = this.infoPQRSNombreRespuestaLastSaved;
            this.infoPQRSOtroClienteNombre = this.infoPQRSOtroClienteNombreLastSaved;
        },

        CargarDatosGeneralesFupPQRS: function () {
            var param = { idPQRS: this.idPQRS };
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ObtenerDatosGeneralesFupPQRS', {
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
                var data = JSON.parse(json.d);
                this.MostrarCliente();
                this.MostrarDatoGeneralesFupPQRS(data[0]);
                this.CargarTimelinePQRS();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
        },

        CargarProcesosAdmin: function () {
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ObtenerProcesosAdmin', {
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
                this.procesosAdmin = data;
                this.CargarDatosGeneralesFupPQRS();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen Procesos Admin");
            });
        },

        ObtenerRolUsuario: function () {
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ObtenerRolUsuario', {
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
                this.rolUsuario = data.rol;
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen Obtener Rol");
            });
        },

        MostrarDatoGeneralesFupPQRS: function (data) {
            this.infoPQRSNroOrden = data.NroOrden;
            if (data.NroOrden != "") {
                this.ObtenerHallazgos();
                this.ObtenerPQRSAsociadosAOrden();
            }
            this.puedeSerCerrada = data.PuedeSerCerrada;
            this.$refs.semaforo.style.color = data.Semaforo;
            this.infoPQRSFuente = data.FuenteDescripcion;
            this.infoPQRSIdFuenteReclamo = data.IdFuenteReclamo;
            this.infoPQRSIsInvolucred = data.IsInvolucred;
            this.infoPQRSDetalle = data.Detalle;
            this.infoPQRSDetalleLastSaved = data.Detalle;
            this.infoPQRSEstado = data.EstadoDescripcion;
            this.infoPQRSEstadoID = data.EstadoID;
            this.infoPQRSDireccionRespuesta = data.DireccionRespuesta;
            this.infoPQRSDireccionRespuestaLastSaved = data.DireccionRespuesta;
            this.infoPQRSEmailRespuesta = data.EmailRespuesta;
            this.infoPQRSEmailRespuestaLastSaved = data.EmailRespuesta;
            this.infoPQRSTelefonoRespuesta = data.TelefonoRespuesta;
            this.infoPQRSTelefonoRespuestaLastSaved = data.TelefonoRespuesta;
            this.infoPQRSFechaCreacion = data.FechaCreacion;
            this.infoPQRSUsuarioCreacion = data.UsuarioCreacion;
            this.infoPQRSNombreRespuesta = data.NombreRespuesta;
            this.infoPQRSNombreRespuestaLastSaved = data.NombreRespuesta;
            this.FechaEntregaObra = (data.FechaEntregaObra != null && data.FechaEntregaObra != undefined) ? data.FechaEntregaObra.substring(0, 10) : "";
            this.FechaEntregaObraEdit = (data.FechaEntregaObra != null && data.FechaEntregaObra != undefined) ? data.FechaEntregaObra.substring(0, 10) : "";
            this.infoFupCiudad = data.CiudadNombre;
            this.infoFupPais = data.PaisNombre;
            this.infoPQRSIdPais = data.PaisId;
            this.tempInfoPQRSIdCiudad = data.CiudadId;
            this.infoPQRSIdCliente = data.IdCliente;
            this.infoPQRSIdClienteTemp = data.IdCliente;
            this.OrdenProcedencia = data.OrdenProcedencia;
            this.infoPQRSClienteNombre = data.ClienteNombre2;
            if (data.FupID != undefined) {
                this.infoFupContacto = data.Contacto;
                this.infoFupObra = data.ObraNombre;
                this.infoPQRSClienteNombre = data.ClienteNombre;
                this.infoFupID = data.FupID;
                this.infoFupId_Ofa = data.Id_Ofa;
                this.mostrarDatosFup = true;
            } else {
                this.infoFupID = null;
                this.mostrarDatosFup = false;
            }
            this.esProcedente = data.EsProcedente;
            this.descripcionListado = data.DescripcionListados;
            this.descripcionPlanos = data.DescripcionPlanos;
            this.descripcionPlanoArmado = data.DescripcionPlanoArmado;
            this.correosListado = data.CorreosListado;
            this.tipoPQRS = data.TipoPQRSId;
            //this.cierre.planAccion = data.PlanAccion;
            //this.cierre.fechaCierrePlan = data.FechaCierre;
            this.comprobacionCierreYaRealizado = data.FechaCierre;
            //this.cierre.planAccionDescripcion = data.DescripcionPlanAccion;
            this.infoPQRSOtroClienteNombreLastSaved = data.OtroCliente;
            this.infoPQRSOtroClienteNombre = data.OtroCliente;
            this.infoPQRSTipo = data.TipoPQRSDescripcion;
            this.infoPQRSSubTipo = data.SubTipoPQRSDescripcion;
            this.fechaPlanAlum = (data.FechaPlanAlum != null ? data.FechaPlanAlum.substring(0, 10) : "");
            this.fechaReqAlum = (data.FechaReqAlum != null ? data.FechaReqAlum.substring(0, 10) : "");
            this.fechaReqAcero = (data.FechaReqAcero != null ? data.FechaReqAcero.substring(0, 10) : "");
            this.fechaDespAlum = (data.FechaDespAlum != null ? data.FechaDespAlum.substring(0, 10) : "");
            this.fechaDespAcero = (data.FechaDespAcero != null ? data.FechaDespAcero.substring(0, 10) : "");
            this.infoPQRSDescripcionProcedencia = data.DescripcionProcedencia;
            this.infoPQRSOrdenGarantiaOMejora = (data.OrdenGarantiaOMejora == "OG" ? "Orden de Garantia" : (data.OrdenGarantiaOMejora == "OM" ? "Orden de Mejora" : ""));
            this.infoPQRSIdOrdenProcedente = data.IdOrdenProcedente;
            this.infoPQRSOrdenProcedente = data.OrdenProcedente;
            var orden = JSON.stringify({ idpqrs: this.idPQRS });
            this.mostrarLoad();
            this.ProcesarSiguienteEstado();
            this.AdministrarFuncionesPorEstado();
            this.MostrarListadoRequerido(false);
        },

        ProcesarSiguienteEstado: function () {
            let idEstadoActual = this.infoPQRSEstadoID;
            if (idEstadoActual == 0 || idEstadoActual == 9) {
                this.mostrarBtnSiguienteEstado = true;
                if (idEstadoActual == 0 && this.rolId == 840) {
                    this.mostrarBtnAnularPQRS = true;
                } else {
                    this.mostrarBtnAnularPQRS = false;
                }
            } else {
                this.mostrarBtnSiguienteEstado = false;
                this.mostrarBtnAnularPQRS = false;
            }
            for (let c = 0; c < this.pqrsPosiblesEstados.length; c++) {
                if (this.pqrsPosiblesEstados[c].PQRSEstadosID == idEstadoActual) {
                    if (c < this.pqrsPosiblesEstados.length - 1) {
                        if (idEstadoActual == 9) {
                            c++;
                        }
                        this.pqrsSiguienteEstadoID = this.pqrsPosiblesEstados[c + 1].PQRSEstadosID;
                        this.pqrsSiguienteEstadoDesc = this.pqrsPosiblesEstados[c + 1].Descripcion;
                    }
                    this.$refs.statusColorBar.style.backgroundColor = this.pqrsPosiblesEstados[c].Color;
                }
            }
        },

        CargarTimelinePQRS: function () {
            var param = { idPQRS: this.idPQRS }
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ObtenerPQRSTimeline', {
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
                var data = JSON.parse(json.d);
                this.ProcesarTimeline(data);
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            });
        },

        LlenarOptionsProcesosRespuestas: function (data) {
            $("#selectProcesoRespuesta").html('<option value="0">-- Seleccionar --</option>');
            data.forEach(element => {
                $("#selectProcesoRespuesta").append($("<option />").val(element.Id).text(element.Proceso));
            });
        },

        ProcesarTimeline: function (data) {
            this.radicado = data.lisRadicado;
            this.radicado.forEach(element => {
                this.archivosRadicadoComunicado[element.IdArchivo] = true;
            });
            this.asignacionProcesos = data.lisAsignacionProcesos;
            this.LlenarOptionsProcesosRespuestas(data.lisAsignacionProcesos);
            var procesosAdminTemp = [];
            this.procesosAdmin.forEach(procesoAdmin => {
                if (procesoAdmin.TipoPQRSId == this.tipoPQRS) {
                    procesosAdminTemp.push(procesoAdmin);
                }
            });
            this.procesosAdmin = procesosAdminTemp;

            this.respuestasProcesos = data.lisRespuestaProcesos;
            this.noConformidades = data.lisNoConformidades;
            if (this.noConformidades.length == 0) {
                this.lastDatePlanAccion = null;
            } else {
                this.noConformidades.forEach(element => {
                    this.lastDatePlanAccion = element.PlanAccionFecha
                });
            }

            this.listados = data.lisListados;
            this.planos = data.lisPlanos;
            this.armado = data.lisArmado;
            this.archivosImplementacionObra = data.lisImplementacionObra;
            this.archivosCierre = data.lisCierreObra;
            this.comunicados = data.lisComunicados;

            this.archivosProduccion = data.lisProduccion;
            this.archivosProduccion.forEach(element => {
                this.archivosComprobanteComunicado[element.IdArchivo] = true;
            });

            this.bitacoraPQRS = data.lisControlCambiosCierre
            this.bitacoraPQRS.forEach(element => {
                if (element.Anexos != "") {
                    element.Anexos = JSON.parse(element.Anexos);
                }
            });
            this.cantidadComentario = data.lisControlCambiosCierre.length;
        },

        confirmarSaltarEstado: function() {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            let value = $("#saltarEstadoCmb").val();
            if(value == null) {
                return;
            }
            let estado = this.pqrsPosiblesEstados.find(x => x.PQRSEstadosID == parseInt(value));
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion6' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                'Estas seguro de saltar al estado ' + estado.Descripcion + '?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion6").click(function () {
                            parentWindow.saltarEstado(estado.PQRSEstadosID);
                        });
                    }
                });
            toastr.options.timeOut = "1000";
            toastr.closeButton = false;            
        },

        saltarEstado: function(estadoId) {
            this.pqrsSiguienteEstadoID = estadoId;
            this.ActualizarEstadoPQRS();
        },

        CargarPosiblesEstados: function () {
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ObtenerPosiblesEstados', {
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
                this.pqrsPosiblesEstados = data;
                this.CargarProcesosAdmin();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen Posibles Estados");
            });
        },

        AdministrarFuncionesPorEstado: function () {
            this.RestablecerFuncionalidades();
            switch (this.infoPQRSEstadoID) {
                case 0:
                    this.funcionalidadesBotones.editarInformacionPQRS = true;
                    this.funcionalidadesBotones.añadirArchivosRadicado = true;
                    this.funcionalidadesBotones.eliminarArchivosRadicado = true;
                    break;
                case 1:
                    this.funcionalidadesBotones.añadirProcesosAsignacion = true;
                    this.funcionalidadesBotones.eliminarArchivosRadicado = true;
                    break;
                case 2:
                    this.funcionalidadesBotones.añadirProcesosAsignacion = true;
                    this.funcionalidadesBotones.solicitarInformacionProcesosAsignados = true;
                    this.funcionalidadesBotones.eliminarArchivosRadicado = true;
                    this.funcionalidadesBotones.eliminarArchivosRespuestas = true;
                    this.funcionalidadesBotones.adicionarRespuestasProcesos = true;
                    break;
                case 3:
                    this.funcionalidadesBotones.añadirProcesosAsignacion = true;
                    this.funcionalidadesBotones.solicitarInformacionProcesosAsignados = true;
                    this.funcionalidadesBotones.adicionarRespuestasProcesos = true;
                    this.funcionalidadesBotones.eliminarArchivosRespuestas = true;
                    break;
                case 5:
                    this.funcionalidadesBotones.eliminarArchivosListados = true;
                    break;
                case 8:
                    this.funcionalidadesBotones.eliminarArchivosComprobacion = true;
                    this.funcionalidadesBotones.añadirArchivosComprobanteObra = true;
                    break;
                case 10:
                    this.funcionalidadesBotones.editarFechasProduccion = true;
                case 9:
                    this.funcionalidadesBotones.eliminarArchivosCierre;
                    this.funcionalidadesBotones.añadirArchivosCierre = true;
                    break;
                case 12:
                    this.funcionalidadesBotones.eliminarArchivosComprobacion = true;
                    this.funcionalidadesBotones.añadirArchivosComprobanteObra = true;
                    break;
            }
        },

        DescargarArchivo: function (NombreArchivo, Ruta) {
            if (vaNombre == 1) {
                if (NombreArchivo != null) {
                    var test = new FormData();
                    window.location = "DownloadHandler.ashx?NombreArchivo=&Ruta=" + Ruta;
                }
            }
            else {
                if (NombreArchivo != null) {
                    var test = new FormData();
                    window.location = "DownloadHandler.ashx?NombreArchivo=" + NombreArchivo + "&Ruta=" + Ruta;
                }
            }
        },

        AdicionarArchivosRadicado: function () {
            const formData = new FormData();

            for (var i = 0; i < this.$refs.addFilesRadicado.files.length; i++) {
                let file = this.$refs.addFilesRadicado.files[i];
                formData.append(file.name, file);
            }
            formData.append("IdPQRS", this.idPQRS);
            formData.append("Tipo", "Radicado");

            this.mostrarLoad();
            fetch('UploadFilesPQRSResumen.ashx', {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                    //'Content-Type': 'multipart/form-data'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    if (this.$refs.addFilesRadicado.files.length > 0) {
                        this.$refs.addFilesRadicado.value = null;
                        toastr.success("Archivos subidos correctamente", "PQRS Resumen");
                        this.CargarIdPQRS();
                        this.$refs.CerrarModalAdicionarArchivos.click();
                    } else {
                        toastr.info("No hay archivos para subir", "PQRS Resumen");
                    }
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
        },

        AdicionarArchivosImplementacion: function () {
            const formData = new FormData();

            for (var i = 0; i < this.$refs.addFilesImplementacion.files.length; i++) {
                let file = this.$refs.addFilesImplementacion.files[i];
                formData.append(file.name, file);
            }
            formData.append("IdPQRS", this.idPQRS);
            formData.append("Tipo", "Implementacion");

            this.mostrarLoad();
            fetch('UploadFilesPQRSResumen.ashx', {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                    //'Content-Type': 'multipart/form-data'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    if (this.$refs.addFilesImplementacion.files.length > 0) {
                        this.$refs.addFilesImplementacion.value = null;
                        toastr.success("Archivos subidos correctamente", "PQRS Resumen - Implementacion");
                        this.pqrsSiguienteEstadoID = 8;
                        this.ActualizarEstadoPQRS();
                        this.CargarIdPQRS();
                        this.$refs.CerrarModalAdicionarArchivosImplementacion.click();
                    } else {
                        toastr.info("No hay archivos para subir", "PQRS Resumen - Implementacion");
                    }
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
        },

        AdicionarArchivosCierre: function () {
            const formData = new FormData();

            for (var i = 0; i < this.$refs.addFilesCierre.files.length; i++) {
                let file = this.$refs.addFilesCierre.files[i];
                formData.append(file.name, file);
            }
            formData.append("IdPQRS", this.idPQRS);
            formData.append("Tipo", "Cierre");

            this.mostrarLoad();
            fetch('UploadFilesPQRSResumen.ashx', {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                    //'Content-Type': 'multipart/form-data'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    if (this.$refs.addFilesCierre.files.length > 0) {
                        this.$refs.addFilesCierre.value = null;
                        toastr.success("Archivos subidos correctamente", "PQRS Resumen - Cierre");
                        this.CargarIdPQRS();
                        this.$refs.CerrarModalAdicionarArchivosCierre.click();
                    } else {
                        toastr.info("No hay archivos para subir", "PQRS Resumen - Cierre");
                    }
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
        },

        AdicionarArchivosListados: function () {
            const formData = new FormData();

            for (var i = 0; i < this.$refs.addFilesListados.files.length; i++) {
                let file = this.$refs.addFilesListados.files[i];
                formData.append(file.name, file);
            }
            formData.append("IdPQRS", this.idPQRS);
            formData.append("Tipo", "Listados");

            this.mostrarLoad();
            fetch('UploadFilesPQRSResumen.ashx', {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                    //'Content-Type': 'multipart/form-data'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    if (this.$refs.addFilesListados.files.length > 0) {
                        this.$refs.addFilesListados.value = null;
                        toastr.success("Archivos subidos correctamente", "PQRS Resumen - Listados");
                        this.CargarIdPQRS();
                        this.$refs.CerrarModalAdicionarArchivosListados.click();
                    } else {
                        toastr.info("No hay archivos para subir", "PQRS Resumen - Listados");
                    }
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
        },

        GuardarPQRSDatosGenerales() {
            var param = {
                idpqrs: this.idPQRS,
                detalle: this.infoPQRSDetalle,
                direccion: this.infoPQRSDireccionRespuesta,
                email: this.infoPQRSEmailRespuesta,
                telefono: this.infoPQRSTelefonoRespuesta,
                nombre: this.infoPQRSNombreRespuesta,
                otroCliente: this.infoPQRSOtroClienteNombre,
                idCiudad: this.infoPQRSIdCiudad,
                idPais: this.infoPQRSIdPais,
                idCliente: this.infoPQRSIdClienteTemp
            };
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/GuardarDatosGeneralesPQRS', {
                method: 'POST',
                body: JSON.stringify(param),
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
                } else {
                    this.CargarDatosGeneralesFupPQRS();
                    toastr.success("Información general actualizada con éxito", "PQRS Resumen");
                    this.$refs.cerrarModificarDatosGeneralesPQRS.click();
                }
                return res.json();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
        },

        DeleteStagedAssignedProcess(index) {
            this.asignacionProcesos.splice(index, 1);
        },

        adicionarProcesoTemporal: function() {
            const selectedProcess = this.$refs.selectAdminProcesses.value;
            let adminProcess = null;
            this.procesosAdmin.forEach(element => {
                if (element.Proceso == selectedProcess) {
                    adminProcess = element;
                }
            });
            if (adminProcess != null) {
                var newProcess = {
                    Proceso: selectedProcess,
                    Email: adminProcess.EmailProceso,
                    Observacion: "",
                    EmailProcesoCC: adminProcess.EmailProcesoCC,
                    IsNew: true
                };
                this.asignacionProcesos.push(newProcess);
                toastr.info(selectedProcess + " añadido", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
            }
        },

        ValidarGuardarProcesosAsignados: function () {
            let procesosNuevos = this.asignacionProcesos.filter(proceso => {
                return proceso.IsNew == true;
            });

            procesosNuevos.forEach(function (proceso, index) {
                let tempEmailArr = proceso.Email.split(";");
                if (tempEmailArr.length > 1) {
                    procesosNuevos.splice(index, 1);
                    tempEmailArr.forEach(email => {
                        var newProcess = {
                            Proceso: proceso.Proceso,
                            Email: email,
                            Observacion: proceso.Observacion,
                            EmailProcesoCC: proceso.EmailProcesoCC,
                            IsNew: true
                        };
                        procesosNuevos.push(newProcess);
                    });
                }
            });

            if (procesosNuevos.length == 0) {
                toastr.info("Nada que guardar", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

//            const emailRegex = new RegExp(/^[a-zA-Z0-9._%+-]+@forsa\.net\.co$/);
            const emailRegex = new RegExp(/^[A-Za-z0-9_!#$%&'*+\/=?`{|}~^.-]+@[A-Za-z0-9.-]+$/, "gm");
            let valid = true;
            procesosNuevos.forEach(proceso => {
                proceso.Email = proceso.Email.trim();
                if (!emailRegex.test(proceso.Email)) {
                    valid = false;
                    toastr.error("Alguna cuenta de correo para el proceso " + proceso.Proceso + " no es válida", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }
            });

            if (valid) {
                this.PreguntaGuardarProcesosAsignados();
            }
        },

        PreguntaGuardarProcesosAsignados: function () {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion6' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                'Está seguro de guardar los procesos asignados?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion6").click(function () {
                            parentWindow.GuardarAsignacionProcesos();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        GuardarAsignacionProcesos: function () {
            let procesosNuevos = this.asignacionProcesos.filter(proceso => {
                return proceso.IsNew == true;
            });

            procesosNuevos.forEach(function (proceso, index) {
                let tempEmailArr = proceso.Email.split(";");
                if (tempEmailArr.length > 1) {
                    procesosNuevos.splice(index, 1);
                    tempEmailArr.forEach(email => {
                        var newProcess = {
                            Proceso: proceso.Proceso,
                            Email: email,
                            Observacion: proceso.Observacion,
                            EmailProcesoCC: proceso.EmailProcesoCC,
                            IsNew: true
                        };
                        procesosNuevos.push(newProcess);
                    });
                }
            });

            this.mostrarLoad();
            var param = {
                idPQRS: parseInt(this.idPQRS),
                procesos: JSON.parse(JSON.stringify(procesosNuevos))
            };
            fetch('FormPQRSResumen.aspx/GuardarProcesos', {
                method: 'POST',
                body: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                this.ocultarLoad();
                if (!res.ok) {
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
                })
                .then(json => {
                    this.ocultarLoad();
                    $("#modalAdicionarProcesos").modal('hide');
                    let response = JSON.parse(json.d);
                    toastr.success("Éxito en la operación", "PQRS Resumen - Actualizar procesos");
                    if (response) {
                        if (this.tipoPQRS != 6) {
                            if (this.infoPQRSEstadoID == 1) {
                                this.ActualizarEstadoPQRS();
                            } else if (this.infoPQRSEstadoID == 3) {
                                this.pqrsSiguienteEstadoID = 2;
                                this.ActualizarEstadoPQRS();
                            }
                        } else {
                            this.pqrsSiguienteEstadoID = 9;
                            this.ActualizarEstadoPQRS();
                        }
                        this.CargarIdPQRS();
                    }
                })
                .catch(error => {
                    this.ocultarLoad();
                    toastr.error(error, "PQRS Resumen - Actualizar procesos");
                });
        },

        AnularPQRS: function () {
            this.mostrarLoad();
            var param = { IdPQRS: parseInt(this.idPQRS), IdNuevoEstado: 13 };
            fetch('FormPQRSResumen.aspx/ActualizarEstado', {
                method: 'POST',
                body: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    toastr.success("Éxito en la operación", "PQRS Resumen - Actualizar Estado");
                    this.CargarIdPQRS();
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen - Actualizar Estado");
            });
        },

        ActualizarEstadoPQRS: function () {
            if (this.pqrsSiguienteEstadoID == 1) {
                this.RadicarPQRS();
                return null;
            }
            this.mostrarLoad();
            var param = { IdPQRS: parseInt(this.idPQRS), IdNuevoEstado: this.pqrsSiguienteEstadoID };
            fetch('FormPQRSResumen.aspx/ActualizarEstado', {
                method: 'POST',
                body: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    toastr.success("Éxito en la operación", "PQRS Resumen - Actualizar Estado");
                    this.CargarIdPQRS();
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen - Actualizar Estado");
            });
        },

        RadicarPQRS: function () {
            this.mostrarLoad();
            var param = { idpqrs: this.idPQRS };
            fetch('FormPQRSConsulta.aspx/RadicarPQRS', {
                method: 'POST',
                body: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    toastr.success("Éxito en la operación", "PQRS Resumen - Actualizar Estado");
                    this.CargarIdPQRS();
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen - Actualizar Estado");
            });
        },

        RestablecerFuncionalidades: function () {
            this.funcionalidadesBotones.editarInformacionPQRS = false;
            this.funcionalidadesBotones.añadirArchivosRadicado = false;
            this.funcionalidadesBotones.añadirProcesosAsignacion = false;
            this.funcionalidadesBotones.solicitarInformacionProcesosAsignados = false;
            this.funcionalidadesBotones.añadirArchivosListados = false;
            this.funcionalidadesBotones.añadirArchivosComprobanteObra = false;
            this.funcionalidadesBotones.añadirArchivosCierre = false;
            this.funcionalidadesBotones.adicionarRespuestasProcesos = false;
            this.funcionalidadesBotones.eliminarArchivosRadicado = false;
            this.funcionalidadesBotones.eliminarArchivosListados = false;
            this.funcionalidadesBotones.eliminarArchivosImplementacion = false;
            this.funcionalidadesBotones.eliminarArchivosCierre = false;
            this.funcionalidadesBotones.editarFechasProduccion = false;
            this.funcionalidadesBotones.eliminarArchivosRespuestas = false;
            this.funcionalidadesBotones.eliminarArchivosComprobacion = false;
        },

        MostrarModalSolicitarInformacionProcesos: function (idPQRSProceso, proceso, correos, idRespuesta) {
            this.idPQRSProcesoTemp = idPQRSProceso;
            this.nombrePQRSProcesoTemp = proceso;
            this.correosPQRSProcesoTemp = correos;
            this.idRespuestaTemp = idRespuesta;
            $("#modalSolicitarInformacionProcesos").modal('show');
        },

        SolicitarInformacionProcesos: function () {
            this.SubirArchivosSolicitudInformacionProcesos();
        },

        SubirArchivosSolicitudInformacionProcesos: function () {
            const formData = new FormData();

            for (var i = 0; i < this.$refs.addFilesSolicitarInformacionProcesos.files.length; i++) {
                let file = this.$refs.addFilesSolicitarInformacionProcesos.files[i];
                formData.append(file.name, file);
            }
            formData.append("IdPQRS", this.idPQRS);
            formData.append("Tipo", "SolicitudInformacionProcesos");

            this.mostrarLoad();
            fetch('UploadFilesPQRSResumen.ashx', {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                    //'Content-Type': 'multipart/form-data'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    if (this.$refs.addFilesSolicitarInformacionProcesos.files.length > 0) {
                        this.$refs.addFilesSolicitarInformacionProcesos.value = null;
                    }
                    this.EnviarCorreosSolicitudesInformacion();
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen - Solicitud Informacion Archivos");
            });
        },

        EnviarCorreosSolicitudesInformacion: function () {
            this.mostrarLoad();
            var param = {
                correos: this.correosPQRSProcesoTemp,
                mensajeAdd: this.$refs.mensajeAdicionalSolicitarInformacionProcesos.value,
                idPQRS: parseInt(this.idPQRS), proceso: this.nombrePQRSProcesoTemp,
                nombreCliente: this.infoPQRSClienteNombre, idRespuesta: this.idRespuestaTemp,
                idProceso: this.idPQRSProcesoTemp
            };
            fetch('FormPQRSResumen.aspx/ProcesarCorreosSolicitudInformacionProcesos', {
                method: 'POST',
                body: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    this.$refs.mensajeAdicionalSolicitarInformacionProcesos.value = "";
                    this.$refs.CerrarModalSolicitarInformacionProcesos.click();
                    toastr.success("Correos enviados", "PQRS Resumen - Solicitud Informacion Procesos");
                    this.pqrsSiguienteEstadoID = 2;
                    this.ActualizarEstadoPQRS();
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen - Solicitud Informacion Procesos");
            });
        },

        ValidarAdicionarRespuestaProceso: function (param) {
            if (param.idProceso == 0 || param.respuesta == "" || typeof (param.respuesta) == "undefined") {
                return false;
            } else {
                return true;
            }
        },

        AdicionarRespuestaProceso: function () {
            var param = {
                idPQRS: parseInt(this.idPQRS),
                idProceso: parseInt(this.adicionRespuestaProceso.proceso),
                respuesta: this.adicionRespuestaProceso.respuesta
            };
            if (!this.ValidarAdicionarRespuestaProceso(param)) {
                toastr.error("Por favor, que los campos esten correctamente ingresados", "PQRS Resumen");
                return false;
            }
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/AdicionarRespuestaProceso', {
                method: 'POST',
                body: JSON.stringify(param),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    if (!res.ok) {
                        this.ocultarLoad();
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }
                    return res.json();
                })
                .then(json => {
                    this.ocultarLoad();
                    var data = JSON.parse(json.d);
                    if (this.$refs.addFilesRespuestaProceso.files.length > 0) {
                        this.SubirArchivosRespuestaProceso(data[0].affectedRows);
                    }
                    this.adicionRespuestaProceso.proceso = 0;
                    this.adicionRespuestaProceso.respuesta = "";
                    this.$refs.CerrarModalAdicionarRespuestasProcesos.click();
                    this.CargarIdPQRS();
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    if (err.json) {
                        return err.json.then(json => {
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        SubirArchivosRespuestaProceso: function (idRespuesta) {
            const formData = new FormData();

            for (var i = 0; i < this.$refs.addFilesRespuestaProceso.files.length; i++) {
                let file = this.$refs.addFilesRespuestaProceso.files[i];
                formData.append(file.name, file);
            }
            formData.append("IdPQRS", this.idPQRS);
            formData.append("Tipo", "RespuestaProcesos");
            formData.append("IdPQRSRespuesta", parseInt(idRespuesta));

            this.mostrarLoad();
            fetch('UploadFilesPQRSResumen.ashx', {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                    //'Content-Type': 'multipart/form-data'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    this.$refs.addFilesRespuestaProceso.value = null;
                    toastr.success("Archivos subidos correctamente", "PQRS Resumen - Respuesta Procesos");
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
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
                    this.CargarIdPQRS();
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

        MostrarEnviarComunicadoCliente: function () {
            this.enviarComunicado.Asunto = "FORSA__Seguimiento a PQRS # " + this.idPQRS + "  / Acompanhamento SAC # " + this.idPQRS + " /  Follow up to PQRS # " + this.idPQRS;
            this.enviarComunicado.para = this.infoPQRSEmailRespuesta
            var modal = $("#ModalEnviarComunicado");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        validarEnviarMensajeCliente: function (enviarComunicado) {
            this.enviarComunicado = enviarComunicado;
            //this.enviarComunicado.asunto = "Respuesta Forsa a Solicitud #" + this.idPQRS;
            var validoCorre = true;
            var dtArray = [];

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

            dtArray = []
            if (this.enviarComunicado.Cc != "" && this.enviarComunicado.Cc != undefined) {
                dtArray = this.enviarComunicado.Cc.split(";");
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
            this.enviarComunicado.Id = this.idPQRS;
            var tempArr = [];

            var archivosRadicadoComunicadoTemp = JSON.parse(JSON.stringify(this.archivosRadicadoComunicado));
            $.each(archivosRadicadoComunicadoTemp, function (index, element) {
                tempArr.push({ Id: parseInt(index), Enviar: element });
            });
            this.enviarComunicado.archivosRadicadoEnviar = tempArr;

            tempArr = [];
            var archivosComprobanteComunicadoTemp = JSON.parse(JSON.stringify(this.archivosComprobanteComunicado));
            $.each(archivosComprobanteComunicadoTemp, function (index, element) {
                tempArr.push({ Id: parseInt(index), Enviar: element });
            });
            this.enviarComunicado.archivosComprobanteEnviar = tempArr;

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
                    this.GuardarArchivosComunicadoCliente();
                    this.enviarComunicado = {};
                    this.CargarIdPQRS();
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

        ObtenerPlantas: function () {
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
        ObtenerOrdenesActivas: function () {
            var IdPqrs = JSON.stringify({ IdPqrs: this.idPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/ObtenerOrdenesActivas', {
                method: 'POST',
                body: IdPqrs,
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
                    this.numordenes = JSON.parse(json.d);
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

        GuardarArchivosComunicadoCliente: function () {
            const formData = new FormData();

            for (var i = 0; i < this.$refs.fileComunicado.files.length; i++) {
                let file = this.$refs.fileComunicado.files[i];
                formData.append(file.name, file);
            }
            formData.append("IdPQRS", this.idPQRS);
            formData.append("Tipo", "ComunicadoCliente");

            this.mostrarLoad();
            fetch('UploadFilesPQRSResumen.ashx', {
                method: 'POST',
                body: formData,
                headers: {
                    'Accept': 'application/json',
                    //'Content-Type': 'multipart/form-data'
                }
            }).then(res => {
                this.ocultarLoad();
                // a non-200 response code
                if (!res.ok) {
                    // create error instance with HTTP status text
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                } else {
                    if (this.$refs.fileComunicado.files.length > 0) {
                        this.$refs.fileComunicado.value = null;
                        toastr.success("Archivos subidos correctamente", "PQRS Resumen - Comunicado Cliente");
                        this.CargarIdPQRS();
                        this.$refs.CerrarModalAdicionarArchivosCierre.click();
                    } else {
                        toastr.info("No hay archivos para subir", "PQRS Resumen - Comunicado Cliente");
                    }
                }
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "PQRS Resumen");
            });
        },

        PreguntavalidarEnviarMensajeCliente: function (enviarComunicado) {
            this.enviarComunicado = enviarComunicado;
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
                            parentWindow.enviarMensajeCliente();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        onChangeComunicado() {

            this.files = [...this.$refs.fileComunicado.files];
        },

        dragover(e) {
            e.preventDefault();
            this.isDragging = true;
        },

        dragleave() {
            this.isDragging = false;
        },

        remove(i) {
            this.files.splice(i, 1);
        },

        dropComunicado(e) {
            e.preventDefault();
            this.$refs.fileProd.files = e.dataTransfer.files;
            this.onChangeProd();
            this.isDragging = false;
        },

        getFiles: function (files) {
            return Promise.all(files.map(file => this.getFile(file)));
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

        ReclamoProcedente: function () {
            // this.pqrsProcedente = [];
            this.procesos = [];
            this.procesosToAdd = [];
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
                    this.pqrsProcedente.EsProcedente = 'null';
                    this.pqrsProcedente.existeOrden = 'false';
                    this.pqrsProcedente.requierelistados = 'false';
                    this.pqrsProcedente.RequierelistadosAcero = false;
                    this.pqrsProcedente.RequierelistadosAluminio = false;
                    this.pqrsProcedente.requiereplanos = 'false';
                    this.pqrsProcedente.RequiereplanosAluminio = false;
                    this.pqrsProcedente.RequiereplanosAcero = false;
                    this.pqrsProcedente.requierearmado = 'false';
                    this.pqrsProcedente.RequierearmadoAluminio = false;
                    this.pqrsProcedente.RequierearmadoAcero = false;
                    this.pqrsProcedente.IdPQRS = this.idPQRS;
                    this.ObtenerPlantas();
                    this.ObtenerOrdenesActivas();
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

        GenerarOrdenMostrarModal: function () {
            this.pqrsGenerarOrden.solucionadoEnObra = 'false';
            this.pqrsGenerarOrden.EsProcedente = 'false';
            this.pqrsGenerarOrden.existeOrden = 'false';
            this.pqrsGenerarOrden.requierelistados = 'false';
            this.pqrsGenerarOrden.RequierelistadosAcero = false;
            this.pqrsGenerarOrden.RequierelistadosAluminio = false;
            this.pqrsGenerarOrden.requiereplanos = 'false';
            this.pqrsGenerarOrden.RequiereplanosAluminio = false;
            this.pqrsGenerarOrden.RequiereplanosAcero = false;
            this.pqrsGenerarOrden.requierearmado = 'false';
            this.pqrsGenerarOrden.RequierearmadoAluminio = false;
            this.pqrsGenerarOrden.RequierearmadoAcero = false;
            this.pqrsGenerarOrden.IdPQRS = this.idPQRS;
            this.ObtenerPlantas();
            this.ObtenerOrdenesActivas();
            var modal = $("#ModalGenerarOrden")
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
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

        ValidarGenerarOrden: function () {
            var validoCorre = true, validoProcesos = true, validoOrden = true;
            if (this.pqrsGenerarOrden.solucionadoEnObra == 'false') {
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
                    if (this.pqrsGenerarOrden.RequierelistadosAcero == true) {
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
                    if (this.pqrsGenerarOrden.RequierelistadosAluminio == true) {
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

                    if (this.pqrsGenerarOrden.RequiereplanosAcero == true) {
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
                    if (this.pqrsGenerarOrden.RequiereplanosAluminio == true) {
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

                if (this.pqrsGenerarOrden.requierearmado == 'true' || this.pqrsGenerarOrden.requierearmado == true) {

                    if (this.pqrsGenerarOrden.RequierearmadoAcero == true) {
                        requiereAlguno = true;
                        if ((this.pqrsGenerarOrden.RequierearmadoAceroCorreos == undefined || this.pqrsGenerarOrden.RequierearmadoAceroCorreos == null) || (this.pqrsGenerarOrden.RequierearmadoAceroCorreos != undefined && this.pqrsGenerarOrden.RequierearmadoAceroCorreos != null && this.pqrsGenerarOrden.RequierearmadoAceroCorreos.length == 0)) {
                            toastr.error("Debe agregar al menos un correo para notificar en los planos de acero", {
                                "timeOut": "0",
                                "extendedTImeout": "0"
                            });
                            validoCorre = false;
                            return false;
                        } else {
                            var dtArray = [];
                            dtArray = this.pqrsGenerarOrden.RequierearmadoAceroCorreos.split(";");
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
                    if (this.pqrsGenerarOrden.RequierearmadoAluminio == true) {
                        requiereAlguno = true;
                        if ((this.pqrsGenerarOrden.RequierearmadoAluminioCorreos == undefined || this.pqrsGenerarOrden.RequierearmadoAluminioCorreos == null) || (this.pqrsGenerarOrden.RequierearmadoAluminioCorreos != undefined && this.pqrsGenerarOrden.RequierearmadoAluminioCorreos != null && this.pqrsGenerarOrden.RequierearmadoAluminioCorreos.length == 0)) {
                            toastr.error("Debe agregar al menos un correo para notificar en los planos de armado de aluminio", {
                                "timeOut": "0",
                                "extendedTImeout": "0"
                            });
                            validoCorre = false;
                            return false;
                        } else {
                            var dtArray = [];
                            dtArray = this.pqrsGenerarOrden.RequierearmadoAluminioCorreos.split(";");
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
                    if ((this.pqrsGenerarOrden.RequiereArmadoDescripcion == undefined || this.pqrsGenerarOrden.RequiereArmadoDescripcion == null || this.pqrsGenerarOrden.RequiereArmadoDescripcion == "")
                        || (this.pqrsGenerarOrden.RequiereArmadoDescripcion != undefined && this.pqrsGenerarOrden.RequiereArmadoDescripcion != null && this.pqrsGenerarOrden.RequiereArmadoDescripcion.length == 0)) {
                        toastr.error("Debe agregar una descripcion si no requiere plano de armado ", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                        validoCorre = false;
                        return false;
                    }
                }

                if (this.pqrsGenerarOrden.requierelistados == 'true' || this.pqrsGenerarOrden.requierelistados == true
                    || this.pqrsGenerarOrden.requiereplanos == 'true' || this.pqrsGenerarOrden.requiereplanos == true
                    || this.pqrsGenerarOrden.requierearmado == 'true' || this.pqrsGenerarOrden.requierearmado == true) {
                    if (!requiereAlguno) {
                        toastr.error("Debe requerir al menos alguna categoria de planos, listados o armado", {
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

        PreguntarGenerarOrden: function () {
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

        GenerarOrden: function () {
            if (!this.pqrsGenerarOrden.RequiereplanosAcero) { this.pqrsGenerarOrden.RequiereplanosAceroCorreos = '' };
            if (!this.pqrsGenerarOrden.RequiereplanosAluminio) { this.pqrsGenerarOrden.RequiereplanosAluminioCorreos = '' };
            if (!this.pqrsGenerarOrden.RequierelistadosAcero) { this.pqrsGenerarOrden.RequierelistadosAceroCorreos = '' };
            if (!this.pqrsGenerarOrden.RequierelistadosAluminio) { this.pqrsGenerarOrden.RequierelistadosAluminioCorreos = '' };
            if (!this.pqrsGenerarOrden.RequierearmadoAcero) { this.pqrsGenerarOrden.RequierearmadoAceroCorreos = '' };
            if (!this.pqrsGenerarOrden.RequierearmadoAluminio) { this.pqrsGenerarOrden.RequierearmadoAluminioCorreos = '' };
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
                    var modal = $("#ModalGenerarOrden")
                    modal.modal('hide');
                    modal.modal({ backdrop: 'static', keyboard: false }, 'hide');
                    this.pqrsGenerarOrden = {};
                    this.CargarIdPQRS();
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

        MostrarAdicionarProcesosProcedencia: function() {
            this.procesosToAdd = [];
            this.procesos = [];
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
            $("#ModalAdicionarProcesos").modal('show');
        },

        ValidarAdicionarProcesosProcedencia: function() {
            if (this.procesosToAdd.length > 0) {
                this.procesosToAdd.forEach(element => {
                    if (element.Comentario == null || element.Comentario.length == 0) {
                        toastr.warning('Todos los procesos deben tener un comentario');
                        return false;
                    }
                });
            }
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion3' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de adicionar estos procesos?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion3").click(function () {
                            parentWindow.FetchAdicionarProcesosProcedencia();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        FetchAdicionarProcesosProcedencia: function() {
            var params = JSON.stringify({
                idPqrs: this.idPQRS,
                Procesos: this.procesosToAdd
            });
            fetch('FormPQRSConsulta.aspx/AdicionarProcesos', {
                method: 'POST',
                body: params,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    if (!res.ok) {
                        this.ocultarLoad();
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    this.ocultarLoad();
                    this.procesosToAdd = [];
                    var modal = $("#ModalAdicionarProcesos")
                    modal.modal('hide');
                    this.CargarIdPQRS();
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

        ValidarPQRSProcedencia: function () {
            var validoCorre = true, validoProcesos = true, validoOrden = true;

            this.pqrsProcedente.procesos = this.procesosToAdd;

            if (this.pqrsProcedente.EsProcedente == 'true' || this.pqrsProcedente.EsProcedente == true) {

                validoProcesos = true;
                if (this.procesosToAdd.length > 0) {
                    this.procesosToAdd.forEach(element => {
                        if (element.Comentario == null || element.Comentario.length == 0) {
                            validoProcesos = false;
                            toastr.warning('Todos los procesos deben tener un comentario');
                        }
                    });
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

        mostrarModalEditarNC: function(noConformidad) {
            this.noConformidadEditNC = JSON.parse(JSON.stringify(noConformidad));
            this.procesosToAdd = [];
            this.procesos = [];
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

            $("#editNoConformidad").modal('show');
        },

        PreguntarValidarNoConformidadEditNC: function() {
            var emailRegex = /[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}/;
            var containsEmail = emailRegex.test(this.noConformidadEditNC.Email);
            if(!containsEmail) {
                toastr.error("Debes especificar al menos 1 Email valido", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion3' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de modificar esta no conformidad?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion3").click(function () {
                            parentWindow.FetchEditarNC();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        FetchEditarNC: function () {
            var params = JSON.stringify({
                noConformidad: this.noConformidadEditNC
            });
            fetch('FormPQRSResumen.aspx/ActualizarNoConformidad', {
                method: 'POST',
                body: params,
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
                    var modal = $("#editNoConformidad")
                    modal.modal('hide');
                    let itemToUpdate = this.noConformidades.find(x => x.PQRSNoConformidadesID == this.noConformidadEditNC.PQRSNoConformidadesID);
                    itemToUpdate.Email = this.noConformidadEditNC.Email;
                    itemToUpdate.Comentario = this.noConformidadEditNC.Comentario;
                    itemToUpdate.Proceso = this.noConformidadEditNC.Proceso;
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

        GuardarPQRSProcedencia: function () {
            let stringHallazgosNoProcedentes = "";
            for (let key in this.hallazgosNoProcedentes) {
                if (this.hallazgosNoProcedentes.hasOwnProperty(key)) {
                    const value = this.hallazgosNoProcedentes[key];
                    if (value == true) {
                        stringHallazgosNoProcedentes += "," + key;
                    }
                }
            }
            stringHallazgosNoProcedentes = stringHallazgosNoProcedentes.substring(1);
            var orden = JSON.stringify({
                pqrs: this.pqrsProcedente,
                hallazgosNoProcedentes: stringHallazgosNoProcedentes
            });
            debugger;
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
                    this.CargarIdPQRS();
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

        MostrarListadoRequerido: function (mostrarModal) {
            this.files = [];
            this.listadosRequeridos = {};
            this.listadosRequeridos.IdPQRS = this.idPQRS;
            this.listadosRequeridos.TipoCargueArchivos = "-1";
            this.listadosRequeridos.TablaCargueArchivos = "-1";

            var orden = JSON.stringify({ idpqrs: this.idPQRS });
            this.mostrarLoad();

            fetch('FormPQRSConsulta.aspx/ObtenerListadosPlanos', {
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
                    var data = JSON.parse(json.d)
                    this.listadosRequeridos.listadosPlanos = data.datos;
                    this.debeCargarListadosOPlanosOArmado = data.debeCargar;
                    if (mostrarModal) {
                        var modal = $("#ModalAgregarListadosRequerido");
                        modal.modal({ backdrop: 'static', keyboard: false }, 'show');
                    }
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

        MostrarComprobanteObra: function () {
            this.files = [];
            this.pqrscomobra = { IdPQRS: this.idPQRS };
            var modal = $("#ModalComprobanteObra");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        CerrarComprobanteObra: function () {
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
                    this.CargarIdPQRS();
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

        ValidarGuardarListadosRequeridos: function () {

            if (this.files.length > 5) {
                toastr.error("No elija mas de 5 archivos", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.files.length > 0) {
                if (this.listadosRequeridos.TipoCargueArchivos == -1 || this.listadosRequeridos.TipoCargueArchivos == "-1") {
                    toastr.error("Elija el tipo de archivo que va a subir acero o aluminio", "PQRS", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }

                if (this.listadosRequeridos.TablaCargueArchivos == -1 || this.listadosRequeridos.TablaCargueArchivos == "-1") {
                    toastr.error("Elija el tipo de archivo que va a subir planos o listados", "PQRS", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }
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
            this.listadosRequeridos.TablaCargueArchivos = parseInt(this.listadosRequeridos.TablaCargueArchivos);
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
                    this.CargarIdPQRS();

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

        dropListados(e) {
            e.preventDefault();
            this.$refs.fileInputListados.files = e.dataTransfer.files;
            this.onChangeListados();
            this.isDragging = false;
        },

        onChangeListados() {

            this.files = [...this.$refs.fileInputListados.files];
        },

        ShowActionPlanDetails: function (noConformidad) {
            this.noConformidadDetails = noConformidad;
            $("#modalActionPlanDetails").modal('show');
        },

        MostrarModalCrearPlanAccionQuejas: function () {
            let noConformidad = {
                Proceso: "",
                DescripcionNoConformidad: "",
                Email: "",
                Comentario: "",
                PlanAccion: "",
                PlanAccionDescripcion: "",
                IdClasificacion: "-1",
                Clasificacion: "",
                UsuarioResponsable: "",
                IdFamiliaGarantia: "-1",
                FamiliaGarantia: ""
            };
            if (noConformidad.PlanAccionFecha != null) {
                noConformidad.PlanAccionFecha = noConformidad.PlanAccionFecha.split("T")[0];
            }
            this.familiasGarantias.forEach(element => {
                if (element.Id == noConformidad.IdFamiliaGarantia) {
                    this.currentFamiliaGarantia = element;
                    return false;
                }
            });
            if (noConformidad.IdFamiliaGarantia != "-1") {
                this.currentFamiliaGarantia.Productos.forEach(element => { element.Selected = false });
                this.textoOtroProductosGarantia = "";
                this.previousFamiliaGarantiaId = noConformidad.IdFamiliaGarantia;
                noConformidad.ProductosGarantias.forEach(element => {
                    this.currentFamiliaGarantia.Productos.forEach(producto => {
                        if (element.Nombre == producto.Nombre) {
                            if (element.Nombre == 'Otro') {
                                this.textoOtroProductosGarantia = element.TextoOpcional;
                            }
                            producto.Selected = true;
                        }
                    })
                });
            }
            this.noConformidadEdit = noConformidad;
            var modal = $("#ModalCrearPlanAccionQuejas");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        isEmptyOrSpaces: function(str){
            return  str == undefined || str === null || str.match(/^ *$/) !== null;
        },

        validarCrearPlanAccionQueja: function () {
            if (this.noConformidadEdit.PlanAccionFecha == undefined || this.noConformidadEdit.PlanAccionFecha == null) {
                toastr.error("Debe agregar una fecha ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            } else {
                var date = new Date(this.noConformidadEdit.PlanAccionFecha);

                if (date.toString() === 'Invalid Date' && this.noConformidadEdit.PlanAccionFecha !== date.toISOString().slice(0, 10)) {
                    toastr.error("Debe agregar una fecha valida ", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }
            }

            if (this.noConformidadEdit.IdFamiliaGarantia == undefined || this.noConformidadEdit.IdFamiliaGarantia == null || this.noConformidadEdit.IdFamiliaGarantia == -1) {
                toastr.error("Debe asignar una familia de garantía", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.noConformidadEdit.IdClasificacion == undefined || this.noConformidadEdit.IdClasificacion == null || this.noConformidadEdit.IdClasificacion == -1) {
                toastr.error("Debe asignar una clasificación", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.isEmptyOrSpaces(this.noConformidadEdit.UsuarioResponsable)) {
                toastr.error("Debe agregar un usuario responsable ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.isEmptyOrSpaces(this.noConformidadEdit.PlanAccion)) {
                toastr.error("Debe agregar un plan de acción ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.isEmptyOrSpaces(this.noConformidadEdit.Comentario)) {
                toastr.error("Debe agregar un comentario ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.isEmptyOrSpaces(this.noConformidadEdit.PlanAccionDescripcion)) {
                toastr.error("Debe agregar una descripción para el plan de acción ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            const emailRegex = new RegExp(/^[A-Za-z0-9_!#$%&'*+\/=?`{|}~^.-]+@[A-Za-z0-9.-]+$/, "gm");
            this.noConformidadEdit.Email = this.noConformidadEdit.Email.trim();
            if (!emailRegex.test(this.noConformidadEdit.Email)) {
                toastr.error("Debe agregar un correo válido", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            this.confirmarCrearPlanAccionQueja();
        },

        confirmarCrearPlanAccionQueja: function () {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion3' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de crear el plan de acción para la Queja?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion3").click(function () {
                            parentWindow.crearPlanAccionQueja();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        crearPlanAccionQueja: function () {
            var productosGarantia = "";
            this.currentFamiliaGarantia.Productos.forEach(element => {
                if (element.Selected) {
                    productosGarantia += element.Id + ",";
                }
            });
            var data = {
                noConformidadEdit: JSON.parse(JSON.stringify(this.noConformidadEdit)),
                productosGarantia: productosGarantia.slice(0, -1),
                textoOtro: this.textoOtroProductosGarantia,
                pqrsId: parseInt(this.idPQRS)
            }
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/CrearPlanAccionQueja', {
                method: 'POST',
                body: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    if (!res.ok) {
                        this.ocultarLoad();
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    this.ocultarLoad();
                    var modal = $("#ModalCrearPlanAccionQuejas");
                    modal.modal('hide');
                    this.noConformidadEdit = {};
                    this.textoOtroProductosGarantia = "";
                    this.ObtenerFamiliasGarantias();
                    this.CargarIdPQRS();
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    if (err.json) {
                        return err.json.then(json => {
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        MostrarCierreObra: function (noConformidad) {
            if (noConformidad.PlanAccionFecha != null) {
                noConformidad.PlanAccionFecha = noConformidad.PlanAccionFecha.split("T")[0];
            }
            this.familiasGarantias.forEach(element => {
                if (element.Id == noConformidad.IdFamiliaGarantia) {
                    this.currentFamiliaGarantia = element;
                    return false;
                }
            });
            if(noConformidad.IdFamiliaGarantia != "-1") {
                this.currentFamiliaGarantia.Productos.forEach(element => { element.Selected = false });
                this.textoOtroProductosGarantia = "";
                this.previousFamiliaGarantiaId = noConformidad.IdFamiliaGarantia;
                noConformidad.ProductosGarantias.forEach(element => {
                    this.currentFamiliaGarantia.Productos.forEach(producto => {
                        if (element.Nombre == producto.Nombre) {
                            if (element.Nombre == 'Otro') {
                                this.textoOtroProductosGarantia = element.TextoOpcional;
                            }
                            producto.Selected = true;
                        }
                    })
                });
            }
            this.noConformidadEdit = noConformidad;
            var modal = $("#ModalCierreObra");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        validarEnviarCierreReclamacion: function () {
            this.cierre = this.noConformidadEdit;
            var date = new Date(this.cierre.PlanAccionFecha);

            if (this.cierre.IdFamiliaGarantia == undefined || this.cierre.IdFamiliaGarantia == null || this.cierre.IdFamiliaGarantia == -1) {
                toastr.error("Debe asignar una familia de garantía", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.cierre.IdClasificacion == undefined || this.cierre.IdClasificacion == null || this.cierre.IdClasificacion == -1) {
                toastr.error("Debe asignar una clasificación", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (this.cierre.UsuarioResponsable == undefined || this.cierre.UsuarioResponsable == null || this.cierre.UsuarioResponsable.length == 0) {
                toastr.error("Debe agregar un usuario responsable ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if (date.toString() === 'Invalid Date' && this.cierre.PlanAccionFecha !== date.toISOString().slice(0, 10)) {
                toastr.error("Debe agregar una fecha valida ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.cierre.PlanAccion == undefined || this.cierre.PlanAccion == null) || (this.cierre.PlanAccion != undefined && this.cierre.PlanAccion != null && this.cierre.PlanAccion.length == 0)) {
                toastr.error("Debe agregar un plan de acción ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }

            if ((this.cierre.PlanAccionDescripcion == undefined || this.cierre.PlanAccionDescripcion == null) || (this.cierre.PlanAccionDescripcion != undefined && this.cierre.PlanAccionDescripcion != null && this.cierre.PlanAccionDescripcion.length == 0)) {
                toastr.error("Debe agregar una descripción para el plan de acción ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }
            this.PreguntavaenviarCierreReclamacion();

        },

        enviarCierreReclamacion: function () {
            var productosGarantia = "";
            this.currentFamiliaGarantia.Productos.forEach(element => {
                if (element.Selected) {
                    productosGarantia += element.Id + ",";
                }
            });
            var data = {
                noConformidadEdit: JSON.parse(JSON.stringify(this.noConformidadEdit)),
                productosGarantia: productosGarantia.slice(0, -1),
                textoOtro: this.textoOtroProductosGarantia
            }
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/GuardarCierreReclamacion', {
                method: 'POST',
                body: JSON.stringify(data),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    if (!res.ok) {
                        this.ocultarLoad();
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }

                    return res.json();
                })
                .then(json => {
                    this.ocultarLoad();
                    var modal = $("#ModalCierreObra");
                    modal.modal('hide');
                    this.noConformidadEdit = {};
                    this.textoOtroProductosGarantia = "";
                    this.ObtenerFamiliasGarantias();
                    this.CargarIdPQRS();
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    if (err.json) {
                        return err.json.then(json => {
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        MostrarProduccion: function () {
            this.files = [];
            this.pqrsprod = { IdPQRS: this.idPQRS };
            var modal = $("#ModalProduccion");
            modal.modal({ backdrop: 'static', keyboard: false }, 'show');
        },

        cerrarProduccion: function () {

            /*if (this.pqrsprod.fecha_plan_alum == undefined || this.pqrsprod.fecha_plan_alum == null) {
                toastr.error("Debe seleccionar la fecha planeada de entrega para producción de aluminio ", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });

                return false;
            }*/

            /*if (this.pqrsprod.fecha_plan_acero == undefined || this.pqrsprod.fecha_plan_acero == null) {
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
                    this.CargarIdPQRS();
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

        PreguntavaenviarCierreReclamacion: function (cierre) {
            this.cierre = cierre;
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion3' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de modificar el Plan de Acción?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion3").click(function () {
                            parentWindow.enviarCierreReclamacion();
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
                'Eestá seguro de haber cargado todos los archivos necesarios?',
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

        dropProd(e) {
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

        onChangeProd() {

            this.files = [...this.$refs.fileProd.files];
        },

        onChangeImpl() {

            this.files = [...this.$refs.fileInputImpl.files];
        },

        onChangeFamiliaGarantia() {
            this.familiasGarantias.forEach(element => {
                if (element.Id == this.noConformidadEdit.IdFamiliaGarantia) {
                    this.currentFamiliaGarantia = element;
                    return false;
                }
            });
            this.currentFamiliaGarantia.Productos.forEach(element => { element.Selected = false });
            this.textoOtroProductosGarantia = "";
            if (this.noConformidadEdit.IdFamiliaGarantia != this.previousFamiliaGarantiaId) {
                return false;
            }
            this.noConformidadEdit.ProductosGarantias.forEach(element => {
                this.currentFamiliaGarantia.Productos.forEach(producto => {
                    if (element.Nombre == producto.Nombre) {
                        if (element.Nombre == 'Otro') {
                            this.textoOtroProductosGarantia = element.TextoOpcional;
                        }
                        producto.Selected = true;
                    }
                })
            });
        },

        BorrarArchivoFisico: function (nameFile, namedir, idArchivo, idTipoArchivo) {
            var param = {
                PQRSId: this.idPQRS,
                nameFile: nameFile,
                namedir: namedir,
                idArchivo: idArchivo,
                idTipoArchivo: idTipoArchivo
            };
            fetch('FormPQRSResumen.aspx/EliminarArchivo', {
                method: 'POST',
                body: JSON.stringify(param),
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
                    toastr.success("Archivo eliminado corretamente", "PQRS Resumen");
                    this.CargarIdPQRS();
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    // In case a custom JSON error response was provided
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                            toastr.error(this.error.value, "PQRS Resumen");
                        });
                    }
                });
        },

        PreguntaBorrarArchivoFisico: function(nameFile, namedir, idArchivo, idTipoArchivo) {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarBorrarArchivo' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de eliminar este archivo?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarBorrarArchivo").click(function () {
                            parentWindow.BorrarArchivoFisico(nameFile, namedir, idArchivo, idTipoArchivo);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        ValidarEditarFechasProduccion: function() {
            /*if(this.$refs.editFechaPlanAcero) {
                toastr.error("No elija mas de 5 archivos", "PQRS", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });
                return false;
            }*/
            this.PreguntarEditarFechasProduccion();
        },

        PreguntarAvanzarEstadoListadosCompletos: function() {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarBorrarArchivo' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de cambiar el estado a Listados Completos?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarBorrarArchivo").click(function () {
                            parentWindow.ListadosCompletos();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        ListadosCompletos: function() {
            var orden = JSON.stringify({ idpqrs: this.idPQRS });
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
                    this.CargarIdPQRS();
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

        PreguntarAvanzarEstadoProduccion: function() {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarBorrarArchivo' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de cambiar el estado a Producción?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarBorrarArchivo").click(function () {
                            parentWindow.Produccion();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        Produccion: function() {
            var orden = JSON.stringify({ idpqrs: this.idPQRS });
            this.mostrarLoad();
            fetch('FormPQRSConsulta.aspx/Produccion', {
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
                    this.CargarIdPQRS();
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

        PreguntarEditarFechasProduccion: function() {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarBorrarArchivo' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de editar las fechas de Producción?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarBorrarArchivo").click(function () {
                            parentWindow.EditarFechasProduccion();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        EditarFechasProduccion: function() {
            var param = {
                planAlum: this.$refs.editFechaPlanAlum.value,
                idPQRS: parseInt(this.idPQRS)
            };
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ActualizarFechasPlanProduccion', {
                method: 'POST',
                body: JSON.stringify(param),
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
                    var modal = $("#ModalEditarProduccion")
                    modal.modal('hide');
                    this.$refs.editFechaPlanAlum.value = "";
                    this.$refs.editFechaPlanAcero.value = "";
                    this.CargarIdPQRS();
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

        redirectCrear: function() {
            window.location.href = "FormPQRS.aspx";
        },

        redirectConsultar: function() {
            window.location.href = "FormPQRSConsulta.aspx";
        },

        ExisteOrdenChange:function() {
            this.pqrsProcedente.OrdenGarantiaOMejora = null;
        },

        noExisteOrdenChange:function() {
            this.pqrsProcedente.Idordenprocedente = null;
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
                    //this.listapaises = JSON.parse(json.d);
                    var data = JSON.parse(json.d);
                    $("#cmbPaises").html("");
                    data.forEach(element => {
                        $("#cmbPaises").append($("<option />").val(element.Id).text(element.Nombre));
                    });
                    $("#cmbPaises").selectpicker('refresh');
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
            var orden = JSON.stringify({ idPais: this.infoPQRSIdPais });
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
                    this.infoPQRSIdCiudad = this.tempInfoPQRSIdCiudad;
                    this.$nextTick(() => $('#cmbCiudad').selectpicker('render'));
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

        MostrarEditResumenDataModal : function() {
            var modal = $("#editResumenDataModal");
            modal.modal('show');
            this.$nextTick(() => $('#cmbPaises').selectpicker('render'));
            this.onChangePais();
            this.onChangeCiudad();
        },

        AgregarControlCambios : function () {
            var anx = 0;
            var cons = this.cantidadComentario + 1;
            var flag = 0;
            var valido = 1;
            var mensaje;

            if ($('#txtTituloObs').val() == "") {
                valido = 0;
                mensaje = "Debe diligenciar Titulo";
            }

            var eventoId = $("#cmbBitacoraEvento").val();
            if(eventoId == -1 || eventoId == "-1" || eventoId == undefined || isNaN(eventoId)) {
                valido = 0;
                mensaje = "Debe seleccionar evento";
            }

            if (valido == 1) {
                this.mostrarLoad();
                var fileUpload = $("#rutaArchivo2").get(0);
                var files = fileUpload.files;
                var fdata = new FormData();
                var tipoAnex = 40;

                for (var i = 0; i < files.length; i++) {
                    anx = 1;
                    fdata.append(files[i].name, files[i]);
                }

                fdata.append('idfup', this.idPQRS);
                fdata.append('tipo', tipoAnex);
                fdata.append('zona', "Control Cambio PQRS");
                fdata.append('EventoPTF', cons);
                fdata.append('version', '');

                var objg = {
                    IdPQRS: this.idPQRS,
                    cons: parseInt(cons),
                    padre: parseInt($('#padreCambio').val()),
                    Comentario: $('#txtObsCntrCm').val(),
                    Estado: this.infoPQRSEstadoID,
                    Titulo: $('#txtTituloObs').val(),
                    EventoId: parseInt(eventoId)
                };
                var param = {
                    Item: objg
                };
                $.ajax({
                    type: "POST",
                    url: "FormPQRSResumen.aspx/GuardarControlCambio",
                    data: JSON.stringify(param),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    // async: false,
                    success: function (msg) {
                        toastr.success("Guardado Correctamente", "PQRS Control Cambios Cierre");
                        this.cantidadComentario = this.cantidadComentario + 1;
                        if (anx > 0) {
                            $.ajax({
                                url: "UploadHandler.ashx",
                                type: "POST",
                                contentType: false,
                                processData: false,
                                data: fdata,
                                // dataType: "json",
                                success: function (result) {
                                    var res = JSON.parse(result);
                                    if (res.conf.id == 1) {
                                        toastr.success(res.conf.descripcion);
                                        //res.lista
                                    }
                                    else {
                                        toastr.error(res.conf.descripcion);
                                    }
                                },
                                error: function (err) {
                                    toastr.error(err.statusText, "PQRS Control Cambios Cierre", {
                                        "timeOut": "0",
                                        "extendedTImeout": "0"
                                    });
                                }
                            });
                        }
                    },
                    error: function () {
                        toastr.error("Failed to Control Cambio", "FUP", {
                            "timeOut": "0",
                            "extendedTImeout": "0"
                        });
                    }
                });
            }
            else {
                toastr.error("Faltan Campos por Diligenciar -  Control Cambio - " + mensaje, "FUP", {
                    "timeOut": "0",
                    "extendedTImeout": "0"
                });

            }
            this.ocultarLoad();
            $("#ModControlCambios").modal('hide');
            $("#txtObsCntrCm").val("");
            $("#txtTituloObs").val("");
            $("#cmbBitacoraEvento").val("-1");
            this.CargarIdPQRS();
        },

        ControlCambioShow :function(title, optionDefault, Respuesta, Titulo ) {
            if (optionDefault) {
                $("#ModControlCambios").val(optionDefault).prop("disabled", true)
            }
            else {
                    $("#ModControlCambios").val(-1).prop("disabled", false)
            }
            $("#padreCambio").val(Respuesta);

            if (Titulo.length > 0) {
                $("#txtTituloObs").val(Titulo)
                $("#txtTituloObs").prop("disabled", true);
            }
            else
                $("#txtTituloObs").prop("disabled", false);

            if (Respuesta == 0)
                $("#AreaControl").hide();
            else {
                $("#AreaControl").val("Comentario Padre : " + Respuesta);
                $("#AreaControl").show();
            }
            $("#txtObsCntrCm").val("");
            var modal = $("#ModControlCambios");
            modal.find('.modal-title').text(title);
            modal.modal('show');
        },

        LlenarControlCambio: function(lista) {
            $("#DinamycChange .Comentario").remove();
            this.cantidadComentario = 0;

            var cardCabecera = '<div class="box-header border-bottom border-primary Comentario" style="z-index: 2;"><table class="col-md-12 table-sm"><thead><tr><th width="12%">FECHA</th><th width="45%">TITULO</th><th width="15%">ESTADO</th><th width="15%">USUARIO</th>' +
                                                                                '<td width="3%"></td></tr></thead></table></div>';
            var cardFoot = '';
            var cardBody = '';
            var idParteDinamica = '';
            var OrdenParte = 0;

            $.each(lista, function (i, r) {
                this.cantidadComentario = this.cantidadComentario + 1;
                if (r.Padre == "0") {

                    if (cardBody != "") {
                        cardBody += '</div></div></div><div class="row item" ></div>';
                    }
                    idParteDinamica = r.Cons;
                    OrdenParte = r.fcp_OrdenParte

                    cardBody +=
                        '<div class="col-md-12 Comentario" style="padding-top: 6x;" id="Parte' + r.Nivel + '"><div id="header' + r.Nivel + '" class="box box-primary"><div class="box-header border-bottom border-primary" style="z-index: 2;">' +
                        '<table class="col-md-12 table-sm"><tr><td width="12%">' + r.Fecha.substring(0, 10) + '</td><td width="45%">' + r.Titulo + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.Usuario + '</td>' +
                        '<td width="3%"><div class="col-md-12" style="padding-bottom: 4px;"><button id="collapse' + r.Nivel + '" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 2px;"><span class="fa fa-angle-double-down"></span></button></div></td></tr></table></div>' +
                        '<div id="body' + r.Nivel + '" class="box-body" style="display: none;padding-top: 8px;margin-left: 10px;margin-right: 10px; ">'

                    cardBody += '<div class="row item" id="' + r.Nivel + '"><label style="font-size: 10px;">Observacion</label><textarea class="form-control col-sm-12 Observacion" rows="2" disabled>' + r.Comentario + '</textarea></div>';
                    if (r.Anexos.length > 0) {
                        var vAnexo = JSON.parse(r.Anexos);
                        var vLineaAnexo = "";
                        jQuery.each(vAnexo, function (j, ane) {
                            vLineaAnexo += '<tr ><td width="10%" >Anexo</td><td width="80%">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                        });
                        cardBody += '<div class="row item" id="' + r.Nivel + '"><table class="col-md-12 table-sm table-bordered">' + vLineaAnexo + '</table></div>';
                    }


                }
                else {
                    if (r.Anexos.length > 0) {
                        var vAnexo = JSON.parse(r.Anexos);
                        var vLineaAnexo = "";
                        jQuery.each(vAnexo, function (j, ane) {
                            vLineaAnexo += '<tr><td width="12%" colspan = "2" class="text-center" >Anexo</td><td colspan = "4">' + ane.nombre + '</td><td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo(\'' + ane.nombre + '\',\'' + ane.ruta + '\')\"> </button></td></tr>'
                        });
                        cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td width="13%">' + r.Usuario + '</td><td width="2%"></tr>' + vLineaAnexo + '</tbody></table></div></div>';
                    }
                    else {
                        cardBody += '<div class="row item" id="' + r.Nivel + '"><div class="col-1"></div> <div class="col-11"><table class="col-md-12 table-sm table-bordered"><tbody><tr><td width="2%"><i class="fa fa-comment"></i></td><td width="10%">' + r.Fecha.substring(0, 10) + '</td><td width="43%">' + r.Comentario + '</td><td width="15%">' + r.Estado + '</td><td width="15%">' + r.EstadoDFT_Desc + '</td><td colspan = "2" width="15%">' + r.Usuario + '</td></tbody></table></div></div>';
                    }
                }
            });
            $("#DinamycChange").append(cardCabecera + cardBody);
        },

        MostrarAgregarRespuestaProceso: function () {
            this.pqrsRespuesta.PQRSId = this.idPQRS;;
            this.pqrsRespuesta.Mensaje = "";
            this.pqrsRespuesta.PQRSIdproceso = "-1";
            this.files = [];
            var orden = JSON.stringify({ idpqrs: this.idPQRS });
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

        VerifyIfAssignedProcessCanBeDeleted: function (idAssignedProcess) {
            let alreadyHaveResponse = false;
            this.respuestasProcesos.forEach(respuestaProcesos => {
                if (respuestaProcesos.IdProceso == idAssignedProcess) {
                    alreadyHaveResponse = true;
                    toastr.error("Este proceso ya tiene respuestas ", "Procesos Asignados", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                    return false;
                }
            });
            if (!alreadyHaveResponse) {
                this.AskDeleteAssignedProcess(idAssignedProcess);
            }
        },

        AskDeleteAssignedProcess: function (idAssignedProcess) {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion9' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                'Está seguro de borrar esta asignación?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion9").click(function () {
                            parentWindow.DeleteAssignedProcess(idAssignedProcess);
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        AskModifyDeliveryDate: function () {
            if (this.FechaEntregaObraEdit == "") {
                toastr.error("Debes asignar una fecha para poder guardar", "Información");
                return;
            }
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.success("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion10' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                'Está seguro de modificar la Fecha de Entrega en Obra?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion10").click(function () {
                            parentWindow.UpdateDeliveryDate();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        UpdateDeliveryDate: function () {
            var data = JSON.stringify({ pqrsId: parseInt(this.idPQRS), deliveryDate: this.FechaEntregaObraEdit });
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/ModifyDeliveryDate', {
                method: 'POST',
                body: data,
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    if (!res.ok) {
                        this.ocultarLoad();
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }
                    return res.json();
                })
                .then(json => {
                    this.EditingFechaEntregaObra = false;
                    this.FechaEntregaObra = this.FechaEntregaObraEdit;
                    this.ocultarLoad();
                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    if (err.json) {
                        return err.json.then(json => {
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        DeleteAssignedProcess: function(idAssignedProcess) {
            var data = JSON.stringify({ idAssignedProcess: idAssignedProcess, pqrsId: parseInt(this.idPQRS) });
            this.mostrarLoad();
            fetch('FormPQRSResumen.aspx/DeleteProcessAssigned', {
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
                    // set the response data
                    this.ocultarLoad();
                    this.CargarIdPQRS();

                })
                .catch(err => {
                    this.ocultarLoad();
                    this.error.value = err;
                    if (err.json) {
                        return err.json.then(json => {
                            // set the JSON response message
                            this.error.value.message = json.message;
                        });
                    }
                });
        },

        GuardarPQRSRespuestaProceso: function (pqrsRespuesta) {
            this.procesosAsignadosPQRS.forEach(element => {
                if(element.PQRSProcesoId == this.pqrsRespuesta.PQRSIdproceso) {
                    this.pqrsRespuesta.Proceso = element.Proceso
                }
            });
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
                    this.pqrsProcesoAclaracionTemp = null;
                    this.CargarIdPQRS();
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

        BuscarAclaracionProceso() {
            this.pqrsProcesoAclaracionTemp = null;
            this.procesosAsignadosPQRS.forEach(element => {
                if(element.PQRSProcesoId == this.pqrsRespuesta.PQRSIdproceso) {
                    this.pqrsProcesoAclaracionTemp = element.InformacionAclaracion;
                    return false;
                }
            }); 
            if(this.pqrsProcesoAclaracionTemp == null) {
                this.procesosAsignadosPQRS.forEach(element => {
                    this.procesosAdmin.forEach(elementAdmin => {
                        if(element.Proceso == elementAdmin.Proceso) {
                            this.pqrsProcesoAclaracionTemp = elementAdmin.Observacion;
                            return false;
                        }
                    });
                });
            }
        },

        AskClosePQRS: function () {
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
                            parentWindow.ClosePQRS();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        ClosePQRS: function () {
            var body = JSON.stringify({ pqrsId: this.idPQRS});
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
                    this.CargarIdPQRS();
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
            var orden = JSON.stringify({ idCiudad: this.infoPQRSIdCiudad });
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
                    if((this.infoPQRSIdCliente == undefined || this.infoPQRSIdCliente == null)
                        && (this.infoFupID == null || this.infoFupID == undefined)) {
                        this.infoPQRSIdCliente = -1;
                        this.infoPQRSIdClienteTemp = -1;
                    }
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

        AddProcesoToPNC() {
            this.procesos.forEach(element => {
                if(element.Proceso == this.ProcesoAddProcesoToPNCTemp) {
                    this.procesosToAdd.push(Object.assign({}, (element)));
                }
            });
            this.ProcesoAddProcesoToPNCTemp = "";
        },

        DeleteProcesoToPNC(index) {
            this.procesosToAdd.splice(index, 1);
        }

    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');

