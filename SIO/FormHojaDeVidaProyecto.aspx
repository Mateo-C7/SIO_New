<%@ Page Title="" Language="C#" MasterPageFile="~/General2HamburgerSideBar.Master" AutoEventWireup="true" CodeBehind="FormHojaDeVidaProyecto.aspx.cs" Inherits="SIO.FormPrototypeReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
    <script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
	<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
	<script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
    <script type="text/javascript" src="Scripts/Datatables_Scripts/datatables.js"></script>
    <script type="text/javascript" src="Scripts/ComboBoxJQuery/js/bootstrap-multiselect.js"></script>
    <script type="text/javascript" src="Scripts/ckeditor.js"></script>
	<script type="text/javascript" src="Scripts/formHojaDeVidaProyecto.js?v=20230922A"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/select2.min.js"></script>
	<script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?v=202006"></script>
	<link rel="Stylesheet" href="Content/bootstrap.min.css" />
	<link rel="Stylesheet" href="Content/SIO.css" />
	<link rel="stylesheet" href="Content/font-awesome.css" />
	<link rel="Stylesheet" href="Content/css/select2.min.css" />
	<link href="Content/toastr.min.css" rel="stylesheet" />
    <link rel="Stylesheet" href="Content/datatables.css" />
    <link rel="Stylesheet" href="Scripts/ComboBoxJQuery/css/bootstrap-multiselect.css"/>
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
            <div id="titulo" >
		        <h3 data-i18n="[html]FUP_hojadevida_titulo">Hoja de Vida del Proyecto</h3>
	        </div>

        </div>
        <!-- Modal Anexos -->
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
							<button type="button" class="btn btn-secondary" data-dismiss="modal" data-i18n="[html]FUP_cancelar">Cancelar</button>
							<button type="button" id="btnUploadFiles" class="btn btn-primary" data-i18n="[html]FUP_SubirArchivo">Cargar Archivo</button>
						</div>
					</div>
				</div>
			</div>

        <!-- Modal control de cambios -->
       <div class="modal fade" id="ModControlCambios" tabindex="-1" role="dialog" aria-hidden="true">
				<div class="modal-dialog" role="document">
					<div class="modal-content">
						<div class="modal-header">
							<h5 class="modal-title" data-i18n="[html]cntrcmb">Control de Cambios</h5>
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
								<div class="col-12" data-i18n="[html]FUP_titulo">Titulo</div>
							</div>
                            <div class="row">
								<div class="col-12">
    								<input type="text" class="form-control" id="txtTituloObs" />
								</div>
							</div>
                            <!--<div class="row Rdft" id = "Rdft">
								<div class="col-12">Tipo de Hallazgo</div>
								<div class="col-6">
                                    <select id="cmbSubProceso" class="form-control">
									</select>
								</div>-->
                            <%--<div class="col-6">
                                    <select id="cmbEstadoDft" class="form-control">
									</select>
								</div>--%>
							</div>
                            <div class="row Rdft2">
								<div class="col-6" data-i18n="[html]FUP_tipo_control">Tipo de Control</div>
								<div class="col-6" data-i18n="[html]FUP_garantias_estado">Estado</div>
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
								<div class="col-12" data-i18n="[html]FUP_notas_observaciones">Notas y Observaciones</div>
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
							<button type="button" class="btn btn-secondary" data-dismiss="modal" data-i18n="[html]FUP_cancelar">Cancelar</button>
							<button type="button" id="btnCntrlCmb" class="btn btn-primary" data-i18n="[html]FUP_enviar">Enviar</button>
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
                                <input type="text" class="form-control" id="isResponseHallazgo" disabled="disabled" hidden />
                            </div>
                            <div id="lblTitulo">
                                <div class="row" >
								    <div class="col-12" data-i18n="[html]FUP_titulo">Titulo</div>
							    </div>
                                <div class="row">
								    <div class="col-12">
    								    <input type="text" class="form-control" id="txtTituloObsC_O" />
								    </div>
							    </div>
                            </div>
                            <div id="camposHallazgos">
                                <div class="row">
                                    <div class="col-6" data-i18n="[html]FUP_orden_hvp">Orden</div>
								    <div class="col-3" data-i18n="[html]FUP_solucionado_obra">Solucionado en obra?</div>
                                    <div class="col-3" data-i18n="[html]FUP_genero_costo">Generó un costo?</div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <select class="form-control" id="cmbGenerarNoReclamo">
                                            <option value="">Seleccionar</option>
                                        </select>
                                    </div>
                                    <div class="col-3">
                                        <select id="cmbSolucionadoEnObra" class="form-control">
                                            <option value="0" data-i18n="[html]no_hpv">No</option>
                                            <option value="1" data-i18n="[html]si_hpv">Si</option>
									    </select>
                                    </div>
                                    <div class="col-3">
                                        <select id="cmbGeneroCosto" class="form-control">
                                            <option value="0" data-i18n="[html]no_hpv">No</option>
                                            <option value="1" data-i18n="[html]si_hpv">Si</option>
									    </select>
                                    </div>
                                </div>
                            </div>

                            <div id="camposRespuestasHallazgos">
                                <div class="row">
                                    <div class="col-6" data-i18n="[html]FUP_plan_accion">Plan de acción</div>
								    <div class="col-6" data-i18n="[html]FUP_fecha_implementacion">Fecha de implementación</div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <select id="cmbPlanAccionC_O" class="form-control">
                                            <option value="1" selected="selected" data-i18n="[html]FUP_correctivo_orden">Correctivo para la orden</option>
                                            <option value="2" data-i18n="[html]FUP_mejora_proceso">Mejora en el proceso</option>
									    </select>
								    </div>
                                    <div class="col-6">
                                        <input type="date" id="txtFecImplResHallazgo"/>
								    </div>
                                </div>
                            </div>

                            <div id="camposBitacoraObra">
                                <div class="row">
                                        <div class="col-12" data-i18n="[html]FUP_director_obra">Director de la Obra</div>
                                </div>
                                <div class="row">
                                    <div class="col-12">
                                        <input type="text" class="form-control" id="txtDirectorObra"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6" data-i18n="[html]FUP_residente_obra">Residente de Obra</div>
								    <div class="col-6" data-i18n="[html]FUP_correo_residente">Email del Residente</div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <input type="text" class="form-control" id="txtResponsableObra"/>
                                    </div>
                                    <div class="col-6">
                                        <input type="text" class="form-control" id="txtEmailResponsableObra"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-6" data-i18n="[html]FUP_telefono_residente">Teléfono del Residente</div>
								    <div class="col-6" data-i18n="[html]FUP_direccion_obra">Dirección de la obra</div>
                                </div>
                                <div class="row">
                                    <div class="col-6">
                                        <input type="text" class="form-control" id="txtTelefonoResponsableObra" pattern="[1-9]{1}[0-9]{9}"/>
                                    </div>
                                    <div class="col-6">
                                        <input type="text" class="form-control" id="txtDireccionObra"/>
                                    </div>
                                </div>
                            </div>

                            <div id="divTxtRecomPregunta">
                                <div class="row">
								    <div class="col-12" id="preguntaTitle" style="font-weight: bold" data-i18n="[html]hvp_RecomendacionPregunta">Detalles</div>
							    </div>
						    </div>
                            <div id="divTxtObservaciones">
                                <div class="row">
								    <div class="col-12" id="observacionesTitle" data-i18n="[html]FUP_detalles">Detalles</div>
							    </div>
                                <div class="row">
								    <div class="col-12">
    								    <textarea id="txtObsCntrCmC_O" class="form-control" rows="4"></textarea>
								    </div>
							    </div>
                            </div>
                            <div id="grpAnexos">
                                <div class="row">
								    <div class="col-12">Anexos</div>
							    </div>
							    <div class="row" style="margin-left: 0px; margin-right: 0px;">
								    <input type="file" class="form-control" id="rutaArchivo2C_O" multiple />
							    </div>
							</div>

						</div>
						<div class="modal-footer">
							<button type="button" class="btn btn-secondary" data-dismiss="modal" data-i18n="[html]FUP_cancelar">Cancelar</button>
							<button type="button" id="btnCntrlCmbC_O" class="btn btn-primary" data-i18n="[html]FUP_enviar">Enviar</button>
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
                                    <span class="font-weight-bold" style="color:blue;">Complejidad: <span id="txtComplejidadGeneral"></span></span>
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
							<div class="col-md-1 col-sm-4 font-weight-bold" data-i18n="[html]FUP_pais">
								Pais: 
							</div>
							<div class="col-md-2 col-sm-8">
                                <input type="text" disabled="disabled" class="form-control" id="txtPais"/>
							</div>
							<div class="col-md-1 col-sm-4 font-weight-bold" data-i18n="[html]FUP_ciudad">
								Ciudad: 
							</div>
							<div class="col-md-2 col-sm-8">
                                <input type="text" disabled="disabled" class="form-control" id="txtCiudad"/>
							</div>
							<div class="col-md-1 col-sm-4 font-weight-bold" data-i18n="[html]FUP_empresa">
								Empresa: 
							</div>
							<div class="col-md-5 col-sm-8">
                                <input type="text" disabled="disabled" class="form-control" id="txtEmpresa"/>
							</div>
						</div>
						<div class="row">
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_contacto">
								Contacto: 
							</div>
							<div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="txtContacto"/>
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_obra">
								Obra: 
							</div>
							<div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="txtObra"/>
							</div>
						</div>
						<div class="row">
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_unds_construir">
								Total Unidades Obra: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdUnidadesConstruir"/>
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_unds_construir_forsa">
								Unds Construir Forsa: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdUnidadesConstruirForsa"/>
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_m2_vivienda">
								M² Vivienda: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdMetrosCuadradosVivienda"/>
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_estrato">
								Estrato: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdEstrato"/>
							</div>
						</div>
						<div class="row">
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_moneda">
								Moneda: 
							</div>
							<div class="col-2" div="divCboIdMoneda">
                                <input type="text" disabled="disabled" class="form-control" id="txtMoneda"/>
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_tipo_vivienda">
								Tipo Obra:
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtTipoVivienda"/>
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_clase_cotizacion" >
								Clase Cotización: 
							</div>
							<div class="col-2" style="display: inline-table">
                                <input type="text" disabled="disabled" class="form-control" id="txtClaseCotizacion"/>
								<!-- <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Clase de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button> -->
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_version">
								Version: 
							</div>
							<div class="col-1">
								<!--<select id="cboVersion" class="form-control " disabled="disabled">
								</select>-->
                                <input type="text" id="cboVersion" disabled="disabled" hidden/>
                                <input type="text" disabled="disabled" class="form-control" id="txtFupVersion"/>
							</div>
						</div>
						<div class="row">
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_estado_cliente">
                                Estado Cliente: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="spanEstadoCliente"/>
								<!-- <input id="txtEstadoCliente" disabled="disabled" type="text" /> -->
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_probabilidad">
                                Probabilidad: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="spanProbabilidad"/>
								<!-- <input id="txtProbabilidad" disabled="disabled" type="text" /> -->
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_fecha_creacion">
								Fecha Creación: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="spanFechaCreacion"/>
								<!-- <input id="txtFechaCreacion" disabled="disabled" type="text" /> -->
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_fecha_solicitaCliente">
								Fecha en que Solicita el Cliente: 
							</div>
							<div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="spanFechaSolicitaCliente"/>
                                <!-- <input id="txtFechaSolicitaCliente" type="date" data-modelo="FecSolicitaCliente" disabled="disabled"/>
                                <button id="btnGrabaFechaSolicita" class="btn SoloUpd" data-toggle="tooltip" onclick="GrabaFechasCliente(1)" data-i18n="[title]FUP_GrabarFechaSolicita"><i class="fa fa-floppy-o"></i></button> -->
                            </div>
					   </div>
						<div class="row">
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_creado_por">
								Creado por: 
							</div>
							<div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="spanCreadoPor"/>
								<!-- <input id="txtCreadoPor" disabled="disabled" type="text" disabled="disabled"/> -->
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_cotizado_por">
								Cotizado por: 
							</div>
							<div class="col-4">
                                <input type="text" disabled="disabled" class="form-control" id="spanCotizadoPor"/>
								<!-- <input id="txtCotizadoPor" disabled="disabled" type="text" disabled="disabled"/> -->
							</div>
						</div>
						<div class="row">
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_gerente_zona">
								Gerente Zona: 
							</div>
							<div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="spanGerenteZona"/>
							</div>
							<div class="col-1 font-weight-bold" data-i18n="[html]FUP_comercial_zona">
								Comercial Zona: 
							</div>
							<div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="spanComercialZona"/>
							</div>
						</div>
                        <div class="row" style="margin-top: 20px;">
                            <div class="col-12">
                                <hr />
                                <h6 class="text-center" data-i18n="[html]FUP_ordenes_de_fabricacion">Ordenes de fabricación</h6>
							    <table id="tbOrdenFabricacion" class="table">
								    <thead>
									    <tr>
										    <th data-i18n="[html]FUP_of_Tipo">Tipo</th>
										    <th data-i18n="[html]FUP_orden_hvp" class="mrv">Orden</th>
										    <th data-i18n="[html]FUP_cant_piezas_alum" class="mrv">Cant Piezas Alum</th>
										    <th data-i18n="[html]FUP_cant_piezas_acc" class="mrv">Cant Piezas Acc</th>
										    <th data-i18n="[html]FUP_planta" class="mrv">Planta</th>
										    <th data-i18n="[html]FUP_responsable" class="mrv">Responsable</th>
										    <th data-i18n="[html]FUP_moduladores" class="mrv">Moduladores</th>
									    </tr>
								    </thead>
								    <tbody id="tbodyOrdenFabricacion"></tbody>
							    </table>
                            </div>
						</div>
                        <div class="row" style="margin-top: 20px;">
                            <div class="col-12">
                                <hr />
                                <h6 class="text-center" data-i18n="[html]FUP_planeacion_tecnicos">Planeacion de Tecnicos </h6>
							    <table id="tbTecnicos" class="table">
								    <thead>
									    <tr>
										    <th data-i18n="[html]FUP_tecnico">Técnico</th>
										    <th data-i18n="[html]FUP_servicio_cp">Servicio Cp</th>
										    <th data-i18n="[html]FUP_fecha_llega_tecnico">Fecha LLega Tecnico Obra</th>
										    <th data-i18n="[html]FUP_fecha_fin_tecnico">Fecha Fin Tecnico Obra</th>
										    <th data-i18n="[html]FUP_dias_total">Dias Total</th>
									    </tr>
								    </thead>
								    <tbody id="tbodyTecnicos"></tbody>
							    </table>
                            </div>
						</div>
					</div>
				</div>
			</div>

            <!-- Ordenes de fabricación
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
			</div> -->

            <!-- General info -->
            <div class="card" id="ParteGeneral">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseTwo" data-i18n="[html]FUP_especificacionTecnica">INFORMACION GENERAL
					</a>
				</div>
				<div id="collapseTwo" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <div class="row">
