<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormFUP_SC.aspx.cs" Inherits="SIO.FormFUP_SC" Culture="en-US" UICulture="en-US" %>
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
    <script type="text/javascript" src="Scripts/formfupSC.js?v=20190306"></script>
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
            <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
                <button type="button" class="btn btn-secondary langes">
                    <img alt="español" src="Imagenes/colombia.png" /></button>
                <button type="button" class="btn btn-secondary langen">
                    <img alt="ingles" src="Imagenes/united-states.png" /></button>
                <button type="button" class="btn btn-secondary langbr">
                    <img alt="portugues" src="Imagenes/brazil.png" /></button>
                TEMPORAL SALIDA COTIZACION
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
                                        <option value="2">Plano</option>
                                        <option value="3">Documento</option>
                                        <option value="4">Fotografia</option>
                                        <option value="5">Plano Tipo Forsa</option>
                                        <option value="6">Carta de Cotizacion</option>
                                        <option value="7">Plano Final Cliente</option>
                                        <option value="8">Memoria</option>
                                        <option value="9">Carta Cotización Final</option>
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


        </div>
          <div class="card">
                <div class="row">
                    <div class="col-1"></div>
                    <div class="col-10">
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
                                    <%--<input id="btnNuevo" type="button" class="btn btn-primary  " value="Nuevo" data-i18n="[value]FUP_nuevo" />--%>
                                    <button id="btnFupBlanco" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_fup_blanco"><i class="fa  fa-file-text"></i></button>
                                    <%--<input id="btnFupBlanco" type="button" class="btn btn-primary " value="Fup Blanco" data-i18n="[value]FUP_fup_blanco" />--%>
                                    <button id="btnDuplicar" class="btn btn-primary " data-toggle="tooltip" data-i18n="[title]FUP_duplicar"><i class="fa fa-copy"></i></button>
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
                                </select> <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:200%; height:200%'  src='Imagenes/Clase de Cotizacion.JPG' class='pull-right'/></div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>-Equipo Nuevo Aluminio (Forsa Alum ó Forsa Plus): Son aquellos proyectos que trabajaran con formaleta de aluminio para todos sus detalles arquitectónicos indicados en los planos del cliente y en el FUP. <br/>- Adaptación: Son aquellos proyectos donde se reutilizaran equipos de formaletas de proyectos anteriores a un nuevo modelo de proyecto.<br/>-  Listados: Son aquellos que el cliente solicita basado en revisión e inventarios internos de su proceso para cubrir sus necesidades en el buen desempeño de su proyecto.</div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                            <div class="col-4">
                                <div class="row">
                                    <div class="col-3" data-i18n="[html]tipo_vaciado">Tipo de Vaciado</div>
                                    <div class="col-6" style="display: inline-table">
                                        <select id="selectTipoVaciado" data-modelo="TipoVaciado" style="width: 80% !important;">
                                            <option value="-1">Tipo Vaciado</option>
                                            <option value="1">MONOLITICO</option>
                                            <option value="2">MUROS Y LOSA EXTERIOR</option>
                                            <option value="3">UNICAMENTE MUROS</option>
                                            <option value="4">UNICAMENTE LOSA</option>
                                            <option value="5">N/A</option>
                                        </select>
                                        <%--<button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>- Monolitico: Es cuando se realiza armados de equipos que implica muros y losas, para vaciar/fundir unidades de vivienda en un solo evento.<br/>- Muros y Losa Posterior: Es cuando se arman formaletas de muro, para ser fundidos en una primera fase. en una segunda fase, se realiza el armado de las formaletas de losa sobre muros fundidos, para la fundición de esta.<br/>- Unicamente Muros: Es cuaando se funde los muros por medio de formaletas, pero la losa se realiza por de medio de otro sistema de construcción.<br/>-Unicamente Losas: Se presenta cuando los muros son construidos en otros y sobre estos armas las formaletas de losa y las apuntalan, para fundir la losa.</div>"><i class="fa fa-info-circle fa-lg"></i></button>--%>
                                        <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>- Monolitico: Es cuando se realiza armados de equipos que implica muros y losas, para vaciar/fundir unidades de vivienda en un solo evento.<br/>- Muros y Losa Posterior: Es cuando se arman formaletas de muro, para ser fundidos en una primera fase. en una segunda fase, se realiza el armado de las formaletas de losa sobre muros fundidos, para la fundición de esta.<br/>- Unicamente Muros: Es cuaando se funde los muros por medio de formaletas, pero la losa se realiza por de medio de otro sistema de construcción.<br/>-Unicamente Losas: Se presenta cuando los muros son construidos en  otros y sobre estos armas las formaletas de losa y las apuntalan, para fundir la losa.<br/>-Nota: Cuando sean solo columnas y vigas utilizar la descripción 'muros y losa posterior'</div>"><i class="fa fa-info-circle fa-lg"></i></button>																																																																																																																																																																																																																																																																									 
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-3" data-i18n="[html]termino_negociacion">Term. Negociacion</div>
                                    <div class="col-6">
                                        <select id="selectTerminoNegociacion" data-modelo="TerminoNegociacion" style="width: 80% !important;">
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-1 medium font-weight-bold text-center divvarof"> 
                                <span data-i18n="[html]orden_referencia">Orden de Referencia</span>
                                    <button id="btnAgregarOrdenReferencia" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i> </button>
                                </div>
                            <div class="col-6 divContentOrdenReferencia divvarof">
                                <div class="row ">
                                    <div class="col-1 text-center">#1</div>
                                    <div class="col-5">
                                        <input type="text" min="0" onblur="ValidarReferencia(this)" class="txtOrdenReferencia fuparr fuplist col-4" /> <span></span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row divvarof">
                                <div class="col-4"></div>
                                <div class="col-1">Otros</div>
                                <div class="col-2">
                                    <textarea id="txtOtros" class="form-control" rows="2" data-modelo="otros"></textarea>
                                </div>
                                <div class="col-1">
                                    <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>Observacion para agregar nuevos detalles de las adaptaciones</div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>Se refiere a la cantidad de niveles que contienen una edicicación veritical. Para el diligenciamiento de esta casilla, se debe registrar el numero de niveles del edificio mas alto a construir en el proyecto con el equipo.</div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]cantidad_fundiciones_piso">
                                Cantidad fundiciones piso:
                            </div>
                            <div class="col-2" style="display: inline-table">
                                <input style="width: 80% !important;" id="txtCantidadFundicionesPiso" class="fuparr" type="number" min="0" data-modelo="FundicionPisos" />
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>Se refiere a la cantidad de armados del equipo que se deben realizar en una planta o nivel de la edificación, para su fundición completa.</div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]nro_equipos">
                                # Equipos
                            </div>
                            <div class="col-2" style="display: inline-table">
                                <input style="width: 80% !important;" id="txtNroEquipos" type="number" min="0" class="" data-modelo="NumeroEquipos" />
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:60%; height:60%'src='Imagenes/8_img_No Equipos.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes/9_img_tipo de sistema de seguridad.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]alineacion_vertical">Alineacion Vertical</div>
                            <div class="col-2" style="display: inline-table">
                                <select style="width: 80% !important;" id="selectAlineacionVertical" class="fuparr" data-modelo="AlineacionVertical">
                                    <option value="-1">Seleccionar</option>
                                    <option value="1">CPC Liso</option>
                                    <option value="2">CPC Dilatado</option>
                                    <option value="3">AGR Liso</option>
                                    <option value="4">AGR Dilatado</option>
                                </select>
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div>- El AGR (Angulo de Arrastre) permite que la FM descanse sobre el ayudando a que no se derrame el concreto por la base de los paneles a partir del armado del segundo nivel y mejorar el plomo de los mismos.<br/>- El CPC (Panel de Ciclo) es un panel que se caracteriza por presentar refuerzos verticales con perforaciones que permiten la introducción de varilla roscada y chapola que actúan como anclaje en el concreto para permitir que dicho panel se mantenga fijo después del desencofre de las otras piezas, esto con el fin de apoyar las Formaletas de muro del siguiente.<br/>- Juntas de Entrepisos</div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-4"></div>
                        </div>

                        <%--<hr />--%>
                        <div class="row divarrlist">
                            <div class="col-2  medium font-weight-bold text-center">
                                <span data-i18n="[html]espesor_muro">Espesor Muro</span>
                                <button id="btnAgregarEspesorMuro" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
                            </div>
                            <div class="col-1">
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/11_img_espesor de muro.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-2  medium font-weight-bold text-center">
                                <span data-i18n="[html]espesor_losas">Espesor Losa</span>
                                <button id="btnAgregarEspesorLosa" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
                            </div>
                            <div class="col-1">
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/12_img_espesor de losa.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-2  medium font-weight-bold text-center">
                                <span data-i18n="[html]enrase_puerta">Enrase puertas (cm)</span>
                                <button id="btnAgregarEnrasePuertas" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
                            </div>
                            <div class="col-1">
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:70%; height:70%'src='Imagenes/23_img_enrase puertas.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-2  medium font-weight-bold text-center">
                                <span data-i18n="[html]enrase_ventanas">Enrase Ventanas (cm)</span>
                                <button id="btnAgregarEnraseVentanas" class="btn btn-sm btn-link align-center fuparr fuplist fupgen fupgenpt0 " data-toggle="tooltip" data-i18n="[title]FUP_agregar"><i class="fa fa-plus-square" style="font-size:14px;"></i></button>
                            </div>
                            <div class="col-1">
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:70%; height:70%' src='Imagenes/24_img_enrase ventanas.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                        </div>

                        <div class="row divarrlist">
                            <div class="col-3 divContentoEspesorMuro">
                                <div class="row ">
                                    <div class="col-3 text-center"># 1</div>
                                    <div class="col-4">
                                        <input type="number" min="0" required class="txtValorMuro fuparr fuplist" />
                                    </div>
                                    <div class="col-3">
                                    </div>
                                </div>
                            </div>
                            <div class="col-3 divContentEspesorLosa">
                                <div class="row">
                                    <div class="col-3 text-center"># 1</div>
                                    <div class="col-4">
                                        <input type="number" min="0" required class="txtValorLosa fuparr fuplist" />
                                    </div>
                                    <div class="col-3">
                                    </div>
                                </div>
                            </div>
                            <div class="col-3 divContentEnrasePuertas">
                                <div class="row ">
                                    <div class="col-3 text-center"># 1</div>
                                    <div class="col-4">
                                        <input type="number" min="0" required class="txtEnrasePuertas fuparr fuplist" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-3 divContentEnraseVentanas">
                                <div class="row ">
                                    <div class="col-3 text-center"># 1</div>
                                    <div class="col-4">
                                        <input type="number" min="0" required class="txtEnraseVentanas fuparr fuplist" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr class="divarrlist" />

                        <div class="row divarrlist">
                            <div class="col-1" data-i18n="[html]altura_libre">Altura libre</div>
                            <div class="col-2" style="display: inline-table">
                                <select style="width: 80% !important;" id="selectAlturaLibre" class="fuparr fuplist" data-modelo="AlturaLibre"></select>
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/10_img_altulibre.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                            <div class="col-1">
                                <input id="txtAlturaInternaSugerida" data-modelo="AlturaIntSugerida" class="fuparr" type="text" />
                            </div>
                            <div class="col-1" data-i18n="[html]tipo_fm_fachada">Tipo FM Fachada</div>
                            <div class="col-2" style="display: inline-table">
                                <select style="width: 80% !important;" id="selectTipoFMFachada" data-modelo="TipoFachada" class="fuparr">
                                    <option value="-1">Seleccionar</option>
                                    <option value="1">Estandar</option>
                                    <option value="2">Alta</option>
                                </select>
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div> -Formaleta Estandar: son FM externas que para alcanzar la altura libre sumado el espesor de losa, requieren que se modulen CAP, como complementos para estas.<br/>- Formaleta Alta: Son FM externo que contemplan la altura del muro y el espesor de losa, eliminando asi el CAP como complemento.</div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]altura_cap">Altura de CAP</div>
                            <div class="col-2" style="display: inline-table">
                                <input style="width: 80% !important;" id="txtAlturaCap1" type="text" data-modelo="AlturaCAP1" class="fuparr" />
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img  style='width:80%; height:80%' src='Imagenes/21_img_altura de cap.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>

                        </div>

                        <div class="row divarrlist">
                            <div class="col-1" data-i18n="[html]tipo_union">Tipo Union:</div>
                            <div class="col-2" style="display: inline-table">
                                <input style="width: 80% !important;" id="txtTipoUnion" data-modelo="TipoUnion" class="fuparr" type="text" />
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:80%; height:80%' src='Imagenes/19_img_tipo union.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]detalle_union">Detalle Union</div>
                            <div class="col-2" style="display: inline-table">
                                <select style="width: 80% !important;" id="selectDetalleUnion" data-modelo="DetalleUnion" class="fuparr fupadap fuplist">
                                    <option value="-1">Seleccionar</option>
                                    <option value="1">Lisa</option>
                                    <option value="2">Dilatada</option>
                                    <option value="3">Cenefa</option>
                                </select>
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:70%; height:70%' src='Imagenes/20_img_detalle unión.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]altura_union">Altura Union:</div>
                            <div class="col-2" style="display: inline-table">
                                <input style="width: 80% !important;" id="txtAlturaUnion" data-modelo="AlturaUnion" class="fuparr" type="text" />
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/18_img_altunion.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <%--<div class="col-1">
                                <input id="txtAlturaCap2" type="text" data-modelo="AlturaCAP2" class="fuparr" />
                            </div>--%>
                        </div>

                        <%--<div class="row">
                            <div class="col-12 medium font-weight-bold text-center" data-i18n="[html]altura_cap">Altura de CAP</div>
                        </div>--%>

                        <div class="row alturacap1 divarrlist"></div>

                        <div class="row divarrlist">
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
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:70%; height:70%' src='Imagenes/26_img_forma de construccion.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-2" data-i18n="[html]distancia_minima_edificaciones">Dist. Min. Edificios / Vivienda (cm)</div>
                            <div class="col-2" style="display: inline-table">
                                <input style="width: 80% !important;" type="number" min="0" id="txtDistMinEdificaciones" class="fuparr fuplist" data-modelo="DistanciaEdifica" />
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="Texto de la Ayuda por definir"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-1" data-i18n="[html]desnivel">Desnivel</div>
                            <div class="col-2" style="display: inline-table">
                                <select style="width: 80% !important;" id="selectDesnivel" data-modelo="Desnivel" class="fuparr fuplist">
                                    <option value="-1">Seleccionar</option>
                                    <option value="1">Ascendente</option>
                                    <option value="2">Descendiente</option>
                                    <option value="3">No Aplica</option>
                                </select>
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/30_img_desnivel.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/28_img_presenta juntas de dilatacion entre muros-espesor entre juntas.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                            <div class="col-2 divEspesorJuntas" data-i18n="[html]espesor_juntas">Espesor entre juntas</div>
                            <div class="col-2 divEspesorJuntas" style="display: inline-table">
                                <input style="width: 80% !important;" type="number" min="0" id="txtEspesorJuntas" data-modelo="EspesorJunta" class="fuparr fuplist" />
                                <button data-tooltip-custom-classes="tooltip-large" type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="bottom" data-html="true" title="<div><img style='width:60%; height:60%' src='Imagenes/29_img_presenta juntas de dilatacion entre muros-espesor entre juntas.jpg' /></div>"><i class="fa fa-info-circle fa-lg"></i></button>
                            </div>
                        </div>
                        <hr class="divarrlist" />

                        <hr class="divarrlist" />

                        <div class="row">
                            <div class="col-10"></div>
                            <div class="col-2">
                                <button type="button" onclick="guardarFUP()" class="btn btn-primary fupgen fupgenpt0 " >
                                    <i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar"></span>
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
                                            <table class="col-md-12">
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
                                                                                <button type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="top" data-html="true" title="<div align='left'><b>Ayuda</b><br/>- Juego de caps panel de ciclo<br/>- Trepante<br/>- Apartamentos planta tipo torre 1<br/>- Para realizar dos armados por nivel</div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                                                                    <button type="button" style="background-color: #bac0c5 !important;" id="add_Equipo" class="btn btn-secondary btn-block align-center fupgen fupgenpt1 " data-i18n="[html]AgregarEquipo">Agregar Equipo</button>
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
                                                                    <button type="button" role="button" class="btn  btn-link divAyuda" data-toggle="tooltip" data-placement="top" data-html="true" title="<div align='left'><b>Ayuda</b><br/>- Juego de caps panel de ciclo<br/>- Trepante<br/>- Apartamentos planta tipo torre 1<br/>- Para realizar dos armados por nivel</div>"><i class="fa fa-info-circle fa-lg"></i></button>
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
                                                                    <button type="button" style="background-color: #bac0c5 !important;" id="add_Adapta" class="btn btn-secondary btn-block fupgen fupgenpt1 " data-i18n="[html]AgregarAdaptacion">Agregar Adaptación</button>
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
                                <div class="row justify-content-start" style="margin-top: 15px; margin-left: 15px;">
                                    <button type="button" class="btn btn-default fupgen fupgenenv " data-toggle="modal" onclick="UploadFielModalShow('Cargar Archivo',0,'Alcance Oferta')">
                                        Subir Archivos
                                    </button>
                                </div>
                                <div class="row justify-content-start" style="margin-top: 15px; margin-left: 15px;">
                                    <button type="button"  class="btn btn-success fupgen fupgenenv " data-toggle="modal" onclick="ActualizarEstado(2)">
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
                            <div class="col-1" data-i18n="[html]FUP_numero_cambios">No. Cambios</div>
                            <div class="col-1">
                                <input id="txtNumeroCambios" type="number" min="0" style="width: 80% !important;" data-modelo-aprobacion="NumeroCambios" />
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_vobo">VoBo FUP</div>
                            <div class="col-2">
                                <select id="cboVoBoFup" class="form-control " data-modelo-aprobacion="Visto_bueno">
                                </select>
                            </div>
                            <div class="col-1" data-i18n="[html]FUP_motivo_rechazo">Motivo de rechazo</div>
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
                            <div class="col-4">
                                <%--<input id="Button1" type="button" class="btn btn-primary" value="Imprimir FUP" data-i18n="[value]FUP_imprimir_aprobacion" OnClientClick="Lista()"/>--%>

                                <button id="btnImprimirAprobacion" type="button" class="btn btn-primary" value="Imprimir FUP" onclick="PrepararReporteFUP();">
                                <i class="fa fa-print"></i> <span data-i18n="[html]FUP_imprimir_aprobacion" > impirmir</span>
                                </button>

                                <button id="btnGuardarAprobacion" type="button" class="btn btn-primary fupapro fupsalcie" value="Guardar y Notificar">
                                <i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar_aprobacion" > guardar</span>
                                </button> 
                            </div>
                            <div class="col-4"></div>
                        </div>

                        <hr />
                        <div class="row">
                            <div class="col-12  medium font-weight-bold text-center" data-i18n="[html]FUP_detalle_devoluciones">Detalle de devoluciones</div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <table class="table table-hover" id="tab_detalle_dev">
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
                                <table class="table table-hover" id="tab_anexos_fup">
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
                        <div class="col-2" data-i18n="[html]sc_m2_adicionales">M2, Adicionales</div>
                        <div class="col-2">
                            <input type="number" step="0.01" min="0" id="txtM2Adicionales" class="sumM2SalidaCot NumeroSalcot" data-modelosc="m2_adicionales" />
                        </div>
                        <div class="col-2">
                            <input type="number" step="0.01" min="0" id="txtValAdicionales" class="sumValSalidaCot NumeroSalcot" data-modelosc="vlr_adicionales" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2"></div>
                        <div class="col-2" data-i18n="[html]sc_detalle_arq">M2, Detalles Arquitectonicos</div>
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
                    <%--Contenedores--%>
                    <div class="row">
                        <div class="col-6">
                            <table class="table table-hover" id="tab_Contenedores">
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
                            <table id="tbComentarios" class="table">
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
                        <div class="justify-content-start" style="margin-top: 15px; margin-left: 15px;">
