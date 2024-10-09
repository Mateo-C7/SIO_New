<%@ Page Title="" Language="C#" MasterPageFile="~/General2HamburgerSideBar.Master" AutoEventWireup="true" CodeBehind="FormPrototypeReport.aspx.cs" Inherits="SIO.FormPrototypeReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
	<script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
	<script type="text/javascript" src="Scripts/formPrototype.js?v=20220810A"></script>
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
	<script type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">

    <div id="loader" style="display: none">
		<h3>Procesando...</h3>
	</div>
	<div id="ohsnap"></div>

	<div class="container-fluid contenedor_fup">
        
		<div class="row">
			<div class="btn-group col align-self-end" style="flex-grow: 0 !important;" role="group" aria-label="Basic example">
				<button type="button" class="btn btn-secondary langes">
					<img alt="español" src="Imagenes/colombia.png" /></button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" /></button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" /></button>
			</div>
            
            <!-- Fragmento pendiente por acomodar en la Master Page -->
            <img class="menu-bar" src="Imagenes/HMIcon.png" style="width: 38px; height: 38px;"/>
            <script type="text/javascript">
                $(".menu-bar").on('click', function() {
                    $("#wrapper").toggleClass("openSidebarYt");
                    $(".sidebarYt").toggleClass("sidebarYtOpened");
                });
            </script>
            <!-- -->

        </div>

        <!-- Modal control de cambios -->
       <div class="modal fade" id="ModControlCambios" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title">Control de Cambios</h5>
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
								<div class="col-12">Tipo de Hallazgo</div>
								<div class="col-6">
                                    <select id="cmbSubProceso" class="form-control">
									</select>
								</div>
                            <%--<div class="col-6">
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

       <!-- Modal Consideraciones y Observaciones -->
       <div class="modal fade" id="ModConsiderationObservation" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-titleC_O">Consideraciones u Observaciones</h5>
							<button type="button" class="close" data-dismiss="modal" aria-label="Close">
								<span aria-hidden="true">&times;</span>
							</button>
						</div>
						<div class="modal-body">
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="text" class="form-control" id="AreaControlC_O" disabled />
								<input type="text" class="form-control" id="padreCambioC_O" disabled hidden/>
								<input type="text" class="form-control" id="EsDFTC_O" disabled hidden/>
                                <!-- Valores tipoEntrada=1:Observacion 2:Seguimiento 3:Hallazgo 4:HojaDeVida-->
                                <input type="text" class="form-control" id="tipoEntrada" disabled="disabled" hidden />
                            </div>
                            <div class="row">
								<div class="col-12">Titulo</div>
							</div>
                            <div class="row">
								<div class="col-12">
    								<input type="text" class="form-control" id="txtTituloObsC_O" />
								</div>
							</div>
                            <div class="row" id = "CamposConsiderationObservation">
								<div class="col-12">Tipo de registro</div>
								<div class="col-12">
                                    <select id="cmbSubProcesoC_O" class="form-control">
                                        <option value="1" selected="selected">Consideracion</option>
                                        <option value="2">Observacion</option>
									</select>
								</div>
							</div>
                            <!--<div class="row Rdft2C_O">
								<div class="col-6">Tipo de Control</div>
								<div class="col-6">Estado</div>
								<div class="col-6">
                                    <select id="cmbSubProceso2C_O" class="form-control">
									</select>
								</div>
								<div class="col-6">
                                    <select id="cmbEstadoDftC_O" class="form-control">
									</select>
								</div>
							</div>-->
                            <div class="row" id="camposSegLogistico">
								<div class="col-6">Fecha despacho</div>
								<div class="col-6">Fecha entrega en obra</div>
								<div class="col-6">
                                    <input type="date" id="txtFecDespachoSegLogistico"/>
								</div>
								<div class="col-6">
                                    <input type="date" id="txtFecEntregaSegLogistico"/>
								</div>
							</div>

                            <div id="camposHallazgos">
                                <div class="row">
                                    <div class="col-6">Área solicitada</div>
								    <div class="col-3">Solucionado en obra?</div>
                                    <div class="col-3">Generó un costo?</div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <select id="cmbAreaSolicitada" class="form-control">
                                            <option value="SCI">SCI</option>
                                            <option value="Produccion">Produccion</option>
									    </select>
                                    </div>
                                    <div class="col-3">
                                        <select id="cmbSolucionadoEnObra" class="form-control">
                                            <option value="0">No</option>
                                            <option value="1">Si</option>
									    </select>
                                    </div>
                                    <div class="col-3">
                                        <select id="cmbGeneroCosto" class="form-control">
                                            <option value="0">No</option>
                                            <option value="1">Si</option>
									    </select>
                                    </div>
                                </div>
                            </div>

                            <div id="divTxtObservaciones">
                                <div class="row">
								    <div class="col-12" id="observacionesTitle">Detalles</div>
							    </div>
                                <div class="row">
								    <div class="col-12">
    								    <textarea id="txtObsCntrCmC_O" class="form-control" rows="3"></textarea>
								    </div>
							    </div>
                            </div>
                            <div class="row">
								<div class="col-12">Anexos</div>
							</div>
							<div class="row" style="margin-left: 0px; margin-right: 0px;">
								<input type="file" class="form-control" id="rutaArchivo2C_O" multiple />
							</div>
						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
							<button type="button" id="btnCntrlCmbC_O" class="btn btn-primary">Enviar</button>
						</div>
					</div>
				</div>
			</div>



        <!-- Upper navigation panel (Search FUP by Id) -->
        <div class="card">
				<div class="row">
					<div class="col-1"></div>
					<div class="col-12">
					<table class="table table-sm table-hover mb-0" id="tbSearchFup">
						<tbody>
							<tr>
								<td colspan="2" align="center" style="width: 90px;" data-i18n="[html]FUP_estado_fup"><h3>Estado FUP</h3>
								</td>
								<td colspan="3" align="center" style="width: 90px;">
									<div id="divEstadoFup" class="fupestado" style="font-weight: bold"></div>
								</td>
								<td colspan="2" align="center">
									<button id="btnNuevo" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_nuevo"><i class="fa fa-file"></i></button>
									<%--<input id="btnNuevo" type="button" class="btn btn-primary  " value="Nuevo" data-i18n="[value]FUP_nuevo" />--%>
									<button id="btnFupBlanco" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_fup_blanco"><i class="fa  fa-file-text"></i></button>
									<%--<input id="btnFupBlanco" type="button" class="btn btn-primary " value="Fup Blanco" data-i18n="[value]FUP_fup_blanco" />--%>
									<button id="btnDuplicar" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_duplicar" ><i class="fa fa-copy"></i></button>
									<%--<input id="btnDuplicar" type="button" class="btn btn-primary " value="Duplicar" data-i18n="[value]FUP_duplicar" />--%>
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
                            <tr id="trComplejidad" hidden="hidden">
                                <td colspan="2" style="border: none" align="center">
                                    <span class="font-weight-bold" style="color:blue ">Complejidad: <span id="txtComplejidadGeneral"></span></span>
                                </td>
                            </tr>
						</tbody>
					</table>
					</div>
			  </div>
		</div>
        
        <!-- Accordion of information, main structure -->
        <div id="accordion">
            <!-- Client general data -->
            <div class="card" id="DatosGen" >
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseDatosGenerales" data-i18n="FUP_datos_generales">DATOS GENERALES</a>
				</div>
				<div id="collapseDatosGenerales" class="collapse show" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-md-1 col-sm-4" data-i18n="[html]FUP_pais">
								Pais *
							</div>
							<div class="col-md-2 col-sm-8">
								<select id="cboIdPais" class="form-control select-filter">
								</select>
							</div>
							<div class="col-md-1 col-sm-4" data-i18n="[html]FUP_ciudad">
								Ciudad *
							</div>
							<div class="col-md-2 col-sm-8">
								<select id="cboIdCiudad" class="form-control select-filter">
								</select>
							</div>
							<div class="col-md-1 col-sm-4" data-i18n="[html]FUP_empresa">
								Empresa *
							</div>
							<div class="col-md-5 col-sm-8">
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
					</div>
				</div>
			</div>

            <!-- General info -->
            <div class="card">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" data-i18n="[html]FUP_informacion_general">INFORMACION GENERAL
					</a>
				</div>
				<div id="collapseTwo" class="collapse" data-parent="#accordion">
					<div class="card-body">
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
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>-Equipo Nuevo Aluminio (Forsa Alum ó Forsa Plus): Son aquellos proyectos que trabajaran con formaleta de aluminio para todos sus detalles arquitectónicos indicados en los planos del cliente y en el FUP. <br/>- Adaptación: Son aquellos proyectos donde se reutilizaran equipos de formaletas de proyectos anteriores a un nuevo modelo de proyecto.<br/>-  Listados: Son aquellos que el cliente solicita basado en revisión e inventarios internos de su proceso para cubrir sus necesidades en el buen desempeño de su proyecto.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Tipo de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>

							</div>
							<div class="col-1" data-i18n="[html]producto">Producto</div>
							<div class="col-3">
								<select id="selectProducto" data-modelo="Producto">
									<option value="-1">Producto</option>
									<option value="1">FORSA ALUM</option>
									<option value="2">FORSA PLUS</option>
									<option value="3">FORSA ACERO</option>
								</select>
							</div>
						</div>

						<div class="row">
							<div class="col-3">
								<div class="row">
									<div class="col-4" data-i18n="[html]tipo_vaciado">Tipo de Vaciado</div>
									<div class="col-8" style="display: inline-table">
										<select id="selectTipoVaciado" data-modelo="TipoVaciado" style="width: 80% !important;">
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
										<select id="selectTerminoNegociacion" data-modelo="TerminoNegociacion" style="width: 80% !important;">
										</select>
									</div>
								</div>
							</div>

							<div class="col-9">
                                <div class="row">
                                    <div class="col-4">
                                        <div class="row">
								            <div class="col-5">Equipo Copia / Modulacion Existente</div>
								            <div class="col-6 SeCopia">
								                <select id="selectCopia" data-modelo="EquipoCopia">
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
										        <input type="text" min="0" onblur="ValidarReferencia(this)" class="txtOrdenReferencia fuparr fuplist " /> <span></span>
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
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:150%; height:150%'  src='Imagenes//Pisos edificacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]cantidad_fundiciones_piso">
								Cantidad fundiciones piso:
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtCantidadFundicionesPiso" class="fuparr" type="number" min="0" data-modelo="FundicionPisos" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>Se refiere a la cantidad de armados del equipo que se deben realizar en una planta o nivel de la edificación, para su fundición completa.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Fundiciones.jpg' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]nro_equipos">
								# Equipos
							</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtNroEquipos" type="number" min="0" class="" data-modelo="NumeroEquipos" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:60%; height:60%'src='Imagenes/8_img_No Equipos.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Numero de Equipos.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
							<div class="col-4" data-i18n="[html]sistemasegNota" style="font-size: 11px;">: En el caso de Brasil el Sistema de Seguridad SM 1.0 será reemplazado por el Sistema de Seguridad SM 2.0 en todos los casos.</div>
							<div class="col-1" data-i18n="[html]alineacion_vertical">Alineacion Vertical</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectAlineacionVertical" class="fuparr" data-modelo="AlineacionVertical">
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
						<div class="row divarrlist">
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]espesor_muro">Espesor Muro</span>
								<button id="btnAgregarEspesorMuro" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
							</div>
							<div class="col-1">
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes//Espesor de Muro.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]espesor_losas">Espesor Losa</span>
								<button id="btnAgregarEspesorLosa" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
							</div>
							<div class="col-1">
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes//Espesor de Losa.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]enrase_puerta">Enrase puertas (cm)</span>
								<button id="btnAgregarEnrasePuertas" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
							</div>
							<div class="col-1">
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%'src='Imagenes/enrrase de puerta.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2  medium font-weight-bold text-center">
								<span data-i18n="[html]enrase_ventanas">Enrase Ventanas (cm)</span>
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
										<!--<input type="number" min="0" required class="txtValorMuro fuparr fuplist" />-->
									</div>
									<div class="col-3">
									</div>
								</div>
							</div>
							<div class="col-3 divContentEspesorLosa">
								<div class="row">
									<div class="col-3 text-center"># 1</div>
									<div class="col-4">
										<!--<input type="number" min="0" required class="txtValorLosa fuparr fuplist" />-->
									</div>
									<div class="col-3">
									</div>
								</div>
							</div>
							<div class="col-3 divContentEnrasePuertas">
								<div class="row ">
									<div class="col-3 text-center"># 1</div>
									<div class="col-4">
										<!--<input type="number" min="0" required class="txtEnrasePuertas fuparr fuplist" />-->
									</div>
								</div>
							</div>
							<div class="col-3 divContentEnraseVentanas">
								<div class="row ">
									<div class="col-3 text-center"># 1</div>
									<div class="col-4">
										<!--<input type="number" min="0" required class="txtEnraseVentanas fuparr fuplist" />-->
									</div>
								</div>
							</div>
						</div>

						<hr class="divarrlist" />

						<div class="row divarrlist">
							<div class="col-1" data-i18n="[html]altura_libre">Altura libre</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectAlturaLibre" class="fuparr fuplist" data-modelo="AlturaLibre"></select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes//Altura Libre.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1 divAlturaLibreVariable" data-i18n="[html]altura_libre_minima">Alt. Libre Min.</div>
							<div class="col-1 divAlturaLibreVariable">
								<input type="number" min="0" class="fuparr" id="txtAlturaLibreMinima" data-modelo="AlturaLibreMinima" />
							</div>
							<div class="col-1 divAlturaLibreVariable" data-i18n="[html]altura_libre_maxima">Alt. Libre Max.</div>
							<div class="col-1 divAlturaLibreVariable">
								<input type="number" min="0" class="fuparr" id="txtAlturaLibreMaxima" data-modelo="AlturaLibreMaxima" />
							</div>
							<div class="col-1 divAlturaLibreOtro" data-i18n="[html]cual_altura_libre">Cual Alt. Libre?</div>
							<div class="col-1 divAlturaLibreOtro">
								<input type="number" min="0" id="txtAlturaLibreCual" data-modelo="AlturaLibreCual" class="fuparr" />
							</div>
						</div>

						<div class="row divarrlist">
							<div class="col-2" data-i18n="[html]altura_fm_interna_sugerida">Altura FM Interna Sugerida:</div>
							<div class="col-2" style="display: inline-table">
								<input id="txtAlturaInternaSugerida" style="width: 80% !important;" data-modelo="AlturaIntSugerida" class="fuparr" type="text" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%' src='Imagenes//Altura de la FM interna.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
						   </div>
							<div class="col-1" data-i18n="[html]tipo_fm_fachada">Tipo FM Fachada</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectTipoFMFachada" data-modelo="TipoFachada" class="fuparr">
									<option value="-1">Seleccionar</option>
									<option value="1">Estandar</option>
									<option value="2">Alta</option>
								</select>
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div> -Formaleta Estandar: son FM externas que para alcanzar la altura libre sumado el espesor de losa, requieren que se modulen CAP, como complementos para estas.<br/>- Formaleta Alta: Son FM externo que contemplan la altura del muro y el espesor de losa, eliminando asi el CAP como complemento.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:80%; height:80%' src='Imagenes//Formaleta de Muro.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
						   </div>

						</div>

						<div class="row divarrlist">
							<div class="col-2" data-i18n="[html]altura_cap">Altura de CAP</div>
							<div class="col-2" style="display: inline-table">
								<input id="txtAlturaCap1" style="width: 80% !important;" data-modelo="AlturaCAP1" class="fuparr" type="text" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:80%; height:80%' src='Imagenes/21_img_altura de cap.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%' src='Imagenes//Altura de Cap.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>

							</div>
								<div class="col-1" id="CapPernadolbl" >¿CAP Pernado?</div>
								<div class="col-2" style="display: inline-table">
									<select style="width:50% !important;" id="selectCAPPernado" data-modelo="CapPernado" class="fuparr" >
										<option value="-1">Seleccione</option>
										<option value="1">SI</option>
										<option value="2">NO</option>
									</select>
								</div>
						</div>
						<div class="row divarrlist">
							<div class="col-1" data-i18n="[html]tipo_union">Tipo Union:</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtTipoUnion" data-modelo="TipoUnion" class="fuparr" type="text" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes//Tipo de Union.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]detalle_union">Detalle Union</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectDetalleUnion" data-modelo="DetalleUnion" class="fuparr fupadap fuplist">
									<option value="-1">Seleccionar</option>
									<option value="1">Lisa</option>
									<option value="2">Dilatada</option>
									<option value="3">Cenefa</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:70%; height:70%' src='Imagenes//Detalle de la Union.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-1" data-i18n="[html]altura_union">Altura Union:</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" id="txtAlturaUnion" data-modelo="AlturaUnion" class="fuparr" type="text" />
								<%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/18_img_altunion.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%' src='Imagenes//Altura de la Union.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>

						<div class="row alturacap1 divarrlist"></div>

						<div class="row divarrlist forsapro">
							<div class="col-1" data-i18n="[html]req_cliente">Req Cliente</div>
							<div class="col-2" style="display: inline-table">
								<select id="selectReqCliente" data-modelo="ReqCliente" class="fuparr" style="width: 80% !important;">
									<option value="-1">Seleccione</option>
									<option value="1">SI</option>
									<option value="2">NO</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Los requerimientos del cliente, aplican cuando requieren solicitar medidas diferentes a las sugeridas por el estándar forsa. Aclarando que esto puede afectar el precio"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-8" data-i18n="[html]req_clienteNota" style="font-size: 11px;">NOTA: Al seleccionar 'Requerimientos del Cliente', se tomaran estos datos y medidas para elaborar las cotizaciones, omitiendo las sugeridas por FORSA en esta sección. Teniendo en cuenta que esto puede llegar a afectar el precio, al salirse del estándar definido por la compañía</div>
						</div>

						<div class="divarrlist">
							<div class="row divReqCliente">
								<div class="col-2" data-i18n="[html]altura_fm_interna_sugerida">Altura FM Interna Sugerida:</div>
								<div class="col-1">
									<input id="txtAlturaInternaSugeridaCliente" data-modelo="AlturaIntSugeridaCliente" type="text" class="fuparr" />
								</div>
								<div class="col-1" data-i18n="[html]tipo_fm_fachada">Tipo FM Fachada</div>
								<div class="col-2">
									<select id="selectTipoFMFachadaCliente" data-modelo="TipoFachadaCliente" class="fuparr">
										<option value="-1">Seleccionar</option>
										<option value="1">Estandar</option>
										<option value="2">Alta</option>
									</select>
								</div>
								<div class="col-1" data-i18n="[html]altura_cap">Altura de CAP</div>
								<div class="col-1">
									<input id="txtAlturaCapCliente1" data-modelo="AlturaCAP1Cliente" type="text" class="fuparr" />
								</div>
							</div>

							<div class="row divReqCliente">
								<div class="col-1" data-i18n="[html]tipo_union">Tipo Union:</div>
								<div class="col-2">
									<select id="selectTipoUnionCliente" data-modelo="TipoUnionCliente" class="fuparr"></select>
									<%--<input id="txtTipoUnionCliente" data-modelo="TipoUnionCliente" type="text" class="fuparr" />--%>
								</div>
								<div class="col-1" data-i18n="[html]detalle_union">Detalle Union</div>
								<div class="col-2">
									<select id="selectDetalleUnionCliente" data-modelo="DetalleUnionCliente" class="fuparr">
										<option value="-1">Seleccionar</option>
										<option value="1">Lisa</option>
										<option value="2">Dilatada</option>
										<option value="3">Cenefa</option>
									</select>
								</div>
								<div class="col-1" data-i18n="[html]altura_union">Altura Union:</div>
								<div class="col-2">
									<input id="txtAlturaUnionCliente" data-modelo="AlturaUnionCliente" type="text" class="fuparr" />
								</div>

								<%--<div class="col-1">
								<input id="txtAlturaCapCliente2" data-modelo="AlturaCAP2Cliente" type="text" class="fuparr" />
							</div>--%>
							</div>

						</div>

						<%--<div class="row">
							<div class="col-12 medium font-weight-bold text-center" data-i18n="[html]altura_cap">Altura de CAP</div>
						</div>--%>

						<div class="row alturacap2 divarrlist"></div>

						<hr class="divarrlist" />
						<div class="divarrlist box-title" data-i18n="[html]FUP_datos_constructivos">Datos Constructivos</div>
						<div class="divarrlist box-title" data-i18n="[html]FUP_datos_Urbanisticos">Basado en Datos Urbanísticos</div>																															   
						<div class="row divarrlist">
							<div class="col-2" data-i18n="[html]forma_construccion">Forma de Construcción</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectFormaConstruccion" data-modelo="FormaConstructiva" class="fuparr fuplist">
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
							<div class="col-2" data-i18n="[html]distancia_minima_edificaciones">Dist. Min. Edificios / Vivienda (cm)</div>
							<div class="col-2" style="display: inline-table">
								<input style="width: 80% !important;" type="number" min="0" id="txtDistMinEdificaciones" class="fuparr fuplist" data-modelo="DistanciaEdifica" />                                
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:100%; height:100%;float:right' src='Imagenes/Distancia Minima entre Edificios.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>

							</div>
							<div class="col-1 forsapro" data-i18n="[html]desnivel">Desnivel</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectDesnivel" data-modelo="Desnivel" class="fuparr fuplist forsapro">
									<option value="-1">Seleccionar</option>
									<option value="1">Ascendente</option>
									<option value="2">Descendiente</option>
									<option value="3">No Aplica</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda forsapro" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:100%; height:100%;float:right' src='Imagenes//Desnivel.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>
						<div class="row divarrlist">
							<div class="col-2" data-i18n="[html]presenta_dilatacion_muros">Junta de dilatacion entre muros</div>
							<div class="col-2" style="display: inline-table">
								<select style="width: 80% !important;" id="selectJuntaDilatacion" data-modelo="DilatacionMuro" class="fuparr fuplist">
									<option value="-1">Seleccionar</option>
									<option value="1">SI</option>
									<option value="2">NO</option>
								</select>
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes/junta de Dilatación en Muro.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
							<div class="col-2 divEspesorJuntas" data-i18n="[html]espesor_juntas">Espesor entre juntas</div>
							<div class="col-2 divEspesorJuntas" style="display: inline-table">
								<input style="width: 80% !important;" type="number" min="0" id="txtEspesorJuntas" data-modelo="EspesorJunta" class="fuparr fuplist" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:90%; height:90%' src='Imagenes/29_juntas de dilatacion entre muros-espesor entre juntas.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
							</div>
						</div>
						<hr class="divarrlist" />

						<hr class="divarrlist" />

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

            <!-- Changes control -->
            <div class="card" id="ParteControlCambio">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseControl" >Control de Cambios</a>
				</div>
				<div id="collapseControl" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShow('Control de Cambios',0,0,'',0)">
								    <i class="fa fa-comment">  </i>
									<b data-i18n="[html]FUP_cntrcmb">Control de Cambios</b>
								</button>
							</div>                            
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary controlcambiosDFT" data-toggle="modal" onclick="ControlCambioShow('Control de Cambios',0,0,'',1)">
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

            <!-- Armado -->
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
            
            <!-- Acta mesa técnica -->
            <div class="card" id="ParteActaMesaTecnica">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseActaMesaTecnica">Acta Mesa Técnica
					</a>
				</div>
				<div id="collapseActaMesaTecnica" class="collapse" data-parent="#accordion">
					<div class="card-body">
                     </div>
                </div>
            </div>

            <!-- Listas de empaque -->
            <div class="card" id="ParteListasEmpaque">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseListasEmpaque">
                        Listas de Empaque
					</a>
				</div>
				<div id="collapseListasEmpaque" class="collapse" data-parent="#accordion">
					<div class="card-body">
                     </div>
                </div>
            </div>

            <!-- Consumibles -->
            <div class="card" id="ParteConsumibles">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseConsumibles">
                        Consumibles
					</a>
				</div>
				<div id="collapseConsumibles" class="collapse" data-parent="#accordion">
					<div class="card-body">
                     </div>
                </div>
            </div>

            <!-- Consideraciones u Observaciones -->
            <div class="card" id="ParteConsiderationObservation">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseControlC_O" >Control de Consideraciones y Observaciones</a>
				</div>
				<div id="collapseControlC_O" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table" id="containerBtnCreateConsiderationObservation">
							    <button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShowC_O('Consideraciones y Observaciones',0,0,'',0, 1)">
								    <i class="fa fa-comment">  </i>
									<b data-i18n="">Observaciones y Consideraciones</b>
								</button>
							</div>                            
							<!--<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary controlcambiosDFT" data-toggle="modal" onclick="ControlCambioShowC_O('Consideraciones y Observaciones',0,0,'',1)">
									<i class="fa fa-map">  </i>
									<b >Control DFT</b>
								</button>
							</div>  -->                          
                        </div>
                        <div id="DinamycChangeC_O">
						    <div class="row Comentario">
							    <div class="col-12">
								    <table class="table table-sm table-hover" id="tab_ControlCambioC_O">
									    <thead class="thead-light">
										    <tr>
											    <th class="text-center" >Fecha</th>
											    <th class="text-center" >Estado Fup</th>
											    <th class="text-center" >Observacion</th>
											    <th class="text-center" >Usuario</th>
										    </tr>
									    </thead>
									    <tbody id="tbodyControlCambioC_O">
									    </tbody>
								    </table>
							    </div>
						    </div>

                        </div>
					</div>
				</div>
			</div>

            <!-- Seguimiento logístico -->
            <div class="card" id="ParteSegLogistico">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseSegLogistico" >Seguimiento Logistico</a>
				</div>
				<div id="collapseSegLogistico" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShowC_O('Seguimiento logistico',0,0,'',0, 2)">
								    <i class="fa fa-comment">  </i>
									<b data-i18n="">Seguimiento Logistico</b>
								</button>
							</div>                            
							<!--<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary controlcambiosDFT" data-toggle="modal" onclick="ControlCambioShowC_O('Consideraciones y Observaciones',0,0,'',1)">
									<i class="fa fa-map">  </i>
									<b >Control DFT</b>
								</button>
							</div>  -->                          
                        </div>
                        <div id="DinamycChangeSegLogistico">
						    <div class="row Comentario">
							    <div class="col-12">
								    <table class="table table-sm table-hover" id="tab_SegLogistico">
									    <thead class="thead-light">
										    <tr>
											    <th class="text-center" >Fecha</th>
											    <th class="text-center" >Estado Fup</th>
											    <th class="text-center" >Observacion</th>
											    <th class="text-center" >Usuario</th>
										    </tr>
									    </thead>
									    <tbody id="tbodySegLogistico">
									    </tbody>
								    </table>
							    </div>
						    </div>

                        </div>
					</div>
				</div>
			</div>

            <!-- Hallazgos en obra -->
            <div class="card" id="ParteHallazgos">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseHallazgos" >Hallazgos en obra</a>
				</div>
				<div id="collapseHallazgos" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShowC_O('Hallazgo en obra',0,0,'',0, 3)">
								    <i class="fa fa-comment">  </i>
									<b data-i18n="">Hallazgos en obra</b>
								</button>
							</div>                            
							<!--<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary controlcambiosDFT" data-toggle="modal" onclick="ControlCambioShowC_O('Consideraciones y Observaciones',0,0,'',1)">
									<i class="fa fa-map">  </i>
									<b >Control DFT</b>
								</button>
							</div>  -->                          
                        </div>
                        <div id="DinamycChangeHallazgos">
						    <div class="row Comentario">
							    <div class="col-12">
								    <table class="table table-sm table-hover" id="tab_Hallazgos">
									    <thead class="thead-light">
										    <tr>
											    <th class="text-center" >Fecha</th>
											    <th class="text-center" >Estado Fup</th>
											    <th class="text-center" >Observacion</th>
											    <th class="text-center" >Usuario</th>
										    </tr>
									    </thead>
									    <tbody id="tbodyHallazgos">
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
</asp:Content>

