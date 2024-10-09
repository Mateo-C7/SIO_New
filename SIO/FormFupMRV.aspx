<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormFupMRV.aspx.cs" Inherits="SIO.FormFupMRV" Culture="en-US" UICulture="en-US" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

	<script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
	<script type="text/javascript" src="Scripts/FormFupMRV.js?v=20211001A"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/select2.min.js"></script>
    <script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
	<script type="text/javascript" src="Scripts/moment.js?v=20200606"></script>
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
			<div class="btn-group col-2 align-self-end" role="group" aria-label="Basic example">
				<button type="button" class="btn btn-secondary langes">
					<img alt="español" src="Imagenes/colombia.png" /></button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" /></button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" /></button>
			</div>
            <div class="col-10">
                <img alt="MRV Logo" src="Imagenes/mrv-logo-8.png"  height="30" />
            </div>
			<!-- Modal -->
			<div class="modal fade" id="UploadFilesModal" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Modal title</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<div>
									<select id="TipoArchivoModal" class="">
										<option value="1">Listado</option>
<%--                                        <option value="2">Plano</option>
										<option value="3">Documento</option>
										<option value="4">Fotografia</option>
										<option value="5">Plano Tipo Forsa</option>
										<option value="6">Carta de Cotizacion</option>
										<option value="7">Plano Final Cliente</option>
										<option value="8">Memoria</option>
										<option value="9">Carta Cotización Final</option>--%>
									</select>
								</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="file" class="form-control-file" id="rutaArchivo" multiple />
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control-file" id="zonaArchivo" disabled />
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control-file" id="EventoPTF" disabled />
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnUploadFiles" class="btn btn-primary">Cargar Archivo</button>
						</div>
					</div>
				</div>
			</div>

			<!-- Modal de Clonación -->
			<div class="modal fade" id="ClonarFupModal" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Duplicar FUP</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row">
								<div class="col-3" data-i18n="[html]FUP_pais">
									Pais *
								</div>
								<div class="col-8">
									<select id="cboIdPaisClon" class="select-filter sfmodal">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-3" data-i18n="[html]FUP_ciudad">
									Ciudad *
								</div>
								<div class="col-8">
									<select id="cboIdCiudadClon" class="select-filter sfmodal">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-3" data-i18n="[html]FUP_empresa">
									Empresa *
								</div>
								<div class="col-10">
									<select id="cboIdEmpresaClon" class="select-filter sfmodal">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-3" data-i18n="[html]FUP_contacto">
									Contacto *
								</div>
								<div class="col-10">
									<select id="cboIdContactoClon" class="select-filter sfmodal">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-3" data-i18n="[html]FUP_obra">
									Obra *
								</div>
								<div class="col-10">
									<select id="cboIdObraClon" class="select-filter sfmodal">
									</select>
								</div>
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnDuplicaFup" class="btn btn-primary">Duplicar</button>
						</div>
					</div>
				</div>
			</div>

			<!-- Modal Reporte -->
			<div class="modal fade" id="ModReporteFup" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Modal title</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<rsweb:reportviewer ID="ReporteFUP" runat="server" Width="800px" Height="600px"> </rsweb:reportviewer>
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
						</div>
					</div>
				</div>
			</div>

            <!-- Modal Control de Cambios -->
			<div class="modal fade" id="ModControlCambios" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Adicionar Control de Cambio</h5>
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
								<div class="col-12">Titulo</div>
							</div>
                            <div class="row">
								<div class="col-12">
    								<input type="text" class="form-control" id="txtTituloObs" />
								</div>
							</div>
                            <div class="row">
								<div class="col-12">Notas y Observaciones</div>
							</div>
                            <div class="row">
								<div class="col-12">
    								<textarea id="txtObsCntrCm" class="form-control" rows="3"></textarea>
								</div>
							</div>
                            <div class="row">
								<div class="col-12">Anexos</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="file" class="form-control" id="rutaArchivo2" multiple />
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnCntrlCmb" class="btn btn-primary">Enviar</button>
						</div>
					</div>
				</div>
			</div>


		</div>
		  <div class="card">
				<div class="row">
					<div class="col-1"></div>
					<div class="col-12">
					<table class="table table-sm table-hover" id="tbSearchFup">
						<tbody>
							<tr>
								<td colspan="2" align="center" style="width: 90px;" data-i18n="[html]FUP_estado_fup"><h3>Estado FUP</h3>
								</td>
								<td colspan="3" align="center" style="width: 90px;">
									<div id="divEstadoFup" class="fupestado" style="font-weight: bold"></div>
								</td>
								<td colspan="2" align="center">
									<button id="btnNuevo" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_nuevo"><i class="fa fa-file"></i></button>
									<button id="btnFupBlanco" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_fup_blanco"><i class="fa  fa-file-text"></i></button>
									<button id="btnDuplicar" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_duplicar" ><i class="fa fa-copy"></i></button>
								</td>
								<td colspan="1" align="center" data-i18n="[html]FUP_fup">FUP</td>
								<td colspan="1" style="width: 90px;">
									<input id="txtIdFUP" type="number" min="0" class="form-control  bg-warning text-dark" />
								</td>
								<td colspan="1" style="width: 90px;">
									<button id="btnBusFup" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search"></i></button>
								</td>
								<td colspan="2" align="center" data-i18n="[html]FUP_orden">Orden Fabricación</td>
								<td colspan="1" style="width: 90px;">
									<input id="txtIdOrden" type="text" class="form-control  bg-warning text-dark" />
								</td>
								<td colspan="1" style="width: 90px;">
									<button id="btnBusOf" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search"></i></button>
								</td>

								<td colspan="3" align="center" <%--data-i18n="[html]FUP_OrdenCliente"--%>>Pedido Cliente</td>
								<td colspan="1" style="width: 90px;">
									<input id="txtIdOrdenCliente" type="number" min="0" class="form-control  bg-warning text-dark" />
								</td>
								<td colspan="1" style="width: 90px;">
									<button id="btnBusOrdenCliente" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search"></i></button>
								</td>

							</tr>
						</tbody>
					</table>
					</div>
			  </div>
		</div>

		<div id="accordion">
			<div class="card" id="DatosGen" >
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseDatosGenerales" data-i18n="FUP_datos_generales">DATOS GENERALES
					</a>
				</div>
				<div id="collapseDatosGenerales" class="collapse show" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-1" data-i18n="[html]FUP_pais">
								Pais *
							</div>
							<div class="col-2">
								<select id="cboIdPais" class="form-control select-filter">
								</select>
							</div>
							<div class="col-1" data-i18n="[html]FUP_ciudad">
								Ciudad *
							</div>
							<div class="col-2">
								<select id="cboIdCiudad" class="form-control select-filter">
								</select>
							</div>
							<div class="col-1" data-i18n="[html]FUP_empresa">
								Empresa *
							</div>
							<div class="col-5">
								<select id="cboIdEmpresa" data-modelo="ID_Cliente" class="form-control select-filter">
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-1" data-i18n="[html]FUP_contacto">
								Contacto *
							</div>
							<div class="col-5">
								<select id="cboIdContacto" data-modelo="ID_Contacto" class="form-control select-filter">
								</select>
							</div>
							<div class="col-1" data-i18n="[html]FUP_obra">
								Obra *
							</div>
							<div class="col-5">
								<select id="cboIdObra" data-modelo="ID_Obra" class="form-control select-filter">
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-1" data-i18n="[html]FUP_unds_construir">
								Total Unidades Obra*
							</div>
							<div class="col-2">
								<input id="txtIdUnidadesConstruir" type="number" min="0" class="form-control " data-modelo="TotalUnidadesConstruir" disabled />
							</div>
							<div class="col-1" data-i18n="[html]FUP_unds_construir_forsa">
								Unds Construir Forsa *
							</div>
							<div class="col-2">
								<input id="txtIdUnidadesConstruirForsa" type="number" min="0" class="form-control " data-modelo="TotalUnidadesConstruirForsa" />
							</div>
							<div class="col-1" data-i18n="[html]FUP_m2_vivienda">
								M² Vivienda *
							</div>
							<div class="col-2">
								<input id="txtIdMetrosCuadradosVivienda" type="number" min="0" class="form-control " data-modelo="MetrosCuadradosVivienda" />
							</div>
							<div class="col-1" data-i18n="[html]FUP_estrato">
								Estrato *
							</div>
							<div class="col-2">
								<select id="cboIdEstrato" class="form-control" data-modelo="Estrato">
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-1" data-i18n="[html]FUP_moneda">
								Moneda * 
							</div>
							<div class="col-2">
								<select data-modelo="ID_Moneda" id="cboIdMoneda" class="form-control ">
								</select>
							</div>
							<div class="col-1" data-i18n="[html]FUP_tipo_vivienda">
								Tipo Obra *
							</div>
							<div class="col-2">
								<select id="cboIdTipoVivienda" class="form-control " data-modelo="TipoVivienda">
								</select>
							</div>
							<div class="col-1" data-i18n="[html]FUP_clase_cotizacion" >
								Clase Cotización 
							</div>
							<div class="col-2" style="display: inline-table">
								<select id="cboClaseCotizacion" class="form-control" data-modelo="ClaseCotizacion" style="width: 60% !important;">
								</select> 
