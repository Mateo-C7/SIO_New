<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormFUP.aspx.cs" Inherits="SIO.FormFUP" Culture="en-US" UICulture="en-US" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

	<script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
	<script type="text/javascript" src="Scripts/formfup.js?v=20241217B"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/select2.min.js"></script>
	<script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>
	<link rel="Stylesheet" href="Content/bootstrap.min.css" />
	<link rel="Stylesheet" href="Content/SIO.css" />
	<link rel="stylesheet" href="Content/font-awesome.css" />
	<link rel="Stylesheet" href="Content/css/select2.min.css" />
	<link href="Content/toastr.min.css" rel="stylesheet" />
	<script type="text/javascript">

</script>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
	<div id="loader" style="display: none">
		<h3>Procesando...</h3>
	</div>
	<div id="ohsnap"></div>

	<div class="container-fluid contenedor_fup">
		<div class="row">
			<div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				<button type="button" class="btn btn-secondary langes">
					<img alt="español" src="Imagenes/colombia.png" /></button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" /></button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" /></button>
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
							<h5 class="modal-title">Reporte</h5>
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
								<input type="text" class="form-control" id="EsDFT" disabled hidden/>
							</div>
                            <div class="row">
								<div class="col-12">Titulo</div>
							</div>
                            <div class="row">
								<div class="col-12">
    								<input type="text" class="form-control" id="txtTituloObs" />
								</div>
							</div>
                            <div class="row Rdft" id = "Rdft">
								<div class="col-12">Tipo de Control</div>
								<div class="col-6">
                                    <select id="cmbSubProceso" class="form-control">
									</select>
								</div>