<%--                                <button type="button" class="btn btn-default " data-toggle="modal" onclick="DescargarArchivo('')">
                                Ver Carta Cotizacion 
                            </button>
--%>                            </div>
                        <div class="col-6">
                            <table class="table table-hover" id="tab_anexos_salidaCot">
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
                                <table class="col-md-12">
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
                                        <button id="btnGuardardevsc" type="button" class="btn btn-primary fupcot fupave " onclick="guardarDevComercial()" value="Devolucion Comercial"  >
                                            <i class="fa fa-undo"></i> <span>Devolver</span>
                                        </button>
                                    </div>
                                    <div class="col-4"></div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12">
                                        <table class="table table-hover" id="tab_devolucionsc">
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
            <div class="card" id="ParteSolicitudRecotizacion">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapseSolicitudRecotizacion" data-i18n="[html]FUP_solicitud_recotizacion">Solicitud de Re-Cotización
                    </a>
                </div>
                <div id="collapseSolicitudRecotizacion" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                   
                            <div id="Div1" class="box-body" padding-top: 20px; margin-left: 15px; margin-right: 15px;">
                                <div class="row">
                                    <div class="col-1" data-i18n="[html]FUP_Estado">Estado</div>
                                    <div class="col-1">
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
                                    <div class="col-4"></div>
                                </div>
                                <div class="row">
                                    <div class="col-1" data-i18n="[html]FUP_observacion_aprobacion">Observación</div>
                                    <div class="col-8">
                                        <textarea id="txtObservacionRecotizacion" class="form-control" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-4">
                                        <button id="btnGuardarRecotizacion" type="button" class="btn btn-primary fupcot " value="Recotizar"  >
                                            <i class="fa fa-undo" ></i> <span data-i18n="[html]FUP_btnrecotiza">Recotizar</span>
                                        </button>
                                    </div>
                                    <div class="col-4"></div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-12">
                                        <table class="table table-hover" id="tab_recotizacion_fup">
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
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapsePlanosTipoForsa" >Definición Técnica del Proyecto - PTF's
                    </a>
                </div>
                <div id="collapsePlanosTipoForsa" class="collapse" data-parent="#accordion">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-1" data-i18n="[html]FUP_Evento">Evento</div>
                            <div class="col-3">
                                <select id="cboEstadoPlanoTipoForsa" class="form-control" data-PTF="Evento">
                                </select>
                            </div>
                            <div class="col-1 varPTFCom" data-i18n="[html]FUP_Planos">Planos</div>
                            <div class="col-2 varPTFCom">
                                <select id="cboPlanosPlanoTipoForsa" class="form-control" data-PTF="Plano">
                                </select>
                            </div>
                            <div class="col-4 justify-content-end">
                                    <button id="btnPTFListaCH" class="btn btn-primary " data-toggle="tooltip" title="Lista de Chequeo Planos"><i class="fa  fa-file-text"></i> </button>
                            </div>
                        </div>
                        <div class="row varPTFSopCom">
                            <div class="col-1" data-i18n="[html]FUP_responsable">Responsable S.C.</div>
                            <div class="col-3">
                                <select id="cboResponsablePlanoTipoForsa" class="form-control" data-PTF="Responsable">
                                </select>
                            </div>
                            <div class="col-2">Fecha Entrega Soporte Com.</div>
                            <div class="col-2">
                                <input id="dtFechaCierrePTF"  data-PTF="FechaCierre" type="date" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-1" data-i18n="[html]FUP_observacion_aprobacion">Observación</div>
                            <div class="col-8">
                                <textarea id="txtObservacionPFT" class="form-control" rows="3" data-PTF="Observacion"></textarea>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-4">
                                <button id="btnGuardarPTF" type="button" class="btn btn-primary" >
                                    <i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar">guardar</span>
                                </button>
                            </div>
                            <div class="col-6"></div>
                        </div>

                        <hr />

                        <div class="row">
                            <div class="col-12">
                                <table class="table table-hover" id="tab_ptf_fup">
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
                        <div class="row">
                            <div class="col-12">
                                <table class="table table-hover" id="tblAnexoPTF">
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
                        </div>

                    </div>
                </div>
            </div>
        
            <div class="card" id="ParteFletesFUP">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapseFletesFUP" >Fletes 
                    </a>
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
                                    <span id="IdTransp" class="label label-default" data-flete="transportador_id" style="display: none">Id</span>
                                    <input type="text" id="lblTransp" disabled="disabled"/>
                                </div>
                            </div>
							  
                            <div class="row">
                                <div class="col-1"></div>
                                <div class="col-2" >Agente de Carga Internacional</div>
                                <div class="col-2">
                                    <span id="IdAgentCarga" class="label label-default" data-flete="agente_carga_id" style="display: none">Id</span>
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
                        </div>

                        <div class="row">
                            <div class="col-6 justify-content-start"  style="margin-top: 15px; margin-left: 15px;">
                                <table  id="tbCotizaFlete" class="table table-sm table-hover table-borderless">
                                <thead class="thead-light">
                                    <tr>
                                        <th width="30%">COTIZACIÓN</th>
                                        <th width="35%"> </th>
                                        <th width="35%"> </th>
                                    </tr>
                                </thead>
                                <tbody id="tbody4">
                                    <tr>    
                                        <td width="30%">Valor Cotizado</td>
                                        <td width="35%">
                                            <input type="number" class="NumeroSalcot" id="txtTotalPropuestaComF" disabled="disabled"/>
                                        </td>
                                        <th width="35%"> </th>
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
                                    <tr id="filaLTipoTurbo">
                                        <td>
                                            <span id="LTipoTurbo">Turbo</span>
                                        </td>
                                        <td>
                                            <input type="number" class="NumeroSalcot" id="fletetxtCant3" data-flete="cantidad_t3"/>
                                        </td>
                                        <td>
                                            <input type="text" class="NumeroSalcot" id="lblVrTipo3" disabled="disabled" data-flete="vr_origen_t3"/>
                                        </td>                                        
                                    </tr>
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
                                    <tr id="filaMinimula">
                                        <td>
                                            <span id="LMinimula">Minimula</span>
                                        </td>
                                        <td>
                                            <input type="number" class="NumeroSalcot" id="fletetxtCant4" data-flete="cantidad_t4"/>
                                        </td>
                                        <td>
                                            <input type="text" class="NumeroSalcot" id="lblVrTipo4" disabled="disabled" data-flete="vr_origen_t4"/>
                                        </td>                                       
                                    </tr>
                                    <tr id="filaLtipo2">
                                        <td>
                                            <span id="LTipo2">Turbo</span>
                                        </td>
                                        <td>
                                            <input type="number" class="NumeroSalcot" id="fletetxtCant2" data-flete="cantidad_t2"/>
                                        </td>
                                        <td>
                                            <input type="text" class="NumeroSalcot" id="lblVrTipo2" disabled="disabled" data-flete="vr_origen_t2"/>
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
                                        <td>VALOR FLETE</td>
                                        <td>
                                            <input type="text" class="NumeroSalcot"  id="LVrFlete" disabled="disabled" />
                                        </td>
                                        <td></td>                                       
                                    </tr>
                                    <tr>
                                        <td>VALOR TOTAL</td>
                                        <td>
                                            <input type="text" class="NumeroSalcot" id="LVrTotalFlete" disabled="disabled"/>
                                        </td>
                                        <td></td>                                       
                                    </tr>
                                    <tr>
                                        <td colspan ="3" align="center">
                                            <button type="button" class="btn btn-primary btn-sm m-0 waves-effect"  onclick="calcular_flete()">
                                            <i class="fa fa-save"></i> <span> Calcular Flete</span>
                                            </button>
                                            <button type="button" class="btn btn-primary btn-sm m-0 waves-effect" onclick="guardar_flete(1)">
                                            <i class="fa fa-save"></i> <span> Guardar Flete</span>
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
                            <div class="col-2" >Gastos En Puerto Origen</div>
                            <div class="col-2"></div>
                            <div class="col-2">
                                <label id="VrGastPtoOrig" data-flete="vr_gastos_origen">0</label>
                            </div>
                        </div>
                        <div class="row varflete">
                            <div class="col-1"></div>
                            <div class="col-2" >Despacho Aduanal</div>
                            <div class="col-2"></div>
                            <div class="col-2">
                                <label id="VrDespAduana" data-flete="vr_aduana_origen">0</label>
                            </div>
                        </div>
                        <div class="row varflete">
                            <div class="col-1"></div>
                            <div class="col-2" >Seguro</div>
                            <div class="col-2"></div>
                            <div class="col-2">
                                <label id="VrSeguro" data-flete="vr_seguro">0</label>
                            </div>
                        </div>
                        <div class="row varflete">
                            <div class="col-1"></div>
                            <div class="col-2" >Total Flete Internacional</div>
                            <div class="col-2"></div>
                            <div class="col-2">
                                <label id="VrFleteInt" data-flete="vr_aduana_origen">0</label>
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
                        <div class="row varflete">
                            <div class="col-1"></div>
                            <div class="col-2" >
                                <label id="VrInternal1" data-flete="vr_internacional_t1">0</label>
                            </div>
                            <div class="col-2"></div>
                            <div class="col-2">
                                <label id="VrInternal2" data-flete="vr_internacional_t2">0</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card" id="ParteVentaCierreCotizacion">
                <div class="card-header">
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapseVentaCierreCotizacion" data-i18n="[html]FUP_venta_cierre_cotizacion">Venta - Cierre - Comercial
                    </a>
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
                                <button type="button" class="btn btn-primary fupcot fupprc " data-toggle="modal" onclick="GuardarVentaCierreComercial()">
                                    <i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="justify-content-start" style="margin-top: 15px; margin-left: 15px;">
                                <button type="button" class="btn btn-default fupcot fupprc " data-toggle="modal" onclick="UploadFielModalShow('Cargar Carta de Cierre',9,'PreCierre')">
                                    Subir Carta de Cierre
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="justify-content-start" style="margin-top: 15px; margin-left: 15px;">
                                <button type="button" class="btn btn-default fupcot fupprc " data-toggle="modal" onclick="UploadFielModalShow('Cargar Planos Aprobados',7,'PreCierre')">
                                    Subir Planos Aprobados  
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
                <a class="collapsed card-link" data-toggle="collapse" href="#collapseSolicitudFac" data-i18n="[html]sc_solicita_facturacion">Fechas - Solicitud Facturación
                </a>
            </div>
            <div id="collapseSolicitudFac" class="collapse" data-parent="#accordion">
                <div class="card-body">
                    <div class="row">
                        <div class="col-2" data-i18n="[html]sf_firmacontrato">Fecha Firma de Contrato *</div>
                        <div class="col-2">
                            <input id="dtfirmaContrato" type="date" />
                        </div>
                        <div class="col-2" data-i18n="[html]sf_Plazo">Fecha Plazo</div>
                        <div class="col-2">
                            <input type="number" id="dtPlazo" />
                        </div>
                        <div class="col-2" data-i18n="[html]sf_m2Cerrados">M2 Cerrados</div>
                        <div class="col-2">
                            <input type="number" class="NumeroSalcot" id="Numberm2Cerrados" disabled="disabled" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-2" data-i18n="[html]sf_FormalizaPago">Fecha Formalización de Pago *</div>
                        <div class="col-2">
                            <input id="dtFormalizaPago" type="date" />
                        </div>
                        <div class="col-2" ></div>
                        <div class="col-2">
                        </div>
                        <div class="col-2" data-i18n="[html]sf_TotalCierre">Vlr Total de Cierre</div>
                        <div class="col-2">
                            <input type="number" class="NumeroSalcot" id="NumberTotalCierre" disabled="disabled" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-2" data-i18n="[html]sf_fechacontractual">Fecha Contractual *</div>
                        <div class="col-2">
                            <input id="dtfechacontractual" type="date" />
                        </div>
                        <div class="col-2" data-i18n="[html]sf_Diasistecnica">Dias Asist. Técnica</div>
                        <div class="col-2">
                            <input type="number" id="NumberDiasistecnica" disabled="disabled" />
                        </div>
                        <div class="col-2" data-i18n="[html]sf_TotalFacturacion">Vlr Total Facturación</div>
                        <div class="col-2">
                            <input type="number" class="NumeroSalcot" id="NumberTotalFacturacion" disabled="disabled" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-2" ></div>
                        <div class="col-2">
                        </div>
                        <div class="col-2" data-i18n="[html]sf_Diasconsumidos">Dias Consumidos</div>
                        <div class="col-2">
                            <input type="number" id="NumberDiasconsumidos" disabled="disabled" />
                        </div>
                        <div class="col-2" >M2 Modulados</div>
                        <div class="col-2">
                            <input type="number" id="m2Modulados" class="NumeroSalcot" disabled="disabled"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2" ></div>
                        <div class="col-2">
                        </div>
                        <div class="col-2""></div>
                        <div class="col-2">
                        </div>
                        <div class="col-2" >Valor Total Modulado</div>
                        <div class="col-2">
                            <input type="number" id="vrTotalModulado" class="NumeroSalcot" disabled="disabled"/>
                        </div>
                    </div>

                    <div class="row justify-content-center">
                        <div class="" style="margin-top: 15px; margin-left: 15px;">
                            <button type="button" class="btn btn-primary fupave fupsf fupofa" onclick="GuardarFechaSolicitud()">
                                <i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
                            </button>
                        </div>
                    </div>

                    <div class="row">
                        <div class="justify-content-start" style="margin-top: 15px; margin-left: 15px;">
                            <button type="button" class="btn btn-success fupsf fupofa" data-toggle="modal" onclick="LlamarSF()">
                                Ir a solicitud de Facturación  
                            </button>
                        </div>
                    </div>

                </div>
            </div>
            </div>
        <div class="card" id="ParteOrdenFabricacion">
            <div class="card-header">
                <a class="collapsed card-link" data-toggle="collapse" href="#collapseOrdenFabricacion" data-i18n="[html]FUP_orden_de_fabricacion">Orden de Fabricación
                </a>
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
                                    <th data-i18n="[html]FUP_of_OEA">OEA</th>
                                    <th data-i18n="[html]FUP_of_Produccion">Producción</th>
                                    <th>M2</th>
                                    <th data-i18n="[html]FUP_of_Precio">Precio</th>
                                    <th data-i18n="[html]FUP_of_Ver">Ver</th>
                                    <th data-i18n="[html]FUP_of_parte">Parte</th>
                                    <th data-i18n="[html]FUP_of_Fecha">Fecha</th>
                                    <th data-i18n="[html]FUP_of_Responsable">Responsable</th>
                                    <th>M2 Prod</th>
                                    <th>M2 Dif</th>
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
                <a class="collapsed card-link" data-toggle="collapse" href="#collapseModCerrado" ">M2 Modulados Estimados para SF</a>
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

                    <div class="row justify-content-center">
                        <div class="" style="margin-top: 15px; margin-left: 15px;">
                            <button type="button" class="btn btn-primary  fupmodfin" onclick="guardar_salida_cot(2)">
                                <i class="fa fa-save"></i> <span data-i18n="[html]FUP_guardar" > guardar</span>
                            </button>
                        </div>
                    </div>

                </div>
            </div>
            </div>

       
        </div>   
    </div>

</asp:Content>

