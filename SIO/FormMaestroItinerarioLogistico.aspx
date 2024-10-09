<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormMaestroItinerarioLogistico.aspx.cs" Inherits="SIO.FormMaestroItinerarioLogistico" Culture="en-US" UICulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

	<script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
	<script type="text/javascript" src="Scripts/formMaestroItinerarioLogistico.js?v=2021009"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/select2.min.js"></script>
	<script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
	<link rel="Stylesheet" href="Content/bootstrap.min.css" />
	<link rel="Stylesheet" href="Content/SIO.css" />
	<link rel="stylesheet" href="Content/font-awesome.css" />
	<link rel="Stylesheet" href="Content/css/select2.min.css" />
	<link href="Content/toastr.min.css" rel="stylesheet" />

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
			<!-- Modal Maestro Itinerario -->
			<div class="modal fade" id="ModControlInsertar" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Itinerario Logístico</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control" id="AreaControl" disabled />
								<input type="text" class="form-control" id="padreCambio" disabled hidden/>
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
								<div class="col-4" style="vertical-align: middle; padding-top: 5px">Año</div>
								<div class="col-4">
									<select id="cboAnioEd" class="form-control select-filter">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">Origen Costo</div>
								<div class="col-6">
									<select id="cboOrigenCosto" class="form-control select-filter">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">Pais Destino</div>
								<div class="col-6">
									<select id="cboPaisDestino" class="form-control select-filter">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">Puerto Arribo</div>
								<div class="col-6">
									<select id="cboPuertoArribo" class="form-control select-filter">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">Fecha Cargue FORSA</div>
								<div class="col-6">
									<input type="date" id="txtFechaCargueForsa" />
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">ETA</div>
								<div class="col-6">
									<input id="txtETA" type="number" min="0" />
	<%--								<select id="cboETA" class="form-control select-filter">
									</select>--%>
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">TT</div>
								<div class="col-6">
									<select id="cboTT" class="form-control select-filter">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">RUTA</div>
								<div class="col-6">
									<select id="cboRuta" class="form-control select-filter">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-4 style="vertical-align: middle; padding-top: 5px">Naviera</div>
								<div class="col-6">
									<input id="txtNaviera" type="number" min="0" />
								</div>
							</div>
							<div class="row">
								<div class="col-3 style="vertical-align: middle; padding-top: 5px">ETD</div>
								<div class="col-3">
									<input id="txtETD" type="number" min="0" />
								</div>
								<div class="col-3 style="vertical-align: middle; padding-top: 5px">Tránsito Total</div>
								<div class="col-3">
									<input id="txtTransito" type="number" min="0" />
								</div>
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnCntrlInserta" onclick="btnCntrlInsertaf()" class="btn btn-primary">Guardar</button>
						</div>
					</div>
				</div>
			</div>

		</div>    
		
		 <!-- Tab links -->
		 <div class="col-10">
			<div class="tab">
			  <div class="col-8" style="font-size: x-large">Tiempos de Tránsito</div>
			</div>

			<!-- Tab content -->
			<div id="Tab1" class="tabcontent">
				<div class="row">
					<div class="col-4" style="font-size: x-large">Tiempos de Tránsito</div>
					<div class="col-2" style="font-size: x-large">Año</div>
					<div class="col-2" style="margin: 8px; background-position: center; font-size: medium; ">
						<select id="cboAnio" ></select>
					</div>
				</div>
				<div class="row">
					<div class="col-4">
						<button id="btnInsertaItinerario" type="button" class="btn btn-outline-primary" onclick=" InsertarMp();">
							<i class="fa fa-plus fa-xs"></i> <span>Insertar</span>
						</button>
					</div>
				</div>
				<div class="row">
				</div>
				<div class="row">					
						<table class="table table-sm table-hover" id="tab_mp">
							<thead>
								<tr class="table-primary">
									<th class="text-center" >Id</th>
									<th class="text-center" >Año</th>
									<th class="text-center" >Origen Costo</th>
									<th class="text-center" >Pais</th>
									<th class="text-center" >Pais Destino</th>
									<th class="text-center" >Puerto Arribo</th>
									<th class="text-center" >Fecha Cargue Forsa</th>
									<th class="text-center" >ETA</th>
									<th class="text-center" >TT</th>
									<th class="text-center" >Ruta</th>
									<th class="text-center" >Naviera</th>
									<th class="text-center" >ETD</th>
									<th class="text-center" >Tránsito Total</th>
									<th class="text-center" > </th>
								</tr>
							</thead>
							<tbody id="tbodymp">
							</tbody>
						</table>
				</div>            
			   </div>
		</div>
	</div>

</asp:Content>