<%--								<div class="col-6">
                                    <select id="cmbEstadoDft" class="form-control">
									</select>
								</div>--%>
							</div>
                            <div class="row Rdft2">
								<div class="col-6">Tipo de Control</div>
								<div class="col-6">Estado</div>
								<div class="col-6">
                                    <select id="cmbSubProceso2" class="form-control">
									</select>
								</div>
								<div class="col-6">
                                    <select id="cmbEstadoDft" class="form-control">
									</select>
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
					<table class="table table-sm table-hover mb-0" id="tbSearchFup">
						<tbody>
							<tr>
								<td align="center"><h6 data-i18n="[html]FUP_estado_fup">Estado FUP</h6>
								</td>
								<td>
                                    <button data-tooltip-custom-classes="tooltip-large2" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:100%; height:100%'  src='Imagenes//AyudaEstados.png' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
								</td>
								<td align="center" style="width: 90px;">
									<div id="divEstadoFup" class="fupestado" style="font-weight: bold"></div>
								</td>
								<td align="center">
									<button id="btnNuevo" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_nuevo"><i class="fa fa-file" style="margin-left: -200%;"></i></button>
									<%--<input id="btnNuevo" type="button" class="btn btn-primary  " value="Nuevo" data-i18n="[value]FUP_nuevo" />--%>
									<button id="btnFupBlanco" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_fup_blanco"><i class="fa  fa-file-text" style="margin-left: -200%;"></i></button>
									<%--<input id="btnFupBlanco" type="button" class="btn btn-primary " value="Fup Blanco" data-i18n="[value]FUP_fup_blanco" />--%>
									<button id="btnDuplicar" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_duplicar" ><i class="fa fa-copy" style="margin-left: -200%;"></i></button>
									<%--<input id="btnDuplicar" type="button" class="btn btn-primary " value="Duplicar" data-i18n="[value]FUP_duplicar" />--%>
								</td>
								<td align="center" data-i18n="[html]FUP_fup">FUP</td>
								<td style="width: 90px;">
									<input id="txtIdFUP" type="number" min="0" class="form-control  bg-warning text-dark" />
								</td>
								<td style="width: 90px;">
									<button id="btnBusFup" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search" style="margin-left: -200%;"></i></button>
								</td>
								<td align="center" data-i18n="[html]FUP_orden">Orden Fabricación</td>
								<td style="width: 90px;">
									<input id="txtIdOrden" type="text" class="form-control  bg-warning text-dark" />
								</td>
								<td style="width: 90px;">
									<button id="btnBusOf" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search" style="margin-left: -200%;"></i></button>
								</td>

								<td align="center" <%--data-i18n="[html]FUP_OrdenCliente"--%>>Pedido Cliente</td>
								<td style="width: 90px;">
									<input id="txtIdOrdenCliente" type="number" min="0" class="form-control  bg-warning text-dark" />
								</td>
								<td style="width: 90px;">
									<button id="btnBusOrdenCliente" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search" style="margin-left: -200%;"></i></button>
								</td>

							</tr>
                            <tr >
                                <td colspan="3" style="border: none" align="center" id="trEstadoDFT" hidden="hidden">
                                    <span class="font-weight-bold" style="color:blue ">PROCESO DEFINICION TECNICA<span id="txtDFTGeneral"></span></span>
                                </td>
                                <td colspan="3" style="border: none" align="center" id="trComplejidad" hidden="hidden">
                                    <span class="font-weight-bold" style="color:blue ">Complejidad: <span id="txtComplejidadGeneral"></span></span>
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
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseDatosGenerales" data-i18n="FUP_datos_generales">DATOS GENERALES</a>
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
                                <select id="monedaPaisTracker" style="display:none"></select>
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
							<div class="col-2 form-inline">
								<select id="cboClaseCotizacion" disabled class="form-control" data-modelo="ClaseCotizacion" style="width: 80% !important;">
                                </select> 
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Clase de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                                <input id="txtFechaSolicitaCliente" type="text" disabled="disabled" class="form-control" />
                                <!--<button id="btnGrabaFechaSolicita" class="btn SoloUpd" data-toggle="tooltip" onclick="GrabaFechasCliente(1)" data-i18n="[title]FUP_GrabarFechaSolicita"><i class="fa fa-floppy-o"></i></button>-->
                            </div>
					   </div>
                        <div class="row">
							<div class="col-3" data-i18n="[html]FUP_LinkObra">
								Link Obra: 
							</div>
                            <div class="col-3">
                                <input id="txtLinkObra" type="text" data-modelo="obra_link" class="form-control" />
                            </div>
							<div class="col-1" data-i18n="[html]FUP_InicioObra">
								Fecha Inicio Obra: 
							</div>
                            <div class="col-2" style="text-align:Left">
                                <input id="txtFecIniObra" type="date" data-modelo="Obra_FecInicio" class="form-control"/>
                            </div>
                        </div>
						<div class="row">
							<div class="col-1" data-i18n="[html]FUP_creado_por">
								Creado por 
							</div>
							<div class="col-5">
								<input id="txtCreadoPor" disabled="disabled" type="text" class="form-control"/>
							</div>
							<div class="col-1" data-i18n="[html]FUP_cotizado_por">
								Cotizado por: 
							</div>
							<div class="col-5">
								<input id="txtCotizadoPor" disabled="disabled" type="text" class="form-control"/>
							</div>
						</div>
						<div class="row">
							<div class="col-1" >Vendedor Zona</div>
							<div class="col-4">
								<select id="cmdVendedorZona" data-modelo="VendedorZona" class="form-control select-filter">
									<option value="-1">Vendedor Zona</option>
								</select>
							</div>
							<div class="col-2" style="display: flex; justify-content: flex-end">ID Recomendacion Tecnico:</div>
							<div class="col-1">
								<input id="txtRecomendacionTecn" data-modelo="RecomendacionTecnico" onblur="ValidarRecomendacion(this)" type="text" class="form-control"/>
							</div>
							<div class="col-4"><input id="txtRecomendacion" disabled="disabled" type="text" class="form-control"/></div>
						</div>

						<div class="row">
							<div class="col-1" data-i18n="[html]tipo_negociacion">Tipo de Negociacion</div>
							<div class="col-2">
								<select id="selectTipoNegociacion" data-modelo="TipoNegociacion" class="form-control">
									<option value="-1">Tipo Negociacion</option>
									<option value="1">Venta</option>
									<option value="2">Reparación</option>
									<option value="3">Arrendamiento</option>
								</select>

							</div>
							<div class="col-1" data-i18n="[html]tipo_cotizacion">Tipo de Cotizacion</div>
							<div class="col-4 form-inline">
								<select id="cboTipoCotizacion" data-modelo="TipoCotizacion" style="width: 80% !important;" class="form-control">
									<option value="-1">Tipo Cotizacion</option>
									<option value="1">Equipo Nuevo</option>
									<option value="2">Adaptaciones</option>
									<option value="3">Listados</option>
								</select>
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>-Equipo Nuevo Aluminio (Forsa Alum ó Forsa Plus): Son aquellos proyectos que trabajaran con formaleta de aluminio para todos sus detalles arquitectónicos indicados en los planos del cliente y en el FUP. <br/>- Adaptación: Son aquellos proyectos donde se reutilizaran equipos de formaletas de proyectos anteriores a un nuevo modelo de proyecto.<br/>-  Listados: Son aquellos que el cliente solicita basado en revisión e inventarios internos de su proceso para cubrir sus necesidades en el buen desempeño de su proyecto.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Tipo de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>

							</div>
							<div class="col-1" data-i18n="[html]producto">Producto</div>
							<div class="col-3">
								<select id="selectProducto" data-modelo="Producto" class="form-control">
									<option value="-1">Producto</option>
									<option value="1">FORSA ALUM</option>
									<option value="2">FORSA PLUS</option>
									<option value="3">FORSA ACERO</option>
								</select>
							</div>
						</div>
						<div class="row ">
							<div class="col-3">
								<div class="row divarrlist">
									<div class="col-4" data-i18n="[html]tipo_vaciado">Tipo de Vaciado</div>
									<div class="col-8 form-inline">
										<select id="selectTipoVaciado" data-modelo="TipoVaciado" style="width: 80% !important;" class="form-control">
											<option value="-1">Tipo Vaciado</option>
											<option value="1">MONOLITICO</option>
											<option value="2">MUROS Y LOSA EXTERIOR</option>
											<option value="3">UNICAMENTE MUROS</option>
											<option value="4">UNICAMENTE LOSA</option>
											<option value="5">N/A</option>
										</select>
										<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>- Monolitico: Es cuando se realiza armados de equipos que implica muros y losas, para vaciar/fundir unidades de vivienda en un solo evento.<br/>- Muros y Losa Posterior: Es cuando se arman formaletas de muro, para ser fundidos en una primera fase. en una segunda fase, se realiza el armado de las formaletas de losa sobre muros fundidos, para la fundición de esta.<br/>- Unicamente Muros: Es cuaando se funde los muros por medio de formaletas, pero la losa se realiza por de medio de otro sistema de construcción.<br/>-Unicamente Losas: Se presenta cuando los muros son construidos en otros y sobre estos armas las formaletas de losa y las apuntalan, para fundir la losa.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
										<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>- Monolitico: Es cuando se realiza armados de equipos que implica muros y losas, para vaciar/fundir unidades de vivienda en un solo evento.<br/>- Muros y Losa Posterior: Es cuando se arman formaletas de muro, para ser fundidos en una primera fase. en una segunda fase, se realiza el armado de las formaletas de losa sobre muros fundidos, para la fundición de esta.<br/>- Unicamente Muros: Es cuaando se funde los muros por medio de formaletas, pero la losa se realiza por de medio de otro sistema de construcción.<br/>-Unicamente Losas: Se presenta cuando los muros son construidos en  otros y sobre estos armas las formaletas de losa y las apuntalan, para fundir la losa.<br/>-Nota: Cuando sean solo columnas y vigas utilizar la descripción 'muros y losa posterior'</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>																																																																																																																																																																																																																																																																									 
										<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Tipo de Vaciado.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
									</div>
								</div>
								<div class="row">
									<div class="col-4" data-i18n="[html]termino_negociacion">Term. Negociacion</div>
									<div class="col-8">
										<select id="selectTerminoNegociacion" data-modelo="TerminoNegociacion" style="width: 80% !important;" class="form-control">
										</select>
									</div>
								</div>
							</div>

							<div class="col-9">
                                <div class="row divarrlist">
                                    <div class="col-4">
                                        <div class="row">
								            <div class="col-5 SeCopia">Equipo Copia / Modulacion Existente</div>
								            <div class="col-6 SeCopia form-inline">
								                <select id="selectCopia" data-modelo="EquipoCopia" class="form-control">
									                <option value="-1">Seleccione</option>
									                <option value="1">SI</option>
									                <option value="0">NO</option>
								                </select>
        								        <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Los requerimientos del cliente, aplican cuando requieren solicitar medidas diferentes a las sugeridas por el estándar forsa. Aclarando que esto puede afectar el precio"><i class="fa fa-info-circle fa-lg"></i></button>
								            </div>
                                        </div>
                                    </div>
                                    <div class="col-8 vaCopia">
                                        <div class="row">
								            <div class="col-2">Fup Copia</div>
								            <div class="col-3">
                                                <input id="txtFupCopia" data-modelo="FupCopia" type="text" onblur="ValidarCopia(this)" />
                                            </div>
								            <div class="col-2">Producto Copia</div>
								            <div class="col-3">
                                                <input id="txtProductoCopia" disabled="disabled" type="text" />
                                            </div>
        							    </div>
                                        <div class="row">
                							<div style="font-size: 11px;">NOTA: El FUP informado es para verificacion y coherencia de las modulaciones nuevas, sin embargo, el alcance del nuevo equipo debe ser considerado segun lo acordado en el nuevo negocio 
                                                El FUP informado debe considerar ordenes de fabircacion FORSA existententes en el sistema 
                                            </div>
                                        </div>

                                    </div>
    							</div>
							</div>
						</div>
                        <div class="row ">
						    <div class="divvarof col-6">
                                <div class="row ">
							        <div class="col-3 medium font-weight-bold text-center divvarof"> 
								        <span data-i18n="[html]orden_referencia">Orden de Referencia</span>
									        <button id="btnAgregarOrdenReferencia" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i> </button>
							        </div>
							        <div class="col-9 divContentOrdenReferencia divvarof">
								        <div class="row ">
									        <div class="col-2 text-center">#1</div>
									        <div class="col-5">
										        <input type="text" min="0" onblur="ValidarReferencia(this)" class="txtOrdenReferencia form-control" /> <span></span>
									        </div>
								        </div>
							        </div>
						        </div>
					        </div>

						    <div class="divvarof col-6">
                                <div class="row ">
                                        <div class="col-2">Otros</div>
							            <div class="col-5">
								            <textarea id="txtOtros" class="form-control" rows="2" data-modelo="otros"></textarea>
							            </div>
							            <div class="col-1">
								            <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:150%; height:150%'  src='Imagenes//Casilla Otros.jpg' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							            </div>
    						    </div>
						    </div>
                        </div>

                        <div class="row">
                            <div class="col-2">
                                <button class="btn btn-info btn-sm SolSimu" id="btnSolicitarSimulacion" style="display:none;" type="button" onclick="SolicitarSimulacion()">
                                    Solicitar simulación
                                </button>
                            </div>
                            <div class="col-4">
                                <div id ="ClasificaCliente" style="text-align:center"></div>
								<button data-tooltip-custom-classes="tooltip-large2" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:100%; height:100%'  src='Imagenes//ClasificacionClientes.png' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                        </div>

						<div class="row">
							<div class="col-8"></div>
							<div class="col-2">
								<button type="button" onclick="guardarFUP_datosGenerales()" class="btn btn-primary fupgen fupgenpt0 " >
									<i class="fa fa-save" style="padding-right: inherit;">  </i> <span data-i18n="[html]FUP_guardar"></span>
								</button>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="card" id="Acordeon-Cot-Rapida" style="display: none">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#Collapse-Cot-Rapida" data-i18n="[html]cotizacion_rapida"></a>
				</div>
				<div id="Collapse-Cot-Rapida" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-12 border p-2">
								<div class="alert alert-primary h6 text-center" role="alert" data-i18n="[html]datos_de_la_cotizacion">
									Datos de la cotización
                                </div>
								<div class="form-row">
									<div class="form-group col-md-3">
										<label for="tipo_obra" data-i18n="[html]tipo_de_escalera">Tipo de escalera</label>
										<select class="form-control" id="select_escalera_cot_rapida">
										</select>
									</div>
									<div class="form-group col-md-3">
										<label for="tipo_obra" data-i18n="[html]numero_de_modulaciones">Número de modulaciones</label>
										<input class="form-control" type="number" min="0" id="modulaciones_cot_rapida" />
									</div>
									<div class="form-group col-md-3">
										<label for="tipo_obra" data-i18n="[html]area_armado_mas_grande">Área en m2 del Armado mas Grande > Densidad</label>
										<input class="form-control" type="number" min="0" id="area_cot_rapida" />
									</div>
									<div class="form-group col-md-3">
										<label for="tipo_obra" data-i18n="[html]numero_de_cambios">Ingresa el número de cambios</label>
										<input class="form-control" type="number" min="0" id="cambios_cot_rapida" />
									</div>
								</div>
								<div class="form-row">
									<div class="form-group col-md-12 text-right">
										<button class="btn btn-primary" type="button" onclick="InsertarEncabezadoCotRap()" data-i18n="[html]calcular">Calcular</button>
									</div>
								</div>
							</div>
						</div>

       					<div class="row">
                            <div>
    						    <div class="form-row">
							        <div class="col-12">
								        <table class="table table-sm">
									        <thead class="thead-light">
										        <tr class="text-center">
											        <th class="text-center" data-i18n="[html]tipo_obra">Tipo Obra</th>
											        <th class="text-center" data-i18n="[html]tipo_de_escalera">Tipo Escalera</th>
											        <th class="text-center" data-i18n="[html]FUP_numero_modulaciones">Nro Modulaciones</th>
											        <th class="text-center" data-i18n="[html]FUP_numero_cambios">Nro Cambios</th>
											        <th class="text-center" data-i18n="[html]area_m2">Area M2</th>
											        <th class="text-center" data-i18n="[html]FUP_fecha_creacion">Fecha Creacion</th>
											        <th class="text-center" data-i18n="[html]entregado_cliente">Entregado a Cliente</th>
											        <th class="text-center" data-i18n="[html]descargar">Descargar</th>
										        </tr>
									        </thead>
									        <tbody id="tbody_cotrap_enc">

									        </tbody>
								        </table>
							        </div>
						        </div>
							    <div class="form-row">
								    <div class="form-group col-md-12 text-right">
									    <button class="btn btn-primary" disabled="disabled" id="btn_cotizar_cotrap" type="button" onclick="entregar_cliente_cotrap()" data-i18n="[html]cotizar">Cotizar</button>
								    </div>
							    </div>
    					    </div>
                        </div>
					</div>
				</div>
			</div>


			<div class="card" id="ParteAlcance">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" data-i18n="[html]FUP_informacion_general">INFORMACION GENERAL
					</a>
				</div>
				<div id="collapseTwo" class="collapse" data-parent="#accordion">
					<div class="card-body">
						
						<div class="row divvarof d-none">
							<div class="col-6"></div>
							<div class="col-1"></div>
							<div class="col-1 ">
								<div class="row">
								</div>
							</div>
							<div class="col-3"></div>
						</div>

						<div class="row divarrlist form-inline">
							<div class="col-1" data-i18n="[html]cantidad_max_piso">
								Cantidad max. piso:
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtCantidadPisos" class="fuparr fuplist form-control" type="number" min="0" data-modelo-tecnico="MaxPisos" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:150%; height:150%'  src='Imagenes//Pisos edificacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]cantidad_fundiciones_piso">
								Cantidad fundiciones piso:
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtCantidadFundicionesPiso" class="fuparr form-control" type="number" min="0" data-modelo-tecnico="FundicionPisos" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>Se refiere a la cantidad de armados del equipo que se deben realizar en una planta o nivel de la edificación, para su fundición completa.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Fundiciones.jpg' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]nro_equipos">
								# Equipos
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtNroEquipos" type="number" min="0" class="form-control" data-modelo-tecnico="NumeroEquipos" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:60%; height:60%'src='Imagenes/8_img_No Equipos.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Numero de Equipos.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>

						<div class="row divarrlist form-inline">
							<div class="col-1" data-i18n="[html]sistema_seguridad">Sistema de Seguridad</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectSistemaSeguridad" class="fuparr form-control" data-modelo-tecnico="SistemaSeguridad">
									<option value="-1">Seleccionar</option>
									<option value="1">Sistema de Seguridad</option>
									<option value="2">Sistema Trepante</option>
									<option value="3">No Aplica</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes//Sistema de Seguridad.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-4" data-i18n="[html]sistemasegNota" style="font-size: 11px;">: En el caso de Brasil el Sistema de Seguridad SM 1.0 será reemplazado por el Sistema de Seguridad SM 2.0 en todos los casos.</div>
							<div class="col-1" data-i18n="[html]alineacion_vertical">Alineacion Vertical</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectAlineacionVertical" class="fuparr form-control" data-modelo-tecnico="AlineacionVertical">
									<option value="-1">Seleccionar</option>
									<option value="1">CPC Liso</option>
									<option value="2">CPC Dilatado</option>
									<option value="3">AGR Liso</option>
									<option value="4">AGR Dilatado</option>
								</select>
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>- El AGR (Angulo de Arrastre) permite que la FM descanse sobre el ayudando a que no se derrame el concreto por la base de los paneles a partir del armado del segundo nivel y mejorar el plomo de los mismos.<br/>- El CPC (Panel de Ciclo) es un panel que se caracteriza por presentar refuerzos verticales con perforaciones que permiten la introducción de varilla roscada y chapola que actúan como anclaje en el concreto para permitir que dicho panel se mantenga fijo después del desencofre de las otras piezas, esto con el fin de apoyar las Formaletas de muro del siguiente.<br/>- Juntas de Entrepisos</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:150%; height:150%' src='Imagenes//Alineacion Vertical.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>

						<%--<hr />--%>
						<div class="row divarrlist form-inline">
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]espesor_muro">Espesor Muro </span>
                                <span class="MedidaEspesores">(cm):</span>
								<button id="btnAgregarEspesorMuro" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
							</div>
							<div class="col-1">
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes//Espesor de Muro.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]espesor_losas">Espesor Losa </span>
                                <span class="MedidaEspesores">(cm):</span>
								<button id="btnAgregarEspesorLosa" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
							</div>
							<div class="col-1">
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes//Espesor de Losa.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]enrase_puerta">Enrase puertas </span>
                                <span class="MedidaEspesores">(cm):</span>
								<button id="btnAgregarEnrasePuertas" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
							</div>
							<div class="col-1">
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%'src='Imagenes/enrrase de puerta.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]enrase_ventanas">Enrase Ventana </span>
                                <span class="MedidaEspesores">(cm):</span>
								<button id="btnAgregarEnraseVentanas" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
							</div>
							<div class="col-1">
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:70%; height:70%' src='Imagenes/enrase de ventana.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>

						<div class="row divarrlist">
							<div class="col-3 divContentoEspesorMuro">
								<div class="row ">
									<div class="col-3 text-center"># 1</div>
									<div class="col-4">
										<input type="number" min="0" required class="txtValorMuro fuparr fuplist form-control" />
									</div>
									<div class="col-3">
									</div>
								</div>
							</div>
							<div class="col-3 divContentEspesorLosa">
								<div class="row">
									<div class="col-3 text-center"># 1</div>
									<div class="col-4">
										<input type="number" min="0" required class="txtValorLosa fuparr fuplist form-control" />
									</div>
									<div class="col-3">
									</div>
								</div>
							</div>
							<div class="col-3 divContentEnrasePuertas">
								<div class="row ">
									<div class="col-3 text-center"># 1</div>
									<div class="col-4">
										<input type="number" min="0" required class="txtEnrasePuertas fuparr fuplist form-control" />
									</div>
								</div>
							</div>
							<div class="col-3 divContentEnraseVentanas">
								<div class="row ">
									<div class="col-3 text-center"># 1</div>
									<div class="col-4">
										<input type="number" min="0" required class="txtEnraseVentanas fuparr fuplist form-control" />
									</div>
								</div>
							</div>
						</div>

						<hr class="divarrlist" />

						<div class="row divarrlist form-inline">
							<div class="col-1">
                                <span data-i18n="[html]altura_libre">Altura libre</span>
                                <span class="MedidaEspesores">(cm):</span>
							</div>
                            
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectAlturaLibre" class="fuparr fuplist form-control" data-modelo-tecnico="AlturaLibre"></select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes//Altura Libre.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1 divAlturaLibreVariable" data-i18n="[html]altura_libre_minima">Alt. Libre Min.</div>
							<div class="col-1 divAlturaLibreVariable">
								<input type="number" min="0" class="fuparr form-control" id="txtAlturaLibreMinima" data-modelo-tecnico="AlturaLibreMinima" />
							</div>
							<div class="col-1 divAlturaLibreVariable" data-i18n="[html]altura_libre_maxima">Alt. Libre Max.</div>
							<div class="col-1 divAlturaLibreVariable">
								<input type="number" min="0" class="fuparr form-control" id="txtAlturaLibreMaxima" data-modelo-tecnico="AlturaLibreMaxima" />
							</div>
							<div class="col-1 divAlturaLibreOtro" data-i18n="[html]cual_altura_libre">Cual Alt. Libre?</div>
							<div class="col-1 divAlturaLibreOtro">
								<input type="number" min="0" id="txtAlturaLibreCual" data-modelo-tecnico="AlturaLibreCual" class="fuparr form-control" />
							</div>
						</div>

						<div class="row divarrlist form-inline">
							<div class="col-2" >
                                <span data-i18n="[html]altura_fm_interna_sugerida">Altura FM Interna Sugerida </span>
                                <span class="MedidaEspesores">(cm):</span>
							</div>
							<div class="col-2" style="display: inline-table">
								<input id="txtAlturaInternaSugerida" style="width: 80% !important;" class="fuparr form-control" type="text" />
								<select style="width: 80% !important; display: none;" id="selectAlturaInternaSugerida" class="fuparr form-control">
									<option value="-1" selected>Seleccionar</option>
									<option value="60">60</option>
									<option value="90">90</option>
									<option value="120">120</option>
									<option value="210">210</option>
									<option value="240">240</option>
									<option value="270">270</option>
									<option value="300">300</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%' src='Imagenes//Altura de la FM interna.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
						   </div>
							<div class="col-1" data-i18n="[html]tipo_fm_fachada">Tipo FM Fachada</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectTipoFMFachada" data-modelo-tecnico="TipoFachada" class="fuparr form-control">
									<option value="-1">Seleccionar</option>
									<option value="1">Estandar</option>
									<option value="2">Alta</option>
								</select>
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div> -Formaleta Estandar: son FM externas que para alcanzar la altura libre sumado el espesor de losa, requieren que se modulen CAP, como complementos para estas.<br/>- Formaleta Alta: Son FM externo que contemplan la altura del muro y el espesor de losa, eliminando asi el CAP como complemento.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:80%; height:80%' src='Imagenes//Formaleta de Muro.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
						   </div>

						</div>

						<div class="row divarrlist form-inline">
							<div class="col-2" data-i18n="[html]altura_cap">Altura de CAP</div>
							<div class="col-2" style="display: inline-table">
								<input id="txtAlturaCap1" style="width: 80% !important;" data-modelo-tecnico="AlturaCAP1" class="fuparr form-control" type="text" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:80%; height:80%' src='Imagenes/21_img_altura de cap.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%' src='Imagenes//Altura de Cap.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>

							</div>
								<div class="col-1" id="CapPernadolbl" >¿CAP Pernado?</div>
								<div class="col-2" style="display: inline-table">
									<select style="width:50% !important;" id="selectCAPPernado" data-modelo-tecnico="CapPernado" class="fuparr form-control" >
										<option value="-1">Seleccione</option>
										<option value="1">SI</option>
										<option value="2">NO</option>
									</select>
								</div>
						</div>
						<div class="row divarrlist form-inline">
							<div class="col-1" data-i18n="[html]tipo_union">Tipo Union:</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtTipoUnion" data-modelo-tecnico="TipoUnion" class="fuparr form-control" type="text" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes//Tipo de Union.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]detalle_union">Detalle Union</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectDetalleUnion" data-modelo-tecnico="DetalleUnion" class="fuparr fupadap fuplist form-control">
									<option value="-1">Seleccionar</option>
									<option value="1">Lisa</option>
									<option value="2">Dilatada</option>
									<option value="3">Cenefa</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:70%; height:70%' src='Imagenes//Detalle de la Union.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1">
                                <span data-i18n="[html]altura_union">Altura Union</span> 
                                <span class="MedidaEspesores">(cm):</span>
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtAlturaUnion" data-modelo-tecnico="AlturaUnion" class="fuparr form-control" type="text" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/18_img_altunion.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%' src='Imagenes//Altura de la Union.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>

						<div class="row alturacap1 divarrlist form-inline"></div>

						<div class="row divarrlist forsapro form-inline">
							<div class="col-1" data-i18n="[html]req_cliente">Req Cliente</div>
							<div class="col-2" style="display: inline-table">
								<select id="selectReqCliente" data-modelo-tecnico="ReqCliente" class="fuparr form-control" style="width: 80% !important;">
									<option value="-1">Seleccione</option>
									<option value="1">SI</option>
									<option value="2">NO</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Los requerimientos del cliente, aplican cuando requieren solicitar medidas diferentes a las sugeridas por el estándar forsa. Aclarando que esto puede afectar el precio"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-8" data-i18n="[html]req_clienteNota" style="font-size: 11px;">NOTA: Al seleccionar 'Requerimientos del Cliente', se tomaran estos datos y medidas para elaborar las cotizaciones, omitiendo las sugeridas por FORSA en esta sección. Teniendo en cuenta que esto puede llegar a afectar el precio, al salirse del estándar definido por la compañía</div>
						</div>

						<div class="divarrlist form-inline">
							<div class="row divReqCliente">
								<div class="col-2" data-i18n="[html]altura_fm_interna_sugerida">Altura FM Interna Sugerida:</div>
								<div class="col-1">
									<input id="txtAlturaInternaSugeridaCliente" data-modelo-tecnico="AlturaIntSugeridaCliente" type="text" class="fuparr form-control" />
								</div>
								<div class="col-1" data-i18n="[html]tipo_fm_fachada">Tipo FM Fachada</div>
								<div class="col-2">
									<select id="selectTipoFMFachadaCliente" data-modelo-tecnico="TipoFachadaCliente" class="fuparr form-control">
										<option value="-1">Seleccionar</option>
										<option value="1">Estandar</option>
										<option value="2">Alta</option>
									</select>
								</div>
								<div class="col-1" data-i18n="[html]altura_cap">Altura de CAP</div>
								<div class="col-1">
									<input id="txtAlturaCapCliente1" data-modelo-tecnico="AlturaCAP1Cliente" type="text" class="fuparr form-control" />
								</div>
							</div>

							<div class="row divReqCliente form-inline">
								<div class="col-1" data-i18n="[html]tipo_union">Tipo Union:</div>
								<div class="col-2">
									<select id="selectTipoUnionCliente" data-modelo-tecnico="TipoUnionCliente" class="fuparr form-control"></select>
									<%--<input id="txtTipoUnionCliente" data-modelo-tecnico="TipoUnionCliente" type="text" class="fuparr" />--%>
								</div>
								<div class="col-1" data-i18n="[html]detalle_union">Detalle Union</div>
								<div class="col-2">
									<select id="selectDetalleUnionCliente" data-modelo-tecnico="DetalleUnionCliente" class="fuparr form-control">
										<option value="-1">Seleccionar</option>
										<option value="1">Lisa</option>
										<option value="2">Dilatada</option>
										<option value="3">Cenefa</option>
									</select>
								</div>
								<div class="col-1" data-i18n="[html]altura_union">Altura Union:</div>
								<div class="col-2">
									<input id="txtAlturaUnionCliente" data-modelo-tecnico="AlturaUnionCliente" type="text" class="fuparr form-control" />
								</div>

								<%--<div class="col-1">
								<input id="txtAlturaCapCliente2" data-modelo-tecnico="AlturaCAP2Cliente" type="text" class="fuparr" />
							</div>--%>
							</div>

						</div>

						<%--<div class="row">
							<div class="col-12 medium font-weight-bold text-center" data-i18n="[html]altura_cap">Altura de CAP</div>
						</div>--%>

						<div class="row alturacap2 divarrlist form-inline"></div>

						<hr class="divarrlist" />
						<div class="divarrlist box-title" data-i18n="[html]FUP_datos_constructivos">Datos Constructivos</div>
						<div class="divarrlist box-title" data-i18n="[html]FUP_datos_Urbanisticos">Basado en Datos Urbanísticos</div>																															   
						<div class="row divarrlist form-inline">
							<div class="col-2" data-i18n="[html]forma_construccion">Forma de Construcción</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectFormaConstruccion" data-modelo-tecnico="FormaConstructiva" class="fuparr fuplist form-control">
									<option value="-1">Seleccionar</option>
									<option value="1">Aislada</option>
									<option value="2">Espejo</option>
									<option value="3">Pareada</option>
									<option value="4">Aislada Espejo</option>
									<option value="5">Aislada Pareada</option>
									<option value="6">Espejo Pareada</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:100%; height:100%' src='Imagenes//Forma de Construccion.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2">
                                <span data-i18n="[html]distancia_minima_edificaciones">Dist. Min. Edificios / Vivienda </span>
                                <span class="MedidaEspesores">(cm):</span>
							</div>
                            
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" type="number" min="0" id="txtDistMinEdificaciones" class="fuparr form-control fuplist" data-modelo-tecnico="DistanciaEdifica" />                                
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:100%; height:100%;float:right' src='Imagenes/Distancia Minima entre Edificios.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>

							</div>
							<div class="col-1 forsapro" data-i18n="[html]desnivel">Desnivel</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectDesnivel" data-modelo-tecnico="Desnivel" class="fuparr form-control fuplist forsapro">
									<option value="-1">Seleccionar</option>
									<option value="1">Ascendente</option>
									<option value="2">Descendiente</option>
									<option value="3">No Aplica</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda forsapro" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:100%; height:100%;float:right' src='Imagenes//Desnivel.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>
						<div class="row divarrlist form-inline">
							<div class="col-2" data-i18n="[html]presenta_dilatacion_muros">Junta de dilatacion entre muros</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectJuntaDilatacion" data-modelo-tecnico="DilatacionMuro" class="form-control fuparr fuplist">
									<option value="-1">Seleccionar</option>
									<option value="1">SI</option>
									<option value="2">NO</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes/junta de Dilatación en Muro.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2 divEspesorJuntas">
                                <span data-i18n="[html]espesor_juntas">Espesor entre juntas</span>
                                <span class="MedidaEspesores"> (cm):</span>
							</div>
							<div class="col-2 divEspesorJuntas" style="display: inline-table">
								<input style="width: 80% !important;" type="number" min="0" id="txtEspesorJuntas" data-modelo-tecnico="EspesorJunta" class="form-control fuparr fuplist" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:90%; height:90%' src='Imagenes/29_juntas de dilatacion entre muros-espesor entre juntas.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>
						<hr class="divarrlist" />

						<hr class="divarrlist" />

						<div class="row">
							<div class="col-8"></div>
							<div class="col-2">
								<button id="btnGuardarInformacionGeneral" type="button" onclick="guardarFUP_informacionGeneral()" class="btn btn-primary fupgen fupgenpt0 " >
									<i class="fa fa-save" style="padding-right: inherit;">  </i> <span data-i18n="[html]FUP_guardar"></span>
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
								<div class="col-md-12" id="Equipos">
									<div id="headerEquipos" class="box box-primary">
										<div class="box-header border-bottom border-primary" style="z-index: 3;">
											<table class="col-md-12 ">
												<tr>
													<td width="97%">
														<h5 id="titleEquipos" class="box-title" data-i18n="[html]EquipoBaseyAdicionales">Equipo Base y Adicionales</h5>
													</td>
													<td width="3%">
														<div class="col-md-12" style="padding-bottom: 4px;">
															<button id="collapseEquipos" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 5px;"><span class="fa fa-angle-double-up"></span></button>
														</div>
													</td>
												</tr>
											</table>
										</div>
										<div id="bodyEquipos" class="box-body" style="padding-top: 20px;">
											<div class="row">
												<div class="col-12">
													<table class="table table-sm table-hover" id="tbEquipos" style="display: normal">
														<thead>
															<tr>
																<th class="text-center" colspan="9" data-i18n="[html]EQUIPOBASE">EQUIPO BASE
																</th>
															</tr>
															<tr>
																<td class="text-center" data-i18n="[html]ConsecutivoEquipoBase">Consecutivo</td>
																<td class="text-center" width="8%" data-i18n="[html]CantidadEquipoBase">Cant</td>
																<td class="text-center"></td>
																<td class="text-center" data-i18n="[html]TipoEquipoBase">Tipo</td>
																<td class="text-center" width="1%">
																	<button type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="top" data-original-title="<img src='imagenes//31_img_tipo equipo.jpg'></img>"><i class="fa fa-info-circle fa-lg"></i></button></td>
																<td class="text-center" width="10%"></td>
																<td class="text-center" width="10%"></td>
																<td class="text-center" width="40%">
																	<table>
																		<tr>
																			<td style="border-top: 0px;" data-i18n="[html]DescripcionEquipoBase">Descripción</td>
																			<td style="border-top: 0px;" width="3%">
																				<%--<button type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="top" data-html="true" title="<div align='left'><b>Ayuda</b><br/>- Juego de caps panel de ciclo<br/>- Trepante<br/>- Apartamentos planta tipo torre 1<br/>- Para realizar dos armados por nivel</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
																				<button type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="top" data-original-title="<img style='float:right' src='imagenes//Equipo Base - Eq Nuevo.jpg'></img>"><i class="fa fa-info-circle fa-lg"></i></button></td>
																			</td>
																		</tr>
																	</table>

																</td>
															</tr>
														</thead>
														<tbody class="bodyEquipos">
															<tr id='add_Equipo1'>
																<td id="consecutivo1" class="text-center">
																	<label style='margin-top: 8px' class="EqConsecutivo">1</label></td>
																<td>
																	<input type="number" min="0" id='txtCant1' placeholder='Cant' class="EqCant" style="margin-top:6px" />
																</td>
																<td>
																	<label style="margin-top: 8px" data-i18n="[html]Equipos">Equipo(s)</label></td>
																<td>
																	<select style="" id='cmbTipoEquipo1' placeholder='Tipo Equipo' class="form-control EqSelect">
																		<option value="1">Sencillo(s)</option>
																		<option value="2">Duplex</option>
																		<option value="3">Triplex</option>
																		<option value="4">Cuadruplex</option>
																		<option value="5">Qutuplex</option>
																		<option value="6">Sextuplex</option>
																	</select>
																	<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/31_img_tipo equipo.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
																</td>
																<td>
																	<label style="margin-top: 8px">de </label>
																</td>
																<td>
																	<label class="lblTipoProductoEquipos" style="margin-top: 8px"></label>
																</td>
																<td>
																	<label style="margin-top: 8px" data-i18n="[html]pararealizarEquipos">para realizar </label>
																</td>
																<td>
																	<input type="text" id='txtDescEquipo1' placeholder='Descripcion' class="form-control EqDesc" />
																</td>
															</tr>
														</tbody>
														<tfoot>
															<tr>
																<td colspan="9">
																	<button type="button" style="background-color: #bac0c5 !important;" id="add_Equipo" class="btn btn-secondary btn-block align-center fupgen fupgenpt1 forsapro" data-i18n="[html]AgregarEquipo">Agregar Equipo</button>
																</td>
															</tr>
														</tfoot>
													</table>
												</div>
											</div>
											<div class="row">

												<div class="col-12">
													<table class="table table-sm table-hover" id="tbAdaptaciones">
														<thead>
															<tr>
																<th class="text-center" colspan="6" data-i18n="[html]AdaptacionesAdicionales">ADICIONALES
																</th>
															</tr>
															<tr>
																<td class="text-center" data-i18n="[html]ConsecutivoEquipos">Consecutivo Equipo</td>
																<td class="text-center" width="15%"></td>
																<td class="text-center" width="10%"></td>
																<td class="text-center" width="10%"></td>
																<td class="text-center" width="45%" data-i18n="[html]DescripcionEquipos">Descripción</td>
																<td style="border-top: 0px;" width="4%">
																	<%--<button type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="top" data-html="true" title="<div align='left'><b>Ayuda</b><br/>- Juego de caps panel de ciclo<br/>- Trepante<br/>- Apartamentos planta tipo torre 1<br/>- Para realizar dos armados por nivel</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
																	<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='float:right' src='Imagenes/Adicionales.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
																</td>
															</tr>
														</thead>
														<tbody>
															<tr id='add_Adapta1'>
																<td>
																	<select class="cmbAdaptacion form-control" id='cmbEquipo1' placeholder='# Equipo'>
																		<option id="1">1</option>
																	</select>
																</td>
																<td>
																	<label style="margin-top: 8px" data-i18n="[html]FormaletaAdicional">Formaleta adicional </label>
																</td>
																<td>
																	<label class="lblTipoProductoEquipos" style="margin-top: 8px"></label>
																</td>
																<td>
																	<label style="margin-top: 8px" data-i18n="[html]ParaRealizarFormaleta">para realizar </label>
																</td>
																<td>
																	<input type="text" id='txtDescAdapt0' placeholder='Descripcion' class="form-control DespAdaptacion" />
																</td>
																<td></td>
															</tr>
														</tbody>
														<tfoot>
															<tr>
																<td colspan="6">
																	<button type="button" style="background-color: #bac0c5 !important;" id="add_Adapta" class="btn btn-secondary btn-block fupgen fupgenpt1 forsapro2" data-i18n="[html]AgregarAdaptacion">Agregar Adaptación</button>
																</td>
															</tr>
														</tfoot>
													</table>

												</div>
											</div>
											<div class="row justify-content-end">
												<button type="button" onclick="GuardadoEquiposyAdap()" class="btn btn-primary fupgen fupgenpt1 "  value="Guardar Equipo Base y Adicionales">
												<i class="fa fa-save"></i> <spam data-i18n="[html]EquipoBaseyAdicionales">Guardar Equipo Base y Adicionales</spam>
												</button>
											</div>
										</div>
									</div>
								</div>
								<div id="LineasDimanicas"></div>
							    <div class="" style="margin-top: 15px; margin-left: 15px; display: flex;">
								    <button type="button" onclick="SimuListado()" class="btn btn-success fupgenlist" style="align-self: flex-start">
									    <i class="fa fa-list-alt" style="padding-right: inherit;">  </i><span class="ml-2">Cotizar Lista Asistida</span>
								    </button>
    							    <button type="button"  style="align-self: flex-start" class="btn btn-info fupgenlist ml-3" data-toggle="modal" onclick="window.location = 'Imagenes/ListaAsistida/PLANTILLA LISTA ASISTIDA-Final.xlsm';" >
									    <i class="fa fa-list-alt" style="padding-right: inherit;">  </i><span class="ml-2">Plantilla Lista Asistida</span>
								    </button>
                                    <button type="button"  style="align-self: flex-start" class="btn btn-info fupgenlist ml-3" data-toggle="modal" onclick="exportarListaAsistida()" >
									    <i class="fa fa-download" style="padding-right: inherit;">  </i><span class="ml-2">Exportar Lista Asistida</span>
								    </button>
<%--                                    <button type="button" onclick="APIForline()" class="btn btn-info ml-3 verforline" >
									    <i class="fa fa-list-alt" style="padding-right: inherit;">  </i><span class="ml-2">ForlinePlus</span>
								    </button>--%>
							    </div>
                                <div class="row fupgenlist">
                                    <div class="ml-3 border p-2" data-i18n="[html]ConsideraPlantillaLista" style="font-size: larger; font-weight:bold;">
										Considera descargar y usar la plantilla Lista Asistida disponible en este módulo, para evitar 
										inconvenientes con la cotización del listado. Recuerda desbloquear las macros, dando click
										derecho en las propiedades del archivo, al frente de SEGURIDAD, click en DESBLOQUEAR y GUARDAR
									</div>
                                </div>

								<div class="row justify-content-start" style="margin-top: 15px; margin-left: 15px;">
