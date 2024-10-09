<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormSalidaCotizacion.aspx.cs" Inherits="SIO.FormSalidaCotizacion" Culture="en-US" UICulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
    <script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>

    <script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
    <script type="text/javascript" src="Scripts/formsalidacotizacion.js?v=202400515A"></script>
    <script type="text/javascript" src="Scripts/formTabs.js?v=20221020"></script>
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

    <!-- Div for justify a discount if it's necessary -->
    <div class="modal fade" id="justifyDiscountModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Justificación de Descuento</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <textarea class="form-control" id="txtJustifyDiscountComment" rows="3"></textarea>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
            <button type="button" class="btn btn-primary" id="btnContinueOperationCoti" onclick="">Continuar</button>
          </div>
        </div>
      </div>
    </div>

    <div class="container-fluid contenedor_fup">
        <div class="row">
            <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
                <button type="button" class="btn btn-secondary langes">
                    <img alt="español" src="Imagenes/colombia.png" /></button>
                <button type="button" class="btn btn-secondary langen">
                    <img alt="english" src="Imagenes/united-states.png" /></button>
                <button type="button" class="btn btn-secondary langbr">
                    <img alt="portugues" src="Imagenes/brazil.png" /></button>
            </div>
        </div>
        <div class="card">
            <div class="row">
                <div class="col-6">
                    <table class="table table-sm table-hover" id="tbSearchFup">
                        <tbody>
                            <tr>
                                <td colspan="1" align="center" data-i18n="[html]FUP_fup">FUP</td>
                                <td colspan="1" style="width: 90px;">
                                    <input id="txtIdFUP" type="number" min="0" class="form-control  bg-warning text-dark" />
                                </td>
                                <td colspan="1" style="width: 90px;">
                                    <button id="btnBusFup" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_buscar"><i class="fa fa-search" style="margin-left: -200%"></i></button>
                                </td>
                                <td colspan="2" align="center" style="width: 90px;" data-i18n="[html]FUP_estado_fup">
                                    <h6>Estado FUP</h6>
                                </td>
                                <td colspan="3" align="center" style="width: 90px;">
                                    <div id="divEstadoFup" class="fupestado" style="font-weight: bold"></div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-6"></div>
            </div>
        </div>

        <div id="accordion">
            <div class="card" id="DatosGen">
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
                                <input type="text" disabled="disabled" class="form-control" id="txtPais"/>
                                <!--<select id="cboIdPais" class="form-control select-filter" disabled>
                                </select>-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_ciudad">
                                Ciudad *
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtCiudad"/>
                                <!--<select id="cboIdCiudad" class="form-control select-filter" disabled>
                                </select>-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_empresa">
                                Empresa *
                            </div>
                            <div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="txtEmpresa"/>
                                <!--<select id="cboIdEmpresa" data-modelo="ID_Cliente" class="form-control select-filter" disabled>
                                </select>-->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1" data-i18n="[html]FUP_contacto">
                                Contacto *
                            </div>
                            <div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="txtContacto"/>
                                <!--<select id="cboIdContacto" data-modelo="ID_Contacto" class="form-control select-filter" disabled>
                                </select>-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_obra">
                                Obra *
                            </div>
                            <div class="col-5">
                                <input type="text" disabled="disabled" class="form-control" id="txtObra"/>
                                <!--<select id="cboIdObra" data-modelo="ID_Obra" class="form-control select-filter" disabled>
                                </select>-->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1" data-i18n="[html]FUP_unds_construir">
                                Total Unidades Obra*
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdUnidadesConstruir"/>
                                <!--<input id="txtIdUnidadesConstruir" type="number" min="0" class="form-control " data-modelo="TotalUnidadesConstruir" disabled />-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_unds_construir_forsa">
                                Unds Construir Forsa *
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdUnidadesConstruirForsa"/>
                                <!--<input id="txtIdUnidadesConstruirForsa" type="number" min="0" class="form-control " data-modelo="TotalUnidadesConstruirForsa" disabled />-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_m2_vivienda">
                                M² Vivienda *
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdMetrosCuadradosVivienda"/>
                                <!--<input id="txtIdMetrosCuadradosVivienda" type="number" min="0" class="form-control " data-modelo="MetrosCuadradosVivienda" disabled />-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_estrato">
                                Estrato *
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtIdEstrato"/>
                                <!--<select id="cboIdEstrato" class="form-control" data-modelo="Estrato" disabled>
                                </select>-->
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1" data-i18n="[html]FUP_moneda">
                                Moneda * 
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtMoneda"/>
                                <!--<select data-modelo="ID_Moneda" id="cboIdMoneda" class="form-control " disabled>
                                </select>-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_tipo_vivienda">
                                Tipo Obra *
                            </div>
                            <div class="col-2">
                                <input type="text" disabled="disabled" class="form-control" id="txtTipoVivienda"/>
                                <!--<select id="cboIdTipoVivienda" class="form-control " data-modelo="TipoVivienda" disabled>
                                </select>-->
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_clase_cotizacion">
                                Clase Cotización 
                            </div>
                            <div class="col-2" style="display: inline-table">
                                <input type="text" disabled="disabled" class="form-control" id="txtClaseCotizacion"/>
                                <!--<select id="cboClaseCotizacion" class="form-control" data-modelo="ClaseCotizacion" style="width: 60% !important;" disabled>
                                </select>-->
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Clase de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_version">
                                Version 
                            </div>
                            <div class="col-1">
                                <select id="cboVersion" class="form-control ">
                                </select>
