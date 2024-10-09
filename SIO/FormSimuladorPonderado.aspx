<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormSimuladorPonderado.aspx.cs" Inherits="SIO.FormSimuladorPonderado" %>
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

	<script type="module" src="Scripts/formSimuladorPonderado.js?v=20231004A"></script>

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
							</tr>
						</tbody>
					</table>
					</div>
			  </div>
		</div>
		<div class="row">
			<ul class="nav nav-tabs mt-3 col-12" style="padding-left: 15px" id="myTab" role="tablist">
				<li class="nav-item">
					<a class="nav-link active" id="resumen-tab" data-toggle="tab" href="#resumen" role="tab" aria-controls="resumen" aria-selected="true">Resumen</a>
				</li>
				<li class="nav-item">
					<a class="nav-link" id="comparativo-tab" data-toggle="tab" href="#comparativo" role="tab" aria-controls="comparativo" aria-selected="false">Comparativo</a>
				</li>
			</ul>
			<div class="tab-content col-8" id="myTabContent">
				<div class="tab-pane fade show active" id="resumen" role="tabpanel" aria-labelledby="resumen-tab">
					<table class="table table-sm table-bordered table-striped text-right" >
						<thead>
							<tr>
								<th colspan="9" class="bg-info text-center">
									Ponderado de Costos
								</th>
							</tr>
							<tr style="height: 10px; border-right: hidden; border-left: hidden;"></tr>
							<tr>
								<th colspan="2" style="
									background-color: #F2F2F2;
									border: hidden;"></th>

								<th class="td-divider"></th>
								<th>TOTAL</th>
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
								<td>Explosión Material</td>
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
							</tr>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;"></td>
								<td>Ordenes</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td class="font-weight-bold" rowspan="9"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">Aluminio</span>
								</td>
								<td>Tasa TRM</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Kilos Aluminio</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Piezas/M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Kg/M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo/M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" ></tr>
							<tr >
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">
										Acero
									</span>
								</td>
								<td>Costo total</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Kg de Acero</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div class="tab-pane fade" id="comparativo" role="tabpanel" aria-labelledby="comparativo-tab">
					<table class="table table-sm table-bordered table-striped text-right" >
						<thead>
							<tr>
								<th colspan="9" class="bg-info text-center">
									Ponderado de Costos
								</th>
							</tr>
							<tr style="height: 10px; border-right: hidden; border-left: hidden;"></tr>
							<tr>
								<th colspan="2" style="
									background-color: #F2F2F2;
									border: hidden;"></th>

								<th class="td-divider"></th>
								<th>TOTAL</th>
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
								<td>Explosión Material</td>
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
							</tr>
							<tr>
								<td style="
									background-color: #F2F2F2;
									border-top: hidden;
									border-left: hidden;"></td>
								<td>Ordenes</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td class="font-weight-bold" rowspan="15"
									style="vertical-align : middle;text-align:center;rotate: -90deg; width: 5%">
									<span style="font-size: 20px">Aluminio</span>
								</td>
								<td>Tasa TRM</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Kilos Aluminio</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo MP</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo MP/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo MOD</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo MOD/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo CIF</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo CIF/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Piezas/M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Kg/M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr>
								<td>Costo/M2</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" ></tr>
							<tr >
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N2</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" ></tr>
							<tr >
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N3</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" ></tr>
							<tr >
								<td class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N4</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr style="height: 20px; border-right: hidden; border-left: hidden;" ></tr>
							<tr >
								<td  class="font-weight-bold" rowspan="4"
									style="vertical-align : middle;text-align:center; font-size: 20px">N5</td>
								<td>Costo Total</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Piezas</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Kg Acero</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
							<tr >
								<td>Costo/Kg</td>
								<td class="td-divider"></td>
								<td>0</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