<%--									<button type="button" onclick="autorizarSubirPlanos()" class="btn btn-info" id="btnAutorizarSubirPlanos">
									    <i class="fa fa-check" style="padding-right: inherit"></i><span class="ml-1">Autorizar Subir Planos</span>
								    </button>--%>
									<button type="button" class="btn btn-secondary ml-2 fupgen fupgenenv " data-toggle="modal" onclick="UploadFielModalShow('Cargar Archivo',0,'Alcance Oferta')">
										Subir Archivos
									</button>
								</div>
								<div class="row justify-content-start" style="margin-top: 15px; margin-left: 15px;">
									<button type="button"  class="btn btn-success fupgen fupgenenv " data-toggle="modal" onclick="ActualizarEstado(2)">
										<i class="fa fa-envelope" style="padding-right: inherit"></i>
										Enviar
									</button>
								</div>
							</div>
						</div>
					</div>
				</div>

			<div class="card" id="ParteAprobacionFUP">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseAprobacionFUP" data-i18n="FUP_aprobacion_fup">Aprobación del FUP </a>
				</div>
				<div id="collapseAprobacionFUP" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-12">
      							<table id="tbAprob" class="table table-sm table-borderless">
									<tbody id="tbodyAprob">
										<tr>
                                            <td width="8%"><span data-i18n="[html]FUP_numero_modulaciones">No. Modulaciones</span></td>
                                            <td width="9%"><input id="txtNumeroModulaciones" type="number" min="0" style="width: 80% !important;" data-modelo-aprobacion="NumeroModulaciones" onblur="NivelComplejidad()" /></td>
                                            <td width="8%"><span data-i18n="[html]FUP_numero_cambios">No. Cambios</span></td>
                                            <td width="9%"><input id="txtNumeroCambios" type="number" min="0" style="width: 80% !important;" data-modelo-aprobacion="NumeroCambios" onblur="NivelComplejidad()" /></td>
                                            <td width="8%">Altura Formaleta</td>
                                            <td width="9%"><input type="number" id="txtAlturaFormaletaAproba" style="width: 80% !important;" class="NumeroSalcot" data-modelo-aprobacion= "AlturaFormaleta"/></td>
                                            <td width="8%" class ="NoComercial">M2 Cargados CT</td>
                                            <td width="9%" class ="NoComercial"><input type="number" id="txtM2CtAproba" disabled style="width: 80% !important;" /></td>
										</tr>
                                        <tr>
                                            <td width="8%">Solicitud DFT</td>
                                            <td width="9%"><input type="text" id="txtSolicitudDFT" style="width: 80% !important;" disabled/></td>
                                            <td width="8%">Fecha Aprob DFT</td>
                                            <td width="9%"><input type="text" id="txtFecAprobDFT" style="width: 80% !important;" disabled/></td>
                                            <td width="8%">M2 No Modulados</td>
                                            <td width="9%"><input type="text" id="txtM2NoModulados" style="width: 80% !important;"/></td>
                                            <td width="8%" class ="NoComercial">No. Ordenes Referencia</td>
                                            <td width="9%" class ="NoComercial"><input type="number" id="txtOrdenRefAproba" style="width: 80% !important;" disabled /></td>
                                        </tr>
										<tr class ="fupServiciosOcultar">
                                            <td>Fec Solicitud Cliente</td>
                                            <td><input type="date" id="txtFecsolcliAproba" disabled/></td>
                                            <td>Fec Aprobacion</td>
                                            <td><input type="text" id="txtFecAprAproba" disabled/></td>
                                            <td>Piezas Cargadas CT</td>
                                            <td><input type="number" id="txtPiezasCtAproba" style="width: 80% !important;" disabled/></td>
                                            <td class ="NoComercial">Piezas / M2</td>
                                            <td class ="NoComercial"><input type="number" id="txtPiezasM2Aproba" style="width: 80% !important;" disabled /></td>
										</tr>
										<tr>
                                            <td class="font-weight-bold ">Nivel de Complejidad</td>
                                            <td ><input type="text" id="txtNivelComplejidad" style="width: 80% !important" class="font-weight-bold " style="color:red" disabled />
                                                 <input style="width:0% !important" type="number" id="txtidNivelComplejidad" data-modelo-aprobacion="NivelComplejidad" hidden /> </td>
                                            <td >Dias según Política</td>
                                            <td><input type="text" id="txtDiasPolitica" data-modelo-aprobacion="DiasPolitica" style="width: 80% !important;" disabled/></td>
                                            <td ><span class ="fecpol">Fecha según Política </span></td>
                                            <td ><input class ="fecpol" type="date" id="txtFecPolitica" data-modelo-aprobacion="FecPolitica" disabled/></td>
                                            <td >Fecha Planeada x SCI</td>
                                            <td><input type="date" id="txtFecConfirmSCI" disabled/></td>
										</tr>
										<tr class ="fupServiciosOcultar">
                                            <td >Tipo Sistema Seguridad</td>
                                            <td colspan="7"><textarea id="txtTipoSisSeg" class="form-control" rows="2" disabled ></textarea> </td>
										</tr>
										<tr class ="fupServiciosOcultar">
                                            <td >Detalles Arquitectonicos</td>
                                            <td colspan="7"><textarea id="txtDetallesAproba" class="form-control" rows="2" disabled ></textarea></td>
										</tr>
										<tr class ="fupServiciosOcultar">
                                            <td ></td>
							                <td  colspan="3"  data-i18n="[html]FUP_LinkObra">
								                Link Obra: 
							                </td>
                                            <td  colspan="3">
                                                <input id="txtLinkObraApr" disabled type="text" class="form-control"/>
                                            </td>
										</tr>
										<tr class ="fupServiciosOcultar">
                                            <td></td>
                                            <th colspan="6">
                                                <table id="tbAprob2" class="table table-sm table-bordered">
								                    <thead class="thead-light">
									                    <tr>
										                    <th class="text-center" width="25%">Tipo de Proyecto</th>
										                    <th class="text-center" width="5%"></th>
                                                            <th class="text-center" width="70%">Consideraciones</th>
									                    </tr>
								                    </thead>
									                <tbody id="tbodyAprob2" >
										                <tr>
                                                            <td rowspan="3" > PROYECTOS DEFINITIVOS <br>(planos maduros u obra en construcción)</td>
                                                            <td></td>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="ckPol1A">
                                                                    <label class="form-check-label" for="flexCheckDefault">
                                                                    a) Proyectos en etapa de Construcción o en lanzamiento de ventas (link de la obra)
                                                                    </label>
                                                             </td>
                                                        </tr>
										                <tr>
                                                            <td></td>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="ckPol1B">
                                                                    <label class="form-check-label" for="flexCheckDefault">
                                                                    b) Planos estructurales y arquitectónicos coordinados.
                                                                    </label>
                                                             </td>
                                                        </tr>
										                <tr>
                                                            <td></td>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="ckPol1C">
                                                                    <label class="form-check-label" for="flexCheckDefault">
                                                                    c) Inicio de obra en los siguientes 12 meses
                                                                    </label>
                                                             </td>
                                                        </tr>
										                <tr>
                                                            <td colspan ="6"> <hr> </td>
                                                        </tr>
										                <tr>
                                                            <td rowspan="4"> PROYECTOS NO DEFINITIVOS <br>(planos en proceso u obra en lanzamiento)</td>
                                                            <td></td>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="ckPol2A">
                                                                    <label class="form-check-label" for="flexCheckDefault">
                                                                    a) Solo planos arquitectonicos o Solo estructurales.
                                                                    </label>
                                                             </td>
                                                        </tr>
										                <tr>
                                                            <td></td>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="ckPol2B">
                                                                    <label class="form-check-label" for="flexCheckDefault">
                                                                    b) Planos estructurales y arquitectonicos NO coordenados.
                                                                    </label>
                                                             </td>
                                                        </tr>
										                <tr>
                                                            <td></td>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="ckPol2C">
                                                                    <label class="form-check-label" for="flexCheckDefault">
                                                                    c) Sin información de inicio de obra en los próximos 12 meses.
                                                                    </label>
                                                             </td>
                                                        </tr>
										                <tr>
                                                            <td></td>
                                                            <td><input class="form-check-input" type="checkbox" value="" id="ckPol2D">
                                                                    <label class="form-check-label" for="flexCheckDefault">
                                                                    d) Planos en formato JPG o PDF.
                                                                    </label>
                                                             </td>
                                                        </tr>
									                </tbody >

                                                </table>
                                            </td>
										</tr>
										<tr class ="fupServiciosOcultar">
                                            <td ></td>
							                <td colspan="2">Tipo de Proyectos  (Calidad de los planos)</td>
                                            <td colspan="2">
                                                <select id="txtTipoPoryectoAp" class="form-control " data-modelo-aprobacion="TipoProyectoApId" >
										            <option value=-1>Seleccione Tipo Proyecto</option>
										            <option value=1>Proyecto Definitivo</option>
										            <option value=2>Proyecto No Definitivo</option>
                                                </select>
                                            </td>
										</tr>
										<tr>
                                            <td colspan="5"></td>
										</tr>
										<tr class ="fupServiciosOcultar">
                                            <td ></td>
                                            <td colspan="3">
							                    <table class="table table-sm table-hover" id="tab_rfichaPrev">
								                    <tbody id="tbodyrFichaPrev">
									                    <tr>
										                    <td class="text-center" width="70%">Requiere Mesa Técnica PreVenta con el Cliente:</td>
										                    <td class="text-center" width="30%">
                                                                <div class="form-group onoffswitch" >
                                                                    <input type="checkbox" onchange="sinoitemPreventa()"  name="onoffswitch" class="onoffswitch-checkbox" id="SiNoPreventa">
                                                                        <label class="onoffswitch-label" for="SiNoPreventa">
                                                                            <span class="onoffswitch-inner"></span>
                                                                            <span class="onoffswitch-switch"></span>
                                                                        </label>
                                                                    </div>
										                    </td>
<%--										                    <td class="text-center" width="25%"> 
							                                    <button type="button" class="btn btn-primary fupapro" onclick="GrabaFichaPreventa()">
								                                    <i class="fa fa-save"></i> <span>. Mesa Tecnica Preventa</span>
							                                    </button>
										                    </td>--%>
									                    </tr>
                                                        <tr>
										                    <td colspan="2">
                                                                <input id="txtInfoPreventa" type="text" disabled/>
										                    </td>
									                    </tr>
								                    </tbody>
							                    </table>
                                            </td>
                                            <td></td>
                                            <td colspan="2">
							                    <table class="table table-sm table-hover" id="tab_fichaPrev">
								                    <thead >
									                    <tr>
        								                    <th class="text-center" colspan ="3">
                                                                <button type="button" class="btn btn-default fupapro" data-toggle="modal" onclick="UploadFielModalShow('Subir Ficha Tecnica Preventa',41,'Aprobacion')">
								                                    Subir Acta Mesa Tecnica Preventa  
							                                    </button>
        								                    </th>
									                    </tr>
								                    </thead>
								                    <thead class="thead-light">
									                    <tr>
										                    <th class="text-center" width="70%">Ver Acta Mesa Tecnica Preventa</th>
										                    <th class="text-center" width="22%">Fecha</th>
										                    <th class="text-center" width="8%"> </th>
									                    </tr>
								                    </thead>
								                    <tbody id="tbodyFichaPrev">
								                    </tbody>
							                    </table>
                                            </td>
										</tr>
										<tr>
                                            <td ><span data-i18n="[html]FUP_vobo">VoBo FUP</span></td>
                                            <td ><select id="cboVoBoFup" class="form-control " data-modelo-aprobacion="Visto_bueno"></select></td>
                                            <td class="DevolCot"><span data-i18n="[html]FUP_motivo_rechazo">Motivo de rechazo</span></td>
                                            <td class="DevolCot"><select id="cboMotivoRechazoFup" class="form-control " data-modelo-aprobacion="MotivoRechazo"></select></td>
										</tr>
										<tr>
                                            <!-- <td width="10%"><span >Estado DFT</span></td>
                                            <td width="20%"><input type="text" id="txtEstadoDft" class="form-control " data-modelo-aprobacion="EstadoDft" disabled="disabled" /></td>
                                            <!-- <td width="20%"><select id="cboEstadoDFT" class="form-control " data-modelo-aprobacion="EstadoDft"></select></td> -->
                                            <!-- <td width="10%"><span >Fecha Aprobación</span></td>
                                            <td class="text-center" width="10%"><input type="text" id="txtFecAprobaDFT" data-modelo-aprobacion="FechaDft" disabled="disabled" /></td>
                                            <td class="font-weight-bold " style="color:blue" width="10%">Versión</td>
                                            <td width="5%"><input type="text" id="txtVersionAprobDFT" class="font-weight-bold " style="color:blue" disabled /></td> -->
										</tr>
                                        <!-- <tr>
                                            <td><span >Requiere Recotización DFT</span></td>
                                            <td><input type="text" id="txtRequiereReco" class="form-control " data-modelo-aprobacion="RequiereReco" disabled="disabled"/></td>
                                            <!-- <td><select id="cboRequiereReco" class="form-control " data-modelo-aprobacion="RequiereReco"></select></td> 
                                            <td width="10%"><span >Fecha Aprobación Cliente DFT</span></td>
                                            <td class="text-center" width="10%"><input type="text" id="txtFecAprobaClienteDFT" data-modelo-aprobacion="FechaClienteDft" disabled="disabled" /></td>
                                        </tr> -->
                                        <!-- <tr>
                                            <td ><span data-i18n="[html]FUP_observacion_aprobacion">Observaciones</span> DFT</td>
                                            <td colspan="5"><textarea id="txtObservacionAprobacionDFT" class="form-control" rows="3" data-modelo-aprobacion="ObservacionAprobacionDFT" disabled="disabled"></textarea></td>
										</tr> -->
                                        <tr>
                                            <td ><span data-i18n="[html]FUP_observacion_aprobacion">Observaciones</span></td>
                                            <td colspan="6"><textarea id="txtObservacionAprobacion" class="form-control" rows="3" data-modelo-aprobacion="ObservacionAprobacion"></textarea></td>
										</tr>
										<tr>
                                            <td ></td>
                                            <td colspan="4" align="center">
                                                <button id="btnImprimirAprobacion" type="button" class="btn btn-primary" value="Imprimir FUP" onclick="PrepararReporteFUP();">
								                    <i class="fa fa-print"></i> <span data-i18n="[html]FUP_imprimir_aprobacion" > imprimir</span>
								                </button>
                                                <button id="btnGuardarAprobacion" type="button" class="btn btn-primary fupapro fupsalcie" value="Guardar y Notificar">
								                    <i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar_aprobacion" > guardar</span>
								                </button> 
                                            </td>
                                            <td ></td>
										</tr>
									</tbody >
                                </table>
                            </div>
                                
						</div>

						<div class="row fupServiciosOcultar">
							<div class="col-12  medium font-weight-bold " >Simulador Cotizaciones</div>
						</div>
						<div class="row fupServiciosOcultar">
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
							<div class="col-2" >
                                <table class="table table-sm table-hover" id="tab_FecSimulacion">
								    <thead class="thead-light">
									    <tr>
										    <th class="text-center medium font-weight-bold">Fecha Simulacion</th>
									    </tr>
								    </thead>
								    <tbody >
									    <tr>
										    <td class="text-center"><input id="txtFecSimulacion" type="text" disabled /></td>
									    </tr>
								    </tbody>
							    </table>                                
							</div>
                            <div class="col-4 msjSimu"> <h5 style="color:red"> Falta Explosionar CT </h5></div>

					    </div>
						<div class="row fupServiciosOcultar">
                            <div class="col-2"></div>
						   <div class="col-8">
								<button id="btnGenerarOrdenCotizacion" type="button" class="btn btn-primary fupapro fupOrdcot2 controlarDisponibilidadAprobacion" value="Generar Orden Cotizacion" onclick="guardarOrdenCotizacion();">
								<i class="fa fa-cogs pr-3"></i> <span > Generar Orden Cotización</span>
								</button> 
						   
								<button id="btnExplosionarOrdenCotizacion" type="button" class="btn btn-primary fupapro fupOrdcot2 " value="Explosionar Orden Cotizacion" onclick="explosionarOrdenCotizacion();">
								<i class="fa fa-cogs pr-3"></i> <span>  Explosionar CT</span>
								</button> 

								<button id="btnReporteCT" type="button" class="btn btn-primary fupsalco fupOrdcot" value="Listado Orden Cotizacion" onclick="LlamarReporteListadoCT(1);">
								<i class="fa fa-print pr-3"></i> <span>  Listado Orden CT</span>
								</button> 
								<button id="btnReporteCTkg" type="button" class="btn btn-success fupsalco fupOrdcot" value="Listado Orden Cotizacion" onclick="LlamarReporteListadoCT(2);">
								<i class="fa fa-print pr-3"></i> <span>  Listado Orden CT KG</span>
								</button> 
                                    
                                <button id="btnMostrarResumenOrden" type="button" class="btn btn-primary" onclick="$('#tabsResumenOrdenContainer').toggle();">
                                    <i class="fa fa-book pr-3"></i> <span>  Resumen Orden</span>
                                </button>
