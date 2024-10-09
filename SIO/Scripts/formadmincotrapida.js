import * as VueModule from 'vue';

const app = VueModule.createApp({
    data() {
        return {
            datatable_items: null,
            items: [],
            item_editando: {},

            datatable_precios: null,
            precios: [],
            precio_editando: {},

            datatable_factores_armado: null,
            factores_armado: [],
            factor_armado_editando: {},

            currentEditingRow: null,
            creating: null,
            index: 0,
            paises: []
        }
    },
    mounted() {
        this.obtenerItems();
        this.obtenerPrecios();
        this.obtenerFactoresArmado();
        this.obtenerPaises();
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

        obtenerPaises: function () {
            this.mostrarLoad();
            fetch('FormAdminCotRapida.aspx/ObtenerPaises', {
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
                this.paises = JSON.parse(json.d);
                $("#selectPais").html('<option value="-1">Seleccionar país</option><option value="-2">TODOS</option>');
                this.paises.forEach(element => {
                    $("#selectPais").append($("<option />").val(element.Id).text(element.Pais));
                });
                $("#selectPais").selectpicker();

                $("#selectPaisArmado").html('<option value="-1">Seleccionar país</option>');
                this.paises.forEach(element => {
                    $("#selectPaisArmado").append($("<option />").val(element.Id).text(element.Pais));
                });
                $("#selectPaisArmado").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Paises");
            });
        },

        obtenerItems: function () {
            this.mostrarLoad();
            fetch('FormAdminCotRapida.aspx/ObtenerItems', {
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
                this.items = JSON.parse(json.d);
                this.$nextTick(() => this.datatable_items = $("#items_table").DataTable({ "order": [] }));

                $("#selectItemCot").html('<option value="-1">Seleccionar Item</option>');
                this.items.forEach(element => {
                    $("#selectItemCot").append($("<option />").val(element.Id).text(element.Item));
                });
                $("#selectItemCot").selectpicker();

                $("#selectItemCotArmado").html('<option value="-1">Seleccionar Item</option>');
                this.items.forEach(element => {
                    $("#selectItemCotArmado").append($("<option />").val(element.Id).text(element.Item));
                });
                $("#selectItemCotArmado").selectpicker();
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Items");
            });
        },

        mostrarModalCreacionItem: function () {
            this.item_editando = { Id: null, CodItem: "", Item: "", CodGrupo: -1, Descripcion: "", Grupo: "" };
            this.creating = true;
            $("#modalItem").modal('show');
        },

        mostrarModalEdicionItem: function (index) {
            let item_editando_temp = this.items[index];
            this.item_editando = {
                Id: item_editando_temp.Id,
                CodItem: item_editando_temp.CodItem,
                Item: item_editando_temp.Item,
                CodGrupo: item_editando_temp.CodGrupo,
                Descripcion: item_editando_temp.Descripcion,
                Grupo: item_editando_temp.Grupo
            };
            this.creating = false;
            $("#modalItem").modal('show');
        },

        guardarItem: function () {
            if (this.item_editando.CodGrupo == 1) {
                this.item_editando.Grupo = "Aluminio";
            } else if (this.item_editando.CodGrupo == 2) {
                this.item_editando.Grupo = "Accesorios";
            } else {
                this.item_editando.Grupo = "Sistema Seguridad";
            }

            var param = {
                item: JSON.parse(JSON.stringify(this.item_editando))
            }
            this.mostrarLoad();
            fetch('FormAdminCotRapida.aspx/ActualizarItem', {
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
                    this.items.splice(0, 0, data);
                    this.index++;
                    this.$nextTick(() => this.datatable_items = $("#items_table").DataTable({ "order": [] }));
                    toastr.success("Item Creado");
                } else {
                    let item_update = this.items.find(x => x.Id == this.item_editando.Id);
                    item_update.Descripcion = this.item_editando.Descripcion;
                    item_update.CodItem = this.item_editando.CodItem;
                    item_update.Item = this.item_editando.Item;
                    item_update.CodGrupo = this.item_editando.CodGrupo;
                    item_update.Grupo = this.item_editando.Grupo;
                    toastr.success("Item Actualizado");
                }
                $("#modalItem").modal('hide');
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Guardado Item");
            });
        },

        obtenerPrecios: function () {
            this.mostrarLoad();
            fetch('FormAdminCotRapida.aspx/ObtenerPrecios', {
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
                this.precios = JSON.parse(json.d);
                this.$nextTick(() => this.datatable_precios = $("#precios_table").DataTable({ "order": [] }));
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Items");
            });
        },

        mostrarModalCreacionPrecio: function () {
            this.precio_editando = { Id: null, IdItemCotRapida: -1, IdPais: -1, Precio: 0 };
            this.creating = true;
            $("#modalPrecio").modal('show');
        },

        mostrarModalEdicionPrecio: function (index) {
            let precio_editando_temp = this.precios[index];
            this.precio_editando = {
                Id: precio_editando_temp.Id,
                IdItemCotRapida: precio_editando_temp.IdItemCotRapida,
                IdPais: precio_editando_temp.IdPais,
                Precio: precio_editando_temp.Precio
            };
            this.$nextTick(function () {
                $('#selectItemCot').selectpicker('refresh');
                $('#selectPais').selectpicker('refresh');
            });
            this.creating = false;
            $("#modalPrecio").modal('show');
        },

        guardarPrecio: function () {
            var param = {
                item: JSON.parse(JSON.stringify(this.precio_editando))
            }
            if(param.item.IdPais == "-2") {
                param.item.IdPais = null;
            }
            this.mostrarLoad();
            fetch('FormAdminCotRapida.aspx/ActualizarPrecio', {
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
                this.precio_editando = {};
                if (this.creating) {
                    this.precios.splice(0, 0, data);
                    this.index++;
                    this.$nextTick(() => this.datatable_precios = $("#precios_table").DataTable({ "order": [] }));
                    toastr.success("Precio Creado");
                } else {
                    let precio_update = this.precios.find(x => x.Id == this.precio_editando.Id);
                    precio_update.IdItemCotRapida = this.precio_editando.IdItemCotRapida;
                    precio_update.IdPais = this.precio_editando.IdPais;
                    precio_update.Precio = this.precio_editando.Precio;
                    precio_update.ItemCotRapida = data.ItemCotRapida;
                    precio_update.Pais = data.Pais;
                    toastr.success("Precio Actualizado");
                }
                $("#modalPrecio").modal('hide');
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Guardado Precio");
            });
        },

        obtenerFactoresArmado: function () {
            this.mostrarLoad();
            fetch('FormAdminCotRapida.aspx/ObtenerFactoresArmado', {
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
                this.factores_armado = JSON.parse(json.d);
                this.$nextTick(() => this.datatable_factores_armado = $("#factores_armado_table").DataTable({ "order": [] }));
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Cargue Items");
            });
        },

        mostrarModalCreacionFactorArmado: function () {
            this.factor_armado_editando = { Id: null, IdItemCotRapida: -1, IdPais: -1, IdTipoVivienda: -1, Factor: 0 };
            this.creating = true;
            $("#modalFactorArmado").modal('show');
        },

        mostrarModalEdicionFactorArmado: function (index) {
            let factor_armado_editando_temp = this.factores_armado[index];
            this.factor_armado_editando = {
                Id: factor_armado_editando_temp.Id,
                IdItemCotRapida: factor_armado_editando_temp.IdItemCotRapida,
                IdPais: factor_armado_editando_temp.IdPais,
                IdTipoVivienda: factor_armado_editando_temp.IdTipoVivienda,
                Factor: factor_armado_editando_temp.Factor
            };
            this.$nextTick(function () {
                $('#selectItemCotArmado').selectpicker('refresh');
                $('#selectPaisArmado').selectpicker('refresh');
            });
            this.creating = false;
            $("#modalFactorArmado").modal('show');
        },

        guardarFactorArmado: function () {
            var param = {
                item: JSON.parse(JSON.stringify(this.factor_armado_editando))
            }
            this.mostrarLoad();
            fetch('FormAdminCotRapida.aspx/ActualizarFactorArmado', {
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
                    this.factores_armado.splice(0, 0, data);
                    this.index++;
                    this.$nextTick(() => this.datatable_factores_armado = $("#factores_armado_table").DataTable({ "order": [] }));
                    toastr.success("Factor Armado Creado");
                } else {
                    let factor_armado_update = this.factores_armado.find(x => x.Id == this.factor_armado_editando.Id);
                    factor_armado_update.IdItemCotRapida = this.factor_armado_editando.IdItemCotRapida;
                    factor_armado_update.IdPais = this.factor_armado_editando.IdPais;
                    factor_armado_update.IdTipoVivienda = this.factor_armado_editando.IdTipoVivienda;
                    factor_armado_update.Factor = this.factor_armado_editando.Factor;
                    factor_armado_update.ItemCotRapida = data.ItemCotRapida;
                    factor_armado_update.Pais = data.Pais;
                    factor_armado_update.TipoVivienda = data.TipoVivienda;
                    toastr.success("Factor Armado Actualizado");
                }
                $("#modalFactorArmado").modal('hide');
            }).catch(error => {
                this.ocultarLoad();
                toastr.error(error, "Guardado Factor Armado");
            });
        }
    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');