<%--                                <input type="text" id="cboVersion" disabled="disabled" hidden/>
                                <input type="text" disabled="disabled" class="form-control" id="txtFupVersion"/>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1" data-i18n="[html]FUP_estado_cliente">Estado Cliente</div>
                            <div class="col-2">
                                <input id="txtEstadoCliente" class="form-control" disabled="disabled" type="text" />
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_probabilidad">Probabilidad</div>
                            <div class="col-2">
                                <input id="txtProbabilidad" class="form-control" disabled="disabled" type="text" />
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_fecha_creacion">
                                Fecha Creación: 
                            </div>
                            <div class="col-2">
                                <input id="txtFechaCreacion" class="form-control" disabled="disabled" type="text" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1" data-i18n="[html]FUP_creado_por">
                                Creado por 
                            </div>
                            <div class="col-5">
                                <input id="txtCreadoPor" class="form-control" type="text" disabled="disabled" />
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_cotizado_por">
                                Cotizado por: 
                            </div>
                            <div class="col-4">
                                <input id="txtCotizadoPor" class="form-control" disabled="disabled" type="text" />
                            </div>
                        </div>
                        <div class="row">
							<div class="col-1" data-i18n="[html]tipo_negociacion">Tipo de Negociacion</div>
							<div class="col-2">
								<input id="txtTipoNegociacion" class="form-control" disabled="disabled" type="text" />
							</div>
							<div class="col-1" data-i18n="[html]tipo_cotizacion">Tipo de Cotizacion</div>
							<div class="col-2 form-inline">
                                <input id="txtTipoCotizacion" class="form-control" disabled="disabled" type="text" />
								<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes//Tipo de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>

							</div>
							<div class="col-1" data-i18n="[html]producto">Producto</div>
							<div class="col-3">
								<input id="txtProducto" class="form-control" disabled="disabled" type="text" />
							</div>
						</div>
                        <div class="row">
                            <div class="col-1" style="font-weight: bold;">Fecha Simulacion</div>
                            <div class="col-5">
                                <input id="txtFecSimu" type="text" class="form-control" style="font-weight: bold;"  disabled="disabled" />
                            </div>
                            <div class="col-6"></div>
                        </div>
                </div>
            </div>
                <div class="card" id="DatosGen2">
                    <div class="card-header">
                        <a class="collapsed card-link" data-toggle="collapse" href="#collapseDatosGen2" data-i18n="AlcanceOferta">Alcance </a>
                    </div>
                    <div id="collapseDatosGen2" class="collapse show" data-parent="#accordion">
                        <div class="card-body">
                             <div row>
                                 <div class="tab">
                                 <button class="tablinks" onclick="SelectTab(event, 'Tab1')">Salida Cotizacion</button>
                                 <button class="tablinks" onclick="SelectTab(event, 'Tab2')">Calculadora Comercial</button>
                                 <button class="tablinks PSugerido" onclick="SelectTab(event, 'Tab3')">Precio Sugerido</button>
                                 <button class="tablinks ResumenSim" onclick="SelectTab(event, 'Tab4')">Resumen Simulación</button>
                               </div>
                                <!-- Tab content -->
                                <div id="Tab1" class="tabcontent">
                                    <div class="row"></div>
                                    <div class="row">

                                    </div>
                                    <div class="row">
					                    <div class="col-10 align-content-center">
                                            <table class="table table-sm table-hover" id="tab_cot">
                                                <thead>
                                                    <tr class="table-info">
                                                        <th class="text-center" >Detalle</th>
                                                        <th class="text-center" >Unitario</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbodycot">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
            
                                    </div>

                                <div id="Tab2" class="tabcontent">
                                    <div class="row"></div>
                                    <div class="row"></div>
                                    <div class="row">
                                        <div class="col-10 align-content-center">
                                            <table class="table table-sm table-hover" id="tab_car">
                                                <thead>
                                                    <tr class="table-info">
                                                        <th class="text-center" width = "40%" >Detalle</th>
                                                        <th class="text-center" width = "25%">Unitario</th>
                                                        <th class="text-center" width = "10%">Descuento</th>
                                                        <th class="text-center" width = "25%">Total</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tbodycar">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                <div id="Tab3" class="tabcontent PSugerido">
                                    <div class="row"></div>
                                    <div class="row"></div>
                                    <div class="row">
                                        <div class="col-10 align-content-center" id="div_sug">
                                            <table class="table table-sm table-hover" id="tab_sug">
                                                <thead>
                                                    <tr class="table-info">
                                                        <th class="text-center" width = "30%" ></th>
                                                        <th class="text-center" width = "20%">Precio Cotizado</th>
                                                        <th class="text-center" width = "20%">Precio Minimo Aprobado</th>
                                                        <th class="text-center" width = "15%">Descuento Maximo</th>
                                                        <th class="text-center" width = "15%">M2</th>
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
                                            <span>Justificacion de descuento</span><br><textarea disabled=disabled rows=3 id=txtDiscountJustification class=form-control></textarea>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-12">
                                            <button type="button" onclick="AutorizarVerPrecio()" id="BtnAutorizarVerPrecio" class="btn btn-info PSugeridobtn" style="display:none">Autorizar ver Precio</button>
                                            <button type="button" onclick="NegarVerPrecio()" id="BtnNegarVerPrecio" class="btn btn-danger PSugeridobtn ml-1" style="display:none">Negar ver Precio</button>
                                        </div>
                                    </div>
                                </div>

                                 <div id="Tab4" class="tabcontent">
                                     <table class="table table-striped table-sm table-bordered text-center">
                                         <thead>
                                             <tr>
                                                 <th>Datos de aluminio</th>
                                                 <th>M2</th>
                                                 <th>Piezas</th>
                                                 <th>Kilogramos</th>
                                                 <th>Piezas / M2</th>
                                                 <th>Kg / M2</th>
                                             </tr>
                                         </thead>
                                         <tbody>
                                             <tr>
                                                 <td></td>
                                                 <td class="camposResumenOrden" id="datosAluminioM2"></td>
                                                 <td class="camposResumenOrden" id="datosAluminioPiezas"></td>
                                                 <td class="camposResumenOrden" id="datosAluminioKilogramos"></td>
                                                 <td class="camposResumenOrden" id="datosAluminioPiezasM2"></td>
                                                 <td class="camposResumenOrden" id="datosAluminioKgM2"></td>
                                             </tr>
                                         </tbody>
                                     </table>
                                     <table class="table table-striped table-sm table-bordered text-center">
                                         <thead>
                                             <tr>
                                                 <th>Niveles</th>
                                                 <th>1</th>
                                                 <th>2</th>
                                                 <th>3</th>
                                                 <th>4</th>
                                                 <th>5</th>
                                                 <th>Total</th>
                                             </tr>
                                         </thead>
                                         <tbody>
                                             <tr>
                                                 <td>Costos</td>
                                                 <td class="camposResumenOrden" id="costosNivel1"></td>
                                                 <td class="camposResumenOrden" id="costosNivel2"></td>
                                                 <td class="camposResumenOrden" id="costosNivel3"></td>
                                                 <td class="camposResumenOrden" id="costosNivel4"></td>
                                                 <td class="camposResumenOrden" id="costosNivel5"></td>
                                                 <td class="camposResumenOrden" id="costosTotal"></td>
                                             </tr>
                                             <tr>
                                                 <td>Kilogramos</td>
                                                 <td class="camposResumenOrden" id="kilogramosNivel1"></td>
                                                 <td class="camposResumenOrden" id="kilogramosNivel2"></td>
                                                 <td class="camposResumenOrden" id="kilogramosNivel3"></td>
                                                 <td class="camposResumenOrden" id="kilogramosNivel4"></td>
                                                 <td class="camposResumenOrden" id="kilogramosNivel5"></td>
                                                 <td></td>
                                             </tr>
                                             <tr>
                                                 <td>Costo / Kg</td>
                                                 <td class="camposResumenOrden" id="costoKgNivel1"></td>
                                                 <td class="camposResumenOrden" id="costoKgNivel2"></td>
                                                 <td class="camposResumenOrden" id="costoKgNivel3"></td>
                                                 <td class="camposResumenOrden" id="costoKgNivel4"></td>
                                                 <td class="camposResumenOrden" id="costoKgNivel5"></td>
                                                 <td></td>
                                             </tr>
                                             <tr>
                                                 <td>% de Participación frente al Nivel 1</td>
                                                 <td class="camposResumenOrden" id="pcParticipacionN1Nivel1"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionN1Nivel2"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionN1Nivel3"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionN1Nivel4"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionN1Nivel5"></td>
                                             </tr>
                                             <tr>
                                                 <td>% de Participación frente al Total</td>
                                                 <td class="camposResumenOrden" id="pcParticipacionTotalNivel1"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionTotalNivel2"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionTotalNivel3"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionTotalNivel4"></td>
                                                 <td class="camposResumenOrden" id="pcParticipacionTotalNivel5"></td>
                                             </tr>
                                         </tbody>
                                     </table>
                                     <hr />
                                     <table class="table table-striped table-sm table-bordered text-center">
                                        <thead>
                                            <tr>
                                                 <th>Niveles</th>
                                                 <th>1</th>
                                                 <th>2</th>
                                                 <th>3</th>
                                                 <th>4</th>
                                                 <th>5</th>
                                                 <th>Total</th>
                                             </tr>
                                        </thead>
                                         <tbody>
                                             <tr>
                                                 <td>COSTO Fabricacion COLOMBIA</td>
                                                 <td class="camposResumenOrden" id="costoFab1"></td>
                                                 <td class="camposResumenOrden" id="costoFab2"></td>
                                                 <td class="camposResumenOrden" id="costoFab3"></td>
                                                 <td class="camposResumenOrden" id="costoFab4"></td>
                                                 <td class="camposResumenOrden" id="costoFab5"></td>
                                                 <td class="camposResumenOrden" id="costoFabTotal"></td>
                                             </tr>
                                             <tr>
                                                 <td>Factor para calcular COSTO DEL FLETE</td>
                                                 <td class="camposResumenOrden" id="costoFleteNivel1"></td>
                                                 <td class="camposResumenOrden" id="costoFleteNivel2"></td>
                                                 <td class="camposResumenOrden" id="costoFleteNivel3"></td>
                                                 <td class="camposResumenOrden" id="costoFleteNivel4"></td>
                                                 <td class="camposResumenOrden" id="costoFleteNivel5"></td>
                                                 <td></td>
                                             </tr>
                                             <tr>
                                                 <td>Costo Fab + Costo de Flete</td>
                                                 <td class="camposResumenOrden" id="costoFabFleteNivel1"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteNivel2"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteNivel3"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteNivel4"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteNivel5"></td>
                                                 <td id="costoTabFleteTotal"></td>
                                             </tr>
                                             <tr>
                                                 <!--<td>Factor para calcular MARGEN</td>-->
                                                 <td>Factor para calcular MARGEN Minimo</td>
                                                 <td class="camposResumenOrden" id="factorMargenNivel1"></td>
                                                 <td class="camposResumenOrden" id="factorMargenNivel2"></td>
                                                 <td class="camposResumenOrden" id="factorMargenNivel3"></td>
                                                 <td class="camposResumenOrden" id="factorMargenNivel4"></td>
                                                 <td class="camposResumenOrden" id="factorMargenNivel5"></td>
                                                 <td></td>
                                             </tr>
                                             <tr>
                                                 <!--<td>Costo Fab + Costo de Flete + Margen</td>-->
                                                 <td>Costo Fab + Costo de Flete + Margen Minimo</td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenNivel1"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenNivel2"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenNivel3"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenNivel4"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenNivel5"></td>
                                                 <td id="costoTabFleteMarTotal"></td>
                                             </tr>
                                             <tr>
                                                 <td>Factor para calcular IMP DE VENTA</td>
                                                 <td class="camposResumenOrden" id="factorImpVentaNivel1"></td>
                                                 <td class="camposResumenOrden" id="factorImpVentaNivel2"></td>
                                                 <td class="camposResumenOrden" id="factorImpVentaNivel3"></td>
                                                 <td class="camposResumenOrden" id="factorImpVentaNivel4"></td>
                                                 <td class="camposResumenOrden" id="factorImpVentaNivel5"></td>
                                                 <td></td>
                                             </tr>
                                             <tr>
                                                 <td>Costo Fab + Costo de Flete + Margen + IMP Minimo</td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenImpNivel1"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenImpNivel2"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenImpNivel3"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenImpNivel4"></td>
                                                 <td class="camposResumenOrden" id="costoFabFleteMargenImpNivel5"></td>
                                                 <td id="costoTabFleteMarImpTotal"></td>
                                             </tr>
                                             <tr>
                                                 <td>Tasa</td>
                                                 <td class="camposResumenOrden" id="tasaNivel1"></td>
                                                 <td class="camposResumenOrden" id="tasaNivel2"></td>
                                                 <td class="camposResumenOrden" id="tasaNivel3"></td>
                                                 <td class="camposResumenOrden" id="tasaNivel4"></td>
                                                 <td class="camposResumenOrden" id="tasaNivel5"></td>
                                                 <td></td>
                                             </tr>
                                             <tr>
                                                 <td>Precio venta Minimo</td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoNivel1"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoNivel2"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoNivel3"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoNivel4"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoNivel5"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoTotal"></td>
                                             </tr>
                                             <tr>
                                                 <td>Precio venta sugerido</td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoMNivel1"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoMNivel2"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoMNivel3"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoMNivel4"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoMNivel5"></td>
                                                 <td class="camposResumenOrden" id="precioVentaSugeridoMTotal"></td>
                                             </tr>
                                             <tr style="height: 20px; border-right: hidden; border-left: hidden;"></tr>
                                             <tr>
                                                 <td>Precio de venta COTIZADO</td>
                                                 <td class="camposResumenOrden" id="precioVentaCotizadoNivel1"></td>
                                                 <td class="camposResumenOrden" id="precioVentaCotizadoNivel2"></td>
                                                 <td class="camposResumenOrden" id="precioVentaCotizadoNivel3"></td>
                                                 <td class="camposResumenOrden" id="precioVentaCotizadoNivel4"></td>
                                                 <td class="camposResumenOrden" id="precioVentaCotizadoNivel5"></td>
                                                 <td class="camposResumenOrden" id="precioVentaCotizadoTotal"></td>
                                             </tr>
                                         </tbody>
                                     </table>
                                     <table class="table table-sm table-striped mt-3 table-bordered text-center">
                                         <tbody class="font-weight-bold">
                                         
                                         </tbody>
                                     </table>
                                 </div>
                            </div>
					        <div class="row">
						    <div class="col-6">
							    <table class="table table-sm table-hover" id="tab_Contenedores">
								    <thead class="thead-light">
									    <tr>
										    <th class="text-center" colspan ="7">CONTENEDORES</th>
									    </tr>
								    </thead>
								    <tbody id="tbody1">
									    <tr>
										    <th class="text-center" width="5%"> </th>
										    <th class="text-center" width="20%">20 Pies</th>
										    <th class="text-center" width="20%"><input type="number" id="txtContenedor20" class="NumeroSalcot" data-modelosc="vlr_Contenedor20" disabled="disabled"/> </th>
										    <th class="text-center" width="10"> </th>
										    <th class="text-center" width="20%">40 Pies</th>
										    <th class="text-center" width="20%"><input type="number" id="txtContenedor40" class="NumeroSalcot" data-modelosc="vlr_Contenedor40" disabled="disabled"/></th>
										    <th class="text-center" width="5%"> </th>
									    </tr>