<%--								<button id="btnReporteOrdenCotizacion" type="button" class="btn btn-primary" value="Reporte Simulador Orden Cotizacion"  onclick="LlamarReporteSimulador();">
								<i class="fa fa-print"></i> <span>  Simulador Orden CT</span>
								</button> --%>
							</div>
                            <div class="col-2"></div>
						</div>

                        <div id="tabsResumenOrdenContainer" style="display:none;">
                            <hr />
                            <div id="resumenOrdenContainer">
                                <ul class="nav nav-tabs" id="resumenOrdenTabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active show" data-toggle="tab" href="#resumen" role="tab" aria-controls="resumen" aria-selected="true" style="font-size:initial !important">Resumen</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#aluminio" role="tab" aria-controls="aluminio" aria-selected="false" style="font-size:initial !important">Aluminio</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#accesorios" role="tab" aria-controls="accesorios" aria-selected="false" style="font-size:initial !important">Accesorios y seguridad</a>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-content" id="resumenOrdenTabsContents">
                                <div class="tab-pane fade active show" id="resumen" role="tabpanel" aria-labelledby="resumen-tab">
                                    <div class="container-fluid mt-3">
                                        <table class="table table-striped table-sm text-center col-8 offset-0 table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="5">Resumen</th>
                                                </tr>
                                                <tr>
                                                    <th>Nivel</th>
                                                    <th>M2</th>
                                                    <th>Valor</th>
                                                    <th>Valor x M2</th>
                                                    <th>Kilogramos</th>
                                                    <th>Valor x Kilo</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td class="camposResumenOrden" id="tdResumenM21"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValor1"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxM21"></td>
                                                    <td class="camposResumenOrden" id="tdResumenKilogramos1"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxKilo1"></td>
                                                </tr>
                                                <tr>
                                                    <td>2</td>
                                                    <td class="camposResumenOrden" id="tdResumenM2nivelAccesorio2"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValornivelAccesorio2"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxM2nivelAccesorio2"></td>
                                                    <td class="camposResumenOrden" id="tdResumenKilogramosnivelAccesorio2"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxKilonivelAccesorio2"></td>
                                                </tr>
                                                <tr>
                                                    <td>3</td>
                                                    <td class="camposResumenOrden" id="tdResumenM2nivelAccesorio3"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValornivelAccesorio3"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxM2nivelAccesorio3"></td>
                                                    <td class="camposResumenOrden" id="tdResumenKilogramosnivelAccesorio3"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxKilonivelAccesorio3"></td>
                                                </tr>
                                                <tr>
                                                    <td>4</td>
                                                    <td class="camposResumenOrden" id="tdResumenM2nivelAccesorio4"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValornivelAccesorio4"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxM2nivelAccesorio4"></td>
                                                    <td class="camposResumenOrden" id="tdResumenKilogramosnivelAccesorio4"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxKilonivelAccesorio4"></td>
                                                </tr>
                                                <tr>
                                                    <td>5</td>
                                                    <td class="camposResumenOrden" id="tdResumenM2nivelAccesorio5"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValornivelAccesorio5"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxM2nivelAccesorio5"></td>
                                                    <td class="camposResumenOrden" id="tdResumenKilogramosnivelAccesorio5"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxKilonivelAccesorio5"></td>
                                                </tr>
                                                <tr>
                                                    <td>Total</td>
                                                    <td class="camposResumenOrden" id="tdResumenM2Total"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorTotal"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxM2Total"></td>
                                                    <td class="camposResumenOrden" id="tdResumenKilogramosTotal"></td>
                                                    <td class="camposResumenOrden" id="tdResumenValorxKiloTotal"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="aluminio" role="tabpanel" aria-labelledby="aluminio-tab">
                                    <div class="container-fluid mt-3">
                                        <div class="row">
                                            <span class="col-1">Piezas Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtPiezasAlum" disabled="disabled" /></span>
                                            <span class="col-1">M2 Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtM2Alum" disabled="disabled" /></span>
                                            <span class="col-1">Piezas / M2 Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtPiezasM2Alum" disabled="disabled" /></span>
                                            <div class="col-3"><div class="text-center font-weight-bold">Proyecto</div></div>
                                        </div>
                                        <div class="row">
                                            <span class="col-1">Costo COP / Kilo</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtCostoCOPKilo" disabled="disabled" /></span>
                                            <span class="col-1">Costo M2 Cop Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtCostoM2CopAlum" disabled="disabled" /></span>
                                            <span class="col-1">Costo Alum COP</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtCostoAlumCOP" disabled="disabled" /></span>
                                            <span class="col-1">Costo Total COP</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtCostoTotalCOP" disabled="disabled" /></span>
                                        </div>
                                        <div class="row">
                                            <span class="col-1">Kilos / M2 Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtKilosM2Alum" disabled="disabled" /></span>
                                            <span class="col-1">Costo USD / Kilo</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtCostoUSDKilo" disabled="disabled" /></span>
                                            <span class="col-1">Costo M2 Usd Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtCostoM2USDAlum" disabled="disabled" /></span>
                                            <span class="col-1">Costo USD</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenInputs" id="txtCostoUSD" disabled="disabled" /></span>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <table class="table table-striped table-sm text-center col-8 offset-0 table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th colspan="5">Aluminio</th>
                                                    </tr>
                                                    <tr>
                                                        <th></th>
                                                        <th>01 - Aluminio</th>
                                                        <th>02 - Aluminio Kanbam</th>
                                                        <th>03 - No Modulado</th>
                                                        <th>Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Cant Piezas</td>
                                                        <td class="camposResumenOrden" id="tdCantPiezas1"></td>
                                                        <td class="camposResumenOrden" id="tdCantPiezas4"></td>
                                                        <td class="camposResumenOrden" id="tdCantPiezas3"></td>
                                                        <td class="camposResumenOrden" id="tdCantPiezasTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>M2</td>
                                                        <td class="camposResumenOrden" id="tdM21"></td>
                                                        <td class="camposResumenOrden" id="tdM24"></td>
                                                        <td class="camposResumenOrden" id="tdM23"></td>
                                                        <td class="camposResumenOrden" id="tdM2Total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pieza x M2</td>
                                                        <td class="camposResumenOrden" id="tdPiezaxM21"></td>
                                                        <td class="camposResumenOrden" id="tdPiezaxM24"></td>
                                                        <td class="camposResumenOrden" id="tdPiezaxM23"></td>
                                                        <td class="camposResumenOrden" id="tdPiezaxM2Total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Kilos</td>
                                                        <td class="camposResumenOrden" id="tdKilos1"></td>
                                                        <td class="camposResumenOrden" id="tdKilos4"></td>
                                                        <td class="camposResumenOrden" id="tdKilos3"></td>
                                                        <td class="camposResumenOrden" id="tdKilosTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Kilos x M2</td>
                                                        <td class="camposResumenOrden" id="tdKilosxM21"></td>
                                                        <td class="camposResumenOrden" id="tdKilosxM24"></td>
                                                        <td class="camposResumenOrden" id="tdKilosxM23"></td>
                                                        <td class="camposResumenOrden" id="tdKilosxM2Total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Chatarra</td>
                                                        <td class="camposResumenOrden" id="tdCostoChatarra1"></td>
                                                        <td class="camposResumenOrden" id="tdCostoChatarra4"></td>
                                                        <td class="camposResumenOrden" id="tdCostoChatarra3"></td>
                                                        <td class="camposResumenOrden" id="tdCostoChatarraTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Kilos Ch</td>
                                                        <td class="camposResumenOrden" id="tdKilosCh1"></td>
                                                        <td class="camposResumenOrden" id="tdKilosCh4"></td>
                                                        <td class="camposResumenOrden" id="tdKilosCh3"></td>
                                                        <td class="camposResumenOrden" id="tdKilosChTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>% Ch</td>
                                                        <td class="camposResumenOrden" id="tdPcCh1"></td>
                                                        <td class="camposResumenOrden" id="tdPcCh4"></td>
                                                        <td class="camposResumenOrden" id="tdPcCh3"></td>
                                                        <td class="camposResumenOrden" id="tdPcChTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Mp</td>
                                                        <td class="camposResumenOrden" id="tdCostoMp1"></td>
                                                        <td class="camposResumenOrden" id="tdCostoMp4"></td>
                                                        <td class="camposResumenOrden" id="tdCostoMp3"></td>
                                                        <td class="camposResumenOrden" id="tdCostoMpTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>% Mp</td>
                                                        <td class="camposResumenOrden" id="tdPcMp1"></td>
                                                        <td class="camposResumenOrden" id="tdPcMp4"></td>
                                                        <td class="camposResumenOrden" id="tdPcMp3"></td>
                                                        <td class="camposResumenOrden" id="tdPcMpTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Total Mp x Kilo</td>
                                                        <td class="camposResumenOrden" id="tdCostoTotalMpxKilo1"></td>
                                                        <td class="camposResumenOrden" id="tdCostoTotalMpxKilo4"></td>
                                                        <td class="camposResumenOrden" id="tdCostoTotalMpxKilo3"></td>
                                                        <td class="camposResumenOrden" id="tdCostoTotalMpxKiloTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Insertos</td>
                                                        <td class="camposResumenOrden" id="tdInsertos1"></td>
                                                        <td class="camposResumenOrden" id="tdInsertos4"></td>
                                                        <td class="camposResumenOrden" id="tdInsertos3"></td>
                                                        <td class="camposResumenOrden" id="tdInsertosTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Total Mp</td>
                                                        <td class="camposResumenOrden" id="tdCostototalMp1"></td>
                                                        <td class="camposResumenOrden" id="tdCostototalMp4"></td>
                                                        <td class="camposResumenOrden" id="tdCostototalMp3"></td>
                                                        <td class="camposResumenOrden" id="tdCostototalMpTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>MOD</td>
                                                        <td class="camposResumenOrden" id="tdMOD1"></td>
                                                        <td class="camposResumenOrden" id="tdMOD4"></td>
                                                        <td class="camposResumenOrden" id="tdMOD3"></td>
                                                        <td class="camposResumenOrden" id="tdMODTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo MOD x Kilo</td>
                                                        <td class="camposResumenOrden" id="tdCostoMODxKilo1"></td>
                                                        <td class="camposResumenOrden" id="tdCostoMODxKilo4"></td>
                                                        <td class="camposResumenOrden" id="tdCostoMODxKilo3"></td>
                                                        <td class="camposResumenOrden" id="tdCostoMODxKiloTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>CIF</td>
                                                        <td class="camposResumenOrden" id="tdCIF1"></td>
                                                        <td class="camposResumenOrden" id="tdCIF4"></td>
                                                        <td class="camposResumenOrden" id="tdCIF3"></td>
                                                        <td class="camposResumenOrden" id="tdCIFTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo CIF x Kilo</td>
                                                        <td class="camposResumenOrden" id="tdCostoCIFxKilo1"></td>
                                                        <td class="camposResumenOrden" id="tdCostoCIFxKilo4"></td>
                                                        <td class="camposResumenOrden" id="tdCostoCIFxKilo3"></td>
                                                        <td class="camposResumenOrden" id="tdCostoCIFxKiloTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo x Item</td>
                                                        <td class="camposResumenOrden" id="tdCostoxItem1"></td>
                                                        <td class="camposResumenOrden" id="tdCostoxItem4"></td>
                                                        <td class="camposResumenOrden" id="tdCostoxItem3"></td>
                                                        <td class="camposResumenOrden" id="tdCostoxItemTotal"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="accesorios" role="tabpanel" aria-labelledby="accesorios-tab">
                                    <div class="container-fluid mt-3">
                                        <table class="table table-striped table-sm text-center col-8 offset-0 table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="6">Accesorios y Sistema Seguridad</th>
                                                </tr>
                                                <tr>
                                                    <th></th>
                                                    <th>02</th>
                                                    <th>03</th>
                                                    <th>04</th>
                                                    <th>05</th>
                                                    <th>Total</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Cant Piezas</td>
                                                    <td class="camposResumenOrden" id="tdCantPiezasASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCantPiezasASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCantPiezasASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCantPiezasASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCantPiezasASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Peso x Item</td>
                                                    <td class="camposResumenOrden" id="tdPesoxItemASS2"></td>
                                                    <td class="camposResumenOrden" id="tdPesoxItemASS3"></td>
                                                    <td class="camposResumenOrden" id="tdPesoxItemASS4"></td>
                                                    <td class="camposResumenOrden" id="tdPesoxItemASS5"></td>
                                                    <td class="camposResumenOrden" id="tdPesoxItemASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Costo Prom x Pieza</td>
                                                    <td class="camposResumenOrden" id="tdCostoPromxPiezaASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCostoPromxPiezaASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCostoPromxPiezaASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCostoPromxPiezaASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCostoPromxPiezaASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Costo x Item</td>
                                                    <td class="camposResumenOrden" id="tdCostoxItemASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCostoxItemASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCostoxItemASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCostoxItemASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCostoxItemASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Total Costo x Item</td>
                                                    <td class="camposResumenOrden" id="tdTotalCostoxItemASS2"></td>
                                                    <td class="camposResumenOrden" id="tdTotalCostoxItemASS3"></td>
                                                    <td class="camposResumenOrden" id="tdTotalCostoxItemASS4"></td>
                                                    <td class="camposResumenOrden" id="tdTotalCostoxItemASS5"></td>
                                                    <td class="camposResumenOrden" id="tdTotalCostoxItemASSTotal"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- EII --%>
						<hr />
                        <div class="row fupServiciosOcultar">
                            <div class="col-2">
                                <table id="tbContratomrv" class="table table-sm table-borderless Solomrv">
                                    <thead>                   
                                        <tr align="right">
                                            <td><a style="font-size:14px; font-weight: bold" download="ContratoMRV.pdf" href="Imagenes//BR//ContratoMRV.pdf">Contrato MRV</a></td>
                                        </tr>
                                        <tr align="right">
                                            <td><a style="font-size:14px; font-weight: bold" download="DestaquesContratoForsaMRV2022.pdf" href="Imagenes//BR//DestaquesContratoForsaMRV2022.pdf">Destaques Contrato MRV</a></td>
                                        </tr>
                                        <tr align="right">
                                            <td><a style="font-size:14px; font-weight: bold" download="InteraccionesCliente_SIO_MRV.pdf" href="Imagenes//BR//InteraccionesCliente_SIO_MRV.pdf">Interacciones Clientes</a></td>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
							<div class="col-8 medium font-weight-bold ">
								<table id="tbSegCotizacion" class="table table-sm">
                                    <thead>
                                            <tr class="thead-light" align="center">
                                                <th colspan="6" align="center" >Seguimiento Entrega de la cotización</th>
                                            </tr>
                                            <tr>
                                                <th ></th>
                                                <th colspan="2">Fecha Solicitud Cliente</th>
                                                <th colspan="2"><input id="txtFecSolCliente" type="date" readonly /></th>
                                                <th ></th>
                                            </tr>
                                            <tr>
                                                <th ></th>
                                                <th colspan="2">Fecha Aprobacion</th>
                                                <th colspan="2"><input id="txtFecAprobacion" type="date" readonly /></th>
                                                <th ></th>
                                            </tr>
										    <tr class="thead-light">
											    <th class="text-center" width="30%">Evento</th>
											    <th class="text-center" width="10%">Días</th>
											    <th class="text-center" width="15%">Fecha Planeada</th>
											    <th class="text-center" width="15%">Fecha Confirmada</th>
											    <th class="text-center" width="15%">Fecha Final</th>
											    <th class="text-center" width="15%">Aprobo Cliente</th>
										    </tr>
                                    </thead>
									<tbody id="tbodySegCotizacion">
										<tr>
											<td width ="30%">Modulacion Aluminio </td>
											<td width ="10%"><input type="text" min="0" id="txtDiasEvento1" datamodel-fecseg="AlDias" onblur="CalcularFecPlan(1)" /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec1Evento1" readonly  /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec2Evento1" datamodel-fecseg="AlConfirma" /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec3Evento1" datamodel-fecseg="AlReal" /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec4Evento1" datamodel-fecseg="AlAprobado" /></td>
										</tr>
										<tr>
											<td width ="30%">Modulacion Accesorios</td>
											<td width ="10%"><input type="text" min="0" id="txtDiasEvento2" datamodel-fecseg="AccDias" onblur="CalcularFecPlan(2)" /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec1Evento2" readonly /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec2Evento2" datamodel-fecseg="AccConfirma" /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec3Evento2" datamodel-fecseg="AccReal" /></td>
											<td class="text-center" width="15%"><input type="date" id="txtFec4Evento2" datamodel-fecseg="AccAprobado" /></td>											
										</tr>
									</tbody>
                                    <tfoot>
                                        <tr class="thead-light" align="center"> 
                                            <td colspan="6"> <button type="button" onclick="guardarFecSeg()" class="btn btn-primary controlarDisponibilidadAprobacion" >
									            <i class="fa fa-save">  </i> <span data-i18n="[html]FUP_guardar"></span>
								                </button>
											</td>
                                        </tr>
                                    </tfoot>
								</table>
							</div>
                            <div class="col-2"></div>
						</div>
<%-- EII --%>

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
				<a class="collapsed card-link" data-toggle="collapse" href="#collapseSalidaCot" data-i18n="[html]sc_salida_cotizacion">Salida Cotizacion</a>
			</div>
			<div id="collapseSalidaCot" class="collapse" data-parent="#accordion">
				<div class="card-body">
                    <div class="row msjSimu">
                        <div class="col-12 "> <h5 style="color:red"> PROYECTO SIN SIMULACION DE COSTO NO SE PUEDE DAR SALIDA </h5></div>
                    </div>
                    <div class="row">
<%--                        <div class="col-2"></div>--%>
                        <div class="col-6">
					        <table id="tab_DatosSalidaCoti" class="table table-sm table-borderless">
						        <thead>
							        <tr  class="thead-light">
								        <th class="text-center" colspan ="6">DETALLE SALIDA COTIZACIÓN</th>
							        </tr>
									<tr>
										<th class="text-center" width="40%"> </th>
										<th class="text-center" width="30%">M2</th>
										<th class="text-center" width="30%">Valor</th>
										<%--<th class="text-center" width="10%"> </th>--%>
									</tr>
						        </thead>
								<tbody id="tbodyDetalleSalida">
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_m2EquipoBase">M2, Equipo Base</th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtM2EquipoBase" class="sumM2SalidaCot NumeroSalcot" data-modelosc="m2_equipo" />
						                </th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtValEquipoBase" class="sumValSalidaCot NumeroSalcot" data-modelosc="vlr_equipo" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_m2_adicionales">Nivel 1 M2, Adicionales</th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtM2Adicionales" class="sumM2SalidaCot NumeroSalcot" data-modelosc="m2_adicionales" />
						                </th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtValAdicionales" class="sumValSalidaCot NumeroSalcot" data-modelosc="vlr_adicionales" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_detalle_arq">Nivel 1 M2, Detalles Arquitectonicos</th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtDetArqM2SC" class="sumM2SalidaCot NumeroSalcot" data-modelosc="m2_Detalle_arquitectonico" />
						                </th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtDetArqValorSC" class="sumValSalidaCot NumeroSalcot" data-modelosc="vlr_Detalle_arquitectonico" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_total_encofrados">Total Encofrados</th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtTotalMSC" class="NumeroSalcot" disabled="disabled" />
						                </th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtTotalValorSC" class="NumeroSalcot" disabled="disabled" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_sis_seguridad">Perimetros de Sist. Seguridad</th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtSistemaTrepanteAccsc" class="NumeroSalcot" data-modelosc="m2_sis_seguridad" />
						                </th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtAccSistemaSegSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_sis_seguridad" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_accesorios_basicos">Accesorios Basicos</th>
						                <th class="col-2"></th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtAccBasicosSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_basico" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_accesorios_complementarios">Accesorios Complementarios</th>
						                <th class="col-2"></th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtAccComplSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_complementario" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_accesorios_opcionales">Accesorios Opcionales</th>
						                <th class="col-2"></th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtAccOpcionalesSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_opcionales" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_accesorios_adicionales">Accesorios Adicionales</th>
						                <th class="col-2"></th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtAccAdicionalesSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_accesorios_adicionales" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_otros_productos">Otros Productos / Servicios</th>
						                <th class="col-2"></th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtOtrosProductoSC" class="sumValSalidaCot2 NumeroSalcot" data-modelosc="vlr_otros_productos" />
						                </th>
                                    </tr>
                                    <tr>
						                <th class="col-2" data-i18n="[html]sc_total_propuestas">Total Propuesta Com.</th>
						                <th class="col-2"></th>
						                <th class="col-2">
							                <input type="number" step="0.01" min="0" id="txtTotalPropuestaCom" class="NumeroSalcot" disabled="disabled"/>
						                </th>
                                    </tr>
                                    <tr>
                                    </tr>
								</tbody>
                            </table>
                        </div>

                        <div class="col-6">
					        <table id="tab_DatosNumCambios" class="table table-sm table-borderless">
						        <thead>
							        <tr  class="thead-light">
								        <th class="text-center" colspan ="3"># CAMBIOS</th>
							        </tr>
									<tr>
										<th class="text-center" width="40%"> </th>
										<th class="text-center" width="30%"></th>
										<th class="text-center" width="30%"></th>
									</tr>
						        </thead>
                                <tbody id="tbodyDetalleSalida">
                                    <tr>
							            <td class="col-2" data-i18n="[html]FUP_numero_modulaciones">No. Modulaciones</td>
							            <td class="col-1">
								            <input id="txtNumeroModulacionesSC" type="number" min="0" style="width: 80% !important;" data-modelosc="NumeroModulacionesSC" />
							            </td>
                                    </tr>
                                    <tr>
							            <td class="col-2" data-i18n="[html]FUP_numero_cambios">No. Cambios</td>
							            <td class="col-1">
								            <input id="txtNumeroCambiosSC" type="number" min="0" style="width: 80% !important;"  data-modelosc="NumeroCambiosSC" />
							            </td>
                                    </tr>
                                    <tr>
<%--										<th class="text-center" width="10%"> </th>--%>
									    <td class="col-2" data-i18n="[html]FUP_fecha_EntregaCliente">Fecha Aval Cierre *</td>
									    <td class="col-1" class="text-center" ><input id="txtFechaAvalCierre" type="date"  data-modelosc="FechaAvalCierre" /></td>

<%--									    <th width="40%" data-i18n="[html]FUP_fecha_EntregaCliente">Fecha Aval Cierre *</th>
									    <th width="40%" class="text-center" ><input id="txtFechaAvalCierre" type="date" /></th>--%>
