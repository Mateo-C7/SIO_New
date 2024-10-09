import { createApp, ref, onMounted, computed } from 'vue';
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

const app = createApp({
    data() {
        return {
            filterClientId: -1,
            filterCountryId: -1,
            filterCityId: -1,
            filterPeriod: null,

            fupId: null,
            version: null,
        ordenFab: null,
        ordenCli: null,
        listComparativo: {
            FechaSimulacion: null,
            FechaCargue: null,
            nivel1: null,
            nivel2: null,
            nivel3: null,
            nivel4: null,
            nivel5: null
            }
        }
    },
    mounted() {
        this.getCountries();
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

        getCountries: function () {
            this.mostrarLoad();
            fetch('FormSimuladorComparativoFiltros.aspx/GetCountries', {
                method: 'POST',

                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            })
                .then(res => {
                    if (!res.ok) {
                        const error = new Error(res.statusText);
                        error.json = res.json();
                        throw error;
                    }
                    return res.json();
                })
                .then(json => {
                    this.listapaises = JSON.parse(json.d);
                    var data = JSON.parse(json.d);
                    $("#countryCmb").html('<option value=-1>Seleccionar país</option>');
                    data.forEach(element => {
                        $("#countryCmb").append($("<option />").val(element.Id).text(element.Nombre));
                    });
                    $("#countryCmb").selectpicker();
                    $("#clientCmb").selectpicker('refresh');
                    $("#cityCmb").selectpicker('refresh');
                })
                .catch(err => {
                    this.error.value = err;
                    if (err.json) {
                        return err.json.then(json => {
                            this.error.value.message = json.message;
                        });
                    }
                });
            this.ocultarLoad();
        },

        onChangePais() {
            var orden = JSON.stringify({ idPais: this.filterCountryId });
            this.mostrarLoad();
            fetch('FormSimuladorComparativoFiltros.aspx/GetCitiesFromCountry', {
                method: 'POST',
                body: orden,
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
                    this.listaCiudades = JSON.parse(json.d);
                    var data = JSON.parse(json.d);
                    $("#cityCmb").html('<option value=-1>Seleccionar ciudad</option>');
                    data.forEach(element => {
                        $("#cityCmb").append($("<option />").val(element.Id).text(element.Nombre));
                    });
                    $("#cityCmb").selectpicker('refresh');
                    this.filterCityId = -1;
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

        onChangeCiudad() {
            var orden = JSON.stringify({ idCiudad: this.filterCityId });
            this.mostrarLoad();
            fetch('FormSimuladorComparativoFiltros.aspx/GetClientsFromCity', {
                method: 'POST',
                body: orden,
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
                    this.listaClientes = JSON.parse(json.d);
                    var data = JSON.parse(json.d);
                    $("#clientCmb").html("<option value=-1>Seleccionar cliente</option> ");
                    data.forEach(element => {
                        $("#clientCmb").append($("<option />").val(element.Id).text(element.Nombre));
                    });
                    $("#clientCmb").selectpicker('refresh');
                    this.filterClientId = -1;
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

        consultar: function () {
            this.listComparativo = {
                FechaSimulacion: null,
                FechaCargue: null,
                nivel1: null,
                nivel2: null,
                nivel3: null,
                nivel4: null,
                nivel5: null
            };
            var params = {
                countryId: parseInt(this.filterCountryId),
                cityId: parseInt(this.filterCityId),
                clientId: parseInt(this.filterClientId),
                yearId: -1,
                monthId: -1
            };
            if (this.filterPeriod != null && this.filterPeriod != "" ) {
                let periodArray = this.filterPeriod.split("-");
                params.yearId = parseInt(periodArray[0]);
                params.monthId = parseInt(periodArray[1]);
            }

            this.mostrarLoad();
            fetch('FormSimuladorComparativoFiltros.aspx/GetComparative', {
                method: 'POST',
                body: JSON.stringify(params),
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
                    this.listComparativo = JSON.parse(json.d);

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

        ChangeLanguage: function (lang) {
            window.AppPQRS.__VUE_I18N__.global.locale = lang;
        }
    }
});

window.AppPQRS = app;
app.use(i18n);
app.mount('#app');
