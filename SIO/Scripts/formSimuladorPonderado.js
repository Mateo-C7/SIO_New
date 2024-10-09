import * as VueModule from 'vue';

const app = VueModule.createApp({
    data() {
        return {
            fupId: null,
            version: null,
        }
    },
    mounted() {

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

        getComparative: function () {
            this.listComparativo = {
                nivel1: null,
                nivel2: null,
                nivel3: null,
                nivel4: null,
                nivel5: null
            };
            var data = JSON.stringify({
                fupId: parseInt(this.fupId),
                version: this.version
            });
            this.mostrarLoad();
            fetch('FormSimuladorPonderado.aspx/GetComparative', {
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
                    this.ocultarLoad();
                    alert("Working!");
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

        obtenerVersionPorOrden: function () {
            var param = { idOrdenFabricacion: this.ordenFab };
            var iteracion = 0;
            var idVersionReciente = '';
            var fupEncontrado = 0;

            this.mostrarLoad();
            fetch('FormFUP.aspx/obtenerVersionPorOrdenFabricacion', {
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
                    $.each(data, function (index, item) {
                        if (iteracion == 0) {
                            iteracion = 1;
                            idVersionReciente = item.eect_vercot_id;
                            fupEncontrado = item.eect_fup_id;
                        }
                    });

                    if (idVersionReciente == '') {
                        toastr.warning("El FUP no existe o no tiene Permisos para Consultarlo", "Busqueda FUP");
                    } else {
                        this.fupId = fupEncontrado;
                        this.obtenerVersionPorIdFup();
                    }
                })
                .catch(err => {
                    this.ocultarLoad();
                    toastr.error("Failed to search FUP", "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                });
        },

        obtenerVersionPorIdFup: function () {
            var param = { idFup: this.fupId };
            var iteracion = 0;
            var idVersionReciente = '';
            this.mostrarLoad();
            fetch('FormFUP.aspx/obtenerVersionPorIdFup', {
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
                    if (json.d != null) {
                        var data = JSON.parse(json.d);
                        $.each(data, function (index, item) {
                            if (iteracion == 0) {
                                iteracion = 1;
                                idVersionReciente = item.eect_vercot_id;
                            }
                        });
                        if (idVersionReciente == '') {
                            toastr.warning("El FUP no existe o no tiene Permisos para Consultarlo", "Busqueda FUP");
                        }
                        else {
                            this.version = idVersionReciente;
                            this.getComparative();
                        }
                    }
                    else {
                        toastr.warning("search FUP Not Exists", "FUP");
                    }
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

        obtenerVersionPorOrdenCliente: function () {
            var param = { idOrdenCliente: this.ordenCli };
            var iteracion = 0;
            var idVersionReciente = '';
            var fupEncontrado = 0;

            this.mostrarLoad();
            fetch('FormFUP.aspx/obtenerFUPporOrdenCliente', {
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
                    $.each(data, function (index, item) {
                        if (iteracion == 0) {
                            iteracion = 1;
                            idVersionReciente = item.eect_vercot_id;
                            fupEncontrado = item.eect_fup_id;
                        }
                    });

                    if (idVersionReciente == '') {
                        toastr.warning("El FUP no existe o no tiene Permisos para Consultarlo", "Busqueda FUP");
                    } else {
                        this.fupId = fupEncontrado;
                        this.obtenerVersionPorIdFup();
                    }
                })
                .catch(err => {
                    this.ocultarLoad();
                    toastr.error("Failed to search FUP", "FUP", {
                        "timeOut": "0",
                        "extendedTImeout": "0"
                    });
                });
        }
    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');