<%--								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Clase de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
							</div>
							<div class="col-1" data-i18n="[html]FUP_version">
								Version 
							</div>
							<div class="col-1">
								<select id="cboVersion" class="form-control ">
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-1" data-i18n="[html]FUP_estado_cliente">Estado Cliente</div>
							<div class="col-2">
								<input id="txtEstadoCliente" disabled="disabled" type="text" />
							</div>
							<div class="col-1" data-i18n="[html]FUP_probabilidad">Probabilidad</div>
							<div class="col-2">
								<input id="txtProbabilidad" disabled="disabled" type="text" />
							</div>
							<div class="col-1" data-i18n="[html]FUP_fecha_creacion">
								Fecha Creación: 
							</div>
							<div class="col-2">
								<input id="txtFechaCreacion" disabled="disabled" type="text" />
							</div>
							<div class="col-1" data-i18n="[html]FUP_fecha_solicitaCliente">
								Fecha en que Solicita el Cliente: 
							</div>
							<div class="col-2">
								<input id="txtFechaSolicitaCliente" type="date" data-modelo="FecSolicitaCliente" />
								<button id="btnGrabaFechaSolicita" class="btn SoloUpd" data-toggle="tooltip" onclick="GrabaFechasCliente(1)" data-i18n="[title]FUP_GrabarFechaSolicita"><i class="fa fa-floppy-o"></i></button>
							</div>
					   </div>
						<div class="row">
							<div class="col-1" data-i18n="[html]FUP_creado_por">
								Creado por 
							</div>
							<div class="col-5">
								<input id="txtCreadoPor" disabled="disabled" type="text" />
							</div>
							<div class="col-1" data-i18n="[html]FUP_cotizado_por">
								Cotizado por: 
							</div>
							<div class="col-4">
								<input id="txtCotizadoPor" disabled="disabled" type="text" />
							</div>
						</div>