<%--                            <rsweb:ReportViewer ID="ReporteFUPv" runat="server" Height="500px" 
                                Width="">
                            </rsweb:ReportViewer>--%>
                            <iframe id="inlinefUP"
                                width="100%"
                                height="400"
                                src="">
                            </iframe>
                        </div>
                    </div>
				</div>
			</div>

            <!-- Changes control -->
            <div class="card" id="ParteControlCambio">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseControl" ata-i18n="[html]FUP_cntrcmb">Control de Cambios</a>
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
                        <div id="DinamycChange" class="contenedor_fup">
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

            <!-- Acta Definicion Tecnica -->
            <div class="card" id="ParteActaDefTecnica">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseActaDefTecnica" data-i18n="[html]FUP_acta_definicion_tecnica">Acta Definicion Técnica</a>
				</div>
				<div id="collapseActaDefTecnica" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_ActaDefTecnica1">
									<thead>
										<tr class="table-primary">
											<th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyActaDefTecnica1">
									</tbody>
								</table>
							</div>
						</div>
					</div>
				</div>
			</div>

            <!-- Criterios o Particularidades del cliente -->
            <div class="card" id="ParteCriteriosParticularidades">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseCriteriosParticularidades"
                        data-i18n="[html]FUP_criterios_cliente">
                        Criterios o Particularidades del cliente
					</a>
				</div>
				<div id="collapseCriteriosParticularidades" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <div class="row">
                            <div class="col-4">
                                <table class="table">
                                    <tbody id="tbodyCriteriosEspecialesCliente">
                                        <tr>
