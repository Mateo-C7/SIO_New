import * as VueModule from 'vue';
import Vue3EasyDataTable from 'vue3-easy-data-table';

const app = VueModule.createApp({
    data() {
        return {
            datatableMonedas: null,
            monedas: [],
            currentEditingMoneda: {},
            currentEditingRow: null,
            creating: null,
            index: 0
        }
    },
    mounted() {
        this.obtenerMonedas();
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

        obtenerMonedas: function () {
            this.mostrarLoad();
            fetch('FormMonedasEspecificacion.aspx/ObtenerMonedas', {
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
                this.monedas = JSON.parse(json.d);
                this.$nextTick(() => this.datatableMonedas = $("#monedas_table").DataTable({ "order": [] }));
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Monedas");
            });
        },

        mostrarModalEdicion: function (index) {
            this.currentEditingMoneda = JSON.parse(JSON.stringify(this.monedas[index]));
            this.creating = false;
            $("#modalEditarMoneda").modal('show');
        },

        mostrarModalCreacion: function () {
            this.currentEditingMoneda = { Activo: true, Descripcion: "" };
            this.creating = true;
            $("#modalEditarMoneda").modal('show');
        },

        guardarMoneda: function () {
            var param = {
                item: JSON.parse(JSON.stringify(this.currentEditingMoneda))
            }
            this.mostrarLoad();
            fetch('FormMonedasEspecificacion.aspx/ActualizarMonedas', {
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
                }
                return res.json();
            }).then(json => {
                this.ocultarLoad();
                let data = JSON.parse(json.d);
                if (this.creating) {
                    this.monedas.splice(0, 0, data);
                    this.index++;
                    this.$nextTick(() => this.datatableMonedas = $("#monedas_table").DataTable({ "order": [] }));
                    toastr.success("Moneda Creada");
                } else {
                    let monedaUpdate = this.monedas.find(x => x.Id == this.currentEditingMoneda.Id);
                    monedaUpdate.Descripcion = this.currentEditingMoneda.Descripcion;
                    monedaUpdate.Activo = this.currentEditingMoneda.Activo;
                    monedaUpdate.Simbolo = this.currentEditingMoneda.Simbolo;
                    monedaUpdate.ERP = this.currentEditingMoneda.ERP;
                    monedaUpdate.SeparadorDecimal = this.currentEditingMoneda.SeparadorDecimal;
                    monedaUpdate.SeparadorMiles = this.currentEditingMoneda.SeparadorMiles;
                    monedaUpdate.CantidadDecimales = this.currentEditingMoneda.CantidadDecimales;
                    toastr.success("Moneda Actualizada");
                }
                $("#modalEditarMoneda").modal('hide');
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Guardado Monedas");
            });
        }
    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');