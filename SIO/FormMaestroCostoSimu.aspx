<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormMaestroCostoSimu.aspx.cs" Inherits="SIO.FormMaestroCostoSimu" Culture="en-US" UICulture="en-US" %>

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
	<script type="text/javascript" src="Scripts/formMaestrocostosimu.js?v=2021009"></script>
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
			<!-- Modal Materia Prima -->
			<div class="modal fade" id="ModControlInsertar" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Costos de Materia Prima</h5>
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
								<div class="col-12">Fecha Inicio Vigencia*</div>
							</div>
							<div class="row">
								<div class="col-12">
									<input type="date" id="txtFechaVigencia" />
								</div>
							</div>
							<div class="row">
								<div class="col-12">Planta</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboPlanta" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Origen*</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboOrigenMp" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Tipo de Materia Prima*</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboMateriaPrima" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Kilos</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtKilos" type="number" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Costo MP</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtCosto" type="number" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Costo Logistico</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtCosto2" type="number" min="0" />
							</div>
                            <div class="row">
								<div class="col-12">Valor Lme</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtValorLme" type="number" step="0.10" min="0" />
							</div>
                            <div class="row">
								<div class="col-12">Valor Lme2</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtValorLme2" type="number" step="0.10" min="0" />
							</div>
                            <div class="row">
								<div class="col-12">Observaciones</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
							    <textarea id="txtObservaciones" rows="5" cols="70"></textarea>
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnCntrlInserta" onclick="btnCntrlInsertaf()" class="btn btn-primary">Guardar</button>
						</div>
					</div>
				</div>
			</div>

			<!-- Modal Mano de Obra -->
			<div class="modal fade" id="MaoControlInsertar" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Costos de Mano de Obra</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control" id="AreaControlMao" disabled />
								<input type="text" class="form-control" id="padreCambioMao" disabled hidden/>
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
								<div class="col-12">Fecha Inicio Vigencia*</div>
							</div>
							<div class="row">
								<div class="col-12">
									<input type="date" id="txtFechaVigenciaMao" />
								</div>
							</div>
							<div class="row">
								<div class="col-12">Planta</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboPlantaMao" class="form-control select-filter">
								</select>
							</div>
                            <div class="row">
								<div class="col-12">Tipo de Formaleta*</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboFormaletaMao" class="form-control select-filter">
								</select>
							</div>
							<div class="row">
								<div class="col-12">Costo Mano de Obra</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtCostoMod" type="number" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Costo CIF</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtCostoCif" type="number" step="0.10" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Porcentaje de Chatarra</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtPorcentajeChatarra" type="number" step="0.10" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Porcentaje de Chatarra Piezas Más de 240 cm </div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtPorcentajeChatarraPiezas" type="number" step="0.10" min="0" />
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
		 <div class="col-12">
			<div class="tab">
			  <button class="tablinks" onclick="SelectTab(event, 'Tab1')">Materia Prima</button>
			  <button class="tablinks" onclick="SelectTab(event, 'Tab2')">Mano de Obra y CIF</button>
			</div>

			<!-- Tab content -->
			<div id="Tab1" class="tabcontent">
				<div class="row"></div>
					<div class="col-8" style="font-size: x-large">Costos Materia Prima</div>
                    <div class="col-8 offset-2 medium font-weight-bold">
					    <table id="tbCalendarioTurnos" class="table table-sm">
                            <thead>
                                    <tr class="thead-light" align="center">
                                        <th colspan="6" align="center" >Período</th>
									    <th colspan="6" align="center" ></th>
                                    </tr>
                                    <tr>
                                        <th colspan="2">Fecha Inicio</th>
                                        <th colspan="2"><input id="txtFechaIniMp" type="date" /></th>
                                        <th colspan="2">Fecha Final</th>
                                        <th colspan="2"><input id="txtFechaFinMp" type="date" /></th>
                                    </tr>
								    <tr class="thead-light">
									    <th colspan="6" align="center" ></th>
									    <th colspan="6" align="center" ></th>
								    </tr>
                            </thead>
						    <tbody id="tbodyCalendarioTurnos">
						    </tbody>
                            <tfoot>
                                <tr class="thead-light" align="left">
                                    <td colspan="6"> <input type="hidden" id="txtActiveFilterMp"/> <button id="btnBuscarMp" type="button" onclick="Buscar('MateriaPrima');" class="btn btn-primary" >
									    <i class="fa fa-search fa-xs"></i> <span >. Buscar por Fechas</span>
								        </button>
								    </td>
                                </tr>
                            </tfoot>
					    </table>
				    </div>
					<div class="col-4">
						<button id="btnInsertaMateriaPrima" type="button" class="btn btn-outline-primary" onclick=" InsertarMp();">
							<i class="fa fa-plus fa-xs" style="padding-right: inherit;"></i> <span>Insertar</span>
						</button>
					</div>
				<div class="row">

				</div>
				<div class="">
					
						<table class="table table-sm table-hover" id="tab_mp">
							<thead>
								<tr class="table-primary">
									<th class="text-center" >Fecha Vigencia</th>
									<th class="text-center" >Planta</th>
									<th class="text-center" >Origen Costo</th>
									<th class="text-center" >Tipo Mp</th>
									<th class="text-center" >Kilos</th>
									<th class="text-center" >Costo MP Kilo</th>
									<th class="text-center" >Costo MP</th>
									<th class="text-center" >Costo Logistico</th>
                                    <th class="text-center" >Valor Lme</th>
                                    <th class="text-center" >Valor Lme2</th>
                                    <th class="text-center" >Observaciones</th>
									<th class="text-center" > </th>
								</tr>
							</thead>
							<tbody id="tbodymp">
							</tbody>
						</table>

				</div>
			
			   </div>

			<div id="Tab2" class="tabcontent">
				<div class="row"></div>
					<div class="col-8" style="font-size: x-large">Costos Mano de Obra y Costos Indirectos</div>
                        <div class="col-8 offset-2 medium font-weight-bold">
					        <table id="tbCalendarioTurnosMo" class="table table-sm">
                                <thead>
                                    <tr class="thead-light" align="center">
                                        <th colspan="6" align="center" >Período</th>
									    <th colspan="6" align="center" ></th>
                                    </tr>
                                    <tr>
                                        <th colspan="2">Fecha Inicio</th>
                                        <th colspan="2"><input id="txtFechaIniMo" type="date" /></th>
                                        <th colspan="2">Fecha Final</th>
                                        <th colspan="2"><input id="txtFechaFinMo" type="date" /></th>
                                    </tr>
								    <tr class="thead-light">
									    <th colspan="6" align="center" ></th>
									    <th colspan="6" align="center" ></th>
								    </tr>
                                </thead>
						    <tbody id="tbodyCalendarioTurnosMo">
						    </tbody>
                            <tfoot>
                                <tr class="thead-light" align="left">
                                    <td colspan="6"> <input type="hidden" id="txtActiveFilterMo"/> <button id="btnBuscarMo" type="button" onclick="Buscar('ManoObra');" class="btn btn-primary" >
									    <i class="fa fa-search fa-xs"></i> <span >. Buscar por Fechas</span>
								        </button>
								    </td>
                                </tr>
                            </tfoot>
					    </table>
				    </div>
					<div class="col-4">
						<button id="btnInsertarManoObra" type="button" class="btn btn-outline-primary" onclick=" InsertarMo();">
							<i class="fa fa-plus fa-xs" style="padding-right: inherit;"></i> <span>Insertar</span>
						</button>
					</div>
				<div class="row"></div>
				<div class="">
						<table class="table table-sm table-hover" id="tab_mc">
							<thead>
								<tr class="table-primary">
									<th class="text-center" >Fecha Inicio Vigencia</th>
									<th class="text-center" >Planta</th>
                                    <th class="text-center" >Tipo Formaleta</th>
									<th class="text-center" >Costo MOD</th>
									<th class="text-center" >Costo CIF</th>
									<th class="text-center" >% Chatarra</th>
									<th class="text-center" >% Chatarra Piezas > 240 </th>
									<th class="text-center" > </th>
								</tr>
							</thead>
							<tbody id="tbodymc">
							</tbody>
						</table>
				</div>
			</div>

		</div>
	</div>

</asp:Content>
