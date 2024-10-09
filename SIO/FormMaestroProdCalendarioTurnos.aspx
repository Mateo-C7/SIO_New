<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormMaestroProdCalendarioTurnos.aspx.cs" Inherits="SIO.FormMaestroProdCalendarioTurnos" Culture="en-US" UICulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

	<script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
	<script type="text/javascript" src="Scripts/FormMaestroProdCalendarioTurnos.js?v=202101320"></script>
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
			<!-- Modal Producto Calendario Turnos -->
			<div class="modal fade" id="ModControlInsertar" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Producto Calendario Turnos</h5>
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
								<div class="col-12">Id</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control" id="txtId" />								
							</div>
							<div class="row">
								<div class="col-12">Fecha</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<div class="col-12">
									<input type="date" id="txtFecha" />
								</div>
							</div>
							<div class="row">
								<div class="col-12">Cant Turnos Col</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtCantTurnos" type="number" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Cant Turnos Bra</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtCantTurnosBr" type="number" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Semana del Mes</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input id="txtSemanaMes" type="number" min="0" />
							</div>
							<div class="row">
								<div class="col-12">Mes del Turno</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<select id="cboMesTurno" class="form-control select-filter">
								</select>
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
			  <div class="col-8" style="font-size: x-large">Producto Calendario Turnos</div>
			</div>

			<!-- Tab content -->
			<div id="Tab1" class="tabcontent">

            <div class="row">
                <div class="col-2">
                    <table id="tbContratomrv" class="table table-sm table-borderless Solomrv">
                        <thead>                   
                            <tr align="right">
                                <td><a style="font-size:14px; font-weight: bold"></a></td>
                            </tr>
                            <tr align="right">
                                <td><a style="font-size:14px; font-weight: bold"></a></td>
                            </tr>
                            <tr align="right">
                                <td><a style="font-size:14px; font-weight: bold"></a></td>
                            </tr>
                        </thead>
                    </table>
                </div>
				<div class="col-8 medium font-weight-bold">
					<table id="tbCalendarioTurnos" class="table table-sm">
                        <thead>
                                <tr class="thead-light" align="center">
                                    <th colspan="6" align="center" >Período</th>
									<th colspan="6" align="center" ></th>
                                </tr>
                                <tr>
                                    <th colspan="2">Fecha Inicio</th>
                                    <th colspan="2"><input id="txtFechaIni" type="date" /></th>
                                    <th colspan="2">Fecha Final</th>
                                    <th colspan="2"><input id="txtFechaFin" type="date" /></th>
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
                                <td colspan="6"> <button id="btnBuscar" type="button" onclick="Buscar('tab_pr');" class="btn btn-primary" value="0">
									<i class="fa fa-search fa-xs"></i> <span >. Buscar por Fechas</span>
								    </button>
								</td>
                            </tr>
                        </tfoot>
					</table>
				</div>
                <div class="col-2"></div>
			</div>

			<div class="row">
				<div class="col-4">
					<button id="btnInsertaPrecio" type="button" class="btn btn-outline-primary" onclick="InsertarPv();">
						<i class="fa fa-plus fa-xs"></i> <span>. Insertar</span>
					</button>
				</div>
			</div>
			<div class="row"></div>
			<div class="row">
			</div>
				<div class="row">
					<table class="table table-sm table-success" id="tab_pr">
						<thead>
							<tr class="table-info">
								<th class="text-center" >Id </th>
								<th class="text-center" >Fecha</th>
								<th class="text-center" >Cant Turnos Col</th>
								<th class="text-center" >Cant Turnos Bra</th>
								<th class="text-center" >Semana Mes</th>
								<th class="text-center" >Mes</th>
<%--								<th class="text-center" >Usuario Crea</th>
								<th class="text-center" >Fecha Crea</th>--%>
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
