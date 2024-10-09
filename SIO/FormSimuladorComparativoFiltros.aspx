<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormSimuladorComparativoFiltros.aspx.cs" Inherits="SIO.FormSimuladorComparativoFiltros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="importmap">
		{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                           "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js", 
                            "vue-i18n" : "./Scripts/vue-i18n.esm-browser.js",
                            "TradEsp" : "./Scripts/translation/es.json",
                            "TradEng" : "./Scripts/translation/en.json",
                            "TradPor" : "./Scripts/translation/pt.json"

                         } 
            }
	</script>
	<script type="module" src="Scripts/formSimuladorComparativoFiltros.js?v=20240815"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/select2.min.js"></script>
	<script type="module" src="Scripts/bootstrap-select.min.js"></script>
	<script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
	<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>

	<link rel="Stylesheet" href="Content/bootstrap.min.css" />
	<link rel="Stylesheet" href="Content/SIO.css" />
	<link rel="Stylesheet" href="Scripts/TableVue/style.css" />
	<link rel="stylesheet" href="Content/font-awesome.css" />
	<link rel="Stylesheet" href="Content/css/select2.min.css" />
    <link rel="Stylesheet" href="Content/bootstrap-select.css" />
	<link href="Content/toastr.min.css" rel="stylesheet" />

    	<!-- Custom Styles -->
	<style type="text/css">
		.vertical-text {
			transform: rotate(-90deg);
			transform-origin: left bottom; /* Adjust this to change the rotation origin */
			white-space: nowrap; /* Prevents text from wrapping */
		}
		.td-divider {
			border-top: hidden !important;
			border-bottom: hidden !important;
			background-color: #F2F2F2;
		}
	</style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<div class="container-fluid contenedor_fup" id="app">
		<div class="row">
             <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				<button @click="ChangeLanguage('es')" type="button" class="btn btn-secondary langes">
					            <img alt="español" src="Imagenes/colombia.png" /></button>
				            <button @click="ChangeLanguage('en')" type="button" class="btn btn-secondary langen">
					            <img alt="ingles" src="Imagenes/united-states.png" /></button>
				            <button @click="ChangeLanguage('pt')" type="button" class="btn btn-secondary langbr">
					            <img alt="portugues" src="Imagenes/brazil.png" /></button>
			</div>
         </div>
		<div class="card">
				<div class="row">
					<div class="col-12 py-2" style="display: flex; justify-content: space-around; align-items: center;">
						<div id="country-filter-container">
							<label class="mr-2" data-i18n="[html]FUP_pais">Pais</label>
							<select id="countryCmb" class="select-filter form-control" v-model="filterCountryId"
								data-width="fit" data-live-search="true" @change="onChangePais">
							</select>
						</div>
						<div id="city-filter-container">
							<label class="mr-2" data-i18n="[html]FUP_ciudad">Ciudad</label>
							<select id="cityCmb" class="select-filter form-control" v-model="filterCityId"
								data-width="fit" data-live-search="true" @change="onChangeCiudad">
							</select>
						</div>
						<div id="client-filter-container">
							<label class="mr-2" data-i18n="[html]FUP_empresa">Cliente</label>
							<select id="clientCmb" class="select-filter form-control" v-model="filterClientId"
								data-width="fit" data-live-search="true">
							</select>
						</div>
						<div id="period-filter-container">
							<label class="mr-2" data-i18n="[html]FUP_empresa">Periodo</label>
							<input type="month" v-model="filterPeriod"/>
						</div>
						<div>
							<button type="button" class="btn btn-primary" @click="consultar()">
								<i class="fa fa-search" style="margin-left: -200%;">
								</i>
							</button>
						</div>
					</div>
			  </div>
		</div>
		<div class="row">
			<ul class="nav nav-tabs mt-3" style="padding-left: 15px" id="myTab" role="tablist">
				<li class="nav-item">
					<a class="nav-link active" id="resumen-tab" data-toggle="tab" href="#resumen" role="tab" aria-controls="resumen" aria-selected="true">Resumen</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" id="comparativo-tab" data-toggle="tab" href="#comparativo" role="tab" aria-controls="comparativo" aria-selected="false">Comparativo</a>
				</li>
			</ul>
			<div class="tab-content col-12" id="myTabContent">
				<div class="tab-pane fade show active" id="resumen" role="tabpanel" aria-labelledby="resumen-tab">
					<table class="table table-sm table-bordered table-striped text-right" v-if="listComparativo.nivel1 != null">
						<thead>
							<tr>
								<th colspan="9" class="bg-info text-center">
									Comparativo de costos Simulador Vs Costos Agregado al Periodo
								</th>
							</tr>
							<tr style="height: 10px; border-right: hidden; border-left: hidden;"></tr>
							<tr>
								<th colspan="2" style="
									background-color: #F2F2F2;
									border: hidden;"></th>

								<th class="td-divider"></th>
								<th>SIMULADOR</th>
								<th class="td-divider"></th>
								<th>COSTOS</th>
								<th class="td-divider"></th>
								<th>VARIACION</th>
								<th>%</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td width= "5%" style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;"></td>
								<td width="10%">Ordenes</td>
								<td width="2%" class="td-divider"></td>
								<td width="30%" style="display:flex; flex-wrap:wrap; width: 100%; justify-content: space-around; gap: 3px">
									<span class="badge badge-primary" v-for="value in listComparativo.nivel1.Orden.split(',')"
										style="font-size: small;">
										{{ value }}
									</span>
								</td>
								<td width="2%" class="td-divider"></td>
								<td width="30%" style="display:flex; flex-wrap:wrap; width: 100%; justify-content: space-around; gap: 3px">
									<span class="badge badge-primary" v-for="value in listComparativo.nivel1.Ordenes_real.split(',')"
										style="font-size: small;">
										{{ value }}
									</span>
								</td>
								<td width="2%" class="td-divider"></td>
								<td width="7%"></td>
								<td width="7%"></td>
							</tr>
							<tr>
								<td width= "5%" style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;
									border-right: hidden;"></td>
							</tr>
							<tr>
								<td class="font-weight-bold" rowspan="8"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">Aluminio</span>
								</td>
								<td>M2</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2xItem.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2_real.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_m2.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_m2.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CantPiezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.Unidades_real.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas == "NaN") ? '' : listComparativo.nivel1.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Kilos Aluminio</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.PesoxItem.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.Peso_real.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_peso.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_peso == "NaN") ? '' : listComparativo.nivel1.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.Costo_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo == "NaN") ? '' : listComparativo.nivel1.percentage_costo.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Costo_real / listComparativo.nivel1.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CantPiezas / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Unidades_real / listComparativo.nivel1.M2_real).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_piezas_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Kg/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.PesoxItem / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Peso_real / listComparativo.nivel1.M2_real).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_kg_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_kg_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_kg_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.M2xItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Costo_real / listComparativo.nivel1.M2_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_m2.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_costo_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" v-if="listComparativo.nivel2 != null"></tr>
							<tr v-if="listComparativo.acero != null">
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">
										Acero
									</span>
								</td>
								<td>Costo total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.Costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.CostoReal.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.DifecenteCostoTotal.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.acero.PercentageCostoTotal == "NaN") ? '' : listComparativo.acero.PercentageCostoTotal.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.acero != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.Piezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.PiezasReal.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.DifecentePiezasTotal.toFixed(2)}}</td>
								<td>{{(listComparativo.acero.PercentagePiezasTotal == "NaN") ? '' : listComparativo.acero.PercentagePiezasTotal.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.acero != null">
								<td>Kg de Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.Kg.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.KgReal.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.DifecenteKgTotal.toFixed(2)}}</td>
								<td>{{(listComparativo.acero.PercentageKgTotal == "NaN") ? '' : listComparativo.acero.PercentageKgTotal.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.acero != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.CostoKg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.CostoKgReal.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.acero.CostoKg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.acero.CostoKgReal == "NaN") ? '' : listComparativo.acero.CostoKgReal.toFixed(2) + '%'}}</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div class="tab-pane fade" id="comparativo" role="tabpanel" aria-labelledby="comparativo-tab">
					<table class="table table-sm table-bordered table-striped text-right" v-if="listComparativo.nivel1 != null">
						<thead>
							<tr>
								<th colspan="9" class="bg-info text-center">
									Comparativo de costos Simulador Vs Costos Agregado al Periodo
								</th>
							</tr>
							<tr style="height: 10px; border-right: hidden; border-left: hidden;"></tr>
							<tr>
								<th colspan="2" style="
									background-color: #F2F2F2;
									border: hidden;"></th>

								<th class="td-divider"></th>
								<th>SIMULADOR</th>
								<th class="td-divider"></th>
								<th>COSTOS</th>
								<th class="td-divider"></th>
								<th>VARIACION</th>
								<th>%</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td width= "5%" style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;"></td>
								<td width="10%">Ordenes</td>
								<td width="2%" class="td-divider"></td>
								<td width="30%" style="display:flex; flex-wrap:wrap; width: 100%; justify-content: space-around; gap: 3px">
									<span class="badge badge-primary" v-for="value in listComparativo.nivel1.Orden.split(',')"
										style="font-size: small;">
										{{ value }}
									</span>
								</td>
								<td width="2%" class="td-divider"></td>
								<td width="30%" style="display:flex; flex-wrap:wrap; width: 100%; justify-content: space-around; gap: 3px">
									<span class="badge badge-primary" v-for="value in listComparativo.nivel1.Ordenes_real.split(',')"
										style="font-size: small;">
										{{ value }}
									</span>
								</td>
								<td width="2%" class="td-divider"></td>
								<td width="7%"></td>
								<td width="7%"></td>
							</tr>
							<tr>
								<td width= "5%" style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;
									border-right: hidden;"></td>
							</tr>
							<tr>
								<td class="font-weight-bold" rowspan="14"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">Aluminio</span>
								</td>
								<td>M2</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2xItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CantPiezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.Unidades_real.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas == "NaN") ? '' : listComparativo.nivel1.percentage_piezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Kilos Aluminio</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.PesoxItem.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.Peso_real.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_peso.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_peso == "NaN") ? '' : listComparativo.nivel1.percentage_peso.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MP</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoMpxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoMP_Real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mp.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mp == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mp.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MP/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoMpxItem / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoMP_Real / listComparativo.nivel1.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mp_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mp_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mp_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MOD</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorMOD_item.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorMOD_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mod.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mod == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mod.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MOD/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorMOD_item / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorMOD_real / listComparativo.nivel1.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mod_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mod_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mod_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo CIF</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorCIF_Item.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorCIF_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_cif.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_cif == "NaN") ? '' : listComparativo.nivel1.percentage_costo_cif.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo CIF/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorCIF_Item / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorCIF_real / listComparativo.nivel1.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_cif_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_cif_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_cif_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.Costo_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo == "NaN") ? '' : listComparativo.nivel1.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Costo_real / listComparativo.nivel1.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CantPiezas / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Unidades_real / listComparativo.nivel1.M2_real).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_piezas_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Kg/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.PesoxItem / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Peso_real / listComparativo.nivel1.M2_real).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_kg_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_kg_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_kg_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.M2xItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.Costo_real / listComparativo.nivel1.M2_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_m2.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_costo_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" v-if="listComparativo.nivel2 != null"></tr>
							<tr v-if="listComparativo.nivel2 != null">
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N2</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.Costo_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel2.percentage_costo == "NaN") ? '' : listComparativo.nivel2.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel2 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.Unidades_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel2.percentage_piezas == "NaN") ? '' : listComparativo.nivel2.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel2 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.Peso_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel2.percentage_peso == "NaN") ? '' : listComparativo.nivel2.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel2 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel2.CostoxItem / listComparativo.nivel2.PesoxItem) ? "$0.00" : (listComparativo.nivel2.CostoxItem / listComparativo.nivel2.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel2.Costo_real / listComparativo.nivel2.Peso_real) ? "$0.00" : (listComparativo.nivel2.Costo_real / listComparativo.nivel2.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel2.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel2.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" v-if="listComparativo.nivel3 != null"></tr>
							<tr v-if="listComparativo.nivel3 != null">
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N3</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.Costo_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel3.percentage_costo == "NaN") ? '' : listComparativo.nivel3.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel3 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.Unidades_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel3.percentage_piezas == "NaN") ? '' : listComparativo.nivel3.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel3 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.Peso_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel3.percentage_peso == "NaN") ? '' : listComparativo.nivel3.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel3 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel3.CostoxItem / listComparativo.nivel3.PesoxItem) ? "$0.00" : (listComparativo.nivel3.CostoxItem / listComparativo.nivel3.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel3.Costo_real / listComparativo.nivel3.Peso_real) ? "$0.00" : (listComparativo.nivel3.Costo_real / listComparativo.nivel3.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel3.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel3.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" v-if="listComparativo.nivel4 != null"></tr>
							<tr v-if="listComparativo.nivel4 != null">
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N4</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.Costo_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel4.percentage_costo == "NaN") ? '' : listComparativo.nivel4.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel4 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.Unidades_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel4.percentage_piezas == "NaN") ? '' : listComparativo.nivel4.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel4 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.Peso_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel4.percentage_peso == "NaN") ? '' : listComparativo.nivel4.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel4 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel4.CostoxItem / listComparativo.nivel4.PesoxItem) ? "$0.00" : (listComparativo.nivel4.CostoxItem / listComparativo.nivel4.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel4.Costo_real / listComparativo.nivel4.Peso_real) ? "$0.00" : (listComparativo.nivel4.Costo_real / listComparativo.nivel4.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel4.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel4.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" v-if="listComparativo.nivel5 != null"></tr>
							<tr v-if="listComparativo.nivel5 != null">
								<td  class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N5</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.Costo_real.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel5.percentage_costo == "NaN") ? '' : listComparativo.nivel5.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel5 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.Unidades_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel5.percentage_piezas == "NaN") ? '' : listComparativo.nivel5.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel5 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.Peso_real.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel5.percentage_peso == "NaN") ? '' : listComparativo.nivel5.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel5 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel5.CostoxItem / listComparativo.nivel5.PesoxItem) ? "$0.00" : (listComparativo.nivel5.CostoxItem / listComparativo.nivel5.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel5.Costo_real / listComparativo.nivel5.Peso_real) ? "$0.00" : (listComparativo.nivel5.Costo_real / listComparativo.nivel5.Peso_real).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel5.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel5.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
		</div>

	</div>
</asp:Content>
