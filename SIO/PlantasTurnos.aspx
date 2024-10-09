<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="PlantasTurnos.aspx.cs" Inherits="SIO.PlantasTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

    <%-- <script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>--%>
    <script type="text/javascript" src="Scripts/formPlantasTurnos.js?v=20230118"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/select2.min.js"></script>
    <script type="text/javascript" src="Scripts/toastr.min.js"></script>
    <script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>
    <link rel="Stylesheet" href="Content/bootstrap.min.css" />
    <link rel="Stylesheet" href="Content/SIO.css" />
    <link rel="Stylesheet" href="Content/PlantasTurnos.css" />
    <link rel="stylesheet" href="Content/font-awesome.css" />
    <link rel="Stylesheet" href="Content/css/select2.min.css" />
    <link href="Content/toastr.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div id="loader" style="display: none">
        <h3>Procesando...</h3>
    </div>
    <div id="ohsnap"></div>

    <div class="container-fluid contenedor_fup">


        <div class="row ">
            <div class="col-1 right-block">Area</div>
            <div class="col-2 center-block">
                <select id="selectPlanta"></select>
            </div>
            <div class="col-1 right-block">F. Inicial</div>
            <div class="col-2 center-block">
                <input id="txtfi" required type="date" />
            </div>
            <div class="col-1 right-block">Final</div>
            <div class="col-2 center-block">
                <input id="txtff" required type="date" />
            </div>
            <div class="col-1 center-block">
                <button id="btnConsultar" type="button" class="btn btn-success">Consultar</button>
            </div>
            <div class="col-1 center-block">
                <button id="btnInsertar" type="button" class="btn btn-primary" >Adicionar Orden</button>
            </div>
        </div>

        <div class="content-total">
            <div id="divHeadSemCalendar" class="container-sem d-flex flex-row justify-content-start align-items-start">
            </div>
            <div id="divHeaderCalendar" class="container-date d-flex flex-row justify-content-start align-items-start">
            </div>
            <div id="divContentCalendar" class="content-date d-flex flex-row justify-content-start align-items-start">
            </div>
        </div>
        <div class="row">
            <div class="col-4">
				<table class="table table-sm table-hover" id="tbTipoC">
                    <thead>
						<tr class="table-primary">
                            <th>Tipo Cotizacion</th>
                            <th>Proyectos</th>
                            <th>M2 SF</th>
                            <th>%</th>
	                    </tr>
                    </thead>
					<tbody id="tbTipoCbody">
					</tbody>
				</table>
			</div>
            <div class="col-4">
				<table class="table table-sm table-hover" id="tbProd">
                    <thead>
						<tr class="table-success">
                            <th>Producto</th>
                            <th>Proyectos</th>
                            <th>M2 SF</th>
                            <th>%</th>
	                    </tr>
                    </thead>
					<tbody id="tbProdbody">
					</tbody>
				</table>
			</div>
            <div class="col-4">
				<table class="table table-sm table-hover" id="tbSem">
                    <thead>
						<tr class="table-info">
                            <th>Semana</th>
                            <th>Proyectos</th>
                            <th>M2 SF</th>
                            <th>%</th>
	                    </tr>
                    </thead>
					<tbody id="tbSembody">
					</tbody>
				</table>
			</div>
        </div>
    </div>

    <!-- Modal Control de Cambios -->
    <div class="modal fade" id="ModInformacion" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Informacion</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-1">FUP</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoFup" disabled />
                        </div>
                        <div class="col-1">Tipo Cotizacion</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoTipoCot" disabled />
                        </div>
                        <div class="col-1">Tipo Proyecto</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoProy" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-1">Cliente</div>
                        <div class="col-11">
                            <input type="text" class="form-control" id="txtMoCliente" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-1">Obra</div>
                        <div class="col-11">
                            <input type="text" class="form-control" id="txtMoObra" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-1">Metros Cerrados</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoM2Cerr" disabled />
                        </div>
                        <div class="col-1">M2 Sf</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoM2Sf" disabled />
                        </div>
                        <div class="col-1">M2 Planeados</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoM2Plan" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-1">M2 Ingenieria</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoM2Ingenieria" disabled />
                        </div>
                        <div class="col-1">M2 Produccion</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoM2Prod" disabled />
                        </div>
                        <div class="col-1">Cerrado Ingenieria</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtCerrIng" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-1">Pendiente Ingenieria</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtPendIng" disabled />
                        </div>
                        <div class="col-1">Pendiente Produccion</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtPendProd" disabled />
                        </div>
                        <div class="col-1">Pendiente Embalaje</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtPendEmba" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-1">Fecha Programada</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoFecha" disabled />
                            <input type="hidden" id="IdPlan" />
                        </div>
                        <div class="col-1">Fecha Inicia</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtMoFecIni" disabled />
                        </div>
                        <div class="col-1">% Orden</div>
                        <div class="col-3">
                            <input type="text" class="form-control" id="txtPorcOrd" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-1">Fecha Validacion</div>
                        <div class="col-3">
                            <input type="date" class="form-control" id="txtFecVal" />
                        </div>
                        <div class="col-1">Curado</div>
                        <div class="col-1">
                            <input type="checkbox" class="form-control" id="txtCurado" style="width:20px;height:20px;" disabled/> 
                        </div>
                        <div class="col-1">Codigo QR</div>
                        <div class="col-1">
                            <input type="checkbox" class="form-control" id="txtQR" style="width:20px;height:20px;" disabled/> 
                        </div>
                        <div class="col-1">Prioritario</div>
                        <div class="col-3">
                            <input type="checkbox" class="form-control" id="txtPrioridad" style="width:20px;height:20px;" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="offset-4 col-3"><button id="btnGuardarDat" class="btn btn-sm btn-link align-left" data-toggle="tooltip" title="Guardar Validacion y Prioridad" onclick="guardarDat()"><i class="fa fa-save" ></i> </button></div>
                        <div class="col-3"><button id="btnBorraDat" class="btn btn-sm btn-link align-left" data-toggle="tooltip" title="Eliminar Item" onclick="eliminarDat()"><i class="fa fa-trash"></i> </button></div>
                        <div class="col-2"></div>
                        
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Generar Nuevo Proyecto -->
    <div class="modal fade" id="ModCreacionPlan" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-1">Orden </div>
                        <div class="col-11">
                               <select id="ListaOrden" class="form-control select-filter sfmodal"></select>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">Fecha Programada</div>
                        <div class="col-4">
                            <input type="date" class="form-control" id="txtFechaInicio" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCrearPlan" class="btn btn-primary small">Planear</button>
                </div>

            </div>
        </div>
    </div>

    <!-- Modal Control de Cambios -->
    <div class="modal fade" id="ModCreacionCronograma" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-2">Fecha Programada</div>
                        <div class="col-4">
                            <input type="hidden" id="hdnIDPlan" />
                            <input type="text" class="form-control" id="txtMoFechaChange" disabled />
                        </div>
                        <div class="col-2">M2 Planeados</div>
                        <div class="col-4">
                            <input type="text" class="form-control" id="txtMoM2PlanChange" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2">FUP</div>
                        <div class="col-4">
                            <input type="text" class="form-control" id="txtMoFupChange" disabled />
                        </div>
                        <div class="col-2">Tipo Cotizacion</div>
                        <div class="col-4">
                            <input type="text" class="form-control" id="txtMoTipoCotChange" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2">Tipo Proyecto</div>
                        <div class="col-4">
                            <input type="text" class="form-control" id="txtMoProyChange" disabled />
                        </div>
                        <div class="col-2">Metros Cerrados</div>
                        <div class="col-4">
                            <input type="text" class="form-control" id="txtMoM2CerrChange" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2">Cliente</div>
                        <div class="col-10">
                            <input type="text" class="form-control" id="txtMoClienteChange" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2">Obra</div>
                        <div class="col-10">
                            <input type="text" class="form-control" id="txtMoObraChange" disabled />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-2">Fecha</div>
                        <div class="col-4">
                            <input type="date" class="form-control" id="txtFechaChange" />
                        </div>
                        <div class="col-2">Cantidad</div>
                        <div class="col-4">
                            <input type="number" min="1" class="form-control" id="txtCantidadChange" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnGuardarPlan" class="btn btn-primary small">Guardar</button>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