<%--					</div>
				</div>
			</div>
			<div class="card">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" data-i18n="[html]FUP_informacion_general">INFORMACION GENERAL
					</a>
				</div>--%>
<%--				<div id="collapseTwo" class="collapse" data-parent="#accordion">
					<div class="card-body">--%>
                        <hr />
						<div class="row">
							<div class="col-1" data-i18n="[html]tipo_negociacion">Tipo de Negociacion</div>
							<div class="col-2">
								<select id="selectTipoNegociacion" data-modelo="TipoNegociacion">
									<option value="-1">Tipo Negociacion</option>
									<option value="1">Venta</option>
									<option value="2">Reparación</option>
									<option value="3">Arrendamiento</option>
								</select>

							</div>
							<div class="col-1" data-i18n="[html]tipo_cotizacion">Tipo de Cotizacion</div>
							<div class="col-2" style="display: inline-table">
								<select id="cboTipoCotizacion" data-modelo="TipoCotizacion" style="width: 80% !important;">
									<option value="-1">Tipo Cotizacion</option>
									<option value="1">Equipo Nuevo</option>
									<option value="2">Adaptaciones</option>
									<option value="3">Listados</option>
								</select>
<%--								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Tipo de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
							</div>
							<div class="col-1" data-i18n="[html]producto">Producto</div>
							<div class="col-2">
								<select id="selectProducto" data-modelo="Producto">
									<option value="-1">Producto</option>
									<option value="1">FORSA ALUM</option>
									<option value="2">FORSA PLUS</option>
									<option value="3">FORSA ACERO</option>
								</select>
							</div>
							<div class="col-3" style="display: inline-table">
                                <table><tr>
                                    <td><a style="font-size:14px; font-weight: bold" download="Premissas Projetos MRV R03_18-02-2021.xlsx" href="Imagenes//BR//Premissas Projetos MRV R03_18-02-2021.xlsx">Premissas Projetos</a></td>
                                    <td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo('Premissas Projetos MRV R03_18-02-2021.xlsx','Imagenes//BR//Premissas Projetos MRV R03_18-02-2021.xlsx')\"> </button></td></tr>
                                    <tr><td><a style="font-size:14px; font-weight: bold" download="Manual MRV.pdf" href="Imagenes//BR//Manual MRV.pdf">Manual FUP</a></td>
                                    <td><button type="button" class="fa fa-download" data-toggle="tooltip" title="Descargar" onclick="DescargarArchivo('Manual MRV.pdf','Imagenes//BR//Manual MRV.pdf')\"> </button></td>
                                </tr></table>
							</div>
						</div>

						<div class="row">
							<div class="col-6 medium font-weight-bold">
							    <div class="medium font-weight-bold text-center divvarof"> 
								    <span data-i18n="[html]orden_referencia" >Orden de Referencia</span>
									    <button id="btnAgregarOrdenReferencia" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i> </button>
								</div>
							    <div class="divContentOrdenReferencia divvarof">
								    <div class="row ">
									    <div class="col-2 text-center">#1</div>
									    <div class="col-6">
										    <input type="text" min="0" onblur="ValidarReferencia(this)" class="txtOrdenReferencia fuparr fuplist" /> <span></span>
									    </div>
								    </div>
							    </div>
                            </div>
							<div class="col-6 medium font-weight-bold">
								<table id="tbPedidosCliente" class="table table-sm">
                                    <thead>
                                            <tr>
                                                <th colspan="3">Ordem de compra <button id="btnAgregarCliente" class="btn btn-sm btn-link align-left" data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i> </button></th>
                                                <th></th>
                                            </tr>
                                    </thead>
									<tbody id="tbodyPedidoCliente">
										<tr>
											<td width ="5%"> #1</td>
											<td width ="35%"><input type="text" min="0" class="txtOrdenCliente" /></td>
											<td width ="50%"></td>
											<td width ="10%"><button  type="button" class="fa fa-upload" data-toggle="tooltip" title="Cargar" onclick="UploadFielModalShow('Agregar Documento',3,'Fechas - Solicitud Facturacion - Documentos Cliente')"> </button>
											</td>
										</tr>
									</tbody>
								</table>
							</div>
						</div>

						<div class="row divvarof">
							<div class="col-6"></div>
							<div class="col-1"></div>
							<div class="col-1 ">
								<div class="row">
								</div>
							</div>
							<div class="col-3"></div>
						</div>

						<hr />

						<div class="row divarrlist">
							<div class="col-1" data-i18n="[html]cantidad_max_piso">
								Cantidad max. piso:
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtCantidadPisos" class="fuparr fuplist" type="number" min="0" data-modelo="MaxPisos" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:150%; height:150%'  src='Imagenes//BR//Pisos edificacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]cantidad_fundiciones_piso">
								Cantidad fundiciones piso:
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtCantidadFundicionesPiso" class="fuparr" type="number" min="0" data-modelo="FundicionPisos" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//BR//Fundiciones.jpg' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]nro_equipos">
								# Equipos
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtNroEquipos" type="number" min="0" class="" data-modelo="NumeroEquipos" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:60%; height:60%'src='Imagenes/8_img_No Equipos.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//BR//Numero de Equipos.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>

						<div class="row divarrlist">
							<div class="col-1" data-i18n="[html]sistema_seguridad">Sistema de Seguridad</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectSistemaSeguridad" class="fuparr" data-modelo="SistemaSeguridad">
									<option value="-1">Seleccionar</option>
									<option value="1">Sistema de Seguridad</option>
									<option value="2">Sistema Trepante</option>
									<option value="3">No Aplica</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes//Sistema de Seguridad.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-4" data-i18n="[html]sistemasegNota" style="font-size: 11px;">: En el caso de Brasil el Sistema de Seguridad Convencional será reemplazado por el Sistema de Seguridad SM2022 en todos los casos.</div>
						</div>

						<%--<hr />--%>
						<div class="row">
							<div class="col-8"></div>
							<div class="col-2">
								<button type="button" onclick="guardarFUP()" class="btn btn-primary fupgen fupgenpt0 " >
									<i class="fa fa-save">  </i> <span data-i18n="[html]FUP_guardar"></span>
								</button>
							</div>
						</div>

					</div>
				</div>
			</div>

				<div id="DinamycSpace">
					<div class="card" id="ParteAlcanceOferta" style="display: none">

						<div class="card-header">
							<a class="collapsed card-link" data-toggle="collapse" href="#dynamic" data-i18n="[html]AlcanceOferta">Alcance de la Oferta</a>
						</div>
						<div id="dynamic" class="collapse" data-parent="#accordion">
							<div class="card-body">
								<div id="LineasDimanicas"></div>
								<div class="row justify-content-start" style="margin-top: 15px; margin-left: 15px;">
									<button type="button" class="btn btn-default fupgen fupgenpt1 " data-toggle="modal" onclick="UploadFielModalShow('Cargar Archivo',0,'Alcance Oferta')">
										Subir Archivos
									</button>
								</div>
								<div class="row justify-content-start" style="margin-top: 15px; margin-left: 15px;">
									<button type="button"  class="btn btn-success fupgen fupgenpt1 " data-toggle="modal" onclick="ActualizarEstado(2)">
										<i class="fa fa-envelope"></i>
										Enviar
									</button>
								</div>
							</div>
						</div>
					</div>
				</div>

			<div class="card" id="ParteAprobacionFUP">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseAprobacionFUP" data-i18n="FUP_aprobacion_fup">Aprobación del FUP 
					</a>
				</div>
				<div id="collapseAprobacionFUP" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" data-i18n="[html]FUP_numero_modulaciones">No. Modulaciones</div>
							<div class="col-1">
								<input id="txtNumeroModulaciones" type="number" min="0" style="width: 80% !important;" data-modelo-aprobacion="NumeroModulaciones" />
							</div>
							<div class="col-1"></div>
							<div class="col-2" data-i18n="[html]FUP_numero_cambios">No. Cambios</div>
							<div class="col-1">
								<input id="txtNumeroCambios" type="number" min="0" style="width: 80% !important;" data-modelo-aprobacion="NumeroCambios" />
							</div>
							<div class="col-1"></div>
							<div class="col-1">Altura Formaleta</div>
							<div class="col-1">
								<input type="number" id="txtAlturaFormaletaAproba" class="NumeroSalcot" data-modelo-aprobacion= AlturaFormaleta"/>
							</div>
						</div>
						<div class="row">
							<div class="col-2" data-i18n="[html]FUP_vobo">VoBo FUP</div>
							<div class="col-2">
								<select id="cboVoBoFup" class="form-control " data-modelo-aprobacion="Visto_bueno">
								</select>
							</div>
							<div class="col-2" data-i18n="[html]FUP_motivo_rechazo">Motivo de rechazo</div>
							<div class="col-2">
								<select id="cboMotivoRechazoFup" class="form-control " data-modelo-aprobacion="MotivoRechazo">
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-2" data-i18n="[html]FUP_observacion_aprobacion">Observaciones</div>
							<div class="col-8">
								<textarea id="txtObservacionAprobacion" class="form-control" rows="3" data-modelo-aprobacion="ObservacionAprobacion"></textarea>
							</div>
						</div>
						<div class="row">
							<div class="col-4"></div>
							<div class="col-6">
								<button id="btnImprimirAprobacion" type="button" class="btn btn-primary" value="Imprimir FUP" onclick="PrepararReporteFUP();">
								<i class="fa fa-print"></i> <span data-i18n="[html]FUP_imprimir_aprobacion" > imprimir</span>
								</button>
								<button id="btnGuardarAprobacion" type="button" class="btn btn-primary fupapro fupsalcie" value="Guardar y Notificar">
								<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar_aprobacion" > guardar</span>
								</button> 
							</div>
						</div>
						<div class="row">
							<div class="col-12  medium font-weight-bold " >Simulador Cotizaciones</div>
						</div>
						<div class="row">
							<div class="col-2" >No. Orden Cotizacion</div>
							<div class="col-2">
								<input id="txtOrdenCotizacion" type="text" disabled style="width: 80% !important;" />
							</div>
						<%--En Analisis--%>
							<div class="col-2">
								<table class="table table-sm table-hover fupsalco fupOrdcot" id="tab_ContenedorFormaleta">
									<thead class="thead-light">
										<tr>
											<th class="text-center" colspan ="4">Marcar CT en Análisis</th>
										</tr>
									</thead>
									<tbody id="tbodyontenedorFormaleta">
										<tr>
											<th class="text-center" width="50%">En Análisis</th>
											<th class="text-center" width="50%"> <input type="checkbox" id="txtAnalisis" data-modelo-Analisis = "EnAnalisis"/></th>
											<th ><button id="btnGuardarAnalisis" class="btn btn-sm btn-link align-left fupsalco fupOrdcot" data-toggle="tooltip" data-i18n="[title]FUP_guardar" onclick="guardarEnAnalisis()"><i class="fa fa-save" style="font-size:14px;"></i> </button> Guardar </th>
										</tr>
									</tbody>
								</table>
							</div>

						   <div class="col-6">
								<button id="btnGenerarOrdenCotizacion" type="button" class="btn btn-primary fupsalco fupOrdcot" value="Generar Orden Cotizacion" onclick="guardarOrdenCotizacion();">
								<i class="fa fa-cogs"></i> <span > Generar Orden Cotización</span>
								</button> 
						   
								<button id="btnExplosionarOrdenCotizacion" type="button" class="btn btn-primary fupsalco fupOrdcot" value="Explosionar Orden Cotizacion" onclick="explosionarOrdenCotizacion();">
								<i class="fa fa-cogs"></i> <span>  Explosionar CT</span>
								</button> 

								<button id="btnReporteCT" type="button" class="btn btn-primary" value="Listado Orden Cotizacion" onclick="LlamarReporteListadoCT();">
								<i class="fa fa-print"></i> <span>  Listado Orden CT</span>
								</button> 

								<button id="btnReporteOrdenCotizacion" type="button" class="btn btn-primary" value="Reporte Simulador Orden Cotizacion" onclick="LlamarReporteSimulador();">
								<i class="fa fa-print"></i> <span>  Simulador Orden CT</span>
								</button> 
							</div>
							<div class="col-1"></div>
						</div>

						<hr />
						<div class="row">
							<div class="col-12  medium font-weight-bold text-center" data-i18n="[html]FUP_detalle_devoluciones">Detalle de devoluciones</div>
						</div>
						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_detalle_dev">
									<thead class="thead-light">
										<tr>
											<th class="text-center" data-i18n="[html]FUP_fecha_detalle_devoluciones" width="10%">Fecha</th>
											<th class="text-center" data-i18n="[html]FUP_motivo_detalle_devoluciones" width="30%">Motivo Devolución</th>
											<th class="text-center" data-i18n="[html]FUP_observacion_detalle_devoluciones" width="40%">Observación</th>
											<th class="text-center" width="20%">Estado</th>
										</tr>
									</thead>
									<tbody id="tbodyDetailsDev">
									</tbody>
								</table>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="card" id="ParteAnexosFUP">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseAnexoFUP" data-i18n="[html]FUP_anexo_fup">Anexos del FUP 
					</a>
				</div>
				<div id="collapseAnexoFUP" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_anexos_fup">
									<thead class="thead-light">
										<tr>
											<th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" >Sección</th>
											<th class="text-center" >Estado</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyAnexosFup">
									</tbody>
								</table>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="card" id="parteSalidaCot">
			<div class="card-header">
				<a class="collapsed card-link" data-toggle="collapse" href="#collapseSalidaCot" data-i18n="[html]sc_salida_cotizacion">Salida Cotizacion
				</a>
			</div>
			<div id="collapseSalidaCot" class="collapse" data-parent="#accordion">
				<div class="card-body">
					<div class="row">
						<div class="col-2"></div>
						<div class="col-6">
							<table id="tab_DatosSalidaCoti" class="table table-sm">
								<thead>
									<tr  class="thead-light">
										<th class="text-center" colspan ="6">DETALLE SALIDA COTIZACIÓN</th>
									</tr>
								</thead>
							</table>
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2"></div>
						<div class="col-2 text-center" data-i18n="[html]sc_m2">M2</div>
						<div class="col-2 text-center" data-i18n="[html]sc_valor">VALOR</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_m2EquipoBase">M2, Equipo Base</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtM2EquipoBase" class="sumM2SalidaCot NumeroSalcot" data-modelosc="m2_equipo" />
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtValEquipoBase" class="sumValSalidaCot NumeroSalcot" data-modelosc="vlr_equipo" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_m2_adicionales">Nivel 1 M2, Adicionales</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtM2Adicionales" class="sumM2SalidaCot NumeroSalcot" data-modelosc="m2_adicionales" />
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtValAdicionales" class="sumValSalidaCot NumeroSalcot" data-modelosc="vlr_adicionales" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_detalle_arq">Nivel 1 M2, Detalles Arquitectonicos</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtDetArqM2SC" class="sumM2SalidaCot NumeroSalcot" data-modelosc="m2_Detalle_arquitectonico" />
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtDetArqValorSC" class="sumValSalidaCot NumeroSalcot" data-modelosc="vlr_Detalle_arquitectonico" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_total_encofrados">Total Encofrados</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtTotalMSC" class="NumeroSalcot" disabled="disabled" />
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtTotalValorSC" class="NumeroSalcot" disabled="disabled" />
						</div>
					</div>


					<div class="row">
													 
														  
						<div class="col-2">
																														
						</div>
						<div class="col-2" data-i18n="[html]sc_sis_seguridad">Perimetros de Sist. Seguridad</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtSistemaTrepanteAccsc" class="NumeroSalcot" data-modelosc="m2_sis_seguridad" />
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtAccSistemaSegSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_sis_seguridad" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_accesorios_basicos">Accesorios Basicos</div>
						<div class="col-2">
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtAccBasicosSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_basico" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_accesorios_complementarios">Accesorios Complementarios</div>
						<div class="col-2">
																							
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtAccComplSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_complementario" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_accesorios_opcionales">Accesorios Opcionales</div>
						<div class="col-2">
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtAccOpcionalesSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_opcionales" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_accesorios_adicionales">Accesorios Adicionales</div>
						<div class="col-2">
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtAccAdicionalesSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_adicionales" />
						</div>
					</div>
					<div class="row">
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_otros_productos">Otros Productos</div>
						<div class="col-2">
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtOtrosProductoSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_otros_productos" />
						</div>
					</div>
					<div class="row">						
						<div class="col-2"></div>
						<div class="col-2" data-i18n="[html]sc_total_propuestas">Total Propuesta Com.</div>
						<div class="col-2">
						</div>
						<div class="col-2">
							<input type="number" step="0.01" min="0" id="txtTotalPropuestaCom" class="NumeroSalcot" disabled="disabled"/>
						</div>
					</div>
					<div class="row">
						<div class="col-3">
							<div class="col-2"><button id="btnCartaCotizacion" type="button" class="btn btn-primary" value="Reporte Carta Cotizacion" onclick="LlamarCartaCot();">
								<i class="fa fa-print"></i> <span > Ir a Carta Cotización</span>
								</button></div>
						</div>
					</div>

					<%--Contenedores--%>
					<div class="row">
						<div class="col-6">
							<table class="table table-sm table-hover" id="tab_Contenedores">
								<thead class="thead-light">
									<tr>
										<th class="text-center" colspan ="4">CONTENEDORES</th>
									</tr>
								</thead>
								<tbody id="tbody1">
									<tr>
										<th class="text-center" width="10%"> </th>
										<th class="text-center" width="40%">20 Pies</th>
										<th class="text-center" width="40%"><input type="number" id="txtContenedor20" class="NumeroSalcot" data-modelosc="vlr_Contenedor20" /> </th>
										<th class="text-center" width="10%"> </th>
									</tr>
									<tr>
										<th class="text-center" > </th>
										<th class="text-center" >40 Pies</th>
										<th class="text-center" ><input type="number" id="txtContenedor40" class="NumeroSalcot" data-modelosc="vlr_Contenedor40" /></th>
										<th class="text-center" > </th>
									</tr>
									<tr>
										<th class="text-center" > </th>
										<th class="text-center" >Total FLETE</th>
										<th class="text-center" ><input type="number" step="0.01" min="0" id="vrFleteLocalTotal" class="NumeroSalcot" disabled="disabled"/></th>
										<th class="text-center" > </th>
									</tr>
									<tr>
										<td colspan ="3" align="center">
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect fupsalco"  onclick="calcular_flete_loc()">
											<i class="fa fa-save"></i> <span> Calcular Flete</span>
											</button>
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect fupsalco" onclick="guardar_flete(2)">
											<i class="fa fa-save"></i> <span> Guardar Flete</span>
											</button>
										</td>
									</tr>
								</tbody>
							</table>
						</div>
						<div class="col-6">
							<table id="tbComentarios" class="table table-sm">
								<thead>
									<tr class="thead-light">
										<th class="text-center" colspan ="4">Comentarios</th>
									</tr>
									<tr>
										<th width="22%" class="text-center">Fecha</th>
										<th width="70%" class="text-center">Comentario</th>
										<th width="3%" align="center" >
											<button id="btnAddComenta" class="btn btn-sm btn-link align-center fupsalco " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
										</th>
									</tr>
								</thead>
								<tbody id="tbodycomentarioSC"></tbody>
								<tfoot>
										<th></th>
										<th class="row justify-content-end">
											<button type="button" class="btn btn-primary fupsalco " data-toggle="modal" onclick="GuardarComentario(1)">
											<i class="fa fa-save"></i> <span> Guardar Comentario</span>
											</button>
										</th>
										<th></th>
								</tfoot>
							</table>
						</div>
					</div>
					<%--Botón Subir Carta Cotización--%>
					<div class="row">
						<div class="justify-content-start" style="margin-top: 15px; margin-left: 15px;">
							<button type="button" class="btn btn-default fupsalco " data-toggle="modal" onclick="UploadFielModalShow('Subir Carta Cotizacion',6,'Salida Cotizacion')">
								Subir Carta  
							</button>
						</div>
					</div>
					<%--Botón Carta Cotización--%>
					<div class="row">
						<div class="col-4">
							<table class="table table-sm table-hover" id="tab_anexos_salidaCot">
								<thead class="thead-light">
									<tr>
										<th class="text-center" width="70%">Ver Carta Cotizacion</th>
										<th class="text-center" width="22%">Fecha</th>
										<th class="text-center" width="8%"> </th>
									</tr>
								</thead>
								<tbody id="tbodyanexos_salidaCot">
								</tbody>
							</table>
						</div>
					<%--Altura Formaleta--%>
						<div class="col-4">
						</div>
					<%--Fecha entrega Cliente--%>
						<div class="col-4">
							<table id="tb_FechasEntregaCliente" class="table table-sm">
								<thead>
									<tr class="thead-light">
										<th class="text-center" colspan ="4">Fecha de Entrega Cotizacion</th>
									</tr>
								</thead>
								<tbody id="tbodyFechas">
									<tr>
										<th class="text-center" width="10%"> </th>
										<th width="40%" data-i18n="[html]FUP_fecha_EntregaCliente">Fecha Entrega de Cotización* *</th>
										<th width="40%" class="text-center" ><input id="txtFechaEntregaCotizaCliente" type="date" /></th>
										<th class="col-1">
											<button id="btnGrabaFechaEntregaCotizacion" class="btn" data-toggle="tooltip" onclick="GrabaFechasCliente(2)" data-i18n="[title]FUP_GrabarfechaEntregaCliente"><i class="fa fa-floppy-o"></i></button>
										</th>
										<th class="text-center" width="10%"> </th>
									</tr>
								</tbody>
							</table>
						</div> 

					</div>

					<div class="row justify-content-center">
						<div class="" style="margin-top: 15px; margin-left: 15px;">
							<button type="button" class="btn btn-primary  fupsalco " onclick="guardar_salida_cot(1)">
								<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
							</button>
						</div>
					</div>

					<div class="row">
						<div class="col-2">
							<button type="button" class="btn btn-success  fupsalcie " onclick="ActualizarEstado(44)">
								<i class="fa fa-envelope"></i> Guardar Valores de Cierre</button>
						</div>
					</div>

					<%--Devolución de Cotización--%>
					<div class="col-md-12 " style="padding-top: 15px;" id="ParteAprobacion">
						<div id="headerAprobacion" class="box box-primary">
							<div class="box-header border-bottom border-primary" style="z-index: 3;">
								<table class="col-md-12 table-sm">
									<tbody>
										<tr>
											<td width="97%">
												<h5 class="box-title card-header card-link" style="">Devolución de Cotizacion</h5>
											</td>
											<td width="3%">
												<div class="col-md-12" style="padding-bottom: 4px;">
													<button id="collapseAprobacion" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 5px;">
														<span class="fa fa-angle-double-down"></span>
													</button>
												</div>
											</td>
										</tr>
									</tbody>
								</table>
							</div>
							<div id="bodyAprobacion" class="box-body" style="display: none; padding-top: 20px; margin-left: 15px; margin-right: 15px;">
								<div class="row">
									<div class="col-2" >Motivo Devolución Comercial</div>
									<div class="col-3">
										<select id="cboMotivodev" class="form-control ">
										</select>
									</div>
									<div class="col-4"></div>
								</div>
								<div class="row">
									<div class="col-2" data-i18n="[html]FUP_observacion_aprobacion">Observación</div>
									<div class="col-8">
										<textarea id="txtObservaciondevsc" class="form-control" rows="3"></textarea>
									</div>
								</div>
							</div>

						</div>

					</div>

				</div>
			</div>
			</div> 

			<div class="card" id="ParteControlCambio">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseControl" data-i18n="cntrcmb">Control de Cambios</a>
				</div>
				<div id="collapseControl" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
									<button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShow('Control de Cambios',0,0,'')">
										<i class="fa fa-comment"></i>
										<b data-i18n="[html]FUP_cntrcmb">Control de Cambios</b>
									</button>

							</div>                            
                        </div>
                        <div id="DinamycChange">
						        <div class="row Comentario">
							        <div class="col-12">
								        <table class="table table-sm table-hover" id="tab_ControlCambio">
									        <thead class="thead-light">
										        <tr>
											        <th class="text-center" >Fecha</th>
											        <th class="text-center" >Estado Fup</th>
											        <th class="text-center" >Observacion</th>
											        <th class="text-center" >Usuario</th>
										        </tr>
									        </thead>
									        <tbody id="tbodyControlCambio">
									        </tbody>
								        </table>
							        </div>
						        </div>
                           </div>
					</div>
				</div>
			</div>

			<div class="card" id="ParteArmado">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseArmado" data-i18n="[html]FUP_planoarmado">Planos de Armado
					</a>
				</div>
				<div id="collapseArmado" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_Armado1">
									<thead>
										<tr class="table-primary">
											<th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyArmado1">
									</tbody>
								</table>
							</div>
						</div>
						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_Armado2">
									<thead>
										<tr class="table-success">
											<th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyArmado2">
									</tbody>
								</table>
							</div>
						</div>

						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_Armado3">
									<thead>
										<tr class="table-info">
											<th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyArmado3">
									</tbody>
								</table>
							</div>
						</div>
					</div>

				</div>
			</div>	   

		</div>   
	</div>
</asp:Content>