<%--                                        <th class="col-1">
                                            <button id="btnGrabaFechaAvalCierre" class="btn" data-toggle="tooltip" onclick="GrabaFechasCliente(2)" data-i18n="[title]FUP_GrabarfechaAvalCierrre">Fecha Aval Cierre<i class="fa fa-floppy-o"></i></button>
                                        </th>--%>
										<th class="text-center" width="10%"> </th>

                                    </tr>
                                    <tr><td colspan="3" ></td></tr>
                                    <tr><td colspan="3" ></td></tr>
							        <tr  class="thead-light servAdapta">
								        <th class="text-center" colspan ="6">SERVICIOS DE ADAPTACIÓN</th>
							        </tr>
                                    <tr class="servAdapta">
							            <td class="col-2">Servicio de Adaptación Finalizado</td>
							            <td class="col-1">
								            <input id="txtSerAdaptacion" type="checkbox" value="" />
							            </td>
                                    </tr>
                                    <tr><td colspan="3" ></td></tr>
                                    <tr>
                                        <td colspan="3" class="text-center"><span style="font-size: 16px; text-decoration:underline; color:red">¡ IMPORTANTE ! </span></td>
                                    </tr>
                                    <tr>
										<td colspan="3" align="justify">**Los valores de salida de Forsa 1Clic son por cantidad de piezas y <b style="font-size: 14px; color:red">NO</b> por cantidad de m2.</td></tr>
                                    <tr>
										<td colspan="3" align="justify">** Los valores de Servicio técnico en obra son por días, <b style="font-size: 14px; color:red">NO</b>  por cantidad de m2.</td></tr>
                                    <tr>
                                        <td colspan="3" class="fletenal" align="justify">No olvides dar click en CALCULAR FLETE para las Cotizaciones Nacionales en Colombia.</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
					<div class="row">
					</div>
					<div class="row">
                        <!-- Cambio pedido el 21/07 -->
                        <table class="table table-sm table-borderless">
                            <tbody>
                                <tr>
                                    <td width="15%"><span >Estado DFT</span></td>
                                    <td width="20%"><input type="text" id="txtEstadoDft" class="form-control " data-modelo-aprobacion="EstadoDft" disabled="disabled" /></td>
                                    
                                    <!-- <td><select id="cboRequiereReco" class="form-control " data-modelo-aprobacion="RequiereReco"></select></td> -->
                                    <td width="15%"><span >Fecha Aprobación Cliente DFT</span></td>
                                    <td class="text-center" width="20%"><input type="text" id="txtFecAprobaClienteDFT" data-modelo-aprobacion="FechaClienteDft" disabled="disabled" /></td>
                                    <td class="font-weight-bold " style="color:blue" width="10%">Versión</td>
                                    <td width="5%"><input type="text" id="txtVersionAprobDFT" class="font-weight-bold " style="color:blue" disabled /></td>
                                </tr>
                                <tr>
                                    <td width="15%"><span >Requiere Recotización DFT</span></td>
                                    <td width="20%"><input type="text" id="txtRequiereReco" class="form-control " data-modelo-aprobacion="RequiereReco" disabled="disabled"/></td>
                                    <td width="15%"><span >Fecha Aprobación</span></td>
                                    <td class="text-center" width="20%"><input type="text" id="txtFecAprobaDFT" data-modelo-aprobacion="FechaDft" disabled="disabled" /></td>
                                </tr>
                                <tr>
                                    <td ><span data-i18n="[html]FUP_observacion_aprobacion">Observaciones</span> DFT</td>
                                    <td colspan="5"><textarea id="txtObservacionAprobacionDFT" class="form-control" rows="3" data-modelo-aprobacion="ObservacionAprobacionDFT" disabled="disabled"></textarea></td>
								</tr>
                            </tbody>
                        </table>
                        <!-- -->						
					</div>
                    <div class="row">
                        <div class="offset-2" id="divMsgRegistroSolicitudCartaManual" style="font-size: 18px; text-decoration:underline">

                        </div>
                    </div>
					<div class="row">
						<div class="col-2">
                            <div class="col-2"><button id="btnCartaCotizacion" type="button" class="btn btn-primary" value="Reporte Carta Cotizacion" onclick="LlamarCartaCot();">
								<i class="fa fa-print" style="padding-right: inherit;"></i> <span> Ir a Carta Cotización</span>
								</button></div>
						</div>
                        <div class="col-2" id="btnSolicitarCartaManual" style="display: none;" >
                            <button type="button" class="btn btn-info" onclick="RegistrarSolicitudCartaManual(1)">Solicitar Carta Manual</button>
                        </div>
                        <div class="col-2" id="btnAutorizarCartaManual" style="display: none;" >
                            <button type="button" class="btn btn-info" onclick="RegistrarSolicitudCartaManual(2)">Autorizar Carta Manual</button>
                        </div>
                        <div class="col-2" id="btnNegarCartaManual" style="display: none;" >
                            <button type="button" class="btn btn-info" onclick="RegistrarSolicitudCartaManual(3)">Negar Carta Manual</button>
                        </div>
                        <div class="col-2" id="btnCancelarSolicitudCartaManual" style="display: none;" >
                            <button type="button" class="btn btn-info" onclick="RegistrarSolicitudCartaManual(4)">Cancelar Soli Carta Manual</button>
                        </div>
                        <div class="col-2">
                            <button type="button" class="btn btn-primary" onclick="window.location = 'Imagenes/accessories_and_consumables_template.xlsx';">
                                <i class="fa fa-file-excel-o" style="padding-right: inherit"></i>
                                <span>Plantilla Accesorios Y Consumibles</span>
                            </button>
                        </div>
					</div>

                    <div class="row">
                        <div class="col-7">
							<table id="tbLinks" class="table table-sm">
								<thead>
									<tr class="thead-light">
										<th class="text-center" colspan ="4">Enlace a Entregables (ASCEND)</th>
									</tr>
									<tr>
										<th width="32%" class="text-center">Link</th>
										<th width="60%" class="text-center">Descripción</th>
                                        <th width="5%" class="text-center"></th>
										<th width="3%" align="center" >
											<button id="btnAddLink" class="btn btn-sm btn-link align-center" data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
										</th>
									</tr>
								</thead>
								<tbody id="tbodyLinkSC"></tbody>
								<tfoot>
										<tr></tr>
										<tr class="row justify-content-end" colspan="2">
                                            <th>
											    <button type="button" class="btn btn-primary" data-toggle="modal" onclick="SaveLinks()">
											        <i class="fa fa-save" style="padding-right: inherit;"></i> <span> Guardar Links</span>
											    </button>
                                            </th>
										</tr>
										<tr></tr>
								</tfoot>
							</table>
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
										<th align="justify" colspan="4"> <span style="font-size: 14px;" id="CiudadObrSalcot"> Ciudad de la Obra </span> </th>
									</tr>
									<tr>
										<td align="justify" colspan="4"> <span style="font-size: 14px; text-decoration:underline;"> EL valor del flete es estimado y puede tener variaciones, por lo que el valor final se confirmará antes del despacho del Equipo cotizado</span> </td>
									</tr>
									<tr>
										<td colspan ="3" align="center">
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect fupsalco fupcot fletenal"  onclick="calcular_flete_loc()">
											<i class="fa fa-save"></i> <span> Calcular Flete</span>
											</button>
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect fupsalco fupcot fletenal" onclick="guardar_flete(2)">
											<i class="fa fa-save"></i> <span> Guardar Flete</span>
											</button>
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect " onclick="ReporteFlete()">
											<i class="fa fa-print"></i> <span>. Carta Flete</span>
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
											<button type="button" class="btn btn-primary fupsalco fupsalcoServicio" data-toggle="modal" onclick="GuardarComentario(1)">
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
							<button type="button" class="btn btn-default fupsalcoManual fupsalcoServicio" data-toggle="modal" onclick="UploadFielModalShow('Subir Carta Cotizacion',6,'Salida Cotizacion')">
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
					<%--Precio Minimo--%>
						<div class="col-4 ">
                            <div class="PminiomSimu">
                                <table class="table table-sm table-hover" id="tab_sug">
                                    <thead>
                                        <tr class="table-info">
                                            <th class="text-center">Descuento Maximo</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodysug">
                                                
                                    </tbody>
                                </table>
                                <style>
                                    td {
                                        vertical-align: middle !important;
                                    }
                                    .discountOutOfRanges {
                                        color: red !important;
                                        font-weight: bolder !important;
                                    }
                                </style>
    						</div>
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
							<button type="button" class="btn btn-success  fupsalcie" onclick="ActualizarEstado(44)">
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
								<div class="row">
									<div class="col-2"></div>
									<div class="col-4">
										<button id="btnGuardardevsc" type="button" class="btn btn-primary fupcot fupave controlarDisponibilidadAprobacion" onclick="guardarDevComercial()" value="Devolucion Comercial"  >
											<i class="fa fa-undo"></i> <span>Devolver</span>
										</button>
									</div>
									<div class="col-4"></div>
								</div>
								<hr />
								<div class="row">
									<div class="col-12">
										<table class="table table-sm table-hover" id="tab_devolucionsc">
											<thead class="thead-light">
												<tr>
													<th class="text-center" data-i18n="[html]FUP_fecha_detalle_devoluciones" width="10%">Fecha</th>
													<th class="text-center" data-i18n="[html]FUP_motivo_detalle_devoluciones" width="30%">Motivo Devolución</th>
													<th class="text-center" data-i18n="[html]FUP_observacion_detalle_devoluciones" width="40%">Observación</th>
													<th class="text-center" width="20%">Estado</th>
												</tr>
											</thead>
											<tbody id="tbodyDevolucionsc">
											</tbody>
										</table>
									</div>
								</div>
							</div>

						</div>

					</div>

				</div>
			</div>
			</div>

			<%-- Nuevo Acordeón Septiembre 21019 --%>
			<div class="card" id="parteProcesoCartaCotizacion">
			<div class="card-header">
				<a class="collapsed card-link" data-toggle="collapse" href="#collapseProcesoCartaCotizacion" data-i18n="[html]ProcesoCartaCotizacion">Proceso Carta Cotizacion</a>
			</div>
			<div id="collapseProcesoCartaCotizacion" class="collapse" data-parent="#accordion">
				<div class="card-body">

				</div>
			</div>
			</div>

			<%-- ------------------------------- --%>

			<div class="card" id="ParteSolicitudRecotizacion">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseSolicitudRecotizacion" data-i18n="[html]FUP_solicitud_recotizacion">Solicitud de Re-Cotización	</a>
				</div>
				<div id="collapseSolicitudRecotizacion" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-4" style="display: inline-table">
								<button class="btn btn-link" data-toggle="tooltip" title="Control Cambios" onclick=" ControlCambioShow('Control de Cambios',0,'Solicitud Recotizacion',0)"><i class="fa fa-comment"></i></button>								
                                <b data-i18n="[html]FUP_cntrcmb">Control de Cambios</b>
							</div>                            
                        </div>
				   
							<div id="Div1" class="box-body" padding-top: 20px; margin-left: 15px; margin-right: 15px;">
								<div class="row">
									<div class="col-2" data-i18n="[html]FUP_Estado">Estado</div>
									<div class="col-2">
										<select id="cboEstadoSolRecotizacion">
											<option value="0">No</option>
											<option value="1">Si</option>
										</select>
									</div>
									<div class="col-2" data-i18n="[html]FUP_motivo_recotizacion">Motivo Re-Cotización</div>
									<div class="col-3">
										<select id="cboTipoRecotizacionFup" class="form-control ">
										</select>
									</div>
									<div class="col-3"></div>
								</div>
								<div class="row">
									<div class="col-2" data-i18n="[html]FUP_observacion_aprobacion">Observación</div>
									<div class="col-8">
										<textarea id="txtObservacionRecotizacion" class="form-control" rows="3"></textarea>
									</div>
								</div>
								<div class="row">
									<div class="col-2"></div>
									<div class="col-4">
										<button id="btnGuardarRecotizacion" type="button" class="btn btn-primary fupcot controlarDisponibilidadAprobacion" value="Recotizar"  >
											<i class="fa fa-undo" ></i> <span data-i18n="[html]FUP_btnrecotiza">Recotizar</span>
										</button>
									</div>
									<div class="col-4"></div>
								</div>
								<hr />
								<div class="row">
									<div class="col-12">
										<table class="table table-sm table-hover" id="tab_recotizacion_fup">
											<thead class="thead-light">
												<tr>
													<th class="text-center" data-i18n="[html]FUP_fecha">Fecha</th>
													<th class="text-center" data-i18n="[html]FUP_version">Version</th>
													<th class="text-center" data-i18n="[html]FUP_motivo_recotizacion">Motivo</th>
													<th class="text-center" data-i18n="[html]FUP_observacion_aprobacion">Observación</th>
													<th class="text-center" data-i18n="[html]FUP_ver_carta_cotizacion">Ver Carta COT</th>
												</tr>
											</thead>
											<tbody id="tbodyRecotizacionFup">
											</tbody>
										</table>
									</div>
								</div>
							</div>						  
					</div>
				</div>
			</div>

			<div class="card" id="PartePlanosTipoForsa">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapsePlanosTipoForsa" >Definición Técnica del Proyecto</a>
				</div>
				<div id="collapsePlanosTipoForsa" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" data-i18n="[html]FUP_Evento">Evento</div>
							<div class="col-4">
								<select id="cboEstadoPlanoTipoForsa" class="form-control" data-PTF="Evento">
								</select>
							</div>
<%--							<div class="col-1 varPTFCom" data-i18n="[html]FUP_Planos">Planos</div>
							<div class="col-2 varPTFCom">
								<select id="cboPlanosPlanoTipoForsa" class="form-control" data-PTF="Plano">
								</select>
							</div>
							<div class="col-4 justify-content-end">
									<button id="btnPTFListaCH" class="btn btn-primary " data-toggle="tooltip" title="Lista de Chequeo Planos"><i class="fa  fa-file-text"></i> </button>
							</div>--%>
						</div>
<%--						<div class="row varPTFSopCom">
							<div class="col-1" data-i18n="[html]FUP_responsable">Responsable S.C.</div>
							<div class="col-3">
								<select id="cboResponsablePlanoTipoForsa" class="form-control" data-PTF="Responsable">
								</select>
							</div>
							<div class="col-2">Fecha Entrega Soporte Com.</div>
							<div class="col-2">
								<input id="dtFechaCierrePTF"  data-PTF="FechaCierre" type="date" />
							</div>
						</div> --%>
                        <div class="row ActaSeguimientoDFT">
							<div class="col-2 varPTFCom" data-i18n="[html]FUP_Planos">Planos</div>
							<div class="col-2 varPTFCom">
								<select id="cboPlanosNuevosCot" class="form-control" data-PTF="Plano">
									<option value="1">Seleccionar...</option>
									<option value="2">Planos Nuevos</option>
									<option value="3">Planos Cotización</option>
								</select>
							</div>                        
                        </div>
                        <div class="row ActaSeguimientoDFTPrograma">
							<div class="col-2 varPTFCom" >Fecha Programada SCI</div>
							<div class="col-2 varPTFCom">
                                <input id="dtProgramadaSCI" type="date" />
							</div>                        
                        </div>
                        <div class="row ActaSeguimientoDFTAval">
							<div class="col-2 varPTFCom" >Fecha Aval (Aprobación de Planos)</div>
							<div class="col-2 varPTFCom">
                                <input id="dtFechaAval" type="date" />
							</div>                        
							<div class="col-2 varPTFCom" >Requiere Recotizar</div>
							<div class="col-2 varPTFCom">
								<select id="cboRequiereCotiza" class="form-control" data-PTF="Plano">
									<option value="1">Seleccionar...</option>
									<option value="2">Si</option>
									<option value="3">No</option>
								</select>
							</div>                        
                        </div>

						<div class="row">
							<div class="col-2" data-i18n="[html]FUP_observacion_aprobacion">Observación</div>
							<div class="col-8">
								<textarea id="txtObservacionPFT" class="form-control" rows="3" data-PTF="Observacion"></textarea>
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-4">
								<button id="btnGuardarPTF" type="button" class="btn btn-primary controlcambiosDFT" >
									<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar">guardar</span>
								</button>
							</div>
							<div class="col-6"></div>
						</div>

						<hr />

						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_ptf_fup">
									<thead class="thead-light">
										<tr>
											<th class="text-center" colspan=10>Ciclo PTF</th>
										</tr>
										<tr>
											<th width="10%" class="text-center" data-i18n="[html]FUP_Evento">Estado</th>
											<th width="10%" class="text-center" >Fecha Registro</th>
											<th width="10%" class="text-center" >Fecha Entrega SCI.</th>
											<th width="10%" class="text-center" >Fecha Acta</th>
											<th width="15%" class="text-center" >Usuario</th>
											<th width="15%" class="text-center" data-i18n="[html]FUP_responsable">Responsable</th>
											<th width="20%" class="text-center" data-i18n="[html]FUP_observacion_aprobacion">Observación</th>
											<th width="7%" class="text-center" >Tipo Plano</th>
											<th width="3%"class="text-center" data-i18n="[html]FUP_archivos">Archivos</th>
										</tr>
<%--                                        <tr>
											<th colspan="8" class="text-center" data-i18n="[html]FUP_observacion_aprobacion">Observación</th>
										</tr>
--%>                                    </thead>
									<tbody id="tbodyPtfFup">
									</tbody>
								</table>
							</div>
						</div>
