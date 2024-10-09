<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormMaestroPreciosSimu.aspx.cs" Inherits="SIO.FormMaestroPreciosSimu" Culture="en-US" UICulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

	<script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
    <script type="text/javascript" src="Scripts/Datatables_Scripts/datatables.js"></script>
	<script type="text/javascript" src="Scripts/FormMaestroPreciosSimu.js?v=202101320"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/select2.min.js"></script>
	<script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
	<link rel="Stylesheet" href="Content/bootstrap.min.css" />
	<link rel="Stylesheet" href="Content/SIO.css" />
	<link rel="stylesheet" href="Content/font-awesome.css" />
	<link rel="Stylesheet" href="Content/css/select2.min.css" />
	<link href="Content/toastr.min.css" rel="stylesheet" />
    <link rel="Stylesheet" href="Scripts/Datatables_Scripts/datatables.min.css" />

	<script type="text/javascript">

	</script>

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<div id="loader" style="display: none">
		<h3>Procesando...</h3>
	</div>
	<div id="ohsnap"></div>

	<div class="container-fluid contenedor_fup">
		
		<div class="row">
			<!-- Modal Márgenes por Nivel -->
			<div class="modal fade" id="ModControlInsertar" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Margenes por Nivel / Región</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control" id="AreaControl" disabled />
								<input type="text" class="form-control" id="padreCambio" disabled hidden/>
								<input type="hidden" id="IdMargen" value="" />
							</div>
							<div class="row">
								<div class="col-12">
									<input type="text" class="form-control" id="txtTituloObs" />
								</div>
							</div>
							<div class="row">
								<div class="col-12"></div>
							</div>
							<div class="row">
								<div class="col-12">Nivel Piezas</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboNivel" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Grupo Pais</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboGrupoPais" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Pais</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboPais" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Porcentaje Márgen</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtPorcentajeMargen" type="number" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Porcentaje Márgen Mínimo</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtPorcentajeMargenMin" type="number" min="0" />
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnCntrlInserta" onclick="btnCntrlInsertaf()" class="btn btn-primary">Guardar</button>
						</div>
					</div>
				</div>
			</div>

			<!-- Modal Precio Venta -->
			<div class="modal fade" id="MaoControlInsertar" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Precio de Venta</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control" id="AreaControlMao" disabled />
								<input type="text" class="form-control" id="padreCambioMao" disabled hidden/>
								<input type="hidden" id="IdPrecioVenta" value="" />
							</div>
							<div class="row">
								<div class="col-12">
									<input type="text" class="form-control" id="txtTituloObsMao" />
								</div>
							</div>
							<div class="row">
								<div class="col-12"></div>
							</div>
							<div class="row">
								<div class="col-12">Item Cotización</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboItemCot" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Grupo Pais</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboGrupoPaisPv" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Pais</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboPaisPv" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Moneda</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboMonedaPv" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Cliente</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboCliente" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Precio</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtPrecioPv" type="number" min="0" />
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnCntrlInsertaMa" onclick="btnCntrlInsertaMaf()" class="btn btn-primary">Guardar</button>
						</div>
					</div>
				</div>
			</div>


		</div>    

		 <!-- Tab links -->
		 <div class="col-10">
			<div class="tab">
			  <button class="tablinks" onclick="SelectTab(event, 'Tab1')">Item Cotizacion</button>
			  <button class="tablinks" onclick="SelectTab(event, 'Tab2')">Margenes</button>
			  <button class="tablinks" onclick="SelectTab(event, 'Tab3')">Precio Venta</button>
			</div>

			<!-- Tab content -->
			<div id="Tab1" class="tabcontent">
				<div class="row"></div>
					<div class="col-8" style="font-size: x-large">Item Cotización</div>
				<div class="row">

				</div>
				<div class="row">
					
						<table class="table table-sm table-hover" id="tab_mp">
							<thead>
								<tr class="table-info">
									<th class="text-center" >Item Cotización</th>
									<th class="text-center" >Nivel</th>
									<th class="text-center" > </th>
								</tr>
							</thead>
							<tbody id="tbodyic">
							</tbody>
						</table>

				</div>
			
			   </div>

			<div id="Tab2" class="tabcontent">
				<div class="row"></div>
					<div class="col-8" style="font-size: x-large">Porcentaje Margenes por Nivel / Region</div>
					<div class="col-4">
						<button id="btnInsertaPorcentaje" type="button" class="btn btn-outline-primary" onclick="InsertarPj();">
							<i class="fa fa-plus fa-xs" style="padding-right: inherit;"></i><span>Insertar</span>
						</button>
					</div>
				
					<fieldset class="border rounded-3 p-3 my-2">
						<legend class="float-none w-auto px-3" style="font-size: x-large;">
						  Filtros
						</legend>
   
						<div class="row">
							<div class="col-4">
								<label>Filtro por Nivel Piezas</label>
								<input type="text" class="form-control" placeholder="Nivel" onkeyup="filterTableMp()" id="filterNivelMp"/>
							</div>
							<div class="col-4">
								<label>Filtro por Grupo Pais</label>
								<input type="text" class="form-control" placeholder="Grupo Pais" onkeyup="filterTableMp()" id="filterGrupoPaisMp"/>
							</div>
							<div class="col-4">
								<label>Filtro por Pais</label>
								<input type="text" class="form-control" placeholder="Pais" onkeyup="filterTableMp()" id="filterPaisMp"/>
							</div>
						</div>
					</fieldset>
					
				
				<div class="">
					<table class="table table-sm table-success" id="tab_ma">
						<thead>
							<tr class="table-info">
								<th class="text-center" >Nivel Piezas </th>
								<th class="text-center" >Grupo Pais</th>
								<th class="text-center" >Pais</th>
								<th class="text-center" >% Margen</th>
								<th class="text-center" >% Margen Min. </th>
								<th class="text-center" > </th>
							</tr>
						</thead>
						<tbody id="tbodyma">
						</tbody>
					</table>
				</div>
			</div> 

			<div id="Tab3" class="tabcontent">
				<div class="row"></div>
					<div class="col-8" style="font-size: x-large">Precios de Venta por Grupo / Region</div>
					<div class="col-4">
						<button id="btnInsertaPrecio" type="button" class="btn btn-outline-primary" onclick="InsertarPv();">
							<i class="fa fa-plus fa-xs" style="padding-right: inherit;"></i><span>Insertar</span>
						</button>

						<!-- <p>Cliente:</p> <input id="txtBusqueda" type="text";" />
						<button id="btnBuscarCliente" type="button" class="btn btn-outline-primary" onclick="Buscar('tab_pr');">
							<i class="fa-brands fa-searchengin fa-xs"></i> <span>Buscar</span>
						</button> -->
					</div>

				
				<fieldset class="border rounded-3 p-3 my-2">
						<legend class="float-none w-auto px-3" style="font-size: x-large;">
						  Filtros
						</legend>
   
						<div class="row">
							<div class="col-4">
								<label>Filtro Item</label>
								<input type="text" class="form-control" placeholder="Item" onkeyup="filterTablePv()" id="filterNivelPv"/>
							</div>
							<div class="col-4">
								<label>Filtro por Grupo Pais</label>
								<input type="text" class="form-control" placeholder="Grupo Pais" onkeyup="filterTablePv()" id="filterGrupoPaisPv"/>
							</div>
							<div class="col-4">
								<label>Filtro por Pais</label>
								<input type="text" class="form-control" placeholder="Pais" onkeyup="filterTablePv()" id="filterPaisPv"/>
							</div>
						</div>
					</fieldset>

				<div class="">
					<table class="table table-sm table-success" id="tab_pr">
						<thead>
							<tr class="table-info">
								<th class="text-center" >Item Cotizacion</th>
								<th class="text-center" >Grupo Pais</th>
								<th class="text-center" >Pais</th>
								<th class="text-center" >Moneda</th>
								<th class="text-center" >Precio</th>
								<th class="text-center" >Cliente</th>
								<th class="text-center" > </th>
							</tr>
						</thead>
						<tbody id="tbodypr">
						</tbody>
					</table>
				</div>
			</div> 
		</div>
	</div>

</asp:Content>
