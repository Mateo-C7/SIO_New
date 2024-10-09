<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormListadoItemsCosto.aspx.cs" Inherits="SIO.FormListadoItemsCosto" Culture="en-US" UICulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
    <script type="text/javascript" src="Scripts/umd/popper.min.js"></script>

    <script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>
    <script type="text/javascript" src="Scripts/formListadoItemsCosto.js?v=20221128"></script>
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

    <!-- Modal cargue rayas -->
    <div class="modal fade" id="cargueMensaje" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="exampleModalLabel">Resultado de la operación</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body" id="modalBodyCargueMensaje">
            ...
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>


    <div id="loader" style="display: none">
        <h3>Procesando...</h3>
    </div>
    <div id="ohsnap"></div>

    <div class="container-fluid contenedor_fup">
        
        <div class="card">
            <div class="row">
                <div class="col-10">
                    <table class="table table-sm table-hover" id="tbSearchFup">
                        <tbody>
                            <tr>
                                <td colspan="1" align="center" >ORDEN</td>
                                <td colspan="1" style="width: 90px;">
                                    <input id="txtOrden" type="text" class="form-control  bg-warning text-dark" />
                                </td>
                                <td colspan="1" align="center" >RAYA</td>
                                <td colspan="1" style="width: 90px;">
                                    <select id="cboRaya" class="form-control ">
								    </select>
                                </td>
                                <td colspan="1" style="width: 90px;">
                                    <button id="btnBusFup" class="btn btn-primary " data-toggle="tooltip" data-i18n="Cargar Todas Las Rayas"><i class="fa fa-search"></i></button>
                                </td>
                                <td colspan="1" align="center" >ERP DESTINO</td>
                                <td colspan="1" style="width: 150px;">
                                    <select id="cboDestino" class="form-control ">
                                        <option value="2" selected>ERP Pruebas </option>
                                        <option value="1">ERP Produccion</option>
								    </select>
                                </td>
                                <td colspan="1" >
                                    <button id="btnCargueItems" class="btn btn-primary " data-toggle="tooltip" onclick="cargarItems();"><i class="fa fa-cogs">  </i> Cargue desde Inventor</button>
                                </td>
                                <td colspan="1" >
                                    <button id="btnCargueErp" class="btn btn-success " data-toggle="tooltip" onclick="EnviarItemsErp();"><i class="fa fa-cogs">  </i> Actualizar Hoja Costos</button>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="col-2"></div>
            </div>
        </div>

        <div id="accordion">
            <div class="card" id="DatosGen">
                <div class="card-header">
                    <input type="checkbox" id="checkAll" >  </input>
                    <a class="collapsed card-link" data-toggle="collapse" href="#collapseDatosGenerales" data-i18n="FUP_datos_generales">ITEMS DE COSTO</a> 
                </div>
                <div id="collapseDatosGenerales" class="collapse show" data-parent="#accordion">
                    <div class="card-body">
                        <div id="DinamycSpace"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