<%--						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tblAnexoPTF">
									<thead class="thead-light">
										<tr>
											<th class="text-center" colspan=10>Anexos sección PTF</th>
										</tr>
										<tr>
											<th class="text-center" width="10%">Fecha</th>
											<th class="text-center" width="15%" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" width="25%">Evento</th>
											<th class="text-center" width="45%" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" width="5%"> </th>
										</tr>
									</thead>
									<tbody id="tbodyAnexoPTF">
									</tbody>
								</table>
							</div>
						</div>--%>

					</div>
				</div>
			</div>
		
			<div class="card" id="ParteFletesFUP">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseFletesFUP" >Fletes</a>
				</div>
				<div id="collapseFletesFUP" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div id="pnlExportacion">
							<div class="row">
								<div class="col-6 justify-content-start" style="margin-top: 15px; margin-left: 15px;">
									<table id="tbExportaFlete" class="table table-sm table-hover table-borderless">
										<thead class="thead-light">
											<tr>
												<th class="text-center col-2" >EXPORTACIÓN</th>
												<th class="text-center col-1" > </th>
												<th class="text-center col-1" > </th>
											</tr>
										</thead>
										<tbody id="tbody2">
										</tbody>
									</table>
								</div>
							</div>
							  
							<div class="row">
								<div class="col-1"></div>
								<div class="col-2" >Transportador</div>
								<div class="col-2">
									<input type="text" id="IdTransp"  data-flete="transportador_id" style="display: none"></input>
									<input type="text" id="lblTransp" disabled="disabled"/>
								</div>
							</div>
							  
							<div class="row">
								<div class="col-1"></div>
								<div class="col-2" >Agente de Carga Internacional</div>
								<div class="col-2">
									<input type="text" id="IdAgentCarga" data-flete="agente_carga_id" style="display: none"></input>
									<input type="text" id="lblAgentCarga" disabled="disabled""/>
								</div>
							</div>
							  
							<div class="row">
								<div class="col-1"></div>
								<div class="col-2">Termino de Negociacion</div>
								<div class="col-3">
									<select id="selectTerminoNegociacion2" data-flete="termino_negociacion_id" style="width: 80% !important;">
									</select>
								</div>
							</div>
							  
							<div class="row">
								<div class="col-1"></div>
								<div class="col-2">Puerto de Cargue</div>
								<div class="col-3">
									<select id="selectPuertoCargue" data-flete="puerto_origen_id" style="width: 80% !important;">
									</select>
								</div>
							</div>
							  
							<div class="row">
								<div class="col-1"></div>
								<div class="col-2">Puerto de Descargue</div>
								<div class="col-3">
									<select id="selectPuertoDescargue" data-flete="puerto_destino_id" style="width: 80% !important;">
									</select>
								</div>
							</div>
							<div class="row">
								<div class="col-1"></div>
								<div class="col-2">Tasa TRM</div>
								<div class="col-3">
									<input type="number" class="NumeroSalcot" id="txtTrm" disabled="disabled" data-flete="vr_trm"/>
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-6 justify-content-start"  style="margin-top: 15px; margin-left: 15px;">
								<table  id="tbCotizaFlete" class="table table-sm table-hover table-borderless">
								<thead class="thead-light">
									<tr>
										<th width="30%">COTIZACIÓN</th>
										<th width="20%"> </th>
										<th width="50%"> </th>
									</tr>
								</thead>
								<tbody id="tbody4">
									<tr>    
										<td width="30%">Valor Cotizado</td>
										<td width="20%">
										</td>
										<th width="50%"> <input type="number" class="NumeroSalcot" id="txtTotalPropuestaComF" disabled="disabled"/></th>
									</tr>
									<tr>    
										<td width="30%">Valor EXW Base para Calculo de Fletes</td>
										<td width="20%">
										</td>
										<th width="50%"> <input type="number" class="NumeroSalcot" id="txtValorEXW" /></th>
									</tr>
									<tr>
										<td>
											<span id="lblCiudadObraFlete" class="label label-default">Ciudad de la Obra</span>
											<span id="IdPaisObraFlete" class="label label-default">Id</span>
											<span id="IdCiuObraFlete" class="label label-default">Id</span>                                            
										</td>
										<td>
											<span id="LblCiuObraFlete" class="label label-warning">Ciudad Obra</span>
										</td>
										<th> </th>
									</tr>
									<tr>
										<td colspan="3">
											<h6>
												<ins>
													<span id="LVehic" class="label label-info">VEHICULOS</span>
												</ins>
											</h6>
										</td>
									</tr>
									<!--<tr id="filaLTipoTurbo">
										<td>
											<span id="LTipoTurbo">Turbo</span>
										</td>
										<td>
											<input type="number" class="NumeroSalcot" id="fletetxtCant3" data-flete="cantidad_t3"/>
										</td>
										<td>
											<input type="text" class="NumeroSalcot" id="lblVrTipo3" disabled="disabled" data-flete="vr_origen_t3"/>
										</td>                                        
									</tr>-->
									<tr id="filaLtipo1">
										<td>
											<span id="LTipo1"></span>
										</td>
										<td>
											<input type="number" class="NumeroSalcot" id="fletetxtCant1" data-flete="cantidad_t1"/>
										</td>
										<td>
											<input type="text" class="NumeroSalcot" id="lblVrTipo1" disabled="disabled" data-flete="vr_origen_t1"/>
										</td>                                        
									</tr>
									<!--<tr id="filaMinimula">
										<td>
											<span id="LMinimula">Minimula</span>
										</td>
										<td>
											<input type="number" class="NumeroSalcot" id="fletetxtCant4" data-flete="cantidad_t4"/>
										</td>
										<td>
											<input type="text" class="NumeroSalcot" id="lblVrTipo4" disabled="disabled" data-flete="vr_origen_t4"/>
										</td>                                       
									</tr>-->
									<tr id="filaLtipo2">
										<td>
											<span id="LTipo2">Turbo</span>
										</td>
										<td>
											<input type="number" class="NumeroSalcot" id="fletetxtCant2" data-flete="cantidad_t2"/>
										</td>
										<td>
											<input type="number" class="NumeroSalcot" id="lblVrTipo2" disabled="disabled" data-flete="vr_origen_t2"/>
										</td>                                        
									</tr>
									<tr>
										<td>
											<span id="LTFPD">Lead Time</span>
										</td>
										<td>
											<span id="lblLTF" data-flete="leadTime"></span>
											<span id="Span1">Días</span>
										</td>
										<td></td>
									</tr>
                                    <tr>
							            <td>
							                <span>Flete Internacional 20 STD </span>
							            </td>
										<td>
							            </td>
										<td>
								            <input type="number" class="NumeroSalcot" id="VrInternal1" disabled="disabled" data-flete="vr_internacional_t1"></input>
							            </td>
						            </tr>
                                    <tr>
							            <td>
							                <span>Flete Internacional 40 HC </span>
							            </td>
										<td>
							            </td>
										<td>
								            <input type="number" class="NumeroSalcot" id="VrInternal2" disabled="disabled" data-flete="vr_internacional_t2"></input>
							            </td>
						            </tr>
                                    <tr>
							            <td>
							                <span>Gastos En Puerto Origen</span>
							            </td>
										<td>
							            </td>
										<td>
								            <input type="number" class="NumeroSalcot" id="VrGastPtoOrig" disabled="disabled" data-flete="vr_gastos_origen"></input>
							            </td>
						            </tr>
                                    <tr>
							            <td>
							                <span>Aduana Origen</span>
							            </td>
										<td>
							            </td>
										<td>
								            <input type="number" class="NumeroSalcot" id="VrDespAduana" disabled="disabled" data-flete="vr_aduana_origen"></input>
							            </td>
						            </tr>
                                    <tr>
							            <td>
							                <span>Seguro</span>
							            </td>
										<td>
							            </td>
										<td>
								            <input type="number" class="NumeroSalcot" id="VrSeguro" disabled="disabled" data-flete="vr_seguro"></input>
							            </td>
						            </tr>
									<tr class="trCamposDAT">
                                        <td>
                                            <span>Gastos Destino</span>
                                        </td>
                                        <td></td>
                                        <td>
                                            <input type="number" class="NumeroSalcot Value-Campo-DAT" id="txtGastosDestino"/>
                                        </td>
                                    </tr>
									<tr class="trCamposDAT">
                                        <td>
                                            <span>Valor Impuestos</span>
                                        </td>
                                        <td></td>
                                        <td>
                                            <input type="number" class="NumeroSalcot Value-Campo-DAT" id="txtValorImpuestos"/>
                                        </td>
                                    </tr>
                                    <tr class="trCamposDAT">
                                        <td>
                                            <span>Aduana Destino</span>
                                        </td>
                                        <td></td>
                                        <td>
                                            <input type="number" class="NumeroSalcot Value-Campo-DAT" id="txtAduanaDestino"/>
                                        </td>
                                    </tr>
                                    <tr class="trCamposDAT">
                                        <td>
                                            <span>Valor destino T20</span>
                                        </td>
                                        <td></td>
                                        <td>
                                            <input type="number" class="NumeroSalcot Value-Campo-DAT" id="txtValorT1"/>
                                        </td>
                                    </tr>
                                    <tr class="trCamposDAT">
                                        <td>
                                            <span>Valor destino T40</span>
                                        </td>
                                        <td></td>
                                        <td>
                                            <input type="number" class="NumeroSalcot Value-Campo-DAT" id="txtValorT2"/>
                                        </td>
                                    </tr>
									<tr>
										<td>VALOR FLETE</td>
                                        <td></td>
										<td>
											<input type="text" class="NumeroSalcot"  id="LVrFlete" disabled="disabled" />
										</td>
									</tr>
									<tr>
										<td>VALOR TOTAL</td>
										<td></td>                                       
										<td>
											<input type="text" class="NumeroSalcot" id="LVrTotalFlete" disabled="disabled"/>
										</td>
									</tr>
									<tr>
										<td colspan ="3" align="center">
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect"  onclick="calcular_flete()">
											<i class="fa fa-save"></i> <span>. Calcular Flete</span>
											</button>
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect controlarDisponibilidadAprobacion" onclick="guardar_flete(1)">
											<i class="fa fa-save"></i> <span>. Guardar Flete</span>
											</button>
											<button type="button" class="btn btn-primary btn-sm m-0 waves-effect controlarDisponibilidadAprobacion" onclick="ReporteFlete()">
											<i class="fa fa-print"></i> <span>. Carta Flete</span>
											</button>
										</td>
									</tr>

								</tbody>
								</table>
							</div>
						</div>

						<div class="row varflete">
							<div class="col-1 varflete"></div>
							<div class="col-2 varflete" >Transporte Interno</div>
							<div class="col-2 varflete"></div>
							<div class="col-2 varflete">
								<label id="VrTransInterno" data-i18n="[html]FUP_flete_VrTransInterno" class="varflete">0</label>
							</div>
						</div>

						<div class="row varflete">
							<div class="col-1"></div>
							<div class="col-2" >Gastos En Puerto Destino</div>
							<div class="col-2"></div>
							<div class="col-2">
								<label id="VrGastosPtoDest" data-flete="vr_gastos_destino">0</label>
							</div>
						</div>
						<div class="row varflete">
							<div class="col-1"></div>
							<div class="col-2" >Despacho Aduanal Destino</div>
							<div class="col-2"></div>
							<div class="col-2">
								<label id="VrDespAduanalDest" data-flete="vr_aduana_destino">0</label>
							</div>
						</div>
						<div class="row varflete">
							<div class="col-1"></div>
							<div class="col-2" >Transporte Aduanal Destino</div>
							<div class="col-2"></div>
							<div class="col-2">
								<label id="vrTranspAduaDest" data-i18n="[html]FUP_flete_vrTranspAduaDest">0</label>
							</div>
						</div>
						<div class="row varflete">
							<div class="col-1"></div>
							<div class="col-2" >
								<label id="LTipo3" data-i18n="[html]FUP_flete_ltipo3"></label>
							</div>
							<div class="col-2"></div>
							<div class="col-2">
								<label id="vrTipo3" data-flete="vr_destino_t1">0</label>
							</div>
						</div>
						<div class="row varflete">
							<div class="col-1"></div>
							<div class="col-2" >
								<label id="LTipo4" data-i18n="[html]FUP_flete_ltipo4"></label>
							</div>
							<div class="col-2"></div>
							<div class="col-2">
								<label id="VrTipo4" data-flete="vr_destino_t2">0</label>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="card" id="ParteVentaCierreCotizacion">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseVentaCierreCotizacion" data-i18n="[html]FUP_venta_cierre_cotizacion">Venta - Cierre - Comercial</a>
				</div>
				<div id="collapseVentaCierreCotizacion" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-12">
								<label data-i18n="[html]FUP_venta_cierre_obser">Observaciones: </label>
								<textarea id="VentaCierreObservacion" class="form-control col-sm-12 Observacion" rows="5"></textarea>
							</div>
						</div>
						<div class="row">
							<div class="col-12">
								<label data-i18n="[html]FUP_venta_cierre_obserM2">Observaciones de Variaciones M2: </label>
								<textarea id="VentaCierreObservacionM2" class="form-control col-sm-12 Observacion" rows="5"></textarea>
							</div>
						</div>
						<div class="row justify-content-end">
							<div class="" style="margin-top: 15px; margin-right: 15px;">
								<button type="button" class="btn btn-primary fupcot fupprc controlarDisponibilidadAprobacion " data-toggle="modal" onclick="GuardarVentaCierreComercial()">
									<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
								</button>
							</div>
						</div>
						<div class="row">
							<div class="justify-content-start" style="margin-top: 15px; margin-left: 15px;">
								<button type="button" class="btn btn-default fupcot fupprc controlarDisponibilidadAprobacion " data-toggle="modal" onclick="UploadFielModalShow('Cargar Carta de Cierre',9,'PreCierre')">
									Subir Carta de Cierre
								</button>
							</div>
						</div>
						<div class="row">
							<div class="justify-content-start" style="margin-top: 15px; margin-left: 15px;">
								<button type="button" class="btn btn-default fupcot fupprc controlarDisponibilidadAprobacion " data-toggle="modal" onclick="UploadFielModalShow('Cargar Planos Aprobados',7,'PreCierre')">
									Subir Planos Aprofbados  
								</button>
							</div>
						</div>
						<div class="row justify-content-center">
							<div class="" style="margin-top: 15px; margin-left: 15px;">
								<button type="button" class="btn btn-success fupprc " data-toggle="modal" onclick="ActualizarEstado(43)">
										<i class="fa fa-envelope"></i>Revisar y Actualizar FUP de Cierre 
								</button>
							</div>
						</div>
					</div>
				</div>
			</div>

			<div class="card" id="ParteFechaSolicitud">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseSolicitudFac" data-i18n="[html]sc_solicita_facturacion">Cierre Comercial - Aprobaciones - Solicitud Facturación</a>
				</div>
				<div id="collapseSolicitudFac" class="collapse" data-parent="#accordion">
					<div class="card-body">
	<%--<%                  ------------------------------------------------------- %>--%>
					<%--Fechas Cierre--%>
					<div class="col-md-12 " style="padding-top: 15px;" id="FechasCierre">
						<div id="FechasCierre1" class="box box-primary">
							<div class="box-header border-bottom border-primary" style="z-index: 3;">
								<table class="col-md-12 table-sm">
									<tbody>
										<tr>
											<td width="97%">
												<h5 class="box-title" style="">Fechas Cierre Comercial</h5>
											</td>
											<td width="3%">
												<div class="col-md-12" style="padding-bottom: 4px;">
													<button id="collapseFechasCierre" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 5px;">
														<span class="fa fa-angle-double-down"></span>
													</button>
												</div>
											</td>
										</tr>
									</tbody>
								</table>
							</div>
							<div id="bodyFechasCierre" class="box-body" style="display: none; padding-top: 20px; margin-left: 15px; margin-right: 15px;">
								<div class="row">
									<div class="col-12 medium font-weight-bold">
										<table id="tb_Fechas" class="table table-sm">
											<thead>
												<tr class="thead-light">
													<th class="text-center" colspan ="6">FECHAS</th>
												</tr>
												<tr>
													<th width="15%" class="text-center">Concepto</th>
													<th width="15%" class="text-center">Valor</th>
													<th width="15%" class="text-center">Concepto</th>
													<th width="15%" class="text-center">Valor</th>
													<th width="15%" class="text-center">Concepto</th>
													<th width="15%" class="text-center">Valor</th>
												</tr>
											</thead>
											<tbody id="tbodyFechas">
												<tr>
													<th data-i18n="[html]sf_firmacontrato">Fecha Firma de Contrato *</th>
													<th class="text-center" ><input id="dtfirmaContrato" type="date" /></th>
													<th data-i18n="[html]sf_FormalizaPago">Fecha Formalización de Pago *</th>
													<th class="text-center" ><input id="dtFormalizaPago"  type="date" /></th>
													<th >Aprobación Planos *</th>
													<th class="text-center" ><input id="dtfechaAprobacionPlanos"  type="date" /></th>
												</tr>
												<tr>
													<th class="text-center " colspan="6">
                                                        <textarea id="nota2" class="form-control" rows="1" disabled="disabled">Señor Comercial,  Recuerde que estas fechas serán validadas en los procesos de avales (jurídico- Tesoreria-ingeniería) </textarea>
                                                    </th>
												</tr>
												<tr>
													<th colspan="2"></th>
													<th >Plazo EXW</th>
													<th class="text-center" ><input type="number" id="dtPlazo" disabled="disabled" style="width: 70% !important;"/></th>
													<th >Fecha Despacho EXW</th>
													<th class="text-center" ><input id="dtfechacontractual" type="date" disabled="disabled" /></th>
												</tr>
												<tr>
													<th colspan="2"></th>
													<th >Plazo EXW Negociado</th>
													<th class="text-center" ><input type="number" class="fecNegLorena" id="dtPlazoNeg" style="width: 70% !important;" disabled="disabled"/></th>
													<th >Fecha Despacho EXW Neg.</th>
													<th class="text-center" ><input id="dtfechaPactadaPlan" type="date" disabled="disabled"/></th>
												</tr>
												<tr>
													<th >Term. Negociacion (TDN)</th>
													<th class="text-center" ><select id="selectTerminoNegociacion3" style="width: 70% !important;"></select></th>
													<th >Plazo Contractual (TDN)</th>
													<th class="text-center" ><input type="number"  id="dtPlazotdn" disabled="disabled" style="width: 70% !important;" /></th>
													<th >Fecha Contractual (TDN)</th>
													<th class="text-center" ><input id="dtfechacontractualtdn" type="date"  disabled="disabled" /></th>
												</tr>
												<tr>
													<th></th>
													<th class="text-center" ></th>
													<th data-i18n="[html]sf_Diasistecnica">Dias Asist. Técnica</th>
													<th class="text-center" ><input type="number" id="NumberDiasistecnica" disabled="disabled" style="width: 70% !important;" /></th>
													<th data-i18n="[html]sf_Diasconsumidos">Dias Consumidos</th>
													<th class="text-center" ><input type="number" id="NumberDiasconsumidos" disabled="disabled" style="width: 70% !important;"  />
												</tr>
												<tr>
													<th >Nota</th>
													<th class="text-center " colspan="5">
                                                        <textarea id="nota" class="form-control" rows="2" disabled="disabled">Estimado Comercial, recuerde que el tiempo según el Término de Negociación está sujeto a Cambios sin previo aviso, ya que se trata de situaciones ajenas a FORSA. Cualquier novedad que afecte el compromiso de entrega, será informada por email desde el área de Logistica.</textarea>
                                                    </th>
												</tr>
												<tr>
													<th data-i18n="[html]sf_ObservaFechas">Observación</th>												
													<th class="text-center " colspan="5">
                                                        <textarea id="ObservaFechas" class="form-control" rows="2"></textarea>
                                                    </th>
												</tr>

											</tbody>
										</table>
									</div>
                                </div>
								<div class="row">
									<div class="col-12 medium font-weight-bold">
										<table id="tbDescuentos" class="table table-sm">
											<thead>
												<tr class="thead-light">
													<th class="text-center" colspan ="9">VALORES DE CIERRE</th>
												</tr>
												<tr class="thead-light">
													<th class="text-center"></th>
													<th class="text-center" colspan ="2">Valores Cotizados</th>
													<th class="text-center" colspan ="2">Cierre Comercial</th>
													<th class="text-center" colspan ="2">Modulados Finales</th>
													<th class="text-center" colspan ="2">Cierre Final (Valores Facturacion)</th>
                                                    <th></th>
												</tr>												<tr>
													<th width="10%" class="text-center">Concepto</th>
													<th width="8%" class="text-center">M2</th>
													<th width="12%" class="text-center">Valor</th>
													<th width="8%" class="text-center">% Descuento</th>
													<th width="12%" class="text-center">Cierre Comercial</th>
													<th width="8%" class="text-center">M2</th>
													<th width="12%" class="text-center">Valor</th>
													<th width="8%" class="text-center">% Descuento</th>
													<th width="12%" class="text-center">Cierre Final</th>
												</tr>
											</thead>
											<tbody id="tbodyDescuentos">
												<tr>
													<th >Vlr Nivel 1</th>
													<th class="text-center" ><input type="number" step="0.01" class="NumeroSalcot" disabled="disabled" id="VlrNiv1m2"/></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " disabled="disabled" id="VlrNoDcto1Cuenta" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto1" value="0" onblur="calculardescto(1, 1, 1) "/></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " id="VlrDcto1Cuenta" onblur="calculardescto(2, 1, 1)" /></th>
													<th class="text-center" ><input type="number" step="0.01" class="NumeroSalcot" disabled="disabled" id="VlrNiv1m2B"/></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " disabled="disabled" id="VlrNoDcto1CuentaB" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto1B" value="0" onblur="calculardescto(1, 2, 1)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " id="VlrDcto1CuentaB" onblur="calculardescto(2, 2, 1)" /></th>
												</tr>
												<tr>
													<th >Vlr Nivel 2</th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" disabled="disabled" id="VlrNoDcto2Cuenta" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto2" value="0" onblur="calculardescto(1, 1, 2)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto2Cuenta" onblur="calculardescto(2, 1, 2)" /></th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " disabled="disabled" id="VlrNoDcto2CuentaB" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto2B" value="0" onblur="calculardescto(1, 2, 2)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto2CuentaB" onblur="calculardescto(2, 2, 2)" /></th>
												</tr>
												<tr>
													<th >Vlr Nivel 3</th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot sumDescVlrNDTto" disabled="disabled" id="VlrNoDcto3Cuenta" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto3" value="0" onblur="calculardescto(1, 1, 3)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto3Cuenta" onblur="calculardescto(2, 1, 3)" /></th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" disabled="disabled" id="VlrNoDcto3CuentaB" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto3B" value="0" onblur="calculardescto(1, 2, 3)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto3CuentaB" onblur="calculardescto(2, 2, 3)" /></th>
												</tr>
												<tr>
													<th >Vlr Nivel 4</th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot sumDescVlrNDTto" disabled="disabled" id="VlrNoDcto4Cuenta" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto4" value="0" onblur="calculardescto(1, 1, 4)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto4Cuenta" onblur="calculardescto(2, 1, 4)" /></th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" disabled="disabled" id="VlrNoDcto4CuentaB" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto4B" value="0" onblur="calculardescto(1, 2, 4)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto4CuentaB" onblur="calculardescto(2, 2, 4)" /></th>
												</tr>
												<tr>
													<th >Vlr Nivel 5</th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" disabled="disabled" id="VlrNoDcto5Cuenta" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto5" value="0" onblur="calculardescto(1, 1, 5)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto5Cuenta" onblur="calculardescto(2, 1, 5)" /></th>
													<th class="text-center" ></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" disabled="disabled" id="VlrNoDcto5CuentaB" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto5B" value="0" onblur="calculardescto(1, 2, 5)" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="VlrDcto5CuentaB" onblur="calculardescto(2, 2, 5)" /></th>
												</tr>
												<tr>
													<th >Total Después Descuentos</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " disabled="disabled" id="VlrTotalm2s1" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " disabled="disabled" id="VlrTotalAntesDescto" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot CondTotalDescto" disabled="disabled" id="VlrTotalDescto" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" disabled="disabled" id="VlrTotalDespuesDescto" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " disabled="disabled" id="VlrTotalm2s2" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot " disabled="disabled" id="VlrTotalAntesDesctoB" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot CondTotalDescto" disabled="disabled" id="VlrTotalDesctoB" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" disabled="disabled" id="VlrTotalDespuesDesctoB" /></th>
												</tr>
												<tr class ="CondicionalTotalDescuento">
													<th>Justificación del Descuento*</th>
													<th class="text-center"  colspan ="8"><input type= "text" id="ObservaMayorDcto" /></th>
												</tr>
												<tr>
													<th >Vlr Total de Cierre Comercial</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="Numberm2Cerrados" disabled="disabled" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="NumberTotalCierre" disabled="disabled" /></th>
													<th class="text-right">Vlr M2 - N1</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="Numberm2N1A" disabled="disabled" /></th>
													<th class="text-right">Vlr M2 - N1+N2+N3</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="Numberm2N13A" disabled="disabled" /></th>
													<th class="text-center" colspan ="2"></th>
												</tr>
												<tr>
													<th >Vlr Total de Cierre Final</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="Numberm2CerradosB" disabled="disabled" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="NumberTotalCierreB" disabled="disabled" /></th>
													<th class="text-right">Vlr M2 - N1 Final</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="Numberm2N1B" disabled="disabled" /></th>
													<th class="text-right">Vlr M2 - N1+N2+N3 Final</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" style="font-weight: bold;" id="Numberm2N13B" disabled="disabled" /></th>
													<th class="text-center" colspan ="2">
                                                        <%--Replicar Valores de Cierre Comercial--%>
                                                        <button type="button" id="btnReplicarValores" class="btn btn-primary fupReplicarValCierre" onclick="ReplicarValoresdeCierreComercial()">Replicar Valores de Cierre Comercial</button>
													</th>
												</tr>
												<tr BGCOLOR ="#B0E0E6" valign ="middle">
													<th data-i18n="[html]sf_TotalFacturacion">Vlr Total Facturación</th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="Numberm2Facturados" /></th>
													<th class="text-center" ><input type="number" class="NumeroSalcot" id="NumberTotalFacturacion"  disabled="disabled" /></th>
													<th class="text-center" colspan ="4" valign ="middle"><span>Indique si el proyecto se facturará sobre los m2 modulados finales, generados por ajustes/modificaciones acordadas con el cliente en el proceso de Definición técnica:</span>
													</th>
                                                    <th class="text-center" colspan ="2">
                                                        <div class="form-group onoffswitch" >
                                                        <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="SiNoCierreM2Fac" >
                                                        <label class="onoffswitch-label" for="SiNoCierreM2Fac">
                                                        <span class="onoffswitch-inner"></span>
                                                        <span class="onoffswitch-switch"></span>
                                                        </label>
                                                        </div>
                                                    </th>
													<th class="text-center" colspan ="1"></th>
												</tr>
												<tr>
													<th>Observación Descuentos Cierre</th>												
													<th class="text-center " colspan="8">
                                                        <textarea id="ObservaDesc" class="form-control" rows="2"></textarea>
                                                    </th>
												</tr>
											</tbody>
										</table>
									</div>
								</div>
								<div class ="row align-items-center ">
									<button type="button" class="btn btn-primary fupave fupsf fupofa f-noTesoJur" onclick="GuardarFechaSolicitudV2(1)">
										<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" >.  guardar</span>
									</button>

								</div>
							</div>
						</div>
					</div>
					<%--Condiciones de Pago--%>
					<div class="col-md-12 " style="padding-top: 15px;" id="CondicionesPago">
						<div id="CondicionesPago1" class="box box-primary">
							<div class="box-header border-bottom border-primary" style="z-index: 3;">
								<table class="col-md-12 table-sm">
									<tbody>
										<tr>
											<td width="97%">
												<h5 class="box-title" style="">Condiciones de Pago</h5>
											</td>
											<td width="3%">
												<div class="col-md-12" style="padding-bottom: 4px;">
													<button id="collapseCondicionesPago" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 5px;">
														<span class="fa fa-angle-double-down"></span>
													</button>
												</div>
											</td>
										</tr>
									</tbody>
								</table>
							</div>
							<div id="bodyCondicionesPago" class="box-body" style="display: none; padding-top: 20px; margin-left: 15px; margin-right: 15px;">
							<div class="row">
								<div class="col-4 font-weight-bold">
									<table id="tbCondicionesPagoCuotas" class="table">
										<thead>
											<tr class="thead-light">
												<th class="text-center" colspan ="4">CONDICIONES PAGO CUOTAS</th>
											</tr>
											<tr>
												<th width="40%" class="text-center">Condiciones de Pago</th>
												<th width="50%" class="text-center">
													<select id="cboCondicionesPago" class="select-filter">
													</select>
												</th>
											</tr>
										</thead>
										<tbody id="tbodyCondicionesPagoCuotas">
										</tbody>
									</table>
									</div>
									<%---- Cuotas de credito--%>
                                                                    <div class="col-8 font-weight-bold divarCuotas" >
									<table id="tbCondicionesCuotasPago" class="table">
										<thead>
											<tr class="thead-light">
												<th class="text-center" colspan ="5" id="titleTableCondicionesCuotas">CUOTAS</th>
											</tr>
											<tr>
												<th width="5%" class="text-center">Consecutivo</th>
												<th width="10%" class="text-center">Fecha</th>
												<th width="40%" class="text-center">Valor</th>
												<th width="45%" class="text-center">Comentarios</th>
												<th width="5%" class="text-center">
													<button id="btnAgregarCuota" class="btn btn-sm btn-link align-left" data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i> </button>
												</th>
                                                <th width="5%" id="columnBoletosBancariosCuotas" class="text-center">Boletos Bancarios</th>
											</tr>
										</thead>
										<tbody id="tbodyCondicionesCuotas">
											<tr>
													<td >1</td>
													<td class="text-center" ><input class="dtFechaCuota" type="date" /></td>
													<td ><input type="number" min="0" class="txtValorCuota NumeroSalcot" /></td>
													<th class="text-center" ><input class="txLeasing" type="text" /></th>
													<td ></td>
											</tr>
										</tbody>
										<tfoot>
											<tr class="thead-light">
												<th class="text-center" colspan ="2">TOTAL CUOTAS</th>
												<th class="text-center" colspan ="3"><input id="txTotalCuotas" type="number" min="0" class="txtValor disabled NumeroSalcot" disabled="disabled" /></th>
											</tr>
											<tr class="thead-light">
												<th data-i18n="[html]sf_TotalCierre" colspan ="2">Vlr Total de Cierre</th>
												<th class="text-center" colspan ="3"><input type="number" class="NumeroSalcot" id="NumberTotalCierre2" disabled="disabled" /></th>
											</tr>
										</tfoot>
									</table>
								</div>
                                								<div class="col-8 medium font-weight-bold divarLeasing"  >
									<table id="tbCondicionesLeasing" class="table">
										<thead>
											<tr class="thead-light">
												<th class="text-center" colspan ="5" id="titulocondiciones">CONDICIONES LEASING</th>
											</tr>
											<tr id="encabezadosTablaLeasing">
												<th width="5%" class="text-center">Consecutivo</th>
												<th width="10%" class="text-center">Fecha</th>
												<th width="40%" class="text-center">Valor</th>
												<th width="45%" class="text-center">Comentarios</th>
												<th width="5%" class="text-center">
													<button id="btnAgregarleasing" class="btn btn-sm btn-link align-left" data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i> </button>
												</th>
                                                <th width="5%" id="columnBoletosBancarios" class="text-center">Boletos Bancarios</th>
											</tr>
										</thead>
										<tbody id="tbodyCondicionesLeasing">
											<tr>
													<th >1</th>
													<th class="text-center"><input class="txLeasing" type="text" /></th>
													<th ><input type="number" min="0" class="txtValorLeasing NumeroSalcot" /></th>
													<th ></th>
											</tr>
										</tbody>
										<tfoot>
											<tr class="thead-light">
												<th class="text-center" colspan ="2" id="titulototalcondiciones">TOTAL LEASING</th>
												<th class="text-center" colspan ="2"><input id="txTotalLeasing"  type="number" min="0" class="txtValor disabled NumeroSalcot" disabled="disabled" /></th>
											</tr>
											<tr class="thead-light">
												<th data-i18n="[html]sf_TotalCierre" colspan ="2">Vlr Total de Cierre</th>
												<th class="text-center" colspan ="2"><input type="number" class="NumeroSalcot" id="NumberTotalCierre3" disabled="disabled" /></th>
											</tr>
										</tfoot>
									</table>
								</div>

							</div>
                              <div class="row">
                                        <button type="button" class="btn btn-info ml-auto mr-3" id="btnEnviarSoliBoletosBancarios" onclick="EnviarSolicitudesBoletosBancarios()" style="display:none;">Enviar Solicitud Boletos Bancarios</button>
                                    </div>
							<div class ="row align-content-center">
								<button type="button" class="btn btn-primary fupave fupsf fupofa f-noTesoJur" onclick="GuardarCondicionPago()">
									<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
								</button>
							</div>
                            <div>

					            <div class="row">
						            <div class="col-12">
							            <table class="table table-sm table-hover" id="tab_detalleCondicionesPago">
								            <thead class="thead-light">
									            <tr>
												<th width="5%" class="text-center">Consecutivo</th>
												<th width="10%" class="text-center">Fecha</th>
												<th width="40%" class="text-center">Valor</th>
												<th width="45%" class="text-center">Comentarios</th>
												<th width="5%" class="text-center">
									            </tr>
								            </thead>
								            <tbody id="tbodydetalleCondicionesPago">
								            </tbody>
							            </table>
						            </div>
					            </div>

                            </div>
						</div>
						</div>
					</div>
					<%--Documentos de Cierre--%>
					<div class="col-md-12 " style="padding-top: 15px;" id="DocumentacionFacturacion">
						<div id="DocumentacionFacturacion1" class="box box-primary">
							<div class="box-header border-bottom border-primary" style="z-index: 3;">
								<table class="col-md-12 table-sm">
									<tbody>
										<tr>
											<td width="97%">
												<h5 class="box-title" style="">Documentacion de Cierre</h5>
											</td>
											<td width="3%">
												<div class="col-md-12" style="padding-bottom: 4px;">
													<button id="collapseDocumentacionFact" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 5px;">
														<span class="fa fa-angle-double-down"></span>
													</button>
												</div>
											</td>
										</tr>
									</tbody>
								</table>
							</div>
							<div id="bodyDocumentacionFact" class="box-body" style="display: none; padding-top: 20px; margin-left: 15px; margin-right: 15px;">
									<div class="row">
										<div class="col-12 medium font-weight-bold">
											<table id="tbDocumentosFacturacion" class="table">
												<thead>
													<tr>
														<th width="50%" class="text-center" colspan ="2">
															<%--Botón Subir Carta Cotización   fupcfm --%>
															<button type="button" class="btn btn-default fupcfm " data-toggle="modal" onclick="UploadFielModalShow('Subir Carta Final Modulado',10,'Fechas - Solicitud Facturacion')">
																Subir Carta Final Modulada 
															</button>
														</th>
														<th width="40%" class="text-center"></th>
													</tr>
												</thead>
												<tbody id="tbodyDocumentosFacturacion">
												</tbody>
											</table>
											<%--Detalle Documentos de Cierre--%>
					                        <div class="row">
						                        <div class="col-12">
							                        <table class="table table-sm table-hover" id="tab_DetalleDocumentosCierre">
								                        <thead class="thead-light">
									                        <tr>
										                        <th class="text-center" >Tipo Documento</th>
										                        <th class="text-center" > Documento </th>
										                        <th class="text-center" > Acciones </th>
										                        <th class="text-center" > </th>
									                        </tr>
								                        </thead>
								                        <tbody id="tbodydetalleDocumentosCierre">
								                        </tbody>
							                        </table>
						                        </div>
					                        </div>

										</div>
									<div class="col-12 medium font-weight-bold">
										<table id="tbPedidosCliente" class="table table-sm">
                                            <thead>
                                                    <tr>
                                                        <th ><button id="btnAgregarCliente" class="btn btn-sm btn-link align-left" data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i> </button> Adicionar Orden Compra / Pedido Cliente </th>
                                                        <th ><button id="btnGuardarCliente" class="btn btn-sm btn-link align-left" data-toggle="tooltip" data-i18n="[title]FUP_guardar" onclick="GuardarOrdenCliente()"><i class="fa fa-save" style="font-size:14px;"></i> </button> Guardar Orden Compra </th>
                                                        <th class="text-center">Comentario</th>
                                                        <th ></th>
                                                    </tr>
                                            </thead>
											<tbody id="tbodyPedidoCliente">
												<tr>
													<td width ="25%">Orden Compra / Pedido Cliente #1</td>
													<td width ="25%"><input type="text" min="0" class="txtOrdenCliente" /></td>
													<td width ="40%"><input type="text" class="ComentarioPedCli" /></td>
													<td width ="10%"><button  type="button" class="fa fa-upload" data-toggle="tooltip" title="Cargar" onclick="UploadFielModalShow('Agregar Documento',3,'Fechas - Solicitud Facturacion - Documentos Cliente')"> </button>
													</td>
												</tr>
											</tbody>
										</table>
									</div>

						            <div class="row">
							            <div class="col-12">
								            <table class="table table-sm table-hover" id="tblAnexoCierre">
									            <thead class="thead-light">
										            <tr>
											            <th class="text-center" colspan=10>Anexos Cierre</th>
										            </tr>
										            <tr>
											            <th class="text-center" width="25%" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											            <th class="text-center" width="55%" data-i18n="[html]FUP_anexo">Anexo</th>
											            <th class="text-center" width="10%">Fecha</th>
											            <th class="text-center" width="10%"> </th>
										            </tr>
									            </thead>
									            <tbody id="tbodyAnexoCierre">
									            </tbody>
								            </table>
							            </div>
						            </div>

								</div>
							</div>

						</div>

					</div>
					<div class="row">
						<div class="col-12 medium font-weight-bold">
							<table id="tab_Botonoes" class="table">
								<thead>
								</thead>
								<tbody id="tbodyBotonoes">
									<tr class="row justify-content-center">
										<th width="30%" class="text-center" colspan ="2"></th>
										<th width="20%" class="text-center" colspan ="2">
											<button type="button" class="btn btn-success fupsf fupofa cumpleCondPago" data-toggle="modal" onclick="LlamarSF()">
												Ir a solicitud de Facturación  
											</button>
										</th>
										<th width="30%" class="text-center" colspan ="2"></th>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
					<%--Aprobaciones--%>
					<div class="col-md-12 " style="padding-top: 6px;" id="headerAprobacionesCierre">
						<div id="AprobacionesCierre1" class="box box-primary">
							<div class="box-header border-bottom border-primary" style="z-index: 2;">
								<table class="col-md-12 table-sm">
									<tbody>
										<tr>
											<td width="97%">
												<h5 class="box-title " style="">Avales para inicio de Fabricación y Despacho</h5>
											</td>
											<td width="3%">
												<div class="col-md-12" style="padding-bottom: 4px;">
													<button id="collapseAprobacionesCierre" onclick="Collapse(this)" type="button" class="btn btn-default btn-sm full-width " style="margin-top: 5px;">
														<span class="fa fa-angle-double-down"></span>
													</button>
												</div>
											</td>
										</tr>
									</tbody>
								</table>
							</div>
							<div id="bodyAprobacionesCierre" class="box-body" style="display: none; padding-top: 20px; margin-left: 15px; margin-right: 15px;">
	                                <div class="row col-6 align-content-center">
										<button type="button" class="btn btn-success fupsf fupofa cumpleCondPago" data-toggle="modal" onclick="SolicitarAvalesSF()">
                                            <i class="fa fa-envelope-o" aria-hidden="true"></i>  Solicitar Avales  
										</button>
					                </div>
									<div class="row">
										<div class="col-12 medium font-weight-bold">
											<table id="tab_DocsApruebaXF" class="table borderless table-sm">
												<thead>
													<tr class="thead-light">
														<th class="text-center" colspan ="5">DOCUMENTOS APROBACIÓN</th>
													</tr>
													<tr>
														<th width="20%" class="text-center">Concepto</th>
														<th width="20%" class="text-center">Aplica</th>
                                                        <th width="15%" class="text-center">Fechas</th>
														<th width="45%" class="text-center">Observación</th>
													</tr>
												</thead>
												<tbody id="tbodyDocAprueba">
													<tr class="avalJuridico">
														<td id="txtSiNoJuridico">Jurídico*</td>
                                                        <td colspan="3">
                                                            <table class="col-12 table-sm">
                                                                <thead>
                                                                    <tr>
                                                                        <th colspan="1" id="txtSiNoJuridicoA">Fabricación</th>
                                                                        <th colspan="1">
															                <select id="SiNoJuridico" onblur="reactioncombosAvalesdefault()">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
                                                                            <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Confirmar que el proyecto cuenta con los documentos legales revisados y aprobados."><i class="fa fa-info-circle fa-lg"></i></button>
                                                                        </th>
                                                                        <th colspan="1" class="text-left"><input id="dtfirmaContratoAproba" type="date" /></th>
														                <th colspan="3" class="text-center"><input type= "text" id="ObservaJuridico" onblur=" $('#cmJuridicoA').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmJuridicoA" /></th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th colspan="1" id="txtSiNoJuridicoB">Despacho</th>
														                <th colspan="1" >
															                <select id="SiNoJuridicoB" onblur=" $('#cmJuridicoB').val(1) ">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
														                    <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Confirmar que el proyecto cuenta con los documentos legales revisados y aprobados."><i class="fa fa-info-circle fa-lg"></i></button>
														                </th>
														                <th colspan="4" class="text-center"><input type= "text" id="ObservaJuridicoB" onblur=" $('#cmJuridicoB').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmJuridicoB" /></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                </tbody>
                                                            </table>
                                                        </td>
													</tr>
													<tr class="avalTesoreria">
														<td id="txtSiNoTesoreria">Tesorería*</td>
														<td colspan="3">
                                                            <table class="col-12 table-sm" style="border-style: none; empty-cells: show; caption-side: inherit">
                                                                <thead>
                                                                    <tr>
                                                                        <th colspan="1" id="txtSiNoTesoreriaA">Fabricación</th>
                                                                        <th colspan="1">
															                <select id="SiNoTesoreria" onblur="reactioncombosAvalesdefault()">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
                                                                            <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Confirmar que las formas de pago en cierre comercial estén cumplidas."><i class="fa fa-info-circle fa-lg"></i></button>
                                                                        </th>
                                                                        <th colspan="1" class="text-left"><input id="dtFormalizaPagoAproba" type="date" /></th>
														                <th colspan="3" class="text-center"><input type= "text" id="ObservaTesoreria" onblur=" $('#cmTesoreriaA').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmTesoreriaA" /></th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th colspan="1" id="SiNoTesoreriaDes">Despacho</th>
														                <th colspan="1" >
															                <select id="SiNoTesoreriaB" onblur=" $('#cmTesoreriaB').val(1) ">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
														                    <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Confirmar que el proyecto cuenta con los documentos legales revisados y aprobados."><i class="fa fa-info-circle fa-lg"></i></button>
														                </th>
														                <th colspan="4" class="text-center"><input type= "text" id="ObservaTesoreriaB" onblur=" $('#cmTesoreriaB').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmTesoreriaB" /></th>
                                                                    </tr>
                                                                </thead>
                                                            </table>
														</td>
													</tr>
													<tr class="avalGercomercial">
														<td id="txtSiNoGerencia">Gerencia Comercial*</td>
														<td colspan="3">
                                                            <table class="col-12 table-sm" style="border-style: none; empty-cells: show; caption-side: inherit">
                                                                <thead>
                                                                    <tr>
                                                                        <th colspan="1" id="txtSiNoGerenciaA">Fabricación</th>
                                                                        <th colspan="1">
															                <select id="SiNoGerencia" onblur=" $('#cmGerenciaA').val(1) ">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
															                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Revisado por Gerencia Comercial."><i class="fa fa-info-circle fa-lg"></i></button>
                                                                        </th>
														                <th colspan="4" class="text-center"><input type= "text" id="ObservaGerencia" onblur=" $('#cmGerenciaA').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmGerenciaA" /></th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th colspan="1" id="txtSiNoGerenciaDes">Despacho</th>
														                <th colspan="1" >
															                <select id="SiNoGerenciaB" onblur=" $('#cmGerenciaB').val(1) ">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
														                    <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Confirmar que el proyecto cuenta con los documentos legales revisados y aprobados."><i class="fa fa-info-circle fa-lg"></i></button>
														                </th>
														                <th colspan="4" class="text-center"><input type= "text" id="ObservaGerenciaB" onblur=" $('#cmGerenciaB').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmGerenciaB" /></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                </tbody>
                                                            </table>
														</td>
													</tr>
													<tr class="avalVicecomercial">
														<td id="txtSiNoVice">Vice Comercial*</td>
														<td colspan="3">
                                                            <table class="col-12 table-sm" style="border-style: none; empty-cells: show; caption-side: inherit">
                                                                <thead>
                                                                    <tr>
                                                                        <th colspan="1" id="txtSiNoViceA">Fabricación</th>
                                                                        <th colspan="1">
															                <select id="SiNoVice" onblur=" $('#cmViceA').val(1) ">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
														                    <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Autorizado por Vicepresidencia Comercial."><i class="fa fa-info-circle fa-lg"></i></button>
                                                                        </th>
                                                                        <th colspan="1" class="text-center">_</th>
														                <th colspan="3" class="text-center"><input type= "text" id="ObservaVice" onblur=" $('#cmViceA').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmViceA" /></th>
                                                                    </tr>
                                                                    <tr>
                                                                        <th colspan="1" id="txtSiNoViceDes">Despacho</th>
														                <th colspan="1" >
															                <select id="SiNoViceB" onblur=" $('#cmViceB').val(1) ">
																                <option value="1">Seleccionar...</option>
																                <option value="2">Aprobado</option>
																                <option value="3">No Aprobado</option>
															                </select>					    
														                    <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Confirmar que el proyecto cuenta con los documentos legales revisados y aprobados."><i class="fa fa-info-circle fa-lg"></i></button>
														                </th>
														                <th colspan="4" class="text-center"><input type= "text" id="ObservaViceB" onblur=" $('#cmViceB').val(1) " /></th>
                                                                        <th colspan="1" class="text-center col-sm-1" style="display:none" ><input type= "text" id="cmViceB" /></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                </tbody>
                                                            </table>
														</td>
													</tr>
												</tbody>
											</table>
										</div>
									</div>
									<div class ="row align-content-center">
										<button type="button" class="btn btn-primary fupAval" onclick="GuardarAvalesFabricacion()">
											<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
										</button>
									</div>
                                	<div class="row">
							            <div class="col-12">
								            <table class="table table-sm table-hover" id="tab_Avales">
									            <thead class="thead-light">
										            <tr>
											            <th class="text-center" colspan=10>Historial Avales</th>
										            </tr>
										            <tr>
											            <th width="15%" class="text-center" >Avalador</th>
											            <th width="20%" class="text-center" >Condicion Aprobacion</th>
											            <th width="20%" class="text-center" >Fecha Registro</th>
											            <th width="30%" class="text-center" >Observaciones</th>
											            <th width="15%" class="text-center" >Usuario</th>
										            </tr>
                                                </thead>
									            <tbody id="tbodytab_Avales">
									            </tbody>
								            </table>
							            </div>
						            </div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
