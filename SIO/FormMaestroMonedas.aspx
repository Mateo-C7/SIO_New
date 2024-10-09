<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormMaestroMonedas.aspx.cs" Inherits="SIO.FormMaestroMonedas" %>
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
	<script type="text/javascript" src="Scripts/FormMaestroMonedas.js"></script>
	<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
	<script type="text/javascript" src="Scripts/toastr.min.js"></script>
	<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
    <script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>
	<link rel="Stylesheet" href="Content/bootstrap.min.css" />
	<link rel="Stylesheet" href="Content/SIO.css" />
	<link rel="stylesheet" href="Content/font-awesome.css" />
	<link rel="Stylesheet" href="Content/css/select2.min.css" />
    <link rel="Stylesheet" href="Scripts/Datatables_Scripts/datatables.min.css" />
	<link href="Content/toastr.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div id="title-container" class="row" style="margin: auto 0px !important;">
        <div class="col-12 font-weight-bold fondoazul" style="color: white; font-size: 12px;">
            Maestro Monedas - Conversión a Dólares
        </div>
    </div>
    
    <div id="money-table-container" class="row" style="margin: 0px 0px 0px 0px !important;">
        <div class="col-7 border border-primary" style="padding: 15px 15px !important;">
            <button type="button" class="btn btn-sm btn-outline-primary" 
                onclick="fillAndShowUpdateMoneyModal(0,0,0,0,0,0,0,'0')">Crear Moneda</button>
            <hr />
            <table id="table-money" class="table table-sm table-striped">
                <thead style="color:white !important; background-color: #1C5AB6 !important">
                    <tr>
                        <th>Moneda</th>
                        <th>Año</th>
                        <th>Mes</th>
                        <th>TRM</th>
                        <th>Periodo</th>
                        <th>Fecha Registro</th>
                        <th>Usuario</th>
                        <th>Editar</th>
                    </tr>
                </thead>
                <tbody id="data-money-tbody">

                </tbody>
            </table>
        </div>
    </div>

    <!-- Modal for creation and update -->
    <div class="modal fade" id="modalCreateAndUpdateMoney" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="modalCreateAndUpdateMoney-title"></h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modalCreateAndUpdateMoney-body">
            <div>
                <input type="hidden" id="modalCreateAndUpdateMoneyMoneyId"/>
                <div class="form-group row" style="margin: 7px 15px 0px 15px !important;">
                    <label for="modalCreateAndUpdateMoneyMoneytxt" class="col-2 col-form-label">Moneda</label>
                    <select class="form-control col-10" id="modalCreateAndUpdateMoneyCmb"></select>
                </div>
                <div class="form-group row" style="margin: 7px 15px 9px 15px !important;">
                    <label for="modalCreateAndUpdateMoneyAnioCmb" class="col-2 col-form-label">Año</label>
                    <select class="form-control col-10" id="modalCreateAndUpdateMoneyAnioCmb"></select>
                </div>
                <div class="form-group row" style="margin: 7px 15px 0px 15px !important;">
                    <label for="modalCreateAndUpdateMoneyMesCmb" class="col-2 col-form-label">Mes</label>
                    <select class="form-control col-10" id="modalCreateAndUpdateMoneyMesCmb"></select>
                </div>
                <div class="form-group row" style="margin: 7px 15px 7px 15px !important;">
                    <label for="modalCreateAndUpdateMoneyValorTRMtxt" class="col-2 col-form-label">Valor TRM</label>
                    <input type="number" step="0.01" class="form-control col-10" id="modalCreateAndUpdateMoneyValorTRMtxt"/>
                </div>
            </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" id="modalCreateAndUpdateMoneyBtnSubmit"
                onclick="CreateUpdateMoney()"></button>
          </div>
        </div>
      </div>
    </div>

    <!-- Custom styles for this page -->
    <style>
        .contenedorprincipal {
            width: 1300px !important;
            margin: 0px auto !important;
            padding-top: 50px !important;
        }
    </style>
</asp:Content>
