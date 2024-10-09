<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormMonedasEspecificacion.aspx.cs" Inherits="SIO.FormMonedasEspecificacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
		<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
		<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
		<script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js", 
                            "vue3-easy-data-table": "./Scripts/vue3-easy-data-table.umd.js" } }
		</script>
		<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
		<script type="text/javascript" src="Scripts/select2.min.js"></script>
		<script type="text/javascript" src="Scripts/toastr.min.js"></script>
		<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
		<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>

        <script type="text/javascript" src="Scripts/Datatables_Scripts/datatables.js"></script>
        <script type="module" src="Scripts/bootstrap-select.min.js"></script>
        <script type="module" src="Scripts/formMonedasEspecificacion.js"></script>

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
         <!--<div class="row">
             <div class="btn-group col align-self-end" role="group" aria-label="Basic example">
				<button type="button" class="btn btn-secondary langes">
					<img alt="español" src="Imagenes/colombia.png" /></button>
				<button type="button" class="btn btn-secondary langen">
					<img alt="ingles" src="Imagenes/united-states.png" /></button>
				<button type="button" class="btn btn-secondary langbr">
					<img alt="portugues" src="Imagenes/brazil.png" /></button>
			</div>
         </div>-->

		<div class="modal fade" id="modalEditarMoneda" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
			<div class="modal-dialog" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h5 class="modal-title" id="exampleModalLabel">Crear/Editar Moneda</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
							<span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Descripcion</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="currentEditingMoneda.Descripcion" type="text"/>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Simbolo</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="currentEditingMoneda.Simbolo" type="text"/>
							</div>
						</div>
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>ERP</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="currentEditingMoneda.ERP" type="text"/>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Sep. Decimal</label>
							</div>
							<div class="col-4">
								<input style="font-size: large !important; font-weight:bold" class="form-control" v-model="currentEditingMoneda.SeparadorDecimal" type="text" maxlength="1"/>
							</div>
						</div>
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Sep. Miles</label>
							</div>
							<div class="col-4">
								<input  style="font-size: large !important; font-weight:bold" class="form-control" v-model="currentEditingMoneda.SeparadorMiles" type="text" maxlength="1"/>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Cant Decimales</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="currentEditingMoneda.CantidadDecimales" type="number" step="1"/>
							</div>
						</div>
						<div class="row">
							<div class="col-2 font-weight-bold">
								<span>Estado</span>
							</div>
							<div class="col-4">
								<select class="form-control" id="selectEstado" data-live-search="true" v-model="currentEditingMoneda.Activo">
									<option value="true">Activo</option>
									<option value="false">Inactivo</option>
								</select>
							</div>
						</div>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
						<button type="button" class="btn btn-primary" v-on:click="guardarMoneda()"
							:disabled="currentEditingMoneda.Descripcion == ''">Guardar</button>
					</div>
				</div>
			</div>
        </div>

		<div class="row">
			<div class="col-12">
				<div class="alert alert-primary text-center" role="alert" style="align-content:center">
				  <h6>Administrar Monedas</h6>
				</div>
			</div>
		</div>

		<div class="row">
			<div class="col-12">
				<button class="btn btn-primary" type="button" @click="mostrarModalCreacion()">
					<i class="fa fa-plus" style="padding-right: inherit"></i>
					<span class="ml-1">Crear</span>
				</button>
			</div>
		</div>

		<div class="row mt-1">
			<div class="col-12" :key="index">
				<table id="monedas_table" class="table table-striped text-center table-sm">
					<thead>
						<tr>
							<th class="text-center">Descripcion</th>
							<th class="text-center">Estado</th>
							<th class="text-center">Simbolo</th>
							<th class="text-center">ERP</th>
							<th class="text-center">Sep. Decimal</th>
							<th class="text-center">Sep. Miles</th>
							<th class="text-center">Cant Decimales</th>
							<th></th>
						</tr>
					</thead>
					<tbody>
						<tr v-for="(moneda, index) in monedas">
							<td>{{ moneda.Descripcion }}</td>
							<td>{{ moneda.Activo ? 'Activo' : 'Inactivo' }}</td>
							<td>{{ moneda.Simbolo }}</td>
							<td>{{ moneda.ERP }}</td>
							<td style="font-size: large; font-weight: bold;">{{ moneda.SeparadorDecimal }}</td>
							<td style="font-size: large; font-weight: bold;">{{ moneda.SeparadorMiles }}</td>
							<td>{{ moneda.CantidadDecimales }}</td>
							<td>
								<button class="btn btn-primary" type="button" @click="mostrarModalEdicion(index)">
									<i class="fa fa-pencil" style="padding-right: inherit"></i>
									<span class="ml-1">Editar</span>
								</button>
							</td>
						</tr>
					</tbody>
				</table>
			</div>
		</div>

	</div>
</asp:Content>