<%--                                            <td class="table-light text-center" style="width: 90%;" id="tdNombreArchivoCriteriosEspecialesCliente"></td>--%>
                                            <td class="table-light text-center" style="width: 100%;" id="tdDescargaArchivoCriteriosEspecialesCliente"></td>
                                        </tr>
                                    </tbody>
                                </table>
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
                                <h6 data-i18n="[html]FUP_aluminio">Aluminio</h6>
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
                                <h6 data-i18n="[html]FUP_escalera">Escalera</h6>
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
                                <h6 data-i18n="[html]FUP_accesorios">Accesorios</h6>
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
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseActaMesaTecnica"
                        data-i18n="[html]FUP_acta_mesa_tecnica">Acta Mesa técnica previa al Despacho y Compromisos para la Implementación en Obra</a>
				</div>
				<div id="collapseActaMesaTecnica" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <div class="row">
							<div class="col-12">
								<table class="table table-sm table-hover" id="tab_ActaMesaTecnica1">
									<thead>
										<tr class="table-primary">
											<th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyActaMesaTecnica1">
									</tbody>
								</table>
							</div>
						</div>
                     </div>
                </div>
            </div>

            <!-- Listas de empaque -->
            <div class="card" id="ParteListasEmpaque"
                >
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseListasEmpaque" data-i18n="[html]FUP_listas_empaque">
                        Listas de Empaque
					</a>
				</div>
				<div id="collapseListasEmpaque" class="collapse" data-parent="#accordion">
					<div class="card-body">