<%--									    <tr>
										    <th class="text-center" > </th>
										    <th class="text-center" >Total FLETE</th>
										    <th class="text-center" ><input type="number" step="0.01" min="0" id="vrFleteLocalTotal" class="NumeroSalcot" disabled="disabled"/></th>
										    <th class="text-center" > </th>
									    </tr>
									    <tr>
										    <td colspan ="3" align="center" >
											    <button type="button" class="btn btn-primary btn-sm m-0 waves-effect fupsalco"  onclick="calcular_flete_loc()">
											    <i class="fa fa-save"></i> <span> Calcular Flete</span>
											    </button>
										    </td>
									    </tr>--%>
								    </tbody>
							    </table>
						    </div>
					    </div>
                        </div>
                       </div>
                </div>
        </div>

        <div class="row justify-content-center" style="color: red !important; font-size: 24px !important;display:none"
            id="divAdvertenciaCartaManual">
            <b><u>CARTA SUBIDA DE FORMA MANUAL, LOS DATOS NO COINCIDEN</u></b>
        </div>

        

    </div>
        <div id="buttonsContainer">
            <div class="row justify-content-center">
                <div class="" style = "margin-top: 15px">
                    <button type="button" id="btnCotizar" class="btn btn-primary fupmodfin" onclick="Cotizar()" style="display:none">
                        <i class="fa fa-cogs mr-1" style="padding-right: inherit;"></i><span data-i18n="[html]FUP_cotizar" html="FUP_cotizar">  Cargar Salida Cotizacion</span>
                    </button>
                    <button type="button" id="btnGuardarCot" class="btn btn-primary" onclick="GuardarCot()" style="display:none">
                        <i class="fa fa-save mr-1" style="padding-right: inherit;"></i><span data-i18n="[html]FUP_guardar" html="FUP_guardar">Guardar</span>
                    </button>
                    <button type="button" id="btnGuardarCalc" class="btn btn-success ml-2" onclick="GuardarCalc()" style="display:none">
                        <i class="fa fa-save mr-1" style="padding-right: inherit;"></i><span>Guardar Carta Comercial</span>
                    </button>                    
                    <button type="button" id="btnImprimir" class="btn btn-success ml-2" data-toggle="modal" onclick="ImprimirSC(0)" style="display:none">
                        <i class="fa fa-print mr-1" style="padding-right: inherit;"></i>Imprimir Carta
                    </button>
                    <button type="button" id="btnImprimirOrg" class="btn btn-success ml-2" data-toggle="modal" onclick="ImprimirSC(1)">
                        <i class="fa fa-print mr-1" style="padding-right: inherit;"></i>Carta Salida Original
                    </button>
                    <span class="ml-2" id="chxDetalle">
                        <input type="checkbox" id="txtDetalle" />
                        <label class="form-check-label" for="txtDetalle">Detallada</label>
                    </span>
                </div>
            </div >
        </div>
</asp:Content>
