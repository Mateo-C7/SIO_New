<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormCargueCostoReal.aspx.cs" Inherits="SIO.FormCargueCostoReal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
		<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
		<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
		<script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js" } 
            }
		</script>

		<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
		<script type="text/javascript" src="Scripts/select2.min.js"></script>
        <script type="module" src="Scripts/bootstrap-select.min.js"></script>
		<script type="text/javascript" src="Scripts/toastr.min.js"></script>
		<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
		<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>
        <script type="text/javascript" src="Scripts/Datatables_Scripts/datatables.min.js"></script>

        <script type="module" src="Scripts/formCargueCostoReal.js"></script>

		<link rel="Stylesheet" href="Content/bootstrap.min.css" />
		<link rel="Stylesheet" href="Content/SIO.css" />
		<link rel="stylesheet" href="Content/font-awesome.css" />
        <link rel="Stylesheet" href="Content/bootstrap-select.css" />
        <link rel="Stylesheet" href="Scripts/Datatables_Scripts/datatables.min.css" />
		<link href="Content/toastr.min.css" rel="stylesheet" />
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
					<img alt="español" src="Imagenes/colombia.png" /></button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" /></button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" /></button>
			</div>
         </div>
		<div>
			<div class="row">
				<div class="p-2 bg-light border col-12" :key="indexStageTable">
					<div class="row col-12">
						<!-- Button trigger modal -->
						<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#loadExcelFileModal">
							<i class="fa fa-file-excel-o" style="padding-right: inherit"></i>
							<span class="ml-2">Subir archivo</span>
						</button>
						<button v-if="items.some(x => x.IsNew === true)" type="button" class="btn btn-success ml-2" v-on:click="confirmSaveNewRows()">
							<i class="fa fa-check-circle-o" style="padding-right: inherit"></i>
							<span class="ml-2">Guardar Cambios</span>
						</button>
						<button v-if="items.some(x => x.IsNew === true)" type="button" class="btn btn-danger ml-2" v-on:click="confirmClearStagedRows()">
							<i class="fa fa-trash-o" style="padding-right: inherit"></i>
							<span class="ml-2">Limpiar Cambios</span>
						</button>
					</div>
					<table class="table table-striped text-center" id="stageTable" style="width: 100%">
						<thead>
							<tr>
								<th>Acciones</th>
								<th>Año</th>
								<th>Mes</th>
								<th>Tipo</th>
								<th>No_Orden</th>
								<th>Cliente</th>
								<th>País</th>
								<th>NAC_EXP</th>
								<th>M2_Vend</th>
								<th>Precio_M2_USD</th>
								<th>Und_Vend</th>
								<th>Kg_Vend</th>
								<th>Und_M2</th>
								<th>M2_Cot</th>
								<th>Vr_Cot_M2</th>
								<th>USD</th>
								<th>OtrosIngresosUSD</th>
								<th>IngresosCOP</th>
								<th>MP_Aluminio</th>
								<th>MP_Plastico</th>
								<th>Kanban</th>
								<th>Stock</th>
								<th>Consumibles</th>
								<th>MOD</th>
								<th>CIF</th>
								<th>TotalAluminio</th>
								<th>Nivel1_Acc</th>
								<th>Nivel2_Acc</th>
								<th>Nivel3_Acc</th>
								<th>Nivel4_Acc</th>
								<th>Nivel5_Acc</th>
								<th>Cons_Obra</th>
								<th>Total_Acc_Almacen</th>
								<th>Nivel1_Acf</th>
								<th>Nivel2_Acf</th>
								<th>Nivel3_Acf</th>
								<th>Nivel4_Acf</th>
								<th>Nivel5_Acf</th>
								<th>TotalAccFabricados</th>
								<th>PLY_STEEL</th>
								<th>Costo_Total</th>
								<th>Porc</th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="(item, index) in items">
								<td>
									<button v-if="item.IsNew === true" type="button" class="btn btn-danger ml-1" v-on:click="RemoveItem(index)">
										<i class="fa fa-times" style="margin-left: -200%"></i>
									</button>
								</td>
								<td>{{item.Anio}}</td>
								<td>{{item.Mes}}</td>
								<td>{{item.Tipo}}</td>
								<td>{{item.NoOrden}}</td>
								<td>{{item.Cliente}}</td>
								<td>{{item.Pais}}</td>
								<td>{{item.NacEx}}</td>
								<td>{{item.M2Vend}}</td>
								<td>{{item.PrecioM2USD}}</td>
								<td>{{item.UndVend}}</td>
								<td>{{item.KgVend}}</td>
								<td>{{item.UndM2}}</td>
								<td>{{item.M2Cot}}</td>
								<td>{{item.VrCot}}</td>
								<td>{{item.USD}}</td>
								<td>{{item.OtrosUSD}}</td>
								<td>{{item.COP}}</td>
								<td>{{item.MpAluminio}}</td>
								<td>{{item.MpPlastico}}</td>
								<td>{{item.Kanban}}</td>
								<td>{{item.Stock}}</td>
								<td>{{item.Consumible}}</td>
								<td>{{item.MOD}}</td>
								<td>{{item.CIF}}</td>
								<td>{{item.TotalAluminio}}</td>
								<td>{{item.Nivel1Almacen}}</td>
								<td>{{item.Nivel2Almacen}}</td>
								<td>{{item.Nivel3Almacen}}</td>
								<td>{{item.Nivel4Almacen}}</td>
								<td>{{item.Nivel5Almacen}}</td>
								<td>{{item.ConsObra}}</td>
								<td>{{item.TotalAccAlmacen}}</td>
								<td>{{item.Nivel1Fabricados}}</td>
								<td>{{item.Nivel2Fabricados}}</td>
								<td>{{item.Nivel3Fabricados}}</td>
								<td>{{item.Nivel4Fabricados}}</td>
								<td>{{item.Nivel5Fabricados}}</td>
								<td>{{item.TotalAccFabricados}}</td>
								<td>{{item.Acero}}</td>
								<td>{{item.CostoTotal}}</td>
								<td>{{item.Porc}}</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
		</div>

		<!-- Modal for load file -->
		<div class="modal fade" id="loadExcelFileModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
			<div class="modal-dialog" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h5 class="modal-title" id="exampleModalLabel">Cargar Excel Costos Real</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
							<span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-6 form-group">
								<label>Año</label>
								<select class="form-control" id="cboYear" v-model="yearLoad">
									<option value="0">-- Seleccionar --</option>
								</select>
							</div>
							<div class="col-6 form-group">
								<label>Mes</label>
								<select class="form-control" id="cboMonth" v-model="monthLoad">
									<option value="0">-- Seleccionar --</option>
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-12 form-group">
								<label>Archivo</label>
								<input type="file" class="form-control" ref="inpFileExcelCostoReal" />
							</div>
						</div>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
						<button type="button" class="btn btn-primary" v-on:click="verifyInputsToLoadExcel()">Cargar</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</asp:Content>