<%--<%--<%                  ------------------------------------------------------- %>--%>

			<div class="card" id="ParteOrdenFabricacion">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseOrdenFabricacion" data-i18n="[html]FUP_orden_de_fabricacion">Orden de Fabricación</a>
				</div>
				<div id="collapseOrdenFabricacion" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2">
								<label> </label>
								<button type="button" id="ActualizarOrdenFabricacion" class="btn form-control col-sm-12" onclick="ObtenerOrdenFabricacion()">Actualizar </button>
										 
							</div>
																									
							<div class="col-2">
								<label data-i18n="[html]FUP_of_planta">Planta: </label>
								<select id="cmbPlantaOrdenes" class="form-control col-sm-12"></select>
							</div>
																								  
							<div class="col-2">
								<label data-i18n="[html]FUP_of_parte">Parte: </label>
								<select id="cmbParteOrdenes" onchange="CargarDatosParteOrden(this)" class="form-control col-sm-12">
									<option value="-1" style="display:none"></option>
								</select>
							</div>
							<div class="col-2">
								<label>M2: </label>
								<input type="text" id="M2Ordenes" class="form-control col-sm-12" disabled></input>
							</div>
							<div class="col-2">
								<label>$: </label>
								<input type="text" id="PesosOrdenes" class="form-control col-sm-12" disabled></input>
							</div>
							<div class="col-2">
								<button type="button" style="margin-top: 32px;" id="GuardarOrdenFabricacion" onclick="GuardarOrdenFab()" class="btn btn-primary form-control col-sm-12 fupofa1 ">
								<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
								</button>
							</div>
						</div>
						<div class="row" style="margin-top: 20px;">
							<table id="tbOrdenFabricacion" class="table">
								<thead>
									<tr>
										<th data-i18n="[html]FUP_of_Tipo">Tipo</th>
										<th class="mrv">Orden</th>
										<th class="mrv">Planta</th>
										<th class="mrv">M2 SF</th>
										<th class="mrv">Precio SF</th>
										<th class="mrv">Ver</th>
										<th class="mrv">Parte</th>
										<th class="mrv">Fecha</th>
										<th class="mrv">Responsable</th>
										<th class="mrv">M2 Ingeni</th>
										<th class="mrv">M2 Dif</th>
										<th class="mrv"> Ir Seguimiento </th>
										<th class="mrv"> Anular </th>
										<th></th>
									</tr>
								</thead>
								<tbody id="tbodyOrdenFabricacion"></tbody>
							</table>
						</div>
					</div>
				</div>
			</div>

			<div class="card" id="ParteModuladosCerrados">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseModCerrado" ">M2 Modulados Final y Consideraciones del Proyecto</a>
				</div>
				<div id="collapseModCerrado" class="collapse" data-parent="#accordion">
					<div class="card-body">
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
								<input type="number" id="txtM2EquipoBasemf" class="sumM2SalidaMF NumeroSalcot" data-modelomf="m2_equipo" />
							</div>
							<div class="col-2">
								<input type="number" id="txtValEquipoBasemf" class="sumValSalidaMF NumeroSalcot" data-modelomf="vlr_equipo" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_m2_adicionales">M2, Adicionales</div>
							<div class="col-2">
								<input type="number" id="txtM2Adicionalesmf" class="sumM2SalidaMF NumeroSalcot" data-modelomf="m2_adicionales" />
							</div>
							<div class="col-2">
								<input type="number" id="txtValAdicionalesmf" class="sumValSalidaMF NumeroSalcot" data-modelomf="vlr_adicionales" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_detalle_arq">M2, Detalles Arquitectonicos</div>
							<div class="col-2">
								<input type="number" id="txtDetArqM2SCmf" class="sumM2SalidaMF NumeroSalcot" data-modelomf="m2_Detalle_arquitectonico" />
							</div>
							<div class="col-2">
								<input type="number" id="txtDetArqValorSCmf" class="sumValSalidaMF NumeroSalcot" data-modelomf="vlr_Detalle_arquitectonico" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_total_encofrados">Total Encofrados</div>
							<div class="col-2">
								<input type="number" id="txtTotalM_MF" class="NumeroSalcot" disabled="disabled" />
							</div>
							<div class="col-2">
								<input type="number" id="txtTotalValorMF" class="NumeroSalcot" disabled="disabled" />
							</div>
						</div>
						<div class="row">
							<div class="col-2">
							</div>
							<div class="col-2" data-i18n="[html]sc_sis_seguridad">Perimetros de Sist. Seguridad</div>
							<div class="col-2">
								<input type="number" id="txtSistemaTrepanteAccmf" class="NumeroSalcot" data-modelomf="m2_sis_seguridad" />
							</div>
							<div class="col-2">
								<input type="number" id="txtAccSistemaSegmf" class="sumValSalidaMF2 NumeroSalcot" data-modelomf="vlr_sis_seguridad" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_accesorios_basicos">Accesorios Basicos</div>
							<div class="col-2">
							</div>
							<div class="col-2">
								<input type="number" id="txtAccBasicosmf" class="sumValSalidaMF2 NumeroSalcot" data-modelomf="vlr_accesorios_basico" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_accesorios_complementarios">Accesorios Complementarios</div>
							<div class="col-2">
																							
							</div>
							<div class="col-2">
								<input type="number" id="txtAccComplmf" class="sumValSalidaMF2 NumeroSalcot" data-modelomf="vlr_accesorios_complementario" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_accesorios_opcionales">Accesorios Opcionales</div>
							<div class="col-2">
							</div>
							<div class="col-2">
								<input type="number" id="txtAccOpcionalesmf" class="sumValSalidaMF2 NumeroSalcot" data-modelomf="vlr_accesorios_opcionales" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_accesorios_adicionales">Accesorios Adicionales</div>
							<div class="col-2">
							</div>
							<div class="col-2">
								<input type="number" id="txtAccAdicionalesmf" class="sumValSalidaMF2 NumeroSalcot" data-modelomf="vlr_accesorios_adicionales" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_otros_productos">Otros Productos</div>
							<div class="col-2">
							</div>
							<div class="col-2">
								<input type="number" id="txtOtrosProductomf" class="sumValSalidaMF2 NumeroSalcot" data-modelomf="vlr_otros_productos" />
							</div>
						</div>
						<div class="row">
							<div class="col-2"></div>
							<div class="col-2" data-i18n="[html]sc_total_propuestas">Total Propuesta Com.</div>
							<div class="col-2">
							</div>
							<div class="col-2">
								<input type="number" id="txtTotalPropuestaComMF" class="NumeroSalcot" disabled="disabled"/>
							</div>
						</div>

					<%--Botón Carta de Cotizacion M2 Modulados Final--%>
					    <div class="row">
    					    <div class="col-6">
							    <table class="table table-sm table-hover" id="tab_anexos_M2Final">
								    <thead>
                                        <tr> <th colspan ="3"> 
                                            <button type="button" class="btn btn-default  " data-toggle="modal" onclick="UploadFielModalShow('Subir Carta Cotizacion M2 Final',25,' M2 Modulados Final')">
								                Subir Carta Cotizacion M2 Final  
							                </button>
                                                </th>
                                        </tr>
									    <tr class="thead-light">
										    <th class="text-center" width="70%">Ver Carta Cotizacion M2 Final</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="8%"> </th>
									    </tr>
								    </thead>
								    <tbody id="tbodyanexos_M2Final">
								    </tbody>
							    </table>                                
                            </div>
                        </div>
						<hr />
						<div class="row">
							<div class="col-1" >Orden CI</div>
							<div class="col-2">
								<input id="txtCIOrdenCotizacion" type="text" disabled style="width: 80% !important;" />
							</div>
						   <div class="col-9">
								<button id="btnGenerarOrdenCI" type="button" class="btn btn-primary fupmodfin" value="Generar Orden Cotizacion" onclick="guardarOrdenCI();">
								<i class="fa fa-cogs pr-3"></i> <span > Generar Orden Mod. final</span>
								</button> 
						   
								<button id="btnExplosionarOrdenCI" type="button" class="btn btn-primary fupmodfin " value="Explosionar Orden Cotizacion" onclick="explosionarOrdenCI();">
								<i class="fa fa-cogs pr-3"></i> <span>  Explosionar Mod. Final</span>
								</button> 

								<button id="btnReporteCI" type="button" class="btn btn-primary fupmodfin" value="Listado Orden Cotizacion" onclick="LlamarReporteListadoCT(3);">
								<i class="fa fa-print pr-3"></i> <span>  Listado Orden Mod Final</span>
								</button> 
								<button id="btnReporteCIkg" type="button" class="btn btn-success fupmodfin" value="Listado Orden Cotizacion" onclick="LlamarReporteListadoCT(4);">
								<i class="fa fa-print pr-3"></i> <span>  Listado Orden Mod Final KG</span>
								</button> 
                                    
                                <button id="btnMostrarResumenOrdenCI" type="button" class="btn btn-primary" onclick="$('#tabsResumenOrdenCIContainer').toggle();">
                                    <i class="fa fa-book pr-3"></i> <span>  Resumen Orden Mod Final</span>
                                </button>
							</div>
						</div>

                        <div id="tabsResumenOrdenCIContainer" style="display:none;">
                         <hr />
                            <div id="resumenOrdenCIContainer">
                                <ul class="nav nav-tabs" id="resumenOrdenCITabs" role="tablist">
                                    <li class="nav-item">
                                        <a class="nav-link active show" data-toggle="tab" href="#CIresumen" role="tab" aria-controls="resumen" aria-selected="true" style="font-size:initial !important">Resumen</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link show" data-toggle="tab" href="#CIaluminio" role="tab" aria-controls="aluminio" aria-selected="false" style="font-size:initial !important">Aluminio</a>
                                    </li>
                                    <li class="nav-item">
                                        <a class="nav-link" data-toggle="tab" href="#CIaccesorios" role="tab" aria-controls="accesorios" aria-selected="false" style="font-size:initial !important">Accesorios y seguridad</a>
                                    </li>
                                </ul>
                            </div>
                            <div class="tab-content" id="resumenOrdenCITabsContents">
                                <div class="tab-pane fade active show" id="CIresumen" role="tabpanel" aria-labelledby="resumen-tab">
                                    <div class="container-fluid mt-3">
                                        <table class="table table-striped table-sm text-center col-8 offset-0 table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="5">Resumen</th>
                                                </tr>
                                                <tr>
                                                    <th>Nivel</th>
                                                    <th>M2</th>
                                                    <th>Valor</th>
                                                    <th>Valor x M2</th>
                                                    <th>Kilogramos</th>
                                                    <th>Valor x Kilo</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIM21"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValor1"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxM21"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIKilogramos1"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxKilo1"></td>
                                                </tr>
                                                <tr>
                                                    <td>2</td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIM2nivelAccesorio2"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValornivelAccesorio2"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxM2nivelAccesorio2"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIKilogramosnivelAccesorio2"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxKilonivelAccesorio2"></td>
                                                </tr>
                                                <tr>
                                                    <td>3</td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIM2nivelAccesorio3"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValornivelAccesorio3"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxM2nivelAccesorio3"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIKilogramosnivelAccesorio3"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxKilonivelAccesorio3"></td>
                                                </tr>
                                                <tr>
                                                    <td>4</td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIM2nivelAccesorio4"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValornivelAccesorio4"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxM2nivelAccesorio4"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIKilogramosnivelAccesorio4"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxKilonivelAccesorio4"></td>
                                                </tr>
                                                <tr>
                                                    <td>5</td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIM2nivelAccesorio5"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValornivelAccesorio5"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxM2nivelAccesorio5"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIKilogramosnivelAccesorio5"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxKilonivelAccesorio5"></td>
                                                </tr>
                                                <tr>
                                                    <td>Total</td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIM2Total"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorTotal"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxM2Total"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIKilogramosTotal"></td>
                                                    <td class="camposResumenOrdenCI" id="tdResumenCIValorxKiloTotal"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="CIaluminio" role="tabpanel" aria-labelledby="aluminio-tab">
                                    <div class="container-fluid mt-3">
                                        <div class="row">
                                            <span class="col-1">Piezas Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCIPiezasAlum" disabled="disabled" /></span>
                                            <span class="col-1">M2 Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCIM2Alum" disabled="disabled" /></span>
                                            <span class="col-1">Piezas / M2 Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCIPiezasM2Alum" disabled="disabled" /></span>
                                            <div class="col-3"><div class="text-center font-weight-bold">Proyecto</div></div>
                                        </div>
                                        <div class="row">
                                            <span class="col-1">Costo COP / Kilo</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCICostoCOPKilo" disabled="disabled" /></span>
                                            <span class="col-1">Costo M2 Cop Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCICostoM2CopAlum" disabled="disabled" /></span>
                                            <span class="col-1">Costo Alum COP</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCICostoAlumCOP" disabled="disabled" /></span>
                                            <span class="col-1">Costo Total COP</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCICostoTotalCOP" disabled="disabled" /></span>
                                        </div>
                                        <div class="row">
                                            <span class="col-1">Kilos / M2 Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCIKilosM2Alum" disabled="disabled" /></span>
                                            <span class="col-1">Costo USD / Kilo</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCICostoUSDKilo" disabled="disabled" /></span>
                                            <span class="col-1">Costo M2 Usd Alum</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCICostoM2USDAlum" disabled="disabled" /></span>
                                            <span class="col-1">Costo USD</span>
                                            <span class="col-2"><input class="form-control camposResumenOrdenCIInputs" id="txtCICostoUSD" disabled="disabled" /></span>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <table class="table table-striped table-sm text-center col-8 offset-0 table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th colspan="5">Aluminio</th>
                                                    </tr>
                                                    <tr>
                                                        <th></th>
                                                        <th>01 - Aluminio</th>
                                                        <th>02 - Aluminio Kanbam</th>
                                                        <th>Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>Cant Piezas</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICantPiezas1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICantPiezas4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICantPiezasTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>M2</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIM21"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIM24"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIM2Total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pieza x M2</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPiezaxM21"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPiezaxM24"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPiezaxM2Total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Kilos</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilos1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilos4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilosTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Kilos x M2</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilosxM21"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilosxM24"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilosxM2Total"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Chatarra</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoChatarra1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoChatarra4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoChatarraTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Kilos Ch</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilosCh1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilosCh4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIKilosChTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>% Ch</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPcCh1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPcCh4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPcChTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Mp</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoMp1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoMp4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoMpTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>% Mp</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPcMp1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPcMp4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIPcMpTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Total Mp x Kilo</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoTotalMpxKilo1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoTotalMpxKilo4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoTotalMpxKiloTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Insertos</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIInsertos1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIInsertos4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIInsertosTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo Total Mp</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostototalMp1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostototalMp4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostototalMpTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>MOD</td>
                                                        <td class="camposResumenOrdenCI" id="tdCIMOD1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIMOD4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCIMODTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo MOD x Kilo</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoMODxKilo1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoMODxKilo4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoMODxKiloTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>CIF</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICIF1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICIF4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICIFTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo CIF x Kilo</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoCIFxKilo1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoCIFxKilo4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoCIFxKiloTotal"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Costo x Item</td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoxItem1"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoxItem4"></td>
                                                        <td class="camposResumenOrdenCI" id="tdCICostoxItemTotal"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane fade" id="CIaccesorios" role="tabpanel" aria-labelledby="accesorios-tab">
                                    <div class="container-fluid mt-3">
                                        <table class="table table-striped table-sm text-center col-8 offset-0 table-bordered">
                                            <thead>
                                                <tr>
                                                    <th colspan="6">Accesorios y Sistema Seguridad</th>
                                                </tr>
                                                <tr>
                                                    <th></th>
                                                    <th>02</th>
                                                    <th>03</th>
                                                    <th>04</th>
                                                    <th>05</th>
                                                    <th>Total</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Cant Piezas</td>
                                                    <td class="camposResumenOrden" id="tdCICantPiezasASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCICantPiezasASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCICantPiezasASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCICantPiezasASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCICantPiezasASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Peso x Item</td>
                                                    <td class="camposResumenOrden" id="tdCIPesoxItemASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCIPesoxItemASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCIPesoxItemASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCIPesoxItemASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCIPesoxItemASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Costo Prom x Pieza</td>
                                                    <td class="camposResumenOrden" id="tdCICostoPromxPiezaASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoPromxPiezaASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoPromxPiezaASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoPromxPiezaASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoPromxPiezaASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Costo x Item</td>
                                                    <td class="camposResumenOrden" id="tdCICostoxItemASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoxItemASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoxItemASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoxItemASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCICostoxItemASSTotal"></td>
                                                </tr>
                                                <tr>
                                                    <td>Total Costo x Item</td>
                                                    <td class="camposResumenOrden" id="tdCITotalCostoxItemASS2"></td>
                                                    <td class="camposResumenOrden" id="tdCITotalCostoxItemASS3"></td>
                                                    <td class="camposResumenOrden" id="tdCITotalCostoxItemASS4"></td>
                                                    <td class="camposResumenOrden" id="tdCITotalCostoxItemASS5"></td>
                                                    <td class="camposResumenOrden" id="tdCITotalCostoxItemASSTotal"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

   						<hr />

                        <div class="row">
                            <div class="col-12">
                                <h6 style="font-weight: bold;">OBSERVACIONES O CONSIDERACIONES DEL CLIENTE DURANTE EL PROCESO DE MODULACION Y/O FABRICACION</h6>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <span style="font-weight: bold; font-style:italic">Señor Proyectista, Indique si el proyecto tiene consideraciones técnicas de modulación o fabricación diferentes al estándar Forsa</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <textarea id="txtObservacionesConsideracionesCliente" rows="5" class="form-control" data-modelomf="ConsideracionObservacionCliente"></textarea>
                            </div>
                        </div>
                        <hr />
						<div class="row justify-content-center">
							<div class="" style="margin-top: 15px; margin-left: 15px;">
								<button type="button" class="btn btn-primary fupmodfin" onclick="guardar_salida_cot(2)">
									<i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
								</button>
							</div>
						</div>

                        <div class="row">
                             <div class="col-2">
                                    <button type="button" class="btn btn-default fupmodfin " data-toggle="modal" onclick="UploadFielModalShow('Subir Documentos de Armado',0,' M2 Modulados Final',0,1)">
								                Subir Documento Armado  
							        </button>
                            </div>
                             <div class="col-2">
                                    <button type="button" class="btn btn-success btn-sm fupmodfin" data-toggle="modal" onclick="CorreoNotificacion(69)">
								                Enviar al Cliente  
							        </button>
                            </div>
                         </div>
                         <div class="row">
    					    <div class="col-4">
							    <table class="table table-sm table-hover" id="tab_anexos_armado1">
								    <thead >
                                        <tr class="table-primary"> 
                                            <th colspan ="4" class="text-center">Juego de Alumino</th>
                                        </tr>
									    <tr class="thead-light">
										    <th class="text-center" colspan ="2" width="68%">Ver Documentos de Armado</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="10%"> </th>
									    </tr>
								    </thead>
								    <tbody id="tbodyanexos_armado1">
								    </tbody>
								    <tfoot>
									    <tr class="thead-light">
										        <th class="text-center" width="30%">Informacion Completa</th>
										        <th class="text-center" width="20%"><input type="checkbox" id="txtAlumIC1" /></th>
										        <th class="text-center" width="30%">No Aplica</th>
										        <th class="text-center" width="20%"><input type="checkbox" id="txtAlumNa1" /></th>
									    </tr>
								    </tfoot>
							    </table>
                            </div> 
   					        <div class="col-4">
							    <table class="table table-sm table-hover" id="tab_anexos_armado2">
								    <thead >
                                        <tr class="table-success"> <th colspan ="4" class="text-center"> 
                                            Juego de Escalera
                                            </th>
                                        </tr>
									    <tr class="thead-light">
										    <th class="text-center" colspan ="2" width="68%">Ver Documentos de Armado</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="10%"> </th>
									    </tr>
								    </thead>
								    <tbody id="tbodyanexos_armado2">
								    </tbody>
								    <tfoot>
									    <tr class="thead-light">
										        <th class="text-center" width="30%">Informacion Completa</th>
										        <th class="text-center" width="20%"><input type="checkbox" id="txtAlumIC2" /></th>
										        <th class="text-center" width="30%">No Aplica</th>
										        <th class="text-center" width="20%"><input type="checkbox" id="txtAlumNa2" /></th>
									    </tr>
								    </tfoot>
							    </table>
                            </div> 
   					        <div class="col-4">
							    <table class="table table-sm table-hover" id="tab_anexos_armado3">
								    <thead >
                                        <tr class="table-info"> <th colspan ="4" class="text-center"> 
                                            Accesorios
                                            </th>
                                        </tr>
									    <tr class="thead-light">
										    <th class="text-center" colspan ="2" width="68%">Ver Documentos de Armado</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="10%"> </th>
									    </tr>
								    </thead>
								    <tbody id="tbodyanexos_armado3">
								    </tbody>
								    <tfoot>
									    <tr class="thead-light">
										        <th class="text-center" width="30%">Informacion Completa</th>
										        <th class="text-center" width="20%"><input type="checkbox" id="txtAlumIC3" /></th>
										        <th class="text-center" width="30%">No Aplica</th>
										        <th class="text-center" width="20%"><input type="checkbox" id="txtAlumNa3" /></th>
									    </tr>
								    </tfoot>
							    </table>
                            </div>                         
                         </div>                                                   
						<div class="row justify-content-center">
							<div class="" style="margin-top: 15px; margin-left: 15px;">
								<button type="button" class="btn btn-primary  fupmodfin fupcfm" onclick="GrabarTablaArmado()">
									<i class="fa fa-save"></i> <span> guardar Datos Armado</span>
								</button>
							</div>
						</div>
					</div>
				</div>
			</div>
			<div class="card" id="ParteActaTec">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseActaTec" >Acta Mesas Técnicas y Validacion de Equipo</a>
				</div>
				<div id="collapseActaTec" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <div class="row">
    					    <div class="col-6">
							    <table class="table table-sm table-hover" id="tab_anexos_FichaTecOper">
								    <thead>
                                        <tr> <th colspan ="3"> 
                                            <button type="button" class="btn btn-default  " data-toggle="modal" onclick="UploadFielModalShow('Ficha Tecnica Operacion Equipo Obra',37,' Modulacion Final')">
								                Ficha Tecnica Operacion Equipo Obra  
							                </button>
                                                </th>
                                        </tr>
									    <tr class="thead-light">
										    <th class="text-center" width="70%">Ver Ficha Tecnica Operacion Equipo Obra</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="8%"> </th>
									    </tr>
								    </thead>
								    <tbody id="tbodyanexos_FichaTecOper">
								    </tbody>
							    </table>                                
                            </div>
                            <div class="col-6">
                            </div>
                        </div>
                        
                        <hr size="1">
                        <div class="row ">
    					        <div  class="col-4">
                                        <span>Requiere Mesa Técnica Previa al despacho con el Cliente:</span>
                                 </div>
                                <div class="col-2">
                                    <div class="form-group onoffswitch" >
                                    <input type="checkbox" onchange="sinoitemFichaTec()"  name="onoffswitch" class="onoffswitch-checkbox" id="SiNoFichaTec" disabled>
                                    <label class="onoffswitch-label" for="SiNoFichaTec">
                                    <span class="onoffswitch-inner"></span>
                                    <span class="onoffswitch-switch"></span>
                                    </label>
                                    </div>
                                </div>
                                <div class="col-6 Mesastec">
                                        <input class="form-check-input" type="checkbox" value="" id="ckNoPreviaDesp">
                                        <label class="form-check-label" for="flexCheckDefault">No Ejecutar Mesa Previa Despacho</label>
                        		    <textarea id="txtObsPreviaDesp" class="form-control" rows="2"></textarea>
								    <button type="button" class="btn btn-primary" onclick="GrabaNoEjecutarMesa(1)">
									    <i class="fa fa-save"></i> . Mesa Previa Despacho
								    </button>
                                </div>
                            </div>
                        <div class="row">
                            <div class="col-6">
                                <table class="table table-sm table-hover" id="tab_actaMesaTecnicaPreviaDespacho">
                                <thead>
                                    <tr>
                                        <th colspan="3">
                                            <button type="button" class="btn btn-default  " data-toggle="modal" onclick="UploadFielModalShow('Subir Acta Mesa Técnica Previa Despacho',36,'Acta Mesa Tecnica Previa Despacho')">
								                Subir Acta Mesa Técnica Previa Despacho
							                </button>
                                        </th>
                                        <tr class="thead-light">
										    <th class="text-center" width="70%">Acta Mesa Técnica Previa Despacho</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="8%"> </th>
									    </tr>
                                    </tr>
                                </thead>
                                <tbody id="tbodyanexos_ActaMesaTecnicaPrevia">
                                    <!-- Table content of uploaded files -->
                                </tbody>
                                </table>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
    					    <div class="col-6">
							    <table class="table table-sm table-hover" id="tab_anexos_ActaPostVenta">
								    <thead>
                                        <tr> <th colspan ="3"> 
                                            <button type="button" class="btn btn-default  " data-toggle="modal" onclick="UploadFielModalShow('Acta Mesa Tecnica Postventa',38,'Acta Mesa Tecnica Postventa')">
								                Subir Acta Mesa Técnica Postventa
							                </button>
                                                </th>
                                        </tr>
									    <tr class="thead-light">
										    <th class="text-center" width="70%">Acta Mesa Ténica Postventa</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="8%"> </th>
									    </tr>
								    </thead>
								    <tbody id="tbodyanexos__ActaPostVenta">
								    </tbody>
							    </table>                                
                            </div>
                                <div class="col-6 Mesastec">
                                        <input class="form-check-input" type="checkbox" value="" id="ckNoPostventa">
                                        <label class="form-check-label" for="flexCheckDefault">No Ejecutar Mesa Postventa</label>
                        		    <textarea id="txtObsPostventa" class="form-control" rows="2"></textarea>
								    <button type="button" class="btn btn-primary" onclick="GrabaNoEjecutarMesa(2)">
									    <i class="fa fa-save"></i> . Mesa Postventa
								    </button>
                                </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-6">
                                <table class="table table-sm table-hover" id="tab_ValidacioDeEquipo">
                                <thead>
                                    <tr>
                                        <th colspan="3">
                                            <button type="button" class="btn btn-default  " data-toggle="modal" onclick="UploadFielModalShow('Subir Validacion de Equipo',39,'Validacion de Equipo')">
								                Subir Validacion de Equipo
							                </button>
                                        </th>
                                        <tr class="thead-light">
										    <th class="text-center" width="70%">Validacion de Equipo</th>
										    <th class="text-center" width="22%">Fecha</th>
										    <th class="text-center" width="8%"> </th>
									    </tr>
                                    </tr>
                                </thead>
                                <tbody id="tbodyanexos_ValidacionDeEquipo">
                                    <!-- Table content of uploaded files -->
                                </tbody>
                                </table>
                            </div>
                        </div>
					</div>
				</div>
			</div>	 

			<div class="card" id="ParteControlCambio">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseControl" >Control de Cambios</a>
				</div>
				<div id="collapseControl" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
									<button type="button"  class="btn btn-success controlarDisponibilidadAprobacion" data-toggle="modal" onclick="ControlCambioShow('Control de Cambios',0,0,'',0)">
										<i class="fa fa-comment">  </i>
										<b data-i18n="[html]FUP_cntrcmb">Control de Cambios</b>
									</button>

							</div>                            
							<div class="col-2" style="display: inline-table">
									<button type="button"  class="btn btn-primary controlcambiosDFT_btn" data-toggle="modal" onclick="ControlCambioShow('Control de Cambios',0,0,'',1)">
										<i class="fa fa-map">  </i>
										<b >Control DFT</b>
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

