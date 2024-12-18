﻿<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormFleteInternacional.aspx.cs" Inherits="SIO.FormFleteInternacional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
		<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
		<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
		<script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                            "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js", 
                            "xlxs": "./Scripts/xlsx.js" } }
		</script>

		

		<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
		<script type="text/javascript" src="Scripts/select2.min.js"></script>
		<script type="text/javascript" src="Scripts/toastr.min.js"></script>
		<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
		<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>

        <script type="text/javascript" src="Scripts/Datatables_Scripts/datatables.js"></script>
        <script type="module" src="Scripts/bootstrap-select.min.js"></script>
        <script type="module" src="Scripts/formFleteInternacional.js"></script>

		<link rel="Stylesheet" href="Content/bootstrap.min.css" />
		<link rel="Stylesheet" href="Content/SIO.css" />
		<link rel="Stylesheet" href="Scripts/TableVue/style.css" />
		<link rel="stylesheet" href="Content/font-awesome.css" />
        <link rel="Stylesheet" href="Content/bootstrap-select.css" />
        <link rel="Stylesheet" href="Scripts/Datatables_Scripts/datatables.min.css" />
		<link rel="Stylesheet" href="Content/css/select2.min.css" />
		<link href="Content/toastr.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div id="loader" style="display: none">
			<h3>Procesando...</h3>
		</div>
		<div id="ohsnap"></div>

    <div class="container-fluid contenedor_fup border py-3" id="app">
         <div class="row">
             <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				<button type="button" class="btn btn-secondary langes">
					<img alt="español" src="Imagenes/colombia.png" /></button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" /></button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" /></button>
			</div>
         </div>

        <div class="modal fade" id="fleteInternacionalEdicionModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Crear/Editar Flete</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div class="row">
                    <div class="col-3 font-weight-bold">
                        <span>Agente Carga</span>
                    </div>
                    <div class="col-9">
                        <select class="form-control" id="selectAgenteCarga" data-live-search="true" v-model="fleteSeleccionado.AgenteCargaId"></select>
                    </div>
                </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Puerto Origen</span>
                      </div>
                      <div class="col-9">
                          <select class="form-control" id="selectPuertoOrigen" data-live-search="true" v-model="fleteSeleccionado.PuertoOrigenId"></select>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Puerto Destino</span>
                      </div>
                      <div class="col-9">
                          <select class="form-control" id="selectPuertoDestino" data-live-search="true" v-model="fleteSeleccionado.PuertoDestinoId"></select>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Tipo Contenedor</span>
                      </div>
                      <div class="col-9">
                          <select class="form-control" id="selectTipoContenedor" data-live-search="true" v-model="fleteSeleccionado.TipoContenedorId"></select>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Despacho Aduanal</span>
                      </div>
                      <div class="col-9">
                          <input type="number" step="0.01" class="form-control" v-model="fleteSeleccionado.DespachoAduanal"/>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Gastos Puerto Origen</span>
                      </div>
                      <div class="col-9">
                          <input type="number" step="0.01" class="form-control" v-model="fleteSeleccionado.GastosPuertoOrigen"/>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Valor Flete Internacional</span>
                      </div>
                      <div class="col-9">
                          <input type="number" step="0.01" class="form-control" v-model="fleteSeleccionado.FleteInternacionalValor"/>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Lead Time CIF</span>
                      </div>
                      <div class="col-9">
                          <input type="number" step="0.01" class="form-control" v-model="fleteSeleccionado.LeadTimeCIF"/>
                      </div>
                  </div>
                  <div class="row">
                      <div class="col-3 font-weight-bold">
                          <span>Estado</span>
                      </div>
                      <div class="col-9">
                          <select class="form-control" id="selectEstado" data-live-search="true" v-model="fleteSeleccionado.Estado">
                              <option value="1">Activo</option>
                              <option value="0">Inactivo</option>
                          </select>
                      </div>
                  </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="CrearActualizarFleteNacional()">Guardar</button>
              </div>
            </div>
          </div>
        </div>

         <!-- Modal -->
        <div class="modal fade" id="importarFletesModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title">Importar archivo</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <div>
                    <h6><b>Recuerda que debe ser un archivo de excel con el siguiente formato en las columnas...
                    (Agente Carga, Puerto Origen, Puerto Destino, Contenedor, Despacho Aduanal, Gastos Puerto Origen, 
                        Valor Flete, Lead Time CIF, Estado). El formato de columnas debe ser estricto para un correcto funcionamiento</b> <br />
                    </h6>
                    <span><a target="_blank" href="DownloadHandler.ashx?NombreArchivo=FleteInternacional_Plantilla.xls&Ruta=/TempFilesFletes/" style="font-size: 16px;">Formato de ejemplo</a></span>
                    <br />
                    <input type="file" class="form-control" id="filesImportar" ref="filesImportar" />
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-dismiss="modal" ref="CerrarModalImportarFletes">Cancelar</button>
                <button type="button" class="btn btn-primary" v-on:click="ImportarFletes()">Añadir</button>
              </div>
            </div>
          </div>
        </div>

        <div class="alert alert-primary font-weight-bold" role="alert" style="font-size: 18px !important;">
            Fletes Internacionales
        </div>
        <div class="mb-3">
            <div class="btn-group" role="group" aria-label="Basic example">
                <button type="button" v-on:click="LimpiarFleteSeleccionado()" class="btn btn-primary">Crear nuevo Flete Internacional</button>
                <button type="button" v-on:click="ExportarXLSFletes()" class="btn btn-secondary">Exportar</button>
                <button type="button" data-toggle="modal" data-target="#importarFletesModal" class="btn btn-primary">Importar</button>
            </div>
        </div>

        <div class="col-12">
            <div :key="index">
                <table class="table table-striped text-center" id="tableFleteInternacional">
                    <thead>
                        <tr>
                            <th>Registro</th>
                            <th>Agente Carga</th>
                            <th>Puerto Origen</th>
                            <th>Pais Puerto Destino</th>
                            <th>Puerto Destino</th>
                            <th>Contenedor</th>
                            <th>Despacho Aduanal</th>
                            <th>Gastos Puerto Origen</th>
                            <th>Flete Valor</th>
                            <th>Lead Time CIF</th>
                            <th>Estado</th>
                            <th>Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="fleteInternacional in fletesInternacionales">
                            <td>{{fleteInternacional.RegistroId}}</td>
                            <td>{{fleteInternacional.AgenteCargaNombre}}</td>
                            <td>{{fleteInternacional.PuertoOrigenNombre}}</td>
                            <td>{{fleteInternacional.PaisPuertoDestino}}</td>
                            <td>{{fleteInternacional.PuertoDestinoNombre}}</td>
                            <td>{{fleteInternacional.TipoContenedorNombre}}</td>
                            <td>${{fleteInternacional.DespachoAduanal}}</td>
                            <td>${{fleteInternacional.GastosPuertoOrigen}}</td>
                            <td>${{fleteInternacional.FleteInternacionalValor}}</td>
                            <td>{{fleteInternacional.LeadTimeCIF}}</td>
                            <td v-if="fleteInternacional.Estado == 1">Activo</td>
                            <td v-else>Inactivo</td>
                            <td>
                                <div class="btn-group" role="group" aria-label="Basic example">
                                    <button type="button" class="btn btn-primary" v-on:click="CargarInformacionEdicionFlete(fleteInternacional.RegistroId)">
                                        <i class="fa fa-pencil-square-o" style="padding-right: 12px;"></i></button>
                                    <button type="button" class="btn btn-danger" v-on:click="BorrarFleteInternacional(fleteInternacional.RegistroId)">
                                        <i class="fa fa-trash-o" style="padding-right: 12px;"></i></button>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>

    </div>
</asp:Content>
