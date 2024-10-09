<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FormAdminCotRapida.aspx.cs" Inherits="SIO.FormAdminCotRapida" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/jquery-3.0.0.min.js"></script>
		<script type="text/javascript" src="Scripts/PopperRefactored/Popper14.js"></script>
		<script type="text/javascript" src="Scripts/PluralRuleParser.js"></script>
		<script type="importmap">
			{ "imports": { "vue": "./Scripts/vue.esm-browser.js" } }
		</script>
		<script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
		<script type="text/javascript" src="Scripts/select2.min.js"></script>
		<script type="text/javascript" src="Scripts/toastr.min.js"></script>
		<script type="text/javascript" src="Scripts/loadingoverlay.min.js"></script>
		<script type="text/javascript" src="Scripts/moment.js?v=20200629"></script>

        <script type="text/javascript" src="Scripts/Datatables_Scripts/datatables.js"></script>
        <script type="module" src="Scripts/bootstrap-select.min.js"></script>
        <script type="module" src="Scripts/formadmincotrapida.js"></script>

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

		<!-- Modales -->
		<div class="modal fade" id="modalItem" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
			<div class="modal-dialog modal-lg" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h5 class="modal-title" id="exampleModalLabel">Crear/Editar Item</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
							<span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Cod Item *</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="item_editando.CodItem" type="text"/>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Item *</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="item_editando.Item" type="text"/>
							</div>
						</div>
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Grupo *</label>
							</div>
							<div class="col-4">
								<select class="form-control" v-model="item_editando.CodGrupo">
									<option value="-1">Seleccionar grupo</option>
									<option value="1">Aluminio</option>
									<option value="2">Accesorios</option>
									<option value="3">Sistema Seguridad</option>
								</select>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Descripcion *</label>
							</div>
							<div class="col-4">
								<textarea class="form-control" v-model="item_editando.Descripcion" rows="2" cols="">
								</textarea>
							</div>
						</div>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
						<button type="button" class="btn btn-primary" v-on:click="guardarItem()"
							:disabled="item_editando.Descripcion == '' || item_editando.CodGrupo == -1
							|| item_editando.CodItem == '' || item_editando.Item == ''">Guardar</button>
					</div>
				</div>
			</div>
		</div>

		<div class="modal fade" id="modalPrecio" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
			<div class="modal-dialog modal-lg" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h5 class="modal-title">Crear/Editar Precio</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
							<span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Item *</label>
							</div>
							<div class="col-4">
								<select class="form-control" data-live-search="true" id="selectItemCot" v-model="precio_editando.IdItemCotRapida">
								</select>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Pais *</label>
							</div>
							<div class="col-4">
								<select class="form-control" data-live-search="true" id="selectPais" v-model="precio_editando.IdPais">
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Porcentaje</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="precio_editando.Precio" type="number" step="0.1" max="100" min="0"/>
							</div>
						</div>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
						<button type="button" class="btn btn-primary" v-on:click="guardarPrecio()"
							:disabled="precio_editando.IdItemCotRapida == '' || precio_editando.IdPais == -1
							|| precio_editando.Precio == ''">Guardar</button>
					</div>
				</div>
			</div>
		</div>

		<div class="modal fade" id="modalFactorArmado" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
			<div class="modal-dialog modal-lg" role="document">
				<div class="modal-content">
					<div class="modal-header">
						<h5 class="modal-title">Crear/Editar Factor Armado</h5>
						<button type="button" class="close" data-dismiss="modal" aria-label="Close">
							<span aria-hidden="true">&times;</span>
						</button>
					</div>
					<div class="modal-body">
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Item *</label>
							</div>
							<div class="col-4">
								<select class="form-control" data-live-search="true" id="selectItemCotArmado" v-model="factor_armado_editando.IdItemCotRapida">
								</select>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Pais *</label>
							</div>
							<div class="col-4">
								<select class="form-control" data-live-search="true" id="selectPaisArmado" v-model="factor_armado_editando.IdPais">
								</select>
							</div>
						</div>
						<div class="row">
							<div class="col-2 font-weight-bold">
								<label>Tipo Vivienda *</label>
							</div>
							<div class="col-4">
								<select class="form-control" id="selectTipoVivienda" v-model="factor_armado_editando.IdTipoVivienda">
									<option value="-1">Seleccionar Tipo Vivienda</option>
									<option value="1">Apartamento</option>
									<option value="2">Casa</option>
								</select>
							</div>
							<div class="col-2 font-weight-bold">
								<label>Factor *</label>
							</div>
							<div class="col-4">
								<input class="form-control" v-model="factor_armado_editando.Factor" type="number" min="0"/>
							</div>
						</div>
					</div>
					<div class="modal-footer">
						<button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
						<button type="button" class="btn btn-primary" v-on:click="guardarFactorArmado()"
							:disabled="factor_armado_editando.IdItemCotRapida == -1 || factor_armado_editando.IdPais == -1
							|| factor_armado_editando.Factor == '' || factor_armado_editando.IdTipoVivienda == -1">Guardar</button>
					</div>
				</div>
			</div>
		</div>
		<!-- Fin Modales -->

		<!-- Tab links -->
		<div class="tab">
			<button class="tablinks" id="tablinks-items" onclick="SelectTab(event, 'items_tab')">Items</button>
			<button class="tablinks" onclick="SelectTab(event, 'precios_tab')">Porcentaje Sistema Seguridad</button>
			<button class="tablinks" onclick="SelectTab(event, 'factor_armado_tab')">Factor Armado</button>
		</div>
			
		<div id="items_tab" class="tabcontent">
			<div class="row">
				<div class="col-12">
					<div class="alert alert-primary text-center" role="alert" style="align-content:center">
					  <h6>Administración de Items - Cotización Rápida</h6>
					</div>
				</div>
			</div>

			<div class="row">
				<div class="col-12">
					<button class="btn btn-primary" type="button" @click="mostrarModalCreacionItem()">
						<i class="fa fa-plus" style="padding-right: inherit"></i>
						<span class="ml-1">Crear</span>
					</button>
				</div>
			</div>

			<div class="row mt-1">
				<div class="col-12" :key="index">
					<table id="items_table" class="table table-striped text-center table-sm">
						<thead>
							<tr>
								<th class="text-center">Codigo Item</th>
								<th class="text-center">Item</th>
								<th class="text-center">Grupo</th>
								<th class="text-center">Descripción</th>
								<th></th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="(item, index) in items">
								<td>{{ item.CodItem }}</td>
								<td>{{ item.Item }}</td>
								<td>{{ item.Grupo }}</td>
								<td>{{ item.Descripcion }}</td>
								<td>
									<button class="btn btn-primary" type="button" @click="mostrarModalEdicionItem(index)">
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

		<div id="precios_tab" class="tabcontent">
			<div class="row">
				<div class="col-12">
					<div class="alert alert-primary text-center" role="alert" style="align-content:center">
					  <h6>Administración de Porcentaje Sistema Seguridad - Cotización Rápida</h6>
					</div>
				</div>
			</div>

			<div class="row">
				<div class="col-12">
					<button class="btn btn-primary" type="button" @click="mostrarModalCreacionPrecio()">
						<i class="fa fa-plus" style="padding-right: inherit"></i>
						<span class="ml-1">Crear</span>
					</button>
				</div>
			</div>

			<div class="row mt-1">
				<div class="col-12" :key="index">
					<table id="precios_table" class="table table-striped text-center table-sm">
						<thead>
							<tr>
								<th class="text-center">Item</th>
								<th class="text-center">Pais</th>
								<th class="text-center">Porcentaje</th>
								<th></th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="(precio, index) in precios">
								<td>{{ precio.ItemCotRapida }}</td>
								<td>{{ precio.Pais }}</td>
								<td>{{ precio.Precio }}%</td>
								<td>
									<button class="btn btn-primary" type="button" @click="mostrarModalEdicionPrecio(index)">
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

		<div id="factor_armado_tab" class="tabcontent">
			<div class="row">
				<div class="col-12">
					<div class="alert alert-primary text-center" role="alert" style="align-content:center">
					  <h6>Administración de Factores de Armado - Cotización Rápida</h6>
					</div>
				</div>
			</div>

			<div class="row">
				<div class="col-12">
					<button class="btn btn-primary" type="button" @click="mostrarModalCreacionFactorArmado()">
						<i class="fa fa-plus" style="padding-right: inherit"></i>
						<span class="ml-1">Crear</span>
					</button>
				</div>
			</div>

			<div class="row mt-1">
				<div class="col-12" :key="index">
					<table id="factores_armado_table" class="table table-striped text-center table-sm">
						<thead>
							<tr>
								<th class="text-center">Item</th>
								<th class="text-center">Pais</th>
								<th class="text-center">Tipo Vivienda</th>
								<th class="text-center">Factor</th>
								<th></th>
							</tr>
						</thead>
						<tbody>
							<tr v-for="(factor_armado, index) in factores_armado">
								<td>{{ factor_armado.ItemCotRapida }}</td>
								<td>{{ factor_armado.Pais }}</td>
								<td>{{ factor_armado.TipoVivienda }}</td>
								<td>{{ factor_armado.Factor }}</td>
								<td>
									<button class="btn btn-primary" type="button" @click="mostrarModalEdicionFactorArmado(index)">
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
	</div>

	<script type="text/javascript">
		$(document).ready(function() {
			document.getElementById("items_tab").style.display = "block";
            document.getElementById("tablinks-items").className += " active";
		});

        function SelectTab(evt, tabName) {
            evt.preventDefault();
            var i, tabcontent, tablinks;
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }
            document.getElementById(tabName).style.display = "block";
            evt.currentTarget.className += " active";
            evt.stopPropagation();
        }
    </script>
</asp:Content>