<%--                        <div class="row">
                            <div class="col-12">
								<table class="table table-sm table-hover" id="tab_ListasEmpaque1">
									<thead>
										<tr class="table-primary">
											<th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyListasEmpaque1">
									</tbody>
								</table>
							</div>
                        </div>--%>
                        <div class="row" style="margin-top: 20px;">
                            <div class="col-6">
                                <hr />
                                <h5 class="text-center" data-i18n="[html]FUP_lista_ordenes_de_fabricacion">Lista de ordenes de fabricación</h5>
							    <table id="tbOrdenFabricacionListasEmpaque" class="table text-center">
								    <thead class="bg-info text-white">
									    <tr>
										    <th class="py-1">No Orden</th>
										    <th class="mrv py-1" data-i18n="[html]FUP_lista_pallet">Lista Pallet</th>
										    <th class="mrv py-1" data-i18n="[html]FUP_lista_consolidada">Lista Consolidada</th>
										    <th class="mrv py-1"data-i18n="[html]FUP_ver_en_linea_pallet">Ver en Linea Pallet</th>
										    <th class="mrv py-1" data-i18n="[html]FUP_ver_en_linea_consolidada">Ver en Linea Consolidada</th>
                                            <th class="mrv py-1"></th>
									    </tr>
								    </thead>
								    <tbody id="tbodyOrdenFabricacionListasEmpaque"></tbody>
							    </table>
                            </div>
						</div>
		            </div>
                </div>
            </div>

            <!-- Consumibles -->
            <div class="card" id="ParteConsumibles">
				<div class="card-header">
					<a class="collapsed card-link" data-toggle="collapse" href="#collapseConsumibles"
                        data-i18n="[html]FUP_consumibles">
                        Consumibles
					</a>
				</div>
				<div id="collapseConsumibles" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <div id="LineasDimanicas" class="mx-4"></div>
                     </div>
                </div>
            </div>

            <!-- Seguimiento logístico -->
            <div class="card" id="ParteSegLogistico">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseSegLogistico"
                        data-i18n="[html]FUP_seguimiento_logistico">Seguimiento Logístico</a>
				</div>
				<div id="collapseSegLogistico" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <div class="row">
                            <div class="col-2" data-i18n="[html]FUP_real_despacho">Real Despacho:</div>
                            <input type="date" id="txtFecRealDespacho" class="col-2" disabled="disabled"/>
                            <div class="col-2" data-i18n="[html]FUP_real_zarpe">Real Zarpe:</div>
                            <input type="date" id="txtFecEstimadaDespacho" class="col-2" disabled="disabled"/>
                        </div>
                        <div class="row">
                            <div class="col-2" data-i18n="[html]FUP_estimada_arribo_mod">Estimada Arribo Mod:</div>
                            <input type="date" id="txtFecEstimadaArriboMod" class="col-2" disabled="disabled"/>
                            <div class="col-2" data-i18n="[html]FUP_estimada_llegada_obra_mod">Estimada Llegada Obra Mod:</div>
                            <input type="date" id="txtFecEstimadaLlegadaObraMod" class="col-2" disabled="disabled"/>
                        </div>
					</div>
				</div>
			</div>

            <!-- Bitacora de Obra -->
            <div class="card" id="ParteBitacoraObra">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseBitacoraObra"
                        data-i18n="[html]FUP_bitacora_obra">Bitácora de Obra</a>
				</div>
				<div id="collapseBitacoraObra" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShowC_O('Bitácora de Obra','Work Log','Diário de Obra',0,0,'',0, 4)">
								    <i class="fa fa-comment">  </i>
									<b data-i18n="[html]FUP_bitacora_obra">Bitácora de Obra</b>
								</button>
							</div> 
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary" data-toggle="modal" onclick="DescargarReporte(1)">
								    <i class="fa fa-print">  </i>
									<b data-i18n="[html]FUP_descargar_bitacora_obra_btn">Descargar Bitácora de Obra</b>
								</button>
							</div>                            
							<!--<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary controlcambiosDFT" data-toggle="modal" onclick="ControlCambioShowC_O('Consideraciones y Observaciones',0,0,'',1)">
									<i class="fa fa-map">  </i>
									<b >Control DFT</b>
								</button>
							</div>  -->                          
                        </div>

                        <div id="DinamycChangeBitacoraObra" class="contenedor_fup">
						    <div class="row Comentario">
							    <div class="col-12">
								    <table class="table table-sm table-hover" id="tab_BitacoraObra">
									    <thead class="thead-light">
										    <tr>
											    <th class="text-center" data-i18n="[html]FUP_of_Fecha">Fecha</th>
											    <th class="text-center" data-i18n="[html]FUP_estado_fup">Estado Fup</th>
											    <th class="text-center" data-i18n="[html]FUP_observacion_detalle_devoluciones">Observacion</th>
											    <th class="text-center" data-i18n="[html]FUP_usuario">Usuario</th>
										    </tr>
									    </thead>
									    <tbody id="tbodyBitacoraObra">
									    </tbody>
								    </table>
							    </div>
						    </div>

                        </div>
                    </div>
				</div>
			</div>
            <!-- Recomendaciones Pedido Venta -->
            <div class="card" id="ParteRecomendaciones">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseRecomendaciones"
                        data-i18n="[html]hvp_Recomendaciones">Recomendaciones de Compra en Obra</a>
				</div>
				<div id="collapseRecomendaciones" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShowC_O('Recomendacion de compra','Purchase recommendation','Recomendaçõa de compra',0,0,'Recomendacion de compra',0, 2)">
								    <i class="fas fa-cart-shopping">  </i>
									<b data-i18n="[html]hvp_RecomendacionesCrear">Crear Recomendacion</b>
								</button>
							</div> 
							<div class="col-1" style="display: inline-table"> </div>
							<div class="col-6" style="display: inline-table">
							    <b id="RecomendadoMes" class="font-weight-bold" style="color:darkred; font-size: 20px; "> AQUI VA EL PRODUCTO ESTRELLA</b>
							</div> 
							<div class="col-1" style="display: inline-table"> </div>
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-info" data-toggle="modal" onclick="window.location = 'Imagenes/CatalogoRecomendacionCompra.xlsx';" >
                                    <i class="fas fa-store"></i>
									<b>Guía A&C</b>
								</button>
							</div> 
                        </div>

                        <div id="DinamycChangeRecomendaciones" class="contenedor_fup">
						    <div class="row Comentario">
							    <div class="col-12">
								    <table class="table table-sm table-hover" id="tab_Recomendacion">
									    <thead class="thead-light">
										    <tr>
											    <th class="text-center" width="10%">Id</th>
											    <th class="text-center" width="15%" data-i18n="[html]FUP_usuario">Usuario</th>
											    <th class="text-center" width="15%" data-i18n="[html]FUP_of_Fecha">Fecha</th>
											    <th class="text-center" width="50%" data-i18n="[html]FUP_observacion_detalle_devoluciones">Observacion</th>
											    <th class="text-center" width="10%"> PV</th>
										    </tr>
									    </thead>
									    <tbody id="tbodyRecomendacion">
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
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseHallazgos"
                        data-i18n="[html]FUP_hallazgos_obra">Hallazgos en obra</a>
				</div>
				<div id="collapseHallazgos" class="collapse" data-parent="#accordion">
					<div class="card-body">
						<div class="row">
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-success" data-toggle="modal" onclick="ControlCambioShowC_O('Hallazgo en obra','Findings on site','Situações em Obra para revisão pela Equipe Interna',0,0,'',0, 3)">
								    <i class="fa fa-comment">  </i>
									<b data-i18n="[html]FUP_hallazgos_obra_btn">Hallazgos en obra</b>
								</button>
							</div>
							<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary" data-toggle="modal" onclick="DescargarReporte(2)">
								    <i class="fa fa-print">  </i>
									<b data-i18n="[html]FUP_descargar_hallazgos_obra_btn">Descargar Hallazgos de Obra</b>
								</button>
							</div>                            
							<!--<div class="col-2" style="display: inline-table">
							    <button type="button"  class="btn btn-primary controlcambiosDFT" data-toggle="modal" onclick="ControlCambioShowC_O('Consideraciones y Observaciones',0,0,'',1)">
									<i class="fa fa-map">  </i>
									<b >Control DFT</b>
								</button>
							</div>  -->                          
                        </div>
                        <div id="DinamycChangeHallazgos" class="contenedor_fup">
						    <div class="row Comentario">
							    <div class="col-12">
								    <table class="table table-sm table-hover" id="tab_Hallazgos">
									    <thead class="thead-light">
										    <tr>
											    <th class="text-center" data-i18n="[html]FUP_of_Fecha">Fecha</th>
											    <th class="text-center" data-i18n="[html]FUP_estado_fup">Estado Fup</th>
											    <th class="text-center" data-i18n="[html]FUP_observacion_detalle_devoluciones">Observacion</th>
											    <th class="text-center" data-i18n="[html]FUP_usuario">Usuario</th>
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

            <!-- Logros Destacado -->
            <div class="card" id="ParteLogros">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseLogros"
                        data-i18n="[html]FUP_logros_destacados">Logros Destacados del Proyecto</a>
				</div>
				<div id="collapseLogros" class="collapse" data-parent="#accordion">
					<div class="card-body">
					        <div class="row">
                                <span  style="font-weight: bold; font-style:italic" data-i18n="[html]FUP_tecnico_informe_hallazgos_positivos">Estimado Técnico, por favor informe los hallazgos positivos de la obra, precepciones del Cliente sobre el uso del encofrado y demás detalles que considere relevantes.</span>
                            </div>
					        <div class="row">
                                <div class="col-12">
                                    <textarea id="txtLogrosDestacados" rows="10" class="form-control" ></textarea>
                                </div>
                            </div>
					        <div class="row">
                                    <button type="button" class="btn btn-info mt-2" onclick="guardarLogrosDestacados()" data-i18n="[html]FUP_guardar_logros_destacados_btn">
                                        Guardar Logros Destacados</button>
                            </div>
					        <div class="row">
                                    <button  data-i18n="[html]FUP_subir_anexos_btn" type="button" class="btn btn-default " data-toggle="modal" onclick="UploadFielModalShow('Logros del Proyecto',35,'Logros Proyecto')">Subir Anexos </button>
                            </div>
                        <div id="anexoLogros" class="contenedor_fup">
						    <div class="row">
							    <div class="col-12">
								    <table class="table table-sm table-hover" id="tab_Logros">
									<thead>
										<tr class="table-primary">
											<th class="text-center" data-i18n="[html]FUP_nombre_archivo">Nombre Archivo</th>
											<th class="text-center" data-i18n="[html]FUP_fecha_creacion">fecha Crea</th>
											<th class="text-center" > </th>
											<th class="text-center" > </th>
										</tr>
									</thead>
									<tbody id="tbodyLogros">
									</tbody>
								    </table>
							    </div>
						    </div>

                        </div>

                    </div>
				</div>
			</div>

            <!-- Estado de Garantías -->
            <div class="card" id="ParteEstadoGarantias">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseEstadoGarantias"
                        data-i18n="[html]FUP_estado_garantias">Estado de Garantias</a>
				</div>
				<div id="collapseEstadoGarantias" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <table id="table_estado_garantias" class="table table-striped text-center" style="font-size: smaller !important;">
                            <thead>
                                <tr>
                                    <th class="text-center" data-i18n="[html]hvp_orden_origen">OrdenOrigen</th>
                                    <th class="text-center" data-i18n="[html]hvp_orden_og_om">OrdenOgOm</th>
                                    <th class="text-center" data-i18n="[html]FUP_garantias_estado">Estado</th>
                                    <th class="text-center" data-i18n="[html]FUP_garantia_descripcion">Descripcion Reclamo</th>
                                    <th class="text-center" data-i18n="[html]hvp_fecha_despacho">Fecha Despacho</th>
                                    <th class="text-center" data-i18n="[html]hvp_fecha_entrega_obra_estimada">Fec Entrega Obra Estimada</th>
                                    <th class="text-center" data-i18n="[html]hvp_fecha_entrega_obra_real">Fec Entrega Obra Real</th>
                                    <th class="text-center" data-i18n="[html]hvp_link_lista_empaque">Link de Lista de Empaque</th>
                                </tr>
                            </thead>
                            <tbody id="table_body_estado_garantias">

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <!-- PQRS Asociados -->
            <div class="card" id="PartePQRSAsociados">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapsePQRSAsociados"
                        data-i18n="[html]FUP_pqrs_asociados">PQRS Asociados</a>
				</div>
				<div id="collapsePQRSAsociados" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <div class="col-12 border pt-3 pb-3">
                            <table id="tPQRSAsociados" class="table table-striped table-sm">
                                <thead>
                                    <tr>
                                        <th data-i18n="[html]FUP_orden_origen">Orden Origen</th>
                                        <th data-i18n="[html]FUP_consecutivo_pqrs">Consecutivo PQRS</th>
                                        <th data-i18n="[html]FUP_garantias_estado">Estado</th>
                                        <th data-i18n="[html]FUP_fuente">Fuente</th>
                                        <th data-i18n="[html]FUP_descripcion">Descripcion</th>
                                        <th data-i18n="[html]hvp_orden_og_om">Og_Om</th>
                                        <th data-i18n="[html]FUP_solucionado_obra">Solucionado En Obra</th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyPQRSAsociados">

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Acta Mesa Ténica Postventa -->
            <div class="card" id="ParteActaPostventa">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseActaPostventa"
                        data-i18n="[html]FUP_acta_postventa">Acta Mesa Ténica Postventa</a>
				</div>
				<div id="collapseActaPostventa" class="collapse" data-parent="#accordion">
					<div class="card-body">
                        <div class="row">
                            <div class="col-12">
                                <table class="table table-sm table-hover" id="tab_ActaPostventa">
									    <thead>
										    <tr class="table-primary">
											    <th class="text-center" data-i18n="[html]FUP_tipo_anexo">Tipo Anexo</th>
											    <th class="text-center" data-i18n="[html]FUP_anexo">Anexo</th>
											    <th class="text-center" data-i18n="[html]FUP_fecha_anexo">fecha Crea</th>
											    <th class="text-center" > </th>
											    <th class="text-center" > </th>
										    </tr>
									    </thead>
									    <tbody id="tbodyActaPostventa" class="text-center">
									    </tbody>
								    </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Consideraciones u Observaciones -->
            <div class="card" id="ParteConsiderationObservation">
			    <div class="card-header">
				    <a class="collapsed card-link" data-toggle="collapse" href="#collapseControlC_O" 
                        data-i18n="[html]FUP_observaciones_consideraciones">Observaciones o consideraciones durante el proceso de Modulación y/o fabricación</a>
				</div>
				<div id="collapseControlC_O" class="collapse" data-parent="#accordion">
					<div class="card-body">
                            <div class="row">
                                <div class="col-12">
                                    <span style="font-weight: bold;" data-i18n="[html]FUP_estimado_tecnico">
                                        Estimado Técnico, la siguiente información detallada, es de manejo interno 
                                        Forsa, por ningún motivo debe ser compartida con el cliente
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <textarea id="txtObservacionesConsideracionesCliente" rows="5" class="form-control" disabled="disabled"></textarea>
                                </div>
                            </div>
					</div>
				</div>
			</div>


        </div>
    <%--</div>--%>
</asp:Content>

