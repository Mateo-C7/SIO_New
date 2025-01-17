﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormSimuladorComparativoModFin.aspx.cs" Inherits="SIO.FormSimuladorComparativoModFin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="importmap">
		{
			"imports": { "vue": "./Scripts/vue.esm-browser.js", 
						"vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js" } 
		}
	</script>

	<script type="module" src="Scripts/formSimuladorComparativoModFin.js?v=20231004A"></script>

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

	<div id="loader" style="display: none">
		<h3>Procesando...</h3>
	</div>
	<div id="ohsnap"></div>

	<div class="container-fluid contenedor_fup" id="app">
		<div class="row">
             <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				<button type="button" class="btn btn-secondary langes">
					<img alt="español" src="Imagenes/colombia.png" />
				</button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" />
				</button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" />
				</button>
			</div>
         </div>
		<div class="card">
				<div class="row">
					<div class="col-1"></div>
					<div class="col-12">
					<table class="table table-sm table-hover mb-0" id="tbSearchFup">
						<tbody>
							<tr>
								<td align="center" data-i18n="[html]FUP_fup">FUP</td>
								<td style="width: 90px;">
									<input v-model="fupId" type="number" min="0" class="form-control  bg-warning text-dark" />
								</td>
								<td style="width: 90px;">
									<button type="button" @click="obtenerVersionPorIdFup()" class="btn btn-primary" data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search" style="margin-left: -200%"></i></button>
								</td>
								<td align="center" data-i18n="[html]FUP_orden">Orden Fabricación</td>
								<td style="width: 90px;">
									<input v-model="ordenFab" id="txtIdOrden" type="text" class="form-control  bg-warning text-dark" />
								</td>
								<td style="width: 90px;">
									<button type="button" class="btn btn-primary" @click="obtenerVersionPorOrden()" data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search" style="margin-left: -200%"></i></button>
								</td>

								<td align="center" <%--data-i18n="[html]FUP_OrdenCliente"--%>></td>
								<td style="width: 90px;">
									
								</td>
								<td style="width: 90px;">
									
								</td>

							</tr>
						</tbody>
					</table>
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
									Comparativo de costos Simulador CT Vs Modulacion Final
								</th>
							</tr>
							<tr style="height: 10px; border-right: hidden; border-left: hidden;"></tr>
							<tr>
								<th colspan="2" style="
									background-color: #F2F2F2;
									border: hidden;"></th>

								<th class="td-divider"></th>
								<th>SIMULADOR CT</th>
								<th class="td-divider"></th>
								<th>MODULACION FINAL</th>
								<th class="td-divider"></th>
								<th>VARIACION</th>
								<th>%</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-bottom: hidden;
									border-left: hidden;"></td>
								<td>Fecha</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.FechaSimulacion}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.FechaModFinal}}</td>
								<td class="td-divider"></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-bottom: hidden;
									border-left: hidden;"></td>
								<td>Fup</td>
								<td class="td-divider"></td>
								<td>{{fupId}}</td>
								<td class="td-divider"></td>
								<td>{{fupId}}</td>
								<td class="td-divider"></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;"></td>
								<td>Ordenes</td>
								<td class="td-divider"></td>
								<td>CT - {{listComparativo.nivel1.Orden}}</td>
								<td class="td-divider"></td>
								<td>CI - {{listComparativo.nivel1.Orden}}</td>
								<td class="td-divider"></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td class="font-weight-bold" rowspan="9"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">Aluminio</span>
								</td>
								<td>Tasa TRM</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.tasaCotizacion.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.tasaModFinal.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_tasa_dolar.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_tasa_dolar == "NaN") ? '' : listComparativo.nivel1.percentage_tasa_dolar.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>M2</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2xItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2xItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_m2.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_m2.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CantPiezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CantPiezasMf.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas == "NaN") ? '' : listComparativo.nivel1.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Kilos Aluminio</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.PesoxItem.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.PesoxItemMf.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_peso.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_peso == "NaN") ? '' : listComparativo.nivel1.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoxItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo == "NaN") ? '' : listComparativo.nivel1.percentage_costo.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItemMf / listComparativo.nivel1.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CantPiezas / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CantPiezasMf / listComparativo.nivel1.M2xItemMf).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_piezas_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Kg/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.PesoxItem / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.PesoxItemMf / listComparativo.nivel1.M2xItemMf).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_kg_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_kg_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_kg_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.M2xItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItemMf / listComparativo.nivel1.M2xItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
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
									Comparativo de costos Simulador CT Vs Modulacion Final
								</th>
							</tr>
							<tr style="height: 10px; border-right: hidden; border-left: hidden;"></tr>
							<tr>
								<th colspan="2" style="
									background-color: #F2F2F2;
									border: hidden;"></th>

								<th class="td-divider"></th>
								<th>SIMULADOR CT</th>
								<th class="td-divider"></th>
								<th>MODULACION FINAL</th>
								<th class="td-divider"></th>
								<th>VARIACION</th>
								<th>%</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-bottom: hidden;
									border-left: hidden;"></td>
								<td>Fecha</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.FechaSimulacion}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.FechaModFinal}}</td>
								<td class="td-divider"></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-bottom: hidden;
									border-left: hidden;"></td>
								<td>Fup</td>
								<td class="td-divider"></td>
								<td>{{fupId}}</td>
								<td class="td-divider"></td>
								<td>{{fupId}}</td>
								<td class="td-divider"></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;"></td>
								<td>Ordenes</td>
								<td class="td-divider"></td>
								<td>CT - {{listComparativo.nivel1.Orden}}</td>
								<td class="td-divider"></td>
								<td>CI - {{listComparativo.nivel1.Orden}}</td>
								<td class="td-divider"></td>
								<td></td>
								<td></td>
							</tr>
							<tr>
								<td class="font-weight-bold" rowspan="15"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">Aluminio</span>
								</td>
								<td>Tasa TRM</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.tasaCotizacion.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.tasaModFinal.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_tasa_dolar.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_tasa_dolar == "NaN") ? '' : listComparativo.nivel1.percentage_tasa_dolar.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>M2</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2xItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.M2xItemMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CantPiezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CantPiezasMf.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas == "NaN") ? '' : listComparativo.nivel1.percentage_piezas.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Kilos Aluminio</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.PesoxItem.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.PesoxItemMf.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_peso.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2})}}</td>
								<td>{{(listComparativo.nivel1.percentage_peso == "NaN") ? '' : listComparativo.nivel1.percentage_peso.toLocaleString("en-US", {minimumFractionDigits: 0, maximumFractionDigits: 2}) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MP</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoMpxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoMpxItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mp.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mp == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mp.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MP/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoMpxItem / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoMpxItemMf / listComparativo.nivel1.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mp_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mp_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mp_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MOD</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorMOD_item.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorMOD_itemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mod.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mod == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mod.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo MOD/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorMOD_item / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorMOD_itemMf / listComparativo.nivel1.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_mod_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_mod_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_mod_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo CIF</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorCIF_Item.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.ValorCIF_ItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_cif.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_cif == "NaN") ? '' : listComparativo.nivel1.percentage_costo_cif.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo CIF/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorCIF_Item / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.ValorCIF_ItemMf / listComparativo.nivel1.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_cif_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_cif_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_cif_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoxItem.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.CostoxItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo == "NaN") ? '' : listComparativo.nivel1.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItemMf / listComparativo.nivel1.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_costo_kg.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel1.percentage_costo_kg == "NaN") ? '' : listComparativo.nivel1.percentage_costo_kg.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Piezas/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CantPiezas / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CantPiezasMf / listComparativo.nivel1.M2xItemMf).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_piezas_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_piezas_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_piezas_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Kg/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.PesoxItem / listComparativo.nivel1.M2xItem).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.PesoxItemMf / listComparativo.nivel1.M2xItemMf).toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel1.diference_kg_m2.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel1.percentage_kg_m2 == "NaN") ? '' : listComparativo.nivel1.percentage_kg_m2.toFixed(2) + '%'}}</td>
							</tr>
							<tr>
								<td>Costo/M2</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItem / listComparativo.nivel1.M2xItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{(listComparativo.nivel1.CostoxItemMf / listComparativo.nivel1.M2xItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
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
								<td>{{listComparativo.nivel2.CostoxItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel2.percentage_costo == "NaN") ? '' : listComparativo.nivel2.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel2 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.CantPiezasMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel2.percentage_piezas == "NaN") ? '' : listComparativo.nivel2.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel2 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.PesoxItemMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel2.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel2.percentage_peso == "NaN") ? '' : listComparativo.nivel2.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel2 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel2.CostoxItem / listComparativo.nivel2.PesoxItem) ? "$0.00" : (listComparativo.nivel2.CostoxItem / listComparativo.nivel2.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel2.CostoxItemMf / listComparativo.nivel2.PesoxItemMf) ? "$0.00" : (listComparativo.nivel2.CostoxItemMf / listComparativo.nivel2.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
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
								<td>{{listComparativo.nivel3.CostoxItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel3.percentage_costo == "NaN") ? '' : listComparativo.nivel3.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel3 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.CantPiezasMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel3.percentage_piezas == "NaN") ? '' : listComparativo.nivel3.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel3 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.PesoxItemMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel3.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel3.percentage_peso == "NaN") ? '' : listComparativo.nivel3.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel3 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel3.CostoxItem / listComparativo.nivel3.PesoxItem) ? "$0.00" : (listComparativo.nivel3.CostoxItem / listComparativo.nivel3.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel3.CostoxItemMf / listComparativo.nivel3.PesoxItemMf) ? "$0.00" : (listComparativo.nivel3.CostoxItemMf / listComparativo.nivel3.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
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
								<td>{{listComparativo.nivel4.CostoxItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel4.percentage_costo == "NaN") ? '' : listComparativo.nivel4.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel4 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.CantPiezasMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel4.percentage_piezas == "NaN") ? '' : listComparativo.nivel4.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel4 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.PesoxItemMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel4.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel4.percentage_peso == "NaN") ? '' : listComparativo.nivel4.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel4 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel4.CostoxItem / listComparativo.nivel4.PesoxItem) ? "$0.00" : (listComparativo.nivel4.CostoxItem / listComparativo.nivel4.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel4.CostoxItemMf / listComparativo.nivel4.PesoxItemMf) ? "$0.00" : (listComparativo.nivel4.CostoxItemMf / listComparativo.nivel4.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
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
								<td>{{listComparativo.nivel5.CostoxItemMf.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.diference_costo.toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td>{{(listComparativo.nivel5.percentage_costo == "NaN") ? '' : listComparativo.nivel5.percentage_costo.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel5 != null">
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.CantPiezas.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.CantPiezasMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.diference_piezas.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel5.percentage_piezas == "NaN") ? '' : listComparativo.nivel5.percentage_piezas.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel5 != null">
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.PesoxItem.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.PesoxItemMf.toFixed(2)}}</td>
								<td class="td-divider"></td>
								<td>{{listComparativo.nivel5.diference_peso.toFixed(2)}}</td>
								<td>{{(listComparativo.nivel5.percentage_peso == "NaN") ? '' : listComparativo.nivel5.percentage_peso.toFixed(2) + '%'}}</td>
							</tr>
							<tr v-if="listComparativo.nivel5 != null">
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel5.CostoxItem / listComparativo.nivel5.PesoxItem) ? "$0.00" : (listComparativo.nivel5.CostoxItem / listComparativo.nivel5.PesoxItem).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
								<td class="td-divider"></td>
								<td>{{isNaN(listComparativo.nivel5.CostoxItemMf / listComparativo.nivel5.PesoxItemMf) ? "$0.00" : (listComparativo.nivel5.CostoxItemMf / listComparativo.nivel5.PesoxItemMf).toLocaleString("en-US", {style:"currency", currency:"USD"})}}</td>
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
