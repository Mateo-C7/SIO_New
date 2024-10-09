import * as VueModule from 'vue';
import Vue3EasyDataTable from 'vue3-easy-data-table';

const app = VueModule.createApp({
    data() {
        return {
            FupId: 0,
            Version: "",
            referencias: [],
            imgReferencia: '',
            familiaReferencia: '',
            tablaReferencias: null,
            ancho1Requerido: false,
            alto1Requerido: false,
            ancho2Requerido: false,
            alto2Requerido: false,
            accsRequerido: false,
            espanRequerido: false,
            ancho1: 0.0,
            alto1: 0.0,
            ancho2: 0.0,
            alto2: 0.0,
            accs: 0.0,
            espan: 0.0,
            itemCantidad: 0,
            descAux: "",
            itemsActuales: [],
            index: 0,
            deshabilitarBtnAdicionar: true,
            parametrosUrlValidos: true,
            productos: {},
            listaSimulada: 0,
            deshabilitarBtnAdd: false,
            cantidadTotal: 0,
            m2Totales: 0
        }
    },
    mounted() {
        this.CargarReferencias();
        this.CargarItems();
        this.CargarProductos();
        this.ConsultarSimulacion();
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

        calculateQuantityAndM2: function () {
            this.cantidadTotal = 0;
            this.m2Totales = 0;
            this.itemsActuales.forEach(item => {
                this.cantidadTotal += item.ItemCantidad;
                this.m2Totales += (item.ItemM2 * item.ItemCantidad) ;
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

        doOnLoad: function (){},

        CargarReferencias: function () {
            this.mostrarLoad();
            fetch('FormListaAsistida.aspx/ObtenerReferencias', {
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
                this.referencias = data;
                $("#cmbReferencias").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    $("#cmbReferencias").append($("<option />").val(element.RefId).text(element.RefDescripcionCompuesta));
                });
                $("#cmbReferencias").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Lista Asistida");
            });
        },

        CargarProductos: function () {
            var urlParams = new URLSearchParams(window.location.search);
            this.FupId = parseInt(urlParams.get('FupId'));
            this.Version = urlParams.get('Version');
            if(isNaN(this.FupId) || this.Version == undefined) {
                this.parametrosUrlValidos = false;
                return false;
            }
            var param = {
                fupId:this.FupId,
                version:this.Version
            }
            this.mostrarLoad();
            fetch('FormListaAsistida.aspx/ObtenerProductos', {
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
                this.productos = data;
                $("#cmbProductos").html('<option value="0">-- Seleccionar --</option>');
                data.forEach(element => {
                    $("#cmbProductos").append($("<option selected />").val(element.id).text(element.descripcion));
                });
                $("#cmbProductos").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Lista Asistida");
            });
        },

        CargarItems: function() {
            var urlParams = new URLSearchParams(window.location.search);
            this.FupId = parseInt(urlParams.get('FupId'));
            this.Version = urlParams.get('Version');
            if(isNaN(this.FupId) || this.Version == undefined) {
                this.parametrosUrlValidos = false;
                return false;
            }
            var param = {
                fupId:this.FupId,
                fupVersion:this.Version
            }
            this.mostrarLoad();
            fetch('FormListaAsistida.aspx/ObtenerItems', {
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
                this.itemsActuales = JSON.parse(json.d);
                this.calculateQuantityAndM2();
                this.CargarTablaReferenciasGuardadas();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Lista Asistida Cargue Items");
            });
        },

        cmbProductoOnChange: function() {
            var refId = $("#cmbReferencias").val();
            var idProducto = $("#cmbProductos").val();

            if(idProducto != 0) {
                this.deshabilitarBtnAdd = false;
            } else {
                this.deshabilitarBtnAdd = true;
            }

            if (refId != 0) {
                if(idProducto != 0) {
                    this.deshabilitarBtnAdicionar = false;
                } else {
                    this.deshabilitarBtnAdicionar = true;
                }
            } else {
                this.deshabilitarBtnAdicionar = true;
            }
        },

        cmbReferenciaOnChange: function() {
            var refId = $("#cmbReferencias").val();
            var idProducto = $("#cmbProductos").val();
            let accs = this.productos.filter(producto => producto.id == idProducto)[0].AccsListaAsistida;
            let Espan = this.productos.filter(producto => producto.id == idProducto)[0].EspanListaAsistida; 

            if (refId != 0) {
                if(idProducto != 0) {
                    this.deshabilitarBtnAdicionar = false;
                }
                this.referencias.forEach(referencia => {
                    if(referencia.RefId == refId) {
                        this.imgReferencia = referencia.RutaImagen;
                        this.familiaReferencia = referencia.RefGrupoDescripcion;
                        this.ancho1Requerido = referencia.RequiereAncho1;
                        this.ancho2Requerido = referencia.RequiereAncho2;
                        this.alto1Requerido = referencia.RequiereAlto1;
                        this.alto2Requerido = referencia.RequiereAlto2;
                        this.accsRequerido = referencia.RequiereACCS;
                        this.espanRequerido = referencia.RequiereESPAN;
                    }
                });
            } else {
                this.imgReferencia = '';
                this.familiaReferencia = '';
                this.ancho1 = 0.0;
                this.alto1 = 0.0;
                this.ancho2 = 0.0;
                this.alto2 = 0.0;
                this.accs = accs;
                this.espan = Espan;
                this.itemCantidad = 0;
                this.descAux = "";
                this.ancho1Requerido = false;
                this.ancho2Requerido = false;
                this.alto1Requerido = false;
                this.alto2Requerido = false;
                this.accsRequerido = false;
                this.espanRequerido = false;
                this.deshabilitarBtnAdicionar = true;
            }
        },
    
        CargarTablaReferenciasGuardadas: function() {
            this.mostrarLoad();
            if(this.tablaReferencias != null) {
                this.tablaReferencias.clear().destroy();
                this.index++;
            }
            this.$nextTick(() => this.tablaReferencias = $("#tableReferencias").DataTable({"order": [], pageLength: 100}));
            this.ocultarLoad();

        },

        ReemplazarEnFormula: function(item, formula) {
            if(formula == undefined || formula == '') {
                item.ItemM2 = 0;
                return item;
            }
            if(item.alto1Requerido) {
                formula = formula.replace(/ALTO1/g, item.ItemAlto1);
            }
            if(item.alto2Requerido) {
                if (item.ItemAlto2 == 0){ formula = formula.replace(/ALTO2/g, item.ItemAlto1);}
                else { formula = formula.replace(/ALTO2/g, item.ItemAlto2);}
            }
            if(item.ancho1Requerido) {
                formula = formula.replace(/ANCHO1/g, item.ItemAncho1);
            }
            if(item.ancho2Requerido) {
                if (item.ItemAncho2 == 0) {formula = formula.replace(/ANCHO2/g, item.ItemAncho1);}
                else {formula = formula.replace(/ANCHO2/g, item.ItemAncho2);}
            }
            if(item.accsRequerido) {
                let match = item.RefCodigo.match(/CU(\d+(\.\d+)?)/);
                let extractedNumber = match ? parseFloat(match[1]) : item.ItemACCS;
                formula = formula.replace(/ACCS/g, extractedNumber);
            }
            if(item.espanRequerido) {
                formula = formula.replace(/ESPAN/g, item.ItemESPAN);
            }
            try {
                item.ItemM2 = parseFloat(eval(formula)).toFixed(2);
                item.ItemTotalM2 = parseFloat(item.ItemM2 * parseInt(item.ItemCantidad)).toFixed(2);
            } catch (error) {

                item.ItemM2 = 0;
                item.ItemTotalM2 = 0;
                toastr.error(error, "Lista Asistida Calculo de M2");
            }
            return item;
        },

        AdicionarItem: function() {
            var refId = $("#cmbReferencias").val();
            var idProducto = $("#cmbProductos").val();
            let accs = this.productos.filter(producto => producto.id == idProducto)[0].AccsListaAsistida;
            let Espan = this.productos.filter(producto => producto.id == idProducto)[0].EspanListaAsistida; 
            var item = {
                ItemCantidad : parseInt(this.itemCantidad),
                RefCodigoId: 0,
                RefCodigo : "",
                ItemAncho1 : this.ancho1,
                ItemAlto1 : this.alto1,
                ItemAncho2 : this.ancho2,
                ItemAlto2 : this.alto2,
                ItemACCS: accs,
                ItemESPAN: Espan,
                ItemDescAux : this.descAux,
                RefGrupoDescripcion : "",
                ItemM2 : 0,
                ItemTotalM2 : 0,
                PendienteGuardar: 1,
                IdProducto: 0,
                Prefijo: ""
            };
            if (refId != 0) {
                this.referencias.forEach(referencia => {
                    if(referencia.RefId == refId) {
                        item.RefCodigo = referencia.RefCodigo;
                        item.RefCodigoId = referencia.RefId;
                        item.RefGrupoDescripcion = referencia.RefGrupoDescripcion;
                        item.ancho1Requerido = referencia.RequiereAncho1;
                        item.ancho2Requerido = referencia.RequiereAncho2;
                        item.alto1Requerido = referencia.RequiereAlto1;
                        item.alto2Requerido = referencia.RequiereAlto2;
                        item.accsRequerido = referencia.RequiereACCS;
                        item.espanRequerido = referencia.RequiereESPAN;
                        item = this.ReemplazarEnFormula(item, referencia.Formula);
                        return false;
                    }
                });
            }
            let prefijoTemp = this.productos.filter(producto => producto.id == idProducto)[0].PrefijoListaAsistida;
            let excludedReferences = ["CU","CU11","CU12","CU12.5","CU12.5-DG","CU12-DG","CU13","CU13-DG","CU14","CU14-DG",
                "CU15","CU15-DG","CU8","CU-DG","AGP","AGR","AGRD","AGRD-E","AGRD-I","AG","AGE","CPBL","CPBL+AGV","CPBL+AGV+DS",
                "CPBL+DS","CPC","CPCD","CPC-TX","CPC-TX","FL"-"FL+AG","FL+AG-D","FL+D","FL+DL","FLA","FLA+D","FLA+DL",
                "FLE","FLP","FLP+D","FLP+DL","FLPC","FLPE","FLPI","FLPI+D","FLPI+DL","FLPL","FLPL-D","FLR","TH","THE","THP",
                "THPE","THPT","THT","TNH","TNH11","TNH2.5","TNH2.8","TNH6.8","TNG6.9","TNHE","TNHP","TNHPE","TNHP-T1",
                "TNHP-T2","TNH-T1","TNH-T2","TNV","TNVD","TV","TVD","TVDT","TVE","TVLLE","TVLLI","TVT"
            ]
            if(idProducto == "2" && jQuery.inArray(item.RefCodigo, excludedReferences) !== -1) {
                prefijoTemp = "";
            }
            item.Prefijo = prefijoTemp;
            item.IdProducto = idProducto;
            this.itemsActuales.push(item);
            this.calculateQuantityAndM2();
            this.CargarTablaReferenciasGuardadas();
            toastr.info("Item añadido satisfactoriamente", "Asistida Adición Items");
            this.ancho1 = 0.0;
            this.alto1 = 0.0;
            this.ancho2 = 0.0;
            this.alto2 = 0.0;
            this.accs = accs;
            this.espan = Espan;
            this.itemCantidad = 0;
            this.descAux = "";
        },

        RemoverItem: function(index) {
            this.itemsActuales.splice(index, 1);
            this.calculateQuantityAndM2();
            this.CargarTablaReferenciasGuardadas();
            toastr.info("Item eliminado satisfactoriamente", "Asistida Substracción Items");
        },

        GuardarListado: function() {

            var param = {
                fupId:this.FupId,
                fupVersion:this.Version,
                items: JSON.parse(JSON.stringify(this.itemsActuales))
            }
            this.mostrarLoad();
            fetch('FormListaAsistida.aspx/InsertarItems', {
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
                this.CargarItems();
                toastr.success("Items guardados satisfactoriamente", "Lista Asistida Guardado Items");
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Lista Asistida Guardado Items");
            });
        },

        BorrarListadoAjax: function() {
            var param = {
                fupId:this.FupId,
                fupVersion:this.Version
            }
            this.mostrarLoad();
            fetch('FormListaAsistida.aspx/BorrarItems', {
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
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Lista Asistida Eliminacion Items");
            });
        },

        LimpiarListado: function() {
            this.itemsActuales = [];
            this.CargarTablaReferenciasGuardadas();
            toastr.info("Items eliminados - Pendiente guardar", "Asistida Limpiar Items");
        },

        EnviarRequestSimular: function() {
            var param = {
                fupId: parseInt(this.FupId),
                version: this.Version
            }
            this.mostrarLoad();
            fetch('FormListaAsistida.aspx/Simular', {
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
                this.ConsultarSimulacion();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Lista Asistida Cargue Items");
            });
        },

        Simular: function() {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.info("<br /><br /><button class='btn btn-default' type='button' id='ConfimarDelAdaptacion6' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de simular la lista?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfimarDelAdaptacion6").click(function () {
                            parentWindow.EnviarRequestSimular();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        ConsultarSimulacion: function() {
            var param = {
                fupId: parseInt(this.FupId),
                version: this.Version
            }
            this.mostrarLoad();
            fetch('FormListaAsistida.aspx/ConsultarSimulacion', {
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
                this.listaSimulada = JSON.parse(json.d);
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Lista Asistida Cargue Items");
            });
        },

        cleanAndTrim: function (value) {
            return value != null ? value.replace(/[^a-zA-Z0-9. ]/g, '').replace(/\s+/g, ' ').trim() : '';
        },

        parseFloatOrDefault: function(value) {
            const cleanedValue = this.cleanAndTrim(value);
            return cleanedValue !== '' ? parseFloat(cleanedValue) : 0;
        },

        parseIntOrDefault: function (value) {
            const cleanedValue = this.cleanAndTrim(value);
            return cleanedValue !== '' ? parseInt(cleanedValue) : 0;
        },

        SubirListado: function() {
            if(this.$refs.inputFileListado.files.length > 0) {
                var idProducto = $("#cmbProductos").val();
                let accs = this.productos.filter(producto => producto.id == idProducto)[0].AccsListaAsistida;
                let Espan = this.productos.filter(producto => producto.id == idProducto)[0].EspanListaAsistida; 

                let file = this.$refs.inputFileListado.files[0];
                const reader = new FileReader();
                if(file.name.includes(".csv")) {
                    this.mostrarLoad();
                    reader.onload = (res) => {
                        let patitionedLines = res.target.result.split("\r\n");
                        for(let c = 11; c < patitionedLines.length - 1 ; c++) {
                             let partitionedLine = patitionedLines[c].split(",");
                             if (partitionedLine[0] != "" && partitionedLine[0] != null) {
                                 var item = {};
                                 if (this.cleanAndTrim(partitionedLine[8]) != "UNION"){
                                     item = {
                                         ItemCantidad: this.parseIntOrDefault(partitionedLine[0]),
                                         RefCodigoId: 0,
                                         RefCodigo: this.cleanAndTrim(partitionedLine[1]).toUpperCase(),
                                         ItemAncho1: this.parseFloatOrDefault(partitionedLine[2]),
                                         ItemAlto1: this.parseFloatOrDefault(partitionedLine[3]),
                                         ItemAlto2: this.parseFloatOrDefault(partitionedLine[4]),
                                         ItemAncho2: this.parseFloatOrDefault(partitionedLine[5]),
                                         ItemACCS: accs,
                                         ItemESPAN: Espan,
                                         ItemDescAux: this.cleanAndTrim(partitionedLine[7]),
                                         RefGrupoDescripcion: "",
                                         ItemM2: this.parseFloatOrDefault(partitionedLine[9]),
                                         ItemTotalM2: this.parseFloatOrDefault(partitionedLine[10]),
                                         PendienteGuardar: 1
                                     };
                                     console.log(partitionedLine[9]);
                                 }
                                 else {
                                     item = {
                                         ItemCantidad: this.parseIntOrDefault(partitionedLine[0]),
                                         RefCodigoId: 0,
                                         RefCodigo: this.cleanAndTrim(partitionedLine[1]).toUpperCase(),
                                         ItemAlto1: this.parseFloatOrDefault(partitionedLine[2]),
                                         ItemAncho1: this.parseFloatOrDefault(partitionedLine[3]),
                                         ItemAncho2: this.parseFloatOrDefault(partitionedLine[5]),
                                         ItemAlto2: this.parseFloatOrDefault(partitionedLine[4]),
                                         ItemACCS: accs,
                                         ItemESPAN: Espan,
                                         ItemDescAux: this.cleanAndTrim(partitionedLine[7]),
                                         RefGrupoDescripcion: "",
                                         ItemM2: this.parseFloatOrDefault(partitionedLine[9]),
                                         ItemTotalM2: this.parseFloatOrDefault(partitionedLine[10]),
                                         PendienteGuardar: 1
                                     };
                                     console.log(partitionedLine[9]);
                                }

                                    this.referencias.forEach(referencia => {
                                    if(referencia.RefCodigo == (partitionedLine[1])) {
                                        item.RefCodigo = referencia.RefCodigo;
                                        item.RefCodigoId = referencia.RefId;
                                        item.RefGrupoDescripcion = referencia.RefGrupoDescripcion;
                                        item.ancho1Requerido = referencia.RequiereAncho1;
                                        item.ancho2Requerido = referencia.RequiereAncho2;
                                        item.alto1Requerido = referencia.RequiereAlto1;
                                        item.alto2Requerido = referencia.RequiereAlto2;
                                        item.accsRequerido = referencia.RequiereACCS;
                                        item.espanRequerido = referencia.RequiereESPAN;

                                        item = this.ReemplazarEnFormula(item, referencia.Formula);
                                        return false;
                                    }
                                });
                                let prefijoTemp = this.productos.filter(producto => producto.id == idProducto)[0].PrefijoListaAsistida;
                                let excludedReferences = ["CU","CU11","CU12","CU12.5","CU12.5-DG","CU12-DG","CU13","CU13-DG","CU14","CU14-DG",
                                    "CU15","CU15-DG","CU8","CU-DG","AGP","AGR","AGRD","AGRD-E","AGRD-I","AG","AGE","CPBL","CPBL+AGV","CPBL+AGV+DS",
                                    "CPBL+DS","CPC","CPCD","CPC-TX","CPC-TX","FL"-"FL+AG","FL+AG-D","FL+D","FL+DL","FLA","FLA+D","FLA+DL",
                                    "FLE","FLP","FLP+D","FLP+DL","FLPC","FLPE","FLPI","FLPI+D","FLPI+DL","FLPL","FLPL-D","FLR","TH","THE","THP",
                                    "THPE","THPT","THT","TNH","TNH11","TNH2.5","TNH2.8","TNH6.8","TNG6.9","TNHE","TNHP","TNHPE","TNHP-T1",
                                    "TNHP-T2","TNH-T1","TNH-T2","TNV","TNVD","TV","TVD","TVDT","TVE","TVLLE","TVLLI","TVT"
                                ];
                                if(idProducto == "2" && jQuery.inArray(item.RefCodigo, excludedReferences) !== -1) {
                                    prefijoTemp = "";
                                }
                                item.Prefijo = prefijoTemp;
                                item.IdProducto = idProducto;
                                this.itemsActuales.push(item);
                            }
                        }
                        this.ocultarLoad();
                        this.calculateQuantityAndM2();
                        this.CargarTablaReferenciasGuardadas();
                    };
                    reader.onerror = (err) => console.log(err);
                    reader.readAsText(file);
                } else {
                    toastr.error("El archivo debe ser de tipo .csv", "Lista Asistida Cargue Items");
                }
                
            } else {
                toastr.info("Ningun archivo para subir", "Lista Asistida Cargue Items");
            }
        },

        ImprimirCarta: function() {
            // create the form
            var form = document.createElement('form');
            form.setAttribute('method', 'post');
            form.setAttribute('action', ' formsalidacotizacion.aspx?fup='+this.FupId);
            form.setAttribute("target", "_blank");

            // add form to body and submit
            document.body.appendChild(form);
            form.submit();
        }

    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');