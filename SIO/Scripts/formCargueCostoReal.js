import * as VueModule from 'vue';
import Vue3EasyDataTable from 'vue3-easy-data-table';
import * as XLSX from 'https://unpkg.com/xlsx/xlsx.mjs';

const app = VueModule.createApp({
    data() {
        return {
            fupId: null,
            version: null,
            stageTable: null,
            indexStageTable: 0,
            years: [
                { IdYear: 2020, Year: "2020" },
                { IdYear: 2021, Year: "2021" },
                { IdYear: 2022, Year: "2022" },
                { IdYear: 2023, Year: "2023" },
                { IdYear: 2024, Year: "2024" },
                { IdYear: 2025, Year: "2025" }
            ],
            months: [
                { IdMes: 1, Mes: "Enero" },
                { IdMes: 2, Mes: "Febrero" },
                { IdMes: 3, Mes: "Marzo" },
                { IdMes: 4, Mes: "Abril" },
                { IdMes: 5, Mes: "Mayo" },
                { IdMes: 6, Mes: "Junio" },
                { IdMes: 7, Mes: "Julio" },
                { IdMes: 8, Mes: "Agosto" },
                { IdMes: 9, Mes: "Septiembre" },
                { IdMes: 10, Mes: "Octubre" },
                { IdMes: 11, Mes: "Noviembre" },
                { IdMes: 12, Mes: "Diciembre" }
            ],
            yearLoad: 0,
            monthLoad: 0,
            items: []
        }
    },
    mounted() {
        this.showMonths();
        this.showYears();
        this.createDataTable();
    },
    methods: {
        showLoading: function () {
            $.LoadingOverlay("show", {
                color: "rgba(0, 0, 0, 0.3)",
                image: "",
                custom: "<div style='color:white; font-size: 100px;' class='fa fa-refresh fa-spin'></div>",
                fade: false
            });
        },

        hideLoading: function () {
            $.LoadingOverlay("hide", true);
        },

        doOnLoad: function () { },

        createDataTable: function () {
            if (this.stageTable != null) {
                this.stageTable.clear().destroy();
                this.indexStageTable++;
            }
            this.$nextTick(() => this.stageTable = $("#stageTable").DataTable({
                dom: 'Bfrtip',
                "order": [],
                buttons: [
                    {
                        extend: 'colvis',
                        collectionLayout: 'fixed columns',
                        collectionTitle: 'Column visibility control'
                    }
                ],
                columns: [
                    { visible: true },
                    { visible: true },
                    { visible: true },
                    { visible: true },
                    { visible: true },
                    { visible: true },
                    { visible: true },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: true },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: true },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: false },
                    { visible: true },
                    { visible: false },
                    { visible: true },
                    { visible: false }
                ],
                language: { url: 'Scripts/Datatables_Scripts/languages/es-ES.json' }
            }));
        },

        // Pending for complete
        fetchMonths: function () {
            fetch('FormCargueCostoReal.aspx/', {
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
                this.hideLoading();
                this.months = JSON.parse(json.d);
            }).catch(error => {
                this.hideLoading();
                toastr.error(error, "Cargue Costo Real");
            });
        },

        showMonths: function () {
            this.months.forEach(month => {
                $("#cboMonth").append($("<option />").val(month.IdMes).text(month.Mes));
            });
        },

        showYears: function () {
            this.years.forEach(year => {
                $("#cboYear").append($("<option />").val(year.IdYear).text(year.Year));
            });
        },

        verifyInputsToLoadExcel: function () {
            if (this.yearLoad == 0) {
                toastr.error("Selecciona un año", "Cargue Costo Real");
                return false;
            }
            if (this.monthLoad == 0) {
                toastr.error("Selecciona un mes", "Cargue Costo Real");
                return false;
            }
            if (this.$refs.inpFileExcelCostoReal.files.length == 0) {
                toastr.error("Selecciona el archivo a cargar", "Cargue Costo Real");
                return false;
            }
            if (this.$refs.inpFileExcelCostoReal.files.length > 1) {
                toastr.error("Sólo puedes cargar un archivo al tiempo", "Cargue Costo Real");
                return false;
            }
            this.askToLoadFile();
        },

        askToLoadFile: function () {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.info("<br /><br /><button class='btn btn-default' type='button' id='ConfirmarExcel' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de cargar este archivo de costos?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfirmarExcel").click(function () {
                            parentWindow.loadExcel();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        convertAndCheckFloat: function (number) {
            if (number == undefined || number == null) {
                return 0;
            }
            number = parseFloat(number);
            if (isNaN(number)) {
                return 0;
            }
            return number;
        },

        checkStrings: function (string) {
            if (string == undefined || string == null) {
                return "";
            }
            string = string.trim();
            return string;
        },

        loadExcel: function () {
            let file = this.$refs.inpFileExcelCostoReal.files[0];
            const reader = new FileReader();
            this.showLoading();
            reader.onload = (res) => {
                const data = res.target.result;
                const workbook = XLSX.read(data, { type: 'binary' });
                const sheetName = workbook.SheetNames[0];
                const excelData = XLSX.utils.sheet_to_json(workbook.Sheets[sheetName], { header: 1 });

                const rows = excelData.slice(1);
                rows.forEach((row) => {
                    let newItem = {
                        Anio: parseInt(this.yearLoad),
                        Mes: this.monthLoad,
                        Tipo: this.checkStrings(row[0]),
                        NoOrden: this.checkStrings(row[1]),
                        Cliente: this.checkStrings(row[2]),
                        Pais: this.checkStrings(row[3]),
                        NacEx: this.checkStrings(row[4]),
                        M2Vend: this.convertAndCheckFloat(row[5]),
                        PrecioM2USD: this.convertAndCheckFloat(row[6]),
                        UndVend: this.convertAndCheckFloat(row[7]),
                        KgVend: this.convertAndCheckFloat(row[8]),
                        UndM2: this.convertAndCheckFloat(row[9]),
                        M2Cot: this.convertAndCheckFloat(row[10]),
                        VrCot: this.convertAndCheckFloat(row[11]),
                        USD: this.convertAndCheckFloat(row[12]),
                        OtrosUSD: this.convertAndCheckFloat(row[13]),
                        COP: this.convertAndCheckFloat(row[14]),
                        MpAluminio: this.convertAndCheckFloat(row[15]),
                        MpPlastico: this.convertAndCheckFloat(row[16]),
                        Kanban: this.convertAndCheckFloat(row[17]),
                        Stock: this.convertAndCheckFloat(row[18]),
                        Consumible: this.convertAndCheckFloat(row[19]),
                        MOD: this.convertAndCheckFloat(row[20]),
                        CIF: this.convertAndCheckFloat(row[21]),
                        TotalAluminio: this.convertAndCheckFloat(row[22]),
                        Nivel1Almacen: this.convertAndCheckFloat(row[23]),
                        Nivel2Almacen: this.convertAndCheckFloat(row[24]),
                        Nivel3Almacen: this.convertAndCheckFloat(row[25]),
                        Nivel4Almacen: this.convertAndCheckFloat(row[26]),
                        Nivel5Almacen: this.convertAndCheckFloat(row[27]),
                        ConsObra: this.convertAndCheckFloat(row[28]),
                        TotalAccAlmacen: this.convertAndCheckFloat(row[29]),
                        Nivel1Fabricados: this.convertAndCheckFloat(row[30]),
                        Nivel2Fabricados: this.convertAndCheckFloat(row[31]),
                        Nivel3Fabricados: this.convertAndCheckFloat(row[32]),
                        Nivel4Fabricados: this.convertAndCheckFloat(row[33]),
                        Nivel5Fabricados: this.convertAndCheckFloat(row[34]),
                        TotalAccFabricados: this.convertAndCheckFloat(row[35]),
                        Acero: this.convertAndCheckFloat(row[36]),
                        CostoTotal: this.convertAndCheckFloat(row[37]),
                        Porc: 0.0,
                        IsNew: true
                    };
                    this.items.push(newItem);
                });
                this.hideLoading();
                this.items.sort((a, b) => {
                    return b.IsNew - a.IsNew;
                });
                this.createDataTable();
                $("#loadExcelFileModal").modal('hide');
                toastr.info("Items cargados correctamente, pendientes por verificar y guardar", "Cargue Costo Real");
            };
            reader.onerror = (err) => {
                console.log(err);
                this.hideLoading();
            }
            reader.readAsBinaryString(file);

        },

        RemoveItem: function (index) {
            this.items.splice(index, 1);
            this.createDataTable();
            toastr.success("Item eliminado satisfactoriamente", "Cargue Costo Real");
        },

        confirmSaveNewRows: function () {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.info("<br /><br /><button class='btn btn-default' type='button' id='ConfirmarGuardado' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de guardar las nuevas filas?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfirmarGuardado").click(function () {
                            parentWindow.saveNewRows();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        saveNewRows: function () {
            var params = {
                rows: this.items.filter(x => x.IsNew === true),
            }
            this.showLoading();
            fetch('FormCargueCostoReal.aspx/SaveRows', {
                method: 'POST',
                body: JSON.stringify(params),
                headers: {
                    'Accept': 'application/json',
                    'Content-type': 'application/json; charset=utf-8'
                }
            }).then(res => {
                if (!res.ok) {
                    this.hideLoading();
                    const error = new Error(res.statusText);
                    error.json = res.json();
                    throw error;
                }
                return res.json();
            }).then(json => {
                this.hideLoading();
                this.items = [];
            }).catch(error => {
                this.hideLoading();
                toastr.error(error, "Cargue Costo Real");
            });
        },

        confirmClearStagedRows: function () {
            toastr.options.timeOut = "0";
            toastr.options.positionClass = 'toast-top-center'
            toastr.closeButton = true;
            var parentWindow = this;
            toastr.info("<br /><br /><button class='btn btn-default' type='button' id='ConfirmarLimpiado' style='margin-right: 15px;margin-left: 45px;' class='btn clear'>Yes</button><button class='btn btn-default' type='button' id='' class='btn clear'>No</button>",
                '¿Estás seguro de borrar las filas en stage?',
                {
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#ConfirmarLimpiado").click(function () {
                            parentWindow.clearStagedRows();
                        });
                    }
                });

            toastr.options.timeOut = "1000";
            toastr.closeButton = false;
        },

        clearStagedRows: function () {
            this.items = this.items.filter(x => x.IsNew === false);
            this.createDataTable();
        }
    }
});

window.Vue = VueModule;
window.AppPQRS = app;
app.mount('#app');